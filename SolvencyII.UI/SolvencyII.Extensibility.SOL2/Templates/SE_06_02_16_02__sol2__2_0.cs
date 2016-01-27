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
    public partial class SE_06_02_16_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new SE_06_02_16_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public SE_06_02_16_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 77;
           FrameworkCode = "s2md";
           DataType = typeof(T__SE_06_02_16_02__sol2__2_0);
           DataTable = "T__SE_06_02_16_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 218, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0040", OrdinateID = 2261, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 1, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Item Title", OrdinateCode = "C0190", OrdinateID = 2238, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 2, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Issuer Name", OrdinateCode = "C0200", OrdinateID = 2239, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 3, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Issuer Code", OrdinateCode = "C0210", OrdinateID = 2240, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0230", HierarchyID = 316, IsRowKey = false, Label = "Issuer Sector", OrdinateCode = "C0230", OrdinateID = 2241, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "EC0231", HierarchyID = 391, IsRowKey = false, Label = "Issuer Sector according to ESA 2010", OrdinateCode = "EC0231", OrdinateID = 2242, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 6, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group", OrdinateCode = "C0240", OrdinateID = 2243, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 7, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group Code", OrdinateCode = "C0250", OrdinateID = 2244, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0270", HierarchyID = 193, IsRowKey = false, Label = "Issuer Country", OrdinateCode = "C0270", OrdinateID = 2245, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "EC0271", HierarchyID = 207, IsRowKey = false, Label = "Country of residence for collective investment undertakings", OrdinateCode = "EC0271", OrdinateID = 2246, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0280", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0280", OrdinateID = 2247, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 11, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "CIC", OrdinateCode = "C0290", OrdinateID = 2248, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 12, ColName = "EC0291", HierarchyID = 311, IsRowKey = false, Label = "Instrument classification according to ESA 2010", OrdinateCode = "EC0291", OrdinateID = 2249, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 13, ColName = "C0300", HierarchyID = 295, IsRowKey = false, Label = "Infrastructure investment", OrdinateCode = "C0300", OrdinateID = 2250, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 14, ColName = "C0310", HierarchyID = 350, IsRowKey = false, Label = "Holdings in related undertakings, including participations", OrdinateCode = "C0310", OrdinateID = 2251, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 15, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "External rating", OrdinateCode = "C0320", OrdinateID = 2252, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 16, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Nominated ECAI", OrdinateCode = "C0330", OrdinateID = 2253, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "ENUMERATION/CODE", ColNumber = 17, ColName = "C0340", HierarchyID = 36, IsRowKey = false, Label = "Credit quality step", OrdinateCode = "C0340", OrdinateID = 2254, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "STRING", ColNumber = 18, ColName = "C0350", HierarchyID = 0, IsRowKey = false, Label = "Internal rating", OrdinateCode = "C0350", OrdinateID = 2255, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "DECIMAL", ColNumber = 19, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Duration", OrdinateCode = "C0360", OrdinateID = 2256, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "MONETARY", ColNumber = 20, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "Unit Solvency II price", OrdinateCode = "C0370", OrdinateID = 2257, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "PERCENTAGE", ColNumber = 21, ColName = "C0380", HierarchyID = 0, IsRowKey = false, Label = "Unit percentage of par amount Solvency II price", OrdinateCode = "C0380", OrdinateID = 2258, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "DATE", ColNumber = 22, ColName = "EC0381", HierarchyID = 0, IsRowKey = false, Label = "Issue date", OrdinateCode = "EC0381", OrdinateID = 2259, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 217, ColType = "DATE", ColNumber = 23, ColName = "C0390", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0390", OrdinateID = 2260, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
