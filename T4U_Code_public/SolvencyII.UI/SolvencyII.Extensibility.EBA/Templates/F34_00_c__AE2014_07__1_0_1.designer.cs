using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class F34_00_c__AE2014_07__1_0_1 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.SolvencyComboBox1 = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();
this.pnlTop = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
this.F34_00_c__AE2014_07__1_0_1_ctrl0 = new F34_00_c__AE2014_07__1_0_1_ctrl();
this.splitForm = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(10,10);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 30298;
this.solvencyLabel0.Size = new System.Drawing.Size(250, 13);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Significant currency (999)" ;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// SolvencyComboBox1
//
this.SolvencyComboBox1.Location = new System.Drawing.Point(10,30);
this.SolvencyComboBox1.Name = "SolvencyComboBox1";
this.SolvencyComboBox1.Size = new System.Drawing.Size(250, 13);
this.SolvencyComboBox1.TabIndex = 1;
this.SolvencyComboBox1.TableNames = "T__F_34_00_c__AE_2014_03__1_0_0";
this.SolvencyComboBox1.ColName = "PAGEEBA_CUS";
this.SolvencyComboBox1.AxisID = 1697;
this.SolvencyComboBox1.OrdinateID = 0;
this.SolvencyComboBox1.HierarchyID = 0;
this.SolvencyComboBox1.StartOrder = 13850;
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
// F34_00_c__AE2014_07__1_0_1_ctrl0
//
this.F34_00_c__AE2014_07__1_0_1_ctrl0.Location = new System.Drawing.Point(0,0);
this.F34_00_c__AE2014_07__1_0_1_ctrl0.Name = "F34_00_c__AE2014_07__1_0_1_ctrl0";
this.F34_00_c__AE2014_07__1_0_1_ctrl0.Size = new System.Drawing.Size(167, 414);
//
// splitForm
//
this.splitForm.Dock = System.Windows.Forms.DockStyle.Fill;
this.splitForm.Location = new System.Drawing.Point(0,60);
this.splitForm.Name = "splitForm";
this.splitForm.Orientation = System.Windows.Forms.Orientation.Vertical;
//
// splitForm.Panel1
//
//
// splitForm.Panel2
//
this.splitForm.Panel2.Controls.Add(this.F34_00_c__AE2014_07__1_0_1_ctrl0);
this.splitForm.Panel2.AutoScroll = true;
this.splitForm.Size = new System.Drawing.Size(777, 414);
this.splitForm.SplitterDistance = 0;
this.splitForm.Panel1Collapsed = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.splitForm);
            this.Controls.Add(this.pnlTop);
            this.Name = "F34_00_c__AE2014_07__1_0_1"; 
            this.Size = new System.Drawing.Size(492, 160); 
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyComboBox SolvencyComboBox1;
private SolvencyPanel pnlTop;
private F34_00_c__AE2014_07__1_0_1_ctrl F34_00_c__AE2014_07__1_0_1_ctrl0;
private SolvencySplitContainer splitForm;

   }
}

