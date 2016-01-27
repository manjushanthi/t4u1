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
    public partial class S_01_03_01_01__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_01_03_01_01__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_01_03_01_01__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 25;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_01_03_01_01__sol2__2_0);
           DataTable = "T__S_01_03_01_01__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 56, ColType = "STRING", ColNumber = 0, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Fund /Portfolio Number", OrdinateCode = "C0040", OrdinateID = 569, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 55, ColType = "STRING", ColNumber = 1, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Name of ring-fenced fund/Matching adjustment portfolio", OrdinateCode = "C0050", OrdinateID = 564, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 55, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0060", HierarchyID = 340, IsRowKey = false, Label = "RFF/MAP/Remaining of a fund", OrdinateCode = "C0060", OrdinateID = 565, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 55, ColType = "ENUMERATION/CODE", ColNumber = 3, ColName = "C0070", HierarchyID = 341, IsRowKey = false, Label = "RFF/MAP with sub RFF/MAP", OrdinateCode = "C0070", OrdinateID = 566, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 55, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0080", HierarchyID = 27, IsRowKey = false, Label = "Material", OrdinateCode = "C0080", OrdinateID = 567, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 55, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0090", HierarchyID = 25, IsRowKey = false, Label = "Article 304", OrdinateCode = "C0090", OrdinateID = 568, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
