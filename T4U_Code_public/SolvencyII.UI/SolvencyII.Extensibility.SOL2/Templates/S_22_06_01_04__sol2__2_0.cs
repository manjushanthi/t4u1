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
    public partial class S_22_06_01_04__sol2__2_0 : UserControlBase2, ISolvencyUserControl, IClosedRowFactory, IControlContainsRepository 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyUserControl Create {get{return new S_22_06_01_04__sol2__2_0();}}
       public IClosedRowRepository CtrlRepository { get; set; } 
       public double Version { get; private set; }
       public string GroupTableIDs { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public int SingleZOrdinateID { get; set; }
       public List<Type> DataTypes { get; private set; }
       public List<string> DataTables { get; private set; }
       public bool SpecialCases { get; set; } 

       public S_22_06_01_04__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           GroupTableIDs = "158";
           TableVID = 158;
           FrameworkCode = "s2md";
           DataTypes = new List<Type>{ typeof(T__S_22_06_01_04__sol2__2_0) };
           DataTables = new List<string>{ "T__S_22_06_01_04__sol2__2_0" };
           SpecialCases = true;
           CtrlRepository = new SpecialCaseRepository<S_22_06_01_04__sol2__2_0_ctrl>(S_22_06_01_04__sol2__2_0_ctrl0, splitForm.Panel2, splitForm.Orientation == Orientation.Horizontal, DataTables);
       }
public IClosedRowControl CreateRowControl() { { return new S_22_06_01_04__sol2__2_0_ctrl(); } }

       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); }  
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
       public void addRow(object sender, EventArgs e) { AddRow(sender); }  
       public void addCol(object sender, EventArgs e) { AddCol(sender); }  

       public void AddRecord(){}
       public void DelRecord(){}
       public void RefreshDR(){}
       public void SetupData(int i, string query){}
       public void BindRepeater(){}
       public bool IsValid(){return true;}
       public bool UpdateData(out string errorText, List<ISolvencyPageControl> nPageControls){errorText = "";return true;}
       public void SetPK_ID(long PK_ID){}

   } 
} 
