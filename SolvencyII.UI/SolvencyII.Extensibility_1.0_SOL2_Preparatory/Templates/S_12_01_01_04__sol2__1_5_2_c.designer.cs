using System.Windows.Forms;
using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_12_01_01_04__sol2__1_5_2_c 
   { 
      private void InitializeComponent() 
      { 
this.S_12_01_01_04__sol2__1_5_2_c_ctrl0 = new S_12_01_01_04__sol2__1_5_2_c_ctrl();
this.dr_Main = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();
            this.SuspendLayout(); 

//
// S_12_01_01_04__sol2__1_5_2_c_ctrl0
//
this.S_12_01_01_04__sol2__1_5_2_c_ctrl0.Location = new System.Drawing.Point(0,0);
this.S_12_01_01_04__sol2__1_5_2_c_ctrl0.Name = "S_12_01_01_04__sol2__1_5_2_c_ctrl0";
this.S_12_01_01_04__sol2__1_5_2_c_ctrl0.Size = new System.Drawing.Size(2482, 179);
//
// dr_Main
//
this.dr_Main.Size = new System.Drawing.Size(2498, 211);
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
this.dr_Main.ItemTemplate.Controls.Add(this.S_12_01_01_04__sol2__1_5_2_c_ctrl0);
this.dr_Main.ItemTemplate.AutoScroll = true;
this.dr_Main.ItemTemplate.Size = new System.Drawing.Size(2492, 205);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.dr_Main);
            this.Name = "S_12_01_01_04__sol2__1_5_2_c"; 
            this.Size = new System.Drawing.Size(2492, 189); 
            this.Load += new System.EventHandler(this.Repeater_Load);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private S_12_01_01_04__sol2__1_5_2_c_ctrl S_12_01_01_04__sol2__1_5_2_c_ctrl0;
private SolvencyDataRepeater dr_Main;

   }
}

