using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SolvencyII.ExcelImportExportLib.Domain;

namespace SolvencyII.ExcelImportExportLib.UI.Dialog
{
    public partial class StatusDlg : Form
    {
        int progressPercentage;
        string message;
        bool cancelled;
        bool completed;
        bool isCancelRequest = false;
        Exception error;

        Timer timerClock = new Timer();
        List<string> tablecodes = new List<string>();

        ImportExportBehaviour behaviour;

        public bool OpenExcelFile { get; set; }

        public StatusDlg(ImportExportBehaviour _behaviour)
        {
            InitializeComponent();

            timerClock.Tick += timerClock_Tick;
            timerClock.Interval = 50;
            timerClock.Enabled = true;

            completed = false;

            behaviour = _behaviour;

        }

        void timerClock_Tick(object sender, EventArgs e)
        {
            lblProcessing.Text = message;
            progressPercentage = progressPercentage <= 100 ? progressPercentage : 100;
            lblPercent.Text = progressPercentage.ToString() + "% completed";
            prbStatus.Value = progressPercentage;

            if (tablecodes.Count() > lstStatus.Items.Count)
            {
                lock (tablecodes)
                {
                    lstStatus.Items.Clear();

                    foreach (string s in tablecodes)
                        lstStatus.Items.Add(s);

                }
            }

            if (cancelled)
            {
                timerClock.Stop();
                //MessageBox.Show(error.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                ExceptionDialog ed = new ExceptionDialog(error);
                ed.ShowDialog();
                
            }

            if(completed)
            {
                btnClose.Enabled = true;

                if (behaviour == ImportExportBehaviour.Exporting)
                    btnOpen.Enabled = true;
                btnCancel.Enabled = false;
            }
        }

        private void StatusDlg_Load(object sender, EventArgs e)
        {
            //btnCancel.Visible = false;
            btnClose.Enabled = false;

            if (behaviour == ImportExportBehaviour.Importing)
                btnOpen.Visible = false;
            else
                btnOpen.Enabled = false;
        }

        public void CompletedHandler(object sender, AsyncCompletedEventArgs e)
        {
            message = (string)e.UserState;
            cancelled = e.Cancelled;
            error = e.Error;
            completed = true;
            if (isCancelRequest)
                message = "Cancelled";
          
        }

        public void ProgressChangedHandler(object sender, ProgressChangedEventArgs e)
        {
            progressPercentage = e.ProgressPercentage;
            message = "Processing " + (string)e.UserState;

            lock (tablecodes)
            {
                tablecodes.Add((string)e.UserState);
            }
            if (isCancelRequest)
                message = "Canceling........, Please wait";
        }

        public void GranuleProgressChangedHandler(object sender, ProgressChangedEventArgs e)
        {
            progressPercentage = e.ProgressPercentage;
            message = "Processing " + (string)e.UserState;
            if (isCancelRequest)
                message = "Canceling........, Please wait";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenExcelFile = true;

            this.Close();
        }

        public event EventHandler<EventArgs> Canceled;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to cancel this operation", " Cancel Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                EventHandler<EventArgs> ea = Canceled;
                if (ea != null)
                {
                    isCancelRequest = true;
                    message = "Canceling........, Please wait";
                    btnCancel.Enabled = false;
                    ea(this, e);
                    //this.Close();
                }
            }

        }
    }
}
