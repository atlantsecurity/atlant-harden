using System.Text.Json;
using AtlantHarden.Services;
using Microsoft.Win32;
using Xunit;

namespace AtlantHarden.Tests
{
    public class BackupTests
    {
        private static JsonElement RoundTrip(object value) =>
            JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(value));

        [Fact]
        public void MultiString_RoundTrips()
        {
            var back = BackupService.ConvertJsonElement(RoundTrip(new[] { "NistP384", "NistP256" }), RegistryValueKind.MultiString);
            Assert.Equal(new[] { "NistP384", "NistP256" }, Assert.IsType<string[]>(back));
        }

        [Fact]
        public void Binary_RoundTrips_FromBase64()
        {
            var bytes = new byte[] { 1, 2, 0xFF };
            var back = BackupService.ConvertJsonElement(RoundTrip(bytes), RegistryValueKind.Binary);
            Assert.Equal(bytes, Assert.IsType<byte[]>(back));
        }

        [Fact]
        public void Dword_AllOnes_RoundTrips()
        {
            var back = BackupService.ConvertJsonElement(RoundTrip(-1), RegistryValueKind.DWord);
            Assert.Equal(-1, back);
        }

        [Fact]
        public void RegExport_FormatsDwordAllOnes_WithoutOverflow()
        {
            var svc = new BackupService();
            Assert.Equal("dword:ffffffff", svc.FormatRegValue(-1, "REG_DWORD"));
            Assert.Equal("dword:ffffffff", svc.FormatRegValue(0xFFFFFFFFu, "REG_DWORD"));
        }

        [Fact]
        public void RegExport_FormatsMultiString_AsHex7()
        {
            var svc = new BackupService();
            var s = svc.FormatRegValue(new[] { "a" }, "REG_MULTI_SZ");
            Assert.Equal("hex(7):61,00,00,00,00,00", s);
        }
    }
}
