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
    public partial class S_08_02_04_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_08_02_04_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_08_02_04_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 91;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_08_02_04_02__sol2__2_0);
           DataTable = "T__S_08_02_04_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 281, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Derivative ID Code", OrdinateCode = "C0040", OrdinateID = 2506, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "STRING", ColNumber = 1, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Name", OrdinateCode = "C0240", OrdinateID = 2494, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "STRING", ColNumber = 2, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Code", OrdinateCode = "C0250", OrdinateID = 2495, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "STRING", ColNumber = 3, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Counterparty group", OrdinateCode = "C0270", OrdinateID = 2496, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "STRING", ColNumber = 4, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Group Code", OrdinateCode = "C0280", OrdinateID = 2497, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "STRING", ColNumber = 5, ColName = "C0300", HierarchyID = 0, IsRowKey = false, Label = "Contract name", OrdinateCode = "C0300", OrdinateID = 2498, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0310", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0310", OrdinateID = 2499, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "STRING", ColNumber = 7, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "CIC", OrdinateCode = "C0320", OrdinateID = 2500, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "STRING", ColNumber = 8, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Trigger value", OrdinateCode = "C0330", OrdinateID = 2501, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0340", HierarchyID = 268, IsRowKey = false, Label = "Unwind trigger of contract", OrdinateCode = "C0340", OrdinateID = 2502, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0350", HierarchyID = 163, IsRowKey = false, Label = "Swap delivered currency", OrdinateCode = "C0350", OrdinateID = 2503, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "ENUMERATION/CODE", ColNumber = 11, ColName = "C0360", HierarchyID = 163, IsRowKey = false, Label = "Swap received currency", OrdinateCode = "C0360", OrdinateID = 2504, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 280, ColType = "DATE", ColNumber = 12, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0370", OrdinateID = 2505, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
