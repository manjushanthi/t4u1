namespace SolvencyII.DataTypeValidation.DialogUI
{
    partial class DataValidationProgress
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblProgressStep = new System.Windows.Forms.Label();
            this.lblProcessing = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 32);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(665, 21);
            this.progressBar1.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(598, 121);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblProgressStep
            // 
            this.lblProgressStep.AutoSize = true;
            this.lblProgressStep.Location = new System.Drawing.Point(12, 67);
            this.lblProgressStep.Name = "lblProgressStep";
            this.lblProgressStep.Size = new System.Drawing.Size(35, 13);
            this.lblProgressStep.TabIndex = 3;
            this.lblProgressStep.Text = "label1";
            // 
            // lblProcessing
            // 
            this.lblProcessing.AutoSize = true;
            this.lblProcessing.Location = new System.Drawing.Point(9, 13);
            this.lblProcessing.Name = "lblProcessing";
            this.lblProcessing.Size = new System.Drawing.Size(83, 13);
            this.lblProcessing.TabIndex = 4;
            this.lblProcessing.Text = "Processing........";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(655, 13);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(22, 13);
            this.lblPercent.TabIndex = 5;
            this.lblPercent.Text = "Val";
            // 
            // DataValidationProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(683, 164);
            this.ControlBox = false;
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblProcessing);
            this.Controls.Add(this.lblProgressStep);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataValidationProgress";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validate container (current report) - Progress";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblProgressStep;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label lblProcessing;
    }
}