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
    public partial class S_23_04_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 195;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_01_01__sol2__2_0_1);
           DataTable = "T__S_23_04_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 687, ColType = "STRING", ColNumber = 0, ColName = "C0005", HierarchyID = 0, IsRowKey = false, Label = "Code of subordinated MMA", OrdinateCode = "C0005", OrdinateID = 4664, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "STRING", ColNumber = 1, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Description of subordinated mutual members' accounts", OrdinateCode = "C0010", OrdinateID = 4651, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "MONETARY", ColNumber = 2, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Amount", OrdinateCode = "C0020", OrdinateID = 4652, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0030", HierarchyID = 199, IsRowKey = false, Label = "Tier", OrdinateCode = "C0030", OrdinateID = 4653, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0040", HierarchyID = 179, IsRowKey = false, Label = "Currency Code", OrdinateCode = "C0040", OrdinateID = 4654, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0070", HierarchyID = 200, IsRowKey = false, Label = "Counted under transitionals?", OrdinateCode = "C0070", OrdinateID = 4655, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "STRING", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Counterparty (if specific)", OrdinateCode = "C0080", OrdinateID = 4656, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "DATE", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Issue date", OrdinateCode = "C0090", OrdinateID = 4657, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "DATE", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0100", OrdinateID = 4658, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "DATE", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "First call date", OrdinateCode = "C0110", OrdinateID = 4659, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "STRING", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Details of further call dates", OrdinateCode = "C0120", OrdinateID = 4660, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "STRING", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Details of incentives to redeem", OrdinateCode = "C0130", OrdinateID = 4661, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "DATE", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Notice period", OrdinateCode = "C0140", OrdinateID = 4662, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 686, ColType = "STRING", ColNumber = 13, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Buy back during the year", OrdinateCode = "C0160", OrdinateID = 4663, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
