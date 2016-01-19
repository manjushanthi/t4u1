using BrightIdeasSoftware;
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
    public partial class ValidationDialog : Form
    {     
        
        public ValidationDialog(IEnumerable<ExcelExportValidationMessage> messsages)
        {
            InitializeComponent();
            this.objectListView.SetObjects(messsages);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
