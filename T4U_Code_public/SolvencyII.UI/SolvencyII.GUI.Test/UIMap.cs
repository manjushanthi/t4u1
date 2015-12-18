namespace SolvencyII.GUI.Test
{
using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
using MouseButtons = System.Windows.Forms.MouseButtons;
using System.Drawing;
using System.Windows.Input;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UITesting.WinControls;
using Microsoft.Win32;
using System.Text;
    using System.IO;
    
    
    public partial class UIMap
    {
        public enum BuildType
        {
            Development,
            PreProduction,
            Production,
            Build

        }

        public string ActiveFileName { get; set; }
        public BuildType ActiveBuildType { get; set; }

        public string ApplicationTitle { get; set; }

        public T4UApplicationClick T4UApplicationClick { get; set; }


        public string GetApplicationVersion(BuildType ActiveBuildType)
        {
            if (ActiveBuildType == BuildType.Build)
            {
                return T4URegistryValue.RegistryRead("ApplicationVersion");
            }
            else
            {
                return T4URegistryValue.RegistryRead("ClickOnceVersion");
            }
        }


        public class T4URegistryValue
        {
            private static string userRoot = "HKEY_CURRENT_USER";
            private static string subkey = @"\Software\EIOPA\XBRT\WINDOWS_T4U\";
            private static string keyName = userRoot + subkey;

            public static string ApplicationVersion = RegistryRead("ApplicationVersion");
            public static string ClickOnceVersion = RegistryRead("ClickOnceVersion");
            public static string DataTierType = RegistryRead("DataTierType");
            public static string LastDatabasePath = RegistryRead("LastDatabasePath");



            public static string RegistryRead(string KeyName)
            {
                string value = (string) Registry.GetValue(keyName, KeyName,string.Empty);
                return value;
            }


            public static void RegistryWrite(string valueName, string newValue)
            {
                Registry.SetValue(keyName, valueName, newValue);
            }

        }


        public void LaunchSpecificApplication(BuildType buildType)
        {
            #region Variable Declarations
            WinComboBox uIOpenComboBox = this.UIRunWindow.UIItemWindow.UIOpenComboBox;
            WinEdit uIOpenEdit = this.UIRunWindow.UIItemWindow1.UIOpenEdit;
            #endregion

            string applicationURL = GetApplicationInstallUrl(buildType);

            uIOpenComboBox.EditableItem = "iexplore " + applicationURL;

            // Type '{Enter}' in 'Open:' text box
            Keyboard.SendKeys(uIOpenEdit, "{Enter}", ModifierKeys.None);
        }

        public void LaunchSpecificApplication(string applicationPath)
        {
            #region Variable Declarations
            WinComboBox uIOpenComboBox = this.UIRunWindow.UIItemWindow.UIOpenComboBox;
            WinEdit uIOpenEdit = this.UIRunWindow.UIItemWindow1.UIOpenEdit;
            #endregion

            uIOpenComboBox.EditableItem = applicationPath;

            // Type '{Enter}' in 'Open:' text box
            Keyboard.SendKeys(uIOpenEdit, "{Enter}", ModifierKeys.None);
        }


        public static string CreateApplicationTitleURL(string activeFileName, BuildType buildType)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(activeFileName);
            sb.Append(" ");
            sb.Append("Application version: ");
            
            if (buildType == BuildType.Build)
                sb.Append(T4URegistryValue.ClickOnceVersion);
            else
                sb.Append(T4URegistryValue.ApplicationVersion);

            sb.Append(" ");
            sb.Append(GetApplicationTitle(buildType));

            return sb.ToString();
        }

        public void ApplicationTitleBar(string activeFileName, BuildType buildType)
        {
            WinTitleBar t4uApplicationTitleBar = new WinTitleBar(new WinWindow());

            //string applicationTitleURL = @"D:\\Thangarajan\\TestApp\\1111.xbrt Application version: 2014.10.23.1  Development Environment";
            string applicationTitleURL = CreateApplicationTitleURL(activeFileName, buildType);
            
            t4uApplicationTitleBar.WindowTitles.Add(applicationTitleURL);
        }

        public static string GetApplicationTitle(BuildType applicationType)
        {
            string defaultApplicationTiltle = "Development Environment";
            if (applicationType == BuildType.Development)
            {
                return "Development Environment";
            }
            else if (applicationType == BuildType.PreProduction)
            {
                return "Test Environment";
            }
            else if (applicationType == BuildType.Production)
            {
                return "";
            }
            else if (applicationType == BuildType.Build)
            {
                return "";
            }
            else
            {
                return defaultApplicationTiltle;
            }
        }

        public string GetApplicationInstallUrl(BuildType applicationType)
        {
            string defalutUrl = "http://eiopa.github.io/XBRT-Deployment/DEV/SolvencyII.StartDEV.application";
            if (applicationType == BuildType.Development)
            {
                return "http://eiopa.github.io/XBRT-Deployment/DEV/SolvencyII.StartDEV.application";
            }
            else if (applicationType == BuildType.PreProduction)
            {
                return "http://dev.eiopa.europa.eu/XBRT/Deployment/WindowsT4U/PRE/SolvencyII.PRE.application";
            }
            else if (applicationType == BuildType.Production)
            {
                return "http://dev.eiopa.europa.eu/XBRT/Deployment/WindowsT4U/PRO/SolvencyII.application";
            }
            else
            {
                return defalutUrl;
            }

        }


        /// <summary>
        /// ActiveApplication
        /// </summary>
        public void ActiveApplication()
        {

            Mouse.Click(this.UIDThangarajanTestApp1Window2.UICUsersnagareththDocuTitleBar, new Point(1104, 5));
        }
        

        public void ExitApplication()
        {

            Mouse.Click(this.UIDThangarajanTestApp1Window2.UICUsersnagareththDocuTitleBar, new Point(1104, 5));

            // Click 'Close' button
            Mouse.Click(this.T4UApplicationClick.ApplicationCloseButton, new Point(40, 3));
        }

        /// <summary>
        /// ExitApplicationByExitMenu
        /// </summary>
        public void ExitApplicationByExitMenu()
        {

            Mouse.Click(this.UIDThangarajanTestApp1Window2.UICUsersnagareththDocuTitleBar, new Point(1104, 5));

            WinMenuItem uIExitMenuItem = this.UIDThangarajanTestApp1Window1.UIMenuStrip1MenuBar.UIMultireportcontainerMenuItem.UIExitMenuItem;
            // Click 'File' -> 'Exit' menu item
            Mouse.Click(uIExitMenuItem, new Point(57, 19));

           
        }

        public void CreateMultiReportContainer(string testfileName)
        {
            #region Variable Declarations
            WinComboBox uIFilenameComboBox = this.UICreateSolvencyIImultWindow.UIDetailsPanePane.UIFilenameComboBox;
            WinButton uISaveButton = this.UICreateSolvencyIImultWindow.UISaveWindow.UISaveButton;
            #endregion

            WinMenuItem uISolvencyIIMenuItem = this.UIDThangarajanTestApp1Window2.UIMenuStrip1MenuBar.UIMultireportcontainerMenuItem.UICreateanewmultireporMenuItem.UISolvencyIIMenuItem;


            Mouse.Click(this.UIDThangarajanTestApp1Window2.UICUsersnagareththDocuTitleBar, new Point(1104, 5));

            // Click 'File' -> 'Create a new multi-report container' -> 'Solvency II' menu item
            Mouse.Click(uISolvencyIIMenuItem, new Point(56, 11));
            
              
            // Select 'TestCase1' in 'File name:' combo box
            uIFilenameComboBox.EditableItem = testfileName;

            if (File.Exists(testfileName))
            {
                #region Variable Declarations
                WinButton uIYesButton = this.UIConfirmSaveAsWindow.UIConfirmSaveAsPane.UIYesButton;
                #endregion

                // Click '&Save' button
                Mouse.Click(uISaveButton, new Point(51, 16));
  
                // Click '&Yes' button
                Mouse.Click(uIYesButton, new Point(31, 17));
            }
            else
            {
                // Click '&Save' button
                Mouse.Click(uISaveButton, new Point(51, 16));
            }
             

        }


        public void CreateReport(string reportName)
        {
            #region Variable Declarations
            WinEdit uITxtReportNameEdit = this.UIAddanewreportWindow.UITxtReportNameWindow.UITxtReportNameEdit;
            WinComboBox uIDateComboBox = this.UIAddanewreportWindow.UICboModelWindow.UIDateComboBox;
            WinDateTimePicker uIDtPeriodDateTimePicker = this.UIAddanewreportWindow.UIDtPeriodWindow.UIDtPeriodDateTimePicker;
            WinEdit uITxtEntityIDEdit = this.UIAddanewreportWindow.UITxtEntityIDWindow.UITxtEntityIDEdit;
            WinEdit uITxtNameEdit = this.UIAddanewreportWindow.UITxtNameWindow.UITxtNameEdit;
            WinButton uICreateanewreportButton = this.UIAddanewreportWindow.UICreateanewreportWindow.UICreateanewreportButton;
            #endregion

            // Click 'Report' -> 'Create a new report' menu item
            Mouse.Click(this.UIDThangarajanTestApp1Window2.UICUsersnagareththDocuTitleBar, new Point(1104, 5));

            // Type 'Report1' in 'txtReportName' text box
            uITxtReportNameEdit.Text = this.CreateReportParams.UITxtReportNameEditText;

            // Type '{Tab}' in 'txtReportName' text box
            Keyboard.SendKeys(uITxtReportNameEdit, this.CreateReportParams.UITxtReportNameEditSendKeys, ModifierKeys.None);

            // Type '{Tab}' in 'Date' combo box
            Keyboard.SendKeys(uIDateComboBox, this.CreateReportParams.UIDateComboBoxSendKeys, ModifierKeys.None);

            // Type '{Tab}' in 'dtPeriod' date time picker
            Keyboard.SendKeys(uIDtPeriodDateTimePicker, this.CreateReportParams.UIDtPeriodDateTimePickerSendKeys, ModifierKeys.None);

            // Type 'Report1' in 'txtEntityID' text box
            uITxtEntityIDEdit.Text = this.CreateReportParams.UITxtEntityIDEditText;

            // Type 'Report1' in 'txtName' text box
            uITxtNameEdit.Text = this.CreateReportParams.UITxtNameEditText;

            // Click 'Create a new report' button
            Mouse.Click(uICreateanewreportButton, new Point(35, 14));
        }

        public void ImportXBRLInstance(string importFileName, string exportFileName)
        {
            #region Variable Declarations
            WinTitleBar uICUsersnagareththDocuTitleBar = this.UIDThangarajanTestApp1Window2.UICUsersnagareththDocuTitleBar;
            
            WinEdit uINameEdit = this.UILocateXBRLfiletoimpoWindow.UIItemWindow.UIQrgxbrlListItem.UINameEdit;

            WinTitleBar uIArelleresultsparsingTitleBar = this.UIArelleresultsparsingWindow.UIArelleresultsparsingTitleBar;
            
            WinButton uIOKButton1 = this.UIArelleresultsparsingWindow.UIOKWindow.UIOKButton;
            WinComboBox uIFilenameComboBox = this.UILocateXBRLfiletoimpoWindow.UIItemWindow1.UIFilenameComboBox;

            WinButton uIOpenButton = this.UILocateXBRLfiletoimpoWindow.UIOpenWindow.UIOpenButton;
            WinEdit uIItemEdit = this.UINameofinstanceWindow.UIItemWindow.UIItemEdit;
            WinButton uIOKButton = this.UINameofinstanceWindow.UIOKWindow.UIOKButton;
            WinTreeItem uIBalancesheetTreeItem = this.UIDThangarajanTestApp1Window2.UITreeView1Window.UIPreparatoryreportingTreeItem.UIS020103BalancesheetTreeItem.UIBalancesheetTreeItem;
        
            #endregion

            WinMenuItem uISolvencyIIMenuItem = this.UIDThangarajanTestApp1Window2.UIMenuStrip1MenuBar.UIMultireportcontainerMenuItem.UICreateanewmultireporMenuItem.UISolvencyIIMenuItem;
            WinMenuItem uISubMenuMenuItem = this.UIDThangarajanTestApp1Window2.UIMenuStrip1MenuBar.UIXBRLMenuItem.UITestLevel1MenuItem.UISubMenuMenuItem;
            WinMenuItem uINativeimportMenuItem = this.UIDThangarajanTestApp1Window2.UIMenuStrip1MenuBar.UIXBRLMenuItem.UIImportXBRLinstancefiMenuItem1.UINativeimportMenuItem;


            // Click 'C:\Users\nagarethth\Documents\TestCase1.xbrt Appli...' title bar
            Mouse.Click(uICUsersnagareththDocuTitleBar, new Point(375, 9));


            //uINativeimportMenuItem.SetFocus();
            // Click 'XBRL' -> 'Import XBRL instance file' -> 'Native import' menu item
            Mouse.Click(uINativeimportMenuItem, new Point(78, 15));
            //Mouse.Click(uISubMenuMenuItem, new Point(78, 15));
            //Mouse.Click(uISubMenu2MenuItem, new Point(78, 15));
            //Mouse.Click(uISubMenu3MenuItem, new Point(78, 15));
            //Mouse.Click(uISubMenu3MenuItem1, new Point(78, 15));

            // Select 'C:\test3.xbrl' in 'File name:' combo box
            uIFilenameComboBox.EditableItem = importFileName;

            // Click '&Open' button
            Mouse.Click(uIOpenButton, new Point(59, 17));

            // Type 'testcase1' in text box
            uIItemEdit.Text = this.TempCode1Params.UIItemEditText;

            // Click '&OK' button
            Mouse.Click(uIOKButton, new Point(27, 10));

            // Click 'Preparatory reporting Groups Quarterly' -> 'S.02.01.06 Balance sheet' -> 'Balance sheet' tree item
            Mouse.Click(uIBalancesheetTreeItem, new Point(18, 4));

            #region Variable Declarations
            WinMenuItem uIExportXBRLinstancefiMenuItem = this.UIDThangarajanTestApp1Window2.UIMenuStrip1MenuBar.UIXBRLMenuItem.UIExportXBRLinstancefiMenuItem;
            WinMenu uIExportXBRLinstancefiMenu = this.UIItemWindow.UIExportXBRLinstancefiMenu;
            WinMenuItem uIIntegratedMenuItem = this.UIDThangarajanTestApp1Window2.UIMenuStrip1MenuBar.UIXBRLMenuItem.UIExportXBRLinstancefiMenuItem.UIIntegratedMenuItem;
            WinComboBox uIFilenameComboBox1 = this.UICreateanewmultireporWindow.UIDetailsPanePane.UIFilenameComboBox;
            WinButton uISaveButton = this.UICreateanewmultireporWindow.UISaveWindow.UISaveButton;
            WinButton uIYesButton = this.UIConfirmSaveAsWindow.UIConfirmSaveAsPane.UIYesButton;
            #endregion

            // Click 'XBRL' -> 'Export XBRL instance file' menu item
            Mouse.Click(uIExportXBRLinstancefiMenuItem, new Point(50, 12));

            // Click 'Export XBRL instance fileDropDown' popup menu
            Mouse.Click(uIExportXBRLinstancefiMenu, new Point(97, 1));

            // Click 'XBRL' -> 'Export XBRL instance file' -> 'Integrated' menu item
            Mouse.Click(uIIntegratedMenuItem, new Point(86, 13));

            // Select 'D:\test.xbrl' in 'File name:' combo box
            uIFilenameComboBox1.EditableItem = exportFileName;

            // Click '&Save' button
            //Mouse.Click(uISaveButton, new Point(28, 20));


            if (File.Exists(exportFileName))
            {
                // Click '&Save' button
                Mouse.Click(uISaveButton, new Point(63, 14));

                // Click '&Yes' button
                Mouse.Click(uIYesButton, new Point(40, 11));
            }
            else
            {
                // Click '&Save' button
                Mouse.Click(uISaveButton, new Point(51, 16));
            }
            Playback.PlaybackSettings.WaitForReadyLevel = WaitForReadyLevel.AllThreads;
            Playback.Wait(1000);
        }
    }


    public class T4UApplicationClick
    {
        public string ActiveFileName { get; set; }
        public SolvencyII.GUI.Test.UIMap.BuildType ActiveBuildType { get; set; }

        public WinWindow t4uWindow = new WinWindow(); 

        public WinTitleBar t4uApplicationTitleBar { get; set; }

        public WinMenuBar MainMenuBar { get; set; }

        public WinMenuItem MultiReportContainerWinMenuItem { get; set; }

        public WinMenuItem ReportWinMenuItem { get; set; }

        public WinMenuItem CreateANewMultiReportContainerWinMenuItem { get; set; }

        public WinMenuItem CreateANewReportMenuItem { get; set; }

        public WinMenuItem SolvencyIIMenuItem { get; set; }


        public WinMenuItem ExitWinMenuItem { get; set; }

        public string ApplicationTitle { get; set; }

        public WinButton ApplicationCloseButton { get; set; }

        public WinWindow UITreeViewWindow { get; set; }


        public WinMenuItem XBRLWinMenuItem { get; set; }

        public WinMenuItem ImportXBRLInstanceFileMenuItem { get; set; }


        public WinMenuItem ImportXBRLInstanceFileNativeImportMenuItem { get; set; }

        public WinMenuItem ImportXBRLInstanceFileImportArelleWithValidationMenuItem { get; set; }
        public WinMenuItem ImportXBRLInstanceFileImportArelleWithOutValidationMenuItem { get; set; }

        public WinMenuItem ImportMenuItem { get; set; }

        public WinMenuItem ImportTestMenuItem { get; set; }

        public T4UApplicationClick(string activeFileName, SolvencyII.GUI.Test.UIMap.BuildType activeBuildType)
        {
            this.ActiveFileName = activeFileName;
            this.ActiveBuildType = activeBuildType;

            this.ApplicationTitle = UIMap.CreateApplicationTitleURL(ActiveFileName, ActiveBuildType);

            t4uWindow.SearchProperties.Clear();
            t4uWindow.SearchProperties[WinWindow.PropertyNames.Name] = UIMap.CreateApplicationTitleURL(activeFileName, activeBuildType); ;
            t4uWindow.SearchProperties.Add(new PropertyExpression(WinWindow.PropertyNames.ClassName, "WindowsForms10.Window", PropertyExpressionOperator.Contains));
            //t4uWindow.WindowTitles.Add(this.ApplicationTitle);

            this.t4uApplicationTitleBar = new WinTitleBar(t4uWindow);


            this.MainMenuBar = new WinMenuBar(t4uWindow);
            MainMenuBar.SearchProperties[WinMenu.PropertyNames.Name] = "menuStrip1";
            //MainMenuBar.WindowTitles.Add(this.ApplicationTitle);


            this.MultiReportContainerWinMenuItem = new WinMenuItem(MainMenuBar);

            MultiReportContainerWinMenuItem.SearchProperties[WinMenu.PropertyNames.Name] = "File";
            //MultiReportContainerWinMenuItem.WindowTitles.Add(this.ApplicationTitle);


            this.CreateANewMultiReportContainerWinMenuItem = new WinMenuItem(MultiReportContainerWinMenuItem);

            CreateANewMultiReportContainerWinMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Create a new multi-report container";
            CreateANewMultiReportContainerWinMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
           //CreateANewMultiReportContainerWinMenuItem.WindowTitles.Add(this.ApplicationTitle);



            this.SolvencyIIMenuItem = new WinMenuItem(CreateANewMultiReportContainerWinMenuItem);
            SolvencyIIMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Solvency II";
            SolvencyIIMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            //SolvencyIIMenuItem.WindowTitles.Add(this.ApplicationTitle);


            this.ExitWinMenuItem = new WinMenuItem(MultiReportContainerWinMenuItem);

            ExitWinMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Exit";
            ExitWinMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            //ExitWinMenuItem.WindowTitles.Add(this.ApplicationTitle);

            /*
            //Report
            this.ReportWinMenuItem = new WinMenuItem(MainMenuBar);

            ReportWinMenuItem.SearchProperties[WinMenu.PropertyNames.Name] = "Report";
            ReportWinMenuItem.WindowTitles.Add(this.ApplicationTitle);


            this.CreateANewReportMenuItem = new WinMenuItem(ReportWinMenuItem);

            CreateANewReportMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Create a new report";
            CreateANewReportMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            CreateANewReportMenuItem.WindowTitles.Add(this.ApplicationTitle);



            this.ApplicationCloseButton = new WinButton(t4uApplicationTitleBar);
            ApplicationCloseButton.SearchProperties[WinButton.PropertyNames.Name] = "Close";
            ApplicationCloseButton.WindowTitles.Add(this.ApplicationTitle);

            UITreeViewWindow = new WinWindow();
            UITreeViewWindow.SearchProperties[WinWindow.PropertyNames.ControlName] = "treeView1";
            UITreeViewWindow.WindowTitles.Add(this.ApplicationTitle);


            this.XBRLWinMenuItem = new WinMenuItem(MainMenuBar);
            XBRLWinMenuItem.SearchProperties[WinMenu.PropertyNames.Name] = "XBRL";
            XBRLWinMenuItem.WindowTitles.Add(this.ApplicationTitle);

            this.ImportXBRLInstanceFileMenuItem = new WinMenuItem(XBRLWinMenuItem);
            ImportXBRLInstanceFileMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Import XBRL instance file";
            ImportXBRLInstanceFileMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            ImportXBRLInstanceFileMenuItem.WindowTitles.Add(this.ApplicationTitle);



            this.ImportXBRLInstanceFileNativeImportMenuItem = new WinMenuItem(ImportXBRLInstanceFileMenuItem);
            ImportXBRLInstanceFileNativeImportMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Native import";
            ImportXBRLInstanceFileNativeImportMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            ImportXBRLInstanceFileNativeImportMenuItem.WindowTitles.Add(this.ApplicationTitle);

            this.ImportXBRLInstanceFileImportArelleWithValidationMenuItem = new WinMenuItem(ImportXBRLInstanceFileMenuItem);
            ImportXBRLInstanceFileImportArelleWithValidationMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Import using Arelle (with validation)";
            ImportXBRLInstanceFileImportArelleWithValidationMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            ImportXBRLInstanceFileImportArelleWithValidationMenuItem.WindowTitles.Add(this.ApplicationTitle);

            this.ImportXBRLInstanceFileImportArelleWithOutValidationMenuItem = new WinMenuItem(ImportXBRLInstanceFileMenuItem);
            ImportXBRLInstanceFileImportArelleWithOutValidationMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Import using Arelle (without validation)";
            ImportXBRLInstanceFileImportArelleWithOutValidationMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            ImportXBRLInstanceFileImportArelleWithOutValidationMenuItem.WindowTitles.Add(this.ApplicationTitle);


            this.ImportMenuItem = new WinMenuItem(XBRLWinMenuItem);
            ImportMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Test Native Import";
            ImportMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            ImportMenuItem.WindowTitles.Add(this.ApplicationTitle);

            this.ImportTestMenuItem = new WinMenuItem(ImportMenuItem);
            ImportTestMenuItem.SearchProperties[WinMenuItem.PropertyNames.Name] = "Native Import";
            ImportTestMenuItem.SearchConfigurations.Add(SearchConfiguration.ExpandWhileSearching);
            ImportTestMenuItem.WindowTitles.Add(this.ApplicationTitle);
             */

        }
    }
}
