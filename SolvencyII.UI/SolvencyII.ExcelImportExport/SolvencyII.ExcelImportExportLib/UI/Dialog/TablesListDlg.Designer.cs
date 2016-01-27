namespace SolvencyII.ExcelImportExportLib.UI.Dialog
{
    partial class TablesListDlg
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.lblTextInfo = new System.Windows.Forms.Label();
            this.clbTablesList = new System.Windows.Forms.CheckedListBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(567, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(486, 338);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Location = new System.Drawing.Point(16, 338);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(70, 17);
            this.chkSelectAll.TabIndex = 5;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // lblTextInfo
            // 
            this.lblTextInfo.AutoSize = true;
            this.lblTextInfo.Location = new System.Drawing.Point(13, 13);
            this.lblTextInfo.Name = "lblTextInfo";
            this.lblTextInfo.Size = new System.Drawing.Size(223, 13);
            this.lblTextInfo.TabIndex = 0;
            this.lblTextInfo.Text = "Select the tables listed below to Import/Export";
            // 
            // clbTablesList
            // 
            this.clbTablesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.clbTablesList.CheckOnClick = true;
            this.clbTablesList.ColumnWidth = 300;
            this.clbTablesList.FormattingEnabled = true;
            this.clbTablesList.Location = new System.Drawing.Point(16, 30);
            this.clbTablesList.MultiColumn = true;
            this.clbTablesList.Name = "clbTablesList";
            this.clbTablesList.Size = new System.Drawing.Size(626, 289);
            this.clbTablesList.TabIndex = 1;
            // 
            // TablesListDlg
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(654, 373);
            this.Controls.Add(this.chkSelectAll);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.clbTablesList);
            this.Controls.Add(this.lblTextInfo);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TablesListDlg";
            this.Text = "Excel Import/Export";
            this.Load += new System.EventHandler(this.MultiCheckListDlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.Label lblTextInfo;
        private System.Windows.Forms.CheckedListBox clbTablesList;
    }
}