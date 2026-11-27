using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AtlantHarden.Models
{
    public enum SettingType
    {
        Registry,
        PowerShell,
        Command,
        FileAssociation,
        Firewall,
        Service,
        AuditPolicy,
        AppRemoval
    }

    public enum RiskLevel
    {
        Low,
        Medium,
        High
    }

    public enum SettingCategory
    {
        WindowsDefender,
        AttackSurfaceReduction,
        NetworkSecurity,
        CredentialProtection,
        EdgeHardening,
        ChromeHardening,
        FirefoxHardening,
        OfficeHardening,
        Privacy,
        Logging,
        FileAssociations,
        Firewall,
        TLSCrypto,
        SystemHardening,
        RemovalMedia,
        AdobeReader,
        Bloatware,
        // DISA STIG product categories — comprehensive STIG coverage, grouped separately
        // from the curated baseline categories above.
        StigWindows11,
        StigEdge,
        StigChrome,
        StigFirefox,
        StigOffice365,
        WindowsUpdate
    }

    /// <summary>Human-readable names for <see cref="SettingCategory"/>.</summary>
    public static class CategoryNames
    {
        public static string Display(SettingCategory c) => c switch
        {
            SettingCategory.WindowsDefender => "Microsoft Defender",
            SettingCategory.AttackSurfaceReduction => "Attack Surface Reduction",
            SettingCategory.NetworkSecurity => "Network Security",
            SettingCategory.CredentialProtection => "Credential Protection",
            SettingCategory.EdgeHardening => "Microsoft Edge",
            SettingCategory.ChromeHardening => "Google Chrome",
            SettingCategory.FirefoxHardening => "Mozilla Firefox",
            SettingCategory.OfficeHardening => "Microsoft Office",
            SettingCategory.Privacy => "Privacy",
            SettingCategory.Logging => "Logging & Auditing",
            SettingCategory.FileAssociations => "File Associations",
            SettingCategory.Firewall => "Windows Firewall",
            SettingCategory.TLSCrypto => "TLS & Cryptography",
            SettingCategory.SystemHardening => "System Hardening",
            SettingCategory.RemovalMedia => "Removable Media",
            SettingCategory.AdobeReader => "Adobe Reader",
            SettingCategory.Bloatware => "Bloatware",
            SettingCategory.WindowsUpdate => "Windows Update",
            SettingCategory.StigWindows11 => "STIG · Windows 11",
            SettingCategory.StigEdge => "STIG · Edge",
            SettingCategory.StigChrome => "STIG · Chrome",
            SettingCategory.StigFirefox => "STIG · Firefox",
            SettingCategory.StigOffice365 => "STIG · Office 365",
            _ => c.ToString()
        };
    }

    public class HardeningSetting : INotifyPropertyChanged
    {
        private bool _isEnabled;
        private bool _isApplied;
        private string _currentValue = string.Empty;

        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DetailedInfo { get; set; } = string.Empty;
        public SettingCategory Category { get; set; }
        public SettingType Type { get; set; }
        public RiskLevel Risk { get; set; }
        
        // For Registry settings
        public string RegistryPath { get; set; } = string.Empty;
        public string RegistryKey { get; set; } = string.Empty;
        public string RegistryValueType { get; set; } = string.Empty;
        public object? RecommendedValue { get; set; }
        public object? DefaultValue { get; set; }

        /// <summary>
        /// Extra registry paths to write/revert the same key+value to, beyond the primary
        /// <see cref="RegistryPath"/>. Used when one logical control must land in more than one
        /// location — e.g. Adobe hardening under both the "Acrobat Reader" and "Adobe Acrobat"
        /// (unified 64-bit) product nodes, so whichever product is installed is hardened.
        /// </summary>
        public string[]? MirrorRegistryPaths { get; set; }

        /// <summary>
        /// For <see cref="SettingType.AppRemoval"/>: the Appx package name (e.g.
        /// "Microsoft.XboxGamingOverlay"). Used to detect whether the app is still installed
        /// (applied = removed). Empty for Win32/OEM removals, which detect via
        /// <see cref="VerifyRegistryPath"/> (the Uninstall key — absent = removed).
        /// </summary>
        public string AppxName { get; set; } = string.Empty;
        
        // For PowerShell/Command settings
        public string ApplyCommand { get; set; } = string.Empty;
        public string RevertCommand { get; set; } = string.Empty;
        
        // For validation - can verify non-Registry settings by checking a registry path
        public string ValidationCommand { get; set; } = string.Empty;
        public string VerifyRegistryPath { get; set; } = string.Empty;
        public string VerifyRegistryKey { get; set; } = string.Empty;
        public object? VerifyValue { get; set; }
        
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                if (_isEnabled != value)
                {
                    _isEnabled = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PendingState));
                    OnPropertyChanged(nameof(HasPendingChange));
                }
            }
        }

        public bool IsApplied
        {
            get => _isApplied;
            set
            {
                if (_isApplied != value)
                {
                    _isApplied = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(PendingState));
                    OnPropertyChanged(nameof(HasPendingChange));
                    OnPropertyChanged(nameof(StatusText));
                }
            }
        }

        public string CurrentValue
        {
            get => _currentValue;
            set
            {
                if (_currentValue != value)
                {
                    _currentValue = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsVerifiable));
                    OnPropertyChanged(nameof(StatusText));
                    OnPropertyChanged(nameof(CurrentValueDisplay));
                }
            }
        }

        /// <summary>
        /// For numeric registry policies where a HIGHER value is stricter (e.g. LDAP client
        /// signing 1 = negotiate, 2 = require). A current value at or above the recommended one
        /// counts as applied and is never lowered, so overlapping baselines (STIG, ACSC, curated)
        /// that ask for different levels end at the strictest instead of "last write wins".
        /// </summary>
        public bool AtLeast { get; set; }

        // Tags for filtering and grouping
        public string[] Tags { get; set; } = Array.Empty<string>();
        
        // Reference URLs for documentation
        public string[]? ReferenceUrls { get; set; }
        
        // Potential impact warning
        public string? ImpactWarning { get; set; }
        
        // Whether this setting requires a reboot
        public bool RequiresReboot { get; set; }
        
        // Whether this is a DISA STIG requirement
        public bool IsStig { get; set; }
        public string? StigId { get; set; }

        // DISA Vulnerability ID (e.g. V-253461) and the product whose STIG this comes from
        // (Windows11, Edge, Chrome, Firefox, Office365) - used for per-product grouping/labels.
        public string? VulnId { get; set; }
        public string? StigProduct { get; set; }

        // Control Correlation Identifiers (CCIs) mapped to this STIG requirement.
        public string[]? Ccis { get; set; }
        
        // Whether this is an ACSC (Australian Cyber Security Centre) requirement
        public bool IsACSC { get; set; }
        
        // ASR Rule GUID (for Attack Surface Reduction rules)
        public string? ASRGuid { get; set; }

        // ---- Display helpers (UI) -------------------------------------------------------

        /// <summary>False when the app can't read this setting's state on this PC.</summary>
        public bool IsVerifiable => CurrentValue is not ("Unknown" or "Check Failed" or "Error" or "Error reading");

        /// <summary>Applied / Not applied / Can't verify.</summary>
        public string StatusText => !IsVerifiable ? "Can't verify" : IsApplied ? "Applied" : "Not applied";

        /// <summary>
        /// What "Apply Selected" will do with this row: "Will apply", "Will revert" (toggle
        /// switched off on an applied setting), or "" when the toggle matches the PC.
        /// </summary>
        public string PendingState =>
            IsEnabled && !IsApplied ? (Category == SettingCategory.Bloatware ? "Will remove" : "Will apply")
            : !IsEnabled && IsApplied && Category != SettingCategory.Bloatware ? "Will revert"
            : string.Empty;

        public bool HasPendingChange => PendingState.Length > 0;

        /// <summary>Severity label: CAT I/II/III for DISA STIG rows, Low/Medium/High risk otherwise.</summary>
        public string RiskDisplay => IsStig
            ? Risk switch { RiskLevel.High => "CAT I", RiskLevel.Medium => "CAT II", _ => "CAT III" }
            : Risk + " risk";

        /// <summary>Friendly category name for badges.</summary>
        public string CategoryDisplay => CategoryNames.Display(Category);

        public bool HasRegistryTarget => Type == SettingType.Registry && !string.IsNullOrEmpty(RegistryPath);

        /// <summary>What this setting changes, in one line, for any setting type.</summary>
        public string ChangeSummary => Type switch
        {
            SettingType.Registry => $"{RegistryPath} → {RegistryKey} = {FormatValue(RecommendedValue)}",
            _ when !string.IsNullOrEmpty(ASRGuid) => $"Microsoft Defender ASR rule {ASRGuid} → Block",
            SettingType.AppRemoval => string.IsNullOrEmpty(AppxName) ? "Runs the program's uninstaller" : $"Removes Store app {AppxName} (all users)",
            _ => ApplyCommand
        };

        public string RecommendedDisplay => Type == SettingType.Registry
            ? FormatValue(RecommendedValue)
            : !string.IsNullOrEmpty(ASRGuid) ? "Block"
            : Type == SettingType.AppRemoval ? "Removed"
            : "On";

        public string CurrentValueDisplay => string.IsNullOrEmpty(CurrentValue) ? "Not set"
            : CurrentValue == "-1" && Type == SettingType.Registry ? "0xFFFFFFFF"
            : CurrentValue;

        public bool HasReferences => ReferenceUrls is { Length: > 0 };

        private static string FormatValue(object? v) => v switch
        {
            null => "(removed)",
            uint u when u == 0xFFFFFFFF => "0xFFFFFFFF",
            string[] arr => string.Join(", ", arr),
            _ => v.ToString() ?? ""
        };

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
