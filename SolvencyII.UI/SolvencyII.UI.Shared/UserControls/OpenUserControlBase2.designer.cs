using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.UI.Shared.UserControls
{
    public partial class OpenUserControlBase2
    {
        private void InitializeComponent()
        {
            this.virtualObjectListView1 = new BrightIdeasSoftware.VirtualObjectListView();
            this.btnInsert = new System.Windows.Forms.Button();
            this.dataListView1 = new BrightIdeasSoftware.DataListView();
            this.btnFiled = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.virtualObjectListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).BeginInit();
            this.SuspendLayout();
            // 
            // virtualObjectListView1
            // 
            this.virtualObjectListView1.Location = new System.Drawing.Point(0, 31);
            this.virtualObjectListView1.Name = "virtualObjectListView1";
            this.virtualObjectListView1.ShowGroups = false;
            this.virtualObjectListView1.Size = new System.Drawing.Size(117, 87);
            this.virtualObjectListView1.TabIndex = 0;
            this.virtualObjectListView1.UseCompatibleStateImageBehavior = false;
            this.virtualObjectListView1.View = System.Windows.Forms.View.Details;
            this.virtualObjectListView1.VirtualMode = true;
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(3, 3);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(75, 23);
            this.btnInsert.TabIndex = 1;
            this.btnInsert.Text = "Add New";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // dataListView1
            // 
            this.dataListView1.DataSource = null;
            this.dataListView1.Location = new System.Drawing.Point(0, 31);
            this.dataListView1.Name = "dataListView1";
            this.dataListView1.Size = new System.Drawing.Size(133, 108);
            this.dataListView1.TabIndex = 2;
            this.dataListView1.UseCompatibleStateImageBehavior = false;
            this.dataListView1.View = System.Windows.Forms.View.Details;
            // 
            // btnFiled
            // 
            this.btnFiled.Location = new System.Drawing.Point(84, 3);
            this.btnFiled.Name = "btnFiled";
            this.btnFiled.Size = new System.Drawing.Size(124, 23);
            this.btnFiled.TabIndex = 7;
            this.btnFiled.Text = "Marked as Reported";
            this.btnFiled.UseVisualStyleBackColor = true;
            this.btnFiled.Click += new System.EventHandler(this.btnFiled_Click);
            // 
            // OpenUserControlBase2
            // 
            this.Controls.Add(this.btnFiled);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.virtualObjectListView1);
            this.Controls.Add(this.dataListView1);
            this.Name = "OpenUserControlBase2";
            this.Size = new System.Drawing.Size(277, 142);
            ((System.ComponentModel.ISupportInitialize)(this.virtualObjectListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataListView1)).EndInit();
            this.ResumeLayout(false);

        }

        private BrightIdeasSoftware.VirtualObjectListView virtualObjectListView1;
        private System.Windows.Forms.Button btnInsert;
        private BrightIdeasSoftware.DataListView dataListView1;
        private System.Windows.Forms.Button btnFiled;
    }
}
