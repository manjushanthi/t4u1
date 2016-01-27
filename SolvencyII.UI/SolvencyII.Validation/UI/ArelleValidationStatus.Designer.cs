namespace SolvencyII.Validation.UI
{
    partial class ArelleValidationStatus
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.objectListView = new BrightIdeasSoftware.ObjectListView();
            this.olvDataPointSignature = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvMessageCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvSeverity = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.hotItemStyle1 = new BrightIdeasSoftware.HotItemStyle();
            this.btnViewDetail = new System.Windows.Forms.Button();
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.objectListView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnViewDetail);
            this.splitContainer1.Panel2.Controls.Add(this.btnContinue);
            this.splitContainer1.Panel2.Controls.Add(this.btnAbort);
            this.splitContainer1.Size = new System.Drawing.Size(888, 484);
            this.splitContainer1.SplitterDistance = 416;
            this.splitContainer1.TabIndex = 0;
            // 
            // objectListView
            // 
            this.objectListView.AllColumns.Add(this.olvDataPointSignature);
            this.objectListView.AllColumns.Add(this.olvMessageCode);
            this.objectListView.AllColumns.Add(this.olvSeverity);
            this.objectListView.AllColumns.Add(this.olvValue);
            this.objectListView.AllColumns.Add(this.olvDescription);
            this.objectListView.AllowColumnReorder = true;
            this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvDataPointSignature,
            this.olvMessageCode,
            this.olvSeverity,
            this.olvValue,
            this.olvDescription});
            this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListView.FullRowSelect = true;
            this.objectListView.GridLines = true;
            this.objectListView.HeaderUsesThemes = false;
            this.objectListView.HeaderWordWrap = true;
            this.objectListView.HideSelection = false;
            this.objectListView.HotItemStyle = this.hotItemStyle1;
            this.objectListView.IncludeColumnHeadersInCopy = true;
            this.objectListView.Location = new System.Drawing.Point(0, 0);
            this.objectListView.Name = "objectListView";
            this.objectListView.OwnerDraw = true;
            this.objectListView.RowHeight = 50;
            this.objectListView.ShowGroups = false;
            this.objectListView.ShowHeaderInAllViews = false;
            this.objectListView.ShowItemToolTips = true;
            this.objectListView.Size = new System.Drawing.Size(888, 416);
            this.objectListView.SortGroupItemsByPrimaryColumn = false;
            this.objectListView.TabIndex = 0;
            this.objectListView.UseAlternatingBackColors = true;
            this.objectListView.UseCompatibleStateImageBehavior = false;
            this.objectListView.UseFilterIndicator = true;
            this.objectListView.UseFiltering = true;
            this.objectListView.UseHotItem = true;
            this.objectListView.View = System.Windows.Forms.View.Details;
            this.objectListView.SizeChanged += new System.EventHandler(this.objectListView_SizeChanged);
            // 
            // olvDataPointSignature
            // 
            this.olvDataPointSignature.AspectName = "DataPointSignature";
            this.olvDataPointSignature.CellPadding = null;
            this.olvDataPointSignature.Text = "Data Point Signature";
            this.olvDataPointSignature.Width = 187;
            this.olvDataPointSignature.WordWrap = true;
            // 
            // olvMessageCode
            // 
            this.olvMessageCode.AspectName = "MessageCode";
            this.olvMessageCode.CellPadding = null;
            this.olvMessageCode.Text = "Message Code";
            this.olvMessageCode.Width = 105;
            this.olvMessageCode.WordWrap = true;
            // 
            // olvSeverity
            // 
            this.olvSeverity.AspectName = "MessageLevel";
            this.olvSeverity.CellPadding = null;
            this.olvSeverity.Text = "Severity";
            // 
            // olvValue
            // 
            this.olvValue.AspectName = "Value";
            this.olvValue.CellPadding = null;
            this.olvValue.Text = "Value";
            this.olvValue.Width = 395;
            this.olvValue.WordWrap = true;
            // 
            // olvDescription
            // 
            this.olvDescription.AspectName = "Description";
            this.olvDescription.CellPadding = null;
            this.olvDescription.Text = "Description";
            this.olvDescription.Width = 496;
            this.olvDescription.WordWrap = true;
            // 
            // hotItemStyle1
            // 
            this.hotItemStyle1.BackColor = System.Drawing.Color.PeachPuff;
            this.hotItemStyle1.ForeColor = System.Drawing.Color.MediumBlue;
            // 
            // btnViewDetail
            // 
            this.btnViewDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewDetail.Location = new System.Drawing.Point(594, 11);
            this.btnViewDetail.Name = "btnViewDetail";
            this.btnViewDetail.Size = new System.Drawing.Size(75, 23);
            this.btnViewDetail.TabIndex = 2;
            this.btnViewDetail.Text = "View Detail";
            this.btnViewDetail.UseVisualStyleBackColor = true;
            this.btnViewDetail.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnContinue
            // 
            this.btnContinue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.btnContinue.Location = new System.Drawing.Point(675, 11);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 1;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            // 
            // btnAbort
            // 
            this.btnAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbort.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.btnAbort.Location = new System.Drawing.Point(756, 11);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(75, 23);
            this.btnAbort.TabIndex = 0;
            this.btnAbort.Text = "Abort";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // ArelleValidationStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 484);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ArelleValidationStatus";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Arelle Validation Status";
            this.Load += new System.EventHandler(this.ArelleValidationStatus_Load);
            this.SizeChanged += new System.EventHandler(this.ArelleValidationStatus_SizeChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button btnAbort;
        private BrightIdeasSoftware.ObjectListView objectListView;
        private BrightIdeasSoftware.OLVColumn olvDataPointSignature;
        private BrightIdeasSoftware.OLVColumn olvMessageCode;
        private BrightIdeasSoftware.OLVColumn olvValue;
        private BrightIdeasSoftware.HotItemStyle hotItemStyle1;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnViewDetail;
        private BrightIdeasSoftware.OLVColumn olvDescription;
        private BrightIdeasSoftware.OLVColumn olvSeverity;
    }
}