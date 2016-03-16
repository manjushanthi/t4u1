using System.Collections.Generic;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.GUI
{
    public partial class frmMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblExportImportStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.tabTopRight = new System.Windows.Forms.TabControl();
            this.tabValidationErrorPage = new System.Windows.Forms.TabPage();
            this.tabCellPropertiesPage = new System.Windows.Forms.TabPage();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createXbrtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cRDIVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solvencyIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solvencyIIPreparatoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recentFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeDatabaseConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createANewReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectActiveReportIIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changeActiveReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteActiveReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeActiveReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.validationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateCurrentReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateCurrentContainerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xBRLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importXBRLIntanceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.integratedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arelleWithValidationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importArelleWithoutValidationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportXBRLInstanceFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.integratedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.arelleWithValidationToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.exportArelleWithoutValidationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateXBRLReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataFromExcelFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.importDataToExcelBusinessTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportDataToBusinessTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.downloadTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadAnEmptyBusinessExcelTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadEnumerationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formlanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.applicationLanguageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.localValidationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteValidationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.databaseTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.annotatedTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dPMDictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.annotatedFULLTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preparatoryS2DictionaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preparatoryS2AnnotatedTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.taxononmyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.preparatoryS2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PreparatoryS2TestXBRLInstancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solvencyIIFULLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cDRIVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullS2TestXBRLInstancesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databasesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLServerBackUpFullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sQLServerBackUpsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.databaseDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rCBusinessCodeMappingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whatsNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logAndSystemDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rSSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseInformationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.statusTxtGeneral = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTxtReportName = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTxtTypeOfReport = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTxtReportDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTxtEntityIdentifier = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusTxtCurrency = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusRSS = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabTopRight.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblExportImportStatus
            // 
            this.lblExportImportStatus.Name = "lblExportImportStatus";
            this.lblExportImportStatus.Size = new System.Drawing.Size(225, 19);
            this.lblExportImportStatus.Spring = true;
            this.lblExportImportStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 18);
            this.toolStripProgressBar1.Visible = false;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 24);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.splitContainer2.Panel2.Padding = new System.Windows.Forms.Padding(3);
            this.splitContainer2.Size = new System.Drawing.Size(1074, 582);
            this.splitContainer2.SplitterDistance = 150;
            this.splitContainer2.TabIndex = 11;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Silver;
            this.splitContainer1.Panel1.Controls.Add(this.treeView1);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitContainer1.Panel2.Controls.Add(this.tabTopRight);
            this.splitContainer1.Size = new System.Drawing.Size(1074, 150);
            this.splitContainer1.SplitterDistance = 358;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.HideSelection = false;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.treeViewImageList;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(352, 144);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // treeViewImageList
            // 
            this.treeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeViewImageList.ImageStream")));
            this.treeViewImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.treeViewImageList.Images.SetKeyName(0, "format-justify-left.ico");
            this.treeViewImageList.Images.SetKeyName(1, "templateno.ico");
            this.treeViewImageList.Images.SetKeyName(2, "templateok.ico");
            // 
            // tabTopRight
            // 
            this.tabTopRight.Controls.Add(this.tabValidationErrorPage);
            this.tabTopRight.Controls.Add(this.tabCellPropertiesPage);
            this.tabTopRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTopRight.Location = new System.Drawing.Point(0, 0);
            this.tabTopRight.Name = "tabTopRight";
            this.tabTopRight.SelectedIndex = 0;
            this.tabTopRight.Size = new System.Drawing.Size(712, 150);
            this.tabTopRight.TabIndex = 0;
            // 
            // tabValidationErrorPage
            // 
            this.tabValidationErrorPage.Location = new System.Drawing.Point(4, 22);
            this.tabValidationErrorPage.Name = "tabValidationErrorPage";
            this.tabValidationErrorPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabValidationErrorPage.Size = new System.Drawing.Size(704, 124);
            this.tabValidationErrorPage.TabIndex = 0;
            this.tabValidationErrorPage.Text = "Validation error";
            this.tabValidationErrorPage.UseVisualStyleBackColor = true;
            // 
            // tabCellPropertiesPage
            // 
            this.tabCellPropertiesPage.Location = new System.Drawing.Point(4, 22);
            this.tabCellPropertiesPage.Name = "tabCellPropertiesPage";
            this.tabCellPropertiesPage.Padding = new System.Windows.Forms.Padding(3);
            this.tabCellPropertiesPage.Size = new System.Drawing.Size(704, 124);
            this.tabCellPropertiesPage.TabIndex = 1;
            this.tabCellPropertiesPage.Text = "Cell properties";
            this.tabCellPropertiesPage.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.reportToolStripMenuItem,
            this.validationToolStripMenuItem,
            this.xBRLToolStripMenuItem,
            this.excelToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(1074, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createXbrtToolStripMenuItem,
            this.openDatabaseToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.recentFilesToolStripMenuItem,
            this.changeDatabaseConnectionToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.DropDownOpening += new System.EventHandler(this.fileToolStripMenuItem_DropDownOpening);
            // 
            // createXbrtToolStripMenuItem
            // 
            this.createXbrtToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cRDIVToolStripMenuItem,
            this.solvencyIIToolStripMenuItem,
            this.solvencyIIPreparatoryToolStripMenuItem});
            this.createXbrtToolStripMenuItem.Name = "createXbrtToolStripMenuItem";
            this.createXbrtToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.createXbrtToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.createXbrtToolStripMenuItem.Text = "Create a &new multi-report container";
            // 
            // cRDIVToolStripMenuItem
            // 
            this.cRDIVToolStripMenuItem.Name = "cRDIVToolStripMenuItem";
            this.cRDIVToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.cRDIVToolStripMenuItem.Text = "CRD IV";
            this.cRDIVToolStripMenuItem.Click += new System.EventHandler(this.cRDIVToolStripMenuItem_Click);
            // 
            // solvencyIIToolStripMenuItem
            // 
            this.solvencyIIToolStripMenuItem.Name = "solvencyIIToolStripMenuItem";
            this.solvencyIIToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.solvencyIIToolStripMenuItem.Text = "Solvency II";
            this.solvencyIIToolStripMenuItem.Click += new System.EventHandler(this.solvencyIIToolStripMenuItem_Click);
            // 
            // solvencyIIPreparatoryToolStripMenuItem
            // 
            this.solvencyIIPreparatoryToolStripMenuItem.Name = "solvencyIIPreparatoryToolStripMenuItem";
            this.solvencyIIPreparatoryToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.solvencyIIPreparatoryToolStripMenuItem.Text = "Solvency II Preparatory";
            this.solvencyIIPreparatoryToolStripMenuItem.Click += new System.EventHandler(this.solvencyIIPreparatoryToolStripMenuItem_Click);
            // 
            // openDatabaseToolStripMenuItem
            // 
            this.openDatabaseToolStripMenuItem.Name = "openDatabaseToolStripMenuItem";
            this.openDatabaseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openDatabaseToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.openDatabaseToolStripMenuItem.Text = "&Open";
            this.openDatabaseToolStripMenuItem.Click += new System.EventHandler(this.openDatabaseToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.saveAsToolStripMenuItem.Text = "Save as";
            this.saveAsToolStripMenuItem.Visible = false;
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Visible = false;
            // 
            // recentFilesToolStripMenuItem
            // 
            this.recentFilesToolStripMenuItem.Name = "recentFilesToolStripMenuItem";
            this.recentFilesToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.recentFilesToolStripMenuItem.Text = "Recent";
            // 
            // changeDatabaseConnectionToolStripMenuItem
            // 
            this.changeDatabaseConnectionToolStripMenuItem.Name = "changeDatabaseConnectionToolStripMenuItem";
            this.changeDatabaseConnectionToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.changeDatabaseConnectionToolStripMenuItem.Text = "Open SQL Server Database ";
            this.changeDatabaseConnectionToolStripMenuItem.Visible = false;
            this.changeDatabaseConnectionToolStripMenuItem.Click += new System.EventHandler(this.changeDatabaseConnectionToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // reportToolStripMenuItem
            // 
            this.reportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createANewReportToolStripMenuItem,
            this.selectActiveReportIIToolStripMenuItem,
            this.changeActiveReportToolStripMenuItem,
            this.deleteActiveReportToolStripMenuItem,
            this.closeActiveReportToolStripMenuItem,
            this.toolStripMenuItem2});
            this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
            this.reportToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.reportToolStripMenuItem.Text = "&Report";
            this.reportToolStripMenuItem.DropDownOpening += new System.EventHandler(this.reportToolStripMenuItem_DropDownOpening);
            // 
            // createANewReportToolStripMenuItem
            // 
            this.createANewReportToolStripMenuItem.Name = "createANewReportToolStripMenuItem";
            this.createANewReportToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.createANewReportToolStripMenuItem.Text = "Create new";
            this.createANewReportToolStripMenuItem.Click += new System.EventHandler(this.createANewReportToolStripMenuItem_Click);
            // 
            // selectActiveReportIIToolStripMenuItem
            // 
            this.selectActiveReportIIToolStripMenuItem.Name = "selectActiveReportIIToolStripMenuItem";
            this.selectActiveReportIIToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.selectActiveReportIIToolStripMenuItem.Text = "Select active report";
            // 
            // changeActiveReportToolStripMenuItem
            // 
            this.changeActiveReportToolStripMenuItem.Name = "changeActiveReportToolStripMenuItem";
            this.changeActiveReportToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.changeActiveReportToolStripMenuItem.Text = "Edit active";
            this.changeActiveReportToolStripMenuItem.Click += new System.EventHandler(this.changeActiveReportToolStripMenuItem_Click);
            // 
            // deleteActiveReportToolStripMenuItem
            // 
            this.deleteActiveReportToolStripMenuItem.Name = "deleteActiveReportToolStripMenuItem";
            this.deleteActiveReportToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.deleteActiveReportToolStripMenuItem.Text = "Delete active";
            this.deleteActiveReportToolStripMenuItem.Click += new System.EventHandler(this.deleteActiveReportToolStripMenuItem_Click);
            // 
            // closeActiveReportToolStripMenuItem
            // 
            this.closeActiveReportToolStripMenuItem.Name = "closeActiveReportToolStripMenuItem";
            this.closeActiveReportToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.closeActiveReportToolStripMenuItem.Text = "Close active";
            this.closeActiveReportToolStripMenuItem.Click += new System.EventHandler(this.closeActiveReportToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 6);
            // 
            // validationToolStripMenuItem
            // 
            this.validationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.validateCurrentReportToolStripMenuItem,
            this.validateCurrentContainerToolStripMenuItem});
            this.validationToolStripMenuItem.Name = "validationToolStripMenuItem";
            this.validationToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.validationToolStripMenuItem.Text = "Validation";
            // 
            // validateCurrentReportToolStripMenuItem
            // 
            this.validateCurrentReportToolStripMenuItem.Name = "validateCurrentReportToolStripMenuItem";
            this.validateCurrentReportToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
            this.validateCurrentReportToolStripMenuItem.Text = "Validate active report (database validations)";
            this.validateCurrentReportToolStripMenuItem.Click += new System.EventHandler(this.validateCurrentReportToolStripMenuItem_Click);
            // 
            // validateCurrentContainerToolStripMenuItem
            // 
            this.validateCurrentContainerToolStripMenuItem.Enabled = false;
            this.validateCurrentContainerToolStripMenuItem.Name = "validateCurrentContainerToolStripMenuItem";
            this.validateCurrentContainerToolStripMenuItem.Size = new System.Drawing.Size(302, 22);
            this.validateCurrentContainerToolStripMenuItem.Text = "Validate active container";
            this.validateCurrentContainerToolStripMenuItem.Visible = false;
            this.validateCurrentContainerToolStripMenuItem.Click += new System.EventHandler(this.validateCurrentContainerToolStripMenuItem_Click);
            // 
            // xBRLToolStripMenuItem
            // 
            this.xBRLToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importXBRLIntanceFileToolStripMenuItem,
            this.exportXBRLInstanceFileToolStripMenuItem,
            this.validateXBRLReportToolStripMenuItem});
            this.xBRLToolStripMenuItem.Name = "xBRLToolStripMenuItem";
            this.xBRLToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.xBRLToolStripMenuItem.Text = "XBRL";
            this.xBRLToolStripMenuItem.DropDownOpening += new System.EventHandler(this.xBRLToolStripMenuItem_DropDownOpening);
            // 
            // importXBRLIntanceFileToolStripMenuItem
            // 
            this.importXBRLIntanceFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.integratedToolStripMenuItem,
            this.arelleWithValidationToolStripMenuItem,
            this.importArelleWithoutValidationToolStripMenuItem});
            this.importXBRLIntanceFileToolStripMenuItem.Enabled = false;
            this.importXBRLIntanceFileToolStripMenuItem.Name = "importXBRLIntanceFileToolStripMenuItem";
            this.importXBRLIntanceFileToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.importXBRLIntanceFileToolStripMenuItem.Text = "&Import XBRL instance file";
            // 
            // integratedToolStripMenuItem
            // 
            this.integratedToolStripMenuItem.Name = "integratedToolStripMenuItem";
            this.integratedToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.integratedToolStripMenuItem.Text = "Integrated";
            this.integratedToolStripMenuItem.Click += new System.EventHandler(this.importintegratedToolStripMenuItem_Click);
            // 
            // arelleWithValidationToolStripMenuItem
            // 
            this.arelleWithValidationToolStripMenuItem.Name = "arelleWithValidationToolStripMenuItem";
            this.arelleWithValidationToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.arelleWithValidationToolStripMenuItem.Text = "Arelle with Validation";
            this.arelleWithValidationToolStripMenuItem.Click += new System.EventHandler(this.importarelleWithValidationToolStripMenuItem_Click);
            // 
            // importArelleWithoutValidationToolStripMenuItem
            // 
            this.importArelleWithoutValidationToolStripMenuItem.Name = "importArelleWithoutValidationToolStripMenuItem";
            this.importArelleWithoutValidationToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.importArelleWithoutValidationToolStripMenuItem.Text = "Arelle without Validation";
            this.importArelleWithoutValidationToolStripMenuItem.Click += new System.EventHandler(this.importarelleWithoutValidationToolStripMenuItem_Click);
            // 
            // exportXBRLInstanceFileToolStripMenuItem
            // 
            this.exportXBRLInstanceFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.integratedToolStripMenuItem1,
            this.arelleWithValidationToolStripMenuItem1,
            this.exportArelleWithoutValidationToolStripMenuItem});
            this.exportXBRLInstanceFileToolStripMenuItem.Name = "exportXBRLInstanceFileToolStripMenuItem";
            this.exportXBRLInstanceFileToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.exportXBRLInstanceFileToolStripMenuItem.Text = "E&xport XBRL instance file";
            // 
            // integratedToolStripMenuItem1
            // 
            this.integratedToolStripMenuItem1.Name = "integratedToolStripMenuItem1";
            this.integratedToolStripMenuItem1.Size = new System.Drawing.Size(203, 22);
            this.integratedToolStripMenuItem1.Text = "Integrated";
            this.integratedToolStripMenuItem1.Click += new System.EventHandler(this.exportintegratedToolStripMenuItem1_Click);
            // 
            // arelleWithValidationToolStripMenuItem1
            // 
            this.arelleWithValidationToolStripMenuItem1.Name = "arelleWithValidationToolStripMenuItem1";
            this.arelleWithValidationToolStripMenuItem1.Size = new System.Drawing.Size(203, 22);
            this.arelleWithValidationToolStripMenuItem1.Text = "Arelle with Validation";
            this.arelleWithValidationToolStripMenuItem1.Click += new System.EventHandler(this.exportarelleWithValidationToolStripMenuItem1_Click);
            // 
            // exportArelleWithoutValidationToolStripMenuItem
            // 
            this.exportArelleWithoutValidationToolStripMenuItem.Name = "exportArelleWithoutValidationToolStripMenuItem";
            this.exportArelleWithoutValidationToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.exportArelleWithoutValidationToolStripMenuItem.Text = "Arelle without Validation";
            this.exportArelleWithoutValidationToolStripMenuItem.Click += new System.EventHandler(this.exportarelleWithoutValidationToolStripMenuItem1_Click);
            // 
            // validateXBRLReportToolStripMenuItem
            // 
            this.validateXBRLReportToolStripMenuItem.Name = "validateXBRLReportToolStripMenuItem";
            this.validateXBRLReportToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.validateXBRLReportToolStripMenuItem.Text = "&Validate XBRL report";
            this.validateXBRLReportToolStripMenuItem.Click += new System.EventHandler(this.validateXBRLReportToolStripMenuItem_Click);
            // 
            // excelToolStripMenuItem
            // 
            this.excelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importDataFromExcelFileToolStripMenuItem,
            this.exportToExcelToolStripMenuItem,
            this.toolStripSeparator1,
            this.importDataToExcelBusinessTemplateToolStripMenuItem,
            this.exportDataToBusinessTemplateToolStripMenuItem,
            this.toolStripSeparator3,
            this.downloadTemplateToolStripMenuItem,
            this.downloadAnEmptyBusinessExcelTemplateToolStripMenuItem,
            this.downloadEnumerationsToolStripMenuItem});
            this.excelToolStripMenuItem.Name = "excelToolStripMenuItem";
            this.excelToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.excelToolStripMenuItem.Text = "Excel";
            // 
            // importDataFromExcelFileToolStripMenuItem
            // 
            this.importDataFromExcelFileToolStripMenuItem.Name = "importDataFromExcelFileToolStripMenuItem";
            this.importDataFromExcelFileToolStripMenuItem.Size = new System.Drawing.Size(391, 22);
            this.importDataFromExcelFileToolStripMenuItem.Text = "Import data from basic Excel template";
            this.importDataFromExcelFileToolStripMenuItem.Click += new System.EventHandler(this.importDataFromExcelFileToolStripMenuItem_Click);
            // 
            // exportToExcelToolStripMenuItem
            // 
            this.exportToExcelToolStripMenuItem.Name = "exportToExcelToolStripMenuItem";
            this.exportToExcelToolStripMenuItem.Size = new System.Drawing.Size(391, 22);
            this.exportToExcelToolStripMenuItem.Text = "Export data to basic Excel template";
            this.exportToExcelToolStripMenuItem.Click += new System.EventHandler(this.exportToExcelToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(388, 6);
            // 
            // importDataToExcelBusinessTemplateToolStripMenuItem
            // 
            this.importDataToExcelBusinessTemplateToolStripMenuItem.Name = "importDataToExcelBusinessTemplateToolStripMenuItem";
            this.importDataToExcelBusinessTemplateToolStripMenuItem.Size = new System.Drawing.Size(391, 22);
            this.importDataToExcelBusinessTemplateToolStripMenuItem.Text = "Import data from Excel Business template";
            this.importDataToExcelBusinessTemplateToolStripMenuItem.Click += new System.EventHandler(this.importDataToExcelBusinessTemplateToolStripMenuItem_Click);
            // 
            // exportDataToBusinessTemplateToolStripMenuItem
            // 
            this.exportDataToBusinessTemplateToolStripMenuItem.Name = "exportDataToBusinessTemplateToolStripMenuItem";
            this.exportDataToBusinessTemplateToolStripMenuItem.Size = new System.Drawing.Size(391, 22);
            this.exportDataToBusinessTemplateToolStripMenuItem.Text = "Export data to Excel Business template";
            this.exportDataToBusinessTemplateToolStripMenuItem.Click += new System.EventHandler(this.exportDataToBusinessTemplateToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(388, 6);
            // 
            // downloadTemplateToolStripMenuItem
            // 
            this.downloadTemplateToolStripMenuItem.Name = "downloadTemplateToolStripMenuItem";
            this.downloadTemplateToolStripMenuItem.Size = new System.Drawing.Size(391, 22);
            this.downloadTemplateToolStripMenuItem.Text = "Download an empty basic Excel template";
            this.downloadTemplateToolStripMenuItem.Click += new System.EventHandler(this.downloadTemplateToolStripMenuItem_Click);
            // 
            // downloadAnEmptyBusinessExcelTemplateToolStripMenuItem
            // 
            this.downloadAnEmptyBusinessExcelTemplateToolStripMenuItem.Name = "downloadAnEmptyBusinessExcelTemplateToolStripMenuItem";
            this.downloadAnEmptyBusinessExcelTemplateToolStripMenuItem.Size = new System.Drawing.Size(391, 22);
            this.downloadAnEmptyBusinessExcelTemplateToolStripMenuItem.Text = "Download an empty business excel template";
            this.downloadAnEmptyBusinessExcelTemplateToolStripMenuItem.Click += new System.EventHandler(this.downloadAnEmptyBusinessExcelTemplateToolStripMenuItem_Click);
            // 
            // downloadEnumerationsToolStripMenuItem
            // 
            this.downloadEnumerationsToolStripMenuItem.Name = "downloadEnumerationsToolStripMenuItem";
            this.downloadEnumerationsToolStripMenuItem.Size = new System.Drawing.Size(391, 22);
            this.downloadEnumerationsToolStripMenuItem.Text = "Download supportive list of basic Excel template dropdowns";
            this.downloadEnumerationsToolStripMenuItem.Click += new System.EventHandler(this.downloadEnumerationsToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.formlanguageToolStripMenuItem,
            this.applicationLanguageToolStripMenuItem,
            this.toolStripMenuItem1,
            this.localValidationToolStripMenuItem,
            this.remoteValidationToolStripMenuItem,
            this.toolStripMenuItem4,
            this.databaseTypeToolStripMenuItem,
            this.toolStripSeparator2});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // formlanguageToolStripMenuItem
            // 
            this.formlanguageToolStripMenuItem.Name = "formlanguageToolStripMenuItem";
            this.formlanguageToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.formlanguageToolStripMenuItem.Text = "&Language";
            this.formlanguageToolStripMenuItem.Visible = false;
            // 
            // applicationLanguageToolStripMenuItem
            // 
            this.applicationLanguageToolStripMenuItem.Name = "applicationLanguageToolStripMenuItem";
            this.applicationLanguageToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.applicationLanguageToolStripMenuItem.Text = "&Form Language";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // localValidationToolStripMenuItem
            // 
            this.localValidationToolStripMenuItem.Checked = true;
            this.localValidationToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.localValidationToolStripMenuItem.Name = "localValidationToolStripMenuItem";
            this.localValidationToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.localValidationToolStripMenuItem.Text = "Local validation";
            this.localValidationToolStripMenuItem.Click += new System.EventHandler(this.localValidationToolStripMenuItem_Click);
            // 
            // remoteValidationToolStripMenuItem
            // 
            this.remoteValidationToolStripMenuItem.Name = "remoteValidationToolStripMenuItem";
            this.remoteValidationToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.remoteValidationToolStripMenuItem.Text = "Remote validation";
            this.remoteValidationToolStripMenuItem.Click += new System.EventHandler(this.remoteValidationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(167, 6);
            // 
            // databaseTypeToolStripMenuItem
            // 
            this.databaseTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sQLiteToolStripMenuItem,
            this.sQLServerToolStripMenuItem});
            this.databaseTypeToolStripMenuItem.Name = "databaseTypeToolStripMenuItem";
            this.databaseTypeToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.databaseTypeToolStripMenuItem.Text = "Database Type";
            // 
            // sQLiteToolStripMenuItem
            // 
            this.sQLiteToolStripMenuItem.Checked = true;
            this.sQLiteToolStripMenuItem.CheckOnClick = true;
            this.sQLiteToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sQLiteToolStripMenuItem.Name = "sQLiteToolStripMenuItem";
            this.sQLiteToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.sQLiteToolStripMenuItem.Text = "SQLite";
            this.sQLiteToolStripMenuItem.Click += new System.EventHandler(this.sQLiteToolStripMenuItem_Click);
            // 
            // sQLServerToolStripMenuItem
            // 
            this.sQLServerToolStripMenuItem.CheckOnClick = true;
            this.sQLServerToolStripMenuItem.Enabled = false;
            this.sQLServerToolStripMenuItem.Name = "sQLServerToolStripMenuItem";
            this.sQLServerToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.sQLServerToolStripMenuItem.Text = "SQL Server";
            this.sQLServerToolStripMenuItem.Click += new System.EventHandler(this.sQLServerToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(167, 6);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.annotatedTemplatesToolStripMenuItem,
            this.taxononmyToolStripMenuItem,
            this.databasesToolStripMenuItem,
            this.userManualToolStripMenuItem,
            this.whatsNewToolStripMenuItem,
            this.logAndSystemDetailsToolStripMenuItem,
            this.rSSToolStripMenuItem,
            this.licenseInformationToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // annotatedTemplatesToolStripMenuItem
            // 
            this.annotatedTemplatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dPMDictionaryToolStripMenuItem,
            this.annotatedFULLTemplatesToolStripMenuItem,
            this.preparatoryS2DictionaryToolStripMenuItem,
            this.preparatoryS2AnnotatedTemplatesToolStripMenuItem});
            this.annotatedTemplatesToolStripMenuItem.Name = "annotatedTemplatesToolStripMenuItem";
            this.annotatedTemplatesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.annotatedTemplatesToolStripMenuItem.Text = "DPM";
            // 
            // dPMDictionaryToolStripMenuItem
            // 
            this.dPMDictionaryToolStripMenuItem.Name = "dPMDictionaryToolStripMenuItem";
            this.dPMDictionaryToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.dPMDictionaryToolStripMenuItem.Text = "Full S2 Dictionary";
            this.dPMDictionaryToolStripMenuItem.Click += new System.EventHandler(this.dPMDictionaryToolStripMenuItem_Click);
            // 
            // annotatedFULLTemplatesToolStripMenuItem
            // 
            this.annotatedFULLTemplatesToolStripMenuItem.Name = "annotatedFULLTemplatesToolStripMenuItem";
            this.annotatedFULLTemplatesToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.annotatedFULLTemplatesToolStripMenuItem.Text = "Full S2 Annotated Templates";
            this.annotatedFULLTemplatesToolStripMenuItem.Click += new System.EventHandler(this.annotatedTemplatesToolStripMenuItem1_Click);
            // 
            // preparatoryS2DictionaryToolStripMenuItem
            // 
            this.preparatoryS2DictionaryToolStripMenuItem.Name = "preparatoryS2DictionaryToolStripMenuItem";
            this.preparatoryS2DictionaryToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.preparatoryS2DictionaryToolStripMenuItem.Text = "Preparatory S2 Dictionary";
            this.preparatoryS2DictionaryToolStripMenuItem.Click += new System.EventHandler(this.preparatoryS2DictionaryToolStripMenuItem_Click);
            // 
            // preparatoryS2AnnotatedTemplatesToolStripMenuItem
            // 
            this.preparatoryS2AnnotatedTemplatesToolStripMenuItem.Name = "preparatoryS2AnnotatedTemplatesToolStripMenuItem";
            this.preparatoryS2AnnotatedTemplatesToolStripMenuItem.Size = new System.Drawing.Size(266, 22);
            this.preparatoryS2AnnotatedTemplatesToolStripMenuItem.Text = "Preparatory S2 Annotated Templates";
            this.preparatoryS2AnnotatedTemplatesToolStripMenuItem.Click += new System.EventHandler(this.preparatoryS2AnnotatedTemplatesToolStripMenuItem_Click);
            // 
            // taxononmyToolStripMenuItem
            // 
            this.taxononmyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.preparatoryS2ToolStripMenuItem,
            this.PreparatoryS2TestXBRLInstancesToolStripMenuItem,
            this.solvencyIIFULLToolStripMenuItem,
            this.cDRIVToolStripMenuItem,
            this.fullS2TestXBRLInstancesToolStripMenuItem});
            this.taxononmyToolStripMenuItem.Name = "taxononmyToolStripMenuItem";
            this.taxononmyToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.taxononmyToolStripMenuItem.Text = "Taxonomy";
            // 
            // preparatoryS2ToolStripMenuItem
            // 
            this.preparatoryS2ToolStripMenuItem.Name = "preparatoryS2ToolStripMenuItem";
            this.preparatoryS2ToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.preparatoryS2ToolStripMenuItem.Text = "Preparatory S2";
            this.preparatoryS2ToolStripMenuItem.Click += new System.EventHandler(this.preparatoryS2ToolStripMenuItem_Click);
            // 
            // PreparatoryS2TestXBRLInstancesToolStripMenuItem
            // 
            this.PreparatoryS2TestXBRLInstancesToolStripMenuItem.Name = "PreparatoryS2TestXBRLInstancesToolStripMenuItem";
            this.PreparatoryS2TestXBRLInstancesToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.PreparatoryS2TestXBRLInstancesToolStripMenuItem.Text = "Preparatory S2 Test Instances ";
            this.PreparatoryS2TestXBRLInstancesToolStripMenuItem.Click += new System.EventHandler(this.PreparatoryS2TestXBRLInstancesToolStripMenuItem_Click);
            // 
            // solvencyIIFULLToolStripMenuItem
            // 
            this.solvencyIIFULLToolStripMenuItem.Name = "solvencyIIFULLToolStripMenuItem";
            this.solvencyIIFULLToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.solvencyIIFULLToolStripMenuItem.Text = "Full S2";
            this.solvencyIIFULLToolStripMenuItem.Click += new System.EventHandler(this.solvencyIIToolStripMenuItem1_Click);
            // 
            // cDRIVToolStripMenuItem
            // 
            this.cDRIVToolStripMenuItem.Name = "cDRIVToolStripMenuItem";
            this.cDRIVToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.cDRIVToolStripMenuItem.Text = "CDR IV";
            this.cDRIVToolStripMenuItem.Click += new System.EventHandler(this.cDRIVToolStripMenuItem_Click);
            // 
            // fullS2TestXBRLInstancesToolStripMenuItem
            // 
            this.fullS2TestXBRLInstancesToolStripMenuItem.Name = "fullS2TestXBRLInstancesToolStripMenuItem";
            this.fullS2TestXBRLInstancesToolStripMenuItem.Size = new System.Drawing.Size(229, 22);
            this.fullS2TestXBRLInstancesToolStripMenuItem.Text = "Full S2 Test Instances";
            this.fullS2TestXBRLInstancesToolStripMenuItem.Click += new System.EventHandler(this.fullS2TestXBRLInstancesToolStripMenuItem_Click);
            // 
            // databasesToolStripMenuItem
            // 
            this.databasesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sQLServerBackUpFullToolStripMenuItem,
            this.sQLServerBackUpsToolStripMenuItem,
            this.databaseDocumentationToolStripMenuItem,
            this.rCBusinessCodeMappingToolStripMenuItem});
            this.databasesToolStripMenuItem.Name = "databasesToolStripMenuItem";
            this.databasesToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.databasesToolStripMenuItem.Text = "Databases";
            // 
            // sQLServerBackUpFullToolStripMenuItem
            // 
            this.sQLServerBackUpFullToolStripMenuItem.Name = "sQLServerBackUpFullToolStripMenuItem";
            this.sQLServerBackUpFullToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.sQLServerBackUpFullToolStripMenuItem.Text = "SQL Server back up(Full)";
            this.sQLServerBackUpFullToolStripMenuItem.Click += new System.EventHandler(this.sQLServerBackUpFullToolStripMenuItem_Click);
            // 
            // sQLServerBackUpsToolStripMenuItem
            // 
            this.sQLServerBackUpsToolStripMenuItem.Name = "sQLServerBackUpsToolStripMenuItem";
            this.sQLServerBackUpsToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.sQLServerBackUpsToolStripMenuItem.Text = "SQL Server back up (Preparatory)";
            this.sQLServerBackUpsToolStripMenuItem.Click += new System.EventHandler(this.sQLServerBackUpsToolStripMenuItem_Click);
            // 
            // databaseDocumentationToolStripMenuItem
            // 
            this.databaseDocumentationToolStripMenuItem.Name = "databaseDocumentationToolStripMenuItem";
            this.databaseDocumentationToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.databaseDocumentationToolStripMenuItem.Text = "Database documentation";
            this.databaseDocumentationToolStripMenuItem.Click += new System.EventHandler(this.databaseDocumentationToolStripMenuItem_Click);
            // 
            // rCBusinessCodeMappingToolStripMenuItem
            // 
            this.rCBusinessCodeMappingToolStripMenuItem.Name = "rCBusinessCodeMappingToolStripMenuItem";
            this.rCBusinessCodeMappingToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
            this.rCBusinessCodeMappingToolStripMenuItem.Text = "RC Business code mapping";
            this.rCBusinessCodeMappingToolStripMenuItem.Click += new System.EventHandler(this.rCBusinessCodeMappingToolStripMenuItem_Click);
            // 
            // userManualToolStripMenuItem
            // 
            this.userManualToolStripMenuItem.Name = "userManualToolStripMenuItem";
            this.userManualToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.userManualToolStripMenuItem.Text = "User Manual";
            this.userManualToolStripMenuItem.Click += new System.EventHandler(this.userManualToolStripMenuItem_Click);
            // 
            // whatsNewToolStripMenuItem
            // 
            this.whatsNewToolStripMenuItem.Name = "whatsNewToolStripMenuItem";
            this.whatsNewToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.whatsNewToolStripMenuItem.Text = "What\'s new";
            this.whatsNewToolStripMenuItem.Click += new System.EventHandler(this.whatsNewToolStripMenuItem_Click);
            // 
            // logAndSystemDetailsToolStripMenuItem
            // 
            this.logAndSystemDetailsToolStripMenuItem.Name = "logAndSystemDetailsToolStripMenuItem";
            this.logAndSystemDetailsToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.logAndSystemDetailsToolStripMenuItem.Text = "Log and System Details";
            this.logAndSystemDetailsToolStripMenuItem.Click += new System.EventHandler(this.logAndSystemDetailsToolStripMenuItem_Click);
            // 
            // rSSToolStripMenuItem
            // 
            this.rSSToolStripMenuItem.Name = "rSSToolStripMenuItem";
            this.rSSToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.rSSToolStripMenuItem.Text = "See News (RSS)";
            this.rSSToolStripMenuItem.Click += new System.EventHandler(this.rSSToolStripMenuItem_Click);
            // 
            // licenseInformationToolStripMenuItem
            // 
            this.licenseInformationToolStripMenuItem.Name = "licenseInformationToolStripMenuItem";
            this.licenseInformationToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.licenseInformationToolStripMenuItem.Text = "Licences information";
            this.licenseInformationToolStripMenuItem.Click += new System.EventHandler(this.licenseInformationToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusTxtGeneral,
            this.lblExportImportStatus,
            this.toolStripProgressBar1,
            this.statusTxtReportName,
            this.statusTxtTypeOfReport,
            this.statusTxtReportDate,
            this.statusTxtEntityIdentifier,
            this.statusTxtCurrency,
            this.toolStripStatusRSS});
            this.statusStrip2.Location = new System.Drawing.Point(0, 606);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1074, 24);
            this.statusStrip2.TabIndex = 12;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // statusTxtGeneral
            // 
            this.statusTxtGeneral.Name = "statusTxtGeneral";
            this.statusTxtGeneral.Size = new System.Drawing.Size(225, 19);
            this.statusTxtGeneral.Spring = true;
            this.statusTxtGeneral.Text = "General Message";
            this.statusTxtGeneral.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusTxtReportName
            // 
            this.statusTxtReportName.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusTxtReportName.DoubleClickEnabled = true;
            this.statusTxtReportName.Name = "statusTxtReportName";
            this.statusTxtReportName.Size = new System.Drawing.Size(78, 19);
            this.statusTxtReportName.Text = "ReportName";
            this.statusTxtReportName.DoubleClick += new System.EventHandler(this.changeActiveReportToolStripMenuItem_Click);
            // 
            // statusTxtTypeOfReport
            // 
            this.statusTxtTypeOfReport.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusTxtTypeOfReport.DoubleClickEnabled = true;
            this.statusTxtTypeOfReport.Name = "statusTxtTypeOfReport";
            this.statusTxtTypeOfReport.Size = new System.Drawing.Size(84, 19);
            this.statusTxtTypeOfReport.Text = "TypeOfReport";
            this.statusTxtTypeOfReport.DoubleClick += new System.EventHandler(this.changeActiveReportToolStripMenuItem_Click);
            // 
            // statusTxtReportDate
            // 
            this.statusTxtReportDate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusTxtReportDate.DoubleClickEnabled = true;
            this.statusTxtReportDate.Name = "statusTxtReportDate";
            this.statusTxtReportDate.Size = new System.Drawing.Size(70, 19);
            this.statusTxtReportDate.Text = "ReportDate";
            this.statusTxtReportDate.DoubleClick += new System.EventHandler(this.changeActiveReportToolStripMenuItem_Click);
            // 
            // statusTxtEntityIdentifier
            // 
            this.statusTxtEntityIdentifier.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusTxtEntityIdentifier.DoubleClickEnabled = true;
            this.statusTxtEntityIdentifier.Name = "statusTxtEntityIdentifier";
            this.statusTxtEntityIdentifier.Size = new System.Drawing.Size(88, 19);
            this.statusTxtEntityIdentifier.Text = "EntityIdentifier";
            this.statusTxtEntityIdentifier.DoubleClick += new System.EventHandler(this.changeActiveReportToolStripMenuItem_Click);
            // 
            // statusTxtCurrency
            // 
            this.statusTxtCurrency.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusTxtCurrency.DoubleClickEnabled = true;
            this.statusTxtCurrency.Name = "statusTxtCurrency";
            this.statusTxtCurrency.Size = new System.Drawing.Size(59, 19);
            this.statusTxtCurrency.Text = "Currency";
            this.statusTxtCurrency.DoubleClick += new System.EventHandler(this.changeActiveReportToolStripMenuItem_Click);
            // 
            // toolStripStatusRSS
            // 
            this.toolStripStatusRSS.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusRSS.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.toolStripStatusRSS.Name = "toolStripStatusRSS";
            this.toolStripStatusRSS.Size = new System.Drawing.Size(229, 19);
            this.toolStripStatusRSS.Text = "Press here to see the news in the RSS feed";
            this.toolStripStatusRSS.Click += new System.EventHandler(this.toolStripStatusRSS_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1074, 630);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Tool for Undertakings";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabTopRight.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem formlanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createXbrtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recentFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xBRLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importXBRLIntanceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportXBRLInstanceFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateXBRLReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createANewReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeActiveReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteActiveReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem selectActiveReportIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem localValidationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteValidationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeActiveReportToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ToolStripStatusLabel lblExportImportStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem cRDIVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem applicationLanguageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem integratedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arelleWithValidationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importArelleWithoutValidationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem integratedToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem arelleWithValidationToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exportArelleWithoutValidationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem databaseTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQLiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQLServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changeDatabaseConnectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solvencyIIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solvencyIIPreparatoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDataFromExcelFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem downloadTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateCurrentReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateCurrentContainerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem annotatedTemplatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dPMDictionaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem annotatedFULLTemplatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportDataToBusinessTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem taxononmyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem solvencyIIFULLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cDRIVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem whatsNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preparatoryS2DictionaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preparatoryS2AnnotatedTemplatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem preparatoryS2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databasesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQLServerBackUpsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem databaseDocumentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem PreparatoryS2TestXBRLInstancesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel statusTxtReportName;
        private System.Windows.Forms.ToolStripStatusLabel statusTxtTypeOfReport;
        private System.Windows.Forms.ToolStripStatusLabel statusTxtEntityIdentifier;
        private System.Windows.Forms.ToolStripStatusLabel statusTxtGeneral;
        private System.Windows.Forms.ToolStripStatusLabel statusTxtReportDate;
        private System.Windows.Forms.ToolStripStatusLabel statusTxtCurrency;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem logAndSystemDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rCBusinessCodeMappingToolStripMenuItem;
        private System.Windows.Forms.ImageList treeViewImageList;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusRSS;
        private System.Windows.Forms.ToolStripMenuItem downloadEnumerationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fullS2TestXBRLInstancesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rSSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sQLServerBackUpFullToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDataToExcelBusinessTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem downloadAnEmptyBusinessExcelTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem licenseInformationToolStripMenuItem;
        private System.Windows.Forms.TabPage tabCellPropertiesPage;
        private System.Windows.Forms.TabPage tabValidationErrorPage;
        private System.Windows.Forms.TabControl tabTopRight;
    }
}