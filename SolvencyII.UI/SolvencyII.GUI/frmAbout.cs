using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.GUI.Classes;
using SolvencyII.UI.Shared.Arelle;
using System;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace SolvencyII.GUI
{
    public partial class frmAbout : Form
    {
        private bool _isArelleInstalledCompleted = false;
        private string _supportedDataBaseVersion = string.Empty;
        private string _supportedExcelTemplateVersion = string.Empty;
        BackgroundWorker worker = null;

        private string _t4UVersionDetails = string.Empty;
        private string _arelleVersionDetails = string.Empty;
        private string _installationType = string.Empty;
        private string _installatedPath = string.Empty;
        private string _frameworkVersion = string.Empty;

        private string _supportedDataBaseVersionPREP = string.Empty;
        private string _supportedExcelTemplateVersionPREP = string.Empty;

        private string _supportedDataBaseVersionCRD = string.Empty;

        public frmAbout()
        {
            InitializeComponent();
        }

        public frmAbout(bool isArelleInstalledCompleted, string supportedDataBaseVersion, string supportedExcelTemplateVersion, string supportedDataBaseVersionPREP, string supportedExcelTemplateVersionPREP, string supportedDataBaseVersionCRD)
        {
            InitializeComponent();
            _isArelleInstalledCompleted = isArelleInstalledCompleted;
            _supportedDataBaseVersion= supportedDataBaseVersion;
            _supportedExcelTemplateVersion = supportedExcelTemplateVersion;
            _supportedDataBaseVersionPREP = supportedDataBaseVersionPREP;
            _supportedExcelTemplateVersionPREP = supportedExcelTemplateVersionPREP;
            _supportedDataBaseVersionCRD = supportedDataBaseVersionCRD;

            if (!ApplicationDeployment.IsNetworkDeployed)
                btnUpdate.Enabled = false;

            setVisibleStatus(false);
            pbLoader.Visible = true;

            worker = new BackgroundWorker();
            worker.DoWork += backgroundWorker_DoWork;
            worker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            
            
        }

        private void setVisibleStatus(bool visibleStatus)
        {
            pbInstalledpath.Visible = visibleStatus;
            lblT4Uversion.Visible = visibleStatus;
            lblArelleVersion.Visible = visibleStatus;
            lblClickOnceVersion.Visible = visibleStatus;
            txtpath.Visible = visibleStatus;
            lblDatabaseVersion.Visible = visibleStatus;
            lblExcelTemplateVersion.Visible = visibleStatus;
            lblFrameWorkVersion.Visible = visibleStatus;
            lblDatabaseVersionPREP.Visible = visibleStatus;
            lblExcelTemplateVersionPREP.Visible = visibleStatus;
            //lblDatabaseVersionCRD.Visible = visibleStatus;
               
        
        }

        /// <summary>
        /// Background worker to collect the information for the About Dialog 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
             getDetails();

        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbLoader.Visible = false;
            SetWindowDetails();
           
            setVisibleStatus(true);
        }

        /// <summary>
        /// To gather the informations required for the About dialog
        /// </summary>

        private void getDetails()
        {

            //MessageBox.Show("getDetails");

             try
             {

                 if (_isArelleInstalledCompleted)
                     _arelleVersionDetails = string.Format("{0} {1} {2}", "Arelle Version", ">>>", new ArelleCmdInterface().GetArelleVersion());
                 else
                     _arelleVersionDetails = "Arelle setup is in process.";
             }
             catch (Exception e)
             {
                 MessageBox.Show("Problem obtaining Arelle version." + e.ToString());
                 _arelleVersionDetails = "Problem obtaining Arelle version.";
             }
             try
             {
                 _t4UVersionDetails = string.Format("{0} {1} {2} ({3})", LanguageLabels.GetLabel(27, "T4U version number"), ">>>", Assembly.GetExecutingAssembly().GetName().Version, (UI.Shared.Functions.OperatingSystemType.Is64BitOperatingSystem()) ? "64 bit" : "32 bit");

                 if (ApplicationDeployment.IsNetworkDeployed)
                     _installationType = string.Format("{0} {1} {2}", LanguageLabels.GetLabel(147, "Click Once deployment version"), ">>>",
                         ApplicationDeployment.CurrentDeployment.CurrentVersion);
                 else
                     _installationType = string.Format("{0}", LanguageLabels.GetLabel(148, "Application is not installed with click once"));

                 _installatedPath = string.Format("{0} {1} {2}", "Installed Path", ">>>", Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SolvencyII.GUI.exe"));

                Assembly asb = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SolvencyII.GUI.exe"));
                if (asb != null)
                {
                    AssemblyName[] referencedAssemblies = asb.GetReferencedAssemblies();
                    foreach (AssemblyName a in referencedAssemblies)
                    {
                        if (a.Name == "System.Core")                        
                             _frameworkVersion = string.Format("{0} {1} {2}","Dotnet Framework Version ",">>>", a.Version.ToString());                        

                    }
                }
                
             }
             catch (Exception e)
             {
                 MessageBox.Show("Problem obtaining about window details." + e.ToString());
             }

        }

        /// <summary>
        /// Set the About dialog's Text content
        /// </summary>

        protected void SetWindowDetails()
        {

            lblT4Uversion.Text = _t4UVersionDetails; 
            lblArelleVersion.Text=_arelleVersionDetails;
            lblClickOnceVersion.Text = _installationType;
            txtpath.Text = _installatedPath;            
            lblFrameWorkVersion.Text = _frameworkVersion;


            #if (FOR_NCA)
            lblDatabaseVersion.Text = string.Format("{0} {1} {2}", "Supported DataBase Version ", ">>>", _supportedDataBaseVersion);
            lblExcelTemplateVersion.Text = string.Format("{0} {1} {2}", "Supported Excel Template Version ", ">>>", _supportedExcelTemplateVersion);
            lblDatabaseVersionPREP.Text = string.Format("{0} {1} {2}", "Supported DataBase Version (Preparatory)", ">>>", _supportedDataBaseVersionPREP);
            lblExcelTemplateVersionPREP.Text = string.Format("{0} {1} {2}", "Supported Excel Template Version (Preparatory)", ">>>", _supportedExcelTemplateVersionPREP);
            //lblDatabaseVersionCRD.Text = string.Format("{0} {1} {2}", "Supported DataBase Version (CRD)", ">>>", _supportedDataBaseVersionCRD);
            #elif  (FOR_UT)
               lblDatabaseVersion.Text = string.Format("{0} {1} {2}", "Supported DataBase Version ", ">>>", "Not applicable ");
               lblExcelTemplateVersion.Text = string.Format("{0} {1} {2}", "Supported Excel Template Version ", ">>>", "Not applicable ");
               lblDatabaseVersionPREP.Text = string.Format("{0} {1} {2}", "Supported DataBase Version (Preparatory)", ">>>", _supportedDataBaseVersionPREP);
               lblExcelTemplateVersionPREP.Text = string.Format("{0} {1} {2}", "Supported Excel Template Version (Preparatory)", ">>>", _supportedExcelTemplateVersionPREP); 
              // lblDatabaseVersionCRD.Text = string.Format("{0} {1} {2}", "Supported Excel Template Version (CRD)", ">>>", "Not applicable ");
#endif

        }        

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ApplicationDeployment.IsNetworkDeployed) 
            CheckUpdates.CheckNow();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// To show the application installed path on-clicking the folder icon
        /// </summary>
      
        private void pbInstalledpath_Click(object sender, EventArgs e)
        {
            string exePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SolvencyII.GUI.exe");
            if (exePath != null && File.Exists(exePath))
                Process.Start(Path.GetDirectoryName(exePath));
        }
    }
}
