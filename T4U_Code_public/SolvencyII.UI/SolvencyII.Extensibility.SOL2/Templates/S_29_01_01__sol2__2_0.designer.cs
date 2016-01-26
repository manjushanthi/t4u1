using System.Windows.Forms;
using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_29_01_01__sol2__2_0 
   { 
      private void InitializeComponent() 
      { 
this.S_29_01_01__sol2__2_0_ctrl0 = new S_29_01_01__sol2__2_0_ctrl();
this.pnlContainer = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
this.dr_Main = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();
            this.SuspendLayout(); 

//
// S_29_01_01__sol2__2_0_ctrl0
//
this.S_29_01_01__sol2__2_0_ctrl0.Location = new System.Drawing.Point(0,0);
this.S_29_01_01__sol2__2_0_ctrl0.Name = "S_29_01_01__sol2__2_0_ctrl0";
this.S_29_01_01__sol2__2_0_ctrl0.Size = new System.Drawing.Size(666, 936);
//
// pnlContainer
//
this.pnlContainer.Controls.Add(this.S_29_01_01__sol2__2_0_ctrl0);
this.pnlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
this.pnlContainer.Location = new System.Drawing.Point(0,0);
this.pnlContainer.Name = "pnlContainer";
this.pnlContainer.Size = new System.Drawing.Size(666, 936);
this.pnlContainer.AutoScroll = true;
//
// dr_Main
//
this.dr_Main.Size = new System.Drawing.Size(712, 968);
this.dr_Main.Location = new System.Drawing.Point(0,0);
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
this.dr_Main.ItemTemplate.Size = new System.Drawing.Size(706, 962);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.dr_Main);
            this.Name = "S_29_01_01__sol2__2_0"; 
            this.Size = new System.Drawing.Size(1138, 946); 
            this.Load += new System.EventHandler(this.Repeater_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private S_29_01_01__sol2__2_0_ctrl S_29_01_01__sol2__2_0_ctrl0;
private SolvencyPanel pnlContainer;
private SolvencyDataRepeater dr_Main;

   }
}
