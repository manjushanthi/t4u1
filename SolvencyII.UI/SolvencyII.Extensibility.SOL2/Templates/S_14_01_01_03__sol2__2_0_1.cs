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
    public partial class S_14_01_01_03__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_14_01_01_03__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_14_01_01_03__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 111;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_14_01_01_03__sol2__2_0_1);
           DataTable = "T__S_14_01_01_03__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 342, ColType = "STRING", ColNumber = 0, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "HRG code", OrdinateCode = "C0170", OrdinateID = 2906, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "MONETARY", ColNumber = 1, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Best Estimate", OrdinateCode = "C0180", OrdinateID = 2902, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "MONETARY", ColNumber = 2, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Capital-at-risk", OrdinateCode = "C0190", OrdinateID = 2903, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "MONETARY", ColNumber = 3, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Surrender value", OrdinateCode = "C0200", OrdinateID = 2904, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 341, ColType = "STRING", ColNumber = 4, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Annualised guaranteed rate (over average duration of guarantee)", OrdinateCode = "C0210", OrdinateID = 2905, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
