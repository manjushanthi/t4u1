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
    public partial class E_01_01_16_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new E_01_01_16_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public E_01_01_16_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 459;
           FrameworkCode = "s2md";
           DataType = typeof(T__E_01_01_16_01__sol2__2_0);
           DataTable = "T__E_01_01_16_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1749, ColType = "STRING", ColNumber = 0, ColName = "EC0010", HierarchyID = 0, IsRowKey = false, Label = "Line identification code", OrdinateCode = "EC0010", OrdinateID = 8844, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1748, ColType = "ENUMERATION/CODE", ColNumber = 1, ColName = "EC0020", HierarchyID = 193, IsRowKey = false, Label = "Issuer Country", OrdinateCode = "EC0020", OrdinateID = 8839, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1748, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "EC0030", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "EC0030", OrdinateID = 8840, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1748, ColType = "MONETARY", ColNumber = 3, ColName = "EC0040", HierarchyID = 0, IsRowKey = false, Label = "Total Solvency II amount", OrdinateCode = "EC0040", OrdinateID = 8841, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1748, ColType = "MONETARY", ColNumber = 4, ColName = "EC0050", HierarchyID = 0, IsRowKey = false, Label = "Accrued interest", OrdinateCode = "EC0050", OrdinateID = 8842, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1748, ColType = "MONETARY", ColNumber = 5, ColName = "EC0060", HierarchyID = 0, IsRowKey = false, Label = "Par amount", OrdinateCode = "EC0060", OrdinateID = 8843, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 