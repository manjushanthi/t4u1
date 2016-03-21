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
    public partial class S_31_01_04_02__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_31_01_04_02__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_31_01_04_02__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 443;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_31_01_04_02__sol2__2_0_1);
           DataTable = "T__S_31_01_04_02__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1688, ColType = "STRING", ColNumber = 0, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Code reinsurer", OrdinateCode = "C0160", OrdinateID = 8572, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1687, ColType = "STRING", ColNumber = 1, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Legal name reinsurer", OrdinateCode = "C0180", OrdinateID = 8565, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1687, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0190", HierarchyID = 410, IsRowKey = false, Label = "Type of reinsurer", OrdinateCode = "C0190", OrdinateID = 8566, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1687, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0200", HierarchyID = 223, IsRowKey = false, Label = "Country of residency", OrdinateCode = "C0200", OrdinateID = 8567, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1687, ColType = "STRING", ColNumber = 4, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "External rating assessment by nominated ECAI", OrdinateCode = "C0210", OrdinateID = 8568, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1687, ColType = "STRING", ColNumber = 5, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Nominated ECAI", OrdinateCode = "C0220", OrdinateID = 8569, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1687, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0230", HierarchyID = 38, IsRowKey = false, Label = "Credit quality step", OrdinateCode = "C0230", OrdinateID = 8570, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1687, ColType = "STRING", ColNumber = 7, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Internal rating", OrdinateCode = "C0240", OrdinateID = 8571, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
