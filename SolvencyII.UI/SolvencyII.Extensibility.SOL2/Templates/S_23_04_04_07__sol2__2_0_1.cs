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
    public partial class S_23_04_04_07__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_04_07__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_04_07__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 209;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_04_07__sol2__2_0_1);
           DataTable = "T__S_23_04_04_07__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 722, ColType = "STRING", ColNumber = 0, ColName = "C0660", HierarchyID = 0, IsRowKey = false, Label = "Number of ring-fenced fund/Matching adjustment portfolios", OrdinateCode = "C0660", OrdinateID = 4795, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 1, ColName = "C0670", HierarchyID = 0, IsRowKey = false, Label = "Notional SCR", OrdinateCode = "C0670", OrdinateID = 4790, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 2, ColName = "C0680", HierarchyID = 0, IsRowKey = false, Label = "Notional SCR (negative results set to zero)", OrdinateCode = "C0680", OrdinateID = 4791, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 3, ColName = "C0690", HierarchyID = 0, IsRowKey = false, Label = "Excess of assets over liablities", OrdinateCode = "C0690", OrdinateID = 4792, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 4, ColName = "C0700", HierarchyID = 0, IsRowKey = false, Label = "Future transfers attributable to shareholders", OrdinateCode = "C0700", OrdinateID = 4793, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 5, ColName = "C0710", HierarchyID = 0, IsRowKey = false, Label = "Adjustment for restricted own fund items in respect of matching adjustment portfolios and ring fenced funds", OrdinateCode = "C0710", OrdinateID = 4794, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
