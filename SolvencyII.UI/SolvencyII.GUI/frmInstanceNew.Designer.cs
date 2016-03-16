namespace SolvencyII.GUI
{
    partial class frmInstanceNew
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInstanceNew));
            this.lblEntityID = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTypeOfReort = new System.Windows.Forms.Label();
            this.btnInsert = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtEntityID = new System.Windows.Forms.TextBox();
            this.dtPeriod = new System.Windows.Forms.DateTimePicker();
            this.cboModel = new System.Windows.Forms.ComboBox();
            this.lblInternalRptName = new System.Windows.Forms.Label();
            this.txtReportName = new System.Windows.Forms.TextBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.cboCurrency = new System.Windows.Forms.ComboBox();
            this.txtEntityScheme = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblEntityID
            // 
            this.lblEntityID.AutoSize = true;
            this.lblEntityID.Location = new System.Drawing.Point(26, 98);
            this.lblEntityID.Name = "lblEntityID";
            this.lblEntityID.Size = new System.Drawing.Size(47, 13);
            this.lblEntityID.TabIndex = 0;
            this.lblEntityID.Text = "Entity ID";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(26, 71);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(83, 13);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Reference Date";
            // 
            // lblTypeOfReort
            // 
            this.lblTypeOfReort.AutoSize = true;
            this.lblTypeOfReort.Location = new System.Drawing.Point(26, 41);
            this.lblTypeOfReort.Name = "lblTypeOfReort";
            this.lblTypeOfReort.Size = new System.Drawing.Size(73, 13);
            this.lblTypeOfReort.TabIndex = 2;
            this.lblTypeOfReort.Text = "Type of report";
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(154, 186);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(119, 24);
            this.btnInsert.TabIndex = 7;
            this.btnInsert.Text = "Create new report";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(279, 186);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtEntityID
            // 
            this.txtEntityID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityID.Location = new System.Drawing.Point(154, 95);
            this.txtEntityID.Name = "txtEntityID";
            this.txtEntityID.Size = new System.Drawing.Size(275, 20);
            this.txtEntityID.TabIndex = 4;
            // 
            // dtPeriod
            // 
            this.dtPeriod.Location = new System.Drawing.Point(154, 68);
            this.dtPeriod.Name = "dtPeriod";
            this.dtPeriod.Size = new System.Drawing.Size(200, 20);
            this.dtPeriod.TabIndex = 3;
            this.dtPeriod.CloseUp += new System.EventHandler(this.dtPeriod_CloseUp);
            // 
            // cboModel
            // 
            this.cboModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboModel.FormattingEnabled = true;
            this.cboModel.Location = new System.Drawing.Point(154, 41);
            this.cboModel.Name = "cboModel";
            this.cboModel.Size = new System.Drawing.Size(275, 21);
            this.cboModel.TabIndex = 2;
            // 
            // lblInternalRptName
            // 
            this.lblInternalRptName.AutoSize = true;
            this.lblInternalRptName.Location = new System.Drawing.Point(26, 9);
            this.lblInternalRptName.Name = "lblInternalRptName";
            this.lblInternalRptName.Size = new System.Drawing.Size(101, 13);
            this.lblInternalRptName.TabIndex = 12;
            this.lblInternalRptName.Text = "Internal report name";
            // 
            // txtReportName
            // 
            this.txtReportName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReportName.Location = new System.Drawing.Point(154, 9);
            this.txtReportName.Name = "txtReportName";
            this.txtReportName.Size = new System.Drawing.Size(275, 20);
            this.txtReportName.TabIndex = 1;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Location = new System.Drawing.Point(26, 156);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(49, 13);
            this.lblCurrency.TabIndex = 14;
            this.lblCurrency.Text = "Currency";
            // 
            // cboCurrency
            // 
            this.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCurrency.FormattingEnabled = true;
            this.cboCurrency.Location = new System.Drawing.Point(154, 148);
            this.cboCurrency.Name = "cboCurrency";
            this.cboCurrency.Size = new System.Drawing.Size(200, 21);
            this.cboCurrency.TabIndex = 6;
            // 
            // txtEntityScheme
            // 
            this.txtEntityScheme.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntityScheme.Location = new System.Drawing.Point(154, 121);
            this.txtEntityScheme.Name = "txtEntityScheme";
            this.txtEntityScheme.Size = new System.Drawing.Size(275, 20);
            this.txtEntityScheme.TabIndex = 16;
            this.txtEntityScheme.Text = "http://standards.iso.org/iso/17442";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Entity Scheme";
            // 
            // frmInstanceNew
            // 
            this.AcceptButton = this.btnInsert;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(441, 234);
            this.Controls.Add(this.txtEntityScheme);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboCurrency);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.txtReportName);
            this.Controls.Add(this.lblInternalRptName);
            this.Controls.Add(this.cboModel);
            this.Controls.Add(this.dtPeriod);
            this.Controls.Add(this.txtEntityID);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnInsert);
            this.Controls.Add(this.lblTypeOfReort);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblEntityID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInstanceNew";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Report";
            this.Load += new System.EventHandler(this.frmInstanceNew_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEntityID;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblTypeOfReort;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtEntityID;
        private System.Windows.Forms.DateTimePicker dtPeriod;
        private System.Windows.Forms.ComboBox cboModel;
        private System.Windows.Forms.Label lblInternalRptName;
        private System.Windows.Forms.TextBox txtReportName;
        private System.Windows.Forms.Label lblCurrency;
        private System.Windows.Forms.ComboBox cboCurrency;
        private System.Windows.Forms.TextBox txtEntityScheme;
        private System.Windows.Forms.Label label1;
    }
}