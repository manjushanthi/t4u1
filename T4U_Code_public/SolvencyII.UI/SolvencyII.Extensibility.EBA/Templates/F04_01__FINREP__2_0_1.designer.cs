using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class F04_01__FINREP__2_0_1 
   { 
      private void InitializeComponent() 
      { 
this.F04_01__FINREP__2_0_1_ctrl0 = new F04_01__FINREP__2_0_1_ctrl();
this.splitForm = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();
            this.SuspendLayout(); 

//
// F04_01__FINREP__2_0_1_ctrl0
//
this.F04_01__FINREP__2_0_1_ctrl0.Location = new System.Drawing.Point(0,0);
this.F04_01__FINREP__2_0_1_ctrl0.Name = "F04_01__FINREP__2_0_1_ctrl0";
this.F04_01__FINREP__2_0_1_ctrl0.Size = new System.Drawing.Size(274, 456);
//
// splitForm
//
this.splitForm.Dock = System.Windows.Forms.DockStyle.Fill;
this.splitForm.Location = new System.Drawing.Point(0,0);
this.splitForm.Name = "splitForm";
this.splitForm.Orientation = System.Windows.Forms.Orientation.Vertical;
//
// splitForm.Panel1
//
//
// splitForm.Panel2
//
this.splitForm.Panel2.Controls.Add(this.F04_01__FINREP__2_0_1_ctrl0);
this.splitForm.Panel2.AutoScroll = true;
this.splitForm.Size = new System.Drawing.Size(777, 456);
this.splitForm.SplitterDistance = 0;
this.splitForm.Panel1Collapsed = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.splitForm);
            this.Name = "F04_01__FINREP__2_0_1"; 
            this.Size = new System.Drawing.Size(532, 120); 
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private F04_01__FINREP__2_0_1_ctrl F04_01__FINREP__2_0_1_ctrl0;
private SolvencySplitContainer splitForm;

   }
}

