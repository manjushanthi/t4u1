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
    public partial class S_30_02_01_03__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_02_01_03__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_02_01_03__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 433;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_02_01_03__sol2__2_0_1);
           DataTable = "T__S_30_02_01_03__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1646, ColType = "STRING", ColNumber = 0, ColName = "C0280", HierarchyID = 0, IsRowKey = false, Label = "Code reinsurer", OrdinateCode = "C0280", OrdinateID = 8456, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1645, ColType = "STRING", ColNumber = 1, ColName = "C0300", HierarchyID = 0, IsRowKey = false, Label = "Legal name reinsurer", OrdinateCode = "C0300", OrdinateID = 8449, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1645, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0310", HierarchyID = 410, IsRowKey = false, Label = "Type of reinsurer", OrdinateCode = "C0310", OrdinateID = 8450, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1645, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0320", HierarchyID = 223, IsRowKey = false, Label = "Country of residency", OrdinateCode = "C0320", OrdinateID = 8451, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1645, ColType = "STRING", ColNumber = 4, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "External rating assessment by nominated ECAI", OrdinateCode = "C0330", OrdinateID = 8452, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1645, ColType = "STRING", ColNumber = 5, ColName = "C0340", HierarchyID = 0, IsRowKey = false, Label = "Nominated ECAI", OrdinateCode = "C0340", OrdinateID = 8453, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1645, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0350", HierarchyID = 38, IsRowKey = false, Label = "Credit quality step", OrdinateCode = "C0350", OrdinateID = 8454, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1645, ColType = "STRING", ColNumber = 7, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Internal rating", OrdinateCode = "C0360", OrdinateID = 8455, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
