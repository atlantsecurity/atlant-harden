# DISA STIG — Microsoft Edge (V2R5)

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_The full DISA Microsoft Edge STIG. Many of these are strict lockdowns (disabling sync, InPrivate, imports, autofill) that go beyond exploitation prevention and add day-to-day friction — which is why only the exploitation-relevant ones appear in the Recommended profile._

**52 settings** on this page &mdash; **7** are part of the Recommended profile.

### A website's ability to query for payment methods must be disabled.

This setting determines whether websites can check if the user has payment methods saved. If this policy is disabled, websites that use "PaymentRequest.canMakePayment" or "PaymentRequest.hasEnrolledInstrument" API will be informed that no payment methods are available. If this policy is enabled or i…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PaymentMethodQueryEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000055 (Vuln V-235767)

### Access to Microsoft 365 Copilot writing assistance must be disabled.

This policy controls whether users can use writing support features in Microsoft Edge for Business, such as Rewrite, which utilizes Microsoft 365 Copilot Chat.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ComposeInlineEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000069 (Vuln V-279940)

### Autofill for addresses must be disabled.

Enables the AutoFill feature and allows users to auto-complete address information in web forms using previously stored information. If this policy is disabled, AutoFill never suggests or fills credit card information, nor will it save additional credit card information that users might submit while…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AutofillAddressEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000029 (Vuln V-235746)

### Autofill for Credit Cards must be disabled.

Enables the Microsoft Edge AutoFill feature and lets users auto complete credit card information in web forms using previously stored information. If this policy is disabled, AutoFill never suggests or fills credit card information, nor will it save additional credit card information that users migh…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AutofillCreditCardEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000028 (Vuln V-235745)

### AutoplayAllowed must be set to disabled.

This policy sets the media autoplay policy for websites. The default setting "Not configured" respects the current media autoplay settings and lets users configure their autoplay settings. Setting to "Enabled" sets media autoplay to "Allow". All websites are allowed to autoplay media. Users cannot o…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AutoplayAllowed` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000024 (Vuln V-235741)

### Background processing must be disabled.

Background processing allows Microsoft Edge processes to start at OS sign-in and keep running after the last browser window is closed. In this scenario, background apps and the current browsing session remain active, including any session cookies. An open background process displays an icon in the s…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\BackgroundModeEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000006 (Vuln V-235724)

### Browser history must be saved.

This setting disables deleting browser history and download history and prevents users from changing this setting.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AllowDeletingBrowserHistory` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** EDGE-00-000033 (Vuln V-235750)

### Bypassing Microsoft Defender SmartScreen prompts for sites must be disabled.

This policy setting allows a decision to be made on whether users can override the Microsoft Defender SmartScreen warnings about potentially malicious websites. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are blocked from continuing to the site. If th…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PreventSmartScreenPromptOverride` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** EDGE-00-000002 (Vuln V-235720)

### Bypassing of Microsoft Defender SmartScreen warnings about downloads must be disabled.

This policy setting allows a decision to be made on whether users can override Microsoft Defender SmartScreen warnings about unverified downloads. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are prevented from completing the unverified downloads. If t…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PreventSmartScreenPromptOverrideForFiles` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** EDGE-00-000003 (Vuln V-235721)

### Copilot must be disabled.

The Sidebar is a launcher bar on the right side of Microsoft Edge's screen. If this policy is enabled or not configured, the Sidebar will be shown. If this policy is disabled, the Sidebar will never be shown. Disabling Sidebar will disable Copilot.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\HubsSidebarEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000066 (Vuln V-260466)

### Data Synchronization must be disabled.

Disables data synchronization in Microsoft Edge. This policy also prevents the sync consent prompt from appearing. If this policy is not set or applied as recommended, users will be able to turn sync on or off. If this policy is applied as mandatory, users will not be able to turn on sync.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SyncDisabled` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000010 (Vuln V-235727)

### Edge development tools must be disabled.

While the risk associated with browser development tools is more related to the proper design of a web application, a risk vector remains within the browser. The developer tools allow end users and application developers to view and edit all types of web application-related data via the browser. Pag…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DeveloperToolsAvailability` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000034 (Vuln V-235751)

### Extensions installation must be blocklisted by default.

List specific extensions that users cannot install in Microsoft Edge. When this policy is deployed, any extensions on this list that were previously installed will be disabled, and the user will not be able to enable them. If an item is removed from the list of blocked extensions, the extension is a…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ExtensionInstallBlocklist\1` = `*` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000041 (Vuln V-235754)

### FriendlyURLs must be disabled.

If FriendlyURLs are enabled, Microsoft Edge will compute additional representations of the URL and place them on the clipboard. This policy configures what format will be pasted when the user pastes in external applications, or inside Microsoft Edge without the "Paste As" context menu item. If confi…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ConfigureFriendlyURLFormat` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000068 (Vuln V-266981)

### Google Cast must be disabled.

Enable this policy to enable Google Cast. Users will be able to launch it from the app menu, page context menus, media controls on Cast-enabled websites, and (if shown) the Cast toolbar icon. Disable this policy to disable Google Cast. By default, Google Cast is enabled.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\EnableMediaRouter` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000026 (Vuln V-235743)

### Guest mode must be disabled.

Enabling Guest mode allows the use of guest profiles in Microsoft Edge. In a guest profile, the browser does not import browsing data from existing profiles, and it deletes browsing data when all guest profiles are closed. If this policy is enabled or not configured, Microsoft Edge lets users browse…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\BrowserGuestModeEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000060 (Vuln V-235772)

### Importing of autofill form data must be disabled.

Allows users to import autofill form data from another browser into Microsoft Edge. If this policy is enabled, the option to manually import autofill data is automatically selected. If this policy is disabled, autofill form data is not imported at first run, and users cannot import it manually. If t…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportAutofillFormData` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000013 (Vuln V-235730)

### Importing of browser settings must be disabled.

Allows users to import browser settings from another browser into Microsoft Edge. If this policy is enabled, the Browser settings check box is automatically selected in the Import browser data dialog box. If this policy is disabled, browser settings are not imported at first run, and users cannot im…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportBrowserSettings` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000014 (Vuln V-235731)

### Importing of browsing history must be disabled.

Allows users to import their browsing history from another browser into Microsoft Edge. If this policy is enabled, the Browsing history check box is automatically selected in the Import browser data dialog box. If this policy is disabled, browsing history data is not imported at first run, and users…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportHistory` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000017 (Vuln V-235734)

### Importing of cookies must be disabled.

Allows users to import cookies from another browser into Microsoft Edge. If this policy is disabled, cookies are not imported on first run. If this policy is not configured, cookies are imported on first run.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportCookies` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000015 (Vuln V-235732)

### Importing of extensions must be disabled.

Allows users to import extensions from another browser into Microsoft Edge. If this policy is enabled, the Extensions check box is automatically selected in the Import browser data dialog box. If this policy is disabled, extensions are not imported at first run, and users cannot import them manually…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportExtensions` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000016 (Vuln V-235733)

### Importing of home page settings must be disabled.

Allows users to import their home page setting from another browser into Microsoft Edge. If this policy is enabled, the option to manually import the home page setting is automatically selected. If this policy is disabled, the home page setting is not imported at first run, and users cannot import i…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportHomepage` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000018 (Vuln V-235735)

### Importing of open tabs must be disabled.

Allows users to import open and pinned tabs from another browser into Microsoft Edge. If this policy is enabled, the Open tabs check box is automatically selected in the Import browser data dialog box. If this policy is disabled, open tabs are not imported at first run, and users cannot import them…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportOpenTabs` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000019 (Vuln V-235736)

### Importing of payment info must be disabled.

Allows users to import payment info from another browser into Microsoft Edge. If this policy is enabled, the payment info check box is automatically selected in the Import browser data dialog box. If this policy is disabled, payment info is not imported at first run, and users cannot import it manua…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportPaymentInfo` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000020 (Vuln V-235737)

### Importing of saved passwords must be disabled.

Allows users to import saved passwords from another browser into Microsoft Edge. If this policy is enabled, the option to manually import saved passwords is automatically selected. If this policy is disabled, saved passwords are not imported on first run, and users cannot import them manually.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportSavedPasswords` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000021 (Vuln V-235738)

### Importing of search engine settings must be disabled.

Allows users to import search engine settings from another browser into Microsoft Edge. If this policy is enabled, the option to import search engine settings is automatically selected. If this policy is disabled, search engine settings are not imported at first run, and users cannot import them man…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportSearchEngine` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000022 (Vuln V-235739)

### Importing of shortcuts must be disabled.

Allows users to import Shortcuts from another browser into Microsoft Edge. If this policy is disabled, Shortcuts are not imported on first run. If this policy is not configured, Shortcuts are imported on first run.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportShortcuts` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000023 (Vuln V-235740)

### InPrivate mode must be disabled.

This setting specifies whether the user can open pages in InPrivate mode in Microsoft Edge. If this policy is not configured or set it to "Enabled", users can open pages in InPrivate mode. Set this policy to "Disabled" to stop users from using InPrivate mode. Set this policy to "Forced" to always us…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\InPrivateModeAvailability` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000005 (Vuln V-235723)

### Microsoft Defender SmartScreen must be configured to block potentially unwanted apps.

This policy setting configures blocking for potentially unwanted apps with Microsoft Defender SmartScreen. Potentially unwanted app blocking with Microsoft Defender SmartScreen provides warning messages to help protect users from adware, coin miners, bundleware, and other low-reputation apps that ar…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SmartScreenPuaEnabled` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** EDGE-00-000051 (Vuln V-235764)

### Microsoft Defender SmartScreen must be enabled.

This policy setting configures Microsoft Defender SmartScreen, which provides warning messages to help protect users from potential phishing scams and malicious software. By default, Microsoft Defender SmartScreen is turned on. If this setting is enabled, Microsoft Defender SmartScreen is turned on.…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SmartScreenEnabled` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** EDGE-00-000050 (Vuln V-235763)

### Network prediction must be disabled.

Enables network prediction and prevents users from changing this setting. This controls DNS prefetching, TCP and SSL pre-connection, and pre-rendering of web pages. If this policy is not configured, network prediction is enabled but the user can change it. Policy options mapping: - NetworkPrediction…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\NetworkPredictionOptions` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000011 (Vuln V-235728)

### Online revocation checks must be performed.

If you enable this policy, Microsoft Edge will perform soft-fail, online OCSP/CRL checks. "Soft fail" means that if the revocation server can't be reached, the certificate will be considered valid. If you disable the policy or don't configure it, Microsoft Edge won't perform online revocation checks…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\EnableOnlineRevocationChecks` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** EDGE-00-000030 (Vuln V-235747)

### Personalization of ads, search, and news by sending browsing history to Microsoft must be disabled.

This policy prevents Microsoft from collecting a user's Microsoft Edge browsing history to be used for personalizing advertising, search, news and other Microsoft services. This setting is only available for users with a Microsoft account. This setting is not available for child accounts or enterpri…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PersonalizationReportingEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000031 (Vuln V-235748)

### Relaunch notification must be required.

Users must be required to restart the browser to finish installation of pending updates and prevent users from continually using an old/vulnerable browser version.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\RelaunchNotification` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000061 (Vuln V-235773)

### Search suggestions must be disabled.

Enables web search suggestions in the Microsoft Edge Address Bar and Auto-Suggest List, and prevents users from changing this policy. If this policy is enabled, web search suggestions are used. If this policy is disabled, web search suggestions are never used; however, local history and local favori…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SearchSuggestEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000012 (Vuln V-235729)

### Session only-based cookies must be enabled.

Cookies must only be allowed per session and only for approved URLs as permanently stored cookies can be used for malicious intent. Approved URLs may be allowlisted via the "CookiesAllowedForUrls" or "SaveCookiesOnExit" policy settings, but these are not requirements.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultCookieSetting` = `4` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000067 (Vuln V-260467)

### Site isolation for every site must be enabled.

The "SitePerProcess" policy can be used to prevent users from opting out of the default behavior of isolating all sites. The "IsolateOrigins" policy can be used to isolate additional, finer-grained origins. Enabling this policy prevents users from opting out of the default behavior where each site r…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SitePerProcess` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** EDGE-00-000047 (Vuln V-235760)

### Site tracking of a user’s location must be disabled.

Set whether websites can track users' physical locations. Tracking can be allowed by default ("AllowGeolocation") or denied by default ("BlockGeolocation"), or the user can be asked each time a website requests their location ("AskGeolocation"). If this policy is not configured, "AskGeolocation" is…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultGeolocationSetting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000032 (Vuln V-235749)

### Spell checking provided by Microsoft Editor must be disabled.

The Microsoft Editor service provides enhanced spell and grammar checking for editable text fields on web pages. If this policy is enabled or incorrectly configured, Microsoft Editor spell check can be used for eligible text fields. If you disable this policy, spell check can only be provided by loc…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\Computer Configuration/Administrative Templates/Microsoft Edge/Spell checking provided by Microsoft Editor must be set to Disabled. Use the Windows Registry Editor to navigate to the following key: HKLM\SOFTWARE\Policies\Microsoft\Edge If the value for MicrosoftEditorProofingEnabled is not set to REG_DWORD = 0, this is a finding.` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000070 (Vuln V-283439)

### Suggestions of similar web pages in the event of a navigation error must be disabled.

This setting allows Microsoft Edge to issue a connection to a web service to generate URL and search suggestions for connectivity issues such as DNS errors. If this policy is enabled, a web service is used to generate URL and search suggestions for network errors. If this policy is disabled, no call…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AlternateErrorPagesEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000056 (Vuln V-235768)

### Supported authentication schemes must be configured.

This setting specifies which HTTP authentication schemes are supported. The policy can be configured by using these values: "basic", "digest", "ntlm", and "negotiate". Separate multiple values with commas. If this policy is not configured, all four schemes are used.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AuthSchemes` = `ntlm,negotiate` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000048 (Vuln V-235761)

### The ability of sites to show pop-ups must be disabled.

Set whether websites can show pop-up windows. Pop-ups can be allowed on all websites ("AllowPopups") or blocked on all sites ("BlockPopups"). If this policy is configured, pop-up windows are blocked by default, and users can change this setting. Policy options mapping: - AllowPopups (1) = Allow all…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultPopupsSetting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000008 (Vuln V-235725)

### The built-in DNS client must be disabled.

This setting controls whether to use the built-in DNS client. This does not affect which DNS servers are used; it only controls the software stack that is used to communicate with them. For example, if the operating system is configured to use an enterprise DNS server, that same server would be used…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\BuiltInDnsClientEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000062 (Vuln V-235774)

### The collections feature must be disabled.

This setting allows users to access the Collections feature, where they can collect, organize, share, and export content more efficiently and with Office integration. If this policy is enabled or not configured, users can access and use the Collections feature in Microsoft Edge. If this policy is di…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\EdgeCollectionsEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000058 (Vuln V-235770)

### The download location prompt must be configured.

This setting provides positive feedback before a download starts, limiting the possibility of inadvertent downloads without notifying the user.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PromptForDownloadLocation` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000052 (Vuln V-235765)

### The Password Manager must be disabled.

Enable Microsoft Edge to save user passwords. If this policy is enabled, users can save their passwords in Microsoft Edge. The next time the user visits the site, Microsoft Edge will enter the password automatically.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PasswordManagerEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000043 (Vuln V-235756)

### The Share Experience feature must be disabled.

If this policy is set to "ShareAllowed" (the default), users will be able to access the Windows 10 Share experience from the Settings and More menu in Microsoft Edge to share with other apps on the system. If this policy is set to "ShareDisallowed", users will not be able to access the Windows 10 Sh…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ConfigureShare` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000059 (Vuln V-235771)

### Use of the QUIC protocol must be disabled.

QUIC is used by more than half of all connections from the Edge web browser to Google's servers, and this activity is undesirable in the DoD. If you enable this policy or don't configure it, the QUIC protocol is allowed. If you disable this policy, the QUIC protocol is blocked.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\QuicAllowed` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000063 (Vuln V-246736)

### User feedback must be disabled.

Microsoft Edge uses the Edge Feedback feature (enabled by default) to allow users to send feedback, suggestions, or customer surveys and to report any issues with the browser. By default, users cannot disable (turn off) the Edge Feedback feature. If this policy is enabled or not configured, users ca…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\UserFeedbackAllowed` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000057 (Vuln V-235769)

### Visual Search must be disabled.

Visual Search allows for quick exploration of more related content about entities in an image. If this policy is enabled or not configured, Visual Search will be enabled via image hover, context menu, and search in Sidebar. If this policy is disabled, Visual Search will be disabled and more informat…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\VisualSearchEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000065 (Vuln V-260465)

### Web Bluetooth API must be disabled.

Control whether websites can access nearby Bluetooth devices. Access can be blocked completely or the site required to ask the user each time it wants to access a Bluetooth device. If this policy is not configured, the default value ('AskWebBluetooth', meaning users are asked each time) is used and…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultWebBluetoothGuardSetting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000027 (Vuln V-235744)

### WebUSB must be disabled.

Set whether websites can access connected USB devices. Access can be blocked completely or the user asked each time a website wants to get access to connected USB devices. Override this policy for specific URL patterns by using the WebUsbAskForUrls and WebUsbBlockedForUrls policies. If this policy i…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultWebUsbGuardSetting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** EDGE-00-000025 (Vuln V-235742)


