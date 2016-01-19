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
    public partial class S_23_04_04_06__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_04_06__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_04_06__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 205;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_04_06__sol2__2_0);
           DataTable = "T__S_23_04_04_06__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 707, ColType = "STRING", ColNumber = 0, ColName = "C0585", HierarchyID = 0, IsRowKey = false, Label = "Code of ancillary own funds", OrdinateCode = "C0585", OrdinateID = 4756, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 706, ColType = "STRING", ColNumber = 1, ColName = "C0590", HierarchyID = 0, IsRowKey = false, Label = "Description of ancillary own funds", OrdinateCode = "C0590", OrdinateID = 4749, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 706, ColType = "MONETARY", ColNumber = 2, ColName = "C0600", HierarchyID = 0, IsRowKey = false, Label = "Amount", OrdinateCode = "C0600", OrdinateID = 4750, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 706, ColType = "STRING", ColNumber = 3, ColName = "C0610", HierarchyID = 0, IsRowKey = false, Label = "Counterpart", OrdinateCode = "C0610", OrdinateID = 4751, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 706, ColType = "DATE", ColNumber = 4, ColName = "C0620", HierarchyID = 0, IsRowKey = false, Label = "issue date", OrdinateCode = "C0620", OrdinateID = 4752, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 706, ColType = "DATE", ColNumber = 5, ColName = "C0630", HierarchyID = 0, IsRowKey = false, Label = "Date of authorisation", OrdinateCode = "C0630", OrdinateID = 4753, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 706, ColType = "STRING", ColNumber = 6, ColName = "C0640", HierarchyID = 0, IsRowKey = false, Label = "Name of supervisory authority having given authorisation", OrdinateCode = "C0640", OrdinateID = 4754, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 706, ColType = "STRING", ColNumber = 7, ColName = "C0650", HierarchyID = 0, IsRowKey = false, Label = "Name of entity concerned", OrdinateCode = "C0650", OrdinateID = 4755, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
