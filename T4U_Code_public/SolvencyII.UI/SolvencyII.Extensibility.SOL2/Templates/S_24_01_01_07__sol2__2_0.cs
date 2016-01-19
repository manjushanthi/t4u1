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
    public partial class S_24_01_01_07__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_24_01_01_07__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_24_01_01_07__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 216;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_24_01_01_07__sol2__2_0);
           DataTable = "T__S_24_01_01_07__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 744, ColType = "STRING", ColNumber = 0, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "Name of related undertaking", OrdinateCode = "C0370", OrdinateID = 4845, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 745, ColType = "STRING", ColNumber = 1, ColName = "C0380", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0380", OrdinateID = 4846, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 743, ColType = "MONETARY", ColNumber = 2, ColName = "C0400", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "C0400", OrdinateID = 4841, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 743, ColType = "MONETARY", ColNumber = 3, ColName = "C0410", HierarchyID = 0, IsRowKey = false, Label = "Type 1 Equity", OrdinateCode = "C0410", OrdinateID = 4842, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 743, ColType = "MONETARY", ColNumber = 4, ColName = "C0420", HierarchyID = 0, IsRowKey = false, Label = "Type 2 Equity", OrdinateCode = "C0420", OrdinateID = 4843, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 743, ColType = "MONETARY", ColNumber = 5, ColName = "C0430", HierarchyID = 0, IsRowKey = false, Label = "Subordinated liabilities", OrdinateCode = "C0430", OrdinateID = 4844, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
