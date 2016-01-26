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
    public partial class S_27_01_04__sol2__2_0_ctrl : SolvencyClosedRowControl, IClosedRowControl 
    { 

        private T__S_27_01_04_01__sol2__2_0 _rowData0;
        private T__S_27_01_04_02__sol2__2_0 _rowData1;
        private T__S_27_01_04_03__sol2__2_0 _rowData2;
        private T__S_27_01_04_04__sol2__2_0 _rowData3;
        private T__S_27_01_04_05__sol2__2_0 _rowData4;
        private T__S_27_01_04_06__sol2__2_0 _rowData5;
        private T__S_27_01_04_07__sol2__2_0 _rowData6;
        private T__S_27_01_04_08__sol2__2_0 _rowData7;
        private T__S_27_01_04_09__sol2__2_0 _rowData8;
        private T__S_27_01_04_10__sol2__2_0 _rowData9;
        private T__S_27_01_04_11__sol2__2_0 _rowData10;
        private T__S_27_01_04_12__sol2__2_0 _rowData11;
        private T__S_27_01_04_13__sol2__2_0 _rowData12;
        private T__S_27_01_04_14__sol2__2_0 _rowData13;
        private T__S_27_01_04_15__sol2__2_0 _rowData14;
        private T__S_27_01_04_16__sol2__2_0 _rowData15;
        private T__S_27_01_04_17__sol2__2_0 _rowData16;
        private T__S_27_01_04_18__sol2__2_0 _rowData17;
        private T__S_27_01_04_19__sol2__2_0 _rowData18;
        private T__S_27_01_04_20__sol2__2_0 _rowData19;
        private T__S_27_01_04_21__sol2__2_0 _rowData20;
        private T__S_27_01_04_23__sol2__2_0 _rowData21;
        private T__S_27_01_04_26__sol2__2_0 _rowData22;
        private T__S_27_01_04_22__sol2__2_0 _rowData23;
        private T__S_27_01_04_24__sol2__2_0 _rowData24;
        private T__S_27_01_04_25__sol2__2_0 _rowData25;
        private bool populating = false;

        [Bindable(true)]
        public object[] UserData
        {
            get {return ReturnUserData();}
            set{if (value != null){
                if (value.Length == 26){ 
                _rowData0 = (T__S_27_01_04_01__sol2__2_0) value[0];
                _rowData1 = (T__S_27_01_04_02__sol2__2_0) value[1];
                _rowData2 = (T__S_27_01_04_03__sol2__2_0) value[2];
                _rowData3 = (T__S_27_01_04_04__sol2__2_0) value[3];
                _rowData4 = (T__S_27_01_04_05__sol2__2_0) value[4];
                _rowData5 = (T__S_27_01_04_06__sol2__2_0) value[5];
                _rowData6 = (T__S_27_01_04_07__sol2__2_0) value[6];
                _rowData7 = (T__S_27_01_04_08__sol2__2_0) value[7];
                _rowData8 = (T__S_27_01_04_09__sol2__2_0) value[8];
                _rowData9 = (T__S_27_01_04_10__sol2__2_0) value[9];
                _rowData10 = (T__S_27_01_04_11__sol2__2_0) value[10];
                _rowData11 = (T__S_27_01_04_12__sol2__2_0) value[11];
                _rowData12 = (T__S_27_01_04_13__sol2__2_0) value[12];
                _rowData13 = (T__S_27_01_04_14__sol2__2_0) value[13];
                _rowData14 = (T__S_27_01_04_15__sol2__2_0) value[14];
                _rowData15 = (T__S_27_01_04_16__sol2__2_0) value[15];
                _rowData16 = (T__S_27_01_04_17__sol2__2_0) value[16];
                _rowData17 = (T__S_27_01_04_18__sol2__2_0) value[17];
                _rowData18 = (T__S_27_01_04_19__sol2__2_0) value[18];
                _rowData19 = (T__S_27_01_04_20__sol2__2_0) value[19];
                _rowData20 = (T__S_27_01_04_21__sol2__2_0) value[20];
                _rowData21 = (T__S_27_01_04_23__sol2__2_0) value[21];
                _rowData22 = (T__S_27_01_04_26__sol2__2_0) value[22];
                _rowData23 = (T__S_27_01_04_22__sol2__2_0) value[23];
                _rowData24 = (T__S_27_01_04_24__sol2__2_0) value[24];
                _rowData25 = (T__S_27_01_04_25__sol2__2_0) value[25];
                 populating = true; 
                 PopulateCombos(); 
                 PopulateSingleControl(_rowData0);
                 PopulateSingleControl(_rowData1);
                 PopulateSingleControl(_rowData2);
                 PopulateSingleControl(_rowData3);
                 PopulateSingleControl(_rowData4);
                 PopulateSingleControl(_rowData5);
                 PopulateSingleControl(_rowData6);
                 PopulateSingleControl(_rowData7);
                 PopulateSingleControl(_rowData8);
                 PopulateSingleControl(_rowData9);
                 PopulateSingleControl(_rowData10);
                 PopulateSingleControl(_rowData11);
                 PopulateSingleControl(_rowData12);
                 PopulateSingleControl(_rowData13);
                 PopulateSingleControl(_rowData14);
                 PopulateSingleControl(_rowData15);
                 PopulateSingleControl(_rowData16);
                 PopulateSingleControl(_rowData17);
                 PopulateSingleControl(_rowData18);
                 PopulateSingleControl(_rowData19);
                 PopulateSingleControl(_rowData20);
                 PopulateSingleControl(_rowData21);
                 PopulateSingleControl(_rowData22);
                 PopulateSingleControl(_rowData23);
                 PopulateSingleControl(_rowData24);
                 PopulateSingleControl(_rowData25);
                 populating = false;
                }} } 
        }

        private void DataChanged(object sender, string colName){if(!populating) { GatherData(); SetToolText(sender); }}

        private void GatherData()
        {
            object userDataObject0 = _rowData0;GatherSingleControl(ref userDataObject0);
            object userDataObject1 = _rowData1;GatherSingleControl(ref userDataObject1);
            object userDataObject2 = _rowData2;GatherSingleControl(ref userDataObject2);
            object userDataObject3 = _rowData3;GatherSingleControl(ref userDataObject3);
            object userDataObject4 = _rowData4;GatherSingleControl(ref userDataObject4);
            object userDataObject5 = _rowData5;GatherSingleControl(ref userDataObject5);
            object userDataObject6 = _rowData6;GatherSingleControl(ref userDataObject6);
            object userDataObject7 = _rowData7;GatherSingleControl(ref userDataObject7);
            object userDataObject8 = _rowData8;GatherSingleControl(ref userDataObject8);
            object userDataObject9 = _rowData9;GatherSingleControl(ref userDataObject9);
            object userDataObject10 = _rowData10;GatherSingleControl(ref userDataObject10);
            object userDataObject11 = _rowData11;GatherSingleControl(ref userDataObject11);
            object userDataObject12 = _rowData12;GatherSingleControl(ref userDataObject12);
            object userDataObject13 = _rowData13;GatherSingleControl(ref userDataObject13);
            object userDataObject14 = _rowData14;GatherSingleControl(ref userDataObject14);
            object userDataObject15 = _rowData15;GatherSingleControl(ref userDataObject15);
            object userDataObject16 = _rowData16;GatherSingleControl(ref userDataObject16);
            object userDataObject17 = _rowData17;GatherSingleControl(ref userDataObject17);
            object userDataObject18 = _rowData18;GatherSingleControl(ref userDataObject18);
            object userDataObject19 = _rowData19;GatherSingleControl(ref userDataObject19);
            object userDataObject20 = _rowData20;GatherSingleControl(ref userDataObject20);
            object userDataObject21 = _rowData21;GatherSingleControl(ref userDataObject21);
            object userDataObject22 = _rowData22;GatherSingleControl(ref userDataObject22);
            object userDataObject23 = _rowData23;GatherSingleControl(ref userDataObject23);
            object userDataObject24 = _rowData24;GatherSingleControl(ref userDataObject24);
            object userDataObject25 = _rowData25;GatherSingleControl(ref userDataObject25);
        }

                private object[] ReturnUserData(){object[] data;
                    data = new object[26];
                    data[0] = _rowData0;
                    data[1] = _rowData1;
                    data[2] = _rowData2;
                    data[3] = _rowData3;
                    data[4] = _rowData4;
                    data[5] = _rowData5;
                    data[6] = _rowData6;
                    data[7] = _rowData7;
                    data[8] = _rowData8;
                    data[9] = _rowData9;
                    data[10] = _rowData10;
                    data[11] = _rowData11;
                    data[12] = _rowData12;
                    data[13] = _rowData13;
                    data[14] = _rowData14;
                    data[15] = _rowData15;
                    data[16] = _rowData16;
                    data[17] = _rowData17;
                    data[18] = _rowData18;
                    data[19] = _rowData19;
                    data[20] = _rowData20;
                    data[21] = _rowData21;
                    data[22] = _rowData22;
                    data[23] = _rowData23;
                    data[24] = _rowData24;
                    data[25] = _rowData25;
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

        public S_27_01_04__sol2__2_0_ctrl()
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