# System Hardening

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Core OS hardening — User Account Control, exploit mitigations (SEHOP, safe DLL search order), Autorun/Autoplay, SmartScreen for downloaded files, and closing privilege-escalation holes such as AlwaysInstallElevated._

**29 settings** on this page &mdash; **18** are part of the Recommended profile.

### Always Process Group Policy

Process Group Policy objects even if they haven't changed

*Why it matters:* Enabling this setting and then selecting the "Process even if the Group Policy objects have not changed" option ensures that the policies will be reprocessed even if none have been changed. This way, any unauthorized changes are forced to match the domain-based group policy settings again.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Group Policy\{35378EAC-683F-11D2-A89A-00C04FBBCFA2}\NoGPOListChanges` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight

### Block CWD DLL Loading

Block DLL loading from current working directory (remote)

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\CWDIllegalInDllSearch` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block DMA Until User Logon

Block Direct Memory Access ports until user logs on

*Why it matters:* Kernel DMA Protection to protect PCs against drive-by Direct Memory Access (DMA) attacks using PCI hot plug devices connected to Thunderbolt 3 ports. Drive-by DMA attacks can lead to disclosure of sensitive information residing on a PC, or even injection of malware that allows attackers to bypass th…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Kernel DMA Protection\DeviceEnumerationPolicy` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Deny Execute Access on Removable Media

Prevent execution of programs from removable storage devices

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\RemovableStorageDevices\Deny_Execute` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight

### Deny Write Access to Removable Media

Prevent writing data to removable storage devices

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\RemovableStorageDevices\Deny_Write` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight
- **&#9888; Impact:** Users will not be able to write to USB drives, external hard drives, etc.

### Disable 8.3 Filename Creation

Disable short 8.3 filename creation for better security

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Always Install Elevated

Prevent installers from using elevated privileges by default

*Why it matters:* Standard user accounts must not be granted elevated privileges. Enabling Windows Installer to elevate privileges when installing applications can allow malicious persons and applications to gain full control of a system.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Installer\AlwaysInstallElevated` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable AutoRun for All Drives

Disable automatic execution features for removable media

*Why it matters:* Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Disable ClickOnce Trust Prompt

Disable ClickOnce application trust prompts

- **Change:** Sets `HKLM\SOFTWARE\MICROSOFT\.NETFramework\Security\TrustManager\PromptingLevel\Internet` = `Disabled` (REG_SZ)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Command Prompt for Users

Prevent users from accessing Command Prompt to limit attack surface

- **Change:** Sets `HKCU\SOFTWARE\Policies\Microsoft\Windows\System\DisableCMD` = `2` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight
- **&#9888; Impact:** Users will not be able to run Command Prompt. Set to 2 to still allow batch files.

### Disable Lock Screen Camera

Disable camera access while screen is locked

*Why it matters:* Enabling camera access from the lock screen could allow for unauthorized use. Requiring logon will ensure the device is only used by authorized personnel.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Personalization\NoLockScreenCamera` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### Disable Storage Sense

Prevent automatic file cleanup that could delete evidence

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\StorageSense\AllowStorageSenseGlobal` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight

### Disable Windows Script Host

Prevent VBS/JS scripts from running via WSH

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows Script Host\Settings\Enabled` = `0` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break legitimate scripts

### Enable Biometric Anti-Spoofing

Enable enhanced anti-spoofing for facial recognition

*Why it matters:* Enhanced anti-spoofing provides additional protections when using facial recognition with devices that support it.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Biometrics\FacialFeatures\EnhancedAntiSpoofing` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable PowerShell Constrained Language Mode

Restrict PowerShell to constrained language mode

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\Environment\__PSLockDownPolicy` = `4` (REG_SZ)
- **Risk:** High  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight
- **&#9888; Impact:** Many PowerShell scripts and administrative tools will not work. Test thoroughly.

### Enable Safe DLL Search Mode

Protect against DLL hijacking attacks

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\SafeDLLSearchMode` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable SEHOP

Enable Structured Exception Handling Overwrite Protection

*Why it matters:* Attackers are constantly looking for vulnerabilities in systems and applications. Structured Exception Handling Overwrite Protection (SEHOP) blocks exploits that use the Structured Exception Handling overwrite technique, a common buffer overflow attack.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Session Manager\kernel\DisableExceptionChainValidation` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable SmartScreen

Enable Windows SmartScreen filter

*Why it matters:* Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\EnableSmartScreen` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable UAC

Enable User Account Control

*Why it matters:* User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting enables UAC.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableLUA` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Virtualization Based Security

Enable application virtualization for UAC

*Why it matters:* User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures non-UAC compliant applications to run in virtualized file and registry entries in per-user locations, allowing them to run.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\EnableVirtualization` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Prevent Local Group Policy Modifications

Prevent users from modifying Local Group Policy settings

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\DisableLocalMachineRun` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight

### Require Admin for Printer Drivers

Enforce Administrator role for adding printer drivers

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Print\Providers\LanMan Print Services\Servers\AddPrinterDrivers` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Show File Extensions

Show file extensions in Windows Explorer

- **Change:** Sets `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\HideFileExt` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Show File Extensions

Always show file extensions in Windows Explorer

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\HideFileExt` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Show Hidden Files

Show hidden files and folders

- **Change:** Sets `HKCU\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Show Hidden Files and Folders

Display hidden files and folders in Windows Explorer

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\Hidden` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Show Protected Operating System Files

Display protected operating system files

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\Advanced\ShowSuperHidden` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight
- **&#9888; Impact:** System files will be visible. Be careful not to modify or delete them.

### SmartScreen Block Level

Set SmartScreen to Block mode

*Why it matters:* Microsoft Defender SmartScreen helps protect systems from programs downloaded from the internet that may be malicious. Enabling Microsoft Defender SmartScreen will warn or prevent users from running potentially malicious programs.

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\System\ShellSmartScreenLevel` = `Block` (REG_SZ)
- **Risk:** Low  &middot;  **Profile:** Maximum-only

### UAC Always Prompt

Always prompt for elevation on secure desktop

*Why it matters:* User Account Control (UAC) is a security mechanism for limiting the elevation of privileges, including administrative accounts, unless authorized. This setting configures the elevation requirements for logged on administrators to complete a task that requires raised privileges.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\ConsentPromptBehaviorAdmin` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended


