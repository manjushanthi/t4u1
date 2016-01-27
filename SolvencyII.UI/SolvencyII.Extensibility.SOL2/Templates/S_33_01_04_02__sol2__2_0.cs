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
    public partial class S_33_01_04_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_33_01_04_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_33_01_04_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 447;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_33_01_04_02__sol2__2_0);
           DataTable = "T__S_33_01_04_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1710, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 8701, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1709, ColType = "MONETARY", ColNumber = 1, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Local capital requirement", OrdinateCode = "C0240", OrdinateID = 8698, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1709, ColType = "MONETARY", ColNumber = 2, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Local minimum capital requirement", OrdinateCode = "C0250", OrdinateID = 8699, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1709, ColType = "MONETARY", ColNumber = 3, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Eligible own funds in accordance with local rules", OrdinateCode = "C0260", OrdinateID = 8700, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
