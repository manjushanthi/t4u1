using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.Data;
using SolvencyII.UI.Shared.Delegates;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.Managers;

namespace SolvencyII.UI.Shared.UserControls
{
    /// <summary>
    /// Base class for the Special cases.
    /// </summary>
    public partial class Open2DUserControlBase : UserControl
    {
        private const long MAX_NUMBER_OF_RECORDS_THAT_CAN_BE_SORTED = 10000;

        #region Declarations, Properties and Contructor

        public long InstanceID { get; set; }
        public int LanguageID { get; set; }
        public int FilingTemplateOrTableID { get; set; }
        private List<ISolvencyPageControl> _pageControls;
        private List<ISolvencyPageControl> _pageTextBoxControls;

        private bool _useVirtualObject = true;
        public bool UseVirtualObject
        {
            get
            {
                dataListView1.Visible = !_useVirtualObject;
                virtualObjectListView1.Visible = _useVirtualObject;
                return _useVirtualObject;
            }
            set { _useVirtualObject = value; }
        }
        private List<OpenColInfo2> LocalColumns { get; set; }
        public bool Set_dFilingIndicator { get; set; }


        public ToolTip ToolTipObject { get; set; }
        private List<Control> _formControls;
        private List<ISolvencyComboBox> _pageComboBoxControls;

        private ISolvencyOpenUserControl OpenUserControl
        {
            get
            {
                if (!DesignMode)
                {
                    return (ISolvencyOpenUserControl) this;
                }
                return null;
            }
        }

        private Dictionary<string, string> SpecifiedColumnsFromCombos
        {
            get
            {
                if (!DesignMode)
                {
                    return (GetPAGEnComboBoxControls().ConvertAll(c => (ISolvencyComboBox) c)).ToDictionary(x => x.ColName, x => x.GetValue);
                }
                return null;
            }
        }

        

        private void LocalOpenComboBoxOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            // The combo has changed...
            // Reset the data access
            if (SaveRow())
            {
                SetupData();
                if (UseVirtualObject)
                    virtualObjectListView1.Refresh();
                else
                    dataListView1.Refresh();
                SetEnabledState();
            }
        }

        private void SetEnabledState()
        {
            bool enable  = PageCombosCheck();
            if (UseVirtualObject)
                virtualObjectListView1.Enabled = enable;
            else
                dataListView1.Enabled = enable;

            //if (SpecifiedColumnsFromCombos.Any(c => string.IsNullOrEmpty(c.Value)))
            //   virtualObjectListView1.Enabled = false;
            //else
            //   objectListView.Enabled = true;

        }

        public bool PageCombosCheck()
        {
            // The text here corresponds to that added in ClosedTableManager.PopulateNPageCombos
            return GetPAGEnComboBoxControls().All(formDataPage => !string.IsNullOrEmpty(formDataPage.GetValue) & formDataPage.GetValue != "Please select or press add button" & TagNullOrFalse(formDataPage));
        }
        private bool TagNullOrFalse(ISolvencyComboBox iComboBox)
        {
            ListViewItem selectedItem = ((ComboBox)iComboBox).SelectedItem as ListViewItem;
            if (selectedItem != null)
            {
                if (selectedItem.Tag == null) return true;
                if (selectedItem.Tag is bool)
                    return !(bool)selectedItem.Tag;
            }
            return true;
        }


        public Open2DUserControlBase()
        {
            InitializeComponent();
            Set_dFilingIndicator = false;
        }

        #endregion

        #region Setup functions

        public void SetupOpenUserControl(TreeItem selectedItem)
        {
            SetupGridFormPosition();
            Stopwatch sw = new Stopwatch();
            LocalColumns = OpenUserControl.Columns.ConvertAll(c => (OpenColInfo2)c);
            sw.Start();
            using (GetSQLData getData = new GetSQLData())
            {
                if (UseVirtualObject) SetupColumns(getData);
                sw.Stop();
                Console.WriteLine("OpenTemplateUserControl SetupColumns {0}ms", sw.ElapsedMilliseconds);
                sw.Reset();
                sw.Start();
                // SetupCombos(getData);
                SetupPAGEnCombos(getData, true, GetPAGEnComboBoxControls(), LanguageID);
                sw.Stop();
                Console.WriteLine("OpenTemplateUserControl SetupCombos {0}ms", sw.ElapsedMilliseconds);
                sw.Reset();
                sw.Start();
                if (selectedItem.SingleZOrdinateID != 0)
                {
                    List<FormDataPage> controlSetupPAGEn = getData.GetFixedDimensionPageData(selectedItem.GroupTableIDs.Split('|').ToList(), selectedItem.SingleZOrdinateID);
                    PopulatePAGEnControls(controlSetupPAGEn);
                }
                SetupData();
                SetEnabledState();
                sw.Stop();
                Console.WriteLine("OpenTemplateUserControl SetupData {0}ms", sw.ElapsedMilliseconds);
                sw.Reset();
                sw.Start();
                if (UseVirtualObject)
                    SetupEventsAndGrid(virtualObjectListView1);
                else
                    SetupEventsAndGrid(dataListView1);
                sw.Stop();
                Console.WriteLine("OpenTemplateUserControl SetupEventsAndGrid {0}ms", sw.ElapsedMilliseconds);
                sw.Reset();
            }
        }

        public List<ISolvencyPageControl> GetPAGEnControls()
        {
            if (_pageControls == null)
            {
                Type solvencyType = typeof(ISolvencyPageControl);
                IEnumerable<ISolvencyPageControl> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyPageControl>();
                _pageControls = (from ISolvencyPageControl control in midStep where (control.ColName.StartsWith("PAGE")) select control).ToList();
            }
            return _pageControls;
        }

        public List<ISolvencyPageControl> GetPAGEnTextBoxControls()
        {
            if (_pageTextBoxControls == null)
            {
                Type solvencyType = typeof(SolvencyPageTextBox);
                IEnumerable<ISolvencyPageControl> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyPageControl>();
                _pageTextBoxControls = (from ISolvencyPageControl control in midStep where (control.ColName.StartsWith("PAGE")) select control).ToList();
            }
            return _pageTextBoxControls;
        }

        public void PopulatePAGEnControls(IEnumerable<FormDataPage> pageData)
        {
            if (pageData != null)
            {
                foreach (FormDataPage item in pageData.Where(p => p.FixedDimension))
                {
                    // We have a name:
                    string name = item.DYN_TAB_COLUMN_NAME.ToUpper();
                    // Locate the control and set the result
                    ISolvencyPageControl caughtControl = GetPAGEnControls().FirstOrDefault(c => c.ColName.ToUpper() == name);
                    if (caughtControl != null)
                        caughtControl.Text = item.Value;
                }
            }
        }

        public GenericDelegates.BoolResponseWithPkID PanelChange;

        private void SetPanelChange(bool subControlVisible, int pkID, List<ISolvencyComboBox> parentCombos)
        {
            if (PanelChange != null)
                PanelChange(subControlVisible, pkID, parentCombos);
        }

        private void SetupGridFormPosition()
        {
            int gridLocationY = GetGridLocationY(); // ((ISolvencyOpenUserControl)this).GridTop;
            if (UseVirtualObject)
            {
                virtualObjectListView1.Location = new Point(0, gridLocationY);
                virtualObjectListView1.Size = new Size(this.Size.Width, Size.Height - gridLocationY);
                virtualObjectListView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                // Reply upon order from cache
                virtualObjectListView1.PrimarySortOrder = SortOrder.None;
            }
            else
            {
                dataListView1.Location = new Point(0, gridLocationY);
                dataListView1.Size = new Size(this.Size.Width, Size.Height - gridLocationY);
                dataListView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                // Reply upon order from cache
                dataListView1.PrimarySortOrder = SortOrder.None;

            }
        }

        private void SetupColumns()
        {
            using (GetSQLData getData = new GetSQLData())
            {
                SetupColumns(getData);
            }
        }

        private void SetupColumns(GetSQLData getData)
        {
            ObjectListView activeGrid;
            if (UseVirtualObject)
                activeGrid = virtualObjectListView1;
            else
                activeGrid = dataListView1;

            List<ISolvencyCollectionMember> cols = OpenUserControl.Columns;
            activeGrid.Columns.Clear();
            int colNumber = 0;
            foreach (OpenColInfo2 colInfo in cols)
            {
                if (colInfo.ColType != "DATE")
                    activeGrid.Columns.Add(CreateColumn(string.Format("{0}\n{1}", colInfo.Label, colInfo.OrdinateCode), colNumber++, true, 75, colInfo.ColType, colInfo.ColName));
                else
                    activeGrid.Columns.Add(CreateColumn(string.Format("{0}\n{1}", colInfo.Label, colInfo.OrdinateCode), colNumber++, true, 85, colInfo.ColType, colInfo.ColName));

                if (colInfo.ColType == "ENUMERATION/CODE")
                {
                    if (colInfo.HierarchyID != 0)
                    {
                        // Populate the combo box data.
                        colInfo.Dimensions = getData.HierarchyLookup2(colInfo.HierarchyID, colInfo.StartOrder, colInfo.NextOrder, LanguageID, colInfo.OrdinateID);
                        if (!colInfo.Dimensions.Any())
                        {
                            List<ComboItem> memComboItems = getData.GetzAxisMemberComboItems(colInfo.AxisID, LanguageID, colInfo.StartOrder, colInfo.NextOrder);
                            foreach (ComboItem comboItem in memComboItems)
                            {
                                colInfo.Dimensions.Add(comboItem.ConvertToOpenComboItem());
                            }
                        }
                    }
                    else
                    {
                        if (colInfo.AxisID != 0)
                        {
                            List<ComboItem> result = getData.GetzAxisMemberComboItems(colInfo.AxisID, LanguageID, colInfo.StartOrder, colInfo.NextOrder);
                            colInfo.Dimensions = result.Select(comboItem => comboItem.ConvertToOpenComboItem()).ToList();
                        }
                    }
                }

            }
            activeGrid.HeaderWordWrap = true;
            
        }

        private void SetupCombos(GetSQLData getData)
        {

            List<ISolvencyComboBox> midStep = GetPAGEnComboBoxControls();
            Dictionary<string, string> startingEntries = midStep.Distinct().ToDictionary(c => c.ColName, c => "");
            getData.GetnPageStartingData(InstanceID, new List<string>{ OpenUserControl.DataTable}, ref startingEntries);

            foreach (ISolvencyComboBox combo in midStep)
            {
                List<ComboItem> popData = new List<ComboItem>();
                // Get OrdinateID for z dimension where the MemberID != 9999
                List<ComboItem> memComboItems = getData.GetzAxisMemberComboItems(combo.AxisID, LanguageID, combo.StartOrder, combo.NextOrder);

                if (memComboItems.Count > 1)
                {
                    // We need to look up the Hierachry and populate popData
                    //List<ComboItem> memComboItems = getData.GetzAxisMemberComboItems(combo.AxisID, LanguageID);

                    // If there is only one there is no selection to be made
                    if (memComboItems.Count > 1) popData.Add(new ComboItem { Value = "", Text = "Please select" });
                    popData.AddRange(memComboItems);
                    combo.TypeOfItems = ComboItemType.MemberItems;
                }
                else
                {
                    List<ComboItem> ordComboItems = getData.GetzAxisOrdinateComboItems(combo.AxisID, LanguageID);
                    // If there is only one there is no selection to be made
                    if (ordComboItems.Count > 1) popData.Add(new ComboItem { Value = "-1", Text = "Please select" });

                    // Populate popData from ordinates
                    popData.AddRange(ordComboItems);
                    combo.TypeOfItems = ComboItemType.AxisOrdinates;
                }
                combo.PopulateWithComboItems(popData, startingEntries[combo.ColName]);

                if (combo.ColumnType == SolvencyDataType.Code)
                {
                    ((SolvencyComboBox)combo).SetDropDownWidth();
                }   

                combo.SetSelectedIndexChanged(LocalOpenComboBoxOnSelectedIndexChanged);
                combo.SetOnDropDown(ComboBoxOnDropDown);
                    ((ComboBox)combo).GotFocus += (sender, args) => SaveRow();
                
                    
            }
        }

        private void SetupPAGEnCombos(GetSQLData getData, bool setupNPageFirstEntries, List<ISolvencyComboBox> midStep, int languageID)
        {
            Dictionary<string, string> startingEntries = midStep.Distinct().ToDictionary(c => c.ColName, c => "");
            if (setupNPageFirstEntries)
                getData.GetnPageStartingData(InstanceID, new List<string> { OpenUserControl.DataTable }, ref startingEntries);

            PopulateNPageCombos.PopulateCombosNPage(getData, InstanceID, new List<string> { OpenUserControl.DataTable }, languageID, midStep, startingEntries, LocalOpenComboBoxOnSelectedIndexChanged, ComboBoxOnDropDown, null, ComboBoxGotFocus);

        }

        private void SetupData()
        {

            // If I get to revisit this I could put the following in the templates:
            //IVirtualListDataSource DataSource = new vDataSource3<T__3001_S_06_02_01_01>();
            //DataSource.Setup(OpenUserControl.DataType, OpenUserControl.DataTable, InstanceID, LocalColumns, SpecifiedColumnsFromCombos);
            //virtualObjectListView1.VirtualListDataSource = DataSource;
            // vDataSource3 could then marry up to the GenericRepository

            List<ISolvencyPageControl> ctrls = GetPAGEnTextBoxControls();

            if (UseVirtualObject)
            {
                if (virtualObjectListView1.VirtualListDataSource.GetType() == typeof (vDataSource3))
                {
                    ((vDataSource3) virtualObjectListView1.VirtualListDataSource).Dispose();
                }
                vDataSource3 _dataSource = new vDataSource3(OpenUserControl.DataType, OpenUserControl.DataTable, InstanceID, LocalColumns, SpecifiedColumnsFromCombos, ctrls);
                _dataSource.refreshList += OnRefreshList;
                virtualObjectListView1.VirtualListDataSource = _dataSource;
                VirtualObjectViewDelegates2.vDataSource = _dataSource;
                if (_dataSource.GetObjectCount() > MAX_NUMBER_OF_RECORDS_THAT_CAN_BE_SORTED)
                {
                    foreach (var column in virtualObjectListView1.Columns)
                    {
                        ((OLVColumn) column).Sortable = false;
                    }
                }
                    virtualObjectListView1.Sort();
            }

        }

        private void OnRefreshList()
        {
            virtualObjectListView1.Invalidate();
            // virtualObjectListView1.BuildList(); // Works but columns sometimes need a second click to order correctly.
        }

        private void SetupEventsAndGrid(VirtualObjectListView grid)
        {
            VirtualObjectViewDelegates2.ClearAllRefs();

            // grid.CellEditActivation = ObjectListView.CellEditActivateMode.SingleClick;
            // grid.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
            //grid.CellEditStarting += VirtualObjectViewDelegates2.objectListView_CellEditStarting;
            //grid.CellEditValidating += VirtualObjectViewDelegates2.objectListView_OnCellEditValidating;
            //grid.CellEditFinishing += VirtualObjectViewDelegates2.objectListView_OnCellEditFinishing;
            grid.FormatRow += VirtualObjectViewDelegates2.objectListView_FormatRow;
            grid.CellToolTipShowing += VirtualObjectViewDelegates2.objectListView_CellToolTipShowing;
            
            // Delete removed by removeing this line.
            // grid.CellRightClick += VirtualObjectViewDelegates2.objectListView_CellRightClick;

            grid.SelectionChanged += SelectionChanged;

            VirtualObjectViewDelegates2.SetColumns(OpenUserControl.Columns.ConvertAll(c => (OpenColInfo2) c));
            VirtualObjectViewDelegates2.SaveCurrentRow += SaveRow;
            // VirtualObjectViewDelegates2.CacheCurrentRow += cacheRow;
            VirtualObjectViewDelegates2.DeleteCurrentRow += DeleteRow;
            
            // Used for cache reference
            VirtualObjectViewDelegates2.vDataSource = (vDataSource3)grid.VirtualListDataSource;

            grid.FullRowSelect = true;
            grid.GridLines = true;
            

            /* If for any reason column one does not allow editing with a single click its because the ObjectListView
             * code needs a slight modification.
             * ObjectListView.cs
             * protected override void OnMouseUp(MouseEventArgs e)
             * To include:
             // We don't edit the primary column by single clicks -- only subitems.
             // Modified by NAJ to allow standard editing of first column unless the full row requires selecting.
             if (this.CellEditActivation == CellEditActivateMode.SingleClick && (args.ColumnIndex <= 0 && this.FullRowSelect))
                 return;
             * 
             */

            // Cache Events
            //grid.SelectedIndexChanged +=
            //grid.SelectionChanged += (sender, args) => SaveRow();
            // grid.LostFocus += (sender, args) => SaveRow();
            // grid.Disposed += (sender, args) => SaveRow();
            
            Form myForm = FindForm();
            if(myForm != null)
                myForm.FormClosing += ParentForm_FormClosing;

            // Use enter key to force save:
            grid.KeyDown += OnKeyDown;

            // A click on a blank line forces save:
            grid.SaveRow += () => SaveRow();

        }

        private void SetupEventsAndGrid(DataListView grid)
        {
            VirtualObjectViewDelegates2.ClearAllRefs();

            grid.FormatRow += VirtualObjectViewDelegates2.objectListView_FormatRow;
            grid.CellToolTipShowing += VirtualObjectViewDelegates2.objectListView_CellToolTipShowing;
            grid.CellRightClick += VirtualObjectViewDelegates2.objectListView_CellRightClick;
            grid.SelectionChanged += SelectionChanged;

            VirtualObjectViewDelegates2.SetColumns(OpenUserControl.Columns.ConvertAll(c => (OpenColInfo2)c));
            VirtualObjectViewDelegates2.SaveCurrentRow += SaveRow;
            VirtualObjectViewDelegates2.DeleteCurrentRow += DeleteRow;

            grid.FullRowSelect = true;
            grid.GridLines = true;
            grid.ShowGroups = false;


        }

        private void SelectionChanged(object sender, EventArgs e)
        {
            int pkID = 0;
            if (UseVirtualObject)
            {
                OpenTableDataRow2 row = (OpenTableDataRow2) virtualObjectListView1.SelectedItem.RowObject;
                pkID = row.PK_ID;
            }
            else
            {
                DataRowView row = (DataRowView) dataListView1.SelectedItem.RowObject;
                int.TryParse(row[row.DataView.Table.Columns["PK_ID"].Ordinal].ToString(), out pkID);
            }
            List<ISolvencyComboBox> combos = GetPAGEnComboBoxControls();
            SetPanelChange(true, pkID, combos);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveRow();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ComboBoxOnDropDown(object sender, EventArgs eventArgs)
        {
            using (GetSQLData getData = new GetSQLData())
            {
                PageCombosEnBold(getData, sender);
            }
        }

        private void ComboBoxGotFocus(object sender, EventArgs eventArgs)
        {
            SaveRow();
        }

        #endregion

        #region Caching and Cache related

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveRow())
            {
                e.Cancel = true;
            }
        }


        public bool SaveRow()
        {
            bool success = true;
            //vDataSource3 refDs = (vDataSource3)objectListView.VirtualListDataSource;
            //if (refDs.CacheItem != null)
            //{
            //    success = SaveRow(refDs);
            //    if (success)
            //    {
            //        refDs.UpdateSecondaryCache();
            //        refDs.CacheItem = null;
            //       objectListView.BuildList();
            //    }
            //    else
            //    {
            //        // Put the cursor onto the correct row.
            //        //MoveCursorToRowWithKey(refDs.CacheItem.PK_ID);
            //    }
            //}
            return success;

        }

        #endregion

        #region Row Changes -> Database updates



        private void DeleteRow(OpenTableDataRow2 row)
        {
            if (row != null)
            {
                PutSQLData putData = new PutSQLData();
                putData.DeleteOpenTableData2(row, OpenUserControl.DataTable, InstanceID);
                putData.DeleteFilingIndicator(InstanceID, this.FilingTemplateOrTableID, OpenUserControl.DataTable);
                putData.Dispose();
                if (UseVirtualObject)
                {
                    // Update the secondary cache
                    vDataSource3 refDs = (vDataSource3) virtualObjectListView1.VirtualListDataSource;
                    refDs.DeleteRowSecondaryCache(row.PK_ID);
                    // Cause the grid to refresh;
                    virtualObjectListView1.BuildList();
                }
                else
                {
                    // Update the secondary cache
                    throw new Exception("This needs to be changed - if it is actually used.");
                }

            }

        }

        #endregion

        #region Control Gathering

        private List<Control> FormControls
        {
            get
            {
                if (_formControls == null)
                {
                    _formControls = new List<Control>();
                    GetAllControls(this, ref _formControls);
                }
                return _formControls;
            }
        }
        private void GetAllControls(Control container, ref List<Control> result)
        {
            foreach (Control c in container.Controls)
            {
                GetAllControls(c, ref result);
                result.Add(c);
            }
        }

        public List<ISolvencyComboBox> GetPAGEnComboBoxControls()
        {
            if (_pageComboBoxControls == null)
            {
                Type solvencyType = typeof (ISolvencyComboBox);
                IEnumerable<ISolvencyComboBox> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyComboBox>();
                _pageComboBoxControls = (from ISolvencyComboBox control in midStep where (control.ColName.StartsWith("PAGE")) select control).ToList();
            }
            return _pageComboBoxControls;
        }

        #endregion

        #region Row Key Investigation

        public List<OpenColInfo2> GetRowKeyCols()
        {
            List<OpenColInfo2> result = OpenUserControl.Columns.ConvertAll(c => (OpenColInfo2) c).Where(d => d.IsRowKey).ToList();
            return result;
        }

        #endregion

        #region Helper functions

        public void PageCombosEnBold(GetSQLData getData, object sender)
        {
            SolvencyComboBox changedControl = sender as SolvencyComboBox;
            if (changedControl != null)
            {
                ISolvencyOpenUserControl ctrl = (ISolvencyOpenUserControl)this;
                var table = ctrl.DataTable;
                List<string> boldValues = new List<string>();
                    string where = string.Format("Where INSTANCE = {0} {1}", InstanceID, ComboHighLightWhere(table, changedControl.ColName));
                    boldValues.AddRange(getData.GetComboBoxHighlights(table, changedControl.ColName, where));

                foreach (var item in changedControl.Items)
                {
                    ListViewItem listViewItem = (ListViewItem)item;
                    listViewItem.Font = new Font(listViewItem.Font, FontStyle.Regular);
                    string thisItem = listViewItem.Name;
                    if (boldValues.Any(v => v == thisItem))
                        listViewItem.Font = new Font(listViewItem.Font, FontStyle.Bold);
                }
            }

        }

        private string ComboHighLightWhere(string tableName, string colName)
        {
            string result = "";
            foreach (var comboControl in GetPAGEnComboBoxControls())
            {
                List<string> tables = comboControl.TableNames.Split('|').ToList();
                int pos = tables.IndexOf(tableName);
                if (pos != -1)
                {
                    if (colName != comboControl.ColName)
                    {
                        if (!comboControl.ValueIsBlank())
                            result += string.Format("AND {0} = {1} ", comboControl.ColName, comboControl.SQLValue());
                        else
                            result += string.Format("AND {0} like '%' ", comboControl.ColName);
                    }
                }
            }
            return result;
        }

        private int GetGridLocationY()
        {
            if (OpenUserControl != null)
                return OpenUserControl.GridTop + 23; // + Insert button
            return 0;
        }   

        private string RowComboLookup(int colNumber, string colValue)
        {
            if (LocalColumns[colNumber].Dimensions != null)
            {
                var result = LocalColumns[colNumber].Dimensions.FirstOrDefault(d => d.Name == colValue);
                if (result != null)
                    return result.Text;
            }
            return "Select";
        }

        /// <summary>
        /// Open Template Data Formating for the Columns. Convert to String to display.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="colNumber"></param>
        /// <param name="editable"></param>
        /// <param name="width"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        private OLVColumn CreateColumn(string text, int colNumber, bool editable, int width, string dataType, string colName)
        {
            OLVColumn working = new OLVColumn
            {
                Text = text,
                AspectGetter = rowObject => RowAspectGetter(colNumber, rowObject),
                IsEditable = editable,
                Width = width,
                ToolTipText = text,
                DataType = dataType.GetColumnType(),
                TextAlign = HorizontalAlignment.Right,
                AutoCompleteEditor = false,
                AspectName = colName
            };
            working.AspectPutter += (rowObject, value) => { RowAspectPutter(rowObject, value, colNumber); };
            return working;
        }

        private object RowAspectGetter(int colNumber, object rowObject)
        {
        
            string colValue;

            if (rowObject is OpenTableDataRow2)
            {
                var row = (OpenTableDataRow2) rowObject;
                colValue = row.ColValues[colNumber];
            }
            else
            {
                var row = (DataRowView)rowObject;
                colValue = row[colNumber].ToString();
            }

            if (!string.IsNullOrEmpty(colValue))
            {
                if (LocalColumns[colNumber].HierarchyID != 0)
                    // We have a combo so need to do a look up;
                    return RowComboLookup(colNumber, colValue);
                switch (LocalColumns[colNumber].ColType)
                {
                    case "BOOLEAN":
                        if (string.IsNullOrEmpty(colValue)) return "";
                        return colValue == "1" ? "true" : "false";
                    case "INTEGER":
                        decimal temp;
                        if (string.IsNullOrEmpty(colValue)) return "";
                        if (decimal.TryParse(colValue, out temp)) return temp.ToString("N0");
                        return (int.Parse(colValue)).ToString("N0");
                    case "PERCENTAGE":
                        if (string.IsNullOrEmpty(colValue)) return "";
                        string stripped = colValue.Replace(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.PercentSymbol, "");
                        return stripped.PercentageToString(CultureInfo.CurrentCulture);
                    case "DECIMAL":
                        if (string.IsNullOrEmpty(colValue)) return "";
                        return colValue.DecimalToString(CultureInfo.CurrentCulture, 2);
                    case "MONETARY":
                        if (string.IsNullOrEmpty(colValue)) return "";
                        return colValue.DecimalToString(CultureInfo.CurrentCulture, 2);
                    case "DATE":
                        if (string.IsNullOrEmpty(colValue)) return "";
                        DateTime check = new DateTime();
                        if (!DateTime.TryParse(colValue, CultureInfo.CurrentCulture, DateTimeStyles.None, out check)) return "";
                        return check.ToString("d");

                    default:
                        return colValue;
                }
            }
            return "";
        }

        private void RowAspectPutter(object rowObject, object value, int colNumber)
        {
            ((OpenTableDataRow2) rowObject).ColValues[colNumber] = ((string) value);
        }

        #endregion

        #region Text Combo Events

        public void AddSingleControlText(object sender)
        {
            SolvencyButton btn = (SolvencyButton) sender;
            ISolvencyComboBox comboBoxI = GetPAGEnComboBoxControls().FirstOrDefault(c => c.ColumnType == SolvencyDataType.String && c.ColName == btn.ColName);
            if (comboBoxI != null)
            {
                SolvencyTextComboBox comboBox = (SolvencyTextComboBox) comboBoxI;
                if (comboBox.ColumnType == SolvencyDataType.String)
                {
                    comboBox.AddEntry();
                }
            }
            // The user now needs to press save.
        }

        protected void DeleteSingleControlText(object sender)
        {
            List<ISolvencyComboBox> comboBoxes = GetPAGEnComboBoxControls().Where(c => c.ColumnType == SolvencyDataType.String).ToList();
            if (comboBoxes.Count() == 1)
            {
                SolvencyTextComboBox comboBox = (SolvencyTextComboBox) comboBoxes[0];
                comboBox.DeleteEntry();
            }
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            //try
            //{
            if (UseVirtualObject)
            {
                virtualObjectListView1.Enabled = false;
                virtualObjectListView1.Dispose();
            }
            else
            {
                dataListView1.Enabled = false;
                dataListView1.Dispose();
            }
            //}
            //catch (ArgumentOutOfRangeException ex)
            //{
            //    // This can happen when the OpenGrid fires its conveluted event structures based upon windows messaging.
            //    // Outside of the t4u scope this message is ignored and the event will continue to 
            //    // dispose.
            //}

            base.Dispose(disposing);
        }

        #endregion

        private void btnInsert_Click(object sender, EventArgs e)
        {
            List<ISolvencyComboBox> combos = GetPAGEnComboBoxControls();
            SetPanelChange(true, -1, combos);
        }

    }
}
