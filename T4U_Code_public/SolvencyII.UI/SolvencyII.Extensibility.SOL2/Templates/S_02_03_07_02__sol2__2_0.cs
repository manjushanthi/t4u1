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
    public partial class S_02_03_07_02__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_02_03_07_02__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_02_03_07_02__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 42;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_02_03_07_02__sol2__2_0);
           DataTable = "T__S_02_03_07_02__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 103, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Code of encumbered assets", OrdinateCode = "C0020", OrdinateID = 1519, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 102, ColType = "STRING", ColNumber = 1, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Description of encumbered assets", OrdinateCode = "C0040", OrdinateID = 1514, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 102, ColType = "MONETARY", ColNumber = 2, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Gross value as per balance sheet", OrdinateCode = "C0050", OrdinateID = 1515, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 102, ColType = "MONETARY", ColNumber = 3, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Amount subject to prior security interests", OrdinateCode = "C0060", OrdinateID = 1516, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 102, ColType = "MONETARY", ColNumber = 4, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Net value of encumbered assets", OrdinateCode = "C0070", OrdinateID = 1517, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 102, ColType = "STRING", ColNumber = 5, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Description of encumbrance", OrdinateCode = "C0080", OrdinateID = 1518, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
