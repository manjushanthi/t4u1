using SolvencyII.Domain.ENumerators; 
using SolvencyII.UI.Shared.Controls; 

namespace SolvencyII.UI.UserControls 
{ 
   partial class C08_02__COREP__ 
   { 
      private void InitializeComponent() 
      { 
this.SolvencyPageTextBox0 = new SolvencyII.UI.Shared.Controls.SolvencyPageTextBox();
this.SolvencyPageTextBox1 = new SolvencyII.UI.Shared.Controls.SolvencyPageTextBox();
this.SolvencyPageTextBox2 = new SolvencyII.UI.Shared.Controls.SolvencyPageTextBox();
this.SolvencyPageTextBox3 = new SolvencyII.UI.Shared.Controls.SolvencyPageTextBox();
this.pnlTop = new SolvencyII.UI.Shared.Controls.SolvencyPanel();
            this.SuspendLayout(); 

//
// SolvencyPageTextBox0
//
this.SolvencyPageTextBox0.Location = new System.Drawing.Point(10,30);
this.SolvencyPageTextBox0.Name = "SolvencyPageTextBox0";
this.SolvencyPageTextBox0.Size = new System.Drawing.Size(250, 13);
this.SolvencyPageTextBox0.TableNames = "T__C_08_02__COREP__2_0_1";
this.SolvencyPageTextBox0.ColName = "PAGEEBA_APR";
this.SolvencyPageTextBox0.FixedDimension = true;
//
// SolvencyPageTextBox1
//
this.SolvencyPageTextBox1.Location = new System.Drawing.Point(267,30);
this.SolvencyPageTextBox1.Name = "SolvencyPageTextBox1";
this.SolvencyPageTextBox1.Size = new System.Drawing.Size(250, 13);
this.SolvencyPageTextBox1.TableNames = "T__C_08_02__COREP__2_0_1";
this.SolvencyPageTextBox1.ColName = "PAGEEBA_CPS";
this.SolvencyPageTextBox1.FixedDimension = true;
//
// SolvencyPageTextBox2
//
this.SolvencyPageTextBox2.Location = new System.Drawing.Point(524,30);
this.SolvencyPageTextBox2.Name = "SolvencyPageTextBox2";
this.SolvencyPageTextBox2.Size = new System.Drawing.Size(250, 13);
this.SolvencyPageTextBox2.TableNames = "T__C_08_02__COREP__2_0_1";
this.SolvencyPageTextBox2.ColName = "PAGEEBA_CPZ";
this.SolvencyPageTextBox2.FixedDimension = true;
//
// SolvencyPageTextBox3
//
this.SolvencyPageTextBox3.Location = new System.Drawing.Point(781,30);
this.SolvencyPageTextBox3.Name = "SolvencyPageTextBox3";
this.SolvencyPageTextBox3.Size = new System.Drawing.Size(250, 13);
this.SolvencyPageTextBox3.TableNames = "T__C_08_02__COREP__2_0_1";
this.SolvencyPageTextBox3.ColName = "PAGEEBA_EXC";
this.SolvencyPageTextBox3.FixedDimension = true;
//
// pnlTop
//
this.pnlTop.Controls.Add(this.SolvencyPageTextBox0);
this.pnlTop.Controls.Add(this.SolvencyPageTextBox1);
this.pnlTop.Controls.Add(this.SolvencyPageTextBox2);
this.pnlTop.Controls.Add(this.SolvencyPageTextBox3);
this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
this.pnlTop.Location = new System.Drawing.Point(0,0);
this.pnlTop.Name = "pnlTop";
this.pnlTop.Size = new System.Drawing.Size(777, 80);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; 
this.Controls.Add(this.pnlTop);
            this.Name = "C08_02__COREP__"; 
            this.Size = new System.Drawing.Size(40, 140); 
            this.ResumeLayout(false); 
            this.PerformLayout(); 

      } 
private SolvencyPageTextBox SolvencyPageTextBox0;
private SolvencyPageTextBox SolvencyPageTextBox1;
private SolvencyPageTextBox SolvencyPageTextBox2;
private SolvencyPageTextBox SolvencyPageTextBox3;
private SolvencyPanel pnlTop;

   }
}

