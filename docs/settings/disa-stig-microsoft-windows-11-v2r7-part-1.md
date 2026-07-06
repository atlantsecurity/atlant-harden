# DISA STIG — Microsoft Windows 11 (V2R7) &mdash; Part 1 of 2

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_The Windows 11 Security Technical Implementation Guide is DISA's authoritative hardening baseline for U.S. Department of Defense systems. Every item below is a formal STIG requirement with its own STIG ID, Vulnerability ID and CCIs, applying DISA's exact mandated value._

**57 settings** on this page &mdash; **34** are part of the Recommended profile.

### Administrator accounts must not be enumerated during elevation.

Enumeration of administrator accounts when elevating can provide part of the logon information to an unauthorized user. This setting configures the system to always require users to type in a username and password to elevate a running application.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\CredUI\EnumerateAdministrators` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000200 (Vuln V-253391)

### Anonymous access to Named Pipes and Shares must be restricted.

Allowing anonymous access to named pipes or shares provides the potential for unauthorized system access. This setting restricts access to those defined in "Network access: Named Pipes that can be accessed anonymously" and "Network access: Shares that can be accessed anonymously", both of which must…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RestrictNullSessAccess` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000165 (Vuln V-253456)

### Anonymous enumeration of SAM accounts must not be allowed.

Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000145 (Vuln V-253453)

### Anonymous enumeration of shares must be restricted.

Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000150 (Vuln V-253454)

### Attachments must be prevented from being downloaded from RSS feeds.

Attachments from RSS feeds may not be secure. This setting will prevent attachments from being downloaded from RSS feeds.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Internet Explorer\Feeds\DisableEnclosureDownload` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000295 (Vuln V-253407)

### Audit policy using subcategories must be enabled.

Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\SCENoApplyLegacyAuditPolicy` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000030 (Vuln V-253437)

### Automatically signing in the last interactive user after a system-initiated restart must be disabled.

Windows can be configured to automatically sign the user back in after a Windows Update restart. Some protections are in place to help ensure this is done in a secure fashion; however, disabling this will prevent the caching of credentials for this purpose and also ensure the user is aware of the re…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\DisableAutomaticRestartSignOn` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000325 (Vuln V-253413)

### Autoplay must be disabled for all drives.

Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000190 (Vuln V-253388)

### Autoplay must be turned off for non-volume devices.

Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. This setting will disable autoplay for non-volume devices (such as Media T…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Explorer\NoAutoplayfornonVolume` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000180 (Vuln V-253386)

### Bluetooth must be turned off unless approved by the organization.

If not configured properly, Bluetooth may allow rogue devices to communicate with a system. If a rogue device is paired with a system, there is potential for sensitive information to be compromised.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\PolicyManager\current\device\Connectivity\AllowBluetooth` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-00-000210 (Vuln V-253291)

### Camera access from the lock screen must be disabled.

Enabling camera access from the lock screen could allow for unauthorized use. Requiring logon will ensure the device is only used by authorized personnel.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Personalization\NoLockScreenCamera` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000005 (Vuln V-253350)

### Command line data must be included in process creation events.

Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Audit\ProcessCreationIncludeCmdLine_Enabled` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000066 (Vuln V-253367)

### Connections to non-domain networks when connected to a domain authenticated network must be blocked.

Multiple network connections can provide additional attack vectors to a system and must be limited. When connected to a domain, communication must go through the domain connection.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WcmSvc\GroupPolicy\fBlockNonDomain` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000060 (Vuln V-253365)

### Credential Guard must be running on Windows 11 domain-joined systems.

Credential Guard uses virtualization-based security to protect information that could be used in credential theft attacks if compromised. This authentication information, which was stored in the Local Security Authority (LSA) in previous versions of Windows, is isolated from the rest of operating sy…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DeviceGuard\LsaCfgFlags` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000075 (Vuln V-253370)

### Downloading print driver packages over HTTP must be prevented.

Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting prevents…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Printers\DisableWebPnPDownload` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000100 (Vuln V-253374)

### Enhanced anti-spoofing for facial recognition must be enabled on Windows 11.

Enhanced anti-spoofing provides additional protections when using facial recognition with devices that support it.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Biometrics\FacialFeatures\EnhancedAntiSpoofing` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000195 (Vuln V-253389)

### Enhanced diagnostic data must be limited to the minimum required to support Windows Analytics.

Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Limiting this capability will prevent potentially sensitive information from being sent outside the enterprise. The "Enhanced" level for telemetry includes additional informat…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection\LimitEnhancedDiagnosticDataWindowsAnalytics` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000204 (Vuln V-253392)

### Group Policy objects must be reprocessed even if they have not changed.

Enabling this setting and then selecting the "Process even if the Group Policy objects have not changed" option ensures that the policies will be reprocessed even if none have been changed. This way, any unauthorized changes are forced to match the domain-based group policy settings again.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Group Policy\{35378EAC-683F-11D2-A89A-00C04FBBCFA2}\NoGPOListChanges` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000090 (Vuln V-253373)

### Hardened UNC Paths must be defined to require mutual authentication and integrity for at least the \\*\SYSVOL…

Additional security requirements are applied to Universal Naming Convention (UNC) paths specified in Hardened UNC paths before allowing access them. This aids in preventing tampering with or spoofing of connections to these paths.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\NetworkProvider\HardenedPaths\\\*\NETLOGON` = `RequireMutualAuthentication=1, RequireIntegrity=1` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000050 (Vuln V-253362.a)

### Hardened UNC Paths must be defined to require mutual authentication and integrity for at least the \\*\SYSVOL…

Additional security requirements are applied to Universal Naming Convention (UNC) paths specified in Hardened UNC paths before allowing access them. This aids in preventing tampering with or spoofing of connections to these paths.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\NetworkProvider\HardenedPaths\\\*\SYSVOL` = `RequireMutualAuthentication=1, RequireIntegrity=1` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000050 (Vuln V-253362.b)

### Indexing of encrypted files must be turned off.

Indexing of encrypted files may expose sensitive data. This setting prevents encrypted files from being indexed.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Search\AllowIndexingEncryptedStoresOrItems` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000305 (Vuln V-253409)

### Insecure logons to an SMB server must be disabled.

Insecure guest logons allow unauthenticated access to shared folders. Shared resources on a system must require authentication to establish proper access.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\LanmanWorkstation\AllowInsecureGuestAuth` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000040 (Vuln V-253360)

### Internet connection sharing must be disabled.

Internet connection sharing makes it possible for an existing internet connection, such as through wireless, to be shared and used by other systems essentially creating a mobile hotspot. This exposes the system sharing the connection to others with potentially malicious purpose.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Network Connections\NC_ShowSharedAccessUI` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000044 (Vuln V-253361)

### IPv6 source routing must be configured to highest protection.

Configuring the system to disable IPv6 source routing protects against spoofing.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters\DisableIpSourceRouting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000020 (Vuln V-253353)

### Kerberos encryption types must be configured to prevent the use of DES and RC4 encryption suites.

Certain encryption types are no longer considered secure. This setting configures a minimum encryption type for Kerberos, preventing the use of the DES and RC4 encryption suites.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Kerberos\Parameters\SupportedEncryptionTypes` = `2147483640` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000190 (Vuln V-253460)

### Local accounts with blank passwords must be restricted to prevent access from the network.

An account without a password can allow unauthorized access to a system as only the username would be required. Password policies must prevent accounts with blank passwords from existing on a system. However, if a local account with a blank password did exist, enabling this setting will prevent netw…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LimitBlankPasswordUse` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000015 (Vuln V-253434)

### Local administrator accounts must have their privileged token filtered to prevent elevated privileges from bei…

A compromised local administrator account can provide means for an attacker to move laterally between domain systems. With User Account Control enabled, filtering the privileged token for built-in administrator accounts will prevent the elevated privileges of these accounts from being used over the…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\LocalAccountTokenFilterPolicy` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000037 (Vuln V-253357)

### Local drives must be prevented from sharing with Remote Desktop Session Hosts.

Preventing users from sharing the local drives on their client computers to Remote Session Hosts that they access helps reduce possible exposure of sensitive data.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fDisableCdm` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000275 (Vuln V-253403)

### Local users on domain-joined computers must not be enumerated.

The username is one part of logon credentials that could be used to gain access to a system. Preventing the enumeration of users limits this information to authorized personnel.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnumerateLocalUsers` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000130 (Vuln V-253379)

### Microsoft consumer experiences must be turned off.

Microsoft consumer experiences provides suggestions and notifications to users, which may include the installation of Windows Store apps. Organizations may control the execution of applications through other means such as allowlisting. Turning off Microsoft consumer experiences will help prevent the…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CloudContent\DisableWindowsConsumerFeatures` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000197 (Vuln V-253390)

### NTLM must be prevented from falling back to a Null session.

NTLM sessions that are allowed to fall back to Null (unauthenticated) sessions may gain unauthorized access.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\LSA\MSV1_0\allownullsessionfallback` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000180 (Vuln V-253458)

### Outgoing secure channel traffic must be encrypted or signed.

Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but not all information is encrypted. If this policy is enabled, outgoing secure channel traffic will be encrypted and signed.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\RequireSignOrSeal` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000035 (Vuln V-253438)

### Outgoing secure channel traffic must be encrypted.

Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but not all information is encrypted. If this policy is enabled, outgoing secure channel traffic will be encrypted.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\SealSecureChannel` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000040 (Vuln V-253439)

### Outgoing secure channel traffic must be signed.

Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but the channel is not integrity checked. If this policy is enabled, outgoing secure channel traffic will be signed.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\SignSecureChannel` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000045 (Vuln V-253440)

### Passwords must not be saved in the Remote Desktop Client.

Saving passwords in the Remote Desktop Client could allow an unauthorized user to establish a remote desktop session to another system. The system must be configured to prevent users from saving passwords in the Remote Desktop Client.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\DisablePasswordSaving` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000270 (Vuln V-253402)

### PKU2U authentication using online identities must be prevented.

PKU2U is a peer-to-peer authentication protocol. This setting prevents online identities from authenticating to domain-joined systems. Authentication will be centrally managed with Windows user accounts.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\LSA\pku2u\AllowOnlineID` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000185 (Vuln V-253459)

### PowerShell script block logging must be enabled on Windows 11.

Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000326 (Vuln V-253414)

### PowerShell Transcription must be enabled on Windows 11.

Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000327 (Vuln V-253415)

### Printing over HTTP must be prevented.

Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting prevents…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Printers\DisableHTTPPrinting` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000110 (Vuln V-253376)

### Remote calls to the Security Account Manager (SAM) must be restricted to Administrators.

The Windows SAM stores users' passwords. Restricting remote rpc connections to the SAM to Administrators helps protect those credentials.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictRemoteSAM` = `O:BAG:BAD:(A;;RC;;;BA)` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000167 (Vuln V-253457)

### Remote Desktop Services must always prompt a client for passwords upon connection.

This setting controls the ability of users to supply passwords automatically as part of their remote desktop connection. Disabling this setting would allow anyone to use the stored credentials in a connection item to connect to the terminal server.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fPromptForPassword` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000280 (Vuln V-253404)

### Remote Desktop Services must be configured with the client connection encryption set to the required level.

Remote connections must be encrypted to prevent interception of data or sensitive information. Selecting "High Level" will ensure encryption of Remote Desktop Services sessions in both directions.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\MinEncryptionLevel` = `3` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000290 (Vuln V-253406)

### Run as different user must be removed from context menus.

The "Run as different user" selection from context menus allows the use of credentials other than the currently logged on user. Using privileged credentials in a standard user session can expose those credentials to theft. Removing this option from context menus helps prevent this from occurring.

- **Change:** Sets `HKLM\SOFTWARE\Classes\exefile\shell\runasuser\SuppressionPolicy` = `4096` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000039 (Vuln V-253359.c)

### Run as different user must be removed from context menus.

The "Run as different user" selection from context menus allows the use of credentials other than the currently logged on user. Using privileged credentials in a standard user session can expose those credentials to theft. Removing this option from context menus helps prevent this from occurring.

- **Change:** Sets `HKLM\SOFTWARE\Classes\batfile\shell\runasuser\SuppressionPolicy` = `4096` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000039 (Vuln V-253359.a)

### Run as different user must be removed from context menus.

The "Run as different user" selection from context menus allows the use of credentials other than the currently logged on user. Using privileged credentials in a standard user session can expose those credentials to theft. Removing this option from context menus helps prevent this from occurring.

- **Change:** Sets `HKLM\SOFTWARE\Classes\mscfile\shell\runasuser\SuppressionPolicy` = `4096` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000039 (Vuln V-253359.d)

### Run as different user must be removed from context menus.

The "Run as different user" selection from context menus allows the use of credentials other than the currently logged on user. Using privileged credentials in a standard user session can expose those credentials to theft. Removing this option from context menus helps prevent this from occurring.

- **Change:** Sets `HKLM\SOFTWARE\Classes\cmdfile\shell\runasuser\SuppressionPolicy` = `4096` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000039 (Vuln V-253359.b)

### Solicited Remote Assistance must not be allowed.

Remote assistance allows another user to view or take control of the local session of a user. Solicited assistance is help that is specifically requested by the local user. This may allow unauthorized parties access to the resources on the computer.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fAllowToGetHelp` = `0` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000155 (Vuln V-253382)

### Structured Exception Handling Overwrite Protection (SEHOP) must be enabled.

Attackers are constantly looking for vulnerabilities in systems and applications. Structured Exception Handling Overwrite Protection (SEHOP) blocks exploits that use the Structured Exception Handling overwrite technique, a common buffer overflow attack.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel\DisableExceptionChainValidation` = `0` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-00-000150 (Vuln V-253284)

### The Application Compatibility Program Inventory must be prevented from collecting data and sending the informa…

Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting will pre…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AppCompat\DisableInventory` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000175 (Vuln V-253385)

### The computer account password must not be prevented from being reset.

Computer account passwords are changed automatically on a regular basis. Disabling automatic password changes can make the system more vulnerable to malicious access. Frequent password changes can be a significant safeguard for the system. A new password for the computer account will be generated ev…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\DisablePasswordChange` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000050 (Vuln V-253441)

### The convenience PIN for Windows 11 must be disabled.

This policy controls whether a domain user can sign in using a convenience PIN to prevent enabling (Password Stuffer).

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Windows\System\AllowDomainPINLogon` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000370 (Vuln V-253423)

### The default autorun behavior must be configured to prevent autorun commands.

Allowing autorun commands to execute may introduce malicious code to a system. Configuring this setting prevents autorun commands from executing.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoAutorun` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000185 (Vuln V-253387)

### The default permissions of global system objects must be increased.

Windows systems maintain a global list of shared system resources such as DOS device names, mutexes, and semaphores. Each type of object is created with a default DACL that specifies who can access the objects with what permissions. If this policy is enabled, the default DACL is stronger, allowing n…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\ProtectionMode` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000240 (Vuln V-253467)

### The display of slide shows on the lock screen must be disabled.

Slide shows that are displayed on the lock screen could display sensitive information to unauthorized personnel. Turning off this feature will limit access to the information to a logged on user.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Personalization\NoLockScreenSlideshow` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000010 (Vuln V-253352)

### The LanMan authentication level must be set to send NTLMv2 response only, and to refuse LM and NTLM.

The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000205 (Vuln V-253462)

### The Microsoft Defender SmartScreen for Explorer must be enabled.

Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnableSmartScreen` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000210 (Vuln V-253395.b)

### The Microsoft Defender SmartScreen for Explorer must be enabled.

Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\ShellSmartScreenLevel` = `Block` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000210 (Vuln V-253395.a)


