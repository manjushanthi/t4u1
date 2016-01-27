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
    public partial class S_23_04_01_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_01_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_01_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 193;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_01_02__sol2__2_0);
           DataTable = "T__S_23_04_01_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 677, ColType = "STRING", ColNumber = 0, ColName = "C0185", HierarchyID = 0, IsRowKey = false, Label = "Code of preference share", OrdinateCode = "C0185", OrdinateID = 4640, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 676, ColType = "STRING", ColNumber = 1, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Description of preference shares", OrdinateCode = "C0190", OrdinateID = 4632, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 676, ColType = "MONETARY", ColNumber = 2, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Amount", OrdinateCode = "C0200", OrdinateID = 4633, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 676, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0210", HierarchyID = 184, IsRowKey = false, Label = "Counted under transitionals?", OrdinateCode = "C0210", OrdinateID = 4634, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 676, ColType = "STRING", ColNumber = 4, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Counterparty (if specific)", OrdinateCode = "C0220", OrdinateID = 4635, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 676, ColType = "DATE", ColNumber = 5, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Issue date", OrdinateCode = "C0230", OrdinateID = 4636, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 676, ColType = "DATE", ColNumber = 6, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "First call date", OrdinateCode = "C0240", OrdinateID = 4637, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 676, ColType = "STRING", ColNumber = 7, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Details of further call dates", OrdinateCode = "C0250", OrdinateID = 4638, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 676, ColType = "STRING", ColNumber = 8, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Details of incentives to redeem", OrdinateCode = "C0260", OrdinateID = 4639, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
