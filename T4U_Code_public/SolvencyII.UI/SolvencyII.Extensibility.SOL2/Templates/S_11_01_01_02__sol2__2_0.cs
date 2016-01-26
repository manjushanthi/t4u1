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
    public partial class S_11_01_01_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_11_01_01_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_11_01_01_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 97;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_11_01_01_02__sol2__2_0);
           DataTable = "T__S_11_01_01_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 300, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0040", OrdinateID = 2585, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "STRING", ColNumber = 1, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Item Title", OrdinateCode = "C0150", OrdinateID = 2573, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "STRING", ColNumber = 2, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Issuer Name", OrdinateCode = "C0160", OrdinateID = 2574, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "STRING", ColNumber = 3, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Issuer Code", OrdinateCode = "C0170", OrdinateID = 2575, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0190", HierarchyID = 316, IsRowKey = false, Label = "Issuer Sector", OrdinateCode = "C0190", OrdinateID = 2576, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "STRING", ColNumber = 5, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group name", OrdinateCode = "C0200", OrdinateID = 2577, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "STRING", ColNumber = 6, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group Code", OrdinateCode = "C0210", OrdinateID = 2578, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0230", HierarchyID = 193, IsRowKey = false, Label = "Issuer Country", OrdinateCode = "C0230", OrdinateID = 2579, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0240", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0240", OrdinateID = 2580, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "STRING", ColNumber = 9, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "CIC", OrdinateCode = "C0250", OrdinateID = 2581, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "MONETARY", ColNumber = 10, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Unit price", OrdinateCode = "C0260", OrdinateID = 2582, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "PERCENTAGE", ColNumber = 11, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Unit percentage of par amount Solvency II price", OrdinateCode = "C0270", OrdinateID = 2583, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 299, ColType = "DATE", ColNumber = 12, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0280", OrdinateID = 2584, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 