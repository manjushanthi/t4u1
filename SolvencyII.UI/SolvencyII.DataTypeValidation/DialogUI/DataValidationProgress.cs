using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolvencyII.DataTypeValidation.DialogUI
{
    public partial class DataValidationProgress : Form
    {
       
        #region Properties

        public string ProcessingStep
        {
            set { lblProgressStep.Text = value; }
        }

        public int ProgressValue
        {
            set { progressBar1.Value = value; }
        }
        public string Percent
        {
            set { lblPercent.Text = value; }
        }

        
        #endregion

        #region Methods

        public DataValidationProgress()
        {
            InitializeComponent();
        }

        #endregion

        #region EVENTS

        public event EventHandler<EventArgs> Canceled;

        private void btnCancel_Click(object sender, EventArgs e)
        {           
            EventHandler<EventArgs> ea = Canceled;          
            if (ea != null)
            {
                btnCancel.Enabled = false;
                lblProcessing.Text = "Canceling........";
                ea(this, e);
            }               

        }
        

        #endregion

       
    }
}
