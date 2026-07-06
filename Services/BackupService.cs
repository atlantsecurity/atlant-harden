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
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _backupDirectory = Path.Combine(appData, "AtlantHarden", "Backups");
            
            if (!Directory.Exists(_backupDirectory))
            {
                Directory.CreateDirectory(_backupDirectory);
            }
        }

        public string BackupDirectory => _backupDirectory;

        /// <summary>
        /// Creates a backup of current registry values BEFORE applying changes
        /// </summary>
        public async Task<BackupInfo> CreateBackupAsync(List<HardeningSetting> settings, string description = "")
        {
            var enabledSettings = settings.Where(s => s.IsEnabled && s.Type == SettingType.Registry).ToList();
            
            var backupInfo = new BackupInfo
            {
                Description = string.IsNullOrEmpty(description) 
                    ? $"Backup before applying {enabledSettings.Count} settings"
                    : description,
                AppliedCategories = enabledSettings
                    .Select(s => s.Category.ToString())
                    .Distinct()
                    .ToList()
            };

            var backupData = new BackupData
            {
                CreatedAt = DateTime.Now,
                ComputerName = Environment.MachineName,
                WindowsVersion = Environment.OSVersion.ToString(),
                RegistryEntries = new List<RegistryBackupEntry>(),
                AppliedSettingIds = new List<string>()
            };

            // Backup registry entries for ALL enabled settings
            foreach (var setting in enabledSettings)
            {
                if (string.IsNullOrEmpty(setting.RegistryPath) || string.IsNullOrEmpty(setting.RegistryKey))
                    continue;

                var keyExists = RegistryService.KeyExists(setting.RegistryPath);
                var valueExists = keyExists && RegistryService.ValueExists(setting.RegistryPath, setting.RegistryKey);
                object? originalValue = null;
                string originalValueType = setting.RegistryValueType ?? "REG_DWORD";

                if (valueExists)
                {
                    originalValue = RegistryService.ReadValue(setting.RegistryPath, setting.RegistryKey);
                    var kind = RegistryService.GetValueKind(setting.RegistryPath, setting.RegistryKey);
                    originalValueType = RegistryService.ValueKindToString(kind);
                }

                var entry = new RegistryBackupEntry
                {
                    Path = setting.RegistryPath,
                    KeyName = setting.RegistryKey,
                    ValueType = originalValueType,
                    KeyExisted = keyExists,
                    ValueExisted = valueExists,
                    OriginalValue = originalValue,
                    NewValue = setting.RecommendedValue,
                    SettingId = setting.Id,
                    SettingName = setting.Name
                };
                
                backupData.RegistryEntries.Add(entry);
                backupData.AppliedSettingIds.Add(setting.Id);
            }

            // Save backup file
            var fileName = $"backup_{backupInfo.CreatedAt:yyyyMMdd_HHmmss}{BackupFileExtension}";
            backupInfo.FilePath = Path.Combine(_backupDirectory, fileName);
            
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(backupData, options);
            await File.WriteAllTextAsync(backupInfo.FilePath, json);
            
            backupInfo.FileSizeBytes = new FileInfo(backupInfo.FilePath).Length;
            backupInfo.SettingsCount = backupData.RegistryEntries.Count;

            // Export .reg file for manual restoration
            var regFileName = $"restore_{backupInfo.CreatedAt:yyyyMMdd_HHmmss}.reg";
            var regFilePath = Path.Combine(_backupDirectory, regFileName);
            await ExportRestoreRegFileAsync(backupData, regFilePath);

            // Also create a .reg file showing what was applied (for reference)
            var appliedRegFileName = $"applied_{backupInfo.CreatedAt:yyyyMMdd_HHmmss}.reg";
            var appliedRegFilePath = Path.Combine(_backupDirectory, appliedRegFileName);
            await ExportAppliedRegFileAsync(backupData, appliedRegFilePath);

            await CleanupOldBackupsAsync();

            return backupInfo;
        }

        /// <summary>
        /// Restore registry values from backup
        /// </summary>
        public async Task<(bool success, int restored, int failed, List<string> errors)> RestoreBackupAsync(string backupFilePath)
        {
            var errors = new List<string>();
            int restored = 0;
            int failed = 0;

            try
            {
                var json = await File.ReadAllTextAsync(backupFilePath);
                var backupData = JsonSerializer.Deserialize<BackupData>(json);
                
                if (backupData == null)
                    return (false, 0, 0, new List<string> { "Could not read backup file" });

                foreach (var entry in backupData.RegistryEntries)
                {
                    try
                    {
                        if (!entry.ValueExisted)
                        {
                            // Value didn't exist before - delete it to restore original state
                            var deleted = RegistryService.DeleteValue(entry.Path, entry.KeyName);
                            if (deleted)
                            {
                                restored++;
                            }
                            else
                            {
                                // It's okay if delete fails - might already not exist
                                restored++;
                            }
                        }
                        else if (entry.OriginalValue != null)
                        {
                            // Restore original value
                            var valueKind = RegistryService.ParseValueKind(entry.ValueType);
                            object valueToWrite = ConvertJsonElement(entry.OriginalValue, valueKind);
                            
                            var written = RegistryService.WriteValue(entry.Path, entry.KeyName, valueToWrite, valueKind);
                            if (written)
                            {
                                restored++;
                            }
                            else
                            {
                                failed++;
                                errors.Add($"Failed to restore: {entry.SettingName ?? entry.KeyName}");
                            }
                        }
                        else
                        {
                            // Value existed but was null - unusual case
                            restored++;
                        }
                    }
                    catch (Exception ex)
                    {
                        failed++;
                        errors.Add($"{entry.SettingName ?? entry.KeyName}: {ex.Message}");
                    }
                }

                return (failed == 0, restored, failed, errors);
            }
            catch (Exception ex)
            {
                return (false, 0, 0, new List<string> { ex.Message });
            }
        }

        /// <summary>
        /// Import and apply a .reg file directly
        /// </summary>
        public async Task<(bool success, string message)> ImportRegFileAsync(string regFilePath)
        {
            try
            {
                if (!File.Exists(regFilePath))
                    return (false, "File not found");

                var psi = new ProcessStartInfo
                {
                    FileName = "reg.exe",
                    Arguments = $"import \"{regFilePath}\"",
                    UseShellExecute = true,
                    Verb = "runas",
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);
                if (process == null)
                    return (false, "Could not start reg.exe");

                await process.WaitForExitAsync();
                
                if (process.ExitCode == 0)
                    return (true, "Registry file imported successfully");
                else
                    return (false, $"reg.exe exited with code {process.ExitCode}");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        private object ConvertJsonElement(object value, RegistryValueKind kind)
        {
            if (value is JsonElement element)
            {
                return kind switch
                {
                    RegistryValueKind.DWord => element.TryGetInt32(out int i) ? i : (element.TryGetInt64(out long l) ? (int)l : 0),
                    RegistryValueKind.QWord => element.TryGetInt64(out long l64) ? l64 : 0L,
                    RegistryValueKind.String => element.GetString() ?? "",
                    RegistryValueKind.ExpandString => element.GetString() ?? "",
                    RegistryValueKind.Binary => element.ValueKind == JsonValueKind.Array 
                        ? element.EnumerateArray().Select(e => (byte)e.GetInt32()).ToArray() 
                        : Array.Empty<byte>(),
                    _ => element.ToString() ?? ""
                };
            }
            return value;
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
                            Description = $"{backupData.RegistryEntries.Count} registry entries",
                            SettingsCount = backupData.RegistryEntries.Count,
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

        /// <summary>
        /// Create a Windows System Restore Point
        /// </summary>
        public async Task<(bool success, string message)> CreateSystemRestorePointAsync(string description = "AtlantHarden")
        {
            try
            {
                // First ensure System Restore is enabled
                var enableResult = await EnableSystemRestoreAsync();
                if (!enableResult.success && !enableResult.message.Contains("already enabled"))
                {
                    // Try to continue anyway
                }

                // Create restore point using PowerShell
                var result = await Task.Run(() =>
                {
                    try
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = "powershell.exe",
                            Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"Checkpoint-Computer -Description '{description}' -RestorePointType 'MODIFY_SETTINGS'\"",
                            UseShellExecute = true,
                            Verb = "runas",
                            CreateNoWindow = false
                        };

                        using var process = Process.Start(psi);
                        if (process == null)
                            return (false, "Could not start PowerShell");

                        process.WaitForExit(120000); // 2 minute timeout

                        if (!process.HasExited)
                        {
                            process.Kill();
                            return (false, "Operation timed out");
                        }

                        // Exit code 0 = success, 1111 = already created within 24 hours
                        if (process.ExitCode == 0)
                            return (true, "System Restore Point created successfully!");
                        else if (process.ExitCode == 1)
                            return (false, "Windows only allows one restore point every 24 hours. Try again later.");
                        else
                            return (false, $"PowerShell exited with code {process.ExitCode}. Make sure you're running as Administrator.");
                    }
                    catch (Exception ex)
                    {
                        return (false, ex.Message);
                    }
                });

                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Enable System Restore on the system drive
        /// </summary>
        public async Task<(bool success, string message)> EnableSystemRestoreAsync()
        {
            try
            {
                var systemDrive = Path.GetPathRoot(Environment.SystemDirectory) ?? "C:\\";
                
                var result = await Task.Run(() =>
                {
                    try
                    {
                        var psi = new ProcessStartInfo
                        {
                            FileName = "powershell.exe",
                            Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"Enable-ComputerRestore -Drive '{systemDrive}'\"",
                            UseShellExecute = true,
                            Verb = "runas",
                            CreateNoWindow = true
                        };

                        using var process = Process.Start(psi);
                        if (process == null)
                            return (false, "Could not start PowerShell");

                        process.WaitForExit(30000);

                        return process.ExitCode == 0 
                            ? (true, "System Restore enabled") 
                            : (false, "Could not enable System Restore");
                    }
                    catch (Exception ex)
                    {
                        return (false, ex.Message);
                    }
                });

                return result;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// Open Windows System Restore dialog
        /// </summary>
        public void OpenSystemRestoreDialog()
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "rstrui.exe",
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(psi);
            }
            catch
            {
                // Try alternative method
                try
                {
                    Process.Start("control.exe", "/name Microsoft.Recovery");
                }
                catch { }
            }
        }

        /// <summary>
        /// Get list of available System Restore points
        /// </summary>
        public async Task<List<SystemRestorePoint>> GetRestorePointsAsync()
        {
            var points = new List<SystemRestorePoint>();
            
            try
            {
                var result = await Task.Run(() =>
                {
                    var psi = new ProcessStartInfo
                    {
                        FileName = "powershell.exe",
                        Arguments = "-NoProfile -ExecutionPolicy Bypass -Command \"Get-ComputerRestorePoint | ConvertTo-Json\"",
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        CreateNoWindow = true
                    };

                    using var process = Process.Start(psi);
                    if (process == null) return "";

                    var output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit(30000);
                    return output;
                });

                if (!string.IsNullOrWhiteSpace(result))
                {
                    // Parse JSON output
                    using var doc = JsonDocument.Parse(result.StartsWith("[") ? result : $"[{result}]");
                    foreach (var element in doc.RootElement.EnumerateArray())
                    {
                        points.Add(new SystemRestorePoint
                        {
                            SequenceNumber = element.GetProperty("SequenceNumber").GetInt32(),
                            Description = element.GetProperty("Description").GetString() ?? "",
                            CreationTime = DateTime.Parse(element.GetProperty("CreationTime").GetString() ?? DateTime.Now.ToString()),
                            RestorePointType = element.GetProperty("RestorePointType").GetInt32()
                        });
                    }
                }
            }
            catch
            {
                // Return empty list on error
            }

            return points.OrderByDescending(p => p.CreationTime).ToList();
        }

        #endregion

        #region Private Methods

        private async Task CleanupOldBackupsAsync()
        {
            try
            {
                var files = Directory.GetFiles(_backupDirectory, $"*{BackupFileExtension}")
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

            await File.WriteAllTextAsync(filePath, sb.ToString());
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
                    var valueStr = FormatRegValue(entry.NewValue, entry.ValueType);
                    sb.AppendLine($"\"{entry.KeyName}\"={valueStr}");
                }
                sb.AppendLine();
            }

            await File.WriteAllTextAsync(filePath, sb.ToString());
        }

        private string ConvertToRegPath(string path)
        {
            return path
                .Replace("HKLM\\", "HKEY_LOCAL_MACHINE\\")
                .Replace("HKCU\\", "HKEY_CURRENT_USER\\")
                .Replace("HKCR\\", "HKEY_CLASSES_ROOT\\");
        }

        private string FormatRegValue(object? value, string valueType)
        {
            if (value == null) return "\"\"";

            // Handle JsonElement
            if (value is JsonElement element)
            {
                value = valueType.ToUpperInvariant() switch
                {
                    "REG_DWORD" => element.TryGetInt32(out int i) ? i : (element.TryGetInt64(out long l) ? (int)l : 0),
                    "REG_QWORD" => element.TryGetInt64(out long l) ? l : 0L,
                    _ => element.GetString() ?? element.ToString()
                };
            }

            return valueType.ToUpperInvariant() switch
            {
                "REG_DWORD" => $"dword:{Convert.ToUInt32(value):x8}",
                "REG_QWORD" => $"hex(b):{FormatQWord(Convert.ToUInt64(value))}",
                "REG_BINARY" when value is byte[] bytes => $"hex:{FormatBinary(bytes)}",
                "REG_EXPAND_SZ" => $"hex(2):{FormatExpandString(value.ToString() ?? "")}",
                _ => $"\"{EscapeRegString(value.ToString() ?? "")}\""
            };
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
        public List<RegistryBackupEntry> RegistryEntries { get; set; } = new();
        public List<string> AppliedSettingIds { get; set; } = new();
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
