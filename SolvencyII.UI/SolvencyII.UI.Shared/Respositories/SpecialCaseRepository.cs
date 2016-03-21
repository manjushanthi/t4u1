using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain;
using SolvencyII.Domain.Constants;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.Data;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.Shared.Respositories
{
    /// <summary>
    /// When the template is a special case with two open dimensions this repository is used.
    /// </summary>
    public class SpecialCaseRepository<T> : IClosedRowRepository
        where T : Control, IClosedRowControl, new()
    {

        #region Properties and Delegates

        private int CONTROL_MARGIN = 7;
        private List<T> ClosedControls { get; set; } 

        private Panel ControlParentPanel { get; set; }
        private bool Horizontal { get; set; }
        private List<string> TableNames { get; set; }
        private List<mAxisOrdinate> LabelText { get; set; }
        private ToolTip _toolTipObject;
        public bool IsDirty { get; set; }

        // If the PkID is anything other than 0 it is being used for OpenTemplate sub control.
        public int PkID { get; private set; }

        public bool Enabled
        {
            get
            {
                return ClosedControls[0].Enabled;
            }
            set
            {
                foreach (T cntr in ClosedControls)
                {
                    cntr.Enabled = value;
                }
            }
        }

        public event GenericDelegates.BoolResultQuestion AskUserQuestion;
        protected bool OnAskUserQuestion(string question)
        {
            if (AskUserQuestion != null) return AskUserQuestion(question);
            return false;
        }
        public event GenericDelegates.Response DeleteControlDR;
        public event GenericDelegates.ListLongs DeleteControl;
        protected bool OnDeleteControl(List<long> pkeys)
        {
            if (OnAskUserQuestion("Are you sure you want to delete this entry?\r\nOnce deleted it cannot be restored."))
            {
                if (DeleteControl != null) return DeleteControl(pkeys);
            }
            return false;
        }

        public event GenericDelegates.StringResponse AlertUser;
        protected void OnAlertUser(string question)
        {
            if (AlertUser != null) 
                AlertUser(question);
        }

        private SolvencyDataComboBox _columnCombo;
        private SolvencyDataComboBox _rowCombo;
        private SolvencyButton _buttonAddCol;
        private SolvencyButton _buttonAddRow;
        private SolvencyButton _buttonDelCol;
        private SolvencyButton _buttonDelRow;

        private ISolvencyDataControl _textCell;

        private readonly ConstantsForDesigner constants = new ConstantsForDesigner();

        private List<SolvencyDataComboBox> _columnCombos = new List<SolvencyDataComboBox>();
        private List<SolvencyDataComboBox> _rowCombos =  new List<SolvencyDataComboBox>();
        private List<SolvencyButton> _buttonDelCols = new List<SolvencyButton>();
        private List<SolvencyButton> _buttonDelRows = new List<SolvencyButton>();
        private List<ISolvencyDataControl> _textCells = new List<ISolvencyDataControl>();

        public void AddControl()
        {
            CreateControl();
        }
        
        #endregion

        #region Constructors

        public SpecialCaseRepository(T baseControl, Panel parentPanel, bool horizontal, List<string> tableNames)
        {
            ClosedControls = new List<T> {baseControl};
            ControlParentPanel = parentPanel;
            Horizontal = horizontal;
            TableNames = tableNames;
            baseControl.AddControl += AddControl;
            baseControl.DelControl += OnDeleteControl;
            baseControl.AddRow += AddRow;
            baseControl.AddCol += AddCol;
            baseControl.DelRow += DelRow;
            baseControl.DelCol += DelCol;

            SetupEvents(ClosedControls[0]);

            _columnCombo = ClosedControls[0].Controls.Find("cboCol", true).FirstOrDefault() as SolvencyDataComboBox;
            _rowCombo =  ClosedControls[0].Controls.Find("cboRow", true).FirstOrDefault() as SolvencyDataComboBox;
            _buttonAddCol = ClosedControls[0].Controls.Find("btnAddCol", true).FirstOrDefault() as SolvencyButton;
            _buttonAddRow = ClosedControls[0].Controls.Find("btnAddRow", true).FirstOrDefault() as SolvencyButton;
            _buttonDelCol = ClosedControls[0].Controls.Find("btnDelCol", true).FirstOrDefault() as SolvencyButton;
            _buttonDelRow = ClosedControls[0].Controls.Find("btnDelRow", true).FirstOrDefault() as SolvencyButton;
            _textCell = ClosedControls[0].Controls.Find("txtCell", true).FirstOrDefault() as ISolvencyDataControl;
            
            if(_textCell != null)
                _textCell.Name = CellKey.CellKeyToText(0, 0, 0);

            _columnCombos.Add(_columnCombo);
            _rowCombos.Add(_rowCombo);
            _textCells.Add(_textCell);
            _buttonDelCols.Add(_buttonDelCol);
            _buttonDelRows.Add(_buttonDelRow);
            
            ResetControlCollections();


            ClosedControls[0].Dock = DockStyle.Fill;

            // **** Development Note *****
            // Each cell will hold its row and column in its name with a delimiter. The primary key, when known will also be stored there.


        }

        

        #endregion

        #region Form Events

        private void AddCol(object sender, EventArgs e)
        {
            // Add new Column combo
            int newColNumber = _columnCombos.Count();
            CreateColumnCombo(newColNumber, "");
            CreateColumnComboDelete(newColNumber);
            // Add new data controls
            for (int i = 0; i < _rowCombos.Count; i++)
            {
                CreateDataControlAtPosition(i, newColNumber, 0, "");
            }
            // Move the add column button
            _buttonAddCol.Location = new Point(_columnCombos[_columnCombos.Count - 1].Location.X + constants.CURRENCY_COLUMN_WIDTH, _buttonAddCol.Location.Y);
        }

        private void AddRow(object sender, EventArgs e)
        {
            // Add new Column combo
            int newRowNumber = _rowCombos.Count();
            CreateRowCombo(newRowNumber, "");
            CreateRowComboDelete(newRowNumber);
            // Add new data controls
            for (int j = 0; j < _columnCombos.Count; j++)
            {
                CreateDataControlAtPosition(newRowNumber, j, 0, "");
            }
            // Move the add column button
            _buttonAddRow.Location = new Point(_buttonAddRow.Location.X, _rowCombos[_rowCombos.Count - 1].Location.Y + constants.ROW_HEIGHT);

        }

        private void DelRow(object sender, EventArgs e)
        {
            int rowNumber = GetButtonInt(sender);
            _rowCombos[rowNumber].Dispose();
            _buttonDelRows[rowNumber].Dispose();
            string wildCard = CellKey.RegexWildCardForRow(rowNumber);
            Regex match = new Regex(wildCard);
            for (int i = _textCells.Count - 1; i >= 0; i--)
            {
                if (match.IsMatch(_textCells[i].Name))
                {
                    _textCells[i].Dispose();
                    _textCells.RemoveAt(i);
                }
            }

        }

        private void DelCol(object sender, EventArgs e)
        {
            int colNumber = GetButtonInt(sender);
            // Remove Form controls
            _columnCombos[colNumber].Dispose();
            _buttonDelCols[colNumber].Dispose();
            string wildCard = CellKey.RegexWildCardForCol(colNumber);
            Regex match = new Regex(wildCard);
            for (int i = _textCells.Count - 1; i >= 0; i--)
            {
                if (match.IsMatch(_textCells[i].Name))
                {
                    _textCells[i].Dispose();
                    _textCells.RemoveAt(i);
                }
            }

        }

        private int GetButtonInt(object sender)
        {
            string name = ((SolvencyButton)sender).Name;
            name = name.Substring(9);
            int number = 0;
            int.TryParse(name, out number);
            return number;
        }

        #endregion
        
        #region Setup Parameters

        public void PopulateLabels(List<mAxisOrdinate> labelText)
        {
            LabelText = labelText;
        }

        public void SetToolTipObject(object toolTip)
        {
            if (typeof (ToolTip) == toolTip.GetType())
                _toolTipObject = (ToolTip) toolTip;
        }

        #endregion

        #region Public functions

        public bool IsValid()
        {
            bool valid = true;
            foreach (T closedControl in ClosedControls)
            {
                List<ISolvencyDataControl> dataControls = closedControl.GetDataControls();
                foreach (ISolvencyDataControl dataControl in dataControls)
                {
                    valid = valid & dataControl.IsValid();
                }
                List<ISolvencyComboBox> dataComboBox = closedControl.GetComboControls();
                foreach (ISolvencyComboBox dataControl in dataComboBox)
                {
                    valid = valid & dataControl.IsValid();
                }
            }
            return valid;
        }

        public bool SaveAll(ISolvencyUserControl userControl, List<ISolvencyPageControl> nPageControls, TreeItem selectedItem, long instanceID, out string errorText)
        {
            // We are given the nPage details and the template reference.
            // With the use of ClosedControls we can save all records.

            List<string> queries = new List<string>();
            List<Dictionary<string, object>> parameters = new List<Dictionary<string, object>>();
            errorText = "";
            StringBuilder sb = new StringBuilder();

            sb.Append(ValidateForm());

            if (string.IsNullOrEmpty(sb.ToString()))
            {

                SQLSpecialUpdate.BuildSQLQuery_Update(ref queries, ref parameters, userControl.DataTables[0], _textCells.OrderBy(c=>c.Name).ToList(), _columnCombos, _rowCombos, instanceID, nPageControls);

                string resultIsControlDataValid = IsControlDataValid(userControl, ClosedControls, nPageControls);
                if (!string.IsNullOrEmpty(resultIsControlDataValid)) MessageBox.Show(resultIsControlDataValid, "Consistency Check");

                if (queries.Any())
                {
                    List<long> result;
                    string errors;
                    using (PutSQLData putData = new PutSQLData())
                    {
                        if (!parameters.Any())
                            result = putData.PutClosedTableData(queries);
                        else
                            result = putData.PutClosedTableData(queries, parameters);

                        errors = putData.Errors;

                        if (result != null)
                        {
                            // Update dFilingIndicator
                            putData.SaveFilingIndicator(instanceID, selectedItem.FilingTemplateOrTableID);
                            if (result.Count > 0)
                            {
                                // Use the new IDs to update the primary key on the control. SUPERSEEDED.
                                //userControl.PK_IDs = result;
                            }
                            return true;
                        }
                    }
                    // Alert the users
                    if (errors.StartsWith("A record conflict occurred") && ClosedControls.Count != 1)
                    {
                        if (selectedItem.TableLabel != null)
                            sb.AppendLine(string.Format("{2}\n{0}\n{1}", selectedItem.TableLabel, errors, LanguageLabels.GetLabel(33, "There was a problem saving the details for:")));
                        else
                            sb.AppendLine(string.Format("{0}", errors));
                    }
                    else
                    {
                        // There is a single control. 
                        // This may be the sub control of the open template.
                        sb.AppendLine(string.Format("{0}", errors));

                    }
                }
            }
            errorText = sb.ToString();
            return false;
        }

        private string ValidateForm()
        {
            StringBuilder sb = new StringBuilder();
            // Check all Column Combos have a selection
            foreach (SolvencyDataComboBox solvencyDataComboBox in _columnCombos)
            {
                if (string.IsNullOrEmpty(solvencyDataComboBox.GetValue) && solvencyDataComboBox.Items.Count > 0)
                {
                    sb.AppendLine("Please select values for each column combo before saving data.");
                    break;
                }
            }
            // Check all Row combos have a selection
            foreach (SolvencyDataComboBox solvencyDataComboBox in _rowCombos)
            {
                if (string.IsNullOrEmpty(solvencyDataComboBox.GetValue) && solvencyDataComboBox.Items.Count > 0)
                {
                    sb.AppendLine("Please select values for each row combo before saving data.");
                    break;
                }
            }
            // Check there are no duplicate Combo selections

            /*
Select t.PAGES2C_OC from T__S_99_99_99_09__sol2__1_5_2_b t
Group by t.PAGES2C_OC
Having Count(PK_ID) > 1
             */


            var cols = (from c in _columnCombos
                                 group c by c.GetValue
                                 into g
                                 where g.Count() > 1
                                 select new {comboValue = g.Key, combos = g}).ToList();

            if (cols.Count > 0)
            {
                foreach (var col in cols)
                {
                    string itemText = "";
                    foreach (SolvencyDataComboBox comboBox in col.combos)
                    {
                        itemText = comboBox.GetItemText(comboBox.SelectedItem);
                        break;
                    }
                    sb.AppendLine(string.Format("There is more than one column combo with the value {0} please correct and try again.", itemText.Trim()));
                }
            }

            // Check there are not duplicate Row selectiions.
            var rows = (from c in _rowCombos
                        group c by c.GetValue
                            into g
                            where g.Count() > 1
                            select new { comboValue = g.Key, combos = g }).ToList();

            if (rows.Count > 0)
            {
                // We have to do something.
                foreach (var row in rows)
                {
                    string itemText = "";
                    foreach (SolvencyDataComboBox comboBox in row.combos)
                    {
                        itemText = comboBox.GetItemText(comboBox.SelectedItem);
                        break;
                    }

                    sb.AppendLine(string.Format("There is more than one column combo with the value {0} please correct and try again.", itemText.Trim()));
                }
            }

            return sb.ToString();
        }

        private string IsControlDataValid(ISolvencyUserControl userControl, List<T> closedControls, List<ISolvencyPageControl> nPageControls)
        {
            IClosedRowControl firstControl = closedControls[0];
            var dataControls = firstControl.GetDataControls();
            var comboControls = firstControl.GetComboControls();
            var dataCombos = firstControl.GetDataComboControls();
            List<string> fieldsNotUsed = new List<string>();

            foreach (string tableName in userControl.DataTables)
            {
                int tablePos = userControl.DataTables.IndexOf(tableName);
                Type tableType = userControl.DataTypes[tablePos];
                PropertyInfo[] props = tableType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // We have RowColumn entries
                foreach (PropertyInfo info in props.Where(i => i.Name.StartsWith("R")))
                {
                    string fieldName = info.Name;
                    if (dataControls.All(c => c.ColName != fieldName))
                    {
                        if(comboControls.All(c => c.ColName != fieldName))
                        {
                            if (dataCombos.All(c => c.ColName != fieldName))
                            {
                                if (nPageControls.All(c => c.ColName != fieldName))
                                {
                                    // We have run out of controls!
                                    fieldsNotUsed.Add(string.Format("{0}.{1}", tableName, fieldName));
                                }
                            }
                        }
                    }
                }

                // Multiple row combos
                foreach (PropertyInfo info in props.Where(i => i.Name.StartsWith("PAGE")))
                {
                    string fieldName = info.Name;
                    if (dataControls.All(c => c.ColName != fieldName))
                    {
                        if (comboControls.All(c => c.ColName != fieldName))
                        {
                            if (dataCombos.All(c => c.ColName != fieldName))
                            {
                                if (nPageControls.All(c => c.ColName != fieldName))
                                {
                                    // We have run out of controls!
                                    fieldsNotUsed.Add(string.Format("{0}.{1}", tableName, fieldName));
                                }
                            }
                        }
                    }
                }
            }
            if (!fieldsNotUsed.Any()) return "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("This is a development error. A template is incorrectly formated.");
            sb.AppendLine(string.Format("The following fields are missing from the user control:"));
            foreach (string s in fieldsNotUsed)
            {
                sb.AppendLine(s);
            }
            return sb.ToString();
        }

        public void PopulateAll(string tableName, List<object> data, bool firstTable)
        {
            if (data != null)
            {
                if (ClosedControls.Count > 1 && firstTable)
                    ClearAllControls();
                SetupData(ClosedControls[0], data, tableName);
                MarkClean();
            }
            else
            {
                ClearAllControls();
            }
        }


        public void SetupData(T userControl, List<object> data, string dataTable)
        {
            if (data != null)
            {
                ResetControlCollections();

                // Get the data formatted to dynamically.
                List<SpecialCell> intermediateData = new List<SpecialCell>();

                string columnCombo = _columnCombo.ColName;
                string rowCombo = _rowCombo.ColName;
                string cellText = _textCell.ColName;
                const string primaryKey = "PK_ID";

                if (data.Count > 0)
                {
                    foreach (object obj in data)
                    {
                        // One obj for each entry
                        PropertyInfo[] propsOne = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                        SpecialCell cellData = new SpecialCell();

                        PropertyInfo info = propsOne.FirstOrDefault(p => p.Name == columnCombo);
                        if (info != null) cellData.ColumnValue = GetStringFromObject(info.GetValue(obj, null));
                        info = propsOne.FirstOrDefault(p => p.Name == rowCombo);
                        if (info != null) cellData.RowValue = GetStringFromObject(info.GetValue(obj, null));
                        info = propsOne.FirstOrDefault(p => p.Name == cellText);
                        if (info != null) cellData.CellValue = GetStringFromObject(info.GetValue(obj, null));
                        info = propsOne.FirstOrDefault(p => p.Name == primaryKey);
                        if (info != null)
                        {
                            string result = GetStringFromObject(info.GetValue(obj, null));
                            if (!string.IsNullOrEmpty(result))
                            {
                                long key = 0;
                                long.TryParse(result, out key);
                                cellData.PrimaryKey = key;
                            }
                        }

                        intermediateData.Add(cellData);

                    }

                }
                else
                {
                    // We have no data so we just need to setup the default txt
                    intermediateData.Add(new SpecialCell {CellValue = "", ColumnValue = "", PrimaryKey = 0, RowValue = ""});
                }

                // Work out the rows and columns
                List<string> rows = intermediateData.GroupBy(i => i.RowValue).Select(g => g.Key).ToList();
                List<string> columns = intermediateData.GroupBy(i => i.ColumnValue).Select(g => g.Key).ToList();

                #region Controls Add Combos and Move Buttons

                // Rows
                _rowCombo.SetValue = rows[0];
                for (int i = 1; i < rows.Count(); i++)
                {
                    CreateRowCombo(i, rows[i]);
                    CreateRowComboDelete(i);
                }
                // Row Add Button
                _buttonAddRow.Location = new Point(_buttonAddRow.Location.X, _rowCombos[_rowCombos.Count - 1].Location.Y + constants.ROW_HEIGHT);

                // Cols
                _columnCombo.SetValue = columns[0];
                for (int j = 1; j < columns.Count(); j++)
                {
                    CreateColumnCombo(j, columns[j]);
                    CreateColumnComboDelete(j);
                }

                // Col Add Button
                _buttonAddCol.Location = new Point(_columnCombos[_columnCombos.Count - 1].Location.X + constants.CURRENCY_COLUMN_WIDTH, _buttonAddCol.Location.Y);


                #endregion

                _textCell.Result = intermediateData[0].CellValue;
                _textCell.Name = CellKey.CellKeyToText(0, 0, intermediateData[0].PrimaryKey);

                for (int i = 0; i < _rowCombos.Count; i++)
                {
                    string rowValue = _rowCombos[i].GetValue;
                    for (int j = 0; j < _columnCombos.Count; j++)
                    {
                        string colValue = _columnCombos[j].GetValue;
                        if (!(i == 0 && j == 0))
                        {
                            SpecialCell cell = intermediateData.FirstOrDefault(c => c.ColumnValue == colValue && c.RowValue == rowValue);
                            long cellPrimaryKey = 0;
                            string cellValue = "";
                            if (cell != null)
                            {
                                cellPrimaryKey = cell.PrimaryKey;
                                cellValue = cell.CellValue;
                            }
                            CreateDataControlAtPosition(i, j, cellPrimaryKey, cellValue);
                        }
                    }
                }



                PropertyInfo[] props = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                long pkey = 0;
                PropertyInfo id = props.FirstOrDefault(p => p.Name == "PK_ID");
                if (id != null) pkey = (long) id.GetValue(data, null);

                foreach (PropertyInfo p in props)
                {
                    // We have a name:
                    string name = p.Name;
                    // Locate the control and set the result
                    ISolvencyDataControl caughtControl = userControl.GetDataControls().FirstOrDefault(c => c.ColName == name);
                    if (caughtControl != null)
                    {
                        caughtControl.Result = p.GetValue(data, null);
                    }
                    else
                    {
                        ISolvencyComboBox caughtCombo = userControl.GetComboControls().FirstOrDefault(c => c.ColName == name);
                        if (caughtCombo != null)
                        {
                            string getValue = (string) p.GetValue(data, null);
                            SolvencyComboBox refCombo = ((SolvencyComboBox) caughtCombo);
                            refCombo.StopIndexChangedEvent = true;
                            refCombo.SelectItemByValue(getValue);
                            refCombo.StopIndexChangedEvent = false;
                            refCombo.SetDropDownWidth();
                        }
                    }
                }
                // SetPkID(userControl, pkey, dataTable);
            }
            else
            {
                ClearSingleControl(userControl);
            }
        }

        private string GetStringFromObject(object myObj)
        {
            if (myObj == null) return "";
            return myObj.ToString();
        }


        private void CreateColumnCombo(int j, string colValue)
        {
            SolvencyDataComboBox additional = _columnCombo.DeepCopyWithItems();
            additional.Location = new Point(additional.Location.X + j * (constants.CURRENCY_COLUMN_WIDTH), additional.Location.Y);
            additional.Name += j.ToString();
            additional.SetValue = colValue;
            ClosedControls[0].Controls.Add(additional);
            _columnCombos.Add(additional);

        }

        private void CreateColumnComboDelete(int j)
        {
            SolvencyButton additional = _buttonDelCol.DeepCopy();
            additional.Location = new Point(additional.Location.X + (j * constants.CURRENCY_COLUMN_WIDTH), additional.Location.Y);
            additional.Name += j.ToString();
            additional.Click += DelCol;
            ClosedControls[0].Controls.Add(additional);
            _buttonDelCols.Add(additional);
        }

        private void CreateRowCombo(int i, string rowValue)
        {
            SolvencyDataComboBox additional = _rowCombo.DeepCopyWithItems();
            additional.Location = new Point(additional.Location.X, additional.Location.Y + i*(constants.ROW_HEIGHT));
            additional.Name += i.ToString();
            additional.SetValue = rowValue;
            ClosedControls[0].Controls.Add(additional);
            _rowCombos.Add(additional);
        }

        private void CreateRowComboDelete(int i)
        {
            SolvencyButton additional = _buttonDelRow.DeepCopy();
            additional.Location = new Point(additional.Location.X, additional.Location.Y + i * (constants.ROW_HEIGHT));
            additional.Name += i.ToString();
            additional.Click += DelRow;
            ClosedControls[0].Controls.Add(additional);
            _buttonDelRows.Add(additional);
        }

        /// <summary>
        /// Create data control at row i, column j.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="primaryKey"></param>
        /// <param name="cellValue"></param>
        private void CreateDataControlAtPosition(int i, int j, long primaryKey, string cellValue)
        {
            // i = row
            // j = col
            ISolvencyDataControl workingCell = _textCell.DeepCopy();
            int locX = _columnCombo.Location.X + (j * constants.CURRENCY_COLUMN_WIDTH);
            int locY = _rowCombo.Location.Y + (i * constants.ROW_HEIGHT) + constants.CONTROL_MARGIN_CENTRAL;
            workingCell.Location = new Point(locX, locY);
            workingCell.Result = cellValue;
            workingCell.Name = CellKey.CellKeyToText(j, i, primaryKey);
            ClosedControls[0].Controls.Add((Control)workingCell);
            _textCells.Add(workingCell);
        }

        public void ClearAllControls()
        {
            // Get rid of all controls - except one
            for (int i = ClosedControls.Count; i > 1; i--)
            {
                ClearEvents(ClosedControls[i - 1]);
                ClosedControls[i - 1].Dispose();
                ClosedControls.RemoveAt(i - 1);
            }
            ClearSingleControl(ClosedControls[0]);
        }
        
        public bool DeleteSingleControl()
        {
            return true;
        }

        /// <summary>
        /// This function is only used for bridging from the open grid to open row control.
        /// If pkId is -1 then insert a new record.
        /// </summary>
        /// <param name="pkId"></param>
        public void SetPkId(int pkId, List<Type> dataTypes, List<string> dataTables)
        {
           
        }

        #endregion

        #region Helper functions

        private void ResetControlCollections()
        {
            for (int i = _columnCombos.Count - 1; i >= 0; i--)
            {
                if (_columnCombos[i].Name != _columnCombo.Name)
                {
                    _columnCombos[i].Dispose();
                    _columnCombos.RemoveAt(i);
                }
            }
            for (int j = _rowCombos.Count - 1; j >= 0; j--)
            {
                if (_rowCombos[j].Name != _rowCombo.Name)
                {
                    _rowCombos[j].Dispose();
                    _rowCombos.RemoveAt(j);
                }
            }

            for (int i = _buttonDelRows.Count - 1; i >= 0; i--)
            {
                if (_buttonDelRows[i].Name != _buttonDelRow.Name)
                {
                    _buttonDelRows[i].Dispose();
                    _buttonDelRows.RemoveAt(i);
                }
            }
            for (int j = _buttonDelCols.Count - 1; j >= 0; j--)
            {
                if (_buttonDelCols[j].Name != _buttonDelCol.Name)
                {
                    _buttonDelCols[j].Dispose();
                    _buttonDelCols.RemoveAt(j);
                }
            }

            for (int k = _textCells.Count - 1; k >= 0; k--)
            {
                if (_textCells[k].Name != _textCell.Name)
                {
                    _textCells[k].Dispose();
                    _textCells.RemoveAt(k);
                }
            }
        }

        private void SetToolText(object sender, EventArgs e)
        {
            if (_toolTipObject != null)
            {
                ISolvencyDataControl ctrl = (ISolvencyDataControl)sender;
                if (ctrl.Text.IndexOf("~") != -1)
                {
                    string trueText = ctrl.TrueValue;
                    if (ctrl.ColumnType != SolvencyDataType.Percentage)
                        _toolTipObject.SetToolTip((Control)sender, trueText);
                    else
                    {
                        // We have a percentage:
                        decimal ctrlValue = decimal.Parse(trueText, NumberStyles.Any, CultureInfo.CurrentCulture);
                        ctrlValue = ctrlValue * 100;
                        _toolTipObject.SetToolTip((Control)sender, ctrlValue.ToString("0.#############################") + " " + CultureInfo.CurrentCulture.NumberFormat.PercentSymbol);
                    }
                }
                else
                {
                    _toolTipObject.SetToolTip((Control)sender, "");
                }
            }

        }

        private void MarkDirty(object sender, EventArgs e)
        {
            IsDirty = true;
            SetToolText(sender, e); 
        }

        private void MarkDirtyFromCombo(object sender, EventArgs e)
        {
            IsDirty = true;
        }

        private void MarkClean()
        {
            IsDirty = false;
        }

        private long GetPkFromObject(object obj)
        {
            long pkey = 0;
            PropertyInfo[] props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            PropertyInfo id = props.FirstOrDefault(p => p.Name == "PK_ID");
            if (id != null) pkey = (long) id.GetValue(obj, null);
            return pkey;
        }

        private void SetPkID(T userControl, long pKID, string tableName = null)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                int pos = TableNames.IndexOf(tableName);
                if (pos != -1)
                    userControl.PK_IDs[pos] = pKID;
            }
            else
            {
                for (int i = 0; i < userControl.PK_IDs.Count(); i++)
                {
                    userControl.PK_IDs[i] = 0;
                }
            }
        }

        public void ClearSingleControl(T userControl)
        {
            ////Clear all the controls
            foreach (ISolvencyDataControl solvencyDataControl in userControl.GetDataControls().Where(i => !(i.ColName.StartsWith("PAGE") || i.ColName == "PK_ID" || i.ColName == "INSTANCE")))
            {
                solvencyDataControl.Result = null;
            }
            foreach (ISolvencyComboBox solvencyComboControl in userControl.GetComboControls().Where(i => !(i.ColName.StartsWith("PAGE") || i.ColName == "PK_ID" || i.ColName == "INSTANCE")))
            {
                SolvencyComboBox refCombo = ((SolvencyComboBox) solvencyComboControl);
                refCombo.SelectFirstItem();
            }

            // Rest the PrimaryKeys
            List<long> pkeys = userControl.PK_IDs;
            for (int i = 0; i < pkeys.Count; i++)
            {
                pkeys[i] = 0;
            }
            userControl.PK_IDs = pkeys;
            MarkClean();
        }

        private T CreateControl()
        {
            T ctrl = new T();
            SetLabels(ctrl);
            ctrl.Location = SetNewControlLocation(ClosedControls, Horizontal);

            ctrl.AddControl += AddControl;
            ctrl.DelControl += OnDeleteControl;

            PopulateUserCombos(ctrl);
            PopulateDataCombos(ctrl);

            ClosedControls.Add(ctrl);
            ControlParentPanel.Controls.Add(ctrl);

            SetupEvents(ctrl);

            // Adjustment for buttons
            if (!Horizontal)
            {
                int rightOfTextBox = LeftOfTextBox(ctrl) + CONTROL_MARGIN;
                //ctrl.Location = new Point(ctrl.Location.X - rightOfTextBox, ctrl.Location.Y);
                ctrl.Location = new Point(ctrl.Location.X - (ctrl.Width - rightOfTextBox) + 1 , ctrl.Location.Y);
                ctrl.BringToFront();
            }
            else
            {
                int topTextBox = TopOfTopTextBox(ctrl);
                ctrl.Location = new Point(ctrl.Location.X, ctrl.Location.Y - topTextBox);
                ctrl.SendToBack();
            }

            return ctrl;
        }

        private int TopOfTopTextBox(T userControl)
        {
            Type textBoxType = typeof(SolvencyCurrencyTextBox);
            IEnumerable<Control> controls = userControl.GetDataControls().Cast<Control>().Where(textBoxType.IsInstanceOfType);
            int minPos = (from SolvencyCurrencyTextBox ctrlInner in controls select ctrlInner.Top).Concat(new[] { 1000 }).Min();
            return minPos - 3;
        }

        private int LeftOfTextBox(T userControl)
        {
            Type textBoxType = typeof(SolvencyCurrencyTextBox);
            IEnumerable<Control> controls = userControl.GetDataControls().Cast<Control>().Where(textBoxType.IsInstanceOfType);
            int minPos = (from SolvencyCurrencyTextBox ctrlInner in controls select ctrlInner.Location.X + ctrlInner.Width).Concat(new[] { 0 }).Max();
            return minPos;
        }


        private void PopulateUserCombos(T ctrl)
        {
            List<ISolvencyComboBox> combos = ClosedControls[0].GetComboControls();
            foreach (SolvencyComboBox comboBox in ctrl.GetComboControls().Cast<SolvencyComboBox>())
            {
                long axisID = comboBox.AxisID;
                ISolvencyComboBox combo = combos.FirstOrDefault(c => c.AxisID == axisID);
                if (combo != null)
                {
                    comboBox.Items.Clear();

                    foreach (object item in ((ComboBox)combo).Items)
                    {
                        comboBox.Items.Add(item);
                    }
                    if (comboBox.Items.Count > 1) comboBox.SelectFirstItem();
                }
                
            }
        }

        private void PopulateDataCombos(T ctrl)
        {
            List<ISolvencyDataComboBox> combos = ClosedControls[0].GetDataComboControls();
            foreach (SolvencyDataComboBox comboBox in ctrl.GetDataComboControls().Cast<SolvencyDataComboBox>())
            {
                long axisID = comboBox.AxisID;
                ISolvencyDataComboBox combo = combos.FirstOrDefault(c => c.AxisID == axisID);
                if (combo != null)
                {
                    comboBox.Items.Clear();
                    comboBox.DisplayMember = "Text";
                    comboBox.ValueMember = "Name";
                    foreach (object item in ((ComboBox)combo).Items)
                    {
                        comboBox.Items.Add(item);
                    }
                    if (comboBox.Items.Count > 1) comboBox.SelectFirstItem();
                }

            }
        }


        private void SetupEvents(T ctrl)
        {
            foreach (ISolvencyDataControl control in ctrl.GetDataControls())
            {
                if (!(control is SolvencyCheckBox))
                {
                    control.TextChanged += MarkDirty;
                }
                else
                {
                    ((SolvencyCheckBox) control).CheckStateChanged += MarkDirty;
                }
            }
            foreach (ISolvencyComboBox comboBox in ctrl.GetComboControls() )
            {
                ((SolvencyComboBox)comboBox).SelectedIndexChanged += MarkDirtyFromCombo;
            }
        }

        private void ClearEvents(T ctrl)
        {
            foreach (ISolvencyDataControl control in ctrl.GetDataControls())
            {
                if (!(control is SolvencyCheckBox))
                {
                    control.TextChanged -= MarkDirty;
                }
                else
                {
                    ((SolvencyCheckBox)control).CheckStateChanged -= MarkDirty;
                }
            }
        }



        private Point SetNewControlLocation(IEnumerable<T> closedControls, bool horizontal)
        {
            if (horizontal)
            {
                int newTop = closedControls.Max(c => c.Location.Y + c.Size.Height);
                return new Point(0, newTop);
            }
            int newX = closedControls.Max(c => c.Location.X + c.Size.Width);
            return new Point(newX, 0);
        }

        private void SetLabels(T ctrl2)
        {
            // Maximum size for labels
            Type textBoxType = typeof (SolvencyCurrencyTextBox);
            IEnumerable<Control> controls = ctrl2.GetDisplayControls().Cast<Control>();

            int maxPos = (from SolvencyCurrencyTextBox ctrlInner in controls.Where(textBoxType.IsInstanceOfType) select ctrlInner.Left).Concat(new[] {1000}).Min();

            //string sampleText = "abcde fghij klmno pqrst uvwxy z1234 567890";
            //int literalWidth = TextRenderer.MeasureText(sampleText, this.Font).Width;


            foreach (SolvencyLabel ctrlLabel in ctrl2.GetDisplayControls())
            {
                if (ctrlLabel.OrdinateID_Label != 0)
                {
                    // Populate the text
                    mAxisOrdinate axisOrdinate = LabelText.SingleOrDefault(o => o.OrdinateID == ctrlLabel.OrdinateID_Label);
                    if (axisOrdinate != null)
                    {
                        if (maxPos - ctrlLabel.Left > 200)
                            ctrlLabel.MaximumSize = new Size(maxPos - ctrlLabel.Left - 5, ctrlLabel.Size.Height);
                        SetText(ctrlLabel, axisOrdinate.OrdinateLabel);
                    }
                }
            }

        }

        private void SetText(ISolvencyDisplayControl label, string text)
        {
            Label labelControl = (Label) label;
            _toolTipObject.SetToolTip(labelControl, text);

            string working = text;
            if (labelControl.MaximumSize.Width != 0 && !string.IsNullOrEmpty(text))
            {
                if ((TextRenderer.MeasureText(text, ((Control) label).Font).Width > labelControl.MaximumSize.Width))
                {
                    working = working + "...";
                    while (TextRenderer.MeasureText(working, ((Control) label).Font).Width > labelControl.MaximumSize.Width - 3)
                    {
                        working = working.Substring(0, working.Length - 4) + "...";
                    }
                }
            }
            label.Text = working;
        }

        #endregion


        private class SpecialCell
        {
            public string ColumnValue { get; set; }
            public string RowValue { get; set; }
            public string CellValue { get; set; }
            public long PrimaryKey { get; set; }
        }

    }
}
