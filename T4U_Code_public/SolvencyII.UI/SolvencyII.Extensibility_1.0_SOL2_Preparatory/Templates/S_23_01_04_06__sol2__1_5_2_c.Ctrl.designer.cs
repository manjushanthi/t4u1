using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_23_01_04_06__sol2__1_5_2_c_ctrl 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel1 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel3 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel5 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel6 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyCurrencyTextBox7 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyTextBox8 = new SolvencyII.UI.Shared.Controls.SolvencyTextBox();
this.splitContainerColTitles = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
this.splitContainerRowTitles = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
this.spltMain = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(117,10);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 1303;
this.solvencyLabel0.Size = new System.Drawing.Size(108, 20);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Explanation" ;
this.solvencyLabel0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel1
//
this.solvencyLabel1.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel1.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel1.Location = new System.Drawing.Point(117,30);
this.solvencyLabel1.Name = "solvencyLabel1";
this.solvencyLabel1.OrdinateID_Label = 0;
this.solvencyLabel1.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel1.TabIndex = 1;
this.solvencyLabel1.Text = "PC0120" ;
this.solvencyLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel3
//
this.solvencyLabel3.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel3.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel3.Location = new System.Drawing.Point(10,30);
this.solvencyLabel3.Name = "solvencyLabel3";
this.solvencyLabel3.OrdinateID_Label = 0;
this.solvencyLabel3.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel3.TabIndex = 3;
this.solvencyLabel3.Text = "PC0130" ;
this.solvencyLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(10,3);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 1304;
this.solvencyLabel4.Size = new System.Drawing.Size(259, 15);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "Other, please explain why you need to use this line" ;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel5
//
this.solvencyLabel5.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel5.Location = new System.Drawing.Point(283,3);
this.solvencyLabel5.Name = "solvencyLabel5";
this.solvencyLabel5.OrdinateID_Label = 0;
this.solvencyLabel5.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel5.TabIndex = 5;
this.solvencyLabel5.Text = "PR0640" ;
this.solvencyLabel5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel6
//
this.solvencyLabel6.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel6.Location = new System.Drawing.Point(283,23);
this.solvencyLabel6.Name = "solvencyLabel6";
this.solvencyLabel6.OrdinateID_Label = 0;
this.solvencyLabel6.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel6.TabIndex = 6;
this.solvencyLabel6.Text = "." ;
this.solvencyLabel6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyCurrencyTextBox7
//
this.solvencyCurrencyTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox7.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox7.Location = new System.Drawing.Point(10,3);
this.solvencyCurrencyTextBox7.Name = "solvencyCurrencyTextBox7";
this.solvencyCurrencyTextBox7.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox7.TabIndex = 7;
this.solvencyCurrencyTextBox7.ColName = "PR0640PC0130";
this.solvencyCurrencyTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyTextBox8
//
this.solvencyTextBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyTextBox8.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyTextBox8.Location = new System.Drawing.Point(117,3);
this.solvencyTextBox8.Name = "solvencyTextBox8";
this.solvencyTextBox8.Size = new System.Drawing.Size(100, 13);
this.solvencyTextBox8.TabIndex = 8;
this.solvencyTextBox8.ColName = "PR0640PC0120";
this.solvencyTextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
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
this.splitContainerColTitles.Panel2.Controls.Add(this.solvencyLabel3);
this.splitContainerColTitles.Size = new System.Drawing.Size(661, 380);
this.splitContainerColTitles.SplitterDistance = 330;
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
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel4);
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel5);
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel6);
//
// splitContainerRowTitles.Panel2
//
this.splitContainerRowTitles.Panel2.Controls.Add(this.solvencyCurrencyTextBox7);
this.splitContainerRowTitles.Panel2.Controls.Add(this.solvencyTextBox8);
this.splitContainerRowTitles.Panel2.AutoScroll = true;
this.splitContainerRowTitles.Size = new System.Drawing.Size(661, 380);
this.splitContainerRowTitles.SplitterDistance = 330;
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
this.spltMain.Size = new System.Drawing.Size(661, 126);
this.spltMain.SplitterDistance = 50;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.spltMain);
            this.Name = "S_23_01_04_06__sol2__1_5_2_c_ctrl"; 
            this.Size = new System.Drawing.Size(661, 76); 
            this.Load += new System.EventHandler(this.BoundControl_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyLabel solvencyLabel1;
private SolvencyLabel solvencyLabel3;
private SolvencyLabel solvencyLabel4;
private SolvencyLabel solvencyLabel5;
private SolvencyLabel solvencyLabel6;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox7;
private SolvencyTextBox solvencyTextBox8;
private SolvencySplitContainer splitContainerColTitles;
private SolvencySplitContainer splitContainerRowTitles;
private SolvencySplitContainer spltMain;

   }
}

