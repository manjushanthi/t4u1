using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class C46_00_c__COREP2014_07__2_0_3_ctrl 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel1 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel3 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyCurrencyTextBox5 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.splitContainerColTitles = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
this.splitContainerRowTitles = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
this.spltMain = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(10,10);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 23659;
this.solvencyLabel0.Size = new System.Drawing.Size(108, 20);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Commercial entities" ;
this.solvencyLabel0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel1
//
this.solvencyLabel1.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel1.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel1.Location = new System.Drawing.Point(10,30);
this.solvencyLabel1.Name = "solvencyLabel1";
this.solvencyLabel1.OrdinateID_Label = 0;
this.solvencyLabel1.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel1.TabIndex = 1;
this.solvencyLabel1.Text = "030" ;
this.solvencyLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel2
//
this.solvencyLabel2.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel2.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel2.Location = new System.Drawing.Point(10,3);
this.solvencyLabel2.Name = "solvencyLabel2";
this.solvencyLabel2.OrdinateID_Label = 23660;
this.solvencyLabel2.Size = new System.Drawing.Size(261, 45);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "(memo item) Accounting assets of the entities that are not considered in fields {LR6, 010, 003} to {LR6, 120, 003}" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel3
//
this.solvencyLabel3.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel3.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel3.Location = new System.Drawing.Point(285,3);
this.solvencyLabel3.Name = "solvencyLabel3";
this.solvencyLabel3.OrdinateID_Label = 0;
this.solvencyLabel3.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel3.TabIndex = 3;
this.solvencyLabel3.Text = "170" ;
this.solvencyLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(285,23);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 0;
this.solvencyLabel4.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "." ;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyCurrencyTextBox5
//
this.solvencyCurrencyTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox5.Location = new System.Drawing.Point(10,3);
this.solvencyCurrencyTextBox5.Name = "solvencyCurrencyTextBox5";
this.solvencyCurrencyTextBox5.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox5.TabIndex = 5;
this.solvencyCurrencyTextBox5.ColName = "R170C030";
this.solvencyCurrencyTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// splitContainerColTitles
//
this.splitContainerColTitles.Dock = System.Windows.Forms.DockStyle.Fill;
this.splitContainerColTitles.Location = new System.Drawing.Point(0,0);
this.splitContainerColTitles.Name = "splitContainerColTitles";
this.splitContainerColTitles.Orientation = System.Windows.Forms.Orientation.Vertical;
this.splitContainerColTitles.Panel1MinSize = 0;
//
// splitContainerColTitles.Panel1
//
//
// splitContainerColTitles.Panel2
//
this.splitContainerColTitles.Panel2.Controls.Add(this.solvencyLabel0);
this.splitContainerColTitles.Panel2.Controls.Add(this.solvencyLabel1);
this.splitContainerColTitles.Size = new System.Drawing.Size(556, 382);
this.splitContainerColTitles.SplitterDistance = 332;
//
// splitContainerRowTitles
//
this.splitContainerRowTitles.Dock = System.Windows.Forms.DockStyle.Fill;
this.splitContainerRowTitles.Location = new System.Drawing.Point(0,0);
this.splitContainerRowTitles.Name = "splitContainerRowTitles";
this.splitContainerRowTitles.Orientation = System.Windows.Forms.Orientation.Vertical;
this.splitContainerRowTitles.Panel1MinSize = 0;
//
// splitContainerRowTitles.Panel1
//
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel2);
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel3);
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel4);
//
// splitContainerRowTitles.Panel2
//
this.splitContainerRowTitles.Panel2.Controls.Add(this.solvencyCurrencyTextBox5);
this.splitContainerRowTitles.Panel2.AutoScroll = true;
this.splitContainerRowTitles.Size = new System.Drawing.Size(556, 382);
this.splitContainerRowTitles.SplitterDistance = 332;
//
// spltMain
//
this.spltMain.Dock = System.Windows.Forms.DockStyle.Fill;
this.spltMain.Location = new System.Drawing.Point(0,0);
this.spltMain.Name = "spltMain";
this.spltMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
this.spltMain.Panel1MinSize = 0;
//
// spltMain.Panel1
//
this.spltMain.Panel1.Controls.Add(this.splitContainerColTitles);
//
// spltMain.Panel2
//
this.spltMain.Panel2.Controls.Add(this.splitContainerRowTitles);
this.spltMain.Panel2.AutoScroll = true;
this.spltMain.Size = new System.Drawing.Size(167, 126);
this.spltMain.SplitterDistance = 50;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.spltMain);
            this.Name = "C46_00_c__COREP2014_07__2_0_3_ctrl"; 
            this.Size = new System.Drawing.Size(167, 76); 
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyLabel solvencyLabel1;
private SolvencyLabel solvencyLabel2;
private SolvencyLabel solvencyLabel3;
private SolvencyLabel solvencyLabel4;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox5;
private SolvencySplitContainer splitContainerColTitles;
private SolvencySplitContainer splitContainerRowTitles;
private SolvencySplitContainer spltMain;

   }
}

