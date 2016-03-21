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
    public partial class S_09_01_04_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_09_01_04_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_09_01_04_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 96;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_09_01_04_01__sol2__2_0_1);
           DataTable = "T__S_09_01_04_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 295, ColType = "STRING", ColNumber = 0, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0120", OrdinateID = 2556, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 296, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 2557, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "STRING", ColNumber = 2, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 2547, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0040", HierarchyID = 316, IsRowKey = false, Label = "Asset category", OrdinateCode = "C0040", OrdinateID = 2548, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0050", HierarchyID = 364, IsRowKey = false, Label = "Portfolio", OrdinateCode = "C0050", OrdinateID = 2549, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0060", HierarchyID = 240, IsRowKey = false, Label = "Asset held in unit-linked and index-linked contracts", OrdinateCode = "C0060", OrdinateID = 2550, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "MONETARY", ColNumber = 6, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Dividends", OrdinateCode = "C0070", OrdinateID = 2551, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "MONETARY", ColNumber = 7, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Interest", OrdinateCode = "C0080", OrdinateID = 2552, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "MONETARY", ColNumber = 8, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Rent", OrdinateCode = "C0090", OrdinateID = 2553, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "MONETARY", ColNumber = 9, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Net gains and losses", OrdinateCode = "C0100", OrdinateID = 2554, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 294, ColType = "MONETARY", ColNumber = 10, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Unrealised gains and losses", OrdinateCode = "C0110", OrdinateID = 2555, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
