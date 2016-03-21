using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolvencyII.ExcelImportExportLib.UI.Dialog
{

    public partial class TablesListDlg3 : Form
    {
        private string[] tableCodes;

        public string[] TableCodes
        {
            get
            {
                return tableCodes;
            }
            set
            {
                tableCodes = value;
            }
        }

        private string[] selectedTableCodes;

        public string[] SelectedTableCodes
        {
            get
            {
                return selectedTableCodes;
            }
        }


        public TablesListDlg3(string[] tableCodes, string[] otherTableCodes)
        {
           InitializeComponent();

            TableCodes = tableCodes;

            if (lstOtherTables != null)
            {
                foreach (string s in otherTableCodes)
                {
                    lstOtherTables.Items.Add(s);
                }
            }
        }

        private void TablesListDlg3_Load(object sender, EventArgs e)
        {
            if (TableCodes != null)
            {
                foreach (string s in tableCodes)
                    clbTablesList.Items.Add(s);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (clbTablesList.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select the sheet to proceed.");
                return;
            }

            selectedTableCodes = new string[clbTablesList.CheckedItems.Count];


            int index = 0;
            foreach (object itemChecked in clbTablesList.CheckedItems)
            {
                selectedTableCodes[index++] = itemChecked.ToString();
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;

            this.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < clbTablesList.Items.Count; i++)
                    clbTablesList.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < clbTablesList.Items.Count; i++)
                    clbTablesList.SetItemChecked(i, false);
            }
        }
    }
}
