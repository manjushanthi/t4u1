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
    public partial class S_02_03_07_03__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_02_03_07_03__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_02_03_07_03__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 43;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_02_03_07_03__sol2__2_0);
           DataTable = "T__S_02_03_07_03__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 106, ColType = "STRING", ColNumber = 0, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0130", OrdinateID = 1525, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 105, ColType = "STRING", ColNumber = 1, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Balance sheet liability", OrdinateCode = "C0090", OrdinateID = 1521, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 105, ColType = "MONETARY", ColNumber = 2, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Gross value", OrdinateCode = "C0100", OrdinateID = 1522, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 105, ColType = "MONETARY", ColNumber = 3, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Preferential claim", OrdinateCode = "C0110", OrdinateID = 1523, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 105, ColType = "MONETARY", ColNumber = 4, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Net amount", OrdinateCode = "C0120", OrdinateID = 1524, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
