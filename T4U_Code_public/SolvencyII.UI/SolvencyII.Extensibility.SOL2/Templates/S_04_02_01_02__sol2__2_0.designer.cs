using System.Windows.Forms;
using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class S_04_02_01_02__sol2__2_0 
   { 
      private void InitializeComponent() 
      { 
this.solvencyLabel0 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel1 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel2 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel3 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.solvencyLabel4 = new SolvencyII.UI.Shared.Controls.SolvencyLabel();
this.S_04_02_01_02__sol2__2_0_ctrl0 = new S_04_02_01_02__sol2__2_0_ctrl();
this.dr_Main = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();
            this.SuspendLayout(); 

//
// solvencyLabel0
//
this.solvencyLabel0.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel0.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel0.Location = new System.Drawing.Point(10,143);
this.solvencyLabel0.Name = "solvencyLabel0";
this.solvencyLabel0.OrdinateID_Label = 1667;
this.solvencyLabel0.Size = new System.Drawing.Size(261, 30);
this.solvencyLabel0.TabIndex = 0;
this.solvencyLabel0.Text = "Frequency of claims for Motor Vehicle Liability (except carrier's liability)" ;
this.solvencyLabel0.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel1
//
this.solvencyLabel1.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel1.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel1.Location = new System.Drawing.Point(285,143);
this.solvencyLabel1.Name = "solvencyLabel1";
this.solvencyLabel1.OrdinateID_Label = 0;
this.solvencyLabel1.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel1.TabIndex = 1;
this.solvencyLabel1.Text = "R0020" ;
this.solvencyLabel1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel2
//
this.solvencyLabel2.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel2.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel2.Location = new System.Drawing.Point(10,176);
this.solvencyLabel2.Name = "solvencyLabel2";
this.solvencyLabel2.OrdinateID_Label = 1668;
this.solvencyLabel2.Size = new System.Drawing.Size(261, 30);
this.solvencyLabel2.TabIndex = 2;
this.solvencyLabel2.Text = "Average cost of claims for Motor Vehicle Liability (except carrier's liability)" ;
this.solvencyLabel2.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel3
//
this.solvencyLabel3.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel3.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel3.Location = new System.Drawing.Point(285,176);
this.solvencyLabel3.Name = "solvencyLabel3";
this.solvencyLabel3.OrdinateID_Label = 0;
this.solvencyLabel3.Size = new System.Drawing.Size(50, 13);
this.solvencyLabel3.TabIndex = 3;
this.solvencyLabel3.Text = "R0030" ;
this.solvencyLabel3.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// solvencyLabel4
//
this.solvencyLabel4.BackColor = System.Drawing.Color.Transparent;
this.solvencyLabel4.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
this.solvencyLabel4.Location = new System.Drawing.Point(10,213);
this.solvencyLabel4.Name = "solvencyLabel4";
this.solvencyLabel4.OrdinateID_Label = 0;
this.solvencyLabel4.Size = new System.Drawing.Size(108, 13);
this.solvencyLabel4.TabIndex = 4;
this.solvencyLabel4.Text = "." ;
this.solvencyLabel4.TextAlign = System.Drawing.ContentAlignment.TopLeft;
//
// S_04_02_01_02__sol2__2_0_ctrl0
//
this.S_04_02_01_02__sol2__2_0_ctrl0.Location = new System.Drawing.Point(0,0);
this.S_04_02_01_02__sol2__2_0_ctrl0.Name = "S_04_02_01_02__sol2__2_0_ctrl0";
this.S_04_02_01_02__sol2__2_0_ctrl0.Size = new System.Drawing.Size(274, 193);
//
// dr_Main
//
this.dr_Main.Size = new System.Drawing.Size(290, 225);
this.dr_Main.Location = new System.Drawing.Point(339,0);
this.dr_Main.Name = "dr_Main";
this.dr_Main.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
this.dr_Main.LayoutStyle = Microsoft.VisualBasic.PowerPacks.DataRepeaterLayoutStyles.Horizontal;
//
// dr_Main.ItemTemplate
//
//
// dr_Main.ItemTemplate
//
this.dr_Main.ItemTemplate.Controls.Add(this.S_04_02_01_02__sol2__2_0_ctrl0);
this.dr_Main.ItemTemplate.AutoScroll = true;
this.dr_Main.ItemTemplate.Size = new System.Drawing.Size(284, 219);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.solvencyLabel0);
this.Controls.Add(this.solvencyLabel1);
this.Controls.Add(this.solvencyLabel2);
this.Controls.Add(this.solvencyLabel3);
this.Controls.Add(this.solvencyLabel4);
this.Controls.Add(this.dr_Main);
            this.Name = "S_04_02_01_02__sol2__2_0"; 
            this.Size = new System.Drawing.Size(599, 274); 
            this.Load += new System.EventHandler(this.Repeater_Load);
            this.Resize += new System.EventHandler(this.Repeater_Resize);
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyLabel solvencyLabel0;
private SolvencyLabel solvencyLabel1;
private SolvencyLabel solvencyLabel2;
private SolvencyLabel solvencyLabel3;
private SolvencyLabel solvencyLabel4;
private S_04_02_01_02__sol2__2_0_ctrl S_04_02_01_02__sol2__2_0_ctrl0;
private SolvencyDataRepeater dr_Main;

   }
}

