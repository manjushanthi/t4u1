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
    public partial class S_32_01_02_01__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_32_01_02_01__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_32_01_02_01__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 372;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_32_01_02_01__sol2__1_5_2_c);
           DataTable = "T__S_32_01_02_01__sol2__1_5_2_c";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1515, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = true, Label = "Identification code", OrdinateCode = "C0020", OrdinateID = 6862, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "ENUMERATION/CODE", ColNumber = 1, ColName = "C0010", HierarchyID = 147, IsRowKey = false, Label = "Country", OrdinateCode = "C0010", OrdinateID = 6839, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "STRING", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Legal Name of the undertaking", OrdinateCode = "C0040", OrdinateID = 6840, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0050", HierarchyID = 80, IsRowKey = false, Label = "Type of undertaking", OrdinateCode = "C0050", OrdinateID = 6841, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "STRING", ColNumber = 4, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Legal form (Annex III L1)", OrdinateCode = "C0060", OrdinateID = 6842, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0070", HierarchyID = 81, IsRowKey = false, Label = "Category (mutual/non mutual)", OrdinateCode = "C0070", OrdinateID = 6843, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "STRING", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Supervisory Authority", OrdinateCode = "C0080", OrdinateID = 6844, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "MONETARY", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Total Balance Sheet (for (re)insurance undertakings)", OrdinateCode = "C0090", OrdinateID = 6845, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "MONETARY", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Total Balance Sheet (for other regulated undertakings)", OrdinateCode = "C0100", OrdinateID = 6846, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "MONETARY", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Total Balance Sheet (non-regulated undertakings)", OrdinateCode = "C0110", OrdinateID = 6847, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "MONETARY", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Written premiums net of reinsurance ceded under IFRS or local GAAP for insurance undertakings", OrdinateCode = "C0120", OrdinateID = 6848, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Turn over defined as the gross revenue under IFRS or local GAAP for other types of undertakings or insurance holding companies", OrdinateCode = "C0130", OrdinateID = 6849, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "MONETARY", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Underwriting performance", OrdinateCode = "C0140", OrdinateID = 6850, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "MONETARY", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Investment performance", OrdinateCode = "C0150", OrdinateID = 6851, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "MONETARY", ColNumber = 14, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Total performance", OrdinateCode = "C0160", OrdinateID = 6852, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "PERCENTAGE", ColNumber = 15, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "% capital share", OrdinateCode = "C0180", OrdinateID = 6853, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "PERCENTAGE", ColNumber = 16, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "% used for the establishment of accounting  consolidated accounts", OrdinateCode = "C0190", OrdinateID = 6854, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "PERCENTAGE", ColNumber = 17, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "% voting rights", OrdinateCode = "C0200", OrdinateID = 6855, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "STRING", ColNumber = 18, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Other criteria", OrdinateCode = "C0210", OrdinateID = 6856, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "ENUMERATION/CODE", ColNumber = 19, ColName = "C0220", HierarchyID = 131, IsRowKey = false, Label = "Level of influence", OrdinateCode = "C0220", OrdinateID = 6857, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "PERCENTAGE", ColNumber = 20, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Proportional Share retained (art.221)", OrdinateCode = "C0230", OrdinateID = 6858, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "ENUMERATION/CODE", ColNumber = 21, ColName = "C0240", HierarchyID = 130, IsRowKey = false, Label = "Inclusion in the scope of Group supervision  [YES/NO]", OrdinateCode = "C0240", OrdinateID = 6859, StartOrder = 1, NextOrder = 7  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "DATE", ColNumber = 22, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Inclusion in the scope of Group supervision [date of decision if art. 214 is applied]", OrdinateCode = "C0250", OrdinateID = 6860, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1514, ColType = "ENUMERATION/CODE", ColNumber = 23, ColName = "C0260", HierarchyID = 134, IsRowKey = false, Label = "Group solvency assessment [method chosen and under method 1, treatment of the undertaking]", OrdinateCode = "C0260", OrdinateID = 6861, StartOrder = 2, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
