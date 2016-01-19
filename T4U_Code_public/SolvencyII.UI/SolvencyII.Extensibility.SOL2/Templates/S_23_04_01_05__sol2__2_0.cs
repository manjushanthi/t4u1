using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.UserControls; 

namespace SolvencyII.UI.UserControls 
{ 
   [Export(typeof(ISolvencyUserControl))]
    public partial class S_23_04_01_05__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_01_05__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_01_05__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 196;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_01_05__sol2__2_0);
           DataTable = "T__S_23_04_01_05__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 683, ColType = "STRING", ColNumber = 0, ColName = "C0565", HierarchyID = 0, IsRowKey = false, Label = "Code of own funds from the financial statements that should not be represented by the reconciliation reserve and do not meet the criteria to be classified as Solvency II own funds", OrdinateCode = "C0565", OrdinateID = 4664, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 682, ColType = "STRING", ColNumber = 1, ColName = "C0570", HierarchyID = 0, IsRowKey = false, Label = "Description of item", OrdinateCode = "C0570", OrdinateID = 4662, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 682, ColType = "MONETARY", ColNumber = 2, ColName = "C0580", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "C0580", OrdinateID = 4663, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
