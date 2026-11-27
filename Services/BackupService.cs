using AtlantHarden.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AtlantHarden.Services
{
    public class BackupService
    {
        private readonly string _backupDirectory;
        private const int MaxBackups = 20;
        private const string BackupFileExtension = ".ashb";

        public BackupService()
        {
            // Admin-only location: restore runs elevated and trusts these files (see SecurePaths).
            _backupDirectory = SecurePaths.Ensure(SecurePaths.Backups);
        }

        public string BackupDirectory => _backupDirectory;

        /// <summary>
        /// Creates a backup of the current state of every setting about to change (being enabled
        /// OR disabled), BEFORE the change is made. Registry values are recorded exactly (primary
        /// path and every mirror path). Non-registry settings (Defender preferences, ASR rules,
        /// firewall/audit policy, file associations, commands) record whether they were applied,
        /// so a restore can re-apply or revert them through the same apply/revert code paths.
        /// </summary>
        public async Task<BackupInfo> CreateBackupAsync(List<HardeningSetting> settings, string label = "")
        {
            var changed = settings.ToList();
            var now = DateTime.Now;
            bool isBaseline = !Directory.EnumerateFiles(_backupDirectory, $"*{BackupFileExtension}").Any();

            var backupData = new BackupData
            {
                CreatedAt = now,
                ComputerName = Environment.MachineName,
                WindowsVersion = Environment.OSVersion.ToString(),
                Label = label,
                IsBaseline = isBaseline,
                RegistryEntries = new List<RegistryBackupEntry>(),
                StateEntries = new List<SettingStateEntry>(),
                AppliedSettingIds = new List<string>()
            };

            foreach (var setting in changed)
            {
                backupData.AppliedSettingIds.Add(setting.Id);

                if (setting.Type == SettingType.Registry
                    && !string.IsNullOrEmpty(setting.RegistryPath) && !string.IsNullOrEmpty(setting.RegistryKey))
                {
                    var paths = new List<string> { setting.RegistryPath };
                    if (setting.MirrorRegistryPaths != null) paths.AddRange(setting.MirrorRegistryPaths);
                    foreach (var path in paths)
                        backupData.RegistryEntries.Add(CaptureRegistryEntry(setting, path));
                }
                else
                {
                    backupData.StateEntries.Add(new SettingStateEntry
                    {
                        SettingId = setting.Id,
                        SettingName = setting.Name,
                        Type = setting.Type.ToString(),
                        WasApplied = setting.IsApplied,
                        PreviousValue = setting.CurrentValue
                    });
                }
            }

            var stamp = $"{now:yyyyMMdd_HHmmss_fff}";
            var fileName = $"backup_{stamp}{BackupFileExtension}";
            var backupInfo = new BackupInfo
            {
                CreatedAt = now,
                FilePath = Path.Combine(_backupDirectory, fileName),
                IsBaseline = isBaseline,
                Label = label,
                SettingsCount = changed.Count,
                Description = DescribeBackup(backupData),
                AppliedCategories = changed.Select(s => s.Category.ToString()).Distinct().ToList()
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(backupData, options);
            await File.WriteAllTextAsync(backupInfo.FilePath, json);
            backupInfo.FileSizeBytes = new FileInfo(backupInfo.FilePath).Length;

            // .reg files for manual restoration / reference. Never let a formatting problem in
            // these convenience files block the backup (and therefore the whole apply).
            try
            {
                await ExportRestoreRegFileAsync(backupData, Path.Combine(_backupDirectory, $"restore_{stamp}.reg"));
                await ExportAppliedRegFileAsync(backupData, Path.Combine(_backupDirectory, $"applied_{stamp}.reg"));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[BackupService] .reg export failed: {ex.Message}");
            }

            await CleanupOldBackupsAsync();
            return backupInfo;
        }

        private static RegistryBackupEntry CaptureRegistryEntry(HardeningSetting setting, string path)
        {
            var keyExists = RegistryService.KeyExists(path);
            var valueExists = keyExists && RegistryService.ValueExists(path, setting.RegistryKey);
            object? originalValue = null;
            string originalValueType = string.IsNullOrEmpty(setting.RegistryValueType) ? "REG_DWORD" : setting.RegistryValueType;

            if (valueExists)
            {
                originalValue = RegistryService.ReadValue(path, setting.RegistryKey);
                originalValueType = RegistryService.ValueKindToString(RegistryService.GetValueKind(path, setting.RegistryKey));
            }

            return new RegistryBackupEntry
            {
                Path = path,
                KeyName = setting.RegistryKey,
                ValueType = originalValueType,
                KeyExisted = keyExists,
                ValueExisted = valueExists,
                OriginalValue = originalValue,
                NewValue = setting.RecommendedValue,
                NewValueType = string.IsNullOrEmpty(setting.RegistryValueType) ? "REG_DWORD" : setting.RegistryValueType,
                SettingId = setting.Id,
                SettingName = setting.Name
            };
        }

        private static string DescribeBackup(BackupData data)
        {
            var what = string.IsNullOrWhiteSpace(data.Label) ? "Before applying changes" : $"Before: {data.Label}";
            var count = data.AppliedSettingIds.Count > 0 ? data.AppliedSettingIds.Count : data.RegistryEntries.Count;
            return $"{what} · {count} settings";
        }

        /// <summary>Result of restoring a backup.</summary>
        public sealed record RestoreResult(bool Success, int Restored, int Failed, int Skipped, List<string> Errors, List<string> SkippedNames);

        /// <summary>
        /// Restore a backup: registry values are written back exactly (or deleted if they did not
        /// exist), and non-registry settings are re-applied or reverted to their recorded state via
        /// <paramref name="applyOrRevert"/>. App removals cannot be undone and are reported as skipped.
        /// </summary>
        public async Task<RestoreResult> RestoreBackupAsync(string backupFilePath,
            IReadOnlyDictionary<string, HardeningSetting>? settingsById = null,
            Func<HardeningSetting, bool, Task<bool>>? applyOrRevert = null)
        {
            var errors = new List<string>();
            var skippedNames = new List<string>();
            int restored = 0, failed = 0, skipped = 0;

            BackupData? backupData;
            try
            {
                var json = await File.ReadAllTextAsync(backupFilePath);
                backupData = JsonSerializer.Deserialize<BackupData>(json);
            }
            catch (Exception ex)
            {
                return new RestoreResult(false, 0, 0, 0, new List<string> { ex.Message }, skippedNames);
            }
            if (backupData == null)
                return new RestoreResult(false, 0, 0, 0, new List<string> { "Could not read backup file" }, skippedNames);

            // Defence in depth: only touch registry values that belong to a setting this app
            // manages (its primary or mirror path + value name). A backup file that names any other
            // location is rejected entry by entry instead of being written as administrator.
            HashSet<string>? allowed = null;
            if (settingsById != null)
            {
                allowed = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                foreach (var s in settingsById.Values)
                {
                    if (s.Type != SettingType.Registry || string.IsNullOrEmpty(s.RegistryKey)) continue;
                    allowed.Add(NormKey(s.RegistryPath, s.RegistryKey));
                    if (s.MirrorRegistryPaths != null)
                        foreach (var m in s.MirrorRegistryPaths) allowed.Add(NormKey(m, s.RegistryKey));
                }
            }

            foreach (var entry in backupData.RegistryEntries)
            {
                if (allowed != null && !allowed.Contains(NormKey(entry.Path, entry.KeyName)))
                {
                    failed++;
                    errors.Add($"Rejected (not a setting this app manages): {entry.Path}\\{entry.KeyName}");
                    continue;
                }
                try
                {
                    bool ok;
                    if (!entry.ValueExisted)
                    {
                        // Value didn't exist before: delete it. A value that is already absent is fine.
                        ok = RegistryService.DeleteValue(entry.Path, entry.KeyName)
                             || !RegistryService.ValueExists(entry.Path, entry.KeyName);
                    }
                    else if (entry.OriginalValue != null)
                    {
                        var valueKind = RegistryService.ParseValueKind(entry.ValueType);
                        ok = RegistryService.WriteValue(entry.Path, entry.KeyName,
                            ConvertJsonElement(entry.OriginalValue, valueKind), valueKind);
                    }
                    else ok = true;

                    if (ok) restored++;
                    else { failed++; errors.Add($"Failed to restore: {entry.SettingName ?? entry.KeyName}"); }
                }
                catch (Exception ex)
                {
                    failed++;
                    errors.Add($"{entry.SettingName ?? entry.KeyName}: {ex.Message}");
                }
            }

            foreach (var state in backupData.StateEntries ?? new List<SettingStateEntry>())
            {
                if (settingsById == null || applyOrRevert == null
                    || !settingsById.TryGetValue(state.SettingId, out var setting))
                {
                    skipped++; skippedNames.Add(state.SettingName ?? state.SettingId);
                    continue;
                }
                if (setting.Type == SettingType.AppRemoval)
                {
                    // An uninstalled app can't be put back automatically.
                    if (!state.WasApplied && setting.IsApplied) { skipped++; skippedNames.Add(setting.Name + " (reinstall manually)"); }
                    continue;
                }
                if (setting.IsApplied == state.WasApplied) { restored++; continue; }
                try
                {
                    if (await applyOrRevert(setting, state.WasApplied)) restored++;
                    else { failed++; errors.Add($"Failed to {(state.WasApplied ? "re-apply" : "revert")}: {setting.Name}"); }
                }
                catch (Exception ex)
                {
                    failed++;
                    errors.Add($"{setting.Name}: {ex.Message}");
                }
            }

            return new RestoreResult(failed == 0, restored, failed, skipped, errors, skippedNames);
        }

        private static string NormKey(string path, string key) =>
            (path.Replace("HKEY_LOCAL_MACHINE", "HKLM").Replace("HKEY_CURRENT_USER", "HKCU").TrimEnd('\\') + "|" + key)
                .ToUpperInvariant();

        /// <summary>
        /// Convert a value read back from backup JSON to the CLR type the registry API expects.
        /// byte[] is serialized by System.Text.Json as a base64 string; string[] as a JSON array.
        /// </summary>
        internal static object ConvertJsonElement(object value, RegistryValueKind kind)
        {
            if (value is not JsonElement element) return value;
            return kind switch
            {
                RegistryValueKind.DWord => element.ValueKind == JsonValueKind.Number && element.TryGetInt64(out long d)
                    ? unchecked((int)d) : 0,
                RegistryValueKind.QWord => element.ValueKind == JsonValueKind.Number && element.TryGetInt64(out long q) ? q : 0L,
                RegistryValueKind.String or RegistryValueKind.ExpandString =>
                    element.ValueKind == JsonValueKind.String ? element.GetString() ?? "" : element.ToString(),
                RegistryValueKind.MultiString => element.ValueKind == JsonValueKind.Array
                    ? element.EnumerateArray().Select(e => e.GetString() ?? "").ToArray()
                    : RegistryService.ToMultiString(element.ToString()),
                RegistryValueKind.Binary => element.ValueKind switch
                {
                    JsonValueKind.String => element.TryGetBytesFromBase64(out var b) ? b : Array.Empty<byte>(),
                    JsonValueKind.Array => element.EnumerateArray().Select(e => (byte)e.GetInt32()).ToArray(),
                    _ => Array.Empty<byte>()
                },
                _ => element.ToString()
            };
        }

        public async Task<List<BackupInfo>> GetAllBackupsAsync()
        {
            var backups = new List<BackupInfo>();

            if (!Directory.Exists(_backupDirectory))
                return backups;

            var files = Directory.GetFiles(_backupDirectory, $"*{BackupFileExtension}")
                                 .OrderByDescending(f => File.GetCreationTime(f));

            foreach (var file in files)
            {
                try
                {
                    var json = await File.ReadAllTextAsync(file);
                    var backupData = JsonSerializer.Deserialize<BackupData>(json);
                    
                    if (backupData != null)
                    {
                        backups.Add(new BackupInfo
                        {
                            FilePath = file,
                            CreatedAt = backupData.CreatedAt,
                            Description = DescribeBackup(backupData),
                            Label = backupData.Label,
                            IsBaseline = backupData.IsBaseline,
                            SettingsCount = backupData.AppliedSettingIds.Count > 0
                                ? backupData.AppliedSettingIds.Count : backupData.RegistryEntries.Count,
                            FileSizeBytes = new FileInfo(file).Length,
                            AppliedCategories = backupData.RegistryEntries
                                .Select(e => e.SettingName ?? e.KeyName)
                                .Take(5)
                                .ToList()
                        });
                    }
                }
                catch
                {
                    backups.Add(new BackupInfo
                    {
                        FilePath = file,
                        CreatedAt = File.GetCreationTime(file),
                        Description = "Corrupted backup file",
                        Status = BackupStatus.Corrupted
                    });
                }
            }

            return backups;
        }

        public async Task<bool> DeleteBackupAsync(string backupFilePath)
        {
            try
            {
                if (File.Exists(backupFilePath))
                {
                    File.Delete(backupFilePath);
                    
                    // Delete associated .reg files
                    var baseName = Path.GetFileNameWithoutExtension(backupFilePath).Replace("backup_", "");
                    var dir = Path.GetDirectoryName(backupFilePath) ?? _backupDirectory;
                    
                    var restoreReg = Path.Combine(dir, $"restore_{baseName}.reg");
                    var appliedReg = Path.Combine(dir, $"applied_{baseName}.reg");
                    
                    if (File.Exists(restoreReg)) File.Delete(restoreReg);
                    if (File.Exists(appliedReg)) File.Delete(appliedReg);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region System Restore

        private const string SystemRestoreKey = @"HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\SystemRestore";

        /// <summary>
        /// Create a Windows System Restore point and VERIFY it exists. Windows normally allows one
        /// restore point per 24 hours and, when throttled, Checkpoint-Computer only prints a warning
        /// and exits 0 — so success is confirmed by finding the new point, and the 24-hour limit is
        /// lifted for this one call (SystemRestorePointCreationFrequency = 0) and then put back.
        /// </summary>
        public async Task<(bool success, string message)> CreateSystemRestorePointAsync(string description = "AtlantHarden")
        {
            object? oldFrequency = null;
            bool hadFrequency = false;
            try
            {
                await EnableSystemRestoreAsync();

                var before = (await GetRestorePointsAsync()).Select(p => p.SequenceNumber).DefaultIfEmpty(0).Max();

                hadFrequency = RegistryService.ValueExists(SystemRestoreKey, "SystemRestorePointCreationFrequency");
                if (hadFrequency) oldFrequency = RegistryService.ReadValue(SystemRestoreKey, "SystemRestorePointCreationFrequency");
                RegistryService.WriteValue(SystemRestoreKey, "SystemRestorePointCreationFrequency", 0, RegistryValueKind.DWord);

                var desc = description.Replace("'", "''");
                var r = await ProcessRunner.RunPowerShellAsync(
                    $"Checkpoint-Computer -Description '{desc}' -RestorePointType 'MODIFY_SETTINGS'",
                    TimeSpan.FromMinutes(5));
                if (r.TimedOut) return (false, "Creating the restore point timed out.");

                var after = (await GetRestorePointsAsync()).Select(p => p.SequenceNumber).DefaultIfEmpty(0).Max();
                if (after > before)
                    return (true, "System Restore point created.");

                var why = string.IsNullOrWhiteSpace(r.StdErr) ? "" : $"\n\n{r.StdErr.Trim()}";
                return (false, "Windows did not create a restore point. System Protection may be turned off for the " +
                               "system drive, or there is no shadow-copy space (System Properties → System Protection)." + why);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
            finally
            {
                if (hadFrequency && oldFrequency != null)
                    RegistryService.WriteValue(SystemRestoreKey, "SystemRestorePointCreationFrequency", oldFrequency, RegistryValueKind.DWord);
                else
                    RegistryService.DeleteValue(SystemRestoreKey, "SystemRestorePointCreationFrequency");
            }
        }

        /// <summary>Enable System Restore on the system drive (no-op if already on).</summary>
        public async Task<(bool success, string message)> EnableSystemRestoreAsync()
        {
            var systemDrive = Path.GetPathRoot(Environment.SystemDirectory) ?? "C:\\";
            var r = await ProcessRunner.RunPowerShellAsync(
                $"Enable-ComputerRestore -Drive '{systemDrive.Replace("'", "''")}'", TimeSpan.FromSeconds(60));
            return r.Success ? (true, "System Restore enabled") : (false, "Could not enable System Restore");
        }

        /// <summary>Open the Windows System Restore wizard.</summary>
        public void OpenSystemRestoreDialog()
        {
            try
            {
                Process.Start(new ProcessStartInfo { FileName = ProcessRunner.Tool("rstrui.exe"), UseShellExecute = true });
            }
            catch
            {
                try { Process.Start(new ProcessStartInfo { FileName = ProcessRunner.Tool("control.exe"), Arguments = "/name Microsoft.Recovery", UseShellExecute = true }); }
                catch { }
            }
        }

        /// <summary>List available System Restore points.</summary>
        public async Task<List<SystemRestorePoint>> GetRestorePointsAsync()
        {
            var points = new List<SystemRestorePoint>();
            try
            {
                var r = await ProcessRunner.RunPowerShellAsync(
                    "Get-ComputerRestorePoint | Select-Object SequenceNumber,Description,RestorePointType," +
                    "@{n='Created';e={$_.ConvertToDateTime($_.CreationTime).ToString('o')}} | ConvertTo-Json -Compress",
                    TimeSpan.FromSeconds(60));
                var json = r.StdOut.Trim();
                if (string.IsNullOrEmpty(json)) return points;

                using var doc = JsonDocument.Parse(json.StartsWith("[") ? json : $"[{json}]");
                foreach (var el in doc.RootElement.EnumerateArray())
                {
                    points.Add(new SystemRestorePoint
                    {
                        SequenceNumber = el.TryGetProperty("SequenceNumber", out var sn) && sn.TryGetInt32(out var n) ? n : 0,
                        Description = el.TryGetProperty("Description", out var d) ? d.GetString() ?? "" : "",
                        CreationTime = el.TryGetProperty("Created", out var c) && DateTime.TryParse(c.GetString(), out var dt) ? dt : DateTime.MinValue,
                        RestorePointType = el.TryGetProperty("RestorePointType", out var rt) && rt.TryGetInt32(out var tp) ? tp : 0
                    });
                }
            }
            catch
            {
                // Return what we have on error
            }
            return points.OrderByDescending(p => p.CreationTime).ToList();
        }

        #endregion

        #region Private Methods

        private async Task CleanupOldBackupsAsync()
        {
            try
            {
                // Never prune the baseline (the very first backup = the PC's original state).
                var files = Directory.GetFiles(_backupDirectory, $"*{BackupFileExtension}")
                    .Where(f => !IsBaselineFile(f))
                    .OrderByDescending(f => File.GetCreationTime(f))
                    .Skip(MaxBackups)
                    .ToList();

                foreach (var file in files)
                {
                    await DeleteBackupAsync(file);
                }
            }
            catch { }
        }

        private static bool IsBaselineFile(string file)
        {
            try
            {
                using var doc = JsonDocument.Parse(File.ReadAllText(file));
                return doc.RootElement.TryGetProperty("IsBaseline", out var b) && b.ValueKind == JsonValueKind.True;
            }
            catch { return false; }
        }

        /// <summary>
        /// Export .reg file for RESTORING original values
        /// </summary>
        private async Task ExportRestoreRegFileAsync(BackupData backupData, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Windows Registry Editor Version 5.00");
            sb.AppendLine();
            sb.AppendLine("; AtlantHarden - RESTORE FILE");
            sb.AppendLine($"; Run this file to restore original values BEFORE hardening");
            sb.AppendLine("; Registry values only. Defender/ASR, firewall, audit-policy and other non-registry");
            sb.AppendLine("; changes are restored by the app's Restore button, not by this file.");
            sb.AppendLine($"; Created: {backupData.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"; Computer: {backupData.ComputerName}");
            sb.AppendLine($"; Total entries: {backupData.RegistryEntries.Count}");
            sb.AppendLine();

            // Group entries that had values to restore
            var entriesToRestore = backupData.RegistryEntries.Where(e => e.ValueExisted && e.OriginalValue != null);
            var groupedEntries = entriesToRestore.GroupBy(e => e.Path);

            foreach (var group in groupedEntries)
            {
                var regPath = ConvertToRegPath(group.Key);
                sb.AppendLine($"[{regPath}]");
                
                foreach (var entry in group)
                {
                    var valueStr = FormatRegValue(entry.OriginalValue, entry.ValueType);
                    sb.AppendLine($"\"{entry.KeyName}\"={valueStr}");
                }
                sb.AppendLine();
            }

            // Add deletion entries for values that didn't exist before
            var entriesToDelete = backupData.RegistryEntries.Where(e => !e.ValueExisted);
            if (entriesToDelete.Any())
            {
                sb.AppendLine("; Delete values that were created by hardening:");
                var deleteGroups = entriesToDelete.GroupBy(e => e.Path);
                
                foreach (var group in deleteGroups)
                {
                    var regPath = ConvertToRegPath(group.Key);
                    sb.AppendLine($"[{regPath}]");
                    
                    foreach (var entry in group)
                    {
                        // Use "-" prefix to delete a value in .reg file
                        sb.AppendLine($"\"{entry.KeyName}\"=-");
                    }
                    sb.AppendLine();
                }
            }

            await File.WriteAllTextAsync(filePath, sb.ToString(), Encoding.Unicode);
        }

        /// <summary>
        /// Export .reg file showing what values were APPLIED (for reference)
        /// </summary>
        private async Task ExportAppliedRegFileAsync(BackupData backupData, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Windows Registry Editor Version 5.00");
            sb.AppendLine();
            sb.AppendLine("; AtlantHarden - APPLIED HARDENING SETTINGS");
            sb.AppendLine($"; This file shows what settings were applied");
            sb.AppendLine($"; Created: {backupData.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"; Computer: {backupData.ComputerName}");
            sb.AppendLine($"; Total entries: {backupData.RegistryEntries.Count}");
            sb.AppendLine();

            var groupedEntries = backupData.RegistryEntries
                .Where(e => e.NewValue != null)
                .GroupBy(e => e.Path);

            foreach (var group in groupedEntries)
            {
                var regPath = ConvertToRegPath(group.Key);
                sb.AppendLine($"; {group.First().SettingName}");
                sb.AppendLine($"[{regPath}]");
                
                foreach (var entry in group)
                {
                    var valueStr = FormatRegValue(entry.NewValue, string.IsNullOrEmpty(entry.NewValueType) ? entry.ValueType : entry.NewValueType);
                    sb.AppendLine($"\"{entry.KeyName}\"={valueStr}");
                }
                sb.AppendLine();
            }

            await File.WriteAllTextAsync(filePath, sb.ToString(), Encoding.Unicode);
        }

        private string ConvertToRegPath(string path)
        {
            return path
                .Replace("HKLM\\", "HKEY_LOCAL_MACHINE\\")
                .Replace("HKCU\\", "HKEY_CURRENT_USER\\")
                .Replace("HKCR\\", "HKEY_CLASSES_ROOT\\")
                .Replace("HKU\\", "HKEY_USERS\\");
        }

        internal string FormatRegValue(object? value, string valueType)
        {
            if (value == null) return "\"\"";
            var type = valueType.ToUpperInvariant();
            var kind = RegistryService.ParseValueKind(type);

            // Values read back from backup JSON arrive as JsonElement.
            if (value is JsonElement) value = ConvertJsonElement(value, kind);

            switch (type)
            {
                case "REG_DWORD":
                    // Registry DWORDs read back as signed int (0xFFFFFFFF -> -1); keep the bit pattern.
                    return $"dword:{unchecked((uint)Convert.ToInt64(value)):x8}";
                case "REG_QWORD":
                    return $"hex(b):{FormatQWord(unchecked((ulong)Convert.ToInt64(value)))}";
                case "REG_BINARY":
                    return value is byte[] bytes ? $"hex:{FormatBinary(bytes)}" : "hex:";
                case "REG_EXPAND_SZ":
                    return $"hex(2):{FormatExpandString(value.ToString() ?? "")}";
                case "REG_MULTI_SZ":
                    var parts = RegistryService.ToMultiString(value);
                    var multi = Encoding.Unicode.GetBytes(string.Join("\0", parts) + "\0\0");
                    return $"hex(7):{FormatBinary(multi)}";
                default:
                    return $"\"{EscapeRegString(value.ToString() ?? "")}\"";
            }
        }

        private string FormatQWord(ulong value)
        {
            var bytes = BitConverter.GetBytes(value);
            return string.Join(",", bytes.Select(b => b.ToString("x2")));
        }

        private string FormatBinary(byte[] bytes)
        {
            return string.Join(",", bytes.Select(b => b.ToString("x2")));
        }

        private string FormatExpandString(string str)
        {
            var bytes = Encoding.Unicode.GetBytes(str + "\0");
            return string.Join(",", bytes.Select(b => b.ToString("x2")));
        }

        private string EscapeRegString(string str)
        {
            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        #endregion
    }

    #region Data Classes

    public class BackupData
    {
        public DateTime CreatedAt { get; set; }
        public string ComputerName { get; set; } = "";
        public string WindowsVersion { get; set; } = "";
        public string Label { get; set; } = "";
        public bool IsBaseline { get; set; }
        public List<RegistryBackupEntry> RegistryEntries { get; set; } = new();
        public List<SettingStateEntry> StateEntries { get; set; } = new();
        public List<string> AppliedSettingIds { get; set; } = new();
    }

    /// <summary>Pre-change state of a non-registry setting (Defender, ASR, firewall, audit, command).</summary>
    public class SettingStateEntry
    {
        public string SettingId { get; set; } = "";
        public string? SettingName { get; set; }
        public string Type { get; set; } = "";
        public bool WasApplied { get; set; }
        public string? PreviousValue { get; set; }
    }

    public class RegistryBackupEntry
    {
        public string Path { get; set; } = "";
        public string KeyName { get; set; } = "";
        public string ValueType { get; set; } = "REG_DWORD";
        public bool KeyExisted { get; set; }
        public bool ValueExisted { get; set; }
        public object? OriginalValue { get; set; }
        public object? NewValue { get; set; }
        public string NewValueType { get; set; } = "";
        public string SettingId { get; set; } = "";
        public string? SettingName { get; set; }
    }

    public class SystemRestorePoint
    {
        public int SequenceNumber { get; set; }
        public string Description { get; set; } = "";
        public DateTime CreationTime { get; set; }
        public int RestorePointType { get; set; }
    }

    #endregion
}
