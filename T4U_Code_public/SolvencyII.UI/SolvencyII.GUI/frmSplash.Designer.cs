namespace SolvencyII.GUI
{
    partial class frmSplash
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSplash));
            this.lblAbout = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlUp = new System.Windows.Forms.Panel();
            this.lblMainVersion = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCheckForUpdates = new System.Windows.Forms.Button();
            this.pnlUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAbout
            // 
            this.lblAbout.Location = new System.Drawing.Point(146, 395);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(297, 162);
            this.lblAbout.TabIndex = 7;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(255, 562);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(91, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlUp
            // 
            this.pnlUp.Controls.Add(this.lblMainVersion);
            this.pnlUp.Controls.Add(this.pictureBox4);
            this.pnlUp.Controls.Add(this.pictureBox3);
            this.pnlUp.Controls.Add(this.pictureBox2);
            this.pnlUp.Controls.Add(this.pictureBox1);
            this.pnlUp.Controls.Add(this.progressBar1);
            this.pnlUp.Location = new System.Drawing.Point(100, 26);
            this.pnlUp.Name = "pnlUp";
            this.pnlUp.Size = new System.Drawing.Size(393, 345);
            this.pnlUp.TabIndex = 9;
            // 
            // lblMainVersion
            // 
            this.lblMainVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMainVersion.Location = new System.Drawing.Point(73, 320);
            this.lblMainVersion.Name = "lblMainVersion";
            this.lblMainVersion.Size = new System.Drawing.Size(258, 24);
            this.lblMainVersion.TabIndex = 12;
            this.lblMainVersion.Text = "Version 2.1.1.1";
            this.lblMainVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::SolvencyII.GUI.Properties.Resources.xbrl_logo;
            this.pictureBox4.Location = new System.Drawing.Point(141, 254);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(182, 63);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 11;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::SolvencyII.GUI.Properties.Resources.t4u;
            this.pictureBox3.Location = new System.Drawing.Point(169, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(64, 64);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 10;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox2.Image = global::SolvencyII.GUI.Properties.Resources.EIOPA_Logo;
            this.pictureBox2.Location = new System.Drawing.Point(73, 59);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(250, 164);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SolvencyII.GUI.Properties.Resources.xbrt;
            this.pictureBox1.Location = new System.Drawing.Point(73, 254);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(62, 63);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar1.Location = new System.Drawing.Point(45, 229);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(303, 16);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 7;
            // 
            // btnCheckForUpdates
            // 
            this.btnCheckForUpdates.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCheckForUpdates.Location = new System.Drawing.Point(241, 530);
            this.btnCheckForUpdates.Name = "btnCheckForUpdates";
            this.btnCheckForUpdates.Size = new System.Drawing.Size(113, 27);
            this.btnCheckForUpdates.TabIndex = 10;
            this.btnCheckForUpdates.Text = "Check for Updates";
            this.btnCheckForUpdates.UseVisualStyleBackColor = true;
            this.btnCheckForUpdates.Visible = false;
            this.btnCheckForUpdates.Click += new System.EventHandler(this.btnCheckForUpdates_Click);
            // 
            // frmSplash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.CancelButton = this.btnOK;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(587, 668);
            this.ControlBox = false;
            this.Controls.Add(this.btnCheckForUpdates);
            this.Controls.Add(this.lblAbout);
            this.Controls.Add(this.pnlUp);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSplash";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSplash";
            this.Load += new System.EventHandler(this.frmSplash_Load);
            this.Click += new System.EventHandler(this.frmSplash_DoubleClick);
            this.DoubleClick += new System.EventHandler(this.frmSplash_DoubleClick);
            this.pnlUp.ResumeLayout(false);
            this.pnlUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lblAbout;
        public System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlUp;
        private System.Windows.Forms.Label lblMainVersion;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.Button btnCheckForUpdates;
    }
}