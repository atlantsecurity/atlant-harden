# Google Chrome Hardening

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Enterprise-policy hardening for Google Chrome — site isolation, Enhanced Safe Browsing, TLS 1.3 hardening, DNS-over-HTTPS and certificate revocation checks._

**18 settings** on this page &mdash; **9** are part of the Recommended profile.

### Block Outdated Plugins

Block running of outdated plugins

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\AllowOutdatedPlugins` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Autoplay

Disable automatic media playback

*Why it matters:* This allows a user to control if videos can play automatically with audio content (without user consent) in Google Chrome. If the policy is set to "True", Google Chrome is allowed to autoplay media. If the policy is set to "False", Google Chrome is not allowed to autoplay media. The "AutoplayAllowli…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\AutoplayAllowed` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Background Mode

Prevent Chrome from running in background

*Why it matters:* Determines whether a Google Chrome process is started on OS login that keeps running when the last browser window is closed, allowing background apps to remain active. The background process displays an icon in the system tray and can always be closed from there. If this policy is set to True, backg…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\BackgroundModeEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Chrome Metrics

Disable usage statistics reporting

*Why it matters:* Enables anonymous reporting of usage and crash-related data about Google Chrome to Google and prevents users from changing this setting. If you enable this setting, anonymous reporting of usage and crash-related data is sent to Google. A crash report could contain sensitive information from the comp…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\MetricsReportingEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Guest Mode

Disable Chrome guest browsing mode

*Why it matters:* If this policy is set to true or not configured, Google Chrome will enable guest logins. Guest logins are Google Chrome profiles where all windows are in incognito mode. If this policy is set to false, Google Chrome will not allow guest profiles to be started.

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\BrowserGuestModeEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Incognito Mode

Disable incognito browsing mode for compliance

*Why it matters:* Incognito mode allows the user to browse the Internet without recording their browsing history/activity. From a forensics perspective, this is unacceptable. Best practice requires that browser history is retained. The "IncognitoModeAvailability" setting controls whether the user may utilize Incognit…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\IncognitoModeAvailability` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** Users won't be able to use Incognito mode

### Disable Media Router

Disable Chrome Cast/media router functionality

*Why it matters:* If this policy is set to ”True” or is not set, Google Cast will be enabled, and users will be able to launch it from the app menu, page context menus, media controls on Cast-enabled websites, and (if shown) the “Cast toolbar” icon. If this policy set to ”False”, Google Cast will be disabled.

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\EnableMediaRouter` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Password Import

Prevent importing saved passwords

*Why it matters:* Importing of saved passwords should be disabled as it could lead to unencrypted account passwords stored on the system from another browser to be viewed. This policy forces the saved passwords to be imported from the previous default browser if enabled. If enabled, this policy also affects the impor…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\ImportSavedPasswords` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Password Manager

Disable built-in password manager

*Why it matters:* Enables saving passwords and using saved passwords in Google Chrome. Malicious sites may take advantage of this feature by using hidden fields gain access to the stored information. If you enable this setting, users can have Google Chrome memorize passwords and provide them automatically the next ti…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\PasswordManagerEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Search Suggestions

Disable search and URL suggestions

*Why it matters:* Search suggestion should be disabled as it could lead to searches being conducted that were never intended to be made. Enables search suggestions in Google Chrome's omnibox and prevents users from changing this setting. If you enable this setting, search suggestions are used. If you disable this set…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\SearchSuggestEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Enable Advanced Protection

Enable Chrome Advanced Protection Program features

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\AdvancedProtectionAllowed` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Audio Sandbox

Run audio processing in a sandboxed process

- **Change:** Sets `HKLM\SOFTWARE\Policies\Google\Chrome\AudioSandboxEnabled` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Certificate Revocation Checks

Enable online certificate revocation checks

*Why it matters:* By setting this policy to true, the previous behavior is restored and online OCSP/CRL checks will be performed. If the policy is not set, or is set to false, then Chrome will not perform online revocation checks. Certificates are revoked when they have been compromised or are no longer valid, and th…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\EnableOnlineRevocationChecks` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable DNS over HTTPS

Enable encrypted DNS queries

- **Change:** Sets `HKLM\SOFTWARE\Policies\Google\Chrome\DnsOverHttpsMode` = `automatic` (REG_SZ)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Enhanced Safe Browsing

Enable enhanced safe browsing protection

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\Recommended\SafeBrowsingProtectionLevel` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Site Isolation

Run each site in its own process

- **Change:** Sets `HKLM\SOFTWARE\Policies\Google\Chrome\SitePerProcess` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable TLS 1.3 Hardening

Enable TLS 1.3 hardening for local anchors

- **Change:** Sets `HKLM\SOFTWARE\Policies\Google\Chrome\TLS13HardeningForLocalAnchorsEnabled` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enforce TLS 1.1 Minimum

Set minimum TLS version to 1.1

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\SSLVersionMin` = `tls1.1` (REG_SZ)
- **Risk:** Low  &middot;  **Profile:** Recommended


