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
    public partial class S_23_04_04_03__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_04_03__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_04_03__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 205;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_04_03__sol2__2_0_1);
           DataTable = "T__S_23_04_04_03__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 710, ColType = "STRING", ColNumber = 0, ColName = "C0265", HierarchyID = 0, IsRowKey = false, Label = "Code of subordinated liability", OrdinateCode = "C0265", OrdinateID = 4761, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "STRING", ColNumber = 1, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Description of subordinated liabilities", OrdinateCode = "C0270", OrdinateID = 4743, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "MONETARY", ColNumber = 2, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Amount", OrdinateCode = "C0280", OrdinateID = 4744, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0290", HierarchyID = 199, IsRowKey = false, Label = "Tier", OrdinateCode = "C0290", OrdinateID = 4745, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0300", HierarchyID = 179, IsRowKey = false, Label = "Currency Code", OrdinateCode = "C0300", OrdinateID = 4746, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0310", HierarchyID = 169, IsRowKey = false, Label = "Issuing entity", OrdinateCode = "C0310", OrdinateID = 4747, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "STRING", ColNumber = 6, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "Lender (if specific)", OrdinateCode = "C0320", OrdinateID = 4748, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0330", HierarchyID = 200, IsRowKey = false, Label = "Counted under transitionals?", OrdinateCode = "C0330", OrdinateID = 4749, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "STRING", ColNumber = 8, ColName = "C0340", HierarchyID = 0, IsRowKey = false, Label = "Counterparty (if specific)", OrdinateCode = "C0340", OrdinateID = 4750, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "DATE", ColNumber = 9, ColName = "C0350", HierarchyID = 0, IsRowKey = false, Label = "Issue date", OrdinateCode = "C0350", OrdinateID = 4751, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "DATE", ColNumber = 10, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0360", OrdinateID = 4752, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "DATE", ColNumber = 11, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "First call date", OrdinateCode = "C0370", OrdinateID = 4753, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "STRING", ColNumber = 12, ColName = "C0380", HierarchyID = 0, IsRowKey = false, Label = "Further call dates", OrdinateCode = "C0380", OrdinateID = 4754, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "STRING", ColNumber = 13, ColName = "C0390", HierarchyID = 0, IsRowKey = false, Label = "Details of incentives to redeem", OrdinateCode = "C0390", OrdinateID = 4755, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "DATE", ColNumber = 14, ColName = "C0400", HierarchyID = 0, IsRowKey = false, Label = "Notice period", OrdinateCode = "C0400", OrdinateID = 4756, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "STRING", ColNumber = 15, ColName = "C0410", HierarchyID = 0, IsRowKey = false, Label = "Name of supervisory authority having given authorisation", OrdinateCode = "C0410", OrdinateID = 4757, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "STRING", ColNumber = 16, ColName = "C0420", HierarchyID = 0, IsRowKey = false, Label = "Buy back during the year", OrdinateCode = "C0420", OrdinateID = 4758, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "PERCENT", ColNumber = 17, ColName = "C0430", HierarchyID = 0, IsRowKey = false, Label = "% of the issue held by entities in the group", OrdinateCode = "C0430", OrdinateID = 4759, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 709, ColType = "MONETARY", ColNumber = 18, ColName = "C0440", HierarchyID = 0, IsRowKey = false, Label = "Contribution to group subordinated liabilities", OrdinateCode = "C0440", OrdinateID = 4760, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
