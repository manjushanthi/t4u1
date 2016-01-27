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
    public partial class S_27_01_02_10__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_27_01_02_10__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_27_01_02_10__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 260;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_27_01_02_10__sol2__1_5_2_c);
           DataTable = "T__S_27_01_02_10__sol2__1_5_2_c";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1038, ColType = "STRING", ColNumber = 0, ColName = "C0750", HierarchyID = 0, IsRowKey = true, Label = "Name of platform", OrdinateCode = "C0750", OrdinateID = 4415, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 1, ColName = "C0660", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Property damage", OrdinateCode = "C0660", OrdinateID = 4406, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 2, ColName = "C0670", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Removal of wreckage", OrdinateCode = "C0670", OrdinateID = 4407, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 3, ColName = "C0680", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Loss of production income", OrdinateCode = "C0680", OrdinateID = 4408, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 4, ColName = "C0690", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Capping of the well or making the well secure", OrdinateCode = "C0690", OrdinateID = 4409, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 5, ColName = "C0700", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Liability insurance and reinsurance obligations", OrdinateCode = "C0700", OrdinateID = 4410, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 6, ColName = "C0710", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Marine Platform Explosion", OrdinateCode = "C0710", OrdinateID = 4411, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 7, ColName = "C0720", HierarchyID = 0, IsRowKey = false, Label = "Estimated Risk Mitigation", OrdinateCode = "C0720", OrdinateID = 4412, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 8, ColName = "C0730", HierarchyID = 0, IsRowKey = false, Label = "Estimated Reinstatement Premiums", OrdinateCode = "C0730", OrdinateID = 4413, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1037, ColType = "MONETARY", ColNumber = 9, ColName = "C0740", HierarchyID = 0, IsRowKey = false, Label = "Net Cat Risk Charge Marine Platform Explosion", OrdinateCode = "C0740", OrdinateID = 4414, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
