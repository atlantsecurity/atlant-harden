using System.Linq;
using AtlantHarden.Services;
using Xunit;

namespace AtlantHarden.Tests
{
    public class CatalogTests
    {
        [Fact]
        public void StigCatalog_Loads_WithoutErrors()
        {
            Assert.Empty(StigCatalogService.LoadErrors);
            Assert.NotEmpty(StigCatalogService.GetStigSettings());
        }

        [Fact]
        public void AllSettings_HaveUniqueIds()
        {
            var ids = new HardeningService().GetAllSettings().Select(s => s.Id).ToList();
            var dups = ids.GroupBy(i => i).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
            Assert.Empty(dups);
        }

        [Fact]
        public void WindowsUpdate_IsInRecommended_ExceptMaximumOnlyOptions()
        {
            var wu = WindowsUpdateSettings.GetAllSettings();
            Assert.NotEmpty(wu);
            var notRecommended = wu.Where(s => !RecommendedProfile.IsRecommended(s)).Select(s => s.Id).OrderBy(i => i).ToArray();
            Assert.Equal(new[] { "WU_DisablePause", "WU_DownloadOverMetered", "WU_LatestUpdatesAsap" }, notRecommended);
        }

        [Fact]
        public void RegistryMultiString_SplitsSeparatedString()
        {
            Assert.Equal(new[] { "a", "b" }, RegistryService.ToMultiString("a\0b"));
            Assert.Equal(new[] { "a", "b" }, RegistryService.ToMultiString(new[] { "a", "b" }));
        }
    }
}
