using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_01_02_07_03__sol2__2_0_ctrl 
   { 
      private void InitializeComponent() 
      { 
this.solvencyButton0 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyLabel1 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel3 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel5 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel6 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyTextBox7 = new SolvencyII.UI.Shared.Controls.SolvencyTextBox();
this.SolvencyDataComboBox8 = new SolvencyII.UI.Shared.Controls.SolvencyDataComboBox();
this.solvencyButton9 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyLabel10 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyDataComboBox11 = new SolvencyII.UI.Shared.Controls.SolvencyDataComboBox();
            this.SuspendLayout(); 

//
// solvencyButton0
//
this.solvencyButton0.ColName = "";
this.solvencyButton0.TableNames = "";
this.solvencyButton0.Location = new System.Drawing.Point(7,10);
this.solvencyButton0.Name = "solvencyButton0";
this.solvencyButton0.Size = new System.Drawing.Size(50, 20);
this.solvencyButton0.TabIndex = 0;
this.solvencyButton0.Text = "Add";
this.solvencyButton0.UseVisualStyleBackColor = true;
this.solvencyButton0.Click += new System.EventHandler(this.btnAdd_Click);
//
// solvencyLabel1
//
this.solvencyLabel1.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel1.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel1.Location = new System.Drawing.Point(469,10);
this.solvencyLabel1.Name = "solvencyLabel1";
this.solvencyLabel1.OrdinateID_Label = 562;
this.solvencyLabel1.Size = new System.Drawing.Size(208, 20);
this.solvencyLabel1.TabIndex = 1;
this.solvencyLabel1.Text = "Country of a branch" ;
this.solvencyLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel2
//
this.solvencyLabel2.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel2.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel2.Location = new System.Drawing.Point(469,30);
this.solvencyLabel2.Name = "solvencyLabel2";
this.solvencyLabel2.OrdinateID_Label = 0;
this.solvencyLabel2.Size = new System.Drawing.Size(208, 13);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "C0040" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel3
//
this.solvencyLabel3.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel3.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel3.Location = new System.Drawing.Point(362,10);
this.solvencyLabel3.Name = "solvencyLabel3";
this.solvencyLabel3.OrdinateID_Label = 561;
this.solvencyLabel3.Size = new System.Drawing.Size(108, 20);
this.solvencyLabel3.TabIndex = 3;
this.solvencyLabel3.Text = "Name of a branch" ;
this.solvencyLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(362,30);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 0;
this.solvencyLabel4.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "C0030" ;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel5
//
this.solvencyLabel5.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel5.Location = new System.Drawing.Point(274,30);
this.solvencyLabel5.Name = "solvencyLabel5";
this.solvencyLabel5.OrdinateID_Label = 8935;
this.solvencyLabel5.Size = new System.Drawing.Size(24, 15);
this.solvencyLabel5.TabIndex = 5;
this.solvencyLabel5.Text = "" ;
this.solvencyLabel5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel6
//
this.solvencyLabel6.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel6.Location = new System.Drawing.Point(312,30);
this.solvencyLabel6.Name = "solvencyLabel6";
this.solvencyLabel6.OrdinateID_Label = 0;
this.solvencyLabel6.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel6.TabIndex = 6;
this.solvencyLabel6.Text = "R0000" ;
this.solvencyLabel6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyTextBox7
//
this.solvencyTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyTextBox7.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyTextBox7.Location = new System.Drawing.Point(362,50);
this.solvencyTextBox7.Name = "solvencyTextBox7";
this.solvencyTextBox7.Size = new System.Drawing.Size(100, 13);
this.solvencyTextBox7.TabIndex = 7;
this.solvencyTextBox7.ColName = "R0000C0030";
this.solvencyTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
//
// SolvencyDataComboBox8
//
this.SolvencyDataComboBox8.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;
this.SolvencyDataComboBox8.Location = new System.Drawing.Point(469,50);
this.SolvencyDataComboBox8.Name = "SolvencyDataComboBox8";
this.SolvencyDataComboBox8.Size = new System.Drawing.Size(200, 13);
this.SolvencyDataComboBox8.TabIndex = 8;
this.SolvencyDataComboBox8.ColName = "R0000C0040";
this.SolvencyDataComboBox8.AxisID = 53;
this.SolvencyDataComboBox8.OrdinateID = 562;
this.SolvencyDataComboBox8.StartOrder = 0;
this.SolvencyDataComboBox8.NextOrder = 0;
//
// solvencyButton9
//
this.solvencyButton9.ColName = "";
this.solvencyButton9.TableNames = "";
this.solvencyButton9.Location = new System.Drawing.Point(7,47);
this.solvencyButton9.Name = "solvencyButton9";
this.solvencyButton9.Size = new System.Drawing.Size(50, 20);
this.solvencyButton9.TabIndex = 9;
this.solvencyButton9.Text = "Delete";
this.solvencyButton9.UseVisualStyleBackColor = true;
this.solvencyButton9.Click += new System.EventHandler(this.btnDel_Click);
//
// solvencyLabel10
//
this.solvencyLabel10.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel10.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel10.Location = new System.Drawing.Point(64,27);
this.solvencyLabel10.Name = "solvencyLabel10";
this.solvencyLabel10.OrdinateID_Label = 563;
this.solvencyLabel10.Size = new System.Drawing.Size(200, 13);
this.solvencyLabel10.TabIndex = 10;
this.solvencyLabel10.Text = "Country" ;
this.solvencyLabel10.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyDataComboBox11
//
this.SolvencyDataComboBox11.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;
this.SolvencyDataComboBox11.Location = new System.Drawing.Point(64,47);
this.SolvencyDataComboBox11.Name = "SolvencyDataComboBox11";
this.SolvencyDataComboBox11.Size = new System.Drawing.Size(200, 13);
this.SolvencyDataComboBox11.TabIndex = 11;
this.SolvencyDataComboBox11.ColName = "PAGES2C_LG";
this.SolvencyDataComboBox11.AxisID = 54;
this.SolvencyDataComboBox11.OrdinateID = 563;
this.SolvencyDataComboBox11.StartOrder = 1;
this.SolvencyDataComboBox11.NextOrder = 100000;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
            this.AutoScroll = true;
this.Controls.Add(this.solvencyButton0);
this.Controls.Add(this.solvencyLabel1);
this.Controls.Add(this.solvencyLabel2);
this.Controls.Add(this.solvencyLabel3);
this.Controls.Add(this.solvencyLabel4);
this.Controls.Add(this.solvencyLabel5);
this.Controls.Add(this.solvencyLabel6);
this.Controls.Add(this.solvencyTextBox7);
this.Controls.Add(this.SolvencyDataComboBox8);
this.Controls.Add(this.solvencyButton9);
this.Controls.Add(this.solvencyLabel10);
this.Controls.Add(this.SolvencyDataComboBox11);
            this.Name = "S_01_02_07_03__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(726, 73); 
            this.Load += new System.EventHandler(this.BoundControl_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyButton solvencyButton0;
private SolvencyLabel solvencyLabel1;
private SolvencyLabel solvencyLabel2;
private SolvencyLabel solvencyLabel3;
private SolvencyLabel solvencyLabel4;
private SolvencyLabel solvencyLabel5;
private SolvencyLabel solvencyLabel6;
private SolvencyTextBox solvencyTextBox7;
private SolvencyDataComboBox SolvencyDataComboBox8;
private SolvencyButton solvencyButton9;
private SolvencyLabel solvencyLabel10;
private SolvencyDataComboBox SolvencyDataComboBox11;

   }
}

