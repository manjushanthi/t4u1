using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SolvencyII.Validation.Domain;
using BrightIdeasSoftware;

namespace SolvencyII.Validation.UI
{
    public partial class ArelleValidationStatus : Form
    {
        private ArelleValidationDisplayType arelleValidationDisplayType;
        string detailLog;
        public ArelleValidationStatus(IEnumerable<ValidationMessage> messsages, string _detailLog, ArelleValidationDisplayType arelleValidationDisplayType, bool isForMigration, string fileName = null) 
        {
            InitializeComponent();

            this.arelleValidationDisplayType = arelleValidationDisplayType;
            if (this.arelleValidationDisplayType == ArelleValidationDisplayType.Validation_result)
            {
                btnAbort.Visible = false;
                btnContinue.Visible = false;
                btnViewDetail.Location = btnAbort.Location;
            }
           

            this.objectListView.SetObjects(messsages);
            detailLog = _detailLog;

           
            this.Text = arelleValidationDisplayType.ToString().Replace("_", " ");
            if (this.arelleValidationDisplayType == ArelleValidationDisplayType.Import_results_for_Native_import || this.arelleValidationDisplayType == ArelleValidationDisplayType.Import_results_for_Arelle_import)
            {
                this.olvValue.IsVisible = false;                
                this.objectListView.RebuildColumns();

            }
            else
            {
                this.olvDescription.IsVisible = false;
                this.objectListView.RebuildColumns();
            }
            if (isForMigration == true)
            {
                btnAbort.Enabled = false;
                if (fileName != null)
                    this.Text = arelleValidationDisplayType.ToString().Replace("_", " ") + " - File : " + fileName;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            ArelleValidationDetailLog dlg = new ArelleValidationDetailLog(detailLog);
            dlg.Show();
        }

        private void ArelleValidationStatus_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void objectListView_SizeChanged(object sender, EventArgs e)
        {

            int column1 = (objectListView.Width * 25)/100;
            int column2 = (objectListView.Width * 40) / 100 - column1;
            int column3 = (objectListView.Width * 50) / 100 - (column1 + column2);
            int column4 = objectListView.Width -(column1 + column2 + column3);

            objectListView.Columns[0].Width = column1;
            objectListView.Columns[1].Width = column2;
            objectListView.Columns[2].Width = column3;
            objectListView.Columns[3].Width = column4;
            
        }

        private void ArelleValidationStatus_Load(object sender, EventArgs e)
        {

        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show(LanguageLabels.GetLabel(29, "Are you sure you want to delete this report?\n\nOnce deleted it cannot be restored."), LanguageLabels.GetLabel(30, "Deletion Confirmation"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            //{
            //    PutSQLData putData = new PutSQLData();
            //    putData.DeleteInstance(InstanceID);
            //    putData.Dispose();
            //    InstanceID = 0;
            //    ClearForm();
            //    EnableExcelMenu();
            //}

        }
    }
}
