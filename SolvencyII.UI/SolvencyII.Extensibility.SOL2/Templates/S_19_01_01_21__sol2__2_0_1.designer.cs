using System.Windows.Forms;
using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_19_01_01_21__sol2__2_0_1 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox1 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox3 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.pnlTop = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
this.S_19_01_01_21__sol2__2_0_1_ctrl0 = new S_19_01_01_21__sol2__2_0_1_ctrl();
this.dr_Main = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(10,10);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 3272;
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
this.SolvencyComboBox1.TableNames = "T__S_19_01_01_21__sol2__2_0_1";
this.SolvencyComboBox1.ColName = "PAGES2C_BL";
this.SolvencyComboBox1.AxisID = 391;
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
this.solvencyLabel2.OrdinateID_Label = 3274;
this.solvencyLabel2.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "Currency (Z0030)" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox3
//
this.SolvencyComboBox3.Location = new System.Drawing.Point(267,30);
this.SolvencyComboBox3.Name = "SolvencyComboBox3";
this.SolvencyComboBox3.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox3.TabIndex = 3;
this.SolvencyComboBox3.TableNames = "T__S_19_01_01_21__sol2__2_0_1";
this.SolvencyComboBox3.ColName = "PAGES2C_OC";
this.SolvencyComboBox3.AxisID = 393;
this.SolvencyComboBox3.OrdinateID = 0;
this.SolvencyComboBox3.HierarchyID = 0;
this.SolvencyComboBox3.StartOrder = 1;
this.SolvencyComboBox3.NextOrder = 100000;
//
// pnlTop
//
this.pnlTop.Controls.Add(this.solvencyLabel0);
this.pnlTop.Controls.Add(this.SolvencyComboBox1);
this.pnlTop.Controls.Add(this.solvencyLabel2);
this.pnlTop.Controls.Add(this.SolvencyComboBox3);
this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
this.pnlTop.Location = new System.Drawing.Point(0,0);
this.pnlTop.Name = "pnlTop";
this.pnlTop.Size = new System.Drawing.Size(777, 60);
//
// S_19_01_01_21__sol2__2_0_1_ctrl0
//
this.S_19_01_01_21__sol2__2_0_1_ctrl0.Location = new System.Drawing.Point(0,0);
this.S_19_01_01_21__sol2__2_0_1_ctrl0.Name = "S_19_01_01_21__sol2__2_0_1_ctrl0";
this.S_19_01_01_21__sol2__2_0_1_ctrl0.Size = new System.Drawing.Size(470, 76);
//
// dr_Main
//
this.dr_Main.Size = new System.Drawing.Size(486, 108);
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
this.dr_Main.ItemTemplate.Controls.Add(this.S_19_01_01_21__sol2__2_0_1_ctrl0);
this.dr_Main.ItemTemplate.AutoScroll = true;
this.dr_Main.ItemTemplate.Size = new System.Drawing.Size(480, 102);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.dr_Main);
            this.Controls.Add(this.pnlTop);
            this.Name = "S_19_01_01_21__sol2__2_0_1"; 
            this.Size = new System.Drawing.Size(480, 160); 
            this.Load += new System.EventHandler(this.Repeater_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyComboBox SolvencyComboBox1;
private SolvencyLabel solvencyLabel2;
private SolvencyComboBox SolvencyComboBox3;
private SolvencyPanel pnlTop;
private S_19_01_01_21__sol2__2_0_1_ctrl S_19_01_01_21__sol2__2_0_1_ctrl0;
private SolvencyDataRepeater dr_Main;

   }
}

