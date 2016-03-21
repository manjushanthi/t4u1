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
    public partial class S_33_01_01_02__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_33_01_01_02__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_33_01_01_02__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 374;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_33_01_01_02__sol2__1_5_2_c);
           DataTable = "T__S_33_01_01_02__sol2__1_5_2_c";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1522, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = true, Label = "Identification code", OrdinateCode = "C0020", OrdinateID = 6893, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1521, ColType = "STRING", ColNumber = 1, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the entity", OrdinateCode = "C0010", OrdinateID = 6889, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1521, ColType = "MONETARY", ColNumber = 2, ColName = "C0240", HierarchyID = 0, IsRowKey = false, Label = "Local capital requirement", OrdinateCode = "C0240", OrdinateID = 6890, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1521, ColType = "MONETARY", ColNumber = 3, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Local minimum capital requirement", OrdinateCode = "C0250", OrdinateID = 6891, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1521, ColType = "MONETARY", ColNumber = 4, ColName = "C0260", HierarchyID = 0, IsRowKey = false, Label = "Eligible own funds in accordance with local rules", OrdinateCode = "C0260", OrdinateID = 6892, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
