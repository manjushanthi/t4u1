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
    public partial class S_25_03_04_01__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_25_03_04_01__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_25_03_04_01__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 86;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_25_03_04_01__sol2__1_5_2_c);
           DataTable = "T__S_25_03_04_01__sol2__1_5_2_c";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 305, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = true, Label = "Unique number of component", OrdinateCode = "C0010", OrdinateID = 1787, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Component Description", OrdinateCode = "C0020", OrdinateID = 1782, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "PC0050", HierarchyID = 44, IsRowKey = false, Label = "Modelling approach to calculation of loss absorbing capacity of technical provisions", OrdinateCode = "PC0050", OrdinateID = 1783, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "PC0060", HierarchyID = 45, IsRowKey = false, Label = "Modelling approach to calculation of loss absorbing capacity of deferred taxes", OrdinateCode = "PC0060", OrdinateID = 1784, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "MONETARY", ColNumber = 4, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Net solvency capital requirement (including the loss absorbing capacity of technical provisions and/or deferred taxes when applicable)", OrdinateCode = "C0040", OrdinateID = 1785, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 304, ColType = "MONETARY", ColNumber = 5, ColName = "PC0070", HierarchyID = 0, IsRowKey = false, Label = "Gross solvency capital requirement (excluding the loss-absorbing capacity of technical provisions and/or deferred taxes when applicable)", OrdinateCode = "PC0070", OrdinateID = 1786, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
