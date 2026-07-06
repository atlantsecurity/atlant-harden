# AtlantHarden — Recommended Settings, Explained

This document explains **every setting applied by the Recommended profile**, one by one — what it changes and why it matters.

**The Recommended profile is engineered for return-on-investment:** it applies the controls that stop how malware and attackers *actually* get in and run, and deliberately leaves out high-friction "compliance theatre" that breaks everyday use without stopping real attacks. It does **not** disable your browser password manager, InPrivate/Incognito, or history deletion; it does **not** enable Controlled Folder Access, FIPS mode, PowerShell Constrained Language Mode, or a BitLocker pre-boot PIN. It is already gaming- and performance-safe.

Every change is backed up before it is applied and is fully reversible. Settings are grouped by category; each category opens with the threat it addresses.

## Contents
- [Attack Surface Reduction (ASR)](#attack-surface-reduction-asr) — 18 settings
- [Microsoft Defender Antivirus](#microsoft-defender-antivirus) — 6 settings
- [Credential Protection](#credential-protection) — 12 settings
- [Network Security](#network-security) — 26 settings
- [System Hardening](#system-hardening) — 18 settings
- [TLS & Cryptography](#tls--cryptography) — 10 settings
- [Office Hardening](#office-hardening) — 8 settings
- [File Association Neutralisation](#file-association-neutralisation) — 13 settings
- [Windows Firewall — LOLBin Blocking](#windows-firewall--lolbin-blocking) — 9 settings
- [Logging & Auditing](#logging--auditing) — 11 settings
- [Removable Media](#removable-media) — 3 settings
- [Microsoft Edge Hardening](#microsoft-edge-hardening) — 9 settings
- [Google Chrome Hardening](#google-chrome-hardening) — 9 settings
- [Mozilla Firefox Hardening](#mozilla-firefox-hardening) — 3 settings
- [Adobe Acrobat / Reader](#adobe-acrobat--reader) — 6 settings
- [DISA STIG — Microsoft Windows 11 (V2R7)](#disa-stig--microsoft-windows-11-v2r7) — 68 settings
- [DISA STIG — Microsoft Edge (V2R5)](#disa-stig--microsoft-edge-v2r5) — 7 settings
- [DISA STIG — Google Chrome (V2R11)](#disa-stig--google-chrome-v2r11) — 3 settings
- [DISA STIG — Mozilla Firefox (V6R7)](#disa-stig--mozilla-firefox-v6r7) — 2 settings
- [DISA STIG — Microsoft Office 365 ProPlus (V3R5)](#disa-stig--microsoft-office-365-proplus-v3r5) — 77 settings

## Attack Surface Reduction (ASR)

_Microsoft Defender ASR rules block the specific behaviours malware relies on — Office spawning executables, scripts launching payloads, credential theft from LSASS, ransomware file patterns — at the kernel level, before code runs. They are the single highest-value anti-malware control and are almost invisible in day-to-day use._

**18 settings in this section.**

**Advanced Ransomware Protection**  
Use advanced protection against ransomware  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low**</sub>  

**Block Adobe Reader Child Processes**  
Prevent Adobe Reader from creating child processes that could be malicious  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low**</sub>  

**Block Credential Stealing from LSASS**  
Block credential stealing from Windows LSASS  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low**</sub>  

**Block Email Executable Content**  
Block executable content from email client and webmail  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low**</sub>  

**Block Executable Office Content**  
Block Office apps from creating executable content  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May prevent Office from saving certain file types

**Block Impersonated System Tools**  
Block executables that impersonate or copy Windows system tools  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low**</sub>  

**Block JS/VBS Downloaded Executables**  
Block JavaScript or VBScript from launching downloaded executables  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May block legitimate installers and scripts

**Block Obfuscated Scripts**  
Block execution of potentially obfuscated scripts  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May block some legitimate PowerShell scripts

**Block Office Child Processes**  
Prevent Office applications from creating child processes  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break legitimate Office workflows that launch external programs

**Block Office Code Injection**  
Prevent Office apps from injecting code into other processes  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break Office add-ins and integrations

**Block Office Communication App Child Processes**  
Prevent Outlook from creating child processes to block social engineering attacks  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Medium**</sub>  

**Block PSExec and WMI Process Creation**  
Block processes created via PSExec and WMI commands to prevent lateral movement  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May block legitimate admin tools - incompatible with SCCM/ConfigMgr

**Block Safe Mode Reboot Commands**  
Prevent bcdedit and bootcfg from restarting machine in Safe Mode where security tools are disabled  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Medium**</sub>  

**Block Untrusted USB Processes**  
Block untrusted and unsigned processes running from USB  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** Will block portable applications from USB drives

**Block Vulnerable Signed Drivers**  
Prevent exploitation of vulnerable signed drivers that could be used for kernel access  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low**</sub>  

**Block Webshell Creation for Servers**  
Prevent web shell script creation on Microsoft Server and Exchange  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low**</sub>  

**Block Win32 API from Office Macros**  
Block Office macros from calling Win32 APIs  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** Will break many legitimate VBA macros that interact with Windows

**Block WMI Event Subscription Persistence**  
Prevent malware from using WMI event subscriptions to persist on the system  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Medium**</sub>  

## Microsoft Defender Antivirus

_Tunes the built-in antivirus itself — cloud-delivered protection, network protection, PUA (potentially unwanted application) blocking and sandboxing — so it catches more, faster, and cannot be casually paused._

**6 settings in this section.**

**Disable Pause Windows Defender Scan**  
Prevent users from pausing Windows Defender scans  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\AllowPause` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable Cloud-Delivered Protection**  
Enable cloud-based protection for better threat detection  
Applies the configured system change.  
<sub>Risk: **Low**</sub>  

**Enable Defender Sandbox**  
Run Windows Defender in a sandbox for better security  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Reboot required</sub>  

**Enable Network Protection**  
Block connections to malicious IP addresses and domains  
Applies the configured system change.  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May occasionally block legitimate websites

**Enable PUA Protection**  
Enable detection of Potentially Unwanted Applications  
Applies the configured system change.  
<sub>Risk: **Low**</sub>  

**Extended Cloud Check Timeout**  
Extend cloud check timeout to 50 seconds  
Applies the configured system change.  
<sub>Risk: **Low**</sub>  

## Credential Protection

_Stops attackers from stealing the credentials that let them move from one machine to the whole network. These settings protect the LSASS process (where Windows holds credentials in memory), stop weak-hash and cleartext storage, and enforce modern authentication — the controls that defeat Mimikatz-style attacks._

**12 settings in this section.**

**Account Lockout Duration (15 minutes)**  
Lock account for 15 minutes after exceeding threshold  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Account Lockout Reset Window (15 minutes)**  
Reset account lockout counter after 15 minutes  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Account Lockout Threshold (5 attempts)**  
Lock account after 5 invalid logon attempts  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Block Blank Password Network Logons**  
Prevent local accounts with blank passwords from network logon  
_Why:_ An account without a password can allow unauthorized access to a system as only the username would be required. Password policies must prevent accounts with blank passwords from existing on a system. However, if a local account with a blank password did exist, enabling this setting will prevent netw…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LimitBlankPasswordUse` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable LM Hash Storage**  
Do not store LAN Manager hash value on next password change  
_Why:_ The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable WDigest Authentication**  
Prevent storing credentials in memory (cleartext)  
_Why:_ When the WDigest Authentication protocol is enabled, plain text passwords are stored in the Local Security Authority Subsystem Service (LSASS) exposing them to theft. WDigest is disabled by default in Windows 11. This setting ensures this is enforced.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest\UseLogonCredential` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable WDigest Negotiation**  
Disable WDigest negotiate protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest\Negotiate` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Do Not Store LM Hash**  
Prevent storage of LAN Manager hash on next password change  
_Why:_ The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable LSASS Audit Mode**  
Audit access to LSASS for security monitoring  
Sets `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\LSASS.exe\AuditLevel` = `8` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Protected Credentials**  
Allow delegation of non-exported credentials  
_Why:_ An exportable version of credentials is provided to remote hosts when using credential delegation which exposes them to theft on the remote host. Restricted Admin mode or Remote Credential Guard allow delegation of non-exportable credentials providing additional protection of the credentials. Enabli…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CredentialsDelegation\AllowProtectedCreds` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enforce NTLMv2 Only**  
Set LAN Manager authentication level to NTLMv2 only  
_Why:_ The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May affect legacy system compatibility

**LSASS Protected Process**  
Run LSASS as a Protected Process Light (PPL)  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RunAsPPL` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Reboot required</sub>  

## Network Security

_Removes the legacy and insecure network behaviour attackers abuse for interception and lateral movement: SMBv1 (EternalBlue/WannaCry), name-resolution poisoning (LLMNR/NetBIOS/WPAD), unsigned SMB/LDAP traffic, and anonymous enumeration of accounts and shares._

**26 settings in this section.**

**Block Anonymous Everyone Access**  
Disable Everyone permissions for anonymous users  
_Why:_ Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable ICMP Redirects**  
Do not allow ICMP redirects to override OSPF routes  
_Why:_ Allowing ICMP redirect of routes can lead to traffic not being routed properly. When disabled, this forces ICMP to be routed via shortest path first.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\EnableICMPRedirect` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable IP Source Routing**  
Prevent IP source routing attacks  
_Why:_ Configuring the system to disable IP source routing protects against spoofing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\DisableIPSourceRouting` = `2` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable LLMNR**  
Disable Link-Local Multicast Name Resolution  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient\EnableMulticast` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable LLMNR (Link-Local Multicast Name Resolution)**  
Disable LLMNR to prevent credential interception attacks  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient\EnableMulticast` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Disable NetBIOS over TCP/IP**  
Stop NetBIOS over TCP/IP service  
Applies the configured system change.  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May affect legacy file sharing

**Disable NetBIOS over TCP/IP**  
Disable NetBIOS name resolution to prevent credential attacks  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\NetBT\Parameters\Interfaces\Tcpip_*\NetbiosOptions` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** May affect legacy applications that rely on NetBIOS name resolution.

**Disable SMBv1 Client**  
Disable the SMBv1 client driver  
_Why:_ SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\mrxsmb10\Start` = `4` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Reboot required</sub>  

**Disable SMBv1 Server**  
Disable the vulnerable SMBv1 protocol (server side)  
_Why:_ SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\SMB1` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Reboot required</sub>  

**Disable WPAD**  
Disable Web Proxy Auto-Discovery protocol  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Wpad\WpadOverride` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable WPAD (Web Proxy Auto-Discovery)**  
Disable automatic proxy discovery to prevent man-in-the-middle attacks  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Wpad\WpadOverride` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable SMB Signing (Client)**  
Enable SMB packet signing for client communications  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\EnableSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Exclude Anonymous from Everyone Group**  
Let Everyone permissions not apply to anonymous users  
_Why:_ Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require LDAP Client Signing**  
Require LDAP client signing for DC communications  
_Why:_ This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Require LDAP Client Signing**  
Require LDAP client to perform signing  
_Why:_ This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require LDAP Server Signing**  
Require LDAP server integrity signing  
Sets `HKLM\System\CurrentControlSet\Services\NTDS\Parameters\LDAPServerIntegrity` = `2` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  

**Require SMB Signing (Client)**  
Require SMB packet signing for client connections  
_Why:_ The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.  
Sets `HKLM\System\CurrentControlSet\Services\LanmanWorkStation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  

**Require SMB Signing (Client)**  
Require SMB packet signing for client communications  
_Why:_ The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require SMB Signing (Server)**  
Require SMB packet signing for server communications  
_Why:_ The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require SMB Signing (Server)**  
Require SMB packet signing for server connections  
_Why:_ The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.  
Sets `HKLM\System\CurrentControlSet\Services\LanmanServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  

**Restrict Anonymous Access to Named Pipes and Shares**  
Do not allow anonymous enumeration of SAM accounts and shares  
_Why:_ Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Restrict Anonymous SAM Enumeration**  
Prevent anonymous enumeration of SAM accounts  
_Why:_ Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Restrict Anonymous SAM Enumeration**  
Do not allow anonymous enumeration of SAM accounts  
_Why:_ Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Restrict Anonymous Share Enumeration**  
Prevent anonymous enumeration of shares  
_Why:_ Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Restrict Null Session Access**  
Restrict anonymous access to named pipes and shares  
_Why:_ Allowing anonymous access to named pipes or shares provides the potential for unauthorized system access. This setting restricts access to those defined in "Network access: Named Pipes that can be accessed anonymously" and "Network access: Shares that can be accessed anonymously", both of which must…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RestrictNullSessAccess` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Use NTLMv2 Only**  
Send NTLMv2 response only, refuse LM and NTLM  
_Why:_ The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** May break authentication with very old systems that don't support NTLMv2.

## System Hardening

_Core OS hardening — User Account Control, exploit mitigations (SEHOP, safe DLL search order), Autorun/Autoplay, SmartScreen for downloaded files, and closing privilege-escalation holes such as AlwaysInstallElevated._

**18 settings in this section.**

**Block CWD DLL Loading**  
Block DLL loading from current working directory (remote)  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\CWDIllegalInDllSearch` = `2` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Block DMA Until User Logon**  
Block Direct Memory Access ports until user logs on  
_Why:_ Kernel DMA Protection to protect PCs against drive-by Direct Memory Access (DMA) attacks using PCI hot plug devices connected to Thunderbolt 3 ports. Drive-by DMA attacks can lead to disclosure of sensitive information residing on a PC, or even injection of malware that allows attackers to bypass th…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Kernel DMA Protection\DeviceEnumerationPolicy` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Disable Always Install Elevated**  
Prevent installers from using elevated privileges by default  
_Why:_ Standard user accounts must not be granted elevated privileges. Enabling Windows Installer to elevate privileges when installing applications can allow malicious persons and applications to gain full control of a system.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\AlwaysInstallElevated` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable AutoRun for All Drives**  
Disable automatic execution features for removable media  
_Why:_ Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Disable Windows Script Host**  
Prevent VBS/JS scripts from running via WSH  
Sets `HKCU\SOFTWARE\Microsoft\Windows Script Host\Settings\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May break legitimate scripts

**Enable Biometric Anti-Spoofing**  
Enable enhanced anti-spoofing for facial recognition  
_Why:_ Enhanced anti-spoofing provides additional protections when using facial recognition with devices that support it.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Biometrics\FacialFeatures\EnhancedAntiSpoofing` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Safe DLL Search Mode**  
Protect against DLL hijacking attacks  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\SafeDLLSearchMode` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable SEHOP**  
Enable Structured Exception Handling Overwrite Protection  
_Why:_ Attackers are constantly looking for vulnerabilities in systems and applications. Structured Exception Handling Overwrite Protection (SEHOP) blocks exploits that use the Structured Exception Handling overwrite technique, a common buffer overflow attack.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel\DisableExceptionChainValidation` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable SmartScreen**  
Enable Windows SmartScreen filter  
_Why:_ Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnableSmartScreen` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable UAC**  
Enable User Account Control  
_Why:_ User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting enables UAC.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableLUA` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Virtualization Based Security**  
Enable application virtualization for UAC  
_Why:_ User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures non-UAC compliant applications to run in virtualized file and registry entries in per-user locations, allowing them to run.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableVirtualization` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Require Admin for Printer Drivers**  
Enforce Administrator role for adding printer drivers  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Print\Providers\LanMan Print Services\Servers\AddPrinterDrivers` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Show File Extensions**  
Always show file extensions in Windows Explorer  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\HideFileExt` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Show File Extensions**  
Show file extensions in Windows Explorer  
Sets `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\HideFileExt` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Show Hidden Files**  
Show hidden files and folders  
Sets `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Show Hidden Files and Folders**  
Display hidden files and folders in Windows Explorer  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Show Protected Operating System Files**  
Display protected operating system files  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\ShowSuperHidden` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** System files will be visible. Be careful not to modify or delete them.

**UAC Always Prompt**  
Always prompt for elevation on secure desktop  
_Why:_ User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the elevation requirements for logged on administrators to complete a task that requires raised privileges.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorAdmin` = `2` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

## TLS & Cryptography

_Disables broken and obsolete cryptography (SSL 2.0/3.0, TLS 1.0/1.1, RC4, DES, 3DES) at the Schannel level and enforces TLS 1.2 with modern cipher suites, so the machine cannot be downgraded onto weak encryption._

**10 settings in this section.**

**.NET Strong Cryptography**  
Enable strong cryptography for .NET Framework  
Sets `HKLM\SOFTWARE\Microsoft\.NETFramework\v4.0.30319\SchUseStrongCrypto` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Configure Strong ECC Curves**  
Set ECC curves to NistP384 and NistP256 for SSL/TLS  
_Why:_ Use of weak or untested encryption algorithms undermines the purposes of utilizing encryption to protect data. By default Windows uses ECC curves with shorter key lengths first. Requiring ECC curves with longer key lengths to be prioritized first helps ensure more secure algorithms are used.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Cryptography\Configuration\SSL\00010002\EccCurves` = `NistP384 NistP256` (REG_MULTI_SZ)  
<sub>Risk: **Low**</sub>  

**Disable DES Cipher**  
Disable the weak DES 56/56 cipher  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\DES 56/56\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable RC4 Cipher**  
Disable the weak RC4 128/128 cipher  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\RC4 128/128\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable SSL 2.0**  
Disable the insecure SSL 2.0 protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\SSL 2.0\Client\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable SSL 3.0**  
Disable the insecure SSL 3.0 protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\SSL 3.0\Client\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable TLS 1.0**  
Disable the legacy TLS 1.0 protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.0\Client\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May break legacy applications

**Disable TLS 1.1**  
Disable the legacy TLS 1.1 protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.1\Client\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May break some older applications

**Disable Triple DES**  
Disable the weak Triple DES 168 cipher  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\Triple DES 168\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May affect legacy application compatibility

**Enable TLS 1.2**  
Ensure TLS 1.2 is enabled  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Client\Enabled` = `4294967295` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

## Office Hardening

_Office documents are the single most common malware-delivery vehicle. These settings disable the features attackers weaponise — VBA macros, Dynamic Data Exchange (DDE) and ActiveX — and block macros carried in files that came from the internet._

**8 settings in this section.**

**Block Macros from Internet**  
Block macros from running in files from the internet  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Common\Security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable All ActiveX**  
Disable all ActiveX controls in Office  
Sets `HKCU\SOFTWARE\Microsoft\Office\Common\Security\DisableAllActiveX` = `1` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** Will disable all ActiveX content in Office documents

**Disable Excel DDE**  
Disable Dynamic Data Exchange in Excel  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Excel\Security\DataConnectionWarnings` = `2` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable Excel VBA Macros**  
Disable all VBA macros in Excel  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Excel\Security\VBAWarnings` = `4` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May break legitimate Excel macros

**Disable Outlook VBA Macros**  
Disable all VBA macros in Outlook  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Outlook\Security\Level` = `4` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  

**Disable PowerPoint VBA Macros**  
Disable all VBA macros in PowerPoint  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\PowerPoint\Security\VBAWarnings` = `4` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May break legitimate PowerPoint macros

**Disable Word DDE**  
Disable Dynamic Data Exchange in Word  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Word\Security\AllowDDE` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable Word VBA Macros**  
Disable all VBA macros in Word  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Word\Security\VBAWarnings` = `4` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** May break legitimate Word macros

## File Association Neutralisation

_Makes script and "scrap" file types that are pure malware-delivery vectors (.js, .vbs, .hta, .wsf, .scr, .chm, ...) open in Notepad instead of executing when double-clicked. Types power users legitimately run (.bat, .ps1, .reg) are deliberately left runnable._

**13 settings in this section.**

**Neutralize .chm Files**  
Associate .chm files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .hta Files**  
Associate .hta files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .iqy Files**  
Associate .iqy files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .js Files**  
Associate .js files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .jse Files**  
Associate .jse files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .scr Files**  
Associate .scr files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .slk Files**  
Associate .slk files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .vbe Files**  
Associate .vbe files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .vbs Files**  
Associate .vbs files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .ws Files**  
Associate .ws files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .wsc Files**  
Associate .wsc files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .wsf Files**  
Associate .wsf files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

**Neutralize .wsh Files**  
Associate .wsh files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low**</sub>  

## Windows Firewall — LOLBin Blocking

_Blocks Living-off-the-Land binaries (LOLBins) — trusted Windows tools such as certutil, mshta, wscript and regsvr32 that attackers abuse to download payloads and reach command-and-control — from making outbound network connections._

**9 settings in this section.**

**Block certutil.exe Network Access**  
Block certutil.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break certificate operations and some installers

**Block cmstp.exe Network Access**  
Block cmstp.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** Rarely affects normal use

**Block cscript.exe Network Access**  
Block cscript.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break administrative scripts

**Block mshta.exe Network Access**  
Block mshta.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break some legacy applications

**Block powershell_ise.exe Network Access**  
Block powershell_ise.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break PowerShell development

**Block regsvr32.exe Network Access**  
Block regsvr32.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break some software installations

**Block rundll32.exe Network Access**  
Block rundll32.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break various Windows functions

**Block wmic.exe Network Access**  
Block wmic.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break system administration tools

**Block wscript.exe Network Access**  
Block wscript.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High**</sub>  
> ⚠ **Impact:** May break administrative scripts

## Logging & Auditing

_You cannot investigate what you did not record. These settings turn on the visibility responders need after an incident: PowerShell script-block/module logging and transcription, process-creation auditing with command lines, and a larger security event log._

**11 settings in this section.**

**Audit Logon Events**  
Enable auditing for logon success and failure  
Enables the Windows audit subcategory.  
<sub>Risk: **Low**</sub>  

**Audit Process Creation**  
Enable auditing for process creation  
Enables the Windows audit subcategory.  
<sub>Risk: **Low**</sub>  

**Enable PowerShell Module Logging**  
Log PowerShell module activity  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ModuleLogging\EnableModuleLogging` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable PowerShell Module Logging**  
Log PowerShell module loading and pipeline execution  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ModuleLogging\EnableModuleLogging` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable PowerShell Script Block Logging**  
Log PowerShell script block execution  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable PowerShell Script Block Logging**  
Log all PowerShell script blocks for security monitoring  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable PowerShell Transcription**  
Enable PowerShell command transcription to files  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable PowerShell Transcription**  
Enable automatic PowerShell session transcription  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enlarge Security Event Log**  
Increase Security event log to 1GB  
Applies the configured system change.  
<sub>Risk: **Low**</sub>  

**Force Audit Policy Subcategory**  
Force audit policy subcategory settings to override category settings  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\SCENoApplyLegacyAuditPolicy` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Log Process Command Line**  
Include command line in process creation events  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Audit\ProcessCreationIncludeCmdLine_Enabled` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

## Removable Media

_Disables Autorun/Autoplay — the classic mechanism by which malware spreads automatically from USB drives and other removable media the instant they are inserted._

**3 settings in this section.**

**Disable Autoplay**  
Disable autoplay for non-volume devices  
_Why:_ Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. This setting will disable autoplay for non-volume devices (such as Media T…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Explorer\NoAutoplayfornonVolume` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable Autorun Completely**  
Completely disable autorun feature  
_Why:_ Allowing autorun commands to execute may introduce malicious code to a system. Configuring this setting prevents autorun commands from executing.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoAutorun` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable Autorun for All Drives**  
Disable autorun/autoplay functionality  
_Why:_ Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

## Microsoft Edge Hardening

_Enterprise-policy hardening for Microsoft Edge. The high-value controls prevent exploitation and phishing — site isolation, SmartScreen, TLS enforcement and certificate checks — rather than stripping convenience features._

**9 settings in this section.**

**Block SSL Error Override**  
Prevent users from bypassing SSL certificate errors  
Sets `HKLM\Software\Policies\Microsoft\Edge\SSLErrorOverrideAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** Users won't be able to visit sites with certificate errors

**Disable Native Messaging User Hosts**  
Disable user-level native messaging hosts  
Sets `HKLM\Software\Policies\Microsoft\Edge\NativeMessagingUserLevelHosts` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable PUA Protection**  
Block potentially unwanted applications in downloads  
_Why:_ This policy setting configures blocking for potentially unwanted apps with Microsoft Defender SmartScreen. Potentially unwanted app blocking with Microsoft Defender SmartScreen provides warning messages to help protect users from adware, coin miners, bundleware, and other low-reputation apps that ar…  
Sets `HKLM\Software\Policies\Microsoft\Edge\SmartScreenPuaEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Site Isolation**  
Run each site in its own process for better security  
_Why:_ The "SitePerProcess" policy can be used to prevent users from opting out of the default behavior of isolating all sites. The "IsolateOrigins" policy can be used to isolate additional, finer-grained origins. Enabling this policy prevents users from opting out of the default behavior where each site r…  
Sets `HKLM\Software\Policies\Microsoft\Edge\SitePerProcess` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable SmartScreen**  
Enable Microsoft Defender SmartScreen for Edge  
_Why:_ This policy setting configures Microsoft Defender SmartScreen, which provides warning messages to help protect users from potential phishing scams and malicious software. By default, Microsoft Defender SmartScreen is turned on. If this setting is enabled, Microsoft Defender SmartScreen is turned on.…  
Sets `HKLM\Software\Policies\Microsoft\Edge\SmartScreenEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enforce TLS 1.2 Minimum**  
Set minimum SSL/TLS version to TLS 1.2  
Sets `HKLM\Software\Policies\Microsoft\Edge\SSLVersionMin` = `tls1.2` (REG_SZ)  
<sub>Risk: **Low**</sub>  

**Prevent Deleting Browser History**  
Prevent users from deleting browsing history  
_Why:_ This setting disables deleting browser history and download history and prevents users from changing this setting.  
Sets `HKLM\Software\Policies\Microsoft\Edge\AllowDeletingBrowserHistory` = `0` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** Useful for compliance but may frustrate users

**Prevent SmartScreen File Override**  
Prevent bypassing SmartScreen warnings for downloads  
_Why:_ This policy setting allows a decision to be made on whether users can override Microsoft Defender SmartScreen warnings about unverified downloads. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are prevented from completing the unverified downloads. If t…  
Sets `HKLM\Software\Policies\Microsoft\Edge\PreventSmartScreenPromptOverrideForFiles` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Prevent SmartScreen Override**  
Prevent users from bypassing SmartScreen warnings  
_Why:_ This policy setting allows a decision to be made on whether users can override the Microsoft Defender SmartScreen warnings about potentially malicious websites. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are blocked from continuing to the site. If th…  
Sets `HKLM\Software\Policies\Microsoft\Edge\PreventSmartScreenPromptOverride` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

## Google Chrome Hardening

_Enterprise-policy hardening for Google Chrome — site isolation, Enhanced Safe Browsing, TLS 1.3 hardening, DNS-over-HTTPS and certificate revocation checks._

**9 settings in this section.**

**Block Outdated Plugins**  
Block running of outdated plugins  
Sets `HKLM\Software\Policies\Google\Chrome\AllowOutdatedPlugins` = `0` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Advanced Protection**  
Enable Chrome Advanced Protection Program features  
Sets `HKLM\Software\Policies\Google\Chrome\AdvancedProtectionAllowed` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Audio Sandbox**  
Run audio processing in a sandboxed process  
Sets `HKLM\SOFTWARE\Policies\Google\Chrome\AudioSandboxEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Certificate Revocation Checks**  
Enable online certificate revocation checks  
_Why:_ By setting this policy to true, the previous behavior is restored and online OCSP/CRL checks will be performed. If the policy is not set, or is set to false, then Chrome will not perform online revocation checks. Certificates are revoked when they have been compromised or are no longer valid, and th…  
Sets `HKLM\Software\Policies\Google\Chrome\EnableOnlineRevocationChecks` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable DNS over HTTPS**  
Enable encrypted DNS queries  
Sets `HKLM\SOFTWARE\Policies\Google\Chrome\DnsOverHttpsMode` = `automatic` (REG_SZ)  
<sub>Risk: **Low**</sub>  

**Enable Enhanced Safe Browsing**  
Enable enhanced safe browsing protection  
Sets `HKLM\Software\Policies\Google\Chrome\Recommended\SafeBrowsingProtectionLevel` = `2` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Site Isolation**  
Run each site in its own process  
Sets `HKLM\SOFTWARE\Policies\Google\Chrome\SitePerProcess` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable TLS 1.3 Hardening**  
Enable TLS 1.3 hardening for local anchors  
Sets `HKLM\SOFTWARE\Policies\Google\Chrome\TLS13HardeningForLocalAnchorsEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enforce TLS 1.1 Minimum**  
Set minimum TLS version to 1.1  
Sets `HKLM\Software\Policies\Google\Chrome\SSLVersionMin` = `tls1.1` (REG_SZ)  
<sub>Risk: **Low**</sub>  

## Mozilla Firefox Hardening

_Enterprise-policy hardening for Mozilla Firefox — TLS floor, DNS-over-HTTPS, and tracking protection._

**3 settings in this section.**

**Enable DNS over HTTPS**  
Enable encrypted DNS queries  
Sets `HKLM\Software\Policies\Mozilla\Firefox\DNSOverHTTPS` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Tracking Protection**  
Enable strict tracking protection  
Sets `HKLM\Software\Policies\Mozilla\Firefox\EnableTrackingProtection` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enforce TLS 1.2 Minimum**  
Set minimum TLS version to 1.2  
_Why:_ Use of versions prior to TLS 1.2 are not permitted. SSL 2.0 and SSL 3.0 contain a number of security flaws. These versions must be disabled in compliance with the Network Infrastructure and Secure Remote Computing STIGs.  
Sets `HKLM\Software\Policies\Mozilla\Firefox\SSLVersionMin` = `tls1.2` (REG_SZ)  
<sub>Risk: **Low**</sub>  

## Adobe Acrobat / Reader

_Applies Adobe reader hardening — Protected Mode/View sandboxing, Enhanced Security and disabling JavaScript — to blunt the malicious-PDF attacks that target the reader._

**6 settings in this section.**

**Disable File Attachments**  
Prevent opening of file attachments  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\iFileAttachmentPerms` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Disable JavaScript**  
Disable JavaScript execution in PDFs  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bDisableJavaScript` = `1` (REG_DWORD)  
<sub>Risk: **Medium**</sub>  
> ⚠ **Impact:** Some PDF forms may not work

**Enable Enhanced Security**  
Enable enhanced security in standalone mode  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bEnhancedSecurityStandalone` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Protected Mode**  
Enable Adobe Reader Protected Mode sandbox  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bProtectedMode` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Enable Protected View**  
Enable Protected View for all files  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\iProtectedView` = `2` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

**Suppress Upsell Messages**  
Suppress Adobe upsell and advertising  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bAcroSuppressUpsell` = `1` (REG_DWORD)  
<sub>Risk: **Low**</sub>  

## DISA STIG — Microsoft Windows 11 (V2R7)

_The Windows 11 Security Technical Implementation Guide is DISA's authoritative hardening baseline for U.S. Department of Defense systems. Every item below is a formal STIG requirement with its own STIG ID, Vulnerability ID and CCIs, applying DISA's exact mandated value._

**68 settings in this section.**

**Administrator accounts must not be enumerated during elevation.**  
Enumeration of administrator accounts when elevating can provide part of the logon information to an unauthorized user. This setting configures the system to always require users to type in a username and password to elevate a running application.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\CredUI\EnumerateAdministrators` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000200 (Vuln V-253391)</sub>  

**Anonymous access to Named Pipes and Shares must be restricted.**  
Allowing anonymous access to named pipes or shares provides the potential for unauthorized system access. This setting restricts access to those defined in "Network access: Named Pipes that can be accessed anonymously" and "Network access: Shares that can be accessed anonymously", both of which must…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RestrictNullSessAccess` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-SO-000165 (Vuln V-253456)</sub>  

**Anonymous enumeration of SAM accounts must not be allowed.**  
Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-SO-000145 (Vuln V-253453)</sub>  

**Anonymous enumeration of shares must be restricted.**  
Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-SO-000150 (Vuln V-253454)</sub>  

**Audit policy using subcategories must be enabled.**  
Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\SCENoApplyLegacyAuditPolicy` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000030 (Vuln V-253437)</sub>  

**Automatically signing in the last interactive user after a system-initiated restart must be disabled.**  
Windows can be configured to automatically sign the user back in after a Windows Update restart. Some protections are in place to help ensure this is done in a secure fashion; however, disabling this will prevent the caching of credentials for this purpose and also ensure the user is aware of the re…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\DisableAutomaticRestartSignOn` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000325 (Vuln V-253413)</sub>  

**Autoplay must be disabled for all drives.**  
Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-CC-000190 (Vuln V-253388)</sub>  

**Autoplay must be turned off for non-volume devices.**  
Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. This setting will disable autoplay for non-volume devices (such as Media T…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Explorer\NoAutoplayfornonVolume` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-CC-000180 (Vuln V-253386)</sub>  

**Command line data must be included in process creation events.**  
Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Audit\ProcessCreationIncludeCmdLine_Enabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000066 (Vuln V-253367)</sub>  

**Connections to non-domain networks when connected to a domain authenticated network must be blocked.**  
Multiple network connections can provide additional attack vectors to a system and must be limited. When connected to a domain, communication must go through the domain connection.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WcmSvc\GroupPolicy\fBlockNonDomain` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000060 (Vuln V-253365)</sub>  

**Enhanced anti-spoofing for facial recognition must be enabled on Windows 11.**  
Enhanced anti-spoofing provides additional protections when using facial recognition with devices that support it.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Biometrics\FacialFeatures\EnhancedAntiSpoofing` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000195 (Vuln V-253389)</sub>  

**Hardened UNC Paths must be defined to require mutual authentication and integrity for at least the \\*\SYSVOL…**  
Additional security requirements are applied to Universal Naming Convention (UNC) paths specified in Hardened UNC paths before allowing access them. This aids in preventing tampering with or spoofing of connections to these paths.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\NetworkProvider\HardenedPaths\\\*\NETLOGON` = `RequireMutualAuthentication=1, RequireIntegrity=1` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000050 (Vuln V-253362.a)</sub>  

**Hardened UNC Paths must be defined to require mutual authentication and integrity for at least the \\*\SYSVOL…**  
Additional security requirements are applied to Universal Naming Convention (UNC) paths specified in Hardened UNC paths before allowing access them. This aids in preventing tampering with or spoofing of connections to these paths.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\NetworkProvider\HardenedPaths\\\*\SYSVOL` = `RequireMutualAuthentication=1, RequireIntegrity=1` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000050 (Vuln V-253362.b)</sub>  

**Insecure logons to an SMB server must be disabled.**  
Insecure guest logons allow unauthenticated access to shared folders. Shared resources on a system must require authentication to establish proper access.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\LanmanWorkstation\AllowInsecureGuestAuth` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000040 (Vuln V-253360)</sub>  

**IPv6 source routing must be configured to highest protection.**  
Configuring the system to disable IPv6 source routing protects against spoofing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters\DisableIpSourceRouting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000020 (Vuln V-253353)</sub>  

**Kerberos encryption types must be configured to prevent the use of DES and RC4 encryption suites.**  
Certain encryption types are no longer considered secure. This setting configures a minimum encryption type for Kerberos, preventing the use of the DES and RC4 encryption suites.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Kerberos\Parameters\SupportedEncryptionTypes` = `2147483640` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000190 (Vuln V-253460)</sub>  

**Local accounts with blank passwords must be restricted to prevent access from the network.**  
An account without a password can allow unauthorized access to a system as only the username would be required. Password policies must prevent accounts with blank passwords from existing on a system. However, if a local account with a blank password did exist, enabling this setting will prevent netw…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LimitBlankPasswordUse` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000015 (Vuln V-253434)</sub>  

**Local administrator accounts must have their privileged token filtered to prevent elevated privileges from bei…**  
A compromised local administrator account can provide means for an attacker to move laterally between domain systems. With User Account Control enabled, filtering the privileged token for built-in administrator accounts will prevent the elevated privileges of these accounts from being used over the…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\LocalAccountTokenFilterPolicy` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000037 (Vuln V-253357)</sub>  

**Local users on domain-joined computers must not be enumerated.**  
The username is one part of logon credentials that could be used to gain access to a system. Preventing the enumeration of users limits this information to authorized personnel.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnumerateLocalUsers` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000130 (Vuln V-253379)</sub>  

**NTLM must be prevented from falling back to a Null session.**  
NTLM sessions that are allowed to fall back to Null (unauthenticated) sessions may gain unauthorized access.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\LSA\MSV1_0\allownullsessionfallback` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000180 (Vuln V-253458)</sub>  

**Outgoing secure channel traffic must be encrypted or signed.**  
Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but not all information is encrypted. If this policy is enabled, outgoing secure channel traffic will be encrypted and signed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\RequireSignOrSeal` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000035 (Vuln V-253438)</sub>  

**Outgoing secure channel traffic must be encrypted.**  
Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but not all information is encrypted. If this policy is enabled, outgoing secure channel traffic will be encrypted.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\SealSecureChannel` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000040 (Vuln V-253439)</sub>  

**Outgoing secure channel traffic must be signed.**  
Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but the channel is not integrity checked. If this policy is enabled, outgoing secure channel traffic will be signed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\SignSecureChannel` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000045 (Vuln V-253440)</sub>  

**PKU2U authentication using online identities must be prevented.**  
PKU2U is a peer-to-peer authentication protocol. This setting prevents online identities from authenticating to domain-joined systems. Authentication will be centrally managed with Windows user accounts.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\LSA\pku2u\AllowOnlineID` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000185 (Vuln V-253459)</sub>  

**PowerShell script block logging must be enabled on Windows 11.**  
Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000326 (Vuln V-253414)</sub>  

**PowerShell Transcription must be enabled on Windows 11.**  
Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000327 (Vuln V-253415)</sub>  

**Remote calls to the Security Account Manager (SAM) must be restricted to Administrators.**  
The Windows SAM stores users' passwords. Restricting remote rpc connections to the SAM to Administrators helps protect those credentials.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictRemoteSAM` = `O:BAG:BAD:(A;;RC;;;BA)` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000167 (Vuln V-253457)</sub>  

**Solicited Remote Assistance must not be allowed.**  
Remote assistance allows another user to view or take control of the local session of a user. Solicited assistance is help that is specifically requested by the local user. This may allow unauthorized parties access to the resources on the computer.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fAllowToGetHelp` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-CC-000155 (Vuln V-253382)</sub>  

**Structured Exception Handling Overwrite Protection (SEHOP) must be enabled.**  
Attackers are constantly looking for vulnerabilities in systems and applications. Structured Exception Handling Overwrite Protection (SEHOP) blocks exploits that use the Structured Exception Handling overwrite technique, a common buffer overflow attack.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel\DisableExceptionChainValidation` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-00-000150 (Vuln V-253284)</sub>  

**The computer account password must not be prevented from being reset.**  
Computer account passwords are changed automatically on a regular basis. Disabling automatic password changes can make the system more vulnerable to malicious access. Frequent password changes can be a significant safeguard for the system. A new password for the computer account will be generated ev…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\DisablePasswordChange` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; STIG: WN11-SO-000050 (Vuln V-253441)</sub>  

**The default autorun behavior must be configured to prevent autorun commands.**  
Allowing autorun commands to execute may introduce malicious code to a system. Configuring this setting prevents autorun commands from executing.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoAutorun` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-CC-000185 (Vuln V-253387)</sub>  

**The default permissions of global system objects must be increased.**  
Windows systems maintain a global list of shared system resources such as DOS device names, mutexes, and semaphores. Each type of object is created with a default DACL that specifies who can access the objects with what permissions. If this policy is enabled, the default DACL is stronger, allowing n…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\ProtectionMode` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; STIG: WN11-SO-000240 (Vuln V-253467)</sub>  

**The LanMan authentication level must be set to send NTLMv2 response only, and to refuse LM and NTLM.**  
The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-SO-000205 (Vuln V-253462)</sub>  

**The Microsoft Defender SmartScreen for Explorer must be enabled.**  
Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnableSmartScreen` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000210 (Vuln V-253395.b)</sub>  

**The network selection user interface (UI) must not be displayed on the logon screen.**  
Enabling interaction with the network selection UI allows users to change connections to available networks without signing into Windows.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\DontDisplayNetworkSelectionUI` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000120 (Vuln V-253378)</sub>  

**The Server Message Block (SMB) v1 protocol must be disabled on the SMB client.**  
SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\mrxsmb10\Start` = `4` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-00-000170 (Vuln V-253288)</sub>  

**The Server Message Block (SMB) v1 protocol must be disabled on the SMB server.**  
SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\SMB1` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-00-000165 (Vuln V-253287)</sub>  

**The system must be configured to ignore NetBIOS name release requests except from WINS servers.**  
Configuring the system to ignore name release requests, except from WINS servers, prevents a denial of service (DoS) attack. The DoS consists of sending a NetBIOS name release request to the server for each entry in the server's cache, causing a response delay in the normal operation of the servers…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netbt\Parameters\NoNameReleaseOnDemand` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; STIG: WN11-CC-000035 (Vuln V-253356)</sub>  

**The system must be configured to meet the minimum session security requirement for NTLM SSP based clients.**  
Microsoft has implemented a variety of security support providers for use with RPC sessions. All of the options must be enabled to ensure the maximum security level.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\MSV1_0\NTLMMinClientSec` = `537395200` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000215 (Vuln V-253464)</sub>  

**The system must be configured to meet the minimum session security requirement for NTLM SSP based servers.**  
Microsoft has implemented a variety of security support providers for use with RPC sessions. All of the options must be enabled to ensure the maximum security level.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\MSV1_0\NTLMMinServerSec` = `537395200` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000220 (Vuln V-253465)</sub>  

**The system must be configured to prevent anonymous users from having the same rights as the Everyone group.**  
Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000160 (Vuln V-253455)</sub>  

**The system must be configured to prevent Internet Control Message Protocol (ICMP) redirects from overriding Op…**  
Allowing ICMP redirect of routes can lead to traffic not being routed properly. When disabled, this forces ICMP to be routed via shortest path first.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\EnableICMPRedirect` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; STIG: WN11-CC-000030 (Vuln V-253355)</sub>  

**The system must be configured to prevent IP source routing.**  
Configuring the system to disable IP source routing protects against spoofing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\DisableIPSourceRouting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000025 (Vuln V-253354)</sub>  

**The system must be configured to prevent the storage of the LAN Manager hash of passwords.**  
The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-SO-000195 (Vuln V-253461)</sub>  

**The system must be configured to require a strong session key.**  
A computer connecting to a domain controller will establish a secure channel. Requiring strong session keys enforces 128-bit encryption between systems.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\RequireStrongKey` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000060 (Vuln V-253443)</sub>  

**The system must be configured to the required LDAP client signing level.**  
This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000210 (Vuln V-253463)</sub>  

**The Windows Installer feature "Always install with elevated privileges" must be disabled.**  
Standard user accounts must not be granted elevated privileges. Enabling Windows Installer to elevate privileges when installing applications can allow malicious persons and applications to gain full control of a system.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\AlwaysInstallElevated` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-CC-000315 (Vuln V-253411)</sub>  

**The Windows Remote Management (WinRM) client must not allow unencrypted traffic.**  
Unencrypted remote access to a system can allow sensitive information to be compromised. Windows remote management connections must be encrypted to prevent this.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowUnencryptedTraffic` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000335 (Vuln V-253417)</sub>  

**The Windows Remote Management (WinRM) client must not use Basic authentication.**  
Basic authentication uses plain text passwords that could be used to compromise a system.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowBasic` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-CC-000330 (Vuln V-253416)</sub>  

**The Windows Remote Management (WinRM) client must not use Digest authentication.**  
Digest authentication is not as strong as other options and may be subject to man-in-the-middle attacks.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowDigest` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000360 (Vuln V-253421)</sub>  

**The Windows Remote Management (WinRM) service must not allow unencrypted traffic.**  
Unencrypted remote access to a system can allow sensitive information to be compromised. Windows remote management connections must be encrypted to prevent this.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\AllowUnencryptedTraffic` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000350 (Vuln V-253419)</sub>  

**The Windows Remote Management (WinRM) service must not store RunAs credentials.**  
Storage of administrative credentials could allow unauthorized access. Disallowing the storage of RunAs credentials for Windows Remote Management will prevent them from being used with plug-ins.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\DisableRunAs` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000355 (Vuln V-253420)</sub>  

**The Windows Remote Management (WinRM) service must not use Basic authentication.**  
Basic authentication uses plain text passwords that could be used to compromise a system.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\AllowBasic` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: WN11-CC-000345 (Vuln V-253418)</sub>  

**The Windows SMB client must be configured to always perform SMB packet signing.**  
The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000100 (Vuln V-253449)</sub>  

**The Windows SMB server must be configured to always perform SMB packet signing.**  
The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000120 (Vuln V-253451)</sub>  

**Unauthenticated RPC clients must be restricted from connecting to the RPC server.**  
Configuring RPC to restrict unauthenticated RPC clients from connecting to the RPC server will prevent anonymous connections.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Rpc\RestrictRemoteClients` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000165 (Vuln V-253383)</sub>  

**User Account Control approval mode for the built-in Administrator must be enabled.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the built-in Administrator account so that it runs in Admin Approval Mode.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\FilterAdministratorToken` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000245 (Vuln V-253468)</sub>  

**User Account Control must automatically deny elevation requests for standard users.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. Denying elevation requests from standard user accounts requires tasks that need elevation to be initiated by accounts with administrative privileges. Thi…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorUser` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000255 (Vuln V-253471)</sub>  

**User Account Control must be configured to detect application installations and prompt for elevation.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting requires Windows to respond to application installation requests by prompting for credentials.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableInstallerDetection` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000260 (Vuln V-253472)</sub>  

**User Account Control must only elevate UIAccess applications that are installed in secure locations.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures Windows to only allow applications installed in a secure location on the file system, such as the Program Files or the Windows\Sy…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableSecureUIAPaths` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000265 (Vuln V-253473)</sub>  

**User Account Control must prompt administrators for consent on the secure desktop.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the elevation requirements for logged on administrators to complete a task that requires raised privileges.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorAdmin` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000250 (Vuln V-253469)</sub>  

**User Account Control must run all administrators in Admin Approval Mode, enabling UAC.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting enables UAC.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableLUA` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000270 (Vuln V-253474)</sub>  

**User Account Control must virtualize file and registry write failures to per-user locations.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures non-UAC compliant applications to run in virtualized file and registry entries in per-user locations, allowing them to run.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableVirtualization` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-SO-000275 (Vuln V-253475)</sub>  

**Users must be prevented from changing installation options.**  
Installation options for applications are typically controlled by administrators. This setting prevents users from changing installation options that may bypass security features.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\EnableUserControl` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000310 (Vuln V-253410)</sub>  

**WDigest Authentication must be disabled.**  
When the WDigest Authentication protocol is enabled, plain text passwords are stored in the Local Security Authority Subsystem Service (LSASS) exposing them to theft. WDigest is disabled by default in Windows 11. This setting ensures this is enforced.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\Wdigest\UseLogonCredential` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000038 (Vuln V-253358)</sub>  

**Windows 11 Kernel (Direct Memory Access) DMA Protection must be enabled.**  
Kernel DMA Protection to protect PCs against drive-by Direct Memory Access (DMA) attacks using PCI hot plug devices connected to Thunderbolt 3 ports. Drive-by DMA attacks can lead to disclosure of sensitive information residing on a PC, or even injection of malware that allows attackers to bypass th…  
Sets `HKLM\Software\Policies\Microsoft\Windows\Kernel DMA Protection\DeviceEnumerationPolicy` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-EP-000310 (Vuln V-253426)</sub>  

**Windows 11 must be configured to enable Remote host allows delegation of non-exportable credentials.**  
An exportable version of credentials is provided to remote hosts when using credential delegation which exposes them to theft on the remote host. Restricted Admin mode or Remote Credential Guard allow delegation of non-exportable credentials providing additional protection of the credentials. Enabli…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CredentialsDelegation\AllowProtectedCreds` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000068 (Vuln V-253368)</sub>  

**Windows 11 must be configured to prioritize ECC Curves with longer key lengths first.**  
Use of weak or untested encryption algorithms undermines the purposes of utilizing encryption to protect data. By default Windows uses ECC curves with shorter key lengths first. Requiring ECC curves with longer key lengths to be prioritized first helps ensure more secure algorithms are used.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Cryptography\Configuration\SSL\00010002\EccCurves` = `NistP384;NistP256` (REG_MULTI_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: WN11-CC-000052 (Vuln V-253363)</sub>  

## DISA STIG — Microsoft Edge (V2R5)

_The full DISA Microsoft Edge STIG. Many of these are strict lockdowns (disabling sync, InPrivate, imports, autofill) that go beyond exploitation prevention and add day-to-day friction — which is why only the exploitation-relevant ones appear in the Recommended profile._

**7 settings in this section.**

**Browser history must be saved.**  
This setting disables deleting browser history and download history and prevents users from changing this setting.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AllowDeletingBrowserHistory` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: EDGE-00-000033 (Vuln V-235750)</sub>  

**Bypassing Microsoft Defender SmartScreen prompts for sites must be disabled.**  
This policy setting allows a decision to be made on whether users can override the Microsoft Defender SmartScreen warnings about potentially malicious websites. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are blocked from continuing to the site. If th…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PreventSmartScreenPromptOverride` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: EDGE-00-000002 (Vuln V-235720)</sub>  

**Bypassing of Microsoft Defender SmartScreen warnings about downloads must be disabled.**  
This policy setting allows a decision to be made on whether users can override Microsoft Defender SmartScreen warnings about unverified downloads. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are prevented from completing the unverified downloads. If t…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PreventSmartScreenPromptOverrideForFiles` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: EDGE-00-000003 (Vuln V-235721)</sub>  

**Microsoft Defender SmartScreen must be configured to block potentially unwanted apps.**  
This policy setting configures blocking for potentially unwanted apps with Microsoft Defender SmartScreen. Potentially unwanted app blocking with Microsoft Defender SmartScreen provides warning messages to help protect users from adware, coin miners, bundleware, and other low-reputation apps that ar…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SmartScreenPuaEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: EDGE-00-000051 (Vuln V-235764)</sub>  

**Microsoft Defender SmartScreen must be enabled.**  
This policy setting configures Microsoft Defender SmartScreen, which provides warning messages to help protect users from potential phishing scams and malicious software. By default, Microsoft Defender SmartScreen is turned on. If this setting is enabled, Microsoft Defender SmartScreen is turned on.…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SmartScreenEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: EDGE-00-000050 (Vuln V-235763)</sub>  

**Online revocation checks must be performed.**  
If you enable this policy, Microsoft Edge will perform soft-fail, online OCSP/CRL checks. "Soft fail" means that if the revocation server can't be reached, the certificate will be considered valid. If you disable the policy or don't configure it, Microsoft Edge won't perform online revocation checks…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\EnableOnlineRevocationChecks` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: EDGE-00-000030 (Vuln V-235747)</sub>  

**Site isolation for every site must be enabled.**  
The "SitePerProcess" policy can be used to prevent users from opting out of the default behavior of isolating all sites. The "IsolateOrigins" policy can be used to isolate additional, finer-grained origins. Enabling this policy prevents users from opting out of the default behavior where each site r…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SitePerProcess` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: EDGE-00-000047 (Vuln V-235760)</sub>  

## DISA STIG — Google Chrome (V2R11)

_The full DISA Google Chrome STIG, including strict policy lockdowns beyond the exploitation-prevention subset used by the Recommended profile._

**3 settings in this section.**

**Deletion of browser history must be disabled.**  
Disabling this function will prevent users from deleting their browsing history, which could be used to identify malicious websites and files that could later be used for anti-virus and Intrusion Detection System (IDS) signatures. Furthermore, preventing users from deleting browsing history could be…  
Sets `HKLM\Software\Policies\Google\Chrome\AllowDeletingBrowserHistory` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: DTBC-0052 (Vuln V-221586)</sub>  

**Online revocation checks must be performed.**  
By setting this policy to true, the previous behavior is restored and online OCSP/CRL checks will be performed. If the policy is not set, or is set to false, then Chrome will not perform online revocation checks. Certificates are revoked when they have been compromised or are no longer valid, and th…  
Sets `HKLM\Software\Policies\Google\Chrome\EnableOnlineRevocationChecks` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: DTBC-0037 (Vuln V-221579)</sub>  

**Safe Browsing must be enabled.**  
Allows you to control whether Google Chrome's Safe Browsing feature is enabled and the mode it operates in. If this policy is set to 'NoProtection' (value 0), Safe Browsing is never active. If this policy is set to 'StandardProtection' (value 1, which is the default), Safe Browsing is always active…  
Sets `HKLM\Software\Policies\Google\Chrome\SafeBrowsingProtectionLevel` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: DTBC-0038 (Vuln V-221580)</sub>  

## DISA STIG — Mozilla Firefox (V6R7)

_The full DISA Mozilla Firefox STIG, including strict policy lockdowns beyond the exploitation-prevention subset used by the Recommended profile._

**2 settings in this section.**

**Firefox must be configured to allow only TLS 1.2 or above.**  
Use of versions prior to TLS 1.2 are not permitted. SSL 2.0 and SSL 3.0 contain a number of security flaws. These versions must be disabled in compliance with the Network Infrastructure and Secure Remote Computing STIGs.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SSLVersionMin` = `tls1.2` (REG_SZ)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: FFOX-00-000002 (Vuln V-251546)</sub>  

**Firefox must prevent the user from quickly deleting data.**  
There should not be an option for a user to "forget" work they have done. This is required to meet nonrepudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableForgetButton` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: FFOX-00-000018 (Vuln V-251562)</sub>  

## DISA STIG — Microsoft Office 365 ProPlus (V3R5)

_The full DISA Office 365 STIG — overwhelmingly high-value anti-document-malware controls (macro blocking, Protected View, ActiveX/DDE hardening, unsigned add-in blocking). The Recommended profile keeps these but omits the legacy file-format blocks that would stop old .doc/.xls/.ppt files from opening._

**77 settings in this section.**

**Active X One-Off forms must only be enabled to load with Outlook Controls.**  
By default, third-party ActiveX controls are not allowed to run in one-off forms in Outlook. You can change this behavior so that Safe Controls (Microsoft Forms 2.0 controls and the Outlook Recipient and Body controls) are allowed in one-off forms, or so that all ActiveX controls are allowed to run.  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\allowactivexoneoffforms` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000007 (Vuln V-223352)</sub>  

**AutoRepublish in Excel must be disabled.**  
This policy setting allows administrators to disable the AutoRepublish feature in Excel. If users choose to publish Excel data to a static Web page and enable the AutoRepublish feature, Excel saves a copy of the data to the Web page every time the user saves the workbook. By default, a message dialo…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\disableautorepublish` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000021 (Vuln V-223330)</sub>  

**AutoRepublish warning alert in Excel must be enabled.**  
This policy setting allows administrators to disable the AutoRepublish feature in Excel. If users choose to publish Excel data to a static Web page and enable the AutoRepublish feature, Excel saves a copy of the data to the Web page every time the user saves the workbook. By default, a message dialo…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Options\disableautorepublishwarning` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000022 (Vuln V-223331)</sub>  

**Custom user interface (UI) code must be blocked from loading in all Office applications.**  
This policy setting controls whether Office 365 ProPlus applications load any custom user interface (UI) code included with a document or template. Office 365 ProPlus allows developers to extend the UI with customization code that is included in a document or template. If this policy setting is enab…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\toolbars\noextensibilitycustomizationfromdocument` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-CO-000004 (Vuln V-223287)</sub>  

**Document metadata for rights managed Office Open XML files must be protected.**  
This policy setting determines whether metadata is encrypted in Office Open XML files that are protected by Information Rights Management (IRM). If this policy setting is enabled, Excel, PowerPoint, and Word encrypt metadata stored in rights-managed Office Open XML files and override any configurati…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\common\security\DRMEncryptProperty` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; STIG: O365-CO-000002 (Vuln V-223285)</sub>  

**Dynamic Data Exchange (DDE) server launch in Excel must be blocked.**  
This policy setting allows you to control whether Dynamic Data Exchange (DDE) server launch is allowed. By default, DDE server launch is turned off, but users can turn on DDE server launch by going to File >> Options >> Trust Center >> Trust Center Settings >> External Content. For security reasons,…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\external content\disableddeserverlaunch` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000003 (Vuln V-223312)</sub>  

**Dynamic Data Exchange (DDE) server lookup in Excel must be blocked.**  
This policy setting allows you to control whether Dynamic Data Exchange (DDE) server lookup is allowed. By default, DDE server lookup is turned on, but users can turn off DDE server lookup by going to File >> Options >> Trust Center >> Trust Center Settings >> External Content. If you enable this po…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\external content\disableddeserverlookup` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000004 (Vuln V-223313)</sub>  

**Extraction options must be blocked when opening corrupt Excel workbooks.**  
This policy setting controls whether Excel presents users with a list of data extraction options before beginning an Open and Repair operation when users choose to open a corrupt workbook in repair or extract mode. If you enable this policy setting, Excel opens the file using the Safe Load process a…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\extractdatadisableui` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000018 (Vuln V-223327)</sub>  

**File attachments from Outlook must be opened in Excel in Protected mode.**  
This policy setting allows you to determine if Excel files in Outlook attachments open in Protected View. If you enable this policy setting, Outlook attachments do not open in Protected View. If you disable or do not configure this policy setting, Outlook attachments open in Protected View.  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\protectedview\DisableAttachmentsInPV` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000034 (Vuln V-223343)</sub>  

**File extensions must be enabled to match file types in Excel.**  
This policy setting controls how Excel loads file types that do not match their extension. Excel can load files with extensions that do not match the files' type. For example, if a comma-separated values (CSV) file named example.csv is renamed example.xls (or any other file extension supported by Ex…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Security\extensionhardening` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000023 (Vuln V-223332)</sub>  

**File validation in Excel must be enabled.**  
This policy setting allows you turn off the file validation feature. If you enable this policy setting, file validation will be turned off. If you disable or do not configure this policy setting, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they conform…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\filevalidation\enableonload` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000025 (Vuln V-223334)</sub>  

**File validation in PowerPoint must be enabled.**  
This policy setting allows you to turn off the file validation feature. If you enable this policy setting, file validation will be turned off. If you disable or do not configure this policy setting, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they confo…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\filevalidation\EnableOnLoad` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PT-000006 (Vuln V-223382)</sub>  

**File validation in Word must be enabled.**  
This policy setting allows the file validation feature to be turned off. If this policy setting is enabled, file validation will be turned off. If this policy setting is disabled or not configured, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they confor…  
Sets `HKCU\software\policies\microsoft\office\16.0\word\security\filevalidation\enableonload` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-WD-000019 (Vuln V-223418)</sub>  

**Files downloaded from the Internet must be opened in Protected view in PowerPoint.**  
This policy setting allows you to determine if files downloaded from the Internet zone open in Protected View. If you enable this policy setting, files downloaded from the Internet zone do not open in Protected View. If you disable or do not configure this policy setting, files downloaded from the I…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableInternetFilesInPV` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PT-000009 (Vuln V-223385)</sub>  

**Files dragged from an Outlook e-mail to the file system must be created in ANSI format.**  
This policy setting controls whether e-mail messages dragged from Outlook to the file system are saved in Unicode or ANSI format.  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\options\general\msgformat` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000005 (Vuln V-223350)</sub>  

**Files failing file validation must be opened in Excel in Protected view mode and disallow edits.**  
This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000033 (Vuln V-223342.b)</sub>  

**Files in unsafe locations must be opened in Protected view in PowerPoint.**  
This policy setting determines whether files located in unsafe locations will open in Protected View. If unsafe locations have not been specified, only the "Downloaded Program Files" and "Temporary Internet Files" folders are considered unsafe locations. If enabling this policy setting, files locate…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableUnsafeLocationsInPV` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PT-000011 (Vuln V-223387)</sub>  

**If file validation fails, files must be opened in Protected view in PowerPoint with ability to edit disabled.**  
This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PT-000012 (Vuln V-223388.b)</sub>  

**If file validation fails, files must be opened in Protected view in Word with ability to edit disabled.**  
This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Word\Security\FileValidation\openinprotectedview` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-WD-000005 (Vuln V-223404.a)</sub>  

**If file validation fails, files must be opened in Protected view in Word with ability to edit disabled.**  
This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Word\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-WD-000005 (Vuln V-223404.b)</sub>  

**In Word, macros must be blocked from running, even if Enable all macros is selected in the Macro Settings sect…**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if "Enable all macros" is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-WD-000016 (Vuln V-223415)</sub>  

**Internet must not be included in Safe Zone for picture download in Outlook.**  
This policy setting controls whether pictures and external content in HTML e-mail messages from untrusted senders on the Internet are downloaded without Outlook users explicitly choosing to do so. If you enable this policy setting, Outlook will automatically download external content in all e-mail m…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\options\mail\Internet` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000009 (Vuln V-223354)</sub>  

**Loading of pictures from Web pages not created in Excel must be disabled.**  
This policy setting controls whether Excel loads graphics when opening Web pages that were not created in Excel. It configures the "Load pictures from Web pages not created in Excel" option under the File tab >> Options >> Advanced >> General >> Web Options... >> General tab. If you enable or do not…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\internet\donotloadpictures` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000020 (Vuln V-223329)</sub>  

**Macros from the Internet must be blocked from running in PowerPoint.**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if "Enable all macros" is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\powerpoint\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PT-000007 (Vuln V-223383)</sub>  

**Macros in all Office applications that are opened programmatically by another application must be opened based…**  
This policy setting controls whether macros can run in an Office 365 ProPlus application that is opened programmatically by another application. If this policy setting is enabled, the user can choose from three options for controlling macro behavior in Excel, PowerPoint, and Word when the applicatio…  
Sets `HKCU\Software\Policies\Microsoft\Office\Common\Security\AutomationSecurity` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-CO-000006 (Vuln V-223289)</sub>  

**Macros must be blocked from running in Access files from the Internet.**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\access\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-AC-000001 (Vuln V-223280)</sub>  

**Macros must be blocked from running in Excel files from the Internet.**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000027 (Vuln V-223336)</sub>  

**Macros must be blocked from running in Visio files from the Internet.**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-VI-000007 (Vuln V-223399)</sub>  

**Office applications must be configured to specify encryption type in password-protected Office 97-2003 files.**  
This policy setting enables you to specify an encryption type for password-protected Office 97-2003 files. If you enable this policy setting, you can specify the type of encryption that Office applications will use to encrypt password-protected files in the older Office 97-2003 file formats. The cho…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Common\Security\defaultencryption12` = `Microsoft Enhanced RSA and AES Cryptographic Provider,AES 256,256` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-CO-000008 (Vuln V-223291)</sub>  

**Office applications must be configured to specify encryption type in password-protected Office Open XML files.**  
This policy setting allows you to specify an encryption type for Office Open XML files. If you enable this policy setting, you can specify the type of encryption that Office applications use to encrypt password-protected files in the Office Open XML file formats used by Excel, PowerPoint, and Word.…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Common\Security\OpenXMLEncryption` = `Microsoft Enhanced RSA and AES Cryptographic Provider,AES 256,256` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-CO-000009 (Vuln V-223292)</sub>  

**Open/save of Excel 2 macrosheets and add-in files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL2Macros` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000007 (Vuln V-223316)</sub>  

**Open/save of Excel 3 macrosheets and add-in files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL3Macros` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000009 (Vuln V-223318)</sub>  

**Open/save of Excel 4 macrosheets and add-in files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL4Macros` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000011 (Vuln V-223320)</sub>  

**Outlook must be configured to allow retrieving of Certificate Revocation Lists (CRLs) always when online.**  
This policy setting controls how Outlook retrieves Certificate Revocation Lists to verify the validity of certificates. Certificate revocation lists (CRLs) are lists of digital certificates that have been revoked by their controlling certificate authorities (CAs), typically because the certificates…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\usecrlchasing` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000013 (Vuln V-223358)</sub>  

**Outlook must be configured to not allow hyperlinks in suspected phishing messages.**  
This policy setting controls whether hyperlinks in suspected phishing e-mail messages in Outlook are allowed. If you enable this policy setting, Outlook will allow hyperlinks in suspected phishing messages that are not also classified as junk e-mail. If you disable or do not configure this policy se…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\options\mail\JunkMailEnableLinks` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000027 (Vuln V-223372)</sub>  

**Outlook must be configured to not run scripts in forms in which the script and the layout are contained within…**  
This policy setting controls whether scripts can run in Outlook forms in which the script and layout are contained within the message. If you enable this policy setting, scripts can run in one-off Outlook forms. If you disable or do not configure this policy setting, Outlook does not run scripts in…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\EnableOneOffFormScripts` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000019 (Vuln V-223364)</sub>  

**Outlook must be configured to prevent users overriding attachment security settings.**  
This policy setting prevents users from overriding the set of attachments blocked by Outlook. If you enable this policy setting users will be prevented from overriding the set of attachments blocked by Outlook. Outlook also checks the "Level1Remove" registry key when this setting is specified. If yo…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\disallowattachmentcustomization` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000008 (Vuln V-223353)</sub>  

**Outlook must use remote procedure call (RPC) encryption to communicate with Microsoft Exchange servers.**  
This policy setting controls whether Outlook uses remote procedure call (RPC) encryption to communicate with Microsoft Exchange servers. If you enable this policy setting, Outlook uses RPC encryption when communicating with an Exchange server. Note: RPC encryption only encrypts the data from the Out…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\rpc\enablerpcencryption` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000002 (Vuln V-223347)</sub>  

**PowerPoint attachments opened from Outlook must be in Protected View.**  
This policy setting allows for determining whether PowerPoint files in Outlook attachments open in Protected View. If enabling this policy setting, Outlook attachments do not open in Protected View. If disabling or not configuring this policy setting, Outlook attachments open in Protected View.  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableAttachmentsInPV` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PT-000010 (Vuln V-223386)</sub>  

**Project must automatically disable unsigned add-ins without informing users.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\Microsoft\office\16.0\ms project\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PR-000002 (Vuln V-223375)</sub>  

**Publisher must automatically disable unsigned add-ins without informing users.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\microsoft\office\16.0\publisher\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PU-000002 (Vuln V-223391)</sub>  

**Publisher must be configured to prompt the user when another application programmatically opens a macro.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if you enable the "Require that application add-ins are signed by Trusted Publishe…  
Sets `HKCU\software\policies\microsoft\office\common\security\automationsecuritypublisher` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PU-000001 (Vuln V-223390)</sub>  

**Scripts associated with public folders must be prevented from execution in Outlook.**  
This policy setting controls whether Outlook executes scripts that are associated with custom forms or folder home pages for public folders.  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\publicfolderscript` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000003 (Vuln V-223348)</sub>  

**Scripts associated with shared folders must be prevented from execution in Outlook.**  
This policy setting controls whether Outlook executes scripts associated with custom forms or folder home pages for shared folders.  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\sharedfolderscript` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000004 (Vuln V-223349)</sub>  

**The ability to demote attachments from Level 2 to Level 1 must be disabled.**  
This policy setting controls whether Outlook users can demote attachments to Level 2 by using a registry key, which will allow them to save files to disk and open them from that location. Outlook uses two levels of security to restrict access to files attached to e-mail messages or other items. File…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\allowuserstolowerattachments` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000015 (Vuln V-223360)</sub>  

**The display of Level 1 attachments must be disabled in Outlook.**  
This policy setting controls whether Outlook blocks potentially dangerous attachments designated Level 1. Outlook uses two levels of security to restrict users' access to files attached to e-mail messages or other items. Files with specific extensions can be categorized as Level 1 (users cannot view…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\ShowLevel1Attach` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000016 (Vuln V-223361)</sub>  

**The HTTP fallback for SIP connection in Lync must be disabled.**  
Prevents from HTTP being used for SIP connection in case TLS or TCP fail.  
Sets `HKLM\Software\Policies\Microsoft\office\16.0\lync\disablehttpconnect` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-LY-000002 (Vuln V-223345)</sub>  

**The load of controls in Forms3 must be blocked.**  
This policy setting allows the user to control how ActiveX controls in UserForms should be initialized based upon whether they are Safe for Initialization (SFI) or Unsafe for Initialization (UFI). ActiveX controls are Component Object Model (COM) objects and have unrestricted access to users' comput…  
Sets `HKCU\SOFTWARE\Policies\Microsoft\vba\security\LoadControlsInForms` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-CO-000013 (Vuln V-223295)</sub>  

**The Macro Runtime Scan Scope must be enabled for all documents.**  
This policy setting specifies for which documents the VBA Runtime Scan feature is enabled. If the feature is disabled for all documents, no runtime scanning of enabled macros will be performed. If the feature is enabled for low trust documents, the feature will be enabled for all documents for which…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\security\macroruntimescanscope` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-CO-000001 (Vuln V-223284)</sub>  

**The Outlook Security Mode must be enabled to always use the Outlook Security Group Policy.**  
This policy setting controls which set of security settings are enforced in Outlook. If you enable this policy setting, you can choose from four options for enforcing Outlook security settings: - Outlook Default Security - This option is the default configuration in Outlook. Users can configure secu…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\adminsecuritymode` = `3` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000014 (Vuln V-223359)</sub>  

**The Publish to Global Address List (GAL) button must be disabled in Outlook.**  
This policy setting controls whether Outlook users can publish e-mail certificates to the Global Address List (GAL). If you enable this policy setting, the "Publish to GAL" button does not display in the "E-mail Security" section of the Trust Center. If you disable or do not configure this policy se…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\publishtogaldisabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000010 (Vuln V-223355)</sub>  

**The Security Level for macros in Outlook must be configured to Warn for signed and disable unsigned.**  
This policy setting controls the security level for macros in Outlook. If you enable this policy setting, you can choose from four options for handling macros in Outlook: - Always warn. This option corresponds to the "Warnings for all macros" option in the "Macro Security" section of the Outlook Tru…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\level` = `3` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000028 (Vuln V-223373)</sub>  

**The SIP security mode in Lync must be enabled.**  
When Lync connects to the server, it supports various authentication mechanisms. This policy allows the user to specify whether Digest and Basic authentication are supported. Disabled (default): NTLM/Kerberos/TLS-DSK/Digest/Basic Enabled: Authentication mechanisms: NTLM/Kerberos/TLS-DSK Gal Download…  
Sets `HKLM\Software\Policies\Microsoft\office\16.0\lync\enablesiphighsecuritymode` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-LY-000001 (Vuln V-223344)</sub>  

**The use of network locations must be ignored in PowerPoint.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\trusted locations\AllowNetworkLocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PT-000013 (Vuln V-223389)</sub>  

**The warning about invalid digital signatures must be enabled to warn Outlook users.**  
This policy setting controls how Outlook warns users about messages with invalid digital signatures. If you enable this policy setting, you can choose from three options for controlling how Outlook users are warned about invalid signatures: - Let user decide if they want to be warned. This option en…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\warnaboutinvalid` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000012 (Vuln V-223357)</sub>  

**Trust Bar notification must be enabled for unsigned application add-ins in Excel and blocked.**  
This policy setting controls whether the specified Office 2016 applications notify users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the ''Require that application add-ins are signed by Trusted Publisher'' po…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000028 (Vuln V-223337)</sub>  

**Trust Bar Notifications for unsigned application add-ins in Access must be disabled and blocked.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\access\security\NoTBPromptUnsignedAddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-AC-000002 (Vuln V-223281)</sub>  

**Trust Bar notifications must be configured to display information in the Message Bar about the content that ha…**  
This policy setting controls whether Office 365 ProPlus applications notify users when potentially unsafe features or content are detected, or whether such features or content are silently disabled without notification. The Message Bar in Office 365 ProPlus applications is used to identify security…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\trustcenter\trustbar` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-CO-000007 (Vuln V-223290)</sub>  

**Trusted Locations on the network must be disabled in Excel.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by selecting the "Allow Trusted Locations on my network (no…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\trusted locations\AllowNetworkLocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000001 (Vuln V-223310)</sub>  

**Trusted Locations on the network must be disabled in Project.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…  
Sets `HKCU\software\policies\microsoft\office\16.0\ms project\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PR-000001 (Vuln V-223374)</sub>  

**Trusted Locations on the network must be disabled in Visio.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…  
Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-VI-000002 (Vuln V-223394)</sub>  

**Trusted Locations on the network must be disabled in Word.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…  
Sets `HKCU\software\policies\microsoft\office\16.0\word\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-WD-000017 (Vuln V-223416)</sub>  

**Unsigned add-ins in PowerPoint must be blocked with no Trust Bar Notification to the user.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\Microsoft\office\16.0\powerpoint\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-PT-000008 (Vuln V-223384)</sub>  

**Untrusted database files must be opened in Excel in Protected View mode.**  
This policy setting controls whether database files (.dbf) opened from an untrusted location are always opened in Protected View. If you enable this policy setting, database files opened from an untrusted location are always opened in Protected View. Users will not be able to change this setting und…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\protectedview\enabledatabasefileprotectedview` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000030 (Vuln V-223339)</sub>  

**Untrusted Microsoft Query files must be blocked from opening in Excel.**  
This policy setting controls whether Microsoft Query files (.iqy, oqy, .dqy, and .rqy) in an untrusted location are prevented from opening. If you enable this policy setting, Microsoft Query files in an untrusted location are prevented from opening. Users will not be able to change this setting unde…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\external content\enableblockunsecurequeryfiles` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000029 (Vuln V-223338)</sub>  

**Updating of links in Excel must be prompted and not automatic.**  
This policy setting controls whether Excel prompts users to update automatic links, or whether the updates occur in the background with no prompt. If you enable or do not configure this policy setting, Excel will prompt users to update automatic links. In addition, the "Ask to update automatic links…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\binaryoptions\fupdateext_78_1` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-EX-000019 (Vuln V-223328)</sub>  

**Users must be prevented from creating new trusted locations in the Trust Center.**  
This policy setting controls whether trusted locations can be defined by users, the Office Customization Tool (OCT), and Group Policy, or if they must be defined by Group Policy alone. If you enable this policy setting, users can specify any location as a trusted location, and a computer can have a…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\security\trusted locations\allow user locations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-CO-000010 (Vuln V-223293)</sub>  

**Visio must automatically disable unsigned add-ins without informing users.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-VI-000003 (Vuln V-223395)</sub>  

**When a custom action is executed that uses the Outlook object model, Outlook must automatically deny it.**  
This policy setting controls whether Outlook prompts users before executing a custom action. Custom actions add functionality to Outlook that can be triggered as part of a rule. Among other possible features, custom actions can be created that reply to messages in ways that circumvent the Outlook mo…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomcustomaction` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000020 (Vuln V-223365)</sub>  

**When a user designs a custom form in Outlook and attempts to bind an Address Information field to a combinatio…**  
This policy setting controls what happens when a user designs a custom form in Outlook and attempts to bind an Address Information field to a combination or formula custom field. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to acces…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\PromptOOMFormulaAccess` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000022 (Vuln V-223367)</sub>  

**When an untrusted program attempts to gain access to a recipient field, such as the, To: field, using the Outl…**  
This policy setting controls what happens when an untrusted program attempts to gain access to a recipient field, such as the ''To:'' field, using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to access a re…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomaddressinformationaccess` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000024 (Vuln V-223369)</sub>  

**When an untrusted program attempts to programmatically access an Address Book using the Outlook object model,…**  
This policy setting controls what happens when an untrusted program attempts to gain access to an Address Book using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to programmatically access an Address Book u…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomaddressbookaccess` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000021 (Vuln V-223366)</sub>  

**When an untrusted program attempts to programmatically send e-mail in Outlook using the Response method of a t…**  
This policy setting controls what happens when an untrusted program attempts to programmatically send e-mail in Outlook using the Response method of a task or meeting request. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to programm…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoommeetingtaskrequestresponse` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000025 (Vuln V-223370)</sub>  

**When an untrusted program attempts to send e-mail programmatically using the Outlook object model, Outlook mus…**  
This policy setting controls what happens when an untrusted program attempts to send e-mail programmatically using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to send e-mail programmatically using the Outl…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomsend` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000026 (Vuln V-223371)</sub>  

**When an untrusted program attempts to use the Save As command to programmatically save an item, Outlook must a…**  
This policy setting controls what happens when an untrusted program attempts to use the Save As command to programmatically save an item. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to use the Save As command to programmatically sa…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomsaveas` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-OU-000023 (Vuln V-223368)</sub>  

**Word attachments opened from Outlook must be in Protected View.**  
This policy setting allows you to determine if Word files in Outlook attachments open in Protected View. If you enable this policy setting, Outlook attachments do not open in Protected View. If you disable or do not configure this policy setting, Outlook attachments open in Protected View.  
Sets `HKCU\software\policies\microsoft\office\16.0\word\security\protectedview\disableattachmentsinpv` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-WD-000006 (Vuln V-223405)</sub>  

**Word must automatically disable unsigned add-ins without informing users.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\microsoft\office\16.0\word\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; STIG: O365-WD-000001 (Vuln V-223400)</sub>  


