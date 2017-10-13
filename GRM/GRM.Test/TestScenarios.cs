using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GRM.Test
{
    [TestClass]
    public class TestScenarios
    {

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
        public void Search_YouTube_12272012_Count()
        {
            var partner = "YouTube";
            var effectiveDate = "12-27-2012";

            var result = GRM.Main.Program.MusicContracts(partner, effectiveDate);
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void Search_YouTube_12272012_Usage()
        {
            var partner = "YouTube";
            var effectiveDate = "12-27-2012";

            var result = GRM.Main.Program.MusicContracts(partner, effectiveDate);
            Assert.AreEqual(true, result.TrueForAll(x => x.Usages == "streaming"));
        }

        [TestMethod]
        public void Search_YouTube_04012012_Count()
        {
            var partner = "YouTube";
            var effectiveDate = "04-01-2012";

            var result = GRM.Main.Program.MusicContracts(partner, effectiveDate);
            Assert.AreEqual(2, result.Count);
        }
        [TestMethod]
        public void Search_YouTube_04012012_Usage()
        {
            var partner = "YouTube";
            var effectiveDate = "04-01-2012";

            var result = GRM.Main.Program.MusicContracts(partner, effectiveDate);
            Assert.AreEqual(true, result.TrueForAll(x => x.Usages == "streaming"));
        }
    }
}
