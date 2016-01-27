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
    public partial class TablesListDlg2 : Form
    {
        private string[] nonEmptyTableCodes;

        public string[] NonEmptyTableCodes
        {
            get
            {
                return nonEmptyTableCodes;
            }
            set
            {
                nonEmptyTableCodes = value;
            }
        }

        private string[] emptyTableCodes;

        public string[] EmptyTableCodes
        {
            get
            {
                return emptyTableCodes;
            }
            set
            {
                emptyTableCodes = value;
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

        public bool SelectedAll { get; set; }

        public TablesListDlg2(string[] nonEmptyTableCodes, string[] emptyTableCodes)
        {
            InitializeComponent();

            NonEmptyTableCodes = nonEmptyTableCodes;
            EmptyTableCodes = emptyTableCodes;
        }

        private void TablesListDlg2_Load(object sender, EventArgs e)
        {
            if (NonEmptyTableCodes != null)
            {
                foreach (string s in nonEmptyTableCodes)
                    clbNonEmptyTable.Items.Add(s);

                foreach (string s in emptyTableCodes)
                    clbEmptyTable.Items.Add(s);
            }
        }

        private void chkSelectNonEmptyTables_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectNonEmptyTables.Checked)
            {
                for (int i = 0; i < clbNonEmptyTable.Items.Count; i++)
                    clbNonEmptyTable.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < clbNonEmptyTable.Items.Count; i++)
                    clbNonEmptyTable.SetItemChecked(i, false);
            }
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < clbNonEmptyTable.Items.Count; i++)
                    clbNonEmptyTable.SetItemChecked(i, true);

                for (int i = 0; i < clbEmptyTable.Items.Count; i++)
                    clbEmptyTable.SetItemChecked(i, true);
            }
            else
            {
                for (int i = 0; i < clbNonEmptyTable.Items.Count; i++)
                    clbNonEmptyTable.SetItemChecked(i, false);

                for (int i = 0; i < clbEmptyTable.Items.Count; i++)
                    clbEmptyTable.SetItemChecked(i, false);

                chkSelectNonEmptyTables.Checked = false;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            selectedTableCodes = new string[clbNonEmptyTable.CheckedItems.Count + clbEmptyTable.CheckedItems.Count];

            if (clbNonEmptyTable.CheckedItems.Count + clbEmptyTable.CheckedItems.Count == 0)
            {
                MessageBox.Show("Please select the sheet to proceed.");
                return;
            }

            int index = 0;
            foreach (object itemChecked in clbNonEmptyTable.CheckedItems)
            {
                selectedTableCodes[index++] = itemChecked.ToString();
            }

            foreach (object itemChecked in clbEmptyTable.CheckedItems)
            {
                selectedTableCodes[index++] = itemChecked.ToString();
            }

            SelectedAll = chkSelectAll.Checked;

            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
