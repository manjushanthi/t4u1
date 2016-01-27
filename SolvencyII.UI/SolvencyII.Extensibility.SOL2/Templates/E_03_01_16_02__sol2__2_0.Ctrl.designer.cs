using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class E_03_01_16_02__sol2__2_0_ctrl 
   { 
      private void InitializeComponent() 
      { 
this.solvencyButton0 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyLabel1 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel3 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyCurrencyTextBox5 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyButton6 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyLabel7 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyDataComboBox8 = new SolvencyII.UI.Shared.Controls.SolvencyDataComboBox();
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
this.solvencyLabel1.Location = new System.Drawing.Point(527,10);
this.solvencyLabel1.Name = "solvencyLabel1";
this.solvencyLabel1.OrdinateID_Label = 8857;
this.solvencyLabel1.Size = new System.Drawing.Size(108, 30);
this.solvencyLabel1.TabIndex = 1;
this.solvencyLabel1.Text = "Accepted reinsurance" ;
this.solvencyLabel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel2
//
this.solvencyLabel2.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel2.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel2.Location = new System.Drawing.Point(527,40);
this.solvencyLabel2.Name = "solvencyLabel2";
this.solvencyLabel2.OrdinateID_Label = 0;
this.solvencyLabel2.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "EC0020" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel3
//
this.solvencyLabel3.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel3.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel3.Location = new System.Drawing.Point(274,40);
this.solvencyLabel3.Name = "solvencyLabel3";
this.solvencyLabel3.OrdinateID_Label = 8858;
this.solvencyLabel3.Size = new System.Drawing.Size(189, 15);
this.solvencyLabel3.TabIndex = 3;
this.solvencyLabel3.Text = "Countries in the materiality threshold" ;
this.solvencyLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(477,40);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 0;
this.solvencyLabel4.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "ER0040" ;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyCurrencyTextBox5
//
this.solvencyCurrencyTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox5.Location = new System.Drawing.Point(527,60);
this.solvencyCurrencyTextBox5.Name = "solvencyCurrencyTextBox5";
this.solvencyCurrencyTextBox5.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox5.TabIndex = 5;
this.solvencyCurrencyTextBox5.ColName = "ER0040EC0020";
this.solvencyCurrencyTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyButton6
//
this.solvencyButton6.ColName = "";
this.solvencyButton6.TableNames = "";
this.solvencyButton6.Location = new System.Drawing.Point(7,57);
this.solvencyButton6.Name = "solvencyButton6";
this.solvencyButton6.Size = new System.Drawing.Size(50, 20);
this.solvencyButton6.TabIndex = 6;
this.solvencyButton6.Text = "Delete";
this.solvencyButton6.UseVisualStyleBackColor = true;
this.solvencyButton6.Click += new System.EventHandler(this.btnDel_Click);
//
// solvencyLabel7
//
this.solvencyLabel7.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel7.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel7.Location = new System.Drawing.Point(64,37);
this.solvencyLabel7.Name = "solvencyLabel7";
this.solvencyLabel7.OrdinateID_Label = 8859;
this.solvencyLabel7.Size = new System.Drawing.Size(200, 13);
this.solvencyLabel7.TabIndex = 7;
this.solvencyLabel7.Text = "Country" ;
this.solvencyLabel7.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyDataComboBox8
//
this.SolvencyDataComboBox8.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;
this.SolvencyDataComboBox8.Location = new System.Drawing.Point(64,57);
this.SolvencyDataComboBox8.Name = "SolvencyDataComboBox8";
this.SolvencyDataComboBox8.Size = new System.Drawing.Size(200, 13);
this.SolvencyDataComboBox8.TabIndex = 8;
this.SolvencyDataComboBox8.ColName = "PAGES2C_LG";
this.SolvencyDataComboBox8.AxisID = 1758;
this.SolvencyDataComboBox8.OrdinateID = 8859;
this.SolvencyDataComboBox8.StartOrder = 1;
this.SolvencyDataComboBox8.NextOrder = 100000;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
            this.AutoScroll = true;
this.Controls.Add(this.solvencyButton0);
this.Controls.Add(this.solvencyLabel1);
this.Controls.Add(this.solvencyLabel2);
this.Controls.Add(this.solvencyLabel3);
this.Controls.Add(this.solvencyLabel4);
this.Controls.Add(this.solvencyCurrencyTextBox5);
this.Controls.Add(this.solvencyButton6);
this.Controls.Add(this.solvencyLabel7);
this.Controls.Add(this.SolvencyDataComboBox8);
            this.Name = "E_03_01_16_02__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(684, 83); 
            this.Load += new System.EventHandler(this.BoundControl_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyButton solvencyButton0;
private SolvencyLabel solvencyLabel1;
private SolvencyLabel solvencyLabel2;
private SolvencyLabel solvencyLabel3;
private SolvencyLabel solvencyLabel4;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox5;
private SolvencyButton solvencyButton6;
private SolvencyLabel solvencyLabel7;
private SolvencyDataComboBox SolvencyDataComboBox8;

   }
}

