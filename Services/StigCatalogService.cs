using AtlantHarden.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;

namespace AtlantHarden.Services
{
    /// <summary>
    /// Loads the embedded DISA STIG catalog (Resources/stig-catalog.json) and projects it into
    /// <see cref="HardeningSetting"/> objects. The catalog is generated from authoritative sources
    /// (Microsoft PowerSTIG + cyber.trackr.live) by tools/Generate-StigCatalog.ps1.
    /// Data lives in JSON - never hand-edit settings here; regenerate the catalog instead.
    /// </summary>
    public static class StigCatalogService
    {
        private const string ResourceSuffix = "stig-catalog.json";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        // Loaded exactly once, thread-safely, on first access.
        private static readonly Lazy<Loaded> _data =
            new(Load, LazyThreadSafetyMode.ExecutionAndPublication);

        private static readonly List<string> _loadErrors = new();

        /// <summary>Non-fatal catalog load problems (missing resource, malformed JSON), if any.</summary>
        public static IReadOnlyList<string> LoadErrors
        {
            get { _ = _data.Value; return _loadErrors; }
        }

        /// <summary>Per-product STIG summaries (name, version, rule count) for the dashboard.</summary>
        public static StigCatalogMetadata GetMetadata() => _data.Value.Metadata;

        /// <summary>All STIG hardening settings defined by the catalog.</summary>
        public static IReadOnlyList<HardeningSetting> GetStigSettings() => _data.Value.Settings;

        private sealed record Loaded(List<HardeningSetting> Settings, StigCatalogMetadata Metadata);

        private static Loaded Load()
        {
            var root = LoadCatalog();
            var metadata = new StigCatalogMetadata(
                root?.Generated ?? string.Empty,
                root?.Source ?? string.Empty,
                root?.Stigs?.Select(s => new StigProductSummary(
                    s.Product ?? string.Empty,
                    s.Name ?? string.Empty,
                    s.StigVersion ?? string.Empty,
                    s.Rules)).ToList() ?? new List<StigProductSummary>());

            var list = new List<HardeningSetting>();
            if (root?.Settings != null)
            {
                foreach (var e in root.Settings)
                {
                    var mapped = MapEntry(e);
                    if (mapped != null) list.Add(mapped);
                }
            }
            return new Loaded(list, metadata);
        }

        private static CatalogRoot? LoadCatalog()
        {
            var asm = Assembly.GetExecutingAssembly();
            var resourceName = asm.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith(ResourceSuffix, StringComparison.OrdinalIgnoreCase));
            if (resourceName == null)
            {
                RecordError($"Embedded STIG catalog ('{ResourceSuffix}') not found in assembly.");
                return null;
            }

            using var stream = asm.GetManifestResourceStream(resourceName);
            if (stream == null)
            {
                RecordError($"Embedded STIG catalog stream '{resourceName}' could not be opened.");
                return null;
            }

            try
            {
                return JsonSerializer.Deserialize<CatalogRoot>(stream, JsonOptions);
            }
            catch (Exception ex)
            {
                // Never crash startup on a bad catalog, but make the failure visible.
                RecordError($"STIG catalog failed to parse: {ex.Message}");
                return null;
            }
        }

        private static void RecordError(string message)
        {
            _loadErrors.Add(message);
            Debug.WriteLine($"[StigCatalogService] {message}");
        }

        private static HardeningSetting? MapEntry(CatalogEntry e)
        {
            if (string.IsNullOrWhiteSpace(e.RegistryPath) || string.IsNullOrWhiteSpace(e.RegistryKey))
                return null;

            // STIG settings live in their own per-product categories (grouped separately from
            // the curated baseline categories), carrying DISA's exact mandated value.
            var category = e.Product switch
            {
                "Windows11" => SettingCategory.StigWindows11,
                "Edge" => SettingCategory.StigEdge,
                "Chrome" => SettingCategory.StigChrome,
                "Firefox" => SettingCategory.StigFirefox,
                "Office365" => SettingCategory.StigOffice365,
                _ => SettingCategory.StigWindows11
            };

            var risk = e.Severity?.ToLowerInvariant() switch
            {
                "high" => RiskLevel.High,
                "low" => RiskLevel.Low,
                _ => RiskLevel.Medium
            };

            var recommended = ConvertValue(e.ValueType, e.Value);

            var detailed = BuildDetailedInfo(e);
            var refs = string.IsNullOrWhiteSpace(e.ReferenceUrl)
                ? new[] { "https://public.cyber.mil/stigs/" }
                : new[] { e.ReferenceUrl!, "https://public.cyber.mil/stigs/" };

            var tags = new List<string> { "stig", e.Product?.ToLowerInvariant() ?? "stig" };
            if (!string.IsNullOrWhiteSpace(e.StigId)) tags.Add(e.StigId!.ToLowerInvariant());

            return new HardeningSetting
            {
                Id = e.Id ?? $"STIG_{e.StigId}",
                Name = e.Name ?? e.StigId ?? e.Id ?? "STIG Setting",
                Description = e.Description ?? string.Empty,
                DetailedInfo = detailed,
                Category = category,
                Type = SettingType.Registry,
                Risk = risk,
                RegistryPath = e.RegistryPath!,
                RegistryKey = e.RegistryKey!,
                RegistryValueType = e.ValueType ?? "REG_DWORD",
                RecommendedValue = recommended,
                DefaultValue = null,            // STIG revert = remove the policy value (not-configured)
                IsStig = true,
                StigId = e.StigId,
                VulnId = e.VulnId,
                StigProduct = e.Product,
                Ccis = e.Ccis,
                RequiresReboot = e.RequiresReboot,
                ImpactWarning = e.ImpactWarning,
                ReferenceUrls = refs,
                Tags = tags.ToArray()
            };
        }

        private static object? ConvertValue(string? valueType, JsonElement value)
        {
            switch (valueType)
            {
                case "REG_DWORD":
                    // STIG DWORD values are unsigned; cast preserves the 32-bit pattern the
                    // registry stores (e.g. 0xFFFFFFFF -> -1), matching RegEdit behavior.
                    if (value.ValueKind == JsonValueKind.Number && value.TryGetInt64(out var d))
                        return unchecked((int)d);
                    return uint.TryParse(value.ToString(), out var dp) ? unchecked((int)dp) : 0;

                case "REG_QWORD":
                    if (value.ValueKind == JsonValueKind.Number && value.TryGetInt64(out var q))
                        return q;
                    return long.TryParse(value.ToString(), out var qp) ? qp : 0L;

                case "REG_MULTI_SZ":
                    if (value.ValueKind == JsonValueKind.Array)
                        return value.EnumerateArray().Select(x => x.GetString() ?? string.Empty).ToArray();
                    return new[] { value.ToString() };

                default: // REG_SZ, REG_EXPAND_SZ, REG_BINARY (string form)
                    return value.ValueKind == JsonValueKind.String ? value.GetString() : value.ToString();
            }
        }

        private static string BuildDetailedInfo(CatalogEntry e)
        {
            var valueText = e.Value.ValueKind == JsonValueKind.Array
                ? string.Join(";", e.Value.EnumerateArray().Select(x => x.GetString() ?? string.Empty))
                : e.Value.ToString();

            var parts = new List<string>
            {
                $"DISA STIG {e.StigId} (Vuln {e.VulnId}) — {e.Product}",
                $"Sets {e.RegistryPath}\\{e.RegistryKey} = {valueText} [{e.ValueType}]"
            };
            if (e.Ccis is { Length: > 0 }) parts.Add("CCIs: " + string.Join(", ", e.Ccis));
            if (!string.IsNullOrWhiteSpace(e.Description)) parts.Add(e.Description!);
            return string.Join(Environment.NewLine, parts);
        }

        // ---- JSON DTOs ----
        private sealed class CatalogRoot
        {
            public string? Schema { get; set; }
            public string? Generated { get; set; }
            public string? Source { get; set; }
            public List<StigSummaryDto>? Stigs { get; set; }
            public int TotalRules { get; set; }
            public List<CatalogEntry>? Settings { get; set; }
        }

        private sealed class StigSummaryDto
        {
            public string? Product { get; set; }
            public string? Name { get; set; }
            public string? Version { get; set; }
            public string? StigVersion { get; set; }
            public int Rules { get; set; }
        }

        private sealed class CatalogEntry
        {
            public string? Id { get; set; }
            public string? StigId { get; set; }
            public string? VulnId { get; set; }
            public string? Product { get; set; }
            public string? Category { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public string? Severity { get; set; }
            public string? RegistryPath { get; set; }
            public string? RegistryKey { get; set; }
            public string? ValueType { get; set; }
            public JsonElement Value { get; set; }
            public bool RequiresReboot { get; set; }
            public string? ImpactWarning { get; set; }
            public string[]? Ccis { get; set; }
            public string? ReferenceUrl { get; set; }
        }
    }

    /// <summary>Catalog-level metadata exposed to the UI.</summary>
    public sealed record StigCatalogMetadata(
        string Generated,
        string Source,
        IReadOnlyList<StigProductSummary> Products);

    /// <summary>Per-product STIG summary (e.g. Windows 11 · V2R7 · 114 rules).</summary>
    public sealed record StigProductSummary(
        string Product,
        string Name,
        string StigVersion,
        int Rules);
}
