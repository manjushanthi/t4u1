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
    public partial class S_24_01_01_02__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_24_01_01_02__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_24_01_01_02__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 214;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_24_01_01_02__sol2__2_0_1);
           DataTable = "T__S_24_01_01_02__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 738, ColType = "STRING", ColNumber = 0, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Name of related undertaking", OrdinateCode = "C0080", OrdinateID = 4841, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 739, ColType = "STRING", ColNumber = 1, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0090", OrdinateID = 4842, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 737, ColType = "MONETARY", ColNumber = 2, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "C0110", OrdinateID = 4837, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 737, ColType = "MONETARY", ColNumber = 3, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Common Equity Tier 1", OrdinateCode = "C0120", OrdinateID = 4838, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 737, ColType = "MONETARY", ColNumber = 4, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Additional Tier 1", OrdinateCode = "C0130", OrdinateID = 4839, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 737, ColType = "MONETARY", ColNumber = 5, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Tier 2", OrdinateCode = "C0140", OrdinateID = 4840, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
