using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_04_02_01_02__sol2__2_0_ctrl 
   { 
      private void InitializeComponent() 
      { 
this.solvencyButton0 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyButton1 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyDataComboBox3 = new SolvencyII.UI.Shared.Controls.SolvencyDataComboBox();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel5 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel6 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel7 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel8 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyCurrencyTextBox9 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox10 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox11 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox12 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
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
this.solvencyLabel2.OrdinateID_Label = 1666;
this.solvencyLabel2.Size = new System.Drawing.Size(207, 13);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "EEA member" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyDataComboBox3
//
this.SolvencyDataComboBox3.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;
this.SolvencyDataComboBox3.Location = new System.Drawing.Point(10,57);
this.SolvencyDataComboBox3.Name = "SolvencyDataComboBox3";
this.SolvencyDataComboBox3.Size = new System.Drawing.Size(207, 13);
this.SolvencyDataComboBox3.TabIndex = 3;
this.SolvencyDataComboBox3.ColName = "PAGES2C_LG";
this.SolvencyDataComboBox3.AxisID = 140;
this.SolvencyDataComboBox3.OrdinateID = 1666;
this.SolvencyDataComboBox3.StartOrder = 1;
this.SolvencyDataComboBox3.NextOrder = 100000;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(117,97);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 1665;
this.solvencyLabel4.Size = new System.Drawing.Size(108, 20);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "FPS" ;
this.solvencyLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel5
//
this.solvencyLabel5.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel5.Location = new System.Drawing.Point(117,117);
this.solvencyLabel5.Name = "solvencyLabel5";
this.solvencyLabel5.OrdinateID_Label = 0;
this.solvencyLabel5.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel5.TabIndex = 5;
this.solvencyLabel5.Text = "C0030" ;
this.solvencyLabel5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel6
//
this.solvencyLabel6.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel6.Location = new System.Drawing.Point(10,97);
this.solvencyLabel6.Name = "solvencyLabel6";
this.solvencyLabel6.OrdinateID_Label = 1664;
this.solvencyLabel6.Size = new System.Drawing.Size(108, 20);
this.solvencyLabel6.TabIndex = 6;
this.solvencyLabel6.Text = "Branch" ;
this.solvencyLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel7
//
this.solvencyLabel7.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel7.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel7.Location = new System.Drawing.Point(10,117);
this.solvencyLabel7.Name = "solvencyLabel7";
this.solvencyLabel7.OrdinateID_Label = 0;
this.solvencyLabel7.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel7.TabIndex = 7;
this.solvencyLabel7.Text = "C0020" ;
this.solvencyLabel7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel8
//
this.solvencyLabel8.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel8.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel8.Location = new System.Drawing.Point(10,77);
this.solvencyLabel8.Name = "solvencyLabel8";
this.solvencyLabel8.OrdinateID_Label = 1663;
this.solvencyLabel8.Size = new System.Drawing.Size(215, 40);
this.solvencyLabel8.TabIndex = 8;
this.solvencyLabel8.Text = "By EEA Member" ;
this.solvencyLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyCurrencyTextBox9
//
this.solvencyCurrencyTextBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox9.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Integer;
this.solvencyCurrencyTextBox9.Location = new System.Drawing.Point(10,137);
this.solvencyCurrencyTextBox9.Name = "solvencyCurrencyTextBox9";
this.solvencyCurrencyTextBox9.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox9.TabIndex = 9;
this.solvencyCurrencyTextBox9.ColName = "R0020C0020";
this.solvencyCurrencyTextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox10
//
this.solvencyCurrencyTextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox10.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Integer;
this.solvencyCurrencyTextBox10.Location = new System.Drawing.Point(117,137);
this.solvencyCurrencyTextBox10.Name = "solvencyCurrencyTextBox10";
this.solvencyCurrencyTextBox10.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox10.TabIndex = 10;
this.solvencyCurrencyTextBox10.ColName = "R0020C0030";
this.solvencyCurrencyTextBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox11
//
this.solvencyCurrencyTextBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox11.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox11.Location = new System.Drawing.Point(10,170);
this.solvencyCurrencyTextBox11.Name = "solvencyCurrencyTextBox11";
this.solvencyCurrencyTextBox11.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox11.TabIndex = 11;
this.solvencyCurrencyTextBox11.ColName = "R0030C0020";
this.solvencyCurrencyTextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox12
//
this.solvencyCurrencyTextBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox12.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox12.Location = new System.Drawing.Point(117,170);
this.solvencyCurrencyTextBox12.Name = "solvencyCurrencyTextBox12";
this.solvencyCurrencyTextBox12.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox12.TabIndex = 12;
this.solvencyCurrencyTextBox12.ColName = "R0030C0030";
this.solvencyCurrencyTextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.solvencyButton0);
this.Controls.Add(this.solvencyButton1);
this.Controls.Add(this.solvencyLabel2);
this.Controls.Add(this.SolvencyDataComboBox3);
this.Controls.Add(this.solvencyLabel4);
this.Controls.Add(this.solvencyLabel5);
this.Controls.Add(this.solvencyLabel6);
this.Controls.Add(this.solvencyLabel7);
this.Controls.Add(this.solvencyLabel8);
this.Controls.Add(this.solvencyCurrencyTextBox9);
this.Controls.Add(this.solvencyCurrencyTextBox10);
this.Controls.Add(this.solvencyCurrencyTextBox11);
this.Controls.Add(this.solvencyCurrencyTextBox12);
            this.Name = "S_04_02_01_02__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(274, 193); 
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
private SolvencyLabel solvencyLabel6;
private SolvencyLabel solvencyLabel7;
private SolvencyLabel solvencyLabel8;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox9;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox10;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox11;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox12;

   }
}

