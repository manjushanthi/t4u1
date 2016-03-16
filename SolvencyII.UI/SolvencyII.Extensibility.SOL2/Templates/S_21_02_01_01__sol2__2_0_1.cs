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
    public partial class S_21_02_01_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_21_02_01_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_21_02_01_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 149;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_21_02_01_01__sol2__2_0_1);
           DataTable = "T__S_21_02_01_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 540, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Risk identification code", OrdinateCode = "C0010", OrdinateID = 3954, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "STRING", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification of the company / person to which the risk relates", OrdinateCode = "C0020", OrdinateID = 3940, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "STRING", ColNumber = 2, ColName = "C0030", HierarchyID = 0, IsRowKey = false, Label = "Description risk", OrdinateCode = "C0030", OrdinateID = 3941, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0040", HierarchyID = 266, IsRowKey = false, Label = "Line of business", OrdinateCode = "C0040", OrdinateID = 3942, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "STRING", ColNumber = 4, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Description risk category covered", OrdinateCode = "C0050", OrdinateID = 3943, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "DATE", ColNumber = 5, ColName = "C0060", HierarchyID = 0, IsRowKey = false, Label = "Validity period (start date)", OrdinateCode = "C0060", OrdinateID = 3944, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "DATE", ColNumber = 6, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Validity period (expiry date)", OrdinateCode = "C0070", OrdinateID = 3945, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0080", HierarchyID = 179, IsRowKey = false, Label = "Currency", OrdinateCode = "C0080", OrdinateID = 3946, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "MONETARY", ColNumber = 8, ColName = "C0090", HierarchyID = 0, IsRowKey = false, Label = "Sum insured", OrdinateCode = "C0090", OrdinateID = 3947, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "MONETARY", ColNumber = 9, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Original deductible policyholder", OrdinateCode = "C0100", OrdinateID = 3948, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "ENUMERATION/CODE", ColNumber = 10, ColName = "C0110", HierarchyID = 417, IsRowKey = false, Label = "Type of underwriting model", OrdinateCode = "C0110", OrdinateID = 3949, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "MONETARY", ColNumber = 11, ColName = "C0120", HierarchyID = 0, IsRowKey = false, Label = "Amount underwriting model", OrdinateCode = "C0120", OrdinateID = 3950, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "MONETARY", ColNumber = 12, ColName = "C0130", HierarchyID = 0, IsRowKey = false, Label = "Sum reinsured on a facultative basis, with all reinsurers", OrdinateCode = "C0130", OrdinateID = 3951, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "MONETARY", ColNumber = 13, ColName = "C0140", HierarchyID = 0, IsRowKey = false, Label = "Sum reinsured, other than on facultative basis, with all reinsurers", OrdinateCode = "C0140", OrdinateID = 3952, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 539, ColType = "MONETARY", ColNumber = 14, ColName = "C0150", HierarchyID = 0, IsRowKey = false, Label = "Net retention of the insurer", OrdinateCode = "C0150", OrdinateID = 3953, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
