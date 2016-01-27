using System.Windows.Forms;
using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class SPV_03_01_20_01__sol2__2_0 
   { 
      private void InitializeComponent() 
      { 
this.SPV_03_01_20_01__sol2__2_0_ctrl0 = new SPV_03_01_20_01__sol2__2_0_ctrl();
this.dr_Main = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();
            this.SuspendLayout(); 

//
// SPV_03_01_20_01__sol2__2_0_ctrl0
//
this.SPV_03_01_20_01__sol2__2_0_ctrl0.Location = new System.Drawing.Point(0,0);
this.SPV_03_01_20_01__sol2__2_0_ctrl0.Name = "SPV_03_01_20_01__sol2__2_0_ctrl0";
this.SPV_03_01_20_01__sol2__2_0_ctrl0.Size = new System.Drawing.Size(447, 101);
//
// dr_Main
//
this.dr_Main.Size = new System.Drawing.Size(463, 133);
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
this.dr_Main.ItemTemplate.Controls.Add(this.SPV_03_01_20_01__sol2__2_0_ctrl0);
this.dr_Main.ItemTemplate.AutoScroll = true;
this.dr_Main.ItemTemplate.Size = new System.Drawing.Size(457, 127);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.dr_Main);
            this.Name = "SPV_03_01_20_01__sol2__2_0"; 
            this.Size = new System.Drawing.Size(457, 120); 
            this.Load += new System.EventHandler(this.Repeater_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SPV_03_01_20_01__sol2__2_0_ctrl SPV_03_01_20_01__sol2__2_0_ctrl0;
private SolvencyDataRepeater dr_Main;

   }
}

