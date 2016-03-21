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
    public partial class S_23_04_04_10__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_23_04_04_10__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_23_04_04_10__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 211;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_23_04_04_10__sol2__2_0_1);
           DataTable = "T__S_23_04_04_10__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 728, ColType = "STRING", ColNumber = 0, ColName = "C0715", HierarchyID = 0, IsRowKey = false, Label = "Line identification", OrdinateCode = "C0715", OrdinateID = 4814, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "STRING", ColNumber = 1, ColName = "C0720", HierarchyID = 0, IsRowKey = false, Label = "Related (Re)insurance undertakings, Insurance Holding Company, Mixed financial Holding Company, ancillary entities and SPV included in the scope of the group calculation", OrdinateCode = "C0720", OrdinateID = 4800, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "ENUMERATION/CODE", ColNumber = 2, ColName = "C0730", HierarchyID = 223, IsRowKey = false, Label = "Country", OrdinateCode = "C0730", OrdinateID = 4801, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 3, ColName = "C0740", HierarchyID = 0, IsRowKey = false, Label = "Contribution of solo SCR to Group SCR", OrdinateCode = "C0740", OrdinateID = 4802, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 4, ColName = "C0750", HierarchyID = 0, IsRowKey = false, Label = "Non available minority interests", OrdinateCode = "C0750", OrdinateID = 4803, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 5, ColName = "C0760", HierarchyID = 0, IsRowKey = false, Label = "Non available own funds related to other own funds items approved by supervisory authority", OrdinateCode = "C0760", OrdinateID = 4804, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 6, ColName = "C0770", HierarchyID = 0, IsRowKey = false, Label = "Non available surplus funds", OrdinateCode = "C0770", OrdinateID = 4805, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 7, ColName = "C0780", HierarchyID = 0, IsRowKey = false, Label = "Non available called but not paid in capital", OrdinateCode = "C0780", OrdinateID = 4806, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 8, ColName = "C0790", HierarchyID = 0, IsRowKey = false, Label = "Non available ancillary own funds", OrdinateCode = "C0790", OrdinateID = 4807, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 9, ColName = "C0800", HierarchyID = 0, IsRowKey = false, Label = "Non available subordinated mutual member accounts", OrdinateCode = "C0800", OrdinateID = 4808, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 10, ColName = "C0810", HierarchyID = 0, IsRowKey = false, Label = "Non available preference shares", OrdinateCode = "C0810", OrdinateID = 4809, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 11, ColName = "C0820", HierarchyID = 0, IsRowKey = false, Label = "Non available Subordinated Liabilites", OrdinateCode = "C0820", OrdinateID = 4810, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 12, ColName = "C0830", HierarchyID = 0, IsRowKey = false, Label = "The amount equal to the value of net deferred tax assets not available at the group level", OrdinateCode = "C0830", OrdinateID = 4811, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 13, ColName = "C0840", HierarchyID = 0, IsRowKey = false, Label = "Non available share premium account related to preference shares at group level", OrdinateCode = "C0840", OrdinateID = 4812, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 727, ColType = "MONETARY", ColNumber = 14, ColName = "C0850", HierarchyID = 0, IsRowKey = false, Label = "Total non available excess own funds", OrdinateCode = "C0850", OrdinateID = 4813, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
