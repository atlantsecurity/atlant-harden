# Adobe Acrobat / Reader

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Applies Adobe reader hardening — Protected Mode/View sandboxing, Enhanced Security and disabling JavaScript — to blunt the malicious-PDF attacks that target the reader._

**6 settings** on this page &mdash; **6** are part of the Recommended profile.

### Disable File Attachments

Prevent opening of file attachments

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\iFileAttachmentPerms` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable JavaScript

Disable JavaScript execution in PDFs

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bDisableJavaScript` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Some PDF forms may not work

### Enable Enhanced Security

Enable enhanced security in standalone mode

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bEnhancedSecurityStandalone` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Protected Mode

Enable Adobe Reader Protected Mode sandbox

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bProtectedMode` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Protected View

Enable Protected View for all files

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\iProtectedView` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Suppress Upsell Messages

Suppress Adobe upsell and advertising

- **Change:** Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bAcroSuppressUpsell` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended


