using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class E_03_01_16_01__sol2__2_0_ctrl 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel1 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel3 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel5 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel6 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel7 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel8 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyCurrencyTextBox9 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox10 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox11 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
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
this.solvencyLabel0.OrdinateID_Label = 8853;
this.solvencyLabel0.Size = new System.Drawing.Size(108, 30);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Accepted reinsurance" ;
this.solvencyLabel0.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel1
//
this.solvencyLabel1.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel1.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel1.Location = new System.Drawing.Point(10,40);
this.solvencyLabel1.Name = "solvencyLabel1";
this.solvencyLabel1.OrdinateID_Label = 0;
this.solvencyLabel1.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel1.TabIndex = 1;
this.solvencyLabel1.Text = "EC0020" ;
this.solvencyLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel2
//
this.solvencyLabel2.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel2.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel2.Location = new System.Drawing.Point(10,3);
this.solvencyLabel2.Name = "solvencyLabel2";
this.solvencyLabel2.OrdinateID_Label = 8854;
this.solvencyLabel2.Size = new System.Drawing.Size(261, 15);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "Home country" ;
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
this.solvencyLabel3.Text = "ER0010" ;
this.solvencyLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(10,23);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 8855;
this.solvencyLabel4.Size = new System.Drawing.Size(261, 30);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "EEA countries outside the materiality threshold - not reported by country" ;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel5
//
this.solvencyLabel5.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel5.Location = new System.Drawing.Point(285,23);
this.solvencyLabel5.Name = "solvencyLabel5";
this.solvencyLabel5.OrdinateID_Label = 0;
this.solvencyLabel5.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel5.TabIndex = 5;
this.solvencyLabel5.Text = "ER0020" ;
this.solvencyLabel5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel6
//
this.solvencyLabel6.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel6.Location = new System.Drawing.Point(10,56);
this.solvencyLabel6.Name = "solvencyLabel6";
this.solvencyLabel6.OrdinateID_Label = 8856;
this.solvencyLabel6.Size = new System.Drawing.Size(261, 30);
this.solvencyLabel6.TabIndex = 6;
this.solvencyLabel6.Text = "Non-EEA countries outside the materiality threshold - not reported by country" ;
this.solvencyLabel6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel7
//
this.solvencyLabel7.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel7.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel7.Location = new System.Drawing.Point(285,56);
this.solvencyLabel7.Name = "solvencyLabel7";
this.solvencyLabel7.OrdinateID_Label = 0;
this.solvencyLabel7.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel7.TabIndex = 7;
this.solvencyLabel7.Text = "ER0030" ;
this.solvencyLabel7.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel8
//
this.solvencyLabel8.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel8.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel8.Location = new System.Drawing.Point(285,76);
this.solvencyLabel8.Name = "solvencyLabel8";
this.solvencyLabel8.OrdinateID_Label = 0;
this.solvencyLabel8.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel8.TabIndex = 8;
this.solvencyLabel8.Text = "." ;
this.solvencyLabel8.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyCurrencyTextBox9
//
this.solvencyCurrencyTextBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox9.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox9.Location = new System.Drawing.Point(10,3);
this.solvencyCurrencyTextBox9.Name = "solvencyCurrencyTextBox9";
this.solvencyCurrencyTextBox9.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox9.TabIndex = 9;
this.solvencyCurrencyTextBox9.ColName = "ER0010EC0020";
this.solvencyCurrencyTextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox10
//
this.solvencyCurrencyTextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox10.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox10.Location = new System.Drawing.Point(10,23);
this.solvencyCurrencyTextBox10.Name = "solvencyCurrencyTextBox10";
this.solvencyCurrencyTextBox10.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox10.TabIndex = 10;
this.solvencyCurrencyTextBox10.ColName = "ER0020EC0020";
this.solvencyCurrencyTextBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox11
//
this.solvencyCurrencyTextBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox11.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox11.Location = new System.Drawing.Point(10,56);
this.solvencyCurrencyTextBox11.Name = "solvencyCurrencyTextBox11";
this.solvencyCurrencyTextBox11.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox11.TabIndex = 11;
this.solvencyCurrencyTextBox11.ColName = "ER0030EC0020";
this.solvencyCurrencyTextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
this.splitContainerColTitles.Size = new System.Drawing.Size(556, 392);
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
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel5);
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel6);
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel7);
this.splitContainerRowTitles.Panel1.Controls.Add(this.solvencyLabel8);
//
// splitContainerRowTitles.Panel2
//
this.splitContainerRowTitles.Panel2.Controls.Add(this.solvencyCurrencyTextBox9);
this.splitContainerRowTitles.Panel2.Controls.Add(this.solvencyCurrencyTextBox10);
this.splitContainerRowTitles.Panel2.Controls.Add(this.solvencyCurrencyTextBox11);
this.splitContainerRowTitles.Panel2.AutoScroll = true;
this.splitContainerRowTitles.Size = new System.Drawing.Size(556, 392);
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
this.spltMain.Size = new System.Drawing.Size(556, 199);
this.spltMain.SplitterDistance = 60;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.spltMain);
            this.Name = "E_03_01_16_01__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(556, 139); 
            this.Load += new System.EventHandler(this.BoundControl_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyLabel solvencyLabel1;
private SolvencyLabel solvencyLabel2;
private SolvencyLabel solvencyLabel3;
private SolvencyLabel solvencyLabel4;
private SolvencyLabel solvencyLabel5;
private SolvencyLabel solvencyLabel6;
private SolvencyLabel solvencyLabel7;
private SolvencyLabel solvencyLabel8;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox9;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox10;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox11;
private SolvencySplitContainer splitContainerColTitles;
private SolvencySplitContainer splitContainerRowTitles;
private SolvencySplitContainer spltMain;

   }
}

