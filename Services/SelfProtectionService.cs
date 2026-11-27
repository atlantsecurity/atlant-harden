using System;
using System.IO;
using System.Threading.Tasks;

namespace AtlantHarden.Services
{
    /// <summary>
    /// Prevents the tool from locking itself out. Several controls this app can enable
    /// (Attack Surface Reduction "block low-prevalence/untrusted-USB" rules, Controlled Folder
    /// Access) could otherwise block this executable from running again — or from writing its
    /// own backups — so the user could no longer revert.
    ///
    /// Allow-listing the exe where it was unzipped (usually Downloads) would exempt a path any
    /// unprivileged process can overwrite. Instead, a protected copy is kept under
    /// %ProgramFiles%\AtlantHarden (writable only by administrators), only THAT path is
    /// allow-listed, a Start-menu shortcut points to it, and stale allow-list entries for
    /// AtlantHarden.exe elsewhere are removed. The exclusions are ASR/CFA-only: they do not
    /// exempt the file from antivirus scanning.
    ///
    /// Note: the Explorer SmartScreen hard-block level is deliberately kept out of the
    /// Recommended profile so SmartScreen stays at the overridable "Warn" level.
    /// </summary>
    public static class SelfProtectionService
    {
        public static string ProtectedDirectory => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "AtlantHarden");
        public static string ProtectedExePath => Path.Combine(ProtectedDirectory, "AtlantHarden.exe");

        /// <summary>True once a protected copy exists (so callers can tell the user where to relaunch).</summary>
        public static bool HasProtectedCopy => File.Exists(ProtectedExePath);

        /// <summary>
        /// Ensure the protected copy exists and is the only AtlantHarden path allow-listed for
        /// ASR and Controlled Folder Access. Best-effort: never blocks the user's action.
        /// </summary>
        public static async Task EnsureToolAllowlistedAsync()
        {
            try
            {
                var exe = Environment.ProcessPath;
                if (string.IsNullOrWhiteSpace(exe)) return;

                var target = ProtectedExePath;
                if (!string.Equals(Path.GetFullPath(exe), target, StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Directory.CreateDirectory(ProtectedDirectory);
                        File.Copy(exe, target, overwrite: true);
                    }
                    catch (Exception ex)
                    {
                        // e.g. the protected copy is running right now; keep the existing copy.
                        ActivityLog.Write($"Self-protection: could not refresh protected copy: {ex.Message}");
                    }
                }
                if (!File.Exists(target)) return;

                var p = target.Replace("'", "''");
                var script =
                    "$ErrorActionPreference='SilentlyContinue';" +
                    $"$p='{p}';" +
                    "$m=Get-MpPreference;" +
                    // Remove AtlantHarden.exe entries pointing anywhere else (old unzip folders).
                    "$m.AttackSurfaceReductionOnlyExclusions | Where-Object { $_ -like '*\\AtlantHarden.exe' -and $_ -ne $p } | " +
                    "  ForEach-Object { Remove-MpPreference -AttackSurfaceReductionOnlyExclusions $_ };" +
                    "$m.ControlledFolderAccessAllowedApplications | Where-Object { $_ -like '*\\AtlantHarden.exe' -and $_ -ne $p } | " +
                    "  ForEach-Object { Remove-MpPreference -ControlledFolderAccessAllowedApplications $_ };" +
                    "Add-MpPreference -AttackSurfaceReductionOnlyExclusions $p;" +
                    "Add-MpPreference -ControlledFolderAccessAllowedApplications $p;" +
                    // All-users Start-menu shortcut to the protected copy.
                    "$lnk=Join-Path $env:ProgramData 'Microsoft\\Windows\\Start Menu\\Programs\\AtlantHarden.lnk';" +
                    "$s=(New-Object -ComObject WScript.Shell).CreateShortcut($lnk);" +
                    "$s.TargetPath=$p; $s.WorkingDirectory=(Split-Path $p); $s.Save();";

                await ProcessRunner.RunPowerShellAsync(script, TimeSpan.FromMinutes(2));
            }
            catch
            {
                // Never let self-protection failure block the user's action.
            }
        }
    }
}
