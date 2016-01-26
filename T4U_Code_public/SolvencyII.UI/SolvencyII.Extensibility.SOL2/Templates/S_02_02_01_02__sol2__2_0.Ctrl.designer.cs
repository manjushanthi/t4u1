using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_02_02_01_02__sol2__2_0_ctrl 
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
this.solvencyCurrencyTextBox8 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox9 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox10 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox11 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox12 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox13 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox14 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox15 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox16 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox17 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox18 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox19 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.solvencyCurrencyTextBox20 = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
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
this.solvencyLabel2.OrdinateID_Label = 1492;
this.solvencyLabel2.Size = new System.Drawing.Size(100, 13);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "Material currency" ;
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
this.SolvencyDataComboBox3.AxisID = 96;
this.SolvencyDataComboBox3.OrdinateID = 1492;
this.SolvencyDataComboBox3.StartOrder = 1;
this.SolvencyDataComboBox3.NextOrder = 100000;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(10,77);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 1491;
this.solvencyLabel4.Size = new System.Drawing.Size(108, 30);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "Value of material currencies" ;
this.solvencyLabel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
//
// solvencyLabel5
//
this.solvencyLabel5.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel5.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel5.Location = new System.Drawing.Point(10,107);
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
this.solvencyCurrencyTextBox6.Location = new System.Drawing.Point(10,147);
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
this.solvencyCurrencyTextBox7.Location = new System.Drawing.Point(10,180);
this.solvencyCurrencyTextBox7.Name = "solvencyCurrencyTextBox7";
this.solvencyCurrencyTextBox7.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox7.TabIndex = 7;
this.solvencyCurrencyTextBox7.ColName = "R0030C0050";
this.solvencyCurrencyTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox8
//
this.solvencyCurrencyTextBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox8.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox8.Location = new System.Drawing.Point(10,258);
this.solvencyCurrencyTextBox8.Name = "solvencyCurrencyTextBox8";
this.solvencyCurrencyTextBox8.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox8.TabIndex = 8;
this.solvencyCurrencyTextBox8.ColName = "R0040C0050";
this.solvencyCurrencyTextBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox9
//
this.solvencyCurrencyTextBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox9.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox9.Location = new System.Drawing.Point(10,278);
this.solvencyCurrencyTextBox9.Name = "solvencyCurrencyTextBox9";
this.solvencyCurrencyTextBox9.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox9.TabIndex = 9;
this.solvencyCurrencyTextBox9.ColName = "R0050C0050";
this.solvencyCurrencyTextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox10
//
this.solvencyCurrencyTextBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox10.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox10.Location = new System.Drawing.Point(10,298);
this.solvencyCurrencyTextBox10.Name = "solvencyCurrencyTextBox10";
this.solvencyCurrencyTextBox10.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox10.TabIndex = 10;
this.solvencyCurrencyTextBox10.ColName = "R0060C0050";
this.solvencyCurrencyTextBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox11
//
this.solvencyCurrencyTextBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox11.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox11.Location = new System.Drawing.Point(10,331);
this.solvencyCurrencyTextBox11.Name = "solvencyCurrencyTextBox11";
this.solvencyCurrencyTextBox11.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox11.TabIndex = 11;
this.solvencyCurrencyTextBox11.ColName = "R0070C0050";
this.solvencyCurrencyTextBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox12
//
this.solvencyCurrencyTextBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox12.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox12.Location = new System.Drawing.Point(10,351);
this.solvencyCurrencyTextBox12.Name = "solvencyCurrencyTextBox12";
this.solvencyCurrencyTextBox12.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox12.TabIndex = 12;
this.solvencyCurrencyTextBox12.ColName = "R0100C0050";
this.solvencyCurrencyTextBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox13
//
this.solvencyCurrencyTextBox13.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox13.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox13.Location = new System.Drawing.Point(10,391);
this.solvencyCurrencyTextBox13.Name = "solvencyCurrencyTextBox13";
this.solvencyCurrencyTextBox13.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox13.TabIndex = 13;
this.solvencyCurrencyTextBox13.ColName = "R0110C0050";
this.solvencyCurrencyTextBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox14
//
this.solvencyCurrencyTextBox14.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox14.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox14.Location = new System.Drawing.Point(10,424);
this.solvencyCurrencyTextBox14.Name = "solvencyCurrencyTextBox14";
this.solvencyCurrencyTextBox14.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox14.TabIndex = 14;
this.solvencyCurrencyTextBox14.ColName = "R0120C0050";
this.solvencyCurrencyTextBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox15
//
this.solvencyCurrencyTextBox15.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox15.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox15.Location = new System.Drawing.Point(10,457);
this.solvencyCurrencyTextBox15.Name = "solvencyCurrencyTextBox15";
this.solvencyCurrencyTextBox15.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox15.TabIndex = 15;
this.solvencyCurrencyTextBox15.ColName = "R0130C0050";
this.solvencyCurrencyTextBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox16
//
this.solvencyCurrencyTextBox16.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox16.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox16.Location = new System.Drawing.Point(10,490);
this.solvencyCurrencyTextBox16.Name = "solvencyCurrencyTextBox16";
this.solvencyCurrencyTextBox16.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox16.TabIndex = 16;
this.solvencyCurrencyTextBox16.ColName = "R0140C0050";
this.solvencyCurrencyTextBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox17
//
this.solvencyCurrencyTextBox17.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox17.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox17.Location = new System.Drawing.Point(10,510);
this.solvencyCurrencyTextBox17.Name = "solvencyCurrencyTextBox17";
this.solvencyCurrencyTextBox17.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox17.TabIndex = 17;
this.solvencyCurrencyTextBox17.ColName = "R0150C0050";
this.solvencyCurrencyTextBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox18
//
this.solvencyCurrencyTextBox18.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox18.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox18.Location = new System.Drawing.Point(10,530);
this.solvencyCurrencyTextBox18.Name = "solvencyCurrencyTextBox18";
this.solvencyCurrencyTextBox18.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox18.TabIndex = 18;
this.solvencyCurrencyTextBox18.ColName = "R0160C0050";
this.solvencyCurrencyTextBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox19
//
this.solvencyCurrencyTextBox19.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox19.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox19.Location = new System.Drawing.Point(10,550);
this.solvencyCurrencyTextBox19.Name = "solvencyCurrencyTextBox19";
this.solvencyCurrencyTextBox19.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox19.TabIndex = 19;
this.solvencyCurrencyTextBox19.ColName = "R0170C0050";
this.solvencyCurrencyTextBox19.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// solvencyCurrencyTextBox20
//
this.solvencyCurrencyTextBox20.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.solvencyCurrencyTextBox20.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.solvencyCurrencyTextBox20.Location = new System.Drawing.Point(10,570);
this.solvencyCurrencyTextBox20.Name = "solvencyCurrencyTextBox20";
this.solvencyCurrencyTextBox20.Size = new System.Drawing.Size(100, 13);
this.solvencyCurrencyTextBox20.TabIndex = 20;
this.solvencyCurrencyTextBox20.ColName = "R0200C0050";
this.solvencyCurrencyTextBox20.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
this.Controls.Add(this.solvencyCurrencyTextBox8);
this.Controls.Add(this.solvencyCurrencyTextBox9);
this.Controls.Add(this.solvencyCurrencyTextBox10);
this.Controls.Add(this.solvencyCurrencyTextBox11);
this.Controls.Add(this.solvencyCurrencyTextBox12);
this.Controls.Add(this.solvencyCurrencyTextBox13);
this.Controls.Add(this.solvencyCurrencyTextBox14);
this.Controls.Add(this.solvencyCurrencyTextBox15);
this.Controls.Add(this.solvencyCurrencyTextBox16);
this.Controls.Add(this.solvencyCurrencyTextBox17);
this.Controls.Add(this.solvencyCurrencyTextBox18);
this.Controls.Add(this.solvencyCurrencyTextBox19);
this.Controls.Add(this.solvencyCurrencyTextBox20);
            this.Name = "S_02_02_01_02__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(167, 593); 
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
private SolvencyCurrencyTextBox solvencyCurrencyTextBox8;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox9;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox10;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox11;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox12;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox13;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox14;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox15;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox16;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox17;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox18;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox19;
private SolvencyCurrencyTextBox solvencyCurrencyTextBox20;

   }
}
