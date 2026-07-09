<#
  Reflection dump of every HardeningSetting to C:\P\winh\_settings.json, in the schema
  Generate-SettingsDocs.ps1 expects. Run against the built assembly, then run the doc generator.
#>
$ErrorActionPreference = 'Stop'
$dll = 'C:\P\winh\AtlantSecurityHardening\bin\Release\net8.0-windows\AtlantHarden.dll'
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

$out | ConvertTo-Json -Depth 5 | Set-Content -Path 'C:\P\winh\_settings.json' -Encoding utf8
Write-Output "Dumped $($out.Count) settings to C:\P\winh\_settings.json"
