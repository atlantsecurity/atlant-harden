# AtlantHarden — Maximum (All) Settings, Explained

This document is the **complete reference for every setting in AtlantHarden** — the full set the **Maximum** profile applies. Each entry shows what it changes, why it matters, the exact system change, and its risk level. The **Profile** line on each setting shows whether it is also part of the curated **Recommended** profile or is **Maximum-only** (an additional strict lockdown).

**Maximum applies everything, including the full DISA STIG lockdowns.** That delivers maximum compliance but *will* add friction — password managers disabled, InPrivate/Incognito off, Controlled Folder Access, legacy file-format blocks, and more. Use it only where you want and understand that trade-off. Every change is backed up automatically; if a setting ever prevents the tool from relaunching, revert with the exported `restore_*.reg` file or Windows System Restore.

The full set is documented **one page per category** — click a page for its complete, setting-by-setting explanation (what each does and why). Large categories are split into parts.

| Page | Settings |
|---|---:|
| [Attack Surface Reduction (ASR)](settings/attack-surface-reduction-asr.md) | 19 |
| [Microsoft Defender Antivirus](settings/microsoft-defender-antivirus.md) | 9 |
| [Credential Protection](settings/credential-protection.md) | 13 |
| [Network Security](settings/network-security.md) | 30 |
| [System Hardening](settings/system-hardening.md) | 29 |
| [TLS & Cryptography](settings/tls-cryptography.md) | 10 |
| [Office Hardening](settings/office-hardening.md) | 8 |
| [File Association Neutralisation](settings/file-association-neutralisation.md) | 24 |
| [Windows Firewall — LOLBin Blocking](settings/windows-firewall-lolbin-blocking.md) | 10 |
| [Logging & Auditing](settings/logging-auditing.md) | 12 |
| [Removable Media](settings/removable-media.md) | 3 |
| [Privacy](settings/privacy.md) | 12 |
| [Microsoft Edge Hardening](settings/microsoft-edge-hardening.md) | 12 |
| [Google Chrome Hardening](settings/google-chrome-hardening.md) | 18 |
| [Mozilla Firefox Hardening](settings/mozilla-firefox-hardening.md) | 10 |
| [Adobe Acrobat / Reader](settings/adobe-acrobat-reader.md) | 26 |
| [DISA STIG — Microsoft Windows 11 (V2R7) &mdash; Part 1 of 2](settings/disa-stig-microsoft-windows-11-v2r7-part-1.md) | 57 |
| [DISA STIG — Microsoft Windows 11 (V2R7) &mdash; Part 2 of 2](settings/disa-stig-microsoft-windows-11-v2r7-part-2.md) | 57 |
| [DISA STIG — Microsoft Edge (V2R5)](settings/disa-stig-microsoft-edge-v2r5.md) | 52 |
| [DISA STIG — Google Chrome (V2R11)](settings/disa-stig-google-chrome-v2r11.md) | 39 |
| [DISA STIG — Mozilla Firefox (V6R7)](settings/disa-stig-mozilla-firefox-v6r7.md) | 43 |
| [DISA STIG — Microsoft Office 365 ProPlus (V3R5) &mdash; Part 1 of 2](settings/disa-stig-microsoft-office-365-proplus-v3r5-part-1.md) | 53 |
| [DISA STIG — Microsoft Office 365 ProPlus (V3R5) &mdash; Part 2 of 2](settings/disa-stig-microsoft-office-365-proplus-v3r5-part-2.md) | 53 |

**Total: 599 settings.**

On each page, every setting is tagged **Recommended** or **Maximum-only**.

