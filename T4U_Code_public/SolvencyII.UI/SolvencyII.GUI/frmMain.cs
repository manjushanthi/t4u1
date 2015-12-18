using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Data.Shared.Helpers;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;
using SolvencyII.GUI.Classes;
using SolvencyII.GUI.MEF;
using SolvencyII.GUI.UserControls;
using SolvencyII.UI.Shared.Arelle;
using SolvencyII.UI.Shared.Config;
using SolvencyII.UI.Shared.Configuration;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.Databases;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.ImpExpVal;
using SolvencyII.UI.Shared.Loggers;
using SolvencyII.UI.Shared.Misc;
using SolvencyII.UI.Shared.Registry;
using SolvencyII.UI.Shared.UserControls;
using SolvencyII.Data.CRT.ETL;
using System.Deployment.Application;
using SolvencyII.UI.Shared.UserInput;
using SolvencyII.Validation.Parser;
using SolvencyII.ExcelImportExportLib;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.Validation;
using SolvencyII.Validation.Domain;
using SolvencyII.Validation.Model;
using SolvencyII.Validation.UI;
using SolvencyII.Validation.Query;
using System.Management;
using Ionic.Zip;
using SolvencyII.DataTypeValidation;
using SolvencyII.UI.Shared.Classes;
using SolvencyII.UI.Shared.Dialog;
using System.Globalization;
using System.Xml.Linq;
using System.Net;
using SolvencyII.GUI.RSS;


namespace SolvencyII.GUI
{
    /// <summary>
    /// Main form containing the jump off points for all user interaction and functionality.
    /// </summary>
    public partial class frmMain : Form
    {
        
        #region Properties and Declaration

        //Settings - Supported version settings for the CRD IV Database &  Excel Templates
        private const string supportedDataBaseVersion_CRD = "2015.02.13";

        //Settings - Supported version settings for the Solvency II Database &  Excel Templates
        private const string supportedDataBaseVersion_FULL = "2015.08.28";
        private const string supportedExcelTemplateVersion_FULL = "2015.08.28";

        //Settings - Supported version settings for the Solvency II PREPARATORY Database &  Excel Templates
        private const string supportedDataBaseVersion_PREPARATORY = "2015.08.28";
        private const string supportedExcelTemplateVersion_PREPARATORY = "2015.04.30";

        private const string supportedMigrationDataBaseVersion_PREPARATORY_FebruaryVersion_2015 = "2015.02.10";
        private const string supportedMigrationDataBaseVersion_PREPARATORY_MarchVersion_2015 = "2015.03.16";
        PreParatoryVersions prepVersion = PreParatoryVersions.SecondVersion;

        private const string supportedExcelTemplateVersion_BUSINESS = "2015.08.28";

        private Panel dynamicPanel { get; set; }

        // External properies
        private long _instanceID;

        public long InstanceID
        {
            get { return _instanceID; }
            set
            {
                _instanceID = value;
                if (RegSettings.InstanceID != _instanceID)
                    RegSettings.InstanceID = _instanceID;
                RefreshInstanceChanged();
            }
        }

        private string _importedFileName;
        private string _importedFullFileName;
        private string _importedInstanceName;
        private bool _migrationRequired=false;

        public string importedFileMigration = null;
        public string importedFileMigration_InstanceName = null;

        private bool _isForMigration=false;
        private string _userManualFileName=string.Empty;

        private TreeItem _selectedItem;

        private int _languageID;
        public int LanguageID
        {
            get
            {
                return _languageID;
            }
            set
            {
                if (_languageID != value)
                {
                    RegSettings.LanguageID = value;
                    LanguageChange(value);
                }
                _languageID = value;
                StaticSettings.LanguageID = _languageID;
            }
        }

        // Closed controls
        private UserControlBase _mainControl;
        private UserControlBase2 _mainControlSpecialCase;
        private IParentUserControl _parentUserControl;
       
        // Open Controls
        private OpenUserControlBase2 _mainOpenControl;

        private System.Windows.Forms.Timer asyncTimer = new System.Windows.Forms.Timer();
        private readonly SolvencyIIExtensions _userExtensibility;
        Stopwatch watch = new Stopwatch();
        private bool isArelleSetupCompleted = false;
        private bool allowTreeSelection = true;

        //Validators
        ObjectListView errorView, errorContainerValidationView, cellPropertiesView;
        IValidator validator;
        ContextMenuStrip ctxErrorViewMenu;

        List<DataTypeValidationResult> dataTypeAllValidationResultsList = null;
        

        private void MEFComposed(string msg)
        {
            Invoke((Action) delegate
                                {
                                    StopToolStripProgressBar();
                                    if (!string.IsNullOrEmpty(msg)) MessageBox.Show(msg);
                                });
        }

        ArelleCmdInterface ImportExportArelle { get; set; }
        BackgroundWorker ImportExportThreadKarol { get; set; }

        private EtlOperations factsETL;

        private string supportedDataBaseVersion = string.Empty;
        private string supportedExcelTemplateVersion = string.Empty;

        private List<CellProperties> tableCellProperties;

        #endregion

        #region Form Events

        public frmMain()
        {
            Logger.WriteLog(eSeverity.Note, "****************  Logging starts  *********************************");
            //To display Terms & Conditions dialog
            #if (FOR_NCA)
                var licenceForm = new frmTermsAndConditionsNCA();
                licenceForm.ShowDialog(this);
                _userManualFileName = "T4U User Manual Full.pdf";
            #elif  (FOR_UT)
                var licenceForm = new frmTermsAndConditionsUT();
                licenceForm.ShowDialog(this);
                _userManualFileName = "T4U User Manual Preparatory.pdf";
            #else
            #error "Compilation variable not set for FOR_NCA nor FOR_UT";
            #endif

                //frmGeneralInformation frmInf = new frmGeneralInformation();
                //frmInf.ShowDialog();
            
            SetupStaticVariables();
            InitializeComponent();

            _userExtensibility = new SolvencyIIExtensions(MEFComposed);

            SetupValidationMenuItems();
            // set up arelle executable in AppInfo/Local if missing or older than resource zip file (background process)
            ActivateXBRLMenuItems(false);
            ArelleSetup.Configure(arelle_setup_completed);
            ActivateXBRLMenuItems(true);
            //Application level settings
            #if (FOR_UT)  
                solvencyIIToolStripMenuItem.Visible = false;
                fullS2TestXBRLInstancesToolStripMenuItem.Visible = false;
                cRDIVToolStripMenuItem.Visible = false;
                dPMDictionaryToolStripMenuItem.Visible = false;
                annotatedFULLTemplatesToolStripMenuItem.Visible = false;
                solvencyIIFULLToolStripMenuItem.Visible = false;
                cDRIVToolStripMenuItem.Visible = false;
                sQLServerBackUpFullToolStripMenuItem.Visible = false;
                importDataToExcelBusinessTemplateToolStripMenuItem.Visible = false;
                downloadAnEmptyBusinessExcelTemplateToolStripMenuItem.Visible = false;
#else
            solvencyIIPreparatoryToolStripMenuItem.Visible = false;
            preparatoryS2AnnotatedTemplatesToolStripMenuItem.Visible = false;
            preparatoryS2DictionaryToolStripMenuItem.Visible = false;
            PreparatoryS2TestXBRLInstancesToolStripMenuItem.Visible = false;
            preparatoryS2ToolStripMenuItem.Visible = false;
            sQLServerBackUpsToolStripMenuItem.Visible = false;
            rCBusinessCodeMappingToolStripMenuItem.Visible = false;
#endif

            // solvencyIIFULLToolStripMenuItem.Visible = false;
            cRDIVToolStripMenuItem.Visible = false;
            cDRIVToolStripMenuItem.Visible = false;

            treeView1.MouseEnter += treeView1_MouseEnter;
            // If we have a click here whilst setting up the template do not allow it.
            treeView1.BeforeSelect += (sender, e) => { e.Cancel = !allowTreeSelection; };
            timer1.Interval = 200;
            timer1.Tick += timer1_Tick;

            //Removed the functionality - Arelle without validation by setting the menu items visibility false.
            importArelleWithoutValidationToolStripMenuItem.Visible = false;
            exportArelleWithoutValidationToolStripMenuItem.Visible = false;
            CreateTempDirectory(Path.Combine(Application.StartupPath, "Temp"));

            InitializeValidationErrorView();
            InitializeCellPropertiesView();
            InitializeValidateContainerErrorView();
            EnableRSS();
        }


        private void frmMain_Load(object sender, EventArgs e)
        {
            StartToolStripProgressBar();
            SetupForm();
            PositionForm();
            //To show whats New dialog
            bool updateFlag = false, showWhatsNew=false;            
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                updateFlag = true;
                //checking the click Once version
                if(RegSettings.ClickOnceVersion!= ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString())
                {
                    showWhatsNew=true;
                    RegSettings.ClickOnceVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                }
                //To open the Database or container by double clicking it
                var args = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData;
                string activatedDBpath = args == null || args.Length == 0 ? null : new Uri(args[0]).LocalPath;
                if (!string.IsNullOrEmpty(activatedDBpath))
                {                   
                    if (File.Exists(activatedDBpath))
                    {                        
                            if (Path.GetExtension(activatedDBpath).ToUpper().EndsWith("XBRT"))
                            if (ManageDatabases.CheckConnectionString(activatedDBpath))
                            {                               
                                StaticSettings.ConnectionString = activatedDBpath;
                                RegSettings.ConnectionString = activatedDBpath;
                                RegSettings.InstanceID = 0;
                                SetupFormForDataTier(eDataTier.SqLite);
                            }
                    }                   
                }

            }
            else
            {
                CheckForupdateForManuallyDeployed();


            }
            //Checking for the multiple instances 
            countInstances();

            //Checking the application version
            if (RegSettings.ApplicationVersion != Assembly.GetExecutingAssembly().GetName().Version.ToString())
            {
                showWhatsNew = true;
                RegSettings.ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
            //Show Whats New
            if (showWhatsNew)   
            WhatsNew.Show(CheckUpdates.CheckNow, updateFlag);

            PopulateTheFormLanguageMenuList();
            MigrateDB();
        }

        /* To check the update through the .application (Click Once's) file*/
        /// <summary>
        ///The below method is used to check for the application updates, if the application is manually copied/deployed as a standalone copy.
        ///This method checks the Clickonce’s deployed link(URL) to get the current active latest version and its checked against the standalone copy’s application version(it's be stored in the XML file)
        /// </summary>
        
        private void CheckForupdateForManuallyDeployed()
        {
            //return;           
            try
            {
                Logger.WriteLog(eSeverity.Note, "Checking for the update available in stand-alone execution");
                if (File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, "VersionDetails.xml")))
                {
                    XElement currentPublishedVersionElement = XElement.Load(Path.Combine(System.Windows.Forms.Application.StartupPath, "VersionDetails.xml"));
                    if (currentPublishedVersionElement != null)
                    {
                        var CurrentApplicationVersion_LocalElement = currentPublishedVersionElement.Element("ClickOnceVersionNumber");
                        string CurrentApplicationVersion_Local = CurrentApplicationVersion_LocalElement == null ? string.Empty : CurrentApplicationVersion_LocalElement.Value;

                        var DeployedWebLinkElement = currentPublishedVersionElement.Element("DeploymentPath");
                        string DeployedWebLink = DeployedWebLinkElement == null ? string.Empty : DeployedWebLinkElement.Value;

                        Logger.WriteLog(eSeverity.Note, "DeployedWebLink from the XML file is");
                        Logger.WriteLog(eSeverity.Note, CurrentApplicationVersion_Local);
                        Logger.WriteLog(eSeverity.Note, "CurrentApplicationVersion from the XML file is");
                        Logger.WriteLog(eSeverity.Note, DeployedWebLink);

                        var webClient = new WebClient();
                        if (!string.IsNullOrEmpty(DeployedWebLink) && !string.IsNullOrEmpty(CurrentApplicationVersion_Local))
                        {
                            string versionXML = webClient.DownloadString(DeployedWebLink);                          
                            if (versionXML != null)
                            {

                                XElement AppElement = XElement.Parse(versionXML);
                                if (AppElement != null)
                                {
                                    XElement currentWebVersion = AppElement.Element("{urn:schemas-microsoft-com:asm.v1}assemblyIdentity");
                                    if (currentWebVersion != null)
                                    {
                                        XAttribute versionAttribute = currentWebVersion.Attribute("version");
                                        if (versionAttribute != null)
                                        {
                                            string webverionNumber = versionAttribute.Value;

                                            Logger.WriteLog(eSeverity.Note, "DeployedWebLink");
                                            Logger.WriteLog(eSeverity.Note, DeployedWebLink);
                                            Logger.WriteLog(eSeverity.Note, "webverionNumber");
                                            Logger.WriteLog(eSeverity.Note, webverionNumber);

                                            if (!string.IsNullOrEmpty(webverionNumber))
                                            {
                                                Version clickverFromWeb = new Version(webverionNumber);                                                
                                                if (clickverFromWeb.CompareTo(new Version(CurrentApplicationVersion_Local)) > 0)
                                                    {
                                                        MessageBox.Show("The version you are using is not the latest one. There is a new version available. Please visit http://t4u.eurofiling.info/ for more info. Using:" + CurrentApplicationVersion_Local + " published " + webverionNumber);
                                                        Logger.WriteLog(eSeverity.Note, "its older version");
                                                    }
                                                    else
                                                    {
                                                        //its a latest copy 
                                                        Logger.WriteLog(eSeverity.Note, "its latest version");

                                                    }                                               
                                            }
                                        }
                                    }
                                }  

                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occured in the Stand-alone update checking function", ex.Message);
                Logger.WriteLog(eSeverity.Error, ex.Message);
            }

        }


        /* To read from the XML file as a backup*/
       /* private void CheckForupdateForManuallyDeployed()
        {
            //return;           
            try
            {
                Logger.WriteLog(eSeverity.Note, "Checking for the update available in stand-alone execution");
                if (File.Exists(Path.Combine(System.Windows.Forms.Application.StartupPath, "VersionDetails.xml")))
                {
                    XElement currentPublishedVersionElement = XElement.Load(Path.Combine(System.Windows.Forms.Application.StartupPath, "VersionDetails.xml"));
                    if (currentPublishedVersionElement != null)
                    {
                        var CurrentApplicationVersion_LocalElement = currentPublishedVersionElement.Element("ClickOnceVersionNumber");
                        string CurrentApplicationVersion_Local = CurrentApplicationVersion_LocalElement == null ? string.Empty : CurrentApplicationVersion_LocalElement.Value;

                        var DeployedWebLinkElement = currentPublishedVersionElement.Element("DeploymentPath");
                        string DeployedWebLink = DeployedWebLinkElement == null ? string.Empty : DeployedWebLinkElement.Value;

                        Logger.WriteLog(eSeverity.Note, "DeployedWebLink from the XML file is");
                        Logger.WriteLog(eSeverity.Note, CurrentApplicationVersion_Local);
                        Logger.WriteLog(eSeverity.Note, "CurrentApplicationVersio from the XML file is");
                        Logger.WriteLog(eSeverity.Note, DeployedWebLink);

                        var webClient = new WebClient();
                        if (!string.IsNullOrEmpty(DeployedWebLink) && !string.IsNullOrEmpty(CurrentApplicationVersion_Local))
                        {
                            string versionXML = webClient.DownloadString(DeployedWebLink);

                            if (versionXML != null)
                            {
                                string webverionNumber = string.Empty;
                                XElement versionElement = XElement.Parse(versionXML);
                                if (versionElement != null)
                                {
                                    var webverionNumberElement = versionElement.Element("CurrentActiveLatestVersion");
                                    webverionNumber = webverionNumberElement == null ? string.Empty : webverionNumberElement.Value;

                                    Logger.WriteLog(eSeverity.Note, "DeployedWebLink");
                                    Logger.WriteLog(eSeverity.Note, DeployedWebLink);
                                    Logger.WriteLog(eSeverity.Note, "webverionNumber");
                                    Logger.WriteLog(eSeverity.Note, webverionNumber);

                                    if (!string.IsNullOrEmpty(webverionNumber))
                                    {
                                        Version clickverFromWeb = new Version(webverionNumber);
                                        if (versionElement != null)
                                        {
                                            if (clickverFromWeb.CompareTo(new Version(CurrentApplicationVersion_Local)) > 0)
                                            {
                                                MessageBox.Show("The version you are using is not the latest one. There is a new version available. Please visit http://t4u.eurofiling.info/ for more info.");
                                                Logger.WriteLog(eSeverity.Note, "its older version");
                                            }
                                            else
                                            {
                                                //its a latest copy 
                                                Logger.WriteLog(eSeverity.Note, "its latest version");
                                               
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception occured in the Stand-alone update checking function", ex.Message);
                Logger.WriteLog(eSeverity.Error, ex.Message);
            }

        }*/



        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save form location
            RegSettings.FormTop = this.Top;
            RegSettings.FormLeft = this.Left;
            RegSettings.FormHeight = this.Height;
            RegSettings.FormWidth = this.Width;
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Ensure Import Export thread stopped.
            if (ImportExportArelle != null)
                ImportExportArelle.CancelArelleAsync();

            if (factsETL != null)
                factsETL.Cancel();

        }

        #endregion

        #region Validation Code

        /// <summary>
        /// To Initialize the Validation object list view 
        /// </summary>

        private void InitializeValidationErrorView()
        {
            errorView = new ObjectListView();

            OLVColumn olvSno = new OLVColumn("#", "SNO");
            OLVColumn olvScope = new OLVColumn("Scope", "Scope");
            OLVColumn olvContext = new OLVColumn("Context", "SerializedContext ");
            OLVColumn olvValidationCode = new OLVColumn("Validation Code", "ValidationCode");
            OLVColumn olvFormula = new OLVColumn("Formula", "Formula");
            OLVColumn olvCellCodes = new OLVColumn("Cell Code", "Cells");
            OLVColumn olvExpression = new OLVColumn("Expression", "Expression");
            OLVColumn olvErrorMessage = new OLVColumn("Error Message", "ErrorMessage");
            olvErrorMessage.FillsFreeSpace = true;

            HotItemStyle hotItemStyle = new HotItemStyle();
            hotItemStyle.BackColor = Color.PeachPuff;
            hotItemStyle.ForeColor = Color.MediumBlue;

            errorView.AllColumns.Add(olvScope);
            errorView.AllColumns.Add(olvValidationCode);
            errorView.AllColumns.Add(olvFormula);
            errorView.AllColumns.Add(olvCellCodes);
            errorView.AllColumns.Add(olvExpression);
            errorView.AllColumns.Add(olvErrorMessage);
            errorView.AllowColumnReorder = true;
            errorView.Columns.AddRange(new ColumnHeader[]
                                           {
                                               olvSno,
                                               olvScope,
                                               olvContext,
                                               olvValidationCode,
                                               olvFormula,
                                               olvExpression,
                                               olvErrorMessage
                                           });
            errorView.Cursor = Cursors.Default;
            errorView.Dock = DockStyle.Fill;
            errorView.FullRowSelect = true;
            errorView.GridLines = true;
            errorView.HeaderUsesThemes = false;
            errorView.HeaderWordWrap = true;
            errorView.HideSelection = false;
            errorView.HotItemStyle = hotItemStyle;
            errorView.IncludeColumnHeadersInCopy = true;
            errorView.Location = new Point(0, 0);
            errorView.Name = "objectListView";
            errorView.OwnerDraw = true;
            errorView.ShowGroups = false;
            errorView.ShowHeaderInAllViews = false;
            errorView.ShowItemToolTips = true;
            errorView.Size = new Size(681, 364);
            errorView.SortGroupItemsByPrimaryColumn = false;
            errorView.TabIndex = 0;
            errorView.UseAlternatingBackColors = true;
            errorView.UseCompatibleStateImageBehavior = false;
            errorView.UseFilterIndicator = true;
            errorView.UseFiltering = true;
            errorView.UseHotItem = true;
            errorView.View = View.Details;

            //Event handler for errorview
            /*errorView.CellClick += (s, obj) => 
            { 
                if (obj.Item == null) return;
                ShowValidationErrorCells(obj.Item.RowObject as ValidationError, null, false);
            };*/
            errorView.SelectedIndexChanged += (s, obj) =>
            {
                if (errorView.SelectedItem == null) return;
                ShowValidationErrorCells(errorView.SelectedItem.RowObject as ValidationError, null, false);
            };
            errorView.CellRightClick += errorView_CellRightClick;



            if(this.tabValidationErrorPage.Controls.Count > 0)
                this.tabValidationErrorPage.Controls.RemoveAt(0);

            this.tabValidationErrorPage.Controls.Add(errorView);

        }

        private void InitializeCellPropertiesView()
        {
            cellPropertiesView = new ObjectListView();

            //OLVColumn olvSno = new OLVColumn("#", "SNO");
            OLVColumn olvDimensionCode = new OLVColumn("Dimension Code", "DimensionCode");
            OLVColumn olvDimensionLabel = new OLVColumn("Dimension Label", "DimensionLabel");
            OLVColumn olvDimensionSignature = new OLVColumn("Signature", "DimensionMemberSignature");
            OLVColumn olvMemberLabel = new OLVColumn("Member Label", "MemberLabel");
            OLVColumn olvMemberCode = new OLVColumn("Member Code", "MemberCode");
            OLVColumn olvSource = new OLVColumn("Source", "Source");

            olvMemberLabel.FillsFreeSpace = true;

            HotItemStyle hotItemStyle = new HotItemStyle();
            hotItemStyle.BackColor = Color.PeachPuff;
            hotItemStyle.ForeColor = Color.MediumBlue;

            cellPropertiesView.AllColumns.Add(olvSource);
            cellPropertiesView.AllColumns.Add(olvDimensionSignature);
            cellPropertiesView.AllColumns.Add(olvDimensionCode);            
            cellPropertiesView.AllColumns.Add(olvDimensionLabel);
            cellPropertiesView.AllColumns.Add(olvMemberCode);
            cellPropertiesView.AllColumns.Add(olvMemberLabel);
            cellPropertiesView.AllowColumnReorder = true;
            cellPropertiesView.Columns.AddRange(new ColumnHeader[]
                                           {
                                               olvSource,
                                               olvDimensionSignature,
                                               olvDimensionCode,
                                               olvDimensionLabel,
                                               olvMemberCode,
                                               olvMemberLabel,
                                           });
            cellPropertiesView.Cursor = Cursors.Default;
            cellPropertiesView.Dock = DockStyle.Fill;
            cellPropertiesView.FullRowSelect = true;
            cellPropertiesView.GridLines = true;
            cellPropertiesView.HeaderUsesThemes = false;
            cellPropertiesView.HeaderWordWrap = true;
            cellPropertiesView.HideSelection = false;
            cellPropertiesView.HotItemStyle = hotItemStyle;
            cellPropertiesView.IncludeColumnHeadersInCopy = true;
            cellPropertiesView.Location = new Point(0, 0);
            cellPropertiesView.Name = "objectListView";
            cellPropertiesView.OwnerDraw = true;
            cellPropertiesView.ShowGroups = false;
            cellPropertiesView.ShowHeaderInAllViews = false;
            cellPropertiesView.ShowItemToolTips = true;
            cellPropertiesView.Size = new Size(681, 364);
            cellPropertiesView.SortGroupItemsByPrimaryColumn = false;
            cellPropertiesView.TabIndex = 0;
            cellPropertiesView.UseAlternatingBackColors = true;
            cellPropertiesView.UseCompatibleStateImageBehavior = false;
            cellPropertiesView.UseFilterIndicator = true;
            cellPropertiesView.UseFiltering = true;
            cellPropertiesView.UseHotItem = true;
            cellPropertiesView.View = View.Details;

            if (this.tabCellPropertiesPage.Controls.Count > 0)
                this.tabCellPropertiesPage.Controls.RemoveAt(0);

            this.tabCellPropertiesPage.Controls.Add(cellPropertiesView);

        }

        /// <summary>
        /// To Initialize the Validation object list view (For Database Validation)
        /// </summary>

        private void InitializeValidateContainerErrorView()
        {
            errorContainerValidationView = new ObjectListView();

            OLVColumn olvTableName = new OLVColumn("TableName", "TableName");
            OLVColumn olvColumnName = new OLVColumn("ColumnName", "ColumnName");
            OLVColumn olvColumnType = new OLVColumn("ColumnType", "ColumnType ");
            OLVColumn olvColumnValue = new OLVColumn("ColumnValue", "ColumnValue");
            OLVColumn olvError = new OLVColumn("Error", "Error");
            olvError.FillsFreeSpace = true;

            HotItemStyle hotItemStyle = new HotItemStyle();
            hotItemStyle.BackColor = Color.PeachPuff;
            hotItemStyle.ForeColor = Color.MediumBlue;

            errorContainerValidationView.AllColumns.Add(olvTableName);
            errorContainerValidationView.AllColumns.Add(olvColumnName);
            errorContainerValidationView.AllColumns.Add(olvColumnType);
            errorContainerValidationView.AllColumns.Add(olvColumnValue);
            errorContainerValidationView.AllColumns.Add(olvError);
            errorContainerValidationView.AllowColumnReorder = true;
            errorContainerValidationView.Columns.AddRange(new ColumnHeader[]
                                                              {
                                                                  olvTableName,
                                                                  olvColumnName,
                                                                  olvColumnType,
                                                                  olvColumnValue,
                                                                  olvError,

                                                              });
            errorContainerValidationView.Cursor = Cursors.Default;
            errorContainerValidationView.Dock = DockStyle.Fill;
            errorContainerValidationView.FullRowSelect = true;
            errorContainerValidationView.GridLines = true;
            errorContainerValidationView.HeaderUsesThemes = false;
            errorContainerValidationView.HeaderWordWrap = true;
            errorContainerValidationView.HideSelection = false;
            errorContainerValidationView.HotItemStyle = hotItemStyle;
            errorContainerValidationView.IncludeColumnHeadersInCopy = true;
            errorContainerValidationView.Location = new Point(0, 0);
            errorContainerValidationView.Name = "objectListView";
            errorContainerValidationView.OwnerDraw = true;
            errorContainerValidationView.ShowGroups = false;
            errorContainerValidationView.ShowHeaderInAllViews = false;
            errorContainerValidationView.ShowItemToolTips = true;
            errorContainerValidationView.Size = new Size(681, 364);
            errorContainerValidationView.SortGroupItemsByPrimaryColumn = false;
            errorContainerValidationView.TabIndex = 0;
            errorContainerValidationView.UseAlternatingBackColors = true;
            errorContainerValidationView.UseCompatibleStateImageBehavior = false;
            errorContainerValidationView.UseFilterIndicator = true;
            errorContainerValidationView.UseFiltering = true;
            errorContainerValidationView.UseHotItem = true;
            errorContainerValidationView.View = View.Details;

        }

        private void errorView_CellRightClick(object sender, CellRightClickEventArgs e)
        {
            if (e.Item == null)
                return;

            //Parse the table codes 
            ValidationErrorParser ep = new ValidationErrorParser(e.Item.RowObject as ValidationError);

            string[] tableCodes = ep.GetTableCodes();

            ctxErrorViewMenu = new ContextMenuStrip();

            ToolStripMenuItem errorTables = new ToolStripMenuItem("Show error cells");

            if (tableCodes != null)
            {
                foreach (string s in tableCodes)
                {
                    errorTables.DropDownItems.Add(new ToolStripMenuItem(s, null, (x, y) =>
                    {
                        ShowValidationErrorCells(e.Item.RowObject as ValidationError, x.ToString(), true);
                    }));

                }
            }

            ctxErrorViewMenu.Items.Add(errorTables);

            /*ctxErrorViewMenu.Items.Add("Show error cells", null, (s, obj) => 
            { 
                if (e.Item == null) return;
                ShowValidationErrorCells(e.Item.RowObject as ValidationError);
            });*/

            ctxErrorViewMenu.Items.Add("Properties", null, (s, obj) =>
            {
                frmValidationError dlg = new frmValidationError(e.Item.RowObject as ValidationError);
                dlg.ShowDialog();
            });



            if (ctxErrorViewMenu != null)
                ctxErrorViewMenu.Show(errorView, e.Location);




        }

        private void ShowValidationErrorCells(ValidationError ve, string overRideTableCode, bool change)
        {
            if (ve == null)
                return;

            //Parse the cell codes 
            ValidationErrorParser ep = new ValidationErrorParser(ve);

            string selectedTableCode = change ? overRideTableCode : ve.TableCode;



            //Check if the clicked error and the main control are the same.
            if (_selectedItem == null || _selectedItem.TableCode.ToUpper() != selectedTableCode.ToUpper())
            {
                GetSQLData sqlData = new GetSQLData(StaticSettings.ConnectionString);
                IEnumerable<TreeItem> treeItems = sqlData.GetTreeBranches(InstanceID);

                TreeItem item = (from t in treeItems
                                 where t.TableCode == selectedTableCode
                                 select t).FirstOrDefault();

                if (item == null)
                    return;

                _selectedItem = item;
                UpdateMainUserControl(false, false, false);

                SubscribeToShowDimensionalEvents();
            }

            string[] cellCodes = null;

            if (_selectedItem.IsTyped)
                cellCodes = ep.GetOpenCellCodes(selectedTableCode);
            else
                cellCodes = ep.GetCellCodes(selectedTableCode);



            // Change the zAxis combos first.
            List<ISolvencyPageControl> ctrl = _mainControl.GetPAGEnControls();
            if (ve.Pages != null)
            {

                foreach (Page p in ve.Pages)
                {
                    ISolvencyPageControl selectedPageCtrl = (from pc in ctrl
                                                             where pc.ColName == p.PageCode
                                                             select pc).FirstOrDefault();

                    if (selectedPageCtrl != null)
                    {
                        SolvencyComboBox cb = selectedPageCtrl as SolvencyComboBox;
                        if (cb != null && p.Member != null)
                            cb.SelectedIndex = cb.FindStringExact(p.Member.MemberLabel);

                        SolvencyTextComboBox tb = selectedPageCtrl as SolvencyTextComboBox;
                        if (tb != null)
                        {
                            int index = -1;
                            foreach (ListViewItem o in tb.Items)
                            {
                                index++;

                                if (o.Text.Trim() == p.MemberText.Trim())
                                    break;
                            }

                            tb.SelectedIndex = index;
                        }

                    }
                }

            }


            if (_selectedItem.IsTyped)
            {
                List<ISolvencyComboBox> combos = _mainOpenControl.GetPAGEnComboBoxControls();
                OpenTemplatePanelChange(true, (int) ve.PK_ID, combos);

            }

            //Get the data controls and page controls
            List<ISolvencyDataControl> dataControls = _mainControl.GetDataControls();
            if (dataControls == null)
                return;

            IList<ISolvencyDataControl> solvencyCtrls = dataControls;
            //Reset all control background color to Windows
            foreach (ISolvencyDataControl c in solvencyCtrls)
            {
                Control uc = c as Control;
                if (uc != null)
                    if(uc.Enabled) uc.BackColor = SystemColors.Window;
            }



            IEnumerable<ISolvencyDataControl> selectedCtrls = from s in solvencyCtrls
                                                              from c in cellCodes
                                                              where s.ColName.ToUpper() == c.ToUpper()
                                                              select s;

            //if (selectedCtrls.Count() < 1)
            //    continue;

            foreach (ISolvencyDataControl c in selectedCtrls)
            {
                Control uc = c as Control;
                if (uc != null)
                    uc.BackColor = Color.LightSalmon;
            }
            //}

            errorView.Select();
            errorView.SelectedItem = null;
        }


        #endregion

        #region Template Selected From Tree View + Tree view methods

        /// <summary>
        /// Tree view node selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                TreeBranch item = (TreeBranch)e.Node.Tag;
                #if !EnableMergeTemplates
                if (item.SubBranches.Count == 0)
                #else
                if (item.SubBranches.Count == 0 || (!item.SubBranches.Any(b => b.SubBranches.Count > 0) && (item.SubBranches.Count != 1)))
                #endif
                {
                    _selectedItem = item;

                    //Execute validator
                    if (validator != null)
                    {
                        validator.ValidateAsync(InstanceID, _selectedItem.TableCode);
                    }

                    // Action required
                    
                    StartToolStripProgressBar();
                    EnableAllMenuItems(false);
                    //treeView1.Enabled = false;
                    allowTreeSelection = false;
                    UpdateMainUserControl(false, false, true);

                    //Setup the dimensions display handler to show the dimensions information

                    SubscribeToShowDimensionalEvents();

                    /*if(_mainOpenControl != null)
                    {
                        List<ISolvencyDataControl> dataControls = _mainOpenControl.getd .GetDataControls();
                        _mainControlSpecialCase.SetupShowDimensionHandler(ShowDimensionsData);
                    }*/
                    
                    //To clear the status of the export & import operations, once its completed
                    lblExportImportStatus.Text = string.Empty;
                    //treeView1.Enabled = true;
                    allowTreeSelection = true;
                    EnableAllMenuItems(true);
                    StopToolStripProgressBar();

                    
                    
                }
                
            }
        }

        void SubscribeToShowDimensionalEvents()
        {
            if (_mainControl != null)
            {
                List<ISolvencyDataControl> dataControls = _mainControl.GetDataControls();
                _mainControl.SetupShowDimensionHandler(ShowDimensionsData);
            }

            if (_mainControlSpecialCase != null)
            {
                List<ISolvencyDataControl> dataControls = _mainControlSpecialCase.GetDataControls();
                _mainControlSpecialCase.SetupShowDimensionHandler(ShowDimensionsData);
            }
        }

        void ChangeNodeIcon(TreeNode child, IEnumerable<ValidationError> valError, int found)
        {
            /*TreeBranch treeBranch = (TreeBranch)child.Tag;

            if (treeBranch != null)
            {*/
            //int found = valError.Where(v => v.TableCode == treeBranch.TableCode).Count();

            if (found > 0)
            {
                child.ImageIndex = 1;
                child.SelectedImageIndex = 1;

                TreeNode parent = child.Parent;

                while (parent != null)
                {
                    parent.ImageIndex = 1;
                    parent.SelectedImageIndex = 1;

                    parent = parent.Parent;
                }
            }

            else
            {
                child.ImageIndex = 2;
                child.SelectedImageIndex = 2;
            }
            //}
        }

        void TraverseTree(TreeNode child, IEnumerable<ValidationError> valError)
        {
            TreeBranch treeBranch = (TreeBranch)child.Tag;

            int found = 0;

            if (treeBranch != null)
                found = valError.Where(v => v.TableCode == treeBranch.TableCode).Count();

            //ChangeNodeIcon(child, valError, found);

            TreeNode node = child;
            do
            {
                ChangeNodeIcon(node, valError, found);
            }
            while ((node = node.Parent) != null);


        }

        void TraverseTree(TreeNodeCollection nodes, IEnumerable<ValidationError> valError)
        {

            foreach (TreeNode child in nodes)
            {
                TreeBranch treeBranch = (TreeBranch)child.Tag;

                if (treeBranch != null)
                {
                    int found = valError.Where(v => v.TableCode == treeBranch.TableCode).Count();
                    ChangeNodeIcon(child, valError, found);

                    TraverseTree(child.Nodes, valError);
                }
            }
        }

        void treeView1_MouseEnter(object sender, EventArgs e)
        {
            if (dynamicPanel != null)
                return;
            dynamicPanel = new Panel();
            dynamicPanel.Height = (int)(3 * this.Height / 4);
            dynamicPanel.Width = splitContainer1.Panel1.Width;
            dynamicPanel.Padding = splitContainer1.Panel1.Padding;

            dynamicPanel.Top = 25;
            dynamicPanel.Left = 0;
            dynamicPanel.BackColor = splitContainer1.Panel1.BackColor;
            dynamicPanel.Controls.Add(treeView1);
            dynamicPanel.Margin = splitContainer1.Panel1.Margin;
            //splitContainer1.Panel1.Invalidate();
            //treeView1.Invalidate();

            this.Controls.Add(dynamicPanel);
            treeView1.Focus();

            splitContainer1.Panel1.Controls.Remove(treeView1);
            dynamicPanel.BringToFront();

            treeView1.MouseEnter -= treeView1_MouseEnter;
            timer1.Enabled = true;

        }

        void timer1_Tick(object sender, EventArgs e)
        {
            if (dynamicPanel != null)
            {
                Point pos = dynamicPanel.PointToClient(Cursor.Position);
                Rectangle dynamicPanelRectangle = dynamicPanel.DisplayRectangle;
                dynamicPanelRectangle.Height += 25;
                dynamicPanelRectangle.Width += 25;

                if (!dynamicPanelRectangle.Contains(pos))
                {
                    timer1.Enabled = false;
                    this.Controls.Remove(dynamicPanel);
                    dynamicPanel = null;

                    treeView1.MouseEnter += treeView1_MouseEnter;
                    splitContainer1.Panel1.Controls.Add(treeView1);
                    splitContainer1.Panel1.Invalidate();
                    treeView1.Invalidate();

                }
            }
        }

        #endregion

        #region LanguageCombo, FactCombo, ZAxisCombo and z combo positioning,

        public void LanguageChange(int languageID)
        {
            _languageID = languageID;
            UpdateMainUserControlLabels();
        }

        private void ComboBoxOnLostFocus(object sender, EventArgs eventArgs)
        {
            CheckEnableSaveCancel();
        }

        // ZAxis combos changed...
        private void ComboBoxOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            CheckEnableSaveCancel();

            if (!CheckIsDirty())
                UpdateMainUserControl(true, true, false); // Here we update the form accordingly accordingly.
            else
            {
                // The form needs to be reset
                if (sender is ISolvencyComboBox)
                {
                    ((ISolvencyComboBox)sender).SetPrevious();
                }
            }

            SubscribeToShowDimensionalEvents();
        }

        private void ComboBoxOnDropDown(object sender, EventArgs eventArgs)
        {
            using (GetSQLData getData = new GetSQLData())
            {
                _mainControl.PageCombosEnBold(getData, sender);
            }
            
        }


        #endregion

        #region Menu subs

        private void EnableExcelMenu(bool menuSetting = true)
        {
            if (!menuSetting)
            {
                excelToolStripMenuItem.Enabled = false;
                return;
            }

            if (!File.Exists(RegSettings.ConnectionString))
            {
                excelToolStripMenuItem.Enabled = false;
                return;
            }

            string connectionString = StaticSettings.ConnectionString;
            if (connectionString == null)
                excelToolStripMenuItem.Enabled = false;
            else
                excelToolStripMenuItem.Enabled = InvokeExcel.IsTemplateFileExits(connectionString, InstanceID, GetExcelTemplatePath(ExcelTemplateType.BasicTemplate)); ;

        }

        /// <summary>
        /// To Activate teh XBRL Menu Items
        /// </summary>
        /// <param name="status">Bool value for Enabled property status  </param>

        private void ActivateXBRLMenuItems(bool status)
        {
            importXBRLIntanceFileToolStripMenuItem.Enabled = status;
            exportXBRLInstanceFileToolStripMenuItem.Enabled = status;
            validateXBRLReportToolStripMenuItem.Enabled = status;

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseApplication();
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmAbout frm = new frmAbout(isArelleSetupCompleted, supportedDataBaseVersion_FULL, supportedExcelTemplateVersion_FULL,supportedDataBaseVersion_PREPARATORY,supportedExcelTemplateVersion_PREPARATORY,supportedDataBaseVersion_CRD);
            frm.ShowDialog();

        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(LanguageLabels.GetLabel(141,"To be implemented"));
        }

        #region Database Management

        /// <summary>
        /// To change the Container/Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void changeDatabaseConnectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            string newConnection = CreateConnectionString.Create();
            if (!string.IsNullOrEmpty(newConnection))
            {
                ClearCurrentControl();
                RegSettings.SQLServerConnection = newConnection;
                StaticSettings.ConnectionString = newConnection;
                SetupFormForDataTier(eDataTier.SqlServer);
            }
        }

        /// <summary>
        /// To open the Container/Database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void openDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _migrationRequired = false;
            if (ManageDatabases.LocateAndSaveDatabasePath())
            {
                ClearCurrentControl();
                GetDbVersion();
                InstanceID = 0;
                EnableMenuItems(true);
                EnableExcelMenu();
                SetFormTitle();
                CheckDBVersion();
                MigrateDB();
                InitializeValidationErrorView();
                InitializeCellPropertiesView();
            }

        }

        private void cRDIVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (ManageDatabases.CreateAndSaveDatabasePath(DbType.CRDIV, GuiSpecific.ApplicationVersion()))
            {
                ClearCurrentControl();
                GetDbVersion();
                InstanceID = 0;
                EnableMenuItems(true);
                EnableExcelMenu();
                SetFormTitle();
                InitializeValidationErrorView();
                InitializeCellPropertiesView();
            }
        }

        private void solvencyIIToolStripMenuItem_Click(object sender, EventArgs e) //cRVIVToolStripMenuItem_Click
        {
            
            if (ManageDatabases.CreateAndSaveDatabasePath(DbType.SolvencyII, GuiSpecific.ApplicationVersion()))
            {
                ClearCurrentControl();
                GetDbVersion();
                InstanceID = 0;
                EnableMenuItems(true);
                EnableExcelMenu();
                SetFormTitle();
                InitializeValidationErrorView();
                InitializeCellPropertiesView();
               
            }

        }

        private void solvencyIIPreparatoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (ManageDatabases.CreateAndSaveDatabasePath(DbType.SolvencyII_Preparatory, GuiSpecific.ApplicationVersion()))
            {
                ClearCurrentControl();
                GetDbVersion();
                InstanceID = 0;
                EnableMenuItems(true);
                EnableExcelMenu();
                SetFormTitle();
                InitializeValidationErrorView();
                InitializeCellPropertiesView();
               
            }

        }

        #endregion

        #region DLL Import  / Export and Validate

        private void xBRLToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            exportXBRLInstanceFileToolStripMenuItem.Enabled = InstanceID != 0;
        }

        private void validateXBRLReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isForMigration = false;
            ValidateXBRLAsync();
        }

        private void exportintegratedToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!CheckCurrencyProperty())
                return;

            PreParatoryVersions prepVersion = PreParatoryVersions.NotApplicable;
            prepVersion = SelectPreparatoryVersion();
            if (StaticSettings.DbType == DbType.SolvencyII_Preparatory && prepVersion == PreParatoryVersions.NotApplicable)
                return;
            _isForMigration = false;
            Logger.WriteLog(eSeverity.Note, "ETL export");
            Logger.WriteLog(eSeverity.Note, "Calling ExportDb2XBRLAsync1 method - argument type is eImportExportOperationType.Integrated");
            ExportDb2XBRLAsync1(eImportExportOperationType.Native_Export, prepVersion);
        }

        private PreParatoryVersions SelectPreparatoryVersion()
        {
            PreParatoryVersions prepVersion = PreParatoryVersions.NotApplicable;
            if (StaticSettings.DbType == DbType.SolvencyII_Preparatory)
            {

                SelectedPreparatoryVersion select = new SelectedPreparatoryVersion();
                if (select.ShowDialog(this) == DialogResult.OK)
                {
                    prepVersion = (PreParatoryVersions)Enum.Parse(typeof(PreParatoryVersions), select.selectedVersion, true);
                    return prepVersion;
                }
                else
                    return prepVersion;

            }
            return prepVersion;

        }

        private void exportarelleWithValidationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!CheckCurrencyProperty())
                return;

            //Arelle export
            PreParatoryVersions prepVersion = PreParatoryVersions.NotApplicable;
            prepVersion = SelectPreparatoryVersion();
            if (StaticSettings.DbType == DbType.SolvencyII_Preparatory && prepVersion == PreParatoryVersions.NotApplicable)
                return;
            _isForMigration = false;
            Logger.WriteLog(eSeverity.Note, "Arelle export");
            Logger.WriteLog(eSeverity.Note, "Calling ExportDb2XBRLAsync1 method - argument type is eImportExportOperationType.ArelleWith");
            ExportDb2XBRLAsync1(eImportExportOperationType.Export_using_Arelle,prepVersion);
        }

        private void exportarelleWithoutValidationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //*******************************************************************************************************************************
            //This implementation is currently not required
            //*******************************************************************************************************************************
            //_isForMigration = false;
            //Logger.WriteLog(eSeverity.Note, "Calling ExportDb2XBRLAsync1 method - argument type is eImportExportOperationType.ArelleWithout");
            //ExportDb2XBRLAsync1(eImportExportOperationType.ArelleWithout);
        }

        private void importintegratedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ETL Import
            _isForMigration = false;
            Logger.WriteLog(eSeverity.Note, "ETL Import");
            Logger.WriteLog(eSeverity.Note, "Calling ExportDb2XBRLAsync1 method - argument type is eImportExportOperationType.Integrated");
            ImportXBRL2DbAsync1(eImportExportOperationType.Native_Import);
        }

        private void importarelleWithValidationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Arelle Import
            _isForMigration = false;
            Logger.WriteLog(eSeverity.Note, "Arelle Import");
            Logger.WriteLog(eSeverity.Note, "Calling ExportDb2XBRLAsync1 method - argument type is eImportExportOperationType.ArelleWith");
            ImportXBRL2DbAsync1(eImportExportOperationType.Import_using_Arelle);
        }

        private void importarelleWithoutValidationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //*******************************************************************************************************************************
            //This implementation is currently not required
            //*******************************************************************************************************************************
            //_isForMigration = false;
            //Logger.WriteLog(eSeverity.Note, "Calling ExportDb2XBRLAsync1 method - argument type is eImportExportOperationType.ArelleWithout");
            //ImportXBRL2DbAsync1(eImportExportOperationType.ArelleWithout);
        }

        protected bool CheckCurrencyProperty()
        {
            bool result = true;
            dInstance instance;
            using (GetSQLData getData = new GetSQLData(StaticSettings.ConnectionString))
            {
                if (getData != null)
                {
                    instance = getData.GetInstanceDetails(Convert.ToInt32(InstanceID));
                    if (instance != null)
                    {
                        if (string.IsNullOrEmpty(instance.EntityCurrency))
                        {
                            MessageBox.Show("Please edit report properties and set up reporting currency, then try to export the XBRL instance", "Missing report property", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            result = false;
                        }

                    }
                    else
                        result = false;
                }
                else
                    result = false;
            }
            return result;
        }



        #endregion

        #region Language - Taxonomy NOT FormLanguage - There are two.

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PopulateTheLanguageMenuList();
            //PopulateTheFormLanguageMenuList();


#if NAJ
            PopulateTheFormLanguageMenuList();
#endif


        }

        private void PopulateTheLanguageMenuList()
        {
            var items = ComboBoxConfig.PopulateLanguages(LanguageItemClick);
            if (items.Length > 0)
            {
                formlanguageToolStripMenuItem.DropDownItems.Clear();
                formlanguageToolStripMenuItem.DropDownItems.AddRange(items);
                SelectLanguage(LanguageID);
            }
        }
        private void PopulateTheFormLanguageMenuList()
        {
            var items = ComboBoxConfig.PopulateFormLanguages(FormLanguageItemClick);
            if (items.Length > 0)
            {
                applicationLanguageToolStripMenuItem.DropDownItems.Clear();
                applicationLanguageToolStripMenuItem.DropDownItems.AddRange(items);
                SelectFormLanguage((int)StaticSettings.FormLanguage);
            }
        }

        private void LanguageItemClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem)sender;
                // Deselect all checked languages then ensure this one is selected
                LanguageID = int.Parse(item.Name);
                SelectLanguage(LanguageID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void FormLanguageItemClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem)sender;
                int formLanguage = int.Parse(item.Name);
                StaticSettings.FormLanguage = (eLanguageID) formLanguage;
                RegSettings.FormLanguage = formLanguage;
                if (StaticSettings.FormLanguage != eLanguageID.InEnglish)
                    MessageBox.Show("The application is currently only supported in English. However an automatic translation has being performed for testing purposes of the translation capabilities. Please note that labels are translated labels could be wrong or misleading as the proper translation will be done in a later stage when all labels are known.");

                SelectFormLanguage(formLanguage);
                SetupLanguage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SelectLanguage(int languageID)
        {
            foreach (ToolStripMenuItem item in formlanguageToolStripMenuItem.DropDownItems)
            {
                item.Checked = (int.Parse(item.Name) == languageID);
            }
        }

        private void SelectFormLanguage(int languageID)
        {
            foreach (ToolStripMenuItem item in applicationLanguageToolStripMenuItem.DropDownItems)
            {
                item.Checked = (int.Parse(item.Name) == languageID);
            }
        }

        #endregion

        #region Validation Menu Items

        private void SetupValidationMenuItems()
        {
            TextBox txtRemoteValidation = new TextBox();
            txtRemoteValidation.LostFocus += new EventHandler(txtRemoteValidation_LostFocus);
            txtRemoteValidation.Width = 100;
            txtRemoteValidation.AutoSize = false;

            string url = RegSettings.RemoteValidationURL;
            if (string.IsNullOrEmpty(url)) url = "localhost:8080";

            txtRemoteValidation.Text = url;

            ToolStripControlHost host = new ToolStripControlHost(txtRemoteValidation);
            host.DisplayStyle = ToolStripItemDisplayStyle.None;
            host.Width = 150;
            host.AutoSize = false;

            remoteValidationToolStripMenuItem.DropDownItems.Add(host);

            bool localVal = RegSettings.LocalValidation;
            localValidationToolStripMenuItem.Checked = localVal;
            remoteValidationToolStripMenuItem.Checked = !localVal;

        }

        private void txtRemoteValidation_LostFocus(object sender, EventArgs e)
        {
            string remURL = ((TextBox) sender).Text;
            RegSettings.RemoteValidationURL = remURL.Trim();
        }

        private void localValidationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remoteValidationToolStripMenuItem.Checked = false;
            localValidationToolStripMenuItem.Checked = true;
            RegSettings.LocalValidation = true;
        }

        private void remoteValidationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            remoteValidationToolStripMenuItem.Checked = true;
            localValidationToolStripMenuItem.Checked = false;
            RegSettings.LocalValidation = false;
        }

        #endregion

        #region Reports Menu Popout

        private void reportToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            PopulateTheReportMenuList();
        }

        private void PopulateTheReportMenuList()
        {
            var items = ComboBoxConfig.PopulateInstance(ReportItemClick);
            bool zeroRecords = items.Length == 0;
            bool zeroInstanceID = InstanceID == 0;
            changeActiveReportToolStripMenuItem.Enabled = !zeroRecords && !zeroInstanceID;
            deleteActiveReportToolStripMenuItem.Enabled = !zeroRecords && !zeroInstanceID;
            closeActiveReportToolStripMenuItem.Enabled = !zeroRecords && !zeroInstanceID;
            selectActiveReportIIToolStripMenuItem.Enabled = !zeroRecords;

            if (items.Length > 0)
            {
                selectActiveReportIIToolStripMenuItem.DropDownItems.Clear();
                selectActiveReportIIToolStripMenuItem.DropDownItems.AddRange(items);
            }
            if (InstanceID > 0)
            {
                foreach (ToolStripMenuItem item1 in selectActiveReportIIToolStripMenuItem.DropDownItems)
                {
                    string[] ids = item1.Name.Split('|');
                    int instanceID = int.Parse(ids[0]);
                    if (instanceID == InstanceID)
                    {
                        item1.Checked = true;

                    }
                }

            }
        }

        private void ReportItemClick(object sender, EventArgs e)
        {
            try
            {
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                item.Checked = true;
                string[] ids = item.Name.Split('|');
                int instanceID = int.Parse(ids[0]);
                if (instanceID != 0)
                {   
                    InstanceID = instanceID;
                    InitializeValidationErrorView();
                    InitializeCellPropertiesView();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void closeActiveReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InstanceID = 0;
            EnableExcelMenu();
            InitializeValidationErrorView();
            InitializeCellPropertiesView();
        }

        private void createANewReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInstanceNew select = new frmInstanceNew(true);
            if(select.ShowDialog(this) == DialogResult.OK)
            {
                InstanceID = select.InstanceID;
                InitializeValidationErrorView();
                InitializeCellPropertiesView();
            }
            select.Dispose();
        }

        private void changeActiveReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form select = new frmInstanceNew(false, _instanceID);
            if (select.ShowDialog(this) == DialogResult.OK)
            {
                // Refresh this instance
                InstanceID = _instanceID;
            }
            select.Dispose();
        }

        private void deleteActiveReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(LanguageLabels.GetLabel(29,"Are you sure you want to delete this report?\n\nOnce deleted it cannot be restored."), LanguageLabels.GetLabel(30, "Deletion Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                PutSQLData putData = new PutSQLData();
                putData.DeleteInstance(InstanceID);
                putData.Dispose();
                InstanceID = 0;
                ClearForm();
                EnableExcelMenu();
                InitializeValidationErrorView();
                InitializeCellPropertiesView();
            }
        }


        #endregion

        #region Recent Files Menu Popout

        private void fileToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            PopulareTheRecentFilesMenuList();
        }

        private void PopulareTheRecentFilesMenuList()
        {
            var items = ComboBoxConfig.PopulateRecentFiles(RecentItemsClick);
            recentFilesToolStripMenuItem.Enabled = items.Length != 0;
            recentFilesToolStripMenuItem.DropDownItems.Clear();
            recentFilesToolStripMenuItem.DropDownItems.AddRange(items);
        }

        private void RecentItemsClick(object sender, EventArgs e)
        {
            ClearCurrentControl();
            try
            {
                ToolStripItem item = (ToolStripItem) sender;
                string connectionString = item.Name;
                ManageDatabases.ChangeDatabase(connectionString);
                GetDbVersion();
                InstanceID = 0;
                EnableMenuItems(true);
                EnableExcelMenu();
                SetFormTitle();
                InitializeValidationErrorView();
                InitializeCellPropertiesView();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        #endregion

        #region SQL Server Entries

        private void sQLiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetupFormDataTypeChanged(eDataTier.SqLite);
            SetupFormForDataTier(eDataTier.SqLite);
        }

        private void sQLServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // MessageBox.Show(LanguageLabels.GetLabel(145,"Please note that this option is not working yet"));
            SetupFormDataTypeChanged(eDataTier.SqlServer);
            SetupFormForDataTier(eDataTier.SqlServer);
        }

        private void SetupFormDataTypeChanged(eDataTier dataType)
        {
            RegSettings.DataTier = dataType;
            StaticSettings.DataTier = dataType;
            StaticSettings.ConnectionString = "";
            RegSettings.InstanceID = 0; // => Clears the form
            InstanceID = 0;

        }

        #endregion

        #region Migration

        /// <summary>
        /// To migarte the Preparatory containers based upon the container supported versions.
        /// </summary>

        protected void MigrateDB()
        {
            if (StaticSettings.DbType == DbType.SolvencyII_Preparatory && _migrationRequired == true)
            {
                DialogResult dialogResult = MessageBox.Show("You are trying to open a container created using previous version of the tool. Would you like to migrate the container to the new version? Note that it may take some time depending on the amount of data.", "Migration", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    return;


                _isForMigration = true;
                StartBusyIndicator();
                string migrationFolder = Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp");
                migrationFolder = Path.Combine(migrationFolder, "Migration");
                if (!Directory.Exists(migrationFolder))
                    Directory.CreateDirectory(migrationFolder);
                else
                {
                    var dir = new DirectoryInfo(migrationFolder);
                    dir.Delete(true);
                    Directory.CreateDirectory(migrationFolder);
                }


                bool isDbCreated = CreateDataBaseForMigration();
                if (Directory.Exists(migrationFolder))
                {
                    //export
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.DoWork += new DoWorkEventHandler(backgroundWorkerExport_DoWork);
                    worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerExport_RunWorkerCompleted);
                    worker.RunWorkerAsync();
                }
                else
                {
                    MessageBox.Show("Error in creating Migration folder");
                    StopBusyIndicator();
                    return;
                }
            }
           

        }

        /// <summary>
        /// To export all the instances  to XBRL files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerExport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ExportAllXBRLInstances();

        }

        /// <summary>
        /// To import all the exported XBRL files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorkerExport_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            //Start importing
            ImportAllinstancesWorker();
           

        }

        /// <summary>
        /// Creation of new supported conatiner/data base 
        /// </summary>
        /// <returns></returns>
        private bool CreateDataBaseForMigration()
        {
            string folderpath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp");
            folderpath = Path.Combine(folderpath, "Migration");
            if (!Directory.Exists(folderpath))
            {
                //throw errorContainerValidationView
                MessageBox.Show("Migration DB folder not created");
                return false;
            }
            string DBpath = Path.Combine(folderpath, "Migration.xbrt");
            string sourceFile = Path.Combine(Application.StartupPath, "T4Udb_Sol2_prep.zip");
            if (File.Exists(DBpath)) File.Delete(DBpath);
            using (ZipFile zip = ZipFile.Read(sourceFile))
                foreach (ZipEntry entry in zip.EntriesSorted)
                {
                    entry.FileName = Path.GetFileName(DBpath);
                    entry.Extract(System.IO.Path.GetDirectoryName(DBpath));
                }

            if (File.Exists(DBpath))
                return true;
            else
                return false;
           


        }

        /// <summary>
        /// On Migration complete event - set the new migrated DB  as connected with T4U    
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       
        private void backgroundWorkerimport_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            StopBusyIndicator();
            string dbName = "SolvencyII_Preparatory";
             SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = string.Format("{0} (*.xbrt)|*.xbrt", dbName);           
            saveFileDialog1.Title = "Specify the db location";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string folderpath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp");
                folderpath = Path.Combine(folderpath, "Migration");               
                string DBpath = Path.Combine(folderpath, "Migration.xbrt");

                string destLocation = saveFileDialog1.FileName;
                if (File.Exists(DBpath))
                {
                    if (File.Exists(destLocation))                    
                        File.Delete(destLocation);
                    
                    File.Copy(DBpath, destLocation); 
                }

                if (File.Exists(destLocation))
                {

                    if (ManageDatabases.ChangeDatabaseAfterMigration(destLocation))
                    {
                        ClearCurrentControl();
                        GetDbVersion();
                        InstanceID = 0;
                        EnableMenuItems(true);
                        EnableExcelMenu();
                        SetFormTitle();
                        CheckDBVersion();
                        MessageBox.Show("Migration successful.");
                        
                    }
                }

                
            }
            _isForMigration = false;
           
        }

        /// <summary>
        /// ImportAllinstancesWorker (BackgroundWorker thread) importes all the exported instances into the newly created supported conatiner
        /// </summary>

        public void ImportAllinstancesWorker()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(backgroundWorkerImport_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerimport_RunWorkerCompleted);
            worker.RunWorkerAsync();
        }

        /// <summary>
        /// Event : Import the migrated XBRL files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void backgroundWorkerImport_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ImportMigreatedXBRLInstances();

        }

       
        /// <summary>
        /// ImportMigreatedXBRLInstances importes all the exported instances into the newly created supported conatiner
        /// </summary>
        private void ImportMigreatedXBRLInstances()
        {
            
            string migrationFolder = Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp");
            migrationFolder = Path.Combine(migrationFolder, "Migration");
            string DBpath = Path.Combine(migrationFolder, "Migration.xbrt");
            //Seeting the latest application Version
            using (GetSQLData getData = new GetSQLData(DBpath))
            {
                //DbType.SolvencyII_Preparatory, GuiSpecific.ApplicationVersion()
                getData.SetApplicationData(GuiSpecific.ApplicationVersion(), DbType.SolvencyII_Preparatory.ToString());
            }


            string[] files = Directory.GetFiles(migrationFolder, "*.XBRL", SearchOption.AllDirectories);

            foreach (string s in files)
            {
               
                if (ImportExportArelle == null)      
                     ImportExportArelle = new ArelleCmdInterface("Loading instance - ");
                ProgressHandler.Progress += ProgressHandler_Progress;
                string sourceXBRL = s;
                //importedFileMigration = sourceXBRL;
                //importedFileMigration = Path.GetFileName(sourceXBRL);
                importedFileMigration = Path.GetFileName(sourceXBRL);
                importedFileMigration_InstanceName = Path.GetFileNameWithoutExtension(sourceXBRL);
                
               ImportExportArelle.ParseInstanceIntoDatabase(eImportExportOperationType.Native_Import, sourceXBRL, ArelleProgress, ImportXBRL2DbArelleComplete_migration, DBpath);
               while (ImportExportArelle != null)
               {
                   //to make the thread to wait................................
               }                
            }
            ProgressHandler.Progress -= ProgressHandler_Progress;


        }

      
      
        private void ImportXBRL2DbArelleComplete_migration(object s, RunWorkerCompletedEventArgs args)
        {

            string report = string.Empty;
            eImportExportOperationType operation_type = eImportExportOperationType.No_Operation_selected;
            if (args.Result is Object[]) 
            {
                object[] resultParams = args.Result as Object[];
                if (resultParams[0] != null)
                    report = resultParams[0].ToString();
                if (resultParams[1] != null)
                {
                    //To get the operation type
                    string value = resultParams[1].ToString();
                    operation_type = (eImportExportOperationType)Enum.Parse(typeof(eImportExportOperationType), value, true);
                }


            }

            string folderpathtest = Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp");
            folderpathtest = Path.Combine(folderpathtest, "Migration");
            string DBpath = Path.Combine(folderpathtest, "Migration.xbrt");
            dInstance instance;
            using (GetSQLData getData = new GetSQLData(DBpath))
            {
                instance = getData.GetInstanceDetails(importedFileMigration);
            }

            //loading the insatnce
            long newInstanceID;            
            string entityName = "un-named";
            if (!string.IsNullOrEmpty(importedFileMigration_InstanceName))
            {
                var val = importedFileMigration_InstanceName.Split('-')[0];
                if (!string.IsNullOrEmpty(val))
                {
                    entityName = val;
                }
                else
                    entityName = importedFileMigration_InstanceName;

            }
            instance.EntityName = entityName;
            //instance.FileName = importedFileMigration;
            if (instance != null)
            {
                PutSQLData putData = new PutSQLData(DBpath);
                //to show validation error details              

                IValidationQuery validationQuery = ValidationFactory.GetValidationQuery(eDataTier.SqLite);
                ISolvencyData conn = ConnectionFactory.GetConnection(eDataTier.SqLite, DBpath);
                //SolvencyII.Validation.GetMessage getMessage = new SolvencyII.Validation.GetMessage(StaticSettings.ConnectionString);
                IEnumerable<ValidationMessage> messages = validationQuery.GetArelleValidationErrors(conn, instance.InstanceID);

                //Show only if there are any errors
                //if (messages != null)
                if (messages != null && messages.Count() > 0)
                {
                    ArelleValidationStatus statusDlg = null;
                    if (operation_type.Equals(eImportExportOperationType.Native_Import))
                    {
                        statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Arelle_results_for_native_import, true, entityName);
                        statusDlg.ShowDialog();
                    }
                }



                string result = putData.InsertUpdateInstance(instance, out newInstanceID);
                factsETL = new EtlOperations();

                factsETL.etlLoadingXBRLinstance(DBpath, Convert.ToInt32(instance.InstanceID));
            }

           
            ImportExportArelle = null;





        }
       


        /// <summary>
        /// This method iterates all the instances in the container  and creates the XBRL files for all the  instances
        /// </summary>

        private void ExportAllXBRLInstances()
        {
            string folderpath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp");
            folderpath = Path.Combine(folderpath, "Migration");
            if (!Directory.Exists(folderpath))
            {
                //throw error
                MessageBox.Show("Migration DB folder not created");
                return;
            }

            GetSQLData sqlData = new GetSQLData(StaticSettings.ConnectionString);
            List<Instance> lst = sqlData.GetAllInstanceDetails();
            ImportExportArelle = new ArelleCmdInterface("Saving instance - ");
            factsETL = new EtlOperations();
            //To test versions
           
            
            //looping all the instances
            foreach (Instance entry in lst)
            {
                if (ImportExportArelle == null)                
                    ImportExportArelle = new ArelleCmdInterface("Saving instance - ");
                ProgressHandler.Progress += ProgressHandler_Progress;
                factsETL.etlSavingXBRLinstance(StaticSettings.ConnectionString, Convert.ToInt32(entry.InstanceID));
                Logger.WriteLog(eSeverity.Note, "Invoking SaveFromDatabaseToInstance.......");
                ImportExportArelle.SaveFromDatabaseToInstance(eImportExportOperationType.Native_Export, Convert.ToInt32(entry.InstanceID), Path.Combine(folderpath, entry.Text + ".xbrl"), ArelleProgress, ExportArelleComplete, prepVersion);
                //Wait the Thread to complete the operation
                while (ImportExportArelle != null)
                {
                   
                }
            }
            ProgressHandler.Progress -= ProgressHandler_Progress;

        }


        #endregion

        #region To Be Implemented

        private void validateActiveTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Validation for table is invoked while cliking tree and save button. This option will be removed from the menu.");
        }

        /// <summary>
        /// To validate the current report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void validateCurrentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (InstanceID != 0)
            {
                //Show message box
                MessageBox.Show("Please note that the following validation results do not replace full XBRL validation. They are based on the T4U’s database and have been included to provide fast, user-friendly feedback. Use Arelle or other XBRL validator to check full XBRL Taxonomy compliance. More information is available in the T4U’s documentation package.", 
                    "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                validator.ValidateAsync(InstanceID);
            }
            else
            {
                MessageBox.Show("Please select the report");
            }
        }

        /// <summary>
        /// The method/event is to validate active report - This feature validates the entered value against the DPM (Data Point Model) data type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void validateCurrentContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //_migrationRequired = true;
            //MigrateDB();
            //return;

            // Invoke the validate active container logic any one of the instance selected            
            if (InstanceID != 0)
            {

                //Initialize the Validation result list 
                dataTypeAllValidationResultsList = new List<DataTypeValidationResult>();
                //Initialize the parameters, required to validate active container
                DataTypeValidationInput dataTypeValidationInput = new DataTypeValidationInput
                {
                    ConnectionString = StaticSettings.ConnectionString,
                    InstanceID = InstanceID,
                    UpdateValidationResults = UpdateValidationResults,
                    ProgressChanges = ProgressChanges,
                    Completed = DataTypeValidationCompleted

                };
                //Initialize the dataTypeValidationManager
                DataTypeValidationManager dataTypeValidationManager = new DataTypeValidationManager();
                //Subscribe to the result update event
                dataTypeValidationManager.UpdateResult += ValidateActiveContainerResult;
                //Invoking the validate active container method
                dataTypeValidationManager.ValidateDB(dataTypeValidationInput);
               
            }
            else
            {
                MessageBox.Show("Please select the report");
            }
            
           

        }
        public void ValidateActiveContainerResult(DataTypeValidationInput validatedResulSet, bool isFaulted)
        {
            if (validatedResulSet != null && isFaulted == false)
            {
                //this.splitContainer1.Panel2.Controls.RemoveAt(0);
                //this.splitContainer1.Panel2.Controls.Add(errorContainerValidationView);
                this.tabContainerValidationPage.Controls.Add(errorContainerValidationView);
                errorContainerValidationView.SetObjects(validatedResulSet.resultSet);
            }
        }

       public void  UserControled(object sender, EventArgs e)
        {

        }

       private void exportDataToBusinessTemplateToolStripMenuItem_Click(object sender, EventArgs e)
       {
           CultureInfo.CurrentCulture.ClearCachedData();
           string connectionString = StaticSettings.ConnectionString;

#if (FOR_UT)  
           //supportedExcelTemplateVersion = supportedExcelTemplateVersion_PREPARATORY;
           CultureInfo.CurrentCulture.ClearCachedData();
           const string fileName = "Exl-Business_Encrypted.xlsx";
           string sourceFile = string.Empty;
           sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Concat("Exl-Business_Encrypted\\", fileName));
           if (!File.Exists(sourceFile))
           {
               MessageBox.Show(string.Format("{0}\n{1}", "Unfortunately a Excel-Business-Template file is missing: ", sourceFile));
               return;
           }

           InvokeExcel.ExportToExcel(connectionString, InstanceID, GetExcelTemplatePath(ExcelTemplateType.BasicTemplate), ExcelTemplateType.BusinessTemplate_Macro, sourceFile);
#else
            InvokeExcel.ExportToExcel(connectionString, InstanceID, GetExcelTemplatePath(ExcelTemplateType.BusinessTemplate), ExcelTemplateType.BusinessTemplate);
#endif

       }
  

        #endregion



        #endregion

        #region Data functions

        /// <summary>
        /// The templates are setup with this method.
        /// ClosedTableManager.Selected sets up all nPage combos etc and raises ClosedTableManager.PopulateDataForDataRepeater
        /// which intern creates the queries to send through to the template.
        /// </summary>
        /// <param name="comboUpdate"></param>
        /// <param name="selectedItem"></param>
        /// <param name="setupNPageFirstEntries"></param>
        private void SelectUserControlData(bool comboUpdate, TreeItem selectedItem, bool setupNPageFirstEntries)
        {
            try
            {
                ClosedTableManager tableManager;
                if (_mainControl != null)
                {
                    tableManager = new ClosedTableManager(_mainControl, LanguageID, InstanceID);
                    tableManager.Selected(ComboBoxOnSelectedIndexChanged, ComboBoxOnLostFocus, comboUpdate, ComboBoxOnDropDown, selectedItem, MessageBoxQuestion.ShowMessageBoxAnsYes, setupNPageFirstEntries, MessageBoxQuestion.AlertMessageBox);
                    _mainControl.GetUserComboBoxControls().ClearGreyed();
                }
                else
                {
                    tableManager = new ClosedTableManager(_mainControlSpecialCase, LanguageID, InstanceID);
                    tableManager.Selected(ComboBoxOnSelectedIndexChanged, ComboBoxOnLostFocus, comboUpdate, ComboBoxOnDropDown, selectedItem, MessageBoxQuestion.ShowMessageBoxAnsYes, setupNPageFirstEntries, MessageBoxQuestion.AlertMessageBox);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLog(eSeverity.Error, ex.Message);
                MessageBox.Show(string.Format(LanguageLabels.GetLabel(142, "Unfortunately there was an error.\nPlease try again.\n\n{0} "), ex.Message), "Error");
            }
        }  

        /// <summary>
        /// Passed as a delegate to the ParentUserControl the save button raises this event.
        /// The ClosedTableManager.Save will gather up required nPage data and calls the control's UpdateData.
        /// </summary>
        /// <param name="ctrl"></param>
        public void SaveUserControlData(IUserControlBase ctrl)
        {
            if (_selectedItem.IsTyped)
            {
                // Check the primary key values.
                string rowKeysCheck = _mainControl.RowKeyCheck();
                if (!string.IsNullOrEmpty(rowKeysCheck))
                {
                    // Report to the user.
                    MessageBox.Show(rowKeysCheck);
                    return; // leave the method.
                }
            }
            ClosedTableManager tableManager = null;
            try
            {
                //tableManager = new ClosedTableManager(_mainControl, LanguageID, InstanceID);
                tableManager = new ClosedTableManager(ctrl, LanguageID, InstanceID);
                tableManager.ShowToolBar += StatusBarUpdate;
                tableManager.ShowMessageBox += response => MessageBox.Show(response);
                if (tableManager.Save(_selectedItem))
                {
                    ctrl.IsDirty = false;
                    if (_selectedItem.IsTyped)
                    {
                        // Open table single row saved.  
                        // Reset the cache and refresh the form.
                        _mainOpenControl.RefreshGrid();
                    }
                }
                //Execute validation each time when user presses the save button
                if (validator != null)
                {
                    validator.ValidateAsync(InstanceID, _selectedItem.TableCode);
                }
            }
            catch (Exception ex)
            {
                if (_selectedItem.IsTyped)
                {
                    if (tableManager != null)
                    {
                        // We have a failure that needs to have the right user feedback;
                        // which of the fields is incorrect.
                        List<OpenColInfo2> cols = _mainOpenControl.GetRowKeyCols();

                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine("This record does not have a unique identity. The identity is made up from the following column(s):");
                        foreach (OpenColInfo2 openColInfo2 in cols)
                        {
                            sb.AppendLine(string.Format("{0} - {1}", openColInfo2.OrdinateCode, openColInfo2.Label));
                        }
                        MessageBox.Show(string.Format("{0}\n\rPlease update and try again. ", sb.ToString()), "Error");
                    }
                }
                else
                {
                    Logger.WriteLog(eSeverity.Error, ex.Message);
                    MessageBox.Show(string.Format("Unfortunately there was an error.\nPlease try again.\n\n{0} ", ex.Message), "Error");
                }
            }
        }

        public void DeleteUserControlData(IUserControlBase ctrl)
        {
            try
            {
                ClosedTableManager tableManager = new ClosedTableManager(_mainControl, LanguageID, InstanceID);
                tableManager.ShowToolBar += StatusBarUpdate;
                tableManager.ShowMessageBox += response => MessageBox.Show(response);
                tableManager.Delete(_selectedItem, true);
                _mainControl.IsDirty = false;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(eSeverity.Error, ex.Message);
                MessageBox.Show(string.Format("Unfortunately there was an error.\nPlease try again.\n\n{0} ", ex.Message), "Error");
            }
            
            // Refresh the form now that the data has been removed
            if (!_selectedItem.IsTyped)
            {
                UpdateMainUserControl(false, false, false);

                SubscribeToShowDimensionalEvents();
            }
            else
            {
                OpenTemplatePanelChange(false, 0, null);
                // Reset the cache and refresh the form.
                _mainOpenControl.RefreshGrid();

            }
        }
                
        public void DeleteSelectedItemData_AllVariants()
        {
            using (PutSQLData putData = new PutSQLData())
            {
                putData.DeleteTableData_AllVariants(_selectedItem.TableID, InstanceID);
            }
            if(_selectedItem.IsTyped)
                _mainOpenControl.RefreshGrid();
        }

        public void ToggleFilingIndicator(bool newValue)
        {
            try
            {
                ClosedTableManager tableManager;
                if(_mainControl != null)
                    tableManager = new ClosedTableManager(_mainControl, LanguageID, InstanceID);
                else
                    tableManager = new ClosedTableManager(_mainControlSpecialCase, LanguageID, InstanceID);
                tableManager.ShowToolBar += StatusBarUpdate;
                tableManager.ShowMessageBox += response => MessageBox.Show(response);
                tableManager.Filed_Toggle(_selectedItem);
                // If toggling to Not Reported delete all data;
                if(!newValue)
                    DeleteSelectedItemData_AllVariants();

            }
            catch (Exception ex)
            {
                Logger.WriteLog(eSeverity.Error, ex.Message);
                MessageBox.Show(string.Format("Unfortunately there was an error.\nPlease try again.\n\n{0} ", ex.Message), "Error");
            }


        }

        public void CancelUserControlData()
        {
            if (!CheckIsDirty())
            {
                if (!_selectedItem.IsTyped)
                {
                    // Refresh the form now that the changes are to be removed
                    UpdateMainUserControl(false, true, false);

                    SubscribeToShowDimensionalEvents();
                }
                else
                {
                    OpenTemplatePanelChange(false, 0, null);

                    // Reset the cache and refresh the form.
                    _mainOpenControl.RefreshGrid();


                }
            }
        }
        #endregion

        #region Helper functions

        private void SetupStaticVariables()
        {

            StaticSettings.ApplicationID = 1; // Setup for windows

            // Get the users language
            int result = RegSettings.FormLanguage;

            // Setting default language
            eLanguageID regLanguageID = eLanguageID.InEnglish;

            if (result != -1)
            {
                regLanguageID = (eLanguageID)result;
            }
            StaticSettings.FormLanguage = regLanguageID;
            StaticSettings.SolvencyIITemplateDBConnectionString = ManageDatabases.GetTemplateDBConnectionStringForSolvencyII();

            if (StaticSettings.FormLanguage != eLanguageID.InEnglish)
                MessageBox.Show("The application is currently only supported in English. However an automatic translation has being performed for testing purposes of the translation capabilities. Please note that labels are translated labels could be wrong or misleading as the proper translation will be done in a later stage when all labels are known.");
            StaticSettings.TestingMode = RegSettings.TestingMode;

        }

        private void PositionForm()
        {

            Top = RegSettings.FormTop;
            Left = RegSettings.FormLeft;
            int formDim = RegSettings.FormHeight;
            if (formDim > 0) Height = RegSettings.FormHeight;
            formDim = RegSettings.FormWidth;
            if (formDim > 0) Width = RegSettings.FormWidth;
        }

        private void SetupForm()
        {
            Cursor.Current = Cursors.WaitCursor;

            // Check the database type, path etc and configure the data access.
            if (!SetupFormForDataTier(RegSettings.DataTier))
            {
                // Close down the application
                MessageBox.Show("Unable to make a database connection so the application is closing.\nPlease make sure the database management system is running properly (that SQL Server is operational and accessable).\nTo access the program immediately try changing the database connection for SQL Server or revert back to SQLite access.", "Connection Error");
                this.Close();
                Application.Exit();
                return;
            }

            // Setup the language
            _languageID = RegSettings.LanguageID;
            StaticSettings.LanguageID = _languageID;

            // Setup the tool tips
            StaticUISettings.MainToolTips = toolTip1;

            // Retrieve local variables from the config file where appropriate.
            InstanceID = RegSettings.InstanceID; // Contains refresh for whole form; RefreshInstanceChanged();

            splitContainer2.Panel2.AutoScroll = true;

            if (!StaticSettings.TestingMode)
            {
                SetupLanguage();
            }


            Cursor.Current = Cursors.Default;
        }

        private void SetupLanguage()
        {
            // StaticSettings.FormLanguage = eLanguageID.InPolish;
            try
            {

                fileToolStripMenuItem.Text = LanguageLabels.GetLabel(1, "Multi report container");
                createXbrtToolStripMenuItem.Text = LanguageLabels.GetLabel(2, "Create a new multi report container");
                openDatabaseToolStripMenuItem.Text = LanguageLabels.GetLabel(3, "Open multi report container");
                recentFilesToolStripMenuItem.Text = LanguageLabels.GetLabel(4, "Recent files");
                exitToolStripMenuItem.Text = LanguageLabels.GetLabel(5, "Exit");

                reportToolStripMenuItem.Text = LanguageLabels.GetLabel(6, "Report");
                createANewReportToolStripMenuItem.Text = LanguageLabels.GetLabel(7, "Create a new report");
                changeActiveReportToolStripMenuItem.Text = LanguageLabels.GetLabel(8, "Edit an active report");
                deleteActiveReportToolStripMenuItem.Text = LanguageLabels.GetLabel(9, "Delete an active report");
                closeActiveReportToolStripMenuItem.Text = LanguageLabels.GetLabel(10, "Close an active report");
                selectActiveReportIIToolStripMenuItem.Text = LanguageLabels.GetLabel(11, "Select a report");

                // xBRLToolStripMenuItem.Text = LanguageLabels.GetLabel();
                importXBRLIntanceFileToolStripMenuItem.Text = LanguageLabels.GetLabel(12, "Import XBRL instance file");
                exportXBRLInstanceFileToolStripMenuItem.Text = LanguageLabels.GetLabel(13, "Export XBRL instance file");
                validateXBRLReportToolStripMenuItem.Text = LanguageLabels.GetLabel(14, "Validate XBRL report");

                settingsToolStripMenuItem.Text = LanguageLabels.GetLabel(15, "Settings");
                formlanguageToolStripMenuItem.Text = LanguageLabels.GetLabel(16, "Language");
                applicationLanguageToolStripMenuItem.Text = "Form " + LanguageLabels.GetLabel(16, "Language");

                aboutToolStripMenuItem.Text = LanguageLabels.GetLabel(17, "About");
                localValidationToolStripMenuItem.Text = LanguageLabels.GetLabel(18, "Local Validation");
                remoteValidationToolStripMenuItem.Text = LanguageLabels.GetLabel(19, "Remote Validation");


                applicationLanguageToolStripMenuItem.Text = LanguageLabels.GetLabel(104, "Application language");
                formlanguageToolStripMenuItem.Text = LanguageLabels.GetLabel(105, "Form language");

                databaseTypeToolStripMenuItem.Text = LanguageLabels.GetLabel(138, "Database Type");
                sQLiteToolStripMenuItem.Text = LanguageLabels.GetLabel(139, "SQLite");
                sQLServerToolStripMenuItem.Text = LanguageLabels.GetLabel(140, "SQL Server");

                // Export
                integratedToolStripMenuItem1.Text = LanguageLabels.GetLabel(106, "Native export");
                arelleWithValidationToolStripMenuItem1.Text = LanguageLabels.GetLabel(107, "Export using Arelle (with validation)");
                exportArelleWithoutValidationToolStripMenuItem.Text = LanguageLabels.GetLabel(108, "Export using Arelle (without validation)");

                // Import
                integratedToolStripMenuItem.Text = LanguageLabels.GetLabel(109, "Native import");
                arelleWithValidationToolStripMenuItem.Text = LanguageLabels.GetLabel(110, "Import using Arelle (with validation)");
                importArelleWithoutValidationToolStripMenuItem.Text = LanguageLabels.GetLabel(111, "Import using Arelle (without validation)");
                //.Text = LanguageLabels.GetLabel(6);

                //Excel 
                excelToolStripMenuItem.Text = LanguageLabels.GetLabel(133, "Excel");
                importDataFromExcelFileToolStripMenuItem.Text = LanguageLabels.GetLabel(134, "Import data from basic Excel template");
                exportToExcelToolStripMenuItem.Text = LanguageLabels.GetLabel(135, "Export data to basic Excel template");
                downloadTemplateToolStripMenuItem.Text = LanguageLabels.GetLabel(136, "Download an empty basic Excel template");
                exportDataToBusinessTemplateToolStripMenuItem.Text = LanguageLabels.GetLabel(137, "Export data to Excel Business template");

                //Help
                helpToolStripMenuItem.Text = LanguageLabels.GetLabel(117, "Help");
                annotatedTemplatesToolStripMenuItem.Text = LanguageLabels.GetLabel(118, "DPM");
                dPMDictionaryToolStripMenuItem.Text = LanguageLabels.GetLabel(119, "Full S2 Dictionary");
                annotatedFULLTemplatesToolStripMenuItem.Text = LanguageLabels.GetLabel(120, "Full S2 Annotated Templates");
                preparatoryS2DictionaryToolStripMenuItem.Text = LanguageLabels.GetLabel(121, "Preparatory S2 Dictionary");
                preparatoryS2AnnotatedTemplatesToolStripMenuItem.Text = LanguageLabels.GetLabel(122, "Preparatory S2 Annotated Templates");
                taxononmyToolStripMenuItem.Text = LanguageLabels.GetLabel(123, "Taxonomy");
                preparatoryS2ToolStripMenuItem.Text = LanguageLabels.GetLabel(124, "Preparatory S2");
                solvencyIIFULLToolStripMenuItem.Text = LanguageLabels.GetLabel(125, "Full S2");
                cDRIVToolStripMenuItem.Text = LanguageLabels.GetLabel(126, "CDR IV");
                //PreparatoryS2TestXBRLInstancesToolStripMenuItem.Text = LanguageLabels.GetLabel(127, "Test XBRL Instances");
                databasesToolStripMenuItem.Text = LanguageLabels.GetLabel(128, "Databases");
                sQLServerBackUpsToolStripMenuItem.Text = LanguageLabels.GetLabel(129, "SQL Server back ups");
                databaseDocumentationToolStripMenuItem.Text = LanguageLabels.GetLabel(130, "Database documentation");
                userManualToolStripMenuItem.Text = LanguageLabels.GetLabel(131, "User Manual");
                whatsNewToolStripMenuItem.Text = LanguageLabels.GetLabel(132, "What's new");

                //Validate
                validationToolStripMenuItem.Text = LanguageLabels.GetLabel(113, "Validation");
                //validateActiveTableToolStripMenuItem.Text = LanguageLabels.GetLabel(114, "Validate active table");
                validateCurrentReportToolStripMenuItem.Text = LanguageLabels.GetLabel(115, "Validate active report");
                validateCurrentContainerToolStripMenuItem.Text = LanguageLabels.GetLabel(116, "Validate active container");


            }
            catch (Exception ex)
            {
                // If there is no database keep all labels as they are.
                Logger.WriteLog(eSeverity.Error, ex.Message);
            }
        }

        private void ClearForm()
        {
            ClearCurrentControl();
            treeView1.Nodes.Clear();
            ClearControls();

            // TreeViewConfig.CleanUp(treeListView1);
        }

        private void ClearControls()
        {
            if (_mainControl != null) _mainControl.Dispose();
            _mainControl = null;
            if (_mainOpenControl != null) _mainOpenControl.Dispose();
            _mainOpenControl = null;
        }

        private void CheckEnableSaveCancel()
        {
            bool enabled = _mainControl != null ? _mainControl.PageCombosCheck() : _mainControlSpecialCase.PageCombosCheck();
            _parentUserControl.EnableSaveCancel(enabled);
        }

        private void SetFormTitle()
        {
           
                Text = string.Format(" {0} {1} {2} {3}", "Tools for Undertakings -", StaticSettings.DbType.ToString().Replace("_", " "), "[" + "Application version:" +
                    Assembly.GetExecutingAssembly().GetName().Version + "] " + Program.ApplicationEnvironment, StaticSettings.DbType == DbType.No_container_selected ? "": StaticSettings.ConnectionString);


        }

        private void ClearCurrentControl()
        {
            // The object list view can fail when disposed of when a column is being edited.
            // By removing focus the events will work themselves through before disposing.
            treeView1.Focus();
            for (int i = splitContainer2.Panel2.Controls.Count; i > 0; i--)
            {
                splitContainer2.Panel2.Controls[i-1].Dispose();
            }

        }

        private void StatusBarUpdate(string message)
        {
           UpdateStatusStrip(message);
        }

        private void ResetCellColor()
        {
            if (_mainControl == null)
                return;

            List<ISolvencyDataControl> dataControls = _mainControl.GetDataControls();

            if (dataControls == null)
                return;

            foreach (ISolvencyDataControl c in dataControls)
            {
                Control uc = c as Control;
                if (uc != null)
                    if (uc.Enabled) uc.BackColor = SystemColors.Window;
            }

            //}
        }

        public void CreateTempDirectory(string dirPath)
        {
            try
            {
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);
            }
            catch (IOException e)
            {
                Logger.WriteLog(eSeverity.Error, string.Format("There was a problem creating temp folder {0}", e.Message));
                MessageBox.Show(string.Format("There was a problem creating temp folder {0}", e.Message));
            }
        }

        private void RefreshInstanceChanged()
        {
            // Clean down the form if it has things on it.
            ClearForm();
            StaticSettings.InstanceDescription = "";

            UpdateStatusStrip("", StatusBarDisplay.ReportName);
            UpdateStatusStrip("", StatusBarDisplay.TypeOfReport);
            UpdateStatusStrip("", StatusBarDisplay.ReportDate);
            UpdateStatusStrip("", StatusBarDisplay.EntityIdentifier);
            UpdateStatusStrip("", StatusBarDisplay.Currency);


            StaticSettings.InstanceCurrency = "EUR";

            string connectionString = RegSettings.ConnectionString;
            if (string.IsNullOrEmpty(connectionString) || !File.Exists(connectionString))
            {
                UpdateStatusStrip(LanguageLabels.GetLabel(112, "Please create or open a container report."));
                return;
            }


            if (InstanceID > 0)
            {
                // Rebuild the list TreeView
                try
                {
                    using (GetSQLData getData = new GetSQLData())
                    {
                        // This is the first attempt at using the database so if there is an error report it
                        treeView1.Populate(getData, InstanceID);
                        // TreeViewConfig.Setup(treeListView1, UpdateFromTreeView, getData, InstanceID);
                        dInstance thisInstance = getData.GetInstanceDetails(InstanceID);
                        if (thisInstance != null)
                        {
                            mModule thisModule = getData.GetModuleDetails(thisInstance.ModuleID);
                            UpdateStatusStrip(thisInstance.EntityName ?? "", StatusBarDisplay.ReportName);
                            UpdateStatusStrip(thisInstance.PeriodEndDateOrInstant.ConvertToDateString() ?? "", StatusBarDisplay.ReportDate);
                            UpdateStatusStrip(thisInstance.EntityIdentifier ?? "", StatusBarDisplay.EntityIdentifier);
                            UpdateStatusStrip(thisInstance.EntityCurrency ?? "", StatusBarDisplay.Currency);

                            if (thisModule != null)
                            {
                                StaticSettings.InstanceDescription = string.Format("{0} {1} {2:yyyy MM dd} - {3}", thisInstance.EntityName ?? "", thisInstance.EntityIdentifier, thisInstance.PeriodEndDateOrInstant, thisModule.ModuleLabel);
                                UpdateStatusStrip(thisModule.ModuleLabel ?? "", StatusBarDisplay.TypeOfReport);
                            }
                            else
                            {
                                StaticSettings.InstanceDescription = string.Format("{0} {1} {2:yyyy MM dd}", thisInstance.EntityName ?? "", thisInstance.EntityIdentifier, thisInstance.PeriodEndDateOrInstant);
                                UpdateStatusStrip("", StatusBarDisplay.TypeOfReport);
                            }
                            StaticSettings.InstanceCurrency = thisInstance.EntityCurrency;
                            UpdateStatusStrip(LanguageLabels.GetLabel(35, "Please select a template."));

                            //To enable Excel Import/Export menu
                            EnableExcelMenu();
                        }
                        else
                        {
                            RegSettings.InstanceID = 0;
                            MessageBox.Show(string.Format(LanguageLabels.GetLabel(22, "There was a problem accessing the selected report with the XBRT container.\nPlease select another report."), RegSettings.ConnectionString), LanguageLabels.GetLabel(23, "SolvencyII Report"));
                            ClearForm();
                            UpdateStatusStrip(LanguageLabels.GetLabel(24, "Please create or select an active report."));
                            EnableExcelMenu();

                        }
                    }


                    //Initialize validator

                    validator = new CRTValidator(eDataTier.SqLite, StaticSettings.ConnectionString,
                        (obj, args) =>
                        {
                            show_status((string)args.UserState);
                        },
                        (obj, args) =>
                        {
                            if (args.Error != null)
                            {
                                StringBuilder sb = new StringBuilder();
                                Exception ex = args.Error;

                                do
                                {
                                    sb.Append(ex.Message).Append("\n");

                                } while ((ex = ex.InnerException) != null);

                                MessageBox.Show("An error occured while validating database.\n" + sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Logger.WriteLog(eSeverity.Note, "Validation error");
                                Logger.WriteLog(eSeverity.Error, sb.ToString());
                                Logger.WriteLog(eSeverity.Error, args.Error.StackTrace);
                                return;
                            }

                            show_status("");

                            IEnumerable<ValidationError> valErrors = validator.SerializeErrors();

                            errorView.SetObjects(valErrors);
                            errorView.Refresh();

                            //Show the errors in the treeview node

                            IEnumerable<string> singleTemplate = valErrors.Select(t => t.TableCode).Distinct();

                            if (singleTemplate.Count() > 1)
                                TraverseTree(treeView1.Nodes, valErrors);
                            else
                                TraverseTree(treeView1.SelectedNode, valErrors);

                            //Reset the cell colors
                            ResetCellColor();


                        }
                    );

                    validator.Initialize(InstanceID);


                }
                catch (ValidationException ve)
                {
                    Logger.WriteLog(eSeverity.Note, "Validation Initialization error");
                    Logger.WriteLog(eSeverity.Critical, ve.Message);

                    string strMsg = "An error occured while initializing validation.\nValidation feature is not supported in this version.";

                    MessageBox.Show(strMsg, "Validation Initialization");

                    validator = null;
                }
                catch (Exception e)
                {
                    Logger.WriteLog(eSeverity.Error, string.Format("There was a problem accessing the database.{0}", e.Message));
                    string strMsg = LanguageLabels.GetLabel(25, @"There was a problem accessing the database.\r\n {0}\r\n {1}\r\nDo you want to select another database?");
                    string strTitle = LanguageLabels.GetLabel(26, "Locate SolvencyII database");
                    if (MessageBox.Show(string.Format(strMsg, e.Message, StaticSettings.ConnectionString), strTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        // Locate a new database and save its path.
                        if (ManageDatabases.LocateAndSaveDatabasePath())
                        {
                            SetupForm();
                            GetDbVersion();
                        }
                    }
                    else
                        ClearForm();
                }



            }
            else
            {
                UpdateStatusStrip(LanguageLabels.GetLabel(24, "Please create or select an active report."));
            }

        }

        private void arelle_setup_completed()
        {
            // enable XBRL (Arelle) menu items when arelle setup completes (in background)
            importXBRLIntanceFileToolStripMenuItem.Enabled = true;
            isArelleSetupCompleted = true;
        }

        #endregion

        #region Main Control Management

        private void UpdateMainUserControlLabels()
        {
            Cursor.Current = Cursors.WaitCursor;
            UserControlBase ctrl = this.FindAllChildrenByType<UserControlBase>().SingleOrDefault();
            if (ctrl != null)
            {
                ClosedTableManager tableManager = new ClosedTableManager(_mainControl, LanguageID, InstanceID);
                tableManager.UpdateLabels(_selectedItem);
            }

            if (_mainOpenControl != null)
            {
                _mainOpenControl.LanguageID = LanguageID;
                _mainOpenControl.SetupOpenUserControl(_selectedItem, null, true);
            }
            Cursor.Current = Cursors.Default;
        }

        private void ShowMainControl(bool openTemplate, bool specialCase)
        {
            ParentUserControl parentUserControl;
            parentUserControl = new ParentUserControl(SaveUserControlData, DeleteUserControlData, ToggleFilingIndicator, CancelUserControlData, openTemplate, _selectedItem.IsTyped);
            _parentUserControl = parentUserControl;

            if (openTemplate) 
                splitContainer2.Panel2.Controls.Add(_mainOpenControl);
            else
            {
                if (!specialCase)
                {
                    parentUserControl.Controls.Add(_mainControl);

                    _mainControl.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
                    _mainControl.AutoScroll = true;
                    _mainControl.Dock = DockStyle.Fill;

                    _mainControl.Width = parentUserControl.Width - 2*_mainControl.Left;
                    int calcHeight = parentUserControl.Height - _mainControl.Top - _mainControl.Left;
                    int drMainHeight = _mainControl.GetDataRepeaterHeight();
                    _mainControl.Height = calcHeight > drMainHeight ? calcHeight : drMainHeight;


                    _mainControl.BringToFront();
                    CheckEnableSaveCancel();
                    splitContainer2.Panel2.Controls.Add(parentUserControl);

                    // Run integity check of closed template
                    string result = UI.Shared.Data.FormIntegrityCheck.AllControlsLinkedToData(_mainControl.GetDataTypes(), _mainControl.GetDataTables(), _mainControl.GetDataControls(), _mainControl.GetComboControls(), _mainControl.GetDataComboBoxControls());
                    if (!string.IsNullOrEmpty(result))
                        MessageBox.Show(result, "Template integrity failure");

                }
                else
                {
                    // SPECIAL CASE
                    parentUserControl.Controls.Add(_mainControlSpecialCase);

                    _mainControlSpecialCase.Anchor = (AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom);
                    _mainControlSpecialCase.AutoScroll = true;
                    _mainControlSpecialCase.Dock = DockStyle.Fill;

                    _mainControlSpecialCase.Width = parentUserControl.Width - 2 * _mainControlSpecialCase.Left;
                    int calcHeight = parentUserControl.Height - _mainControlSpecialCase.Top - _mainControlSpecialCase.Left;
                    _mainControlSpecialCase.Height = calcHeight;


                    _mainControlSpecialCase.BringToFront();
                    CheckEnableSaveCancel();
                    splitContainer2.Panel2.Controls.Add(parentUserControl);

                    // Run integity check of closed template
                    string result = UI.Shared.Data.FormIntegrityCheck.AllControlsLinkedToData(_mainControlSpecialCase.GetDataTypes(), _mainControlSpecialCase.GetDataTables(), _mainControlSpecialCase.GetDataControls(), new List<ISolvencyComboBox>(), _mainControlSpecialCase.GetDataComboBoxControls());
                    if (!string.IsNullOrEmpty(result))
                        MessageBox.Show(result, "Template integrity failure");

                }
            }
        }

        private void UpdateMainUserControl(bool comboUpdate, bool skipDirtyCheck, bool setupNPageFirstEntries)
        {
            Cursor.Current = Cursors.WaitCursor;
            StaticUISettings.MainToolTips.RemoveAll(); // Reset the tool tips before updating the user control.
            try
            {
                if (_selectedItem != null & !CheckIsDirty(skipDirtyCheck))
                {
                    UpdateStatusStrip(_selectedItem.TemplateVariant + " → " + _selectedItem.DisplayText);

                    if (!_selectedItem.IsTyped)
                    {
                        _mainOpenControl = null;

                        // Closed table
                        if (!comboUpdate)
                        {
                            ClearCurrentControl();
                            // Locate the correct control to populate
                            // With some of the larger merged templates - over 3MB is size - it can take time to 
                            // instantiate them thus the need for the progress bar and corresponding thread here. 
                            bool taskCompleted = false;
                            Task task = new Task(() =>
                                                     {
                                                         try
                                                         {
                                                             LocateClosedUserControl(out _mainControl, out _mainControlSpecialCase);
                                                         }
                                                         catch (Exception e)
                                                         {
                                                             Logger.WriteLog(eSeverity.Error, "frmMain.LocateClosedUserControl", e.ToString());
                                                             MessageBox.Show(string.Format("The was an error attmpting to locate the template \r\n{0}", e.Message));
                                                         }
                                                         finally
                                                         {
                                                             taskCompleted = true;
                                                         }
                                                     });
                            task.Start();
                            while (!taskCompleted)
                            {
                                Thread.Sleep(50);
                                Application.DoEvents();
                            }
                            //LocateClosedUserControl(out _mainControl, out _mainControlSpecialCase);

                        }

                        if (_mainControl != null)
                        {
                            // Setup this windows specific control
                            _mainControl.InstanceID = InstanceID;                            

                            if (!comboUpdate) ShowMainControl(false, _mainControlSpecialCase != null);
                            // Select the data for the user control
                            SelectUserControlData(comboUpdate, _selectedItem, setupNPageFirstEntries);
                            
                        }
                        else
                        {
                            if (_mainControlSpecialCase != null)
                            {
                                // Special case here

                                _mainControlSpecialCase.InstanceID = InstanceID;

                                if (!comboUpdate) ShowMainControl(false, _mainControlSpecialCase != null);
                                // Select the data for the user control
                                SelectUserControlData(comboUpdate, _selectedItem, setupNPageFirstEntries);


                            }
                            else
                                UpdateStatusStrip(string.Format("{0} ({1}/{2})", LanguageLabels.GetLabel(36, "No template found"), _selectedItem.FrameworkCode, _selectedItem.TableID));
                        }
                    }
                    else
                    {

                        // Open table list 
                        if (!comboUpdate)
                        {
                            _mainOpenControl = null;
                            _mainControl = null;
                            _openCombosSelection = null;


                            ClearCurrentControl();
                            // Locate the correct control to populate
                            _mainOpenControl = LocateOpenUserControl2();
                            _mainOpenControl.ToggleFilingIndicator += ToggleFilingIndicator;
                        }

                        if (_mainOpenControl != null)
                        {
                            // See the OpenUserControlBase2_Load for setup and data population
                            // Setup this windows specific control
                            _mainOpenControl.InstanceID = InstanceID;
                            _mainOpenControl.LanguageID = LanguageID;
                            _mainOpenControl.FilingTemplateOrTableID = _selectedItem.FilingTemplateOrTableID;
                            _mainOpenControl.SetupOpenUserControl(_selectedItem, _openCombosSelection, setupNPageFirstEntries);

                            if (!comboUpdate) ShowMainControl(true, _mainControlSpecialCase != null);

                            // It appears that a repaint is needed to ensure the grid is shown correctly.
                            _mainOpenControl.GetVirtualObjectListView().Refresh();
                            _mainOpenControl.GetVirtualObjectListView().BuildList();
                            
                           
                            // this.Refresh();

                            // Open table sub control
                            if (!comboUpdate)
                            {
                                _mainControl = null;
                                // Locate the correct control to position
                                _mainControl = LocateOpenUserRowControl();
                            }
                            if (_mainControl != null)
                            {
                                // Setup this windows specific control
                                _mainControl.InstanceID = InstanceID;
                                if (!comboUpdate) ShowMainControl(false, _mainControlSpecialCase != null);
                                SelectUserControlData(comboUpdate, _selectedItem, setupNPageFirstEntries);
                            }

                        }
                        else
                        {
                            UpdateStatusStrip(string.Format("{0} ({1}/{2})", LanguageLabels.GetLabel(36, "No template found"), _selectedItem.FrameworkCode, _selectedItem.TableID));
                        }
                    }
                }
                
                // Update the template with its status
                SetFiledStatus();
            }
            catch (Exception ex)
            {
                allowTreeSelection = true;
                EnableAllMenuItems(true);
                Logger.WriteLog(eSeverity.Error, ex.Message);
                MessageBox.Show(string.Format("Unfortunately there was an error.\nPlease try again.\n\n{0} ", ex.Message), "Error");
            }
            finally
            {
                Cursor.Current = Cursors.Default;    
            }
            

            
        }

        private string prevRowColCode = string.Empty;

        private void ShowDimensionsData(object sender, string rowcolCode)
        {
            if (prevRowColCode != rowcolCode)
            {
                prevRowColCode = rowcolCode;

                //check if the cell properties for a table is already loaded
                List<CellProperties> tableCodes = null;

                if (tableCellProperties != null)
                    tableCodes = tableCellProperties.Where(t => t.TableCode.ToUpper() == _selectedItem.TableCode.ToUpper()).ToList();


                if (tableCellProperties == null || tableCodes == null || tableCodes.Count == 0)
                {

                    using (GetSQLData sqlData = new GetSQLData(StaticSettings.ConnectionString))
                    {
                        tableCellProperties = sqlData.GetCellProperties(_selectedItem.TableCode);
                    }
                }

                if (tableCellProperties != null)
                {
                    string[] codes = new string[2];

                    try
                    {
                        codes[0] = rowcolCode.Length > 0 ? rowcolCode.Substring(0, 5) : null;
                        codes[1] = rowcolCode.Length > 4 ? rowcolCode.Substring(5, 5) : null;
                    }
                    catch (Exception e) { }

                    cellPropertiesView.ClearObjects();

                    //Select only those cells which are specific to a cell
                    List<CellProperties> selectedCells =
                        (from t in tableCellProperties
                         from c in codes
                         where t.OrdinateCode != null && c != null  && t.OrdinateCode.ToUpper() == c.ToUpper()
                         select t).ToList();

                    cellPropertiesView.SetObjects(selectedCells);
                }



                //MessageBox.Show("Row/Column code: " + rowcolCode);


            }
        }

        //private void DeleteOpenTemplateData(string tableName)
        //{
        //    using (PutSQLData putData = new PutSQLData())
        //    {
        //        putData.DeleteOpenTableData2(tableName, InstanceID);
        //    }
        //}

        private void SetFiledStatus()
        {
            ClosedTableManager tableManager = new ClosedTableManager(_mainControl, LanguageID, InstanceID);
            bool isFiled = tableManager.IsFiled(_selectedItem);
            if(_parentUserControl != null)
                ((ParentUserControl) _parentUserControl).Filed = isFiled;
            if (_mainOpenControl != null)
                _mainOpenControl.Filed = isFiled;
        }

        private bool CheckIsDirty(bool skipCheck = false)
        {
            if (skipCheck) return false;
            if (_mainControl != null)
            {
                if (_mainControl.IsDirty)
                {
                    if (MessageBox.Show("You have not saved your changes!\r\n\r\nOk to exit without saving and Cancel to go back to the form", LanguageLabels.GetLabel(80, "Confirm cancel"), MessageBoxButtons.OKCancel) != DialogResult.OK)
                        return true;
                }
            }
            return false;
        }

        private void LocateClosedUserControl(out UserControlBase mainControl, out UserControlBase2 mainControlSpecialCase)
        {
            // Check to see if the control is found with the MEF components
            watch.Start();
            object ctl = _userExtensibility.GetControlByTableVID(_selectedItem);
            watch.Stop();
            mainControl = ctl as UserControlBase;
            mainControlSpecialCase = ctl as UserControlBase2;

            Debug.WriteLine(string.Format("Found control with MEF {0}", watch.ElapsedMilliseconds));
            watch.Reset();
            if (mainControl != null)
            {
                mainControl.Left = 10;
                mainControl.Top = 50;
            }
            if (mainControlSpecialCase != null)
            {
                mainControlSpecialCase.Left = 10;
                mainControlSpecialCase.Top = 50;
            }
        }

        private UserControlBase LocateOpenUserRowControl()
        {
            // Check to see if the control is found with the MEF components
            watch.Start();
            UserControlBase ctl = _userExtensibility.GetOpenSubControlByTableVID(_selectedItem);
            watch.Stop();
            Debug.WriteLine(string.Format("Found control with MEF {0}", watch.ElapsedMilliseconds));
            watch.Reset();
            if (ctl != null)
            {
                ctl.Left = 10;
                ctl.Top = 50;
            }
            return ctl;
        }

        private Dictionary<string, string> _openCombosSelection { get; set; }
        private void OpenTemplatePanelChange(bool subControlVisible, int pkID, List<ISolvencyComboBox> parentCombos)
        {
            if (subControlVisible)
            {
                // Hide the grid and show the user control
                _mainOpenControl.Visible = false;
                if (_mainControl != null)
                {
                    _openCombosSelection = parentCombos.Distinct().ToDictionary(c => c.ColName, c => c.GetValue);
                    _mainControl.Parent.Visible = true;
                    _mainControl.Visible = true;
                    _mainControl.SetOpenControl(pkID, parentCombos);
                    // For this event the open template is Filed
                    _parentUserControl.Filed = true;
                    _parentUserControl.ChangeCancelToClose = true;
                    
                }
            }
            else
            {
                // Hide the user control and show the grid
                _mainOpenControl.Visible = true;
                if (_mainControl != null)
                    _mainControl.Visible = false;


                // Refresh the data
                _mainOpenControl.RefreshGrid();
            }

            SubscribeToShowDimensionalEvents();
        }

        private OpenUserControlBase2 LocateOpenUserControl2()
        {
            // Check to see if the control is found with the MEF components
            int MARGIN = 3;
            watch.Start();
            OpenUserControlBase2 ctl = _userExtensibility.GetOpenControlByTableVID(_selectedItem);
            ctl.PanelChange += OpenTemplatePanelChange;
            watch.Stop();
            Debug.WriteLine(string.Format("Found control with MEF {0}", watch.ElapsedMilliseconds));
            watch.Reset();
            if (ctl != null)
            {
                ctl.Left = MARGIN;
                ctl.Top = 0;
                ctl.Width = splitContainer2.Panel2.Width - (2*MARGIN);
                ctl.Height = splitContainer2.Panel2.Height;
                ctl.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                ctl.AutoScroll = true;
            }
            return ctl;
        }


        #endregion

        #region Toolbar ProgressBar




        private void StartToolStripProgressBar()
        {
            toolStripProgressBar1.Visible = true;
            asyncTimer.Interval = 200;
            asyncTimer.Tick += ToolStripProgressBar_Tick;
            asyncTimer.Start();
        }

        private void ToolStripProgressBar_Tick(object sender, EventArgs e)
        {
            int currentValue = toolStripProgressBar1.Value;
            currentValue += 2;
            if (currentValue > 100) currentValue = 0;
            toolStripProgressBar1.Value = currentValue;
        }

        private void StopToolStripProgressBar()
        {
            asyncTimer.Stop();
            asyncTimer.Tick -= AsyncTimerTick;
            toolStripProgressBar1.Visible = false;
        }

        #endregion

        #region Import / Export / Valuation Async Management

        private void StartBusyIndicator()
        {
            ClearCurrentControl();

            ProgressBar bar = new ProgressBar
                                  {
                                      Name = "barProgress", 
                                      Location = new Point(10, 10), 
                                      Value = 0, 
                                      Width = 200
                                  };
            bar.BringToFront();
            bar.BackColor = Color.SkyBlue;


            splitContainer2.Panel2.Controls.Add(bar);
            
            asyncTimer.Interval = 200;
            asyncTimer.Tick += AsyncTimerTick;
            asyncTimer.Start();

            Cursor.Current = Cursors.WaitCursor;

            EnableAllMenuItems(false);
            
            treeView1.Enabled = false;


        }
        
        private void AsyncTimerTick(object sender, EventArgs e)
        {
            ProgressBar barTimer = (ProgressBar) splitContainer2.Panel2.Controls["barProgress"];
            if (barTimer == null)
            {
                StopBusyIndicator();
                return;
            }
            int currentValue = barTimer.Value;
            currentValue += 2;
            if (currentValue > 100) currentValue = 0;
            barTimer.Value = currentValue;
            toolStripProgressBar1.Value = currentValue;
        }

        private void StopBusyIndicator()
        {
            asyncTimer.Stop();
            asyncTimer.Tick -= AsyncTimerTick;
            splitContainer2.Panel2.Controls.RemoveByKey("barProgress");
            toolStripProgressBar1.Visible = false;
            Cursor.Current = Cursors.Default;
            EnableAllMenuItems(true);
            treeView1.Enabled = true;
        }

        public void ArelleProgress(object s, ProgressChangedEventArgs args)
        {
            if (ImportExportArelle != null)
            {
                string msg = args.UserState as string;
                show_status(ImportExportArelle.StatusPrompt + msg);
            }
        }

        private void AsyncResponse(bool success, string message, ImportExportValuationAsync.ResponseType type)
        {
            Invoke((Action) delegate
                                {
                                    // Stop graphical progress indicator
                                    StopBusyIndicator();

                                    // Deal with the response.
                                    if (success)
                                    {
                                        switch (type)
                                        {

                                            case ImportExportValuationAsync.ResponseType.Import:
                                                // Note the message contins the newly inserted instance ID
                                                InstanceID = int.Parse(message);
                                                show_status(LanguageLabels.GetLabel(37, "Imported successfully"));
                                                //MessageBox.Show(LanguageLabels.GetLabel(37, "Imported successfully"));
                                                break;
                                            case ImportExportValuationAsync.ResponseType.Export:
                                                // Note the message contains the exported file name
                                                show_status("Exported successfully.");

                                                // Setup the form to show the selected tree item - if one exists.
                                                // RefreshTemplate(); // Removed since it was considered a bug.
                                                break;
                                            case ImportExportValuationAsync.ResponseType.Validation:
                                                MessageBox.Show(LanguageLabels.GetLabel(40, "File passed validation checks successfully"), LanguageLabels.GetLabel(39, "Validation Check"));
                                                break;
                                            default:
                                                throw new ArgumentOutOfRangeException("type");
                                        }
                                    }
                                    else
                                    {
                                        switch (type)
                                        {
                                            case ImportExportValuationAsync.ResponseType.Import:
                                                MessageBox.Show(message, LanguageLabels.GetLabel(41,"Import problem"));
                                                break;
                                            case ImportExportValuationAsync.ResponseType.Export:
                                                MessageBox.Show(message, LanguageLabels.GetLabel(41,"Export problem"));
                                                // Setup the form to show the selected tree item - if one exists.
                                                RefreshTemplate();
                                                break;
                                            case ImportExportValuationAsync.ResponseType.Validation:
                                                // User must be alerted with dialogue.
                                                Form frm = new frmPopup(LanguageLabels.GetLabel(42, "Validation Failure"), message);
                                                frm.ShowDialog(this);
                                                break;
                                            default:
                                                throw new ArgumentOutOfRangeException("type");
                                        }
                                    }

                                    

                                });
        }

        private void show_status(string statusMsg)
        {
            if (!this.lblExportImportStatus.IsDisposed) // ignore events after closing T4U while Arelle still running
                Invoke((Action) delegate
                                    {
                                        this.lblExportImportStatus.Text = statusMsg;
                                    });
        }

        #region Export Code

        private void ExportDb2XBRLAsync1(eImportExportOperationType type,PreParatoryVersions prepVersion)
        {

            Logger.WriteLog(eSeverity.Note, "Method ExportDb2XBRLAsync1 starts execution");
            Logger.WriteLog(eSeverity.Note, "Export operation starts execution.............");

            //Get the file name from the user
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XBRL File|*.xbrl";
            saveFileDialog1.Title = LanguageLabels.GetLabel(2, "Create XBRL file");
            if(saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            if (saveFileDialog1.FileName == "")
                return;

            ClearCurrentControl();

            string targetXBRL = saveFileDialog1.FileName;
            StartBusyIndicator();

            //Run ETL process
            ImportExportThreadKarol = new BackgroundWorker();
            ImportExportThreadKarol.WorkerSupportsCancellation = true;

            
            factsETL = new EtlOperations();

            //TODO:Nicholas, check if T4U is connecting to SQLite or SQLServer and agree with Karol how to raise the right ETL function

            ProgressHandler.Progress += ProgressHandler_Progress;
            //Do Work event
            ImportExportThreadKarol.DoWork += (sender, args) => factsETL.etlSavingXBRLinstance(StaticSettings.ConnectionString, Convert.ToInt32(InstanceID));

            //Completed event
            ImportExportThreadKarol.RunWorkerCompleted += (sender, args) =>
            {
                // Stop graphical progress indicator
                StopBusyIndicator();


                ProgressHandler.Progress -= ProgressHandler_Progress;
                //Check for an exception or operation cancelled
                if(args.Cancelled == true || args.Error != null)
                {
                    Logger.WriteLog(eSeverity.Error, args.Error.ToString());
                    ExceptionHandler.AsynchronousThreadException(eSeverity.Error, args.Error, "Export XBRL",
                        "An error occured while processing ETL.\nPlease check the log for more information.");

                   
                    show_status("");

                    return; //do not process further
                }

                
                //ExportDb2XBRLAsync2(type, targetXBRL);

                //Call Arelle command line processor
                ClearCurrentControl();
                ImportExportArelle = new ArelleCmdInterface("Saving instance - ");
                Logger.WriteLog(eSeverity.Note, "Invoking SaveFromDatabaseToInstance.......");
                ImportExportArelle.SaveFromDatabaseToInstance(type, InstanceID, targetXBRL, ArelleProgress, ExportArelleComplete, prepVersion);

                AsyncResponse(true, "Export completed", ImportExportValuationAsync.ResponseType.Export);
                factsETL = null;
            };
            Logger.WriteLog(eSeverity.Note, "Invoking ImportExportThreadKarol ..............");
            //Invoke the asynchronous process
            ImportExportThreadKarol.RunWorkerAsync();

        }

        /*private void ExportDb2XBRLAsync2(eImportExportOperationType type, string targetXBRL)
        {
            ClearCurrentControl();
            ImportExportThreadArelle = new ArelleCmdInterface().saveFromDatabaseToInstance(type, InstanceID, targetXBRL, show_status, ShowArelleValidationLogMessage);
        }*/

        private void ExportArelleComplete(object s, RunWorkerCompletedEventArgs args)
        {
            Logger.WriteLog(eSeverity.Note, "ExportArelleComplete method starts execution");
            //Get the active instance ID
            //Displays error log in list box
            IValidationQuery validationQuery = ValidationFactory.GetValidationQuery(eDataTier.SqLite);
            ISolvencyData conn = ConnectionFactory.GetConnection(eDataTier.SqLite, StaticSettings.ConnectionString);
            //SolvencyII.Validation.GetMessage getMessage = new SolvencyII.Validation.GetMessage(StaticSettings.ConnectionString);
            
            IEnumerable<ValidationMessage> messages;

            show_status("");
            string _success = "successfully";

            //Handle exception if there is an issue in Arelle processing
            if(args.Error != null || args.Cancelled == true)
            {
                ExceptionHandler.AsynchronousThreadException(eSeverity.Error, args.Error, "Export XBRL",
                        "An error occured while processing Exporting or validating XBRL.\nPlease check the log for more information.");

                return; //do not process further
            }
            
            if(ImportExportArelle.InstanceID > 0)
                messages = validationQuery.GetArelleValidationErrors(conn, ImportExportArelle.InstanceID);
            else
                messages = validationQuery.GetArelleValidationErrors(conn, InstanceID);

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

            if (_isForMigration == false)
            {
                //Show only if there are any errors
                if (messages != null && messages.Count() > 0)
                {
                    _success = "with messages";
                    ArelleValidationStatus statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Arelle_validation_results_for_export,false);//need to change
                    statusDlg.ShowDialog();

                    //if the log was ignored.

                    if (statusDlg != null)
                    {
                        //if the log was ignored.
                        if (statusDlg.DialogResult == DialogResult.Abort)
                        {
                            if (MessageBox.Show("Are you sure you want to abort the export operation.", LanguageLabels.GetLabel(30, "Deletion Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                //Delete the file if the export operation is aborted.
                                if (File.Exists(filePath))
                                    File.Delete(filePath);

                                return;
                            }

                        }
                    }



                }
                else
                {



                    if (report != null || report != "")
                    {
                        ArelleValidationDetailLog dtlLog = new ArelleValidationDetailLog(report);
                        dtlLog.ShowDialog();
                    }
                }
            }
            else //For migration to show the list of errors.......
            {
                string fileName = string.Empty;
                if (messages != null && messages.Count() > 0)
                {
                    _success = "with messages";
                    if (!string.IsNullOrEmpty(filePath)) 
                        fileName = Path.GetFileName(filePath);
                    
                    ArelleValidationStatus statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Arelle_validation_results_for_export, true, fileName);//need to change
                    statusDlg.ShowDialog();
                }

            }


            //Show a message box that import XBRL is complete
            ImportExportArelle = null;
            if (_isForMigration == false)                    
            MessageBox.Show(string.Format("Export XBRL is completed {0}.", _success), 
                            "Export XBRL", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region Import code

        private void ImportXBRL2DbAsync1(eImportExportOperationType type)
        {
            Logger.WriteLog(eSeverity.Note, "starting ImportXBRL2DbAsync1 method execution");
            Logger.WriteLog(eSeverity.Note, "************ Import operation starts *****************");
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "SolvencyII (*.xbrl)|*.xbrl";
            openFileDialog1.Title = LanguageLabels.GetLabel(43, "Locate xbrl file to import");
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ClearCurrentControl();

                string sourceXBRL = openFileDialog1.FileName;

                // If this file has already been imported Do not import it again.
                _importedFileName = Path.GetFileName(sourceXBRL);
                _importedFullFileName = sourceXBRL;
                
                // We can import a file more than twice.
                //dInstance instance;
                //using (GetSQLData getData = new GetSQLData())
                //{
                //    instance = getData.GetInstanceDetails(_importedFileName);
                //}
                //if (instance != null)
                //{
                //    MessageBox.Show(string.Format("A file has already been imported with the name:\n{0}\n\nPlease rename it and try again.", _importedFileName));
                //    return;
                //}

                // Get information for the instance we are about to create.
                string message = "";
                if (PopupDialog.ShowInputDialog(ref message, "Name of instance") == DialogResult.OK)
                {
                if (string.IsNullOrEmpty(message)) message = "Un-named";
                _importedInstanceName = message;
                    StartBusyIndicator();
                ImportExportArelle = new ArelleCmdInterface("Loading instance - ");
                Logger.WriteLog(eSeverity.Note, "Calling ParseInstanceIntoDatabase");
                ImportExportArelle.ParseInstanceIntoDatabase(type, sourceXBRL, ArelleProgress, ImportXBRL2DbArelleComplete);
                }
            }
        }

        private void ImportXBRL2DbArelleComplete(object s, RunWorkerCompletedEventArgs args)
        {
            //string report = (string)args.Result;
            //New implementation to handle multiple parameters
            string report = string.Empty;
            eImportExportOperationType operation_type = eImportExportOperationType.No_Operation_selected;
            if (args.Result is Object[]) // cast #1
            {
                object[] resultParams = args.Result as Object[];
                if (resultParams[0] != null)                
                    report = resultParams[0].ToString();
                if (resultParams[1] != null)
                {
                    //To get the operation type
                    string value = resultParams[1].ToString();
                    operation_type = (eImportExportOperationType)Enum.Parse(typeof(eImportExportOperationType), value, true);
                }
                

            }

            ClearCurrentControl();
            // This is called once the Arelle originated code has been competed. 
            // Its action is passed through the the backgroundworker thread created within ArelleCmdInterface.
            // Insert the instance
            dInstance instance;
            using (GetSQLData getData = new GetSQLData())
            {
                instance = getData.GetInstanceDetails(_importedFileName);
            }

            //clear the status bar
            show_status("");
            string _success = "successfully";

            //Handle exception if there is an issue in Arelle processing
            if (args.Error != null || args.Cancelled == true)
            {

                //Remove the instance if it is created
                using(PutSQLData putData = new PutSQLData())
                {
                    if(instance.InstanceID > 0)
                        putData.DeleteInstance(instance.InstanceID);
                }

                ExceptionHandler.AsynchronousThreadException(eSeverity.Error, args.Error, "Export XBRL",
                        "An error occured while processing Exporting or validating XBRL.\nPlease check the log for more information.");

                return; //do not process further
            }

            if (instance != null)
            {
                PutSQLData putData = new PutSQLData();
                try
                {
                    //Displays error log in list box
                    IValidationQuery validationQuery = ValidationFactory.GetValidationQuery(eDataTier.SqLite);
                    ISolvencyData conn = ConnectionFactory.GetConnection(eDataTier.SqLite, StaticSettings.ConnectionString);
                    //SolvencyII.Validation.GetMessage getMessage = new SolvencyII.Validation.GetMessage(StaticSettings.ConnectionString);
                    IEnumerable<ValidationMessage> messages = validationQuery.GetArelleValidationErrors(conn, instance.InstanceID);

                    //Show only if there are any errors
                    if (messages != null && messages.Count() > 0)
                    {
                        _success = "with messages";
                        ArelleValidationStatus statusDlg = null;
                        if(operation_type.Equals(eImportExportOperationType.Native_Import))
                        {
                            statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Arelle_results_for_native_import,false);
                            statusDlg.ShowDialog();
                        }
                        if (operation_type.Equals(eImportExportOperationType.Import_using_Arelle))
                        {
                            statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Arelle_validation_results,false);
                            statusDlg.ShowDialog();
                        }
                        //throw
                        if (operation_type.Equals(eImportExportOperationType.No_Operation_selected))
                        {
                           //throw error
                        }


                        if (statusDlg != null)
                        {
                            //if the log was ignored.
                            if (statusDlg.DialogResult == DialogResult.Abort)
                            {
                                //Remove the instance if it is created
                                if (MessageBox.Show("Are you sure you want to abort the import operation.", LanguageLabels.GetLabel(30, "Deletion Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    //Remove the instance if it is created
                                    using (PutSQLData delInstance = new PutSQLData())
                                    {
                                        if (instance.InstanceID > 0)
                                        {
                                            delInstance.DeleteInstance(InstanceID);
                                            delInstance.Dispose();
                                            InstanceID = 0;
                                            ClearForm();
                                            EnableExcelMenu();
                                        }
                                    }

                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (report != null || report != "")
                        {
                            ArelleValidationDetailLog dtlLog = new ArelleValidationDetailLog(report);
                            dtlLog.ShowDialog();
                        }
                    }

                    long newInstanceID;
                    instance.EntityName = _importedInstanceName;
                    // Using the full path here prevents problems with duplication.
                    // The instances are located using the _importedFileName - not the FullFileName
                    instance.FileName = _importedFullFileName; 
                    string result = putData.InsertUpdateInstance(instance, out newInstanceID);
                    

                    if (!string.IsNullOrEmpty(result))
                    {
                        // We have errors inserting the instance.
                        this.Invoke((MethodInvoker)delegate() { MessageBox.Show(string.Format("Unable to update the instance: {0}", putData.Errors[0])); });
                        return;
                    }

                    // Run Karol's code in background
                    ProgressHandler.Progress += ProgressHandler_Progress;

                    this.Invoke((MethodInvoker) StartBusyIndicator);
                    ImportExportThreadKarol = new BackgroundWorker();
                    ImportExportThreadKarol.WorkerSupportsCancellation = true;
                    
                    factsETL = new EtlOperations();

                    //DoWork event
                    ImportExportThreadKarol.DoWork += (sender, arg) => factsETL.etlLoadingXBRLinstance(StaticSettings.ConnectionString, Convert.ToInt32(instance.InstanceID));

                    //Completed Event
                    ImportExportThreadKarol.RunWorkerCompleted += (sender, arg) =>
                    {
                        // Stop graphical progress indicator
                        StopBusyIndicator();

                        ProgressHandler.Progress -= ProgressHandler_Progress;

                        //Check for an exception or operation cancelled
                        if (args.Cancelled == true || arg.Error != null)
                        {
                            ExceptionHandler.AsynchronousThreadException(eSeverity.Error, args.Error, "Export XBRL",
                                "An error occured while processing ETL.\nPlease check the log for more information.");



                            return; //do not process further
                        }

                        AsyncResponse(true, instance.InstanceID.ToString(), ImportExportValuationAsync.ResponseType.Import);
                        factsETL = null;

                        //Show a message box that import XBRL is complete

                        //New implementation to handle ETL errors:
                        messages = validationQuery.GetETLErrors(conn, instance.InstanceID);
                        if (messages != null && messages.Count() > 0)
                        {
                            _success = "with messages";
                             ArelleValidationStatus statusDlg = null;
                            if(operation_type.Equals(eImportExportOperationType.Native_Import))
                            {
                                statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Import_results_for_Native_import,false);
                                statusDlg.ShowDialog();
                            }
                            if (operation_type.Equals(eImportExportOperationType.Import_using_Arelle))
                            {
                                statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Import_results_for_Arelle_import,false);
                                statusDlg.ShowDialog();
                            }
                            if (operation_type.Equals(eImportExportOperationType.No_Operation_selected))
                            {
                                //throw error
                            }
                            //New implementation for the ETL failures: to delete the imported report
                            if (statusDlg != null)
                            {
                                //if the log was ignored.
                                if (statusDlg.DialogResult == DialogResult.Abort)
                                {
                                    if (MessageBox.Show( "Are you sure you want to abort the import operation.", LanguageLabels.GetLabel(30, "Deletion Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                    {
                                        //Remove the instance if it is created
                                        using (PutSQLData delInstance = new PutSQLData())
                                        {
                                            if (instance.InstanceID > 0)
                                            {                                                
                                                delInstance.DeleteInstance(InstanceID);
                                                delInstance.Dispose();
                                                InstanceID = 0;
                                                ClearForm();
                                                EnableExcelMenu();
                                            }
                                        }

                                        return;
                                    }
                                }
                            }
                            
                        }
                          
                
                        MessageBox.Show(string.Format("Import XBRL is completed {0}.", _success), 
                                        "Import XBRL", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Select the imported instance:
                        if (newInstanceID != 0)
                        {
                            InstanceID = newInstanceID;
                            if (instance != null)
                            {
                                if (string.IsNullOrEmpty(instance.EntityCurrency))
                                {
                                    MessageBox.Show("Please edit report properties and set up reporting currency", "Missing report property", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                            InitializeValidationErrorView();
                            InitializeCellPropertiesView();
                        }

                    };

                    ImportExportThreadKarol.RunWorkerAsync();

                }
                finally
                {
                    putData.Dispose();
                }

            }
            else
            {
                // Error published from Arelle code
            }
        }
        
        #endregion

        private void ProgressHandler_Progress(int current, int total, string message)
        {
            if (total != 0)
                show_status(string.Format("{0} {1}/{2}", message, current, total));
            else
                show_status(string.Format("{0} {1}", message, current));
        }

        private void ValidateXBRLAsync(string sourceXBRL = "")
        {
            if (string.IsNullOrEmpty(sourceXBRL))
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "SolvencyII (*.xbrl)|*.xbrl";
                openFileDialog1.Title = LanguageLabels.GetLabel(44,"Locate xbrl file to validate");
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    sourceXBRL = openFileDialog1.FileName;
                }
            }
            if (!string.IsNullOrEmpty(sourceXBRL))
            {
                ClearCurrentControl();
                // We have a path.
                if (RegSettings.LocalValidation)
                {
                    ImportExportArelle = new ArelleCmdInterface("Validation - ");
                    ImportExportArelle.ValidateXBRL(sourceXBRL, StaticSettings.ConnectionString, ArelleProgress, ValidateArelleComplete);
                }
                else
                {
                    // Remote Only

                    ImportExportValuationAsync async = new ImportExportValuationAsync();
                    async.AsyncResponse += AsyncResponse;
                    StartBusyIndicator();
                    Thread background = new Thread(() => async.ValuationAsync(sourceXBRL));
                    background.Start();
                }

            }
        }

        private void ValidateArelleComplete(object s, RunWorkerCompletedEventArgs args)
        {
            //Get the active instance ID
            //Displays error log in list box
            IValidationQuery validationQuery = ValidationFactory.GetValidationQuery(eDataTier.SqLite);
            ISolvencyData conn = ConnectionFactory.GetConnection(eDataTier.SqLite, StaticSettings.ConnectionString);
            //SolvencyII.Validation.GetMessage getMessage = new SolvencyII.Validation.GetMessage(StaticSettings.ConnectionString);

            IEnumerable<ValidationMessage> messages;

            show_status("");

            //Handle exception if there is an issue in Arelle processing
            if (args.Error != null || args.Cancelled == true)
            {
                ExceptionHandler.AsynchronousThreadException(eSeverity.Error, args.Error, "Validate XBRL",
                        "An error occured while validating XBRL.\nPlease check the log for more information.");

                return; //do not process further
            }

            if (ImportExportArelle.InstanceID > 0)
                messages = validationQuery.GetArelleValidationErrors(conn, ImportExportArelle.InstanceID);
            else
                messages = validationQuery.GetArelleValidationErrors(conn, InstanceID);

            //New implementation to handle multiple parameters from the thread.
            string report = string.Empty;
            if (args.Result is Object[]) // cast #1
            {
                object[] resultParams = args.Result as Object[];
                if (resultParams[0] != null)
                {
                    report = resultParams[0].ToString();
                }

            }

            //Show only if there are any errors
            string _success = "successfully";
            if (messages != null && messages.Count() > 0)
            {
                _success = "with messages";
                ArelleValidationStatus statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Validation_result,false);
                statusDlg.ShowDialog();

                //if the log was ignored.
                if (statusDlg.DialogResult == DialogResult.Abort)
                    return;
            }
            else
            {
                //string report = (string)args.Result;

                if (report != null || report != "")
                {
                    ArelleValidationDetailLog dtlLog = new ArelleValidationDetailLog(report);
                    dtlLog.ShowDialog();
                }
            }

            //Show a message box that import XBRL is complete

            MessageBox.Show(string.Format("XBRL validation is completed {0}.", _success), 
                            "XBRL validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region DateTier Management

        /// <summary>
        /// Check the database type, path etc and configure the data access.
        /// </summary>
        /// <param name="value"></param>
        private bool SetupFormForDataTier(eDataTier value)
        {
            StaticSettings.DataTier = value;
            // Update menus
            bool sqlite = value == eDataTier.SqLite;
            createXbrtToolStripMenuItem.Visible = sqlite;
            openDatabaseToolStripMenuItem.Visible = sqlite;
            recentFilesToolStripMenuItem.Visible = sqlite;
            changeDatabaseConnectionToolStripMenuItem.Visible = !sqlite;

            sQLiteToolStripMenuItem.Checked = sqlite;
            sQLServerToolStripMenuItem.Checked = !sqlite;

            // Update data 
            bool successfulConnection = true;
            if (StaticSettings.DataTier == eDataTier.SqLite)
            {
                string connectionString = RegSettings.ConnectionString;
                if (connectionString != null && File.Exists(connectionString))
                {
                    if (System.IO.Path.GetExtension(connectionString).ToUpper().EndsWith("XBRT"))
                    {
                        // This approach has been used to facilitate multiplatform development for the Data.Shared component.
                        StaticSettings.ConnectionString = connectionString;
                        SetFormTitle();
                        RecentFilesRegistryManagement.OpenFile(connectionString);
                        EnableMenuItems(true);
                        EnableExcelMenu();
                        //CheckDBVersion();
                    }
                }
                else
                {
                    // Ensure everything is cleaned down.
                    ClearForm();
                    MessageBox.Show(LanguageLabels.GetLabel(20, "Please create a new XBRT container or open an existing one."), LanguageLabels.GetLabel(21, "No active XBRT container"));
                    EnableMenuItems(false);
                    EnableExcelMenu();
                    
                }
            }
            else
            {
                // SQL Server
                StaticSettings.ConnectionString = RegSettings.SQLServerConnection;

                bool escape = false;
               
                while (!escape)
                {
                    try
                    {
                        using (GetSQLData getData = new GetSQLData())
                        {
                            getData.ConnectionCheck();
                        }
                        escape = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("There was an error attempting to open the SQL Server:\n{0}", ex.Message), "Connection Error");
                    }
                    finally
                    {
                        if (!escape)
                        {
                            if (MessageBox.Show("Would you like to change the connection string?", "Connection Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                string newConnection = CreateConnectionString.Create();
                                if (!string.IsNullOrEmpty(newConnection))
                                {
                                    RegSettings.SQLServerConnection = newConnection;
                                    StaticSettings.ConnectionString = newConnection;
                                }
                                else
                                {
                                    successfulConnection = false;
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Would you like to switch back to SQLite data mode?", "Connection Error", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                {
                                    SetupFormDataTypeChanged(eDataTier.SqLite);
                                    StaticSettings.ConnectionString = RegSettings.ConnectionString;
                                    escape = true;
                                }
                                else
                                {
                                    successfulConnection = false;
                                    escape = false;
                                }
                                
                            }
                        }
                    }
                }
            }

            if (successfulConnection)
            {
                GetDbVersion();
                CheckDBVersion();
                SetFormTitle();
                EnableExcelMenu();

            }
            return successfulConnection;
        }

        private void GetDbVersion()
        {
            if(!string.IsNullOrEmpty(StaticSettings.ConnectionString))
            {
                using (GetSQLData getData = new GetSQLData())
                {
                    GetDbVersion(getData);
                }
            }
        }
        private void GetDbVersion(GetSQLData getData)
        {
            // Setup the static db type
            aApplication version = getData.CheckDBVersion();
            if (version != null && version.DatabaseType != null)
            {
                DbType dbType = (DbType)Enum.Parse(typeof(DbType), version.DatabaseType);
                StaticSettings.DbType = dbType;
            }
        }


        private void CheckDBVersion()
        {
            
                if(StaticSettings.DbType!=DbType.No_container_selected)
                {
                    using (GetSQLData getData = new GetSQLData())
                    {
                        aApplication appInfo = getData.CheckDBVersion();
                        string thisVersion = GuiSpecific.ApplicationVersion();

                        if (appInfo.ApplicationVersion == null)
                        {
                            //MessageBox.Show(string.Format("This container was created with previous version. " +
                            //    "Please create new container with the newest version (migration is not currently supported). " +
                            //    "Aplication version: {0} Database Version: {1}", thisVersion, appInfo.ApplicationVersion));
                            //MessageBox.Show(string.Format(LanguageLabels.GetLabel(143, "This container was created with previous version. Please create new container with the newest version (migration is not currently supported). Aplication version: {0} Database Version: {1}"), thisVersion, appInfo.ApplicationVersion));
                            MessageBox.Show(string.Format("You are trying to open a container created using previous version of the tool ({0}) for which migration is not supported. Please create a new container.", appInfo.ApplicationVersion));
                            _migrationRequired = false;
                        }
                        else
                        {
                            Version ver = new Version(appInfo.ApplicationVersion.ToString());

                            if (StaticSettings.DbType == DbType.SolvencyII)
                                supportedDataBaseVersion = supportedDataBaseVersion_FULL;
                            if (StaticSettings.DbType == DbType.SolvencyII_Preparatory)
                                supportedDataBaseVersion = supportedDataBaseVersion_PREPARATORY;
                            if (StaticSettings.DbType == DbType.CRDIV)
                                supportedDataBaseVersion = supportedDataBaseVersion_CRD;

                            if (!string.IsNullOrEmpty(supportedDataBaseVersion))
                            {
                                //migrattion not supported version
                                if (ver.CompareTo(new Version(supportedMigrationDataBaseVersion_PREPARATORY_FebruaryVersion_2015)) < 0)                                
                                {
                                   
                                    MessageBox.Show(string.Format("You are trying to open a container created using previous version of the tool ({0}) for which migration is not supported. Please create a new container.", appInfo.ApplicationVersion));
                                    _migrationRequired = false;
                                    Logger.WriteLog(eSeverity.Note, "Migration not supported ");

                                }
                                else //migrattion Feb version to  -> 1.5.2(b)
                                if (ver.CompareTo(new Version(supportedMigrationDataBaseVersion_PREPARATORY_FebruaryVersion_2015)) >= 0 && ver.CompareTo(new Version(supportedMigrationDataBaseVersion_PREPARATORY_MarchVersion_2015)) < 0)
                                {
                                    
                                    _migrationRequired = true;
                                    Logger.WriteLog(eSeverity.Note, "Version for Migration - second version");
                                    Logger.WriteLog(eSeverity.Note, ver.ToString());
                                    Logger.WriteLog(eSeverity.Note, "Migration required to 1.5.2(b)");

                                }
                                else //migrattion Feb version to  -> 1.5.2(c)
                                 if (ver.CompareTo(new Version(supportedDataBaseVersion)) < 0)
                                 {
                                       
                                     _migrationRequired = true;
                                     prepVersion = PreParatoryVersions.ThirdVersion;
                                     Logger.WriteLog(eSeverity.Note, "Version for Migration - third version");
                                     Logger.WriteLog(eSeverity.Note, ver.ToString());
                                     Logger.WriteLog(eSeverity.Note, "Migration required to 1.5.2(c)");
                                 }
                                else //newer version
                                  {
                                     Logger.WriteLog(eSeverity.Note, "Migration not required, newer version");
                                  }
                            }

                        }

                    }
            }
            
        }

        private string GetExcelTemplatePath(ExcelTemplateType type)
        {
            if (type == ExcelTemplateType.BasicTemplate)
            {
                if (StaticSettings.DbType == DbType.SolvencyII)
                    return ("ExcelTemplates\\Full\\");

                
                if (StaticSettings.DbType == DbType.SolvencyII_Preparatory)   
                    return ("ExcelTemplates\\Preparatory\\");
            }
            else if(type == ExcelTemplateType.BusinessTemplate)
            {
                return ("ExcelTemplates\\BusinessTemplates\\");
            }
            else if(type == ExcelTemplateType.BusinessTemplate_Macro)
            {
                return ("Exl-Business_Encrypted\\");
            }

            return (string.Empty);
        }

        #endregion

        #region Excel
        private void downloadTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = StaticSettings.ConnectionString;
            InvokeExcel.DownloadTemplate(connectionString, InstanceID, GetExcelTemplatePath(ExcelTemplateType.BasicTemplate));
        }

        private void importDataFromExcelFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture.ClearCachedData();  
            //
            if (StaticSettings.DbType == DbType.SolvencyII)            
               supportedExcelTemplateVersion=supportedExcelTemplateVersion_FULL;           
            if (StaticSettings.DbType == DbType.SolvencyII_Preparatory)
                supportedExcelTemplateVersion = supportedExcelTemplateVersion_PREPARATORY;

            
            ////

            string connectionString = StaticSettings.ConnectionString;
            InvokeExcel.ImportFromExcel(connectionString, InstanceID, supportedExcelTemplateVersion, ExcelTemplateType.BasicTemplate);
        }

        private void exportToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture.ClearCachedData();
            string connectionString = StaticSettings.ConnectionString;
            InvokeExcel.ExportToExcel(connectionString, InstanceID, GetExcelTemplatePath(ExcelTemplateType.BasicTemplate), ExcelTemplateType.BasicTemplate);
        }

        private void downloadEnumerationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sourceFile = string.Empty;

            if (StaticSettings.DbType == DbType.SolvencyII)
                sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Concat("Enumerations\\", "excel_template_enumerations_17.xlsx"));

            if (StaticSettings.DbType == DbType.SolvencyII_Preparatory)
                sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Concat("Enumerations\\", "excel_template_enumerations_152c.xlsx"));

            //Check if the file exists
            if (!File.Exists(sourceFile))
            {
                MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(91, "Unfortunately a file is missing:"), sourceFile));
                return;
            }
            System.Diagnostics.Process.Start(sourceFile);
        }
        #endregion

        #region HelpFiles

        /// <summary>
        /// To download the RC business excel sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void rCBusinessCodeMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //For RC BusinessCode mapping  
            try
            {
                if (StaticSettings.DbType != DbType.No_container_selected)
                {
                    string connectionString = StaticSettings.ConnectionString;
                    InvokeExcel.getRcBusinessCodes(connectionString);
                }
            }
            catch(Exception ex)
            {
                Logger.WriteLog(eSeverity.Error, string.Format("There was a problem generating RC  BusinessCode Mapping excel file {0}", ex.Message));
                MessageBox.Show(string.Format("There was a problem generating RC  BusinessCode Mapping excel file. Error details logged. "));

            }

        }

        /// <summary>
        /// To download the user manual 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (_userManualFileName!=string.Empty)
            downloadHelpFiles("T4U User Manual.pdf",false);
        }

      
        private void PreparatoryS2TestXBRLInstancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "Preparatory S2 Test XBRL Instances.zip";
            downloadHelpFiles(fileName, false);

        }

        private void fullS2TestXBRLInstancesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "EIOPA_SolvencyII_XBRL_Instance_documents_2.0.0.zip";
            downloadHelpFiles(fileName, false);
        }


        private void dPMDictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "EIOPA_SolvencyII_DPM_Dictionary.xlsx";
            downloadHelpFiles2("HelpFiles\\Full\\", fileName);
        }

        private void annotatedTemplatesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            const string fileName = "EIOPA_SolvencyII_DPM_Annotated_Templates.xlsx";
            downloadHelpFiles2("HelpFiles\\Full\\", fileName);
        }

   
        private void databaseDocumentationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "T4U Database Documentation.pdf";
            downloadHelpFiles(fileName,false);
        }

        private void preparatoryS2DictionaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "EIOPA_SolvencyII_DPM_Dictionary.xlsx";
            downloadHelpFiles(fileName, false);
        }

        private void preparatoryS2AnnotatedTemplatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "EIOPA_SolvencyII_DPM_Annotated_Templates_Preparatory.xlsx";
            downloadHelpFiles(fileName, false);
        }

        /// <summary>
        /// To download Taxonomies & help files
        /// </summary>
        /// <param name="fileName">fileName</param>
        /// <param name="isTaxonomyFile">bool</param>
        protected void downloadHelpFiles(string fileName, bool isTaxonomyFile)
        {

            string sourceFile = string.Empty;
            if (isTaxonomyFile==true)
                sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Concat("Arelle\\Taxonomies\\", fileName));                
            else
                sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Concat("HelpFiles\\", fileName));
           
            //Check if the file exists
            if (!File.Exists(sourceFile))
            {
                MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(91, "Unfortunately a file is missing:"), sourceFile));
                return;
            }
            System.Diagnostics.Process.Start(sourceFile);          
            
        }

        protected void downloadHelpFiles2(string folderPath, string fileName)
        {

            string sourceFile = string.Empty;

            sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Concat(folderPath, fileName));

            //Check if the file exists
            if (!File.Exists(sourceFile))
            {
                MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(91, "Unfortunately a file is missing:"), sourceFile));
                return;
            }
            System.Diagnostics.Process.Start(sourceFile);

        }

        private void solvencyIIToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            const string fileName = "EIOPA_SolvencyII_XBRL_Taxonomy_2.0.0.zip";
            downloadHelpFiles(fileName, true);
        }

        private void cDRIVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "crd4.zip";
            downloadHelpFiles(fileName, true);
        }

        private void preparatoryS2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "EIOPA_SolvencyII_Preparatory_XBRL_Taxonomy_152c.zip";
            downloadHelpFiles(fileName, true);

        }

        private void sQLServerBackUpsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "T4Udb_Sol2_Prep.zip";
            downloadHelpFiles(fileName, false);
        }

        private void whatsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WhatsNew.Show(CheckUpdates.CheckNow, ApplicationDeployment.IsNetworkDeployed); 
        }

        private void logAndSystemDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applicationAndLogInfo();
        }

        #endregion

        #region Helper functions

        private void CloseApplication()
        {
            // Close();
            Application.Exit();
        }

        private void EnableMenuItems(bool enable)
        {

            settingsToolStripMenuItem.Enabled = enable;
            reportToolStripMenuItem.Enabled = enable;
            xBRLToolStripMenuItem.Enabled = enable;
            validationToolStripMenuItem.Enabled = enable;
            rCBusinessCodeMappingToolStripMenuItem.Enabled = enable;

        }

        private void EnableAllMenuItems(bool enable)
        {
            EnableMenuItems(enable);
            EnableExcelMenu(enable);
            fileToolStripMenuItem.Enabled = enable;
            rCBusinessCodeMappingToolStripMenuItem.Enabled = enable;
        }

        private void RefreshTemplate()
        {
            if (_selectedItem != null)
            {
                UpdateMainUserControl(false, true, true);

                SubscribeToShowDimensionalEvents();
            }
        }

        #endregion

        #region Status Bar

        /// <summary>
        /// To update the status bar
        /// </summary>
        /// <param name="newStatusMessage"></param>
        /// <param name="customStatusText"></param>

        public void UpdateStatusStrip(string newStatusMessage, StatusBarDisplay customStatusText = StatusBarDisplay.GeneralMsg)
        {
            if (customStatusText == StatusBarDisplay.GeneralMsg)
            {
                if (!string.IsNullOrEmpty(newStatusMessage))
                {
                    statusTxtGeneral.Text = newStatusMessage;
                }
                else
                {
                    statusTxtGeneral.Text = string.Empty;
                }
            }
            else if (customStatusText == StatusBarDisplay.ReportName)
            {
                string reportNameLabel = LanguageLabels.GetLabel(46, "Report name");
                UpdateStatusStripLabel(statusTxtReportName, reportNameLabel, newStatusMessage);
            }
            else if (customStatusText == StatusBarDisplay.TypeOfReport)
            {
                string typeOfReportLabel = LanguageLabels.GetLabel(47, "Type of report");
                UpdateStatusStripLabel(statusTxtTypeOfReport, typeOfReportLabel, newStatusMessage);
            }
            else if (customStatusText == StatusBarDisplay.ReportDate)
            {
                string reportDateLabel = LanguageLabels.GetLabel(48, "Report Date");
                UpdateStatusStripLabel(statusTxtReportDate, reportDateLabel, newStatusMessage);
            }
            else if (customStatusText == StatusBarDisplay.EntityIdentifier)
            {
                string entityIdentifierLabel = LanguageLabels.GetLabel(49, "Entity identifier");
                UpdateStatusStripLabel(statusTxtEntityIdentifier, entityIdentifierLabel, newStatusMessage);
            }
            else if (customStatusText == StatusBarDisplay.Currency)
            {
                string currencyLabel = LanguageLabels.GetLabel(51, "Currency");
                UpdateStatusStripLabel(statusTxtCurrency, currencyLabel, newStatusMessage);
            }

        }

        public void UpdateStatusStripLabel(ToolStripStatusLabel toolStripStatusLabel, string label, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                toolStripStatusLabel.Visible = true;
                toolStripStatusLabel.Text = label + ": " + text;
                ;
            }
            else
            {
                toolStripStatusLabel.Visible = false;
                toolStripStatusLabel.Text = string.Empty;
            }
        }

        #endregion      

        #region Handling Mutiple Instances

        public static void countInstances()
        {
            Process[] processlist = Process.GetProcesses();
            int count = 0;
            if (processlist != null)
            {
                if (ApplicationDeployment.IsNetworkDeployed)
                    count = processlist.Where(p => p.ProcessName.Contains("SolvencyII_")).Count();
                else
                    count = processlist.Where(p => p.ProcessName.Contains("SolvencyII.GUI")).Count();
            }
            if (count > 1)
            {
                MessageBox.Show("There is already other T4U application in execution. Currently we are not supporting multiple instances");
            }

        }


        #endregion

        #region About menu

        /// <summary>
        /// To display the about window
        /// </summary>
        /// <returns></returns>

        protected string AboutWindowDetails()
        {

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("{0}: {1} ({2})", LanguageLabels.GetLabel(27, "T4U version number"), Assembly.GetExecutingAssembly().GetName().Version, (SolvencyII.UI.Shared.Functions.OperatingSystemType.Is64BitOperatingSystem()) ? "64 bit" : "32 bit"));
            sb.AppendLine();

            try
            {

                if (isArelleSetupCompleted)
                    sb.AppendLine(new ArelleCmdInterface().GetArelleVersion());
                else
                    sb.AppendLine("Arelle setup is in process.");

            }
            catch (Exception)
            {

                sb.AppendLine("Problem obtaining Arelle version.");
            }

            //Modified for Click Once Version Number
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                sb.AppendLine(string.Format("{0}: {1}", LanguageLabels.GetLabel(147, "Click Once deployment version"),
                    ApplicationDeployment.CurrentDeployment.CurrentVersion));
            }
            else
            {
                //TODO:Correct label number 112
                sb.AppendLine(string.Format("{0}", LanguageLabels.GetLabel(148, "Application is not installed with click once")));
            }
            //Modified for Click Once Version Number
            string exePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SolvencyII.GUI.exe");
            sb.AppendLine(string.Format("\nPath: {0}", exePath));
            return (sb.ToString());
        }

        

        #endregion

        #region Application And System Details

        /// <summary>
        /// To get the logs & system details as a zip file in the menu - on the fyi creation
        /// </summary>

        public void applicationAndLogInfo()
        {           
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Zip Files|*.zip;*.rar";
                dialog.FileName = "details";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;
                string destFile = dialog.FileName;
                //string archiveName = String.Format("archive-{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));

                //getting the machine information
                StringBuilder sb = new StringBuilder();
                ManagementClass osClass = new ManagementClass("Win32_OperatingSystem");
                foreach (ManagementObject queryObj in osClass.GetInstances())
                {
                    foreach (PropertyData prop in queryObj.Properties)
                    {

                        sb.AppendLine(string.Format("{0}: {1} {2}", prop.Name, "=", prop.Value));
                    }
                }


                //getting the application information               
                StringBuilder applicationDetails = new StringBuilder();
                applicationDetails.AppendLine(AboutWindowDetails());
                applicationDetails.AppendLine(string.Format("{0}: {1} {2}", "supportedDataBaseVersion", "=", supportedDataBaseVersion));
                applicationDetails.AppendLine(string.Format("{0}: {1} {2}", "supportedExcelTemplateVersion", "=", supportedExcelTemplateVersion));

                // add a named entry to the zip file, using a string for content
                using (ZipFile zip = new ZipFile())
                {
                    ZipEntry entry = zip.AddEntry("Application_Details.txt", applicationDetails.ToString());
                    entry = zip.AddEntry("System_Details.txt", sb.ToString());
                    string logPath = Path.GetDirectoryName(Application.ExecutablePath);
                    logPath = Path.Combine(logPath, "Log.txt");
                    if (File.Exists(logPath))
                        zip.AddFile(logPath, @"\");
                    if (StaticSettings.ConnectionString != null && File.Exists(StaticSettings.ConnectionString))
                        if (System.IO.Path.GetExtension(StaticSettings.ConnectionString).ToUpper().EndsWith("XBRT"))
                            zip.AddFile(StaticSettings.ConnectionString, @"\");

                    zip.Save(destFile);
                }
                MessageBox.Show("Download complete.", "Completed.", MessageBoxButtons.OK, MessageBoxIcon.Information);

            
        }
        #endregion

        #region DataType Validation

        /// <summary>
        /// To update the DB validation results 
        /// </summary>
        /// <param name="dataTypeAllValidationResults"></param>
        /// <param name="message"> current status of validation </param>
        /// <param name="percentage">value</param>

        private void UpdateValidationResults(List<DataTypeValidationResult> dataTypeAllValidationResults, string message, int percentage)
        {
            if (dataTypeAllValidationResults.Count > 0)
            {
                statusTxtGeneral.Text = message;
                toolStripProgressBar1.Value = percentage; 

                foreach (DataTypeValidationResult dataTypeValidationResult in dataTypeAllValidationResults)
                {
                    dataTypeAllValidationResultsList.Add(dataTypeValidationResult);                   
                }
            }
        }

        /// <summary>
        /// progress update
        /// </summary>
        /// <param name="message"></param>
        /// <param name="percentage"></param>

        private void ProgressChanges(string message, int percentage)
        {
            statusTxtGeneral.Text = message;
            toolStripProgressBar1.Value = percentage;
        }
        private void DataTypeValidationCompleted()
        {
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
            this.splitContainer1.Panel2.Controls.RemoveAt(0);
            this.splitContainer1.Panel2.Controls.Add(errorContainerValidationView);
            errorContainerValidationView.SetObjects(dataTypeAllValidationResultsList);
            
        }

        #endregion

        #region RSS

        /// <summary>
        /// To display the RSS feed information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void toolStripStatusRSS_Click(object sender, EventArgs e)
        {
            //To display the Object list view with the RSS contents
            DisplayRSS();
        }

        /// <summary>
        /// To enable the RSS in the SolvencyII.GUI application
        /// </summary>

        protected void EnableRSS()
        {
            RssNewsReader rss = new RssNewsReader();
            List<RssFeed> rssMessages = new List<RssFeed>();
            //Get the RSS information 
            rssMessages = rss.ReadRSS();
            if (rssMessages != null)
            {
                if (rssMessages.Count > 0)
                {

                    if (RegSettings.RssCount != rssMessages.Count())
                    {
                        toolStripStatusRSS.ForeColor = System.Drawing.Color.Yellow;
                        toolStripStatusRSS.BackColor = System.Drawing.Color.Red;                       
                        
                    }
                    else
                    {
                        toolStripStatusRSS.ForeColor = System.Drawing.Color.Black;
                        toolStripStatusRSS.BackColor = SystemColors.Control;
                    }


                }
            }
        }

        /// <summary>
        /// DisplayRSS method will reterive the RSS feed information from the web link, The web link will be available in the RSSLinkInformation.xml & its deployed along with the application
        /// </summary>

        protected void DisplayRSS()
        {
            RssNewsReader rss = new RssNewsReader();
            List<RssFeed> rssMessages = new List<RssFeed>();
            //Get the RSS
            rssMessages = rss.ReadRSS();
            if (rssMessages != null)
            {
                if (rssMessages.Count > 0)
                {
                        RSS_UI validationDialog = new RSS_UI(rssMessages);
                        validationDialog.ShowDialog();
                        toolStripStatusRSS.ForeColor = System.Drawing.Color.Black;
                        toolStripStatusRSS.BackColor = SystemColors.Control;
                        RegSettings.RssCount = rssMessages.Count();
                }
            }
        }

        /// <summary>
        /// Event:- To display the RSS in the Status bar
        /// </summary>
        private void rSSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DisplayRSS();
        }

        #endregion

        private void sQLServerBackUpFullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            const string fileName = "T4U_Sol2.zip";
            downloadHelpFiles(fileName, false);
        }

        private void downloadAnEmptyBusinessExcelTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string connectionString = StaticSettings.ConnectionString;
            InvokeExcel.DownloadTemplate(connectionString, InstanceID, GetExcelTemplatePath(ExcelTemplateType.BusinessTemplate));
        }

        private void importDataToExcelBusinessTemplateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CultureInfo.CurrentCulture.ClearCachedData();

#if (FOR_UT) 
            MessageBox.Show("Import excel business template is not supported in Preparatory version.", "Excel import", MessageBoxButtons.OK, MessageBoxIcon.Information);
#else
            string connectionString = StaticSettings.ConnectionString;
            InvokeExcel.ImportFromExcel(connectionString, InstanceID, supportedExcelTemplateVersion_BUSINESS, ExcelTemplateType.BusinessTemplate);
#endif
        }

        private void licenseInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            downloadHelpFiles("Licences.html", false);
        }

    }

    /// <summary>
    /// Enums- Staus bar properties
    /// </summary>
    public enum StatusBarDisplay
    {
        ReportName,
        TypeOfReport,
        EntityIdentifier,
        GeneralMsg,
        Currency,
        ReportDate
    }
}
