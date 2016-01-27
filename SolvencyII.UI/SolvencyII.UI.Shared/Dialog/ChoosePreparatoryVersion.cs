
using SolvencyII.UI.Shared.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolvencyII.UI.Shared.Dialog
{
    public partial class SelectedPreparatoryVersion : Form
    {
        public string selectedVersion;
        public SelectedPreparatoryVersion()
        {
            InitializeComponent();
            LoadOptions();
        }

        public void LoadOptions()
        {
            int count = 1;
            foreach (PreParatoryVersions val in Enum.GetValues(typeof(PreParatoryVersions)))
            {
                if (PreParatoryVersions.NotApplicable != val)
                {
                    RadioButton rbPrepVersion = new RadioButton();
                    rbPrepVersion.Location = new Point(25, 25 * count);
                    rbPrepVersion.Name = (val).ToString().Trim();
                    rbPrepVersion.Text = SolvencyII.UI.Shared.Classes.Extensions.EnumExtendedName(val);
                    rbPrepVersion.Width = 100;
                    rbPrepVersion.CheckedChanged += new EventHandler(CheckedChanged);
                    gbPreparatoryVersion.Controls.Add(rbPrepVersion);
                    gbPreparatoryVersion.Controls[rbPrepVersion.Name].Show();
                }
                ++count;
            }
            btnOK.Enabled = false;

        }

        protected void CheckedChanged(object sender, EventArgs e)
        {
            if( btnOK.Enabled == false)
                btnOK.Enabled = true;
            selectedVersion = ((RadioButton)sender).Name;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {            
                DialogResult = DialogResult.OK;
                this.Close();            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {           
                DialogResult = DialogResult.Cancel;
                Close();           
        }

    }


}
