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
        AuditPolicy
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
        StigOffice365
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
                }
            }
        }

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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
