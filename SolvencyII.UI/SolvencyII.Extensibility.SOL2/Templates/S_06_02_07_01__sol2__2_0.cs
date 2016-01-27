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
    public partial class S_06_02_07_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_06_02_07_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_06_02_07_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 74;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_06_02_07_01__sol2__2_0);
           DataTable = "T__S_06_02_07_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 203, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0010", OrdinateID = 2195, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 204, ColType = "STRING", ColNumber = 1, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0040", OrdinateID = 2196, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 205, ColType = "STRING", ColNumber = 2, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Fund number", OrdinateCode = "C0070", OrdinateID = 2197, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 206, ColType = "STRING", ColNumber = 3, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Matching portfolio number", OrdinateCode = "C0080", OrdinateID = 2198, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0060", HierarchyID = 342, IsRowKey = false, Label = "Portfolio", OrdinateCode = "C0060", OrdinateID = 2182, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0090", HierarchyID = 220, IsRowKey = false, Label = "Asset held in unit linked and index linked contracts", OrdinateCode = "C0090", OrdinateID = 2183, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0100", HierarchyID = 43, IsRowKey = false, Label = "Asset pledged as collateral", OrdinateCode = "C0100", OrdinateID = 2184, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0110", HierarchyID = 190, IsRowKey = false, Label = "Country of custody", OrdinateCode = "C0110", OrdinateID = 2185, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "STRING", ColNumber = 8, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Custodian", OrdinateCode = "C0120", OrdinateID = 2186, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "DECIMAL", ColNumber = 9, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Quantity", OrdinateCode = "C0130", OrdinateID = 2187, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "MONETARY", ColNumber = 10, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Par amount", OrdinateCode = "C0140", OrdinateID = 2188, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "ENUMERATION/CODE", ColNumber = 11, ColName = "C0150", HierarchyID = 446, IsRowKey = false, Label = "Valuation method", OrdinateCode = "C0150", OrdinateID = 2189, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "MONETARY", ColNumber = 12, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Acquisition value", OrdinateCode = "C0160", OrdinateID = 2190, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "MONETARY", ColNumber = 13, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Total Solvency II amount", OrdinateCode = "C0170", OrdinateID = 2191, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "MONETARY", ColNumber = 14, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Accrued interest", OrdinateCode = "C0180", OrdinateID = 2192, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "ENUMERATION/CODE", ColNumber = 15, ColName = "C0400", HierarchyID = 348, IsRowKey = false, Label = "Deposit lodged as security in accordance with Article 162 (2)€", OrdinateCode = "C0400", OrdinateID = 2193, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 202, ColType = "ENUMERATION/CODE", ColNumber = 16, ColName = "C0410", HierarchyID = 349, IsRowKey = false, Label = "Confirmation that there are no rights of set off", OrdinateCode = "C0410", OrdinateID = 2194, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
