using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolvencyII.GUI.RSS
{
    public partial class RSS_UI : Form
    {
        public RSS_UI(IEnumerable<RssFeed> messsages)
        {
            InitializeComponent();
            this.objectListView.SetObjects(messsages.Reverse());
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
