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
    public partial class F32_02_a__AE2014_03__1_0_0 : UserControlBase, ISolvencyUserControl, IClosedRowFactory 
    { 
       // UserControlGenerator version: 2015.2.20.0
       public ISolvencyUserControl Create {get{return new F32_02_a__AE2014_03__1_0_0();}}
       public IClosedRowControl CreateRowControl() { return new F32_02_a__AE2014_03__1_0_0_ctrl(); }
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

       public F32_02_a__AE2014_03__1_0_0()
       {
           InitializeComponent();
           {splitForm.SplitterWidth = 1;}
           Version = 1.0;
           GroupTableIDs = "662";
           TableVID = 662;
           FrameworkCode = "AE";
           DataTypes = new List<Type>{ typeof(T__F_32_02_a__AE_2014_03__1_0_0) };
           DataTables = new List<string>{ "T__F_32_02_a__AE_2014_03__1_0_0" };
           PK_IDs = new List<long> { 0 };
           SpecialCases = false;
           // CtrlRepository = new ClosedRowRepository<F32_02_a__AE2014_03__1_0_0_ctrl>(F32_02_a__AE2014_03__1_0_0_ctrl0, splitForm.Panel2, splitForm.Orientation == Orientation.Horizontal, DataTables);
       }

       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); }  
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 

   } 
} 
