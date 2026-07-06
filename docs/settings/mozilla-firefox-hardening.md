# Mozilla Firefox Hardening

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Enterprise-policy hardening for Mozilla Firefox — TLS floor, DNS-over-HTTPS, and tracking protection._

**10 settings** in this category &mdash; **3** are part of the Recommended profile.

### Disable Default Browser Agent

Disable Firefox default browser agent

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\DisableDefaultBrowserAgent` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Firefox Accounts

Disable Firefox sync and accounts

*Why it matters:* Disable Firefox Accounts integration (Sync). It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may remain unsecured. They increase the risk to…

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\DisableFirefoxAccounts` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Form History

Disable saving form and search history

*Why it matters:* To protect privacy and sensitive data, Firefox provides the ability to configure the program so that data entered into forms is not saved. This mitigates the risk of a website gleaning private information from prefilled information.

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\DisableFormHistory` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Password Manager

Disable built-in password manager (use external)

*Why it matters:* Firefox can be set to store passwords for sites visited by the user. These individual passwords are stored in a file and can be protected by a master password. Autofill of the password can then be enabled when the site is visited. This feature could also be used to autofill the certificate PIN, whic…

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\PasswordManagerEnabled` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Pocket

Disable Pocket integration in Firefox

*Why it matters:* Pocket, previously known as Read It Later, is a social bookmarking service for storing, sharing, and discovering web bookmarks. Data gathering cloud services such as this are generally disabled in the DoD.

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\DisablePocket` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Private Browsing

Disable private browsing mode for compliance

*Why it matters:* Private browsing allows the user to browse the internet without recording their browsing history/activity. From a forensics perspective, this is unacceptable. Best practice requires that browser history is retained.

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\DisablePrivateBrowsing` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** Users won't be able to use private browsing

### Disable Telemetry

Disable Firefox telemetry and data collection

*Why it matters:* Firefox by default sends information about Firefox to Mozilla servers. There should be no background submission of technical and other information from DoD computers to Mozilla with portions posted publicly.

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\DisableTelemetry` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Enable DNS over HTTPS

Enable encrypted DNS queries

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\DNSOverHTTPS` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Tracking Protection

Enable strict tracking protection

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\EnableTrackingProtection` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enforce TLS 1.2 Minimum

Set minimum TLS version to 1.2

*Why it matters:* Use of versions prior to TLS 1.2 are not permitted. SSL 2.0 and SSL 3.0 contain a number of security flaws. These versions must be disabled in compliance with the Network Infrastructure and Secure Remote Computing STIGs.

- **Change:** Sets `HKLM\Software\Policies\Mozilla\Firefox\SSLVersionMin` = `tls1.2` (REG_SZ)
- **Risk:** Low  &middot;  **Profile:** Recommended


