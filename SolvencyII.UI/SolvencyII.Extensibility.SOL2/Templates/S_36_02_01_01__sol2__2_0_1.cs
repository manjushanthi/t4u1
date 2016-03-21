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
    public partial class S_36_02_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_36_02_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_36_02_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 453;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_36_02_01_01__sol2__2_0_1);
           DataTable = "T__S_36_02_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1721, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "ID of intragroup transaction", OrdinateCode = "C0010", OrdinateID = 8769, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1722, ColType = "STRING", ColNumber = 1, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "ID code of the instrument", OrdinateCode = "C0080", OrdinateID = 8770, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1723, ColType = "STRING", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Identification code of investor / buyer", OrdinateCode = "C0030", OrdinateID = 8771, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1724, ColType = "STRING", ColNumber = 3, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the issuer / seller", OrdinateCode = "C0060", OrdinateID = 8772, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "STRING", ColNumber = 4, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Investor/ Buyer", OrdinateCode = "C0020", OrdinateID = 8750, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "STRING", ColNumber = 5, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Issuer/ Seller name", OrdinateCode = "C0050", OrdinateID = 8751, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0100", HierarchyID = 324, IsRowKey = false, Label = "Transaction type", OrdinateCode = "C0100", OrdinateID = 8752, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "DATE", ColNumber = 7, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Transaction Trade date", OrdinateCode = "C0110", OrdinateID = 8753, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "DATE", ColNumber = 8, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Maturity date", OrdinateCode = "C0120", OrdinateID = 8754, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0130", HierarchyID = 179, IsRowKey = false, Label = "Currency", OrdinateCode = "C0130", OrdinateID = 8755, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "MONETARY", ColNumber = 10, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Notional amount at transaction date", OrdinateCode = "C0140", OrdinateID = 8756, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "MONETARY", ColNumber = 11, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Notional amount at reporting date", OrdinateCode = "C0150", OrdinateID = 8757, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "MONETARY", ColNumber = 12, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Value of collateral", OrdinateCode = "C0160", OrdinateID = 8758, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "ENUMERATION/CODE", ColNumber = 13, ColName = "C0170", HierarchyID = 365, IsRowKey = false, Label = "Use of derivatives (by buyer)", OrdinateCode = "C0170", OrdinateID = 8760, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "STRING", ColNumber = 14, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Identification code Asset / Liability underlying the derivative", OrdinateCode = "C0180", OrdinateID = 8761, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "STRING", ColNumber = 15, ColName = "C0200", HierarchyID = 0, IsRowKey = false, Label = "Counterparty name for which credit protection is purchased", OrdinateCode = "C0200", OrdinateID = 8763, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "STRING", ColNumber = 16, ColName = "C0210", HierarchyID = 0, IsRowKey = false, Label = "Swap delivered interest rate (for buyer)", OrdinateCode = "C0210", OrdinateID = 8765, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "STRING", ColNumber = 17, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Swap received interest rate (for buyer)", OrdinateCode = "C0220", OrdinateID = 8766, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "ENUMERATION/CODE", ColNumber = 18, ColName = "C0230", HierarchyID = 183, IsRowKey = false, Label = "Swap delivered currency (for buyer)", OrdinateCode = "C0230", OrdinateID = 8767, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "ENUMERATION/CODE", ColNumber = 19, ColName = "C0240", HierarchyID = 183, IsRowKey = false, Label = "Swap received currency (for buyer)", OrdinateCode = "C0240", OrdinateID = 8768, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
