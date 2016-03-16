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
    public partial class S_33_01_01_01__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_33_01_01_01__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_33_01_01_01__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 373;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_33_01_01_01__sol2__1_5_2_c);
           DataTable = "T__S_33_01_01_01__sol2__1_5_2_c";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1517, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = true, Label = "Identification code", OrdinateCode = "C0020", OrdinateID = 6885, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1518, ColType = "ENUMERATION/CODE", ColNumber = 1, ColName = "C0040", HierarchyID = 72, IsRowKey = true, Label = "Entity Level/RFF/ Remaining Part", OrdinateCode = "C0040", OrdinateID = 6886, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1519, ColType = "STRING", ColNumber = 2, ColName = "C0050", HierarchyID = 0, IsRowKey = true, Label = "Fund Number", OrdinateCode = "C0050", OrdinateID = 6887, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "STRING", ColNumber = 3, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the entity", OrdinateCode = "C0010", OrdinateID = 6863, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 4, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "SCR Market Risk", OrdinateCode = "C0060", OrdinateID = 6864, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 5, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "SCR Counterparty Default Risk", OrdinateCode = "C0070", OrdinateID = 6865, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "SCR Life Underwriting Risk", OrdinateCode = "C0080", OrdinateID = 6866, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "SCR Health Underwriting Risk", OrdinateCode = "C0090", OrdinateID = 6867, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "SCR Non-life Underwriting Risk", OrdinateCode = "C0100", OrdinateID = 6868, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "SCR Operational Risk", OrdinateCode = "C0110", OrdinateID = 6869, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Solo SCR", OrdinateCode = "C0120", OrdinateID = 6870, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Solo MCR", OrdinateCode = "C0130", OrdinateID = 6871, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Eligible Solo Own Funds to cover the SCR", OrdinateCode = "C0140", OrdinateID = 6872, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "STRING", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "if undertaking specific parameters used specify where", OrdinateCode = "C0150", OrdinateID = 6874, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "STRING", ColNumber = 14, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "if Simplifications used specify where", OrdinateCode = "C0160", OrdinateID = 6875, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "STRING", ColNumber = 15, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "if Partial Internal Model used specify where", OrdinateCode = "C0170", OrdinateID = 6876, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "STRING", ColNumber = 16, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Group or solo internal model", OrdinateCode = "C0180", OrdinateID = 6878, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "DATE", ColNumber = 17, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Date of initial approval", OrdinateCode = "C0190", OrdinateID = 6879, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "DATE", ColNumber = 18, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Date of approval of latest major change", OrdinateCode = "C0200", OrdinateID = 6880, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "DATE", ColNumber = 19, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Date of decision", OrdinateCode = "C0210", OrdinateID = 6882, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "MONETARY", ColNumber = 20, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Amount", OrdinateCode = "C0220", OrdinateID = 6883, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1516, ColType = "STRING", ColNumber = 21, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Reason", OrdinateCode = "C0230", OrdinateID = 6884, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
