using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_22_06_01_02__sol2__2_0_ctrl 
   { 
      private void InitializeComponent() 
      { 
this.solvencyButton0 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyButton1 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyDataComboBox3 = new SolvencyII.UI.Shared.Controls.SolvencyDataComboBox();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel5 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyCurrencyTextBox6 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox7 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
            this.SuspendLayout(); 

//
// solvencyButton0
//
this.solvencyButton0.ColName = "";
this.solvencyButton0.TableNames = "";
this.solvencyButton0.Location = new System.Drawing.Point(17,10);
this.solvencyButton0.Name = "solvencyButton0";
this.solvencyButton0.Size = new System.Drawing.Size(50, 20);
this.solvencyButton0.TabIndex = 0;
this.solvencyButton0.Text = "Delete";
this.solvencyButton0.UseVisualStyleBackColor = true;
this.solvencyButton0.Click += new System.EventHandler(this.btnDel_Click);
//
// solvencyButton1
//
this.solvencyButton1.ColName = "";
this.solvencyButton1.TableNames = "";
this.solvencyButton1.Location = new System.Drawing.Point(68,10);
this.solvencyButton1.Name = "solvencyButton1";
this.solvencyButton1.Size = new System.Drawing.Size(50, 20);
this.solvencyButton1.TabIndex = 1;
this.solvencyButton1.Text = "Add";
this.solvencyButton1.UseVisualStyleBackColor = true;
this.solvencyButton1.Click += new System.EventHandler(this.btnAdd_Click);
//
// solvencyLabel2
//
this.solvencyLabel2.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel2.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel2.Location = new System.Drawing.Point(10,37);
this.solvencyLabel2.Name = "solvencyLabel2";
this.solvencyLabel2.OrdinateID_Label = 4114;
this.solvencyLabel2.Size = new System.Drawing.Size(100, 13);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "Other than reporting currency" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyDataComboBox3
//
this.SolvencyDataComboBox3.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;
this.SolvencyDataComboBox3.Location = new System.Drawing.Point(10,57);
this.SolvencyDataComboBox3.Name = "SolvencyDataComboBox3";
this.SolvencyDataComboBox3.Size = new System.Drawing.Size(100, 13);
this.SolvencyDataComboBox3.TabIndex = 3;
this.SolvencyDataComboBox3.ColName = "PAGES2C_OC";
this.SolvencyDataComboBox3.AxisID = 562;
this.SolvencyDataComboBox3.OrdinateID = 4114;
this.SolvencyDataComboBox3.StartOrder = 1;
this.SolvencyDataComboBox3.NextOrder = 100000;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(10,77);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 4113;
this.solvencyLabel4.Size = new System.Drawing.Size(108, 60);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "Part of the Best Estimate subject to volatility adjustment written in currencies" ;
this.solvencyLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel5
//
this.solvencyLabel5.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel5.Location = new System.Drawing.Point(10,137);
this.solvencyLabel5.Name = "solvencyLabel5";
this.solvencyLabel5.OrdinateID_Label = 0;
this.solvencyLabel5.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel5.TabIndex = 5;
this.solvencyLabel5.Text = "C0050" ;
this.solvencyLabel5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyCurrencyTextBox6
//
this.solvencyCurrencyTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox6.Location = new System.Drawing.Point(10,157);
this.solvencyCurrencyTextBox6.Name = "solvencyCurrencyTextBox6";
this.solvencyCurrencyTextBox6.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox6.TabIndex = 6;
this.solvencyCurrencyTextBox6.ColName = "R0020C0050";
this.solvencyCurrencyTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox7
//
this.solvencyCurrencyTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox7.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox7.Location = new System.Drawing.Point(10,190);
this.solvencyCurrencyTextBox7.Name = "solvencyCurrencyTextBox7";
this.solvencyCurrencyTextBox7.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox7.TabIndex = 7;
this.solvencyCurrencyTextBox7.ColName = "R0030C0050";
this.solvencyCurrencyTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.solvencyButton0);
this.Controls.Add(this.solvencyButton1);
this.Controls.Add(this.solvencyLabel2);
this.Controls.Add(this.SolvencyDataComboBox3);
this.Controls.Add(this.solvencyLabel4);
this.Controls.Add(this.solvencyLabel5);
this.Controls.Add(this.solvencyCurrencyTextBox6);
this.Controls.Add(this.solvencyCurrencyTextBox7);
            this.Name = "S_22_06_01_02__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(167, 213); 
            this.Load += new System.EventHandler(this.BoundControl_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyButton solvencyButton0;
private SolvencyButton solvencyButton1;
private SolvencyLabel solvencyLabel2;
private SolvencyDataComboBox SolvencyDataComboBox3;
private SolvencyLabel solvencyLabel4;
private SolvencyLabel solvencyLabel5;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox6;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox7;

   }
}

