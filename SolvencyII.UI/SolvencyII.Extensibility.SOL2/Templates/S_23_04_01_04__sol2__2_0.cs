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
    public partial class S_23_04_01_04__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_01_04__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_01_04__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 195;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_01_04__sol2__2_0);
           DataTable = "T__S_23_04_01_04__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 681, ColType = "STRING", ColNumber = 0, ColName = "C0445", HierarchyID = 0, IsRowKey = false, Label = "Code of items approved by supervisory authority as basic own funds", OrdinateCode = "C0445", OrdinateID = 4661, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 680, ColType = "STRING", ColNumber = 1, ColName = "C0450", HierarchyID = 0, IsRowKey = false, Label = "Other items approved by supervisory authority as basic own funds not specified above", OrdinateCode = "C0450", OrdinateID = 4654, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 680, ColType = "MONETARY", ColNumber = 2, ColName = "C0460", HierarchyID = 0, IsRowKey = false, Label = "Amount", OrdinateCode = "C0460", OrdinateID = 4655, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 680, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0470", HierarchyID = 163, IsRowKey = false, Label = "Currency Code", OrdinateCode = "C0470", OrdinateID = 4656, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 680, ColType = "MONETARY", ColNumber = 4, ColName = "C0480", HierarchyID = 0, IsRowKey = false, Label = "Tier 1", OrdinateCode = "C0480", OrdinateID = 4657, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 680, ColType = "MONETARY", ColNumber = 5, ColName = "C0490", HierarchyID = 0, IsRowKey = false, Label = "Tier 2", OrdinateCode = "C0490", OrdinateID = 4658, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 680, ColType = "MONETARY", ColNumber = 6, ColName = "C0500", HierarchyID = 0, IsRowKey = false, Label = "Tier 3", OrdinateCode = "C0500", OrdinateID = 4659, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 680, ColType = "DATE", ColNumber = 7, ColName = "C0510", HierarchyID = 0, IsRowKey = false, Label = "Date of authorisation", OrdinateCode = "C0510", OrdinateID = 4660, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
