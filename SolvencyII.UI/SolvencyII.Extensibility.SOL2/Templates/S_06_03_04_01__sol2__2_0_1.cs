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
    public partial class S_06_03_04_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_06_03_04_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_06_03_04_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 84;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_06_03_04_01__sol2__2_0_1);
           DataTable = "T__S_06_03_04_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 242, ColType = "STRING", ColNumber = 0, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0100", OrdinateID = 2347, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 243, ColType = "STRING", ColNumber = 1, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Collective Investments Undertaking ID Code", OrdinateCode = "C0010", OrdinateID = 2348, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 241, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0030", HierarchyID = 314, IsRowKey = false, Label = "Underlying asset category", OrdinateCode = "C0030", OrdinateID = 2343, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 241, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0040", HierarchyID = 231, IsRowKey = false, Label = "Country of issue", OrdinateCode = "C0040", OrdinateID = 2344, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 241, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0050", HierarchyID = 182, IsRowKey = false, Label = "Currency", OrdinateCode = "C0050", OrdinateID = 2345, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 241, ColType = "MONETARY", ColNumber = 5, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Total amount", OrdinateCode = "C0060", OrdinateID = 2346, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
