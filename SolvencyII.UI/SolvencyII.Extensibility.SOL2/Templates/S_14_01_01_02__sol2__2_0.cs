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
    public partial class S_14_01_01_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_14_01_01_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_14_01_01_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 107;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_14_01_01_02__sol2__2_0);
           DataTable = "T__S_14_01_01_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 331, ColType = "STRING", ColNumber = 0, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Product ID code", OrdinateCode = "C0090", OrdinateID = 2871, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 330, ColType = "ENUMERATION/CODE", ColNumber = 1, ColName = "C0100", HierarchyID = 243, IsRowKey = false, Label = "Product classification", OrdinateCode = "C0100", OrdinateID = 2864, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 330, ColType = "STRING", ColNumber = 2, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Type of product", OrdinateCode = "C0110", OrdinateID = 2865, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 330, ColType = "STRING", ColNumber = 3, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Product denomination", OrdinateCode = "C0120", OrdinateID = 2866, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 330, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0130", HierarchyID = 235, IsRowKey = false, Label = "Product still commercialised?", OrdinateCode = "C0130", OrdinateID = 2867, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 330, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0140", HierarchyID = 244, IsRowKey = false, Label = "Type of premium", OrdinateCode = "C0140", OrdinateID = 2868, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 330, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0150", HierarchyID = 238, IsRowKey = false, Label = "Use of financial instrument for replication?", OrdinateCode = "C0150", OrdinateID = 2869, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 330, ColType = "INTEGER", ColNumber = 7, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Number of HRGs in products", OrdinateCode = "C0160", OrdinateID = 2870, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
