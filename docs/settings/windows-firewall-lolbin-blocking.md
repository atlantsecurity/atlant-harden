# Windows Firewall — LOLBin Blocking

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Blocks Living-off-the-Land binaries (LOLBins) — trusted Windows tools such as certutil, mshta, wscript and regsvr32 that attackers abuse to download payloads and reach command-and-control — from making outbound network connections._

**10 settings** in this category &mdash; **9** are part of the Recommended profile.

### Block certutil.exe Network Access

Block certutil.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break certificate operations and some installers

### Block cmstp.exe Network Access

Block cmstp.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Rarely affects normal use

### Block cscript.exe Network Access

Block cscript.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break administrative scripts

### Block mshta.exe Network Access

Block mshta.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break some legacy applications

### Block msiexec.exe Network Access

Block msiexec.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** May break software installation from network

### Block powershell_ise.exe Network Access

Block powershell_ise.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break PowerShell development

### Block regsvr32.exe Network Access

Block regsvr32.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break some software installations

### Block rundll32.exe Network Access

Block rundll32.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break various Windows functions

### Block wmic.exe Network Access

Block wmic.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break system administration tools

### Block wscript.exe Network Access

Block wscript.exe from making outbound network connections

- **Change:** Creates a Windows Firewall rule blocking the binary's outbound network access.
- **Risk:** High  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break administrative scripts


