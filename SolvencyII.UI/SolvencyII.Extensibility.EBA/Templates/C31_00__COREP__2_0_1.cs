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
    public partial class C31_00__COREP__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.2.20.0
       public ISolvencyOpenUserControl Create {get{return new C31_00__COREP__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public C31_00__COREP__2_0_1()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 608;
           FrameworkCode = "COREP";
           DataType = typeof(T__C_31_00__COREP__2_0_1);
           DataTable = "T__C_31_00__COREP__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "STRING", ColNumber = 0, ColName = "C010", HierarchyID = 0, IsRowKey = true, Label = "Code", OrdinateCode = "010", OrdinateID = 21565, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "STRING", ColNumber = 1, ColName = "C020", HierarchyID = 0, IsRowKey = true, Label = "Group code", OrdinateCode = "020", OrdinateID = 21566, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 2, ColName = "C030", HierarchyID = 0, IsRowKey = false, Label = "Up to 1 Month", OrdinateCode = "030", OrdinateID = 21568, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 3, ColName = "C040", HierarchyID = 0, IsRowKey = false, Label = "Greater than 1 month up to 2 Months", OrdinateCode = "040", OrdinateID = 21569, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 4, ColName = "C050", HierarchyID = 0, IsRowKey = false, Label = "Greater than 2 months up to 3 Months", OrdinateCode = "050", OrdinateID = 21570, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 5, ColName = "C060", HierarchyID = 0, IsRowKey = false, Label = "Greater than 3 months up to 4 Months", OrdinateCode = "060", OrdinateID = 21571, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 6, ColName = "C070", HierarchyID = 0, IsRowKey = false, Label = "Greater than 4 months up to 5 Months", OrdinateCode = "070", OrdinateID = 21572, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 7, ColName = "C080", HierarchyID = 0, IsRowKey = false, Label = "Greater than 5 months up to 6 Months", OrdinateCode = "080", OrdinateID = 21573, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 8, ColName = "C090", HierarchyID = 0, IsRowKey = false, Label = "Greater than 6 months up to 7 Months", OrdinateCode = "090", OrdinateID = 21574, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 9, ColName = "C100", HierarchyID = 0, IsRowKey = false, Label = "Greater than 7 months up to 8 Months", OrdinateCode = "100", OrdinateID = 21575, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 10, ColName = "C110", HierarchyID = 0, IsRowKey = false, Label = "Greater than 8 months up to 9 Months", OrdinateCode = "110", OrdinateID = 21576, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 11, ColName = "C120", HierarchyID = 0, IsRowKey = false, Label = "Greater than 9 months up to 10 Months", OrdinateCode = "120", OrdinateID = 21577, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 12, ColName = "C130", HierarchyID = 0, IsRowKey = false, Label = "Greater than 10 months up to 11 Months", OrdinateCode = "130", OrdinateID = 21578, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 13, ColName = "C140", HierarchyID = 0, IsRowKey = false, Label = "Greater than 11 months up to 12 Months", OrdinateCode = "140", OrdinateID = 21579, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 14, ColName = "C150", HierarchyID = 0, IsRowKey = false, Label = "Greater than 12 months up to 15 Months", OrdinateCode = "150", OrdinateID = 21580, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 15, ColName = "C160", HierarchyID = 0, IsRowKey = false, Label = "Greater than 15 months up to 18 Months", OrdinateCode = "160", OrdinateID = 21581, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 16, ColName = "C170", HierarchyID = 0, IsRowKey = false, Label = "Greater than 18 months up to 21 Months", OrdinateCode = "170", OrdinateID = 21582, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 17, ColName = "C180", HierarchyID = 0, IsRowKey = false, Label = "Greater than 21 months up to 24 Months", OrdinateCode = "180", OrdinateID = 21583, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 18, ColName = "C190", HierarchyID = 0, IsRowKey = false, Label = "Greater than 24 months up to 27 Months", OrdinateCode = "190", OrdinateID = 21584, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 19, ColName = "C200", HierarchyID = 0, IsRowKey = false, Label = "Greater than 27 months up to 30 Months", OrdinateCode = "200", OrdinateID = 21585, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 20, ColName = "C210", HierarchyID = 0, IsRowKey = false, Label = "Greater than 30 months up to 33 Months", OrdinateCode = "210", OrdinateID = 21586, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 21, ColName = "C220", HierarchyID = 0, IsRowKey = false, Label = "Greater than 33 months up to 36 Months", OrdinateCode = "220", OrdinateID = 21587, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 22, ColName = "C230", HierarchyID = 0, IsRowKey = false, Label = "Greater than 3 years up to 5 years", OrdinateCode = "230", OrdinateID = 21588, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 23, ColName = "C240", HierarchyID = 0, IsRowKey = false, Label = "Greater than 5 years up to 10 years", OrdinateCode = "240", OrdinateID = 21589, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 24, ColName = "C250", HierarchyID = 0, IsRowKey = false, Label = "Greater than 10 years", OrdinateCode = "250", OrdinateID = 21590, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1463, ColType = "MONETARY", ColNumber = 25, ColName = "C260", HierarchyID = 0, IsRowKey = false, Label = "Undefined maturity", OrdinateCode = "260", OrdinateID = 21591, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
