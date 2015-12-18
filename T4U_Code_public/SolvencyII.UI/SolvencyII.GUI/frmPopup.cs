using System;
using System.Windows.Forms;

namespace SolvencyII.GUI
{
    public partial class frmPopup : Form
    {
        private readonly string _title;
        private readonly string _message;

        public frmPopup(string title, string message)
        {
            InitializeComponent();
            _title = title;
            _message = message;
        }

        private void frmPopup_Load(object sender, EventArgs e)
        {
            Text = _title;
            richTextBox1.Text = _message;
        }


    }
}
