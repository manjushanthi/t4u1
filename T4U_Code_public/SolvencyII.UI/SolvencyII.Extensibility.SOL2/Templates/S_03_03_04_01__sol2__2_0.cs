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
    public partial class S_03_03_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_03_03_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_03_03_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 51;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_03_03_04_01__sol2__2_0);
           DataTable = "T__S_03_03_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 122, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Code of guarantee", OrdinateCode = "C0010", OrdinateID = 1626, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 121, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Name of receiver of guarantee", OrdinateCode = "C0020", OrdinateID = 1620, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 121, ColType = "STRING", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Code of receiver of guarantee", OrdinateCode = "C0030", OrdinateID = 1621, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 121, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0060", HierarchyID = 264, IsRowKey = false, Label = "Triggering event(s) of guarantee", OrdinateCode = "C0060", OrdinateID = 1622, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 121, ColType = "MONETARY", ColNumber = 4, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Estimation of the maximum value of guarantee", OrdinateCode = "C0070", OrdinateID = 1623, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 121, ColType = "STRING", ColNumber = 5, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Specific triggering event(s) of guarantee", OrdinateCode = "C0080", OrdinateID = 1624, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 121, ColType = "DATE", ColNumber = 6, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Effective date of guarantee", OrdinateCode = "C0090", OrdinateID = 1625, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
