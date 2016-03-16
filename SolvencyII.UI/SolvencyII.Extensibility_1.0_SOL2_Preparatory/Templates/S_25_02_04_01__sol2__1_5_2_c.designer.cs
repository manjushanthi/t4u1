using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_25_02_04_01__sol2__1_5_2_c 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyTextComboBox1 = new SolvencyII.UI.Shared.Controls.SolvencyTextComboBox();
this.solvencyButton2 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyButton3 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.pnlTop = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(10,30);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 1689;
this.solvencyLabel0.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Number of fund (Z0010)" ;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyTextComboBox1
//
this.SolvencyTextComboBox1.Location = new System.Drawing.Point(10,50);
this.SolvencyTextComboBox1.Name = "SolvencyTextComboBox1";
this.SolvencyTextComboBox1.Size = new System.Drawing.Size(250, 13);
this.SolvencyTextComboBox1.TabIndex = 1;
this.SolvencyTextComboBox1.TableNames = "T__S_25_02_04_01__sol2__1_5_2_c";
this.SolvencyTextComboBox1.ColName = "PAGES2C_NF";
this.SolvencyTextComboBox1.OrdinateID = 0;
//
// solvencyButton2
//
this.solvencyButton2.ColName = "PAGES2C_NF";
this.solvencyButton2.TableNames = "T__S_25_02_04_01__sol2__1_5_2_c";
this.solvencyButton2.Location = new System.Drawing.Point(267,50);
this.solvencyButton2.Name = "solvencyButton2";
this.solvencyButton2.Size = new System.Drawing.Size(37, 21);
this.solvencyButton2.TabIndex = 2;
this.solvencyButton2.Text = "Add";
this.solvencyButton2.UseVisualStyleBackColor = true;
this.solvencyButton2.Click += new System.EventHandler(this.addControlText);
//
// solvencyButton3
//
this.solvencyButton3.ColName = "PAGES2C_NF";
this.solvencyButton3.TableNames = "T__S_25_02_04_01__sol2__1_5_2_c";
this.solvencyButton3.Location = new System.Drawing.Point(311,50);
this.solvencyButton3.Name = "solvencyButton3";
this.solvencyButton3.Size = new System.Drawing.Size(51, 21);
this.solvencyButton3.TabIndex = 3;
this.solvencyButton3.Text = "Delete";
this.solvencyButton3.UseVisualStyleBackColor = true;
this.solvencyButton3.Click += new System.EventHandler(this.deleteControlText);
//
// pnlTop
//
this.pnlTop.Controls.Add(this.solvencyLabel0);
this.pnlTop.Controls.Add(this.SolvencyTextComboBox1);
this.pnlTop.Controls.Add(this.solvencyButton2);
this.pnlTop.Controls.Add(this.solvencyButton3);
this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
this.pnlTop.Location = new System.Drawing.Point(0,0);
this.pnlTop.Name = "pnlTop";
this.pnlTop.Size = new System.Drawing.Size(777, 80);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.pnlTop);
            this.Name = "S_25_02_04_01__sol2__1_5_2_c"; 
            this.Size = new System.Drawing.Size(40, 180); 
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyTextComboBox SolvencyTextComboBox1;
private SolvencyButton solvencyButton2;
private SolvencyButton solvencyButton3;
private SolvencyPanel pnlTop;

   }
}

