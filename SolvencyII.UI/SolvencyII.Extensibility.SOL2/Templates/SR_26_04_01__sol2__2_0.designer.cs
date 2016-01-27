using System.Windows.Forms;
using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class SR_26_04_01__sol2__2_0 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyTextComboBox1 = new SolvencyII.UI.Shared.Controls.SolvencyTextComboBox();
this.solvencyButton2 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyButton3 = new SolvencyII.UI.Shared.Controls.SolvencyButton();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox5 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.solvencyLabel6 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox7 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.pnlTop = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
this.SR_26_04_01__sol2__2_0_ctrl0 = new SR_26_04_01__sol2__2_0_ctrl();
this.pnlContainer = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
this.dr_Main = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(10,10);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 5794;
this.solvencyLabel0.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Fund/Portfolio Number (Z0030)" ;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyTextComboBox1
//
this.SolvencyTextComboBox1.Location = new System.Drawing.Point(10,30);
this.SolvencyTextComboBox1.Name = "SolvencyTextComboBox1";
this.SolvencyTextComboBox1.Size = new System.Drawing.Size(250, 13);
this.SolvencyTextComboBox1.TabIndex = 1;
this.SolvencyTextComboBox1.TableNames = "T__SR_26_04_01_01__sol2__2_0|T__SR_26_04_01_02__sol2__2_0|T__SR_26_04_01_03__sol2__2_0|T__SR_26_04_01_04__sol2__2_0|T__SR_26_04_01_05__sol2__2_0|T__SR_26_04_01_06__sol2__2_0|T__SR_26_04_01_07__sol2__2_0|T__SR_26_04_01_08__sol2__2_0|T__SR_26_04_01_09__sol2__2_0";
this.SolvencyTextComboBox1.ColName = "PAGES2C_FN";
this.SolvencyTextComboBox1.OrdinateID = 0;
//
// solvencyButton2
//
this.solvencyButton2.ColName = "PAGES2C_FN";
this.solvencyButton2.TableNames = "T__SR_26_04_01_01__sol2__2_0|T__SR_26_04_01_02__sol2__2_0|T__SR_26_04_01_03__sol2__2_0|T__SR_26_04_01_04__sol2__2_0|T__SR_26_04_01_05__sol2__2_0|T__SR_26_04_01_06__sol2__2_0|T__SR_26_04_01_07__sol2__2_0|T__SR_26_04_01_08__sol2__2_0|T__SR_26_04_01_09__sol2__2_0";
this.solvencyButton2.Location = new System.Drawing.Point(267,30);
this.solvencyButton2.Name = "solvencyButton2";
this.solvencyButton2.Size = new System.Drawing.Size(37, 21);
this.solvencyButton2.TabIndex = 2;
this.solvencyButton2.Text = "Add";
this.solvencyButton2.UseVisualStyleBackColor = true;
this.solvencyButton2.Click += new System.EventHandler(this.addControlText);
//
// solvencyButton3
//
this.solvencyButton3.ColName = "PAGES2C_FN";
this.solvencyButton3.TableNames = "T__SR_26_04_01_01__sol2__2_0|T__SR_26_04_01_02__sol2__2_0|T__SR_26_04_01_03__sol2__2_0|T__SR_26_04_01_04__sol2__2_0|T__SR_26_04_01_05__sol2__2_0|T__SR_26_04_01_06__sol2__2_0|T__SR_26_04_01_07__sol2__2_0|T__SR_26_04_01_08__sol2__2_0|T__SR_26_04_01_09__sol2__2_0";
this.solvencyButton3.Location = new System.Drawing.Point(311,30);
this.solvencyButton3.Name = "solvencyButton3";
this.solvencyButton3.Size = new System.Drawing.Size(51, 21);
this.solvencyButton3.TabIndex = 3;
this.solvencyButton3.Text = "Delete";
this.solvencyButton3.UseVisualStyleBackColor = true;
this.solvencyButton3.Click += new System.EventHandler(this.deleteControlText);
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(369,10);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 5792;
this.solvencyLabel4.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "Article 112 (Z0010)" ;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox5
//
this.SolvencyComboBox5.Location = new System.Drawing.Point(369,30);
this.SolvencyComboBox5.Name = "SolvencyComboBox5";
this.SolvencyComboBox5.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox5.TabIndex = 5;
this.SolvencyComboBox5.TableNames = "T__SR_26_04_01_01__sol2__2_0|T__SR_26_04_01_02__sol2__2_0|T__SR_26_04_01_03__sol2__2_0|T__SR_26_04_01_04__sol2__2_0|T__SR_26_04_01_05__sol2__2_0|T__SR_26_04_01_06__sol2__2_0|T__SR_26_04_01_07__sol2__2_0|T__SR_26_04_01_08__sol2__2_0|T__SR_26_04_01_09__sol2__2_0";
this.SolvencyComboBox5.ColName = "PAGES2C_AO";
this.SolvencyComboBox5.AxisID = 1031;
this.SolvencyComboBox5.OrdinateID = 0;
this.SolvencyComboBox5.HierarchyID = 0;
this.SolvencyComboBox5.StartOrder = 1;
this.SolvencyComboBox5.NextOrder = 100000;
//
// solvencyLabel6
//
this.solvencyLabel6.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel6.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel6.Location = new System.Drawing.Point(626,10);
this.solvencyLabel6.Name = "solvencyLabel6";
this.solvencyLabel6.OrdinateID_Label = 5793;
this.solvencyLabel6.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel6.TabIndex = 6;
this.solvencyLabel6.Text = "Ring Fenced Fund/Matching adjustment portfolio or remaining part (Z0020)" ;
this.solvencyLabel6.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox7
//
this.SolvencyComboBox7.Location = new System.Drawing.Point(626,30);
this.SolvencyComboBox7.Name = "SolvencyComboBox7";
this.SolvencyComboBox7.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox7.TabIndex = 7;
this.SolvencyComboBox7.TableNames = "T__SR_26_04_01_01__sol2__2_0|T__SR_26_04_01_02__sol2__2_0|T__SR_26_04_01_03__sol2__2_0|T__SR_26_04_01_04__sol2__2_0|T__SR_26_04_01_05__sol2__2_0|T__SR_26_04_01_06__sol2__2_0|T__SR_26_04_01_07__sol2__2_0|T__SR_26_04_01_08__sol2__2_0|T__SR_26_04_01_09__sol2__2_0";
this.SolvencyComboBox7.ColName = "PAGES2C_PO";
this.SolvencyComboBox7.AxisID = 1032;
this.SolvencyComboBox7.OrdinateID = 0;
this.SolvencyComboBox7.HierarchyID = 0;
this.SolvencyComboBox7.StartOrder = 1;
this.SolvencyComboBox7.NextOrder = 100000;
//
// pnlTop
//
this.pnlTop.Controls.Add(this.solvencyLabel0);
this.pnlTop.Controls.Add(this.SolvencyTextComboBox1);
this.pnlTop.Controls.Add(this.solvencyButton2);
this.pnlTop.Controls.Add(this.solvencyButton3);
this.pnlTop.Controls.Add(this.solvencyLabel4);
this.pnlTop.Controls.Add(this.SolvencyComboBox5);
this.pnlTop.Controls.Add(this.solvencyLabel6);
this.pnlTop.Controls.Add(this.SolvencyComboBox7);
this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
this.pnlTop.Location = new System.Drawing.Point(0,0);
this.pnlTop.Name = "pnlTop";
this.pnlTop.Size = new System.Drawing.Size(777, 60);
//
// SR_26_04_01__sol2__2_0_ctrl0
//
this.SR_26_04_01__sol2__2_0_ctrl0.Location = new System.Drawing.Point(0,0);
this.SR_26_04_01__sol2__2_0_ctrl0.Name = "SR_26_04_01__sol2__2_0_ctrl0";
this.SR_26_04_01__sol2__2_0_ctrl0.Size = new System.Drawing.Size(1201, 1593);
//
// pnlContainer
//
this.pnlContainer.Controls.Add(this.SR_26_04_01__sol2__2_0_ctrl0);
this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
this.pnlContainer.Location = new System.Drawing.Point(0,0);
this.pnlContainer.Name = "pnlContainer";
this.pnlContainer.Size = new System.Drawing.Size(1201, 1593);
this.pnlContainer.AutoScroll = true;
//
// dr_Main
//
this.dr_Main.Size = new System.Drawing.Size(1247, 1625);
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
this.dr_Main.ItemTemplate.Controls.Add(this.pnlContainer);
this.dr_Main.ItemTemplate.AutoScroll = true;
this.dr_Main.ItemTemplate.Size = new System.Drawing.Size(1241, 1619);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.dr_Main);
            this.Controls.Add(this.pnlTop);
            this.Name = "SR_26_04_01__sol2__2_0"; 
            this.Size = new System.Drawing.Size(5673, 1603); 
            this.Load += new System.EventHandler(this.Repeater_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyTextComboBox SolvencyTextComboBox1;
private SolvencyButton solvencyButton2;
private SolvencyButton solvencyButton3;
private SolvencyLabel solvencyLabel4;
private SolvencyComboBox SolvencyComboBox5;
private SolvencyLabel solvencyLabel6;
private SolvencyComboBox SolvencyComboBox7;
private SolvencyPanel pnlTop;
private SR_26_04_01__sol2__2_0_ctrl SR_26_04_01__sol2__2_0_ctrl0;
private SolvencyPanel pnlContainer;
private SolvencyDataRepeater dr_Main;

   }
}

