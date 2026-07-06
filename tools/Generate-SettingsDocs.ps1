<#
  Generates human-readable, per-setting documentation for the Recommended and Maximum
  profiles from the dumped settings data (_settings.json produced by reflection over the
  built assembly). Writes two Markdown reference docs.
#>
$ErrorActionPreference = 'Stop'
$data = Get-Content 'C:\P\winh\_settings.json' -Raw | ConvertFrom-Json

function Norm($path, $key) {
    $p = ("$path" -replace 'HKEY_LOCAL_MACHINE','HKLM' -replace 'HKEY_CURRENT_USER','HKCU').TrimEnd('\')
    return ("$p|$key").ToUpperInvariant()
}

# Map (path|key) -> STIG rationale, to enrich terse baseline settings that have a STIG twin.
$stigWhy = @{}
foreach ($s in $data | Where-Object { $_.IsStig -and $_.Path }) {
    $k = Norm $s.Path $s.Key
    if (-not $stigWhy.ContainsKey($k) -and $s.Description) { $stigWhy[$k] = $s.Description }
}

# Ordered categories: friendly title + threat/why intro.
$cat = [ordered]@{
 'AttackSurfaceReduction' = @{ t='Attack Surface Reduction (ASR)'; why='Microsoft Defender ASR rules block the specific behaviours malware relies on — Office spawning executables, scripts launching payloads, credential theft from LSASS, ransomware file patterns — at the kernel level, before code runs. They are the single highest-value anti-malware control and are almost invisible in day-to-day use.' }
 'WindowsDefender' = @{ t='Microsoft Defender Antivirus'; why='Tunes the built-in antivirus itself — cloud-delivered protection, network protection, PUA (potentially unwanted application) blocking and sandboxing — so it catches more, faster, and cannot be casually paused.' }
 'CredentialProtection' = @{ t='Credential Protection'; why='Stops attackers from stealing the credentials that let them move from one machine to the whole network. These settings protect the LSASS process (where Windows holds credentials in memory), stop weak-hash and cleartext storage, and enforce modern authentication — the controls that defeat Mimikatz-style attacks.' }
 'NetworkSecurity' = @{ t='Network Security'; why='Removes the legacy and insecure network behaviour attackers abuse for interception and lateral movement: SMBv1 (EternalBlue/WannaCry), name-resolution poisoning (LLMNR/NetBIOS/WPAD), unsigned SMB/LDAP traffic, and anonymous enumeration of accounts and shares.' }
 'SystemHardening' = @{ t='System Hardening'; why='Core OS hardening — User Account Control, exploit mitigations (SEHOP, safe DLL search order), Autorun/Autoplay, SmartScreen for downloaded files, and closing privilege-escalation holes such as AlwaysInstallElevated.' }
 'TLSCrypto' = @{ t='TLS & Cryptography'; why='Disables broken and obsolete cryptography (SSL 2.0/3.0, TLS 1.0/1.1, RC4, DES, 3DES) at the Schannel level and enforces TLS 1.2 with modern cipher suites, so the machine cannot be downgraded onto weak encryption.' }
 'OfficeHardening' = @{ t='Office Hardening'; why='Office documents are the single most common malware-delivery vehicle. These settings disable the features attackers weaponise — VBA macros, Dynamic Data Exchange (DDE) and ActiveX — and block macros carried in files that came from the internet.' }
 'FileAssociations' = @{ t='File Association Neutralisation'; why='Makes script and "scrap" file types that are pure malware-delivery vectors (.js, .vbs, .hta, .wsf, .scr, .chm, ...) open in Notepad instead of executing when double-clicked. Types power users legitimately run (.bat, .ps1, .reg) are deliberately left runnable.' }
 'Firewall' = @{ t='Windows Firewall — LOLBin Blocking'; why='Blocks Living-off-the-Land binaries (LOLBins) — trusted Windows tools such as certutil, mshta, wscript and regsvr32 that attackers abuse to download payloads and reach command-and-control — from making outbound network connections.' }
 'Logging' = @{ t='Logging & Auditing'; why='You cannot investigate what you did not record. These settings turn on the visibility responders need after an incident: PowerShell script-block/module logging and transcription, process-creation auditing with command lines, and a larger security event log.' }
 'RemovalMedia' = @{ t='Removable Media'; why='Disables Autorun/Autoplay — the classic mechanism by which malware spreads automatically from USB drives and other removable media the instant they are inserted.' }
 'Privacy' = @{ t='Privacy'; why='Reduces the data Windows sends to Microsoft and third parties — telemetry, advertising ID, location, Cortana/Bing and consumer "suggestions". These are privacy improvements rather than anti-malware controls, which is why they are excluded from the Recommended profile.' }
 'EdgeHardening' = @{ t='Microsoft Edge Hardening'; why='Enterprise-policy hardening for Microsoft Edge. The high-value controls prevent exploitation and phishing — site isolation, SmartScreen, TLS enforcement and certificate checks — rather than stripping convenience features.' }
 'ChromeHardening' = @{ t='Google Chrome Hardening'; why='Enterprise-policy hardening for Google Chrome — site isolation, Enhanced Safe Browsing, TLS 1.3 hardening, DNS-over-HTTPS and certificate revocation checks.' }
 'FirefoxHardening' = @{ t='Mozilla Firefox Hardening'; why='Enterprise-policy hardening for Mozilla Firefox — TLS floor, DNS-over-HTTPS, and tracking protection.' }
 'AdobeReader' = @{ t='Adobe Acrobat / Reader'; why='Applies Adobe reader hardening — Protected Mode/View sandboxing, Enhanced Security and disabling JavaScript — to blunt the malicious-PDF attacks that target the reader.' }
 'StigWindows11' = @{ t='DISA STIG — Microsoft Windows 11 (V2R7)'; why='The Windows 11 Security Technical Implementation Guide is DISA''s authoritative hardening baseline for U.S. Department of Defense systems. Every item below is a formal STIG requirement with its own STIG ID, Vulnerability ID and CCIs, applying DISA''s exact mandated value.' }
 'StigEdge' = @{ t='DISA STIG — Microsoft Edge (V2R5)'; why='The full DISA Microsoft Edge STIG. Many of these are strict lockdowns (disabling sync, InPrivate, imports, autofill) that go beyond exploitation prevention and add day-to-day friction — which is why only the exploitation-relevant ones appear in the Recommended profile.' }
 'StigChrome' = @{ t='DISA STIG — Google Chrome (V2R11)'; why='The full DISA Google Chrome STIG, including strict policy lockdowns beyond the exploitation-prevention subset used by the Recommended profile.' }
 'StigFirefox' = @{ t='DISA STIG — Mozilla Firefox (V6R7)'; why='The full DISA Mozilla Firefox STIG, including strict policy lockdowns beyond the exploitation-prevention subset used by the Recommended profile.' }
 'StigOffice365' = @{ t='DISA STIG — Microsoft Office 365 ProPlus (V3R5)'; why='The full DISA Office 365 STIG — overwhelmingly high-value anti-document-malware controls (macro blocking, Protected View, ActiveX/DDE hardening, unsigned add-in blocking). The Recommended profile keeps these but omits the legacy file-format blocks that would stop old .doc/.xls/.ppt files from opening.' }
}

function ChangeLine($s) {
    if ($s.ASR) { return "Enables the Microsoft Defender ASR rule in **Block** mode." }
    switch ($s.Category) {
        'Firewall'         { return "Creates a Windows Firewall rule blocking the binary's outbound network access." }
        'FileAssociations' { return "Re-associates the file extension to open as text (Notepad) instead of executing." }
    }
    if ($s.Path -and $s.Key) { return "Sets ``$($s.Path)\$($s.Key)`` = ``$($s.Value)`` ($($s.ValType))" }
    if ($s.Type -eq 'AuditPolicy') { return "Enables the Windows audit subcategory." }
    return "Applies the configured system change."
}

function RenderSetting($s, [bool]$showProfile) {
    $sb = [System.Text.StringBuilder]::new()
    $name = ($s.Name -replace '\s+', ' ').Trim()
    if ($name.Length -gt 130) { $name = $name.Substring(0,130).TrimEnd() + '...' }   # keep headings sane
    [void]$sb.AppendLine("### $name")
    [void]$sb.AppendLine()
    # What it does (+ enriched why for terse baseline settings that have a STIG twin)
    $desc = ($s.Description -replace '\s+', ' ').Trim()
    if ($desc) { [void]$sb.AppendLine($desc); [void]$sb.AppendLine() }
    if (-not $s.IsStig -and $s.Path -and $desc.Length -lt 150) {
        $twin = $stigWhy[(Norm $s.Path $s.Key)]
        if ($twin) {
            $twin = ($twin -replace '\s+', ' ').Trim()
            if ($twin.Length -gt 340) { $twin = $twin.Substring(0,340).TrimEnd() + '...' }
            [void]$sb.AppendLine("*Why it matters:* $twin")
            [void]$sb.AppendLine()
        }
    }
    # Details as a clean bullet list (renders reliably on GitHub)
    [void]$sb.AppendLine("- **Change:** " + (ChangeLine $s))
    $meta = "- **Risk:** $($s.Risk)"
    if ($showProfile) { $meta += if ($s.Rec) { "  &middot;  **Profile:** Recommended" } else { "  &middot;  **Profile:** Maximum-only" } }
    if ($s.IsStig -and $s.StigId) { $meta += "  &middot;  **STIG:** $($s.StigId)"; if ($s.VulnId) { $meta += " (Vuln $($s.VulnId))" } }
    if ($s.IsACSC) { $meta += "  &middot;  ACSC Essential Eight" }
    if ($s.Reboot) { $meta += "  &middot;  Reboot required" }
    [void]$sb.AppendLine($meta)
    if ($s.Impact) { [void]$sb.AppendLine("- **&#9888; Impact:** " + (($s.Impact -replace '\s+',' ').Trim())) }
    [void]$sb.AppendLine()
    return $sb.ToString()
}

function Slug($catKey) {
    return (($cat[$catKey].t -replace '[^a-zA-Z0-9]+','-').Trim('-')).ToLower()
}

# One small, self-contained page per category. GitHub will not render a single
# 150-300 KB markdown file (it shows raw source), so the full set is split so each
# page renders. Every page lists ALL settings in that category, tagged Recommended
# vs Maximum-only.
function WriteCategoryPages($setDir) {
    if (Test-Path $setDir) { Remove-Item (Join-Path $setDir '*.md') -Force -ErrorAction SilentlyContinue }
    New-Item -ItemType Directory -Force -Path $setDir | Out-Null
    foreach ($ck in $cat.Keys) {
        $grp = @($data | Where-Object { $_.Category -eq $ck } | Sort-Object Name)
        if ($grp.Count -eq 0) { continue }
        $recN = @($grp | Where-Object { $_.Rec }).Count
        $sb = [System.Text.StringBuilder]::new()
        [void]$sb.AppendLine("# $($cat[$ck].t)")
        [void]$sb.AppendLine()
        [void]$sb.AppendLine("[&larr; Recommended index](../Recommended-Settings-Explained.md) &nbsp;&middot;&nbsp; [Maximum index](../Maximum-Settings-Explained.md)")
        [void]$sb.AppendLine()
        [void]$sb.AppendLine("_$($cat[$ck].why)_")
        [void]$sb.AppendLine()
        [void]$sb.AppendLine("**$($grp.Count) settings** in this category &mdash; **$recN** are part of the Recommended profile.")
        [void]$sb.AppendLine()
        foreach ($s in $grp) { [void]$sb.Append((RenderSetting $s $true)) }
        Set-Content -Path (Join-Path $setDir ("{0}.md" -f (Slug $ck))) -Value $sb.ToString() -Encoding utf8
    }
    return @(Get-ChildItem $setDir -Filter *.md).Count
}

# Small index page (renders fine) linking to each per-category page.
function WriteIndex([bool]$recOnly, $title, $preamble, $outFile) {
    $sb = [System.Text.StringBuilder]::new()
    [void]$sb.AppendLine("# $title")
    [void]$sb.AppendLine()
    [void]$sb.AppendLine($preamble)
    [void]$sb.AppendLine()
    [void]$sb.AppendLine("The full set is documented **one category per page** — click a category for its complete, setting-by-setting explanation (what each does and why).")
    [void]$sb.AppendLine()
    $col = if ($recOnly) { 'Recommended settings' } else { 'Settings' }
    [void]$sb.AppendLine("| Category | $col |")
    [void]$sb.AppendLine("|---|---:|")
    foreach ($ck in $cat.Keys) {
        $grp = @($data | Where-Object { $_.Category -eq $ck })
        $n = if ($recOnly) { @($grp | Where-Object { $_.Rec }).Count } else { $grp.Count }
        if ($n -le 0) { continue }
        [void]$sb.AppendLine("| [$($cat[$ck].t)](settings/$(Slug $ck).md) | $n |")
    }
    [void]$sb.AppendLine()
    $tot = if ($recOnly) { @($data | Where-Object { $_.Rec }).Count } else { @($data).Count }
    [void]$sb.AppendLine("**Total: $tot settings.**")
    if (-not $recOnly) { [void]$sb.AppendLine(); [void]$sb.AppendLine("On each category page, every setting is tagged **Recommended** or **Maximum-only**.") }
    Set-Content -Path $outFile -Value $sb.ToString() -Encoding utf8
    Write-Output "Wrote index: $outFile"
}

$recPre = @'
This document explains **every setting applied by the Recommended profile**, one by one — what it changes and why it matters.

**The Recommended profile is engineered for return-on-investment:** it applies the controls that stop how malware and attackers *actually* get in and run, and deliberately leaves out high-friction "compliance theatre" that breaks everyday use without stopping real attacks. It does **not** disable your browser password manager, InPrivate/Incognito, or history deletion; it does **not** enable Controlled Folder Access, FIPS mode, PowerShell Constrained Language Mode, or a BitLocker pre-boot PIN. It is already gaming- and performance-safe.

Every change is backed up before it is applied and is fully reversible. Settings are grouped by category; each category opens with the threat it addresses.
'@

$maxPre = @'
This document is the **complete reference for every setting in AtlantHarden** — the full set the **Maximum** profile applies. Each entry shows what it changes, why it matters, the exact system change, and its risk level. The **Profile** line on each setting shows whether it is also part of the curated **Recommended** profile or is **Maximum-only** (an additional strict lockdown).

**Maximum applies everything, including the full DISA STIG lockdowns.** That delivers maximum compliance but *will* add friction — password managers disabled, InPrivate/Incognito off, Controlled Folder Access, legacy file-format blocks, and more. Use it only where you want and understand that trade-off. Every change is backed up automatically; if a setting ever prevents the tool from relaunching, revert with the exported `restore_*.reg` file or Windows System Restore.
'@

$pages = WriteCategoryPages 'C:\P\winh\settings'
WriteIndex $true  'AtlantHarden — Recommended Settings, Explained' $recPre 'C:\P\winh\Recommended-Settings-Explained.md'
WriteIndex $false 'AtlantHarden — Maximum (All) Settings, Explained' $maxPre 'C:\P\winh\Maximum-Settings-Explained.md'
Write-Output "Wrote $pages per-category pages to C:\P\winh\settings"
