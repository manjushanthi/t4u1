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
    public partial class S_32_01_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_32_01_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_32_01_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 445;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_32_01_04_01__sol2__2_0);
           DataTable = "T__S_32_01_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1703, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 8657, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "ENUMERATION/CODE", ColNumber = 1, ColName = "C0010", HierarchyID = 207, IsRowKey = false, Label = "Country", OrdinateCode = "C0010", OrdinateID = 8623, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "STRING", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Legal Name of the undertaking", OrdinateCode = "C0040", OrdinateID = 8625, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0050", HierarchyID = 388, IsRowKey = false, Label = "Type of undertaking", OrdinateCode = "C0050", OrdinateID = 8627, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "STRING", ColNumber = 4, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Legal form", OrdinateCode = "C0060", OrdinateID = 8629, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0070", HierarchyID = 382, IsRowKey = false, Label = "Category (mutual/non mutual)", OrdinateCode = "C0070", OrdinateID = 8631, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "STRING", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Supervisory Authority", OrdinateCode = "C0080", OrdinateID = 8633, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "MONETARY", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Total Balance Sheet (for (re)insurance undertakings)", OrdinateCode = "C0090", OrdinateID = 8635, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "MONETARY", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Total Balance Sheet (for other regulated undertakings)", OrdinateCode = "C0100", OrdinateID = 8636, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "MONETARY", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Total Balance Sheet (non-regulated undertakings)", OrdinateCode = "C0110", OrdinateID = 8637, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "MONETARY", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Written premiums net of reinsurance ceded under IFRS or local GAAP for (re)insurance undertakings", OrdinateCode = "C0120", OrdinateID = 8638, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Turn over defined as the gross revenue under IFRS or local GAAP for other types of undertakings or insurance holding companies", OrdinateCode = "C0130", OrdinateID = 8639, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "MONETARY", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Underwriting performance", OrdinateCode = "C0140", OrdinateID = 8640, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "MONETARY", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Investment performance", OrdinateCode = "C0150", OrdinateID = 8641, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "MONETARY", ColNumber = 14, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Total performance", OrdinateCode = "C0160", OrdinateID = 8642, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "ENUMERATION/CODE", ColNumber = 15, ColName = "C0170", HierarchyID = 11, IsRowKey = false, Label = "Accounting standard", OrdinateCode = "C0170", OrdinateID = 8644, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "PERCENTAGE", ColNumber = 16, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "% capital share", OrdinateCode = "C0180", OrdinateID = 8646, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "PERCENTAGE", ColNumber = 17, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "% used for the establishment of accounting consolidated accounts", OrdinateCode = "C0190", OrdinateID = 8647, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "PERCENTAGE", ColNumber = 18, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "% voting rights", OrdinateCode = "C0200", OrdinateID = 8648, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "STRING", ColNumber = 19, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Other criteria", OrdinateCode = "C0210", OrdinateID = 8649, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "ENUMERATION/CODE", ColNumber = 20, ColName = "C0220", HierarchyID = 145, IsRowKey = false, Label = "Level of influence", OrdinateCode = "C0220", OrdinateID = 8650, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "PERCENTAGE", ColNumber = 21, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Proportional share used for group solvency calculation", OrdinateCode = "C0230", OrdinateID = 8651, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "ENUMERATION/CODE", ColNumber = 22, ColName = "C0240", HierarchyID = 162, IsRowKey = false, Label = "Yes/No", OrdinateCode = "C0240", OrdinateID = 8653, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "DATE", ColNumber = 23, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Date of decision if art. 214 is applied", OrdinateCode = "C0250", OrdinateID = 8654, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1702, ColType = "ENUMERATION/CODE", ColNumber = 24, ColName = "C0260", HierarchyID = 158, IsRowKey = false, Label = "Method used and under method 1, treatment of the undertaking", OrdinateCode = "C0260", OrdinateID = 8656, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
