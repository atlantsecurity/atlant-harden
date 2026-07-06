# Network Security

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Removes the legacy and insecure network behaviour attackers abuse for interception and lateral movement: SMBv1 (EternalBlue/WannaCry), name-resolution poisoning (LLMNR/NetBIOS/WPAD), unsigned SMB/LDAP traffic, and anonymous enumeration of accounts and shares._

**30 settings** in this category &mdash; **26** are part of the Recommended profile.

### Block Anonymous Everyone Access

Disable Everyone permissions for anonymous users

*Why it matters:* Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable DCOM

Disable Distributed Component Object Model for remote commands

- **Change:** Sets `HKLM\Software\Microsoft\OLE\EnableDCOM` = `N` (REG_SZ)
- **Risk:** High  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** May break some remote administration tools

### Disable File Sharing Within Profile

Prevent users from sharing files within their profile

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoFileSharingControl` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight
- **&#9888; Impact:** Users won't be able to share files from their profile. Use network shares instead.

### Disable ICMP Redirects

Do not allow ICMP redirects to override OSPF routes

*Why it matters:* Allowing ICMP redirect of routes can lead to traffic not being routed properly. When disabled, this forces ICMP to be routed via shortest path first.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\EnableICMPRedirect` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable IP Helper Service

Disable IP Helper to prevent port proxy attacks

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\iphlpsvc\Start` = `4` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** May affect IPv6 transition technologies

### Disable IP Source Routing

Prevent IP source routing attacks

*Why it matters:* Configuring the system to disable IP source routing protects against spoofing.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\DisableIPSourceRouting` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable IPv6

Disable IPv6 on all network interfaces

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\services\tcpip6\parameters\DisabledComponents` = `255` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** May break Microsoft services, modern apps, and IPv6-only networks

### Disable LLMNR

Disable Link-Local Multicast Name Resolution

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient\EnableMulticast` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable LLMNR (Link-Local Multicast Name Resolution)

Disable LLMNR to prevent credential interception attacks

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient\EnableMulticast` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Disable NetBIOS over TCP/IP

Stop NetBIOS over TCP/IP service

- **Change:** Applies the configured system change.
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May affect legacy file sharing

### Disable NetBIOS over TCP/IP

Disable NetBIOS name resolution to prevent credential attacks

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\NetBT\Parameters\Interfaces\Tcpip_*\NetbiosOptions` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight
- **&#9888; Impact:** May affect legacy applications that rely on NetBIOS name resolution.

### Disable SMBv1 Client

Disable the SMBv1 client driver

*Why it matters:* SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\mrxsmb10\Start` = `4` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  Reboot required

### Disable SMBv1 Server

Disable the vulnerable SMBv1 protocol (server side)

*Why it matters:* SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\SMB1` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  Reboot required

### Disable WPAD

Disable Web Proxy Auto-Discovery protocol

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Wpad\WpadOverride` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable WPAD (Web Proxy Auto-Discovery)

Disable automatic proxy discovery to prevent man-in-the-middle attacks

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Wpad\WpadOverride` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Enable SMB Signing (Client)

Enable SMB packet signing for client communications

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\EnableSecuritySignature` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Exclude Anonymous from Everyone Group

Let Everyone permissions not apply to anonymous users

*Why it matters:* Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Require LDAP Client Signing

Require LDAP client signing for DC communications

*Why it matters:* This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Require LDAP Client Signing

Require LDAP client to perform signing

*Why it matters:* This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Require LDAP Server Signing

Require LDAP server integrity signing

- **Change:** Sets `HKLM\System\CurrentControlSet\Services\NTDS\Parameters\LDAPServerIntegrity` = `2` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended

### Require SMB Signing (Client)

Require SMB packet signing for client connections

*Why it matters:* The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.

- **Change:** Sets `HKLM\System\CurrentControlSet\Services\LanmanWorkStation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended

### Require SMB Signing (Client)

Require SMB packet signing for client communications

*Why it matters:* The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Require SMB Signing (Server)

Require SMB packet signing for server communications

*Why it matters:* The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Require SMB Signing (Server)

Require SMB packet signing for server connections

*Why it matters:* The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.

- **Change:** Sets `HKLM\System\CurrentControlSet\Services\LanmanServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended

### Restrict Anonymous Access to Named Pipes and Shares

Do not allow anonymous enumeration of SAM accounts and shares

*Why it matters:* Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Restrict Anonymous SAM Enumeration

Prevent anonymous enumeration of SAM accounts

*Why it matters:* Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Restrict Anonymous SAM Enumeration

Do not allow anonymous enumeration of SAM accounts

*Why it matters:* Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Restrict Anonymous Share Enumeration

Prevent anonymous enumeration of shares

*Why it matters:* Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Restrict Null Session Access

Restrict anonymous access to named pipes and shares

*Why it matters:* Allowing anonymous access to named pipes or shares provides the potential for unauthorized system access. This setting restricts access to those defined in "Network access: Named Pipes that can be accessed anonymously" and "Network access: Shares that can be accessed anonymously", both of which must…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RestrictNullSessAccess` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Use NTLMv2 Only

Send NTLMv2 response only, refuse LM and NTLM

*Why it matters:* The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight
- **&#9888; Impact:** May break authentication with very old systems that don't support NTLMv2.


