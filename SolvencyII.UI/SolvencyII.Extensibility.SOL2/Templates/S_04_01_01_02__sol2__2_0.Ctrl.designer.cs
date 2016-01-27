using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_04_01_01_02__sol2__2_0_ctrl 
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
this.solvencyLabel9 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel10 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyCurrencyTextBox11 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox12 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox13 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox14 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox15 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox16 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox17 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox18 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox19 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
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
this.solvencyLabel2.OrdinateID_Label = 1645;
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
this.SolvencyDataComboBox3.ColName = "PAGES2C_LR";
this.SolvencyDataComboBox3.AxisID = 128;
this.SolvencyDataComboBox3.OrdinateID = 1645;
this.SolvencyDataComboBox3.StartOrder = 1;
this.SolvencyDataComboBox3.NextOrder = 100000;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(224,97);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 1644;
this.solvencyLabel4.Size = new System.Drawing.Size(108, 75);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "Business underwritten in the considered country through FPS, by the undertaking or any EEA branch" ;
this.solvencyLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel5
//
this.solvencyLabel5.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel5.Location = new System.Drawing.Point(224,172);
this.solvencyLabel5.Name = "solvencyLabel5";
this.solvencyLabel5.OrdinateID_Label = 0;
this.solvencyLabel5.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel5.TabIndex = 5;
this.solvencyLabel5.Text = "C0100" ;
this.solvencyLabel5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel6
//
this.solvencyLabel6.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel6.Location = new System.Drawing.Point(117,97);
this.solvencyLabel6.Name = "solvencyLabel6";
this.solvencyLabel6.OrdinateID_Label = 1643;
this.solvencyLabel6.Size = new System.Drawing.Size(108, 75);
this.solvencyLabel6.TabIndex = 6;
this.solvencyLabel6.Text = "Business underwritten through FPS, by the EEA branch established in the considered country" ;
this.solvencyLabel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel7
//
this.solvencyLabel7.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel7.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel7.Location = new System.Drawing.Point(117,172);
this.solvencyLabel7.Name = "solvencyLabel7";
this.solvencyLabel7.OrdinateID_Label = 0;
this.solvencyLabel7.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel7.TabIndex = 7;
this.solvencyLabel7.Text = "C0090" ;
this.solvencyLabel7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel8
//
this.solvencyLabel8.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel8.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel8.Location = new System.Drawing.Point(10,97);
this.solvencyLabel8.Name = "solvencyLabel8";
this.solvencyLabel8.OrdinateID_Label = 1642;
this.solvencyLabel8.Size = new System.Drawing.Size(108, 75);
this.solvencyLabel8.TabIndex = 8;
this.solvencyLabel8.Text = "Business underwritten in the considered country, by the EEA branch established in this country" ;
this.solvencyLabel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel9
//
this.solvencyLabel9.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel9.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel9.Location = new System.Drawing.Point(10,172);
this.solvencyLabel9.Name = "solvencyLabel9";
this.solvencyLabel9.OrdinateID_Label = 0;
this.solvencyLabel9.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel9.TabIndex = 9;
this.solvencyLabel9.Text = "C0080" ;
this.solvencyLabel9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel10
//
this.solvencyLabel10.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel10.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel10.Location = new System.Drawing.Point(10,77);
this.solvencyLabel10.Name = "solvencyLabel10";
this.solvencyLabel10.OrdinateID_Label = 1641;
this.solvencyLabel10.Size = new System.Drawing.Size(322, 95);
this.solvencyLabel10.TabIndex = 10;
this.solvencyLabel10.Text = "By EEA member" ;
this.solvencyLabel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel10.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyCurrencyTextBox11
//
this.solvencyCurrencyTextBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox11.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox11.Location = new System.Drawing.Point(10,192);
this.solvencyCurrencyTextBox11.Name = "solvencyCurrencyTextBox11";
this.solvencyCurrencyTextBox11.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox11.TabIndex = 11;
this.solvencyCurrencyTextBox11.ColName = "R0020C0080";
this.solvencyCurrencyTextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox12
//
this.solvencyCurrencyTextBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox12.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox12.Location = new System.Drawing.Point(117,192);
this.solvencyCurrencyTextBox12.Name = "solvencyCurrencyTextBox12";
this.solvencyCurrencyTextBox12.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox12.TabIndex = 12;
this.solvencyCurrencyTextBox12.ColName = "R0020C0090";
this.solvencyCurrencyTextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox13
//
this.solvencyCurrencyTextBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox13.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox13.Location = new System.Drawing.Point(224,192);
this.solvencyCurrencyTextBox13.Name = "solvencyCurrencyTextBox13";
this.solvencyCurrencyTextBox13.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox13.TabIndex = 13;
this.solvencyCurrencyTextBox13.ColName = "R0020C0100";
this.solvencyCurrencyTextBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox14
//
this.solvencyCurrencyTextBox14.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox14.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox14.Location = new System.Drawing.Point(10,212);
this.solvencyCurrencyTextBox14.Name = "solvencyCurrencyTextBox14";
this.solvencyCurrencyTextBox14.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox14.TabIndex = 14;
this.solvencyCurrencyTextBox14.ColName = "R0030C0080";
this.solvencyCurrencyTextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox15
//
this.solvencyCurrencyTextBox15.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox15.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox15.Location = new System.Drawing.Point(117,212);
this.solvencyCurrencyTextBox15.Name = "solvencyCurrencyTextBox15";
this.solvencyCurrencyTextBox15.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox15.TabIndex = 15;
this.solvencyCurrencyTextBox15.ColName = "R0030C0090";
this.solvencyCurrencyTextBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox16
//
this.solvencyCurrencyTextBox16.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox16.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox16.Location = new System.Drawing.Point(224,212);
this.solvencyCurrencyTextBox16.Name = "solvencyCurrencyTextBox16";
this.solvencyCurrencyTextBox16.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox16.TabIndex = 16;
this.solvencyCurrencyTextBox16.ColName = "R0030C0100";
this.solvencyCurrencyTextBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
this.solvencyCurrencyTextBox16.Enabled = false;
this.solvencyCurrencyTextBox16.BackColor = System.Drawing.Color.Gray;
//
// solvencyCurrencyTextBox17
//
this.solvencyCurrencyTextBox17.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox17.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox17.Location = new System.Drawing.Point(10,232);
this.solvencyCurrencyTextBox17.Name = "solvencyCurrencyTextBox17";
this.solvencyCurrencyTextBox17.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox17.TabIndex = 17;
this.solvencyCurrencyTextBox17.ColName = "R0040C0080";
this.solvencyCurrencyTextBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox18
//
this.solvencyCurrencyTextBox18.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox18.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox18.Location = new System.Drawing.Point(117,232);
this.solvencyCurrencyTextBox18.Name = "solvencyCurrencyTextBox18";
this.solvencyCurrencyTextBox18.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox18.TabIndex = 18;
this.solvencyCurrencyTextBox18.ColName = "R0040C0090";
this.solvencyCurrencyTextBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox19
//
this.solvencyCurrencyTextBox19.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox19.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox19.Location = new System.Drawing.Point(224,232);
this.solvencyCurrencyTextBox19.Name = "solvencyCurrencyTextBox19";
this.solvencyCurrencyTextBox19.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox19.TabIndex = 19;
this.solvencyCurrencyTextBox19.ColName = "R0040C0100";
this.solvencyCurrencyTextBox19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
this.solvencyCurrencyTextBox19.Enabled = false;
this.solvencyCurrencyTextBox19.BackColor = System.Drawing.Color.Gray;
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
this.Controls.Add(this.solvencyLabel9);
this.Controls.Add(this.solvencyLabel10);
this.Controls.Add(this.solvencyCurrencyTextBox11);
this.Controls.Add(this.solvencyCurrencyTextBox12);
this.Controls.Add(this.solvencyCurrencyTextBox13);
this.Controls.Add(this.solvencyCurrencyTextBox14);
this.Controls.Add(this.solvencyCurrencyTextBox15);
this.Controls.Add(this.solvencyCurrencyTextBox16);
this.Controls.Add(this.solvencyCurrencyTextBox17);
this.Controls.Add(this.solvencyCurrencyTextBox18);
this.Controls.Add(this.solvencyCurrencyTextBox19);
            this.Name = "S_04_01_01_02__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(381, 255); 
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
private SolvencyLabel solvencyLabel9;
private SolvencyLabel solvencyLabel10;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox11;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox12;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox13;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox14;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox15;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox16;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox17;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox18;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox19;

   }
}

