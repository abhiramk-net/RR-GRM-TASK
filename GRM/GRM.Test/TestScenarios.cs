using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GRM.Test
{
    [TestClass]
    public class TestScenarios
    {
        [TestMethod]
        public void Search_ITunes_03012012()
        {
            var partner = "ITunes";
            var effectiveDate = "03-01-2012";

            var result = GRM.Main.Program.PrintMusicContracts(partner, effectiveDate);
        }

        [TestMethod]
        public void Search_ITunes_03012012_Count()
        {
            var partner = "ITunes";
            var effectiveDate = "03-01-2012";

            var result = GRM.Main.Program.MusicContracts(partner, effectiveDate);

            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void Search_ITunes_03012012_Usage()
        {
            var partner = "ITunes";
            var effectiveDate = "03-01-2012";

            var result = GRM.Main.Program.MusicContracts(partner, effectiveDate);

            Assert.AreEqual(true, result.TrueForAll(x => x.Usages == "digital download"));
        }

        [TestMethod]
        public void Search_YouTube_12272012()
        {
            var partner = "YouTube";
            var effectiveDate = "12-27-2012";

            var result = GRM.Main.Program.PrintMusicContracts(partner, effectiveDate);
        }

        [TestMethod]
        public void Search_YouTube_04012012()
        {
            var partner = "YouTube";
            var effectiveDate = "04-01-2012";

            var result = GRM.Main.Program.PrintMusicContracts(partner, effectiveDate);
        }
    }
}
