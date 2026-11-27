using System;
using System.Collections.Generic;

namespace AtlantHarden.Models
{
    public class BackupInfo
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Description { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public int SettingsCount { get; set; }
        public long FileSizeBytes { get; set; }
        public string Version { get; set; } = "1.0";
        public List<string> AppliedCategories { get; set; } = new();
        public BackupStatus Status { get; set; } = BackupStatus.Complete;

        /// <summary>What the backup was taken before (e.g. "Recommended profile").</summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>The first backup ever taken on this PC: its original state. Never auto-pruned.</summary>
        public bool IsBaseline { get; set; }
    }

    public enum BackupStatus
    {
        Creating,
        Complete,
        Corrupted,
        Restoring
    }
}
