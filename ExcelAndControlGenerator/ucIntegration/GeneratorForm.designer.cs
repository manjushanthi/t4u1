namespace ucIntegration
{
    partial class GeneratorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openXBRTContainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnCreate = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPoco = new System.Windows.Forms.Button();
            this.txtPoco = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblSelectedTemplate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxMergedTemplates = new System.Windows.Forms.CheckBox();
            this.btnHeader = new System.Windows.Forms.Button();
            this.chkCreateProject = new System.Windows.Forms.CheckBox();
            this.btnMultipleNoTreeView = new System.Windows.Forms.Button();
            this.btnExploreFolder = new System.Windows.Forms.Button();
            this.chkiOS = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMulti = new System.Windows.Forms.TextBox();
            this.btnSaveAs = new System.Windows.Forms.Button();
            this.btnMultiple = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numVersion = new System.Windows.Forms.NumericUpDown();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVersion)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(861, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openXBRTContainerToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openXBRTContainerToolStripMenuItem
            // 
            this.openXBRTContainerToolStripMenuItem.Name = "openXBRTContainerToolStripMenuItem";
            this.openXBRTContainerToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.openXBRTContainerToolStripMenuItem.Text = "Open XBRT Container";
            this.openXBRTContainerToolStripMenuItem.Click += new System.EventHandler(this.openXBRTContainerToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 476);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(861, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(18, 81);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(160, 23);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "Create Single Template";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox4);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(861, 452);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 2;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(0, 0);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(286, 452);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPoco);
            this.groupBox4.Controls.Add(this.txtPoco);
            this.groupBox4.Location = new System.Drawing.Point(18, 368);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(536, 81);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Poco Generation";
            // 
            // btnPoco
            // 
            this.btnPoco.Location = new System.Drawing.Point(18, 45);
            this.btnPoco.Name = "btnPoco";
            this.btnPoco.Size = new System.Drawing.Size(160, 23);
            this.btnPoco.TabIndex = 13;
            this.btnPoco.Text = "Creat Poco Objects";
            this.btnPoco.UseVisualStyleBackColor = true;
            this.btnPoco.Click += new System.EventHandler(this.btnPoco_Click);
            // 
            // txtPoco
            // 
            this.txtPoco.Location = new System.Drawing.Point(18, 19);
            this.txtPoco.Name = "txtPoco";
            this.txtPoco.Size = new System.Drawing.Size(494, 20);
            this.txtPoco.TabIndex = 13;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblSelectedTemplate);
            this.groupBox3.Location = new System.Drawing.Point(18, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(536, 57);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Single Selected Template";
            // 
            // lblSelectedTemplate
            // 
            this.lblSelectedTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSelectedTemplate.Location = new System.Drawing.Point(3, 16);
            this.lblSelectedTemplate.Name = "lblSelectedTemplate";
            this.lblSelectedTemplate.Padding = new System.Windows.Forms.Padding(2);
            this.lblSelectedTemplate.Size = new System.Drawing.Size(530, 38);
            this.lblSelectedTemplate.TabIndex = 4;
            this.lblSelectedTemplate.Text = "None";
            this.lblSelectedTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxMergedTemplates);
            this.groupBox1.Controls.Add(this.btnHeader);
            this.groupBox1.Controls.Add(this.chkCreateProject);
            this.groupBox1.Controls.Add(this.btnMultipleNoTreeView);
            this.groupBox1.Controls.Add(this.btnExploreFolder);
            this.groupBox1.Controls.Add(this.chkiOS);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnCreate);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMulti);
            this.groupBox1.Controls.Add(this.btnSaveAs);
            this.groupBox1.Controls.Add(this.btnMultiple);
            this.groupBox1.Location = new System.Drawing.Point(18, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(536, 222);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Action";
            // 
            // checkBoxMergedTemplates
            // 
            this.checkBoxMergedTemplates.AutoSize = true;
            this.checkBoxMergedTemplates.Location = new System.Drawing.Point(189, 130);
            this.checkBoxMergedTemplates.Name = "checkBoxMergedTemplates";
            this.checkBoxMergedTemplates.Size = new System.Drawing.Size(139, 17);
            this.checkBoxMergedTemplates.TabIndex = 17;
            this.checkBoxMergedTemplates.Text = "Generate merged tables";
            this.checkBoxMergedTemplates.UseVisualStyleBackColor = true;
            // 
            // btnHeader
            // 
            this.btnHeader.Location = new System.Drawing.Point(184, 81);
            this.btnHeader.Name = "btnHeader";
            this.btnHeader.Size = new System.Drawing.Size(160, 23);
            this.btnHeader.TabIndex = 16;
            this.btnHeader.Text = "Header Test";
            this.btnHeader.UseVisualStyleBackColor = true;
            this.btnHeader.Click += new System.EventHandler(this.btnHeader_Click);
            // 
            // chkCreateProject
            // 
            this.chkCreateProject.AutoSize = true;
            this.chkCreateProject.Location = new System.Drawing.Point(334, 129);
            this.chkCreateProject.Name = "chkCreateProject";
            this.chkCreateProject.Size = new System.Drawing.Size(93, 17);
            this.chkCreateProject.TabIndex = 15;
            this.chkCreateProject.Text = "Create Project";
            this.chkCreateProject.UseVisualStyleBackColor = true;
            // 
            // btnMultipleNoTreeView
            // 
            this.btnMultipleNoTreeView.Location = new System.Drawing.Point(158, 187);
            this.btnMultipleNoTreeView.Name = "btnMultipleNoTreeView";
            this.btnMultipleNoTreeView.Size = new System.Drawing.Size(134, 23);
            this.btnMultipleNoTreeView.TabIndex = 14;
            this.btnMultipleNoTreeView.Text = "Spec NonTree";
            this.btnMultipleNoTreeView.UseVisualStyleBackColor = true;
            this.btnMultipleNoTreeView.Click += new System.EventHandler(this.btnMultipleNoTreeView_Click);
            // 
            // btnExploreFolder
            // 
            this.btnExploreFolder.Location = new System.Drawing.Point(432, 187);
            this.btnExploreFolder.Name = "btnExploreFolder";
            this.btnExploreFolder.Size = new System.Drawing.Size(92, 23);
            this.btnExploreFolder.TabIndex = 13;
            this.btnExploreFolder.Text = "Explore Folder";
            this.btnExploreFolder.UseVisualStyleBackColor = true;
            this.btnExploreFolder.Click += new System.EventHandler(this.btnExploreFolder_Click);
            // 
            // chkiOS
            // 
            this.chkiOS.AutoSize = true;
            this.chkiOS.Location = new System.Drawing.Point(432, 129);
            this.chkiOS.Name = "chkiOS";
            this.chkiOS.Size = new System.Drawing.Size(84, 17);
            this.chkiOS.TabIndex = 12;
            this.chkiOS.Text = "iOS Controls";
            this.chkiOS.UseVisualStyleBackColor = true;
            this.chkiOS.CheckedChanged += new System.EventHandler(this.chkiOS_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(334, 187);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(92, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(18, 44);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(456, 20);
            this.txtPath.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Folder for new templates";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Path for single template";
            // 
            // txtMulti
            // 
            this.txtMulti.Location = new System.Drawing.Point(18, 152);
            this.txtMulti.Name = "txtMulti";
            this.txtMulti.Size = new System.Drawing.Size(494, 20);
            this.txtMulti.TabIndex = 9;
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.Location = new System.Drawing.Point(481, 42);
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(31, 23);
            this.btnSaveAs.TabIndex = 7;
            this.btnSaveAs.Text = "...";
            this.btnSaveAs.UseVisualStyleBackColor = true;
            this.btnSaveAs.Click += new System.EventHandler(this.btnSaveAs_Click);
            // 
            // btnMultiple
            // 
            this.btnMultiple.Location = new System.Drawing.Point(18, 187);
            this.btnMultiple.Name = "btnMultiple";
            this.btnMultiple.Size = new System.Drawing.Size(134, 23);
            this.btnMultiple.TabIndex = 8;
            this.btnMultiple.Text = "Create Multiple Template";
            this.btnMultiple.UseVisualStyleBackColor = true;
            this.btnMultiple.Click += new System.EventHandler(this.btnMultiple_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numVersion);
            this.groupBox2.Location = new System.Drawing.Point(18, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(536, 57);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Settings";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Enabled = false;
            this.checkBox1.Location = new System.Drawing.Point(518, 26);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(159, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Single template name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(286, 23);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(229, 20);
            this.txtName.TabIndex = 13;
            this.txtName.Text = "singleControl.cs";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Version";
            // 
            // numVersion
            // 
            this.numVersion.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVersion.Location = new System.Drawing.Point(85, 23);
            this.numVersion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numVersion.Name = "numVersion";
            this.numVersion.Size = new System.Drawing.Size(53, 20);
            this.numVersion.TabIndex = 11;
            this.numVersion.Value = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            // 
            // GeneratorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 498);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GeneratorForm";
            this.Text = "Form Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numVersion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripMenuItem openXBRTContainerToolStripMenuItem;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblSelectedTemplate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnSaveAs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtMulti;
        private System.Windows.Forms.Button btnMultiple;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numVersion;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox chkiOS;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnPoco;
        private System.Windows.Forms.TextBox txtPoco;
        private System.Windows.Forms.Button btnExploreFolder;
        private System.Windows.Forms.Button btnMultipleNoTreeView;
        private System.Windows.Forms.CheckBox chkCreateProject;
        private System.Windows.Forms.Button btnHeader;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxMergedTemplates;
    }
}

