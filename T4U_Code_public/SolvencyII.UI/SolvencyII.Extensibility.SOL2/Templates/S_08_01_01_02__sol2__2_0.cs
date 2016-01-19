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
    public partial class S_08_01_01_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_08_01_01_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_08_01_01_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 85;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_08_01_01_02__sol2__2_0);
           DataTable = "T__S_08_01_01_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 252, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Derivative ID Code", OrdinateCode = "C0040", OrdinateID = 2394, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 1, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Name", OrdinateCode = "C0260", OrdinateID = 2378, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 2, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Code", OrdinateCode = "C0270", OrdinateID = 2379, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 3, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "External rating", OrdinateCode = "C0290", OrdinateID = 2380, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 4, ColName = "C0300", HierarchyID = 0, IsRowKey = false, Label = "Nominated ECAI", OrdinateCode = "C0300", OrdinateID = 2381, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0310", HierarchyID = 36, IsRowKey = false, Label = "Credit quality step", OrdinateCode = "C0310", OrdinateID = 2382, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 6, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "Internal rating", OrdinateCode = "C0320", OrdinateID = 2383, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 7, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Group", OrdinateCode = "C0330", OrdinateID = 2384, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 8, ColName = "C0340", HierarchyID = 0, IsRowKey = false, Label = "Counterparty group Code", OrdinateCode = "C0340", OrdinateID = 2385, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 9, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Contract name", OrdinateCode = "C0360", OrdinateID = 2386, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0370", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0370", OrdinateID = 2387, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 11, ColName = "C0380", HierarchyID = 0, IsRowKey = false, Label = "CIC", OrdinateCode = "C0380", OrdinateID = 2388, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "STRING", ColNumber = 12, ColName = "C0390", HierarchyID = 0, IsRowKey = false, Label = "Trigger value", OrdinateCode = "C0390", OrdinateID = 2389, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "ENUMERATION/CODE", ColNumber = 13, ColName = "C0400", HierarchyID = 268, IsRowKey = false, Label = "Unwind trigger of contract", OrdinateCode = "C0400", OrdinateID = 2390, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "ENUMERATION/CODE", ColNumber = 14, ColName = "C0410", HierarchyID = 163, IsRowKey = false, Label = "Swap delivered currency", OrdinateCode = "C0410", OrdinateID = 2391, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "ENUMERATION/CODE", ColNumber = 15, ColName = "C0420", HierarchyID = 163, IsRowKey = false, Label = "Swap received currency", OrdinateCode = "C0420", OrdinateID = 2392, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 251, ColType = "DATE", ColNumber = 16, ColName = "C0430", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0430", OrdinateID = 2393, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
