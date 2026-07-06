# Logging & Auditing

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_You cannot investigate what you did not record. These settings turn on the visibility responders need after an incident: PowerShell script-block/module logging and transcription, process-creation auditing with command lines, and a larger security event log._

**12 settings** in this category &mdash; **11** are part of the Recommended profile.

### Audit Logon Events

Enable auditing for logon success and failure

- **Change:** Enables the Windows audit subcategory.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Audit Process Creation

Enable auditing for process creation

- **Change:** Enables the Windows audit subcategory.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable PowerShell Module Logging

Log PowerShell module activity

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ModuleLogging\EnableModuleLogging` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable PowerShell Module Logging

Log PowerShell module loading and pipeline execution

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ModuleLogging\EnableModuleLogging` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Enable PowerShell Script Block Logging

Log PowerShell script block execution

*Why it matters:* Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable PowerShell Script Block Logging

Log all PowerShell script blocks for security monitoring

*Why it matters:* Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\ScriptBlockLogging\EnableScriptBlockLogging` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Enable PowerShell Transcription

Enable PowerShell command transcription to files

*Why it matters:* Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable PowerShell Transcription

Enable automatic PowerShell session transcription

*Why it matters:* Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\PowerShell\Transcription\EnableTranscripting` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Enlarge Security Event Log

Increase Security event log to 1GB

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Recommended

### Force Audit Policy Subcategory

Force audit policy subcategory settings to override category settings

*Why it matters:* Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\SCENoApplyLegacyAuditPolicy` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Halt When Audit Log Full

Shut down system when security audit log is full

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\CrashOnAuditFail` = `1` (REG_DWORD)
- **Risk:** High  &middot;  **Profile:** Maximum-only  &middot;  ACSC Essential Eight
- **&#9888; Impact:** System will shut down if audit log becomes full. Ensure adequate log size and monitoring.

### Log Process Command Line

Include command line in process creation events

*Why it matters:* Maintaining an audit trail of system activity logs can help identify configuration errors, troubleshoot service disruptions, and analyze compromises that have occurred, as well as detect attacks. Audit logs are necessary to provide a trail of evidence in case the system or network is compromised. Co…

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Policies\System\Audit\ProcessCreationIncludeCmdLine_Enabled` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended


