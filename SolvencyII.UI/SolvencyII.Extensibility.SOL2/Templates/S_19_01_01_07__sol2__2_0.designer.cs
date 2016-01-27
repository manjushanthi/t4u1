using System.Windows.Forms;
using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_19_01_01_07__sol2__2_0 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox1 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox3 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox5 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.solvencyLabel6 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox7 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.pnlTop = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
this.S_19_01_01_07__sol2__2_0_ctrl0 = new S_19_01_01_07__sol2__2_0_ctrl();
this.dr_Main = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(10,10);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 3242;
this.solvencyLabel0.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Line of business (Z0010)" ;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox1
//
this.SolvencyComboBox1.Location = new System.Drawing.Point(10,30);
this.SolvencyComboBox1.Name = "SolvencyComboBox1";
this.SolvencyComboBox1.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox1.TabIndex = 1;
this.SolvencyComboBox1.TableNames = "T__S_19_01_01_07__sol2__2_0";
this.SolvencyComboBox1.ColName = "PAGES2C_BL";
this.SolvencyComboBox1.AxisID = 382;
this.SolvencyComboBox1.OrdinateID = 0;
this.SolvencyComboBox1.HierarchyID = 0;
this.SolvencyComboBox1.StartOrder = 1;
this.SolvencyComboBox1.NextOrder = 100000;
//
// solvencyLabel2
//
this.solvencyLabel2.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel2.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel2.Location = new System.Drawing.Point(267,10);
this.solvencyLabel2.Name = "solvencyLabel2";
this.solvencyLabel2.OrdinateID_Label = 3243;
this.solvencyLabel2.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "Accident year / Underwriting year (Z0020)" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox3
//
this.SolvencyComboBox3.Location = new System.Drawing.Point(267,30);
this.SolvencyComboBox3.Name = "SolvencyComboBox3";
this.SolvencyComboBox3.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox3.TabIndex = 3;
this.SolvencyComboBox3.TableNames = "T__S_19_01_01_07__sol2__2_0";
this.SolvencyComboBox3.ColName = "PAGES2C_AX";
this.SolvencyComboBox3.AxisID = 383;
this.SolvencyComboBox3.OrdinateID = 0;
this.SolvencyComboBox3.HierarchyID = 0;
this.SolvencyComboBox3.StartOrder = 1;
this.SolvencyComboBox3.NextOrder = 100000;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(524,10);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 3244;
this.solvencyLabel4.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "Currency (Z0030)" ;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox5
//
this.SolvencyComboBox5.Location = new System.Drawing.Point(524,30);
this.SolvencyComboBox5.Name = "SolvencyComboBox5";
this.SolvencyComboBox5.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox5.TabIndex = 5;
this.SolvencyComboBox5.TableNames = "T__S_19_01_01_07__sol2__2_0";
this.SolvencyComboBox5.ColName = "PAGES2C_OC";
this.SolvencyComboBox5.AxisID = 384;
this.SolvencyComboBox5.OrdinateID = 0;
this.SolvencyComboBox5.HierarchyID = 0;
this.SolvencyComboBox5.StartOrder = 1;
this.SolvencyComboBox5.NextOrder = 100000;
//
// solvencyLabel6
//
this.solvencyLabel6.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel6.Location = new System.Drawing.Point(781,10);
this.solvencyLabel6.Name = "solvencyLabel6";
this.solvencyLabel6.OrdinateID_Label = 3245;
this.solvencyLabel6.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel6.TabIndex = 6;
this.solvencyLabel6.Text = "Currency conversion (Z0040)" ;
this.solvencyLabel6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox7
//
this.SolvencyComboBox7.Location = new System.Drawing.Point(781,30);
this.SolvencyComboBox7.Name = "SolvencyComboBox7";
this.SolvencyComboBox7.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox7.TabIndex = 7;
this.SolvencyComboBox7.TableNames = "T__S_19_01_01_07__sol2__2_0";
this.SolvencyComboBox7.ColName = "PAGES2C_AF";
this.SolvencyComboBox7.AxisID = 385;
this.SolvencyComboBox7.OrdinateID = 0;
this.SolvencyComboBox7.HierarchyID = 0;
this.SolvencyComboBox7.StartOrder = 1;
this.SolvencyComboBox7.NextOrder = 100000;
//
// pnlTop
//
this.pnlTop.Controls.Add(this.solvencyLabel0);
this.pnlTop.Controls.Add(this.SolvencyComboBox1);
this.pnlTop.Controls.Add(this.solvencyLabel2);
this.pnlTop.Controls.Add(this.SolvencyComboBox3);
this.pnlTop.Controls.Add(this.solvencyLabel4);
this.pnlTop.Controls.Add(this.SolvencyComboBox5);
this.pnlTop.Controls.Add(this.solvencyLabel6);
this.pnlTop.Controls.Add(this.SolvencyComboBox7);
this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
this.pnlTop.Location = new System.Drawing.Point(0,0);
this.pnlTop.Name = "pnlTop";
this.pnlTop.Size = new System.Drawing.Size(777, 60);
//
// S_19_01_01_07__sol2__2_0_ctrl0
//
this.S_19_01_01_07__sol2__2_0_ctrl0.Location = new System.Drawing.Point(0,0);
this.S_19_01_01_07__sol2__2_0_ctrl0.Name = "S_19_01_01_07__sol2__2_0_ctrl0";
this.S_19_01_01_07__sol2__2_0_ctrl0.Size = new System.Drawing.Size(1944, 376);
//
// dr_Main
//
this.dr_Main.Size = new System.Drawing.Size(1960, 408);
this.dr_Main.Location = new System.Drawing.Point(0,60);
this.dr_Main.Name = "dr_Main";
this.dr_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
this.dr_Main.Dock = DockStyle.Fill;
this.dr_Main.LayoutStyle = Microsoft.VisualBasic.PowerPacks.DataRepeaterLayoutStyles.Horizontal;
//
// dr_Main.ItemTemplate
//
//
// dr_Main.ItemTemplate
//
this.dr_Main.ItemTemplate.Controls.Add(this.S_19_01_01_07__sol2__2_0_ctrl0);
this.dr_Main.ItemTemplate.AutoScroll = true;
this.dr_Main.ItemTemplate.Size = new System.Drawing.Size(1954, 402);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.dr_Main);
            this.Controls.Add(this.pnlTop);
            this.Name = "S_19_01_01_07__sol2__2_0"; 
            this.Size = new System.Drawing.Size(1954, 386); 
            this.Load += new System.EventHandler(this.Repeater_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyComboBox SolvencyComboBox1;
private SolvencyLabel solvencyLabel2;
private SolvencyComboBox SolvencyComboBox3;
private SolvencyLabel solvencyLabel4;
private SolvencyComboBox SolvencyComboBox5;
private SolvencyLabel solvencyLabel6;
private SolvencyComboBox SolvencyComboBox7;
private SolvencyPanel pnlTop;
private S_19_01_01_07__sol2__2_0_ctrl S_19_01_01_07__sol2__2_0_ctrl0;
private SolvencyDataRepeater dr_Main;

   }
}

