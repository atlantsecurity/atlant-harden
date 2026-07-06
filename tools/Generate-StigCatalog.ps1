<#
.SYNOPSIS
    Generates Resources/stig-catalog.json for AtlantSecurityHardening from authoritative
    DISA STIG data: Microsoft PowerSTIG (parsed registry rules) enriched with cyber.trackr.live
    (human STIG IDs, severity, CCIs), merged on Vulnerability ID.

.DESCRIPTION
    Reads the pinned PowerSTIG processed XMLs in tools/stig-source/, selects automatable
    registry rules (OrganizationValueRequired=False AND non-empty ValueData), enriches each
    via the trackr.live API (cached to tools/.cache/), maps each rule to the app's
    SettingCategory, and writes a single embedded JSON catalog.

    To refresh for a future quarterly STIG release:
      1. Update $Products below with the new PowerSTIG filenames + version/release.
      2. Drop the new PowerSTIG XMLs into tools/stig-source/ (raw.githubusercontent.com/
         microsoft/PowerStig/dev/source/StigData/Processed/<file>).
      3. Re-run this script. Bump <Version> in AtlantHarden.csproj.

.NOTES
    No third-party modules required. Network access to cyber.trackr.live is used for STIG-ID
    enrichment; on failure the generator falls back to the Vulnerability ID and PowerSTIG data.
#>
[CmdletBinding()]
param(
    [switch]$NoNetwork,                 # skip trackr.live; use V-IDs + PowerSTIG fields only
    [int]$ThrottleMs = 150              # politeness delay between live API calls
)

$ErrorActionPreference = 'Stop'
$ProgressPreference     = 'SilentlyContinue'

$ToolsDir   = $PSScriptRoot
$ProjectDir = Split-Path $ToolsDir -Parent
$SourceDir  = Join-Path $ToolsDir 'stig-source'
$CacheDir   = Join-Path $ToolsDir '.cache'
$OutFile    = Join-Path $ProjectDir 'Resources\stig-catalog.json'
New-Item -ItemType Directory -Force -Path $CacheDir, (Split-Path $OutFile -Parent) | Out-Null

# --- Product configuration: PowerSTIG file <-> trackr.live API coordinates -----------------
$Products = @(
    @{ Key='Windows11'; Name='Microsoft Windows 11';        File='WindowsClient-11-2.7.xml'; Api='Windows_11';                    Ver='2'; Rel='7'; DefaultCategory='SystemHardening' }
    @{ Key='Edge';      Name='Microsoft Edge';              File='MS-Edge-2.5.xml';          Api='Edge';                          Ver='2'; Rel='5'; DefaultCategory='EdgeHardening'   }
    @{ Key='Chrome';    Name='Google Chrome';               File='Google-Chrome-2.11.xml';   Api='Google_Chrome_Current_Windows'; Ver='2'; Rel='11';DefaultCategory='ChromeHardening' }
    @{ Key='Firefox';   Name='Mozilla Firefox';             File='FireFox-All-6.7.xml';      Api='Mozilla_Firefox';               Ver='6'; Rel='7'; DefaultCategory='FirefoxHardening'}
    @{ Key='Office365'; Name='Microsoft Office 365 ProPlus';File='Office-365ProPlus-3.5.xml';Api='Office_365_ProPlus';            Ver='3'; Rel='5'; DefaultCategory='OfficeHardening' }
)

# --- Helpers --------------------------------------------------------------------------------

function ConvertTo-HklmPath([string]$key) {
    $key -replace '^HKEY_LOCAL_MACHINE','HKLM' `
         -replace '^HKEY_CURRENT_USER','HKCU' `
         -replace '^HKEY_CLASSES_ROOT','HKCR' `
         -replace '^HKEY_USERS','HKU' `
         -replace '^HKEY_CURRENT_CONFIG','HKCC'
}

function ConvertTo-RegTypeString([string]$dscType) {
    switch ($dscType) {
        'Dword'        { 'REG_DWORD' }
        'Qword'        { 'REG_QWORD' }
        'String'       { 'REG_SZ' }
        'ExpandString' { 'REG_EXPAND_SZ' }
        'MultiString'  { 'REG_MULTI_SZ' }
        'Binary'       { 'REG_BINARY' }
        default        { 'REG_SZ' }
    }
}

# Map a Windows 11 rule to an existing SettingCategory by registry path / value name heuristics.
function Resolve-WindowsCategory([string]$path, [string]$name) {
    $p = $path.ToUpperInvariant()
    $n = $name.ToUpperInvariant()
    if ($p -match '\\LSA' -or $p -match 'CREDENTIALSDELEGATION' -or $n -match 'NOLMHASH|LMCOMPATIBILITY|RESTRICTANONYMOUS|LIMITBLANKPASSWORD|DISABLEDOMAINCREDS|RUNASPPL') { return 'CredentialProtection' }
    if ($p -match 'WINDOWS DEFENDER|WINDOWS ADVANCED THREAT PROTECTION|MPENGINE' )                              { return 'WindowsDefender' }
    if ($p -match 'EVENTLOG|POWERSHELL\\' -or $n -match 'ENABLESCRIPTBLOCKLOGGING|ENABLEMODULELOGGING|ENABLETRANSCRIPTING|PROCESSCREATIONINCLUDECMDLINE' ) { return 'Logging' }
    if ($p -match '\\LANMANSERVER|\\LANMANWORKSTATION|\\NETLOGON|\\SMB|\\NETBT|\\TCPIP|\\DNSCLIENT|LLTD|LLMNR|WCN' -or $n -match 'SMB|LDAP|NTLM|NETBIOS|MULTICAST|HARDENEDPATHS') { return 'NetworkSecurity' }
    if ($p -match 'CRYPTOGRAPHY|SCHANNEL|\\SSL\\|ECCCURVES' -or $n -match 'ECCCURVES|ENABLED.*CIPHER' )           { return 'TLSCrypto' }
    if ($p -match 'DATACOLLECTION|ADVERTISINGINFO|\\CLOUDCONTENT|\\PERSONALIZATION|\\INPUTPERSONALIZATION|LOCATIONANDSENSORS|TELEMETRY' -or $n -match 'ALLOWTELEMETRY|DISABLEDATACOLLECTION|ADVERTISING' ) { return 'Privacy' }
    if ($p -match 'EXPLORER\\AUTOPLAY|\\AUTORUN|REMOVABLESTORAGE|\\USB' -or $n -match 'NOAUTORUN|NODRIVETYPEAUTORUN|AUTORUN' ) { return 'RemovalMedia' }
    return 'SystemHardening'
}

$script:LiveOk = 0; $script:LiveCached = 0; $script:LiveFail = 0

# Fetch a rule's trackr.live detail (STIG id, severity, CCIs, title/desc), cached on disk.
function Get-RuleEnrichment($prod, [string]$vid) {
    $apiVid   = ($vid -split '\.')[0]                           # strip PowerSTIG .a/.b suffix
    $cacheKey = ('{0}_{1}_{2}_{3}.json' -f $prod.Api, $prod.Ver, $prod.Rel, $apiVid)
    $cacheFile= Join-Path $CacheDir $cacheKey
    if (Test-Path $cacheFile) {
        $script:LiveCached++
        return (Get-Content $cacheFile -Raw | ConvertFrom-Json)
    }
    if ($NoNetwork) { $script:LiveFail++; return $null }
    $url = "https://cyber.trackr.live/api/stig/$($prod.Api)/$($prod.Ver)/$($prod.Rel)/$apiVid"
    $maxAttempts = 6
    for ($attempt=1; $attempt -le $maxAttempts; $attempt++) {
        try {
            $resp = Invoke-WebRequest -Uri $url -Headers @{ 'User-Agent'='atlant-stig-generator' } -UseBasicParsing -TimeoutSec 30
            $resp.Content | Out-File -FilePath $cacheFile -Encoding utf8
            Start-Sleep -Milliseconds $ThrottleMs
            $script:LiveOk++
            return ($resp.Content | ConvertFrom-Json)
        } catch {
            $code = $null
            try { $code = [int]$_.Exception.Response.StatusCode } catch {}
            if ($attempt -eq $maxAttempts) {
                $script:LiveFail++; Write-Warning "trackr miss $($prod.Api) $vid : $($_.Exception.Message)"; return $null
            }
            # Exponential backoff; extra patience on 429 rate limiting
            $wait = if ($code -eq 429) { [Math]::Min(8000, 1000 * [Math]::Pow(2, $attempt-1)) } else { 400 * $attempt }
            Start-Sleep -Milliseconds $wait
        }
    }
}

function Limit-Text([string]$s, [int]$max) {
    if ([string]::IsNullOrWhiteSpace($s)) { return '' }
    $t = ($s -replace '\s+',' ').Trim()
    if ($t.Length -le $max) { return $t }
    return $t.Substring(0,$max).TrimEnd() + '…'
}

# Pull the <VulnDiscussion> text out of the PowerSTIG <Description> blob.
function Get-VulnDiscussion([string]$desc) {
    if ($desc -match '(?s)<VulnDiscussion>(.*?)</VulnDiscussion>') { return $matches[1] }
    return $desc
}

# --- Main -----------------------------------------------------------------------------------

$entries  = New-Object System.Collections.Generic.List[object]
$summaries= New-Object System.Collections.Generic.List[object]
$seenIds  = New-Object System.Collections.Generic.HashSet[string]

foreach ($prod in $Products) {
    $xmlPath = Join-Path $SourceDir $prod.File
    if (-not (Test-Path $xmlPath)) { throw "Missing PowerSTIG source: $xmlPath" }
    [xml]$xml = Get-Content $xmlPath -Raw
    $rules = $xml.GetElementsByTagName('Rule') | Where-Object { $_.ParentNode.Name -eq 'RegistryRule' }

    $count = 0
    foreach ($r in $rules) {
        $orgReq = "$($r.OrganizationValueRequired)"
        $rawData= "$($r.ValueData)"
        if ($orgReq -eq 'True' -or [string]::IsNullOrWhiteSpace($rawData)) { continue }  # not automatable

        $vid      = "$($r.id)"
        $regType  = ConvertTo-RegTypeString "$($r.ValueType)"
        $regPath  = ConvertTo-HklmPath ("$($r.Key)").Trim()
        $regName  = "$($r.ValueName)"

        # Typed value for JSON (int / string / string[])
        $value =
            switch ($regType) {
                'REG_DWORD'     { $tmp=0; if([int]::TryParse($rawData.Trim(),[ref]$tmp)){ $tmp } else { $rawData.Trim() } }
                'REG_QWORD'     { $tmp=[long]0; if([long]::TryParse($rawData.Trim(),[ref]$tmp)){ $tmp } else { $rawData.Trim() } }
                'REG_MULTI_SZ'  { @($rawData -split ';' | ForEach-Object { $_.Trim() } | Where-Object { $_ -ne '' }) }
                default         { $rawData.Trim() }
            }

        # Enrichment (STIG ID, severity, CCIs, official title/description)
        $enr      = Get-RuleEnrichment $prod $vid
        $stigId   = if ($enr -and $enr.version) { "$($enr.version)" } else { $vid }
        $severity = if ($enr -and $enr.severity) { "$($enr.severity)".ToLowerInvariant() } else { ("$($r.severity)").ToLowerInvariant() }
        if ($severity -notin @('high','medium','low')) { $severity = 'medium' }
        $ccis     = @()
        if ($enr -and $enr.identifiers) {
            $rawIds = if ($enr.identifiers -is [array]) { $enr.identifiers } else { "$($enr.identifiers)" -split '\s+' }
            $ccis = @($rawIds | Where-Object { $_ -match '^CCI-' })
        }

        $title    = if ($enr -and $enr.'requirement-title') { "$($enr.'requirement-title')" } else { "$($prod.Name) $regName" }
        $name     = Limit-Text $title 110
        $descSrc  = if ($enr -and $enr.'requirement-description') { "$($enr.'requirement-description')" } else { Get-VulnDiscussion "$($r.Description)" }
        $desc     = Limit-Text $descSrc 300
        $fixText  = if ($enr -and $enr.'fix-text') { "$($enr.'fix-text')" } else { '' }

        # Category
        $category = if ($prod.Key -eq 'Windows11') { Resolve-WindowsCategory $regPath $regName } else { $prod.DefaultCategory }

        # Unique id
        $base = ('STIG_{0}_{1}' -f ($stigId -replace '[^A-Za-z0-9]','_'), ($regName -replace '[^A-Za-z0-9]','_'))
        if ($base.Length -gt 90) { $base = $base.Substring(0,90) }
        $id = $base; $suffix = 1
        while (-not $seenIds.Add($id)) { $id = "${base}_$suffix"; $suffix++ }

        # GPO-style registry policy values take effect at policy refresh/logon, not a full reboot.
        # Only flag reboot when the fix text explicitly says a restart is required to apply it.
        $reboot = ($fixText -match '(?i)(restart|reboot)\s+(the\s+)?(system|computer|machine)\s+(is\s+)?(required|for (the|this) (change|setting|policy) to (take|be in) effect)') `
                  -or ($descSrc -match '(?i)(this (setting|change|policy)|requirement)\s+requires\s+(a\s+)?(reboot|restart)')
        $refVid = ($vid -split '\.')[0]
        $refUrl = "https://cyber.trackr.live/stig/$($prod.Api)/$($prod.Ver)/$($prod.Rel)/$refVid"

        $entries.Add([ordered]@{
            id           = $id
            stigId       = $stigId
            vulnId       = $vid
            product      = $prod.Key
            category     = $category
            name         = $name
            description  = $desc
            severity     = $severity
            registryPath = $regPath
            registryKey  = $regName
            valueType    = $regType
            value        = $value
            requiresReboot = [bool]$reboot
            impactWarning  = $null
            ccis         = $ccis
            referenceUrl = $refUrl
        }) | Out-Null
        $count++
    }

    $summaries.Add([ordered]@{
        product = $prod.Key; name = $prod.Name; version = "$($prod.Ver).$($prod.Rel)"
        stigVersion = "V$($prod.Ver)R$($prod.Rel)"; rules = $count
    }) | Out-Null
    Write-Host ("  {0,-22} {1} rules" -f $prod.Name, $count)
}

$catalog = [ordered]@{
    schema      = 'atlant.stig-catalog/1'
    generated   = (Get-Date -Format 'yyyy-MM-dd')
    source      = 'Microsoft PowerSTIG (registry rules) + cyber.trackr.live (STIG IDs/CCIs)'
    note        = 'Automatable registry rules only (OrganizationValueRequired=false). Org-specific rules are excluded.'
    stigs       = $summaries
    totalRules  = $entries.Count
    settings    = $entries
}

# Emit JSON (PowerShell 7 keeps int/array types; depth covers nested ccis/value arrays).
# Write UTF-8 without BOM for clean diffs and universal readers.
$json = $catalog | ConvertTo-Json -Depth 8
[System.IO.File]::WriteAllText($OutFile, $json, (New-Object System.Text.UTF8Encoding($false)))

Write-Host ""
Write-Host ("Wrote {0} settings -> {1}" -f $entries.Count, $OutFile)
Write-Host ("Enrichment: live={0} cached={1} fallback={2}" -f $script:LiveOk, $script:LiveCached, $script:LiveFail)
Write-Host ("Per product: " + (($summaries | ForEach-Object { '{0}={1}' -f $_.product,$_.rules }) -join '  '))
