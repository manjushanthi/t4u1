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
    public partial class S_24_01_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_24_01_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_24_01_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 210;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_24_01_01_01__sol2__2_0);
           DataTable = "T__S_24_01_01_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 722, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Name of related undertaking", OrdinateCode = "C0010", OrdinateID = 4803, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 723, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0020", OrdinateID = 4804, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "C0040", OrdinateID = 4799, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 3, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Common Equity Tier 1", OrdinateCode = "C0050", OrdinateID = 4800, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 4, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Additional Tier 1", OrdinateCode = "C0060", OrdinateID = 4801, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 721, ColType = "MONETARY", ColNumber = 5, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Tier 2", OrdinateCode = "C0070", OrdinateID = 4802, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
