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
    public partial class S_08_01_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_08_01_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_08_01_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 84;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_08_01_01_01__sol2__2_0);
           DataTable = "T__S_08_01_01_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 246, ColType = "STRING", ColNumber = 0, ColName = "C0440", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0440", OrdinateID = 2373, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 247, ColType = "STRING", ColNumber = 1, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Derivative ID Code", OrdinateCode = "C0040", OrdinateID = 2374, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 248, ColType = "STRING", ColNumber = 2, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Fund number", OrdinateCode = "C0070", OrdinateID = 2375, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 249, ColType = "STRING", ColNumber = 3, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Instrument underlying the derivative", OrdinateCode = "C0090", OrdinateID = 2376, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0060", HierarchyID = 342, IsRowKey = false, Label = "Portfolio", OrdinateCode = "C0060", OrdinateID = 2356, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0080", HierarchyID = 220, IsRowKey = false, Label = "Derivatives held in index-linked and unit-linked contracts", OrdinateCode = "C0080", OrdinateID = 2357, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0110", HierarchyID = 343, IsRowKey = false, Label = "Use of derivative", OrdinateCode = "C0110", OrdinateID = 2358, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "DECIMAL", ColNumber = 7, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Delta", OrdinateCode = "C0120", OrdinateID = 2359, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "MONETARY", ColNumber = 8, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Notional amount of the derivative", OrdinateCode = "C0130", OrdinateID = 2360, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0140", HierarchyID = 300, IsRowKey = false, Label = "Buyer / Seller", OrdinateCode = "C0140", OrdinateID = 2361, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "MONETARY", ColNumber = 10, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Premium paid to date", OrdinateCode = "C0150", OrdinateID = 2362, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "MONETARY", ColNumber = 11, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Premium received to date", OrdinateCode = "C0160", OrdinateID = 2363, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "INTEGER", ColNumber = 12, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Number of contracts", OrdinateCode = "C0170", OrdinateID = 2364, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "INTEGER", ColNumber = 13, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Contract size", OrdinateCode = "C0180", OrdinateID = 2365, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "MONETARY", ColNumber = 14, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Maximum loss under unwinding event", OrdinateCode = "C0190", OrdinateID = 2366, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "MONETARY", ColNumber = 15, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Swap outflow amount", OrdinateCode = "C0200", OrdinateID = 2367, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "MONETARY", ColNumber = 16, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Swap inflow amount", OrdinateCode = "C0210", OrdinateID = 2368, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "DATE", ColNumber = 17, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Initial date", OrdinateCode = "C0220", OrdinateID = 2369, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "DECIMAL", ColNumber = 18, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Duration", OrdinateCode = "C0230", OrdinateID = 2370, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "MONETARY", ColNumber = 19, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Solvency II value", OrdinateCode = "C0240", OrdinateID = 2371, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 245, ColType = "ENUMERATION/CODE", ColNumber = 20, ColName = "C0250", HierarchyID = 447, IsRowKey = false, Label = "Valuation method", OrdinateCode = "C0250", OrdinateID = 2372, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 