using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;

namespace AtlantHarden.Models
{
    public class CategoryInfo : INotifyPropertyChanged
    {
        private int _settingsCount;
        private int _enabledCount;
        private int _appliedCount;

        public SettingCategory Category { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        // Sidebar grouping: "Hardening Categories" (baseline) or "DISA STIG Compliance".
        public string Group { get; set; } = "Hardening Categories";
        
        public int SettingsCount 
        { 
            get => _settingsCount;
            set { _settingsCount = value; OnPropertyChanged(); }
        }
        
        public int EnabledCount 
        { 
            get => _enabledCount;
            set { _enabledCount = value; OnPropertyChanged(); }
        }
        
        public int AppliedCount 
        { 
            get => _appliedCount;
            set { _appliedCount = value; OnPropertyChanged(); }
        }
        
        public Color AccentColor { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static CategoryInfo Create(SettingCategory category)
        {
            return category switch
            {
                SettingCategory.WindowsDefender => new CategoryInfo
                {
                    Category = category,
                    Name = "Windows Defender",
                    Description = "Configure Windows Defender settings, real-time protection, and cloud-based protection",
                    Icon = "🛡️",
                    AccentColor = Color.FromRgb(0, 120, 215)
                },
                SettingCategory.AttackSurfaceReduction => new CategoryInfo
                {
                    Category = category,
                    Name = "Attack Surface Reduction",
                    Description = "Enable ASR rules to block Office malware, ransomware, and other attack vectors",
                    Icon = "🎯",
                    AccentColor = Color.FromRgb(232, 17, 35)
                },
                SettingCategory.NetworkSecurity => new CategoryInfo
                {
                    Category = category,
                    Name = "Network Security",
                    Description = "Configure SMB, NTLM, LDAP signing, and network protocol security",
                    Icon = "🌐",
                    AccentColor = Color.FromRgb(0, 153, 188)
                },
                SettingCategory.CredentialProtection => new CategoryInfo
                {
                    Category = category,
                    Name = "Credential Protection",
                    Description = "Harden LSASS, disable credential caching, and protect against Mimikatz",
                    Icon = "🔐",
                    AccentColor = Color.FromRgb(107, 107, 107)
                },
                SettingCategory.EdgeHardening => new CategoryInfo
                {
                    Category = category,
                    Name = "Microsoft Edge",
                    Description = "Secure Microsoft Edge browser with enterprise policies and SmartScreen",
                    Icon = "🌐",
                    AccentColor = Color.FromRgb(0, 120, 215)
                },
                SettingCategory.ChromeHardening => new CategoryInfo
                {
                    Category = category,
                    Name = "Google Chrome",
                    Description = "Secure Google Chrome browser with enterprise policies and Safe Browsing",
                    Icon = "🔴",
                    AccentColor = Color.FromRgb(66, 133, 244)
                },
                SettingCategory.FirefoxHardening => new CategoryInfo
                {
                    Category = category,
                    Name = "Mozilla Firefox",
                    Description = "Secure Mozilla Firefox browser with enterprise policies and tracking protection",
                    Icon = "🦊",
                    AccentColor = Color.FromRgb(255, 100, 0)
                },
                SettingCategory.OfficeHardening => new CategoryInfo
                {
                    Category = category,
                    Name = "Office Hardening",
                    Description = "Disable Office macros, DDE, and protect against malspam attacks",
                    Icon = "📄",
                    AccentColor = Color.FromRgb(217, 83, 30)
                },
                SettingCategory.Privacy => new CategoryInfo
                {
                    Category = category,
                    Name = "Privacy Settings",
                    Description = "Control telemetry, advertising ID, location, and data collection",
                    Icon = "👁️",
                    AccentColor = Color.FromRgb(135, 100, 184)
                },
                SettingCategory.Logging => new CategoryInfo
                {
                    Category = category,
                    Name = "Logging & Auditing",
                    Description = "Enable PowerShell logging, process creation auditing, and event log sizing",
                    Icon = "📋",
                    AccentColor = Color.FromRgb(16, 124, 16)
                },
                SettingCategory.FileAssociations => new CategoryInfo
                {
                    Category = category,
                    Name = "File Associations",
                    Description = "Protect against ransomware by changing dangerous file associations",
                    Icon = "📁",
                    AccentColor = Color.FromRgb(255, 140, 0)
                },
                SettingCategory.Firewall => new CategoryInfo
                {
                    Category = category,
                    Name = "Windows Firewall",
                    Description = "Configure firewall rules and block LOLBins from network access",
                    Icon = "🔥",
                    AccentColor = Color.FromRgb(209, 52, 56)
                },
                SettingCategory.TLSCrypto => new CategoryInfo
                {
                    Category = category,
                    Name = "TLS & Cryptography",
                    Description = "Disable weak ciphers, protocols, and enforce strong TLS settings",
                    Icon = "🔒",
                    AccentColor = Color.FromRgb(0, 99, 177)
                },
                SettingCategory.SystemHardening => new CategoryInfo
                {
                    Category = category,
                    Name = "System Hardening",
                    Description = "General OS hardening including UAC, DEP, ASLR, and DLL protection",
                    Icon = "⚙️",
                    AccentColor = Color.FromRgb(76, 74, 72)
                },
                SettingCategory.RemovalMedia => new CategoryInfo
                {
                    Category = category,
                    Name = "Removable Media",
                    Description = "Disable autorun and control USB device security",
                    Icon = "💾",
                    AccentColor = Color.FromRgb(0, 120, 212)
                },
                SettingCategory.AdobeReader => new CategoryInfo
                {
                    Category = category,
                    Name = "Adobe Reader",
                    Description = "Apply Adobe Reader DC security hardening (STIG compliance)",
                    Icon = "📕",
                    AccentColor = Color.FromRgb(255, 0, 0)
                },
                SettingCategory.Bloatware => new CategoryInfo
                {
                    Category = category,
                    Name = "Bloatware Removal",
                    Description = "Remove unnecessary Windows apps and provisioned packages",
                    Icon = "🗑️",
                    AccentColor = Color.FromRgb(104, 118, 138)
                },
                SettingCategory.StigWindows11 => new CategoryInfo
                {
                    Category = category,
                    Name = "Windows 11 (V2R7)",
                    Description = "DISA STIG for Microsoft Windows 11 — OS, credential, network, audit, and policy controls",
                    Icon = "🪟",
                    Group = "DISA STIG Compliance",
                    AccentColor = Color.FromRgb(0, 120, 215)
                },
                SettingCategory.StigEdge => new CategoryInfo
                {
                    Category = category,
                    Name = "Microsoft Edge (V2R5)",
                    Description = "DISA STIG for Microsoft Edge — SmartScreen, isolation, TLS, and policy enforcement",
                    Icon = "🌐",
                    Group = "DISA STIG Compliance",
                    AccentColor = Color.FromRgb(0, 153, 188)
                },
                SettingCategory.StigChrome => new CategoryInfo
                {
                    Category = category,
                    Name = "Google Chrome (V2R11)",
                    Description = "DISA STIG for Google Chrome — Safe Browsing, extensions, and enterprise policies",
                    Icon = "🔵",
                    Group = "DISA STIG Compliance",
                    AccentColor = Color.FromRgb(66, 133, 244)
                },
                SettingCategory.StigFirefox => new CategoryInfo
                {
                    Category = category,
                    Name = "Mozilla Firefox (V6R7)",
                    Description = "DISA STIG for Mozilla Firefox — TLS, telemetry, and tracking protection policies",
                    Icon = "🦊",
                    Group = "DISA STIG Compliance",
                    AccentColor = Color.FromRgb(255, 100, 0)
                },
                SettingCategory.StigOffice365 => new CategoryInfo
                {
                    Category = category,
                    Name = "Office 365 ProPlus (V3R5)",
                    Description = "DISA STIG for Microsoft Office 365 ProPlus — macro, OLE, and document attack controls",
                    Icon = "📘",
                    Group = "DISA STIG Compliance",
                    AccentColor = Color.FromRgb(217, 83, 30)
                },
                _ => new CategoryInfo
                {
                    Category = category,
                    Name = "Unknown",
                    Description = "Unknown category",
                    Icon = "❓",
                    AccentColor = Color.FromRgb(128, 128, 128)
                }
            };
        }
    }
}
