using SolvencyII.Domain.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for ObjectExtTest and is intended
    ///to contain all ObjectExtTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObjectExtTest
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
        ///A test for DecimalToString
        ///</summary>
        [TestMethod()]
        public void DecimalToStringTest1()
        {
            object value = 12345.1234;
            CultureInfo culture = CultureInfo.CurrentCulture;
            int decimalPlaces = 4;
            string expected = "12,345.1234";
            string actual;
            actual = ObjectExt.DecimalToString(value, culture, decimalPlaces);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DecimalToStringTest2()
        {
            object value = 12345.123456;
            CultureInfo culture = CultureInfo.CurrentCulture;
            int decimalPlaces = 4;
            string expected = "12,345.1235~";
            string actual;
            actual = ObjectExt.DecimalToString(value, culture, decimalPlaces);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DecimalToStringTest3()
        {
            object value = "0";
            CultureInfo culture = CultureInfo.CurrentCulture;
            int decimalPlaces = 2;
            string expected = "0.00";
            string actual;
            actual = ObjectExt.DecimalToString(value, culture, decimalPlaces);
            Assert.AreEqual(expected, actual);
        }


        [TestMethod()]
        public void DecimalToStringTest4()
        {
            object value = "123";
            CultureInfo culture = CultureInfo.CurrentCulture;
            int decimalPlaces = 2;
            string expected = "123.00";
            string actual;
            actual = ObjectExt.DecimalToString(value, culture, decimalPlaces);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DecimalToStringTest5()
        {
            object value = 0.123456;
            CultureInfo culture = CultureInfo.CurrentCulture;
            int decimalPlaces = 4;
            string expected = "0.1235~";
            string actual;
            actual = ObjectExt.DecimalToString(value, culture, decimalPlaces);
            Assert.AreEqual(expected, actual);
        }

    }
}
