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
    public partial class S_08_01_02_01__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_08_01_02_01__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_08_01_02_01__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 21;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_08_01_02_01__sol2__1_5_2_c);
           DataTable = "T__S_08_01_02_01__sol2__1_5_2_c";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 74, ColType = "STRING", ColNumber = 0, ColName = "PC0440", HierarchyID = 0, IsRowKey = true, Label = "Line identification", OrdinateCode = "PC0440", OrdinateID = 638, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 76, ColType = "STRING", ColNumber = 1, ColName = "C0040", HierarchyID = 0, IsRowKey = true, Label = "ID Code", OrdinateCode = "C0040", OrdinateID = 640, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 75, ColType = "STRING", ColNumber = 2, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 639, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 77, ColType = "STRING", ColNumber = 3, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Fund number", OrdinateCode = "C0070", OrdinateID = 641, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0060", HierarchyID = 68, IsRowKey = false, Label = "Portfolio", OrdinateCode = "C0060", OrdinateID = 622, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0080", HierarchyID = 96, IsRowKey = false, Label = "Derivatives held in unit linked and index linked funds (Y/N)", OrdinateCode = "C0080", OrdinateID = 623, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0110", HierarchyID = 69, IsRowKey = false, Label = "Use of derivative", OrdinateCode = "C0110", OrdinateID = 624, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "DECIMAL", ColNumber = 7, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Delta", OrdinateCode = "C0120", OrdinateID = 625, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "MONETARY", ColNumber = 8, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Notional amount", OrdinateCode = "C0130", OrdinateID = 626, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0140", HierarchyID = 13, IsRowKey = false, Label = "Long or short position", OrdinateCode = "C0140", OrdinateID = 627, StartOrder = 2, NextOrder = 9  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "MONETARY", ColNumber = 10, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Premium paid/received to date", OrdinateCode = "C0150", OrdinateID = 628, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "INTEGER", ColNumber = 11, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Number of contracts", OrdinateCode = "C0170", OrdinateID = 629, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "INTEGER", ColNumber = 12, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Contract dimension", OrdinateCode = "C0180", OrdinateID = 630, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "MONETARY", ColNumber = 13, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Maximum loss under unwinding event", OrdinateCode = "C0190", OrdinateID = 631, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "MONETARY", ColNumber = 14, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Swap outflow amount", OrdinateCode = "C0200", OrdinateID = 632, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "MONETARY", ColNumber = 15, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Swap inflow amount", OrdinateCode = "C0210", OrdinateID = 633, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "DATE", ColNumber = 16, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Trade date", OrdinateCode = "C0220", OrdinateID = 634, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "DECIMAL", ColNumber = 17, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Duration", OrdinateCode = "C0230", OrdinateID = 635, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "MONETARY", ColNumber = 18, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "SII value", OrdinateCode = "C0240", OrdinateID = 636, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 73, ColType = "ENUMERATION/CODE", ColNumber = 19, ColName = "C0250", HierarchyID = 34, IsRowKey = false, Label = "Valuation method SII", OrdinateCode = "C0250", OrdinateID = 637, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
