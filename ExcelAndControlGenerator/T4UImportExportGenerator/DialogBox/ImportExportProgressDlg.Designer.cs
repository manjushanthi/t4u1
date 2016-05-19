namespace T4UImportExportGenerator.DialogBox
{
    partial class ImportExportProgressDlg
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
            this.label5 = new System.Windows.Forms.Label();
            this.lstStatus = new System.Windows.Forms.ListBox();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.prbStatus = new System.Windows.Forms.ProgressBar();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 58);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Tables completed";
            // 
            // lstStatus
            // 
            this.lstStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstStatus.FormattingEnabled = true;
            this.lstStatus.Location = new System.Drawing.Point(7, 87);
            this.lstStatus.MultiColumn = true;
            this.lstStatus.Name = "lstStatus";
            this.lstStatus.Size = new System.Drawing.Size(725, 264);
            this.lstStatus.TabIndex = 17;
            // 
            // lblPercent
            // 
            this.lblPercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPercent.Location = new System.Drawing.Point(582, 10);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPercent.Size = new System.Drawing.Size(156, 13);
            this.lblPercent.TabIndex = 16;
            this.lblPercent.Text = "label4";
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.Location = new System.Drawing.Point(4, 10);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(35, 13);
            this.lblProcessing.TabIndex = 15;
            this.lblProcessing.Text = "label3";
            // 
            // prbStatus
            // 
            this.prbStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prbStatus.Location = new System.Drawing.Point(7, 30);
            this.prbStatus.Name = "prbStatus";
            this.prbStatus.Size = new System.Drawing.Size(730, 14);
            this.prbStatus.TabIndex = 14;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(662, 390);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 19;
            this.btnOK.Text = "Close";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // ImportExportProgressDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 425);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lstStatus);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblProcessing);
            this.Controls.Add(this.prbStatus);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportExportProgressDlg";
            this.ShowInTaskbar = false;
            this.Text = "Generating T4U template";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox lstStatus;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblProcessing;
        private System.Windows.Forms.ProgressBar prbStatus;
        private System.Windows.Forms.Button btnOK;
    }
}