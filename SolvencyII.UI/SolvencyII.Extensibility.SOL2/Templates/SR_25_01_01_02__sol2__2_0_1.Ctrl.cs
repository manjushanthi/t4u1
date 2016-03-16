using System;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.UserControls 
{ 
    // UserControlGenerator version: 2015.4.15.1
    [DefaultBindingProperty("UserData")]
    public partial class SR_25_01_01_02__sol2__2_0_1_ctrl : SolvencyClosedRowControl, IClosedRowControl 
    { 

        private T__SR_25_01_01_02__sol2__2_0_1 _rowData0;
        private bool populating = false;

        [Bindable(true)]
        public object[] UserData
        {
            get {return ReturnUserData();}
            set{if (value != null){
                if (value.Length == 1){ 
                _rowData0 = (T__SR_25_01_01_02__sol2__2_0_1) value[0];
                 populating = true; 
                 PopulateCombos(); 
                 PopulateSingleControl(_rowData0);
                 populating = false;
                }} } 
        }

        private void DataChanged(object sender, string colName){if(!populating) { GatherData(); SetToolText(sender); }}

        private void GatherData()
        {
            object userDataObject0 = _rowData0;GatherSingleControl(ref userDataObject0);
        }

                private object[] ReturnUserData(){object[] data;
                    data = new object[1];
                    data[0] = _rowData0;
                    return data;}

        public event GenericDelegates.Response AddControl;
        private void btnAdd_Click(object sender, EventArgs e) { if (AddControl != null) AddControl(); }
        public event GenericDelegates.ListLongs DelControl;
        private void btnDelControl_Click(List<long> res) { if (DelControl != null) DelControl(res); }
        public event GenericDelegates.Response DelControlDR;
        private void btnDel_Click(object sender, EventArgs e) { if (DelControlDR != null) DelControlDR(); }

        public event EventHandler AddRow;
        private void btnAddRow_Click(object sender, EventArgs e) { if (AddRow != null) AddRow(sender, e); }
        public event EventHandler AddCol;
        private void btnAddCol_Click(object sender, EventArgs e) { if (AddCol != null) AddCol(sender, e); }
        public event EventHandler DelRow;
        private void btnDelRow_Click(object sender, EventArgs e) { if (DelRow != null) DelRow(sender, e); }
        public event EventHandler DelCol;
        private void btnDelCol_Click(object sender, EventArgs e) { if (DelCol != null) DelCol(sender, e); }

        public List<long> PK_IDs { get; set; }

        public SR_25_01_01_02__sol2__2_0_1_ctrl()
        {
            InitializeComponent();
            { 
                spltMain.SplitterWidth = 1; 
                splitContainerColTitles.SplitterWidth = 1; 
                splitContainerRowTitles.SplitterWidth = 1; 
            } 
            PK_IDs = new List<long> {  };
            Dock = DockStyle.Fill;
            if (!DesignMode){splitContainerRowTitles.Panel2.Scroll += Panel2_Scroll;spltMain.SizeChanged += PanelResized;splitContainerRowTitles.Panel2.MouseWheel += MouseWheelScroll;}
        }
        private void BoundControl_Load(object sender, EventArgs e)
        {
            try{
                 if (!DesignMode)
                     {SetupEvents(ref DelControlDR, ref AddControl);}
            }catch (NullReferenceException){
                // This is a DataRepeater event.
            }
            this.SetupDataChangedEvents(DataChanged, GetDataControls());
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
