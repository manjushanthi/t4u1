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
    public partial class S_24_01_01_09__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_24_01_01_09__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_24_01_01_09__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 218;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_24_01_01_09__sol2__2_0);
           DataTable = "T__S_24_01_01_09__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 752, ColType = "STRING", ColNumber = 0, ColName = "C0510", HierarchyID = 0, IsRowKey = false, Label = "Name of related undertaking", OrdinateCode = "C0510", OrdinateID = 4859, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 753, ColType = "STRING", ColNumber = 1, ColName = "C0520", HierarchyID = 0, IsRowKey = false, Label = "Asset ID Code", OrdinateCode = "C0520", OrdinateID = 4860, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 751, ColType = "MONETARY", ColNumber = 2, ColName = "C0540", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "C0540", OrdinateID = 4855, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 751, ColType = "MONETARY", ColNumber = 3, ColName = "C0550", HierarchyID = 0, IsRowKey = false, Label = "Type 1 Equity", OrdinateCode = "C0550", OrdinateID = 4856, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 751, ColType = "MONETARY", ColNumber = 4, ColName = "C0560", HierarchyID = 0, IsRowKey = false, Label = "Type 2 Equity", OrdinateCode = "C0560", OrdinateID = 4857, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 751, ColType = "MONETARY", ColNumber = 5, ColName = "C0570", HierarchyID = 0, IsRowKey = false, Label = "Subordinated liabilities", OrdinateCode = "C0570", OrdinateID = 4858, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
