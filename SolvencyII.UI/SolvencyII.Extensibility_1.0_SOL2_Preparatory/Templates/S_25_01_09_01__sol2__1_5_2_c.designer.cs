using System.Windows.Forms;
using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_25_01_09_01__sol2__1_5_2_c 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox1 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.pnlTop = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
this.S_25_01_09_01__sol2__1_5_2_c_ctrl0 = new S_25_01_09_01__sol2__1_5_2_c_ctrl();
this.dr_Main = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(10,10);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 1435;
this.solvencyLabel0.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Article 112 (Z0010)" ;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox1
//
this.SolvencyComboBox1.Location = new System.Drawing.Point(10,30);
this.SolvencyComboBox1.Name = "SolvencyComboBox1";
this.SolvencyComboBox1.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox1.TabIndex = 1;
this.SolvencyComboBox1.TableNames = "T__S_25_01_09_01__sol2__1_5_2_c";
this.SolvencyComboBox1.ColName = "PAGES2C_AO";
this.SolvencyComboBox1.AxisID = 174;
this.SolvencyComboBox1.OrdinateID = 0;
this.SolvencyComboBox1.HierarchyID = 0;
this.SolvencyComboBox1.StartOrder = 1;
this.SolvencyComboBox1.NextOrder = 100000;
//
// pnlTop
//
this.pnlTop.Controls.Add(this.solvencyLabel0);
this.pnlTop.Controls.Add(this.SolvencyComboBox1);
this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
this.pnlTop.Location = new System.Drawing.Point(0,0);
this.pnlTop.Name = "pnlTop";
this.pnlTop.Size = new System.Drawing.Size(777, 60);
//
// S_25_01_09_01__sol2__1_5_2_c_ctrl0
//
this.S_25_01_09_01__sol2__1_5_2_c_ctrl0.Location = new System.Drawing.Point(0,0);
this.S_25_01_09_01__sol2__1_5_2_c_ctrl0.Name = "S_25_01_09_01__sol2__1_5_2_c_ctrl0";
this.S_25_01_09_01__sol2__1_5_2_c_ctrl0.Size = new System.Drawing.Size(594, 271);
//
// dr_Main
//
this.dr_Main.Size = new System.Drawing.Size(610, 303);
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
this.dr_Main.ItemTemplate.Controls.Add(this.S_25_01_09_01__sol2__1_5_2_c_ctrl0);
this.dr_Main.ItemTemplate.AutoScroll = true;
this.dr_Main.ItemTemplate.Size = new System.Drawing.Size(604, 297);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.dr_Main);
            this.Controls.Add(this.pnlTop);
            this.Name = "S_25_01_09_01__sol2__1_5_2_c"; 
            this.Size = new System.Drawing.Size(604, 281); 
            this.Load += new System.EventHandler(this.Repeater_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyComboBox SolvencyComboBox1;
private SolvencyPanel pnlTop;
private S_25_01_09_01__sol2__1_5_2_c_ctrl S_25_01_09_01__sol2__1_5_2_c_ctrl0;
private SolvencyDataRepeater dr_Main;

   }
}

