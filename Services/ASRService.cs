using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AtlantHarden.Services
{
    public class ASRService
    {
        // ASR Rule GUIDs and their names
        public static readonly Dictionary<string, string> ASRRules = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "D4F940AB-401B-4EFC-AADC-AD5F3C50688A", "Block Office Child Processes" },
            { "75668C1F-73B5-4CF0-BB93-3ECF5CB7CC84", "Block Office Code Injection" },
            { "92E97FA1-2EDF-4476-BDD6-9DD0B4DDDC7B", "Block Win32 API from Office Macros" },
            { "3B576869-A4EC-4529-8536-B80A7769E899", "Block Executable Office Content" },
            { "5BEB7EFE-FD9A-4556-801D-275E5FFC04CC", "Block Obfuscated Scripts" },
            { "BE9BA2D9-53EA-4CDC-84E5-9B1EEEE46550", "Block Email Executable Content" },
            { "D3E037E1-3EB8-44C8-A917-57927947596D", "Block JS/VBS Downloaded Executables" },
            { "B2B3F03D-6A65-4F7B-A9C7-1C7EF74A9BA4", "Block Untrusted USB Processes" },
            { "C1DB55AB-C21A-4637-BB3F-A12568109D35", "Advanced Ransomware Protection" },
            { "9E6C4E1F-7D60-472F-BA1A-A39EF669E4B2", "Block Credential Stealing from LSASS" },
            { "01443614-CD74-433A-B99E-2ECDC07BFC25", "Block Low Prevalence Executables" },
            { "26190899-1602-49E8-8B27-EB1D0A1CE869", "Block Office Communication Child Processes" },
            { "7674BA52-37EB-4A4F-A9A1-F0F9A1619A2C", "Block Adobe Reader Child Processes" },
            { "E6DB77E5-3DF2-4CF1-B95A-636979351E5B", "Block WMI Event Subscription Persistence" },
            { "D1E49AAC-8F56-4280-B9BA-993A6D77406C", "Block PSExec/WMI Process Creation" },
            { "56A863A9-875E-4185-98A7-B882C64B5CE5", "Block Vulnerable Signed Drivers" },
            { "33DDEDF1-C6E0-47CB-833E-DE6133960387", "Block Safe Mode Reboot" },
            { "C0033C00-D16D-4114-A5A0-DC9B3A7D2CEB", "Block Impersonated System Tools" },
            { "A8F5898E-1DC8-49A9-9878-85004B8A61E6", "Block Webshell Creation" }
        };

        // ASR Action values
        public enum ASRAction
        {
            Disabled = 0,
            Enabled = 1,  // Block mode
            Audit = 2,
            Warn = 6,
            NotConfigured = -1
        }

        private Dictionary<string, ASRAction> _cachedASRStatus = new Dictionary<string, ASRAction>(StringComparer.OrdinalIgnoreCase);
        private bool _hasLoaded = false;

        /// <summary>
        /// Gets all ASR rules and their current status via PowerShell
        /// </summary>
        public async Task<Dictionary<string, ASRAction>> GetAllASRStatusAsync()
        {
            var result = new Dictionary<string, ASRAction>(StringComparer.OrdinalIgnoreCase);
            
            // Initialize all rules as NotConfigured
            foreach (var guid in ASRRules.Keys)
            {
                result[guid] = ASRAction.NotConfigured;
            }

            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"$prefs = Get-MpPreference; if ($prefs.AttackSurfaceReductionRules_Ids) { for ($i=0; $i -lt $prefs.AttackSurfaceReductionRules_Ids.Count; $i++) { Write-Output ('{0}={1}' -f $prefs.AttackSurfaceReductionRules_Ids[$i], $prefs.AttackSurfaceReductionRules_Actions[$i]) } }\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = Process.Start(psi);
                if (process == null) return result;

                var output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();

                // Parse the output
                var lines = output.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var parts = line.Split('=');
                    if (parts.Length == 2)
                    {
                        var guid = parts[0].Trim();
                        if (int.TryParse(parts[1].Trim(), out int action))
                        {
                            result[guid] = (ASRAction)action;
                        }
                    }
                }

                _cachedASRStatus = result;
                _hasLoaded = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error getting ASR status: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Check if a specific ASR rule is enabled (Block mode = 1)
        /// </summary>
        public async Task<bool> IsASRRuleEnabledAsync(string guid)
        {
            if (!_hasLoaded)
            {
                await GetAllASRStatusAsync();
            }

            if (_cachedASRStatus.TryGetValue(guid.ToUpperInvariant(), out var action))
            {
                return action == ASRAction.Enabled;
            }

            return false;
        }

        /// <summary>
        /// Get the action for a specific ASR rule
        /// </summary>
        public async Task<ASRAction> GetASRRuleActionAsync(string guid)
        {
            if (!_hasLoaded)
            {
                await GetAllASRStatusAsync();
            }

            if (_cachedASRStatus.TryGetValue(guid.ToUpperInvariant(), out var action))
            {
                return action;
            }

            return ASRAction.NotConfigured;
        }

        /// <summary>
        /// Enable an ASR rule (set to Block mode)
        /// </summary>
        public async Task<bool> EnableASRRuleAsync(string guid)
        {
            return await SetASRRuleActionAsync(guid, ASRAction.Enabled);
        }

        /// <summary>
        /// Disable an ASR rule
        /// </summary>
        public async Task<bool> DisableASRRuleAsync(string guid)
        {
            return await SetASRRuleActionAsync(guid, ASRAction.Disabled);
        }

        /// <summary>
        /// Set an ASR rule to a specific action
        /// </summary>
        public async Task<bool> SetASRRuleActionAsync(string guid, ASRAction action)
        {
            try
            {
                string command;
                
                if (action == ASRAction.Disabled || action == ASRAction.NotConfigured)
                {
                    // To disable/remove an ASR rule, we set it to Disabled (action 0)
                    // Using numeric value for more reliable behavior
                    command = $"Set-MpPreference -AttackSurfaceReductionRules_Ids {guid} -AttackSurfaceReductionRules_Actions 0";
                }
                else
                {
                    // For enabling, use numeric action values for reliability
                    var actionValue = action switch
                    {
                        ASRAction.Enabled => 1,
                        ASRAction.Audit => 2,
                        ASRAction.Warn => 6,
                        _ => 0
                    };
                    command = $"Add-MpPreference -AttackSurfaceReductionRules_Ids {guid} -AttackSurfaceReductionRules_Actions {actionValue}";
                }

                var psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{command}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = Process.Start(psi);
                if (process == null) return false;

                var error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                if (process.ExitCode != 0)
                {
                    Debug.WriteLine($"ASR set failed: {error}");
                    return false;
                }

                // Update cache
                _cachedASRStatus[guid.ToUpperInvariant()] = action;
                Debug.WriteLine($"ASR rule {guid} set to {action}");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error setting ASR rule: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Extract GUID from an ASR Apply command
        /// </summary>
        public static string? ExtractGuidFromCommand(string command)
        {
            if (string.IsNullOrEmpty(command)) return null;

            // Look for GUID pattern in the command
            foreach (var guid in ASRRules.Keys)
            {
                if (command.Contains(guid, StringComparison.OrdinalIgnoreCase))
                {
                    return guid;
                }
            }

            return null;
        }

        /// <summary>
        /// Clear the cached ASR status so it will be refreshed on next check
        /// </summary>
        public void ClearCache()
        {
            _cachedASRStatus.Clear();
            _hasLoaded = false;
        }

        /// <summary>
        /// Check if ASR is available on this system
        /// </summary>
        public static async Task<bool> IsASRAvailableAsync()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"Get-MpPreference | Out-Null; $?\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = Process.Start(psi);
                if (process == null) return false;

                var output = await process.StandardOutput.ReadToEndAsync();
                await process.WaitForExitAsync();

                return output.Trim().Equals("True", StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}
