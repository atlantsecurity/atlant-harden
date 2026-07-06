# Attack Surface Reduction (ASR)

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Microsoft Defender ASR rules block the specific behaviours malware relies on — Office spawning executables, scripts launching payloads, credential theft from LSASS, ransomware file patterns — at the kernel level, before code runs. They are the single highest-value anti-malware control and are almost invisible in day-to-day use._

**19 settings** on this page &mdash; **18** are part of the Recommended profile.

### Advanced Ransomware Protection

Use advanced protection against ransomware

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block Adobe Reader Child Processes

Prevent Adobe Reader from creating child processes that could be malicious

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block Credential Stealing from LSASS

Block credential stealing from Windows LSASS

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block Email Executable Content

Block executable content from email client and webmail

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block Executable Office Content

Block Office apps from creating executable content

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May prevent Office from saving certain file types

### Block Impersonated System Tools

Block executables that impersonate or copy Windows system tools

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block JS/VBS Downloaded Executables

Block JavaScript or VBScript from launching downloaded executables

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May block legitimate installers and scripts

### Block Low Prevalence Executables

Block executables that don't meet prevalence, age, or trusted list criteria

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** High  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** May block legitimate new or uncommon software

### Block Obfuscated Scripts

Block execution of potentially obfuscated scripts

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May block some legitimate PowerShell scripts

### Block Office Child Processes

Prevent Office applications from creating child processes

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break legitimate Office workflows that launch external programs

### Block Office Code Injection

Prevent Office apps from injecting code into other processes

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break Office add-ins and integrations

### Block Office Communication App Child Processes

Prevent Outlook from creating child processes to block social engineering attacks

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Medium  &middot;  **Profile:** Recommended

### Block PSExec and WMI Process Creation

Block processes created via PSExec and WMI commands to prevent lateral movement

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May block legitimate admin tools - incompatible with SCCM/ConfigMgr

### Block Safe Mode Reboot Commands

Prevent bcdedit and bootcfg from restarting machine in Safe Mode where security tools are disabled

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Medium  &middot;  **Profile:** Recommended

### Block Untrusted USB Processes

Block untrusted and unsigned processes running from USB

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Will block portable applications from USB drives

### Block Vulnerable Signed Drivers

Prevent exploitation of vulnerable signed drivers that could be used for kernel access

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block Webshell Creation for Servers

Prevent web shell script creation on Microsoft Server and Exchange

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Block Win32 API from Office Macros

Block Office macros from calling Win32 APIs

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Will break many legitimate VBA macros that interact with Windows

### Block WMI Event Subscription Persistence

Prevent malware from using WMI event subscriptions to persist on the system

- **Change:** Enables the Microsoft Defender ASR rule in **Block** mode.
- **Risk:** Medium  &middot;  **Profile:** Recommended


