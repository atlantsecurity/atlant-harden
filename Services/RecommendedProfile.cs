using AtlantHarden.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AtlantHarden.Services
{
    /// <summary>
    /// Decides which settings belong to the "Recommended" (and tighter "Basic/Essentials")
    /// security profile, using a real-world return-on-investment model rather than blanket
    /// risk levels or full STIG compliance.
    ///
    /// PHILOSOPHY
    /// ----------
    /// Defend against how malware and attackers ACTUALLY get in and run, and skip controls
    /// that cripple a normal/power-user machine without stopping real attacks.
    ///
    /// INCLUDE (high ROI, low friction):
    ///   • Anti-malware execution: all useful ASR rules, Defender real-time/cloud/network/PUA.
    ///   • Anti-exploitation: SmartScreen/Safe Browsing, browser site isolation/sandbox, SEHOP,
    ///     DEP/ASLR, DMA protection, blocking SSL-error/SmartScreen override.
    ///   • Document & script malware: Office macro-from-Internet blocking, Protected View,
    ///     ActiveX/DDE hardening, neutralizing script file types (.js/.vbs/.hta/...), disabling
    ///     Windows Script Host, blocking LOLBins (mshta/wscript/certutil/...) from the network.
    ///   • Credential theft: LSASS protection (RunAsPPL/ASR-LSASS), WDigest off, NoLMHash,
    ///     NTLMv2-only, SMB signing, anonymous/null-session restrictions.
    ///   • Legacy protocols / network poisoning: SMBv1 off, LLMNR/NBT-NS/WPAD off, LDAP signing.
    ///   • Privilege & autorun: UAC enabled, AlwaysInstallElevated off, Autorun/Autoplay off.
    ///   • Visibility: PowerShell script-block/module logging, process command-line auditing.
    ///   • Modern crypto: disable SSL2/3, TLS1.0/1.1, RC4/DES.
    ///
    /// EXCLUDE (low ROI and/or high friction — the "compliance theater" for a personal machine):
    ///   • Browser convenience removal: disable Password Manager, InPrivate/Incognito, history
    ///     deletion, sync, autofill, import, dev tools, Copilot, spellcheck, QUIC, etc.
    ///     (If the browser is exploited the attacker reads the password store regardless — so
    ///      PREVENTING exploitation matters far more than removing the password manager.)
    ///   • Usability/feature lockdowns: disable Bluetooth, camera, command prompt, convenience
    ///     PIN, Game DVR, lock-screen features, Storage Sense.
    ///   • Brittle/aggressive controls: Controlled Folder Access, PowerShell Constrained Language
    ///     Mode, FIPS mode, BitLocker pre-boot PIN, disable IPv6/DCOM, block-low-prevalence ASR,
    ///     deny-write/execute on removable media, halt-on-audit-full.
    ///   • DoD policy/telemetry items that don't affect malware (telemetry levels, consumer
    ///     features, domain-only netlogon/GPO, RDP host hardening, privacy toggles).
    /// </summary>
    public static class RecommendedProfile
    {
        // Browser controls that genuinely reduce exploitation/phishing/MITM risk (allow-list:
        // for browser categories ONLY these keys are recommended; all the feature-disabling
        // STIG settings are intentionally left out).
        private static readonly HashSet<string> BrowserSecurityKeys = new(StringComparer.OrdinalIgnoreCase)
        {
            "SitePerProcess",                         // site isolation (Spectre/UXSS)
            "SmartScreenEnabled",                     // malware/phishing protection
            "SmartScreenPuaEnabled",                  // block potentially unwanted apps
            "PreventSmartScreenPromptOverride",       // can't click through site warnings
            "PreventSmartScreenPromptOverrideForFiles",// can't click through download warnings
            "SSLErrorOverrideAllowed",                // can't click through cert errors
            "AllowDeletingBrowserHistory",            // prevent users/malware wiping history (anti-tamper)
            "SSLVersionMin",                          // modern TLS floor
            "EnableOnlineRevocationChecks",           // cert revocation
            "SafeBrowsingProtectionLevel",            // enhanced safe browsing (Chrome)
            "AdvancedProtectionAllowed",              // advanced protection (Chrome)
            "AudioSandboxEnabled",                    // sandbox audio process (Chrome)
            "DnsOverHttpsMode", "DNSOverHTTPS",       // DoH (anti-tamper/MITM)
            "TLS13HardeningForLocalAnchorsEnabled",   // TLS 1.3 hardening (Chrome)
            "AllowOutdatedPlugins",                   // block outdated plugins (Chrome)
            "EnableTrackingProtection",               // tracking protection (Firefox)
            "DisableForgetButton",                    // Firefox: prevent quick history/data wipe (anti-tamper)
            "NativeMessagingUserLevelHosts"           // shrink extension native-messaging surface
        };

        private static readonly HashSet<SettingCategory> BrowserCategories = new()
        {
            SettingCategory.EdgeHardening, SettingCategory.ChromeHardening, SettingCategory.FirefoxHardening,
            SettingCategory.StigEdge, SettingCategory.StigChrome, SettingCategory.StigFirefox
        };

        // Adobe Acrobat/Reader: the PDF exploitation / malware-execution mitigations belong in
        // Recommended. The cloud/service/feature toggles (Document Cloud, Sign, webmail, SharePoint,
        // prefs sync, welcome screen, disable-updater, FIPS, digital-signature URL fetch) are
        // Maximum-only — same treatment as browser feature-removal. Allow-list of Recommended keys:
        private static readonly HashSet<string> AdobeSecurityKeys = new(StringComparer.OrdinalIgnoreCase)
        {
            "bProtectedMode", "iProtectedView", "bEnhancedSecurityStandalone", "bEnhancedSecurityInBrowser",
            "bDisableJavaScript", "iFileAttachmentPerms", "bEnableFlash", "iURLPerms", "iUnknownURLPerms",
            "bDisablePDFHandlerSwitching", "bDisableTrustedFolders", "bDisableTrustedSites", "bAcroSuppressUpsell"
        };

        // Script/scrap file types that are pure malware-delivery vectors and are never
        // double-clicked legitimately. (.bat/.cmd/.ps1/.reg/.iso/.url are intentionally left
        // runnable — power users invoke those directly.)
        private static readonly HashSet<string> NeutralizeExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            "js", "jse", "vbs", "vbe", "wsf", "wsh", "ws", "wsc", "hta", "scr", "chm", "iqy", "slk", "pif"
        };

        // Registry keys to exclude from the mostly-include OS/Office/network categories
        // (chosen because the key is unambiguous; names collide too often to rely on).
        private static readonly HashSet<string> ExcludedKeys = new(StringComparer.OrdinalIgnoreCase)
        {
            "DisabledComponents",            // Disable IPv6 — breaks services/tunnels
            "EnableDCOM",                    // Disable DCOM — breaks apps/remote
            "DisableCMD",                    // Disable Command Prompt — cripples power users
            "__PSLockDownPolicy",            // PowerShell Constrained Language — breaks scripting
            "CrashOnAuditFail",              // Halts (BSODs) the box when the audit log fills
            "DisableDomainCreds",            // No cached creds — breaks offline laptop logon
            "EnableVirtualizationBasedSecurity", "LsaCfgFlags", // VBS/Credential Guard — VM/driver friction
            "UseAdvancedStartup", "UseTPMPIN",                  // BitLocker pre-boot PIN — every-boot friction
            "AllowBluetooth",                // Disable Bluetooth — breaks peripherals
            "AllowDomainPINLogon",           // Disable convenience PIN — Windows Hello PIN is good security+UX
            "AllowGameDVR",                  // Disable Game DVR — no security value
            "AutoConnectAllowedOEM",         // Wi-Fi Sense — dead feature
            "NC_ShowSharedAccessUI",         // Disable ICS — low value
            "AllowStorageSenseGlobal",       // not security
            "Deny_Write", "Deny_Execute",    // Block removable media R/W/X — USB friction
            "NoFileSharingControl",          // breaks in-profile file sharing
            "DisableInventory", "MSAOptional", "DisableWindowsConsumerFeatures",
            "DisableThirdPartySuggestions", "DisableUserAuth",  // consumer/telemetry, not malware
            "DisableEnclosureDownload",      // RSS — niche
            "AllowIndexingEncryptedStoresOrItems",
            "LimitEnhancedDiagnosticDataWindowsAnalytics", "AllowTelemetry", "DODownloadMode",
            "NoLockScreenCamera", "NoLockScreenSlideshow", "Value", // camera/lock-screen feature
            "DCSettingIndex", "ACSettingIndex",  // password on resume from sleep — friction
            "AllowWindowsInkWorkspace", "LetAppsActivateWithVoice", "LetAppsActivateWithVoiceAboveLock",
            "NoToastApplicationNotificationOnLockScreen",
            "SuppressionPolicy",             // remove "Run as different user" — minor
            "DisableLocalMachineRun", "NoGPOListChanges", // local/Domain GPO — low value/ friction
            "RequireSecurityDevice",         // Hello-for-Business hardware — niche
            "DisableWebPnPDownload", "NoWebServices", "DisableHTTPPrinting", // printing over HTTP — printer friction
            // Keep OS/Explorer SmartScreen at the overridable "Warn" level rather than a hard
            // "Block" — a hard block prevents launching unsigned/low-reputation apps with no
            // "Run anyway", which would lock the user out of relaunching THIS tool. SmartScreen
            // itself stays ON via EnableSmartScreen/SmartScreenEnabled.
            "ShellSmartScreenLevel",
        };

        // Name fragments (lower-cased) that mark friction/feature/policy items in the
        // mostly-include categories. Names come from official STIG titles and are stable.
        private static readonly string[] ExcludedNameFragments =
        {
            "remote desktop",            // RDP host/client hardening (incl. password saving) — niche/friction
            "ip helper service",         // disabling iphlpsvc breaks IPv6 tunnels/Teredo and some VPNs
            "internet connection sharing",
            "fips",                      // FIPS mode — breaks many apps
            "8.3 filename",
            "clickonce",
            "consumer experience", "consumer account",
            "microsoft account",
            "telemetry", "diagnostic data", "inventory",
            "game recording", "game dvr",
            "cortana", "copilot", "voice",
            "ink workspace",
            "lock screen", "slide show",
            "camera",
            "run as different user",
            "over http", "web publishing", "online ordering", "print driver packages",
            "rss feed",
            "storage sense",
            "wi-fi sense",
            "convenience pin",
            "bitlocker",
            "credential guard", "virtualization-based security",
            "resume from sleep", "resume (on battery", "prompted for a password on resume",
            "smart device", "expansion pack", "flash player",       // Office legacy feature toggles
            "sharepoint server",
            "junk email",
            "updates from other",                                    // delivery optimization (privacy)
            "third-party",
            "indexing of encrypted",
        };

        // Defender settings to leave out (false-positive-prone / privacy).
        private static readonly string[] DefenderExcludedNameFragments =
        {
            "controlled folder access",  // blocks legit apps writing to Documents/Desktop
            "high cloud block level",    // aggressive — false positives on niche software
            "send all samples"           // uploads documents — privacy
        };

        // ASR rules to leave out of Recommended (false positives on legitimate niche software).
        private static readonly string[] AsrExcludedNameFragments =
        {
            "low prevalence"             // blocks brand-new/rare legit executables
        };

        /// <summary>
        /// The smart default: high-ROI, low-friction protection against real attacks.
        /// </summary>
        public static bool IsRecommended(HardeningSetting s)
        {
            // Attack Surface Reduction — the single best anti-malware control set.
            if (!string.IsNullOrEmpty(s.ASRGuid))
                return !MatchesAny(s.Name, AsrExcludedNameFragments);

            switch (s.Category)
            {
                // Privacy is a separate concern from malware/exploitation; not part of the
                // security baseline.
                case SettingCategory.Privacy:
                    return false;

                // Bloatware removal is a destructive, review-first cleanup action — never part
                // of an automatically-applied security profile.
                case SettingCategory.Bloatware:
                    return false;

                // Windows Defender core protections (minus the brittle/privacy ones).
                case SettingCategory.WindowsDefender:
                    return !MatchesAny(s.Name, DefenderExcludedNameFragments);

                // LOLBin egress blocking — high value; only msiexec is left out (legit network installs).
                case SettingCategory.Firewall:
                    return !s.Name.Contains("msiexec", StringComparison.OrdinalIgnoreCase);

                // Office: keep the real anti-document-malware controls (macro-from-Internet
                // blocking, Protected View, ActiveX/DDE/Forms, unsigned add-in blocking, file
                // validation, Outlook object-model prompts). But DON'T hard-block opening legacy
                // file formats — that stops users opening their existing .doc/.xls/.ppt files
                // (Protected View already sandboxes them). Macro-sheet (XLM) blocks are kept.
                case SettingCategory.OfficeHardening:
                case SettingCategory.StigOffice365:
                {
                    var nm = (s.Name ?? string.Empty).ToLowerInvariant();
                    if ((nm.Contains("open/save") || nm.Contains("binary drawings")) && !nm.Contains("macrosheet"))
                        return false;                                                          // legacy format open-block (Word/Excel/PPT/Visio)
                    if (nm.Contains("default file block")) return false;                      // hard-block (no Protected View)
                    return !MatchesAny(s.Name, ExcludedNameFragments);                         // junk-mail/flash/etc.
                }

                // Neutralize only the pure-malware script/scrap file types.
                case SettingCategory.FileAssociations:
                    return IsNeutralizeExtension(s.Id);

                // Browsers: ONLY the exploitation/phishing/MITM mitigations; never the
                // feature-removal settings (password manager, InPrivate, history, sync, ...).
                case SettingCategory.EdgeHardening:
                case SettingCategory.ChromeHardening:
                case SettingCategory.FirefoxHardening:
                case SettingCategory.StigEdge:
                case SettingCategory.StigChrome:
                case SettingCategory.StigFirefox:
                    return BrowserSecurityKeys.Contains(s.RegistryKey ?? string.Empty);

                // Adobe: only the PDF exploitation/malware mitigations (allow-list); the
                // cloud/service/feature toggles and FIPS are Maximum-only.
                case SettingCategory.AdobeReader:
                    return AdobeSecurityKeys.Contains(s.RegistryKey ?? string.Empty);

                // Everything else (OS, credential, network, Office, logging, crypto, autorun):
                // mostly include — they are genuine attack-prevention — minus the friction list.
                default:
                    if (ExcludedKeys.Contains(s.RegistryKey ?? string.Empty)) return false;
                    if (MatchesAny(s.Name, ExcludedNameFragments)) return false;
                    return true;
            }
        }

        /// <summary>
        /// Essentials ("Basic"): the can't-go-wrong core for someone who wants the biggest
        /// risk reduction with effectively zero friction. A strict subset of Recommended.
        /// </summary>
        public static bool IsEssential(HardeningSetting s)
        {
            if (!IsRecommended(s)) return false;

            if (!string.IsNullOrEmpty(s.ASRGuid)) return true;          // all recommended ASR rules
            if (s.Category == SettingCategory.WindowsDefender) return true;
            if (s.Category == SettingCategory.FileAssociations) return true; // script neutralization

            // Browser exploitation/phishing protections.
            if (BrowserCategories.Contains(s.Category))
                return s.RegistryKey != null &&
                       (s.RegistryKey.StartsWith("SmartScreen", StringComparison.OrdinalIgnoreCase) ||
                        s.RegistryKey.Equals("SitePerProcess", StringComparison.OrdinalIgnoreCase) ||
                        s.RegistryKey.Equals("SafeBrowsingProtectionLevel", StringComparison.OrdinalIgnoreCase) ||
                        s.RegistryKey.Equals("AdvancedProtectionAllowed", StringComparison.OrdinalIgnoreCase) ||
                        s.RegistryKey.StartsWith("PreventSmartScreen", StringComparison.OrdinalIgnoreCase));

            // The highest-value OS / credential / document / visibility controls.
            var key = s.RegistryKey ?? string.Empty;
            if (EssentialKeys.Contains(key)) return true;
            var name = s.Name?.ToLowerInvariant() ?? string.Empty;
            if (name.Contains("smartscreen")) return true;                       // Explorer SmartScreen
            if (name.Contains("macro") && name.Contains("internet")) return true;// macros from Internet
            if (name.Contains("smbv1") || name.Contains("smb v1")) return true;  // SMBv1 off
            if (name.Contains("autorun") || name.Contains("autoplay")) return true;
            if (name.Contains("script host")) return true;                       // disable WSH

            return false;
        }

        private static readonly HashSet<string> EssentialKeys = new(StringComparer.OrdinalIgnoreCase)
        {
            "SMB1", "NoLMHash", "RunAsPPL", "UseLogonCredential", "Negotiate", "LmCompatibilityLevel",
            "EnableLUA", "ConsentPromptBehaviorAdmin", "FilterAdministratorToken",
            "AlwaysInstallElevated", "NoAutorun", "NoDriveTypeAutoRun", "NoAutoplayfornonVolume",
            "EnableScriptBlockLogging", "ProcessCreationIncludeCmdLine_Enabled",
            "blockcontentexecutionfrominternet", "VBAWarnings"
        };

        private static bool IsNeutralizeExtension(string? id)
        {
            if (string.IsNullOrEmpty(id) || !id.StartsWith("FileAssoc_", StringComparison.OrdinalIgnoreCase))
                return false;
            var ext = id.Substring("FileAssoc_".Length);
            return NeutralizeExtensions.Contains(ext);
        }

        private static bool MatchesAny(string? text, string[] fragments)
        {
            if (string.IsNullOrEmpty(text)) return false;
            var t = text.ToLowerInvariant();
            foreach (var f in fragments)
                if (t.Contains(f)) return true;
            return false;
        }
    }
}
