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
    public partial class ExceptionDialog : Form
    {
        Exception exception;

        public ExceptionDialog(Exception ex)
        {
            InitializeComponent();
            exception = ex;
        }

        private void ExceptionDialog_Load(object sender, EventArgs e)
        {
            textBox1.Text = exception.Message;

            StringBuilder sb = new StringBuilder();
            Exception ex = exception;

            do
            {
                sb.Append(ex.Message).Append("\n");

            } while ((ex = ex.InnerException) != null);

            //sb.Append(exception.StackTrace);

            richTextBox1.Text = sb.ToString();
        }
    }
}
