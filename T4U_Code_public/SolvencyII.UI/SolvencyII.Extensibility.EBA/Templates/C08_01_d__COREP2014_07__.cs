using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms; 
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Respositories;
using SolvencyII.UI.Shared.UserControls; 

namespace SolvencyII.UI.UserControls 
{ 
   [Export(typeof(ISolvencyUserControl))]
    public partial class C08_01_d__COREP2014_07__ : UserControlBase, ISolvencyUserControl, IClosedRowFactory 
    { 
       // UserControlGenerator version: 2015.2.20.0
       public ISolvencyUserControl Create {get{return new C08_01_d__COREP2014_07__();}}
       public IClosedRowControl CreateRowControl() { return new C08_01_d__COREP2014_07___ctrl(); }
       public IClosedRowRepository CtrlRepository { get; set; } 
       public double Version { get; private set; }
       public string GroupTableIDs { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public int SingleZOrdinateID { get; set; }
       public List<Type> DataTypes { get; private set; }
       public List<string> DataTables { get; private set; }public void SetupData(int i, string query){}public void BindRepeater(){}public void AddRecord(){}public void DelRecord(){}public void RefreshDR(){}
       public List<long> PK_IDs { get; set; } public void SetPK_ID(long PK_ID) { }public bool UpdateData(out string errorText, List<ISolvencyPageControl> nPageControls) { errorText = ""; return true; } 
       public bool SpecialCases { get; set; } 

       public C08_01_d__COREP2014_07__()
       {
           InitializeComponent();
           {splitForm.SplitterWidth = 1;}
           Version = 1.0;
           GroupTableIDs = "606";
           TableVID = 606;
           FrameworkCode = "COREP";
           DataTypes = new List<Type>{ typeof(T__C_08_01_d__COREP__2_0_1) };
           DataTables = new List<string>{ "T__C_08_01_d__COREP__2_0_1" };
           PK_IDs = new List<long> { 0 };
           SpecialCases = false;
           // CtrlRepository = new ClosedRowRepository<C08_01_d__COREP2014_07___ctrl>(C08_01_d__COREP2014_07___ctrl0, splitForm.Panel2, splitForm.Orientation == Orientation.Horizontal, DataTables);
       }

       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); }  
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 

   } 
} 