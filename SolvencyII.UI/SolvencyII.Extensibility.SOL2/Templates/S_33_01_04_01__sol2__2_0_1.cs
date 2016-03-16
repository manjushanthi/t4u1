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
    public partial class S_33_01_04_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_33_01_04_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_33_01_04_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 449;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_33_01_04_01__sol2__2_0_1);
           DataTable = "T__S_33_01_04_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1705, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 8686, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1706, ColType = "ENUMERATION/CODE", ColNumber = 1, ColName = "C0040", HierarchyID = 367, IsRowKey = false, Label = "Entity Level/RFF or MAP/ Remaining Part", OrdinateCode = "C0040", OrdinateID = 8687, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1707, ColType = "STRING", ColNumber = 2, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Fund Number", OrdinateCode = "C0050", OrdinateID = 8688, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "STRING", ColNumber = 3, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 8659, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 4, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "SCR Market Risk", OrdinateCode = "C0060", OrdinateID = 8660, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 5, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "SCR Counterparty Default Risk", OrdinateCode = "C0070", OrdinateID = 8661, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "SCR Life Underwriting Risk", OrdinateCode = "C0080", OrdinateID = 8662, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "SCR Health Underwriting Risk", OrdinateCode = "C0090", OrdinateID = 8663, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "SCR Non-life Underwriting Risk", OrdinateCode = "C0100", OrdinateID = 8664, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "SCR Operational Risk", OrdinateCode = "C0110", OrdinateID = 8665, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Individual SCR", OrdinateCode = "C0120", OrdinateID = 8667, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Individual MCR", OrdinateCode = "C0130", OrdinateID = 8668, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Eligible Individual Own Funds to cover the SCR", OrdinateCode = "C0140", OrdinateID = 8669, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "STRING", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Use of undertaking specific parameters", OrdinateCode = "C0150", OrdinateID = 8671, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "STRING", ColNumber = 14, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Use of simplifications", OrdinateCode = "C0160", OrdinateID = 8672, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "STRING", ColNumber = 15, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Use of Partial Internal Model", OrdinateCode = "C0170", OrdinateID = 8673, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "ENUMERATION/CODE", ColNumber = 16, ColName = "C0180", HierarchyID = 29, IsRowKey = false, Label = "Group or individual internal model", OrdinateCode = "C0180", OrdinateID = 8675, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "DATE", ColNumber = 17, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Date of initial approval of IM", OrdinateCode = "C0190", OrdinateID = 8676, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "DATE", ColNumber = 18, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Date of approval of latest major change of IM", OrdinateCode = "C0200", OrdinateID = 8677, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "DATE", ColNumber = 19, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Date of decision of capital add-on", OrdinateCode = "C0210", OrdinateID = 8679, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 20, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Amount of capital add-on", OrdinateCode = "C0220", OrdinateID = 8680, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "STRING", ColNumber = 21, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Reason of capital add-on", OrdinateCode = "C0230", OrdinateID = 8681, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 22, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Local capital requirement", OrdinateCode = "C0240", OrdinateID = 8683, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 23, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Local minimum capital requirement", OrdinateCode = "C0250", OrdinateID = 8684, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1704, ColType = "MONETARY", ColNumber = 24, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Eligible own funds in accordance with local rules", OrdinateCode = "C0260", OrdinateID = 8685, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
