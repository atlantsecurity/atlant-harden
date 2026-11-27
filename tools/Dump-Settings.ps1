<#
  Reflection dump of every HardeningSetting to <repo>\_settings.json, in the schema
  Generate-SettingsDocs.ps1 expects. Run against the built assembly, then run the doc generator.
#>
param(
    [string] $RepoRoot = (Split-Path -Parent $PSScriptRoot),
    [string] $Dll
)
$ErrorActionPreference = 'Stop'
if (-not $Dll) { $Dll = Join-Path $RepoRoot 'bin\Release\net8.0-windows\AtlantHarden.dll' }
$dll = $Dll
$outJson = Join-Path $RepoRoot '_settings.json'
Add-Type -Path $dll

$svc = [AtlantHarden.Services.HardeningService]::new()
$rec = [AtlantHarden.Services.RecommendedProfile]
# Bloatware removal is a separate cleanup action, not part of the security profiles/score, so it
# is excluded from the settings reference docs (which document the Basic/Recommended/Maximum set).
$all = $svc.GetAllSettings() | Where-Object { $_.Category.ToString() -ne 'Bloatware' }

$out = foreach ($s in $all) {
    [pscustomobject]@{
        Name        = $s.Name
        Description = $s.Description
        Category    = $s.Category.ToString()
        Type        = $s.Type.ToString()
        Risk        = $s.Risk.ToString()
        Path        = $s.RegistryPath
        Key         = $s.RegistryKey
        Value       = $s.RecommendedValue
        ValType     = $s.RegistryValueType
        ASR         = -not [string]::IsNullOrEmpty($s.ASRGuid)
        IsStig      = $s.IsStig
        StigId      = $s.StigId
        VulnId      = $s.VulnId
        IsACSC      = $s.IsACSC
        Reboot      = $s.RequiresReboot
        Impact      = $s.ImpactWarning
        Rec         = $rec::IsRecommended($s)
    }
}

$out | ConvertTo-Json -Depth 5 | Set-Content -Path $outJson -Encoding utf8
Write-Output "Dumped $($out.Count) settings to $outJson"
