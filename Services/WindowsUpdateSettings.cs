using AtlantHarden.Models;
using System.Collections.Generic;

namespace AtlantHarden.Services
{
    /// <summary>
    /// Keep Windows, drivers/firmware, other Microsoft products (Office, .NET, ...), Store apps
    /// and browsers updating automatically. Unpatched software is one of the most common ways in,
    /// so these are part of the Recommended profile. Policy values are removed on revert so the
    /// PC goes back to its own (or its organization's) Windows Update configuration.
    /// </summary>
    public static class WindowsUpdateSettings
    {
        private const string Wu = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate";
        private const string Au = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\WindowsUpdate\AU";
        private const string Ux = @"HKLM\SOFTWARE\Microsoft\WindowsUpdate\UX\Settings";
        private const string OfficeUpdate = @"HKLM\SOFTWARE\Policies\Microsoft\office\16.0\common\officeupdate";

        private static readonly string[] WuDocs =
        {
            "https://learn.microsoft.com/windows/deployment/update/waas-wu-settings",
            "https://learn.microsoft.com/windows/client-management/mdm/policy-csp-update"
        };

        private static HardeningSetting Reg(string id, string name, string description, string path, string key,
            object value, object? defaultValue, string[] tags, string? impact = null, string[]? mirrors = null,
            string[]? refs = null) => new()
        {
            Id = id,
            Name = name,
            Description = description,
            Category = SettingCategory.WindowsUpdate,
            Type = SettingType.Registry,
            Risk = RiskLevel.Low,
            RegistryPath = path,
            RegistryKey = key,
            RegistryValueType = value is string ? "REG_SZ" : "REG_DWORD",
            RecommendedValue = value,
            DefaultValue = defaultValue,     // null = delete the value on revert
            MirrorRegistryPaths = mirrors,
            ImpactWarning = impact,
            ReferenceUrls = refs ?? WuDocs,
            Tags = tags
        };

        public static List<HardeningSetting> GetAllSettings() => new()
        {
            // ---- Windows itself ---------------------------------------------------------------
            Reg("WU_EnableAutomaticUpdates", "Turn on automatic updates",
                "Make sure Windows Update is not switched off by policy (NoAutoUpdate = 0).",
                Au, "NoAutoUpdate", 0, null, new[] { "windows update", "patching" }),

            Reg("WU_AutoDownloadAndInstall", "Download and install updates automatically",
                "Updates are downloaded and installed automatically; Windows restarts outside your active hours " +
                "(Configure Automatic Updates = 4, auto download and schedule the install).",
                Au, "AUOptions", 4, null, new[] { "windows update", "patching", "install" },
                impact: "Windows may restart outside active hours to finish installing updates."),

            Reg("WU_NoQualityUpdateDeferral", "Don't delay security updates",
                "Turns off any deferral of monthly quality (security) updates so they install as soon as they're released.",
                Wu, "DeferQualityUpdates", 0, null, new[] { "windows update", "deferral", "patching" }),

            Reg("WU_RestartNotifications", "Notify me when a restart is needed to finish updating",
                "Shows a notification when an update needs a restart, so pending security fixes aren't left half-installed.",
                Ux, "RestartNotificationsAllowed2", 1, 0, new[] { "windows update", "restart", "notification" }),

            // ---- Other Microsoft products (Office MSI, .NET, SQL, Visual Studio, ...) -----------
            Reg("WU_MicrosoftUpdate", "Receive updates for other Microsoft products",
                "Also installs updates for Microsoft Office, .NET, SQL Server, Visual Studio and other Microsoft software " +
                "through Windows Update (Microsoft Update).",
                Au, "AllowMUUpdateService", 1, null, new[] { "windows update", "microsoft update", "office", ".net" },
                mirrors: new[] { Ux }),

            // ---- Drivers and firmware -----------------------------------------------------------
            Reg("WU_IncludeDrivers", "Include driver and firmware updates",
                "Lets Windows Update install driver and device firmware updates (UEFI/BIOS, storage, Wi-Fi, TPM and others " +
                "that ship through Windows Update), instead of excluding them.",
                Wu, "ExcludeWUDriversInQualityUpdate", 0, null, new[] { "windows update", "drivers", "firmware" }),

            Reg("WU_DeviceDriverSearch", "Get drivers from Windows Update for new devices",
                "When a device is connected, Windows looks for its driver on Windows Update.",
                @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\DriverSearching", "SearchOrderConfig", 1, 1,
                new[] { "windows update", "drivers" }),

            // ---- Microsoft 365 / Office Click-to-Run --------------------------------------------
            Reg("WU_OfficeAutoUpdate", "Keep Microsoft Office updating automatically",
                "Turns on automatic updates for Microsoft 365 Apps / Office 2016-2024 (Click-to-Run).",
                OfficeUpdate, "enableautomaticupdates", 1, null, new[] { "office", "updates", "patching" },
                refs: new[] { "https://learn.microsoft.com/microsoft-365-apps/updates/configure-update-settings-microsoft-365-apps" }),

            Reg("WU_OfficeLockUpdates", "Don't let Office updates be switched off",
                "Hides the Enable/Disable Updates option in Office so updates can't be turned off from inside the apps.",
                OfficeUpdate, "hideenabledisableupdates", 1, null, new[] { "office", "updates" },
                refs: new[] { "https://learn.microsoft.com/microsoft-365-apps/updates/configure-update-settings-microsoft-365-apps" }),

            // ---- Store apps and browsers --------------------------------------------------------
            Reg("WU_StoreAppAutoUpdate", "Update Microsoft Store apps automatically",
                "Keeps Store apps (including App Installer/winget, Photos, Terminal and Store-installed Office) up to date.",
                @"HKLM\SOFTWARE\Policies\Microsoft\WindowsStore", "AutoDownload", 4, null,
                new[] { "store", "apps", "updates" }),

            Reg("WU_EdgeAutoUpdate", "Keep Microsoft Edge updating",
                "Makes sure Edge updates are allowed (Update policy override default = always allow).",
                @"HKLM\SOFTWARE\Policies\Microsoft\EdgeUpdate", "UpdateDefault", 1, null,
                new[] { "edge", "browser", "updates" },
                refs: new[] { "https://learn.microsoft.com/deployedge/microsoft-edge-update-policies#updatedefault" }),

            Reg("WU_ChromeAutoUpdate", "Keep Google Chrome updating",
                "Makes sure Chrome updates are allowed (Update policy override default = always allow).",
                @"HKLM\SOFTWARE\Policies\Google\Update", "UpdateDefault", 1, null,
                new[] { "chrome", "browser", "updates" },
                refs: new[] { "https://support.google.com/chrome/a/answer/6350036" }),

            // ---- Maximum only -------------------------------------------------------------------
            Reg("WU_LatestUpdatesAsap", "Get the latest updates as soon as they're available",
                "Opts in to receiving new Windows features and non-security improvements early, as they roll out.",
                Ux, "IsContinuousInnovationOptedIn", 1, 0, new[] { "windows update", "features" },
                impact: "New Windows features can appear before the general rollout."),

            Reg("WU_DownloadOverMetered", "Download updates over metered connections",
                "Lets Windows Update download over connections marked as metered (phone hotspots, capped plans).",
                Wu, "AllowAutoWindowsUpdateDownloadOverMeteredNetwork", 1, null, new[] { "windows update", "metered" },
                impact: "Can use mobile data on a hotspot or capped connection."),

            Reg("WU_DisablePause", "Remove the option to pause updates",
                "Hides \"Pause updates\" in Settings so updates can't be postponed.",
                Wu, "SetDisablePauseUXAccess", 1, null, new[] { "windows update", "pause" },
                impact: "Users can no longer pause updates, even temporarily.")
        };
    }
}
