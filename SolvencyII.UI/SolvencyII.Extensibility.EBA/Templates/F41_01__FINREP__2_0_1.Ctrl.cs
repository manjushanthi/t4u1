using System;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using System.Collections.Generic;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.UserControls 
{ 
       // UserControlGenerator version: 2015.2.20.0
    public partial class F41_01__FINREP__2_0_1_ctrl : SolvencyClosedRowControl, IClosedRowControl 
    { 

        public event GenericDelegates.Response AddControl;
        private void btnAdd_Click(object sender, EventArgs e) { if (AddControl != null) AddControl(); }
        public event GenericDelegates.ListLongs DelControl;public event GenericDelegates.Response DelControlDR;
        private void btnDel_Click(object sender, EventArgs e) { if (DelControl != null) DelControl(PK_IDs); }

        public event EventHandler AddRow;
        private void btnAddRow_Click(object sender, EventArgs e) { if (AddRow != null) AddRow(sender, e); }
        public event EventHandler AddCol;
        private void btnAddCol_Click(object sender, EventArgs e) { if (AddCol != null) AddCol(sender, e); }
        public event EventHandler DelRow;
        private void btnDelRow_Click(object sender, EventArgs e) { if (DelRow != null) DelRow(sender, e); }
        public event EventHandler DelCol;
        private void btnDelCol_Click(object sender, EventArgs e) { if (DelCol != null) DelCol(sender, e); }

        public List<long> PK_IDs { get; set; } public void SetPK_ID(long PK_ID) { }public bool UpdateData(out string errorText, List<ISolvencyPageControl> nPageControls) { errorText = ""; return true; }

        public F41_01__FINREP__2_0_1_ctrl()
        {
            InitializeComponent();
            { 
                spltMain.SplitterWidth = 1; 
                splitContainerColTitles.SplitterWidth = 1; 
                splitContainerRowTitles.SplitterWidth = 1; 
            } 
            PK_IDs = new List<long> { 0 };
            Dock = DockStyle.Fill;
            if (!DesignMode){splitContainerRowTitles.Panel2.Scroll += Panel2_Scroll;splitContainerRowTitles.Panel2.SizeChanged += PanelResized;splitContainerRowTitles.Panel2.MouseWheel += MouseWheelScroll;}
        }
        private void MouseWheelScroll(object sender, MouseEventArgs e){PositionScrollBars();}

        private void PanelResized(object sender, EventArgs e){
            splitContainerColTitles.Panel2.HorizontalScroll.Minimum = splitContainerRowTitles.Panel2.HorizontalScroll.Minimum;
            splitContainerColTitles.Panel2.HorizontalScroll.Maximum = splitContainerRowTitles.Panel2.HorizontalScroll.Maximum;
            splitContainerRowTitles.Panel1.VerticalScroll.Minimum = splitContainerRowTitles.Panel2.VerticalScroll.Minimum;
            splitContainerRowTitles.Panel1.VerticalScroll.Maximum = splitContainerRowTitles.Panel2.VerticalScroll.Maximum;
            PositionScrollBars();}

        private void PositionScrollBars(){// winForms bug work around
            int posX = -splitContainerRowTitles.Panel2.AutoScrollPosition.X;if (posX == 0) posX = 1;
            int posY = -splitContainerRowTitles.Panel2.AutoScrollPosition.Y;if (posY == 0) posY = 1;
            splitContainerRowTitles.Panel2.Focus();
            splitContainerColTitles.Panel2.HorizontalScroll.Value = posX;
            splitContainerRowTitles.Panel1.VerticalScroll.Value = posY;}

        private void Panel2_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e){
            int pos = e.NewValue; if (pos <= 0) pos = 1;
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll) splitContainerColTitles.Panel2.HorizontalScroll.Value = pos;
            else splitContainerRowTitles.Panel1.VerticalScroll.Value = pos;
            splitContainerRowTitles.Panel2.Focus();}

   } 
} 
