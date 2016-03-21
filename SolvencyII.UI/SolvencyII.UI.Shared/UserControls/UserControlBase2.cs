using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.Shared.UserControls
{
    /// <summary>
    /// This is currently not used.
    /// </summary>
    public class UserControlBase2 : UserControl, IUserControlBase
    {
        GenericDelegates.DisplayDimensions oldDisplayDimHandler;

        #region Declaration and Properties

        private List<Control> _formControls;
        private List<ISolvencyDataControl> _dataControls;
        private List<ISolvencyPageControl> _pageControls;
        private List<ISolvencyComboBox> _pageComboBoxControls;
        private List<ISolvencyComboBox> _userComboBoxControls;
        private List<ISolvencyDataComboBox> _dataComboBoxControls;

        private ToolTip _toolTipObject;

        public long InstanceID { get; set; }

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

        public ToolTip ToolTipObject
        {
            get { return _toolTipObject; }
            set
            {
                if (value != null)
                {
                    _toolTipObject = value;
                    _toolTipObject.RemoveAll();
                    //((ISolvencyUserControl) this).CtrlRepository.SetToolTipObject(value);
                }
            }
        }

        #endregion

        #region Gather Controls and Info

        //private SolvencyClosedRowControl GetFirstUserControl()
        //{
        //    return GetDataControls().Cast<SolvencyClosedRowControl>().FirstOrDefault();
        //}

        //private List<SolvencyClosedRowControl> GetAllUserControl()
        //{
        //    return GetDataControls().Cast<SolvencyClosedRowControl>().ToList();
        //}

        public List<ISolvencyDataControl> GetDataControls()
        {
            if (_dataControls == null)
            {
                //Type solvencyType = typeof(IClosedRowControl);
                Type solvencyType = typeof(ISolvencyDataControl);
                IEnumerable<Control> midStep = FormControls.Where(solvencyType.IsInstanceOfType);
                _dataControls = (from ISolvencyDataControl control in midStep select control).ToList();
            }
            return _dataControls;
        }

        public List<ISolvencyPageControl> GetPAGEnControls()
        {
            if (_pageControls == null)
            {
                Type solvencyType = typeof (ISolvencyPageControl);
                IEnumerable<ISolvencyPageControl> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyPageControl>();
                _pageControls = (from ISolvencyPageControl control in midStep where (control.ColName.StartsWith("PAGE")) select control).ToList();
            }
            return _pageControls;
        }

        private List<ISolvencyComboBox> GetPAGEnComboBoxControls()
        {
            if (_pageComboBoxControls == null)
            {
                Type solvencyType = typeof (ISolvencyComboBox);
                IEnumerable<ISolvencyComboBox> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyComboBox>();
                _pageComboBoxControls = (from ISolvencyComboBox control in midStep where (control.ColName.StartsWith("PAGE")) select control).ToList();
            }
            return _pageComboBoxControls;
        }

        public List<ISolvencyComboBox> GetUserComboBoxControls()
        {
            if (_userComboBoxControls == null)
            {
                // Used for populating the items within combos
                Type solvencyType = typeof (ISolvencyComboBox);
                IEnumerable<ISolvencyComboBox> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyComboBox>();
                _userComboBoxControls = midStep.ToList();
                // Note NAJ 20141113 Changed, some of the use ComboBoxes do not have PAGE but RxxxxCxxxx thus this change.
                // _userComboBoxControls = (from ISolvencyComboBox control in midStep where (!control.ColName.StartsWith("PAGE")) select control).ToList();
            }
            return _userComboBoxControls;
        }

        public List<ISolvencyDataComboBox> GetDataComboBoxControls()
        {
            if (_dataComboBoxControls == null)
            {
                // Used for populating the items within combos
                Type solvencyType = typeof (ISolvencyDataComboBox);
                IEnumerable<ISolvencyDataComboBox> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyDataComboBox>();
                _dataComboBoxControls = (from ISolvencyDataComboBox control in midStep where (control.ColName.StartsWith("PAGE")) select control).ToList();
            }
            return _dataComboBoxControls;
        }

        public List<String> GetDataTables()
        {
            ISolvencyUserControl ctrl = (ISolvencyUserControl)this;
            return ctrl.DataTables;
        }

        public List<Type> GetDataTypes()
        {
            ISolvencyUserControl ctrl = (ISolvencyUserControl)this;
            return ctrl.DataTypes;
        }

        public bool IsDirty { get; set; }

        #endregion
    
        #region Repository Methods


        public bool RepositoryEnabled
        {
            get
            {
                if (!DesignMode)
                    return GetRepository().Enabled;
                return true;
            }
            set
            {
                if (!DesignMode)
                    GetRepository().Enabled = value;
            }
        }

        private bool IsRepositoryDirty()
        {
            return GetRepository().IsDirty;
        }

        private IClosedRowRepository GetRepository()
        {
            IControlContainsRepository ctrl = (IControlContainsRepository)this;
            return ctrl.CtrlRepository;
        }

        #endregion

        public string RowKeyCheck()
        {

            // This has been removed to accommodate nil values - RowKey fields can be null.
            // 23 Feb 2015 NAJ

            //// Check all controls where they have a IsRowKey = true;
            //StringBuilder sb = new StringBuilder();
            //// This is currently only run on Open templates - so there is only one ClosedRowControl
            //List<ISolvencyDataControl> ctrls = GetDataControls()[0].GetDataControls();
            //foreach (ISolvencyDataControl ctrl in ctrls.Where(c => c.IsRowKey))
            //{
            //    if (string.IsNullOrEmpty(ctrl.Text))
            //        sb.AppendLine(string.Format("{0} cannot be blank it is a key field", ctrl.ColName));
            //}
            //if (sb.Length > 0)
            //    return string.Format("Problem saving this information\r\n{0}", sb.ToString());

            return "";
        }


        #region Populate / clear the controls

        public void SetOpenControl(int pkId, List<ISolvencyComboBox> parentCombos)
        {
            if (pkId != 0)
            {
                ISolvencyUserControl ctrl = (ISolvencyUserControl)this;
                if (parentCombos != null)
                {
                    List<ISolvencyComboBox> combos = GetPAGEnComboBoxControls();
                    foreach (ISolvencyComboBox combo in combos)
                    {
                        ISolvencyComboBox parentCombo = parentCombos.Where(c => c.ColName == combo.ColName).FirstOrDefault();
                        if (parentCombo != null)
                        {
                            // Setup the selected value.
                            combo.SetValue = parentCombo.GetValue;
                        }

                    }

                }
                GetRepository().SetPkId(pkId, ctrl.DataTypes, ctrl.DataTables);
            }
        }

        public void PopulatePAGEnControls(IEnumerable<FormDataPage> pageData)
        {
            if (pageData != null)
            {
                foreach (FormDataPage item in pageData.Where(p=>p.FixedDimension))
                {
                    // We have a name:
                    string name = item.DYN_TAB_COLUMN_NAME;
                    // Locate the control and set the result
                    ISolvencyPageControl caughtControl = GetPAGEnControls().FirstOrDefault(c => c.ColName == name.ToUpper());
                    if (caughtControl != null)
                        caughtControl.Text = item.Value;
                }
            }
        }

        public void PopulateLabels(List<mAxisOrdinate> axisOrdinates)
        {

            // Maximum size for labels
            Type textBoxType = typeof(SolvencyCurrencyTextBox);
            //SolvencyClosedRowControl firstControl = GetFirstUserControl();
            //IEnumerable<Control> controls = firstControl.GetControls();

            IEnumerable<Control> controls = GetDataControls().Cast<Control>();

            int maxPos = (from SolvencyCurrencyTextBox ctrl in controls.Where(textBoxType.IsInstanceOfType) select ctrl.Left).Concat(new[] { 1000 }).Min();


            //string sampleText = "abcde fghij klmno pqrst uvwxy z1234 567890";
            //int literalWidth = TextRenderer.MeasureText(sampleText, this.Font).Width;

            Type lableType = typeof(SolvencyLabel);
            foreach (SolvencyLabel ctrl in controls.Where(lableType.IsInstanceOfType))
            {
                if (ctrl.OrdinateID_Label != 0)
                {
                    // Populate the text
                    mAxisOrdinate axisOrdinate = axisOrdinates.SingleOrDefault(o => o.OrdinateID == ctrl.OrdinateID_Label);
                    if (axisOrdinate != null)
                    {
                        if (maxPos - ctrl.Left > 200)
                            ctrl.MaximumSize = new Size(maxPos - ctrl.Left - 5, ctrl.Size.Height);
                        SetText(ctrl, axisOrdinate.OrdinateLabel);
                    }
                }
            }
        }

        public void ClearFormControls()
        {
            GetRepository().ClearAllControls();
        }

        public void SetText(ISolvencyDisplayControl label, string text)
        {
            Label labelControl = (Label) label;
            _toolTipObject.SetToolTip(labelControl, text);

            string working = text;
            if (labelControl.MaximumSize.Width != 0 && !string.IsNullOrEmpty(text))
            {
                if ((TextRenderer.MeasureText(text, Font).Width > labelControl.MaximumSize.Width))
                {
                    working = working + "...";
                    while (TextRenderer.MeasureText(working, Font).Width > labelControl.MaximumSize.Width - 3)
                    {
                        working = working.Substring(0, working.Length - 4) + "...";
                    }
                }
            }
            label.Text = working;
        }

        #endregion

        #region SQL based methods

        public string BuildSQLQuery_Select(string tableName, bool comboUpdate)
        {
            // Based upon what we know we need to retrieve all relevant records
            // Then populate the collection of user controls.

            // Build the query
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Select * from [{0}] Where Instance = {1} ", tableName, InstanceID));
            foreach (ISolvencyPageControl formDataPage in GetPAGEnControls())
            {
                List<string> tables = formDataPage.TableNames.Split('|').ToList();
                int position = tables.IndexOf(tableName);
                if (position != -1)
                {
                    string comboValue;
                    if (formDataPage.GetType() != typeof(SolvencyTextComboBox))
                        comboValue = formDataPage.SQLValue();
                    else
                        comboValue = string.Format("'{0}'", formDataPage.Text);
                    

                    if (!string.IsNullOrEmpty(comboValue))
                        sb.Append(string.Format("AND {0} = {1} ", formDataPage.ColName, comboValue));
                }
            }
            sb.Append(" Order by PK_ID ");
            return sb.ToString();
        }

        public string BuildRepositoryWhere_Select(string tableName, bool comboUpdate)
        {
            throw new NotImplementedException();
        }

        public string BuildSQLQuery_Delete(string tableName, bool usePageN)
        {

            int primaryKey = GetRepository().PkID;

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Delete from {0} Where INSTANCE = {1} ", tableName, InstanceID));
            
            if (usePageN)
            {
                foreach (ISolvencyPageControl formDataPage in GetPAGEnControls())
                {
                    List<string> tables = formDataPage.TableNames.Split('|').ToList();
                    int position = tables.IndexOf(tableName);
                    if (position != -1)
                    {
                        string comboValue;
                        if (formDataPage.GetType() != typeof(SolvencyTextComboBox))
                            comboValue = formDataPage.SQLValue();
                        else
                            comboValue = string.Format("'{0}'", formDataPage.Text);


                        if (!string.IsNullOrEmpty(comboValue))
                            sb.Append(string.Format("AND {0} = {1} ", formDataPage.ColName, comboValue));
                    }
                }
            }

            if (primaryKey != 0)
                sb.Append(string.Format("AND PK_ID = {0} ", primaryKey));

            return sb.ToString();
        }

        #endregion

        #region PK_ID, Primary Key access

        //private long GetPK_ID(string tableName)
        //{
        //    ISolvencyUserControl ctrl = (ISolvencyUserControl) this;
        //    int pos = ctrl.DataTables.IndexOf(tableName);
        //    if (pos == -1) return 0;
        //    return ctrl.PK_IDs[pos];
        //}

        //private void SetPK_ID(long pK_ID, string tableName = null)
        //{
        //    ISolvencyUserControl ctrl = (ISolvencyUserControl) this;
        //    if (!string.IsNullOrEmpty(tableName))
        //    {
        //        int pos = ctrl.DataTables.IndexOf(tableName);
        //        if (pos != -1)
        //            ctrl.PK_IDs[pos] = pK_ID;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < ctrl.PK_IDs.Count(); i++)
        //        {
        //            ctrl.PK_IDs[i] = 0;
        //        }
        //    }
        //}

        #endregion

        #region Combos

        public void PageCombosEnBold(GetSQLData getData, object sender)
        {
            SolvencyComboBox changedControl = (SolvencyComboBox) sender;
            if (changedControl != null && changedControl.ColumnType == SolvencyDataType.Code)
            {
                ISolvencyUserControl ctrl = (ISolvencyUserControl) this;
                var tables = ctrl.DataTables;
                List<string> boldValues = new List<string>();
                foreach (string table in tables)
                {
                    string where = string.Format("Where INSTANCE = {0} {1}", InstanceID, ComboHighLightWhere(table, changedControl.ColName));
                    boldValues.AddRange(getData.GetComboBoxHighlights(table, changedControl.ColName, where));
                }

                foreach (var item in changedControl.Items)
                {
                    ListViewItem listViewItem = (ListViewItem) item;
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
            foreach (var comboControl in GetPAGEnControls())
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

        public bool PageCombosCheck()
        {
            // The text here corresponds to that added in ClosedTableManager.PopulateNPageCombos
            return GetPAGEnComboBoxControls().All(formDataPage => !string.IsNullOrEmpty(formDataPage.GetValue) & formDataPage.GetValue != "Please select or press add button" & TagNullOrFalse(formDataPage));
            //(formDataPage.TypeOfItems == ComboItemType.TextEntries & formDataPage.GetValue != "Please select")
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

        #endregion

        #region Set User controls grey

        public void GreyOutInputControls(List<FactInformation> dataFacts)
        {
            
        }

        //public void GreyOutInputControls(List<FactInformation> dataFacts)
        //{

        //    if (dataFacts != null)
        //    {

        //        //foreach (FactInformation fact in dataFacts)
        //        //{
        //        //    var caughtControl = GetDataControls().FirstOrDefault(c => c.OrdinateID_OneDim == fact.XordinateID && c.OrdinateID_TwoDim == fact.YordinateID);
        //        //    if (caughtControl != null)
        //        //    {
        //        //        var control = (Control)caughtControl;
        //        //        control.BackColor = Color.Gray;
        //        //        control.Enabled = false;
        //        //    }
        //        //}

        //        Type solvencyType = typeof (ISolvencyDataControl);
        //        foreach (ISolvencyDataControl ctrl in FormControls.Where(c => solvencyType.IsInstanceOfType(c)))
        //        {
        //            Type ctrlType = ctrl.GetType();
        //            if (ctrlType != typeof (SolvencyLabel))
        //            {
        //                // Get the information 
        //                FactInformation info = dataFacts.SingleOrDefault(f => f.XordinateID == ctrl.OrdinateID_OneDim && f.YordinateID == ctrl.OrdinateID_TwoDim);
        //                var control = (Control) ctrl;
        //                if (info != null)
        //                {
        //                    control.BackColor = Color.Gray;
        //                    control.Enabled = false;
        //                }
        //                else
        //                {
        //                    control.BackColor = Color.White;
        //                    control.Enabled = true;

        //                }

        //            }
        //        }
        //    }
        //}

        #endregion

        #region Text Combo Events

        public void AddSingleControlText(object sender)
        {
            SolvencyButton btn = (SolvencyButton)sender;
            ISolvencyComboBox comboBoxI = GetPAGEnComboBoxControls().FirstOrDefault(c => c.ColumnType == SolvencyDataType.String && c.ColName == btn.ColName);
            if (comboBoxI != null)
            {
                SolvencyTextComboBox comboBox = (SolvencyTextComboBox)comboBoxI;
                if (comboBox.ColumnType == SolvencyDataType.String)
                {
                    if (comboBox.AddEntry())
                        GetRepository().ClearAllControls();
                }
            }
            // The user now needs to press save.
        }

        public void DeleteSingleControlText(object sender)
        {
            // Remove the item from the combo and then delete the record.
            if (GetRepository().DeleteSingleControl())
            {
                SolvencyButton btn = (SolvencyButton) sender;
                ISolvencyComboBox comboBoxI = GetPAGEnComboBoxControls().FirstOrDefault(c => c.ColumnType == SolvencyDataType.String && c.ColName == btn.ColName);
                if (comboBoxI != null)
                {
                    SolvencyTextComboBox comboBox = (SolvencyTextComboBox) comboBoxI;
                    if (comboBox.ColumnType == SolvencyDataType.String)
                    {
                        comboBox.DeleteEntry();
                    }
                }
            }
        }

        // Special case events
        public void AddRow(object sender) { Debugger.Break(); }
        public void AddCol(object sender) { Debugger.Break(); }

        #endregion

        #region Dispose

        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

 public void SetupShowDimensionHandler(GenericDelegates.DisplayDimensions displayDimHandler)
        {
            List<ISolvencyDataControl> dataControls = GetDataControls();

            if (dataControls != null)
            {

                if (oldDisplayDimHandler != null)
                {
                    foreach (ISolvencyControl control in dataControls)
                        control.DisplayDimensions -= oldDisplayDimHandler;
                }

                foreach (ISolvencyControl control in dataControls)
                    control.DisplayDimensions += displayDimHandler;
                
            }
        
            //Subscribe for page controls

            if (_pageControls != null)
            {
                if (oldDisplayDimHandler != null)
                {
                    foreach (ISolvencyControl control in _pageControls)
                        control.DisplayDimensions -= oldDisplayDimHandler;
                    
                }

                foreach (ISolvencyControl control in _pageControls)
                    control.DisplayDimensions += displayDimHandler;
            }

            oldDisplayDimHandler = displayDimHandler;
        }

    }
}
