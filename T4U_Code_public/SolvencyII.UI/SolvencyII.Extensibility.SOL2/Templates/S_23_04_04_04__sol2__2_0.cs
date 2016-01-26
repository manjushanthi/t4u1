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
    public partial class S_23_04_04_04__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_04_04__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_04_04__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 203;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_04_04__sol2__2_0);
           DataTable = "T__S_23_04_04_04__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 701, ColType = "STRING", ColNumber = 0, ColName = "C0445", HierarchyID = 0, IsRowKey = false, Label = "Code of items approved by supervisory authority as basic own funds", OrdinateCode = "C0445", OrdinateID = 4743, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "STRING", ColNumber = 1, ColName = "C0450", HierarchyID = 0, IsRowKey = false, Label = "Other items approved by supervisory authority as basic own funds not specified above", OrdinateCode = "C0450", OrdinateID = 4731, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "MONETARY", ColNumber = 2, ColName = "C0460", HierarchyID = 0, IsRowKey = false, Label = "Amount", OrdinateCode = "C0460", OrdinateID = 4732, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0470", HierarchyID = 163, IsRowKey = false, Label = "Currency Code", OrdinateCode = "C0470", OrdinateID = 4733, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "MONETARY", ColNumber = 4, ColName = "C0480", HierarchyID = 0, IsRowKey = false, Label = "Tier 1", OrdinateCode = "C0480", OrdinateID = 4734, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "MONETARY", ColNumber = 5, ColName = "C0490", HierarchyID = 0, IsRowKey = false, Label = "Tier 2", OrdinateCode = "C0490", OrdinateID = 4735, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "MONETARY", ColNumber = 6, ColName = "C0500", HierarchyID = 0, IsRowKey = false, Label = "Tier 3", OrdinateCode = "C0500", OrdinateID = 4736, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "DATE", ColNumber = 7, ColName = "C0510", HierarchyID = 0, IsRowKey = false, Label = "Date of authorisation", OrdinateCode = "C0510", OrdinateID = 4737, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "STRING", ColNumber = 8, ColName = "C0520", HierarchyID = 0, IsRowKey = false, Label = "Name of supervisory authority having given authorisation", OrdinateCode = "C0520", OrdinateID = 4738, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "STRING", ColNumber = 9, ColName = "C0530", HierarchyID = 0, IsRowKey = false, Label = "Name of entity concerned", OrdinateCode = "C0530", OrdinateID = 4739, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "STRING", ColNumber = 10, ColName = "C0540", HierarchyID = 0, IsRowKey = false, Label = "Buy back during the year", OrdinateCode = "C0540", OrdinateID = 4740, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "PERCENTAGE", ColNumber = 11, ColName = "C0550", HierarchyID = 0, IsRowKey = false, Label = "% of the issue held by entities in the group", OrdinateCode = "C0550", OrdinateID = 4741, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 700, ColType = "MONETARY", ColNumber = 12, ColName = "C0560", HierarchyID = 0, IsRowKey = false, Label = "Contribution to group other basic own funds", OrdinateCode = "C0560", OrdinateID = 4742, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 