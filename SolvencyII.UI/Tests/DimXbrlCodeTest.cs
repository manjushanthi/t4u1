using SolvencyII.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for DimXbrlCodeTest and is intended
    ///to contain all DimXbrlCodeTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DimXbrlCodeTest
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


        /// <summary>
        ///A test for DimXbrlCode Constructor
        ///</summary>
        [TestMethod()]
        public void ToStringTestTrue()
        {
            string dimXbrlCode = "PAGEpre_code";
            bool includesPAGE = true;
            DimXbrlCode target = new DimXbrlCode(dimXbrlCode, includesPAGE);
            Assert.AreEqual("pre_code", target.ToString());
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTestFalse()
        {
            string dimXbrlCode = "pre_code";
            bool includesPAGE = false;
            DimXbrlCode target = new DimXbrlCode(dimXbrlCode, includesPAGE);
            Assert.AreEqual("pre_code", target.ToString());
        }

        /// <summary>
        ///A test for DimCode
        ///</summary>
        [TestMethod()]
        public void DimCodeTest()
        {
            string dimXbrlCode = "PAGEpre_code";
            bool includesPAGE = true;
            DimXbrlCode target = new DimXbrlCode(dimXbrlCode, includesPAGE);
            string expected = "code";
            string actual = target.DimCode;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Prefix
        ///</summary>
        [TestMethod()]
        public void PrefixTest()
        {
            string dimXbrlCode = "PAGEpre_code";
            bool includesPAGE = true;
            DimXbrlCode target = new DimXbrlCode(dimXbrlCode, includesPAGE);
            string expected = "pre";
            string actual = target.Prefix;
            Assert.AreEqual(expected, actual);

        }
    }
}
