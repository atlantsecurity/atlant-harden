# AtlantHarden — Windows, Browser & Office Security Hardening Tool

![Atlant Security Logo](Resources/logo.png)

AtlantHarden is a comprehensive Windows 10/11 security hardening application with a modern, professional UI. It applies **599 hardening settings** — including **354 DISA STIG controls** across Windows 11, Edge, Chrome, Firefox, and Office 365 (latest releases), plus the **ACSC Essential Eight** — through one-click, review-before-apply profiles with full backup and restore. The released build is a single **self-contained executable** (no .NET runtime to install).

## ⬇️ Download

### [Download AtlantHarden v2.0 for Windows](https://github.com/atlantsecurity/atlant-harden/releases/latest/download/AtlantHarden.zip)

Single `.zip`, ~63 MB, **self-contained** — no .NET install required. Extract it, then right-click `AtlantHarden.exe` → **Run as administrator**. It's not code-signed yet, so SmartScreen may prompt — choose *More info → Run anyway*. See all [releases and notes](https://github.com/atlantsecurity/atlant-harden/releases/latest).

## Features

### 🛡️ Comprehensive Security Categories
- **Windows Defender** - Configure real-time protection, PUA detection, and cloud protection
- **Attack Surface Reduction** - All 19 Microsoft ASR rules for Office, scripts, USB, and ransomware protection
- **Network Security** - Harden SMB, NTLM, LDAP signing, and disable legacy protocols (DISA STIG tagged)
- **Credential Protection** - Protect LSASS, disable WDigest, and prevent Mimikatz attacks (DISA STIG tagged)
- **Browser Hardening** - Secure Edge, Chrome, and Firefox with enterprise policies (DISA STIG: Edge V2R5, Chrome V2R11, Firefox V6R7)
- **Office Hardening** - Disable macros, DDE, and protect against document-based attacks (DISA STIG: Office 365 ProPlus V3R5)
- **Privacy Settings** - Control telemetry, advertising ID, and data collection
- **Logging & Auditing** - Enable PowerShell logging, process auditing, and event log sizing (DISA STIG tagged)
- **File Associations** - Neutralize dangerous file types to prevent ransomware
- **Windows Firewall** - Block LOLBins from network access
- **TLS/Cryptography** - Disable weak ciphers and enforce modern TLS (DISA STIG tagged)
- **System Hardening** - UAC, DEP, ASLR, DLL protection, and more (DISA STIG tagged)
- **Adobe Reader** - Apply STIG-compliant security settings

### 🎯 Attack Surface Reduction (ASR) Rules
All 19 Microsoft ASR rules are supported with real-time status verification:
- Block Office applications from creating child processes
- Block Office apps from injecting code into other processes
- Block Win32 API calls from Office macros
- Block executable content from email client and webmail
- Block execution of potentially obfuscated scripts
- Block JavaScript/VBScript from launching downloaded content
- Block untrusted/unsigned processes from USB
- Advanced ransomware protection
- Block credential stealing from LSASS
- Block low-prevalence executables
- Block Adobe Reader child processes
- Block Office communication app child processes
- Block WMI event subscription persistence
- Block PSExec/WMI process creation
- Block abuse of vulnerable signed drivers
- Block Safe Mode reboot commands
- Block impersonated system tools
- Block webshell creation for servers

### 🏛️ DISA STIG Compliance
**354 automatable DISA STIG requirements** across five products, sourced from the latest
DISA releases and loaded from an auditable, regenerable catalog (`Resources/stig-catalog.json`):

| Product | STIG Version | Requirements |
|---------|--------------|--------------|
| Microsoft Windows 11 | V2R7 | 114 |
| Microsoft Edge | V2R5 | 52 |
| Google Chrome | V2R11 | 39 |
| Mozilla Firefox | V6R7 | 43 |
| Microsoft Office 365 ProPlus | V3R5 | 106 |

- Dedicated **DISA STIG Compliance** category group in the sidebar (one category per product),
  kept separate from the curated baseline hardening categories
- Per-product compliance breakdown on the dashboard
- Real STIG ID (e.g. `WN11-SO-000195`, `EDGE-00-000002`), Vulnerability ID, and CCIs per setting
- Severity mapped to CAT I/II/III — filter and bulk-select **STIG: CAT I/II/III** within any product
- Org-specific rules (no single correct value) are intentionally excluded from auto-apply
- Catalog generated from Microsoft PowerSTIG + cyber.trackr.live via `tools/Generate-StigCatalog.ps1`

### 🦘 ACSC (Australian Cyber Security Centre) Compliance
34 ACSC Windows Hardening settings based on the July 2024 guidance:
- **High Priority Settings** - Command Prompt restrictions, Group Policy enforcement, AutoRun disabling
- **Medium Priority Settings** - Anonymous access restrictions, account lockout policies, DMA protection, removable media controls
- **Low Priority Settings** - File extension visibility, hidden files, recent documents clearing
- **Network Security** - SMB/LDAP signing, NTLMv2 enforcement, LLMNR/NetBIOS disabling, WPAD protection
- **PowerShell Hardening** - Script block logging, module logging, transcription, constrained language mode

### 🎚️ One-Click Profiles (review before you apply)
Three curated profiles, each with **Apply** and a **Show settings** button that opens a scrollable review of every setting (name, description, registry change, current vs. recommended value) before anything is applied:
- **Basic** (95 settings) - the highest-impact, effectively zero-friction core
- **Recommended** (325 settings) - the smart default: applies the controls that stop real malware and exploitation (ASR, Defender, SmartScreen, macro/script blocking, credential-theft protection, exploit mitigations) while deliberately **skipping** high-friction lockdowns. It does **not** disable browser password managers, InPrivate/Incognito, history deletion, Controlled Folder Access, FIPS, or a BitLocker pre-boot PIN — and is already gaming- and performance-safe.
- **Maximum** (599 settings) - everything, including the strict DISA STIG lockdowns

**Self-protection:** before enabling any setting, AtlantHarden allow-lists its own executable for Microsoft Defender ASR and Controlled Folder Access, and keeps Explorer SmartScreen at an overridable level — so this (unsigned) tool can always be relaunched to revert.

### 💾 Backup & Restore
- **Automatic Backups** - Creates backup before applying any changes
- **System Restore Points** - Create Windows System Restore points from the app
- **Multiple Versions** - Keeps up to 20 timestamped backup versions
- **One-Click Restore** - Easily revert to any previous state
- **REG File Export** - Also exports .reg files for manual restoration

### 📦 Configuration Import/Export
- **Export Configuration** - Save your selected settings to a JSON file
- **Import Configuration** - Load settings from a previously exported file
- **Command Line Support** - Automate deployment with `--config` parameter
- **Silent Mode** - Run unattended with `--silent --apply` flags
- **Profile Sharing** - Share configurations across multiple systems

### 🎨 Professional UI
- Modern dark theme with Atlant Security branding
- Collapsible, grouped sidebar — **Hardening Categories** and **DISA STIG Compliance** sections
- Security Score dashboard with overall, per-product STIG, and ACSC compliance percentages
- "Show settings" review screen for each profile
- Real-time ASR rule status from Windows Defender
- Risk badges (CAT I/II/III), impact warnings, and per-setting STIG ID / Vulnerability ID / CCIs
- Search and CAT-severity filtering across all settings
- Responsive layout that scales down to small screens

## Requirements

- Windows 10 (1709 or later) or Windows 11, 64-bit
- Administrator privileges
- **No .NET runtime required** — the released build is self-contained (building from source needs the .NET 8 SDK)
- Windows Defender enabled (for ASR rules)

## Installation

1. Download the latest release (`AtlantHarden.zip`) and extract it
2. Right-click `AtlantHarden.exe` → **Run as administrator**
   - The download is not yet code-signed, so SmartScreen may prompt — choose *More info → Run anyway*

## Command Line Usage

```
AtlantHarden.exe [options]

Options:
  --config, -c <file>    Load configuration from JSON file
  --apply, -a            Auto-apply the loaded configuration
  --silent, -s           Run in silent mode (no GUI, for automation)
  --help, -h, /?         Show help message

Examples:
  AtlantHarden.exe
      Launch the application normally with GUI

  AtlantHarden.exe --config myconfig.json
      Launch GUI with configuration pre-loaded

  AtlantHarden.exe --config myconfig.json --apply --silent
      Apply configuration silently and exit (for automation/scripts)
```

## Usage

### ⚠️ IMPORTANT: Before You Start
**Always create a System Restore Point in the Backup section before applying any settings!**
Do not rely solely on the app's automatic backups. A manual System Restore Point gives you the best protection.

### Applying Settings

1. Launch the application (requires Administrator privileges)
2. Create a System Restore Point in the Backup section
3. Browse categories using the left sidebar
4. Enable/disable individual settings using the toggle switches
5. Review the risk level and impact warnings
6. Click "Apply Selected" to apply enabled settings
7. A backup will be created automatically

### Disabling Settings

1. Navigate to the category containing the setting
2. Toggle the setting OFF (it will show as enabled but unchecked)
3. Click "Apply Selected"
4. The setting will be reverted to its default/disabled state

### Restoring from Backup

1. Click "Backups & Restore" in the sidebar
2. Select a backup from the list
3. Click "Restore" to revert to that state
4. Some settings may require a reboot

### Keyboard Shortcuts

- `Ctrl+A` - Select all settings in current category
- `Ctrl+D` - Deselect all settings
- `Ctrl+R` - Refresh status
- `Ctrl+S` - Apply selected settings

## Building from Source

### Prerequisites

- Visual Studio 2022 or later
- .NET 8.0 SDK
- Windows 10/11 SDK

### Build Steps

1. Open `AtlantHarden.sln` in Visual Studio
2. Restore NuGet packages
3. Build the solution (Release configuration recommended)
4. Output will be in `bin/Release/net8.0-windows/`

To produce the self-contained single-file release (`AtlantHarden.exe`, no .NET install needed):

```
dotnet publish AtlantHarden.csproj -c Release -r win-x64 --self-contained true ^
  -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true
```

## Security Considerations

⚠️ **Important**: Some settings may affect application compatibility. Always:

1. Create a System Restore point before running
2. Test in a non-production environment first
3. Review impact warnings for each setting
4. Keep backups available for quick rollback

## Credits

Based on the [Windows 10 Security Hardening Script](https://atlantsecurity.com) by Atlant Security.

Additional resources:
- [Microsoft ASR Rules Reference](https://learn.microsoft.com/en-us/defender-endpoint/attack-surface-reduction-rules-reference)
- [DISA STIGs](https://public.cyber.mil/stigs/)
- [LOLBAS Project](https://lolbas-project.github.io/)
- [Microsoft Security Baselines](https://docs.microsoft.com/en-us/windows/security/threat-protection/windows-security-baselines)
- [CIS Benchmarks](https://www.cisecurity.org/benchmark/microsoft_windows_desktop)

## License

This repository is **source-available for transparency and audit — not open source.** You may
read and audit the code, but any *use* of it requires a paid commercial license. See
[`LICENSE`](LICENSE) for the full terms; for commercial licensing, use the
[contact form at atlantsecurity.com](https://atlantsecurity.com/contact).

The **compiled AtlantHarden application** is **free to use as long as you don't charge for its use** —
running it on your own systems (personal or internal to an organization), without charging anyone, is
free. **The moment you charge for use of the app — including using it to secure clients' or commercial
systems as a paid product or service — you need a commercial licensing agreement** with Atlant Security
LTD ([contact form](https://atlantsecurity.com/contact)). This repository license does not govern the binary.

© 2026 Atlant Security LTD. All rights reserved.

## Support

For issues and feature requests, please contact Atlant Security or open an issue on GitHub.

---

**Atlant Security** - Protecting Your Digital Fortress  
[https://atlantsecurity.com](https://atlantsecurity.com)
