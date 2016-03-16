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
    public partial class S_27_01_06_09__sol2__1_5_2_c : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_27_01_06_09__sol2__1_5_2_c();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_27_01_06_09__sol2__1_5_2_c()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 347;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_27_01_06_09__sol2__1_5_2_c);
           DataTable = "T__S_27_01_06_09__sol2__1_5_2_c";
           GridTop = 60;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1430, ColType = "STRING", ColNumber = 0, ColName = "C0650", HierarchyID = 0, IsRowKey = true, Label = "Name of vessel", OrdinateCode = "C0650", OrdinateID = 6479, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1429, ColType = "MONETARY", ColNumber = 1, ColName = "C0580", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Share marine hull  in tanker t", OrdinateCode = "C0580", OrdinateID = 6472, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1429, ColType = "MONETARY", ColNumber = 2, ColName = "C0590", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Share marine liability in tanker t", OrdinateCode = "C0590", OrdinateID = 6473, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1429, ColType = "MONETARY", ColNumber = 3, ColName = "C0600", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Share marine oil pollution liability in tanker t", OrdinateCode = "C0600", OrdinateID = 6474, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1429, ColType = "MONETARY", ColNumber = 4, ColName = "C0610", HierarchyID = 0, IsRowKey = false, Label = "Gross Cat Risk Charge Marine Tanker Collision", OrdinateCode = "C0610", OrdinateID = 6475, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1429, ColType = "MONETARY", ColNumber = 5, ColName = "C0620", HierarchyID = 0, IsRowKey = false, Label = "Estimated Risk Mitigation", OrdinateCode = "C0620", OrdinateID = 6476, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1429, ColType = "MONETARY", ColNumber = 6, ColName = "C0630", HierarchyID = 0, IsRowKey = false, Label = "Estimated Reinstatement Premiums", OrdinateCode = "C0630", OrdinateID = 6477, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1429, ColType = "MONETARY", ColNumber = 7, ColName = "C0640", HierarchyID = 0, IsRowKey = false, Label = "Net Cat Risk Charge Marine Tanker Collision", OrdinateCode = "C0640", OrdinateID = 6478, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
