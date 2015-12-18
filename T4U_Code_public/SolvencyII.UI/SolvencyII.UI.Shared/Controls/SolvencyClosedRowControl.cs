
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SolvencyII.Data.Shared;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Configuration;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// This is the class that contains a single row of user data for closed tables
    /// </summary>
    public class SolvencyClosedRowControl : UserControl
    {
        private List<Control> _formControls;
        private List<ISolvencyDataControl> _dataControls;
        private List<ISolvencyComboBox> _comboControls;
        private List<ISolvencyDisplayControl> _displayControls;
        private ISolvencyUserControl _userControl;

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
        //public List<ISolvencyDataControl> GetDataControls()
        //{
        //    if (_dataControls == null)
        //    {
        //        Type solvencyType = typeof(ISolvencyDataControl);
        //        IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
        //        _dataControls = (from ISolvencyDataControl control in midStep select control).ToList();
        //    }
        //    return _dataControls;
        //}
        //public List<ISolvencyComboBox> GetComboControls()
        //{
        //    if (_comboControls == null)
        //    {
        //        Type solvencyType = typeof(ISolvencyComboBox);
        //        IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
        //        _comboControls = (from ISolvencyComboBox control in midStep select control).ToList();
        //    }
        //    return _comboControls;
        //}
        public List<ISolvencyDataComboBox> GetDataComboControls()
        {
            Type solvencyType = typeof (ISolvencyDataComboBox);
            IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
            return (from ISolvencyDataComboBox control in midStep select control).ToList();
        }

        public List<ISolvencyDisplayControl> GetDisplayControls()
        {
            if (_displayControls == null)
            {
                Type solvencyType = typeof(ISolvencyDisplayControl);
                IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
                _displayControls = (from ISolvencyDisplayControl control in midStep select control).ToList();
            }
            return _displayControls;
        }

        public List<ISolvencyDataComboBox> GetPageComboControls()
        {
            Type solvencyType = typeof(ISolvencyDataComboBox);
            IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
            return (from ISolvencyDataComboBox control in midStep select control).ToList();
        }


        public ISolvencyUserControl GetUserControl()
        {
            if (_userControl == null)
            {
                List<Control> controls = new List<Control>();
                GetAllControls(this.Parent.Parent.Parent.Parent.Parent, ref controls);
                Type solvencyType = typeof (ISolvencyUserControl);
                Control usrCtrl = controls.FirstOrDefault(c => solvencyType.IsInstanceOfType(c));
                if(usrCtrl != null)
                    _userControl = (ISolvencyUserControl) usrCtrl;
            }
            return _userControl;
        }

        private bool shownMessage;
        public bool IsValid()
        {
            bool valid = true;
            List<ISolvencyDataControl> dataControls = GetDataControls();
            foreach (ISolvencyDataControl dataControl in dataControls)
            {
                valid = valid & dataControl.IsValid();
            }
            List<ISolvencyComboBox> dataComboBox = GetComboControls();
            foreach (ISolvencyComboBox dataControl in dataComboBox)
            {
                valid = valid & dataControl.IsValid();
            }

            // Set parent flag to make sure corrected 
            if (!valid)
            {
                if (!shownMessage)
                {
                    MessageBox.Show("Some of the values for the form are not valid. Please update those controls in red.");
                    shownMessage = true;
                }
            }
            else
            {
                shownMessage = false;
            }

            return valid;
        }


        public long GetPK_ID(List<string> dataTables, string tableName)
        {
            int pos = dataTables.IndexOf(tableName);
            if (pos == -1) return 0;
            return ((IClosedRowControl)this).PK_IDs[pos];
        }

        public void SetPK_ID(List<string> dataTables, long pK_ID, string tableName = null)
        {
            if (!string.IsNullOrEmpty(tableName))
            {
                int pos = dataTables.IndexOf(tableName);
                if (pos != -1)
                    ((IClosedRowControl)this).PK_IDs[pos] = pK_ID;
            }
            else
            {
                for (int i = 0; i < ((IClosedRowControl)this).PK_IDs.Count(); i++)
                {
                    ((IClosedRowControl)this).PK_IDs[i] = 0;
                }
            }
        }
        public string ColName { get; set; } // Delete Me

        public void ResetCacheRefs()
        {
            // This means a reset will be performed and new controls added when appropriate.
            _formControls = null;
            _dataControls = null;
            _comboControls = null;
            _displayControls = null;
        }


        #region Data to Control

        public void PopulateCombos()
        {
            Stopwatch sw = new Stopwatch();
            Stopwatch sw2 = new Stopwatch();
            sw.Start();
            using (GetSQLData getData = new GetSQLData())
            {
                sw2.Start();
                Managers.PopulateNPageCombos.PopulateComboUserControls(getData, GetDataComboControls(),  StaticSettings.LanguageID);
                sw2.Stop();
                Console.WriteLine("      SolvencyClosedRowControl PopulateCombos ISolvencyDataComboBox {0}ms", sw2.ElapsedMilliseconds);
                sw2.Restart();
                Managers.PopulateNPageCombos.PopulateComboUserControls(getData, GetComboControls(), StaticSettings.LanguageID);
                sw2.Stop();
                Console.WriteLine("      SolvencyClosedRowControl PopulateCombos ISolvencyComboBox {0}ms", sw2.ElapsedMilliseconds);
                sw2.Reset();
            }
            sw.Stop();
            Console.WriteLine("    SolvencyClosedRowControl PopulateCombos {0}ms", sw.ElapsedMilliseconds);
            sw.Reset();
        }

        public void PopulateSingleControl(object data)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            if (data != null)
            {
                // PopulateCombos();


                PropertyInfo[] props = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

                // long pkey = 0;
                // PropertyInfo id = props.FirstOrDefault(p => p.Name == "PK_ID");
                // if (id != null) pkey = (long)id.GetValue(data, null);

                foreach (PropertyInfo p in props)
                {
                    // We have a name:
                    string name = p.Name;
                    // Locate the control and set the result
                    ISolvencyDataControl caughtControl = GetDataControls().FirstOrDefault(c => c.ColName == name);
                    if (caughtControl != null)
                    {
                        caughtControl.Result = p.GetValue(data, null);    
                        SetToolText(caughtControl);
                        if (caughtControl as SolvencyCheckBox != null)
                        {
                            ((SolvencyDataComboBox)caughtControl).SetDropDownWidth();
                        }
                    }
                    else
                    {
                        ISolvencyComboBox caughtCombo = GetComboControls().FirstOrDefault(c => c.ColName == name);
                        if (caughtCombo != null)
                        {
                            string getValue = (string)p.GetValue(data, null);
                            SolvencyComboBox refCombo = ((SolvencyComboBox)caughtCombo);
                            refCombo.StopIndexChangedEvent = true;
                            refCombo.SelectItemByValue(getValue);
                            refCombo.StopIndexChangedEvent = false;
                            refCombo.SetDropDownWidth();
                        }
                    }
                }
                sw.Stop();
                Console.WriteLine("    SolvencyClosedRowControl PopulateSingleControl {0}ms", sw.ElapsedMilliseconds);
                sw.Reset();

            }
            else
            {
                // ClearSingleControl(userControl);
            }
        }

        /// <summary>
        /// Sub gets called when a data change has been made.
        /// </summary>
        /// <param name="data"></param>
        public void GatherSingleControl(ref object data)
        {
                if (IsValid())
            {
                if (data != null)
                {
                    // We know there has been a data change:
                    SetDirtyFlag();

                    PropertyInfo[] props = data.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    try
                    {


                        foreach (PropertyInfo p in props)
                        {
                            // We have a name:
                            string name = p.Name;

                            // Locate the control and set the result
                            ISolvencyDataControl caughtControl = GetDataControls().FirstOrDefault(c => c.ColName == name);
                            if (caughtControl != null)
                            {

                                switch (caughtControl.ColumnType)
                                {
                                    case SolvencyDataType.Boolean:
                                        if (caughtControl.Result != null)
                                        {
                                            bool temp = (bool)caughtControl.Result;
                                            p.SetValue(data, temp, null);
                                        }
                                        else
                                            p.SetValue(data, null, null);
                                        break;
                                    case SolvencyDataType.Date:
                                        p.SetValue(data, caughtControl.Result, null);
                                        break;
                                    case SolvencyDataType.Integer:
                                        Int64 temp2 = 0;
                                        if ((caughtControl.Result != null) && (Int64.TryParse(caughtControl.Result.ToString(), out temp2)))
                                            p.SetValue(data, temp2, null);
                                        else
                                        {
                                            if (string.IsNullOrEmpty(caughtControl.Text))
                                                p.SetValue(data, null, null);
                                            else
                                                p.SetValue(data, 0, null);
                                        }
                                        break;
                                    case SolvencyDataType.Decimal:
                                    case SolvencyDataType.Monetry:
                                    case SolvencyDataType.Percentage:
                                        decimal temp3 = 0;
                                        if (string.IsNullOrEmpty(caughtControl.Text))
                                        {
                                            p.SetValue(data, null, null);
                                            break;
                                        }
                                        if (decimal.TryParse(caughtControl.Result.ToString(), out temp3))
                                            p.SetValue(data, temp3, null);
                                        else
                                        {
                                            p.SetValue(data, 0, null);
                                        }
                                        break;
                                    case SolvencyDataType.Code:
                                        // Added 
                                        if (caughtControl as ISolvencyDataComboBox != null)
                                            p.SetValue(data, ((ISolvencyDataComboBox)caughtControl).GetValue, null);
                                        else
                                            p.SetValue(data, caughtControl.Result.ToString(), null);
                                        break;
                                    case SolvencyDataType.String:
                                        p.SetValue(data, caughtControl.Result.ToString(), null);
                                        break;
                                    default:
                                        Type propertyType = p.PropertyType;
                                        var convertedValue = Convert.ChangeType(caughtControl.Result, propertyType);
                                        p.SetValue(data, convertedValue, null);
                                        break;
                                }

                            }
                            else
                            {
                                ISolvencyComboBox caughtCombo = GetComboControls().FirstOrDefault(c => c.ColName == name);
                                if (caughtCombo != null)
                                {

                                    p.SetValue(data, caughtCombo.GetValue, null);
                                    //string getValue = (string)p.GetValue(data, null);
                                    //SolvencyComboBox refCombo = ((SolvencyComboBox)caughtCombo);
                                    //refCombo.StopIndexChangedEvent = true;
                                    //refCombo.SelectItemByValue(getValue);
                                    //refCombo.StopIndexChangedEvent = false;
                                    //refCombo.SetDropDownWidth();
                                }
                            }

                            //if (beforeChange != null)
                            //{
                            //    if (!beforeChange.Equals(p.GetValue(data, null)))
                            //        changedValue = true;
                            //}
                            //else 
                            //    if (p.GetValue(data, null) != null) changedValue = true;
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }

                    //if(changedValue)
                    //    NotifyDirty();
                }
                else
                {
                    // ClearSingleControl(userControl);
                }
            }
        }

        private void SetDirtyFlag()
        {
            ISolvencyUserControl ctrl = GetUserControl();
            ctrl.IsDirty = true;
        }

        public List<ISolvencyDataControl> GetDataControls()
        {
            if (_dataControls == null)
            {
                Type solvencyType = typeof(ISolvencyDataControl);
                IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
                _dataControls = (from ISolvencyDataControl control in midStep select control).ToList();
            }
            return _dataControls;
        }

        public List<ISolvencyComboBox> GetComboControls()
        {
            if (_comboControls == null)
            {
                Type solvencyType = typeof(ISolvencyComboBox);
                IEnumerable<Control> midStep = FormControls.Where(c => solvencyType.IsInstanceOfType(c));
                _comboControls = (from ISolvencyComboBox control in midStep select control).ToList();
            }
            return _comboControls;
        }

        #endregion

        public void SetupEvents(ref GenericDelegates.Response onDelControlDr, ref GenericDelegates.Response onAddControl)
        {
            ISolvencyUserControl ctrl = GetUserControl();
            onDelControlDr += ctrl.DelRecord;
            onAddControl += ctrl.AddRecord;
        }


        public void SetToolText(object sender)
        {
            if (StaticUISettings.MainToolTips != null)
            {
                ISolvencyDataControl ctrl = (ISolvencyDataControl)sender;
                if (ctrl.Text.IndexOf("~") != -1)
                {
                    string trueText = ctrl.TrueValue;
                    if (ctrl.ColumnType != SolvencyDataType.Percentage)
                        StaticUISettings.MainToolTips.SetToolTip((Control)sender, trueText);
                    else
                    {
                        // We have a percentage:
                        decimal ctrlValue = decimal.Parse(trueText, NumberStyles.Any, CultureInfo.CurrentCulture);
                        ctrlValue = ctrlValue * 100;
                        StaticUISettings.MainToolTips.SetToolTip((Control)sender, ctrlValue.ToString("0.#############################") + " " + CultureInfo.CurrentCulture.NumberFormat.PercentSymbol);
                    }
                }
                else
                {
                    StaticUISettings.MainToolTips.SetToolTip((Control)sender, "");
                }
            }

        }

    }
}

