# DISA STIG — Microsoft Windows 11 (V2R7) &mdash; Part 2 of 2

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_The Windows 11 Security Technical Implementation Guide is DISA's authoritative hardening baseline for U.S. Department of Defense systems. Every item below is a formal STIG requirement with its own STIG ID, Vulnerability ID and CCIs, applying DISA's exact mandated value._

**57 settings** on this page &mdash; **34** are part of the Recommended profile.

### The network selection user interface (UI) must not be displayed on the logon screen.

Enabling interaction with the network selection UI allows users to change connections to available networks without signing into Windows.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\DontDisplayNetworkSelectionUI` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000120 (Vuln V-253378)

### The Remote Desktop Session Host must require secure RPC communications.

Allowing unsecure RPC communication exposes the system to man in the middle attacks and data disclosure attacks. A man in the middle attack occurs when an intruder captures packets between a client and server and modifies them before allowing the packets to be exchanged. Usually the attacker will mo…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fEncryptRPCTraffic` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000285 (Vuln V-253405)

### The Server Message Block (SMB) v1 protocol must be disabled on the SMB client.

SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\mrxsmb10\Start` = `4` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-00-000170 (Vuln V-253288)

### The Server Message Block (SMB) v1 protocol must be disabled on the SMB server.

SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\SMB1` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-00-000165 (Vuln V-253287)

### The setting to allow Microsoft accounts to be optional for modern style apps must be enabled.

Control of credentials and the system must be maintained within the enterprise. Enabling this setting allows enterprise credentials to be used with modern style apps that support this, instead of Microsoft accounts.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\MSAOptional` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000170 (Vuln V-253384)

### The system must be configured to ignore NetBIOS name release requests except from WINS servers.

Configuring the system to ignore name release requests, except from WINS servers, prevents a denial of service (DoS) attack. The DoS consists of sending a NetBIOS name release request to the server for each entry in the server's cache, causing a response delay in the normal operation of the servers…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netbt\Parameters\NoNameReleaseOnDemand` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000035 (Vuln V-253356)

### The system must be configured to meet the minimum session security requirement for NTLM SSP based clients.

Microsoft has implemented a variety of security support providers for use with RPC sessions. All of the options must be enabled to ensure the maximum security level.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\MSV1_0\NTLMMinClientSec` = `537395200` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000215 (Vuln V-253464)

### The system must be configured to meet the minimum session security requirement for NTLM SSP based servers.

Microsoft has implemented a variety of security support providers for use with RPC sessions. All of the options must be enabled to ensure the maximum security level.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\MSV1_0\NTLMMinServerSec` = `537395200` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000220 (Vuln V-253465)

### The system must be configured to prevent anonymous users from having the same rights as the Everyone group.

Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000160 (Vuln V-253455)

### The system must be configured to prevent Internet Control Message Protocol (ICMP) redirects from overriding Op…

Allowing ICMP redirect of routes can lead to traffic not being routed properly. When disabled, this forces ICMP to be routed via shortest path first.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\EnableICMPRedirect` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000030 (Vuln V-253355)

### The system must be configured to prevent IP source routing.

Configuring the system to disable IP source routing protects against spoofing.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\DisableIPSourceRouting` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000025 (Vuln V-253354)

### The system must be configured to prevent the storage of the LAN Manager hash of passwords.

The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000195 (Vuln V-253461)

### The system must be configured to require a strong session key.

A computer connecting to a domain controller will establish a secure channel. Requiring strong session keys enforces 128-bit encryption between systems.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\RequireStrongKey` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000060 (Vuln V-253443)

### The system must be configured to the required LDAP client signing level.

This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000210 (Vuln V-253463)

### The system must be configured to use FIPS-compliant algorithms for encryption, hashing, and signing.

This setting ensures that the system uses algorithms that are FIPS-compliant for encryption, hashing, and signing. FIPS-compliant algorithms meet specific standards established by the U.S. Government and must be the algorithms used for all OS encryption functions.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\FIPSAlgorithmPolicy\Enabled` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-SO-000230 (Vuln V-253466)

### The use of a hardware security device with Windows Hello for Business must be enabled.

The use of a Trusted Platform Module (TPM) to store keys for Windows Hello for Business provides additional security. Keys stored in the TPM may only be used on that system while keys stored using software are more susceptible to compromise and could be used on other systems.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\PassportForWork\RequireSecurityDevice` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000255 (Vuln V-253400)

### The user must be prompted for a password on resume from sleep (plugged in).

Authentication must always be required when accessing a system. This setting ensures the user is prompted for a password on resume from sleep (plugged in).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Power\PowerSettings\0e796bdb-100d-47d6-a2d5-f7d2daa51f51\ACSettingIndex` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000150 (Vuln V-253381)

### The Windows Installer feature "Always install with elevated privileges" must be disabled.

Standard user accounts must not be granted elevated privileges. Enabling Windows Installer to elevate privileges when installing applications can allow malicious persons and applications to gain full control of a system.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\AlwaysInstallElevated` = `0` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000315 (Vuln V-253411)

### The Windows Remote Management (WinRM) client must not allow unencrypted traffic.

Unencrypted remote access to a system can allow sensitive information to be compromised. Windows remote management connections must be encrypted to prevent this.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowUnencryptedTraffic` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000335 (Vuln V-253417)

### The Windows Remote Management (WinRM) client must not use Basic authentication.

Basic authentication uses plain text passwords that could be used to compromise a system.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowBasic` = `0` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000330 (Vuln V-253416)

### The Windows Remote Management (WinRM) client must not use Digest authentication.

Digest authentication is not as strong as other options and may be subject to man-in-the-middle attacks.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowDigest` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000360 (Vuln V-253421)

### The Windows Remote Management (WinRM) service must not allow unencrypted traffic.

Unencrypted remote access to a system can allow sensitive information to be compromised. Windows remote management connections must be encrypted to prevent this.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\AllowUnencryptedTraffic` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000350 (Vuln V-253419)

### The Windows Remote Management (WinRM) service must not store RunAs credentials.

Storage of administrative credentials could allow unauthorized access. Disallowing the storage of RunAs credentials for Windows Remote Management will prevent them from being used with plug-ins.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\DisableRunAs` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000355 (Vuln V-253420)

### The Windows Remote Management (WinRM) service must not use Basic authentication.

Basic authentication uses plain text passwords that could be used to compromise a system.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\AllowBasic` = `0` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000345 (Vuln V-253418)

### The Windows SMB client must be configured to always perform SMB packet signing.

The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000100 (Vuln V-253449)

### The Windows SMB server must be configured to always perform SMB packet signing.

The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000120 (Vuln V-253451)

### Toast notifications to the lock screen must be turned off.

Toast notifications that are displayed on the lock screen could display sensitive information to unauthorized personnel. Turning off this feature will limit access to the information to a logged on user.

- **Change:** Sets `HKCU\SOFTWARE\Policies\Microsoft\Windows\CurrentVersion\PushNotifications\NoToastApplicationNotificationOnLockScreen` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-UC-000015 (Vuln V-253477)

### Unauthenticated RPC clients must be restricted from connecting to the RPC server.

Configuring RPC to restrict unauthenticated RPC clients from connecting to the RPC server will prevent anonymous connections.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Rpc\RestrictRemoteClients` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000165 (Vuln V-253383)

### Unencrypted passwords must not be sent to third-party SMB Servers.

Some non-Microsoft SMB servers only support unencrypted (plain text) password authentication. Sending plain text passwords across the network, when authenticating to an SMB server, reduces the overall security of the environment. Check with the vendor of the SMB server to see if there is a way to su…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\EnablePlainTextPassword` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-SO-000110 (Vuln V-253450)

### User Account Control approval mode for the built-in Administrator must be enabled.

User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the built-in Administrator account so that it runs in Admin Approval Mode.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\FilterAdministratorToken` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000245 (Vuln V-253468)

### User Account Control must automatically deny elevation requests for standard users.

User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. Denying elevation requests from standard user accounts requires tasks that need elevation to be initiated by accounts with administrative privileges. Thi…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorUser` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000255 (Vuln V-253471)

### User Account Control must be configured to detect application installations and prompt for elevation.

User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting requires Windows to respond to application installation requests by prompting for credentials.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableInstallerDetection` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000260 (Vuln V-253472)

### User Account Control must only elevate UIAccess applications that are installed in secure locations.

User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures Windows to only allow applications installed in a secure location on the file system, such as the Program Files or the Windows\Sy…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableSecureUIAPaths` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000265 (Vuln V-253473)

### User Account Control must prompt administrators for consent on the secure desktop.

User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the elevation requirements for logged on administrators to complete a task that requires raised privileges.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorAdmin` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000250 (Vuln V-253469)

### User Account Control must run all administrators in Admin Approval Mode, enabling UAC.

User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting enables UAC.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableLUA` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000270 (Vuln V-253474)

### User Account Control must virtualize file and registry write failures to per-user locations.

User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures non-UAC compliant applications to run in virtualized file and registry entries in per-user locations, allowing them to run.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableVirtualization` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-SO-000275 (Vuln V-253475)

### Users must be prevented from changing installation options.

Installation options for applications are typically controlled by administrators. This setting prevents users from changing installation options that may bypass security features.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\EnableUserControl` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000310 (Vuln V-253410)

### Users must be prompted for a password on resume from sleep (on battery).

Authentication must always be required when accessing a system. This setting ensures the user is prompted for a password on resume from sleep (on battery).

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Power\PowerSettings\0e796bdb-100d-47d6-a2d5-f7d2daa51f51\DCSettingIndex` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000145 (Vuln V-253380)

### Virtualization-based Security must be enabled on Windows 11 with the platform security level configured to Sec…

Virtualization-based Security (VBS) provides the platform for the additional security features, Credential Guard and virtualization-based protection of code integrity. Secure Boot is the minimum security level with DMA protection providing additional memory protection. DMA Protection requires a CPU…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DeviceGuard\EnableVirtualizationBasedSecurity` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000070 (Vuln V-253369.a)

### WDigest Authentication must be disabled.

When the WDigest Authentication protocol is enabled, plain text passwords are stored in the Local Security Authority Subsystem Service (LSASS) exposing them to theft. WDigest is disabled by default in Windows 11. This setting ensures this is enforced.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\Wdigest\UseLogonCredential` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000038 (Vuln V-253358)

### Web publishing and online ordering wizards must be prevented from downloading a list of providers.

Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting prevents…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoWebServices` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000105 (Vuln V-253375)

### Wi-Fi Sense must be disabled.

Wi-Fi Sense automatically connects the system to known hotspots and networks that contacts have shared. It also allows the sharing of the system's known networks to contacts. Automatically connecting to hotspots and shared networks can expose a system to unsecured or potentially malicious systems.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config\AutoConnectAllowedOEM` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000065 (Vuln V-253366)

### Windows 11 Kernel (Direct Memory Access) DMA Protection must be enabled.

Kernel DMA Protection to protect PCs against drive-by Direct Memory Access (DMA) attacks using PCI hot plug devices connected to Thunderbolt 3 ports. Drive-by DMA attacks can lead to disclosure of sensitive information residing on a PC, or even injection of malware that allows attackers to bypass th…

- **Change:** Sets `HKLM\Software\Policies\Microsoft\Windows\Kernel DMA Protection\DeviceEnumerationPolicy` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-EP-000310 (Vuln V-253426)

### Windows 11 must be configured to disable Windows Game Recording and Broadcasting.

Windows Game Recording and Broadcasting is intended for use with games; however, it could potentially record screen shots of other applications and expose sensitive data. Disabling the feature will prevent this from occurring.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\GameDVR\AllowGameDVR` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000252 (Vuln V-253399)

### Windows 11 must be configured to enable Remote host allows delegation of non-exportable credentials.

An exportable version of credentials is provided to remote hosts when using credential delegation which exposes them to theft on the remote host. Restricted Admin mode or Remote Credential Guard allow delegation of non-exportable credentials providing additional protection of the credentials. Enabli…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CredentialsDelegation\AllowProtectedCreds` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000068 (Vuln V-253368)

### Windows 11 must be configured to prevent users from receiving suggestions for third-party or additional applic…

Windows spotlight features may suggest apps and content from third-party software publishers in addition to Microsoft apps and content.

- **Change:** Sets `HKCU\SOFTWARE\Policies\Microsoft\Windows\CloudContent\DisableThirdPartySuggestions` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000390 (Vuln V-253425)

### Windows 11 must be configured to prevent Windows apps from being activated by voice while the system is locked…

Allowing Windows apps to be activated by voice from the lock screen could allow for unauthorized use. Requiring logon will ensure the apps are only used by authorized personnel.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy\LetAppsActivateWithVoice` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000365 (Vuln V-253422.b)

### Windows 11 must be configured to prevent Windows apps from being activated by voice while the system is locked…

Allowing Windows apps to be activated by voice from the lock screen could allow for unauthorized use. Requiring logon will ensure the apps are only used by authorized personnel.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy\LetAppsActivateWithVoiceAboveLock` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000365 (Vuln V-253422.a)

### Windows 11 must be configured to prioritize ECC Curves with longer key lengths first.

Use of weak or untested encryption algorithms undermines the purposes of utilizing encryption to protect data. By default Windows uses ECC curves with shorter key lengths first. Requiring ECC curves with longer key lengths to be prioritized first helps ensure more secure algorithms are used.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Cryptography\Configuration\SSL\00010002\EccCurves` = `NistP384;NistP256` (REG_MULTI_SZ)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  **STIG:** WN11-CC-000052 (Vuln V-253363)

### Windows 11 must cover or disable the built-in or attached camera when not in use.

It is detrimental for operating systems to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may remain unsecured. They increase the risk to the platform by providing additional at…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam\Value` = `Deny` (REG_SZ)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000007 (Vuln V-253351)

### Windows 11 systems must block consumer account user authentication.

It is detrimental for operating systems to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore, may remain unsecured. They increase the risk to the platform by providing additional a…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\MicrosoftAccount\DisableUserAuth` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-00-000126 (Vuln V-279688)

### Windows 11 systems must use a BitLocker PIN for pre-boot authentication.

If data at rest is unencrypted, it is vulnerable to disclosure. Even if the operating system enforces permissions on data access, an adversary can remove non-volatile memory and read it directly, thereby circumventing operating system controls. Encrypting the data ensures that confidentiality is pro…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\FVE\UseTPMPIN` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-00-000031 (Vuln V-253260.b)

### Windows 11 systems must use a BitLocker PIN for pre-boot authentication.

If data at rest is unencrypted, it is vulnerable to disclosure. Even if the operating system enforces permissions on data access, an adversary can remove non-volatile memory and read it directly, thereby circumventing operating system controls. Encrypting the data ensures that confidentiality is pro…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\FVE\UseAdvancedStartup` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-00-000031 (Vuln V-253260.a)

### Windows Ink Workspace must be configured to disallow access above the lock.

This action secures Windows Ink, which contains applications and features oriented toward pen computing.

- **Change:** Sets `HKLM\Software\Policies\Microsoft\WindowsInkWorkspace\AllowWindowsInkWorkspace` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000385 (Vuln V-253424)

### Windows Telemetry must not be configured to Full.

Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Limiting this capability will prevent potentially sensitive information from being sent outside the enterprise. The "Security" option for Telemetry configures the lowest amoun…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection\AllowTelemetry` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000205 (Vuln V-253393)

### Windows Update must not obtain updates from other PCs on the internet.

Windows 11 allows Windows Update to obtain updates from additional sources instead of Microsoft. In addition to Microsoft, updates can be obtained from and sent to PCs on the local network as well as on the Internet. This is part of the Windows Update trusted process, however to minimize outside exp…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config\DODownloadMode` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000206 (Vuln V-253394.b)

### Windows Update must not obtain updates from other PCs on the internet.

Windows 11 allows Windows Update to obtain updates from additional sources instead of Microsoft. In addition to Microsoft, updates can be obtained from and sent to PCs on the local network as well as on the Internet. This is part of the Windows Update trusted process, however to minimize outside exp…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DeliveryOptimization\DODownloadMode` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  **STIG:** WN11-CC-000206 (Vuln V-253394.a)


