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
    public partial class S_11_01_04_02__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_11_01_04_02__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_11_01_04_02__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 102;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_11_01_04_02__sol2__2_0_1);
           DataTable = "T__S_11_01_04_02__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 317, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0040", OrdinateID = 2647, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "STRING", ColNumber = 1, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 2634, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "STRING", ColNumber = 2, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Item Title", OrdinateCode = "C0150", OrdinateID = 2635, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "STRING", ColNumber = 3, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Issuer Name", OrdinateCode = "C0160", OrdinateID = 2636, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "STRING", ColNumber = 4, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Issuer Code", OrdinateCode = "C0170", OrdinateID = 2637, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0190", HierarchyID = 338, IsRowKey = false, Label = "Issuer Sector", OrdinateCode = "C0190", OrdinateID = 2638, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "STRING", ColNumber = 6, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group name", OrdinateCode = "C0200", OrdinateID = 2639, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "STRING", ColNumber = 7, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group Code", OrdinateCode = "C0210", OrdinateID = 2640, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0230", HierarchyID = 209, IsRowKey = false, Label = "Issuer Country", OrdinateCode = "C0230", OrdinateID = 2641, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0240", HierarchyID = 179, IsRowKey = false, Label = "Currency", OrdinateCode = "C0240", OrdinateID = 2642, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "STRING", ColNumber = 10, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "CIC", OrdinateCode = "C0250", OrdinateID = 2643, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "MONETARY", ColNumber = 11, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Unit price", OrdinateCode = "C0260", OrdinateID = 2644, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "PERCENT", ColNumber = 12, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Unit percentage of par amount Solvency II price", OrdinateCode = "C0270", OrdinateID = 2645, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 316, ColType = "DATE", ColNumber = 13, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0280", OrdinateID = 2646, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
