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
    public partial class S_34_01_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_34_01_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_34_01_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 448;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_34_01_04_01__sol2__2_0);
           DataTable = "T__S_34_01_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1713, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the undertaking", OrdinateCode = "C0020", OrdinateID = 8709, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1712, ColType = "STRING", ColNumber = 1, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 8703, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1712, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0040", HierarchyID = 146, IsRowKey = false, Label = "Aggregated or not", OrdinateCode = "C0040", OrdinateID = 8704, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1712, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0050", HierarchyID = 6, IsRowKey = false, Label = "Type of capital requirement", OrdinateCode = "C0050", OrdinateID = 8705, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1712, ColType = "MONETARY", ColNumber = 4, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Notional SCR or Sectoral capital requirement", OrdinateCode = "C0060", OrdinateID = 8706, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1712, ColType = "MONETARY", ColNumber = 5, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Notional MCR or Sectoral minimum capital requirement", OrdinateCode = "C0070", OrdinateID = 8707, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1712, ColType = "MONETARY", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Notional or Sectoral Eligible Own Funds", OrdinateCode = "C0080", OrdinateID = 8708, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
