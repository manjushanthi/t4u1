using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using ClassicRelationalETL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SolvencyII.Data.Shared;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Arelle;
using SolvencyII.UI.Shared.Classes;
using SolvencyII.Validation;
using SolvencyII.Validation.Domain;
using SolvencyII.Validation.Query;
using SolvencyII.Validation.UI;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for EtlOperationsTest and is intended
    ///to contain all EtlOperationsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ArelleTest
    {
        private string connectionString = "C:\\temp\\Sol2Test.xbrt";
        private ArelleCmdInterface ExportArelle;
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
        ///A test for EtlOperations Constructor
        ///</summary>
        [TestMethod()]
        public void ExportArelleTest01()
        {
            EtlOperations factsETL = new EtlOperations();
            string targetXBRL = "C:\\temp\\exported.xbrl";
            int InstanceID = 1001;
            eImportExportOperationType type = eImportExportOperationType.Native_Export;
            PreParatoryVersions prepVersion = PreParatoryVersions.NotApplicable;

            factsETL.etlSavingXBRLinstance(connectionString, Convert.ToInt32(InstanceID));
            ExportArelle = new ArelleCmdInterface("Saving instance - ");
            ExportArelle.SaveFromDatabaseToInstance(type, InstanceID, targetXBRL, null, ExportArelleComplete, prepVersion);
            factsETL = null;

        }


        private void ExportArelleComplete(object s, RunWorkerCompletedEventArgs args)
        {
            //Get the active instance ID
            //Displays error log in list box
            IValidationQuery validationQuery = ValidationFactory.GetValidationQuery(eDataTier.SqLite);
            ISolvencyData conn = ConnectionFactory.GetConnection(eDataTier.SqLite, connectionString);

            IEnumerable<ValidationMessage> messages = null;

            //Handle exception if there is an issue in Arelle processing
            if (args.Error != null || args.Cancelled == true)
            {
                Assert.Fail(string.Format("An error occured while processing Exporting or validating XBRL {0}.\nPlease check the log for more information.", args.Error));
                return; //do not process further
            }

            if (ExportArelle.InstanceID > 0)
                messages = validationQuery.GetArelleValidationErrors(conn, ExportArelle.InstanceID);
            else
                Assert.Fail("Instance was not retrieved");

            //string report = (string)args.Result;
            //New implementation to handle multiple parameters
            string report = string.Empty;
            string filePath = string.Empty;
            if (args.Result is Object[]) // cast #1
            {
                object[] resultParams = args.Result as Object[];
                if (resultParams[0] != null)
                {
                    report = resultParams[0].ToString();
                }
                if (resultParams[2] != null)
                {
                    filePath = resultParams[2].ToString();
                }

            }


            {
                //Show only if there are any errors
                if (messages != null && messages.Count() > 0)
                {
                    Assert.Fail(string.Format("There were {0} messages", messages.Count()));
                }
                else
                {
                    Assert.IsFalse(string.IsNullOrEmpty(report));
                }
            }


            //Show a message box that import XBRL is complete
            ExportArelle = null;

        }


    }
}
