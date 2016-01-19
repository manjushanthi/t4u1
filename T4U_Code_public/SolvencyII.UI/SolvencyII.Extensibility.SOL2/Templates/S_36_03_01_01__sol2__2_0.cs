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
    public partial class S_36_03_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_36_03_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_36_03_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 452;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_36_03_01_01__sol2__2_0);
           DataTable = "T__S_36_03_01_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1728, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "ID of intragroup transaction", OrdinateCode = "C0010", OrdinateID = 8795, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1729, ColType = "ENUMERATION/CODE", ColNumber = 1, ColName = "C0160", HierarchyID = 259, IsRowKey = false, Label = "Line of business", OrdinateCode = "C0160", OrdinateID = 8796, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1730, ColType = "STRING", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Identification code of cedent", OrdinateCode = "C0030", OrdinateID = 8797, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1731, ColType = "STRING", ColNumber = 3, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Identification code of reinsurer", OrdinateCode = "C0060", OrdinateID = 8798, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "STRING", ColNumber = 4, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Name of cedent", OrdinateCode = "C0020", OrdinateID = 8785, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "STRING", ColNumber = 5, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Name of reinsurer", OrdinateCode = "C0050", OrdinateID = 8786, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "DATE", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Validity period (start date)", OrdinateCode = "C0080", OrdinateID = 8787, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "DATE", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Validity period (expiry date)", OrdinateCode = "C0090", OrdinateID = 8788, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0100", HierarchyID = 163, IsRowKey = false, Label = "Currency of contract/treaty", OrdinateCode = "C0100", OrdinateID = 8789, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "ENUMERATION/CODE", ColNumber = 9, ColName = "C0110", HierarchyID = 405, IsRowKey = false, Label = "Type of reinsurance contract/treaty", OrdinateCode = "C0110", OrdinateID = 8790, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "MONETARY", ColNumber = 10, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Maximum cover by reinsurer under contract/treaty", OrdinateCode = "C0120", OrdinateID = 8791, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "MONETARY", ColNumber = 11, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Net Receivables", OrdinateCode = "C0130", OrdinateID = 8792, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "MONETARY", ColNumber = 12, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Total reinsurance recoverables", OrdinateCode = "C0140", OrdinateID = 8793, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1727, ColType = "MONETARY", ColNumber = 13, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Reinsurance result (for reinsured entity)", OrdinateCode = "C0150", OrdinateID = 8794, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
