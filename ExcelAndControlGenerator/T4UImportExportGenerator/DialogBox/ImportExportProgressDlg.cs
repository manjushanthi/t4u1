using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace T4UImportExportGenerator.DialogBox
{
    public partial class ImportExportProgressDlg : Form
    {
        int progressPercentage;
        string message;
        bool cancelled;
        Exception error;

        Timer timerClock = new Timer();
        List<string> tablecodes = new List<string>();

        protected object objectLock = new Object();

        public ImportExportProgressDlg()
        {
            InitializeComponent();

            timerClock.Tick += timerClock_Tick;
            timerClock.Interval = 100;
            timerClock.Enabled = true;

        }

        void timerClock_Tick(object sender, EventArgs e)
        {
            lblProcessing.Text = message;
            lblPercent.Text = progressPercentage.ToString() + "% completed";
            prbStatus.Value = progressPercentage;

            if (tablecodes.Count() > lstStatus.Items.Count)
            {
                lstStatus.Items.Clear();

                lock (objectLock)
                {
                    foreach (string s in tablecodes)
                    {
                        lstStatus.Items.Add(s);
                    }
                }
            }

            if (cancelled)
            {
                timerClock.Stop();
                MessageBox.Show(error.Message + "\n" + error.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //ExceptionDialog ed = new ExceptionDialog(error);
                //ed.ShowDialog();
            }
        }

        public void CompletedHandler(object sender, AsyncCompletedEventArgs e)
        {
            message = (string)e.UserState;
            cancelled = e.Cancelled;
            error = e.Error;

            //Wait for some time before stopping the clock
            System.Threading.Thread.Sleep(500);
            timerClock.Stop();
        }

        public void ProgressChangedHandler(object sender, ProgressChangedEventArgs e)
        {
            progressPercentage = e.ProgressPercentage;
            message = "Processing " + (string)e.UserState;

            lock (objectLock)
            {
                tablecodes.Add((string)e.UserState);
            }
        }
    }
}
