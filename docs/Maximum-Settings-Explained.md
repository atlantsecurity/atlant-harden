# AtlantHarden — Maximum (All) Settings, Explained

This document is the **complete reference for every setting in AtlantHarden** — the full set the **Maximum** profile applies. Each entry shows what it changes, why it matters, the exact system change, and its risk level. The **Profile** line on each setting shows whether it is also part of the curated **Recommended** profile or is **Maximum-only** (an additional strict lockdown).

**Maximum applies everything, including the full DISA STIG lockdowns.** That delivers maximum compliance but *will* add friction — password managers disabled, InPrivate/Incognito off, Controlled Folder Access, legacy file-format blocks, and more. Use it only where you want and understand that trade-off. Every change is backed up automatically; if a setting ever prevents the tool from relaunching, revert with the exported `restore_*.reg` file or Windows System Restore.

## Contents
- [Attack Surface Reduction (ASR)](#attack-surface-reduction-asr) — 19 settings
- [Microsoft Defender Antivirus](#microsoft-defender-antivirus) — 9 settings
- [Credential Protection](#credential-protection) — 13 settings
- [Network Security](#network-security) — 30 settings
- [System Hardening](#system-hardening) — 29 settings
- [TLS & Cryptography](#tls--cryptography) — 10 settings
- [Office Hardening](#office-hardening) — 8 settings
- [File Association Neutralisation](#file-association-neutralisation) — 24 settings
- [Windows Firewall — LOLBin Blocking](#windows-firewall--lolbin-blocking) — 10 settings
- [Logging & Auditing](#logging--auditing) — 12 settings
- [Removable Media](#removable-media) — 3 settings
- [Privacy](#privacy) — 12 settings
- [Microsoft Edge Hardening](#microsoft-edge-hardening) — 12 settings
- [Google Chrome Hardening](#google-chrome-hardening) — 18 settings
- [Mozilla Firefox Hardening](#mozilla-firefox-hardening) — 10 settings
- [Adobe Acrobat / Reader](#adobe-acrobat--reader) — 6 settings
- [DISA STIG — Microsoft Windows 11 (V2R7)](#disa-stig--microsoft-windows-11-v2r7) — 114 settings
- [DISA STIG — Microsoft Edge (V2R5)](#disa-stig--microsoft-edge-v2r5) — 52 settings
- [DISA STIG — Google Chrome (V2R11)](#disa-stig--google-chrome-v2r11) — 39 settings
- [DISA STIG — Mozilla Firefox (V6R7)](#disa-stig--mozilla-firefox-v6r7) — 43 settings
- [DISA STIG — Microsoft Office 365 ProPlus (V3R5)](#disa-stig--microsoft-office-365-proplus-v3r5) — 106 settings

## Attack Surface Reduction (ASR)

_Microsoft Defender ASR rules block the specific behaviours malware relies on — Office spawning executables, scripts launching payloads, credential theft from LSASS, ransomware file patterns — at the kernel level, before code runs. They are the single highest-value anti-malware control and are almost invisible in day-to-day use._

**19 settings in this section.**

**Advanced Ransomware Protection**  
Use advanced protection against ransomware  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block Adobe Reader Child Processes**  
Prevent Adobe Reader from creating child processes that could be malicious  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block Credential Stealing from LSASS**  
Block credential stealing from Windows LSASS  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block Email Executable Content**  
Block executable content from email client and webmail  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block Executable Office Content**  
Block Office apps from creating executable content  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May prevent Office from saving certain file types

**Block Impersonated System Tools**  
Block executables that impersonate or copy Windows system tools  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block JS/VBS Downloaded Executables**  
Block JavaScript or VBScript from launching downloaded executables  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May block legitimate installers and scripts

**Block Low Prevalence Executables**  
Block executables that don't meet prevalence, age, or trusted list criteria  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** May block legitimate new or uncommon software

**Block Obfuscated Scripts**  
Block execution of potentially obfuscated scripts  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May block some legitimate PowerShell scripts

**Block Office Child Processes**  
Prevent Office applications from creating child processes  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break legitimate Office workflows that launch external programs

**Block Office Code Injection**  
Prevent Office apps from injecting code into other processes  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break Office add-ins and integrations

**Block Office Communication App Child Processes**  
Prevent Outlook from creating child processes to block social engineering attacks  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block PSExec and WMI Process Creation**  
Block processes created via PSExec and WMI commands to prevent lateral movement  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May block legitimate admin tools - incompatible with SCCM/ConfigMgr

**Block Safe Mode Reboot Commands**  
Prevent bcdedit and bootcfg from restarting machine in Safe Mode where security tools are disabled  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block Untrusted USB Processes**  
Block untrusted and unsigned processes running from USB  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** Will block portable applications from USB drives

**Block Vulnerable Signed Drivers**  
Prevent exploitation of vulnerable signed drivers that could be used for kernel access  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block Webshell Creation for Servers**  
Prevent web shell script creation on Microsoft Server and Exchange  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block Win32 API from Office Macros**  
Block Office macros from calling Win32 APIs  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** Will break many legitimate VBA macros that interact with Windows

**Block WMI Event Subscription Persistence**  
Prevent malware from using WMI event subscriptions to persist on the system  
Enables the Microsoft Defender ASR rule in **Block** mode.  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## Microsoft Defender Antivirus

_Tunes the built-in antivirus itself — cloud-delivered protection, network protection, PUA (potentially unwanted application) blocking and sandboxing — so it catches more, faster, and cannot be casually paused._

**9 settings in this section.**

**Disable Pause Windows Defender Scan**  
Prevent users from pausing Windows Defender scans  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\AllowPause` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable Cloud-Delivered Protection**  
Enable cloud-based protection for better threat detection  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Controlled Folder Access**  
Protect important folders from ransomware and malicious apps  
Applies the configured system change.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** May block legitimate applications. Whitelist apps as needed.

**Enable Defender Sandbox**  
Run Windows Defender in a sandbox for better security  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; Reboot required</sub>  

**Enable Network Protection**  
Block connections to malicious IP addresses and domains  
Applies the configured system change.  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May occasionally block legitimate websites

**Enable PUA Protection**  
Enable detection of Potentially Unwanted Applications  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Extended Cloud Check Timeout**  
Extend cloud check timeout to 50 seconds  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**High Cloud Block Level**  
Set cloud block level to high for aggressive protection  
Applies the configured system change.  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** May cause false positives

**Send All Samples to Microsoft**  
Automatically send suspicious samples for analysis  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

## Credential Protection

_Stops attackers from stealing the credentials that let them move from one machine to the whole network. These settings protect the LSASS process (where Windows holds credentials in memory), stop weak-hash and cleartext storage, and enforce modern authentication — the controls that defeat Mimikatz-style attacks._

**13 settings in this section.**

**Account Lockout Duration (15 minutes)**  
Lock account for 15 minutes after exceeding threshold  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Account Lockout Reset Window (15 minutes)**  
Reset account lockout counter after 15 minutes  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Account Lockout Threshold (5 attempts)**  
Lock account after 5 invalid logon attempts  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Block Blank Password Network Logons**  
Prevent local accounts with blank passwords from network logon  
_Why:_ An account without a password can allow unauthorized access to a system as only the username would be required. Password policies must prevent accounts with blank passwords from existing on a system. However, if a local account with a blank password did exist, enabling this setting will prevent netw…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LimitBlankPasswordUse` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable Domain Credential Caching**  
Prevent storage of credentials for network authentication  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\DisableDomainCreds` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** May affect domain authentication in some scenarios

**Disable LM Hash Storage**  
Do not store LAN Manager hash value on next password change  
_Why:_ The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable WDigest Authentication**  
Prevent storing credentials in memory (cleartext)  
_Why:_ When the WDigest Authentication protocol is enabled, plain text passwords are stored in the Local Security Authority Subsystem Service (LSASS) exposing them to theft. WDigest is disabled by default in Windows 11. This setting ensures this is enforced.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest\UseLogonCredential` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable WDigest Negotiation**  
Disable WDigest negotiate protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest\Negotiate` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Do Not Store LM Hash**  
Prevent storage of LAN Manager hash on next password change  
_Why:_ The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable LSASS Audit Mode**  
Audit access to LSASS for security monitoring  
Sets `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\LSASS.exe\AuditLevel` = `8` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Protected Credentials**  
Allow delegation of non-exported credentials  
_Why:_ An exportable version of credentials is provided to remote hosts when using credential delegation which exposes them to theft on the remote host. Restricted Admin mode or Remote Credential Guard allow delegation of non-exportable credentials providing additional protection of the credentials. Enabli…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CredentialsDelegation\AllowProtectedCreds` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enforce NTLMv2 Only**  
Set LAN Manager authentication level to NTLMv2 only  
_Why:_ The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May affect legacy system compatibility

**LSASS Protected Process**  
Run LSASS as a Protected Process Light (PPL)  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RunAsPPL` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; Reboot required</sub>  

## Network Security

_Removes the legacy and insecure network behaviour attackers abuse for interception and lateral movement: SMBv1 (EternalBlue/WannaCry), name-resolution poisoning (LLMNR/NetBIOS/WPAD), unsigned SMB/LDAP traffic, and anonymous enumeration of accounts and shares._

**30 settings in this section.**

**Block Anonymous Everyone Access**  
Disable Everyone permissions for anonymous users  
_Why:_ Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable DCOM**  
Disable Distributed Component Object Model for remote commands  
Sets `HKLM\Software\Microsoft\OLE\EnableDCOM` = `N` (REG_SZ)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** May break some remote administration tools

**Disable File Sharing Within Profile**  
Prevent users from sharing files within their profile  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoFileSharingControl` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** Users won't be able to share files from their profile. Use network shares instead.

**Disable ICMP Redirects**  
Do not allow ICMP redirects to override OSPF routes  
_Why:_ Allowing ICMP redirect of routes can lead to traffic not being routed properly. When disabled, this forces ICMP to be routed via shortest path first.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\EnableICMPRedirect` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable IP Helper Service**  
Disable IP Helper to prevent port proxy attacks  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\iphlpsvc\Start` = `4` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** May affect IPv6 transition technologies

**Disable IP Source Routing**  
Prevent IP source routing attacks  
_Why:_ Configuring the system to disable IP source routing protects against spoofing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\DisableIPSourceRouting` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable IPv6**  
Disable IPv6 on all network interfaces  
Sets `HKLM\SYSTEM\CurrentControlSet\services\tcpip6\parameters\DisabledComponents` = `255` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** May break Microsoft services, modern apps, and IPv6-only networks

**Disable LLMNR**  
Disable Link-Local Multicast Name Resolution  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient\EnableMulticast` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable LLMNR (Link-Local Multicast Name Resolution)**  
Disable LLMNR to prevent credential interception attacks  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\DNSClient\EnableMulticast` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Disable NetBIOS over TCP/IP**  
Stop NetBIOS over TCP/IP service  
Applies the configured system change.  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May affect legacy file sharing

**Disable NetBIOS over TCP/IP**  
Disable NetBIOS name resolution to prevent credential attacks  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\NetBT\Parameters\Interfaces\Tcpip_*\NetbiosOptions` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** May affect legacy applications that rely on NetBIOS name resolution.

**Disable SMBv1 Client**  
Disable the SMBv1 client driver  
_Why:_ SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\mrxsmb10\Start` = `4` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; Reboot required</sub>  

**Disable SMBv1 Server**  
Disable the vulnerable SMBv1 protocol (server side)  
_Why:_ SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\SMB1` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; Reboot required</sub>  

**Disable WPAD**  
Disable Web Proxy Auto-Discovery protocol  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Wpad\WpadOverride` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable WPAD (Web Proxy Auto-Discovery)**  
Disable automatic proxy discovery to prevent man-in-the-middle attacks  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings\Wpad\WpadOverride` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable SMB Signing (Client)**  
Enable SMB packet signing for client communications  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\EnableSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Exclude Anonymous from Everyone Group**  
Let Everyone permissions not apply to anonymous users  
_Why:_ Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require LDAP Client Signing**  
Require LDAP client signing for DC communications  
_Why:_ This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Require LDAP Client Signing**  
Require LDAP client to perform signing  
_Why:_ This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require LDAP Server Signing**  
Require LDAP server integrity signing  
Sets `HKLM\System\CurrentControlSet\Services\NTDS\Parameters\LDAPServerIntegrity` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Require SMB Signing (Client)**  
Require SMB packet signing for client connections  
_Why:_ The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.  
Sets `HKLM\System\CurrentControlSet\Services\LanmanWorkStation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Require SMB Signing (Client)**  
Require SMB packet signing for client communications  
_Why:_ The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require SMB Signing (Server)**  
Require SMB packet signing for server communications  
_Why:_ The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require SMB Signing (Server)**  
Require SMB packet signing for server connections  
_Why:_ The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.  
Sets `HKLM\System\CurrentControlSet\Services\LanmanServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Restrict Anonymous Access to Named Pipes and Shares**  
Do not allow anonymous enumeration of SAM accounts and shares  
_Why:_ Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Restrict Anonymous SAM Enumeration**  
Prevent anonymous enumeration of SAM accounts  
_Why:_ Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Restrict Anonymous SAM Enumeration**  
Do not allow anonymous enumeration of SAM accounts  
_Why:_ Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Restrict Anonymous Share Enumeration**  
Prevent anonymous enumeration of shares  
_Why:_ Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Restrict Null Session Access**  
Restrict anonymous access to named pipes and shares  
_Why:_ Allowing anonymous access to named pipes or shares provides the potential for unauthorized system access. This setting restricts access to those defined in "Network access: Named Pipes that can be accessed anonymously" and "Network access: Shares that can be accessed anonymously", both of which must…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RestrictNullSessAccess` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Use NTLMv2 Only**  
Send NTLMv2 response only, refuse LM and NTLM  
_Why:_ The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** May break authentication with very old systems that don't support NTLMv2.

## System Hardening

_Core OS hardening — User Account Control, exploit mitigations (SEHOP, safe DLL search order), Autorun/Autoplay, SmartScreen for downloaded files, and closing privilege-escalation holes such as AlwaysInstallElevated._

**29 settings in this section.**

**Always Process Group Policy**  
Process Group Policy objects even if they haven't changed  
_Why:_ Enabling this setting and then selecting the "Process even if the Group Policy objects have not changed" option ensures that the policies will be reprocessed even if none have been changed. This way, any unauthorized changes are forced to match the domain-based group policy settings again.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Group Policy\{35378EAC-683F-11D2-A89A-00C04FBBCFA2}\NoGPOListChanges` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Block CWD DLL Loading**  
Block DLL loading from current working directory (remote)  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\CWDIllegalInDllSearch` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Block DMA Until User Logon**  
Block Direct Memory Access ports until user logs on  
_Why:_ Kernel DMA Protection to protect PCs against drive-by Direct Memory Access (DMA) attacks using PCI hot plug devices connected to Thunderbolt 3 ports. Drive-by DMA attacks can lead to disclosure of sensitive information residing on a PC, or even injection of malware that allows attackers to bypass th…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Kernel DMA Protection\DeviceEnumerationPolicy` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Deny Execute Access on Removable Media**  
Prevent execution of programs from removable storage devices  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\RemovableStorageDevices\Deny_Execute` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Deny Write Access to Removable Media**  
Prevent writing data to removable storage devices  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\RemovableStorageDevices\Deny_Write` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** Users will not be able to write to USB drives, external hard drives, etc.

**Disable 8.3 Filename Creation**  
Disable short 8.3 filename creation for better security  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Always Install Elevated**  
Prevent installers from using elevated privileges by default  
_Why:_ Standard user accounts must not be granted elevated privileges. Enabling Windows Installer to elevate privileges when installing applications can allow malicious persons and applications to gain full control of a system.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\AlwaysInstallElevated` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable AutoRun for All Drives**  
Disable automatic execution features for removable media  
_Why:_ Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Disable ClickOnce Trust Prompt**  
Disable ClickOnce application trust prompts  
Sets `HKLM\SOFTWARE\MICROSOFT\.NETFramework\Security\TrustManager\PromptingLevel\Internet` = `Disabled` (REG_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Command Prompt for Users**  
Prevent users from accessing Command Prompt to limit attack surface  
Sets `HKCU\SOFTWARE\Policies\Microsoft\Windows\System\DisableCMD` = `2` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** Users will not be able to run Command Prompt. Set to 2 to still allow batch files.

**Disable Lock Screen Camera**  
Disable camera access while screen is locked  
_Why:_ Enabling camera access from the lock screen could allow for unauthorized use. Requiring logon will ensure the device is only used by authorized personnel.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Personalization\NoLockScreenCamera` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Storage Sense**  
Prevent automatic file cleanup that could delete evidence  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\StorageSense\AllowStorageSenseGlobal` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Disable Windows Script Host**  
Prevent VBS/JS scripts from running via WSH  
Sets `HKCU\SOFTWARE\Microsoft\Windows Script Host\Settings\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break legitimate scripts

**Enable Biometric Anti-Spoofing**  
Enable enhanced anti-spoofing for facial recognition  
_Why:_ Enhanced anti-spoofing provides additional protections when using facial recognition with devices that support it.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Biometrics\FacialFeatures\EnhancedAntiSpoofing` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable PowerShell Constrained Language Mode**  
Restrict PowerShell to constrained language mode  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Environment\__PSLockDownPolicy` = `4` (REG_SZ)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** Many PowerShell scripts and administrative tools will not work. Test thoroughly.

**Enable Safe DLL Search Mode**  
Protect against DLL hijacking attacks  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\SafeDLLSearchMode` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable SEHOP**  
Enable Structured Exception Handling Overwrite Protection  
_Why:_ Attackers are constantly looking for vulnerabilities in systems and applications. Structured Exception Handling Overwrite Protection (SEHOP) blocks exploits that use the Structured Exception Handling overwrite technique, a common buffer overflow attack.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel\DisableExceptionChainValidation` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable SmartScreen**  
Enable Windows SmartScreen filter  
_Why:_ Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnableSmartScreen` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable UAC**  
Enable User Account Control  
_Why:_ User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting enables UAC.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableLUA` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Virtualization Based Security**  
Enable application virtualization for UAC  
_Why:_ User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures non-UAC compliant applications to run in virtualized file and registry entries in per-user locations, allowing them to run.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableVirtualization` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Prevent Local Group Policy Modifications**  
Prevent users from modifying Local Group Policy settings  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisableLocalMachineRun` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Require Admin for Printer Drivers**  
Enforce Administrator role for adding printer drivers  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Print\Providers\LanMan Print Services\Servers\AddPrinterDrivers` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Show File Extensions**  
Show file extensions in Windows Explorer  
Sets `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\HideFileExt` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Show File Extensions**  
Always show file extensions in Windows Explorer  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\HideFileExt` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Show Hidden Files**  
Show hidden files and folders  
Sets `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Show Hidden Files and Folders**  
Display hidden files and folders in Windows Explorer  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Show Protected Operating System Files**  
Display protected operating system files  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\ShowSuperHidden` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** System files will be visible. Be careful not to modify or delete them.

**SmartScreen Block Level**  
Set SmartScreen to Block mode  
_Why:_ Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\ShellSmartScreenLevel` = `Block` (REG_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**UAC Always Prompt**  
Always prompt for elevation on secure desktop  
_Why:_ User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the elevation requirements for logged on administrators to complete a task that requires raised privileges.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorAdmin` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## TLS & Cryptography

_Disables broken and obsolete cryptography (SSL 2.0/3.0, TLS 1.0/1.1, RC4, DES, 3DES) at the Schannel level and enforces TLS 1.2 with modern cipher suites, so the machine cannot be downgraded onto weak encryption._

**10 settings in this section.**

**.NET Strong Cryptography**  
Enable strong cryptography for .NET Framework  
Sets `HKLM\SOFTWARE\Microsoft\.NETFramework\v4.0.30319\SchUseStrongCrypto` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Configure Strong ECC Curves**  
Set ECC curves to NistP384 and NistP256 for SSL/TLS  
_Why:_ Use of weak or untested encryption algorithms undermines the purposes of utilizing encryption to protect data. By default Windows uses ECC curves with shorter key lengths first. Requiring ECC curves with longer key lengths to be prioritized first helps ensure more secure algorithms are used.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Cryptography\Configuration\SSL\00010002\EccCurves` = `NistP384 NistP256` (REG_MULTI_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable DES Cipher**  
Disable the weak DES 56/56 cipher  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\DES 56/56\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable RC4 Cipher**  
Disable the weak RC4 128/128 cipher  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\RC4 128/128\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable SSL 2.0**  
Disable the insecure SSL 2.0 protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\SSL 2.0\Client\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable SSL 3.0**  
Disable the insecure SSL 3.0 protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\SSL 3.0\Client\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable TLS 1.0**  
Disable the legacy TLS 1.0 protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.0\Client\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break legacy applications

**Disable TLS 1.1**  
Disable the legacy TLS 1.1 protocol  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.1\Client\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break some older applications

**Disable Triple DES**  
Disable the weak Triple DES 168 cipher  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Ciphers\Triple DES 168\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May affect legacy application compatibility

**Enable TLS 1.2**  
Ensure TLS 1.2 is enabled  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL\Protocols\TLS 1.2\Client\Enabled` = `4294967295` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## Office Hardening

_Office documents are the single most common malware-delivery vehicle. These settings disable the features attackers weaponise — VBA macros, Dynamic Data Exchange (DDE) and ActiveX — and block macros carried in files that came from the internet._

**8 settings in this section.**

**Block Macros from Internet**  
Block macros from running in files from the internet  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Common\Security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable All ActiveX**  
Disable all ActiveX controls in Office  
Sets `HKCU\SOFTWARE\Microsoft\Office\Common\Security\DisableAllActiveX` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** Will disable all ActiveX content in Office documents

**Disable Excel DDE**  
Disable Dynamic Data Exchange in Excel  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Excel\Security\DataConnectionWarnings` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable Excel VBA Macros**  
Disable all VBA macros in Excel  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Excel\Security\VBAWarnings` = `4` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break legitimate Excel macros

**Disable Outlook VBA Macros**  
Disable all VBA macros in Outlook  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Outlook\Security\Level` = `4` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable PowerPoint VBA Macros**  
Disable all VBA macros in PowerPoint  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\PowerPoint\Security\VBAWarnings` = `4` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break legitimate PowerPoint macros

**Disable Word DDE**  
Disable Dynamic Data Exchange in Word  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Word\Security\AllowDDE` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable Word VBA Macros**  
Disable all VBA macros in Word  
Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Word\Security\VBAWarnings` = `4` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break legitimate Word macros

## File Association Neutralisation

_Makes script and "scrap" file types that are pure malware-delivery vectors (.js, .vbs, .hta, .wsf, .scr, .chm, ...) open in Notepad instead of executing when double-clicked. Types power users legitimately run (.bat, .ps1, .reg) are deliberately left runnable._

**24 settings in this section.**

**Neutralize .bat Files**  
Associate .bat files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .chm Files**  
Associate .chm files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .cmd Files**  
Associate .cmd files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .deploy Files**  
Associate .deploy files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .diff Files**  
Associate .diff files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .hta Files**  
Associate .hta files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .iqy Files**  
Associate .iqy files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .iso Files**  
Associate .iso files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .js Files**  
Associate .js files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .jse Files**  
Associate .jse files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .prn Files**  
Associate .prn files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .ps1 Files**  
Associate .ps1 files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .rdg Files**  
Associate .rdg files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .reg Files**  
Associate .reg files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .scr Files**  
Associate .scr files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .slk Files**  
Associate .slk files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .url Files**  
Associate .url files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .vbe Files**  
Associate .vbe files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .vbs Files**  
Associate .vbs files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .wcx Files**  
Associate .wcx files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Neutralize .ws Files**  
Associate .ws files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .wsc Files**  
Associate .wsc files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .wsf Files**  
Associate .wsf files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Neutralize .wsh Files**  
Associate .wsh files with Notepad to prevent automatic execution  
Re-associates the file extension to open as text (Notepad) instead of executing.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## Windows Firewall — LOLBin Blocking

_Blocks Living-off-the-Land binaries (LOLBins) — trusted Windows tools such as certutil, mshta, wscript and regsvr32 that attackers abuse to download payloads and reach command-and-control — from making outbound network connections._

**10 settings in this section.**

**Block certutil.exe Network Access**  
Block certutil.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break certificate operations and some installers

**Block cmstp.exe Network Access**  
Block cmstp.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** Rarely affects normal use

**Block cscript.exe Network Access**  
Block cscript.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break administrative scripts

**Block mshta.exe Network Access**  
Block mshta.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break some legacy applications

**Block msiexec.exe Network Access**  
Block msiexec.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** May break software installation from network

**Block powershell_ise.exe Network Access**  
Block powershell_ise.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break PowerShell development

**Block regsvr32.exe Network Access**  
Block regsvr32.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break some software installations

**Block rundll32.exe Network Access**  
Block rundll32.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break various Windows functions

**Block wmic.exe Network Access**  
Block wmic.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break system administration tools

**Block wscript.exe Network Access**  
Block wscript.exe from making outbound network connections  
Creates a Windows Firewall rule blocking the binary's outbound network access.  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** May break administrative scripts

## Logging & Auditing

_You cannot investigate what you did not record. These settings turn on the visibility responders need after an incident: PowerShell script-block/module logging and transcription, process-creation auditing with command lines, and a larger security event log._

**12 settings in this section.**

**Audit Logon Events**  
Enable auditing for logon success and failure  
Enables the Windows audit subcategory.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Audit Process Creation**  
Enable auditing for process creation  
Enables the Windows audit subcategory.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable PowerShell Module Logging**  
Log PowerShell module activity  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ModuleLogging\EnableModuleLogging` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable PowerShell Module Logging**  
Log PowerShell module loading and pipeline execution  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ModuleLogging\EnableModuleLogging` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable PowerShell Script Block Logging**  
Log PowerShell script block execution  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable PowerShell Script Block Logging**  
Log all PowerShell script blocks for security monitoring  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enable PowerShell Transcription**  
Enable PowerShell command transcription to files  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable PowerShell Transcription**  
Enable automatic PowerShell session transcription  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Enlarge Security Event Log**  
Increase Security event log to 1GB  
Applies the configured system change.  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Force Audit Policy Subcategory**  
Force audit policy subcategory settings to override category settings  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\SCENoApplyLegacyAuditPolicy` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Halt When Audit Log Full**  
Shut down system when security audit log is full  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\CrashOnAuditFail` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  
> ⚠ **Impact:** System will shut down if audit log becomes full. Ensure adequate log size and monitoring.

**Log Process Command Line**  
Include command line in process creation events  
_Why:_ Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Audit\ProcessCreationIncludeCmdLine_Enabled` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## Removable Media

_Disables Autorun/Autoplay — the classic mechanism by which malware spreads automatically from USB drives and other removable media the instant they are inserted._

**3 settings in this section.**

**Disable Autoplay**  
Disable autoplay for non-volume devices  
_Why:_ Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. This setting will disable autoplay for non-volume devices (such as Media T…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Explorer\NoAutoplayfornonVolume` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable Autorun Completely**  
Completely disable autorun feature  
_Why:_ Allowing autorun commands to execute may introduce malicious code to a system. Configuring this setting prevents autorun commands from executing.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoAutorun` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable Autorun for All Drives**  
Disable autorun/autoplay functionality  
_Why:_ Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## Privacy

_Reduces the data Windows sends to Microsoft and third parties — telemetry, advertising ID, location, Cortana/Bing and consumer "suggestions". These are privacy improvements rather than anti-malware controls, which is why they are excluded from the Recommended profile._

**12 settings in this section.**

**Block Language List Access**  
Prevent websites from accessing local language list  
Sets `HKCU\Control Panel\International\User Profile\HttpAcceptLanguageOptOut` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Clear Recent Documents on Exit**  
Clear recent documents list when user logs off  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\ClearRecentDocsOnExit` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; ACSC Essential Eight</sub>  

**Deny Location Access**  
Disable location services for apps  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\Location` = `Deny` (REG_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Advertising ID**  
Disable the unique advertising ID for this device  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AdvertisingInfo\DisabledByGroupPolicy` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Bing Search**  
Disable Bing web search in Start Menu  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search\BingSearchEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Consumer Features**  
Disable Microsoft consumer features and suggestions  
_Why:_ Microsoft consumer experiences provides suggestions and notifications to users, which may include the installation of Windows Store apps. Organizations may control the execution of applications through other means such as allowlisting. Turning off Microsoft consumer experiences will help prevent the…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CloudContent\DisableWindowsConsumerFeatures` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Cortana**  
Disable Cortana consent and suggestions  
Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Search\CortanaConsent` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable GameDVR**  
Disable Windows Game DVR and broadcasting  
_Why:_ Windows Game Recording and Broadcasting is intended for use with games; however, it could potentially record screen shots of other applications and expose sensitive data. Disabling the feature will prevent this from occurring.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\GameDVR\AllowGameDVR` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Inventory Collector**  
Disable application inventory data collection  
_Why:_ Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting will pre…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AppCompat\DisableInventory` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Lock Screen Notifications**  
Prevent toast notifications on lock screen  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CurrentVersion\PushNotifications\NoToastApplicationNotificationOnLockScreen` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Settings Sync**  
Disable synchronization of Windows settings to cloud  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\SettingSync\DisableSettingSync` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Telemetry**  
Set Windows telemetry to security only level  
_Why:_ Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Limiting this capability will prevent potentially sensitive information from being sent outside the enterprise. The "Security" option for Telemetry configures the lowest amoun…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection\AllowTelemetry` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

## Microsoft Edge Hardening

_Enterprise-policy hardening for Microsoft Edge. The high-value controls prevent exploitation and phishing — site isolation, SmartScreen, TLS enforcement and certificate checks — rather than stripping convenience features._

**12 settings in this section.**

**Block SSL Error Override**  
Prevent users from bypassing SSL certificate errors  
Sets `HKLM\Software\Policies\Microsoft\Edge\SSLErrorOverrideAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** Users won't be able to visit sites with certificate errors

**Disable Background Mode**  
Prevent Edge from running in background after closing  
_Why:_ Background processing allows Microsoft Edge processes to start at OS sign-in and keep running after the last browser window is closed. In this scenario, background apps and the current browsing session remain active, including any session cookies. An open background process displays an icon in the s…  
Sets `HKLM\Software\Policies\Microsoft\Edge\BackgroundModeEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable InPrivate Browsing**  
Disable InPrivate browsing mode for compliance  
_Why:_ This setting specifies whether the user can open pages in InPrivate mode in Microsoft Edge. If this policy is not configured or set it to "Enabled", users can open pages in InPrivate mode. Set this policy to "Disabled" to stop users from using InPrivate mode. Set this policy to "Forced" to always us…  
Sets `HKLM\Software\Policies\Microsoft\Edge\InPrivateModeAvailability` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** Users won't be able to use InPrivate browsing

**Disable Native Messaging User Hosts**  
Disable user-level native messaging hosts  
Sets `HKLM\Software\Policies\Microsoft\Edge\NativeMessagingUserLevelHosts` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable Password Manager**  
Disable built-in password manager (use external)  
_Why:_ Enable Microsoft Edge to save user passwords. If this policy is enabled, users can save their passwords in Microsoft Edge. The next time the user visits the site, Microsoft Edge will enter the password automatically.  
Sets `HKLM\Software\Policies\Microsoft\Edge\PasswordManagerEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Enable PUA Protection**  
Block potentially unwanted applications in downloads  
_Why:_ This policy setting configures blocking for potentially unwanted apps with Microsoft Defender SmartScreen. Potentially unwanted app blocking with Microsoft Defender SmartScreen provides warning messages to help protect users from adware, coin miners, bundleware, and other low-reputation apps that ar…  
Sets `HKLM\Software\Policies\Microsoft\Edge\SmartScreenPuaEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Site Isolation**  
Run each site in its own process for better security  
_Why:_ The "SitePerProcess" policy can be used to prevent users from opting out of the default behavior of isolating all sites. The "IsolateOrigins" policy can be used to isolate additional, finer-grained origins. Enabling this policy prevents users from opting out of the default behavior where each site r…  
Sets `HKLM\Software\Policies\Microsoft\Edge\SitePerProcess` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable SmartScreen**  
Enable Microsoft Defender SmartScreen for Edge  
_Why:_ This policy setting configures Microsoft Defender SmartScreen, which provides warning messages to help protect users from potential phishing scams and malicious software. By default, Microsoft Defender SmartScreen is turned on. If this setting is enabled, Microsoft Defender SmartScreen is turned on.…  
Sets `HKLM\Software\Policies\Microsoft\Edge\SmartScreenEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enforce TLS 1.2 Minimum**  
Set minimum SSL/TLS version to TLS 1.2  
Sets `HKLM\Software\Policies\Microsoft\Edge\SSLVersionMin` = `tls1.2` (REG_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Prevent Deleting Browser History**  
Prevent users from deleting browsing history  
_Why:_ This setting disables deleting browser history and download history and prevents users from changing this setting.  
Sets `HKLM\Software\Policies\Microsoft\Edge\AllowDeletingBrowserHistory` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** Useful for compliance but may frustrate users

**Prevent SmartScreen File Override**  
Prevent bypassing SmartScreen warnings for downloads  
_Why:_ This policy setting allows a decision to be made on whether users can override Microsoft Defender SmartScreen warnings about unverified downloads. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are prevented from completing the unverified downloads. If t…  
Sets `HKLM\Software\Policies\Microsoft\Edge\PreventSmartScreenPromptOverrideForFiles` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Prevent SmartScreen Override**  
Prevent users from bypassing SmartScreen warnings  
_Why:_ This policy setting allows a decision to be made on whether users can override the Microsoft Defender SmartScreen warnings about potentially malicious websites. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are blocked from continuing to the site. If th…  
Sets `HKLM\Software\Policies\Microsoft\Edge\PreventSmartScreenPromptOverride` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## Google Chrome Hardening

_Enterprise-policy hardening for Google Chrome — site isolation, Enhanced Safe Browsing, TLS 1.3 hardening, DNS-over-HTTPS and certificate revocation checks._

**18 settings in this section.**

**Block Outdated Plugins**  
Block running of outdated plugins  
Sets `HKLM\Software\Policies\Google\Chrome\AllowOutdatedPlugins` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable Autoplay**  
Disable automatic media playback  
_Why:_ This allows a user to control if videos can play automatically with audio content (without user consent) in Google Chrome. If the policy is set to "True", Google Chrome is allowed to autoplay media. If the policy is set to "False", Google Chrome is not allowed to autoplay media. The "AutoplayAllowli…  
Sets `HKLM\Software\Policies\Google\Chrome\AutoplayAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Background Mode**  
Prevent Chrome from running in background  
_Why:_ Determines whether a Google Chrome process is started on OS login that keeps running when the last browser window is closed, allowing background apps to remain active. The background process displays an icon in the system tray and can always be closed from there. If this policy is set to True, backg…  
Sets `HKLM\Software\Policies\Google\Chrome\BackgroundModeEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Chrome Metrics**  
Disable usage statistics reporting  
_Why:_ Enables anonymous reporting of usage and crash-related data about Google Chrome to Google and prevents users from changing this setting. If you enable this setting, anonymous reporting of usage and crash-related data is sent to Google. A crash report could contain sensitive information from the comp…  
Sets `HKLM\Software\Policies\Google\Chrome\MetricsReportingEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Guest Mode**  
Disable Chrome guest browsing mode  
_Why:_ If this policy is set to true or not configured, Google Chrome will enable guest logins. Guest logins are Google Chrome profiles where all windows are in incognito mode. If this policy is set to false, Google Chrome will not allow guest profiles to be started.  
Sets `HKLM\Software\Policies\Google\Chrome\BrowserGuestModeEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Incognito Mode**  
Disable incognito browsing mode for compliance  
_Why:_ Incognito mode allows the user to browse the Internet without recording their browsing history/activity. From a forensics perspective, this is unacceptable. Best practice requires that browser history is retained. The "IncognitoModeAvailability" setting controls whether the user may utilize Incognit…  
Sets `HKLM\Software\Policies\Google\Chrome\IncognitoModeAvailability` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** Users won't be able to use Incognito mode

**Disable Media Router**  
Disable Chrome Cast/media router functionality  
_Why:_ If this policy is set to ”True” or is not set, Google Cast will be enabled, and users will be able to launch it from the app menu, page context menus, media controls on Cast-enabled websites, and (if shown) the “Cast toolbar” icon. If this policy set to ”False”, Google Cast will be disabled.  
Sets `HKLM\Software\Policies\Google\Chrome\EnableMediaRouter` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Password Import**  
Prevent importing saved passwords  
_Why:_ Importing of saved passwords should be disabled as it could lead to unencrypted account passwords stored on the system from another browser to be viewed. This policy forces the saved passwords to be imported from the previous default browser if enabled. If enabled, this policy also affects the impor…  
Sets `HKLM\Software\Policies\Google\Chrome\ImportSavedPasswords` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Password Manager**  
Disable built-in password manager  
_Why:_ Enables saving passwords and using saved passwords in Google Chrome. Malicious sites may take advantage of this feature by using hidden fields gain access to the stored information. If you enable this setting, users can have Google Chrome memorize passwords and provide them automatically the next ti…  
Sets `HKLM\Software\Policies\Google\Chrome\PasswordManagerEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Search Suggestions**  
Disable search and URL suggestions  
_Why:_ Search suggestion should be disabled as it could lead to searches being conducted that were never intended to be made. Enables search suggestions in Google Chrome's omnibox and prevents users from changing this setting. If you enable this setting, search suggestions are used. If you disable this set…  
Sets `HKLM\Software\Policies\Google\Chrome\SearchSuggestEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Enable Advanced Protection**  
Enable Chrome Advanced Protection Program features  
Sets `HKLM\Software\Policies\Google\Chrome\AdvancedProtectionAllowed` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Audio Sandbox**  
Run audio processing in a sandboxed process  
Sets `HKLM\SOFTWARE\Policies\Google\Chrome\AudioSandboxEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Certificate Revocation Checks**  
Enable online certificate revocation checks  
_Why:_ By setting this policy to true, the previous behavior is restored and online OCSP/CRL checks will be performed. If the policy is not set, or is set to false, then Chrome will not perform online revocation checks. Certificates are revoked when they have been compromised or are no longer valid, and th…  
Sets `HKLM\Software\Policies\Google\Chrome\EnableOnlineRevocationChecks` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable DNS over HTTPS**  
Enable encrypted DNS queries  
Sets `HKLM\SOFTWARE\Policies\Google\Chrome\DnsOverHttpsMode` = `automatic` (REG_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Enhanced Safe Browsing**  
Enable enhanced safe browsing protection  
Sets `HKLM\Software\Policies\Google\Chrome\Recommended\SafeBrowsingProtectionLevel` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Site Isolation**  
Run each site in its own process  
Sets `HKLM\SOFTWARE\Policies\Google\Chrome\SitePerProcess` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable TLS 1.3 Hardening**  
Enable TLS 1.3 hardening for local anchors  
Sets `HKLM\SOFTWARE\Policies\Google\Chrome\TLS13HardeningForLocalAnchorsEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enforce TLS 1.1 Minimum**  
Set minimum TLS version to 1.1  
Sets `HKLM\Software\Policies\Google\Chrome\SSLVersionMin` = `tls1.1` (REG_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## Mozilla Firefox Hardening

_Enterprise-policy hardening for Mozilla Firefox — TLS floor, DNS-over-HTTPS, and tracking protection._

**10 settings in this section.**

**Disable Default Browser Agent**  
Disable Firefox default browser agent  
Sets `HKLM\Software\Policies\Mozilla\Firefox\DisableDefaultBrowserAgent` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Firefox Accounts**  
Disable Firefox sync and accounts  
_Why:_ Disable Firefox Accounts integration (Sync). It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may remain unsecured. They increase the risk to…  
Sets `HKLM\Software\Policies\Mozilla\Firefox\DisableFirefoxAccounts` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Form History**  
Disable saving form and search history  
_Why:_ To protect privacy and sensitive data, Firefox provides the ability to configure the program so that data entered into forms is not saved. This mitigates the risk of a website gleaning private information from prefilled information.  
Sets `HKLM\Software\Policies\Mozilla\Firefox\DisableFormHistory` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Password Manager**  
Disable built-in password manager (use external)  
_Why:_ Firefox can be set to store passwords for sites visited by the user. These individual passwords are stored in a file and can be protected by a master password. Autofill of the password can then be enabled when the site is visited. This feature could also be used to autofill the certificate PIN, whic…  
Sets `HKLM\Software\Policies\Mozilla\Firefox\PasswordManagerEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Pocket**  
Disable Pocket integration in Firefox  
_Why:_ Pocket, previously known as Read It Later, is a social bookmarking service for storing, sharing, and discovering web bookmarks. Data gathering cloud services such as this are generally disabled in the DoD.  
Sets `HKLM\Software\Policies\Mozilla\Firefox\DisablePocket` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Disable Private Browsing**  
Disable private browsing mode for compliance  
_Why:_ Private browsing allows the user to browse the internet without recording their browsing history/activity. From a forensics perspective, this is unacceptable. Best practice requires that browser history is retained.  
Sets `HKLM\Software\Policies\Mozilla\Firefox\DisablePrivateBrowsing` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only</sub>  
> ⚠ **Impact:** Users won't be able to use private browsing

**Disable Telemetry**  
Disable Firefox telemetry and data collection  
_Why:_ Firefox by default sends information about Firefox to Mozilla servers. There should be no background submission of technical and other information from DoD computers to Mozilla with portions posted publicly.  
Sets `HKLM\Software\Policies\Mozilla\Firefox\DisableTelemetry` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only</sub>  

**Enable DNS over HTTPS**  
Enable encrypted DNS queries  
Sets `HKLM\Software\Policies\Mozilla\Firefox\DNSOverHTTPS` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Tracking Protection**  
Enable strict tracking protection  
Sets `HKLM\Software\Policies\Mozilla\Firefox\EnableTrackingProtection` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enforce TLS 1.2 Minimum**  
Set minimum TLS version to 1.2  
_Why:_ Use of versions prior to TLS 1.2 are not permitted. SSL 2.0 and SSL 3.0 contain a number of security flaws. These versions must be disabled in compliance with the Network Infrastructure and Secure Remote Computing STIGs.  
Sets `HKLM\Software\Policies\Mozilla\Firefox\SSLVersionMin` = `tls1.2` (REG_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## Adobe Acrobat / Reader

_Applies Adobe reader hardening — Protected Mode/View sandboxing, Enhanced Security and disabling JavaScript — to blunt the malicious-PDF attacks that target the reader._

**6 settings in this section.**

**Disable File Attachments**  
Prevent opening of file attachments  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\iFileAttachmentPerms` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Disable JavaScript**  
Disable JavaScript execution in PDFs  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bDisableJavaScript` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended**</sub>  
> ⚠ **Impact:** Some PDF forms may not work

**Enable Enhanced Security**  
Enable enhanced security in standalone mode  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bEnhancedSecurityStandalone` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Protected Mode**  
Enable Adobe Reader Protected Mode sandbox  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bProtectedMode` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Enable Protected View**  
Enable Protected View for all files  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\iProtectedView` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

**Suppress Upsell Messages**  
Suppress Adobe upsell and advertising  
Sets `HKLM\Software\Policies\Adobe\Acrobat Reader\DC\FeatureLockDown\bAcroSuppressUpsell` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended**</sub>  

## DISA STIG — Microsoft Windows 11 (V2R7)

_The Windows 11 Security Technical Implementation Guide is DISA's authoritative hardening baseline for U.S. Department of Defense systems. Every item below is a formal STIG requirement with its own STIG ID, Vulnerability ID and CCIs, applying DISA's exact mandated value._

**114 settings in this section.**

**Administrator accounts must not be enumerated during elevation.**  
Enumeration of administrator accounts when elevating can provide part of the logon information to an unauthorized user. This setting configures the system to always require users to type in a username and password to elevate a running application.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\CredUI\EnumerateAdministrators` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000200 (Vuln V-253391)</sub>  

**Anonymous access to Named Pipes and Shares must be restricted.**  
Allowing anonymous access to named pipes or shares provides the potential for unauthorized system access. This setting restricts access to those defined in "Network access: Named Pipes that can be accessed anonymously" and "Network access: Shares that can be accessed anonymously", both of which must…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RestrictNullSessAccess` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000165 (Vuln V-253456)</sub>  

**Anonymous enumeration of SAM accounts must not be allowed.**  
Anonymous enumeration of SAM accounts allows anonymous log on users (null session connections) to list all accounts names, thus providing a list of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymousSAM` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000145 (Vuln V-253453)</sub>  

**Anonymous enumeration of shares must be restricted.**  
Allowing anonymous logon users (null session connections) to list all account names and enumerate all shared resources can provide a map of potential points to attack the system.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictAnonymous` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000150 (Vuln V-253454)</sub>  

**Attachments must be prevented from being downloaded from RSS feeds.**  
Attachments from RSS feeds may not be secure. This setting will prevent attachments from being downloaded from RSS feeds.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Internet Explorer\Feeds\DisableEnclosureDownload` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000295 (Vuln V-253407)</sub>  

**Audit policy using subcategories must be enabled.**  
Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\SCENoApplyLegacyAuditPolicy` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000030 (Vuln V-253437)</sub>  

**Automatically signing in the last interactive user after a system-initiated restart must be disabled.**  
Windows can be configured to automatically sign the user back in after a Windows Update restart. Some protections are in place to help ensure this is done in a secure fashion; however, disabling this will prevent the caching of credentials for this purpose and also ensure the user is aware of the re…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\DisableAutomaticRestartSignOn` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000325 (Vuln V-253413)</sub>  

**Autoplay must be disabled for all drives.**  
Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000190 (Vuln V-253388)</sub>  

**Autoplay must be turned off for non-volume devices.**  
Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. This setting will disable autoplay for non-volume devices (such as Media T…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Explorer\NoAutoplayfornonVolume` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000180 (Vuln V-253386)</sub>  

**Bluetooth must be turned off unless approved by the organization.**  
If not configured properly, Bluetooth may allow rogue devices to communicate with a system. If a rogue device is paired with a system, there is potential for sensitive information to be compromised.  
Sets `HKLM\SOFTWARE\Microsoft\PolicyManager\current\device\Connectivity\AllowBluetooth` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-00-000210 (Vuln V-253291)</sub>  

**Camera access from the lock screen must be disabled.**  
Enabling camera access from the lock screen could allow for unauthorized use. Requiring logon will ensure the device is only used by authorized personnel.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Personalization\NoLockScreenCamera` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000005 (Vuln V-253350)</sub>  

**Command line data must be included in process creation events.**  
Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Audit\ProcessCreationIncludeCmdLine_Enabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000066 (Vuln V-253367)</sub>  

**Connections to non-domain networks when connected to a domain authenticated network must be blocked.**  
Multiple network connections can provide additional attack vectors to a system and must be limited. When connected to a domain, communication must go through the domain connection.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WcmSvc\GroupPolicy\fBlockNonDomain` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000060 (Vuln V-253365)</sub>  

**Credential Guard must be running on Windows 11 domain-joined systems.**  
Credential Guard uses virtualization-based security to protect information that could be used in credential theft attacks if compromised. This authentication information, which was stored in the Local Security Authority (LSA) in previous versions of Windows, is isolated from the rest of operating sy…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DeviceGuard\LsaCfgFlags` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000075 (Vuln V-253370)</sub>  

**Downloading print driver packages over HTTP must be prevented.**  
Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting prevents…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Printers\DisableWebPnPDownload` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000100 (Vuln V-253374)</sub>  

**Enhanced anti-spoofing for facial recognition must be enabled on Windows 11.**  
Enhanced anti-spoofing provides additional protections when using facial recognition with devices that support it.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Biometrics\FacialFeatures\EnhancedAntiSpoofing` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000195 (Vuln V-253389)</sub>  

**Enhanced diagnostic data must be limited to the minimum required to support Windows Analytics.**  
Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Limiting this capability will prevent potentially sensitive information from being sent outside the enterprise. The "Enhanced" level for telemetry includes additional informat…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection\LimitEnhancedDiagnosticDataWindowsAnalytics` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000204 (Vuln V-253392)</sub>  

**Group Policy objects must be reprocessed even if they have not changed.**  
Enabling this setting and then selecting the "Process even if the Group Policy objects have not changed" option ensures that the policies will be reprocessed even if none have been changed. This way, any unauthorized changes are forced to match the domain-based group policy settings again.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Group Policy\{35378EAC-683F-11D2-A89A-00C04FBBCFA2}\NoGPOListChanges` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000090 (Vuln V-253373)</sub>  

**Hardened UNC Paths must be defined to require mutual authentication and integrity for at least the \\*\SYSVOL…**  
Additional security requirements are applied to Universal Naming Convention (UNC) paths specified in Hardened UNC paths before allowing access them. This aids in preventing tampering with or spoofing of connections to these paths.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\NetworkProvider\HardenedPaths\\\*\NETLOGON` = `RequireMutualAuthentication=1, RequireIntegrity=1` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000050 (Vuln V-253362.a)</sub>  

**Hardened UNC Paths must be defined to require mutual authentication and integrity for at least the \\*\SYSVOL…**  
Additional security requirements are applied to Universal Naming Convention (UNC) paths specified in Hardened UNC paths before allowing access them. This aids in preventing tampering with or spoofing of connections to these paths.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\NetworkProvider\HardenedPaths\\\*\SYSVOL` = `RequireMutualAuthentication=1, RequireIntegrity=1` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000050 (Vuln V-253362.b)</sub>  

**Indexing of encrypted files must be turned off.**  
Indexing of encrypted files may expose sensitive data. This setting prevents encrypted files from being indexed.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Windows Search\AllowIndexingEncryptedStoresOrItems` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000305 (Vuln V-253409)</sub>  

**Insecure logons to an SMB server must be disabled.**  
Insecure guest logons allow unauthenticated access to shared folders. Shared resources on a system must require authentication to establish proper access.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\LanmanWorkstation\AllowInsecureGuestAuth` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000040 (Vuln V-253360)</sub>  

**Internet connection sharing must be disabled.**  
Internet connection sharing makes it possible for an existing internet connection, such as through wireless, to be shared and used by other systems essentially creating a mobile hotspot. This exposes the system sharing the connection to others with potentially malicious purpose.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Network Connections\NC_ShowSharedAccessUI` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000044 (Vuln V-253361)</sub>  

**IPv6 source routing must be configured to highest protection.**  
Configuring the system to disable IPv6 source routing protects against spoofing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip6\Parameters\DisableIpSourceRouting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000020 (Vuln V-253353)</sub>  

**Kerberos encryption types must be configured to prevent the use of DES and RC4 encryption suites.**  
Certain encryption types are no longer considered secure. This setting configures a minimum encryption type for Kerberos, preventing the use of the DES and RC4 encryption suites.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Kerberos\Parameters\SupportedEncryptionTypes` = `2147483640` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000190 (Vuln V-253460)</sub>  

**Local accounts with blank passwords must be restricted to prevent access from the network.**  
An account without a password can allow unauthorized access to a system as only the username would be required. Password policies must prevent accounts with blank passwords from existing on a system. However, if a local account with a blank password did exist, enabling this setting will prevent netw…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LimitBlankPasswordUse` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000015 (Vuln V-253434)</sub>  

**Local administrator accounts must have their privileged token filtered to prevent elevated privileges from bei…**  
A compromised local administrator account can provide means for an attacker to move laterally between domain systems. With User Account Control enabled, filtering the privileged token for built-in administrator accounts will prevent the elevated privileges of these accounts from being used over the…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\LocalAccountTokenFilterPolicy` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000037 (Vuln V-253357)</sub>  

**Local drives must be prevented from sharing with Remote Desktop Session Hosts.**  
Preventing users from sharing the local drives on their client computers to Remote Session Hosts that they access helps reduce possible exposure of sensitive data.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fDisableCdm` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000275 (Vuln V-253403)</sub>  

**Local users on domain-joined computers must not be enumerated.**  
The username is one part of logon credentials that could be used to gain access to a system. Preventing the enumeration of users limits this information to authorized personnel.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnumerateLocalUsers` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000130 (Vuln V-253379)</sub>  

**Microsoft consumer experiences must be turned off.**  
Microsoft consumer experiences provides suggestions and notifications to users, which may include the installation of Windows Store apps. Organizations may control the execution of applications through other means such as allowlisting. Turning off Microsoft consumer experiences will help prevent the…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CloudContent\DisableWindowsConsumerFeatures` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000197 (Vuln V-253390)</sub>  

**NTLM must be prevented from falling back to a Null session.**  
NTLM sessions that are allowed to fall back to Null (unauthenticated) sessions may gain unauthorized access.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\LSA\MSV1_0\allownullsessionfallback` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000180 (Vuln V-253458)</sub>  

**Outgoing secure channel traffic must be encrypted or signed.**  
Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but not all information is encrypted. If this policy is enabled, outgoing secure channel traffic will be encrypted and signed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\RequireSignOrSeal` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000035 (Vuln V-253438)</sub>  

**Outgoing secure channel traffic must be encrypted.**  
Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but not all information is encrypted. If this policy is enabled, outgoing secure channel traffic will be encrypted.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\SealSecureChannel` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000040 (Vuln V-253439)</sub>  

**Outgoing secure channel traffic must be signed.**  
Requests sent on the secure channel are authenticated, and sensitive information (such as passwords) is encrypted, but the channel is not integrity checked. If this policy is enabled, outgoing secure channel traffic will be signed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\SignSecureChannel` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000045 (Vuln V-253440)</sub>  

**Passwords must not be saved in the Remote Desktop Client.**  
Saving passwords in the Remote Desktop Client could allow an unauthorized user to establish a remote desktop session to another system. The system must be configured to prevent users from saving passwords in the Remote Desktop Client.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\DisablePasswordSaving` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000270 (Vuln V-253402)</sub>  

**PKU2U authentication using online identities must be prevented.**  
PKU2U is a peer-to-peer authentication protocol. This setting prevents online identities from authenticating to domain-joined systems. Authentication will be centrally managed with Windows user accounts.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\LSA\pku2u\AllowOnlineID` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000185 (Vuln V-253459)</sub>  

**PowerShell script block logging must be enabled on Windows 11.**  
Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000326 (Vuln V-253414)</sub>  

**PowerShell Transcription must be enabled on Windows 11.**  
Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000327 (Vuln V-253415)</sub>  

**Printing over HTTP must be prevented.**  
Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting prevents…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Printers\DisableHTTPPrinting` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000110 (Vuln V-253376)</sub>  

**Remote calls to the Security Account Manager (SAM) must be restricted to Administrators.**  
The Windows SAM stores users' passwords. Restricting remote rpc connections to the SAM to Administrators helps protect those credentials.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RestrictRemoteSAM` = `O:BAG:BAD:(A;;RC;;;BA)` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000167 (Vuln V-253457)</sub>  

**Remote Desktop Services must always prompt a client for passwords upon connection.**  
This setting controls the ability of users to supply passwords automatically as part of their remote desktop connection. Disabling this setting would allow anyone to use the stored credentials in a connection item to connect to the terminal server.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fPromptForPassword` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000280 (Vuln V-253404)</sub>  

**Remote Desktop Services must be configured with the client connection encryption set to the required level.**  
Remote connections must be encrypted to prevent interception of data or sensitive information. Selecting "High Level" will ensure encryption of Remote Desktop Services sessions in both directions.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\MinEncryptionLevel` = `3` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000290 (Vuln V-253406)</sub>  

**Run as different user must be removed from context menus.**  
The "Run as different user" selection from context menus allows the use of credentials other than the currently logged on user. Using privileged credentials in a standard user session can expose those credentials to theft. Removing this option from context menus helps prevent this from occurring.  
Sets `HKLM\SOFTWARE\Classes\exefile\shell\runasuser\SuppressionPolicy` = `4096` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000039 (Vuln V-253359.c)</sub>  

**Run as different user must be removed from context menus.**  
The "Run as different user" selection from context menus allows the use of credentials other than the currently logged on user. Using privileged credentials in a standard user session can expose those credentials to theft. Removing this option from context menus helps prevent this from occurring.  
Sets `HKLM\SOFTWARE\Classes\batfile\shell\runasuser\SuppressionPolicy` = `4096` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000039 (Vuln V-253359.a)</sub>  

**Run as different user must be removed from context menus.**  
The "Run as different user" selection from context menus allows the use of credentials other than the currently logged on user. Using privileged credentials in a standard user session can expose those credentials to theft. Removing this option from context menus helps prevent this from occurring.  
Sets `HKLM\SOFTWARE\Classes\mscfile\shell\runasuser\SuppressionPolicy` = `4096` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000039 (Vuln V-253359.d)</sub>  

**Run as different user must be removed from context menus.**  
The "Run as different user" selection from context menus allows the use of credentials other than the currently logged on user. Using privileged credentials in a standard user session can expose those credentials to theft. Removing this option from context menus helps prevent this from occurring.  
Sets `HKLM\SOFTWARE\Classes\cmdfile\shell\runasuser\SuppressionPolicy` = `4096` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000039 (Vuln V-253359.b)</sub>  

**Solicited Remote Assistance must not be allowed.**  
Remote assistance allows another user to view or take control of the local session of a user. Solicited assistance is help that is specifically requested by the local user. This may allow unauthorized parties access to the resources on the computer.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fAllowToGetHelp` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000155 (Vuln V-253382)</sub>  

**Structured Exception Handling Overwrite Protection (SEHOP) must be enabled.**  
Attackers are constantly looking for vulnerabilities in systems and applications. Structured Exception Handling Overwrite Protection (SEHOP) blocks exploits that use the Structured Exception Handling overwrite technique, a common buffer overflow attack.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel\DisableExceptionChainValidation` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-00-000150 (Vuln V-253284)</sub>  

**The Application Compatibility Program Inventory must be prevented from collecting data and sending the informa…**  
Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting will pre…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AppCompat\DisableInventory` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000175 (Vuln V-253385)</sub>  

**The computer account password must not be prevented from being reset.**  
Computer account passwords are changed automatically on a regular basis. Disabling automatic password changes can make the system more vulnerable to malicious access. Frequent password changes can be a significant safeguard for the system. A new password for the computer account will be generated ev…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\DisablePasswordChange` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000050 (Vuln V-253441)</sub>  

**The convenience PIN for Windows 11 must be disabled.**  
This policy controls whether a domain user can sign in using a convenience PIN to prevent enabling (Password Stuffer).  
Sets `HKLM\Software\Policies\Microsoft\Windows\System\AllowDomainPINLogon` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000370 (Vuln V-253423)</sub>  

**The default autorun behavior must be configured to prevent autorun commands.**  
Allowing autorun commands to execute may introduce malicious code to a system. Configuring this setting prevents autorun commands from executing.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoAutorun` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000185 (Vuln V-253387)</sub>  

**The default permissions of global system objects must be increased.**  
Windows systems maintain a global list of shared system resources such as DOS device names, mutexes, and semaphores. Each type of object is created with a default DACL that specifies who can access the objects with what permissions. If this policy is enabled, the default DACL is stronger, allowing n…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\ProtectionMode` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000240 (Vuln V-253467)</sub>  

**The display of slide shows on the lock screen must be disabled.**  
Slide shows that are displayed on the lock screen could display sensitive information to unauthorized personnel. Turning off this feature will limit access to the information to a logged on user.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Personalization\NoLockScreenSlideshow` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000010 (Vuln V-253352)</sub>  

**The LanMan authentication level must be set to send NTLMv2 response only, and to refuse LM and NTLM.**  
The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000205 (Vuln V-253462)</sub>  

**The Microsoft Defender SmartScreen for Explorer must be enabled.**  
Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnableSmartScreen` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000210 (Vuln V-253395.b)</sub>  

**The Microsoft Defender SmartScreen for Explorer must be enabled.**  
Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\ShellSmartScreenLevel` = `Block` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000210 (Vuln V-253395.a)</sub>  

**The network selection user interface (UI) must not be displayed on the logon screen.**  
Enabling interaction with the network selection UI allows users to change connections to available networks without signing into Windows.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\DontDisplayNetworkSelectionUI` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000120 (Vuln V-253378)</sub>  

**The Remote Desktop Session Host must require secure RPC communications.**  
Allowing unsecure RPC communication exposes the system to man in the middle attacks and data disclosure attacks. A man in the middle attack occurs when an intruder captures packets between a client and server and modifies them before allowing the packets to be exchanged. Usually the attacker will mo…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Terminal Services\fEncryptRPCTraffic` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000285 (Vuln V-253405)</sub>  

**The Server Message Block (SMB) v1 protocol must be disabled on the SMB client.**  
SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\mrxsmb10\Start` = `4` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-00-000170 (Vuln V-253288)</sub>  

**The Server Message Block (SMB) v1 protocol must be disabled on the SMB server.**  
SMBv1 is a legacy protocol that uses the MD5 algorithm as part of SMB. MD5 is known to be vulnerable to a number of attacks such as collision and preimage attacks as well as not being FIPS compliant. Disabling SMBv1 support may prevent access to file or print sharing resources with systems or device…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanServer\Parameters\SMB1` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-00-000165 (Vuln V-253287)</sub>  

**The setting to allow Microsoft accounts to be optional for modern style apps must be enabled.**  
Control of credentials and the system must be maintained within the enterprise. Enabling this setting allows enterprise credentials to be used with modern style apps that support this, instead of Microsoft accounts.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\MSAOptional` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000170 (Vuln V-253384)</sub>  

**The system must be configured to ignore NetBIOS name release requests except from WINS servers.**  
Configuring the system to ignore name release requests, except from WINS servers, prevents a denial of service (DoS) attack. The DoS consists of sending a NetBIOS name release request to the server for each entry in the server's cache, causing a response delay in the normal operation of the servers…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netbt\Parameters\NoNameReleaseOnDemand` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000035 (Vuln V-253356)</sub>  

**The system must be configured to meet the minimum session security requirement for NTLM SSP based clients.**  
Microsoft has implemented a variety of security support providers for use with RPC sessions. All of the options must be enabled to ensure the maximum security level.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\MSV1_0\NTLMMinClientSec` = `537395200` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000215 (Vuln V-253464)</sub>  

**The system must be configured to meet the minimum session security requirement for NTLM SSP based servers.**  
Microsoft has implemented a variety of security support providers for use with RPC sessions. All of the options must be enabled to ensure the maximum security level.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\MSV1_0\NTLMMinServerSec` = `537395200` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000220 (Vuln V-253465)</sub>  

**The system must be configured to prevent anonymous users from having the same rights as the Everyone group.**  
Access by anonymous users must be restricted. If this setting is enabled, then anonymous users have the same rights and permissions as the built-in Everyone group. Anonymous users must not have these permissions or rights.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\EveryoneIncludesAnonymous` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000160 (Vuln V-253455)</sub>  

**The system must be configured to prevent Internet Control Message Protocol (ICMP) redirects from overriding Op…**  
Allowing ICMP redirect of routes can lead to traffic not being routed properly. When disabled, this forces ICMP to be routed via shortest path first.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\EnableICMPRedirect` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000030 (Vuln V-253355)</sub>  

**The system must be configured to prevent IP source routing.**  
Configuring the system to disable IP source routing protects against spoofing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Tcpip\Parameters\DisableIPSourceRouting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000025 (Vuln V-253354)</sub>  

**The system must be configured to prevent the storage of the LAN Manager hash of passwords.**  
The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000195 (Vuln V-253461)</sub>  

**The system must be configured to require a strong session key.**  
A computer connecting to a domain controller will establish a secure channel. Requiring strong session keys enforces 128-bit encryption between systems.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\Netlogon\Parameters\RequireStrongKey` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000060 (Vuln V-253443)</sub>  

**The system must be configured to the required LDAP client signing level.**  
This setting controls the signing requirements for LDAP clients. This setting must be set to Negotiate signing or Require signing, depending on the environment and type of LDAP server in use.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LDAP\LDAPClientIntegrity` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000210 (Vuln V-253463)</sub>  

**The system must be configured to use FIPS-compliant algorithms for encryption, hashing, and signing.**  
This setting ensures that the system uses algorithms that are FIPS-compliant for encryption, hashing, and signing. FIPS-compliant algorithms meet specific standards established by the U.S. Government and must be the algorithms used for all OS encryption functions.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\FIPSAlgorithmPolicy\Enabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-SO-000230 (Vuln V-253466)</sub>  

**The use of a hardware security device with Windows Hello for Business must be enabled.**  
The use of a Trusted Platform Module (TPM) to store keys for Windows Hello for Business provides additional security. Keys stored in the TPM may only be used on that system while keys stored using software are more susceptible to compromise and could be used on other systems.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\PassportForWork\RequireSecurityDevice` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000255 (Vuln V-253400)</sub>  

**The user must be prompted for a password on resume from sleep (plugged in).**  
Authentication must always be required when accessing a system. This setting ensures the user is prompted for a password on resume from sleep (plugged in).  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Power\PowerSettings\0e796bdb-100d-47d6-a2d5-f7d2daa51f51\ACSettingIndex` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000150 (Vuln V-253381)</sub>  

**The Windows Installer feature "Always install with elevated privileges" must be disabled.**  
Standard user accounts must not be granted elevated privileges. Enabling Windows Installer to elevate privileges when installing applications can allow malicious persons and applications to gain full control of a system.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\AlwaysInstallElevated` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000315 (Vuln V-253411)</sub>  

**The Windows Remote Management (WinRM) client must not allow unencrypted traffic.**  
Unencrypted remote access to a system can allow sensitive information to be compromised. Windows remote management connections must be encrypted to prevent this.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowUnencryptedTraffic` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000335 (Vuln V-253417)</sub>  

**The Windows Remote Management (WinRM) client must not use Basic authentication.**  
Basic authentication uses plain text passwords that could be used to compromise a system.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowBasic` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000330 (Vuln V-253416)</sub>  

**The Windows Remote Management (WinRM) client must not use Digest authentication.**  
Digest authentication is not as strong as other options and may be subject to man-in-the-middle attacks.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Client\AllowDigest` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000360 (Vuln V-253421)</sub>  

**The Windows Remote Management (WinRM) service must not allow unencrypted traffic.**  
Unencrypted remote access to a system can allow sensitive information to be compromised. Windows remote management connections must be encrypted to prevent this.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\AllowUnencryptedTraffic` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000350 (Vuln V-253419)</sub>  

**The Windows Remote Management (WinRM) service must not store RunAs credentials.**  
Storage of administrative credentials could allow unauthorized access. Disallowing the storage of RunAs credentials for Windows Remote Management will prevent them from being used with plug-ins.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\DisableRunAs` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000355 (Vuln V-253420)</sub>  

**The Windows Remote Management (WinRM) service must not use Basic authentication.**  
Basic authentication uses plain text passwords that could be used to compromise a system.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\WinRM\Service\AllowBasic` = `0` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000345 (Vuln V-253418)</sub>  

**The Windows SMB client must be configured to always perform SMB packet signing.**  
The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB client will only communicate with an SMB server that performs SMB packet signing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000100 (Vuln V-253449)</sub>  

**The Windows SMB server must be configured to always perform SMB packet signing.**  
The server message block (SMB) protocol provides the basis for many network operations. Digitally signed SMB packets aid in preventing man-in-the-middle attacks. If this policy is enabled, the SMB server will only communicate with an SMB client that performs SMB packet signing.  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanManServer\Parameters\RequireSecuritySignature` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000120 (Vuln V-253451)</sub>  

**Toast notifications to the lock screen must be turned off.**  
Toast notifications that are displayed on the lock screen could display sensitive information to unauthorized personnel. Turning off this feature will limit access to the information to a logged on user.  
Sets `HKCU\SOFTWARE\Policies\Microsoft\Windows\CurrentVersion\PushNotifications\NoToastApplicationNotificationOnLockScreen` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-UC-000015 (Vuln V-253477)</sub>  

**Unauthenticated RPC clients must be restricted from connecting to the RPC server.**  
Configuring RPC to restrict unauthenticated RPC clients from connecting to the RPC server will prevent anonymous connections.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows NT\Rpc\RestrictRemoteClients` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000165 (Vuln V-253383)</sub>  

**Unencrypted passwords must not be sent to third-party SMB Servers.**  
Some non-Microsoft SMB servers only support unencrypted (plain text) password authentication. Sending plain text passwords across the network, when authenticating to an SMB server, reduces the overall security of the environment. Check with the vendor of the SMB server to see if there is a way to su…  
Sets `HKLM\SYSTEM\CurrentControlSet\Services\LanmanWorkstation\Parameters\EnablePlainTextPassword` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-SO-000110 (Vuln V-253450)</sub>  

**User Account Control approval mode for the built-in Administrator must be enabled.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the built-in Administrator account so that it runs in Admin Approval Mode.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\FilterAdministratorToken` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000245 (Vuln V-253468)</sub>  

**User Account Control must automatically deny elevation requests for standard users.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. Denying elevation requests from standard user accounts requires tasks that need elevation to be initiated by accounts with administrative privileges. Thi…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorUser` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000255 (Vuln V-253471)</sub>  

**User Account Control must be configured to detect application installations and prompt for elevation.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting requires Windows to respond to application installation requests by prompting for credentials.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableInstallerDetection` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000260 (Vuln V-253472)</sub>  

**User Account Control must only elevate UIAccess applications that are installed in secure locations.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures Windows to only allow applications installed in a secure location on the file system, such as the Program Files or the Windows\Sy…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableSecureUIAPaths` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000265 (Vuln V-253473)</sub>  

**User Account Control must prompt administrators for consent on the secure desktop.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the elevation requirements for logged on administrators to complete a task that requires raised privileges.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorAdmin` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000250 (Vuln V-253469)</sub>  

**User Account Control must run all administrators in Admin Approval Mode, enabling UAC.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting enables UAC.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableLUA` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000270 (Vuln V-253474)</sub>  

**User Account Control must virtualize file and registry write failures to per-user locations.**  
User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures non-UAC compliant applications to run in virtualized file and registry entries in per-user locations, allowing them to run.  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableVirtualization` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-SO-000275 (Vuln V-253475)</sub>  

**Users must be prevented from changing installation options.**  
Installation options for applications are typically controlled by administrators. This setting prevents users from changing installation options that may bypass security features.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\EnableUserControl` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000310 (Vuln V-253410)</sub>  

**Users must be prompted for a password on resume from sleep (on battery).**  
Authentication must always be required when accessing a system. This setting ensures the user is prompted for a password on resume from sleep (on battery).  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Power\PowerSettings\0e796bdb-100d-47d6-a2d5-f7d2daa51f51\DCSettingIndex` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000145 (Vuln V-253380)</sub>  

**Virtualization-based Security must be enabled on Windows 11 with the platform security level configured to Sec…**  
Virtualization-based Security (VBS) provides the platform for the additional security features, Credential Guard and virtualization-based protection of code integrity. Secure Boot is the minimum security level with DMA protection providing additional memory protection. DMA Protection requires a CPU…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DeviceGuard\EnableVirtualizationBasedSecurity` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000070 (Vuln V-253369.a)</sub>  

**WDigest Authentication must be disabled.**  
When the WDigest Authentication protocol is enabled, plain text passwords are stored in the Local Security Authority Subsystem Service (LSASS) exposing them to theft. WDigest is disabled by default in Windows 11. This setting ensures this is enforced.  
Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\Wdigest\UseLogonCredential` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000038 (Vuln V-253358)</sub>  

**Web publishing and online ordering wizards must be prevented from downloading a list of providers.**  
Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Turning off this capability will prevent potentially sensitive information from being sent outside the enterprise and uncontrolled updates to the system. This setting prevents…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoWebServices` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000105 (Vuln V-253375)</sub>  

**Wi-Fi Sense must be disabled.**  
Wi-Fi Sense automatically connects the system to known hotspots and networks that contacts have shared. It also allows the sharing of the system's known networks to contacts. Automatically connecting to hotspots and shared networks can expose a system to unsecured or potentially malicious systems.  
Sets `HKLM\SOFTWARE\Microsoft\WcmSvc\wifinetworkmanager\config\AutoConnectAllowedOEM` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000065 (Vuln V-253366)</sub>  

**Windows 11 Kernel (Direct Memory Access) DMA Protection must be enabled.**  
Kernel DMA Protection to protect PCs against drive-by Direct Memory Access (DMA) attacks using PCI hot plug devices connected to Thunderbolt 3 ports. Drive-by DMA attacks can lead to disclosure of sensitive information residing on a PC, or even injection of malware that allows attackers to bypass th…  
Sets `HKLM\Software\Policies\Microsoft\Windows\Kernel DMA Protection\DeviceEnumerationPolicy` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-EP-000310 (Vuln V-253426)</sub>  

**Windows 11 must be configured to disable Windows Game Recording and Broadcasting.**  
Windows Game Recording and Broadcasting is intended for use with games; however, it could potentially record screen shots of other applications and expose sensitive data. Disabling the feature will prevent this from occurring.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\GameDVR\AllowGameDVR` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000252 (Vuln V-253399)</sub>  

**Windows 11 must be configured to enable Remote host allows delegation of non-exportable credentials.**  
An exportable version of credentials is provided to remote hosts when using credential delegation which exposes them to theft on the remote host. Restricted Admin mode or Remote Credential Guard allow delegation of non-exportable credentials providing additional protection of the credentials. Enabli…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CredentialsDelegation\AllowProtectedCreds` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000068 (Vuln V-253368)</sub>  

**Windows 11 must be configured to prevent users from receiving suggestions for third-party or additional applic…**  
Windows spotlight features may suggest apps and content from third-party software publishers in addition to Microsoft apps and content.  
Sets `HKCU\SOFTWARE\Policies\Microsoft\Windows\CloudContent\DisableThirdPartySuggestions` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000390 (Vuln V-253425)</sub>  

**Windows 11 must be configured to prevent Windows apps from being activated by voice while the system is locked…**  
Allowing Windows apps to be activated by voice from the lock screen could allow for unauthorized use. Requiring logon will ensure the apps are only used by authorized personnel.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy\LetAppsActivateWithVoice` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000365 (Vuln V-253422.b)</sub>  

**Windows 11 must be configured to prevent Windows apps from being activated by voice while the system is locked…**  
Allowing Windows apps to be activated by voice from the lock screen could allow for unauthorized use. Requiring logon will ensure the apps are only used by authorized personnel.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\AppPrivacy\LetAppsActivateWithVoiceAboveLock` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000365 (Vuln V-253422.a)</sub>  

**Windows 11 must be configured to prioritize ECC Curves with longer key lengths first.**  
Use of weak or untested encryption algorithms undermines the purposes of utilizing encryption to protect data. By default Windows uses ECC curves with shorter key lengths first. Requiring ECC curves with longer key lengths to be prioritized first helps ensure more secure algorithms are used.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Cryptography\Configuration\SSL\00010002\EccCurves` = `NistP384;NistP256` (REG_MULTI_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: WN11-CC-000052 (Vuln V-253363)</sub>  

**Windows 11 must cover or disable the built-in or attached camera when not in use.**  
It is detrimental for operating systems to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may remain unsecured. They increase the risk to the platform by providing additional at…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam\Value` = `Deny` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000007 (Vuln V-253351)</sub>  

**Windows 11 systems must block consumer account user authentication.**  
It is detrimental for operating systems to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore, may remain unsecured. They increase the risk to the platform by providing additional a…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\MicrosoftAccount\DisableUserAuth` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-00-000126 (Vuln V-279688)</sub>  

**Windows 11 systems must use a BitLocker PIN for pre-boot authentication.**  
If data at rest is unencrypted, it is vulnerable to disclosure. Even if the operating system enforces permissions on data access, an adversary can remove non-volatile memory and read it directly, thereby circumventing operating system controls. Encrypting the data ensures that confidentiality is pro…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\FVE\UseTPMPIN` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-00-000031 (Vuln V-253260.b)</sub>  

**Windows 11 systems must use a BitLocker PIN for pre-boot authentication.**  
If data at rest is unencrypted, it is vulnerable to disclosure. Even if the operating system enforces permissions on data access, an adversary can remove non-volatile memory and read it directly, thereby circumventing operating system controls. Encrypting the data ensures that confidentiality is pro…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\FVE\UseAdvancedStartup` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-00-000031 (Vuln V-253260.a)</sub>  

**Windows Ink Workspace must be configured to disallow access above the lock.**  
This action secures Windows Ink, which contains applications and features oriented toward pen computing.  
Sets `HKLM\Software\Policies\Microsoft\WindowsInkWorkspace\AllowWindowsInkWorkspace` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000385 (Vuln V-253424)</sub>  

**Windows Telemetry must not be configured to Full.**  
Some features may communicate with the vendor, sending system information or downloading data or components for the feature. Limiting this capability will prevent potentially sensitive information from being sent outside the enterprise. The "Security" option for Telemetry configures the lowest amoun…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DataCollection\AllowTelemetry` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000205 (Vuln V-253393)</sub>  

**Windows Update must not obtain updates from other PCs on the internet.**  
Windows 11 allows Windows Update to obtain updates from additional sources instead of Microsoft. In addition to Microsoft, updates can be obtained from and sent to PCs on the local network as well as on the Internet. This is part of the Windows Update trusted process, however to minimize outside exp…  
Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\DeliveryOptimization\Config\DODownloadMode` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000206 (Vuln V-253394.b)</sub>  

**Windows Update must not obtain updates from other PCs on the internet.**  
Windows 11 allows Windows Update to obtain updates from additional sources instead of Microsoft. In addition to Microsoft, updates can be obtained from and sent to PCs on the local network as well as on the Internet. This is part of the Windows Update trusted process, however to minimize outside exp…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\DeliveryOptimization\DODownloadMode` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: WN11-CC-000206 (Vuln V-253394.a)</sub>  

## DISA STIG — Microsoft Edge (V2R5)

_The full DISA Microsoft Edge STIG. Many of these are strict lockdowns (disabling sync, InPrivate, imports, autofill) that go beyond exploitation prevention and add day-to-day friction — which is why only the exploitation-relevant ones appear in the Recommended profile._

**52 settings in this section.**

**A website's ability to query for payment methods must be disabled.**  
This setting determines whether websites can check if the user has payment methods saved. If this policy is disabled, websites that use "PaymentRequest.canMakePayment" or "PaymentRequest.hasEnrolledInstrument" API will be informed that no payment methods are available. If this policy is enabled or i…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PaymentMethodQueryEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000055 (Vuln V-235767)</sub>  

**Access to Microsoft 365 Copilot writing assistance must be disabled.**  
This policy controls whether users can use writing support features in Microsoft Edge for Business, such as Rewrite, which utilizes Microsoft 365 Copilot Chat.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ComposeInlineEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000069 (Vuln V-279940)</sub>  

**Autofill for addresses must be disabled.**  
Enables the AutoFill feature and allows users to auto-complete address information in web forms using previously stored information. If this policy is disabled, AutoFill never suggests or fills credit card information, nor will it save additional credit card information that users might submit while…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AutofillAddressEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000029 (Vuln V-235746)</sub>  

**Autofill for Credit Cards must be disabled.**  
Enables the Microsoft Edge AutoFill feature and lets users auto complete credit card information in web forms using previously stored information. If this policy is disabled, AutoFill never suggests or fills credit card information, nor will it save additional credit card information that users migh…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AutofillCreditCardEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000028 (Vuln V-235745)</sub>  

**AutoplayAllowed must be set to disabled.**  
This policy sets the media autoplay policy for websites. The default setting "Not configured" respects the current media autoplay settings and lets users configure their autoplay settings. Setting to "Enabled" sets media autoplay to "Allow". All websites are allowed to autoplay media. Users cannot o…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AutoplayAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000024 (Vuln V-235741)</sub>  

**Background processing must be disabled.**  
Background processing allows Microsoft Edge processes to start at OS sign-in and keep running after the last browser window is closed. In this scenario, background apps and the current browsing session remain active, including any session cookies. An open background process displays an icon in the s…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\BackgroundModeEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000006 (Vuln V-235724)</sub>  

**Browser history must be saved.**  
This setting disables deleting browser history and download history and prevents users from changing this setting.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AllowDeletingBrowserHistory` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: EDGE-00-000033 (Vuln V-235750)</sub>  

**Bypassing Microsoft Defender SmartScreen prompts for sites must be disabled.**  
This policy setting allows a decision to be made on whether users can override the Microsoft Defender SmartScreen warnings about potentially malicious websites. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are blocked from continuing to the site. If th…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PreventSmartScreenPromptOverride` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: EDGE-00-000002 (Vuln V-235720)</sub>  

**Bypassing of Microsoft Defender SmartScreen warnings about downloads must be disabled.**  
This policy setting allows a decision to be made on whether users can override Microsoft Defender SmartScreen warnings about unverified downloads. If this setting is enabled, users cannot ignore Microsoft Defender SmartScreen warnings, and are prevented from completing the unverified downloads. If t…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PreventSmartScreenPromptOverrideForFiles` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: EDGE-00-000003 (Vuln V-235721)</sub>  

**Copilot must be disabled.**  
The Sidebar is a launcher bar on the right side of Microsoft Edge's screen. If this policy is enabled or not configured, the Sidebar will be shown. If this policy is disabled, the Sidebar will never be shown. Disabling Sidebar will disable Copilot.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\HubsSidebarEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000066 (Vuln V-260466)</sub>  

**Data Synchronization must be disabled.**  
Disables data synchronization in Microsoft Edge. This policy also prevents the sync consent prompt from appearing. If this policy is not set or applied as recommended, users will be able to turn sync on or off. If this policy is applied as mandatory, users will not be able to turn on sync.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SyncDisabled` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000010 (Vuln V-235727)</sub>  

**Edge development tools must be disabled.**  
While the risk associated with browser development tools is more related to the proper design of a web application, a risk vector remains within the browser. The developer tools allow end users and application developers to view and edit all types of web application-related data via the browser. Pag…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DeveloperToolsAvailability` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000034 (Vuln V-235751)</sub>  

**Extensions installation must be blocklisted by default.**  
List specific extensions that users cannot install in Microsoft Edge. When this policy is deployed, any extensions on this list that were previously installed will be disabled, and the user will not be able to enable them. If an item is removed from the list of blocked extensions, the extension is a…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ExtensionInstallBlocklist\1` = `*` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000041 (Vuln V-235754)</sub>  

**FriendlyURLs must be disabled.**  
If FriendlyURLs are enabled, Microsoft Edge will compute additional representations of the URL and place them on the clipboard. This policy configures what format will be pasted when the user pastes in external applications, or inside Microsoft Edge without the "Paste As" context menu item. If confi…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ConfigureFriendlyURLFormat` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000068 (Vuln V-266981)</sub>  

**Google Cast must be disabled.**  
Enable this policy to enable Google Cast. Users will be able to launch it from the app menu, page context menus, media controls on Cast-enabled websites, and (if shown) the Cast toolbar icon. Disable this policy to disable Google Cast. By default, Google Cast is enabled.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\EnableMediaRouter` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000026 (Vuln V-235743)</sub>  

**Guest mode must be disabled.**  
Enabling Guest mode allows the use of guest profiles in Microsoft Edge. In a guest profile, the browser does not import browsing data from existing profiles, and it deletes browsing data when all guest profiles are closed. If this policy is enabled or not configured, Microsoft Edge lets users browse…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\BrowserGuestModeEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000060 (Vuln V-235772)</sub>  

**Importing of autofill form data must be disabled.**  
Allows users to import autofill form data from another browser into Microsoft Edge. If this policy is enabled, the option to manually import autofill data is automatically selected. If this policy is disabled, autofill form data is not imported at first run, and users cannot import it manually. If t…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportAutofillFormData` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000013 (Vuln V-235730)</sub>  

**Importing of browser settings must be disabled.**  
Allows users to import browser settings from another browser into Microsoft Edge. If this policy is enabled, the Browser settings check box is automatically selected in the Import browser data dialog box. If this policy is disabled, browser settings are not imported at first run, and users cannot im…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportBrowserSettings` = `0` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000014 (Vuln V-235731)</sub>  

**Importing of browsing history must be disabled.**  
Allows users to import their browsing history from another browser into Microsoft Edge. If this policy is enabled, the Browsing history check box is automatically selected in the Import browser data dialog box. If this policy is disabled, browsing history data is not imported at first run, and users…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportHistory` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000017 (Vuln V-235734)</sub>  

**Importing of cookies must be disabled.**  
Allows users to import cookies from another browser into Microsoft Edge. If this policy is disabled, cookies are not imported on first run. If this policy is not configured, cookies are imported on first run.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportCookies` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000015 (Vuln V-235732)</sub>  

**Importing of extensions must be disabled.**  
Allows users to import extensions from another browser into Microsoft Edge. If this policy is enabled, the Extensions check box is automatically selected in the Import browser data dialog box. If this policy is disabled, extensions are not imported at first run, and users cannot import them manually…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportExtensions` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000016 (Vuln V-235733)</sub>  

**Importing of home page settings must be disabled.**  
Allows users to import their home page setting from another browser into Microsoft Edge. If this policy is enabled, the option to manually import the home page setting is automatically selected. If this policy is disabled, the home page setting is not imported at first run, and users cannot import i…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportHomepage` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000018 (Vuln V-235735)</sub>  

**Importing of open tabs must be disabled.**  
Allows users to import open and pinned tabs from another browser into Microsoft Edge. If this policy is enabled, the Open tabs check box is automatically selected in the Import browser data dialog box. If this policy is disabled, open tabs are not imported at first run, and users cannot import them…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportOpenTabs` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000019 (Vuln V-235736)</sub>  

**Importing of payment info must be disabled.**  
Allows users to import payment info from another browser into Microsoft Edge. If this policy is enabled, the payment info check box is automatically selected in the Import browser data dialog box. If this policy is disabled, payment info is not imported at first run, and users cannot import it manua…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportPaymentInfo` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000020 (Vuln V-235737)</sub>  

**Importing of saved passwords must be disabled.**  
Allows users to import saved passwords from another browser into Microsoft Edge. If this policy is enabled, the option to manually import saved passwords is automatically selected. If this policy is disabled, saved passwords are not imported on first run, and users cannot import them manually.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportSavedPasswords` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000021 (Vuln V-235738)</sub>  

**Importing of search engine settings must be disabled.**  
Allows users to import search engine settings from another browser into Microsoft Edge. If this policy is enabled, the option to import search engine settings is automatically selected. If this policy is disabled, search engine settings are not imported at first run, and users cannot import them man…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportSearchEngine` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000022 (Vuln V-235739)</sub>  

**Importing of shortcuts must be disabled.**  
Allows users to import Shortcuts from another browser into Microsoft Edge. If this policy is disabled, Shortcuts are not imported on first run. If this policy is not configured, Shortcuts are imported on first run.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ImportShortcuts` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000023 (Vuln V-235740)</sub>  

**InPrivate mode must be disabled.**  
This setting specifies whether the user can open pages in InPrivate mode in Microsoft Edge. If this policy is not configured or set it to "Enabled", users can open pages in InPrivate mode. Set this policy to "Disabled" to stop users from using InPrivate mode. Set this policy to "Forced" to always us…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\InPrivateModeAvailability` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000005 (Vuln V-235723)</sub>  

**Microsoft Defender SmartScreen must be configured to block potentially unwanted apps.**  
This policy setting configures blocking for potentially unwanted apps with Microsoft Defender SmartScreen. Potentially unwanted app blocking with Microsoft Defender SmartScreen provides warning messages to help protect users from adware, coin miners, bundleware, and other low-reputation apps that ar…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SmartScreenPuaEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: EDGE-00-000051 (Vuln V-235764)</sub>  

**Microsoft Defender SmartScreen must be enabled.**  
This policy setting configures Microsoft Defender SmartScreen, which provides warning messages to help protect users from potential phishing scams and malicious software. By default, Microsoft Defender SmartScreen is turned on. If this setting is enabled, Microsoft Defender SmartScreen is turned on.…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SmartScreenEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: EDGE-00-000050 (Vuln V-235763)</sub>  

**Network prediction must be disabled.**  
Enables network prediction and prevents users from changing this setting. This controls DNS prefetching, TCP and SSL pre-connection, and pre-rendering of web pages. If this policy is not configured, network prediction is enabled but the user can change it. Policy options mapping: - NetworkPrediction…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\NetworkPredictionOptions` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000011 (Vuln V-235728)</sub>  

**Online revocation checks must be performed.**  
If you enable this policy, Microsoft Edge will perform soft-fail, online OCSP/CRL checks. "Soft fail" means that if the revocation server can't be reached, the certificate will be considered valid. If you disable the policy or don't configure it, Microsoft Edge won't perform online revocation checks…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\EnableOnlineRevocationChecks` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: EDGE-00-000030 (Vuln V-235747)</sub>  

**Personalization of ads, search, and news by sending browsing history to Microsoft must be disabled.**  
This policy prevents Microsoft from collecting a user's Microsoft Edge browsing history to be used for personalizing advertising, search, news and other Microsoft services. This setting is only available for users with a Microsoft account. This setting is not available for child accounts or enterpri…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PersonalizationReportingEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000031 (Vuln V-235748)</sub>  

**Relaunch notification must be required.**  
Users must be required to restart the browser to finish installation of pending updates and prevent users from continually using an old/vulnerable browser version.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\RelaunchNotification` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000061 (Vuln V-235773)</sub>  

**Search suggestions must be disabled.**  
Enables web search suggestions in the Microsoft Edge Address Bar and Auto-Suggest List, and prevents users from changing this policy. If this policy is enabled, web search suggestions are used. If this policy is disabled, web search suggestions are never used; however, local history and local favori…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SearchSuggestEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000012 (Vuln V-235729)</sub>  

**Session only-based cookies must be enabled.**  
Cookies must only be allowed per session and only for approved URLs as permanently stored cookies can be used for malicious intent. Approved URLs may be allowlisted via the "CookiesAllowedForUrls" or "SaveCookiesOnExit" policy settings, but these are not requirements.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultCookieSetting` = `4` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000067 (Vuln V-260467)</sub>  

**Site isolation for every site must be enabled.**  
The "SitePerProcess" policy can be used to prevent users from opting out of the default behavior of isolating all sites. The "IsolateOrigins" policy can be used to isolate additional, finer-grained origins. Enabling this policy prevents users from opting out of the default behavior where each site r…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\SitePerProcess` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: EDGE-00-000047 (Vuln V-235760)</sub>  

**Site tracking of a user’s location must be disabled.**  
Set whether websites can track users' physical locations. Tracking can be allowed by default ("AllowGeolocation") or denied by default ("BlockGeolocation"), or the user can be asked each time a website requests their location ("AskGeolocation"). If this policy is not configured, "AskGeolocation" is…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultGeolocationSetting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000032 (Vuln V-235749)</sub>  

**Spell checking provided by Microsoft Editor must be disabled.**  
The Microsoft Editor service provides enhanced spell and grammar checking for editable text fields on web pages. If this policy is enabled or incorrectly configured, Microsoft Editor spell check can be used for eligible text fields. If you disable this policy, spell check can only be provided by loc…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\Computer Configuration/Administrative Templates/Microsoft Edge/Spell checking provided by Microsoft Editor must be set to Disabled. Use the Windows Registry Editor to navigate to the following key: HKLM\SOFTWARE\Policies\Microsoft\Edge If the value for MicrosoftEditorProofingEnabled is not set to REG_DWORD = 0, this is a finding.` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000070 (Vuln V-283439)</sub>  

**Suggestions of similar web pages in the event of a navigation error must be disabled.**  
This setting allows Microsoft Edge to issue a connection to a web service to generate URL and search suggestions for connectivity issues such as DNS errors. If this policy is enabled, a web service is used to generate URL and search suggestions for network errors. If this policy is disabled, no call…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AlternateErrorPagesEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000056 (Vuln V-235768)</sub>  

**Supported authentication schemes must be configured.**  
This setting specifies which HTTP authentication schemes are supported. The policy can be configured by using these values: "basic", "digest", "ntlm", and "negotiate". Separate multiple values with commas. If this policy is not configured, all four schemes are used.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\AuthSchemes` = `ntlm,negotiate` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000048 (Vuln V-235761)</sub>  

**The ability of sites to show pop-ups must be disabled.**  
Set whether websites can show pop-up windows. Pop-ups can be allowed on all websites ("AllowPopups") or blocked on all sites ("BlockPopups"). If this policy is configured, pop-up windows are blocked by default, and users can change this setting. Policy options mapping: - AllowPopups (1) = Allow all…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultPopupsSetting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000008 (Vuln V-235725)</sub>  

**The built-in DNS client must be disabled.**  
This setting controls whether to use the built-in DNS client. This does not affect which DNS servers are used; it only controls the software stack that is used to communicate with them. For example, if the operating system is configured to use an enterprise DNS server, that same server would be used…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\BuiltInDnsClientEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000062 (Vuln V-235774)</sub>  

**The collections feature must be disabled.**  
This setting allows users to access the Collections feature, where they can collect, organize, share, and export content more efficiently and with Office integration. If this policy is enabled or not configured, users can access and use the Collections feature in Microsoft Edge. If this policy is di…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\EdgeCollectionsEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000058 (Vuln V-235770)</sub>  

**The download location prompt must be configured.**  
This setting provides positive feedback before a download starts, limiting the possibility of inadvertent downloads without notifying the user.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PromptForDownloadLocation` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000052 (Vuln V-235765)</sub>  

**The Password Manager must be disabled.**  
Enable Microsoft Edge to save user passwords. If this policy is enabled, users can save their passwords in Microsoft Edge. The next time the user visits the site, Microsoft Edge will enter the password automatically.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\PasswordManagerEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000043 (Vuln V-235756)</sub>  

**The Share Experience feature must be disabled.**  
If this policy is set to "ShareAllowed" (the default), users will be able to access the Windows 10 Share experience from the Settings and More menu in Microsoft Edge to share with other apps on the system. If this policy is set to "ShareDisallowed", users will not be able to access the Windows 10 Sh…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\ConfigureShare` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000059 (Vuln V-235771)</sub>  

**Use of the QUIC protocol must be disabled.**  
QUIC is used by more than half of all connections from the Edge web browser to Google's servers, and this activity is undesirable in the DoD. If you enable this policy or don't configure it, the QUIC protocol is allowed. If you disable this policy, the QUIC protocol is blocked.  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\QuicAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000063 (Vuln V-246736)</sub>  

**User feedback must be disabled.**  
Microsoft Edge uses the Edge Feedback feature (enabled by default) to allow users to send feedback, suggestions, or customer surveys and to report any issues with the browser. By default, users cannot disable (turn off) the Edge Feedback feature. If this policy is enabled or not configured, users ca…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\UserFeedbackAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000057 (Vuln V-235769)</sub>  

**Visual Search must be disabled.**  
Visual Search allows for quick exploration of more related content about entities in an image. If this policy is enabled or not configured, Visual Search will be enabled via image hover, context menu, and search in Sidebar. If this policy is disabled, Visual Search will be disabled and more informat…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\VisualSearchEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000065 (Vuln V-260465)</sub>  

**Web Bluetooth API must be disabled.**  
Control whether websites can access nearby Bluetooth devices. Access can be blocked completely or the site required to ask the user each time it wants to access a Bluetooth device. If this policy is not configured, the default value ('AskWebBluetooth', meaning users are asked each time) is used and…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultWebBluetoothGuardSetting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000027 (Vuln V-235744)</sub>  

**WebUSB must be disabled.**  
Set whether websites can access connected USB devices. Access can be blocked completely or the user asked each time a website wants to get access to connected USB devices. Override this policy for specific URL patterns by using the WebUsbAskForUrls and WebUsbBlockedForUrls policies. If this policy i…  
Sets `HKLM\SOFTWARE\Policies\Microsoft\Edge\DefaultWebUsbGuardSetting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: EDGE-00-000025 (Vuln V-235742)</sub>  

## DISA STIG — Google Chrome (V2R11)

_The full DISA Google Chrome STIG, including strict policy lockdowns beyond the exploitation-prevention subset used by the Recommended profile._

**39 settings in this section.**

**AI-powered History Search must be disabled.**  
AI History Search is a feature that allows users to search their browsing history and receive generated answers based on page contents and not just the page title and URL. 0 = Allow the feature to be used, while allowing Google to use relevant data to improve its AI models. Relevant data may include…  
Sets `HKLM\Software\Policies\Google\Chrome\"\HistorySearchSettings` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0079 (Vuln V-275784)</sub>  

**Anonymized data collection must be disabled.**  
Enable URL-keyed anonymized data collection in Google Chrome and prevent users from changing this setting. URL-keyed anonymized data collection sends URLs of pages the user visits to Google to make searches and browsing better. If you enable this policy, URL-keyed anonymized data collection is alway…  
Sets `HKLM\Software\Policies\Google\Chrome\UrlKeyedAnonymizedDataCollectionEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0066 (Vuln V-221597)</sub>  

**AutoFill for addresses must be disabled.**  
Enabling Google Chrome's AutoFill feature allows users to auto complete address information in web forms using previously stored information. If this setting is disabled, Autofill will never suggest or fill address information, nor will it save additional address information that the user might subm…  
Sets `HKLM\Software\Policies\Google\Chrome\AutofillAddressEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0071 (Vuln V-226403)</sub>  

**AutoFill for credit cards must be disabled.**  
Enabling Google Chrome's AutoFill feature allows users to auto complete credit card information in web forms using previously stored information. If this setting is disabled, Autofill will never suggest or fill credit card information, nor will it save additional credit card information that the use…  
Sets `HKLM\Software\Policies\Google\Chrome\AutofillCreditCardEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0070 (Vuln V-226402)</sub>  

**Autoplay must be disabled.**  
This allows a user to control if videos can play automatically with audio content (without user consent) in Google Chrome. If the policy is set to "True", Google Chrome is allowed to autoplay media. If the policy is set to "False", Google Chrome is not allowed to autoplay media. The "AutoplayAllowli…  
Sets `HKLM\Software\Policies\Google\Chrome\AutoplayAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0064 (Vuln V-221595)</sub>  

**Background processing must be disabled.**  
Determines whether a Google Chrome process is started on OS login that keeps running when the last browser window is closed, allowing background apps to remain active. The background process displays an icon in the system tray and can always be closed from there. If this policy is set to True, backg…  
Sets `HKLM\Software\Policies\Google\Chrome\BackgroundModeEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0017 (Vuln V-221570)</sub>  

**Browser history must be saved.**  
This policy disables saving browser history in Google Chrome and prevents users from changing this setting. If this setting is enabled, browsing history is not saved. If this setting is disabled or not set, browsing history is saved.  
Sets `HKLM\Software\Policies\Google\Chrome\SavingBrowserHistoryDisabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0039 (Vuln V-221581)</sub>  

**Chrome development tools must be disabled.**  
While the risk associated with browser development tools is more related to the proper design of a web application, a risk vector remains within the browser. The developer tools allow end users and application developers to view and edit all types of web application related data via the browser. Pag…  
Sets `HKLM\Software\Policies\Google\Chrome\DeveloperToolsAvailability` = `2` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0068 (Vuln V-221599)</sub>  

**Cloud print sharing must be disabled.**  
Policy enables Google Chrome to act as a proxy between Google Cloud Print and legacy printers connected to the machine. If this setting is enabled or not configured, users can enable the cloud print proxy by authentication with their Google account. If this setting is disabled, users cannot enable t…  
Sets `HKLM\Software\Policies\Google\Chrome\CloudPrintProxyEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0023 (Vuln V-221573)</sub>  

**Collection of WebRTC event logs must be disabled.**  
If the policy is set to “true”, Google Chrome is allowed to collect WebRTC event logs from Google services (e.g., Google Meet), and upload those logs to Google. If the policy is set to “false”, or is unset, Google Chrome may not collect nor upload such logs. These logs contain diagnostic information…  
Sets `HKLM\Software\Policies\Google\Chrome\WebRtcEventLogCollectionAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0067 (Vuln V-221598)</sub>  

**Create Themes with AI must be disabled.**  
Create Themes with AI lets users create custom themes/wallpapers by preselecting from a list of options. 0 = Allow the feature to be used, while allowing Google to use relevant data to improve its AI models. Relevant data may include prompts, inputs, outputs, source materials, and written feedback,…  
Sets `HKLM\Software\Policies\Google\Chrome\"\CreateThemesSettings` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0075 (Vuln V-275780)</sub>  

**Default search provider must be enabled.**  
Policy enables the use of a default search provider. If you enable this setting, a default search is performed when the user types text in the omnibox that is not a URL. You can specify the default search provider to be used by setting the rest of the default search policies. If these are left empty…  
Sets `HKLM\Software\Policies\Google\Chrome\DefaultSearchProviderEnabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0009 (Vuln V-221566)</sub>  

**Deletion of browser history must be disabled.**  
Disabling this function will prevent users from deleting their browsing history, which could be used to identify malicious websites and files that could later be used for anti-virus and Intrusion Detection System (IDS) signatures. Furthermore, preventing users from deleting browsing history could be…  
Sets `HKLM\Software\Policies\Google\Chrome\AllowDeletingBrowserHistory` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: DTBC-0052 (Vuln V-221586)</sub>  

**DevTools Generative AI features must be disabled.**  
These features in Google Chrome's DevTools employ generative AI models to provide additional debugging information. To use these features, Google Chrome collects data such as error messages, stack traces, code snippets, and network requests and sends them to a server owned by Google, which runs a ge…  
Sets `HKLM\Software\Policies\Google\Chrome\"\DevToolsGenAiSettings` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0076 (Vuln V-275781)</sub>  

**Extensions installation must be blocklisted by default.**  
Extensions are developed by third party sources and are designed to extend Google Chrome's functionality. An extension can be made by anyone, to do and access almost anything on a system; this means they pose a high risk to any system that would allow all extensions to be installed by default. Allow…  
Sets `HKLM\Software\Policies\Google\Chrome\ExtensionInstallBlocklist\1` = `*` (REG_MULTI_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0005 (Vuln V-221562)</sub>  

**Firewall traversal from remote host must be disabled.**  
Remote connections should never be allowed that bypass the firewall, as there is no way to verify if they can be trusted. Enables usage of STUN and relay servers when remote clients are trying to establish a connection to this machine. If this setting is enabled, then remote clients can discover and…  
Sets `HKLM\Software\Policies\Google\Chrome\RemoteAccessHostFirewallTraversal` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0001 (Vuln V-221558)</sub>  

**GenAI local foundational model must be disabled.**  
Configure how Google Chrome downloads the foundational GenAI model and uses it for inference locally. When the policy is set to Allowed (0) or not set, the model is downloaded automatically, and used for inference. When the policy is set to Disabled (1), the model will not be downloaded. Model downl…  
Sets `HKLM\Software\Policies\Google\Chrome\"\GenAILocalFoundationalModelSettings` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0077 (Vuln V-275782)</sub>  

**Google Cast must be disabled.**  
If this policy is set to ”True” or is not set, Google Cast will be enabled, and users will be able to launch it from the app menu, page context menus, media controls on Cast-enabled websites, and (if shown) the “Cast toolbar” icon. If this policy set to ”False”, Google Cast will be disabled.  
Sets `HKLM\Software\Policies\Google\Chrome\EnableMediaRouter` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0063 (Vuln V-221594)</sub>  

**Google Data Synchronization must be disabled.**  
Disables data synchronization in Google Chrome using Google-hosted synchronization services and prevents users from changing this setting. If you enable this setting, users cannot change or override this setting in Google Chrome. If this policy is left not set the user will be able to enable Google…  
Sets `HKLM\Software\Policies\Google\Chrome\SyncDisabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0020 (Vuln V-221571)</sub>  

**Guest Mode must be disabled.**  
If this policy is set to true or not configured, Google Chrome will enable guest logins. Guest logins are Google Chrome profiles where all windows are in incognito mode. If this policy is set to false, Google Chrome will not allow guest profiles to be started.  
Sets `HKLM\Software\Policies\Google\Chrome\BrowserGuestModeEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0069 (Vuln V-226401)</sub>  

**Help Me Write must be disabled.**  
Help Me Write is an AI-based writing assistant for short-form content on the web. Suggested content is based on prompts entered by the user and the content of the web page. 0 = Allow the feature to be used, while allowing Google to use relevant data to improve its AI models. Relevant data may includ…  
Sets `HKLM\Software\Policies\Google\Chrome\"\HelpMeWriteSettings` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0078 (Vuln V-275783)</sub>  

**Import AutoFill form data must be disabled.**  
This policy forces the autofill form data to be imported from the previous default browser if enabled. If enabled, this policy also affects the import dialog. If disabled, the autofill form data is not imported. If it is not set, the user may be asked whether to import, or importing may happen autom…  
Sets `HKLM\Software\Policies\Google\Chrome\ImportAutofillFormData` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0072 (Vuln V-226404)</sub>  

**Importing of saved passwords must be disabled.**  
Importing of saved passwords should be disabled as it could lead to unencrypted account passwords stored on the system from another browser to be viewed. This policy forces the saved passwords to be imported from the previous default browser if enabled. If enabled, this policy also affects the impor…  
Sets `HKLM\Software\Policies\Google\Chrome\ImportSavedPasswords` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0029 (Vuln V-221577)</sub>  

**Incognito mode must be disabled.**  
Incognito mode allows the user to browse the Internet without recording their browsing history/activity. From a forensics perspective, this is unacceptable. Best practice requires that browser history is retained. The "IncognitoModeAvailability" setting controls whether the user may utilize Incognit…  
Sets `HKLM\Software\Policies\Google\Chrome\IncognitoModeAvailability` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0030 (Vuln V-221578)</sub>  

**Metrics reporting to Google must be disabled.**  
Enables anonymous reporting of usage and crash-related data about Google Chrome to Google and prevents users from changing this setting. If you enable this setting, anonymous reporting of usage and crash-related data is sent to Google. A crash report could contain sensitive information from the comp…  
Sets `HKLM\Software\Policies\Google\Chrome\MetricsReportingEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0026 (Vuln V-221575)</sub>  

**Network prediction must be disabled.**  
Enables network prediction in Google Chrome and prevents users from changing this setting. If you enable or disable this setting, users cannot change or override this setting in Google Chrome. If this policy is left not set, this will be disabled but the user will be able to change it.  
Sets `HKLM\Software\Policies\Google\Chrome\NetworkPredictionOptions` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0025 (Vuln V-221574)</sub>  

**Online revocation checks must be performed.**  
By setting this policy to true, the previous behavior is restored and online OCSP/CRL checks will be performed. If the policy is not set, or is set to false, then Chrome will not perform online revocation checks. Certificates are revoked when they have been compromised or are no longer valid, and th…  
Sets `HKLM\Software\Policies\Google\Chrome\EnableOnlineRevocationChecks` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: DTBC-0037 (Vuln V-221579)</sub>  

**Prompt for download location must be enabled.**  
If the policy is enabled, the user will be asked where to save each file before downloading. If the policy is disabled, downloads will start immediately, and the user will not be asked where to save the file. If the policy is not configured, the user will be able to change this setting.  
Sets `HKLM\Software\Policies\Google\Chrome\PromptForDownloadLocation` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0053 (Vuln V-221587)</sub>  

**Safe Browsing Extended Reporting must be disabled.**  
Enables Google Chrome's Safe Browsing Extended Reporting and prevents users from changing this setting. Extended Reporting sends some system information and page content to Google servers to help detect dangerous apps and sites. If the setting is set to "True", then reports will be created and sent…  
Sets `HKLM\Software\Policies\Google\Chrome\SafeBrowsingExtendedReportingEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0057 (Vuln V-221590)</sub>  

**Safe Browsing must be enabled.**  
Allows you to control whether Google Chrome's Safe Browsing feature is enabled and the mode it operates in. If this policy is set to 'NoProtection' (value 0), Safe Browsing is never active. If this policy is set to 'StandardProtection' (value 1, which is the default), Safe Browsing is always active…  
Sets `HKLM\Software\Policies\Google\Chrome\SafeBrowsingProtectionLevel` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: DTBC-0038 (Vuln V-221580)</sub>  

**Search suggestions must be disabled.**  
Search suggestion should be disabled as it could lead to searches being conducted that were never intended to be made. Enables search suggestions in Google Chrome's omnibox and prevents users from changing this setting. If you enable this setting, search suggestions are used. If you disable this set…  
Sets `HKLM\Software\Policies\Google\Chrome\SearchSuggestEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0027 (Vuln V-221576)</sub>  

**Site tracking users location must be disabled.**  
Website tracking is the practice of gathering information as to which websites were accesses by a browser. The common method of doing this is to have a website create a tracking cookie on the browser. If the information of what sites are being accessed is made available to unauthorized persons, this…  
Sets `HKLM\Software\Policies\Google\Chrome\DefaultGeolocationSetting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0002 (Vuln V-221559)</sub>  

**Sites ability to show pop-ups must be disabled.**  
Chrome allows you to manage whether unwanted pop-up windows appear. Pop-up windows that are opened when the end user clicks a link are not blocked. If you enable this policy setting, most unwanted pop-up windows are prevented from appearing. If you disable this policy setting, pop-up windows are not…  
Sets `HKLM\Software\Policies\Google\Chrome\DefaultPopupsSetting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0004 (Vuln V-221561)</sub>  

**Tab Compare Settings must be disabled.**  
Tab Compare is an AI-powered tool for comparing information across a user's tabs. For example, the feature can be offered to the user when multiple tabs with products in a similar category are open. 0 = Allow the feature to be used, while allowing Google to use relevant data to improve its AI models…  
Sets `HKLM\Software\Policies\Google\Chrome\"\TabCompareSettings` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0080 (Vuln V-275785)</sub>  

**The Password Manager must be disabled.**  
Enables saving passwords and using saved passwords in Google Chrome. Malicious sites may take advantage of this feature by using hidden fields gain access to the stored information. If you enable this setting, users can have Google Chrome memorize passwords and provide them automatically the next ti…  
Sets `HKLM\Software\Policies\Google\Chrome\PasswordManagerEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0011 (Vuln V-221567)</sub>  

**The URL protocol schema javascript must be disabled.**  
Each access to a URL is handled by the browser according to the URL's "scheme". The "scheme" of a URL is the section before the ":". The term "protocol" is often mistakenly used for a "scheme". The difference is that the scheme is how the browser handles a URL and the protocol is how the browser com…  
Sets `HKLM\Software\Policies\Google\Chrome\URLBlocklist\1` = `javascript://*` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0021 (Vuln V-221572)</sub>  

**Use of the QUIC protocol must be disabled.**  
QUIC is used by more than half of all connections from the Chrome web browser to Google's servers, and this activity is undesirable in the DoD. Setting the policy to Enabled or leaving it unset allows the use of QUIC protocol in Google Chrome. Setting the policy to Disabled disallows the use of QUIC…  
Sets `HKLM\Software\Policies\Google\Chrome\QuicAllowed` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0074 (Vuln V-245538)</sub>  

**Web Bluetooth API must be disabled.**  
Setting the policy to 3 lets websites ask for access to nearby Bluetooth devices. Setting the policy to 2 denies access to nearby Bluetooth devices. Leaving the policy unset lets sites ask for access, but users can change this setting. 2 = Do not allow any site to request access to Bluetooth devices…  
Sets `HKLM\Software\Policies\Google\Chrome\DefaultWebBluetoothGuardSetting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0073 (Vuln V-241787)</sub>  

**WebUSB must be disabled.**  
Allows you to set whether websites are allowed to get access to connected USB devices. Access can be completely blocked, or the user can be asked every time a website wants to get access to connected USB devices. If this policy is left not set, ”3” will be used, and the user will be able to change i…  
Sets `HKLM\Software\Policies\Google\Chrome\DefaultWebUsbGuardSetting` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: DTBC-0058 (Vuln V-221591)</sub>  

## DISA STIG — Mozilla Firefox (V6R7)

_The full DISA Mozilla Firefox STIG, including strict policy lockdowns beyond the exploitation-prevention subset used by the Recommended profile._

**43 settings in this section.**

**Background submission of information to Mozilla must be disabled.**  
Firefox by default sends information about Firefox to Mozilla servers. There should be no background submission of technical and other information from DoD computers to Mozilla with portions posted publicly.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableTelemetry` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000014 (Vuln V-251558)</sub>  

**Firefox accounts must be disabled.**  
Disable Firefox Accounts integration (Sync). It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may remain unsecured. They increase the risk to…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableFirefoxAccounts` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000034 (Vuln V-251578)</sub>  

**Firefox autoplay must be disabled.**  
Autoplay allows the user to control whether videos can play automatically (without user consent) with audio content. The user must be able to select content that is run within the browser window.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\Permissions\Autoplay\Default` = `block-audio-video` (REG_SZ)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000021 (Vuln V-251565)</sub>  

**Firefox cryptomining protection must be enabled.**  
The Content Blocking/Tracking Protection feature stops Firefox from loading content from malicious sites. The content might be a script or an image, for example. If a site is on one of the tracker lists that Firefox is set to use, the fingerprinting script (or other tracking script/image) will not b…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\EnableTrackingProtection\Cryptomining` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000024 (Vuln V-251568)</sub>  

**Firefox deprecated ciphers must be disabled.**  
A weak cipher is defined as an encryption/decryption algorithm that uses a key of insufficient length. Using an insufficient length for a key in an encryption/decryption algorithm opens up the possibility (or probability) that the encryption scheme could be broken.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisabledCiphers\TLS_RSA_WITH_3DES_EDE_CBC_SHA` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000027 (Vuln V-251571)</sub>  

**Firefox development tools must be disabled.**  
Information needed by an attacker to begin looking for possible vulnerabilities in a web browser includes any information about the web browser and plug-ins or modules being used. When debugging or trace information is enabled in a production web browser, information about the web browser, such as w…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableDeveloperTools` = `1` (REG_DWORD)  
<sub>Risk: **Low** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000015 (Vuln V-251559)</sub>  

**Firefox encrypted media extensions must be disabled.**  
Enable or disable Encrypted Media Extensions and optionally lock it. If "Enabled" is set to "false", Firefox does not download encrypted media extensions (such as Widevine) unless the user consents to installing them. If "Locked" is set to "true" and "Enabled" is set to "false", Firefox will not dow…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\EncryptedMediaExtensions\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000037 (Vuln V-251581.a)</sub>  

**Firefox encrypted media extensions must be disabled.**  
Enable or disable Encrypted Media Extensions and optionally lock it. If "Enabled" is set to "false", Firefox does not download encrypted media extensions (such as Widevine) unless the user consents to installing them. If "Locked" is set to "true" and "Enabled" is set to "false", Firefox will not dow…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\EncryptedMediaExtensions\Locked` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000037 (Vuln V-251581.b)</sub>  

**Firefox feedback reporting must be disabled.**  
Disable the menus for reporting sites (Submit Feedback, Report Deceptive Site). It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may remain u…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableFeedbackCommands` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000036 (Vuln V-251580)</sub>  

**Firefox fingerprinting protection must be enabled.**  
The Content Blocking/Tracking Protection feature stops Firefox from loading content from malicious sites. The content might be a script or an image, for example. If a site is on one of the tracker lists that Firefox is set to use, the fingerprinting script (or other tracking script/image) will not b…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\EnableTrackingProtection\Fingerprinting` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000023 (Vuln V-251567)</sub>  

**Firefox must be configured so that DNS over HTTPS is disabled.**  
DNS over HTTPS has generally not been adopted in the DoD. DNS is tightly controlled. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlooked and therefore may rem…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DNSOverHTTPS\Enabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000033 (Vuln V-251577)</sub>  

**Firefox must be configured to allow only TLS 1.2 or above.**  
Use of versions prior to TLS 1.2 are not permitted. SSL 2.0 and SSL 3.0 contain a number of security flaws. These versions must be disabled in compliance with the Network Infrastructure and Secure Remote Computing STIGs.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SSLVersionMin` = `tls1.2` (REG_SZ)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: FFOX-00-000002 (Vuln V-251546)</sub>  

**Firefox must be configured to block pop-up windows.**  
Pop-up windows may be used to launch an attack within a new browser window with altered settings. This setting blocks pop-up windows created while the page is loading.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\PopupBlocking\Default` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000009 (Vuln V-251553.a)</sub>  

**Firefox must be configured to block pop-up windows.**  
Pop-up windows may be used to launch an attack within a new browser window with altered settings. This setting blocks pop-up windows created while the page is loading.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\PopupBlocking\Locked` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000009 (Vuln V-251553.b)</sub>  

**Firefox must be configured to disable form fill assistance.**  
To protect privacy and sensitive data, Firefox provides the ability to configure the program so that data entered into forms is not saved. This mitigates the risk of a website gleaning private information from prefilled information.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableFormHistory` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000007 (Vuln V-251551)</sub>  

**Firefox must be configured to disable the installation of extensions.**  
A browser extension is a program that has been installed into the browser to add functionality. Where a plug-in interacts only with a web page and usually a third-party external application (e.g., Flash, Adobe Reader), an extension interacts with the browser program itself. Extensions are not embedd…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\InstallAddonsPermission\Default` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000013 (Vuln V-251557)</sub>  

**Firefox must be configured to not automatically update installed add-ons and plugins.**  
Set this to false to disable checking for updated versions of the Extensions/Themes. Automatic updates from untrusted sites puts the enclave at risk of attack and may override security settings.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\ExtensionUpdate` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000005 (Vuln V-251549)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Locked` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.f)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Cookies` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.b)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Downloads` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.c)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\FormData` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.d)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\History` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.e)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Cache` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.a)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\OfflineApps` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.g)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\Sessions` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.h)</sub>  

**Firefox must be configured to not delete data upon shutdown.**  
For diagnostic purposes, data must remain behind when the browser is closed. This is required to meet non-repudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SanitizeOnShutdown\SiteSettings` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000017 (Vuln V-252881.i)</sub>  

**Firefox must be configured to not use a password store with or without a master password.**  
Firefox can be set to store passwords for sites visited by the user. These individual passwords are stored in a file and can be protected by a master password. Autofill of the password can then be enabled when the site is visited. This feature could also be used to autofill the certificate PIN, whic…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\PasswordManagerEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000008 (Vuln V-251552)</sub>  

**Firefox must have the DOD root certificates installed.**  
The DOD root certificates will ensure that the trust chain is established for server certificates issued from the DOD Certificate Authority (CA).  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\Certificates\ImportEnterpriseRoots` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000016 (Vuln V-251560)</sub>  

**Firefox must not recommend extensions as the user is using the browser.**  
The Recommended Extensions program recommends extensions to users as they surf the web. The user must not be encouraged to install extensions from the websites they visit. Allowed extensions are to be centrally managed.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\UserMessaging\ExtensionRecommendations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000028 (Vuln V-251572)</sub>  

**Firefox must prevent the user from quickly deleting data.**  
There should not be an option for a user to "forget" work they have done. This is required to meet nonrepudiation controls.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableForgetButton` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: FFOX-00-000018 (Vuln V-251562)</sub>  

**Firefox network prediction must be disabled.**  
If network prediction is enabled, requests to URLs are made without user consent. The browser should always make a direct DNS request without prefetching occurring.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\NetworkPrediction` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000022 (Vuln V-251566)</sub>  

**Firefox private browsing must be disabled.**  
Private browsing allows the user to browse the internet without recording their browsing history/activity. From a forensics perspective, this is unacceptable. Best practice requires that browser history is retained.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisablePrivateBrowsing` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000019 (Vuln V-251563)</sub>  

**Firefox search suggestions must be disabled.**  
Search suggestions must be disabled as this could lead to searches being conducted that were never intended to be made.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\SearchSuggestEnabled` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000020 (Vuln V-251564)</sub>  

**Firefox Studies must be disabled.**  
Studies try out different features and ideas before they are released to all Firefox users. Testing beta software is not in the DoD user's mission.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisableFirefoxStudies` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000039 (Vuln V-252909)</sub>  

**Pocket must be disabled.**  
Pocket, previously known as Read It Later, is a social bookmarking service for storing, sharing, and discovering web bookmarks. Data gathering cloud services such as this are generally disabled in the DoD.  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\DisablePocket` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000038 (Vuln V-252908)</sub>  

**The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…**  
The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\SponsoredTopSites` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000029 (Vuln V-251573.g)</sub>  

**The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…**  
The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\SponsoredPocket` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000029 (Vuln V-251573.f)</sub>  

**The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…**  
The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Snippets` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000029 (Vuln V-251573.e)</sub>  

**The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…**  
The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Search` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000029 (Vuln V-251573.d)</sub>  

**The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…**  
The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Locked` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000029 (Vuln V-251573.b)</sub>  

**The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…**  
The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Highlights` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000029 (Vuln V-251573.a)</sub>  

**The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…**  
The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\TopSites` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000029 (Vuln V-251573.h)</sub>  

**The Firefox New Tab page must not show Top Sites, Sponsored Top Sites, Pocket Recommendations, Sponsored Pocke…**  
The New Tab page by default shows a list of built-in top sites, as well as the top sites the user has visited. It is detrimental for applications to provide, or install by default, functionality exceeding requirements or mission objectives. These unnecessary capabilities or services are often overlo…  
Sets `HKLM\SOFTWARE\Policies\Mozilla\Firefox\FirefoxHome\Pocket` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: FFOX-00-000029 (Vuln V-251573.c)</sub>  

## DISA STIG — Microsoft Office 365 ProPlus (V3R5)

_The full DISA Office 365 STIG — overwhelmingly high-value anti-document-malware controls (macro blocking, Protected View, ActiveX/DDE hardening, unsigned add-in blocking). The Recommended profile keeps these but omits the legacy file-format blocks that would stop old .doc/.xls/.ppt files from opening._

**106 settings in this section.**

**Active X One-Off forms must only be enabled to load with Outlook Controls.**  
By default, third-party ActiveX controls are not allowed to run in one-off forms in Outlook. You can change this behavior so that Safe Controls (Microsoft Forms 2.0 controls and the Outlook Recipient and Body controls) are allowed in one-off forms, or so that all ActiveX controls are allowed to run.  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\allowactivexoneoffforms` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000007 (Vuln V-223352)</sub>  

**AutoRepublish in Excel must be disabled.**  
This policy setting allows administrators to disable the AutoRepublish feature in Excel. If users choose to publish Excel data to a static Web page and enable the AutoRepublish feature, Excel saves a copy of the data to the Web page every time the user saves the workbook. By default, a message dialo…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\disableautorepublish` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000021 (Vuln V-223330)</sub>  

**AutoRepublish warning alert in Excel must be enabled.**  
This policy setting allows administrators to disable the AutoRepublish feature in Excel. If users choose to publish Excel data to a static Web page and enable the AutoRepublish feature, Excel saves a copy of the data to the Web page every time the user saves the workbook. By default, a message dialo…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Options\disableautorepublishwarning` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000022 (Vuln V-223331)</sub>  

**Custom user interface (UI) code must be blocked from loading in all Office applications.**  
This policy setting controls whether Office 365 ProPlus applications load any custom user interface (UI) code included with a document or template. Office 365 ProPlus allows developers to extend the UI with customization code that is included in a document or template. If this policy setting is enab…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\toolbars\noextensibilitycustomizationfromdocument` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000004 (Vuln V-223287)</sub>  

**Document metadata for rights managed Office Open XML files must be protected.**  
This policy setting determines whether metadata is encrypted in Office Open XML files that are protected by Information Rights Management (IRM). If this policy setting is enabled, Excel, PowerPoint, and Word encrypt metadata stored in rights-managed Office Open XML files and override any configurati…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\common\security\DRMEncryptProperty` = `1` (REG_DWORD)  
<sub>Risk: **High** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000002 (Vuln V-223285)</sub>  

**Dynamic Data Exchange (DDE) server launch in Excel must be blocked.**  
This policy setting allows you to control whether Dynamic Data Exchange (DDE) server launch is allowed. By default, DDE server launch is turned off, but users can turn on DDE server launch by going to File >> Options >> Trust Center >> Trust Center Settings >> External Content. For security reasons,…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\external content\disableddeserverlaunch` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000003 (Vuln V-223312)</sub>  

**Dynamic Data Exchange (DDE) server lookup in Excel must be blocked.**  
This policy setting allows you to control whether Dynamic Data Exchange (DDE) server lookup is allowed. By default, DDE server lookup is turned on, but users can turn off DDE server lookup by going to File >> Options >> Trust Center >> Trust Center Settings >> External Content. If you enable this po…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\external content\disableddeserverlookup` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000004 (Vuln V-223313)</sub>  

**Extraction options must be blocked when opening corrupt Excel workbooks.**  
This policy setting controls whether Excel presents users with a list of data extraction options before beginning an Open and Repair operation when users choose to open a corrupt workbook in repair or extract mode. If you enable this policy setting, Excel opens the file using the Safe Load process a…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\extractdatadisableui` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000018 (Vuln V-223327)</sub>  

**File attachments from Outlook must be opened in Excel in Protected mode.**  
This policy setting allows you to determine if Excel files in Outlook attachments open in Protected View. If you enable this policy setting, Outlook attachments do not open in Protected View. If you disable or do not configure this policy setting, Outlook attachments open in Protected View.  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\protectedview\DisableAttachmentsInPV` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000034 (Vuln V-223343)</sub>  

**File extensions must be enabled to match file types in Excel.**  
This policy setting controls how Excel loads file types that do not match their extension. Excel can load files with extensions that do not match the files' type. For example, if a comma-separated values (CSV) file named example.csv is renamed example.xls (or any other file extension supported by Ex…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Security\extensionhardening` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000023 (Vuln V-223332)</sub>  

**File validation in Excel must be enabled.**  
This policy setting allows you turn off the file validation feature. If you enable this policy setting, file validation will be turned off. If you disable or do not configure this policy setting, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they conform…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\filevalidation\enableonload` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000025 (Vuln V-223334)</sub>  

**File validation in PowerPoint must be enabled.**  
This policy setting allows you to turn off the file validation feature. If you enable this policy setting, file validation will be turned off. If you disable or do not configure this policy setting, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they confo…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\filevalidation\EnableOnLoad` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PT-000006 (Vuln V-223382)</sub>  

**File validation in Word must be enabled.**  
This policy setting allows the file validation feature to be turned off. If this policy setting is enabled, file validation will be turned off. If this policy setting is disabled or not configured, file validation will be turned on. Office Binary Documents (97-2003) are checked to see if they confor…  
Sets `HKCU\software\policies\microsoft\office\16.0\word\security\filevalidation\enableonload` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-WD-000019 (Vuln V-223418)</sub>  

**Files downloaded from the Internet must be opened in Protected view in PowerPoint.**  
This policy setting allows you to determine if files downloaded from the Internet zone open in Protected View. If you enable this policy setting, files downloaded from the Internet zone do not open in Protected View. If you disable or do not configure this policy setting, files downloaded from the I…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableInternetFilesInPV` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PT-000009 (Vuln V-223385)</sub>  

**Files dragged from an Outlook e-mail to the file system must be created in ANSI format.**  
This policy setting controls whether e-mail messages dragged from Outlook to the file system are saved in Unicode or ANSI format.  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\options\general\msgformat` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000005 (Vuln V-223350)</sub>  

**Files failing file validation must be opened in Excel in Protected view mode and disallow edits.**  
This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Excel\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000033 (Vuln V-223342.b)</sub>  

**Files in unsafe locations must be opened in Protected view in PowerPoint.**  
This policy setting determines whether files located in unsafe locations will open in Protected View. If unsafe locations have not been specified, only the "Downloaded Program Files" and "Temporary Internet Files" folders are considered unsafe locations. If enabling this policy setting, files locate…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableUnsafeLocationsInPV` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PT-000011 (Vuln V-223387)</sub>  

**Flash player activation must be disabled in all Office programs.**  
This policy setting controls whether the Adobe Flash control can be activated by Office documents. Note that activation blocking applies only within Office processes. If you enable this policy setting, you can choose from three options to control whether and how Flash is blocked from activation: 1.…  
Sets `HKLM\SOFTWARE\Microsoft\Office\Common\COM Compatibility\COMMENT` = `Block all Flash activation` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-CO-000027 (Vuln V-223309)</sub>  

**If file validation fails, files must be opened in Protected view in PowerPoint with ability to edit disabled.**  
This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PT-000012 (Vuln V-223388.b)</sub>  

**If file validation fails, files must be opened in Protected view in Word with ability to edit disabled.**  
This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Word\Security\FileValidation\openinprotectedview` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-WD-000005 (Vuln V-223404.a)</sub>  

**If file validation fails, files must be opened in Protected view in Word with ability to edit disabled.**  
This policy setting controls how Office handles documents when they fail file validation. If you enable this policy setting, you can configure the following options for files that fail file validation: - Block files completely. Users cannot open the files. - Open files in Protected View and disallow…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Word\Security\FileValidation\DisableEditFromPV` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-WD-000005 (Vuln V-223404.b)</sub>  

**In Word, macros must be blocked from running, even if Enable all macros is selected in the Macro Settings sect…**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if "Enable all macros" is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-WD-000016 (Vuln V-223415)</sub>  

**Internet must not be included in Safe Zone for picture download in Outlook.**  
This policy setting controls whether pictures and external content in HTML e-mail messages from untrusted senders on the Internet are downloaded without Outlook users explicitly choosing to do so. If you enable this policy setting, Outlook will automatically download external content in all e-mail m…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\options\mail\Internet` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000009 (Vuln V-223354)</sub>  

**Loading of pictures from Web pages not created in Excel must be disabled.**  
This policy setting controls whether Excel loads graphics when opening Web pages that were not created in Excel. It configures the "Load pictures from Web pages not created in Excel" option under the File tab >> Options >> Advanced >> General >> Web Options... >> General tab. If you enable or do not…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\internet\donotloadpictures` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000020 (Vuln V-223329)</sub>  

**Macros from the Internet must be blocked from running in PowerPoint.**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if "Enable all macros" is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\powerpoint\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PT-000007 (Vuln V-223383)</sub>  

**Macros in all Office applications that are opened programmatically by another application must be opened based…**  
This policy setting controls whether macros can run in an Office 365 ProPlus application that is opened programmatically by another application. If this policy setting is enabled, the user can choose from three options for controlling macro behavior in Excel, PowerPoint, and Word when the applicatio…  
Sets `HKCU\Software\Policies\Microsoft\Office\Common\Security\AutomationSecurity` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000006 (Vuln V-223289)</sub>  

**Macros must be blocked from running in Access files from the Internet.**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\access\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-AC-000001 (Vuln V-223280)</sub>  

**Macros must be blocked from running in Excel files from the Internet.**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000027 (Vuln V-223336)</sub>  

**Macros must be blocked from running in Visio files from the Internet.**  
This policy setting allows you to block macros from running in Office files that come from the Internet. If you enable this policy setting, macros are blocked from running, even if “Enable all macros” is selected in the Macro Settings section of the Trust Center. Also, instead of having the choice t…  
Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-VI-000007 (Vuln V-223399)</sub>  

**Office applications must be configured to specify encryption type in password-protected Office 97-2003 files.**  
This policy setting enables you to specify an encryption type for password-protected Office 97-2003 files. If you enable this policy setting, you can specify the type of encryption that Office applications will use to encrypt password-protected files in the older Office 97-2003 file formats. The cho…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Common\Security\defaultencryption12` = `Microsoft Enhanced RSA and AES Cryptographic Provider,AES 256,256` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000008 (Vuln V-223291)</sub>  

**Office applications must be configured to specify encryption type in password-protected Office Open XML files.**  
This policy setting allows you to specify an encryption type for Office Open XML files. If you enable this policy setting, you can specify the type of encryption that Office applications use to encrypt password-protected files in the Office Open XML file formats used by Excel, PowerPoint, and Word.…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Common\Security\OpenXMLEncryption` = `Microsoft Enhanced RSA and AES Cryptographic Provider,AES 256,256` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000009 (Vuln V-223292)</sub>  

**Office applications must not load XML expansion packs with Smart Documents.**  
This policy setting controls whether Office 365 ProPlus applications can load an XML expansion pack manifest file with a Smart Document.  
Sets `HKCU\software\policies\microsoft\office\common\smart tag\for neverloadmanifests` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-CO-000012 (Vuln V-223294)</sub>  

**Open/save of dBase III / IV format files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\DBaseFiles` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000005 (Vuln V-223314)</sub>  

**Open/save of Dif and Sylk format files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\DifandSylkFiles` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000006 (Vuln V-223315)</sub>  

**Open/save of Excel 2 macrosheets and add-in files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL2Macros` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000007 (Vuln V-223316)</sub>  

**Open/save of Excel 2 worksheets must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL2Worksheets` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000008 (Vuln V-223317)</sub>  

**Open/save of Excel 3 macrosheets and add-in files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL3Macros` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000009 (Vuln V-223318)</sub>  

**Open/save of Excel 3 worksheets must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL3Worksheets` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000010 (Vuln V-223319)</sub>  

**Open/save of Excel 4 macrosheets and add-in files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL4Macros` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000011 (Vuln V-223320)</sub>  

**Open/save of Excel 4 workbooks must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL4Workbooks` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000012 (Vuln V-223321)</sub>  

**Open/save of Excel 4 worksheets must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\XL4Worksheets` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000013 (Vuln V-223322)</sub>  

**Open/save of Excel 95 workbooks must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\fileblock\xl95workbooks` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000014 (Vuln V-223323)</sub>  

**Open/save of Excel 95-97 workbooks and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\office\16.0\excel\security\fileblock\XL9597WorkbooksandTemplates` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000015 (Vuln V-223324)</sub>  

**Open/Save of PowerPoint 97-2003 presentations, shows, templates, and add-in files must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save PowerPoint files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be select…  
Sets `HKCU\software\policies\microsoft\office\16.0\powerpoint\security\fileblock\binaryfiles` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-PT-000003 (Vuln V-223379)</sub>  

**Open/save of Web pages and Excel 2003 XML spreadsheets must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\fileblock\htmlandxmlssfiles` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000017 (Vuln V-223326)</sub>  

**Open/Save of Word 2 and earlier binary documents and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\Word2Files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000008 (Vuln V-223407)</sub>  

**Open/Save of Word 2000 binary documents and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\Word2000Files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000009 (Vuln V-223408)</sub>  

**Open/Save of Word 2003 binary documents and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word2003files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000010 (Vuln V-223409)</sub>  

**Open/Save of Word 2007 and later binary documents and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word2007files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000011 (Vuln V-223410)</sub>  

**Open/Save of Word 6.0 binary documents and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word60files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000012 (Vuln V-223411)</sub>  

**Open/Save of Word 95 binary documents and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word95files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000013 (Vuln V-223412)</sub>  

**Open/Save of Word 97 binary documents and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\word97files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000014 (Vuln V-223413)</sub>  

**Open/Save of Word XP binary documents and templates must be blocked.**  
This policy setting allows you to determine whether users can open, view, edit, or save Word files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected are…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\wordxpfiles` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000015 (Vuln V-223414)</sub>  

**Outlook must be configured to allow retrieving of Certificate Revocation Lists (CRLs) always when online.**  
This policy setting controls how Outlook retrieves Certificate Revocation Lists to verify the validity of certificates. Certificate revocation lists (CRLs) are lists of digital certificates that have been revoked by their controlling certificate authorities (CAs), typically because the certificates…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\usecrlchasing` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000013 (Vuln V-223358)</sub>  

**Outlook must be configured to not allow hyperlinks in suspected phishing messages.**  
This policy setting controls whether hyperlinks in suspected phishing e-mail messages in Outlook are allowed. If you enable this policy setting, Outlook will allow hyperlinks in suspected phishing messages that are not also classified as junk e-mail. If you disable or do not configure this policy se…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\options\mail\JunkMailEnableLinks` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000027 (Vuln V-223372)</sub>  

**Outlook must be configured to not run scripts in forms in which the script and the layout are contained within…**  
This policy setting controls whether scripts can run in Outlook forms in which the script and layout are contained within the message. If you enable this policy setting, scripts can run in one-off Outlook forms. If you disable or do not configure this policy setting, Outlook does not run scripts in…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\EnableOneOffFormScripts` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000019 (Vuln V-223364)</sub>  

**Outlook must be configured to prevent users overriding attachment security settings.**  
This policy setting prevents users from overriding the set of attachments blocked by Outlook. If you enable this policy setting users will be prevented from overriding the set of attachments blocked by Outlook. Outlook also checks the "Level1Remove" registry key when this setting is specified. If yo…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\disallowattachmentcustomization` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000008 (Vuln V-223353)</sub>  

**Outlook must use remote procedure call (RPC) encryption to communicate with Microsoft Exchange servers.**  
This policy setting controls whether Outlook uses remote procedure call (RPC) encryption to communicate with Microsoft Exchange servers. If you enable this policy setting, Outlook uses RPC encryption when communicating with an Exchange server. Note: RPC encryption only encrypts the data from the Out…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\rpc\enablerpcencryption` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000002 (Vuln V-223347)</sub>  

**PowerPoint attachments opened from Outlook must be in Protected View.**  
This policy setting allows for determining whether PowerPoint files in Outlook attachments open in Protected View. If enabling this policy setting, Outlook attachments do not open in Protected View. If disabling or not configuring this policy setting, Outlook attachments open in Protected View.  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\protectedview\DisableAttachmentsInPV` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PT-000010 (Vuln V-223386)</sub>  

**Project must automatically disable unsigned add-ins without informing users.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\Microsoft\office\16.0\ms project\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PR-000002 (Vuln V-223375)</sub>  

**Publisher must automatically disable unsigned add-ins without informing users.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\microsoft\office\16.0\publisher\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PU-000002 (Vuln V-223391)</sub>  

**Publisher must be configured to prompt the user when another application programmatically opens a macro.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if you enable the "Require that application add-ins are signed by Trusted Publishe…  
Sets `HKCU\software\policies\microsoft\office\common\security\automationsecuritypublisher` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PU-000001 (Vuln V-223390)</sub>  

**Scripts associated with public folders must be prevented from execution in Outlook.**  
This policy setting controls whether Outlook executes scripts that are associated with custom forms or folder home pages for public folders.  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\publicfolderscript` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000003 (Vuln V-223348)</sub>  

**Scripts associated with shared folders must be prevented from execution in Outlook.**  
This policy setting controls whether Outlook executes scripts associated with custom forms or folder home pages for shared folders.  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\sharedfolderscript` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000004 (Vuln V-223349)</sub>  

**Sending of diagnostic data to Microsoft must be disabled.**  
Diagnostic data is used to keep Office secure and up to date; detect, diagnose and remediate problems; and make product improvements.  
Sets `HKCU\software\policies\Microsoft\office\common\clienttelemetry\SendTelemetry is REG_DWORD = 3, this is not a finding. If the registry key does not exist or` = `3` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-CO-000028 (Vuln V-278355)</sub>  

**The ability to demote attachments from Level 2 to Level 1 must be disabled.**  
This policy setting controls whether Outlook users can demote attachments to Level 2 by using a registry key, which will allow them to save files to disk and open them from that location. Outlook uses two levels of security to restrict access to files attached to e-mail messages or other items. File…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\allowuserstolowerattachments` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000015 (Vuln V-223360)</sub>  

**The default file block behavior must be set to not open blocked files in Excel.**  
This policy setting allows you to determine whether users can open, view, edit, or save Excel files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open, view, edit, or save files. The options that can be selected ar…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\fileblock\OpenInProtectedView` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-EX-000016 (Vuln V-223325)</sub>  

**The default file block behavior must be set to not open blocked files in PowerPoint.**  
This policy setting allows you to determine if users can open, view, or edit Word files. If you enable this policy setting, you can set one of these options: - Blocked files are not opened. - Blocked files open in Protected View and cannot be edited. - Blocked files open in Protected View and can be…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\fileblock\OpenInProtectedView` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-PT-000004 (Vuln V-223380)</sub>  

**The default file block behavior must be set to not open blocked files in Word.**  
This policy setting allows you to determine if users can open, view, or edit Word files. If you enable this policy setting, you can set one of these options: - Blocked files are not opened. - Blocked files open in Protected View and cannot be edited. - Blocked files open in Protected View and can be…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\word\security\fileblock\OpenInProtectedView` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-WD-000007 (Vuln V-223406)</sub>  

**The display of Level 1 attachments must be disabled in Outlook.**  
This policy setting controls whether Outlook blocks potentially dangerous attachments designated Level 1. Outlook uses two levels of security to restrict users' access to files attached to e-mail messages or other items. Files with specific extensions can be categorized as Level 1 (users cannot view…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\ShowLevel1Attach` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000016 (Vuln V-223361)</sub>  

**The HTTP fallback for SIP connection in Lync must be disabled.**  
Prevents from HTTP being used for SIP connection in case TLS or TCP fail.  
Sets `HKLM\Software\Policies\Microsoft\office\16.0\lync\disablehttpconnect` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-LY-000002 (Vuln V-223345)</sub>  

**The junk email protection level must be set to No Automatic Filtering.**  
This policy setting controls the Junk E-mail protection level. The Junk E-mail Filter in Outlook helps to prevent junk email messages, also known as spam, from cluttering a user's Inbox. The filter evaluates each incoming message based on several factors, including the time when the message was sent…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\Options\Mail\junkmailprotection` = `3` (REG_SZ)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-OU-000006 (Vuln V-223351)</sub>  

**The load of controls in Forms3 must be blocked.**  
This policy setting allows the user to control how ActiveX controls in UserForms should be initialized based upon whether they are Safe for Initialization (SFI) or Unsafe for Initialization (UFI). ActiveX controls are Component Object Model (COM) objects and have unrestricted access to users' comput…  
Sets `HKCU\SOFTWARE\Policies\Microsoft\vba\security\LoadControlsInForms` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000013 (Vuln V-223295)</sub>  

**The Macro Runtime Scan Scope must be enabled for all documents.**  
This policy setting specifies for which documents the VBA Runtime Scan feature is enabled. If the feature is disabled for all documents, no runtime scanning of enabled macros will be performed. If the feature is enabled for low trust documents, the feature will be enabled for all documents for which…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\security\macroruntimescanscope` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000001 (Vuln V-223284)</sub>  

**The Office client must be prevented from polling the SharePoint Server for published links.**  
This policy setting controls whether Office 365 ProPlus applications can poll Office servers to retrieve lists of published links. If this policy setting is enabled, Office 365 ProPlus applications cannot poll an Office server for published links. If this policy setting is disabled or not configured…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\portal\linkpublishingdisabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-CO-000003 (Vuln V-223286)</sub>  

**The Outlook Security Mode must be enabled to always use the Outlook Security Group Policy.**  
This policy setting controls which set of security settings are enforced in Outlook. If you enable this policy setting, you can choose from four options for enforcing Outlook security settings: - Outlook Default Security - This option is the default configuration in Outlook. Users can configure secu…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\adminsecuritymode` = `3` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000014 (Vuln V-223359)</sub>  

**The Publish to Global Address List (GAL) button must be disabled in Outlook.**  
This policy setting controls whether Outlook users can publish e-mail certificates to the Global Address List (GAL). If you enable this policy setting, the "Publish to GAL" button does not display in the "E-mail Security" section of the Trust Center. If you disable or do not configure this policy se…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\Outlook\security\publishtogaldisabled` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000010 (Vuln V-223355)</sub>  

**The Security Level for macros in Outlook must be configured to Warn for signed and disable unsigned.**  
This policy setting controls the security level for macros in Outlook. If you enable this policy setting, you can choose from four options for handling macros in Outlook: - Always warn. This option corresponds to the "Warnings for all macros" option in the "Macro Security" section of the Outlook Tru…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\level` = `3` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000028 (Vuln V-223373)</sub>  

**The SIP security mode in Lync must be enabled.**  
When Lync connects to the server, it supports various authentication mechanisms. This policy allows the user to specify whether Digest and Basic authentication are supported. Disabled (default): NTLM/Kerberos/TLS-DSK/Digest/Basic Enabled: Authentication mechanisms: NTLM/Kerberos/TLS-DSK Gal Download…  
Sets `HKLM\Software\Policies\Microsoft\office\16.0\lync\enablesiphighsecuritymode` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-LY-000001 (Vuln V-223344)</sub>  

**The use of network locations must be ignored in PowerPoint.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\PowerPoint\security\trusted locations\AllowNetworkLocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PT-000013 (Vuln V-223389)</sub>  

**The warning about invalid digital signatures must be enabled to warn Outlook users.**  
This policy setting controls how Outlook warns users about messages with invalid digital signatures. If you enable this policy setting, you can choose from three options for controlling how Outlook users are warned about invalid signatures: - Let user decide if they want to be warned. This option en…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\warnaboutinvalid` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000012 (Vuln V-223357)</sub>  

**Trust Bar notification must be enabled for unsigned application add-ins in Excel and blocked.**  
This policy setting controls whether the specified Office 2016 applications notify users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the ''Require that application add-ins are signed by Trusted Publisher'' po…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000028 (Vuln V-223337)</sub>  

**Trust Bar Notifications for unsigned application add-ins in Access must be disabled and blocked.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\access\security\NoTBPromptUnsignedAddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-AC-000002 (Vuln V-223281)</sub>  

**Trust Bar notifications must be configured to display information in the Message Bar about the content that ha…**  
This policy setting controls whether Office 365 ProPlus applications notify users when potentially unsafe features or content are detected, or whether such features or content are silently disabled without notification. The Message Bar in Office 365 ProPlus applications is used to identify security…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\trustcenter\trustbar` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000007 (Vuln V-223290)</sub>  

**Trusted Locations on the network must be disabled in Excel.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by selecting the "Allow Trusted Locations on my network (no…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\trusted locations\AllowNetworkLocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000001 (Vuln V-223310)</sub>  

**Trusted Locations on the network must be disabled in Project.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…  
Sets `HKCU\software\policies\microsoft\office\16.0\ms project\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PR-000001 (Vuln V-223374)</sub>  

**Trusted Locations on the network must be disabled in Visio.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…  
Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-VI-000002 (Vuln V-223394)</sub>  

**Trusted Locations on the network must be disabled in Word.**  
This policy setting controls whether trusted locations on the network can be used. If you enable this policy setting, users can specify trusted locations on network shares or in other remote locations that are not under their direct control by clicking the "Add new location" button in the Trusted Lo…  
Sets `HKCU\software\policies\microsoft\office\16.0\word\security\trusted locations\allownetworklocations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-WD-000017 (Vuln V-223416)</sub>  

**Unsigned add-ins in PowerPoint must be blocked with no Trust Bar Notification to the user.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\Microsoft\office\16.0\powerpoint\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-PT-000008 (Vuln V-223384)</sub>  

**Untrusted database files must be opened in Excel in Protected View mode.**  
This policy setting controls whether database files (.dbf) opened from an untrusted location are always opened in Protected View. If you enable this policy setting, database files opened from an untrusted location are always opened in Protected View. Users will not be able to change this setting und…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\security\protectedview\enabledatabasefileprotectedview` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000030 (Vuln V-223339)</sub>  

**Untrusted Microsoft Query files must be blocked from opening in Excel.**  
This policy setting controls whether Microsoft Query files (.iqy, oqy, .dqy, and .rqy) in an untrusted location are prevented from opening. If you enable this policy setting, Microsoft Query files in an untrusted location are prevented from opening. Users will not be able to change this setting unde…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\excel\security\external content\enableblockunsecurequeryfiles` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000029 (Vuln V-223338)</sub>  

**Updating of links in Excel must be prompted and not automatic.**  
This policy setting controls whether Excel prompts users to update automatic links, or whether the updates occur in the background with no prompt. If you enable or do not configure this policy setting, Excel will prompt users to update automatic links. In addition, the "Ask to update automatic links…  
Sets `HKCU\software\policies\microsoft\office\16.0\excel\options\binaryoptions\fupdateext_78_1` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-EX-000019 (Vuln V-223328)</sub>  

**Users must be prevented from creating new trusted locations in the Trust Center.**  
This policy setting controls whether trusted locations can be defined by users, the Office Customization Tool (OCT), and Group Policy, or if they must be defined by Group Policy alone. If you enable this policy setting, users can specify any location as a trusted location, and a computer can have a…  
Sets `HKCU\software\policies\microsoft\office\16.0\common\security\trusted locations\allow user locations` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-CO-000010 (Vuln V-223293)</sub>  

**Visio 2000-2002 Binary Drawings, Templates and Stencils must be blocked.**  
This policy setting allows you to determine whether users can open or save Visio files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open or save files. The options that can be selected are below. Note: Not all opt…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\visio\security\fileblock\visio2000files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-VI-000004 (Vuln V-223396)</sub>  

**Visio 2003-2010 Binary Drawings, Templates and Stencils must be blocked.**  
This policy setting allows you to determine whether users can open or save Visio files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open or save files. The options that can be selected are below. Note: Not all opt…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\visio\security\fileblock\visio2003files` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-VI-000005 (Vuln V-223397)</sub>  

**Visio 5.0 or earlier Binary Drawings, Templates and Stencils must be blocked.**  
This policy setting allows you to determine whether users can open or save Visio files with the format specified by the title of this policy setting. If you enable this policy setting, you can specify whether users can open or save files. The options that can be selected are below. Note: Not all opt…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\visio\security\fileblock\visio50andearlierfiles` = `2` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: Maximum-only &nbsp;·&nbsp; STIG: O365-VI-000006 (Vuln V-223398)</sub>  

**Visio must automatically disable unsigned add-ins without informing users.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\microsoft\office\16.0\visio\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-VI-000003 (Vuln V-223395)</sub>  

**When a custom action is executed that uses the Outlook object model, Outlook must automatically deny it.**  
This policy setting controls whether Outlook prompts users before executing a custom action. Custom actions add functionality to Outlook that can be triggered as part of a rule. Among other possible features, custom actions can be created that reply to messages in ways that circumvent the Outlook mo…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomcustomaction` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000020 (Vuln V-223365)</sub>  

**When a user designs a custom form in Outlook and attempts to bind an Address Information field to a combinatio…**  
This policy setting controls what happens when a user designs a custom form in Outlook and attempts to bind an Address Information field to a combination or formula custom field. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to acces…  
Sets `HKCU\Software\Policies\Microsoft\Office\16.0\outlook\security\PromptOOMFormulaAccess` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000022 (Vuln V-223367)</sub>  

**When an untrusted program attempts to gain access to a recipient field, such as the, To: field, using the Outl…**  
This policy setting controls what happens when an untrusted program attempts to gain access to a recipient field, such as the ''To:'' field, using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to access a re…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomaddressinformationaccess` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000024 (Vuln V-223369)</sub>  

**When an untrusted program attempts to programmatically access an Address Book using the Outlook object model,…**  
This policy setting controls what happens when an untrusted program attempts to gain access to an Address Book using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to programmatically access an Address Book u…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomaddressbookaccess` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000021 (Vuln V-223366)</sub>  

**When an untrusted program attempts to programmatically send e-mail in Outlook using the Response method of a t…**  
This policy setting controls what happens when an untrusted program attempts to programmatically send e-mail in Outlook using the Response method of a task or meeting request. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to programm…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoommeetingtaskrequestresponse` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000025 (Vuln V-223370)</sub>  

**When an untrusted program attempts to send e-mail programmatically using the Outlook object model, Outlook mus…**  
This policy setting controls what happens when an untrusted program attempts to send e-mail programmatically using the Outlook object model. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to send e-mail programmatically using the Outl…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomsend` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000026 (Vuln V-223371)</sub>  

**When an untrusted program attempts to use the Save As command to programmatically save an item, Outlook must a…**  
This policy setting controls what happens when an untrusted program attempts to use the Save As command to programmatically save an item. If you enable this policy setting, you can choose from four different options when an untrusted program attempts to use the Save As command to programmatically sa…  
Sets `HKCU\software\policies\microsoft\office\16.0\outlook\security\promptoomsaveas` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-OU-000023 (Vuln V-223368)</sub>  

**Word attachments opened from Outlook must be in Protected View.**  
This policy setting allows you to determine if Word files in Outlook attachments open in Protected View. If you enable this policy setting, Outlook attachments do not open in Protected View. If you disable or do not configure this policy setting, Outlook attachments open in Protected View.  
Sets `HKCU\software\policies\microsoft\office\16.0\word\security\protectedview\disableattachmentsinpv` = `0` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-WD-000006 (Vuln V-223405)</sub>  

**Word must automatically disable unsigned add-ins without informing users.**  
This policy setting controls whether the specified Office application notifies users when unsigned application add-ins are loaded or silently disable such add-ins without notification. This policy setting only applies if the "Require that application add-ins are signed by Trusted Publisher" policy s…  
Sets `HKCU\software\policies\microsoft\office\16.0\word\security\notbpromptunsignedaddin` = `1` (REG_DWORD)  
<sub>Risk: **Medium** &nbsp;·&nbsp; Profile: **Recommended** &nbsp;·&nbsp; STIG: O365-WD-000001 (Vuln V-223400)</sub>  


