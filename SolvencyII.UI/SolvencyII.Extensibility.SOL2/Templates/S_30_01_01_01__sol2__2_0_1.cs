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
    public partial class S_30_01_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_01_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_01_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 429;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_01_01_01__sol2__2_0_1);
           DataTable = "T__S_30_01_01_01__sol2__2_0_1";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1618, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance program code", OrdinateCode = "C0020", OrdinateID = 8401, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1619, ColType = "STRING", ColNumber = 1, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Risk identification code", OrdinateCode = "C0030", OrdinateID = 8402, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1620, ColType = "STRING", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Facultative reinsurance placement identification code", OrdinateCode = "C0040", OrdinateID = 8403, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0050", HierarchyID = 418, IsRowKey = false, Label = "Finite reinsurance or similar arrangements", OrdinateCode = "C0050", OrdinateID = 8387, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0060", HierarchyID = 423, IsRowKey = false, Label = "Proportional", OrdinateCode = "C0060", OrdinateID = 8388, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "STRING", ColNumber = 5, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Identification of the company/person to which the risk relates", OrdinateCode = "C0070", OrdinateID = 8389, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "STRING", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Description risk", OrdinateCode = "C0080", OrdinateID = 8390, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "STRING", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Description risk category covered", OrdinateCode = "C0090", OrdinateID = 8391, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "DATE", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Validity period (start date)", OrdinateCode = "C0100", OrdinateID = 8392, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "DATE", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Validity period (expiry date)", OrdinateCode = "C0110", OrdinateID = 8393, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0120", HierarchyID = 179, IsRowKey = false, Label = "Currency", OrdinateCode = "C0120", OrdinateID = 8394, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Sum insured", OrdinateCode = "C0130", OrdinateID = 8395, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "ENUMERATION/CODE", ColNumber = 12, ColName = "C0140", HierarchyID = 417, IsRowKey = false, Label = "Type of underwriting model", OrdinateCode = "C0140", OrdinateID = 8396, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "MONETARY", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Amount underwriting model", OrdinateCode = "C0150", OrdinateID = 8397, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "MONETARY", ColNumber = 14, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Sum reinsured on a facultative basis, with all reinsurers", OrdinateCode = "C0160", OrdinateID = 8398, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "MONETARY", ColNumber = 15, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Facultative reinsurance premium ceded to all reinsurers for 100% of the reinsurance placement", OrdinateCode = "C0170", OrdinateID = 8399, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1617, ColType = "MONETARY", ColNumber = 16, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Facultative reinsurance commission", OrdinateCode = "C0180", OrdinateID = 8400, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
