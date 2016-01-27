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
    public partial class S_21_02_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_21_02_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_21_02_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 146;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_21_02_01_01__sol2__2_0);
           DataTable = "T__S_21_02_01_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 528, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Risk identification code", OrdinateCode = "C0010", OrdinateID = 3921, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification of the company / person to which the risk relates", OrdinateCode = "C0020", OrdinateID = 3907, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "STRING", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Description risk", OrdinateCode = "C0030", OrdinateID = 3908, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0040", HierarchyID = 246, IsRowKey = false, Label = "Line of business", OrdinateCode = "C0040", OrdinateID = 3909, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "STRING", ColNumber = 4, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Description risk category covered", OrdinateCode = "C0050", OrdinateID = 3910, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "DATE", ColNumber = 5, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Validity period (start date)", OrdinateCode = "C0060", OrdinateID = 3911, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "DATE", ColNumber = 6, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Validity period (expiry date)", OrdinateCode = "C0070", OrdinateID = 3912, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0080", HierarchyID = 163, IsRowKey = false, Label = "Currency", OrdinateCode = "C0080", OrdinateID = 3913, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "MONETARY", ColNumber = 8, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Sum insured", OrdinateCode = "C0090", OrdinateID = 3914, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "MONETARY", ColNumber = 9, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Original deductible policyholder", OrdinateCode = "C0100", OrdinateID = 3915, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0110", HierarchyID = 394, IsRowKey = false, Label = "Type of underwriting model", OrdinateCode = "C0110", OrdinateID = 3916, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "MONETARY", ColNumber = 11, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Amount underwriting model", OrdinateCode = "C0120", OrdinateID = 3917, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "MONETARY", ColNumber = 12, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Sum reinsured on a facultative basis, with all reinsurers", OrdinateCode = "C0130", OrdinateID = 3918, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "MONETARY", ColNumber = 13, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Sum reinsured, other than on facultative basis, with all reinsurers", OrdinateCode = "C0140", OrdinateID = 3919, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 527, ColType = "MONETARY", ColNumber = 14, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Net retention of the insurer", OrdinateCode = "C0150", OrdinateID = 3920, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
