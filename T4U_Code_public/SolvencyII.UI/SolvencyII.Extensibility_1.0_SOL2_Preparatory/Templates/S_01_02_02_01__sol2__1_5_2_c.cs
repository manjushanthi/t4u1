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
    public partial class S_01_02_02_01__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_01_02_02_01__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_01_02_02_01__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 6;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_01_02_02_01__sol2__1_5_2_c);
           DataTable = "T__S_01_02_02_01__sol2__1_5_2_c";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 17, ColType = "STRING", ColNumber = 0, ColName = "R0020", HierarchyID = 0, IsRowKey = true, Label = "Group identification code", OrdinateCode = "R0020", OrdinateID = 87, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "DATE", ColNumber = 1, ColName = "R0080", HierarchyID = 0, IsRowKey = false, Label = "Reporting date", OrdinateCode = "R0080", OrdinateID = 77, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "DATE", ColNumber = 2, ColName = "R0090", HierarchyID = 0, IsRowKey = false, Label = "Reference date", OrdinateCode = "R0090", OrdinateID = 78, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "R0110", HierarchyID = 141, IsRowKey = false, Label = "Currency used for reporting", OrdinateCode = "R0110", OrdinateID = 79, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "R0120", HierarchyID = 22, IsRowKey = false, Label = "Accounting standard", OrdinateCode = "R0120", OrdinateID = 80, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "R0130", HierarchyID = 199, IsRowKey = false, Label = "Model used", OrdinateCode = "R0130", OrdinateID = 81, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "R0040", HierarchyID = 83, IsRowKey = false, Label = "Composite undertaking? (Y/N)", OrdinateCode = "R0040", OrdinateID = 82, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "R0150", HierarchyID = 71, IsRowKey = false, Label = "RFF? (Y/N)", OrdinateCode = "R0150", OrdinateID = 83, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "R0160", HierarchyID = 133, IsRowKey = false, Label = "Consolidation method 1 or a combination of methods is used for calculating group solvency of at least one undertaking in the scope? (Y/N)", OrdinateCode = "R0160", OrdinateID = 84, StartOrder = 2, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "STRING", ColNumber = 9, ColName = "R0010", HierarchyID = 0, IsRowKey = false, Label = "Name", OrdinateCode = "R0010", OrdinateID = 85, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 16, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "R0050", HierarchyID = 156, IsRowKey = false, Label = "Reporting country", OrdinateCode = "R0050", OrdinateID = 86, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
