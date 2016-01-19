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
    public partial class S_31_01_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_31_01_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_31_01_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 437;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_31_01_01_01__sol2__2_0);
           DataTable = "T__S_31_01_01_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1677, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Code reinsurer", OrdinateCode = "C0040", OrdinateID = 8533, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 1, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance recoverables: Premium provision Non-life including Non-SLT Health", OrdinateCode = "C0060", OrdinateID = 8523, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 2, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance recoverables: Claims provisions Non-life including Non-SLT Health", OrdinateCode = "C0070", OrdinateID = 8524, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 3, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance recoverables: Technical provisions Life including SLT Health", OrdinateCode = "C0080", OrdinateID = 8525, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 4, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Adjustment for expected losses due to counterparty default", OrdinateCode = "C0090", OrdinateID = 8526, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 5, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance recoverables: Total reinsurance recoverables", OrdinateCode = "C0100", OrdinateID = 8527, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 6, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Net receivables", OrdinateCode = "C0110", OrdinateID = 8528, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 7, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Assets pledged by reinsurer", OrdinateCode = "C0120", OrdinateID = 8529, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 8, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Financial guarantees", OrdinateCode = "C0130", OrdinateID = 8530, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 9, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Cash deposits", OrdinateCode = "C0140", OrdinateID = 8531, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1676, ColType = "MONETARY", ColNumber = 10, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Total guarantees received", OrdinateCode = "C0150", OrdinateID = 8532, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
