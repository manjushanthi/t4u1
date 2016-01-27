using System.Configuration;
using System.IO;
using SolvencyII.Data.Shared.Dictionaries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using System.Collections.Generic;
using SolvencyII.UI.Shared.Configuration;
using SolvencyII.UI.Shared.Databases;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for LanguageLabelsTest and is intended
    ///to contain all LanguageLabelsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class LanguageLabelsTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            
        }
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
        ///A test for DataSource
        ///</summary>
        [TestMethod()]
        public void DataSourceTest()
        {
            string deploymentDirectory = TestContext.DeploymentDirectory;

            StaticSettings.ConnectionString = deploymentDirectory + @"\TestCase1.xbrt";
            StaticSettings.SolvencyIITemplateDBConnectionString = deploymentDirectory + @"\TemplateDatabase.db";

            StaticSettings.ApplicationID = 1;
            StaticSettings.FormLanguage = eLanguageID.InEnglish;
            
            
            Assert.AreEqual("Multi report container", LanguageLabels.GetLabel(1));
            Assert.AreEqual("Create a new multi report container", LanguageLabels.GetLabel(2));
        }
    }
}
