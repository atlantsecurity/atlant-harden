# Microsoft Defender Antivirus

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Tunes the built-in antivirus itself — cloud-delivered protection, network protection, PUA (potentially unwanted application) blocking and sandboxing — so it catches more, faster, and cannot be casually paused._

**9 settings** on this page &mdash; **6** are part of the Recommended profile.

### Disable Pause Windows Defender Scan

Prevent users from pausing Windows Defender scans

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\AllowPause` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Enable Cloud-Delivered Protection

Enable cloud-based protection for better threat detection

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Controlled Folder Access

Protect important folders from ransomware and malicious apps

- **Change:** Applies the configured system change.
- **Risk:** High  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** May block legitimate applications. Whitelist apps as needed.

### Enable Defender Sandbox

Run Windows Defender in a sandbox for better security

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  Reboot required

### Enable Network Protection

Block connections to malicious IP addresses and domains

- **Change:** Applies the configured system change.
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May occasionally block legitimate websites

### Enable PUA Protection

Enable detection of Potentially Unwanted Applications

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Extended Cloud Check Timeout

Extend cloud check timeout to 50 seconds

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Recommended

### High Cloud Block Level

Set cloud block level to high for aggressive protection

- **Change:** Applies the configured system change.
- **Risk:** Medium  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** May cause false positives

### Send All Samples to Microsoft

Automatically send suspicious samples for analysis

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Maximum-only


