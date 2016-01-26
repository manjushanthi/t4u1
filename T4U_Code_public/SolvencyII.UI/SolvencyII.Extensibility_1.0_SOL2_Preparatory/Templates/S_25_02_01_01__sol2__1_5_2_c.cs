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
    public partial class S_25_02_01_01__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_25_02_01_01__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_25_02_01_01__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 69;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_25_02_01_01__sol2__1_5_2_c);
           DataTable = "T__S_25_02_01_01__sol2__1_5_2_c";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 250, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = true, Label = "Unique number of component", OrdinateCode = "C0010", OrdinateID = 1637, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 249, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Component Description", OrdinateCode = "C0020", OrdinateID = 1632, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 249, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "PC0040", HierarchyID = 44, IsRowKey = false, Label = "Modelling approach to calculation of loss absorbing capacity of technical provisions", OrdinateCode = "PC0040", OrdinateID = 1633, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 249, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "PC0050", HierarchyID = 45, IsRowKey = false, Label = "Modelling approach to calculation of loss absorbing capacity of deferred taxes", OrdinateCode = "PC0050", OrdinateID = 1634, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 249, ColType = "MONETARY", ColNumber = 4, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Net solvency capital requirement (including the loss absorbing capacity of technical provisions and/or deferred taxes when applicable)", OrdinateCode = "C0030", OrdinateID = 1635, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 249, ColType = "MONETARY", ColNumber = 5, ColName = "PC0060", HierarchyID = 0, IsRowKey = false, Label = "Gross solvency capital requirement (excluding the loss-absorbing capacity of technical provisions and/or deferred taxes when applicable)", OrdinateCode = "PC0060", OrdinateID = 1636, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 