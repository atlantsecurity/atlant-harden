using AtlantHarden.Models;
using System.Collections.Generic;

namespace AtlantHarden.Services
{
    /// <summary>
    /// ACSC (Australian Cyber Security Centre) Windows Hardening Settings
    /// Based on: Hardening Microsoft Windows 10 and Windows 11 Workstations (July 2024)
    /// https://www.cyber.gov.au/resources-business-and-government/maintaining-devices-and-systems/system-hardening-and-administration/system-hardening/hardening-microsoft-windows-10-and-windows-11-workstations
    /// </summary>
    public static class ACSCSettings
    {
        public static List<HardeningSetting> GetHighPrioritySettings()
        {
            return new List<HardeningSetting>
            {
                // Command Prompt Restrictions
                new HardeningSetting
                {
                    Id = "ACSC_DisableCMD",
                    Name = "Disable Command Prompt for Users",
                    Description = "Prevent users from accessing Command Prompt to limit attack surface",
                    DetailedInfo = "ACSC High Priority: Malicious actors can use Command Prompt to execute built-in Windows tools for reconnaissance and lateral movement.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.High,
                    RegistryPath = @"HKCU\SOFTWARE\Policies\Microsoft\Windows\System",
                    RegistryKey = "DisableCMD",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2, // 2 = Disable CMD but allow batch files
                    DefaultValue = 0,
                    ImpactWarning = "Users will not be able to run Command Prompt. Set to 2 to still allow batch files.",
                    Tags = new[] { "acsc", "high-priority", "cmd", "command-prompt" },
                    IsACSC = true
                },

                // Local Group Policy Processing
                new HardeningSetting
                {
                    Id = "ACSC_AlwaysProcessGPO",
                    Name = "Always Process Group Policy",
                    Description = "Process Group Policy objects even if they haven't changed",
                    DetailedInfo = "ACSC High Priority: Ensures security settings are consistently enforced even if malicious actors attempt to manipulate them.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\Group Policy\{35378EAC-683F-11D2-A89A-00C04FBBCFA2}",
                    RegistryKey = "NoGPOListChanges",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "high-priority", "gpo", "policy" },
                    IsACSC = true
                },

                // Prevent Local Group Policy Changes
                new HardeningSetting
                {
                    Id = "ACSC_PreventLocalPolicyChange",
                    Name = "Prevent Local Group Policy Modifications",
                    Description = "Prevent users from modifying Local Group Policy settings",
                    DetailedInfo = "ACSC High Priority: Relying on users to set Group Policy creates potential for misconfiguration or malicious manipulation.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer",
                    RegistryKey = "DisableLocalMachineRun",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "high-priority", "gpo", "local-policy" },
                    IsACSC = true
                },

                // Disable AutoRun/AutoPlay
                new HardeningSetting
                {
                    Id = "ACSC_DisableAutoRun",
                    Name = "Disable AutoRun for All Drives",
                    Description = "Disable automatic execution features for removable media",
                    DetailedInfo = "ACSC Essential Eight ML3: Automatic execution can allow malware on removable media to execute without user interaction.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer",
                    RegistryKey = "NoDriveTypeAutoRun",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 255, // Disable for all drive types
                    DefaultValue = 145,
                    Tags = new[] { "acsc", "essential-eight", "autorun", "removable-media" },
                    IsACSC = true
                },

                // Disable File and Print Sharing
                new HardeningSetting
                {
                    Id = "ACSC_DisableFileSharing",
                    Name = "Disable File Sharing Within Profile",
                    Description = "Prevent users from sharing files within their profile",
                    DetailedInfo = "ACSC High Priority: Local file sharing can expose sensitive information on a compromised workstation.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer",
                    RegistryKey = "NoFileSharingControl",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    ImpactWarning = "Users won't be able to share files from their profile. Use network shares instead.",
                    Tags = new[] { "acsc", "high-priority", "file-sharing", "network" },
                    IsACSC = true
                },

                // Centralized Logging
                new HardeningSetting
                {
                    Id = "ACSC_AuditLogFull",
                    Name = "Halt When Audit Log Full",
                    Description = "Shut down system when security audit log is full",
                    DetailedInfo = "ACSC: Prevents malicious actors from conducting activities when audit logs cannot record them.",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.High,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "CrashOnAuditFail",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    ImpactWarning = "System will shut down if audit log becomes full. Ensure adequate log size and monitoring.",
                    Tags = new[] { "acsc", "audit", "logging", "security" },
                    IsACSC = true
                }
            };
        }

        public static List<HardeningSetting> GetMediumPrioritySettings()
        {
            return new List<HardeningSetting>
            {
                // Anonymous Connection Restrictions
                new HardeningSetting
                {
                    Id = "ACSC_RestrictAnonymousSAM",
                    Name = "Restrict Anonymous SAM Enumeration",
                    Description = "Do not allow anonymous enumeration of SAM accounts",
                    DetailedInfo = "ACSC Medium Priority: Prevents attackers from enumerating user accounts anonymously.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "RestrictAnonymousSAM",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "medium-priority", "anonymous", "sam" },
                    IsACSC = true
                },

                new HardeningSetting
                {
                    Id = "ACSC_RestrictAnonymous",
                    Name = "Restrict Anonymous Access to Named Pipes and Shares",
                    Description = "Do not allow anonymous enumeration of SAM accounts and shares",
                    DetailedInfo = "ACSC Medium Priority: Further restricts anonymous access to system resources.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "RestrictAnonymous",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "medium-priority", "anonymous", "shares" },
                    IsACSC = true
                },

                new HardeningSetting
                {
                    Id = "ACSC_EveryoneIncludesAnonymous",
                    Name = "Exclude Anonymous from Everyone Group",
                    Description = "Let Everyone permissions not apply to anonymous users",
                    DetailedInfo = "ACSC Medium Priority: Ensures anonymous users don't inherit Everyone group permissions.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "EveryoneIncludesAnonymous",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "medium-priority", "anonymous", "permissions" },
                    IsACSC = true
                },

                // Account Lockout Policy
                new HardeningSetting
                {
                    Id = "ACSC_LockoutThreshold",
                    Name = "Account Lockout Threshold (5 attempts)",
                    Description = "Lock account after 5 invalid logon attempts",
                    DetailedInfo = "ACSC Medium Priority: Protects against brute-force password attacks.",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Command,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "net accounts /lockoutthreshold:5",
                    RevertCommand = "net accounts /lockoutthreshold:0",
                    Tags = new[] { "acsc", "medium-priority", "lockout", "password" },
                    IsACSC = true
                },

                new HardeningSetting
                {
                    Id = "ACSC_LockoutDuration",
                    Name = "Account Lockout Duration (15 minutes)",
                    Description = "Lock account for 15 minutes after exceeding threshold",
                    DetailedInfo = "ACSC Medium Priority: Slows down brute-force attacks.",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Command,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "net accounts /lockoutduration:15",
                    RevertCommand = "net accounts /lockoutduration:30",
                    Tags = new[] { "acsc", "medium-priority", "lockout", "duration" },
                    IsACSC = true
                },

                new HardeningSetting
                {
                    Id = "ACSC_LockoutWindow",
                    Name = "Account Lockout Reset Window (15 minutes)",
                    Description = "Reset account lockout counter after 15 minutes",
                    DetailedInfo = "ACSC Medium Priority: Controls the window for counting failed logon attempts.",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Command,
                    Risk = RiskLevel.Low,
                    ApplyCommand = "net accounts /lockoutwindow:15",
                    RevertCommand = "net accounts /lockoutwindow:30",
                    Tags = new[] { "acsc", "medium-priority", "lockout", "reset" },
                    IsACSC = true
                },

                // Endpoint Device Controls - External Media
                new HardeningSetting
                {
                    Id = "ACSC_DenyWriteRemovable",
                    Name = "Deny Write Access to Removable Media",
                    Description = "Prevent writing data to removable storage devices",
                    DetailedInfo = "ACSC Medium Priority: Prevents data exfiltration via USB drives and other removable media.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.High,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\RemovableStorageDevices",
                    RegistryKey = "Deny_Write",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    ImpactWarning = "Users will not be able to write to USB drives, external hard drives, etc.",
                    Tags = new[] { "acsc", "medium-priority", "removable", "usb", "dlp" },
                    IsACSC = true
                },

                new HardeningSetting
                {
                    Id = "ACSC_DenyExecuteRemovable",
                    Name = "Deny Execute Access on Removable Media",
                    Description = "Prevent execution of programs from removable storage devices",
                    DetailedInfo = "ACSC Medium Priority: Prevents malware execution from USB drives.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\RemovableStorageDevices",
                    RegistryKey = "Deny_Execute",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "medium-priority", "removable", "execute" },
                    IsACSC = true
                },

                // DMA Protection
                new HardeningSetting
                {
                    Id = "ACSC_BlockDMA",
                    Name = "Block DMA Until User Logon",
                    Description = "Block Direct Memory Access ports until user logs on",
                    DetailedInfo = "ACSC Medium Priority: Protects against DMA attacks via Thunderbolt/FireWire ports.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\Kernel DMA Protection",
                    RegistryKey = "DeviceEnumerationPolicy",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0, // 0 = Block all
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "medium-priority", "dma", "thunderbolt" },
                    IsACSC = true
                },

                // Pause Windows Defender Scan
                new HardeningSetting
                {
                    Id = "ACSC_DisablePauseScan",
                    Name = "Disable Pause Windows Defender Scan",
                    Description = "Prevent users from pausing Windows Defender scans",
                    DetailedInfo = "ACSC Medium Priority: Ensures scans complete fully and cannot be interrupted by users or malware.",
                    Category = SettingCategory.WindowsDefender,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows Defender",
                    RegistryKey = "AllowPause",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "medium-priority", "defender", "scan" },
                    IsACSC = true
                }
            };
        }

        public static List<HardeningSetting> GetLowPrioritySettings()
        {
            return new List<HardeningSetting>
            {
                // Show File Extensions
                new HardeningSetting
                {
                    Id = "ACSC_ShowFileExtensions",
                    Name = "Show File Extensions",
                    Description = "Always show file extensions in Windows Explorer",
                    DetailedInfo = "ACSC Low Priority: Helps users identify potentially malicious files disguised with fake extensions.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    RegistryKey = "HideFileExt",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0, // 0 = Show extensions
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "low-priority", "extensions", "explorer" },
                    IsACSC = true
                },

                // Show Hidden Files
                new HardeningSetting
                {
                    Id = "ACSC_ShowHiddenFiles",
                    Name = "Show Hidden Files and Folders",
                    Description = "Display hidden files and folders in Windows Explorer",
                    DetailedInfo = "ACSC Low Priority: Helps detect malware that hides in hidden folders.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    RegistryKey = "Hidden",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1, // 1 = Show hidden
                    DefaultValue = 2,
                    Tags = new[] { "acsc", "low-priority", "hidden", "explorer" },
                    IsACSC = true
                },

                // Show Protected OS Files
                new HardeningSetting
                {
                    Id = "ACSC_ShowSuperHidden",
                    Name = "Show Protected Operating System Files",
                    Description = "Display protected operating system files",
                    DetailedInfo = "ACSC Low Priority: Helps detect rootkits and malware hiding as system files.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                    RegistryKey = "ShowSuperHidden",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    ImpactWarning = "System files will be visible. Be careful not to modify or delete them.",
                    Tags = new[] { "acsc", "low-priority", "system-files", "explorer" },
                    IsACSC = true
                },

                // Disable Recent Documents
                new HardeningSetting
                {
                    Id = "ACSC_NoRecentDocs",
                    Name = "Clear Recent Documents on Exit",
                    Description = "Clear recent documents list when user logs off",
                    DetailedInfo = "ACSC Low Priority: Reduces information disclosure about user activity.",
                    Category = SettingCategory.Privacy,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer",
                    RegistryKey = "ClearRecentDocsOnExit",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "low-priority", "privacy", "recent-docs" },
                    IsACSC = true
                },

                // Disable Storage Sense
                new HardeningSetting
                {
                    Id = "ACSC_DisableStorageSense",
                    Name = "Disable Storage Sense",
                    Description = "Prevent automatic file cleanup that could delete evidence",
                    DetailedInfo = "ACSC Low Priority: Preserves files that may be needed for forensic investigation.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\StorageSense",
                    RegistryKey = "AllowStorageSenseGlobal",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "low-priority", "storage", "cleanup" },
                    IsACSC = true
                }
            };
        }

        public static List<HardeningSetting> GetNetworkSecuritySettings()
        {
            return new List<HardeningSetting>
            {
                // SMB Signing
                new HardeningSetting
                {
                    Id = "ACSC_RequireSMBSigning_Client",
                    Name = "Require SMB Signing (Client)",
                    Description = "Require SMB packet signing for client communications",
                    DetailedInfo = "ACSC: Protects against man-in-the-middle attacks on SMB traffic.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters",
                    RegistryKey = "RequireSecuritySignature",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "smb", "signing", "network" },
                    IsACSC = true
                },

                new HardeningSetting
                {
                    Id = "ACSC_EnableSMBSigning_Client",
                    Name = "Enable SMB Signing (Client)",
                    Description = "Enable SMB packet signing for client communications",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters",
                    RegistryKey = "EnableSecuritySignature",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "smb", "signing", "network" },
                    IsACSC = true
                },

                new HardeningSetting
                {
                    Id = "ACSC_RequireSMBSigning_Server",
                    Name = "Require SMB Signing (Server)",
                    Description = "Require SMB packet signing for server communications",
                    DetailedInfo = "ACSC: Protects against man-in-the-middle attacks when workstation acts as server.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters",
                    RegistryKey = "RequireSecuritySignature",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "smb", "signing", "server" },
                    IsACSC = true
                },

                // LDAP Signing
                new HardeningSetting
                {
                    Id = "ACSC_LDAPClientSigning",
                    Name = "Require LDAP Client Signing",
                    Description = "Require LDAP client to perform signing",
                    DetailedInfo = "ACSC: Protects against man-in-the-middle attacks on LDAP communications.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\LDAP",
                    RegistryKey = "LDAPClientIntegrity",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2, // 2 = Require signing
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "ldap", "signing", "domain" },
                    IsACSC = true
                },

                // NTLMv2 Only
                new HardeningSetting
                {
                    Id = "ACSC_NTLMv2Only",
                    Name = "Use NTLMv2 Only",
                    Description = "Send NTLMv2 response only, refuse LM and NTLM",
                    DetailedInfo = "ACSC: Prevents use of weak LM and NTLMv1 authentication protocols.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "LmCompatibilityLevel",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 5, // 5 = NTLMv2 only, refuse LM and NTLM
                    DefaultValue = 3,
                    ImpactWarning = "May break authentication with very old systems that don't support NTLMv2.",
                    Tags = new[] { "acsc", "ntlm", "authentication", "security" },
                    IsACSC = true
                },

                // Disable LM Hash Storage
                new HardeningSetting
                {
                    Id = "ACSC_NoLMHash",
                    Name = "Do Not Store LM Hash",
                    Description = "Prevent storage of LAN Manager hash on next password change",
                    DetailedInfo = "ACSC: LM hashes are easily cracked. This prevents their storage.",
                    Category = SettingCategory.CredentialProtection,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Lsa",
                    RegistryKey = "NoLMHash",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "lm-hash", "password", "security" },
                    IsACSC = true
                },

                // Disable LLMNR
                new HardeningSetting
                {
                    Id = "ACSC_DisableLLMNR",
                    Name = "Disable LLMNR (Link-Local Multicast Name Resolution)",
                    Description = "Disable LLMNR to prevent credential interception attacks",
                    DetailedInfo = "ACSC: LLMNR can be exploited to intercept credentials on local networks.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient",
                    RegistryKey = "EnableMulticast",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 0,
                    DefaultValue = 1,
                    Tags = new[] { "acsc", "llmnr", "network", "credentials" },
                    IsACSC = true
                },

                // Disable NetBIOS over TCP/IP
                new HardeningSetting
                {
                    Id = "ACSC_DisableNetBIOS",
                    Name = "Disable NetBIOS over TCP/IP",
                    Description = "Disable NetBIOS name resolution to prevent credential attacks",
                    DetailedInfo = "ACSC: NetBIOS can be exploited similar to LLMNR for credential interception.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Medium,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Services\NetBT\Parameters\Interfaces\Tcpip_*",
                    RegistryKey = "NetbiosOptions",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 2, // 2 = Disable NetBIOS
                    DefaultValue = 0,
                    ImpactWarning = "May affect legacy applications that rely on NetBIOS name resolution.",
                    Tags = new[] { "acsc", "netbios", "network", "legacy" },
                    IsACSC = true
                },

                // Disable WPAD
                new HardeningSetting
                {
                    Id = "ACSC_DisableWPAD",
                    Name = "Disable WPAD (Web Proxy Auto-Discovery)",
                    Description = "Disable automatic proxy discovery to prevent man-in-the-middle attacks",
                    DetailedInfo = "ACSC: WPAD can be exploited to redirect traffic through malicious proxies.",
                    Category = SettingCategory.NetworkSecurity,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Wpad",
                    RegistryKey = "WpadOverride",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "wpad", "proxy", "network" },
                    IsACSC = true
                }
            };
        }

        public static List<HardeningSetting> GetPowerShellSettings()
        {
            return new List<HardeningSetting>
            {
                // PowerShell Script Block Logging
                new HardeningSetting
                {
                    Id = "ACSC_PSScriptBlockLogging",
                    Name = "Enable PowerShell Script Block Logging",
                    Description = "Log all PowerShell script blocks for security monitoring",
                    DetailedInfo = "ACSC: Essential for detecting malicious PowerShell usage and forensic analysis.",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging",
                    RegistryKey = "EnableScriptBlockLogging",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "powershell", "logging", "security" },
                    IsACSC = true
                },

                // PowerShell Module Logging
                new HardeningSetting
                {
                    Id = "ACSC_PSModuleLogging",
                    Name = "Enable PowerShell Module Logging",
                    Description = "Log PowerShell module loading and pipeline execution",
                    DetailedInfo = "ACSC: Provides visibility into PowerShell module usage for security monitoring.",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ModuleLogging",
                    RegistryKey = "EnableModuleLogging",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "powershell", "logging", "modules" },
                    IsACSC = true
                },

                // PowerShell Transcription
                new HardeningSetting
                {
                    Id = "ACSC_PSTranscription",
                    Name = "Enable PowerShell Transcription",
                    Description = "Enable automatic PowerShell session transcription",
                    DetailedInfo = "ACSC: Creates detailed logs of all PowerShell sessions for forensic analysis.",
                    Category = SettingCategory.Logging,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.Low,
                    RegistryPath = @"HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription",
                    RegistryKey = "EnableTranscripting",
                    RegistryValueType = "REG_DWORD",
                    RecommendedValue = 1,
                    DefaultValue = 0,
                    Tags = new[] { "acsc", "powershell", "logging", "transcription" },
                    IsACSC = true
                },

                // Constrained Language Mode
                new HardeningSetting
                {
                    Id = "ACSC_PSConstrainedLanguage",
                    Name = "Enable PowerShell Constrained Language Mode",
                    Description = "Restrict PowerShell to constrained language mode",
                    DetailedInfo = "ACSC: Significantly limits PowerShell attack surface by restricting available functionality.",
                    Category = SettingCategory.SystemHardening,
                    Type = SettingType.Registry,
                    Risk = RiskLevel.High,
                    RegistryPath = @"HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Environment",
                    RegistryKey = "__PSLockDownPolicy",
                    RegistryValueType = "REG_SZ",
                    RecommendedValue = "4",
                    DefaultValue = null,
                    ImpactWarning = "Many PowerShell scripts and administrative tools will not work. Test thoroughly.",
                    Tags = new[] { "acsc", "powershell", "constrained", "lockdown" },
                    IsACSC = true
                }
            };
        }

        public static List<HardeningSetting> GetAllSettings()
        {
            var allSettings = new List<HardeningSetting>();
            allSettings.AddRange(GetHighPrioritySettings());
            allSettings.AddRange(GetMediumPrioritySettings());
            allSettings.AddRange(GetLowPrioritySettings());
            allSettings.AddRange(GetNetworkSecuritySettings());
            allSettings.AddRange(GetPowerShellSettings());
            return allSettings;
        }
    }
}
