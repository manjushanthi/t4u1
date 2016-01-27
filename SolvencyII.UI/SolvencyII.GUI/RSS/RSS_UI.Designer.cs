namespace SolvencyII.GUI.RSS
{
    partial class RSS_UI
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
            this.btnClose = new System.Windows.Forms.Button();
            this.objectListView = new BrightIdeasSoftware.ObjectListView();
            this.olvDescription = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvTitle = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvPublicationDate = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.Location = new System.Drawing.Point(660, 367);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // objectListView
            // 
            this.objectListView.AllColumns.Add(this.olvTitle);
            this.objectListView.AllColumns.Add(this.olvDescription);
            this.objectListView.AllColumns.Add(this.olvPublicationDate);
            this.objectListView.AllowColumnReorder = true;
            this.objectListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvTitle,
            this.olvDescription,
            this.olvPublicationDate});
            this.objectListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.objectListView.FullRowSelect = true;
            this.objectListView.GridLines = true;
            this.objectListView.HeaderUsesThemes = false;
            this.objectListView.HeaderWordWrap = true;
            this.objectListView.HideSelection = false;
            this.objectListView.IncludeColumnHeadersInCopy = true;
            this.objectListView.Location = new System.Drawing.Point(4, 3);
            this.objectListView.Name = "objectListView";
            this.objectListView.OwnerDraw = true;
            this.objectListView.RowHeight = 50;
            this.objectListView.ShowGroups = false;
            this.objectListView.ShowHeaderInAllViews = false;
            this.objectListView.ShowItemToolTips = true;
            this.objectListView.Size = new System.Drawing.Size(731, 358);
            this.objectListView.SortGroupItemsByPrimaryColumn = false;
            this.objectListView.TabIndex = 0;
            this.objectListView.UseAlternatingBackColors = true;
            this.objectListView.UseCompatibleStateImageBehavior = false;
            this.objectListView.UseFilterIndicator = true;
            this.objectListView.UseFiltering = true;
            this.objectListView.UseHotItem = true;
            this.objectListView.View = System.Windows.Forms.View.Details;
           
            // 
            // olvTitle
            // 
            this.olvTitle.AspectName = "Title ";
            this.olvTitle.WordWrap = true;
            this.olvTitle.CellPadding = null;
            this.olvTitle.DisplayIndex = 0;
            this.olvTitle.Text = "Title ";
            this.olvTitle.Width = 195;
            // 
            // olvDescription
            // 
            this.olvDescription.AspectName = "Description ";
            this.olvDescription.CellPadding = null;
            this.olvDescription.WordWrap = true;
            this.olvDescription.Text = "Description ";
            this.olvDescription.Width = 375;
            // 
            // olvPublicationDate
            // 
            this.olvPublicationDate.AspectName = "PublicationDate ";
            this.olvPublicationDate.CellPadding = null;
            this.olvPublicationDate.WordWrap = true;
            this.olvPublicationDate.Text = "PublicationDate ";
            this.olvPublicationDate.Width = 155;
            // 
            // RSS_UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(739, 393);
            this.Controls.Add(this.objectListView);
            this.Controls.Add(this.btnClose);
            this.Name = "RSS_UI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSS";
            ((System.ComponentModel.ISupportInitialize)(this.objectListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private BrightIdeasSoftware.ObjectListView objectListView;
        private BrightIdeasSoftware.HotItemStyle hotItemStyle1;
        private BrightIdeasSoftware.OLVColumn olvDescription;
        private BrightIdeasSoftware.OLVColumn olvTitle;
        private BrightIdeasSoftware.OLVColumn olvPublicationDate;
       
    }
}