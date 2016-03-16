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
    public partial class S_08_01_01_02__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_08_01_01_02__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_08_01_01_02__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 88;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_08_01_01_02__sol2__2_0_1);
           DataTable = "T__S_08_01_01_02__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 261, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Derivative ID Code", OrdinateCode = "C0040", OrdinateID = 2424, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 1, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Name", OrdinateCode = "C0260", OrdinateID = 2408, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 2, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Code", OrdinateCode = "C0270", OrdinateID = 2409, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 3, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "External rating", OrdinateCode = "C0290", OrdinateID = 2410, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 4, ColName = "C0300", HierarchyID = 0, IsRowKey = false, Label = "Nominated ECAI", OrdinateCode = "C0300", OrdinateID = 2411, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0310", HierarchyID = 38, IsRowKey = false, Label = "Credit quality step", OrdinateCode = "C0310", OrdinateID = 2412, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 6, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "Internal rating", OrdinateCode = "C0320", OrdinateID = 2413, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 7, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Group", OrdinateCode = "C0330", OrdinateID = 2414, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 8, ColName = "C0340", HierarchyID = 0, IsRowKey = false, Label = "Counterparty group code", OrdinateCode = "C0340", OrdinateID = 2415, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 9, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Contract name", OrdinateCode = "C0360", OrdinateID = 2416, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0370", HierarchyID = 179, IsRowKey = false, Label = "Currency", OrdinateCode = "C0370", OrdinateID = 2417, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 11, ColName = "C0380", HierarchyID = 0, IsRowKey = false, Label = "CIC", OrdinateCode = "C0380", OrdinateID = 2418, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "STRING", ColNumber = 12, ColName = "C0390", HierarchyID = 0, IsRowKey = false, Label = "Trigger value", OrdinateCode = "C0390", OrdinateID = 2419, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "ENUMERATION/CODE", ColNumber = 13, ColName = "C0400", HierarchyID = 288, IsRowKey = false, Label = "Unwind trigger of contract", OrdinateCode = "C0400", OrdinateID = 2420, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "ENUMERATION/CODE", ColNumber = 14, ColName = "C0410", HierarchyID = 179, IsRowKey = false, Label = "Swap delivered currency", OrdinateCode = "C0410", OrdinateID = 2421, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "ENUMERATION/CODE", ColNumber = 15, ColName = "C0420", HierarchyID = 179, IsRowKey = false, Label = "Swap received currency", OrdinateCode = "C0420", OrdinateID = 2422, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 260, ColType = "DATE", ColNumber = 16, ColName = "C0430", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0430", OrdinateID = 2423, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
