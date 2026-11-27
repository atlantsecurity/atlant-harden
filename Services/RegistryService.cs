using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace AtlantHarden.Services
{
    public class RegistryService
    {
        /// <summary>
        /// SID of the signed-in (interactive) user whose HKCU per-user settings should change.
        /// When a standard user elevates with a separate admin account, this process's own
        /// HKCU is the ADMIN's hive; per-user policies written there would never reach the person
        /// at the keyboard. In that case HKCU paths are redirected to HKU\&lt;their SID&gt;.
        /// Null when the interactive user is the same account as this process (normal case).
        /// </summary>
        public static string? TargetUserSid { get; private set; }

        /// <summary>DOMAIN\user whose per-user (HKCU) settings this app changes.</summary>
        public static string TargetUserName { get; private set; } = Environment.UserDomainName + "\\" + Environment.UserName;

        static RegistryService()
        {
            try { DetectInteractiveUser(); } catch { /* fall back to this process's HKCU */ }
        }

        [DllImport("wtsapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, int infoClass,
            out IntPtr buffer, out int bytesReturned);
        [DllImport("wtsapi32.dll")]
        private static extern void WTSFreeMemory(IntPtr memory);
        private const int WTSUserName = 5, WTSDomainName = 7;

        private static string? QuerySession(int sessionId, int infoClass)
        {
            if (!WTSQuerySessionInformation(IntPtr.Zero, sessionId, infoClass, out var buf, out _)) return null;
            try { return Marshal.PtrToStringUni(buf); } finally { WTSFreeMemory(buf); }
        }

        private static void DetectInteractiveUser()
        {
            var session = System.Diagnostics.Process.GetCurrentProcess().SessionId;
            var user = QuerySession(session, WTSUserName);
            var domain = QuerySession(session, WTSDomainName);
            if (string.IsNullOrEmpty(user)) return;

            var account = string.IsNullOrEmpty(domain) ? user : $"{domain}\\{user}";
            var sid = new NTAccount(account).Translate(typeof(SecurityIdentifier)).Value;
            var mySid = WindowsIdentity.GetCurrent().User?.Value;
            if (string.Equals(sid, mySid, StringComparison.OrdinalIgnoreCase)) return;

            // Only redirect when that user's hive is actually loaded (they're signed in).
            using var hive = Registry.Users.OpenSubKey(sid);
            if (hive == null) return;
            TargetUserSid = sid;
            TargetUserName = account;
        }

        public static object? ReadValue(string path, string keyName)
        {
            try
            {
                var (rootKey, subPath) = ParseRegistryPath(path);
                using var key = rootKey.OpenSubKey(subPath);
                return key?.GetValue(keyName);
            }
            catch
            {
                return null;
            }
        }

        public static bool WriteValue(string path, string keyName, object value, RegistryValueKind valueKind)
        {
            try
            {
                var (rootKey, subPath) = ParseRegistryPath(path);
                using var key = rootKey.CreateSubKey(subPath, true);
                key?.SetValue(keyName, CoerceValue(value, valueKind), valueKind);
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Coerce a setting's value object to the CLR type the registry API expects for the kind.
        // Fixes two silent failures: DWORDs authored as uint (e.g. 0xFFFFFFFF overflows Int32 in
        // SetValue) and REG_MULTI_SZ values authored as a single '\0'/';'-separated string.
        private static object CoerceValue(object value, RegistryValueKind valueKind) => valueKind switch
        {
            RegistryValueKind.DWord => unchecked((int)Convert.ToInt64(value)),
            RegistryValueKind.QWord => Convert.ToInt64(value),
            RegistryValueKind.MultiString => ToMultiString(value),
            RegistryValueKind.String or RegistryValueKind.ExpandString => value?.ToString() ?? string.Empty,
            _ => value
        };

        // A REG_MULTI_SZ value may be authored as a string[] or as a single string with '\0'
        // (or ';') separators. Normalize to the string[] the registry API requires.
        public static string[] ToMultiString(object? value) =>
            value is string[] arr
                ? arr
                : (value?.ToString() ?? string.Empty).Split(new[] { '\0', ';' }, StringSplitOptions.RemoveEmptyEntries);

        public static bool DeleteValue(string path, string keyName)
        {
            try
            {
                var (rootKey, subPath) = ParseRegistryPath(path);
                using var key = rootKey.OpenSubKey(subPath, true);
                key?.DeleteValue(keyName, false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteKey(string path)
        {
            try
            {
                var (rootKey, subPath) = ParseRegistryPath(path);
                rootKey.DeleteSubKeyTree(subPath, false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool KeyExists(string path)
        {
            try
            {
                var (rootKey, subPath) = ParseRegistryPath(path);
                using var key = rootKey.OpenSubKey(subPath);
                return key != null;
            }
            catch
            {
                return false;
            }
        }

        public static bool ValueExists(string path, string keyName)
        {
            try
            {
                var (rootKey, subPath) = ParseRegistryPath(path);
                using var key = rootKey.OpenSubKey(subPath);
                return key?.GetValue(keyName) != null;
            }
            catch
            {
                return false;
            }
        }

        public static RegistryValueKind GetValueKind(string path, string keyName)
        {
            try
            {
                var (rootKey, subPath) = ParseRegistryPath(path);
                using var key = rootKey.OpenSubKey(subPath);
                return key?.GetValueKind(keyName) ?? RegistryValueKind.Unknown;
            }
            catch
            {
                return RegistryValueKind.Unknown;
            }
        }

        public static Dictionary<string, object?> GetAllValues(string path)
        {
            var values = new Dictionary<string, object?>();
            try
            {
                var (rootKey, subPath) = ParseRegistryPath(path);
                using var key = rootKey.OpenSubKey(subPath);
                if (key != null)
                {
                    foreach (var valueName in key.GetValueNames())
                    {
                        values[valueName] = key.GetValue(valueName);
                    }
                }
            }
            catch
            {
                // Return empty dictionary on error
            }
            return values;
        }

        public static RegistryValueKind ParseValueKind(string valueType)
        {
            return valueType.ToUpperInvariant() switch
            {
                "REG_SZ" => RegistryValueKind.String,
                "REG_DWORD" => RegistryValueKind.DWord,
                "REG_QWORD" => RegistryValueKind.QWord,
                "REG_BINARY" => RegistryValueKind.Binary,
                "REG_MULTI_SZ" => RegistryValueKind.MultiString,
                "REG_EXPAND_SZ" => RegistryValueKind.ExpandString,
                _ => RegistryValueKind.String
            };
        }

        public static string ValueKindToString(RegistryValueKind kind)
        {
            return kind switch
            {
                RegistryValueKind.String => "REG_SZ",
                RegistryValueKind.DWord => "REG_DWORD",
                RegistryValueKind.QWord => "REG_QWORD",
                RegistryValueKind.Binary => "REG_BINARY",
                RegistryValueKind.MultiString => "REG_MULTI_SZ",
                RegistryValueKind.ExpandString => "REG_EXPAND_SZ",
                _ => "REG_SZ"
            };
        }

        private static (RegistryKey rootKey, string subPath) ParseRegistryPath(string path)
        {
            var normalizedPath = path.Replace("HKEY_LOCAL_MACHINE", "HKLM")
                                     .Replace("HKEY_CURRENT_USER", "HKCU")
                                     .Replace("HKEY_CLASSES_ROOT", "HKCR")
                                     .Replace("HKEY_USERS", "HKU")
                                     .Replace("HKEY_CURRENT_CONFIG", "HKCC");

            var parts = normalizedPath.Split(new[] { '\\' }, 2);
            var rootKeyName = parts[0].ToUpperInvariant();
            var subPath = parts.Length > 1 ? parts[1] : string.Empty;

            if (rootKeyName == "HKCU" && TargetUserSid != null)
                return (Registry.Users, string.IsNullOrEmpty(subPath) ? TargetUserSid : TargetUserSid + "\\" + subPath);

            RegistryKey rootKey = rootKeyName switch
            {
                "HKLM" => Registry.LocalMachine,
                "HKCU" => Registry.CurrentUser,
                "HKCR" => Registry.ClassesRoot,
                "HKU" => Registry.Users,
                "HKCC" => Registry.CurrentConfig,
                _ => throw new ArgumentException($"Unknown registry root: {rootKeyName}")
            };

            return (rootKey, subPath);
        }
    }
}
