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
    public partial class S_31_02_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_31_02_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_31_02_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 444;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_31_02_01_01__sol2__2_0_1);
           DataTable = "T__S_31_02_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1691, ColType = "STRING", ColNumber = 0, ColName = "C0001", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0001", OrdinateID = 8588, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1692, ColType = "STRING", ColNumber = 1, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Internal code of SPV", OrdinateCode = "C0030", OrdinateID = 8589, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1693, ColType = "STRING", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "ID Code of SPV notes or other financing mechanism issued", OrdinateCode = "C0040", OrdinateID = 8590, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0060", HierarchyID = 271, IsRowKey = false, Label = "Lines of Business SPV securitisation relates", OrdinateCode = "C0060", OrdinateID = 8574, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0070", HierarchyID = 286, IsRowKey = false, Label = "Type of Trigger(s) in the SPV", OrdinateCode = "C0070", OrdinateID = 8575, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "STRING", ColNumber = 5, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Contractual trigger event", OrdinateCode = "C0080", OrdinateID = 8576, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0090", HierarchyID = 287, IsRowKey = false, Label = "Same trigger as in underlying cedant's portfolio?", OrdinateCode = "C0090", OrdinateID = 8577, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0100", HierarchyID = 390, IsRowKey = false, Label = "Basis risk arising from risk-transfer structure", OrdinateCode = "C0100", OrdinateID = 8578, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0110", HierarchyID = 391, IsRowKey = false, Label = "Basis risk arising from contractual terms", OrdinateCode = "C0110", OrdinateID = 8579, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "MONETARY", ColNumber = 9, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "SPV assets ring-fenced to settle cedant-specific obligations", OrdinateCode = "C0120", OrdinateID = 8580, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "MONETARY", ColNumber = 10, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Other non cedant-specific SPV Assets for which recourse may exist", OrdinateCode = "C0130", OrdinateID = 8581, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "MONETARY", ColNumber = 11, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Other recourse arising from securitisation", OrdinateCode = "C0140", OrdinateID = 8582, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "MONETARY", ColNumber = 12, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Total maximum possible obligations from SPV under reinsurance policy", OrdinateCode = "C0150", OrdinateID = 8583, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "ENUMERATION/CODE", ColNumber = 13, ColName = "C0160", HierarchyID = 412, IsRowKey = false, Label = "SPV fully funded in relation to cedant obligations throughout the reporting period", OrdinateCode = "C0160", OrdinateID = 8584, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "MONETARY", ColNumber = 14, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Current recoverables from SPV", OrdinateCode = "C0170", OrdinateID = 8585, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "ENUMERATION/CODE", ColNumber = 15, ColName = "C0180", HierarchyID = 426, IsRowKey = false, Label = "Identification of material investments held by cedant in SPV", OrdinateCode = "C0180", OrdinateID = 8586, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1690, ColType = "ENUMERATION/CODE", ColNumber = 16, ColName = "C0190", HierarchyID = 427, IsRowKey = false, Label = "Securitisation assets related to cedant held in trust with other third party than cedant / sponsor?", OrdinateCode = "C0190", OrdinateID = 8587, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
