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
    public partial class S_15_01_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_15_01_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_15_01_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 112;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_15_01_04_01__sol2__2_0);
           DataTable = "T__S_15_01_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 342, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Product ID code", OrdinateCode = "C0040", OrdinateID = 2899, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 343, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 2900, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "STRING", ColNumber = 2, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 2891, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "STRING", ColNumber = 3, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Product Denomination", OrdinateCode = "C0050", OrdinateID = 2892, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "STRING", ColNumber = 4, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Description of the product", OrdinateCode = "C0060", OrdinateID = 2893, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "DATE", ColNumber = 5, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Initial date of guarantee", OrdinateCode = "C0070", OrdinateID = 2894, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "DATE", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Final date of guarantee", OrdinateCode = "C0080", OrdinateID = 2895, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0090", HierarchyID = 50, IsRowKey = false, Label = "Type of guarantee", OrdinateCode = "C0090", OrdinateID = 2896, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "PERCENTAGE", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Guareanteed level", OrdinateCode = "C0100", OrdinateID = 2897, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "STRING", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Description of the guarantee", OrdinateCode = "C0110", OrdinateID = 2898, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
