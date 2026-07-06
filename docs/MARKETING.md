# AtlantHarden

### Free Windows 11, Browser & Office Hardening Tool

**One click. 599 security settings. 354 DISA STIG controls. And — finally — hardening that doesn't break your machine.**

Free to use · No account · No telemetry · Self-contained download (no .NET install required)

---

## Windows ships insecure by default

Every fresh Windows install trades security for convenience. Legacy SMBv1. Cleartext credentials cached in memory. PowerShell with no logging. Browsers that ignore enterprise policy. Office that runs macros from the internet. Fixing it by hand means hundreds of registry keys, Group Policy objects, and PowerShell commands — and one wrong value away from a system that won't boot.

AtlantHarden does all of it in one click, with a full backup taken before every change.

## …but most hardening tools overcorrect and cripple your PC

Here's the part nobody talks about. Blindly applying a DISA STIG or CIS benchmark to a personal or power-user machine **wrecks it**: it disables your browser's password manager, kills InPrivate/Incognito, blocks you from deleting your own history, turns on Controlled Folder Access that blocks your apps from saving, enables FIPS mode that breaks software, and demands a BitLocker PIN on every boot — all for compliance checkboxes that add little real-world security.

**AtlantHarden v2.0 is built around a smarter idea:** stop how malware and attackers *actually* get in and run, and skip the friction that doesn't stop them.

> If your browser gets exploited, the attacker reads your saved passwords whether or not the password manager is "disabled." So AtlantHarden spends its effort *preventing the exploitation* — site isolation, SmartScreen, exploit mitigations, credential-theft blocking — and leaves your password manager working.

---

## At a glance

| | |
|---|---|
| **599 hardening settings** | Registry, PowerShell, firewall, file associations, audit policy, ASR rules |
| **354 DISA STIG controls** | Windows 11, Edge, Chrome, Firefox & Office 365 — the *latest* releases, each tagged with its STIG ID, Vulnerability ID, and CCIs |
| **34 ACSC Essential Eight** | Aligned to the Australian Cyber Security Centre's Windows guidance |
| **3 one-click profiles** | Basic, Recommended, Maximum — review every setting before you apply |
| **Full backup + restore** | Automatic pre-change backup, `.reg` export, and System Restore integration |
| **Self-contained** | Single `.exe`, no .NET runtime to install, runs on any Windows 10/11 x64 |
| **Free to use** | No account, no telemetry, no upsell — charging for the app's use requires a commercial license |

---

## Three profiles. You decide how far to go.

Every profile has an **Apply** button *and* a **Show settings** button — so you can scroll through exactly what it changes (name, plain-English description, the registry value, current vs. recommended) **before** anything happens.

### 🛡️ Basic — *95 settings*
The highest-impact, effectively zero-friction core: Attack Surface Reduction, Microsoft Defender, SmartScreen, macro and script blocking, credential-theft protection, UAC, and security logging. If you do nothing else, do this.

### ⚡ Recommended — *325 settings* · **the smart default**
The profile most people will click — and the reason AtlantHarden exists. It applies the controls that stop real malware and exploitation:

- **Anti-malware execution** — 18 Attack Surface Reduction rules + Microsoft Defender cloud, network & PUA protection
- **Anti-exploitation** — browser site isolation, SmartScreen / Safe Browsing, SEHOP, DEP/ASLR, DMA protection
- **Document & script malware** — Office macro-from-internet blocking, Protected View, and neutralized `.js/.vbs/.hta/.scr` file types
- **Credential theft** — LSASS protection, WDigest off, NoLMHash, NTLMv2-only, SMB signing
- **Legacy protocol & network poisoning** — SMBv1 off, LLMNR/NBT-NS/WPAD off, LDAP signing
- **Visibility** — PowerShell script-block logging and process command-line auditing

…and it **deliberately leaves out** the things that cripple a real machine: it does **not** disable your password manager, InPrivate/Incognito, or history deletion; it does **not** turn on Controlled Folder Access, FIPS, Constrained Language Mode, or a BitLocker pre-boot PIN. It's already **gaming- and performance-safe** — no Virtualization-Based Security, no anti-cheat conflicts.

### 🔒 Maximum — *599 settings*
Everything, including the strict DISA STIG lockdowns. Full compliance for high-security and audited environments that understand — and want — the friction.

---

## DISA STIG compliance — current to the latest releases

AtlantHarden ships **354 automatable DISA STIG requirements** across five products, each in its own dedicated category, applying DISA's exact mandated value:

| Product | STIG Version | Requirements |
|---------|--------------|--------------|
| Microsoft Windows 11 | V2R7 | 114 |
| Microsoft Edge | V2R5 | 52 |
| Google Chrome | V2R11 | 39 |
| Mozilla Firefox | V6R7 | 43 |
| Microsoft Office 365 ProPlus | V3R5 | 106 |

- Every setting is tagged with its **real STIG ID** (`WN11-SO-000195`, `EDGE-00-000002`, `DTBC-0001`, `FFOX-00-000002`, `O365-AC-000001`), **Vulnerability ID**, and **CCIs** — cross-reference straight against the checklist.
- **Per-product compliance dashboard** with live percentage scores.
- **Filter and bulk-select by CAT I / II / III** severity within any product.
- Sourced from an **auditable, regenerable catalog** built from Microsoft PowerSTIG and the published DISA benchmarks — refreshed each quarter, not hand-typed.

Plus **34 ACSC Essential Eight** settings (Australian Cyber Security Centre, July 2024) with their own live compliance score and priority levels.

---

## What it actually blocks

**19 Attack Surface Reduction rules** — Office apps spawning child processes, credential theft from LSASS, ransomware behavior, obfuscated scripts, executables from email, JS/VBS droppers, WMI persistence, PSExec/WMI process creation, vulnerable signed drivers, and more.

**LOLBin firewall rules** — block Living-off-the-Land binaries (certutil, mshta, wscript, regsvr32, wmic…) from reaching the network, killing the download-and-C2 cradles attackers rely on.

**File association neutralization** — dangerous script extensions (`.js`, `.vbs`, `.hta`, `.wsf`, `.scr`, `.chm`…) open in Notepad instead of executing, while leaving `.bat`, `.ps1`, and `.reg` runnable for power users.

**Browser hardening across three engines** — Edge, Chrome, and Firefox get site isolation, SmartScreen / Safe Browsing, TLS enforcement, certificate revocation, and DNS-over-HTTPS.

**The PowerShell logging triad** — script-block logging, module logging, and transcription for full command visibility.

**Credential protection** — LSASS run as a protected process, WDigest disabled, NTLMv2 enforced, LM hashes never stored — the controls that defeat Mimikatz-style attacks.

---

## It won't lock itself out

A hardening tool that's so aggressive you can't run it again to *undo* the damage is worthless. Before applying anything, AtlantHarden registers itself as an allowed app for Attack Surface Reduction and Controlled Folder Access, and keeps Explorer SmartScreen at an overridable level — so you can always relaunch it. And if you ever do lock yourself out, every change can be reverted **without** the app:

- **Automatic backup before every apply** — stores the exact original value of each key
- **`.reg` export** — double-click to restore outside the app
- **System Restore Point integration** — create and manage restore points from the app
- **One-click restore** with per-entry success/failure reporting
- Up to **20 timestamped backups** kept locally

---

## Built for the desktop *and* the fleet

**Review-first interface** — a dark dashboard with a live security score, per-setting risk badges, impact warnings, search, CAT-severity filters, a collapsible sidebar, and a per-product STIG compliance breakdown.

**Silent deployment** — push a gold-image policy across every machine with no GUI:

```
AtlantHarden.exe --config policy.json --apply --silent
```

**Configuration import / export** — build your policy once in the GUI, export it as portable JSON, version-control it, and deploy it everywhere. Exit codes (`0` success, `1` error) for clean script integration.

**One-click HTML security report** — overall score, per-category breakdown, every setting with current vs. recommended value and status, plus STIG and ACSC metrics. Timestamped and audit-ready.

---

## Technical details

| | |
|---|---|
| **Platform** | Windows 10 (1709+) / Windows 11, 64-bit |
| **Privileges** | Administrator (it changes system-level security settings) |
| **Runtime** | Self-contained — **no .NET install required** |
| **Distribution** | Single signed-ready `.exe`, ~68 MB |
| **Backup formats** | `.reg` (Windows Registry) + JSON |
| **Config format** | JSON |
| **Price** | Free to use; charging for the app's use (paid service / for clients) requires a commercial license |

---

## Get started in two minutes

1. **Download** AtlantHarden and extract the zip.
2. **Right-click `AtlantHarden.exe` → Run as administrator.** (It's not yet code-signed, so SmartScreen may prompt — choose *More info → Run anyway*.)
3. **Create a System Restore Point** from the Backup tab — always recommended before your first run.
4. **Click "Show settings"** on the Recommended profile to see exactly what will change.
5. **Apply.** A backup is created automatically.

---

> **The bottom line:** AtlantHarden gives you genuine DISA-STIG-grade protection and a security baseline that stops the malware, exploits, and credential theft that actually happen — without the compliance theater that turns your PC into a brick. Comprehensive when you want it. Sensible by default.

**AtlantHarden v2.0** — by [Atlant Security](https://atlantsecurity.com) · Free Windows hardening for everyone.
