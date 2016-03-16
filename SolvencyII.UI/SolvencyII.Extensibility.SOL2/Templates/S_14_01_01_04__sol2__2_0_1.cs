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
    public partial class S_14_01_01_04__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_14_01_01_04__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_14_01_01_04__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 112;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_14_01_01_04__sol2__2_0_1);
           DataTable = "T__S_14_01_01_04__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 344, ColType = "STRING", ColNumber = 0, ColName = "C0220", HierarchyID = 0, IsRowKey = false, Label = "Product ID code", OrdinateCode = "C0220", OrdinateID = 2908, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 345, ColType = "STRING", ColNumber = 1, ColName = "C0230", HierarchyID = 0, IsRowKey = false, Label = "HRG code", OrdinateCode = "C0230", OrdinateID = 2909, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 343, ColType = "TRUE", ColNumber = 2, ColName = "C0250", HierarchyID = 0, IsRowKey = false, Label = "Link", OrdinateCode = "C0250", OrdinateID = 2907, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
