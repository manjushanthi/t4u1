using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Input;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using System.IO;
using System.Threading;


namespace SolvencyII.GUI.Test
{
    /// <summary>
    /// Summary description for CodedUITest1
    /// </summary>
    [CodedUITest]
    public class SolvencyIIGUITest
    {
        public SolvencyIIGUITest()
        {
        }


        [TestCategory("Build Environment")]
        [TestMethod]
        public void LaunchApplicationFromBuild()
        {
            LaunchApplication(UIMap.BuildType.Build);
        }


        public void LaunchApplication(UIMap.BuildType buildType)
        {


            UIMap.T4URegistryValue.RegistryWrite("TestMode", "true");

            string currentDirectory = System.Environment.CurrentDirectory;
            string deploymentDirectory = TestContext.DeploymentDirectory;

            DirectoryInfo svnRootDirectoryInfo = Directory.GetParent(deploymentDirectory).Parent.Parent.Parent.Parent;
            


            DirectoryInfo directoryInfo = Directory.GetParent(deploymentDirectory).Parent.Parent;
            string testApplicationPath = "\"" + directoryInfo.FullName + @"\SolvencyII.GUI\bin\Debug\SolvencyII.GUI.exe" + "\"";

            string oldclickOnceVersion = UIMap.GetApplicationVersion(buildType);

            string activeFileName = UIMap.T4URegistryValue.LastDatabasePath;

            this.UIMap.InvokeRunPrompt();
            if (buildType == Test.UIMap.BuildType.Build)
            {
                this.UIMap.LaunchSpecificApplication(testApplicationPath); 
            }
            else
            {
                this.UIMap.LaunchSpecificApplication(this.UIMap.ActiveBuildType);
            }
            this.UIMap.AcceptTerms();
            Playback.Wait(1000);

            string newclickOnceVersion = UIMap.GetApplicationVersion(buildType);
            
            if (string.IsNullOrEmpty(activeFileName) || !File.Exists(activeFileName))
            {
                this.UIMap.IfNoExistingFiles();
            }

            
                if (oldclickOnceVersion != newclickOnceVersion)
            {
                this.UIMap.CloseWhatsNew();
            }
            
            
            string documentsPath = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string testCaseFileName = documentsPath + @"\TestCase1.xbrt";
            //string testCaseFileName = "\"" + svnRootDirectoryInfo.FullName + @"\Temp\TestCase1.xbrt" + "\"";
            string testCaseFileName = svnRootDirectoryInfo.FullName + @"\Temp\TestCase1.xbrt";
            //USE TEMP FOLDER OF SVN
            /*
            string testCaseFileName = "\"" + svnRootDirectoryInfo.FullName + @"\Temp\TestCase1.xbrt" + "\"";
            if (File.Exists(testCaseFileName))
            {
                File.Delete(testCaseFileName);
            }
            */

            this.UIMap.CreateMultiReportContainer(testCaseFileName);

            //DirectoryInfo svnRootFolderPath = Directory.GetParent(deploymentDirectory).Parent.Parent.Parent.Parent;
            //string importFileName = "\"" + svnRootFolderPath + @"\13. XBRL Instance Documents\1.5.2 (generated with DPM Architect)\qrg\qrg.xbrl" + "\"";

            string importFileName = "\"" + svnRootDirectoryInfo.FullName + @"\13. XBRL Instance Documents\1.5.2 (generated with DPM Architect)\qrg\ImportTestCase1_qrg.xbrl" + "\"";

            //string exportFileName = "\"" + svnRootDirectoryInfo.FullName + @"\Temp\ExportTestCase1_qrg.xbrl" + "\"";
            string exportFileName = svnRootDirectoryInfo.FullName + @"\Temp\ExportTestCase1_qrg.xbrl";
            //string exportFileName = documentsPath + @"\Temp\ExportTestCase1_qrg.xbrl";

            string importFileName1 = svnRootDirectoryInfo.FullName + @"\13. XBRL Instance Documents\1.5.2 (generated with DPM Architect)\qrg\ImportTestCase1_qrg.xbrl";


            this.UIMap.ImportXBRLInstance(importFileName, exportFileName);
            this.UIMap.ExitApplicationByExitMenu();

            bool result = CompareFileBytes(importFileName1, exportFileName);


            Assert.AreEqual(true, result, "Import and Export File are not same");

            //this.UIMap.ActiveApplication();
            

        }

        private static bool CompareFileBytes(string fileName1, string fileName2)
        {
            // Compare file sizes before continuing. 
            // If sizes are equal then compare bytes.
            if (CompareFileSizes(fileName1, fileName2))
            {
                int file1byte = 0;
                int file2byte = 0;

                // Open a System.IO.FileStream for each file.
                // Note: With the 'using' keyword the streams 
                // are closed automatically.
                using (FileStream fileStream1 = new FileStream(fileName1, FileMode.Open),
                                  fileStream2 = new FileStream(fileName2, FileMode.Open))
                {
                    // Read and compare a byte from each file until a
                    // non-matching set of bytes is found or the end of
                    // file is reached.
                    do
                    {
                        file1byte = fileStream1.ReadByte();
                        file2byte = fileStream2.ReadByte();
                    }
                    while ((file1byte == file2byte) && (file1byte != -1));
                }

                return ((file1byte - file2byte) == 0);
            }
            else
            {
                return false;
            }
        }

        private static bool CompareFileSizes(string fileName1, string fileName2)
        {
            bool fileSizeEqual = true;

            // Create System.IO.FileInfo objects for both files
            FileInfo fileInfo1 = new FileInfo(fileName1);
            FileInfo fileInfo2 = new FileInfo(fileName2);

            // Compare file sizes
            if (fileInfo1.Length != fileInfo2.Length)
            {
                // File sizes are not equal therefore files are not identical
                fileSizeEqual = false;
            }

            return fileSizeEqual;
        }

        #region Additional test attributes

        // You can use the following additional attributes as you write your tests:

        ////Use TestInitialize to run code before running each test 
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        ////Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{        
        //    // To generate code for this test, select "Generate Code for Coded UI Test" from the shortcut menu and select one of the menu items.
        //}

        #endregion

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
        private TestContext testContextInstance;

        public UIMap UIMap
        {
            get
            {
                if ((this.map == null))
                {
                    this.map = new UIMap();
                }

                return this.map;
            }
        }

        private UIMap map;
    }
}
