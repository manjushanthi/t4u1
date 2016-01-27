using System.Collections.Generic;
using System.Configuration;
using SolvencyII.Data.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.Entities;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for TreeBranchBuilderTest and is intended
    ///to contain all TreeBranchBuilderTest Unit Tests
    ///</summary>
    [TestClass]
    public class TreeBranchBuilderTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for BuildTree
        ///</summary>
        [TestMethod]
        public void BuildTree_DPM_Test()
        {
            //StaticSettings.ConnectionString = @"V:\EIOPA\Data\DPM_release_ver7_clean.xbrt";
            // StaticSettings.ConnectionString = @"V:\EIOPA\Data\20140212_DPM_EBA.xbrt";

            StaticSettings.ConnectionString = TestContext.DeploymentDirectory + @"\TestCase1.xbrt";

            const int instanceID = 1;
            GetSQLData getData = new GetSQLData(StaticSettings.ConnectionString);
            TreeBranch actual = getData.GetTree(instanceID);
            getData.Dispose();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.SubBranches.Count > 0);
            /*
            Assert.IsTrue(actual.SubBranches[0].DisplayText == "MD, Annual Reporting, Solo");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].DisplayText == "Balance Sheet, Solo (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].SubBranches[0].DisplayText == "Balance Sheet, Solo (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].SubBranches[0].GroupTableIDs == "1367");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].SubBranches[0].IsTyped == false);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].SubBranches[0].HasLocationRange);

            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].DisplayText == "D1 - Investments Data - Portfolio list (detailed list of  investments), Solo (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches.Count == 2);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[0].DisplayText == "D1 - Investments Data - Portfolio list (detailed list of  investments), Solo, 01 (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[0].GroupTableIDs == "3001");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[0].IsTyped);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[0].HasLocationRange);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[1].DisplayText == "D1 - Investments Data - Portfolio list (detailed list of  investments), Solo, 02 (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[1].GroupTableIDs == "3002");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[1].IsTyped);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[1].HasLocationRange);

            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].DisplayText == "E3: Non-life Insurance Claims Information, Solo (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches.Count == 3);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[0].DisplayText == "E3: Non-life Insurance Claims Information, Solo, Gross Claims Paid (non-cumulative) (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[0].IsTyped == false);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[0].GroupTableIDs == "10001|10401");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[1].DisplayText == "E3: Non-life Insurance Claims Information [Part 2 - Reinsurance Recoveries received (non-cumulative)] (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[1].IsTyped == false);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[1].GroupTableIDs == "11001|11401");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[2].DisplayText == "E3: Non-life Insurance Claims Information [Part 3 - Net Claims Paid (non-cumulative)] (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[2].IsTyped == false);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[2].GroupTableIDs == "12001|12401");
            */


        }

        [TestMethod]
        public void BuildTree_For_Preparatory_Reporting_Solo_Annual()
        {
            //StaticSettings.ConnectionString = @"V:\EIOPA\Data\DPM_release_ver7_clean.xbrt";
            // StaticSettings.ConnectionString = @"V:\EIOPA\Data\20140212_DPM_EBA.xbrt";

            StaticSettings.ConnectionString = TestContext.DeploymentDirectory + @"\Preparatory\Preparatory_Reporting_Solo_Annual.xbrt";

            const int instanceID = 1;
            GetSQLData getData = new GetSQLData(StaticSettings.ConnectionString);
            TreeBranch actual = getData.GetTree(instanceID);
            getData.Dispose();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.SubBranches.Count > 0);

            Assert.IsTrue(actual.SubBranches[0].DisplayText == "Preparatory reporting Solo Annual");

            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].DisplayText == "S.01.01.01 Content of the submission");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].SubBranches[0].DisplayText == "Content of the submission");
            /*
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].SubBranches[0].GroupTableIDs == "1367");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].SubBranches[0].IsTyped == false);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[0].SubBranches[0].HasLocationRange);

            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].DisplayText == "D1 - Investments Data - Portfolio list (detailed list of  investments), Solo (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches.Count == 2);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[0].DisplayText == "D1 - Investments Data - Portfolio list (detailed list of  investments), Solo, 01 (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[0].GroupTableIDs == "3001");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[0].IsTyped);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[0].HasLocationRange);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[1].DisplayText == "D1 - Investments Data - Portfolio list (detailed list of  investments), Solo, 02 (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[1].GroupTableIDs == "3002");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[1].IsTyped);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[1].SubBranches[1].HasLocationRange);

            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].DisplayText == "E3: Non-life Insurance Claims Information, Solo (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches.Count == 3);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[0].DisplayText == "E3: Non-life Insurance Claims Information, Solo, Gross Claims Paid (non-cumulative) (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[0].IsTyped == false);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[0].GroupTableIDs == "10001|10401");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[1].DisplayText == "E3: Non-life Insurance Claims Information [Part 2 - Reinsurance Recoveries received (non-cumulative)] (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[1].IsTyped == false);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[1].GroupTableIDs == "11001|11401");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[2].DisplayText == "E3: Non-life Insurance Claims Information [Part 3 - Net Claims Paid (non-cumulative)] (MD)");
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[2].IsTyped == false);
            Assert.IsTrue(actual.SubBranches[0].SubBranches[2].SubBranches[2].GroupTableIDs == "12001|12401");

            */

        }

   
        [TestMethod]
        public void BuildTreeBranchesTestDpm()
        {
            StaticSettings.ConnectionString = TestContext.DeploymentDirectory + @"\TestCase1.xbrt";

            const int instanceID = 1;
            GetSQLData getData = new GetSQLData(StaticSettings.ConnectionString);
            List<TreeItem> actual = getData.GetTreeBranches(instanceID);
            getData.Dispose();
            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.Count > 0);
        }

    }
}
