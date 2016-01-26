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
    public partial class S_23_02_01__sol2__2_0 : UserControlBase, ISolvencyUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyUserControl Create {get{return new S_23_02_01__sol2__2_0();}}
       public double Version { get; private set; }
       public string GroupTableIDs { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public int SingleZOrdinateID { get; set; }
       public List<Type> DataTypes { get; private set; }
       public List<string> DataTables { get; private set; }
       public bool SpecialCases { get; set; } 

       public S_23_02_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           GroupTableIDs = "166|167|168|169";
           TableVID = 166;
           FrameworkCode = "s2md";
           DataTypes = new List<Type>{ typeof(T__S_23_02_01_01__sol2__2_0),typeof(T__S_23_02_01_02__sol2__2_0),typeof(T__S_23_02_01_03__sol2__2_0),typeof(T__S_23_02_01_04__sol2__2_0) };
           DataTables = new List<string>{ "T__S_23_02_01_01__sol2__2_0","T__S_23_02_01_02__sol2__2_0","T__S_23_02_01_03__sol2__2_0","T__S_23_02_01_04__sol2__2_0" };
           SpecialCases = false;
       }

       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); }  
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 

        #region Data 

        private List<T__S_23_02_01_01__sol2__2_0> mainData0 = new List<T__S_23_02_01_01__sol2__2_0>() {new T__S_23_02_01_01__sol2__2_0()};
        private List<long?> deletedData0 = new List<long?>();
        public GenericRepository<T__S_23_02_01_01__sol2__2_0> Repository0{get{if (!DesignMode) return new GenericRepository<T__S_23_02_01_01__sol2__2_0>();return null;} }
        private T__S_23_02_01_01__sol2__2_0 SingleRow0 {get { return new T__S_23_02_01_01__sol2__2_0(); } }

        private List<T__S_23_02_01_02__sol2__2_0> mainData1 = new List<T__S_23_02_01_02__sol2__2_0>() {new T__S_23_02_01_02__sol2__2_0()};
        private List<long?> deletedData1 = new List<long?>();
        public GenericRepository<T__S_23_02_01_02__sol2__2_0> Repository1{get{if (!DesignMode) return new GenericRepository<T__S_23_02_01_02__sol2__2_0>();return null;} }
        private T__S_23_02_01_02__sol2__2_0 SingleRow1 {get { return new T__S_23_02_01_02__sol2__2_0(); } }

        private List<T__S_23_02_01_03__sol2__2_0> mainData2 = new List<T__S_23_02_01_03__sol2__2_0>() {new T__S_23_02_01_03__sol2__2_0()};
        private List<long?> deletedData2 = new List<long?>();
        public GenericRepository<T__S_23_02_01_03__sol2__2_0> Repository2{get{if (!DesignMode) return new GenericRepository<T__S_23_02_01_03__sol2__2_0>();return null;} }
        private T__S_23_02_01_03__sol2__2_0 SingleRow2 {get { return new T__S_23_02_01_03__sol2__2_0(); } }

        private List<T__S_23_02_01_04__sol2__2_0> mainData3 = new List<T__S_23_02_01_04__sol2__2_0>() {new T__S_23_02_01_04__sol2__2_0()};
        private List<long?> deletedData3 = new List<long?>();
        public GenericRepository<T__S_23_02_01_04__sol2__2_0> Repository3{get{if (!DesignMode) return new GenericRepository<T__S_23_02_01_04__sol2__2_0>();return null;} }
        private T__S_23_02_01_04__sol2__2_0 SingleRow3 {get { return new T__S_23_02_01_04__sol2__2_0(); } }
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
       }

       private void dr_Main_ItemValueNeeded(object sender, DataRepeaterItemValueEventArgs e)
       {
           if (e.ItemIndex < mainData0.Count){object[] data = new object[4];data[0] = mainData0[e.ItemIndex];data[1] = mainData1[e.ItemIndex];data[2] = mainData2[e.ItemIndex];data[3] = mainData3[e.ItemIndex];e.Value = data;            }
       }

       private void RefreshDataRepeater() {dr_Main.BeginResetItemTemplate();dr_Main.EndResetItemTemplate();}

       public void AddRecord()
       {
           mainData0.Add(SingleRow0);
           mainData1.Add(SingleRow1);
           mainData2.Add(SingleRow2);
           mainData3.Add(SingleRow3);
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
       }

       public void BindRepeater(){dr_Main.ItemCount = mainData0.Count;}

        public bool UpdateData(out string errorText, List<ISolvencyPageControl> nPageControls)
        {
            try {
                using (var repository0 = Repository0)
                using (var repository1 = Repository1)
                using (var repository2 = Repository2)
                using (var repository3 = Repository3)
                {
                    bool success = true;
                    repository0.BeginTransaction();  // Shared singleton connection => one transaction
                    success = UserRepository(dr_Main, repository0, mainData0, deletedData0, nPageControls);
                    success = success & UserRepository(dr_Main, repository1, mainData1, deletedData1, nPageControls);
                    success = success & UserRepository(dr_Main, repository2, mainData2, deletedData2, nPageControls);
                    success = success & UserRepository(dr_Main, repository3, mainData3, deletedData3, nPageControls);
                    if (success) {
                        repository0.Commit();
                        errorText = "";
                    } else {
                        repository0.RollBack();
                        errorText = repository0.ErrorMessage;
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