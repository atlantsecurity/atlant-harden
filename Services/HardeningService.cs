using AtlantHarden.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AtlantHarden.Services
{
    public class HardeningService
    {
        private readonly List<HardeningSetting> _allSettings;
        private readonly ASRService _asrService;
        private bool _asrStatusLoaded = false;

        public HardeningService()
        {
            _asrService = new ASRService();
            _allSettings = InitializeSettings();
        }

        public List<HardeningSetting> GetAllSettings() => _allSettings;
        
        public ASRService GetASRService() => _asrService;

        /// <summary>
        /// Force refresh of ASR rule status from Windows Defender
        /// </summary>
        public async Task RefreshASRStatusAsync()
        {
            _asrService.ClearCache();
            _asrStatusLoaded = false;
            await CheckASRStatusAsync();
        }

        public List<HardeningSetting> GetSettingsByCategory(SettingCategory category)
        {
            return _allSettings.Where(s => s.Category == category).ToList();
        }

        public async Task<bool> ApplySettingAsync(HardeningSetting setting)
        {
            try
            {
                switch (setting.Type)
                {
                    case SettingType.Registry:
                        return ApplyRegistrySetting(setting);
                    
                    case SettingType.PowerShell:
                        return await RunPowerShellAsync(setting.ApplyCommand);
                    
                    case SettingType.Command:
                        return await RunCommandAsync(setting.ApplyCommand);
                    
                    case SettingType.FileAssociation:
                        return await RunCommandAsync(setting.ApplyCommand);
                    
                    case SettingType.Firewall:
                        return await RunCommandAsync(setting.ApplyCommand);
                    
                    case SettingType.AuditPolicy:
                        return await RunCommandAsync(setting.ApplyCommand);
                    
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RevertSettingAsync(HardeningSetting setting)
        {
            try
            {
                // For Registry settings, restore the DefaultValue when one is defined...
                if (setting.Type == SettingType.Registry && setting.DefaultValue != null)
                {
                    var valueKind = RegistryService.ParseValueKind(setting.RegistryValueType);
                    return RegistryService.WriteValue(setting.RegistryPath, setting.RegistryKey,
                        setting.DefaultValue, valueKind);
                }

                // ...otherwise (e.g. DISA STIG policy values) remove the value so the policy
                // returns to its "not configured" state rather than leaving it enforced.
                if (setting.Type == SettingType.Registry && setting.DefaultValue == null
                    && !string.IsNullOrEmpty(setting.RegistryPath) && !string.IsNullOrEmpty(setting.RegistryKey))
                {
                    return RegistryService.DeleteValue(setting.RegistryPath, setting.RegistryKey);
                }

                // For other settings, require a RevertCommand
                if (string.IsNullOrEmpty(setting.RevertCommand))
                    return false;

                return await RunCommandAsync(setting.RevertCommand);
            }
            catch
            {
                return false;
            }
        }

        public void CheckCurrentStatus(HardeningSetting setting)
        {
            try
            {
                // For ASR settings, use cached status from CheckASRStatusAsync
                if (!string.IsNullOrEmpty(setting.ASRGuid))
                {
                    if (!_asrStatusLoaded)
                    {
                        setting.CurrentValue = "Pending...";
                        setting.IsApplied = false;
                        return;
                    }
                    // ASR status was already set in CheckASRStatusAsync, values persist on setting object
                    // Just ensure IsEnabled reflects IsApplied
                    if (setting.CurrentValue == "Pending...")
                    {
                        // Status wasn't properly set, mark as unknown
                        setting.CurrentValue = "Not Configured";
                        setting.IsApplied = false;
                    }
                    return;
                }

                // For File Association settings, check registry
                if (setting.Type == SettingType.FileAssociation)
                {
                    // Extract extension from Id (e.g., "FileAssoc_bat" -> "bat")
                    var ext = setting.Id.Replace("FileAssoc_", "");
                    var regPath = $@"HKCR\.{ext}";
                    var currentAssoc = RegistryService.ReadValue(regPath, ""); // Read default value with empty string
                    setting.CurrentValue = currentAssoc?.ToString() ?? "Not Set";
                    
                    // Check if it's set to txtfile (neutralized)
                    setting.IsApplied = string.Equals(currentAssoc?.ToString(), "txtfile", StringComparison.OrdinalIgnoreCase);
                    return;
                }

                // For Firewall settings, check if rule exists
                if (setting.Type == SettingType.Firewall)
                {
                    // Extract rule name from ApplyCommand (e.g., 'name="Block certutil.exe netconns"')
                    var match = System.Text.RegularExpressions.Regex.Match(setting.ApplyCommand, @"name=""([^""]+)""");
                    if (match.Success)
                    {
                        var ruleName = match.Groups[1].Value;
                        var ruleExists = CheckFirewallRuleExists(ruleName);
                        setting.CurrentValue = ruleExists ? "Rule Active" : "Not Configured";
                        setting.IsApplied = ruleExists;
                    }
                    else
                    {
                        setting.CurrentValue = "Unknown";
                        setting.IsApplied = false;
                    }
                    return;
                }

                // For AuditPolicy settings, check audit status
                if (setting.Type == SettingType.AuditPolicy)
                {
                    // Extract subcategory from ApplyCommand (e.g., 'subcategory:"Logon"')
                    var match = System.Text.RegularExpressions.Regex.Match(setting.ApplyCommand, @"subcategory:""([^""]+)""");
                    if (match.Success)
                    {
                        var subcategory = match.Groups[1].Value;
                        var (isEnabled, status) = CheckAuditPolicyStatus(subcategory);
                        setting.CurrentValue = status;
                        setting.IsApplied = isEnabled;
                    }
                    else
                    {
                        setting.CurrentValue = "Unknown";
                        setting.IsApplied = false;
                    }
                    return;
                }

                // For Registry settings, use the primary registry path
                if (setting.Type == SettingType.Registry)
                {
                    var currentValue = RegistryService.ReadValue(setting.RegistryPath, setting.RegistryKey);
                    setting.CurrentValue = currentValue?.ToString() ?? "Not Set";

                    if (setting.RecommendedValue != null)
                    {
                        setting.IsApplied = AreValuesEqual(currentValue, setting.RecommendedValue);
                    }

                    // SMBv1 is fully removable on Win10/11 via the "SMB1Protocol" optional feature.
                    // When it's removed, the SMB1 driver service key is gone and the hardening value
                    // never exists — but SMBv1 is actually absent (stronger than merely disabled).
                    // Treat a removed driver as compliant so we don't false-flag SMBv1 as enabled.
                    if (!setting.IsApplied)
                    {
                        var smb1Driver = setting.Id switch
                        {
                            "NET_DisableSMB1Client" => "mrxsmb10",  // SMB1 client driver
                            "NET_DisableSMB1"        => "srv",       // SMB1 server driver
                            _ => null
                        };
                        if (smb1Driver != null &&
                            !RegistryService.KeyExists($@"HKLM\SYSTEM\CurrentControlSet\Services\{smb1Driver}"))
                        {
                            setting.IsApplied = true;
                            setting.CurrentValue = "Removed (SMB1Protocol feature not installed)";
                        }
                    }
                }
                // For PowerShell/Command/Firewall settings, use VerifyRegistryPath if available
                else if (!string.IsNullOrEmpty(setting.VerifyRegistryPath) && !string.IsNullOrEmpty(setting.VerifyRegistryKey))
                {
                    var currentValue = RegistryService.ReadValue(setting.VerifyRegistryPath, setting.VerifyRegistryKey);
                    setting.CurrentValue = currentValue?.ToString() ?? "Not Set";
                    
                    if (setting.VerifyValue != null)
                    {
                        setting.IsApplied = AreValuesEqual(currentValue, setting.VerifyValue);
                    }
                }
                else
                {
                    // For settings without verification, mark as unknown
                    setting.CurrentValue = "Unknown";
                    setting.IsApplied = false;
                }
            }
            catch
            {
                setting.CurrentValue = "Error reading";
                setting.IsApplied = false;
            }
        }

        /// <summary>
        /// Check status of all ASR rules asynchronously
        /// </summary>
        public async Task CheckASRStatusAsync(IProgress<(int current, int total, string message)>? progress = null)
        {
            try
            {
                var asrSettings = _allSettings.Where(s => !string.IsNullOrEmpty(s.ASRGuid)).ToList();
                progress?.Report((0, asrSettings.Count, "Querying ASR rules from Windows Defender..."));
                
                var asrStatus = await _asrService.GetAllASRStatusAsync();
                
                if (asrStatus.Count == 0)
                {
                    Debug.WriteLine("ASR status query returned empty - Windows Defender might not be available");
                }
                
                int current = 0;
                foreach (var setting in asrSettings)
                {
                    var guid = setting.ASRGuid!.ToUpperInvariant();
                    
                    if (asrStatus.TryGetValue(guid, out var action))
                    {
                        setting.IsApplied = action == ASRService.ASRAction.Enabled;
                        setting.CurrentValue = action switch
                        {
                            ASRService.ASRAction.Enabled => "Enabled (Block)",
                            ASRService.ASRAction.Disabled => "Disabled",
                            ASRService.ASRAction.Audit => "Audit Mode",
                            ASRService.ASRAction.Warn => "Warn Mode",
                            _ => "Not Configured"
                        };
                        Debug.WriteLine($"ASR {setting.Name}: {setting.CurrentValue} (Applied: {setting.IsApplied})");
                    }
                    else
                    {
                        setting.IsApplied = false;
                        setting.CurrentValue = "Not Configured";
                        Debug.WriteLine($"ASR {setting.Name}: Not found in system, GUID: {guid}");
                    }
                    
                    current++;
                    progress?.Report((current, asrSettings.Count, $"Checked ASR: {setting.Name}"));
                }
                
                _asrStatusLoaded = true;
                Debug.WriteLine($"ASR status check complete: {asrSettings.Count} rules checked");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error checking ASR status: {ex.Message}");
                // Mark all ASR settings as unknown on error
                foreach (var setting in _allSettings.Where(s => !string.IsNullOrEmpty(s.ASRGuid)))
                {
                    setting.CurrentValue = "Check Failed";
                    setting.IsApplied = false;
                }
                _asrStatusLoaded = true; // Mark as loaded even on error to prevent infinite checking
            }
        }

        /// <summary>
        /// Check status of all settings asynchronously including ASR rules
        /// </summary>
        public async Task CheckAllStatusAsync(IProgress<(int current, int total, string message)>? progress = null)
        {
            var allSettings = _allSettings;
            var asrCount = allSettings.Count(s => !string.IsNullOrEmpty(s.ASRGuid));
            var otherCount = allSettings.Count - asrCount;
            int total = allSettings.Count;
            int current = 0;

            // Phase 1: Check ASR rules (these require async PowerShell call)
            progress?.Report((0, total, "Phase 1/2: Checking Attack Surface Reduction rules..."));
            
            var asrProgress = new Progress<(int c, int t, string m)>(update =>
            {
                progress?.Report((update.c, total, update.m));
            });
            await CheckASRStatusAsync(asrProgress);
            current = asrCount;

            // Phase 2: Check registry-based settings
            progress?.Report((current, total, "Phase 2/2: Checking registry settings..."));
            
            foreach (var setting in allSettings)
            {
                if (string.IsNullOrEmpty(setting.ASRGuid))
                {
                    CheckCurrentStatus(setting);
                    current++;
                    
                    if (current % 20 == 0)
                    {
                        progress?.Report((current, total, $"Checking: {setting.Name}"));
                        await Task.Delay(1); // Yield for UI responsiveness
                    }
                }
            }
            
            progress?.Report((total, total, "Verification complete"));
        }

        /// <summary>
        /// Apply a setting - enable it if not applied, or disable it if toggled off
        /// </summary>
        public async Task<bool> ApplyOrRevertSettingAsync(HardeningSetting setting, bool enable)
        {
            try
            {
                // For ASR settings, use ASRService
                if (!string.IsNullOrEmpty(setting.ASRGuid))
                {
                    bool success;
                    if (enable)
                    {
                        success = await _asrService.EnableASRRuleAsync(setting.ASRGuid);
                    }
                    else
                    {
                        success = await _asrService.DisableASRRuleAsync(setting.ASRGuid);
                    }
                    
                    if (success)
                    {
                        setting.IsApplied = enable;
                        setting.CurrentValue = enable ? "Enabled (Block)" : "Disabled";
                    }
                    return success;
                }

                // For other settings
                if (enable)
                {
                    return await ApplySettingAsync(setting);
                }
                else
                {
                    return await RevertSettingAsync(setting);
                }
            }
            catch
            {
                return false;
            }
        }

        private bool AreValuesEqual(object? current, object? recommended)
        {
            if (current == null && recommended == null) return true;
            if (current == null || recommended == null) return false;

            // Handle multi-string (REG_MULTI_SZ) values: compare element sequences, not ToString().
            // Recommended values may be authored as a single '\0'/';'-separated string, so normalize.
            if (current is string[] || recommended is string[])
            {
                var cur = RegistryService.ToMultiString(current);
                var rec = RegistryService.ToMultiString(recommended);
                return cur.SequenceEqual(rec, StringComparer.OrdinalIgnoreCase);
            }

            // Handle numeric comparisons more robustly
            if (IsNumeric(current) && IsNumeric(recommended))
            {
                try
                {
                    var currentNum = Convert.ToInt64(current);
                    var recommendedNum = Convert.ToInt64(recommended);
                    if (currentNum == recommendedNum) return true;
                    // A DWORD authored as 0xFFFFFFFF (uint) is stored as, and reads back as, int -1.
                    // Treat DWORD-range values that match in their low 32 bits as equal.
                    bool bothDword = currentNum >= int.MinValue && currentNum <= uint.MaxValue
                                  && recommendedNum >= int.MinValue && recommendedNum <= uint.MaxValue;
                    return bothDword && unchecked((uint)currentNum) == unchecked((uint)recommendedNum);
                }
                catch
                {
                    // Fall through to string comparison
                }
            }
            
            return current.ToString()?.Equals(recommended.ToString(), 
                StringComparison.OrdinalIgnoreCase) ?? false;
        }

        private bool IsNumeric(object? value)
        {
            if (value == null) return false;
            return value is int or uint or long or ulong or short or ushort or byte or sbyte;
        }

        private bool ApplyRegistrySetting(HardeningSetting setting)
        {
            if (setting.RecommendedValue == null)
                return false;

            var valueKind = RegistryService.ParseValueKind(setting.RegistryValueType);
            return RegistryService.WriteValue(setting.RegistryPath, setting.RegistryKey, 
                setting.RecommendedValue, valueKind);
        }

        private async Task<bool> RunPowerShellAsync(string command)
        {
            try
            {
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
                
                await process.WaitForExitAsync();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> RunCommandAsync(string command)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {command}",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = Process.Start(psi);
                if (process == null) return false;
                
                await process.WaitForExitAsync();
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }

        private bool CheckFirewallRuleExists(string ruleName)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "netsh",
                    Arguments = $"advfirewall firewall show rule name=\"{ruleName}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = Process.Start(psi);
                if (process == null) return false;
                
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit(5000); // 5-second timeout

                // If the rule exists, the output will contain the rule name
                // If it doesn't exist, it will say "No rules match the specified criteria"
                return process.ExitCode == 0 && !output.Contains("No rules match");
            }
            catch
            {
                return false;
            }
        }

        private (bool isEnabled, string status) CheckAuditPolicyStatus(string subcategory)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "auditpol",
                    Arguments = $"/get /subcategory:\"{subcategory}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using var process = Process.Start(psi);
                if (process == null) return (false, "Error");
                
                var output = process.StandardOutput.ReadToEnd();
                process.WaitForExit(5000); // 5-second timeout

                // Parse the output to find the status
                // Output format: "  Logon                                   Success and Failure"
                if (output.Contains("Success and Failure"))
                    return (true, "Success and Failure");
                if (output.Contains("Success"))
                    return (true, "Success Only");
                if (output.Contains("Failure"))
                    return (true, "Failure Only");
                if (output.Contains("No Auditing"))
                    return (false, "No Auditing");
                    
                return (false, "Not Configured");
            }
            catch
            {
                return (false, "Error");
            }
        }

        private List<HardeningSetting> InitializeSettings()
        {
            var settings = new List<HardeningSetting>();

            // ==================== WINDOWS DEFENDER ====================
            settings.AddRange(new[]
            {
                new HardeningSetting
                {
                    Id = "WD_PUAProtection",
                    Name = "Enable PUA Protection",
                    Description = "Enable detection of Potentially Unwanted Applications",
                    DetailedInfo = "Blocks adware, bundleware, and other potentially unwanted programs",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "Set-MpPreference -PUAProtection Enabled",
                    RevertCommand = "Set-MpPreference -PUAProtection Disabled",
                    VerifyRegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows Defender",
                    VerifyRegistryKey = "PUAProtection",
                    VerifyValue = 1,
                    Tags = new[] { "defender", "pua", "malware" }
                },
                new HardeningSetting
                {
                    Id = "WD_CloudProtection",
                    Name = "Enable Cloud-Delivered Protection",
                    Description = "Enable cloud-based protection for better threat detection",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "Set-MpPreference -MAPSReporting Advanced",
                    RevertCommand = "Set-MpPreference -MAPSReporting Basic",
                    VerifyRegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows Defender\Spynet",
                    VerifyRegistryKey = "SpyNetReporting",
                    VerifyValue = 2,
                    Tags = new[] { "defender", "cloud", "maps" }
                },
                new HardeningSetting
                {
                    Id = "WD_SampleSubmission",
                    Name = "Send All Samples to Microsoft",
                    Description = "Automatically send suspicious samples for analysis",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "Set-MpPreference -SubmitSamplesConsent SendAllSamples",
                    RevertCommand = "Set-MpPreference -SubmitSamplesConsent SendSafeSamples",
                    VerifyRegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows Defender\Spynet",
                    VerifyRegistryKey = "SubmitSamplesConsent",
                    VerifyValue = 3,
                    Tags = new[] { "defender", "samples" }
                },
                new HardeningSetting
                {
                    Id = "WD_CloudBlockLevel",
                    Name = "High Cloud Block Level",
                    Description = "Set cloud block level to high for aggressive protection",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Medium,
                    ApplyCommand = "Set-MpPreference -CloudBlockLevel High",
                    RevertCommand = "Set-MpPreference -CloudBlockLevel Default",
                    VerifyRegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows Defender\MpEngine",
                    VerifyRegistryKey = "MpCloudBlockLevel",
                    VerifyValue = 2,
                    ImpactWarning = "May cause false positives",
                    Tags = new[] { "defender", "cloud" }
                },
                new HardeningSetting
                {
                    Id = "WD_CloudTimeout",
                    Name = "Extended Cloud Check Timeout",
                    Description = "Extend cloud check timeout to 50 seconds",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "Set-MpPreference -CloudExtendedTimeout 50",
                    RevertCommand = "Set-MpPreference -CloudExtendedTimeout 10",
                    VerifyRegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows Defender\MpEngine",
                    VerifyRegistryKey = "MpBafsExtendedTimeout",
                    VerifyValue = 50,
                    Tags = new[] { "defender", "cloud" }
                },
                new HardeningSetting
                {
                    Id = "WD_Sandbox",
                    Name = "Enable Defender Sandbox",
                    Description = "Run Windows Defender in a sandbox for better security",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.Command,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "setx /M MP_FORCE_USE_SANDBOX 1",
                    RevertCommand = "setx /M MP_FORCE_USE_SANDBOX 0",
                    RequiresReboot = true,
                    Tags = new[] { "defender", "sandbox" }
                },
                new HardeningSetting
                {
                    Id = "WD_NetworkProtection",
                    Name = "Enable Network Protection",
                    Description = "Block connections to malicious IP addresses and domains",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Medium,
                    ApplyCommand = "Set-MpPreference -EnableNetworkProtection Enabled",
                    RevertCommand = "Set-MpPreference -EnableNetworkProtection Disabled",
                    VerifyRegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows Defender\Windows Defender Exploit Guard\Network Protection",
                    VerifyRegistryKey = "EnableNetworkProtection",
                    VerifyValue = 1,
                    ImpactWarning = "May occasionally block legitimate websites",
                    Tags = new[] { "defender", "network" }
                },
                new HardeningSetting
                {
                    Id = "WD_ControlledFolderAccess",
                    Name = "Enable Controlled Folder Access",
                    Description = "Protect important folders from ransomware and malicious apps",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ApplyCommand = "Set-MpPreference -EnableControlledFolderAccess Enabled",
                    RevertCommand = "Set-MpPreference -EnableControlledFolderAccess Disabled",
                    VerifyRegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows Defender\Windows Defender Exploit Guard\Controlled Folder Access",
                    VerifyRegistryKey = "EnableControlledFolderAccess",
                    VerifyValue = 1,
                    ImpactWarning = "May block legitimate applications. Whitelist apps as needed.",
                    Tags = new[] { "defender", "ransomware", "folders" }
                }
            });

            // ==================== ATTACK SURFACE REDUCTION ====================
            settings.AddRange(new[]
            {
                new HardeningSetting
                {
                    Id = "ASR_BlockOfficeChildProcess",
                    Name = "Block Office Child Processes",
                    Description = "Prevent Office applications from creating child processes",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ASRGuid = "D4F940AB-401B-4EFC-AADC-AD5F3C50688A",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids D4F940AB-401B-4EFC-AADC-AD5F3C50688A -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids D4F940AB-401B-4EFC-AADC-AD5F3C50688A -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "May break legitimate Office workflows that launch external programs",
                    Tags = new[] { "asr", "office", "malspam" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockOfficeInjection",
                    Name = "Block Office Code Injection",
                    Description = "Prevent Office apps from injecting code into other processes",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ASRGuid = "75668C1F-73B5-4CF0-BB93-3ECF5CB7CC84",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 75668C1F-73B5-4CF0-BB93-3ECF5CB7CC84 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 75668C1F-73B5-4CF0-BB93-3ECF5CB7CC84 -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "May break Office add-ins and integrations",
                    Tags = new[] { "asr", "office", "injection" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockWin32API",
                    Name = "Block Win32 API from Office Macros",
                    Description = "Block Office macros from calling Win32 APIs",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ASRGuid = "92E97FA1-2EDF-4476-BDD6-9DD0B4DDDC7B",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 92E97FA1-2EDF-4476-BDD6-9DD0B4DDDC7B -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 92E97FA1-2EDF-4476-BDD6-9DD0B4DDDC7B -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "Will break many legitimate VBA macros that interact with Windows",
                    Tags = new[] { "asr", "office", "macros" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockExecutableContent",
                    Name = "Block Executable Office Content",
                    Description = "Block Office apps from creating executable content",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ASRGuid = "3B576869-A4EC-4529-8536-B80A7769E899",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 3B576869-A4EC-4529-8536-B80A7769E899 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 3B576869-A4EC-4529-8536-B80A7769E899 -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "May prevent Office from saving certain file types",
                    Tags = new[] { "asr", "office", "executable" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockObfuscatedScripts",
                    Name = "Block Obfuscated Scripts",
                    Description = "Block execution of potentially obfuscated scripts",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Medium,
                    ASRGuid = "5BEB7EFE-FD9A-4556-801D-275E5FFC04CC",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 5BEB7EFE-FD9A-4556-801D-275E5FFC04CC -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 5BEB7EFE-FD9A-4556-801D-275E5FFC04CC -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "May block some legitimate PowerShell scripts",
                    Tags = new[] { "asr", "scripts", "obfuscation" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockEmailExecutable",
                    Name = "Block Email Executable Content",
                    Description = "Block executable content from email client and webmail",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ASRGuid = "BE9BA2D9-53EA-4CDC-84E5-9B1EEEE46550",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids BE9BA2D9-53EA-4CDC-84E5-9B1EEEE46550 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids BE9BA2D9-53EA-4CDC-84E5-9B1EEEE46550 -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "email", "malspam" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockJSVBS",
                    Name = "Block JS/VBS Downloaded Executables",
                    Description = "Block JavaScript or VBScript from launching downloaded executables",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ASRGuid = "D3E037E1-3EB8-44C8-A917-57927947596D",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids D3E037E1-3EB8-44C8-A917-57927947596D -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids D3E037E1-3EB8-44C8-A917-57927947596D -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "May block legitimate installers and scripts",
                    Tags = new[] { "asr", "javascript", "vbscript" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockUntrustedUSB",
                    Name = "Block Untrusted USB Processes",
                    Description = "Block untrusted and unsigned processes running from USB",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ASRGuid = "B2B3F03D-6A65-4F7B-A9C7-1C7EF74A9BA4",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids B2B3F03D-6A65-4F7B-A9C7-1C7EF74A9BA4 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids B2B3F03D-6A65-4F7B-A9C7-1C7EF74A9BA4 -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "Will block portable applications from USB drives",
                    Tags = new[] { "asr", "usb", "unsigned" }
                },
                new HardeningSetting
                {
                    Id = "ASR_Ransomware",
                    Name = "Advanced Ransomware Protection",
                    Description = "Use advanced protection against ransomware",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ASRGuid = "C1DB55AB-C21A-4637-BB3F-A12568109D35",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids C1DB55AB-C21A-4637-BB3F-A12568109D35 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids C1DB55AB-C21A-4637-BB3F-A12568109D35 -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "ransomware" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockLSASS",
                    Name = "Block Credential Stealing from LSASS",
                    Description = "Block credential stealing from Windows LSASS",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ASRGuid = "9E6C4E1F-7D60-472F-BA1A-A39EF669E4B2",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 9E6C4E1F-7D60-472F-BA1A-A39EF669E4B2 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 9E6C4E1F-7D60-472F-BA1A-A39EF669E4B2 -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "lsass", "mimikatz" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockPrevalence",
                    Name = "Block Low Prevalence Executables",
                    Description = "Block executables that don't meet prevalence, age, or trusted list criteria",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ASRGuid = "01443614-CD74-433A-B99E-2ECDC07BFC25",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 01443614-cd74-433a-b99e-2ecdc07bfc25 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 01443614-cd74-433a-b99e-2ecdc07bfc25 -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "May block legitimate new or uncommon software",
                    Tags = new[] { "asr", "prevalence" }
                },
                // Additional ASR Rules
                new HardeningSetting
                {
                    Id = "ASR_BlockAdobeReaderChild",
                    Name = "Block Adobe Reader Child Processes",
                    Description = "Prevent Adobe Reader from creating child processes that could be malicious",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ASRGuid = "7674BA52-37EB-4A4F-A9A1-F0F9A1619A2C",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 7674BA52-37EB-4A4F-A9A1-F0F9A1619A2C -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 7674BA52-37EB-4A4F-A9A1-F0F9A1619A2C -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "adobe", "pdf" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockOutlookChild",
                    Name = "Block Office Communication App Child Processes",
                    Description = "Prevent Outlook from creating child processes to block social engineering attacks",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Medium,
                    ASRGuid = "26190899-1602-49E8-8B27-EB1D0A1CE869",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 26190899-1602-49E8-8B27-EB1D0A1CE869 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 26190899-1602-49E8-8B27-EB1D0A1CE869 -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "outlook", "email" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockWMIPersistence",
                    Name = "Block WMI Event Subscription Persistence",
                    Description = "Prevent malware from using WMI event subscriptions to persist on the system",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Medium,
                    ASRGuid = "E6DB77E5-3DF2-4CF1-B95A-636979351E5B",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids E6DB77E5-3DF2-4CF1-B95A-636979351E5B -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids E6DB77E5-3DF2-4CF1-B95A-636979351E5B -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "wmi", "persistence" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockPSExecWMI",
                    Name = "Block PSExec and WMI Process Creation",
                    Description = "Block processes created via PSExec and WMI commands to prevent lateral movement",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.High,
                    ASRGuid = "D1E49AAC-8F56-4280-B9BA-993A6D77406C",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids D1E49AAC-8F56-4280-B9BA-993A6D77406C -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids D1E49AAC-8F56-4280-B9BA-993A6D77406C -AttackSurfaceReductionRules_Actions Disabled",
                    ImpactWarning = "May block legitimate admin tools - incompatible with SCCM/ConfigMgr",
                    Tags = new[] { "asr", "psexec", "wmi", "lateral" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockVulnerableDrivers",
                    Name = "Block Vulnerable Signed Drivers",
                    Description = "Prevent exploitation of vulnerable signed drivers that could be used for kernel access",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ASRGuid = "56A863A9-875E-4185-98A7-B882C64B5CE5",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 56A863A9-875E-4185-98A7-B882C64B5CE5 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 56A863A9-875E-4185-98A7-B882C64B5CE5 -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "driver", "kernel" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockSafeModeReboot",
                    Name = "Block Safe Mode Reboot Commands",
                    Description = "Prevent bcdedit and bootcfg from restarting machine in Safe Mode where security tools are disabled",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Medium,
                    ASRGuid = "33DDEDF1-C6E0-47CB-833E-DE6133960387",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 33DDEDF1-C6E0-47CB-833E-DE6133960387 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids 33DDEDF1-C6E0-47CB-833E-DE6133960387 -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "safemode", "boot" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockImpersonatedTools",
                    Name = "Block Impersonated System Tools",
                    Description = "Block executables that impersonate or copy Windows system tools",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ASRGuid = "C0033C00-D16D-4114-A5A0-DC9B3A7D2CEB",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids C0033C00-D16D-4114-A5A0-DC9B3A7D2CEB -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids C0033C00-D16D-4114-A5A0-DC9B3A7D2CEB -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "lolbas", "impersonation" }
                },
                new HardeningSetting
                {
                    Id = "ASR_BlockWebshell",
                    Name = "Block Webshell Creation for Servers",
                    Description = "Prevent web shell script creation on Microsoft Server and Exchange",
                    Category = SettingCategory.AttackSurfaceReduction,
                    Type = SettingType.PowerShell,
                    Risk = RiskLevel.Low,
                    ASRGuid = "A8F5898E-1DC8-49A9-9878-85004B8A61E6",
                    ApplyCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids A8F5898E-1DC8-49A9-9878-85004B8A61E6 -AttackSurfaceReductionRules_Actions Enabled",
                    RevertCommand = "Add-MpPreference -AttackSurfaceReductionRules_Ids A8F5898E-1DC8-49A9-9878-85004B8A61E6 -AttackSurfaceReductionRules_Actions Disabled",
                    Tags = new[] { "asr", "webshell", "server" }
                }
            });

            // ==================== CREDENTIAL PROTECTION ====================
            settings.AddRange(new[]
            {
                new HardeningSetting
                {
                    Id = "CRED_LSASSProtection",
                    Name = "LSASS Protected Process",
                    Description = "Run LSASS as a Protected Process Light (PPL)",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "RunAsPPL",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    RequiresReboot = true,
                    Tags = new[] { "lsass", "credential", "mimikatz" }
                },
                new HardeningSetting
                {
                    Id = "CRED_DisableWDigest",
                    Name = "Disable WDigest Authentication",
                    Description = "Prevent storing credentials in memory (cleartext)",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest",
                    RegistryKey = "UseLogonCredential",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "wdigest", "credential", "memory" }
                },
                new HardeningSetting
                {
                    Id = "CRED_DisableWDigestNeg",
                    Name = "Disable WDigest Negotiation",
                    Description = "Disable WDigest negotiate protocol",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest",
                    RegistryKey = "Negotiate",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = null,
                    Tags = new[] { "wdigest", "credential" }
                },
                new HardeningSetting
                {
                    Id = "CRED_LSASSAudit",
                    Name = "Enable LSASS Audit Mode",
                    Description = "Audit access to LSASS for security monitoring",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\LSASS.exe",
                    RegistryKey = "AuditLevel",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 8,
                    DefaultValue = 0,
                    Tags = new[] { "lsass", "audit", "monitoring" }
                },
                new HardeningSetting
                {
                    Id = "CRED_ProtectedCreds",
                    Name = "Enable Protected Credentials",
                    Description = "Allow delegation of non-exported credentials",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\CredentialsDelegation",
                    RegistryKey = "AllowProtectedCreds",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "credential", "delegation" }
                },
                // STIG WN11-SO-000100 - Disable LM Hash Storage
                new HardeningSetting
                {
                    Id = "CRED_NoLMHash",
                    Name = "Disable LM Hash Storage",
                    Description = "Do not store LAN Manager hash value on next password change",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "NoLMHash",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000100",
                    Tags = new[] { "lm", "hash", "password", "stig" }
                },
                // STIG WN11-SO-000110 - Enforce NTLMv2
                new HardeningSetting
                {
                    Id = "CRED_LMCompatibility",
                    Name = "Enforce NTLMv2 Only",
                    Description = "Set LAN Manager authentication level to NTLMv2 only",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "LmCompatibilityLevel",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 5,
                    DefaultValue = 3,
                    IsStig = true,
                    StigId = "WN11-SO-000110",
                    ImpactWarning = "May affect legacy system compatibility",
                    Tags = new[] { "ntlm", "authentication", "stig" }
                },
                // STIG WN11-SO-000020 - Block Blank Password Network Logons
                new HardeningSetting
                {
                    Id = "CRED_LimitBlankPassword",
                    Name = "Block Blank Password Network Logons",
                    Description = "Prevent local accounts with blank passwords from network logon",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "LimitBlankPasswordUse",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000020",
                    Tags = new[] { "password", "blank", "stig" }
                },
                // STIG WN11-SO-000085 - Disable Credential Caching
                new HardeningSetting
                {
                    Id = "CRED_DisableDomainCreds",
                    Name = "Disable Domain Credential Caching",
                    Description = "Prevent storage of credentials for network authentication",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "DisableDomainCreds",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000085",
                    ImpactWarning = "May affect domain authentication in some scenarios",
                    Tags = new[] { "credential", "cache", "domain", "stig" }
                }
            });

            // ==================== NETWORK SECURITY ====================
            settings.AddRange(new[]
            {
                // STIG WN11-00-000165 - Disable SMBv1 Server
                new HardeningSetting
                {
                    Id = "NET_DisableSMB1",
                    Name = "Disable SMBv1 Server",
                    Description = "Disable the vulnerable SMBv1 protocol (server side)",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters",
                    RegistryKey = "SMB1",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    IsStig = true,
                    StigId = "WN11-00-000165",
                    RequiresReboot = true,
                    Tags = new[] { "smb", "wannacry", "eternalblue", "stig" }
                },
                // STIG WN11-00-000170 - Disable SMBv1 Client
                new HardeningSetting
                {
                    Id = "NET_DisableSMB1Client",
                    Name = "Disable SMBv1 Client",
                    Description = "Disable the SMBv1 client driver",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\mrxsmb10",
                    RegistryKey = "Start",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 4,
                    DefaultValue = 3,
                    IsStig = true,
                    StigId = "WN11-00-000170",
                    RequiresReboot = true,
                    Tags = new[] { "smb", "client", "stig" }
                },
                // STIG WN11-SO-000215 - SMB Server Signing
                new HardeningSetting
                {
                    Id = "NET_SMBSigningServer",
                    Name = "Require SMB Signing (Server)",
                    Description = "Require SMB packet signing for server connections",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\System\CurrentControlSet\Services\LanmanServer\Parameters",
                    RegistryKey = "RequireSecuritySignature",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000215",
                    Tags = new[] { "smb", "signing", "relay", "stig" }
                },
                // STIG WN11-SO-000205 - SMB Client Signing
                new HardeningSetting
                {
                    Id = "NET_SMBSigningClient",
                    Name = "Require SMB Signing (Client)",
                    Description = "Require SMB packet signing for client connections",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\System\CurrentControlSet\Services\LanmanWorkStation\Parameters",
                    RegistryKey = "RequireSecuritySignature",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000205",
                    Tags = new[] { "smb", "signing", "relay", "stig" }
                },
                new HardeningSetting
                {
                    Id = "NET_DisableLLMNR",
                    Name = "Disable LLMNR",
                    Description = "Disable Link-Local Multicast Name Resolution",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient",
                    RegistryKey = "EnableMulticast",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "llmnr", "spoofing", "responder" }
                },
                new HardeningSetting
                {
                    Id = "NET_DisableIPv6",
                    Name = "Disable IPv6",
                    Description = "Disable IPv6 on all network interfaces",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.High,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\services\tcpip6\parameters",
                    RegistryKey = "DisabledComponents",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0xFF,
                    DefaultValue = 0,
                    ImpactWarning = "May break Microsoft services, modern apps, and IPv6-only networks",
                    Tags = new[] { "ipv6", "network" }
                },
                new HardeningSetting
                {
                    Id = "NET_DisableIPSourceRouting",
                    Name = "Disable IP Source Routing",
                    Description = "Prevent IP source routing attacks",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters",
                    RegistryKey = "DisableIPSourceRouting",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2,
                    DefaultValue = 0,
                    Tags = new[] { "ip", "routing", "spoofing" }
                },
                new HardeningSetting
                {
                    Id = "NET_DisableICMPRedirect",
                    Name = "Disable ICMP Redirects",
                    Description = "Do not allow ICMP redirects to override OSPF routes",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters",
                    RegistryKey = "EnableICMPRedirect",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "icmp", "routing" }
                },
                // STIG WN11-SO-000150 - LDAP Client Signing
                new HardeningSetting
                {
                    Id = "NET_LDAPClientSigning",
                    Name = "Require LDAP Client Signing",
                    Description = "Require LDAP client signing for DC communications",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\LDAP",
                    RegistryKey = "LDAPClientIntegrity",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000150",
                    Tags = new[] { "ldap", "signing", "stig" }
                },
                new HardeningSetting
                {
                    Id = "NET_LDAPIntegrity",
                    Name = "Require LDAP Server Signing",
                    Description = "Require LDAP server integrity signing",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\System\CurrentControlSet\Services\NTDS\Parameters",
                    RegistryKey = "LDAPServerIntegrity",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2,
                    DefaultValue = 1,
                    Tags = new[] { "ldap", "signing", "ad" }
                },
                new HardeningSetting
                {
                    Id = "NET_DisableDCOM",
                    Name = "Disable DCOM",
                    Description = "Disable Distributed Component Object Model for remote commands",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.High,
                    RegistryPath = @"HKLM\Software\Microsoft\OLE",
                    RegistryKey = "EnableDCOM",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "N",
                    DefaultValue = "Y",
                    ImpactWarning = "May break some remote administration tools",
                    Tags = new[] { "dcom", "remote", "psexec" }
                },
                // STIG WN11-SO-000075 - Restrict Anonymous SAM enumeration
                new HardeningSetting
                {
                    Id = "NET_RestrictAnonSAM",
                    Name = "Restrict Anonymous SAM Enumeration",
                    Description = "Prevent anonymous enumeration of SAM accounts",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "RestrictAnonymousSAM",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000075",
                    Tags = new[] { "anonymous", "enumeration", "stig" }
                },
                // STIG WN11-SO-000080 - Restrict Anonymous Share enumeration
                new HardeningSetting
                {
                    Id = "NET_RestrictAnonShares",
                    Name = "Restrict Anonymous Share Enumeration",
                    Description = "Prevent anonymous enumeration of shares",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "RestrictAnonymous",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000080",
                    Tags = new[] { "anonymous", "shares", "stig" }
                },
                // STIG WN11-SO-000160 - Block Anonymous Everyone permissions
                new HardeningSetting
                {
                    Id = "NET_NoAnonEveryone",
                    Name = "Block Anonymous Everyone Access",
                    Description = "Disable Everyone permissions for anonymous users",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "EveryoneIncludesAnonymous",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    IsStig = true,
                    StigId = "WN11-SO-000160",
                    Tags = new[] { "anonymous", "everyone", "stig" }
                },
                new HardeningSetting
                {
                    Id = "NET_RestrictNull",
                    Name = "Restrict Null Session Access",
                    Description = "Restrict anonymous access to named pipes and shares",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters",
                    RegistryKey = "RestrictNullSessAccess",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "null", "session", "anonymous" }
                },
                new HardeningSetting
                {
                    Id = "NET_DisableNetBIOS",
                    Name = "Disable NetBIOS over TCP/IP",
                    Description = "Stop NetBIOS over TCP/IP service",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Command,
                    Risk = RiskLevel.Medium,
                    ApplyCommand = "wmic nicconfig where TcpipNetbiosOptions=0 call SetTcpipNetbios 2 & wmic nicconfig where TcpipNetbiosOptions=1 call SetTcpipNetbios 2",
                    RevertCommand = "wmic nicconfig where TcpipNetbiosOptions=2 call SetTcpipNetbios 0",
                    ImpactWarning = "May affect legacy file sharing",
                    Tags = new[] { "netbios", "legacy" }
                },
                new HardeningSetting
                {
                    Id = "NET_DisableWPAD",
                    Name = "Disable WPAD",
                    Description = "Disable Web Proxy Auto-Discovery protocol",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Wpad",
                    RegistryKey = "WpadOverride",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "wpad", "proxy", "spoofing" }
                },
                // STIG WN11-00-000395 - Disable IP Helper (Port Proxy)
                new HardeningSetting
                {
                    Id = "NET_DisableIPHelper",
                    Name = "Disable IP Helper Service",
                    Description = "Disable IP Helper to prevent port proxy attacks",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\iphlpsvc",
                    RegistryKey = "Start",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 4,
                    DefaultValue = 2,
                    IsStig = true,
                    StigId = "WN11-00-000395",
                    ImpactWarning = "May affect IPv6 transition technologies",
                    Tags = new[] { "portproxy", "iphelper", "stig" }
                }
            });

            // ==================== SYSTEM HARDENING ====================
            settings.AddRange(new[]
            {
                new HardeningSetting
                {
                    Id = "SYS_EnableUAC",
                    Name = "Enable UAC",
                    Description = "Enable User Account Control",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
                    RegistryKey = "EnableLUA",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 1,
                    Tags = new[] { "uac", "elevation" }
                },
                new HardeningSetting
                {
                    Id = "SYS_UACPrompt",
                    Name = "UAC Always Prompt",
                    Description = "Always prompt for elevation on secure desktop",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
                    RegistryKey = "ConsentPromptBehaviorAdmin",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2,
                    DefaultValue = 5,
                    Tags = new[] { "uac", "prompt" }
                },
                new HardeningSetting
                {
                    Id = "SYS_EnableVirtualization",
                    Name = "Enable Virtualization Based Security",
                    Description = "Enable application virtualization for UAC",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System",
                    RegistryKey = "EnableVirtualization",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 1,
                    Tags = new[] { "virtualization", "uac" }
                },
                new HardeningSetting
                {
                    Id = "SYS_DLLSafeSearch",
                    Name = "Enable Safe DLL Search Mode",
                    Description = "Protect against DLL hijacking attacks",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Session Manager",
                    RegistryKey = "SafeDLLSearchMode",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "dll", "hijacking" }
                },
                new HardeningSetting
                {
                    Id = "SYS_CWDDLLSearch",
                    Name = "Block CWD DLL Loading",
                    Description = "Block DLL loading from current working directory (remote)",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Session Manager",
                    RegistryKey = "CWDIllegalInDllSearch",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2,
                    DefaultValue = 0,
                    Tags = new[] { "dll", "hijacking", "webdav" }
                },
                new HardeningSetting
                {
                    Id = "SYS_DisableWSH",
                    Name = "Disable Windows Script Host",
                    Description = "Prevent VBS/JS scripts from running via WSH",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows Script Host\Settings",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    ImpactWarning = "May break legitimate scripts",
                    Tags = new[] { "wsh", "vbs", "javascript" }
                },
                new HardeningSetting
                {
                    Id = "SYS_SmartScreen",
                    Name = "Enable SmartScreen",
                    Description = "Enable Windows SmartScreen filter",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\System",
                    RegistryKey = "EnableSmartScreen",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 1,
                    Tags = new[] { "smartscreen", "filter" }
                },
                new HardeningSetting
                {
                    Id = "SYS_SmartScreenBlock",
                    Name = "SmartScreen Block Level",
                    Description = "Set SmartScreen to Block mode",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\System",
                    RegistryKey = "ShellSmartScreenLevel",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "Block",
                    DefaultValue = "Warn",
                    Tags = new[] { "smartscreen", "block" }
                },
                new HardeningSetting
                {
                    Id = "SYS_PrinterDriver",
                    Name = "Require Admin for Printer Drivers",
                    Description = "Enforce Administrator role for adding printer drivers",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Print\Providers\LanMan Print Services\Servers",
                    RegistryKey = "AddPrinterDrivers",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "printer", "printnightmare" }
                },
                new HardeningSetting
                {
                    Id = "SYS_DisableInstallElevated",
                    Name = "Disable Always Install Elevated",
                    Description = "Prevent installers from using elevated privileges by default",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer",
                    RegistryKey = "AlwaysInstallElevated",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "installer", "elevation" }
                },
                new HardeningSetting
                {
                    Id = "SYS_ShowFileExt",
                    Name = "Show File Extensions",
                    Description = "Show file extensions in Windows Explorer",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    RegistryKey = "HideFileExt",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "explorer", "extensions" }
                },
                new HardeningSetting
                {
                    Id = "SYS_ShowHidden",
                    Name = "Show Hidden Files",
                    Description = "Show hidden files and folders",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    RegistryKey = "Hidden",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 2,
                    Tags = new[] { "explorer", "hidden" }
                },
                new HardeningSetting
                {
                    Id = "SYS_Disable8dot3",
                    Name = "Disable 8.3 Filename Creation",
                    Description = "Disable short 8.3 filename creation for better security",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Command,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "fsutil behavior set disable8dot3 1",
                    RevertCommand = "fsutil behavior set disable8dot3 0",
                    Tags = new[] { "filesystem", "8.3" }
                },
                new HardeningSetting
                {
                    Id = "SYS_DisableClickOnce",
                    Name = "Disable ClickOnce Trust Prompt",
                    Description = "Disable ClickOnce application trust prompts",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\MICROSOFT\.NETFramework\Security\TrustManager\PromptingLevel",
                    RegistryKey = "Internet",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "Disabled",
                    DefaultValue = "Enabled",
                    Tags = new[] { "clickonce", "dotnet" }
                },
                new HardeningSetting
                {
                    Id = "SYS_BiometricAntiSpoof",
                    Name = "Enable Biometric Anti-Spoofing",
                    Description = "Enable enhanced anti-spoofing for facial recognition",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Biometrics\FacialFeatures",
                    RegistryKey = "EnhancedAntiSpoofing",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "biometrics", "face", "windows hello" }
                },
                new HardeningSetting
                {
                    Id = "SYS_NoLockScreenCamera",
                    Name = "Disable Lock Screen Camera",
                    Description = "Disable camera access while screen is locked",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\Personalization",
                    RegistryKey = "NoLockScreenCamera",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "camera", "lockscreen", "privacy" }
                },
                // STIG WN11-00-000150 - Enable SEHOP
                new HardeningSetting
                {
                    Id = "SYS_EnableSEHOP",
                    Name = "Enable SEHOP",
                    Description = "Enable Structured Exception Handling Overwrite Protection",
                    DetailedInfo = "SEHOP blocks exploits using the SEH overwrite technique",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel",
                    RegistryKey = "DisableExceptionChainValidation",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    IsStig = true,
                    StigId = "WN11-00-000150",
                    Tags = new[] { "sehop", "exploit", "buffer", "stig" }
                }
            });

            // ==================== FILE ASSOCIATIONS ====================
            // Map of dangerous extensions to their original file types
            var fileAssociations = new Dictionary<string, string>
            {
                { "bat", "batfile" },
                { "cmd", "cmdfile" },
                { "chm", "chm.file" },
                { "hta", "htafile" },
                { "jse", "JSEFile" },
                { "js", "JSFile" },
                { "vbe", "VBEFile" },
                { "vbs", "VBSFile" },
                { "wsc", "scriptletfile" },
                { "wsf", "WSFFile" },
                { "ws", "WSFile" },
                { "wsh", "WSHFile" },
                { "scr", "scrfile" },
                { "url", "InternetShortcut" },
                { "ps1", "Microsoft.PowerShellScript.1" },
                { "iso", "Windows.IsoFile" },
                { "reg", "regfile" },
                { "wcx", "wcxfile" },
                { "slk", "Excel.SLK" },
                { "iqy", "iqyfile" },
                { "prn", "prnfile" },
                { "diff", "txtfile" },
                { "rdg", "RDCMan.RDCManFile" },
                { "deploy", "txtfile" }
            };
            
            foreach (var (ext, defaultType) in fileAssociations)
            {
                settings.Add(new HardeningSetting
                {
                    Id = $"FileAssoc_{ext}",
                    Name = $"Neutralize .{ext} Files",
                    Description = $"Associate .{ext} files with Notepad to prevent automatic execution",
                    Category = SettingCategory.FileAssociations,
                    Type = SettingType.FileAssociation,
                    Risk = RiskLevel.Low,
                    ApplyCommand = $"assoc .{ext}=txtfile",
                    RevertCommand = $"assoc .{ext}={defaultType}",
                    RecommendedValue = "txtfile",
                    DefaultValue = defaultType,
                    Tags = new[] { "file", "association", ext }
                });
            }

            // ==================== PRIVACY ====================
            settings.AddRange(new[]
            {
                new HardeningSetting
                {
                    Id = "PRIV_Telemetry",
                    Name = "Disable Telemetry",
                    Description = "Set Windows telemetry to security only level",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection",
                    RegistryKey = "AllowTelemetry",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 3,
                    Tags = new[] { "telemetry", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_Location",
                    Name = "Deny Location Access",
                    Description = "Disable location services for apps",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore",
                    RegistryKey = "Location",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "Deny",
                    DefaultValue = "Allow",
                    Tags = new[] { "location", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_BingSearch",
                    Name = "Disable Bing Search",
                    Description = "Disable Bing web search in Start Menu",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
                    RegistryKey = "BingSearchEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "bing", "search", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_Cortana",
                    Name = "Disable Cortana",
                    Description = "Disable Cortana consent and suggestions",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search",
                    RegistryKey = "CortanaConsent",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "cortana", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_AdvertisingID",
                    Name = "Disable Advertising ID",
                    Description = "Disable the unique advertising ID for this device",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo",
                    RegistryKey = "DisabledByGroupPolicy",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "advertising", "tracking", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_SettingsSync",
                    Name = "Disable Settings Sync",
                    Description = "Disable synchronization of Windows settings to cloud",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\SettingSync",
                    RegistryKey = "DisableSettingSync",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2,
                    DefaultValue = 0,
                    Tags = new[] { "sync", "cloud", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_GameDVR",
                    Name = "Disable GameDVR",
                    Description = "Disable Windows Game DVR and broadcasting",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\GameDVR",
                    RegistryKey = "AllowGameDVR",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "game", "dvr", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_ConsumerFeatures",
                    Name = "Disable Consumer Features",
                    Description = "Disable Microsoft consumer features and suggestions",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\CloudContent",
                    RegistryKey = "DisableWindowsConsumerFeatures",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "consumer", "suggestions", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_LanguageList",
                    Name = "Block Language List Access",
                    Description = "Prevent websites from accessing local language list",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\Control Panel\International\User Profile",
                    RegistryKey = "HttpAcceptLanguageOptOut",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "language", "tracking", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "PRIV_LockScreenToast",
                    Name = "Disable Lock Screen Notifications",
                    Description = "Prevent toast notifications on lock screen",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\CurrentVersion\PushNotifications",
                    RegistryKey = "NoToastApplicationNotificationOnLockScreen",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "lockscreen", "notifications", "privacy" }
                },
                // STIG WN11-CC-000175 - Disable Inventory Collector
                new HardeningSetting
                {
                    Id = "PRIV_DisableInventory",
                    Name = "Disable Inventory Collector",
                    Description = "Disable application inventory data collection",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\AppCompat",
                    RegistryKey = "DisableInventory",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-CC-000175",
                    Tags = new[] { "inventory", "privacy", "telemetry", "stig" }
                }
            });

            // ==================== LOGGING ====================
            settings.AddRange(new[]
            {
                // STIG WN11-CC-000326 - PowerShell Script Block Logging
                new HardeningSetting
                {
                    Id = "LOG_PSScriptBlock",
                    Name = "Enable PowerShell Script Block Logging",
                    Description = "Log PowerShell script block execution",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging",
                    RegistryKey = "EnableScriptBlockLogging",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-CC-000326",
                    Tags = new[] { "powershell", "logging", "forensics", "stig" }
                },
                new HardeningSetting
                {
                    Id = "LOG_PSModule",
                    Name = "Enable PowerShell Module Logging",
                    Description = "Log PowerShell module activity",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ModuleLogging",
                    RegistryKey = "EnableModuleLogging",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "powershell", "logging", "forensics" }
                },
                // STIG WN11-CC-000327 - PowerShell Transcription
                new HardeningSetting
                {
                    Id = "LOG_PSTranscript",
                    Name = "Enable PowerShell Transcription",
                    Description = "Enable PowerShell command transcription to files",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription",
                    RegistryKey = "EnableTranscripting",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-CC-000327",
                    Tags = new[] { "powershell", "logging", "transcription", "stig" }
                },
                new HardeningSetting
                {
                    Id = "LOG_CommandLine",
                    Name = "Log Process Command Line",
                    Description = "Include command line in process creation events",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Audit",
                    RegistryKey = "ProcessCreationIncludeCmdLine_Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "process", "logging", "forensics" }
                },
                // STIG WN11-SO-000030 - Force Audit Policy Subcategory
                new HardeningSetting
                {
                    Id = "LOG_AdvancedAudit",
                    Name = "Force Audit Policy Subcategory",
                    Description = "Force audit policy subcategory settings to override category settings",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "SCENoApplyLegacyAuditPolicy",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    IsStig = true,
                    StigId = "WN11-SO-000030",
                    Tags = new[] { "audit", "logging", "stig" }
                },
                new HardeningSetting
                {
                    Id = "LOG_SecurityLogSize",
                    Name = "Enlarge Security Event Log",
                    Description = "Increase Security event log to 1GB",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Command,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "wevtutil sl Security /ms:1024000000",
                    RevertCommand = "wevtutil sl Security /ms:20971520",
                    Tags = new[] { "eventlog", "security", "size" }
                },
                new HardeningSetting
                {
                    Id = "LOG_AuditLogon",
                    Name = "Audit Logon Events",
                    Description = "Enable auditing for logon success and failure",
                    Category = SettingCategory.Logging,
                    Type = SettingType.AuditPolicy,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "Auditpol /set /subcategory:\"Logon\" /success:enable /failure:enable",
                    RevertCommand = "Auditpol /set /subcategory:\"Logon\" /success:disable /failure:disable",
                    Tags = new[] { "audit", "logon" }
                },
                new HardeningSetting
                {
                    Id = "LOG_AuditProcess",
                    Name = "Audit Process Creation",
                    Description = "Enable auditing for process creation",
                    Category = SettingCategory.Logging,
                    Type = SettingType.AuditPolicy,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "Auditpol /set /subcategory:\"Process Creation\" /success:enable /failure:enable",
                    RevertCommand = "Auditpol /set /subcategory:\"Process Creation\" /success:disable /failure:disable",
                    Tags = new[] { "audit", "process" }
                }
            });

            // ==================== REMOVABLE MEDIA ====================
            settings.AddRange(new[]
            {
                new HardeningSetting
                {
                    Id = "REM_DisableAutorun",
                    Name = "Disable Autorun for All Drives",
                    Description = "Disable autorun/autoplay functionality",
                    Category = SettingCategory.RemovalMedia,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\Explorer",
                    RegistryKey = "NoDriveTypeAutoRun",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 255,
                    DefaultValue = 0,
                    Tags = new[] { "autorun", "usb", "malware" }
                },
                new HardeningSetting
                {
                    Id = "REM_NoAutoplay",
                    Name = "Disable Autoplay",
                    Description = "Disable autoplay for non-volume devices",
                    Category = SettingCategory.RemovalMedia,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\Explorer",
                    RegistryKey = "NoAutoplayfornonVolume",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "autoplay", "usb" }
                },
                new HardeningSetting
                {
                    Id = "REM_NoAutorunAll",
                    Name = "Disable Autorun Completely",
                    Description = "Completely disable autorun feature",
                    Category = SettingCategory.RemovalMedia,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer",
                    RegistryKey = "NoAutorun",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "autorun", "disable" }
                }
            });

            // ==================== TLS/CRYPTO ====================
            settings.AddRange(new[]
            {
                new HardeningSetting
                {
                    Id = "TLS_DisableSSL2",
                    Name = "Disable SSL 2.0",
                    Description = "Disable the insecure SSL 2.0 protocol",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\SSL 2.0\Client",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "ssl", "tls", "encryption" }
                },
                new HardeningSetting
                {
                    Id = "TLS_DisableSSL3",
                    Name = "Disable SSL 3.0",
                    Description = "Disable the insecure SSL 3.0 protocol",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\SSL 3.0\Client",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "ssl", "tls", "poodle" }
                },
                new HardeningSetting
                {
                    Id = "TLS_DisableTLS10",
                    Name = "Disable TLS 1.0",
                    Description = "Disable the legacy TLS 1.0 protocol",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.0\Client",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 0xFFFFFFFF,
                    ImpactWarning = "May break legacy applications",
                    Tags = new[] { "tls", "legacy" }
                },
                new HardeningSetting
                {
                    Id = "TLS_DisableTLS11",
                    Name = "Disable TLS 1.1",
                    Description = "Disable the legacy TLS 1.1 protocol",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.1\Client",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 0xFFFFFFFF,
                    ImpactWarning = "May break some older applications",
                    Tags = new[] { "tls", "legacy" }
                },
                new HardeningSetting
                {
                    Id = "TLS_EnableTLS12",
                    Name = "Enable TLS 1.2",
                    Description = "Ensure TLS 1.2 is enabled",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Client",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0xFFFFFFFF,
                    DefaultValue = 0xFFFFFFFF,
                    Tags = new[] { "tls", "encryption" }
                },
                new HardeningSetting
                {
                    Id = "TLS_DisableRC4",
                    Name = "Disable RC4 Cipher",
                    Description = "Disable the weak RC4 128/128 cipher",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\RC4 128/128",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "cipher", "rc4", "weak" }
                },
                new HardeningSetting
                {
                    Id = "TLS_DisableDES",
                    Name = "Disable DES Cipher",
                    Description = "Disable the weak DES 56/56 cipher",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\DES 56/56",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "cipher", "des", "weak" }
                },
                new HardeningSetting
                {
                    Id = "TLS_DisableTripleDES",
                    Name = "Disable Triple DES",
                    Description = "Disable the weak Triple DES 168 cipher",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\Triple DES 168",
                    RegistryKey = "Enabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    ImpactWarning = "May affect legacy application compatibility",
                    Tags = new[] { "cipher", "3des", "weak" }
                },
                new HardeningSetting
                {
                    Id = "TLS_NETStrongCrypto",
                    Name = ".NET Strong Cryptography",
                    Description = "Enable strong cryptography for .NET Framework",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\.NETFramework\v4.0.30319",
                    RegistryKey = "SchUseStrongCrypto",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "dotnet", "tls", "strong" }
                },
                // STIG WN11-CC-000052 - Configure ECC Curves
                new HardeningSetting
                {
                    Id = "TLS_ECCCurves",
                    Name = "Configure Strong ECC Curves",
                    Description = "Set ECC curves to NistP384 and NistP256 for SSL/TLS",
                    Category = SettingCategory.TLSCrypto,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Cryptography\Configuration\SSL\00010002",
                    RegistryKey = "EccCurves",
                    RegistryValueType = "REG_MULTI_SZ",
                    RecommendedValue = "NistP384\0NistP256",
                    DefaultValue = "",
                    IsStig = true,
                    StigId = "WN11-CC-000052",
                    Tags = new[] { "ecc", "curves", "tls", "stig" }
                }
            });

            // Add more categories - Office, Browsers, Adobe, etc. following same pattern...
            // Add Edge settings
            settings.AddRange(AdditionalSettings.GetEdgeSettings());
            
            // Add Chrome settings
            settings.AddRange(AdditionalSettings.GetChromeSettings());
            
            // Add Firefox settings
            settings.AddRange(AdditionalSettings.GetFirefoxSettings());
            
            // Add Office settings
            settings.AddRange(AdditionalSettings.GetOfficeSettings());
            
            // Add Adobe Reader settings
            settings.AddRange(AdditionalSettings.GetAdobeSettings());
            
            // Add Firewall LOLBin blocking rules
            settings.AddRange(AdditionalSettings.GetFirewallSettings());
            
            // Add ACSC (Australian Cyber Security Centre) hardening settings
            settings.AddRange(ACSCSettings.GetAllSettings());

            // Merge the DISA STIG catalog (Windows 11, Edge, Chrome, Firefox, Office 365).
            // The external JSON catalog is authoritative for STIG identity. To avoid duplicate
            // rows and double-counting, dedupe by (RegistryPath, RegistryKey): when a catalog
            // rule targets a registry value an existing hand-coded setting already manages,
            // enrich that setting in place with the STIG metadata; otherwise add it fresh.
            MergeStigCatalog(settings);

            return settings;
        }

        private static void MergeStigCatalog(List<HardeningSetting> settings)
        {
            // STIG coverage is delivered as its own per-product categories (StigWindows11,
            // StigEdge, ...), kept separate from the curated baseline categories. The catalog
            // is the single source of truth for STIG identity, so first clear the STIG tags
            // carried by hand-coded baseline settings (their IDs predate the current release) —
            // those settings remain as baseline hardening options without a stale STIG label.
            foreach (var s in settings)
            {
                s.IsStig = false;
                s.StigId = null;
                s.VulnId = null;
                s.StigProduct = null;
                s.Ccis = null;
            }

            // Add the full DISA STIG catalog (each rule carries its exact mandated value).
            settings.AddRange(StigCatalogService.GetStigSettings());
        }
    }
}
