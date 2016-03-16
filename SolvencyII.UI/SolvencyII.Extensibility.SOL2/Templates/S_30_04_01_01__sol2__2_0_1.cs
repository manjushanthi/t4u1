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
    public partial class S_30_04_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_04_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_04_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 436;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_04_01_01__sol2__2_0_1);
           DataTable = "T__S_30_04_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1659, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance program code", OrdinateCode = "C0010", OrdinateID = 8507, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1660, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Treaty identification code", OrdinateCode = "C0020", OrdinateID = 8508, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1661, ColType = "STRING", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Progressive section number in treaty", OrdinateCode = "C0030", OrdinateID = 8509, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1662, ColType = "STRING", ColNumber = 3, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Progressive number of surplus/layer in program", OrdinateCode = "C0040", OrdinateID = 8510, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1663, ColType = "STRING", ColNumber = 4, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Code reinsurer", OrdinateCode = "C0050", OrdinateID = 8511, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1664, ColType = "STRING", ColNumber = 5, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Code collateral provider (if applicable)", OrdinateCode = "C0140", OrdinateID = 8512, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1665, ColType = "STRING", ColNumber = 6, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Code broker", OrdinateCode = "C0070", OrdinateID = 8513, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1658, ColType = "STRING", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Activity code broker", OrdinateCode = "C0090", OrdinateID = 8500, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1658, ColType = "PERCENT", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Share reinsurer (%)", OrdinateCode = "C0100", OrdinateID = 8501, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1658, ColType = "MONETARY", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Exposure ceded for reinsurer's share (amount)", OrdinateCode = "C0110", OrdinateID = 8502, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1658, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0120", HierarchyID = 306, IsRowKey = false, Label = "Type of collateral (if applicable)", OrdinateCode = "C0120", OrdinateID = 8503, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1658, ColType = "STRING", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Description of the reinsurers limit collateralised", OrdinateCode = "C0130", OrdinateID = 8504, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1658, ColType = "MONETARY", ColNumber = 12, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Estimated outgoing reinsurance premium for reinsurer's share", OrdinateCode = "C0160", OrdinateID = 8505, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1658, ColType = "STRING", ColNumber = 13, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Annotations", OrdinateCode = "C0170", OrdinateID = 8506, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
