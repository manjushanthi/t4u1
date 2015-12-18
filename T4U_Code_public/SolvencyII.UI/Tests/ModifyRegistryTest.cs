using SolvencyII.UI.Shared.Registry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for ModifyRegistryTest and is intended
    ///to contain all ModifyRegistryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ModifyRegistryTest
    {
        private const string TestKey = "TestLastDatabasePath";

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
        ///A test for ModifyRegistry Constructor
        ///</summary>


        /// <summary>
        ///A test for Read
        ///</summary>
        [TestMethod()]
        public void ReadTest()
        {
            ModifyRegistry target = new ModifyRegistry();
            string KeyName = TestKey;
            string actual;
            actual = target.Read(KeyName);
            Assert.IsTrue(actual == null || actual == "123!321");
        }

        /// <summary>
        ///A test for Write
        ///</summary>
        [TestMethod()]
        public void WriteTest()
        {
            ModifyRegistry target = new ModifyRegistry(); 
            string KeyName = TestKey;
            object Value = "123!321";
            bool expected = true;
            bool actual;
            actual = target.Write(KeyName, Value);
            Assert.AreEqual(expected, actual);
        }
    }
}
