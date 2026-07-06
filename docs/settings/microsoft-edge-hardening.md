# Microsoft Edge Hardening

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Enterprise-policy hardening for Microsoft Edge. The high-value controls prevent exploitation and phishing — site isolation, SmartScreen, TLS enforcement and certificate checks — rather than stripping convenience features._

**12 settings** on this page &mdash; **9** are part of the Recommended profile.

### Block SSL Error Override

Prevent users from bypassing SSL certificate errors

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\SSLErrorOverrideAllowed` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Users won't be able to visit sites with certificate errors

### Disable Background Mode

Prevent Edge from running in background after closing

*Why it matters:* Background processing allows Microsoft Edge processes to start at OS sign-in and keep running after the last browser window is closed. In this scenario, background apps and the current browsing session remain active, including any session cookies. An open background process displays an icon in the s…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\BackgroundModeEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable InPrivate Browsing

Disable InPrivate browsing mode for compliance

*Why it matters:* This setting specifies whether the user can open pages in InPrivate mode in Microsoft Edge. If this policy is not configured or set it to "Enabled", users can open pages in InPrivate mode. Set this policy to "Disabled" to stop users from using InPrivate mode. Set this policy to "Forced" to always us…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\InPrivateModeAvailability` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** Users won't be able to use InPrivate browsing

### Disable Native Messaging User Hosts

Disable user-level native messaging hosts

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\NativeMessagingUserLevelHosts` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Password Manager

Disable built-in password manager (use external)

*Why it matters:* Enable Microsoft Edge to save user passwords. If this policy is enabled, users can save their passwords in Microsoft Edge. The next time the user visits the site, Microsoft Edge will enter the password automatically.

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\PasswordManagerEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Enable PUA Protection

Block potentially unwanted applications in downloads

*Why it matters:* This policy setting configures blocking for potentially unwanted apps with Microsoft Defender SmartScreen. Potentially unwanted app blocking with Microsoft Defender SmartScreen provides warning messages to help protect users from adware, coin miners, bundleware, and other low-reputation apps that ar…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\SmartScreenPuaEnabled` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Site Isolation

Run each site in its own process for better security

*Why it matters:* The "SitePerProcess" policy can be used to prevent users from opting out of the default behavior of isolating all sites. The "IsolateOrigins" policy can be used to isolate additional, finer-grained origins. Enabling this policy prevents users from opting out of the default behavior where each site r…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\SitePerProcess` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable SmartScreen

Enable Microsoft Defender SmartScreen for Edge

*Why it matters:* This policy setting configures Microsoft Defender SmartScreen, which provides warning messages to help protect users from potential phishing scams and malicious software. By default, Microsoft Defender SmartScreen is turned on. If this setting is enabled, Microsoft Defender SmartScreen is turned on.…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\SmartScreenEnabled` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enforce TLS 1.2 Minimum

Set minimum SSL/TLS version to TLS 1.2

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\SSLVersionMin` = `tls1.2` (REG_SZ)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Prevent Deleting Browser History

Prevent users from deleting browsing history

*Why it matters:* This setting disables deleting browser history and download history and prevents users from changing this setting.

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\AllowDeletingBrowserHistory` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Useful for compliance but may frustrate users

### Prevent SmartScreen File Override

Prevent bypassing SmartScreen warnings for downloads

*Why it matters:* This policy setting allows a decision to be made on whether users can override Microsoft Defender SmartScreen warnings about unverified downloads. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are prevented from completing the unverified downloads. If t…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\PreventSmartScreenPromptOverrideForFiles` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Prevent SmartScreen Override

Prevent users from bypassing SmartScreen warnings

*Why it matters:* This policy setting allows a decision to be made on whether users can override the Microsoft Defender SmartScreen warnings about potentially malicious websites. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are blocked from continuing to the site. If th…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Edge\PreventSmartScreenPromptOverride` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended


