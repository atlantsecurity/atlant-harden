using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AtlantHarden.Models;
using Microsoft.Win32;

namespace AtlantHarden.Services
{
    /// <summary>
    /// The "Bloatware Removal" catalog: a curated set of clearly-junk Microsoft Store apps,
    /// plus OEM/AV bloat (McAfee, Norton, vendor "assistant/update" apps, WildTangent games)
    /// that is DETECTED on the running machine so only relevant items are offered.
    ///
    /// These are SettingType.AppRemoval: "applied" means the app is gone. Removal is not
    /// auto-reversible (Store apps reinstall from the Store, Win32 apps from the vendor), so
    /// nothing here is part of Basic/Recommended/Maximum — it is a separate, review-first action.
    /// </summary>
    public static class BloatwareSettings
    {
        public static List<HardeningSetting> GetAllSettings()
        {
            var list = new List<HardeningSetting>();
            list.AddRange(GetStoreBloat());
            try { list.AddRange(DetectOemBloat()); } catch { /* detection is best-effort */ }
            return list;
        }

        // ---- Microsoft Store / Appx bloat (curated, vetted-safe) -----------------------------
        private static HardeningSetting Appx(string id, string name, string desc, string package, bool preselect)
            => new HardeningSetting
            {
                Id = "BLOAT_" + id,
                Name = name,
                Description = desc,
                Category = SettingCategory.Bloatware,
                Type = SettingType.AppRemoval,
                Risk = RiskLevel.Low,
                AppxName = package,
                // Destructive, so never pre-checked at rest — otherwise the cross-category
                // "Apply Selected" sweep could uninstall it from another view. The "Clean Up
                // Bloat" button selects the "recommended" items on demand; the sidebar shows
                // the list unchecked. Classification is carried in the "recommended" tag.
                IsEnabled = false,
                // Remove for every user AND the provisioned copy, so it doesn't return for new users.
                ApplyCommand =
                    $"Get-AppxPackage -AllUsers -Name '{package}' | Remove-AppxPackage -AllUsers -ErrorAction SilentlyContinue; " +
                    $"Get-AppxProvisionedPackage -Online | Where-Object {{ $_.DisplayName -eq '{package}' }} | " +
                    "Remove-AppxProvisionedPackage -Online -AllUsers -ErrorAction SilentlyContinue",
                ImpactWarning = "Removes the app for all users. Reinstall from the Microsoft Store if you need it back.",
                Tags = preselect
                    ? new[] { "bloatware", "appx", "cleanup", "recommended" }
                    : new[] { "bloatware", "appx", "cleanup" }
            };

        private static List<HardeningSetting> GetStoreBloat() => new()
        {
            // --- clearly junk: pre-selected -----------------------------------------------
            Appx("CandyCrushSaga", "Candy Crush Saga", "Pre-installed King game", "king.com.CandyCrushSaga", true),
            Appx("CandyCrushSoda", "Candy Crush Soda Saga", "Pre-installed King game", "king.com.CandyCrushSodaSaga", true),
            Appx("BubbleWitch3", "Bubble Witch 3 Saga", "Pre-installed King game", "king.com.BubbleWitch3Saga", true),
            Appx("FarmHeroes", "Farm Heroes Saga", "Pre-installed King game", "king.com.FarmHeroesSaga", true),
            Appx("Solitaire", "Microsoft Solitaire Collection", "Ad-supported solitaire games", "Microsoft.MicrosoftSolitaireCollection", true),
            Appx("3DBuilder", "3D Builder", "Legacy 3D printing app", "Microsoft.3DBuilder", true),
            Appx("3DViewer", "Mixed Reality / 3D Viewer", "Legacy 3D model viewer", "Microsoft.Microsoft3DViewer", true),
            Appx("MixedRealityPortal", "Mixed Reality Portal", "Windows MR setup (needs a headset)", "Microsoft.MixedReality.Portal", true),
            Appx("GetHelp", "Get Help", "Microsoft support-chat app", "Microsoft.GetHelp", true),
            Appx("Tips", "Tips", "Windows tips / suggestions app", "Microsoft.Getstarted", true),
            Appx("FeedbackHub", "Feedback Hub", "Windows telemetry / feedback app", "Microsoft.WindowsFeedbackHub", true),
            Appx("BingNews", "Microsoft News", "MSN news + ads", "Microsoft.BingNews", true),
            Appx("BingWeather", "MSN Weather", "MSN weather + ads", "Microsoft.BingWeather", true),
            Appx("OfficeHub", "Office / Microsoft 365 (trial hub)", "Office upsell hub", "Microsoft.MicrosoftOfficeHub", true),
            Appx("Skype", "Skype", "Pre-installed Skype", "Microsoft.SkypeApp", true),
            Appx("People", "People", "Legacy contacts app", "Microsoft.People", true),
            Appx("Wallet", "Wallet", "Legacy Microsoft Wallet", "Microsoft.Wallet", true),
            Appx("Clipchamp", "Clipchamp", "Video editor (upsell)", "Clipchamp.Clipchamp", true),
            Appx("MixedRealityLearning", "Mixed Reality Viewer", "Print 3D / MR", "Microsoft.Print3D", true),
            Appx("Whiteboard", "Microsoft Whiteboard", "Collaboration whiteboard", "Microsoft.Whiteboard", true),

            // --- debatable / useful to some: NOT pre-selected -----------------------------
            Appx("XboxGameOverlay", "Xbox Game Bar Overlay", "Xbox overlay (part of Game Bar)", "Microsoft.XboxGamingOverlay", false),
            Appx("XboxApp", "Xbox", "Xbox app + Game Pass", "Microsoft.GamingApp", false),
            Appx("TeamsConsumer", "Microsoft Teams (personal / Chat)", "Consumer Teams chat", "MicrosoftTeams", false),
            Appx("Todos", "Microsoft To Do", "Task list app", "Microsoft.Todos", false),
            Appx("PowerAutomate", "Power Automate", "Desktop automation", "Microsoft.PowerAutomateDesktop", false),
            Appx("Maps", "Windows Maps", "Offline maps app", "Microsoft.WindowsMaps", false),
            Appx("PhoneLink", "Phone Link", "Link to Android/iPhone", "Microsoft.YourPhone", false),
            Appx("Journal", "Microsoft Journal", "Pen note-taking app", "Microsoft.MicrosoftJournal", false),
            Appx("FamilySafety", "Family Safety", "Microsoft Family app", "MicrosoftCorporationII.MicrosoftFamily", false),
            Appx("QuickAssist", "Quick Assist", "Remote-help tool (support-scam vector)", "MicrosoftCorporationII.QuickAssist", false),
        };

        // ---- OEM / AV bloat: detected on THIS machine ----------------------------------------
        // (needle, friendly note, pre-select). Match is a case-insensitive DisplayName substring.
        private static readonly (string needle, string? note, bool preselect)[] OemPatterns =
        {
            ("McAfee",              "For a fully clean removal, run McAfee's official MCPR removal tool afterward.", true),
            ("Norton",              "For a fully clean removal, run Norton's official Remove and Reinstall (NRnR) tool afterward.", true),
            ("WildTangent",         null, true),
            ("Wild Tangent",        null, true),
            ("HP JumpStart",        null, true),
            ("HP Documentation",    null, true),
            ("HP Connection Optimizer", null, true),
            ("myHP",                null, true),
            ("Dell Customer Connect", null, true),
            ("Dell Digital Delivery", null, true),
            ("Lenovo Now",          null, true),
            ("Booking.com",         null, true),
            ("Amazon.com",          null, true),
            // Vendor management/telemetry tools: useful to some (deliver driver updates) -> NOT pre-selected
            ("Dell SupportAssist",  "Dell diagnostics/telemetry. Also delivers some driver updates.", false),
            ("Dell Update",         "Delivers Dell driver/firmware updates.", false),
            ("Dell Optimizer",      "Dell performance/telemetry service.", false),
            ("Lenovo Vantage",      "Delivers Lenovo driver updates and settings.", false),
            ("Lenovo Smart",        null, false),
            ("HP Support Assistant","HP diagnostics and driver tool.", false),
            ("ExpressVPN",          null, false),
        };

        private static List<HardeningSetting> DetectOemBloat()
        {
            var found = new List<HardeningSetting>();
            var seenKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            var seenIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var (display, uninstallCmd, keyPath) in EnumerateInstalledPrograms())
            {
                foreach (var (needle, note, preselect) in OemPatterns)
                {
                    if (display.IndexOf(needle, StringComparison.OrdinalIgnoreCase) < 0) continue;
                    if (!seenKeys.Add(keyPath)) break;   // one setting per install
                    var id = "BLOAT_OEM_" + Regex.Replace(display, "[^A-Za-z0-9]", "");
                    // De-dupe by resulting Id too: the same product registered under both the
                    // native and WOW6432Node Uninstall hives would otherwise produce two settings
                    // sharing one Id (breaks Id-keyed config matching, shows a duplicate row).
                    if (!seenIds.Add(id)) break;
                    var impact = "Uninstalls this program. Not auto-reversible — reinstall from the vendor if needed."
                                 + (note != null ? " " + note : string.Empty);
                    found.Add(new HardeningSetting
                    {
                        Id = id,
                        Name = "Remove: " + display,
                        Description = "OEM/pre-installed program detected on this PC",
                        Category = SettingCategory.Bloatware,
                        Type = SettingType.AppRemoval,
                        Risk = RiskLevel.Medium,
                        IsEnabled = false,   // never pre-checked at rest (see Appx() note)
                        ApplyCommand = uninstallCmd,
                        VerifyRegistryPath = keyPath,   // Uninstall key: absent after removal
                        ImpactWarning = impact,
                        Tags = preselect
                            ? new[] { "bloatware", "oem", "cleanup", "recommended" }
                            : new[] { "bloatware", "oem", "cleanup" }
                    });
                    break;
                }
            }
            return found;
        }

        private static IEnumerable<(string display, string uninstall, string keyPath)> EnumerateInstalledPrograms()
        {
            // HKLM only — never HKCU. The uninstall command string is later executed ELEVATED
            // (the app runs as administrator). HKCU is writable by any unprivileged process, so
            // enumerating it would let a non-admin plant a fake "McAfee"/"Norton" entry whose
            // UninstallString is an arbitrary command, then have AtlantHarden run it as admin —
            // a local privilege-escalation vector. HKLM keys require admin to write, so a value
            // there is already trusted at the same level the command would run.
            var hives = new (RegistryKey root, string sub, string display)[]
            {
                (Registry.LocalMachine, @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall", @"HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall"),
                (Registry.LocalMachine, @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall", @"HKLM\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall"),
            };
            foreach (var (root, sub, dispRoot) in hives)
            {
                RegistryKey? key = null;
                try { key = root.OpenSubKey(sub); } catch { }
                if (key == null) continue;
                using (key)
                {
                    foreach (var name in key.GetSubKeyNames())
                    {
                        string? display = null, cmd = null;
                        try
                        {
                            using var app = key.OpenSubKey(name);
                            if (app == null) continue;
                            if ((app.GetValue("SystemComponent") as int?) == 1) continue;
                            if (app.GetValue("WindowsInstaller") is int wi && wi == 1 && app.GetValue("ParentKeyName") != null) continue;
                            display = app.GetValue("DisplayName") as string;
                            if (string.IsNullOrWhiteSpace(display)) continue;
                            cmd = BuildSilentUninstall(app.GetValue("QuietUninstallString") as string,
                                                       app.GetValue("UninstallString") as string, name);
                        }
                        catch { continue; }
                        if (string.IsNullOrWhiteSpace(cmd)) continue;
                        yield return (display!, cmd!, dispRoot + "\\" + name);
                    }
                }
            }
        }

        // Prefer the vendor's quiet uninstall; convert MSI uninstalls to a silent form.
        private static string? BuildSilentUninstall(string? quiet, string? uninstall, string keyName)
        {
            if (!string.IsNullOrWhiteSpace(quiet)) return quiet;
            if (string.IsNullOrWhiteSpace(uninstall)) return null;
            var u = uninstall!;
            if (u.IndexOf("msiexec", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                var m = Regex.Match(u, @"\{[0-9A-Fa-f\-]{36}\}");
                var code = m.Success ? m.Value : (keyName.StartsWith("{") ? keyName : null);
                if (code != null) return $"MsiExec.exe /X{code} /qn /norestart";
            }
            return u; // non-MSI without a quiet string: best-effort (may prompt)
        }
    }
}
