#Requires -Version 7
<#
.SYNOPSIS
    Builds, signs and packages an AtlantHarden release zip.

.DESCRIPTION
    1. dotnet publish (Release, win-x64, self-contained single file)
    2. Sign AtlantHarden.exe with Atlant Security's Azure Artifact Signing account
       (scripts/sign-agents.ps1, your own `az login` session; 1 signature of quota)
    3. Verify the signature (signer + timestamp)
    4. Regenerate the settings docs and assemble the zip:
       AtlantHarden.exe, README.md, LICENSE.txt (EULA), QUICKSTART.txt,
       Recommended-/Maximum-Settings-Explained.md

    Release builds only - never sign Debug/test builds (quota).

.EXAMPLE
    pwsh -File tools/Package-Release.ps1
#>
param(
    [string] $RepoRoot = (Split-Path -Parent $PSScriptRoot),
    [string] $OutDir,
    [switch] $SkipSign
)
$ErrorActionPreference = 'Stop'
if (-not $OutDir) { $OutDir = Join-Path $RepoRoot 'dist' }

$publish = Join-Path $RepoRoot 'publish'
$stage   = Join-Path $OutDir 'AtlantHarden'
Remove-Item -Recurse -Force $publish, $stage -ErrorAction SilentlyContinue
New-Item -ItemType Directory -Force -Path $stage | Out-Null

Write-Host '== Build'
dotnet build (Join-Path $RepoRoot 'AtlantHarden.csproj') -c Release
if ($LASTEXITCODE) { throw 'build failed' }
dotnet publish (Join-Path $RepoRoot 'AtlantHarden.csproj') -c Release -r win-x64 --self-contained true `
    -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true `
    -o $publish
if ($LASTEXITCODE) { throw 'publish failed' }
$exe = Join-Path $publish 'AtlantHarden.exe'

if (-not $SkipSign) {
    Write-Host '== Sign'
    & (Join-Path $RepoRoot 'scripts/sign-agents.ps1') -Path $exe -Description 'AtlantHarden'
    $sig = Get-AuthenticodeSignature -LiteralPath $exe
    if ($sig.Status -ne 'Valid' -or -not $sig.TimeStamperCertificate) { throw "Signature check failed: $($sig.Status)" }
    Write-Host ("Signer:      {0}" -f $sig.SignerCertificate.Subject)
    Write-Host ("Timestamped: {0}" -f $sig.TimeStamperCertificate.Subject)
}

Write-Host '== Docs'
& (Join-Path $RepoRoot 'tools/Dump-Settings.ps1') -RepoRoot $RepoRoot
& (Join-Path $RepoRoot 'tools/Generate-SettingsDocs.ps1') -RepoRoot $RepoRoot -ZipDocsDir $stage

Write-Host '== Package'
Copy-Item $exe $stage
Copy-Item (Join-Path $RepoRoot 'README.md') $stage
Copy-Item (Join-Path $RepoRoot 'EULA.txt') (Join-Path $stage 'LICENSE.txt')
Copy-Item (Join-Path $RepoRoot 'packaging/QUICKSTART.txt') $stage
$zip = Join-Path $OutDir 'AtlantHarden.zip'
Remove-Item $zip -ErrorAction SilentlyContinue
Compress-Archive -Path (Join-Path $stage '*') -DestinationPath $zip
$hash = (Get-FileHash $zip -Algorithm SHA256).Hash
Write-Host "Wrote $zip"
Write-Host "SHA256: $hash"
