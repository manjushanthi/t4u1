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
    public partial class S_30_02_01_04__sol2__2_0 : OpenUserControlBase2, ISolvencyOpenUserControl 
    { 
       // UserControlGenerator version: 2015.4.15.1
       public ISolvencyOpenUserControl Create {get{return new S_30_02_01_04__sol2__2_0();}}
       public double Version { get; private set; }
       public int TableVID { get; private set; }
       public string FrameworkCode { get; private set; }
       public int VersionT4U { get { return 2; } }
       public Type DataType { get; private set; }
       public string DataTable { get; private set; }
       public List<ISolvencyCollectionMember> Columns { get; set; } 
       public int GridTop { get; private set; } 

       public S_30_02_01_04__sol2__2_0()
       {
           InitializeComponent();
           Version = 1.0;
           TableVID = 431;
           FrameworkCode = "s2md";
           DataType = typeof(T__S_30_02_01_04__sol2__2_0);
           DataTable = "T__S_30_02_01_04__sol2__2_0";
           GridTop = 10;
           SetupColumns();
       }
       private void SetupColumns()
       {
           Columns = new List<ISolvencyCollectionMember>();
           Columns.Add(new OpenColInfo2 {AxisID = 1649, ColType = "STRING", ColNumber = 0, ColName = "C0370", HierarchyID = 0, IsRowKey = false, Label = "Code broker", OrdinateCode = "C0370", OrdinateID = 8452, StartOrder = 1, NextOrder = 100000  });
           Columns.Add(new OpenColInfo2 {AxisID = 1648, ColType = "STRING", ColNumber = 1, ColName = "C0390", HierarchyID = 0, IsRowKey = false, Label = "Legal name broker", OrdinateCode = "C0390", OrdinateID = 8451, StartOrder = 1, NextOrder = 100000  });
       }
       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); } 
       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } 
   } 
} 