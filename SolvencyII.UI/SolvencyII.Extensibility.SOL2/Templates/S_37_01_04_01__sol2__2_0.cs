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
    public partial class S_37_01_04_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_37_01_04_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_37_01_04_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 454;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_37_01_04_01__sol2__2_0);
           DataTable = "T__S_37_01_04_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1737, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the counterparty of the group", OrdinateCode = "C0020", OrdinateID = 8826, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1738, ColType = "STRING", ColNumber = 1, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the group entity", OrdinateCode = "C0120", OrdinateID = 8827, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1739, ColType = "STRING", ColNumber = 2, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Identification code of the exposure", OrdinateCode = "C0060", OrdinateID = 8828, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "STRING", ColNumber = 3, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Name of the external counterparty", OrdinateCode = "C0010", OrdinateID = 8815, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0040", HierarchyID = 207, IsRowKey = false, Label = "Country of the exposure", OrdinateCode = "C0040", OrdinateID = 8816, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0050", HierarchyID = 189, IsRowKey = false, Label = "Nature of the exposure", OrdinateCode = "C0050", OrdinateID = 8817, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "STRING", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "External Rating", OrdinateCode = "C0080", OrdinateID = 8818, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "STRING", ColNumber = 7, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Nominated ECAI", OrdinateCode = "C0090", OrdinateID = 8819, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "ENUMERATION/CODE", ColNumber = 8, ColName = "C0100", HierarchyID = 316, IsRowKey = false, Label = "Sector", OrdinateCode = "C0100", OrdinateID = 8820, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "STRING", ColNumber = 9, ColName = "C0110", HierarchyID = 0, IsRowKey = false, Label = "Group entity subject to the exposure", OrdinateCode = "C0110", OrdinateID = 8821, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "DATE", ColNumber = 10, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Maturity (asset side)/ Validity (liability side)", OrdinateCode = "C0140", OrdinateID = 8822, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "MONETARY", ColNumber = 11, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Value of the exposure", OrdinateCode = "C0150", OrdinateID = 8823, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "ENUMERATION/CODE", ColNumber = 12, ColName = "C0160", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0160", OrdinateID = 8824, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1736, ColType = "MONETARY", ColNumber = 13, ColName = "C0170", HierarchyID = 0, IsRowKey = false, Label = "Maximum amount to be paid by the reinsurer", OrdinateCode = "C0170", OrdinateID = 8825, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
