using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_22_06_01_04__sol2__2_0_ctrl 
   { 
      private void InitializeComponent() 
      { 
this.lblRow = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.cboRow = new SolvencyII.UI.Shared.Controls.SolvencyDataComboBox();
this.btnAddRow = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.btnDelRow = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.lblCol = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.cboCol = new SolvencyII.UI.Shared.Controls.SolvencyDataComboBox();
this.btnDelCol = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.txtCell = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();
this.btnAddCol = new SolvencyII.UI.Shared.Controls.SolvencyButton();
            this.SuspendLayout(); 

//
// lblRow
//
this.lblRow.BackColor = System.Drawing.Color.Transparent;
this.lblRow.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.lblRow.Location = new System.Drawing.Point(120,50);
this.lblRow.Name = "lblRow";
this.lblRow.OrdinateID_Label = 4126;
this.lblRow.Size = new System.Drawing.Size(108, 26);
this.lblRow.TabIndex = 0;
this.lblRow.Text = "Other than reporting currency" ;
this.lblRow.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// cboRow
//
this.cboRow.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;
this.cboRow.Location = new System.Drawing.Point(120,90);
this.cboRow.Name = "cboRow";
this.cboRow.Size = new System.Drawing.Size(100, 13);
this.cboRow.TabIndex = 1;
this.cboRow.ColName = "PAGES2C_OC";
this.cboRow.AxisID = 572;
this.cboRow.OrdinateID = 4126;
this.cboRow.StartOrder = 1;
this.cboRow.NextOrder = 100000;
//
// btnAddRow
//
this.btnAddRow.ColName = "PAGES2C_OC";
this.btnAddRow.TableNames = "T__S_22_06_01_04__sol2__2_0";
this.btnAddRow.Location = new System.Drawing.Point(120,113);
this.btnAddRow.Name = "btnAddRow";
this.btnAddRow.Size = new System.Drawing.Size(100, 20);
this.btnAddRow.TabIndex = 1;
this.btnAddRow.Text = "Add Row";
this.btnAddRow.UseVisualStyleBackColor = true;
this.btnAddRow.Click += new System.EventHandler(this.btnAddRow_Click);
//
// btnDelRow
//
this.btnDelRow.ColName = "PAGES2C_OC";
this.btnDelRow.TableNames = "T__S_22_06_01_04__sol2__2_0";
this.btnDelRow.Location = new System.Drawing.Point(10,90);
this.btnDelRow.Name = "btnDelRow";
this.btnDelRow.Size = new System.Drawing.Size(100, 20);
this.btnDelRow.TabIndex = 1;
this.btnDelRow.Text = "Del Row";
this.btnDelRow.UseVisualStyleBackColor = true;
this.btnDelRow.Click += new System.EventHandler(this.btnDelRow_Click);
//
// lblCol
//
this.lblCol.BackColor = System.Drawing.Color.Transparent;
this.lblCol.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.lblCol.Location = new System.Drawing.Point(230,10);
this.lblCol.Name = "lblCol";
this.lblCol.OrdinateID_Label = 4128;
this.lblCol.Size = new System.Drawing.Size(108, 26);
this.lblCol.TabIndex = 4;
this.lblCol.Text = "Other than home country" ;
this.lblCol.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// cboCol
//
this.cboCol.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;
this.cboCol.Location = new System.Drawing.Point(230,70);
this.cboCol.Name = "cboCol";
this.cboCol.Size = new System.Drawing.Size(100, 13);
this.cboCol.TabIndex = 5;
this.cboCol.ColName = "PAGES2C_LG";
this.cboCol.AxisID = 574;
this.cboCol.OrdinateID = 4128;
this.cboCol.StartOrder = 1;
this.cboCol.NextOrder = 100000;
//
// btnDelCol
//
this.btnDelCol.ColName = "PAGES2C_LG";
this.btnDelCol.TableNames = "T__S_22_06_01_04__sol2__2_0";
this.btnDelCol.Location = new System.Drawing.Point(230,50);
this.btnDelCol.Name = "btnDelCol";
this.btnDelCol.Size = new System.Drawing.Size(100, 20);
this.btnDelCol.TabIndex = 5;
this.btnDelCol.Text = "Del Col";
this.btnDelCol.UseVisualStyleBackColor = true;
this.btnDelCol.Click += new System.EventHandler(this.btnDelCol_Click);
//
// txtCell
//
this.txtCell.BorderStyle = System.Windows.Forms.BorderStyle.None;
this.txtCell.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Monetry;
this.txtCell.Location = new System.Drawing.Point(230,93);
this.txtCell.Name = "txtCell";
this.txtCell.Size = new System.Drawing.Size(100, 13);
this.txtCell.TabIndex = 7;
this.txtCell.ColName = "R0040C0050";
this.txtCell.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
//
// btnAddCol
//
this.btnAddCol.ColName = "R0040C0050";
this.btnAddCol.TableNames = "T__S_22_06_01_04__sol2__2_0";
this.btnAddCol.Location = new System.Drawing.Point(337,70);
this.btnAddCol.Name = "btnAddCol";
this.btnAddCol.Size = new System.Drawing.Size(100, 20);
this.btnAddCol.TabIndex = 7;
this.btnAddCol.Text = "Add Col";
this.btnAddCol.UseVisualStyleBackColor = true;
this.btnAddCol.Click += new System.EventHandler(this.btnAddCol_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.lblRow);
this.Controls.Add(this.cboRow);
this.Controls.Add(this.btnAddRow);
this.Controls.Add(this.btnDelRow);
this.Controls.Add(this.lblCol);
this.Controls.Add(this.cboCol);
this.Controls.Add(this.btnDelCol);
this.Controls.Add(this.txtCell);
this.Controls.Add(this.btnAddCol);
            this.Name = "S_22_06_01_04__sol2__2_0_ctrl"; 
            this.Size = new System.Drawing.Size(444, 133); 
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel lblRow;
private SolvencyDataComboBox cboRow;
private SolvencyButton btnAddRow;
private SolvencyButton btnDelRow;
private SolvencyLabel lblCol;
private SolvencyDataComboBox cboCol;
private SolvencyButton btnDelCol;
private SolvencyCurrencyTextBox txtCell;
private SolvencyButton btnAddCol;

   }
}

