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
    public partial class S_30_02_01_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_02_01_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_02_01_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 429;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_02_01_02__sol2__2_0);
           DataTable = "T__S_30_02_01_02__sol2__2_0";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1638, ColType = "STRING", ColNumber = 0, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance program code", OrdinateCode = "C0150", OrdinateID = 8435, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1639, ColType = "STRING", ColNumber = 1, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Risk identification code", OrdinateCode = "C0160", OrdinateID = 8436, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1640, ColType = "STRING", ColNumber = 2, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Facultative reinsurance placement identification code", OrdinateCode = "C0170", OrdinateID = 8437, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1641, ColType = "STRING", ColNumber = 3, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Code reinsurer", OrdinateCode = "C0180", OrdinateID = 8438, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1642, ColType = "STRING", ColNumber = 4, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Code broker", OrdinateCode = "C0200", OrdinateID = 8439, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1637, ColType = "STRING", ColNumber = 5, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Activity code broker", OrdinateCode = "C0220", OrdinateID = 8429, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1637, ColType = "PERCENTAGE", ColNumber = 6, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Share reinsurer (%)", OrdinateCode = "C0230", OrdinateID = 8430, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1637, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0240", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0240", OrdinateID = 8431, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1637, ColType = "MONETARY", ColNumber = 8, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Sum reinsured to facultative reinsurer", OrdinateCode = "C0250", OrdinateID = 8432, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1637, ColType = "MONETARY", ColNumber = 9, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Facultative ceded reinsurance premium", OrdinateCode = "C0260", OrdinateID = 8433, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1637, ColType = "STRING", ColNumber = 10, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Annotations", OrdinateCode = "C0270", OrdinateID = 8434, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
