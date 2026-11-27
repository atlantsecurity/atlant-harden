#Requires -Version 5.1
<#
.SYNOPSIS
    Signs Atlant Security Windows agent binaries with Azure Artifact Signing.

.DESCRIPTION
    Wraps SignTool + the Artifact Signing dlib. By default it authenticates with your
    Azure CLI session (run `az login --tenant atlantsecurity.com` once per machine).

    - Each file signed = 1 signature against the account's monthly quota (Basic: 5,000).
    - Files that already carry a valid signature (third-party DLLs, or files you already
      signed) are skipped unless -Force is used.
    - Kernel drivers (.sys, .cat) are never signed here. They must go through Microsoft
      Hardware Dev Center attestation signing.
    - Use -WhatIf to list what would be signed without using any quota.

    Configuration (parameters or environment variables):
      ARTIFACT_SIGNING_ENDPOINT  default https://neu.codesigning.azure.net (North Europe)
      ARTIFACT_SIGNING_ACCOUNT   default atlantsecurity-signing
      ARTIFACT_SIGNING_PROFILE   default atlant-public-trust
      SIGNTOOL_PATH              optional, full path to signtool.exe
      ARTIFACT_SIGNING_DLIB      optional, full path to Azure.CodeSigning.Dlib.dll

.EXAMPLE
    .\sign-agents.ps1 -Path .\build\Release -Recurse -Description "Atlant SIEM Agent" -WhatIf

.EXAMPLE
    .\sign-agents.ps1 -Path .\build\Release -Recurse -Description "Atlant SIEM Agent"

.EXAMPLE
    .\sign-agents.ps1 -Path .\dist\AtlantDlpAgent.msi -Description "Atlant DLP Agent"

.EXAMPLE
    # CI / build server with a service principal (AZURE_TENANT_ID, AZURE_CLIENT_ID, AZURE_CLIENT_SECRET set)
    .\sign-agents.ps1 -Path .\out -Recurse -Auth Environment
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(
    [Parameter(Mandatory = $true, Position = 0)]
    [string[]] $Path,

    [string]   $Description,
    [string]   $DescriptionUrl = 'https://atlantsecurity.com',
    [string[]] $Include = @('*.exe', '*.dll', '*.msi', '*.msix', '*.appx', '*.ps1', '*.psm1', '*.psd1'),
    [switch]   $Recurse,
    [switch]   $Force,

    [ValidateSet('AzureCli', 'Environment')]
    [string]   $Auth = 'AzureCli',

    [string]   $Endpoint      = $(if ($env:ARTIFACT_SIGNING_ENDPOINT) { $env:ARTIFACT_SIGNING_ENDPOINT } else { 'https://neu.codesigning.azure.net' }),
    [string]   $AccountName   = $(if ($env:ARTIFACT_SIGNING_ACCOUNT)  { $env:ARTIFACT_SIGNING_ACCOUNT }  else { 'atlantsecurity-signing' }),
    [string]   $ProfileName   = $(if ($env:ARTIFACT_SIGNING_PROFILE)  { $env:ARTIFACT_SIGNING_PROFILE }  else { 'atlant-public-trust' }),
    [string]   $CorrelationId,
    [string]   $SignToolPath  = $env:SIGNTOOL_PATH,
    [string]   $DlibPath      = $env:ARTIFACT_SIGNING_DLIB,
    [ValidateRange(1, 200)]
    [int]      $BatchSize     = 25
)

Set-StrictMode -Version Latest
$ErrorActionPreference = 'Stop'

if ($env:OS -ne 'Windows_NT') {
    throw 'SignTool only runs on Windows. On macOS/Linux use sign-agents.sh (Jsign) instead.'
}

$TimestampUrl = 'http://timestamp.acs.microsoft.com'
$DriverExtensions = @('.sys', '.cat')

function Find-SignTool {
    if ($SignToolPath) {
        if (-not (Test-Path -LiteralPath $SignToolPath)) { throw "SIGNTOOL_PATH not found: $SignToolPath" }
        return (Resolve-Path -LiteralPath $SignToolPath).Path
    }
    # Newest Windows SDK first. The 10.0.20348 SDK is not supported by the Artifact Signing dlib.
    $kits = Join-Path ${env:ProgramFiles(x86)} 'Windows Kits\10\bin'
    if (Test-Path -LiteralPath $kits) {
        $candidate = Get-ChildItem -LiteralPath $kits -Directory |
            Where-Object { $_.Name -match '^10\.0\.\d+\.\d+$' -and $_.Name -notlike '10.0.20348*' } |
            Sort-Object { [version]$_.Name } -Descending |
            ForEach-Object { Join-Path $_.FullName 'x64\signtool.exe' } |
            Where-Object { Test-Path -LiteralPath $_ } |
            Select-Object -First 1
        if ($candidate) { return $candidate }
    }
    $onPath = Get-Command signtool.exe -ErrorAction SilentlyContinue
    if ($onPath) { return $onPath.Source }
    throw 'signtool.exe not found. Install the Windows SDK signing tools (or the Microsoft.Windows.SDK.BuildTools NuGet package) or set SIGNTOOL_PATH.'
}

function Find-Dlib([string] $Arch) {
    if ($DlibPath) {
        if (-not (Test-Path -LiteralPath $DlibPath)) { throw "ARTIFACT_SIGNING_DLIB not found: $DlibPath" }
        return (Resolve-Path -LiteralPath $DlibPath).Path
    }
    $roots = @(
        (Join-Path $env:LOCALAPPDATA 'Microsoft\MicrosoftArtifactSigningClientTools'),
        (Join-Path $env:LOCALAPPDATA 'Microsoft\MicrosoftTrustedSigningClientTools'),
        (Join-Path $env:ProgramFiles 'Microsoft\ArtifactSigningClientTools'),
        (Join-Path ${env:ProgramFiles(x86)} 'Microsoft\ArtifactSigningClientTools')
    ) | Where-Object { Test-Path -LiteralPath $_ }

    $otherArch = if ($Arch -eq 'x64') { 'x86' } else { 'x64' }
    foreach ($root in $roots) {
        $best = Get-ChildItem -LiteralPath $root -Recurse -Filter 'Azure.CodeSigning.Dlib.dll' -ErrorAction SilentlyContinue |
            Where-Object { $_.FullName -notmatch "\\$otherArch\\" } |
            Sort-Object { if ($_.FullName -match "\\$Arch\\") { 0 } else { 1 } } |
            Select-Object -First 1
        if ($best) { return $best.FullName }
    }
    throw 'Azure.CodeSigning.Dlib.dll not found. Run: winget install -e --id Microsoft.Azure.ArtifactSigningClientTools  (or set ARTIFACT_SIGNING_DLIB).'
}

# --- 1. Collect files -------------------------------------------------------------------
$toSign        = @()
$alreadySigned = @()
$drivers       = @()

foreach ($p in $Path) {
    if (-not (Test-Path -LiteralPath $p)) { throw "Path not found: $p" }
    $item = Get-Item -LiteralPath $p
    $explicitFile = -not $item.PSIsContainer
    $candidates = if ($item.PSIsContainer) {
        @(Get-ChildItem -LiteralPath $item.FullName -File -Recurse:$Recurse)
    } else {
        @($item)
    }

    foreach ($f in $candidates) {
        if ($DriverExtensions -contains $f.Extension.ToLowerInvariant()) { $drivers += $f.FullName; continue }

        $matchesInclude = $false
        foreach ($pattern in $Include) { if ($f.Name -like $pattern) { $matchesInclude = $true; break } }
        if (-not $matchesInclude) {
            if ($explicitFile) { Write-Warning "Skipping $($f.Name): file type not in -Include." }
            continue
        }

        if (-not $Force) {
            $sig = Get-AuthenticodeSignature -LiteralPath $f.FullName
            if ($sig.Status -eq 'Valid') {
                $alreadySigned += "$($f.FullName)  [$($sig.SignerCertificate.Subject)]"
                continue
            }
        }
        if ($toSign -notcontains $f.FullName) { $toSign += $f.FullName }
    }
}

if ($drivers.Count -gt 0) {
    Write-Warning ("Not signing {0} kernel driver file(s); they need Hardware Dev Center attestation signing:`n  {1}" -f $drivers.Count, ($drivers -join "`n  "))
}
if ($alreadySigned.Count -gt 0) {
    Write-Host ("Skipping {0} file(s) that already have a valid signature (use -Force to re-sign):" -f $alreadySigned.Count)
    $alreadySigned | ForEach-Object { Write-Host "  $_" }
}

Write-Host ("Files to sign: {0} (= {0} signatures of monthly quota)" -f $toSign.Count)
$toSign | ForEach-Object { Write-Host "  $_" }

if ($WhatIfPreference) { Write-Host 'WhatIf: nothing was signed.'; return }
if ($toSign.Count -eq 0) { Write-Host 'Nothing to sign.'; return }

# --- 2. Tools and authentication ----------------------------------------------------------
$signtool = Find-SignTool
$arch     = if ($signtool -match '\\x86\\') { 'x86' } else { 'x64' }
$dlib     = Find-Dlib $arch
Write-Host "SignTool: $signtool"
Write-Host "Dlib:     $dlib"
Write-Host "Account:  $AccountName / $ProfileName @ $Endpoint"

if ($Auth -eq 'AzureCli') {
    if (-not (Get-Command az -ErrorAction SilentlyContinue)) {
        throw 'Azure CLI not found. Install it (winget install -e --id Microsoft.AzureCLI), then run: az login --tenant atlantsecurity.com'
    }
    # Windows PowerShell 5.1 turns redirected native stderr into terminating errors under 'Stop'.
    $savedPreference = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    try { $null = & az account show --only-show-errors 2>$null; $azExit = $LASTEXITCODE }
    finally { $ErrorActionPreference = $savedPreference }
    if ($azExit -ne 0) { throw 'Not logged in to Azure. Run: az login --tenant atlantsecurity.com' }
}

# Use exactly one credential type so signing never falls back to an unexpected identity.
$allCredentials = @(
    'EnvironmentCredential', 'WorkloadIdentityCredential', 'ManagedIdentityCredential',
    'SharedTokenCacheCredential', 'VisualStudioCredential', 'VisualStudioCodeCredential',
    'AzureCliCredential', 'AzurePowerShellCredential', 'AzureDeveloperCliCredential',
    'InteractiveBrowserCredential'
)
$keepCredential = if ($Auth -eq 'AzureCli') { 'AzureCliCredential' } else { 'EnvironmentCredential' }

$metadata = [ordered]@{
    Endpoint               = $Endpoint
    CodeSigningAccountName = $AccountName
    CertificateProfileName = $ProfileName
    ExcludeCredentials     = @($allCredentials | Where-Object { $_ -ne $keepCredential })
}
if ($CorrelationId) { $metadata.CorrelationId = $CorrelationId }

$metadataPath = Join-Path ([IO.Path]::GetTempPath()) ("artifact-signing-{0}.json" -f [guid]::NewGuid())
[IO.File]::WriteAllText($metadataPath, ($metadata | ConvertTo-Json), (New-Object System.Text.UTF8Encoding $false))

# --- 3. Sign ----------------------------------------------------------------------------
$signArgs = @('sign', '/v', '/fd', 'SHA256', '/tr', $TimestampUrl, '/td', 'SHA256',
              '/dlib', $dlib, '/dmdf', $metadataPath)
if ($Description)    { $signArgs += @('/d', $Description) }
if ($DescriptionUrl) { $signArgs += @('/du', $DescriptionUrl) }
if ($VerbosePreference -eq 'Continue') { $signArgs += '/debug' }

try {
    for ($i = 0; $i -lt $toSign.Count; $i += $BatchSize) {
        $last  = [Math]::Min($i + $BatchSize, $toSign.Count) - 1
        $batch = @($toSign[$i..$last])
        & $signtool @signArgs @batch
        if ($LASTEXITCODE -ne 0) {
            throw "SignTool failed (exit $LASTEXITCODE). Re-run with -Verbose for /debug output. 403 errors usually mean a missing 'Artifact Signing Certificate Profile Signer' role or a wrong endpoint region."
        }
    }
}
finally {
    Remove-Item -LiteralPath $metadataPath -ErrorAction SilentlyContinue
}

# --- 4. Verify --------------------------------------------------------------------------
Write-Host 'Verifying...'
$failed = @()
foreach ($f in $toSign) {
    $savedPreference = $ErrorActionPreference
    $ErrorActionPreference = 'Continue'
    try { $null = & $signtool verify /pa /q $f 2>&1; $verifyExit = $LASTEXITCODE }
    finally { $ErrorActionPreference = $savedPreference }
    $sig = Get-AuthenticodeSignature -LiteralPath $f
    if ($verifyExit -ne 0 -or $sig.Status -ne 'Valid' -or -not $sig.TimeStamperCertificate) {
        $failed += $f
        Write-Host ("  FAIL {0}  status={1}" -f $f, $sig.Status) -ForegroundColor Red
    }
    else {
        Write-Host ("  OK   {0}  signer: {1}" -f $f, $sig.SignerCertificate.GetNameInfo('SimpleName', $false)) -ForegroundColor Green
    }
}

if ($failed.Count -gt 0) { throw "$($failed.Count) file(s) failed verification." }
Write-Host ("Signed and verified {0} file(s)." -f $toSign.Count)
