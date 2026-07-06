using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AtlantHarden.Services
{
    /// <summary>
    /// Prevents the tool from locking itself out. Several controls this app can enable
    /// (Attack Surface Reduction "block low-prevalence/untrusted-USB" rules, Controlled Folder
    /// Access) would otherwise block this unsigned, low-reputation executable from running
    /// again — or from writing its own backups — so the user could no longer revert.
    ///
    /// Before applying any settings we register THIS executable as an allowed app for those
    /// specific Defender features. The exclusions are narrow (this one .exe path only); they do
    /// NOT exempt the process from antivirus scanning, so the hardening value elsewhere stands.
    ///
    /// Note: the OS/Explorer SmartScreen hard-block level (ShellSmartScreenLevel = "Block") is
    /// deliberately kept out of the Recommended profile (see RecommendedProfile) so SmartScreen
    /// stays at the overridable "Warn" level and the user can always relaunch the tool.
    /// </summary>
    public static class SelfProtectionService
    {
        /// <summary>
        /// Allow-list this executable for ASR and Controlled Folder Access so applying the
        /// hardening profile cannot prevent the tool from running or writing backups later.
        /// Best-effort: silently no-ops if Defender is unavailable or managed.
        /// </summary>
        public static async Task EnsureToolAllowlistedAsync()
        {
            try
            {
                var exe = Environment.ProcessPath; // full path to AtlantHarden.exe
                if (string.IsNullOrWhiteSpace(exe)) return;

                // Add-MpPreference de-duplicates identical entries, so this is safe to repeat.
                // -AttackSurfaceReductionOnlyExclusions: exempt this file from ASR rules only.
                // -ControlledFolderAccessAllowedApplications: let it write to protected folders.
                var script =
                    "$ErrorActionPreference='SilentlyContinue';" +
                    $"$p='{exe.Replace("'", "''")}';" +
                    "try{ Add-MpPreference -AttackSurfaceReductionOnlyExclusions $p } catch {};" +
                    "try{ Add-MpPreference -ControlledFolderAccessAllowedApplications $p } catch {};";

                await RunPowerShellAsync(script);
            }
            catch
            {
                // Never let self-protection failure block the user's action.
            }
        }

        private static async Task RunPowerShellAsync(string command)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -NonInteractive -ExecutionPolicy Bypass -Command \"{command}\"",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };
                using var process = Process.Start(psi);
                if (process == null) return;
                await process.WaitForExitAsync();
            }
            catch
            {
                // ignore
            }
        }
    }
}
