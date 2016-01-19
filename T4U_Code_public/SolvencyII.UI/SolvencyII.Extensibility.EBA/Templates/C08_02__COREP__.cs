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
    public partial class C08_02__COREP__ : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.2.20.0
       public ISolvencyOpenUserControl Create {get{return new C08_02__COREP__();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public C08_02__COREP__()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 560;
           FrameworkCode = "COREP";
           DataType = typeof(T__C_08_02__COREP__2_0_1);
           DataTable = "T__C_08_02__COREP__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "STRING", ColNumber = 0, ColName = "C005", HierarchyID = 0, IsRowKey = true, Label = "Obligor grade", OrdinateCode = "005", OrdinateID = 19965, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "PERCENTAGE", ColNumber = 1, ColName = "C010", HierarchyID = 0, IsRowKey = false, Label = "Internal rating System - PD assigned to the obligor grade or pool", OrdinateCode = "010", OrdinateID = 19966, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 2, ColName = "C110", HierarchyID = 0, IsRowKey = false, Label = "Exposure value", OrdinateCode = "110", OrdinateID = 19979, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 3, ColName = "C120", HierarchyID = 0, IsRowKey = false, Label = "Of which: off balance sheet items", OrdinateCode = "120", OrdinateID = 19980, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 4, ColName = "C130", HierarchyID = 0, IsRowKey = false, Label = "Of which: arising from counterparty credit risk", OrdinateCode = "130", OrdinateID = 19981, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 5, ColName = "C140", HierarchyID = 0, IsRowKey = false, Label = "Of which: large financial sector entities and unregulated financial entities", OrdinateCode = "140", OrdinateID = 19982, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 6, ColName = "C150", HierarchyID = 0, IsRowKey = false, Label = "Guarantees", OrdinateCode = "150", OrdinateID = 19985, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 7, ColName = "C160", HierarchyID = 0, IsRowKey = false, Label = "Credit derivatives", OrdinateCode = "160", OrdinateID = 19986, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 8, ColName = "C170", HierarchyID = 0, IsRowKey = false, Label = "Own estimates of lgd's are used:", OrdinateCode = "170", OrdinateID = 19988, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 9, ColName = "C180", HierarchyID = 0, IsRowKey = false, Label = "Eligible financial collateral", OrdinateCode = "180", OrdinateID = 19989, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 10, ColName = "C190", HierarchyID = 0, IsRowKey = false, Label = "Real estate", OrdinateCode = "190", OrdinateID = 19991, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 11, ColName = "C200", HierarchyID = 0, IsRowKey = false, Label = "Other physical collateral", OrdinateCode = "200", OrdinateID = 19992, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 12, ColName = "C210", HierarchyID = 0, IsRowKey = false, Label = "Receivables", OrdinateCode = "210", OrdinateID = 19993, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 13, ColName = "C020", HierarchyID = 0, IsRowKey = false, Label = "Original exposure conversion factors", OrdinateCode = "020", OrdinateID = 19967, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 14, ColName = "C030", HierarchyID = 0, IsRowKey = false, Label = "Of which: large financial sector entities and unregulated financial entities", OrdinateCode = "030", OrdinateID = 19968, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 15, ColName = "C090", HierarchyID = 0, IsRowKey = false, Label = "Exposure after CRM substitution effects pre conversion factors", OrdinateCode = "090", OrdinateID = 19977, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 16, ColName = "C220", HierarchyID = 0, IsRowKey = false, Label = "Unfunded credit protection", OrdinateCode = "220", OrdinateID = 19995, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "PERCENTAGE", ColNumber = 17, ColName = "C230", HierarchyID = 0, IsRowKey = false, Label = "Exposure weighted average lgd (%)", OrdinateCode = "230", OrdinateID = 19996, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "PERCENTAGE", ColNumber = 18, ColName = "C240", HierarchyID = 0, IsRowKey = false, Label = "Exposure weighted average LGD (%) for large financial sector entities and unregulated financial entities", OrdinateCode = "240", OrdinateID = 19997, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "INTEGER", ColNumber = 19, ColName = "C250", HierarchyID = 0, IsRowKey = false, Label = "Exposure-weighted average maturity value (days)", OrdinateCode = "250", OrdinateID = 19998, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 20, ColName = "C255", HierarchyID = 0, IsRowKey = false, Label = "Risk weighted exposure amount pre SME-supporting factor", OrdinateCode = "255", OrdinateID = 19999, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 21, ColName = "C260", HierarchyID = 0, IsRowKey = false, Label = "Risk weighted exposure amount after SME-supporting factor", OrdinateCode = "260", OrdinateID = 20000, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 22, ColName = "C270", HierarchyID = 0, IsRowKey = false, Label = "Of which: large financial sector entities and unregulated financial entities", OrdinateCode = "270", OrdinateID = 20001, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 23, ColName = "C280", HierarchyID = 0, IsRowKey = false, Label = "Expected loss amount", OrdinateCode = "280", OrdinateID = 20003, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 24, ColName = "C290", HierarchyID = 0, IsRowKey = false, Label = "(-) value adjustments and provisions", OrdinateCode = "290", OrdinateID = 20004, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "INTEGER", ColNumber = 25, ColName = "C300", HierarchyID = 0, IsRowKey = false, Label = "Number of obligors", OrdinateCode = "300", OrdinateID = 20005, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 26, ColName = "C040", HierarchyID = 0, IsRowKey = false, Label = "(-) Guarantees", OrdinateCode = "040", OrdinateID = 19971, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 27, ColName = "C050", HierarchyID = 0, IsRowKey = false, Label = "(-) Credit derivatives", OrdinateCode = "050", OrdinateID = 19972, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 28, ColName = "C060", HierarchyID = 0, IsRowKey = false, Label = "(-) Other funded credit protection", OrdinateCode = "060", OrdinateID = 19973, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 29, ColName = "C070", HierarchyID = 0, IsRowKey = false, Label = "(-) Total outflows", OrdinateCode = "070", OrdinateID = 19975, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 30, ColName = "C080", HierarchyID = 0, IsRowKey = false, Label = "Total inflows (+)", OrdinateCode = "080", OrdinateID = 19976, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1354, ColType = "MONETARY", ColNumber = 31, ColName = "C100", HierarchyID = 0, IsRowKey = false, Label = "Of which: off balance sheet items", OrdinateCode = "100", OrdinateID = 19978, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
