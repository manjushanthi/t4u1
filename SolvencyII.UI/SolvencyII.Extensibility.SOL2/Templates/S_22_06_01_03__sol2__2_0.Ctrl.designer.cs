using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_22_06_01_03__sol2__2_0_ctrl 
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
this.solvencyCurrencyTextBox7 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox8 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
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
this.solvencyLabel1.Location = new System.Drawing.Point(706,10);
this.solvencyLabel1.Name = "solvencyLabel1";
this.solvencyLabel1.OrdinateID_Label = 4120;
this.solvencyLabel1.Size = new System.Drawing.Size(108, 60);
this.solvencyLabel1.TabIndex = 1;
this.solvencyLabel1.Text = "Part of the Best Estimate subject to volatility adjustment written in the reporting currency" ;
this.solvencyLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel2
//
this.solvencyLabel2.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel2.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel2.Location = new System.Drawing.Point(706,70);
this.solvencyLabel2.Name = "solvencyLabel2";
this.solvencyLabel2.OrdinateID_Label = 0;
this.solvencyLabel2.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "C0040" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel3
//
this.solvencyLabel3.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel3.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel3.Location = new System.Drawing.Point(599,10);
this.solvencyLabel3.Name = "solvencyLabel3";
this.solvencyLabel3.OrdinateID_Label = 4119;
this.solvencyLabel3.Size = new System.Drawing.Size(108, 60);
this.solvencyLabel3.TabIndex = 3;
this.solvencyLabel3.Text = "Total value of Best Estimate subject to volatility adjustment (for all currencies)" ;
this.solvencyLabel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(599,70);
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
this.solvencyLabel5.Location = new System.Drawing.Point(274,70);
this.solvencyLabel5.Name = "solvencyLabel5";
this.solvencyLabel5.OrdinateID_Label = 4121;
this.solvencyLabel5.Size = new System.Drawing.Size(261, 30);
this.solvencyLabel5.TabIndex = 5;
this.solvencyLabel5.Text = "Total value of Best Estimate subject to volatility adjustment in countries other than home country" ;
this.solvencyLabel5.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel6
//
this.solvencyLabel6.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel6.Location = new System.Drawing.Point(549,70);
this.solvencyLabel6.Name = "solvencyLabel6";
this.solvencyLabel6.OrdinateID_Label = 0;
this.solvencyLabel6.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel6.TabIndex = 6;
this.solvencyLabel6.Text = "R0040" ;
this.solvencyLabel6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyCurrencyTextBox7
//
this.solvencyCurrencyTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox7.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox7.Location = new System.Drawing.Point(599,90);
this.solvencyCurrencyTextBox7.Name = "solvencyCurrencyTextBox7";
this.solvencyCurrencyTextBox7.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox7.TabIndex = 7;
this.solvencyCurrencyTextBox7.ColName = "R0040C0030";
this.solvencyCurrencyTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox8
//
this.solvencyCurrencyTextBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox8.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox8.Location = new System.Drawing.Point(706,90);
this.solvencyCurrencyTextBox8.Name = "solvencyCurrencyTextBox8";
this.solvencyCurrencyTextBox8.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox8.TabIndex = 8;
this.solvencyCurrencyTextBox8.ColName = "R0040C0040";
this.solvencyCurrencyTextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyButton9
//
this.solvencyButton9.ColName = "";
this.solvencyButton9.TableNames = "";
this.solvencyButton9.Location = new System.Drawing.Point(7,87);
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
this.solvencyLabel10.Location = new System.Drawing.Point(64,67);
this.solvencyLabel10.Name = "solvencyLabel10";
this.solvencyLabel10.OrdinateID_Label = 4122;
this.solvencyLabel10.Size = new System.Drawing.Size(200, 13);
this.solvencyLabel10.TabIndex = 10;
this.solvencyLabel10.Text = "Other than home country" ;
this.solvencyLabel10.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyDataComboBox11
//
this.SolvencyDataComboBox11.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;
this.SolvencyDataComboBox11.Location = new System.Drawing.Point(64,87);
this.SolvencyDataComboBox11.Name = "SolvencyDataComboBox11";
this.SolvencyDataComboBox11.Size = new System.Drawing.Size(200, 13);
this.SolvencyDataComboBox11.TabIndex = 11;
this.SolvencyDataComboBox11.ColName = "PAGES2C_LG";
this.SolvencyDataComboBox11.AxisID = 568;
this.SolvencyDataComboBox11.OrdinateID = 4122;
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
this.Controls.Add(this.solvencyCurrencyTextBox7);
this.Controls.Add(this.solvencyCurrencyTextBox8);
this.Controls.Add(this.solvencyButton9);
this.Controls.Add(this.solvencyLabel10);
this.Controls.Add(this.SolvencyDataComboBox11);
            this.Name = "S_22_06_01_03__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(863, 113); 
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
private SolvencyCurrencyTextBox solvencyCurrencyTextBox7;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox8;
private SolvencyButton solvencyButton9;
private SolvencyLabel solvencyLabel10;
private SolvencyDataComboBox SolvencyDataComboBox11;

   }
}

