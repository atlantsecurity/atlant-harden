using AtlantHarden.Models;
using System.Collections.Generic;

namespace AtlantHarden.Services
{
    public static class AdditionalSettings
    {
        public static List<HardeningSetting> GetEdgeSettings()
        {
            return new List<HardeningSetting>
            {
                new HardeningSetting
                {
                    Id = "EDGE_SitePerProcess",
                    Name = "Enable Site Isolation",
                    Description = "Run each site in its own process for better security",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "SitePerProcess",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "edge", "isolation", "security" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_SSLVersionMin",
                    Name = "Enforce TLS 1.2 Minimum",
                    Description = "Set minimum SSL/TLS version to TLS 1.2",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "SSLVersionMin",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "tls1.2",
                    DefaultValue = "tls1",
                    Tags = new[] { "edge", "tls", "encryption" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_SmartScreen",
                    Name = "Enable SmartScreen",
                    Description = "Enable Microsoft Defender SmartScreen for Edge",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "SmartScreenEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 1,
                    Tags = new[] { "edge", "smartscreen", "phishing" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_PreventSmartScreenOverride",
                    Name = "Prevent SmartScreen Override",
                    Description = "Prevent users from bypassing SmartScreen warnings",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "PreventSmartScreenPromptOverride",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "edge", "smartscreen" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_PreventSmartScreenFileOverride",
                    Name = "Prevent SmartScreen File Override",
                    Description = "Prevent bypassing SmartScreen warnings for downloads",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "PreventSmartScreenPromptOverrideForFiles",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "edge", "smartscreen", "downloads" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_SSLErrorOverride",
                    Name = "Block SSL Error Override",
                    Description = "Prevent users from bypassing SSL certificate errors",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "SSLErrorOverrideAllowed",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    ImpactWarning = "Users won't be able to visit sites with certificate errors",
                    Tags = new[] { "edge", "ssl", "certificates" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_PUAProtection",
                    Name = "Enable PUA Protection",
                    Description = "Block potentially unwanted applications in downloads",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "SmartScreenPuaEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "edge", "pua", "downloads" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_NativeMessaging",
                    Name = "Disable Native Messaging User Hosts",
                    Description = "Disable user-level native messaging hosts",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "NativeMessagingUserLevelHosts",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "edge", "native", "messaging" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_BackgroundMode",
                    Name = "Disable Background Mode",
                    Description = "Prevent Edge from running in background after closing",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "BackgroundModeEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "edge", "background", "performance" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_DeleteHistory",
                    Name = "Prevent Deleting Browser History",
                    Description = "Prevent users from deleting browsing history",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "AllowDeletingBrowserHistory",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    ImpactWarning = "Useful for compliance but may frustrate users",
                    Tags = new[] { "edge", "history", "compliance" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_InPrivate",
                    Name = "Disable InPrivate Browsing",
                    Description = "Disable InPrivate browsing mode for compliance",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "InPrivateModeAvailability",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    ImpactWarning = "Users won't be able to use InPrivate browsing",
                    Tags = new[] { "edge", "inprivate", "compliance" }
                },
                new HardeningSetting
                {
                    Id = "EDGE_PasswordManager",
                    Name = "Disable Password Manager",
                    Description = "Disable built-in password manager (use external)",
                    Category = SettingCategory.EdgeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Microsoft\Edge",
                    RegistryKey = "PasswordManagerEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "edge", "passwords" }
                }
            };
        }

        public static List<HardeningSetting> GetChromeSettings()
        {
            return new List<HardeningSetting>
            {
                new HardeningSetting
                {
                    Id = "CHROME_SitePerProcess",
                    Name = "Enable Site Isolation",
                    Description = "Run each site in its own process",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Google\Chrome",
                    RegistryKey = "SitePerProcess",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "chrome", "isolation" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_AdvancedProtection",
                    Name = "Enable Advanced Protection",
                    Description = "Enable Chrome Advanced Protection Program features",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "AdvancedProtectionAllowed",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "chrome", "protection" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_AudioSandbox",
                    Name = "Enable Audio Sandbox",
                    Description = "Run audio processing in a sandboxed process",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Google\Chrome",
                    RegistryKey = "AudioSandboxEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "sandbox", "audio" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_DNSOverHTTPS",
                    Name = "Enable DNS over HTTPS",
                    Description = "Enable encrypted DNS queries",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Google\Chrome",
                    RegistryKey = "DnsOverHttpsMode",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "automatic",
                    DefaultValue = "off",
                    Tags = new[] { "chrome", "dns", "encryption" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_TLS13",
                    Name = "Enable TLS 1.3 Hardening",
                    Description = "Enable TLS 1.3 hardening for local anchors",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Google\Chrome",
                    RegistryKey = "TLS13HardeningForLocalAnchorsEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "chrome", "tls", "encryption" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_OutdatedPlugins",
                    Name = "Block Outdated Plugins",
                    Description = "Block running of outdated plugins",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "AllowOutdatedPlugins",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "plugins" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_BackgroundMode",
                    Name = "Disable Background Mode",
                    Description = "Prevent Chrome from running in background",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "BackgroundModeEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "background" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_Metrics",
                    Name = "Disable Chrome Metrics",
                    Description = "Disable usage statistics reporting",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "MetricsReportingEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "privacy", "telemetry" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_SearchSuggest",
                    Name = "Disable Search Suggestions",
                    Description = "Disable search and URL suggestions",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "SearchSuggestEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "privacy", "search" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_ImportPasswords",
                    Name = "Disable Password Import",
                    Description = "Prevent importing saved passwords",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "ImportSavedPasswords",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "passwords" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_RevocationChecks",
                    Name = "Enable Certificate Revocation Checks",
                    Description = "Enable online certificate revocation checks",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "EnableOnlineRevocationChecks",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "chrome", "certificates", "revocation" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_SSLVersionMin",
                    Name = "Enforce TLS 1.1 Minimum",
                    Description = "Set minimum TLS version to 1.1",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "SSLVersionMin",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "tls1.1",
                    DefaultValue = "tls1",
                    Tags = new[] { "chrome", "tls" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_DisableAutoplay",
                    Name = "Disable Autoplay",
                    Description = "Disable automatic media playback",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "AutoplayAllowed",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "autoplay", "media" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_SafeBrowsing",
                    Name = "Enable Enhanced Safe Browsing",
                    Description = "Enable enhanced safe browsing protection",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome\Recommended",
                    RegistryKey = "SafeBrowsingProtectionLevel",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "safebrowsing" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_GuestMode",
                    Name = "Disable Guest Mode",
                    Description = "Disable Chrome guest browsing mode",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "BrowserGuestModeEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "guest" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_MediaRouter",
                    Name = "Disable Media Router",
                    Description = "Disable Chrome Cast/media router functionality",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "EnableMediaRouter",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "cast", "media" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_Incognito",
                    Name = "Disable Incognito Mode",
                    Description = "Disable incognito browsing mode for compliance",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "IncognitoModeAvailability",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    ImpactWarning = "Users won't be able to use Incognito mode",
                    Tags = new[] { "chrome", "incognito", "compliance" }
                },
                new HardeningSetting
                {
                    Id = "CHROME_PasswordManager",
                    Name = "Disable Password Manager",
                    Description = "Disable built-in password manager",
                    Category = SettingCategory.ChromeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Google\Chrome",
                    RegistryKey = "PasswordManagerEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "chrome", "passwords" }
                }
            };
        }

        public static List<HardeningSetting> GetFirefoxSettings()
        {
            return new List<HardeningSetting>
            {
                new HardeningSetting
                {
                    Id = "FF_Telemetry",
                    Name = "Disable Telemetry",
                    Description = "Disable Firefox telemetry and data collection",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "DisableTelemetry",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "firefox", "telemetry", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "FF_BrowserAgent",
                    Name = "Disable Default Browser Agent",
                    Description = "Disable Firefox default browser agent",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "DisableDefaultBrowserAgent",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "firefox", "agent" }
                },
                new HardeningSetting
                {
                    Id = "FF_PasswordManager",
                    Name = "Disable Password Manager",
                    Description = "Disable built-in password manager (use external)",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "PasswordManagerEnabled",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "firefox", "passwords" }
                },
                new HardeningSetting
                {
                    Id = "FF_SSLVersionMin",
                    Name = "Enforce TLS 1.2 Minimum",
                    Description = "Set minimum TLS version to 1.2",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "SSLVersionMin",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "tls1.2",
                    DefaultValue = "tls1",
                    Tags = new[] { "firefox", "tls" }
                },
                new HardeningSetting
                {
                    Id = "FF_DoH",
                    Name = "Enable DNS over HTTPS",
                    Description = "Enable encrypted DNS queries",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "DNSOverHTTPS",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "firefox", "dns", "doh" }
                },
                new HardeningSetting
                {
                    Id = "FF_TrackingProtection",
                    Name = "Enable Tracking Protection",
                    Description = "Enable strict tracking protection",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "EnableTrackingProtection",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "firefox", "tracking", "privacy" }
                },
                new HardeningSetting
                {
                    Id = "FF_Pocket",
                    Name = "Disable Pocket",
                    Description = "Disable Pocket integration in Firefox",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "DisablePocket",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "firefox", "pocket" }
                },
                new HardeningSetting
                {
                    Id = "FF_PrivateBrowsing",
                    Name = "Disable Private Browsing",
                    Description = "Disable private browsing mode for compliance",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "DisablePrivateBrowsing",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    ImpactWarning = "Users won't be able to use private browsing",
                    Tags = new[] { "firefox", "private", "compliance" }
                },
                new HardeningSetting
                {
                    Id = "FF_FormHistory",
                    Name = "Disable Form History",
                    Description = "Disable saving form and search history",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "DisableFormHistory",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "firefox", "forms", "history" }
                },
                new HardeningSetting
                {
                    Id = "FF_FirefoxAccounts",
                    Name = "Disable Firefox Accounts",
                    Description = "Disable Firefox sync and accounts",
                    Category = SettingCategory.FirefoxHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Mozilla\Firefox",
                    RegistryKey = "DisableFirefoxAccounts",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "firefox", "sync", "accounts" }
                }
            };
        }

        public static List<HardeningSetting> GetOfficeSettings()
        {
            var settings = new List<HardeningSetting>();
            var officeVersions = new[] { "16.0" }; // Focus on Office 2016/2019/365

            foreach (var version in officeVersions)
            {
                // Excel settings
                settings.Add(new HardeningSetting
                {
                    Id = $"OFFICE_Excel_VBA",
                    Name = "Disable Excel VBA Macros",
                    Description = "Disable all VBA macros in Excel",
                    Category = SettingCategory.OfficeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = $@"HKCU\SOFTWARE\Microsoft\Office\{version}\Excel\Security",
                    RegistryKey = "VBAWarnings",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 4,
                    DefaultValue = 1,
                    ImpactWarning = "May break legitimate Excel macros",
                    Tags = new[] { "office", "excel", "vba", "macros" }
                });

                settings.Add(new HardeningSetting
                {
                    Id = $"OFFICE_Excel_DDE",
                    Name = "Disable Excel DDE",
                    Description = "Disable Dynamic Data Exchange in Excel",
                    Category = SettingCategory.OfficeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = $@"HKCU\SOFTWARE\Microsoft\Office\{version}\Excel\Security",
                    RegistryKey = "DataConnectionWarnings",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2,
                    DefaultValue = 0,
                    Tags = new[] { "office", "excel", "dde" }
                });

                // Word settings
                settings.Add(new HardeningSetting
                {
                    Id = $"OFFICE_Word_VBA",
                    Name = "Disable Word VBA Macros",
                    Description = "Disable all VBA macros in Word",
                    Category = SettingCategory.OfficeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = $@"HKCU\SOFTWARE\Microsoft\Office\{version}\Word\Security",
                    RegistryKey = "VBAWarnings",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 4,
                    DefaultValue = 1,
                    ImpactWarning = "May break legitimate Word macros",
                    Tags = new[] { "office", "word", "vba", "macros" }
                });

                settings.Add(new HardeningSetting
                {
                    Id = $"OFFICE_Word_DDE",
                    Name = "Disable Word DDE",
                    Description = "Disable Dynamic Data Exchange in Word",
                    Category = SettingCategory.OfficeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = $@"HKCU\SOFTWARE\Microsoft\Office\{version}\Word\Security",
                    RegistryKey = "AllowDDE",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 2,
                    Tags = new[] { "office", "word", "dde" }
                });

                // PowerPoint settings
                settings.Add(new HardeningSetting
                {
                    Id = $"OFFICE_PPT_VBA",
                    Name = "Disable PowerPoint VBA Macros",
                    Description = "Disable all VBA macros in PowerPoint",
                    Category = SettingCategory.OfficeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = $@"HKCU\SOFTWARE\Microsoft\Office\{version}\PowerPoint\Security",
                    RegistryKey = "VBAWarnings",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 4,
                    DefaultValue = 1,
                    ImpactWarning = "May break legitimate PowerPoint macros",
                    Tags = new[] { "office", "powerpoint", "vba", "macros" }
                });

                // Outlook settings
                settings.Add(new HardeningSetting
                {
                    Id = $"OFFICE_Outlook_VBA",
                    Name = "Disable Outlook VBA Macros",
                    Description = "Disable all VBA macros in Outlook",
                    Category = SettingCategory.OfficeHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = $@"HKCU\SOFTWARE\Microsoft\Office\{version}\Outlook\Security",
                    RegistryKey = "Level",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 4,
                    DefaultValue = 1,
                    Tags = new[] { "office", "outlook", "vba", "macros" }
                });
            }

            // Global Office settings
            settings.Add(new HardeningSetting
            {
                Id = "OFFICE_DisableActiveX",
                Name = "Disable All ActiveX",
                Description = "Disable all ActiveX controls in Office",
                Category = SettingCategory.OfficeHardening,
                Type = SettingType.Registry,
                Risk = RiskLevel.Medium,
                RegistryPath = @"HKCU\SOFTWARE\Microsoft\Office\Common\Security",
                RegistryKey = "DisableAllActiveX",
                RegistryValueType = "REG_DWORD",
                RecommendedValue = 1,
                DefaultValue = 0,
                ImpactWarning = "Will disable all ActiveX content in Office documents",
                Tags = new[] { "office", "activex", "security" }
            });

            settings.Add(new HardeningSetting
            {
                Id = "OFFICE_BlockMacrosInternet",
                Name = "Block Macros from Internet",
                Description = "Block macros from running in files from the internet",
                Category = SettingCategory.OfficeHardening,
                Type = SettingType.Registry,
                Risk = RiskLevel.Low,
                RegistryPath = @"HKCU\SOFTWARE\Microsoft\Office\16.0\Common\Security",
                RegistryKey = "blockcontentexecutionfrominternet",
                RegistryValueType = "REG_DWORD",
                RecommendedValue = 1,
                DefaultValue = 0,
                Tags = new[] { "office", "macros", "internet" }
            });

            return settings;
        }

        public static List<HardeningSetting> GetAdobeSettings()
        {
            return new List<HardeningSetting>
            {
                new HardeningSetting
                {
                    Id = "ADOBE_ProtectedMode",
                    Name = "Enable Protected Mode",
                    Description = "Enable Adobe Reader Protected Mode sandbox",
                    Category = SettingCategory.AdobeReader,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown",
                    RegistryKey = "bProtectedMode",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "adobe", "sandbox", "protected" }
                },
                new HardeningSetting
                {
                    Id = "ADOBE_ProtectedView",
                    Name = "Enable Protected View",
                    Description = "Enable Protected View for all files",
                    Category = SettingCategory.AdobeReader,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown",
                    RegistryKey = "iProtectedView",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2,
                    DefaultValue = 0,
                    Tags = new[] { "adobe", "protected", "view" }
                },
                new HardeningSetting
                {
                    Id = "ADOBE_EnhancedSecurity",
                    Name = "Enable Enhanced Security",
                    Description = "Enable enhanced security in standalone mode",
                    Category = SettingCategory.AdobeReader,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown",
                    RegistryKey = "bEnhancedSecurityStandalone",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "adobe", "standalone", "security" }
                },
                new HardeningSetting
                {
                    Id = "ADOBE_DisableJavaScript",
                    Name = "Disable JavaScript",
                    Description = "Disable JavaScript execution in PDFs",
                    Category = SettingCategory.AdobeReader,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown",
                    RegistryKey = "bDisableJavaScript",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    ImpactWarning = "Some PDF forms may not work",
                    Tags = new[] { "adobe", "javascript" }
                },
                new HardeningSetting
                {
                    Id = "ADOBE_DisableAttachments",
                    Name = "Disable File Attachments",
                    Description = "Prevent opening of file attachments",
                    Category = SettingCategory.AdobeReader,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown",
                    RegistryKey = "iFileAttachmentPerms",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "adobe", "attachments" }
                },
                new HardeningSetting
                {
                    Id = "ADOBE_SuppressUpsell",
                    Name = "Suppress Upsell Messages",
                    Description = "Suppress Adobe upsell and advertising",
                    Category = SettingCategory.AdobeReader,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown",
                    RegistryKey = "bAcroSuppressUpsell",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "adobe", "upsell", "advertising" }
                }
            };
        }

        public static List<HardeningSetting> GetFirewallSettings()
        {
            var lolbins = new[]
            {
                ("certutil", @"%systemroot%\system32\certutil.exe", "May break certificate operations and some installers"),
                ("cmstp", @"%systemroot%\system32\cmstp.exe", "Rarely affects normal use"),
                ("cscript", @"%systemroot%\system32\cscript.exe", "May break administrative scripts"),
                ("mshta", @"%systemroot%\system32\mshta.exe", "May break some legacy applications"),
                ("msiexec", @"%systemroot%\system32\msiexec.exe", "May break software installation from network"),
                ("regsvr32", @"%systemroot%\system32\regsvr32.exe", "May break some software installations"),
                ("rundll32", @"%systemroot%\system32\rundll32.exe", "May break various Windows functions"),
                ("wmic", @"%systemroot%\system32\wbem\wmic.exe", "May break system administration tools"),
                ("wscript", @"%systemroot%\system32\wscript.exe", "May break administrative scripts"),
                ("powershell_ise", @"%systemroot%\system32\WindowsPowerShell\v1.0\powershell_ise.exe", "May break PowerShell development")
            };

            var settings = new List<HardeningSetting>();

            foreach (var (name, path, warning) in lolbins)
            {
                settings.Add(new HardeningSetting
                {
                    Id = $"FW_Block_{name}",
                    Name = $"Block {name}.exe Network Access",
                    Description = $"Block {name}.exe from making outbound network connections",
                    Category = SettingCategory.Firewall,
                    Type = SettingType.Firewall,
                    Risk = RiskLevel.High,
                    ApplyCommand = $"netsh advfirewall firewall add rule name=\"Block {name}.exe netconns\" program=\"{path}\" protocol=tcp dir=out enable=yes action=block profile=any",
                    RevertCommand = $"netsh advfirewall firewall delete rule name=\"Block {name}.exe netconns\"",
                    ImpactWarning = warning,
                    Tags = new[] { "firewall", "lolbin", name }
                });
            }

            return settings;
        }
    }
}
