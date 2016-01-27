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
    public partial class S_07_01_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_07_01_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_07_01_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 83;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_07_01_04_01__sol2__2_0);
           DataTable = "T__S_07_01_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 241, ColType = "STRING", ColNumber = 0, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0200", OrdinateID = 2352, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 242, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 2353, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 243, ColType = "STRING", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0040", OrdinateID = 2354, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "STRING", ColNumber = 3, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 2337, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0060", HierarchyID = 296, IsRowKey = false, Label = "Collateral type", OrdinateCode = "C0060", OrdinateID = 2338, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0070", HierarchyID = 242, IsRowKey = false, Label = "Type of structured product", OrdinateCode = "C0070", OrdinateID = 2339, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0080", HierarchyID = 46, IsRowKey = false, Label = "Capital protection", OrdinateCode = "C0080", OrdinateID = 2340, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0090", HierarchyID = 319, IsRowKey = false, Label = "Underlying security / index / portfolio", OrdinateCode = "C0090", OrdinateID = 2341, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0100", HierarchyID = 318, IsRowKey = false, Label = "Callable or Putable", OrdinateCode = "C0100", OrdinateID = 2342, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0110", HierarchyID = 298, IsRowKey = false, Label = "Synthetic structured product", OrdinateCode = "C0110", OrdinateID = 2343, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0120", HierarchyID = 320, IsRowKey = false, Label = "Prepayment structured product", OrdinateCode = "C0120", OrdinateID = 2344, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Collateral value", OrdinateCode = "C0130", OrdinateID = 2345, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "ENUMERATION/CODE", ColNumber = 12, ColName = "C0140", HierarchyID = 52, IsRowKey = false, Label = "Collateral portfolio", OrdinateCode = "C0140", OrdinateID = 2346, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "PERCENTAGE", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Fixed Annual Return", OrdinateCode = "C0150", OrdinateID = 2347, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "STRING", ColNumber = 14, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Variable Annual Return", OrdinateCode = "C0160", OrdinateID = 2348, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "PERCENTAGE", ColNumber = 15, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Loss given default", OrdinateCode = "C0170", OrdinateID = 2349, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "PERCENTAGE", ColNumber = 16, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Attachment point", OrdinateCode = "C0180", OrdinateID = 2350, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 240, ColType = "PERCENTAGE", ColNumber = 17, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Detachment point", OrdinateCode = "C0190", OrdinateID = 2351, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
