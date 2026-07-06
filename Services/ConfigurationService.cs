using AtlantHarden.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AtlantHarden.Services
{
    /// <summary>
    /// Service for exporting and importing security configuration profiles
    /// </summary>
    public class ConfigurationService
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        /// <summary>App version, read from the assembly so it never drifts from the .csproj.</summary>
        public static string CurrentAppVersion { get; } =
            System.Reflection.Assembly.GetExecutingAssembly().GetName().Version is { } v
                ? $"{v.Major}.{v.Minor}.{v.Build}" : "2.0.0";

        /// <summary>
        /// Configuration profile containing enabled settings
        /// </summary>
        public class ConfigurationProfile
        {
            public string Name { get; set; } = "Custom Profile";
            public string Description { get; set; } = "";
            public string CreatedBy { get; set; } = "AtlantHarden";
            public DateTime CreatedAt { get; set; } = DateTime.Now;
            public string AppVersion { get; set; } = CurrentAppVersion;
            public List<string> EnabledSettingIds { get; set; } = new();
            public Dictionary<string, object?> CustomValues { get; set; } = new();
            
            // Metadata
            public int TotalSettings { get; set; }
            public int EnabledCount { get; set; }
            public ProfileStatistics? Statistics { get; set; }
        }

        public class ProfileStatistics
        {
            public int WindowsDefender { get; set; }
            public int AttackSurfaceReduction { get; set; }
            public int NetworkSecurity { get; set; }
            public int CredentialProtection { get; set; }
            public int BrowserHardening { get; set; }
            public int Privacy { get; set; }
            public int Logging { get; set; }
            public int SystemHardening { get; set; }
            public int STIGSettings { get; set; }
            public int ACSCSettings { get; set; }
        }

        /// <summary>
        /// Export current configuration to a JSON file
        /// </summary>
        public static bool ExportConfiguration(IEnumerable<HardeningSetting> settings, string filePath, string profileName = "Custom Profile", string description = "")
        {
            try
            {
                var allSettings = settings.ToList();
                var enabledSettings = allSettings.Where(s => s.IsEnabled).ToList();

                var profile = new ConfigurationProfile
                {
                    Name = profileName,
                    Description = description,
                    CreatedAt = DateTime.Now,
                    AppVersion = CurrentAppVersion,
                    TotalSettings = allSettings.Count,
                    EnabledCount = enabledSettings.Count,
                    EnabledSettingIds = enabledSettings.Select(s => s.Id).ToList(),
                    Statistics = new ProfileStatistics
                    {
                        WindowsDefender = enabledSettings.Count(s => s.Category == SettingCategory.WindowsDefender),
                        AttackSurfaceReduction = enabledSettings.Count(s => s.Category == SettingCategory.AttackSurfaceReduction),
                        NetworkSecurity = enabledSettings.Count(s => s.Category == SettingCategory.NetworkSecurity),
                        CredentialProtection = enabledSettings.Count(s => s.Category == SettingCategory.CredentialProtection),
                        BrowserHardening = enabledSettings.Count(s => 
                            s.Category == SettingCategory.EdgeHardening || 
                            s.Category == SettingCategory.ChromeHardening || 
                            s.Category == SettingCategory.FirefoxHardening),
                        Privacy = enabledSettings.Count(s => s.Category == SettingCategory.Privacy),
                        Logging = enabledSettings.Count(s => s.Category == SettingCategory.Logging),
                        SystemHardening = enabledSettings.Count(s => s.Category == SettingCategory.SystemHardening),
                        STIGSettings = enabledSettings.Count(s => s.IsStig),
                        ACSCSettings = enabledSettings.Count(s => s.IsACSC)
                    }
                };

                // Store any custom values that differ from defaults
                foreach (var setting in enabledSettings)
                {
                    if (setting.RecommendedValue != null)
                    {
                        profile.CustomValues[setting.Id] = setting.RecommendedValue;
                    }
                }

                var json = JsonSerializer.Serialize(profile, JsonOptions);
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error exporting configuration: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Import configuration from a JSON file
        /// </summary>
        public static ConfigurationProfile? LoadConfiguration(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return null;

                var json = File.ReadAllText(filePath);
                return JsonSerializer.Deserialize<ConfigurationProfile>(json, JsonOptions);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading configuration: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Apply a configuration profile to settings
        /// </summary>
        public static int ApplyConfiguration(ConfigurationProfile profile, IEnumerable<HardeningSetting> settings)
        {
            int appliedCount = 0;
            var settingsList = settings.ToList();
            var enabledIds = new HashSet<string>(profile.EnabledSettingIds);

            foreach (var setting in settingsList)
            {
                bool shouldEnable = enabledIds.Contains(setting.Id);
                
                if (setting.IsEnabled != shouldEnable)
                {
                    setting.IsEnabled = shouldEnable;
                    appliedCount++;
                }
            }

            return appliedCount;
        }

        /// <summary>
        /// Validate a configuration file
        /// </summary>
        public static (bool isValid, string message, ConfigurationProfile? profile) ValidateConfiguration(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return (false, $"File not found: {filePath}", null);

                var json = File.ReadAllText(filePath);
                
                if (string.IsNullOrWhiteSpace(json))
                    return (false, "Configuration file is empty", null);

                var profile = JsonSerializer.Deserialize<ConfigurationProfile>(json, JsonOptions);
                
                if (profile == null)
                    return (false, "Failed to parse configuration file", null);

                if (profile.EnabledSettingIds == null || profile.EnabledSettingIds.Count == 0)
                    return (false, "Configuration contains no enabled settings", null);

                return (true, $"Valid configuration: {profile.Name} ({profile.EnabledCount} settings)", profile);
            }
            catch (JsonException ex)
            {
                return (false, $"Invalid JSON format: {ex.Message}", null);
            }
            catch (Exception ex)
            {
                return (false, $"Error reading configuration: {ex.Message}", null);
            }
        }

        /// <summary>
        /// Get default configuration file path
        /// </summary>
        public static string GetDefaultConfigPath()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var configDir = Path.Combine(appData, "AtlantHarden", "Configs");
            Directory.CreateDirectory(configDir);
            return configDir;
        }

        /// <summary>
        /// Create a summary of a configuration profile
        /// </summary>
        public static string GetProfileSummary(ConfigurationProfile profile)
        {
            var summary = new System.Text.StringBuilder();
            summary.AppendLine($"Profile: {profile.Name}");
            summary.AppendLine($"Created: {profile.CreatedAt:yyyy-MM-dd HH:mm}");
            summary.AppendLine($"Settings: {profile.EnabledCount} of {profile.TotalSettings} enabled");
            
            if (profile.Statistics != null)
            {
                summary.AppendLine();
                summary.AppendLine("Categories:");
                if (profile.Statistics.WindowsDefender > 0)
                    summary.AppendLine($"  • Windows Defender: {profile.Statistics.WindowsDefender}");
                if (profile.Statistics.AttackSurfaceReduction > 0)
                    summary.AppendLine($"  • Attack Surface Reduction: {profile.Statistics.AttackSurfaceReduction}");
                if (profile.Statistics.NetworkSecurity > 0)
                    summary.AppendLine($"  • Network Security: {profile.Statistics.NetworkSecurity}");
                if (profile.Statistics.CredentialProtection > 0)
                    summary.AppendLine($"  • Credential Protection: {profile.Statistics.CredentialProtection}");
                if (profile.Statistics.BrowserHardening > 0)
                    summary.AppendLine($"  • Browser Hardening: {profile.Statistics.BrowserHardening}");
                if (profile.Statistics.SystemHardening > 0)
                    summary.AppendLine($"  • System Hardening: {profile.Statistics.SystemHardening}");
                if (profile.Statistics.STIGSettings > 0)
                    summary.AppendLine($"  • DISA STIG: {profile.Statistics.STIGSettings}");
                if (profile.Statistics.ACSCSettings > 0)
                    summary.AppendLine($"  • ACSC: {profile.Statistics.ACSCSettings}");
            }

            return summary.ToString();
        }
    }
}
