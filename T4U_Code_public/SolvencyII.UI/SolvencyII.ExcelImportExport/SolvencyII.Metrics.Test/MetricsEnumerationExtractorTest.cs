using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SolvencyII.Metrics.Test
{
    [TestClass]
    public class MetricsEnumerationExtractorTest
    {
        MetricsEnumerationExtractor extractor;
        AT2DPM.DAL.DPMdbConnection conn;
        AT2DPM.DAL.Model.DPMdb dpmdb;

        [TestInitialize]
        public void before()
        {
            conn = new AT2DPM.DAL.DPMdbConnection();
            dpmdb = conn.OpenDpmConnection(TestHelper.testDbLocation);
            extractor = new MetricsEnumerationExtractor(dpmdb);
        }
        
        [TestMethod]
        public void IsEnumerationTest()
        {
            bool result = extractor.IsMetricEnumeration("ei8");
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsEnumerationTestFalse()
        {
            bool result = extractor.IsMetricEnumeration("gi8");
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void GetEntireHeirarchyAM_6()
        {
            var result = extractor.getComboBoxItemsForMetric("ei8");
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void GetEntireHeirarchySE_11()
        {
            var result = extractor.getComboBoxItemsForMetric("ei12");
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void GetEntireHeirarchyMC_3FromStarting()
        {
            var result = extractor.getComboBoxItemsForMetric("ei71");
            Assert.AreEqual(7, result.Count);
        }

        [TestMethod]
        public void GetEntireHeirarchyGA_13()
        {
            List<MetricComboBoxItem> result = extractor.getComboBoxItemsForMetric("ei19");
            MetricComboBoxItem firrst = result.Single(x => x.isAbstract);
            Assert.AreEqual(firrst.member.MemberCode, "x0");

            foreach (var child in firrst.children)
                Assert.IsTrue(child.parent.Equals(firrst));

            Assert.AreEqual(1, result.Count);
        }

        [TestCleanup]
        public void after()
        {
            conn.CloseActiveDatabase();
            dpmdb = null;
            conn = null;
            extractor = null;
        }
    }
}
