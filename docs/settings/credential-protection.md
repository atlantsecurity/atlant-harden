# Credential Protection

[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)

_Stops attackers from stealing the credentials that let them move from one machine to the whole network. These settings protect the LSASS process (where Windows holds credentials in memory), stop weak-hash and cleartext storage, and enforce modern authentication — the controls that defeat Mimikatz-style attacks._

**13 settings** on this page &mdash; **12** are part of the Recommended profile.

### Account Lockout Duration (15 minutes)

Lock account for 15 minutes after exceeding threshold

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Account Lockout Reset Window (15 minutes)

Reset account lockout counter after 15 minutes

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Account Lockout Threshold (5 attempts)

Lock account after 5 invalid logon attempts

- **Change:** Applies the configured system change.
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Block Blank Password Network Logons

Prevent local accounts with blank passwords from network logon

*Why it matters:* An account without a password can allow unauthorized access to a system as only the username would be required. Password policies must prevent accounts with blank passwords from existing on a system. However, if a local account with a blank password did exist, enabling this setting will prevent netw…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LimitBlankPasswordUse` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable Domain Credential Caching

Prevent storage of credentials for network authentication

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\DisableDomainCreds` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Maximum-only
- **&#9888; Impact:** May affect domain authentication in some scenarios

### Disable LM Hash Storage

Do not store LAN Manager hash value on next password change

*Why it matters:* The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable WDigest Authentication

Prevent storing credentials in memory (cleartext)

*Why it matters:* When the WDigest Authentication protocol is enabled, plain text passwords are stored in the Local Security Authority Subsystem Service (LSASS) exposing them to theft. WDigest is disabled by default in Windows 11. This setting ensures this is enforced.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest\UseLogonCredential` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Disable WDigest Negotiation

Disable WDigest negotiate protocol

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\SecurityProviders\WDigest\Negotiate` = `0` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Do Not Store LM Hash

Prevent storage of LAN Manager hash on next password change

*Why it matters:* The LAN Manager hash uses a weak encryption algorithm and there are several tools available that use this hash to retrieve account passwords. This setting controls whether or not a LAN Manager hash of the password is stored in the SAM the next time the password is changed.

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\NoLMHash` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended  &middot;  ACSC Essential Eight

### Enable LSASS Audit Mode

Audit access to LSASS for security monitoring

- **Change:** Sets `HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\LSASS.exe\AuditLevel` = `8` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enable Protected Credentials

Allow delegation of non-exported credentials

*Why it matters:* An exportable version of credentials is provided to remote hosts when using credential delegation which exposes them to theft on the remote host. Restricted Admin mode or Remote Credential Guard allow delegation of non-exportable credentials providing additional protection of the credentials. Enabli…

- **Change:** Sets `HKLM\SOFTWARE\Policies\Microsoft\Windows\CredentialsDelegation\AllowProtectedCreds` = `1` (REG_DWORD)
- **Risk:** Low  &middot;  **Profile:** Recommended

### Enforce NTLMv2 Only

Set LAN Manager authentication level to NTLMv2 only

*Why it matters:* The Kerberos v5 authentication protocol is the default for authentication of users who are logging on to domain accounts. NTLM, which is less secure, is retained in later Windows versions for compatibility with clients and servers that are running earlier versions of Windows or applications that sti…

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\LmCompatibilityLevel` = `5` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended
- **&#9888; Impact:** May affect legacy system compatibility

### LSASS Protected Process

Run LSASS as a Protected Process Light (PPL)

- **Change:** Sets `HKLM\SYSTEM\CurrentControlSet\Control\Lsa\RunAsPPL` = `1` (REG_DWORD)
- **Risk:** Medium  &middot;  **Profile:** Recommended  &middot;  Reboot required


