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
    public partial class F40_02__FINREP2014_03__2_1_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.2.20.0
       public ISolvencyOpenUserControl Create {get{return new F40_02__FINREP2014_03__2_1_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public F40_02__FINREP2014_03__2_1_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 594;
           FrameworkCode = "FINREP";
           DataType = typeof(T__F_40_02__FINREP__2_0_1);
           DataTable = "T__F_40_02__FINREP__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1432, ColType = "STRING", ColNumber = 0, ColName = "C010", HierarchyID = 0, IsRowKey = true, Label = "Security code", OrdinateCode = "010", OrdinateID = 21191, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1432, ColType = "STRING", ColNumber = 1, ColName = "C040", HierarchyID = 0, IsRowKey = true, Label = "Holding company code", OrdinateCode = "040", OrdinateID = 21194, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1432, ColType = "STRING", ColNumber = 2, ColName = "C020", HierarchyID = 0, IsRowKey = false, Label = "Entity code", OrdinateCode = "020", OrdinateID = 21192, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1432, ColType = "STRING", ColNumber = 3, ColName = "C030", HierarchyID = 0, IsRowKey = false, Label = "Holding company LEI code", OrdinateCode = "030", OrdinateID = 21193, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1432, ColType = "STRING", ColNumber = 4, ColName = "C050", HierarchyID = 0, IsRowKey = false, Label = "Holding company name", OrdinateCode = "050", OrdinateID = 21195, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1432, ColType = "PERCENTAGE", ColNumber = 5, ColName = "C060", HierarchyID = 0, IsRowKey = false, Label = "Accumulated equity interest (%)", OrdinateCode = "060", OrdinateID = 21196, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1432, ColType = "MONETARY", ColNumber = 6, ColName = "C070", HierarchyID = 0, IsRowKey = false, Label = "Carrying amount", OrdinateCode = "070", OrdinateID = 21197, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1432, ColType = "MONETARY", ColNumber = 7, ColName = "C080", HierarchyID = 0, IsRowKey = false, Label = "Acquisition cost", OrdinateCode = "080", OrdinateID = 21198, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
