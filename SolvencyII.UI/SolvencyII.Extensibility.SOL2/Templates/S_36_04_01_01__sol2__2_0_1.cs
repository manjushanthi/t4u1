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
    public partial class S_36_04_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_36_04_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_36_04_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 455;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_36_04_01_01__sol2__2_0_1);
           DataTable = "T__S_36_04_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1733, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "ID of intragroup transaction", OrdinateCode = "C0010", OrdinateID = 8802, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1734, ColType = "STRING", ColNumber = 1, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the Investor/ Buyer/ Beneficiary", OrdinateCode = "C0030", OrdinateID = 8803, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1735, ColType = "STRING", ColNumber = 2, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the Issuer/ Seller/ Provider", OrdinateCode = "C0060", OrdinateID = 8804, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "STRING", ColNumber = 3, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Investor/ Buyer/ Beneficiary name", OrdinateCode = "C0020", OrdinateID = 8789, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "STRING", ColNumber = 4, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Issuer/ Seller/ Provider name", OrdinateCode = "C0050", OrdinateID = 8790, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0080", HierarchyID = 325, IsRowKey = false, Label = "Transaction type", OrdinateCode = "C0080", OrdinateID = 8791, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "DATE", ColNumber = 6, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Transaction Issue date", OrdinateCode = "C0090", OrdinateID = 8792, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "DATE", ColNumber = 7, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Effective date of agreement/ contract underlying transaction", OrdinateCode = "C0100", OrdinateID = 8793, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "DATE", ColNumber = 8, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Expiry date of agreement/ contract underlying transaction", OrdinateCode = "C0110", OrdinateID = 8794, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0120", HierarchyID = 179, IsRowKey = false, Label = "Currency of transaction", OrdinateCode = "C0120", OrdinateID = 8795, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "STRING", ColNumber = 10, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Trigger event", OrdinateCode = "C0130", OrdinateID = 8796, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "MONETARY", ColNumber = 11, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Value of transaction/ collateral / Guarantee", OrdinateCode = "C0140", OrdinateID = 8797, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "MONETARY", ColNumber = 12, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Maximum possible value of contingent liabilities", OrdinateCode = "C0150", OrdinateID = 8798, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "MONETARY", ColNumber = 13, ColName = "C0160", HierarchyID = 0, IsRowKey = false, Label = "Maximum possible value of contingent liabilities not included in Solvency II Balance Sheet", OrdinateCode = "C0160", OrdinateID = 8799, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "MONETARY", ColNumber = 14, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Maximum value of letters of credit/ guarantees", OrdinateCode = "C0170", OrdinateID = 8800, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1732, ColType = "MONETARY", ColNumber = 15, ColName = "C0180", HierarchyID = 0, IsRowKey = false, Label = "Value of guaranteed assets", OrdinateCode = "C0180", OrdinateID = 8801, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
