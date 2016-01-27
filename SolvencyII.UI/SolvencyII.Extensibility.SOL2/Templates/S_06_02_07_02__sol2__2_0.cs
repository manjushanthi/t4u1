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
    public partial class S_06_02_07_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_06_02_07_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_06_02_07_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 75;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_06_02_07_02__sol2__2_0);
           DataTable = "T__S_06_02_07_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 209, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0040", OrdinateID = 2219, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 1, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Item Title", OrdinateCode = "C0190", OrdinateID = 2200, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 2, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Issuer Name", OrdinateCode = "C0200", OrdinateID = 2201, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 3, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Issuer Code", OrdinateCode = "C0210", OrdinateID = 2202, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0230", HierarchyID = 316, IsRowKey = false, Label = "Issuer Sector", OrdinateCode = "C0230", OrdinateID = 2203, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 5, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group", OrdinateCode = "C0240", OrdinateID = 2204, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 6, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group Code", OrdinateCode = "C0250", OrdinateID = 2205, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0270", HierarchyID = 193, IsRowKey = false, Label = "Issuer Country", OrdinateCode = "C0270", OrdinateID = 2206, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0280", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0280", OrdinateID = 2207, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 9, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "CIC", OrdinateCode = "C0290", OrdinateID = 2208, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0300", HierarchyID = 295, IsRowKey = false, Label = "Infrastructure investment", OrdinateCode = "C0300", OrdinateID = 2209, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "ENUMERATION/CODE", ColNumber = 11, ColName = "C0310", HierarchyID = 350, IsRowKey = false, Label = "Holdings in related undertakings, including participations", OrdinateCode = "C0310", OrdinateID = 2210, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 12, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "External rating", OrdinateCode = "C0320", OrdinateID = 2211, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 13, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Nominated ECAI", OrdinateCode = "C0330", OrdinateID = 2212, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "ENUMERATION/CODE", ColNumber = 14, ColName = "C0340", HierarchyID = 36, IsRowKey = false, Label = "Credit quality step", OrdinateCode = "C0340", OrdinateID = 2213, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "STRING", ColNumber = 15, ColName = "C0350", HierarchyID = 0, IsRowKey = false, Label = "Internal rating", OrdinateCode = "C0350", OrdinateID = 2214, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "DECIMAL", ColNumber = 16, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Duration", OrdinateCode = "C0360", OrdinateID = 2215, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "MONETARY", ColNumber = 17, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "Unit Solvency II price", OrdinateCode = "C0370", OrdinateID = 2216, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "PERCENTAGE", ColNumber = 18, ColName = "C0380", HierarchyID = 0, IsRowKey = false, Label = "Unit percentage of par amount Solvency II price", OrdinateCode = "C0380", OrdinateID = 2217, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 208, ColType = "DATE", ColNumber = 19, ColName = "C0390", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0390", OrdinateID = 2218, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
