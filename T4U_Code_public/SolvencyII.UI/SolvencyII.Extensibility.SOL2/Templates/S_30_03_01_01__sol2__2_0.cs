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
    public partial class S_30_03_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_03_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_03_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 432;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_03_01_01__sol2__2_0);
           DataTable = "T__S_30_03_01_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1652, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance program code", OrdinateCode = "C0010", OrdinateID = 8487, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1653, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Treaty identification code", OrdinateCode = "C0020", OrdinateID = 8488, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1654, ColType = "STRING", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Progressive section number in treaty", OrdinateCode = "C0030", OrdinateID = 8489, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1655, ColType = "STRING", ColNumber = 3, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Progressive number of surplus/layer in program", OrdinateCode = "C0040", OrdinateID = 8490, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1656, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0070", HierarchyID = 251, IsRowKey = false, Label = "Line of business", OrdinateCode = "C0070", OrdinateID = 8491, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "INTEGER", ColNumber = 5, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Quantity of surplus/layers in program", OrdinateCode = "C0050", OrdinateID = 8454, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0060", HierarchyID = 395, IsRowKey = false, Label = "Finite reinsurance or similar arrangements", OrdinateCode = "C0060", OrdinateID = 8455, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "STRING", ColNumber = 7, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Description risk category covered", OrdinateCode = "C0080", OrdinateID = 8456, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0090", HierarchyID = 402, IsRowKey = false, Label = "Type of reinsurance treaty", OrdinateCode = "C0090", OrdinateID = 8457, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "STRING", ColNumber = 9, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Inclusion of catastrophic reinsurance cover", OrdinateCode = "C0100", OrdinateID = 8458, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "DATE", ColNumber = 10, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Validity period (start date)", OrdinateCode = "C0110", OrdinateID = 8459, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "DATE", ColNumber = 11, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Validity period (expiry date)", OrdinateCode = "C0120", OrdinateID = 8460, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "ENUMERATION/CODE", ColNumber = 12, ColName = "C0130", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0130", OrdinateID = 8461, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "ENUMERATION/CODE", ColNumber = 13, ColName = "C0140", HierarchyID = 394, IsRowKey = false, Label = "Type of underwriting model", OrdinateCode = "C0140", OrdinateID = 8462, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "MONETARY", ColNumber = 14, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Estimated Subject Premium income (XL-ESPI)", OrdinateCode = "C0150", OrdinateID = 8463, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "MONETARY", ColNumber = 15, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Gross Estimated Treaty Premium Income (proportional and non proportional)", OrdinateCode = "C0160", OrdinateID = 8464, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "MONETARY", ColNumber = 16, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Aggregate deductibles (amount)", OrdinateCode = "C0170", OrdinateID = 8465, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 17, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Aggregate deductibles (%)", OrdinateCode = "C0180", OrdinateID = 8466, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "MONETARY", ColNumber = 18, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Retention or priority (amount)", OrdinateCode = "C0190", OrdinateID = 8467, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 19, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Retention or priority (%)", OrdinateCode = "C0200", OrdinateID = 8468, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "MONETARY", ColNumber = 20, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Limit (amount)", OrdinateCode = "C0210", OrdinateID = 8469, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 21, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Limit (%)", OrdinateCode = "C0220", OrdinateID = 8470, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "MONETARY", ColNumber = 22, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Maximum cover per risk or event", OrdinateCode = "C0230", OrdinateID = 8471, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "MONETARY", ColNumber = 23, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Maximum cover per treaty", OrdinateCode = "C0240", OrdinateID = 8472, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "INTEGER", ColNumber = 24, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Number of reinstatements", OrdinateCode = "C0250", OrdinateID = 8473, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "STRING", ColNumber = 25, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Descriptions of reinstatements", OrdinateCode = "C0260", OrdinateID = 8474, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 26, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Maximum Reinsurance Commission", OrdinateCode = "C0270", OrdinateID = 8475, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 27, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Minimum Reinsurance Commission", OrdinateCode = "C0280", OrdinateID = 8476, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 28, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "Expected Reinsurance Commission", OrdinateCode = "C0290", OrdinateID = 8477, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 29, ColName = "C0300", HierarchyID = 0, IsRowKey = false, Label = "Maximum Overriding Commission", OrdinateCode = "C0300", OrdinateID = 8478, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 30, ColName = "C0310", HierarchyID = 0, IsRowKey = false, Label = "Minimum Overriding Commission", OrdinateCode = "C0310", OrdinateID = 8479, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 31, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "Expected Overriding Commission", OrdinateCode = "C0320", OrdinateID = 8480, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 32, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Maximum profit commission", OrdinateCode = "C0330", OrdinateID = 8481, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 33, ColName = "C0340", HierarchyID = 0, IsRowKey = false, Label = "Minimum Profit Commission", OrdinateCode = "C0340", OrdinateID = 8482, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 34, ColName = "C0350", HierarchyID = 0, IsRowKey = false, Label = "Expected Profit Commission", OrdinateCode = "C0350", OrdinateID = 8483, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 35, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "XL rate 1", OrdinateCode = "C0360", OrdinateID = 8484, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "PERCENTAGE", ColNumber = 36, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "XL rate 2", OrdinateCode = "C0370", OrdinateID = 8485, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1651, ColType = "ENUMERATION/CODE", ColNumber = 37, ColName = "C0380", HierarchyID = 7, IsRowKey = false, Label = "XL premium flat", OrdinateCode = "C0380", OrdinateID = 8486, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
