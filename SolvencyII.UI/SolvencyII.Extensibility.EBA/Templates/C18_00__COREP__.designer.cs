using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class C18_00__COREP__ 
   { 
      private void InitializeComponent() 
      { 
this.SolvencyPageTextBox0 = new SolvencyII.UI.Shared.Controls.SolvencyPageTextBox();
this.pnlTop = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
this.C18_00__COREP___ctrl0 = new C18_00__COREP___ctrl();
this.splitForm = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
            this.SuspendLayout(); 

//
// SolvencyPageTextBox0
//
this.SolvencyPageTextBox0.Location = new System.Drawing.Point(10,10);
this.SolvencyPageTextBox0.Name = "SolvencyPageTextBox0";
this.SolvencyPageTextBox0.Size = new System.Drawing.Size(250, 13);
this.SolvencyPageTextBox0.TableNames = "T__C_18_00__COREP__2_0_1";
this.SolvencyPageTextBox0.ColName = "PAGEEBA_CUE";
this.SolvencyPageTextBox0.FixedDimension = true;
//
// pnlTop
//
this.pnlTop.Controls.Add(this.SolvencyPageTextBox0);
this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
this.pnlTop.Location = new System.Drawing.Point(0,0);
this.pnlTop.Name = "pnlTop";
this.pnlTop.Size = new System.Drawing.Size(777, 60);
//
// C18_00__COREP___ctrl0
//
this.C18_00__COREP___ctrl0.Location = new System.Drawing.Point(0,0);
this.C18_00__COREP___ctrl0.Name = "C18_00__COREP___ctrl0";
this.C18_00__COREP___ctrl0.Size = new System.Drawing.Size(809, 1191);
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
this.splitForm.Panel2.Controls.Add(this.C18_00__COREP___ctrl0);
this.splitForm.Panel2.AutoScroll = true;
this.splitForm.Size = new System.Drawing.Size(809, 1191);
this.splitForm.SplitterDistance = 0;
this.splitForm.Panel1Collapsed = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.splitForm);
            this.Controls.Add(this.pnlTop);
            this.Name = "C18_00__COREP__"; 
            this.Size = new System.Drawing.Size(1134, 120); 
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyPageTextBox SolvencyPageTextBox0;
private SolvencyPanel pnlTop;
private C18_00__COREP___ctrl C18_00__COREP___ctrl0;
private SolvencySplitContainer splitForm;

   }
}

