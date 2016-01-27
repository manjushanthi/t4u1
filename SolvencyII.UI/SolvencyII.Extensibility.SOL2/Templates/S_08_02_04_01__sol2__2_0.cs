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
    public partial class S_08_02_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_08_02_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_08_02_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 90;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_08_02_04_01__sol2__2_0);
           DataTable = "T__S_08_02_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 274, ColType = "STRING", ColNumber = 0, ColName = "C0440", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0440", OrdinateID = 2488, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 275, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 2489, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 276, ColType = "STRING", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Derivative ID Code", OrdinateCode = "C0040", OrdinateID = 2490, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 277, ColType = "STRING", ColNumber = 3, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Fund number", OrdinateCode = "C0070", OrdinateID = 2491, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 278, ColType = "STRING", ColNumber = 4, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Instrument underlying the derivative", OrdinateCode = "C0090", OrdinateID = 2492, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "STRING", ColNumber = 5, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 2472, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0060", HierarchyID = 342, IsRowKey = false, Label = "Portfolio", OrdinateCode = "C0060", OrdinateID = 2473, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0080", HierarchyID = 220, IsRowKey = false, Label = "Derivatives held in index-linked and unit-linked contracts", OrdinateCode = "C0080", OrdinateID = 2474, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0110", HierarchyID = 343, IsRowKey = false, Label = "Use of derivative", OrdinateCode = "C0110", OrdinateID = 2475, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "MONETARY", ColNumber = 9, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Notional amount of the derivative", OrdinateCode = "C0120", OrdinateID = 2476, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0130", HierarchyID = 301, IsRowKey = false, Label = "Buyer / Seller", OrdinateCode = "C0130", OrdinateID = 2477, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "MONETARY", ColNumber = 11, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Premium paid to date", OrdinateCode = "C0140", OrdinateID = 2478, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "MONETARY", ColNumber = 12, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Premium received to date", OrdinateCode = "C0150", OrdinateID = 2479, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "MONETARY", ColNumber = 13, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Profit and loss to date", OrdinateCode = "C0160", OrdinateID = 2480, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "INTEGER", ColNumber = 14, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Number of contracts", OrdinateCode = "C0170", OrdinateID = 2481, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "INTEGER", ColNumber = 15, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Contract size", OrdinateCode = "C0180", OrdinateID = 2482, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "MONETARY", ColNumber = 16, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Maximum loss under unwinding event", OrdinateCode = "C0190", OrdinateID = 2483, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "MONETARY", ColNumber = 17, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Swap outflow amount", OrdinateCode = "C0200", OrdinateID = 2484, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "MONETARY", ColNumber = 18, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Swap inflow amount", OrdinateCode = "C0210", OrdinateID = 2485, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "DATE", ColNumber = 19, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Initial date", OrdinateCode = "C0220", OrdinateID = 2486, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 273, ColType = "MONETARY", ColNumber = 20, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Solvency II value", OrdinateCode = "C0230", OrdinateID = 2487, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
