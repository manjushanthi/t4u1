using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Base class for main templates contains key functionality required for interaction.
    /// </summary>
    public class SolvencyUserControl : UserControl
    {
        private List<Control> _formControls;
        private List<ISolvencyDataControl> _dataControls;
        private List<ISolvencyComboBox> _comboControls;
        //private List<ISolvencyDisplayControl> _displayControls;
        //private List<ISolvencyDataComboBox> _dataCombos;

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

        #region Data to Control

        public void PopulateSingleControl(object data)
        {
            if (data != null)
            {
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
                
            }
            else
            {
                // ClearSingleControl(userControl);
            }
        }

        public void GatherSingleControl(ref object data)
        {
            if (data != null)
            {
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

                        try
                        {
                            //p.SetValue(data, Convert.ChangeType(caughtControl.Result, p.PropertyType), null);
                            //caughtControl.Result = p.GetValue(data, null);
                            switch (caughtControl.ColumnType)
                            {
                                case SolvencyDataType.Boolean:
                                    bool temp = (bool)caughtControl.Result;
                                    p.SetValue(data, temp, null);
                                    break;
                                case SolvencyDataType.Date:
                                    p.SetValue(data, caughtControl.Result, null);
                                    break;
                                case SolvencyDataType.Integer:
                                    Int64 temp2 = 0;
                                    if (Int64.TryParse(caughtControl.Result.ToString(), out temp2))
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
                                    if (decimal.TryParse(caughtControl.Result.ToString(), out temp3))
                                        p.SetValue(data, temp3, null);
                                    else
                                    {
                                        if (string.IsNullOrEmpty(caughtControl.Text))
                                            p.SetValue(data, null, null);
                                        else
                                            p.SetValue(data, 0, null);
                                    }
                                    break;
                                case SolvencyDataType.Code:
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
                        catch (Exception e)
                        {
                            MessageBox.Show(e.ToString());
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
                }

            }
            else
            {
                // ClearSingleControl(userControl);
            }
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SolvencyUserControl
            // 
            this.Name = "SolvencyUserControl";
            this.Size = new System.Drawing.Size(349, 218);
            this.ResumeLayout(false);

        }



    }
}
