# Office Hardening

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Office documents are the single most common malware-delivery vehicle. These settings disable the features attackers weaponise — VBA macros, Dynamic Data Exchange (DDE) and ActiveX — and block macros carried in files that came from the internet._

**8 settings** on this page &mdash; **8** are part of the Recommended profile.

### Block Macros from Internet

Block macros from running in files from the internet

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Common\Security\blockcontentexecutionfrominternet` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable All ActiveX

Disable all ActiveX controls in Office

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Office\Common\Security\DisableAllActiveX` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** Will disable all ActiveX content in Office documents

### Disable Excel DDE

Disable Dynamic Data Exchange in Excel

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Excel\Security\DataConnectionWarnings` = `2` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Excel VBA Macros

Disable all VBA macros in Excel

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Excel\Security\VBAWarnings` = `4` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break legitimate Excel macros

### Disable Outlook VBA Macros

Disable all VBA macros in Outlook

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Outlook\Security\Level` = `4` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended

### Disable PowerPoint VBA Macros

Disable all VBA macros in PowerPoint

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\PowerPoint\Security\VBAWarnings` = `4` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break legitimate PowerPoint macros

### Disable Word DDE

Disable Dynamic Data Exchange in Word

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Word\Security\AllowDDE` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Word VBA Macros

Disable all VBA macros in Word

- **Change:** Sets `HKCU\SOFTWARE\Microsoft\Office\16.0\Word\Security\VBAWarnings` = `4` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May break legitimate Word macros


