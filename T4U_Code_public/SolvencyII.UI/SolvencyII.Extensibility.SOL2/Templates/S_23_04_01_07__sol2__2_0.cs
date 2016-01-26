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
    public partial class S_23_04_01_07__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_01_07__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_01_07__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 198;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_01_07__sol2__2_0);
           DataTable = "T__S_23_04_01_07__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 687, ColType = "STRING", ColNumber = 0, ColName = "C0655", HierarchyID = 0, IsRowKey = false, Label = "Code of fund", OrdinateCode = "C0655", OrdinateID = 4677, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "STRING", ColNumber = 1, ColName = "C0660", HierarchyID = 0, IsRowKey = false, Label = "Name of ring-fenced fund/Matching adjustment portfolios", OrdinateCode = "C0660", OrdinateID = 4671, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "MONETARY", ColNumber = 2, ColName = "C0670", HierarchyID = 0, IsRowKey = false, Label = "Notional SCR", OrdinateCode = "C0670", OrdinateID = 4672, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "MONETARY", ColNumber = 3, ColName = "C0680", HierarchyID = 0, IsRowKey = false, Label = "Notional SCR (negative results set to zero)", OrdinateCode = "C0680", OrdinateID = 4673, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "MONETARY", ColNumber = 4, ColName = "C0690", HierarchyID = 0, IsRowKey = false, Label = "Excess of assets over liablities", OrdinateCode = "C0690", OrdinateID = 4674, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "MONETARY", ColNumber = 5, ColName = "C0700", HierarchyID = 0, IsRowKey = false, Label = "Future transfers attributable to shareholders", OrdinateCode = "C0700", OrdinateID = 4675, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "MONETARY", ColNumber = 6, ColName = "C0710", HierarchyID = 0, IsRowKey = false, Label = "Adjustment for restricted own fund items in respect of matching adjustment portfolios and ring fenced funds", OrdinateCode = "C0710", OrdinateID = 4676, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 