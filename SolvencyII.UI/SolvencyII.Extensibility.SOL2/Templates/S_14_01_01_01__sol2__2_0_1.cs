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
    public partial class S_14_01_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_14_01_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_14_01_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 109;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_14_01_01_01__sol2__2_0_1);
           DataTable = "T__S_14_01_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 336, ColType = "STRING", ColNumber = 0, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0240", OrdinateID = 2891, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 337, ColType = "STRING", ColNumber = 1, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Product ID code", OrdinateCode = "C0010", OrdinateID = 2892, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 338, ColType = "STRING", ColNumber = 2, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Fund number", OrdinateCode = "C0020", OrdinateID = 2893, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 335, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0030", HierarchyID = 265, IsRowKey = false, Label = "Line of Business", OrdinateCode = "C0030", OrdinateID = 2885, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 335, ColType = "INTEGER", ColNumber = 4, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Number of contracts at the end of the year", OrdinateCode = "C0040", OrdinateID = 2886, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 335, ColType = "INTEGER", ColNumber = 5, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Number of new contracts during year", OrdinateCode = "C0050", OrdinateID = 2887, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 335, ColType = "MONETARY", ColNumber = 6, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Total amount of Written premiums", OrdinateCode = "C0060", OrdinateID = 2888, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 335, ColType = "MONETARY", ColNumber = 7, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Total amount of claims paid during year", OrdinateCode = "C0070", OrdinateID = 2889, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 335, ColType = "STRING", ColNumber = 8, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Country", OrdinateCode = "C0080", OrdinateID = 2890, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
