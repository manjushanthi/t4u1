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
    public partial class S_25_03_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_25_03_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_25_03_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 235;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_25_03_04_01__sol2__2_0);
           DataTable = "T__S_25_03_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 815, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Unique number of component", OrdinateCode = "C0010", OrdinateID = 5080, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 814, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Component Description", OrdinateCode = "C0020", OrdinateID = 5077, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 814, ColType = "MONETARY", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Calculation of the Solvency Capital Requirement", OrdinateCode = "C0030", OrdinateID = 5078, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 814, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0060", HierarchyID = 30, IsRowKey = false, Label = "Consideration of the future management actions regarding technical provisions and/or deferred taxes", OrdinateCode = "C0060", OrdinateID = 5079, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
