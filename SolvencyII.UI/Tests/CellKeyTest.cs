using SolvencyII.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for CellKeyTest and is intended
    ///to contain all CellKeyTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CellKeyTest
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
        ///A test for RowNumber
        ///</summary>
        [TestMethod()]
        public void RowNumberTest()
        {
            string cellName = "x1_2_3";
            CellKey actual = new CellKey(cellName); 
            int expectedCol = 1;
            int expectedRow = 2;
            long expectedPrimaryKey = 3;
            Assert.AreEqual(expectedCol, actual.ColumnNumber);
            Assert.AreEqual(expectedRow, actual.RowNumber);
            Assert.AreEqual(expectedPrimaryKey, actual.PrimaryKey);
        }

        

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            CellKey target = new CellKey(8,7,6);
            string expected = "x8_7_6";
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest2()
        {
            string expected = "x8_7_6";
            CellKey target = new CellKey(expected);
            string actual;
            actual = target.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void StaticToStringTest2()
        {
            string expected = "x8_7_6";
            string actual = CellKey.CellKeyToText(8,7,6);
            Assert.AreEqual(expected, actual);
        }

    }
}
