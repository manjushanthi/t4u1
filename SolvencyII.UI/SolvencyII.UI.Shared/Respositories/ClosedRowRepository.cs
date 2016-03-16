using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;
using SolvencyII.UI.Shared.Data;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.Shared.Respositories
{
    /// <summary>
    /// Closed templates require the use of a repository to manage a collection of controls.
    /// Where there is only one record only one control.
    /// When the templates are semi open multiple controls are generated.
    /// </summary>
    public class ClosedRowRepository<T> : IClosedRowRepository
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
        //public event GenericDelegates.ListLongs DeleteControl;
        //protected bool OnDeleteControl(List<long> pkeys)
        //{
        //    if (OnAskUserQuestion("Are you sure you want to delete this entry?\r\nOnce deleted it cannot be restored."))
        //    {
        //        if (DeleteControl != null) return DeleteControl(pkeys);
        //    }
        //    return false;
        //}

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



        public void AddControl()
        {
            CreateControl();
        }
        
        #endregion

        #region Constructors

        public ClosedRowRepository(T baseControl, Panel parentPanel, bool horizontal, List<string> tableNames)
        {
            ClosedControls = new List<T> {baseControl};
            ControlParentPanel = parentPanel;
            Horizontal = horizontal;
            TableNames = tableNames;
            baseControl.AddControl += AddControl;
            baseControl.DelControl += OnDeleteControl;
            SetupEvents(ClosedControls[0]);
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

            foreach (string tableName in userControl.DataTables)
            {
                int tablePos = userControl.DataTables.IndexOf(tableName);
                Type tableType = userControl.DataTypes[tablePos];
                foreach (T closedControl in ClosedControls)
                {
                    SQLClosedUpdate.BuildSQLQuery_Update(ref queries, ref parameters, tableName, closedControl.PK_IDs[tablePos], closedControl, instanceID, tablePos, tableType, nPageControls);   
                }
            }

            string resultIsControlDataValid = IsControlDataValid(userControl, ClosedControls, nPageControls);
            if (!string.IsNullOrEmpty(resultIsControlDataValid)) MessageBox.Show(resultIsControlDataValid, "Consistency Check");

            if (queries.Any())
            {
                List<long> result;
                string errors;
                using (PutSQLData putData = new PutSQLData())
                {
                    if (!parameters.Any())
                        result = putData.PutClosedTableDate(queries);
                    else
                        result = putData.PutClosedTableData(queries, parameters);

                    errors = putData.Errors;

                    if (result != null)
                    {
                        // Update dFilingIndicator
                        putData.SaveFilingIndicator(instanceID, selectedItem.FilingTemplateOrTableID);
                        if (result.Count > 0)
                        {
                            // Use the new IDs to update the primary key on the control
                            // ToDo: NAJ remove line below if not needed.
                            //userControl.PK_IDs = result;
                        }
                        if (result.Count == 1 && ClosedControls.Count == 1)
                        {
                            // Use the new IDs to update the primary key on the control
                            ClosedControls[0].PK_IDs = result;
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
            errorText = sb.ToString();
            return false;
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
                if (data.Count == 1)
                {
                    // We only have one data row.
                    if (ClosedControls.Count > 1 && firstTable)
                        ClearAllControls();
                    PopulateSingleControl(ClosedControls[0], data[0], tableName);
                }
                else
                {
                    // We need to find which control each row belongs.
                    if (ClosedControls.Count != data.Count)
                    {

                        // There are not the correct number of controls to data rows.
                        ClearAllControls();
                        // Make sure the correct number are there.
                        try
                        {
                            for (int i = 0; i < data.Count - 1; i++)
                            {




                                CreateControl();
                            }
                        }
                        catch (System.ComponentModel.Win32Exception ex)
                        {
                            if (ex.Message == "Error creating window handle.")
                            {
                                //TODO: Nicholas. Eventually we will need to solve this issue 
                                // Use vb action pack repeater control?
                                // Control reuse?

                                // We cannot get more than 10,000 controls in a panel
                                // USERHandleProcessQuota
                                // https://trello.com/c/Jrf7lNDp/81-limit-of-controls-reached-in-large-forms

                                /*
                                  *** Critical ***  System.ComponentModel.Win32Exception: Error creating window handle.
                                       at System.Windows.Forms.NativeWindow.CreateHandle(CreateParams cp)
                                       at System.Windows.Forms.Control.CreateHandle()
                                       at System.Windows.Forms.Control.CreateControl(Boolean fIgnoreVisible)
                                       at System.Windows.Forms.Control.CreateControl(Boolean fIgnoreVisible)
                                       at System.Windows.Forms.Control.CreateControl()
                                       at System.Windows.Forms.Control.ControlCollection.Add(Control value)
                                       at SolvencyII.UI.Shared.Respositories.ClosedRowRepository`1.CreateControl() in d:\XBRT\6. Windows T4U\SolvencyII.UI\SolvencyII.UI.Shared\Respositories\ClosedRowRepository.cs:line 461
                                       at SolvencyII.UI.Shared.Respositories.ClosedRowRepository`1.PopulateAll(String tableName, List`1 data, Boolean firstTable) in d:\XBRT\6. Windows T4U\SolvencyII.UI\SolvencyII.UI.Shared\Respositories\ClosedRowRepository.cs:line 279| from ClosedRowRepository.PopulateAll
                                 */


                                Loggers.Logger.WriteLog(eSeverity.Critical, "ClosedRowRepository.PopulateAll", ex.ToString());
                                
                                // When limit reached nothing new can be shown so clear all controls.
                                ClearAllControls();

                                OnAlertUser("Limit of controls shown has been reached: Error creating window handle.\r\n\r\nThere are simply too many rows of data to appear on this semi-open template.\r\nDo not attempt to save the this blank template - data may be lost.\r\nPlease contact support.");

                                return;
                            }
                            throw;
                        }
                    }

                    int pos = TableNames.IndexOf(tableName);
                    
                    // Populate each control
                    for (int j = 0; j < data.Count; j++)
                    {
                        T cntrl = ClosedControls[j];
                        PopulateSingleControl(cntrl, data[j], tableName);
                    }
                }
                MarkClean();
            }
            else
            {
                ClearAllControls();
            }

        }

        public void PopulateSingleControl(T userControl, object data, string dataTable)
        {
            if (data != null)
            {
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
                        string caughtTextCheck = caughtControl.Text;
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
                SetPkID(userControl, pkey, dataTable);
            }
            else
            {
                ClearSingleControl(userControl);
            }
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
            // This is only going to be called when there is one control specified by the z combos
            if (ClosedControls.Count() == 1)
            {
                if (PkID == 0)
                    return OnDeleteControl(ClosedControls[0].PK_IDs);
                return OnDeleteControl(new List<long>{ PkID });
            }
            return false;
        }

        /// <summary>
        /// This function is only used for bridging from the open grid to open row control.
        /// If pkId is -1 then insert a new record.
        /// </summary>
        /// <param name="pkId"></param>
        public void SetPkId(int pkId, List<Type> dataTypes, List<string> dataTables)
        {
            PkID = pkId;
            if (pkId != -1)
            {
                using (GetSQLData getData = new GetSQLData())
                {
                    string query = string.Format("Select * from {0} where PK_ID = {1}", dataTables[0], pkId);
                    List<object> result = getData.GetClosedTableInfo(dataTypes[0], query);
                    PopulateAll(TableNames[0], result, true);
                }
            }
            else
            {
                // This is for insertion on Open template 
                ClearAllControls();
            }
            IsDirty = false;
        }

        #endregion

        #region Helper functions

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
                        _toolTipObject.SetToolTip((Control)sender, ctrlValue.ToString("G29") + " " + CultureInfo.CurrentCulture.NumberFormat.PercentSymbol);
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

    }
}
