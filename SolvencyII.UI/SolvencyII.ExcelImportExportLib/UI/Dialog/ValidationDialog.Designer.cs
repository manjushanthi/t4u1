namespace SolvencyII.ExcelImportExportLib.UI.Dialog
{
    partial class ValidationDialog
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
            this.objectListView = new BrightIdeasSoftware.ObjectListView();
            this.olvTableCode = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvFieldType = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvValue = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // objectListView
            // 
            this.objectListView.AllColumns.Add(this.olvTableCode);
            this.objectListView.AllColumns.Add(this.olvFieldType);
            this.objectListView.AllColumns.Add(this.olvValue);
            this.objectListView.AllowColumnReorder = true;
            this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvTableCode,
            this.olvFieldType,
            this.olvValue});
            this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objectListView.FullRowSelect = true;
            this.objectListView.GridLines = true;
            this.objectListView.HeaderUsesThemes = false;
            this.objectListView.HeaderWordWrap = true;
            this.objectListView.HideSelection = false;
            this.objectListView.IncludeColumnHeadersInCopy = true;
            this.objectListView.Location = new System.Drawing.Point(0, 0);
            this.objectListView.Name = "objectListView";
            this.objectListView.OwnerDraw = true;
            this.objectListView.RowHeight = 50;
            this.objectListView.ShowGroups = false;
            this.objectListView.ShowHeaderInAllViews = false;
            this.objectListView.ShowItemToolTips = true;
            this.objectListView.Size = new System.Drawing.Size(733, 281);
            this.objectListView.SortGroupItemsByPrimaryColumn = false;
            this.objectListView.TabIndex = 0;
            this.objectListView.UseAlternatingBackColors = true;
            this.objectListView.UseCompatibleStateImageBehavior = false;
            this.objectListView.UseFilterIndicator = true;
            this.objectListView.UseFiltering = true;
            this.objectListView.UseHotItem = true;
            this.objectListView.View = System.Windows.Forms.View.Details;
            // 
            // olvTableCode
            // 
            this.olvTableCode.AspectName = "TableCode ";
            this.olvTableCode.CellPadding = null;
            this.olvTableCode.Text = "TableCode ";
            this.olvTableCode.Width = 175;
            // 
            // olvFieldType
            // 
            this.olvFieldType.AspectName = "FieldType";
            this.olvFieldType.CellPadding = null;
            this.olvFieldType.Text = "FieldType";
            this.olvFieldType.Width = 250;
            // 
            // olvValue
            // 
            this.olvValue.AspectName = "Value";
            this.olvValue.CellPadding = null;
            this.olvValue.Text = "Value";
            this.olvValue.Width = 300;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.objectListView);
            this.panel1.Location = new System.Drawing.Point(4, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 281);
            this.panel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.Location = new System.Drawing.Point(662, 290);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // ValidationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 325);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panel1);
            this.Name = "ValidationDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel export validation results";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView objectListView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;     
        private BrightIdeasSoftware.HotItemStyle hotItemStyle1;
        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnViewDetail;
        private BrightIdeasSoftware.OLVColumn olvTableCode;
        private BrightIdeasSoftware.OLVColumn olvFieldType;
        private BrightIdeasSoftware.OLVColumn olvValue;
        


    }
}