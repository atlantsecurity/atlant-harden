# DISA STIG — Mozilla Firefox (V6R7)

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_The full DISA Mozilla Firefox STIG, including strict policy lockdowns beyond the exploitation-prevention subset used by the Recommended profile._

**43 settings** on this page &mdash; **2** are part of the Recommended profile.

### Background submission of information to Mozilla must be disabled.

Firefox by default sends information about Firefox to Mozilla servers. There should be no background submission of technical and other information from DoD computers to Mozilla with portions posted publicly.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableTelemetry` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000014 (Vuln V-251558)

### Firefox accounts must be disabled.

Disable Firefox Accounts integration (Sync). It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may remain unsecured. They increase the risk to…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableFirefoxAccounts` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000034 (Vuln V-251578)

### Firefox autoplay must be disabled.

Autoplay allows the user to control whether videos can play automatically (without user consent) with audio content. The user must be able to select content that is run within the browser window.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\Permissions\Autoplay\Default` = `block-audio-video` (REG_SZ)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000021 (Vuln V-251565)

### Firefox cryptomining protection must be enabled.

The Content Blocking/Tracking Protection feature stops Firefox from loading content from malicious sites. The content might be a script or an image, for example. If a site is on one of the tracker lists that Firefox is set to use, the fingerprinting script (or other tracking script/image) will not b…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\EnableTrackingProtection\Cryptomining` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000024 (Vuln V-251568)

### Firefox deprecated ciphers must be disabled.

A weak cipher is defined as an encryption/decryption algorithm that uses a key of insufficient length. Using an insufficient length for a key in an encryption/decryption algorithm opens up the possibility (or probability) that the encryption scheme could be broken.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisabledCiphers\TLS_RSA_WITH_3DES_EDE_CBC_SHA` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000027 (Vuln V-251571)

### Firefox development tools must be disabled.

Information needed by an attacker to begin looking for possible vulnerabilities in a web browser includes any information about the web browser and plug-ins or modules being used. When debugging or trace information is enabled in a production web browser, information about the web browser, such as w…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableDeveloperTools` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000015 (Vuln V-251559)

### Firefox encrypted media extensions must be disabled.

Enable or disable Encrypted Media Extensions and optionally lock it. If "Enabled" is set to "false", Firefox does not download encrypted media extensions (such as Widevine) unless the user consents to installing them. If "Locked" is set to "true" and "Enabled" is set to "false", Firefox will not dow…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\EncryptedMediaExtensions\Enabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000037 (Vuln V-251581.a)

### Firefox encrypted media extensions must be disabled.

Enable or disable Encrypted Media Extensions and optionally lock it. If "Enabled" is set to "false", Firefox does not download encrypted media extensions (such as Widevine) unless the user consents to installing them. If "Locked" is set to "true" and "Enabled" is set to "false", Firefox will not dow…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\EncryptedMediaExtensions\Locked` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000037 (Vuln V-251581.b)

### Firefox feedback reporting must be disabled.

Disable the menus for reporting sites (Submit Feedback, Report Deceptive Site). It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may remain u…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableFeedbackCommands` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000036 (Vuln V-251580)

### Firefox fingerprinting protection must be enabled.

The Content Blocking/Tracking Protection feature stops Firefox from loading content from malicious sites. The content might be a script or an image, for example. If a site is on one of the tracker lists that Firefox is set to use, the fingerprinting script (or other tracking script/image) will not b…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\EnableTrackingProtection\Fingerprinting` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000023 (Vuln V-251567)

### Firefox must be configured so that DNS over HTTPS is disabled.

DNS over HTTPS has generally not been adopted in the DoD. DNS is tightly controlled. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may rem…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DNSOverHTTPS\Enabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000033 (Vuln V-251577)

### Firefox must be configured to allow only TLS 1.2 or above.

Use of versions prior to TLS 1.2 are not permitted. SSL 2.0 and SSL 3.0 contain a number of security flaws. These versions must be disabled in compliance with the Network Infrastructure and Secure Remote Computing STIGs.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SSLVersionMin` = `tls1.2` (REG_SZ)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** FFOX-00-000002 (Vuln V-251546)

### Firefox must be configured to block pop-up windows.

Pop-up windows may be used to launch an attack within a new browser window with altered settings. This setting blocks pop-up windows created while the page is loading.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\PopupBlocking\Default` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000009 (Vuln V-251553.a)

### Firefox must be configured to block pop-up windows.

Pop-up windows may be used to launch an attack within a new browser window with altered settings. This setting blocks pop-up windows created while the page is loading.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\PopupBlocking\Locked` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000009 (Vuln V-251553.b)

### Firefox must be configured to disable form fill assistance.

To protect privacy and sensitive data, Firefox provides the ability to configure the program so that data entered into forms is not saved. This mitigates the risk of a website gleaning private information from prefilled information.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableFormHistory` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000007 (Vuln V-251551)

### Firefox must be configured to disable the installation of extensions.

A browser extension is a program that has been installed into the browser to add functionality. Where a plug-in interacts only with a web page and usually a third-party external application (e.g., Flash, Adobe Reader), an extension interacts with the browser program itself. Extensions are not embedd…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\InstallAddonsPermission\Default` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000013 (Vuln V-251557)

### Firefox must be configured to not automatically update installed add-ons and plugins.

Set this to false to disable checking for updated versions of the Extensions/Themes. Automatic updates from untrusted sites puts the enclave at risk of attack and may override security settings.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\ExtensionUpdate` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000005 (Vuln V-251549)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Locked` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.f)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Cookies` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.b)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Downloads` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.c)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\FormData` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.d)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\History` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.e)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Cache` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.a)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\OfflineApps` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.g)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Sessions` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.h)

### Firefox must be configured to not delete data upon shutdown.

For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\SiteSettings` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000017 (Vuln V-252881.i)

### Firefox must be configured to not use a password store with or without a master password.

Firefox can be set to store passwords for sites visited by the user. These individual passwords are stored in a file and can be protected by a master password. Autofill of the password can then be enabled when the site is visited. This feature could also be used to autofill the certificate PIN, whic…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\PasswordManagerEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000008 (Vuln V-251552)

### Firefox must have the DOD root certificates installed.

The DOD root certificates will ensure that the trust chain is established for server certificates issued from the DOD Certificate Authority (CA).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\Certificates\ImportEnterpriseRoots` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000016 (Vuln V-251560)

### Firefox must not recommend extensions as the user is using the browser.

The Recommended Extensions program recommends extensions to users as they surf the web. The user must not be encouraged to install extensions from the websites they visit. Allowed extensions are to be centrally managed.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\UserMessaging\ExtensionRecommendations` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000028 (Vuln V-251572)

### Firefox must prevent the user from quickly deleting data.

There should not be an option for a user to "forget" work they have done. This is required to meet nonrepudiation controls.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableForgetButton` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** FFOX-00-000018 (Vuln V-251562)

### Firefox network prediction must be disabled.

If network prediction is enabled, requests to URLs are made without user consent. The browser should always make a direct DNS request without prefetching occurring.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\NetworkPrediction` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000022 (Vuln V-251566)

### Firefox private browsing must be disabled.

Private browsing allows the user to browse the internet without recording their browsing history/activity. From a forensics perspective, this is unacceptable. Best practice requires that browser history is retained.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisablePrivateBrowsing` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000019 (Vuln V-251563)

### Firefox search suggestions must be disabled.

Search suggestions must be disabled as this could lead to searches being conducted that were never intended to be made.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SearchSuggestEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000020 (Vuln V-251564)

### Firefox Studies must be disabled.

Studies try out different features and ideas before they are released to all Firefox users. Testing beta software is not in the DoD user's mission.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableFirefoxStudies` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000039 (Vuln V-252909)

### Pocket must be disabled.

Pocket, previously known as Read It Later, is a social bookmarking service for storing, sharing, and discovering web bookmarks. Data gathering cloud services such as this are generally disabled in the DoD.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisablePocket` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000038 (Vuln V-252908)

### The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…

The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\SponsoredTopSites` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000029 (Vuln V-251573.g)

### The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…

The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\SponsoredPocket` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000029 (Vuln V-251573.f)

### The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…

The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Snippets` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000029 (Vuln V-251573.e)

### The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…

The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Search` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000029 (Vuln V-251573.d)

### The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…

The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Locked` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000029 (Vuln V-251573.b)

### The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…

The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Highlights` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000029 (Vuln V-251573.a)

### The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…

The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\TopSites` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000029 (Vuln V-251573.h)

### The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…

The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Pocket` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** FFOX-00-000029 (Vuln V-251573.c)


