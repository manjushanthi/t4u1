namespace SolvencyII.GUI
{
    partial class frmValidationError
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtScope = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtContext = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtValidationCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtErrorMessage = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFormula = new System.Windows.Forms.TextBox();
            this.Cells = new System.Windows.Forms.Label();
            this.txtCells = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Scope";
            // 
            // txtScope
            // 
            this.txtScope.Location = new System.Drawing.Point(58, 13);
            this.txtScope.Name = "txtScope";
            this.txtScope.ReadOnly = true;
            this.txtScope.Size = new System.Drawing.Size(100, 20);
            this.txtScope.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Context";
            // 
            // txtContext
            // 
            this.txtContext.Location = new System.Drawing.Point(58, 39);
            this.txtContext.Name = "txtContext";
            this.txtContext.ReadOnly = true;
            this.txtContext.Size = new System.Drawing.Size(396, 20);
            this.txtContext.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(164, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Validation code";
            // 
            // txtValidationCode
            // 
            this.txtValidationCode.Location = new System.Drawing.Point(250, 12);
            this.txtValidationCode.Name = "txtValidationCode";
            this.txtValidationCode.ReadOnly = true;
            this.txtValidationCode.Size = new System.Drawing.Size(100, 20);
            this.txtValidationCode.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Error message";
            // 
            // txtErrorMessage
            // 
            this.txtErrorMessage.Location = new System.Drawing.Point(88, 62);
            this.txtErrorMessage.Name = "txtErrorMessage";
            this.txtErrorMessage.ReadOnly = true;
            this.txtErrorMessage.Size = new System.Drawing.Size(366, 20);
            this.txtErrorMessage.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 95);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Formula";
            // 
            // txtFormula
            // 
            this.txtFormula.Location = new System.Drawing.Point(88, 88);
            this.txtFormula.Name = "txtFormula";
            this.txtFormula.ReadOnly = true;
            this.txtFormula.Size = new System.Drawing.Size(366, 20);
            this.txtFormula.TabIndex = 9;
            // 
            // Cells
            // 
            this.Cells.AutoSize = true;
            this.Cells.Location = new System.Drawing.Point(8, 114);
            this.Cells.Name = "Cells";
            this.Cells.Size = new System.Drawing.Size(29, 13);
            this.Cells.TabIndex = 10;
            this.Cells.Text = "Cells";
            // 
            // txtCells
            // 
            this.txtCells.Location = new System.Drawing.Point(88, 114);
            this.txtCells.Multiline = true;
            this.txtCells.Name = "txtCells";
            this.txtCells.ReadOnly = true;
            this.txtCells.Size = new System.Drawing.Size(422, 104);
            this.txtCells.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Expression";
            // 
            // txtExpression
            // 
            this.txtExpression.Location = new System.Drawing.Point(88, 224);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.ReadOnly = true;
            this.txtExpression.Size = new System.Drawing.Size(422, 100);
            this.txtExpression.TabIndex = 13;
            // 
            // frmValidationError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(522, 336);
            this.Controls.Add(this.txtExpression);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCells);
            this.Controls.Add(this.Cells);
            this.Controls.Add(this.txtFormula);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtErrorMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtValidationCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtContext);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtScope);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmValidationError";
            this.ShowInTaskbar = false;
            this.Text = "frmValidationError";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtScope;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtContext;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtValidationCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtErrorMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFormula;
        private System.Windows.Forms.Label Cells;
        private System.Windows.Forms.TextBox txtCells;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtExpression;
    }
}