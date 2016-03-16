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
    public partial class S_15_02_04_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_15_02_04_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_15_02_04_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 117;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_15_02_04_01__sol2__2_0_1);
           DataTable = "T__S_15_02_04_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 356, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Product ID code", OrdinateCode = "C0040", OrdinateID = 2953, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 357, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 2954, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "STRING", ColNumber = 2, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 2942, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "STRING", ColNumber = 3, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Product Denomination", OrdinateCode = "C0050", OrdinateID = 2943, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0060", HierarchyID = 416, IsRowKey = false, Label = "Type of hedging", OrdinateCode = "C0060", OrdinateID = 2944, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0070", HierarchyID = 50, IsRowKey = false, Label = "Delta hedged", OrdinateCode = "C0070", OrdinateID = 2945, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0080", HierarchyID = 50, IsRowKey = false, Label = "Rho hedged", OrdinateCode = "C0080", OrdinateID = 2946, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0090", HierarchyID = 50, IsRowKey = false, Label = "Gamma hedged", OrdinateCode = "C0090", OrdinateID = 2947, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0100", HierarchyID = 50, IsRowKey = false, Label = "Vega hedged", OrdinateCode = "C0100", OrdinateID = 2948, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0110", HierarchyID = 50, IsRowKey = false, Label = "FX hedged", OrdinateCode = "C0110", OrdinateID = 2949, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "STRING", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Other hedged risks", OrdinateCode = "C0120", OrdinateID = 2950, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Economic result without hedging", OrdinateCode = "C0130", OrdinateID = 2951, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 355, ColType = "MONETARY", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Economic result with hedging", OrdinateCode = "C0140", OrdinateID = 2952, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
