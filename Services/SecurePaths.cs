using System;
using System.IO;

namespace AtlantHarden.Services
{
    /// <summary>
    /// Locations for data this ELEVATED app writes and later trusts (backups it restores as
    /// administrator, its activity log). They live under %ProgramFiles%\AtlantHarden, where only
    /// administrators/SYSTEM can create or modify files — unlike %LOCALAPPDATA% (writable by any
    /// unelevated process of the same user, so malware could plant a backup that a later restore
    /// would write to HKLM as admin, or swap the log folder for a junction) or %ProgramData%
    /// (where any user can pre-create the folder and own it).
    /// </summary>
    public static class SecurePaths
    {
        public static string Root => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "AtlantHarden");

        public static string Backups => Path.Combine(Root, "Backups");
        public static string Logs => Path.Combine(Root, "Logs");

        /// <summary>Where v2.1 and earlier kept backups (user-writable; not trusted for restore).</summary>
        public static string LegacyBackups => Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AtlantHarden", "Backups");

        /// <summary>Create the directory if needed and refuse to use it if it's a reparse point (junction/symlink).</summary>
        public static string Ensure(string path)
        {
            Directory.CreateDirectory(path);
            var info = new DirectoryInfo(path);
            if (info.Attributes.HasFlag(FileAttributes.ReparsePoint))
                throw new IOException($"Refusing to use {path}: it is a junction or symbolic link.");
            return path;
        }
    }
}
