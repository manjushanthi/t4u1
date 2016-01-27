using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq; 
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Respositories;
using SolvencyII.UI.Shared.UserControls; 
using SolvencyII.Data.Shared.Repository;
using Microsoft.VisualBasic.PowerPacks;

namespace SolvencyII.UI.UserControls 
{ 
   [Export(typeof(ISolvencyUserControl))]
    public partial class S_19_01_01__sol2__2_0 : UserControlBase, ISolvencyUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyUserControl Create {get{return new S_19_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public string GroupTableIDs { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public int SingleZOrdinateID { get; set; }
       public List<Type> DataTypes { get; private set; }
       public List<string> DataTables { get; private set; }
       public bool SpecialCases { get; set; } 

       public S_19_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           GroupTableIDs = "123|124|125|126|127|128|129|130|131|132|133|134|135|136|137|138|139|140|141|142|143";
           TableVID = 123;
           FrameworkCode = "s2md";
           DataTypes = new List<Type>{ typeof(T__S_19_01_01_01__sol2__2_0),typeof(T__S_19_01_01_07__sol2__2_0),typeof(T__S_19_01_01_13__sol2__2_0),typeof(T__S_19_01_01_19__sol2__2_0),typeof(T__S_19_01_01_20__sol2__2_0),typeof(T__S_19_01_01_21__sol2__2_0),typeof(T__S_19_01_01_02__sol2__2_0),typeof(T__S_19_01_01_08__sol2__2_0),typeof(T__S_19_01_01_14__sol2__2_0),typeof(T__S_19_01_01_03__sol2__2_0),typeof(T__S_19_01_01_09__sol2__2_0),typeof(T__S_19_01_01_15__sol2__2_0),typeof(T__S_19_01_01_04__sol2__2_0),typeof(T__S_19_01_01_10__sol2__2_0),typeof(T__S_19_01_01_16__sol2__2_0),typeof(T__S_19_01_01_05__sol2__2_0),typeof(T__S_19_01_01_11__sol2__2_0),typeof(T__S_19_01_01_17__sol2__2_0),typeof(T__S_19_01_01_06__sol2__2_0),typeof(T__S_19_01_01_12__sol2__2_0),typeof(T__S_19_01_01_18__sol2__2_0) };
           DataTables = new List<string>{ "T__S_19_01_01_01__sol2__2_0","T__S_19_01_01_07__sol2__2_0","T__S_19_01_01_13__sol2__2_0","T__S_19_01_01_19__sol2__2_0","T__S_19_01_01_20__sol2__2_0","T__S_19_01_01_21__sol2__2_0","T__S_19_01_01_02__sol2__2_0","T__S_19_01_01_08__sol2__2_0","T__S_19_01_01_14__sol2__2_0","T__S_19_01_01_03__sol2__2_0","T__S_19_01_01_09__sol2__2_0","T__S_19_01_01_15__sol2__2_0","T__S_19_01_01_04__sol2__2_0","T__S_19_01_01_10__sol2__2_0","T__S_19_01_01_16__sol2__2_0","T__S_19_01_01_05__sol2__2_0","T__S_19_01_01_11__sol2__2_0","T__S_19_01_01_17__sol2__2_0","T__S_19_01_01_06__sol2__2_0","T__S_19_01_01_12__sol2__2_0","T__S_19_01_01_18__sol2__2_0" };
           SpecialCases = false;
       }

       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); }  
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 

        #region Data 

        private List<T__S_19_01_01_01__sol2__2_0> mainData0 = new List<T__S_19_01_01_01__sol2__2_0>() {new T__S_19_01_01_01__sol2__2_0()};
        private List<long?> deletedData0 = new List<long?>();
        public GenericRepository<T__S_19_01_01_01__sol2__2_0> Repository0{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_01__sol2__2_0>();return null;} }
        private T__S_19_01_01_01__sol2__2_0 SingleRow0 {get { return new T__S_19_01_01_01__sol2__2_0(); } }

        private List<T__S_19_01_01_07__sol2__2_0> mainData1 = new List<T__S_19_01_01_07__sol2__2_0>() {new T__S_19_01_01_07__sol2__2_0()};
        private List<long?> deletedData1 = new List<long?>();
        public GenericRepository<T__S_19_01_01_07__sol2__2_0> Repository1{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_07__sol2__2_0>();return null;} }
        private T__S_19_01_01_07__sol2__2_0 SingleRow1 {get { return new T__S_19_01_01_07__sol2__2_0(); } }

        private List<T__S_19_01_01_13__sol2__2_0> mainData2 = new List<T__S_19_01_01_13__sol2__2_0>() {new T__S_19_01_01_13__sol2__2_0()};
        private List<long?> deletedData2 = new List<long?>();
        public GenericRepository<T__S_19_01_01_13__sol2__2_0> Repository2{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_13__sol2__2_0>();return null;} }
        private T__S_19_01_01_13__sol2__2_0 SingleRow2 {get { return new T__S_19_01_01_13__sol2__2_0(); } }

        private List<T__S_19_01_01_19__sol2__2_0> mainData3 = new List<T__S_19_01_01_19__sol2__2_0>() {new T__S_19_01_01_19__sol2__2_0()};
        private List<long?> deletedData3 = new List<long?>();
        public GenericRepository<T__S_19_01_01_19__sol2__2_0> Repository3{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_19__sol2__2_0>();return null;} }
        private T__S_19_01_01_19__sol2__2_0 SingleRow3 {get { return new T__S_19_01_01_19__sol2__2_0(); } }

        private List<T__S_19_01_01_20__sol2__2_0> mainData4 = new List<T__S_19_01_01_20__sol2__2_0>() {new T__S_19_01_01_20__sol2__2_0()};
        private List<long?> deletedData4 = new List<long?>();
        public GenericRepository<T__S_19_01_01_20__sol2__2_0> Repository4{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_20__sol2__2_0>();return null;} }
        private T__S_19_01_01_20__sol2__2_0 SingleRow4 {get { return new T__S_19_01_01_20__sol2__2_0(); } }

        private List<T__S_19_01_01_21__sol2__2_0> mainData5 = new List<T__S_19_01_01_21__sol2__2_0>() {new T__S_19_01_01_21__sol2__2_0()};
        private List<long?> deletedData5 = new List<long?>();
        public GenericRepository<T__S_19_01_01_21__sol2__2_0> Repository5{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_21__sol2__2_0>();return null;} }
        private T__S_19_01_01_21__sol2__2_0 SingleRow5 {get { return new T__S_19_01_01_21__sol2__2_0(); } }

        private List<T__S_19_01_01_02__sol2__2_0> mainData6 = new List<T__S_19_01_01_02__sol2__2_0>() {new T__S_19_01_01_02__sol2__2_0()};
        private List<long?> deletedData6 = new List<long?>();
        public GenericRepository<T__S_19_01_01_02__sol2__2_0> Repository6{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_02__sol2__2_0>();return null;} }
        private T__S_19_01_01_02__sol2__2_0 SingleRow6 {get { return new T__S_19_01_01_02__sol2__2_0(); } }

        private List<T__S_19_01_01_08__sol2__2_0> mainData7 = new List<T__S_19_01_01_08__sol2__2_0>() {new T__S_19_01_01_08__sol2__2_0()};
        private List<long?> deletedData7 = new List<long?>();
        public GenericRepository<T__S_19_01_01_08__sol2__2_0> Repository7{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_08__sol2__2_0>();return null;} }
        private T__S_19_01_01_08__sol2__2_0 SingleRow7 {get { return new T__S_19_01_01_08__sol2__2_0(); } }

        private List<T__S_19_01_01_14__sol2__2_0> mainData8 = new List<T__S_19_01_01_14__sol2__2_0>() {new T__S_19_01_01_14__sol2__2_0()};
        private List<long?> deletedData8 = new List<long?>();
        public GenericRepository<T__S_19_01_01_14__sol2__2_0> Repository8{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_14__sol2__2_0>();return null;} }
        private T__S_19_01_01_14__sol2__2_0 SingleRow8 {get { return new T__S_19_01_01_14__sol2__2_0(); } }

        private List<T__S_19_01_01_03__sol2__2_0> mainData9 = new List<T__S_19_01_01_03__sol2__2_0>() {new T__S_19_01_01_03__sol2__2_0()};
        private List<long?> deletedData9 = new List<long?>();
        public GenericRepository<T__S_19_01_01_03__sol2__2_0> Repository9{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_03__sol2__2_0>();return null;} }
        private T__S_19_01_01_03__sol2__2_0 SingleRow9 {get { return new T__S_19_01_01_03__sol2__2_0(); } }

        private List<T__S_19_01_01_09__sol2__2_0> mainData10 = new List<T__S_19_01_01_09__sol2__2_0>() {new T__S_19_01_01_09__sol2__2_0()};
        private List<long?> deletedData10 = new List<long?>();
        public GenericRepository<T__S_19_01_01_09__sol2__2_0> Repository10{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_09__sol2__2_0>();return null;} }
        private T__S_19_01_01_09__sol2__2_0 SingleRow10 {get { return new T__S_19_01_01_09__sol2__2_0(); } }

        private List<T__S_19_01_01_15__sol2__2_0> mainData11 = new List<T__S_19_01_01_15__sol2__2_0>() {new T__S_19_01_01_15__sol2__2_0()};
        private List<long?> deletedData11 = new List<long?>();
        public GenericRepository<T__S_19_01_01_15__sol2__2_0> Repository11{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_15__sol2__2_0>();return null;} }
        private T__S_19_01_01_15__sol2__2_0 SingleRow11 {get { return new T__S_19_01_01_15__sol2__2_0(); } }

        private List<T__S_19_01_01_04__sol2__2_0> mainData12 = new List<T__S_19_01_01_04__sol2__2_0>() {new T__S_19_01_01_04__sol2__2_0()};
        private List<long?> deletedData12 = new List<long?>();
        public GenericRepository<T__S_19_01_01_04__sol2__2_0> Repository12{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_04__sol2__2_0>();return null;} }
        private T__S_19_01_01_04__sol2__2_0 SingleRow12 {get { return new T__S_19_01_01_04__sol2__2_0(); } }

        private List<T__S_19_01_01_10__sol2__2_0> mainData13 = new List<T__S_19_01_01_10__sol2__2_0>() {new T__S_19_01_01_10__sol2__2_0()};
        private List<long?> deletedData13 = new List<long?>();
        public GenericRepository<T__S_19_01_01_10__sol2__2_0> Repository13{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_10__sol2__2_0>();return null;} }
        private T__S_19_01_01_10__sol2__2_0 SingleRow13 {get { return new T__S_19_01_01_10__sol2__2_0(); } }

        private List<T__S_19_01_01_16__sol2__2_0> mainData14 = new List<T__S_19_01_01_16__sol2__2_0>() {new T__S_19_01_01_16__sol2__2_0()};
        private List<long?> deletedData14 = new List<long?>();
        public GenericRepository<T__S_19_01_01_16__sol2__2_0> Repository14{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_16__sol2__2_0>();return null;} }
        private T__S_19_01_01_16__sol2__2_0 SingleRow14 {get { return new T__S_19_01_01_16__sol2__2_0(); } }

        private List<T__S_19_01_01_05__sol2__2_0> mainData15 = new List<T__S_19_01_01_05__sol2__2_0>() {new T__S_19_01_01_05__sol2__2_0()};
        private List<long?> deletedData15 = new List<long?>();
        public GenericRepository<T__S_19_01_01_05__sol2__2_0> Repository15{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_05__sol2__2_0>();return null;} }
        private T__S_19_01_01_05__sol2__2_0 SingleRow15 {get { return new T__S_19_01_01_05__sol2__2_0(); } }

        private List<T__S_19_01_01_11__sol2__2_0> mainData16 = new List<T__S_19_01_01_11__sol2__2_0>() {new T__S_19_01_01_11__sol2__2_0()};
        private List<long?> deletedData16 = new List<long?>();
        public GenericRepository<T__S_19_01_01_11__sol2__2_0> Repository16{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_11__sol2__2_0>();return null;} }
        private T__S_19_01_01_11__sol2__2_0 SingleRow16 {get { return new T__S_19_01_01_11__sol2__2_0(); } }

        private List<T__S_19_01_01_17__sol2__2_0> mainData17 = new List<T__S_19_01_01_17__sol2__2_0>() {new T__S_19_01_01_17__sol2__2_0()};
        private List<long?> deletedData17 = new List<long?>();
        public GenericRepository<T__S_19_01_01_17__sol2__2_0> Repository17{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_17__sol2__2_0>();return null;} }
        private T__S_19_01_01_17__sol2__2_0 SingleRow17 {get { return new T__S_19_01_01_17__sol2__2_0(); } }

        private List<T__S_19_01_01_06__sol2__2_0> mainData18 = new List<T__S_19_01_01_06__sol2__2_0>() {new T__S_19_01_01_06__sol2__2_0()};
        private List<long?> deletedData18 = new List<long?>();
        public GenericRepository<T__S_19_01_01_06__sol2__2_0> Repository18{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_06__sol2__2_0>();return null;} }
        private T__S_19_01_01_06__sol2__2_0 SingleRow18 {get { return new T__S_19_01_01_06__sol2__2_0(); } }

        private List<T__S_19_01_01_12__sol2__2_0> mainData19 = new List<T__S_19_01_01_12__sol2__2_0>() {new T__S_19_01_01_12__sol2__2_0()};
        private List<long?> deletedData19 = new List<long?>();
        public GenericRepository<T__S_19_01_01_12__sol2__2_0> Repository19{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_12__sol2__2_0>();return null;} }
        private T__S_19_01_01_12__sol2__2_0 SingleRow19 {get { return new T__S_19_01_01_12__sol2__2_0(); } }

        private List<T__S_19_01_01_18__sol2__2_0> mainData20 = new List<T__S_19_01_01_18__sol2__2_0>() {new T__S_19_01_01_18__sol2__2_0()};
        private List<long?> deletedData20 = new List<long?>();
        public GenericRepository<T__S_19_01_01_18__sol2__2_0> Repository20{get{if (!DesignMode) return new GenericRepository<T__S_19_01_01_18__sol2__2_0>();return null;} }
        private T__S_19_01_01_18__sol2__2_0 SingleRow20 {get { return new T__S_19_01_01_18__sol2__2_0(); } }
        #endregion

       
       private void Repeater_Load(object sender, EventArgs e)
       {
           dr_Main.VirtualMode = true;
           dr_Main.ItemValueNeeded += dr_Main_ItemValueNeeded;
           dr_Main.ItemsRemoved += dr_Main_ItemsRemoved;
           if (DesignMode) dr_Main.ItemCount = 1;
       }

       private void dr_Main_ItemsRemoved(object sender, DataRepeaterAddRemoveItemsEventArgs e){
           deletedData0.Add(mainData0[e.ItemIndex].PK_ID);mainData0.RemoveAt(e.ItemIndex);
           deletedData1.Add(mainData1[e.ItemIndex].PK_ID);mainData1.RemoveAt(e.ItemIndex);
           deletedData2.Add(mainData2[e.ItemIndex].PK_ID);mainData2.RemoveAt(e.ItemIndex);
           deletedData3.Add(mainData3[e.ItemIndex].PK_ID);mainData3.RemoveAt(e.ItemIndex);
           deletedData4.Add(mainData4[e.ItemIndex].PK_ID);mainData4.RemoveAt(e.ItemIndex);
           deletedData5.Add(mainData5[e.ItemIndex].PK_ID);mainData5.RemoveAt(e.ItemIndex);
           deletedData6.Add(mainData6[e.ItemIndex].PK_ID);mainData6.RemoveAt(e.ItemIndex);
           deletedData7.Add(mainData7[e.ItemIndex].PK_ID);mainData7.RemoveAt(e.ItemIndex);
           deletedData8.Add(mainData8[e.ItemIndex].PK_ID);mainData8.RemoveAt(e.ItemIndex);
           deletedData9.Add(mainData9[e.ItemIndex].PK_ID);mainData9.RemoveAt(e.ItemIndex);
           deletedData10.Add(mainData10[e.ItemIndex].PK_ID);mainData10.RemoveAt(e.ItemIndex);
           deletedData11.Add(mainData11[e.ItemIndex].PK_ID);mainData11.RemoveAt(e.ItemIndex);
           deletedData12.Add(mainData12[e.ItemIndex].PK_ID);mainData12.RemoveAt(e.ItemIndex);
           deletedData13.Add(mainData13[e.ItemIndex].PK_ID);mainData13.RemoveAt(e.ItemIndex);
           deletedData14.Add(mainData14[e.ItemIndex].PK_ID);mainData14.RemoveAt(e.ItemIndex);
           deletedData15.Add(mainData15[e.ItemIndex].PK_ID);mainData15.RemoveAt(e.ItemIndex);
           deletedData16.Add(mainData16[e.ItemIndex].PK_ID);mainData16.RemoveAt(e.ItemIndex);
           deletedData17.Add(mainData17[e.ItemIndex].PK_ID);mainData17.RemoveAt(e.ItemIndex);
           deletedData18.Add(mainData18[e.ItemIndex].PK_ID);mainData18.RemoveAt(e.ItemIndex);
           deletedData19.Add(mainData19[e.ItemIndex].PK_ID);mainData19.RemoveAt(e.ItemIndex);
           deletedData20.Add(mainData20[e.ItemIndex].PK_ID);mainData20.RemoveAt(e.ItemIndex);
       }

       private void dr_Main_ItemValueNeeded(object sender, DataRepeaterItemValueEventArgs e)
       {
           if (e.ItemIndex < mainData0.Count){object[] data = new object[21];data[0] = mainData0[e.ItemIndex];data[1] = mainData1[e.ItemIndex];data[2] = mainData2[e.ItemIndex];data[3] = mainData3[e.ItemIndex];data[4] = mainData4[e.ItemIndex];data[5] = mainData5[e.ItemIndex];data[6] = mainData6[e.ItemIndex];data[7] = mainData7[e.ItemIndex];data[8] = mainData8[e.ItemIndex];data[9] = mainData9[e.ItemIndex];data[10] = mainData10[e.ItemIndex];data[11] = mainData11[e.ItemIndex];data[12] = mainData12[e.ItemIndex];data[13] = mainData13[e.ItemIndex];data[14] = mainData14[e.ItemIndex];data[15] = mainData15[e.ItemIndex];data[16] = mainData16[e.ItemIndex];data[17] = mainData17[e.ItemIndex];data[18] = mainData18[e.ItemIndex];data[19] = mainData19[e.ItemIndex];data[20] = mainData20[e.ItemIndex];e.Value = data;            }
       }

       private void RefreshDataRepeater() {dr_Main.BeginResetItemTemplate();dr_Main.EndResetItemTemplate();}

       public void AddRecord()
       {
           mainData0.Add(SingleRow0);
           mainData1.Add(SingleRow1);
           mainData2.Add(SingleRow2);
           mainData3.Add(SingleRow3);
           mainData4.Add(SingleRow4);
           mainData5.Add(SingleRow5);
           mainData6.Add(SingleRow6);
           mainData7.Add(SingleRow7);
           mainData8.Add(SingleRow8);
           mainData9.Add(SingleRow9);
           mainData10.Add(SingleRow10);
           mainData11.Add(SingleRow11);
           mainData12.Add(SingleRow12);
           mainData13.Add(SingleRow13);
           mainData14.Add(SingleRow14);
           mainData15.Add(SingleRow15);
           mainData16.Add(SingleRow16);
           mainData17.Add(SingleRow17);
           mainData18.Add(SingleRow18);
           mainData19.Add(SingleRow19);
           mainData20.Add(SingleRow20);
           dr_Main.ItemCount = mainData0.Count;
           dr_Main.CurrentItemIndex = mainData0.Count - 1;
       }

       public void DelRecord(){dr_Main.RemoveAt(dr_Main.CurrentItemIndex);}

       public void RefreshDR(){dr_Main.ItemCount = mainData0.Count;RefreshDataRepeater();}

       public void SetupData(int i, string query)
       {
           if (i == 0)using (var repository0 = Repository0){if (repository0 != null){ mainData0 = repository0.Select(query).ToList();if (mainData0.Count == 0) mainData0.Add(SingleRow0);} }
           if (i == 1)using (var repository1 = Repository1){if (repository1 != null){ mainData1 = repository1.Select(query).ToList();if (mainData1.Count == 0) mainData1.Add(SingleRow1);} }
           if (i == 2)using (var repository2 = Repository2){if (repository2 != null){ mainData2 = repository2.Select(query).ToList();if (mainData2.Count == 0) mainData2.Add(SingleRow2);} }
           if (i == 3)using (var repository3 = Repository3){if (repository3 != null){ mainData3 = repository3.Select(query).ToList();if (mainData3.Count == 0) mainData3.Add(SingleRow3);} }
           if (i == 4)using (var repository4 = Repository4){if (repository4 != null){ mainData4 = repository4.Select(query).ToList();if (mainData4.Count == 0) mainData4.Add(SingleRow4);} }
           if (i == 5)using (var repository5 = Repository5){if (repository5 != null){ mainData5 = repository5.Select(query).ToList();if (mainData5.Count == 0) mainData5.Add(SingleRow5);} }
           if (i == 6)using (var repository6 = Repository6){if (repository6 != null){ mainData6 = repository6.Select(query).ToList();if (mainData6.Count == 0) mainData6.Add(SingleRow6);} }
           if (i == 7)using (var repository7 = Repository7){if (repository7 != null){ mainData7 = repository7.Select(query).ToList();if (mainData7.Count == 0) mainData7.Add(SingleRow7);} }
           if (i == 8)using (var repository8 = Repository8){if (repository8 != null){ mainData8 = repository8.Select(query).ToList();if (mainData8.Count == 0) mainData8.Add(SingleRow8);} }
           if (i == 9)using (var repository9 = Repository9){if (repository9 != null){ mainData9 = repository9.Select(query).ToList();if (mainData9.Count == 0) mainData9.Add(SingleRow9);} }
           if (i == 10)using (var repository10 = Repository10){if (repository10 != null){ mainData10 = repository10.Select(query).ToList();if (mainData10.Count == 0) mainData10.Add(SingleRow10);} }
           if (i == 11)using (var repository11 = Repository11){if (repository11 != null){ mainData11 = repository11.Select(query).ToList();if (mainData11.Count == 0) mainData11.Add(SingleRow11);} }
           if (i == 12)using (var repository12 = Repository12){if (repository12 != null){ mainData12 = repository12.Select(query).ToList();if (mainData12.Count == 0) mainData12.Add(SingleRow12);} }
           if (i == 13)using (var repository13 = Repository13){if (repository13 != null){ mainData13 = repository13.Select(query).ToList();if (mainData13.Count == 0) mainData13.Add(SingleRow13);} }
           if (i == 14)using (var repository14 = Repository14){if (repository14 != null){ mainData14 = repository14.Select(query).ToList();if (mainData14.Count == 0) mainData14.Add(SingleRow14);} }
           if (i == 15)using (var repository15 = Repository15){if (repository15 != null){ mainData15 = repository15.Select(query).ToList();if (mainData15.Count == 0) mainData15.Add(SingleRow15);} }
           if (i == 16)using (var repository16 = Repository16){if (repository16 != null){ mainData16 = repository16.Select(query).ToList();if (mainData16.Count == 0) mainData16.Add(SingleRow16);} }
           if (i == 17)using (var repository17 = Repository17){if (repository17 != null){ mainData17 = repository17.Select(query).ToList();if (mainData17.Count == 0) mainData17.Add(SingleRow17);} }
           if (i == 18)using (var repository18 = Repository18){if (repository18 != null){ mainData18 = repository18.Select(query).ToList();if (mainData18.Count == 0) mainData18.Add(SingleRow18);} }
           if (i == 19)using (var repository19 = Repository19){if (repository19 != null){ mainData19 = repository19.Select(query).ToList();if (mainData19.Count == 0) mainData19.Add(SingleRow19);} }
           if (i == 20)using (var repository20 = Repository20){if (repository20 != null){ mainData20 = repository20.Select(query).ToList();if (mainData20.Count == 0) mainData20.Add(SingleRow20);} }
       }

       public void BindRepeater(){dr_Main.ItemCount = mainData0.Count;}

        public bool UpdateData(out string errorText, List<ISolvencyPageControl> nPageControls)
        {
            try {
                using (var repository0 = Repository0)
                using (var repository1 = Repository1)
                using (var repository2 = Repository2)
                using (var repository3 = Repository3)
                using (var repository4 = Repository4)
                using (var repository5 = Repository5)
                using (var repository6 = Repository6)
                using (var repository7 = Repository7)
                using (var repository8 = Repository8)
                using (var repository9 = Repository9)
                using (var repository10 = Repository10)
                using (var repository11 = Repository11)
                using (var repository12 = Repository12)
                using (var repository13 = Repository13)
                using (var repository14 = Repository14)
                using (var repository15 = Repository15)
                using (var repository16 = Repository16)
                using (var repository17 = Repository17)
                using (var repository18 = Repository18)
                using (var repository19 = Repository19)
                using (var repository20 = Repository20)
                {
                    bool success = true;
                    repository0.BeginTransaction();  // Shared singleton connection => one transaction
                    success = UserRepository(dr_Main, repository0, mainData0, deletedData0, nPageControls);
                    success = success & UserRepository(dr_Main, repository1, mainData1, deletedData1, nPageControls);
                    success = success & UserRepository(dr_Main, repository2, mainData2, deletedData2, nPageControls);
                    success = success & UserRepository(dr_Main, repository3, mainData3, deletedData3, nPageControls);
                    success = success & UserRepository(dr_Main, repository4, mainData4, deletedData4, nPageControls);
                    success = success & UserRepository(dr_Main, repository5, mainData5, deletedData5, nPageControls);
                    success = success & UserRepository(dr_Main, repository6, mainData6, deletedData6, nPageControls);
                    success = success & UserRepository(dr_Main, repository7, mainData7, deletedData7, nPageControls);
                    success = success & UserRepository(dr_Main, repository8, mainData8, deletedData8, nPageControls);
                    success = success & UserRepository(dr_Main, repository9, mainData9, deletedData9, nPageControls);
                    success = success & UserRepository(dr_Main, repository10, mainData10, deletedData10, nPageControls);
                    success = success & UserRepository(dr_Main, repository11, mainData11, deletedData11, nPageControls);
                    success = success & UserRepository(dr_Main, repository12, mainData12, deletedData12, nPageControls);
                    success = success & UserRepository(dr_Main, repository13, mainData13, deletedData13, nPageControls);
                    success = success & UserRepository(dr_Main, repository14, mainData14, deletedData14, nPageControls);
                    success = success & UserRepository(dr_Main, repository15, mainData15, deletedData15, nPageControls);
                    success = success & UserRepository(dr_Main, repository16, mainData16, deletedData16, nPageControls);
                    success = success & UserRepository(dr_Main, repository17, mainData17, deletedData17, nPageControls);
                    success = success & UserRepository(dr_Main, repository18, mainData18, deletedData18, nPageControls);
                    success = success & UserRepository(dr_Main, repository19, mainData19, deletedData19, nPageControls);
                    success = success & UserRepository(dr_Main, repository20, mainData20, deletedData20, nPageControls);
                    if (success) {
                        repository0.Commit();
                        errorText = "";
                    } else {
                        repository0.RollBack();
                        errorText = repository0.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                        errorText += "\r\n" + repository1.ErrorMessage;
                    }
                    return success;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                errorText = e.Message;
            }
            return false;
        }

       public void SetPK_ID(long PK_ID){
           mainData0.Clear();
           if (PK_ID == -1){mainData0.Add(SingleRow0);}
           else{using (var repository0 = Repository0){mainData0.Add(repository0.Where(r => r.PK_ID == PK_ID).FirstOrDefault());}}
           dr_Main.ItemCount = 1;RefreshDataRepeater();
       }

   } 
} 
