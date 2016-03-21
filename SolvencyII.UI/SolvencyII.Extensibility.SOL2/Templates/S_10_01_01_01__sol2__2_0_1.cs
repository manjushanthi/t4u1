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
    public partial class S_10_01_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_10_01_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_10_01_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 97;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_10_01_01_01__sol2__2_0_1);
           DataTable = "T__S_10_01_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 298, ColType = "STRING", ColNumber = 0, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0180", OrdinateID = 2570, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "STRING", ColNumber = 1, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Fund number", OrdinateCode = "C0050", OrdinateID = 2571, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0040", HierarchyID = 364, IsRowKey = false, Label = "Portfolio", OrdinateCode = "C0040", OrdinateID = 2558, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0060", HierarchyID = 316, IsRowKey = false, Label = "Asset category", OrdinateCode = "C0060", OrdinateID = 2559, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "STRING", ColNumber = 4, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Counterparty Name", OrdinateCode = "C0070", OrdinateID = 2560, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "STRING", ColNumber = 5, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Counterparty code", OrdinateCode = "C0080", OrdinateID = 2561, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0100", HierarchyID = 317, IsRowKey = false, Label = "Counterparty asset category", OrdinateCode = "C0100", OrdinateID = 2562, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0110", HierarchyID = 240, IsRowKey = false, Label = "Assets held in unit-linked and index-linked contracts", OrdinateCode = "C0110", OrdinateID = 2563, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0120", HierarchyID = 422, IsRowKey = false, Label = "Position in the Contract", OrdinateCode = "C0120", OrdinateID = 2564, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "MONETARY", ColNumber = 9, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Near leg amount", OrdinateCode = "C0130", OrdinateID = 2565, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "MONETARY", ColNumber = 10, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Far leg amount", OrdinateCode = "C0140", OrdinateID = 2566, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "DATE", ColNumber = 11, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Start date", OrdinateCode = "C0150", OrdinateID = 2567, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "DATE", ColNumber = 12, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0160", OrdinateID = 2568, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 297, ColType = "MONETARY", ColNumber = 13, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Solvency II Value", OrdinateCode = "C0170", OrdinateID = 2569, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
