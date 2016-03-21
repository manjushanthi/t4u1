namespace SolvencyII.Validation.UI
{
    partial class ArelleValidationDetailLog
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
            this.rtbDetailLog = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbDetailLog
            // 
            this.rtbDetailLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDetailLog.Location = new System.Drawing.Point(0, 0);
            this.rtbDetailLog.Name = "rtbDetailLog";
            this.rtbDetailLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbDetailLog.Size = new System.Drawing.Size(639, 482);
            this.rtbDetailLog.TabIndex = 0;
            this.rtbDetailLog.Text = "";
            // 
            // ArelleValidationDetailLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 482);
            this.Controls.Add(this.rtbDetailLog);
            this.Name = "ArelleValidationDetailLog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Arelle Validation Detail Log";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbDetailLog;
    }
}