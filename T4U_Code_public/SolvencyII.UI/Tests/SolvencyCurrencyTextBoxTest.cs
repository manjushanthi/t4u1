using System.Globalization;
using SolvencyII.Domain.ENumerators;
using SolvencyII.UI.Shared.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for SolvencyCurrencyTextBoxTest and is intended
    ///to contain all SolvencyCurrencyTextBoxTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SolvencyCurrencyTextBoxTest
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
        ///A test for IsValid
        ///</summary>
        [TestMethod()]
        public void IsValidTest()
        {
            SolvencyCurrencyTextBox target = new SolvencyCurrencyTextBox(); 
            target.ColumnType = SolvencyDataType.Integer;
            RunNestedTests(target, true);
            target.ColumnType = SolvencyDataType.Monetry;
            RunNestedTests(target);
            target.ColumnType = SolvencyDataType.Percentage;
            RunNestedTests(target);
        }

        private void RunNestedTests(SolvencyCurrencyTextBox target, bool intType = false)
        {
            target.Text = "";
            Assert.IsTrue(target.IsValid());
            target.Text = "1e30";
            Assert.IsFalse(target.IsValid());
            target.Text = "1e9";
            Assert.IsTrue(target.IsValid());
            target.Text = "0";
            Assert.IsTrue(target.IsValid());
            target.Text = "-1e9";
            Assert.IsTrue(target.IsValid());
            target.Text = "-1e30";
            Assert.IsFalse(target.IsValid());
            target.Text = "alpaha";
            Assert.IsFalse(target.IsValid());

            if (!intType)
            {
                target.Text = string.Format("1{0}000{0}000{1}01", CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator, CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                Assert.IsTrue(target.IsValid());
                target.Text = string.Format("1{1}000{1}000{0}01", CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator, CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                Assert.IsFalse(target.IsValid());
            }
            else
            {
                target.Text = string.Format("1{0}000{0}000", CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator);
                Assert.IsTrue(target.IsValid());
                target.Text = string.Format("1{0}000{0}000", CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
                Assert.IsFalse(target.IsValid());
                
            }


        }
    }
}
