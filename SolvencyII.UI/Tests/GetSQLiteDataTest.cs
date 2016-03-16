using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

    #region Top Stuff

using System;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;

namespace Tests
{
    [TestClass()]
    public class GetSQLiteDataTest
    {
        #endregion

        private static string _testDB;
        private static GetSQLData _getData;
        private static long _instanceID;
        private static int _tableID;
        private static long _moduleID;
        private static long _ordinateID;
        private static long _ordinateMDID;
        private static long _ordinateHDID;
        private static int _hierarchyID;
        private static int _axisID;
        public static int _axisMemberID; 

        private enum Test_DB
        {
            DPM,
            EBA,
            CRT
        }
        private static Test_DB _testingDbType = Test_DB.DPM;


        private TestContext testContextInstance;
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
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
            string deploymentDirectory = testContext.DeploymentDirectory;

            // Take a copy of the neweset database and use it for tests.
            // Get assemble path
            string folder = System.Environment.CurrentDirectory;
            //string folder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string sourceFile;
            switch (_testingDbType)
            {
                case Test_DB.DPM:
                    sourceFile = deploymentDirectory + @"\TemplateDatabase.db";
                    _tableID = 1367;
                    _moduleID = 1930;
                    _ordinateID = 1373; // 1370;
                    _ordinateMDID = 3011;
                    _ordinateHDID = 10009;
                    _hierarchyID = 1349;
                    _axisID = 1369;
                    _axisMemberID = 10003;
                    break;
                case Test_DB.EBA:

                    sourceFile = Path.Combine(folder, "TemplateDB_CRV.db");
                    //_tableID = 486;
                    _tableID = 558;
                    _moduleID = 1;
                    _ordinateID = 17838;
                    _ordinateMDID = 3011;
                    _ordinateHDID = 18656;
                    _hierarchyID = 425;
                    _axisID = 1369;
                    _axisMemberID = 1598;
                    break;
                case Test_DB.CRT:
                    sourceFile = Path.Combine(folder, "TemplatDB_CRT.db");
                    //_tableID = 486;
                    _tableID = 10;
                    _moduleID = 1;
                    _ordinateID = 17838;
                    _ordinateMDID = 3011;
                    _ordinateHDID = 18656;
                    _hierarchyID = 425;
                    _axisID = 1369;
                    _axisMemberID = 1598;

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Assert.IsTrue(File.Exists(sourceFile));
            string targetFile = Path.Combine(Path.GetTempPath(), "EIOPA");
            if (!Directory.Exists(targetFile)) Directory.CreateDirectory(targetFile);
            targetFile = Path.Combine(targetFile, string.Format("{0:yyyyMMdd_HHmmss_ff}.db", DateTime.Now));
            File.Copy(sourceFile, targetFile);
            Assert.IsTrue(File.Exists(targetFile));
            _testDB = targetFile;
            Assert.IsNotNull(_testDB);

            dInstance instance = new dInstance
                            {
                                ModuleID = 1930, 
                                EntityIdentifier = "TEST", 
                                EntityCurrency = "EUR", 
                                PeriodEndDateOrInstant = DateTime.Now.AddYears(1), 
                                EntityName = "Test Entry",
                                Timestamp = DateTime.Now
                            };
            PutSQLData putData = new PutSQLData(_testDB);
            putData.InsertUpdateInstance(instance, out _instanceID);
            putData.Dispose();

            _getData = new GetSQLData(_testDB);
            Assert.IsNotNull(_getData);
        }
        //
        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            _getData.Dispose();
            if(File.Exists(_testDB))
                File.Delete(_testDB);

        }
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
        ///A test for GetTree
        ///</summary>
        [TestMethod()]
        public void GetTreeTest()
        {
            int instanceID = 0;
            TreeBranch actual;
            actual = _getData.GetTree(instanceID);
            Assert.IsTrue(actual.SubBranches.Count > 0);

        }


         /// <summary>
        ///A test for ConnectionCheck
        ///</summary>
        [TestMethod()]
        public void ConnectionCheckTest()
        {
            bool expected = true;
            bool actual;
            actual = _getData.ConnectionCheck();
            Assert.AreEqual(expected, actual);
        }



        /// <summary>
        ///A test for GetAvailableComboData
        ///</summary>
        //[TestMethod()]
        //public void GetAvailableComboDataTest()
        //{
            
        //    long instanceID = 1;
        //    int tableVid = _tableID;
        //    List<ComboSelection> comboSelections = null; // TODO: Initialize to an appropriate value
        //    string zDimMemAdj = string.Empty; // TODO: Initialize to an appropriate value
        //    string zDimMemAdjExpected = string.Empty; // TODO: Initialize to an appropriate value
        //    IEnumerable<dAvailableTable> expected = null; // TODO: Initialize to an appropriate value
        //    IEnumerable<dAvailableTable> actual;
        //    actual = _getData.GetAvailableComboData(instanceID, tableVid, comboSelections, out zDimMemAdj);
        //    Assert.AreEqual(zDimMemAdjExpected, zDimMemAdj);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for GetCloseTemplateData
        /////</summary>
        //[TestMethod()]
        //public void GetCloseTemplateData_Test()
        //{
            
        //    long instanceID = 1;
        //    string groupTableIds = _tableID.ToString();
        //    SignatureUsingCombos signatureManagement = new SignatureUsingCombos(new List<ComboSelection>(){} );
        //    FormData actual;
        //    List<SolvencyIIColumnInfo> colInfo = new List<SolvencyIIColumnInfo>();
        //    colInfo.Add(new SolvencyIIColumnInfo { ColName = "R030C010", ColumnType = typeof(double) });
        //    colInfo.Add(new SolvencyIIColumnInfo { ColName = "R040C010", ColumnType = typeof(double) });
        //    actual = _getData.GetCloseTemplateData(instanceID, groupTableIds, signatureManagement, colInfo);
        //    // This approach is no longer used.
        //    Assert.IsTrue(actual.PageData.Count > 0);
        //    Assert.IsTrue(actual.ControlData.Count > 0);
        //}




        /// <summary>
        ///A test for GetInstanceDetails
        ///</summary>
        [TestMethod()]
        public void GetInstanceDetailsTest()
        {
            long instanceID = _instanceID; 
            dInstance actual;
            actual = _getData.GetInstanceDetails(instanceID);
            Assert.AreEqual(instanceID, actual.InstanceID);
        }

        /// <summary>
        ///A test for GetInstanceDropDownData
        ///</summary>
        [TestMethod()]
        public void GetInstanceDropDownData_Test()
        {
            
            IEnumerable<ComboItem> actual;
            actual = _getData.GetInstanceDropDownData();
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetLanguageDictionary
        ///</summary>
        [TestMethod()]
        public void GetLanguageDictionary_Test()
        {
            int applicationID = 1;
            eLanguageID languageID = eLanguageID.InEnglish;
            List<LanguageLabel> actual;
            actual = _getData.GetLanguageDictionary(applicationID, languageID);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetLanguageDropDownData
        ///</summary>
        [TestMethod()]
        public void GetLanguageDropDownDataTest()
        {
            IEnumerable<ComboItem> actual;
            actual = _getData.GetLanguageDropDownData();
            Assert.IsTrue(actual.Any());
        }



        /// <summary>
        ///A test for GetModuleDetails
        ///</summary>
        [TestMethod()]
        public void GetModuleDetailsTest()
        {
            mModule actual;
            actual = _getData.GetModuleDetails(_moduleID);
            Assert.AreEqual(_moduleID, actual.ModuleID);
        }


        /// <summary>
        ///A test for GetOrdinateHierarchyID_HD
        ///</summary>
        [TestMethod()]
        public void GetOrdinateHierarchyID_HDTest()
        {
            ComboHierarchy actual;
            actual = _getData.GetOrdinateHierarchyID_HD(_ordinateHDID);
            Assert.IsTrue(actual.HierarchyID != 0);
        }

        /// <summary>
        ///A test for GetOrdinateHierarchyID_MD
        ///</summary>
        [TestMethod()]
        public void GetOrdinateHierarchyID_MDTest()
        {
            ComboHierarchy actual;
            actual = _getData.GetOrdinateHierarchyID_MD(_ordinateMDID);
            if(_testingDbType == Test_DB.DPM)
                Assert.IsTrue(actual.HierarchyID!= 0);

        }

        /// <summary>
        ///A test for GetOrdinateType
        ///</summary>
        [TestMethod()]
        public void GetOrdinateTypeTest()
        {
            string expected = "MONETARY";
            string actual;
            actual = _getData.GetOrdinateType(_ordinateID);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for GetTableAxis
        ///</summary>
        [TestMethod()]
        public void GetTableAxisTest()
        {

            List<string> tableVid = new List<string>() { _tableID.ToString() };
            string axisOrientation = string.Empty; 
            IEnumerable<mAxis> actual;
            actual = _getData.GetTableAxis(tableVid, axisOrientation);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTableAxis
        ///</summary>
        [TestMethod()]
        public void GetTableAxisTest1()
        {

            int tableVid = _tableID;
            string axisOrientation = string.Empty; 
            IEnumerable<mAxis> actual;
            actual = _getData.GetTableAxis(tableVid, axisOrientation);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTableAxisOrdinate
        ///</summary>
        [TestMethod()]
        public void GetTableAxisOrdinateTest()
        {

            int tableVid = _tableID;
            IEnumerable<mAxisOrdinate> actual;
            actual = _getData.GetTableAxisOrdinate(tableVid);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTableAxisOrdinate
        ///</summary>
        [TestMethod()]
        public void GetTableAxisOrdinateTest1()
        {

            List<string> tableVid = new List<string>() { _tableID.ToString() };
            IEnumerable<mAxisOrdinate> actual;
            actual = _getData.GetTableAxisOrdinate(tableVid);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTableAxisOrdinateColumns
        ///</summary>
        [TestMethod()]
        public void GetTableAxisOrdinateColumnsTest()
        {

            int tableVid = _tableID;
            IEnumerable<mAxisOrdinate> actual;
            actual = _getData.GetTableAxisOrdinateColumns(tableVid);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTableAxisOrdinateColumns
        ///</summary>
        [TestMethod()]
        public void GetTableAxisOrdinateColumnsTest1()
        {

            List<string> tableVid = new List<string>() { _tableID.ToString() };
            IEnumerable<mAxisOrdinate> actual;
            actual = _getData.GetTableAxisOrdinateColumns(tableVid);
            Assert.IsTrue(actual.Any());
        }



        /// <summary>
        ///A test for GetTableLabelText
        ///</summary>
        [TestMethod()]
        public void GetTableLabelText_Test()
        {
            List<string> tableVids = new List<string>() { _tableID.ToString() };
            int language = 1; 
            IEnumerable<mAxisOrdinate> actual;
            actual = _getData.GetTableLabelText(tableVids, language);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTableLabelText
        ///</summary>
        [TestMethod()]
        public void GetTableLabelText_Test1()
        {

            int tableVid = _tableID;
            int language = 1;
            IEnumerable<mAxisOrdinate> actual;
            actual = _getData.GetTableLabelText(tableVid, language);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTableLabelTextNoTranslations
        ///</summary>
        [TestMethod()]
        public void GetTableLabelTextNoTranslationsTest()
        {
            List<string> tableVids = new List<string>() { _tableID.ToString() };
            IEnumerable<mAxisOrdinate> actual;
            actual = _getData.GetTableLabelTextNoTranslations(tableVids);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTableLabelTextNoTranslations
        ///</summary>
        [TestMethod()]
        public void GetTableLabelTextNoTranslationsTest1()
        {

            int tableVid = _tableID;
            IEnumerable<mAxisOrdinate> actual;
            actual = _getData.GetTableLabelTextNoTranslations(tableVid);
            Assert.IsTrue(actual.Any());
        }





        /// <summary>
        ///A test for GetTreeBranches
        ///</summary>
        [TestMethod()]
        public void GetTreeBranchesTest()
        {
             
            long instanceID = 1; 
            List<TreeItem> actual;
            actual = _getData.GetTreeBranches(instanceID);
            Assert.IsTrue(actual.Any());
        }

        /// <summary>
        ///A test for GetTreeViewModules
        ///</summary>
        [TestMethod()]
        public void GetTreeViewModulesTest()
        {
            IEnumerable<TreeItem> actual;
            actual = _getData.GetTreeViewModules(0);
            Assert.IsTrue(actual.Any());
        }





        /// <summary>
        ///A test for GetZdimTreeTables
        ///</summary>
        [TestMethod()]
        public void GetZdimTreeTablesTest()
        {
            Dictionary<int, int> actual;
            actual = _getData.GetZdimTreeTables();
            // There are not any in the DPM database
            if(_testingDbType == Test_DB.DPM)
                Assert.IsTrue(!actual.Any());
            else
                Assert.IsTrue(actual.Any());
        }






        /// <summary>
        ///A test for GetzAxisOrdinate
        ///</summary>
        [TestMethod()]
        public void GetzAxisOrdinateTest()
        {
            List<mAxisOrdinate> actual;
            actual = _getData.GetzAxisOrdinate(_axisID);
            Assert.IsTrue(actual.Any());
        }

        ///// <summary>
        /////A test for GetzAxisOrdinateOwnerPrefix
        /////</summary>
        //[TestMethod()]
        //public void GetzAxisOrdinateOwnerPrefixTest()
        //{
        //    string actual;
        //    actual = _getData.GetzAxisOrdinateOwnerPrefix(_axisID);
        //    Assert.IsTrue(actual.Any());
        //}



        /// <summary>
        ///A test for TreeItemsFromModule
        ///</summary>
        [TestMethod()]
        public void TreeItemsFromModuleTest()
        {
            mModule m =new mModule();
            m.ModuleID = 1234;
            m.ModuleLabel = "Testing";
            TreeItem actual;
            actual = GetSQLData.TreeItemsFromModule(m);
            Assert.AreEqual(m.ModuleID, actual.ModuleID);
            Assert.AreEqual(m.ModuleLabel, actual.DisplayText);
        }


        #region Form Generator Queries - SQLite Only

        /// <summary>
        ///A test for GetAllTableControls
        ///</summary>
        [TestMethod()]
        public void GetAllTableControlsTest()
        {

            List<string> tableVids = new List<string> { _tableID.ToString() };
            int singleZOrdinateID = 0;

            IEnumerable<FactInformation> actual;
            actual = _getData.GetAllTableControls(tableVids, singleZOrdinateID);
            Assert.IsTrue(actual.Any());
        }



        /// <summary>
        ///A test for GetLocationRanges
        ///</summary>
        [TestMethod()]
        public void GetLocationRangesTest()
        {
            List<string> tableGroupIDs = new List<string>() { _tableID.ToString() };
            List<string> actual;
            actual = _getData.GetLocationRanges(tableGroupIDs);
            if (_testingDbType == Test_DB.DPM)
                Assert.IsTrue(actual.Any());
            else
            {
                Assert.IsFalse(actual.Any()); // Not used in EBA or CRT
            }
        }



        #endregion





        //[TestMethod()]
        //public void GenericRepositoryWhereTest()
        //{
        //    GenericRepository<T__1365_S_02_01_03_01> _cashFlowTypeRepository;
        //    using (_cashFlowTypeRepository = new GenericRepository<T__1365_S_02_01_03_01>(_getData.Connection))
        //    {
        //        var test = _cashFlowTypeRepository.Where(r => r.INSTANCE == _instanceID);
        //        Assert.IsTrue(test.Count() > 0);
        //    }
        //}

        //[TestMethod()]
        //public void GenericRepositoryReadTest()
        //{
        //    GenericRepository<T__1365_S_02_01_03_01> _cashFlowTypeRepository;
        //    using (_cashFlowTypeRepository = new GenericRepository<T__1365_S_02_01_03_01>(_getData.Connection))
        //    {
        //        var test2 = _cashFlowTypeRepository.Read(r => r.INSTANCE == _instanceID && r.PAGE1 == "s2c_dim:CS(s2c_CS:x23)");
        //        Assert.IsNotNull(test2);
        //        Assert.AreEqual(_instanceID, test2.INSTANCE);
        //    }
        //}

        //[TestMethod()]
        //public void GenericRepositoryInsertAndDeleteTest()
        //{
        //    GenericRepository<T__10001_S_19_01_02_01> _cashFlowTypeRepository;
        //    string deleteText = "DELETE_ME";
        //    using (_cashFlowTypeRepository = new GenericRepository<T__10001_S_19_01_02_01>(_testDB))
        //    {
        //        var test = _cashFlowTypeRepository.Where(r => r.INSTANCE == _instanceID);
        //        int startCount = test.Count();
        //        T__10001_S_19_01_02_01 addition = new T__10001_S_19_01_02_01();
        //        addition.INSTANCE = _instanceID;
        //        addition.PAGE10003 = deleteText;
        //        Assert.IsTrue(_cashFlowTypeRepository.Create(addition)); 
        //        long uniqueId = addition.PK_ID ?? 0; // POCO needs attributes for Primary key for this to work.
        //        Assert.AreNotEqual(-1, uniqueId, "Failed to append record");
        //        test = _cashFlowTypeRepository.Where(r => r.INSTANCE == _instanceID);
        //        Assert.AreEqual(startCount + 1, test.Count(), "Count incorrect - not added correctly");
        //        test = _cashFlowTypeRepository.Where(r => r.INSTANCE == _instanceID && r.PAGE10003 == deleteText);
        //        Assert.AreEqual(1, test.Count(), "Count incorrect - record not found");

        //        T__10001_S_19_01_02_01 gathered = _cashFlowTypeRepository.Read(uniqueId);
        //        Assert.IsNotNull(gathered);
        //        Assert.AreEqual(_instanceID, gathered.INSTANCE);

        //        gathered.PAGE10003 = deleteText + "_EXTRATEXT";
        //        Assert.IsTrue(_cashFlowTypeRepository.Update(gathered));

        //        T__10001_S_19_01_02_01 gatheredAgain = _cashFlowTypeRepository.Read(uniqueId);
        //        Assert.IsNotNull(gatheredAgain);
        //        Assert.AreEqual(deleteText + "_EXTRATEXT", gatheredAgain.PAGE10003);

        //        Assert.IsTrue(_cashFlowTypeRepository.Delete(uniqueId), "Failed to delete record");
        //        test = _cashFlowTypeRepository.Where(r => r.INSTANCE == _instanceID);
        //        Assert.AreEqual(startCount, test.Count(), "Count incorrect - not deleted correctly");
        //    }
        //}

    }
}
