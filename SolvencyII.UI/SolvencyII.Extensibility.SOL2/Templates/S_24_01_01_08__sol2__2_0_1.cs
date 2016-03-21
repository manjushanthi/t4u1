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
    public partial class S_24_01_01_08__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_24_01_01_08__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_24_01_01_08__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 220;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_24_01_01_08__sol2__2_0_1);
           DataTable = "T__S_24_01_01_08__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 760, ColType = "STRING", ColNumber = 0, ColName = "C0440", HierarchyID = 0, IsRowKey = false, Label = "Name of related undertaking", OrdinateCode = "C0440", OrdinateID = 4883, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 761, ColType = "STRING", ColNumber = 1, ColName = "C0450", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0450", OrdinateID = 4884, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 759, ColType = "MONETARY", ColNumber = 2, ColName = "C0470", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "C0470", OrdinateID = 4879, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 759, ColType = "MONETARY", ColNumber = 3, ColName = "C0480", HierarchyID = 0, IsRowKey = false, Label = "Type 1 Equity", OrdinateCode = "C0480", OrdinateID = 4880, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 759, ColType = "MONETARY", ColNumber = 4, ColName = "C0490", HierarchyID = 0, IsRowKey = false, Label = "Type 2 Equity", OrdinateCode = "C0490", OrdinateID = 4881, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 759, ColType = "MONETARY", ColNumber = 5, ColName = "C0500", HierarchyID = 0, IsRowKey = false, Label = "Subordinated liabilities", OrdinateCode = "C0500", OrdinateID = 4882, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
