namespace SolvencyII.GUI
{
    partial class frmAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAbout));
            this.btnUpdate = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblDatabaseVersionPREP = new System.Windows.Forms.Label();
            this.lblExcelTemplateVersionPREP = new System.Windows.Forms.Label();
            this.lblFrameWorkVersion = new System.Windows.Forms.Label();
            this.pbLoader = new System.Windows.Forms.PictureBox();
            this.lblExcelTemplateVersion = new System.Windows.Forms.Label();
            this.lblDatabaseVersion = new System.Windows.Forms.Label();
            this.pbInstalledpath = new System.Windows.Forms.PictureBox();
            this.txtpath = new System.Windows.Forms.TextBox();
            this.lblClickOnceVersion = new System.Windows.Forms.Label();
            this.lblArelleVersion = new System.Windows.Forms.Label();
            this.lblT4Uversion = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInstalledpath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(3, 541);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(107, 28);
            this.btnUpdate.TabIndex = 15;
            this.btnUpdate.Text = "Check for Updates";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.AliceBlue;
            this.groupBox2.Controls.Add(this.lblDatabaseVersionPREP);
            this.groupBox2.Controls.Add(this.lblExcelTemplateVersionPREP);
            this.groupBox2.Controls.Add(this.lblFrameWorkVersion);
            this.groupBox2.Controls.Add(this.pbLoader);
            this.groupBox2.Controls.Add(this.lblExcelTemplateVersion);
            this.groupBox2.Controls.Add(this.lblDatabaseVersion);
            this.groupBox2.Controls.Add(this.pbInstalledpath);
            this.groupBox2.Controls.Add(this.txtpath);
            this.groupBox2.Controls.Add(this.lblClickOnceVersion);
            this.groupBox2.Controls.Add(this.lblArelleVersion);
            this.groupBox2.Controls.Add(this.lblT4Uversion);
            this.groupBox2.Location = new System.Drawing.Point(3, 252);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(505, 283);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            // 
            // lblDatabaseVersionPREP
            // 
            this.lblDatabaseVersionPREP.AutoSize = true;
            this.lblDatabaseVersionPREP.Location = new System.Drawing.Point(6, 183);
            this.lblDatabaseVersionPREP.Name = "lblDatabaseVersionPREP";
            this.lblDatabaseVersionPREP.Size = new System.Drawing.Size(127, 13);
            this.lblDatabaseVersionPREP.TabIndex = 11;
            this.lblDatabaseVersionPREP.Text = "lblDatabaseVersionPREP";
            // 
            // lblExcelTemplateVersionPREP
            // 
            this.lblExcelTemplateVersionPREP.AutoSize = true;
            this.lblExcelTemplateVersionPREP.Location = new System.Drawing.Point(6, 155);
            this.lblExcelTemplateVersionPREP.Name = "lblExcelTemplateVersionPREP";
            this.lblExcelTemplateVersionPREP.Size = new System.Drawing.Size(151, 13);
            this.lblExcelTemplateVersionPREP.TabIndex = 10;
            this.lblExcelTemplateVersionPREP.Text = "lblExcelTemplateVersionPREP";
            // 
            // lblFrameWorkVersion
            // 
            this.lblFrameWorkVersion.AutoSize = true;
            this.lblFrameWorkVersion.Location = new System.Drawing.Point(6, 216);
            this.lblFrameWorkVersion.Name = "lblFrameWorkVersion";
            this.lblFrameWorkVersion.Size = new System.Drawing.Size(107, 13);
            this.lblFrameWorkVersion.TabIndex = 9;
            this.lblFrameWorkVersion.Text = "lblFrameWorkVersion";
            // 
            // pbLoader
            // 
            this.pbLoader.Image = global::SolvencyII.GUI.Properties.Resources.loading;
            this.pbLoader.Location = new System.Drawing.Point(173, 69);
            this.pbLoader.Name = "pbLoader";
            this.pbLoader.Size = new System.Drawing.Size(153, 160);
            this.pbLoader.TabIndex = 8;
            this.pbLoader.TabStop = false;
            // 
            // lblExcelTemplateVersion
            // 
            this.lblExcelTemplateVersion.AutoSize = true;
            this.lblExcelTemplateVersion.Location = new System.Drawing.Point(6, 124);
            this.lblExcelTemplateVersion.Name = "lblExcelTemplateVersion";
            this.lblExcelTemplateVersion.Size = new System.Drawing.Size(122, 13);
            this.lblExcelTemplateVersion.TabIndex = 7;
            this.lblExcelTemplateVersion.Text = "lblExcelTemplateVersion";
            // 
            // lblDatabaseVersion
            // 
            this.lblDatabaseVersion.AutoSize = true;
            this.lblDatabaseVersion.Location = new System.Drawing.Point(6, 96);
            this.lblDatabaseVersion.Name = "lblDatabaseVersion";
            this.lblDatabaseVersion.Size = new System.Drawing.Size(98, 13);
            this.lblDatabaseVersion.TabIndex = 6;
            this.lblDatabaseVersion.Text = "lblDatabaseVersion";
            // 
            // pbInstalledpath
            // 
            this.pbInstalledpath.Image = global::SolvencyII.GUI.Properties.Resources.folder_open;
            this.pbInstalledpath.Location = new System.Drawing.Point(460, 232);
            this.pbInstalledpath.Name = "pbInstalledpath";
            this.pbInstalledpath.Size = new System.Drawing.Size(39, 43);
            this.pbInstalledpath.TabIndex = 5;
            this.pbInstalledpath.TabStop = false;
            this.pbInstalledpath.Click += new System.EventHandler(this.pbInstalledpath_Click);
            // 
            // txtpath
            // 
            this.txtpath.BackColor = System.Drawing.Color.AliceBlue;
            this.txtpath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtpath.Location = new System.Drawing.Point(6, 246);
            this.txtpath.Multiline = true;
            this.txtpath.Name = "txtpath";
            this.txtpath.ReadOnly = true;
            this.txtpath.Size = new System.Drawing.Size(376, 29);
            this.txtpath.TabIndex = 4;
            this.txtpath.Text = "txtpath";
            // 
            // lblClickOnceVersion
            // 
            this.lblClickOnceVersion.AutoSize = true;
            this.lblClickOnceVersion.Location = new System.Drawing.Point(6, 69);
            this.lblClickOnceVersion.Name = "lblClickOnceVersion";
            this.lblClickOnceVersion.Size = new System.Drawing.Size(101, 13);
            this.lblClickOnceVersion.TabIndex = 2;
            this.lblClickOnceVersion.Text = "lblClickOnceVersion";
            // 
            // lblArelleVersion
            // 
            this.lblArelleVersion.AutoSize = true;
            this.lblArelleVersion.Location = new System.Drawing.Point(6, 44);
            this.lblArelleVersion.Name = "lblArelleVersion";
            this.lblArelleVersion.Size = new System.Drawing.Size(78, 13);
            this.lblArelleVersion.TabIndex = 1;
            this.lblArelleVersion.Text = "lblArelleVersion";
            // 
            // lblT4Uversion
            // 
            this.lblT4Uversion.AutoSize = true;
            this.lblT4Uversion.Location = new System.Drawing.Point(6, 16);
            this.lblT4Uversion.Name = "lblT4Uversion";
            this.lblT4Uversion.Size = new System.Drawing.Size(72, 13);
            this.lblT4Uversion.TabIndex = 0;
            this.lblT4Uversion.Text = "lblT4Uversion";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(433, 541);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 28);
            this.btnOk.TabIndex = 19;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox2.Image = global::SolvencyII.GUI.Properties.Resources.EIOPA_Logo;
            this.pictureBox2.Location = new System.Drawing.Point(122, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(250, 164);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SolvencyII.GUI.Properties.Resources.xbrt;
            this.pictureBox1.Location = new System.Drawing.Point(326, 183);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 63);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::SolvencyII.GUI.Properties.Resources.xbrl_logo;
            this.pictureBox4.Location = new System.Drawing.Point(161, 183);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(159, 63);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::SolvencyII.GUI.Properties.Resources.t4u;
            this.pictureBox3.Location = new System.Drawing.Point(91, 182);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(64, 64);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 11;
            this.pictureBox3.TabStop = false;
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(511, 578);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.pictureBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLoader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbInstalledpath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblT4Uversion;
        private System.Windows.Forms.Label lblArelleVersion;
        private System.Windows.Forms.Label lblClickOnceVersion;
        private System.Windows.Forms.TextBox txtpath;
        private System.Windows.Forms.PictureBox pbInstalledpath;
        private System.Windows.Forms.Label lblExcelTemplateVersion;
        private System.Windows.Forms.Label lblDatabaseVersion;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.PictureBox pbLoader;
        private System.Windows.Forms.Label lblExcelTemplateVersionPREP;
        private System.Windows.Forms.Label lblDatabaseVersionPREP;
        private System.Windows.Forms.Label lblFrameWorkVersion;
        
    }
}