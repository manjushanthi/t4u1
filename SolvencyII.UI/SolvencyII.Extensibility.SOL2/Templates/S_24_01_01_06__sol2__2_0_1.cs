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
    public partial class S_24_01_01_06__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_24_01_01_06__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_24_01_01_06__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 218;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_24_01_01_06__sol2__2_0_1);
           DataTable = "T__S_24_01_01_06__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 752, ColType = "STRING", ColNumber = 0, ColName = "C0300", HierarchyID = 0, IsRowKey = false, Label = "Name of related undertaking", OrdinateCode = "C0300", OrdinateID = 4869, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 753, ColType = "STRING", ColNumber = 1, ColName = "C0310", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0310", OrdinateID = 4870, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 751, ColType = "MONETARY", ColNumber = 2, ColName = "C0330", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "C0330", OrdinateID = 4865, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 751, ColType = "MONETARY", ColNumber = 3, ColName = "C0340", HierarchyID = 0, IsRowKey = false, Label = "Type 1 Equity", OrdinateCode = "C0340", OrdinateID = 4866, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 751, ColType = "MONETARY", ColNumber = 4, ColName = "C0350", HierarchyID = 0, IsRowKey = false, Label = "Type 2 Equity", OrdinateCode = "C0350", OrdinateID = 4867, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 751, ColType = "MONETARY", ColNumber = 5, ColName = "C0360", HierarchyID = 0, IsRowKey = false, Label = "Subordinated liabilities", OrdinateCode = "C0360", OrdinateID = 4868, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
