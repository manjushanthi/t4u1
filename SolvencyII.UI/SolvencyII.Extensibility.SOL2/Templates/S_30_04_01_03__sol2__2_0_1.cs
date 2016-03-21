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
    public partial class S_30_04_01_03__sol2__2_0_1 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_04_01_03__sol2__2_0_1();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_04_01_03__sol2__2_0_1()
       {
           InitializeComponent();
           Version = 1;
           TableVID = 438;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_04_01_03__sol2__2_0_1);
           DataTable = "T__S_30_04_01_03__sol2__2_0_1";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1671, ColType = "STRING", ColNumber = 0, ColName = "C0270", HierarchyID = 0, IsRowKey = false, Label = "Code broker", OrdinateCode = "C0270", OrdinateID = 8525, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1670, ColType = "STRING", ColNumber = 1, ColName = "C0290", HierarchyID = 0, IsRowKey = false, Label = "Legal name broker", OrdinateCode = "C0290", OrdinateID = 8524, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 
