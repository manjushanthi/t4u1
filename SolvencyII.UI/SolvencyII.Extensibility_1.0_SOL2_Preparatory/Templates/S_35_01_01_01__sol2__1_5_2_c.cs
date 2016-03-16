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
    public partial class S_35_01_01_01__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_35_01_01_01__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_35_01_01_01__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 376;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_35_01_01_01__sol2__1_5_2_c);
           DataTable = "T__S_35_01_01_01__sol2__1_5_2_c";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1528, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = true, Label = "Identification code", OrdinateCode = "C0020", OrdinateID = 6926, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "STRING", ColNumber = 1, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 6903, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0040", HierarchyID = 138, IsRowKey = false, Label = "Method of group solvency calculation used", OrdinateCode = "C0040", OrdinateID = 6904, StartOrder = 2, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 3, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (gross of IGT)", OrdinateCode = "C0070", OrdinateID = 6906, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 4, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (net of IGT)", OrdinateCode = "C0080", OrdinateID = 6907, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "PERCENTAGE", ColNumber = 5, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Contribution to Group TP (excluding IGT) (%)", OrdinateCode = "C0090", OrdinateID = 6908, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 6, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (gross of IGT)", OrdinateCode = "C0100", OrdinateID = 6910, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 7, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (net of IGT)", OrdinateCode = "C0110", OrdinateID = 6911, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "PERCENTAGE", ColNumber = 8, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Contribution to Group TP (excluding IGT) (%)", OrdinateCode = "C0120", OrdinateID = 6912, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 9, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (gross of IGT)", OrdinateCode = "C0130", OrdinateID = 6914, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 10, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (net of IGT)", OrdinateCode = "C0140", OrdinateID = 6915, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "PERCENTAGE", ColNumber = 11, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Contribution to Group TP (excluding IGT) (%)", OrdinateCode = "C0150", OrdinateID = 6916, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 12, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (gross of IGT)", OrdinateCode = "C0160", OrdinateID = 6918, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 13, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (net of IGT)", OrdinateCode = "C0170", OrdinateID = 6919, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "PERCENTAGE", ColNumber = 14, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Contribution to Group TP (excluding IGT) (%)", OrdinateCode = "C0180", OrdinateID = 6920, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 15, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (gross of IGT)", OrdinateCode = "C0190", OrdinateID = 6922, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 16, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Amount of gross TP (net of IGT)", OrdinateCode = "C0200", OrdinateID = 6923, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "PERCENTAGE", ColNumber = 17, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Contribution to Group TP (excluding IGT) (%)", OrdinateCode = "C0210", OrdinateID = 6924, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1527, ColType = "MONETARY", ColNumber = 18, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Total amount of TP (excluding IGT)", OrdinateCode = "C0060", OrdinateID = 6925, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
