# DISA STIG — Google Chrome (V2R11)

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_The full DISA Google Chrome STIG, including strict policy lockdowns beyond the exploitation-prevention subset used by the Recommended profile._

**39 settings** in this category &mdash; **3** are part of the Recommended profile.

### AI-powered History Search must be disabled.

AI History Search is a feature that allows users to search their browsing history and receive generated answers based on page contents and not just the page title and URL. 0 = Allow the feature to be used, while allowing Google to use relevant data to improve its AI models. Relevant data may include…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\"\HistorySearchSettings` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0079 (Vuln V-275784)

### Anonymized data collection must be disabled.

Enable URL-keyed anonymized data collection in Google Chrome and prevent users from changing this setting. URL-keyed anonymized data collection sends URLs of pages the user visits to Google to make searches and browsing better. If you enable this policy, URL-keyed anonymized data collection is alway…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\UrlKeyedAnonymizedDataCollectionEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0066 (Vuln V-221597)

### AutoFill for addresses must be disabled.

Enabling Google Chrome's AutoFill feature allows users to auto complete address information in web forms using previously stored information. If this setting is disabled, Autofill will never suggest or fill address information, nor will it save additional address information that the user might subm…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\AutofillAddressEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0071 (Vuln V-226403)

### AutoFill for credit cards must be disabled.

Enabling Google Chrome's AutoFill feature allows users to auto complete credit card information in web forms using previously stored information. If this setting is disabled, Autofill will never suggest or fill credit card information, nor will it save additional credit card information that the use…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\AutofillCreditCardEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0070 (Vuln V-226402)

### Autoplay must be disabled.

This allows a user to control if videos can play automatically with audio content (without user consent) in Google Chrome. If the policy is set to "True", Google Chrome is allowed to autoplay media. If the policy is set to "False", Google Chrome is not allowed to autoplay media. The "AutoplayAllowli…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\AutoplayAllowed` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0064 (Vuln V-221595)

### Background processing must be disabled.

Determines whether a Google Chrome process is started on OS login that keeps running when the last browser window is closed, allowing background apps to remain active. The background process displays an icon in the system tray and can always be closed from there. If this policy is set to True, backg…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\BackgroundModeEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0017 (Vuln V-221570)

### Browser history must be saved.

This policy disables saving browser history in Google Chrome and prevents users from changing this setting. If this setting is enabled, browsing history is not saved. If this setting is disabled or not set, browsing history is saved.

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\SavingBrowserHistoryDisabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0039 (Vuln V-221581)

### Chrome development tools must be disabled.

While the risk associated with browser development tools is more related to the proper design of a web application, a risk vector remains within the browser. The developer tools allow end users and application developers to view and edit all types of web application related data via the browser. Pag…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\DeveloperToolsAvailability` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0068 (Vuln V-221599)

### Cloud print sharing must be disabled.

Policy enables Google Chrome to act as a proxy between Google Cloud Print and legacy printers connected to the machine. If this setting is enabled or not configured, users can enable the cloud print proxy by authentication with their Google account. If this setting is disabled, users cannot enable t…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\CloudPrintProxyEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0023 (Vuln V-221573)

### Collection of WebRTC event logs must be disabled.

If the policy is set to “true”, Google Chrome is allowed to collect WebRTC event logs from Google services (e.g., Google Meet), and upload those logs to Google. If the policy is set to “false”, or is unset, Google Chrome may not collect nor upload such logs. These logs contain diagnostic information…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\WebRtcEventLogCollectionAllowed` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0067 (Vuln V-221598)

### Create Themes with AI must be disabled.

Create Themes with AI lets users create custom themes/wallpapers by preselecting from a list of options. 0 = Allow the feature to be used, while allowing Google to use relevant data to improve its AI models. Relevant data may include prompts, inputs, outputs, source materials, and written feedback,…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\"\CreateThemesSettings` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0075 (Vuln V-275780)

### Default search provider must be enabled.

Policy enables the use of a default search provider. If you enable this setting, a default search is performed when the user types text in the omnibox that is not a URL. You can specify the default search provider to be used by setting the rest of the default search policies. If these are left empty…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\DefaultSearchProviderEnabled` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0009 (Vuln V-221566)

### Deletion of browser history must be disabled.

Disabling this function will prevent users from deleting their browsing history, which could be used to identify malicious websites and files that could later be used for anti-virus and Intrusion Detection System (IDS) signatures. Furthermore, preventing users from deleting browsing history could be…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\AllowDeletingBrowserHistory` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** DTBC-0052 (Vuln V-221586)

### DevTools Generative AI features must be disabled.

These features in Google Chrome's DevTools employ generative AI models to provide additional debugging information. To use these features, Google Chrome collects data such as error messages, stack traces, code snippets, and network requests and sends them to a server owned by Google, which runs a ge…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\"\DevToolsGenAiSettings` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0076 (Vuln V-275781)

### Extensions installation must be blocklisted by default.

Extensions are developed by third party sources and are designed to extend Google Chrome's functionality. An extension can be made by anyone, to do and access almost anything on a system; this means they pose a high risk to any system that would allow all extensions to be installed by default. Allow…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\ExtensionInstallBlocklist\1` = `*` (REG_MULTI_SZ)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0005 (Vuln V-221562)

### Firewall traversal from remote host must be disabled.

Remote connections should never be allowed that bypass the firewall, as there is no way to verify if they can be trusted. Enables usage of STUN and relay servers when remote clients are trying to establish a connection to this machine. If this setting is enabled, then remote clients can discover and…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\RemoteAccessHostFirewallTraversal` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0001 (Vuln V-221558)

### GenAI local foundational model must be disabled.

Configure how Google Chrome downloads the foundational GenAI model and uses it for inference locally. When the policy is set to Allowed (0) or not set, the model is downloaded automatically, and used for inference. When the policy is set to Disabled (1), the model will not be downloaded. Model downl…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\"\GenAILocalFoundationalModelSettings` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0077 (Vuln V-275782)

### Google Cast must be disabled.

If this policy is set to ”True” or is not set, Google Cast will be enabled, and users will be able to launch it from the app menu, page context menus, media controls on Cast-enabled websites, and (if shown) the “Cast toolbar” icon. If this policy set to ”False”, Google Cast will be disabled.

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\EnableMediaRouter` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0063 (Vuln V-221594)

### Google Data Synchronization must be disabled.

Disables data synchronization in Google Chrome using Google-hosted synchronization services and prevents users from changing this setting. If you enable this setting, users cannot change or override this setting in Google Chrome. If this policy is left not set the user will be able to enable Google…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\SyncDisabled` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0020 (Vuln V-221571)

### Guest Mode must be disabled.

If this policy is set to true or not configured, Google Chrome will enable guest logins. Guest logins are Google Chrome profiles where all windows are in incognito mode. If this policy is set to false, Google Chrome will not allow guest profiles to be started.

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\BrowserGuestModeEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0069 (Vuln V-226401)

### Help Me Write must be disabled.

Help Me Write is an AI-based writing assistant for short-form content on the web. Suggested content is based on prompts entered by the user and the content of the web page. 0 = Allow the feature to be used, while allowing Google to use relevant data to improve its AI models. Relevant data may includ…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\"\HelpMeWriteSettings` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0078 (Vuln V-275783)

### Import AutoFill form data must be disabled.

This policy forces the autofill form data to be imported from the previous default browser if enabled. If enabled, this policy also affects the import dialog. If disabled, the autofill form data is not imported. If it is not set, the user may be asked whether to import, or importing may happen autom…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\ImportAutofillFormData` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0072 (Vuln V-226404)

### Importing of saved passwords must be disabled.

Importing of saved passwords should be disabled as it could lead to unencrypted account passwords stored on the system from another browser to be viewed. This policy forces the saved passwords to be imported from the previous default browser if enabled. If enabled, this policy also affects the impor…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\ImportSavedPasswords` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0029 (Vuln V-221577)

### Incognito mode must be disabled.

Incognito mode allows the user to browse the Internet without recording their browsing history/activity. From a forensics perspective, this is unacceptable. Best practice requires that browser history is retained. The "IncognitoModeAvailability" setting controls whether the user may utilize Incognit…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\IncognitoModeAvailability` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0030 (Vuln V-221578)

### Metrics reporting to Google must be disabled.

Enables anonymous reporting of usage and crash-related data about Google Chrome to Google and prevents users from changing this setting. If you enable this setting, anonymous reporting of usage and crash-related data is sent to Google. A crash report could contain sensitive information from the comp…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\MetricsReportingEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0026 (Vuln V-221575)

### Network prediction must be disabled.

Enables network prediction in Google Chrome and prevents users from changing this setting. If you enable or disable this setting, users cannot change or override this setting in Google Chrome. If this policy is left not set, this will be disabled but the user will be able to change it.

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\NetworkPredictionOptions` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0025 (Vuln V-221574)

### Online revocation checks must be performed.

By setting this policy to true, the previous behavior is restored and online OCSP/CRL checks will be performed. If the policy is not set, or is set to false, then Chrome will not perform online revocation checks. Certificates are revoked when they have been compromised or are no longer valid, and th…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\EnableOnlineRevocationChecks` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** DTBC-0037 (Vuln V-221579)

### Prompt for download location must be enabled.

If the policy is enabled, the user will be asked where to save each file before downloading. If the policy is disabled, downloads will start immediately, and the user will not be asked where to save the file. If the policy is not configured, the user will be able to change this setting.

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\PromptForDownloadLocation` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0053 (Vuln V-221587)

### Safe Browsing Extended Reporting must be disabled.

Enables Google Chrome's Safe Browsing Extended Reporting and prevents users from changing this setting. Extended Reporting sends some system information and page content to Google servers to help detect dangerous apps and sites. If the setting is set to "True", then reports will be created and sent…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\SafeBrowsingExtendedReportingEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0057 (Vuln V-221590)

### Safe Browsing must be enabled.

Allows you to control whether Google Chrome's Safe Browsing feature is enabled and the mode it operates in. If this policy is set to 'NoProtection' (value 0), Safe Browsing is never active. If this policy is set to 'StandardProtection' (value 1, which is the default), Safe Browsing is always active…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\SafeBrowsingProtectionLevel` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** DTBC-0038 (Vuln V-221580)

### Search suggestions must be disabled.

Search suggestion should be disabled as it could lead to searches being conducted that were never intended to be made. Enables search suggestions in Google Chrome's omnibox and prevents users from changing this setting. If you enable this setting, search suggestions are used. If you disable this set…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\SearchSuggestEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0027 (Vuln V-221576)

### Site tracking users location must be disabled.

Website tracking is the practice of gathering information as to which websites were accesses by a browser. The common method of doing this is to have a website create a tracking cookie on the browser. If the information of what sites are being accessed is made available to unauthorized persons, this…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\DefaultGeolocationSetting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0002 (Vuln V-221559)

### Sites ability to show pop-ups must be disabled.

Chrome allows you to manage whether unwanted pop-up windows appear. Pop-up windows that are opened when the end user clicks a link are not blocked. If you enable this policy setting, most unwanted pop-up windows are prevented from appearing. If you disable this policy setting, pop-up windows are not…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\DefaultPopupsSetting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0004 (Vuln V-221561)

### Tab Compare Settings must be disabled.

Tab Compare is an AI-powered tool for comparing information across a user's tabs. For example, the feature can be offered to the user when multiple tabs with products in a similar category are open. 0 = Allow the feature to be used, while allowing Google to use relevant data to improve its AI models…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\"\TabCompareSettings` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0080 (Vuln V-275785)

### The Password Manager must be disabled.

Enables saving passwords and using saved passwords in Google Chrome. Malicious sites may take advantage of this feature by using hidden fields gain access to the stored information. If you enable this setting, users can have Google Chrome memorize passwords and provide them automatically the next ti…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\PasswordManagerEnabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0011 (Vuln V-221567)

### The URL protocol schema javascript must be disabled.

Each access to a URL is handled by the browser according to the URL's "scheme". The "scheme" of a URL is the section before the ":". The term "protocol" is often mistakenly used for a "scheme". The difference is that the scheme is how the browser handles a URL and the protocol is how the browser com…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\URLBlocklist\1` = `javascript://*` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0021 (Vuln V-221572)

### Use of the QUIC protocol must be disabled.

QUIC is used by more than half of all connections from the Chrome web browser to Google's servers, and this activity is undesirable in the DoD. Setting the policy to Enabled or leaving it unset allows the use of QUIC protocol in Google Chrome. Setting the policy to Disabled disallows the use of QUIC…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\QuicAllowed` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0074 (Vuln V-245538)

### Web Bluetooth API must be disabled.

Setting the policy to 3 lets websites ask for access to nearby Bluetooth devices. Setting the policy to 2 denies access to nearby Bluetooth devices. Leaving the policy unset lets sites ask for access, but users can change this setting. 2 = Do not allow any site to request access to Bluetooth devices…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\DefaultWebBluetoothGuardSetting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0073 (Vuln V-241787)

### WebUSB must be disabled.

Allows you to set whether websites are allowed to get access to connected USB devices. Access can be completely blocked, or the user can be asked every time a website wants to get access to connected USB devices. If this policy is left not set, ”3” will be used, and the user will be able to change i…

- **Change:** Sets `HKLM\Software\Policies\Google\Chrome\DefaultWebUsbGuardSetting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** DTBC-0058 (Vuln V-221591)


