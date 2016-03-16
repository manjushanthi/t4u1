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
    public partial class S_06_02_01_01__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_06_02_01_01__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_06_02_01_01__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 15;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_06_02_01_01__sol2__1_5_2_c);
           DataTable = "T__S_06_02_01_01__sol2__1_5_2_c";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 46, ColType = "STRING", ColNumber = 0, ColName = "PC0010", HierarchyID = 0, IsRowKey = true, Label = "Line identification", OrdinateCode = "PC0010", OrdinateID = 527, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 47, ColType = "STRING", ColNumber = 1, ColName = "C0040", HierarchyID = 0, IsRowKey = true, Label = "ID Code", OrdinateCode = "C0040", OrdinateID = 528, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 48, ColType = "STRING", ColNumber = 2, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Fund number", OrdinateCode = "C0070", OrdinateID = 529, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0060", HierarchyID = 66, IsRowKey = false, Label = "Portfolio", OrdinateCode = "C0060", OrdinateID = 517, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0090", HierarchyID = 96, IsRowKey = false, Label = "Asset held in unit linked and index linked funds (Y/N)", OrdinateCode = "C0090", OrdinateID = 518, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0100", HierarchyID = 86, IsRowKey = false, Label = "Asset pledged as collateral", OrdinateCode = "C0100", OrdinateID = 519, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0110", HierarchyID = 144, IsRowKey = false, Label = "Country of custody", OrdinateCode = "C0110", OrdinateID = 520, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "DECIMAL", ColNumber = 7, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Quantity", OrdinateCode = "C0130", OrdinateID = 521, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "MONETARY", ColNumber = 8, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Total par amount", OrdinateCode = "C0140", OrdinateID = 522, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0150", HierarchyID = 31, IsRowKey = false, Label = "Valuation method SII", OrdinateCode = "C0150", OrdinateID = 523, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "MONETARY", ColNumber = 10, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Acquisition price", OrdinateCode = "C0160", OrdinateID = 524, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "MONETARY", ColNumber = 11, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Total SII amount", OrdinateCode = "C0170", OrdinateID = 525, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 45, ColType = "MONETARY", ColNumber = 12, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Accrued interest", OrdinateCode = "C0180", OrdinateID = 526, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
