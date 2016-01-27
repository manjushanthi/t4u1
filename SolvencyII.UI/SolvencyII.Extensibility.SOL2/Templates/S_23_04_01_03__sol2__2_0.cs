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
    public partial class S_23_04_01_03__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_01_03__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_01_03__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 194;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_01_03__sol2__2_0);
           DataTable = "T__S_23_04_01_03__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 679, ColType = "STRING", ColNumber = 0, ColName = "C0265", HierarchyID = 0, IsRowKey = false, Label = "Code of subordinated liability", OrdinateCode = "C0265", OrdinateID = 4653, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "STRING", ColNumber = 1, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Description of subordinated liabilities", OrdinateCode = "C0270", OrdinateID = 4641, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "MONETARY", ColNumber = 2, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Amount", OrdinateCode = "C0280", OrdinateID = 4642, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0290", HierarchyID = 183, IsRowKey = false, Label = "Tier", OrdinateCode = "C0290", OrdinateID = 4643, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0300", HierarchyID = 163, IsRowKey = false, Label = "Currency Code", OrdinateCode = "C0300", OrdinateID = 4644, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "STRING", ColNumber = 5, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "Lender (if specific)", OrdinateCode = "C0320", OrdinateID = 4645, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0330", HierarchyID = 184, IsRowKey = false, Label = "Counted under transitionals?", OrdinateCode = "C0330", OrdinateID = 4646, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "DATE", ColNumber = 7, ColName = "C0350", HierarchyID = 0, IsRowKey = false, Label = "Issue date", OrdinateCode = "C0350", OrdinateID = 4647, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "DATE", ColNumber = 8, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0360", OrdinateID = 4648, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "DATE", ColNumber = 9, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "First call date", OrdinateCode = "C0370", OrdinateID = 4649, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "STRING", ColNumber = 10, ColName = "C0380", HierarchyID = 0, IsRowKey = false, Label = "Further call dates", OrdinateCode = "C0380", OrdinateID = 4650, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "STRING", ColNumber = 11, ColName = "C0390", HierarchyID = 0, IsRowKey = false, Label = "Details of incentives to redeem", OrdinateCode = "C0390", OrdinateID = 4651, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 678, ColType = "DATE", ColNumber = 12, ColName = "C0400", HierarchyID = 0, IsRowKey = false, Label = "Notice period", OrdinateCode = "C0400", OrdinateID = 4652, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
