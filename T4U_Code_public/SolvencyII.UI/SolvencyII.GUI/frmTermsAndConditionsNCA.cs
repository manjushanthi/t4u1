using SolvencyII.GUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolvencyII.GUI
{
    public partial class frmTermsAndConditionsNCA : Form
    {
        private bool isLicenceAccepted = false;
        public frmTermsAndConditionsNCA()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(frmLicence_Closed);
        }

        private void chkBoxAgree_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxAgree.Checked)
            {
                btnAccept.Enabled = true;
                btnAccept.Focus();
            }
            else
            {
                btnAccept.Enabled = false;
                
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            isLicenceAccepted = true;
            this.Close();            
        }

        void frmLicence_Closed(object sender, FormClosedEventArgs e)
        {
            if (isLicenceAccepted == true)           
                this.Close();           
            else
                Environment.Exit(Environment.ExitCode);
        }

        
    }
}
