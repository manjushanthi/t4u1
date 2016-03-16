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
    public partial class SPV_03_01_20_02__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new SPV_03_01_20_02__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public SPV_03_01_20_02__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 475;
           FrameworkCode = "s2md";
           DataType = typeof(T__SPV_03_01_20_02__sol2__2_0_1);
           DataTable = "T__SPV_03_01_20_02__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1785, ColType = "STRING", ColNumber = 0, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Arrangement", OrdinateCode = "C0010", OrdinateID = 8919, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1784, ColType = "DATE", ColNumber = 1, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Date of issuance", OrdinateCode = "C0020", OrdinateID = 8911, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1784, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0030", HierarchyID = 32, IsRowKey = false, Label = "Issues / uses commenced prior to implementation of Directive 2009/138/EC", OrdinateCode = "C0030", OrdinateID = 8912, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1784, ColType = "STRING", ColNumber = 3, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Name of cedant", OrdinateCode = "C0040", OrdinateID = 8913, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1784, ColType = "STRING", ColNumber = 4, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Cedant code", OrdinateCode = "C0050", OrdinateID = 8914, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1784, ColType = "MONETARY", ColNumber = 5, ColName = "C0070", HierarchyID = 0, IsRowKey = false, Label = "Aggregate maximum risk exposure per arrangement", OrdinateCode = "C0070", OrdinateID = 8915, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1784, ColType = "MONETARY", ColNumber = 6, ColName = "C0080", HierarchyID = 0, IsRowKey = false, Label = "Assets held for separable risk", OrdinateCode = "C0080", OrdinateID = 8916, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1784, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0090", HierarchyID = 31, IsRowKey = false, Label = "Compliance with the fully funded requirement for the arrangement throughout the reporting period", OrdinateCode = "C0090", OrdinateID = 8917, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1784, ColType = "DECIMAL", ColNumber = 8, ColName = "C0100", HierarchyID = 0, IsRowKey = false, Label = "Duration", OrdinateCode = "C0100", OrdinateID = 8918, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
