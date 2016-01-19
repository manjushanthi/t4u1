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
    public partial class S_31_02_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_31_02_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_31_02_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 443;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_31_02_04_01__sol2__2_0);
           DataTable = "T__S_31_02_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1697, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 8609, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1698, ColType = "STRING", ColNumber = 1, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Internal code of SPV", OrdinateCode = "C0030", OrdinateID = 8610, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1699, ColType = "STRING", ColNumber = 2, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "ID Code of SPV notes or other financing mechanism issued", OrdinateCode = "C0040", OrdinateID = 8611, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "STRING", ColNumber = 3, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of reinsured undertaking", OrdinateCode = "C0010", OrdinateID = 8594, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0060", HierarchyID = 251, IsRowKey = false, Label = "Lines of Business SPV securitisation relates", OrdinateCode = "C0060", OrdinateID = 8595, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0070", HierarchyID = 266, IsRowKey = false, Label = "Type of Trigger(s) in the SPV", OrdinateCode = "C0070", OrdinateID = 8596, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "STRING", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Contractual trigger event", OrdinateCode = "C0080", OrdinateID = 8597, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0090", HierarchyID = 267, IsRowKey = false, Label = "Same trigger as in underlying cedant's portfolio?", OrdinateCode = "C0090", OrdinateID = 8598, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0100", HierarchyID = 367, IsRowKey = false, Label = "Basis risk arising from risk-transfer structure", OrdinateCode = "C0100", OrdinateID = 8599, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0110", HierarchyID = 368, IsRowKey = false, Label = "Basis risk arising from contractual terms", OrdinateCode = "C0110", OrdinateID = 8600, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "MONETARY", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "SPV assets ring-fenced to settle cedant-specific obligations", OrdinateCode = "C0120", OrdinateID = 8601, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Other non cedant-specific SPV Assets for which recourse may exist", OrdinateCode = "C0130", OrdinateID = 8602, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "MONETARY", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Other recourse arising from securitisation", OrdinateCode = "C0140", OrdinateID = 8603, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "MONETARY", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Total maximum possible obligations from SPV under reinsurance policy", OrdinateCode = "C0150", OrdinateID = 8604, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "ENUMERATION/CODE", ColNumber = 14, ColName = "C0160", HierarchyID = 389, IsRowKey = false, Label = "SPV fully funded in relation to cedant obligations throughout the reporting period", OrdinateCode = "C0160", OrdinateID = 8605, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "MONETARY", ColNumber = 15, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Current recoverables from SPV", OrdinateCode = "C0170", OrdinateID = 8606, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "ENUMERATION/CODE", ColNumber = 16, ColName = "C0180", HierarchyID = 403, IsRowKey = false, Label = "Identification of material investments held by cedant in SPV", OrdinateCode = "C0180", OrdinateID = 8607, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1696, ColType = "ENUMERATION/CODE", ColNumber = 17, ColName = "C0190", HierarchyID = 404, IsRowKey = false, Label = "Securitisation assets related to cedant held in trust with other third party than cedant / sponsor?", OrdinateCode = "C0190", OrdinateID = 8608, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
