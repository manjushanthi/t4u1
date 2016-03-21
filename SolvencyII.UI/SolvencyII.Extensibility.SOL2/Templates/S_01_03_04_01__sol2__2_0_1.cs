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
    public partial class S_01_03_04_01__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_01_03_04_01__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_01_03_04_01__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 30;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_01_03_04_01__sol2__2_0_1);
           DataTable = "T__S_01_03_04_01__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 70, ColType = "STRING", ColNumber = 0, ColName = "C0020", HierarchyID = 0, IsRowKey = false, Label = "Identification code", OrdinateCode = "C0020", OrdinateID = 612, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 71, ColType = "STRING", ColNumber = 1, ColName = "C0040", HierarchyID = 0, IsRowKey = false, Label = "Fund /Portfolio Number", OrdinateCode = "C0040", OrdinateID = 613, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 69, ColType = "STRING", ColNumber = 2, ColName = "C0010", HierarchyID = 0, IsRowKey = false, Label = "Legal name of the undertaking", OrdinateCode = "C0010", OrdinateID = 606, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 69, ColType = "STRING", ColNumber = 3, ColName = "C0050", HierarchyID = 0, IsRowKey = false, Label = "Name of ring-fenced fund/Matching adjustment portfolio", OrdinateCode = "C0050", OrdinateID = 607, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 69, ColType = "ENUMERATION/CODE", ColNumber = 4, ColName = "C0060", HierarchyID = 362, IsRowKey = false, Label = "RFF/MAP/Remaining part of a fund", OrdinateCode = "C0060", OrdinateID = 608, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 69, ColType = "ENUMERATION/CODE", ColNumber = 5, ColName = "C0070", HierarchyID = 363, IsRowKey = false, Label = "RFF/MAP with sub RFF/MAP", OrdinateCode = "C0070", OrdinateID = 609, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 69, ColType = "ENUMERATION/CODE", ColNumber = 6, ColName = "C0080", HierarchyID = 27, IsRowKey = false, Label = "Material", OrdinateCode = "C0080", OrdinateID = 610, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 69, ColType = "ENUMERATION/CODE", ColNumber = 7, ColName = "C0090", HierarchyID = 25, IsRowKey = false, Label = "Article 304", OrdinateCode = "C0090", OrdinateID = 611, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
