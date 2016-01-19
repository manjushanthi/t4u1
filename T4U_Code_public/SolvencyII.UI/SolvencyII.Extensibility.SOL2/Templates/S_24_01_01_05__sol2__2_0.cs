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
    public partial class S_24_01_01_05__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_24_01_01_05__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_24_01_01_05__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 214;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_24_01_01_05__sol2__2_0);
           DataTable = "T__S_24_01_01_05__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 736, ColType = "STRING", ColNumber = 0, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Name of related undertaking", OrdinateCode = "C0230", OrdinateID = 4831, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 737, ColType = "STRING", ColNumber = 1, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0240", OrdinateID = 4832, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 735, ColType = "MONETARY", ColNumber = 2, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "C0260", OrdinateID = 4827, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 735, ColType = "MONETARY", ColNumber = 3, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Type 1 Equity", OrdinateCode = "C0270", OrdinateID = 4828, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 735, ColType = "MONETARY", ColNumber = 4, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Type 2 Equity", OrdinateCode = "C0280", OrdinateID = 4829, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 735, ColType = "MONETARY", ColNumber = 5, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "Subordinated liabilities", OrdinateCode = "C0290", OrdinateID = 4830, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
