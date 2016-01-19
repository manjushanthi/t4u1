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
    public partial class S_31_02_04_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_31_02_04_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_31_02_04_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 444;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_31_02_04_02__sol2__2_0);
           DataTable = "T__S_31_02_04_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1701, ColType = "STRING", ColNumber = 0, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Internal code of SPV", OrdinateCode = "C0200", OrdinateID = 8621, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "ENUMERATION/CODE", ColNumber = 1, ColName = "C0220", HierarchyID = 390, IsRowKey = false, Label = "Legal nature of SPV", OrdinateCode = "C0220", OrdinateID = 8612, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "STRING", ColNumber = 2, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "Name of SPV", OrdinateCode = "C0230", OrdinateID = 8613, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "STRING", ColNumber = 3, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Incorporation no. of SPV", OrdinateCode = "C0240", OrdinateID = 8614, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0250", HierarchyID = 194, IsRowKey = false, Label = "SPV country of authorisation", OrdinateCode = "C0250", OrdinateID = 8615, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0260", HierarchyID = 398, IsRowKey = false, Label = "SPV authorisation conditions", OrdinateCode = "C0260", OrdinateID = 8616, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "STRING", ColNumber = 6, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "External rating assessment by nominated ECAI", OrdinateCode = "C0270", OrdinateID = 8617, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "STRING", ColNumber = 7, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Nominated ECAI", OrdinateCode = "C0280", OrdinateID = 8618, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0290", HierarchyID = 36, IsRowKey = false, Label = "Credit quality step", OrdinateCode = "C0290", OrdinateID = 8619, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1700, ColType = "STRING", ColNumber = 9, ColName = "C0300", HierarchyID = 0, IsRowKey = false, Label = "Internal rating", OrdinateCode = "C0300", OrdinateID = 8620, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
