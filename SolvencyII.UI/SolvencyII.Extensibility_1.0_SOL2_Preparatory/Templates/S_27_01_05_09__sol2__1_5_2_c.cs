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
    public partial class S_27_01_05_09__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_27_01_05_09__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_27_01_05_09__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 325;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_27_01_05_09__sol2__1_5_2_c);
           DataTable = "T__S_27_01_05_09__sol2__1_5_2_c";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1342, ColType = "STRING", ColNumber = 0, ColName = "C0650", HierarchyID = 0, IsRowKey = true, Label = "Name of vessel", OrdinateCode = "C0650", OrdinateID = 5971, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1341, ColType = "MONETARY", ColNumber = 1, ColName = "C0580", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Share marine hull  in tanker t", OrdinateCode = "C0580", OrdinateID = 5964, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1341, ColType = "MONETARY", ColNumber = 2, ColName = "C0590", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Share marine liability in tanker t", OrdinateCode = "C0590", OrdinateID = 5965, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1341, ColType = "MONETARY", ColNumber = 3, ColName = "C0600", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Share marine oil pollution liability in tanker t", OrdinateCode = "C0600", OrdinateID = 5966, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1341, ColType = "MONETARY", ColNumber = 4, ColName = "C0610", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Marine Tanker Collision", OrdinateCode = "C0610", OrdinateID = 5967, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1341, ColType = "MONETARY", ColNumber = 5, ColName = "C0620", HierarchyID = 0, IsRowKey = false, Label = "Estimated Risk Mitigation", OrdinateCode = "C0620", OrdinateID = 5968, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1341, ColType = "MONETARY", ColNumber = 6, ColName = "C0630", HierarchyID = 0, IsRowKey = false, Label = "Estimated Reinstatement Premiums", OrdinateCode = "C0630", OrdinateID = 5969, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1341, ColType = "MONETARY", ColNumber = 7, ColName = "C0640", HierarchyID = 0, IsRowKey = false, Label = "Net Cat Risk Charge Marine Tanker Collision", OrdinateCode = "C0640", OrdinateID = 5970, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
