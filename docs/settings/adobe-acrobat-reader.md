# Adobe Acrobat / Reader

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Applies Adobe reader hardening — Protected Mode/View sandboxing, Enhanced Security and disabling JavaScript — to blunt the malicious-PDF attacks that target the reader._

**26 settings** on this page &mdash; **13** are part of the Recommended profile.

### Block File Attachments

Prevent opening or launching PDF file attachments (STIG V-213174)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\iFileAttachmentPerms` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block Unknown URL Access

Block PDFs from opening unknown/untrusted URLs (STIG V-213173)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cDefaultLaunchURLPerms\iUnknownURLPerms` = `3` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Adobe Cloud (Send)

Disable the Adobe cloud 'Send' plug-in (STIG V-213177)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cCloud\bAdobeSendPluginToggle` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Adobe Document Cloud

Disable Adobe Document Cloud services (STIG V-213178)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cServices\bToggleAdobeDocumentServices` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Adobe Sign

Disable the Adobe Sign integration (STIG V-213183)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cServices\bToggleAdobeSign` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Adobe Trust List Download

Stop auto-download of the Adobe Approved Trust List (STIG V-213191)

- **Change:** Sets `HKCU\Software\Adobe\Acrobat Reader\DC\Security\cDigSig\cAdobeDownload\bLoadSettingsFromURL` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Adobe Updater

Disable the in-app updater (org-managed patching) (STIG V-213187)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cServices\bUpdater` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** Disables Acrobat/Reader's own updater -- only use if you patch it centrally, or it will go unpatched.

### Disable EUTL Trust List Download

Stop auto-download of the European Union Trusted List (STIG V-213190)

- **Change:** Sets `HKCU\Software\Adobe\Acrobat Reader\DC\Security\cDigSig\cEUTLDownload\bLoadSettingsFromURL` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Flash in PDFs

Disable the legacy Flash player inside PDFs (STIG V-213175)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bEnableFlash` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Installer Repair/Modify

Prevent repair/modify of the installation (anti-tamper) (STIG V-213180)

- **Change:** Sets `HKLM\Software\Adobe\Acrobat Reader\DC\Installer\DisableMaintenance` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable JavaScript

Disable JavaScript execution in PDFs

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bDisableJavaScript` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Some interactive PDF forms may not work

### Disable Preference Sync

Disable syncing preferences to the Adobe cloud (STIG V-213179)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cServices\bTogglePrefsSync` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable SharePoint Integration

Disable SharePoint / Office 365 connections (STIG V-213185)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cSharePoint\bDisableSharePointFeatures` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Third-Party Web Connectors

Disable third-party cloud/web connectors (STIG V-213181)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cServices\bToggleWebConnectors` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Trusted Folders

Disable privileged locations (trusted files/folders) (STIG V-213188)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bDisableTrustedFolders` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Trusted Sites

Disable privileged locations (trusted sites) (STIG V-213189)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bDisableTrustedSites` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Webmail

Disable sending PDFs via webmail (STIG V-213184)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cWebmailProfiles\bDisableWebmail` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Welcome Screen

Hide the Adobe welcome / onboarding screen (STIG V-213186)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cWelcomeScreen\bShowWelcomeScreen` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Enable Enhanced Security (Browser)

Enforce Enhanced Security in the browser plug-in (STIG V-213169)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bEnhancedSecurityInBrowser` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Enhanced Security (Standalone)

Enforce Enhanced Security outside the browser (STIG V-213168)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bEnhancedSecurityStandalone` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable FIPS Mode

Force FIPS 140 cryptography in Acrobat/Reader (STIG V-213193)

- **Change:** Sets `HKCU\Software\Adobe\Acrobat Reader\DC\AVGeneral\bFIPSMode` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** FIPS mode restricts cryptography and can break some signing workflows.

### Enable Protected Mode

Run Adobe in the Protected Mode sandbox (STIG V-213170)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bProtectedMode` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Protected View

Open every PDF in Protected View (STIG V-213171)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\iProtectedView` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Lock Default PDF Handler

Prevent switching the default PDF handler (STIG V-213176)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bDisablePDFHandlerSwitching` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Restrict URL Access from PDFs

Restrict PDFs from silently accessing URLs (STIG V-213172)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\cDefaultLaunchURLPerms\iURLPerms` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Suppress Upsell Messages

Suppress Adobe upsell and advertising (STIG V-213182)

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bAcroSuppressUpsell` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended


