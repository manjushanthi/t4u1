using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.GUI.Classes;
using Timer = System.Windows.Forms.Timer;
using System.Deployment;
using System.Deployment.Application;

namespace SolvencyII.GUI
{
    public partial class frmSplash : Form
    {
        private static frmSplash ms_frmSplash = null;
        private static Thread ms_oThread = null;
        private const int TIMER_INTERVAL = 3000;
        private Timer UpdateTimer = new Timer();

        public frmSplash()
        {
            InitializeComponent();
            UpdateTimer.Interval = TIMER_INTERVAL;
            UpdateTimer.Tick += UpdateTimer_Tick;
            UpdateTimer.Start();
            btnCheckForUpdates.Visible = true;
            //this.TopMost = true;
            try
            {
                this.BackColor = Color.White;
                // Make the background color of form display transparently.

                pnlUp.Location = new Point(pnlUp.Location.X, 70);
                System.Drawing.Drawing2D.GraphicsPath objDraw = new System.Drawing.Drawing2D.GraphicsPath();
                objDraw.AddEllipse(40, 0, this.Width - 90, this.Width - 90);
                this.Region = new Region(objDraw);

                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {

                    if (System.Deployment.Application.ApplicationDeployment.CurrentDeployment.ActivationUri.AbsolutePath.Contains("/PRE/"))
                    {
                        lblMainVersion.Text = "Alpha PRE " + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                    }
                    else
                    {
                        lblMainVersion.Text = "Alpha " + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                    }

                }
                else
                {
                    lblMainVersion.Text = "Alpha " + Application.ProductVersion.ToString();

                    btnCheckForUpdates.Visible = false;
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        void UpdateTimer_Tick(object sender, EventArgs e)
        {
            // Close this window
            CloseForm();

            // Release the timer
            UpdateTimer.Stop();
            UpdateTimer.Dispose();
        }

        // A static method to create the thread and 
        // launch the SplashScreen.
        static public void ShowSplashScreen()
        {
            // Make sure it's only launched once.
            if (ms_frmSplash != null)
                return;
            ms_oThread = new Thread(ShowForm);
            ms_oThread.IsBackground = true;
            ms_oThread.SetApartmentState(ApartmentState.STA);
            ms_oThread.Start();
        }

        // A private entry point for the thread.
        static private void ShowForm()
        {
            ms_frmSplash = new frmSplash();
            Application.Run(ms_frmSplash);
        }

        // Close the form without setting the parent.
        static public void CloseForm()
        {
            if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false)
            {
                ms_frmSplash.Close();
                ms_frmSplash.Dispose();
            }
            ms_oThread = null;
            ms_frmSplash = null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            CloseForm();
        }
        public static void ShowAbout(string Abouttext)
        {

            //Now shows about using the splash form
            ms_frmSplash = new frmSplash();
            ms_frmSplash.UpdateTimer.Stop();
            System.Drawing.Drawing2D.GraphicsPath objDraw = new System.Drawing.Drawing2D.GraphicsPath();
            objDraw.AddEllipse(0, 0, ms_frmSplash.Width, ms_frmSplash.Width);
            ms_frmSplash.Region = new Region(objDraw);

            ms_frmSplash.lblAbout.Text = Abouttext;

            ms_frmSplash.btnOK.Visible = true;
            //ms_frmSplash.btnCheckForUpdates.Visible = true;
            ms_frmSplash.TransparencyKey = Color.YellowGreen;
            ms_frmSplash.progressBar1.MarqueeAnimationSpeed = 0;
            ms_frmSplash.progressBar1.ForeColor = Color.Aqua;
            ms_frmSplash.progressBar1.Style = ProgressBarStyle.Blocks;
            ms_frmSplash.progressBar1.Value = 100;
            ms_frmSplash.progressBar1.Height = 10;

            ms_frmSplash.progressBar1.Visible = false;

            ms_frmSplash.ShowDialog();
            

        }


        private void frmSplash_DoubleClick(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void btnCheckForUpdates_Click(object sender, EventArgs e)
        {
            //this.TopMost = false;
            CheckUpdates.CheckNow();
            //this.TopMost = true;

        }
        

        private void frmSplash_Load(object sender, EventArgs e)
        {
            SetupLabels();
        }

        private void SetupLabels()
        {
            btnCheckForUpdates.Text = LanguageLabels.GetLabel(64, "Check for updates");
            btnOK.Text = LanguageLabels.GetLabel(65, "OK");
        }
    }
}
