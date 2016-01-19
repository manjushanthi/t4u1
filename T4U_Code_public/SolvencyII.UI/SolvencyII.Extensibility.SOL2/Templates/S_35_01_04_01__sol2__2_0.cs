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
    public partial class S_35_01_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_35_01_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_35_01_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 449;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_35_01_04_01__sol2__2_0);
           DataTable = "T__S_35_01_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1716, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 8745, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "STRING", ColNumber = 1, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of each undertaking", OrdinateCode = "C0010", OrdinateID = 8711, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0040", HierarchyID = 152, IsRowKey = false, Label = "Method of group solvency calculation used", OrdinateCode = "C0040", OrdinateID = 8712, StartOrder = 2, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 3, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0050", OrdinateID = 8714, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 4, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP net of IGT", OrdinateCode = "C0060", OrdinateID = 8715, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 5, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0070", OrdinateID = 8717, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP net of IGT", OrdinateCode = "C0080", OrdinateID = 8718, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "PERCENTAGE", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Net contribution to Group TP (%)", OrdinateCode = "C0090", OrdinateID = 8719, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0100", OrdinateID = 8721, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP net of IGT", OrdinateCode = "C0110", OrdinateID = 8722, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "PERCENTAGE", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Net contribution to Group TP (%)", OrdinateCode = "C0120", OrdinateID = 8723, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0130", OrdinateID = 8725, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP net of IGT", OrdinateCode = "C0140", OrdinateID = 8726, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "PERCENTAGE", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Net contribution to Group TP (%)", OrdinateCode = "C0150", OrdinateID = 8727, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 14, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0160", OrdinateID = 8729, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 15, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP net of IGT", OrdinateCode = "C0170", OrdinateID = 8730, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "PERCENTAGE", ColNumber = 16, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Net contribution to Group TP (%)", OrdinateCode = "C0180", OrdinateID = 8731, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 17, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0190", OrdinateID = 8733, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 18, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP net of IGT", OrdinateCode = "C0200", OrdinateID = 8734, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "PERCENTAGE", ColNumber = 19, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Net contribution to Group TP (%)", OrdinateCode = "C0210", OrdinateID = 8735, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 20, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0220", OrdinateID = 8737, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 21, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP net of IGT", OrdinateCode = "C0230", OrdinateID = 8738, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 22, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0240", OrdinateID = 8740, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 23, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0250", OrdinateID = 8742, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1715, ColType = "MONETARY", ColNumber = 24, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Amount of TP gross of IGT", OrdinateCode = "C0260", OrdinateID = 8744, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
