# Removable Media

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Disables Autorun/Autoplay — the classic mechanism by which malware spreads automatically from USB drives and other removable media the instant they are inserted._

**3 settings** on this page &mdash; **3** are part of the Recommended profile.

### Disable Autoplay

Disable autoplay for non-volume devices

*Why it matters:* Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. This setting will disable autoplay for non-volume devices (such as Media T…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\Explorer\NoAutoplayfornonVolume` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Autorun Completely

Completely disable autorun feature

*Why it matters:* Allowing autorun commands to execute may introduce malicious code to a system. Configuring this setting prevents autorun commands from executing.

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\Explorer\NoAutorun` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Autorun for All Drives

Disable autorun/autoplay functionality

*Why it matters:* Allowing autoplay to execute may introduce malicious code to a system. Autoplay begins reading from a drive as soon as media is inserted in the drive. As a result, the setup file of programs or music on audio media may start. By default, autoplay is disabled on removable drives, such as the floppy d…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\policies\Explorer\NoDriveTypeAutoRun` = `255` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended


