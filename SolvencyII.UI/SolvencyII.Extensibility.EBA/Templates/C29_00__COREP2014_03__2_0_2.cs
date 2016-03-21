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
    public partial class C29_00__COREP2014_03__2_0_2 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.2.20.0
       public ISolvencyOpenUserControl Create {get{return new C29_00__COREP2014_03__2_0_2();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public C29_00__COREP2014_03__2_0_2()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 602;
           FrameworkCode = "COREP";
           DataType = typeof(T__C_29_00__COREP__2_0_1);
           DataTable = "T__C_29_00__COREP__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "STRING", ColNumber = 0, ColName = "C010", HierarchyID = 0, IsRowKey = true, Label = "Code", OrdinateCode = "010", OrdinateID = 21390, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "STRING", ColNumber = 1, ColName = "C020", HierarchyID = 0, IsRowKey = true, Label = "Group code", OrdinateCode = "020", OrdinateID = 21391, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "BOOLEAN", ColNumber = 2, ColName = "C030", HierarchyID = 0, IsRowKey = false, Label = "Transactions where there is an exposure to underlying assets", OrdinateCode = "030", OrdinateID = 21392, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C040", HierarchyID = 10280, IsRowKey = false, Label = "Type of connection", OrdinateCode = "040", OrdinateID = 21393, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 4, ColName = "C200", HierarchyID = 0, IsRowKey = false, Label = "(-) Value adjustments and provisions", OrdinateCode = "200", OrdinateID = 21414, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 5, ColName = "C210", HierarchyID = 0, IsRowKey = false, Label = "(-) Exposures deducted from own funds", OrdinateCode = "210", OrdinateID = 21415, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 6, ColName = "C220", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "220", OrdinateID = 21417, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 7, ColName = "C230", HierarchyID = 0, IsRowKey = false, Label = "Of which: Non-trading book", OrdinateCode = "230", OrdinateID = 21418, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "PERCENTAGE", ColNumber = 8, ColName = "C240", HierarchyID = 0, IsRowKey = false, Label = "% of eligible capital", OrdinateCode = "240", OrdinateID = 21419, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 9, ColName = "C250", HierarchyID = 0, IsRowKey = false, Label = "(-) Debt instruments", OrdinateCode = "250", OrdinateID = 21422, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 10, ColName = "C260", HierarchyID = 0, IsRowKey = false, Label = "(-) Equity instruments", OrdinateCode = "260", OrdinateID = 21423, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 11, ColName = "C270", HierarchyID = 0, IsRowKey = false, Label = "(-) Derivatives", OrdinateCode = "270", OrdinateID = 21424, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 12, ColName = "C280", HierarchyID = 0, IsRowKey = false, Label = "(-) Loan commitments", OrdinateCode = "280", OrdinateID = 21426, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 13, ColName = "C290", HierarchyID = 0, IsRowKey = false, Label = "(-) Financial Guarantees", OrdinateCode = "290", OrdinateID = 21427, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 14, ColName = "C300", HierarchyID = 0, IsRowKey = false, Label = "(-) Other commitments", OrdinateCode = "300", OrdinateID = 21428, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 15, ColName = "C310", HierarchyID = 0, IsRowKey = false, Label = "(-) Funded credit protection other than substitution effect", OrdinateCode = "310", OrdinateID = 21429, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 16, ColName = "C320", HierarchyID = 0, IsRowKey = false, Label = "(-) Real estate", OrdinateCode = "320", OrdinateID = 21430, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 17, ColName = "C330", HierarchyID = 0, IsRowKey = false, Label = "(-) Amounts exempted", OrdinateCode = "330", OrdinateID = 21431, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 18, ColName = "C340", HierarchyID = 0, IsRowKey = false, Label = "Total", OrdinateCode = "340", OrdinateID = 21433, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 19, ColName = "C350", HierarchyID = 0, IsRowKey = false, Label = "Of which: Non-trading book", OrdinateCode = "350", OrdinateID = 21434, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "PERCENTAGE", ColNumber = 20, ColName = "C360", HierarchyID = 0, IsRowKey = false, Label = "% of eligible capital", OrdinateCode = "360", OrdinateID = 21435, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 21, ColName = "C130", HierarchyID = 0, IsRowKey = false, Label = "Debt instruments", OrdinateCode = "130", OrdinateID = 21406, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 22, ColName = "C140", HierarchyID = 0, IsRowKey = false, Label = "Equity instruments", OrdinateCode = "140", OrdinateID = 21407, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 23, ColName = "C150", HierarchyID = 0, IsRowKey = false, Label = "Derivatives", OrdinateCode = "150", OrdinateID = 21408, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 24, ColName = "C160", HierarchyID = 0, IsRowKey = false, Label = "Loan commitments", OrdinateCode = "160", OrdinateID = 21410, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 25, ColName = "C170", HierarchyID = 0, IsRowKey = false, Label = "Financial guarantees", OrdinateCode = "170", OrdinateID = 21411, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 26, ColName = "C180", HierarchyID = 0, IsRowKey = false, Label = "Other commitments", OrdinateCode = "180", OrdinateID = 21412, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 27, ColName = "C050", HierarchyID = 0, IsRowKey = false, Label = "Total original exposure", OrdinateCode = "050", OrdinateID = 21395, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 28, ColName = "C190", HierarchyID = 0, IsRowKey = false, Label = "Additional exposures arising from transactions where there is an exposure to underlying assets", OrdinateCode = "190", OrdinateID = 21413, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 29, ColName = "C060", HierarchyID = 0, IsRowKey = false, Label = "Of which: defaulted", OrdinateCode = "060", OrdinateID = 21396, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 30, ColName = "C070", HierarchyID = 0, IsRowKey = false, Label = "Debt instruments", OrdinateCode = "070", OrdinateID = 21398, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 31, ColName = "C080", HierarchyID = 0, IsRowKey = false, Label = "Equity instruments", OrdinateCode = "080", OrdinateID = 21399, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 32, ColName = "C090", HierarchyID = 0, IsRowKey = false, Label = "Derivatives", OrdinateCode = "090", OrdinateID = 21400, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 33, ColName = "C100", HierarchyID = 0, IsRowKey = false, Label = "Loan commitments", OrdinateCode = "100", OrdinateID = 21402, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 34, ColName = "C110", HierarchyID = 0, IsRowKey = false, Label = "Financial guarantees", OrdinateCode = "110", OrdinateID = 21403, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1448, ColType = "MONETARY", ColNumber = 35, ColName = "C120", HierarchyID = 0, IsRowKey = false, Label = "Other commitments", OrdinateCode = "120", OrdinateID = 21404, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
