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
    public partial class S_25_03_03__sol2__1_5_2_c_ctrl : SolvencyClosedRowControl, IClosedRowControl 
    { 

        private T__S_25_03_03_02__sol2__1_5_2_c _rowData0;
        private T__S_25_03_03_03__sol2__1_5_2_c _rowData1;
        private bool populating = false;

        [Bindable(true)]
        public object[] UserData
        {
            get {return ReturnUserData();}
            set{if (value != null){
                if (value.Length == 2){ 
                _rowData0 = (T__S_25_03_03_02__sol2__1_5_2_c) value[0];
                _rowData1 = (T__S_25_03_03_03__sol2__1_5_2_c) value[1];
                 populating = true; 
                 PopulateCombos(); 
                 PopulateSingleControl(_rowData0);
                 PopulateSingleControl(_rowData1);
                 populating = false;
                }} } 
        }

        private void DataChanged(object sender, string colName){if(!populating) { GatherData(); SetToolText(sender); }}

        private void GatherData()
        {
            object userDataObject0 = _rowData0;GatherSingleControl(ref userDataObject0);
            object userDataObject1 = _rowData1;GatherSingleControl(ref userDataObject1);
        }

                private object[] ReturnUserData(){object[] data;
                    data = new object[2];
                    data[0] = _rowData0;
                    data[1] = _rowData1;
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

        public S_25_03_03__sol2__1_5_2_c_ctrl()
        {
            InitializeComponent();
            PK_IDs = new List<long> {  };
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
   } 
} 
