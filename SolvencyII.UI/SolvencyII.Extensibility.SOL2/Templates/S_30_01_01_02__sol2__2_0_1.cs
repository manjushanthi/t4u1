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
    public partial class S_30_01_01_02__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_01_01_02__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_01_01_02__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 430;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_01_01_02__sol2__2_0_1);
           DataTable = "T__S_30_01_01_02__sol2__2_0_1";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1624, ColType = "STRING", ColNumber = 0, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance program code", OrdinateCode = "C0190", OrdinateID = 8418, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1625, ColType = "STRING", ColNumber = 1, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Risk identification code", OrdinateCode = "C0200", OrdinateID = 8419, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1626, ColType = "STRING", ColNumber = 2, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Facultative reinsurance placement identification code", OrdinateCode = "C0210", OrdinateID = 8420, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0220", HierarchyID = 418, IsRowKey = false, Label = "Finite reinsurance or similar arrangements", OrdinateCode = "C0220", OrdinateID = 8406, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0230", HierarchyID = 423, IsRowKey = false, Label = "Proportional", OrdinateCode = "C0230", OrdinateID = 8407, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "STRING", ColNumber = 5, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Identification of the company/person to which the risk relates", OrdinateCode = "C0240", OrdinateID = 8408, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "STRING", ColNumber = 6, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Description risk category covered", OrdinateCode = "C0250", OrdinateID = 8409, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "DATE", ColNumber = 7, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Validity period (start date)", OrdinateCode = "C0260", OrdinateID = 8410, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "DATE", ColNumber = 8, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Validity period (expiry date)", OrdinateCode = "C0270", OrdinateID = 8411, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0280", HierarchyID = 179, IsRowKey = false, Label = "Currency", OrdinateCode = "C0280", OrdinateID = 8412, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "MONETARY", ColNumber = 10, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "Sum Insured", OrdinateCode = "C0290", OrdinateID = 8413, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "MONETARY", ColNumber = 11, ColName = "C0300", HierarchyID = 0, IsRowKey = false, Label = "Capital at risk", OrdinateCode = "C0300", OrdinateID = 8414, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "MONETARY", ColNumber = 12, ColName = "C0310", HierarchyID = 0, IsRowKey = false, Label = "Sum reinsured on a facultative basis, with all reinsurers", OrdinateCode = "C0310", OrdinateID = 8415, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "MONETARY", ColNumber = 13, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "Facultative reinsurance premium ceded to all reinsurers for 100% of the reinsurance placement", OrdinateCode = "C0320", OrdinateID = 8416, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1623, ColType = "MONETARY", ColNumber = 14, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Facultative reinsurance commission", OrdinateCode = "C0330", OrdinateID = 8417, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
