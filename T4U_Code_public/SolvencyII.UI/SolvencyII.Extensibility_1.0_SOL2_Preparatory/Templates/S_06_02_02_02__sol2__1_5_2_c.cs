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
    public partial class S_06_02_02_02__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_06_02_02_02__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_06_02_02_02__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 18;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_06_02_02_02__sol2__1_5_2_c);
           DataTable = "T__S_06_02_02_02__sol2__1_5_2_c";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 62, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = true, Label = "ID Code", OrdinateCode = "C0040", OrdinateID = 582, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 61, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 581, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "STRING", ColNumber = 2, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Item Title", OrdinateCode = "C0190", OrdinateID = 565, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "STRING", ColNumber = 3, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Issuer Name", OrdinateCode = "C0200", OrdinateID = 566, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "STRING", ColNumber = 4, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Issuer Code", OrdinateCode = "C0210", OrdinateID = 567, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0230", HierarchyID = 221, IsRowKey = false, Label = "Issuer Sector", OrdinateCode = "C0230", OrdinateID = 568, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "STRING", ColNumber = 6, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group", OrdinateCode = "C0240", OrdinateID = 569, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "STRING", ColNumber = 7, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Issuer Group Code", OrdinateCode = "C0250", OrdinateID = 570, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0270", HierarchyID = 147, IsRowKey = false, Label = "Issuer Country", OrdinateCode = "C0270", OrdinateID = 571, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0280", HierarchyID = 141, IsRowKey = false, Label = "Currency (ISO code)", OrdinateCode = "C0280", OrdinateID = 572, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "STRING", ColNumber = 10, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "CIC", OrdinateCode = "C0290", OrdinateID = 573, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "ENUMERATION/CODE", ColNumber = 11, ColName = "C0310", HierarchyID = 73, IsRowKey = false, Label = "Participation", OrdinateCode = "C0310", OrdinateID = 574, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "STRING", ColNumber = 12, ColName = "C0320", HierarchyID = 0, IsRowKey = false, Label = "External rating", OrdinateCode = "C0320", OrdinateID = 575, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "STRING", ColNumber = 13, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Rating agency", OrdinateCode = "C0330", OrdinateID = 576, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "DECIMAL", ColNumber = 14, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Duration", OrdinateCode = "C0360", OrdinateID = 577, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "MONETARY", ColNumber = 15, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "Unit SII price", OrdinateCode = "C0370", OrdinateID = 578, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "PERCENTAGE", ColNumber = 16, ColName = "C0380", HierarchyID = 0, IsRowKey = false, Label = "Percentage of par SII value", OrdinateCode = "C0380", OrdinateID = 579, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 60, ColType = "DATE", ColNumber = 17, ColName = "C0390", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0390", OrdinateID = 580, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
