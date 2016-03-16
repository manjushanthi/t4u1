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
    public partial class S_11_01_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_11_01_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_11_01_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 99;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_11_01_01_01__sol2__2_0_1);
           DataTable = "T__S_11_01_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 305, ColType = "STRING", ColNumber = 0, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0290", OrdinateID = 2599, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 306, ColType = "STRING", ColNumber = 1, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0040", OrdinateID = 2600, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "STRING", ColNumber = 2, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Name of counterparty pledging the collateral", OrdinateCode = "C0060", OrdinateID = 2589, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "STRING", ColNumber = 3, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Name of the group of the counterparty pledging the collateral", OrdinateCode = "C0070", OrdinateID = 2590, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0080", HierarchyID = 206, IsRowKey = false, Label = "Country of custody", OrdinateCode = "C0080", OrdinateID = 2591, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "DECIMAL", ColNumber = 5, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Quantity", OrdinateCode = "C0090", OrdinateID = 2592, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "MONETARY", ColNumber = 6, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Par amount", OrdinateCode = "C0100", OrdinateID = 2593, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0110", HierarchyID = 469, IsRowKey = false, Label = "Valuation method", OrdinateCode = "C0110", OrdinateID = 2594, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "MONETARY", ColNumber = 8, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Total amount", OrdinateCode = "C0120", OrdinateID = 2595, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "MONETARY", ColNumber = 9, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Accrued interest", OrdinateCode = "C0130", OrdinateID = 2596, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0140", HierarchyID = 319, IsRowKey = false, Label = "Type of asset for which the collateral is held", OrdinateCode = "C0140", OrdinateID = 2598, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
