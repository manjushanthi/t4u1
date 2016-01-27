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
    public partial class S_36_01_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_36_01_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_36_01_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 450;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_36_01_01_01__sol2__2_0);
           DataTable = "T__S_36_01_01_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1718, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "ID of intragroup transaction", OrdinateCode = "C0010", OrdinateID = 8758, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1719, ColType = "STRING", ColNumber = 1, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "ID code of the instrument", OrdinateCode = "C0080", OrdinateID = 8759, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1720, ColType = "STRING", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Identification code for investor/ lender", OrdinateCode = "C0030", OrdinateID = 8760, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1721, ColType = "STRING", ColNumber = 3, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Identification code for issuer / borrower", OrdinateCode = "C0060", OrdinateID = 8761, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "STRING", ColNumber = 4, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Investor/ lender name", OrdinateCode = "C0020", OrdinateID = 8746, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "STRING", ColNumber = 5, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Issuer/ borrower name", OrdinateCode = "C0050", OrdinateID = 8747, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0100", HierarchyID = 303, IsRowKey = false, Label = "Transaction type", OrdinateCode = "C0100", OrdinateID = 8748, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "DATE", ColNumber = 7, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Transaction Issue date", OrdinateCode = "C0110", OrdinateID = 8749, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "DATE", ColNumber = 8, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Maturity date of transaction", OrdinateCode = "C0120", OrdinateID = 8750, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0130", HierarchyID = 163, IsRowKey = false, Label = "Currency of transaction", OrdinateCode = "C0130", OrdinateID = 8751, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "MONETARY", ColNumber = 10, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Contractual amount of transaction/ Transaction price", OrdinateCode = "C0140", OrdinateID = 8752, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "MONETARY", ColNumber = 11, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Value of collateral/ asset", OrdinateCode = "C0150", OrdinateID = 8753, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "MONETARY", ColNumber = 12, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Amount of redemptions/ prepayments/ paybacks during reporting period", OrdinateCode = "C0160", OrdinateID = 8754, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "MONETARY", ColNumber = 13, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Amount of dividends/ interest/ coupon and other payments made during reporting period", OrdinateCode = "C0170", OrdinateID = 8755, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "MONETARY", ColNumber = 14, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Balance of contractual amount of transaction at reporting date", OrdinateCode = "C0180", OrdinateID = 8756, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1717, ColType = "STRING", ColNumber = 15, ColName = "C0190", HierarchyID = 0, IsRowKey = false, Label = "Coupon/ Interest rate", OrdinateCode = "C0190", OrdinateID = 8757, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
