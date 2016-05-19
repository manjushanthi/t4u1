using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ucIntegration
{
    public partial class frmHeaderTest : Form
    {
        public frmHeaderTest()
        {
            InitializeComponent();
        }
        public Panel GetPanel { get { return pnlHeader; } }
    }
}
