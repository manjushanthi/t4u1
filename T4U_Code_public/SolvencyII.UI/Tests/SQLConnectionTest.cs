using SolvencyII.Data.SQL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for SQLConnectionTest and is intended
    ///to contain all SQLConnectionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SQLConnectionTest
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


        ///// <summary>
        /////A test for Query
        /////</summary>
        //[TestMethod()]
        //public void QueryTest()
        //{
        //    string databaseConnection = string.Empty; 
        //    SQLConnection target = new SQLConnection(databaseConnection); 
           
        //    Type tableType = null; // TODO: Initialize to an appropriate value
        //    string query = string.Empty; // TODO: Initialize to an appropriate value
        //    object[] args = null; // TODO: Initialize to an appropriate value
        //    List<object> expected = null; // TODO: Initialize to an appropriate value
        //    List<object> actual;
        //    actual = target.Query(tableType, query, args);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}
