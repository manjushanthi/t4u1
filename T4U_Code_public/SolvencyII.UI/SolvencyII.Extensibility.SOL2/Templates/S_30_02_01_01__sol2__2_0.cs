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
    public partial class S_30_02_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_02_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_02_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 428;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_02_01_01__sol2__2_0);
           DataTable = "T__S_30_02_01_01__sol2__2_0";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1630, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance program code", OrdinateCode = "C0020", OrdinateID = 8422, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1631, ColType = "STRING", ColNumber = 1, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Risk identification code", OrdinateCode = "C0030", OrdinateID = 8423, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1632, ColType = "STRING", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Facultative reinsurance placement identification code", OrdinateCode = "C0040", OrdinateID = 8424, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1633, ColType = "STRING", ColNumber = 3, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Code reinsurer", OrdinateCode = "C0050", OrdinateID = 8425, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1634, ColType = "STRING", ColNumber = 4, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Code broker", OrdinateCode = "C0070", OrdinateID = 8426, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1629, ColType = "STRING", ColNumber = 5, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Activity code broker", OrdinateCode = "C0090", OrdinateID = 8416, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1629, ColType = "PERCENTAGE", ColNumber = 6, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Share reinsurer (%)", OrdinateCode = "C0100", OrdinateID = 8417, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1629, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0110", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0110", OrdinateID = 8418, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1629, ColType = "MONETARY", ColNumber = 8, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Sum reinsured to facultative reinsurer", OrdinateCode = "C0120", OrdinateID = 8419, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1629, ColType = "MONETARY", ColNumber = 9, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Facultative ceded reinsurance premium", OrdinateCode = "C0130", OrdinateID = 8420, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1629, ColType = "STRING", ColNumber = 10, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Annotations", OrdinateCode = "C0140", OrdinateID = 8421, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
