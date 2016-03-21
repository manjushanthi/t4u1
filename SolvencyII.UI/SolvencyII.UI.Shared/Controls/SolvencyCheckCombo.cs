using System;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed Combo box that simply has yes and no.
    /// </summary>
    public class SolvencyCheckCombo : ComboBox, ISolvencyDataControl
    {
        public SolvencyCheckCombo()
        {
            Items.Add(new ComboItem {Text = ""});
            Items.Add(new ComboItem {Text = "Yes"});
            Items.Add(new ComboItem {Text = "No"});

            DisplayMember = "Text";
            DropDownStyle = ComboBoxStyle.DropDownList;
            ColumnType = SolvencyDataType.Boolean;
            Width = 100;
            SelectedIndexChanged += SelectedIndexChangedAbstractCheck;

        }


        public SolvencyDataType ColumnType { get; set; }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public string TrueValue { get; private set; }
        public bool IsRowKey { get; set; }
        public ISolvencyDataControl DeepCopy()
        {
            SolvencyCheckCombo result = new SolvencyCheckCombo();
            this.CopyDataControl(result);
            return result;
        }


        public object Result
        {
            get
            {
                if (!DesignMode)
                {
                    switch (SelectedIndex)
                    {
                        case -1:
                        case 0:
                            return null;
                        case 1:
                            return true;
                        case 2:
                            return false;
                        default:
                            throw new ArgumentException("Invalid selection to solvencyCheckCombo.");
                    }
                }
                return null;
            }
            set
            {
                if (!DesignMode)
                {
                    
                    if (value == null)
                        SelectedItem = Items[0];
                    else
                    {
                        bool myVal;
                        if (bool.TryParse(value.ToString(), out myVal))
                        {
                            if (myVal)
                                SelectedItem = Items[1];
                            else
                                SelectedItem = Items[2];
                        }
                        else
                        {
                            // We have a non-boolean
                            int myInt;
                            if (int.TryParse(value.ToString(), out myInt))
                            {
                                if (myInt == 0)
                                    SelectedItem = Items[2];
                                else
                                    SelectedItem = Items[1];
                            }
                        }
                    }
                }
            }
        }

        public bool IsValid()
        {
            // Cannot be invalid
            return true;
        }

        #region DataChanged Event

        private void SelectedIndexChangedAbstractCheck(object sender, EventArgs e)
        {
            OnDataChanged();
        }

        public event GenericDelegates.SolvencyControlChanged DataChanged;
        public event GenericDelegates.DisplayDimensions DisplayDimensions;

        private void OnDataChanged()
        {
            if (DataChanged != null)
                DataChanged(this, this.ColName);
        }

        private void OnDisplayDimensions()
        {
            if (DisplayDimensions != null)
                DisplayDimensions(this, this.ColName);
        }

        #endregion
    }
}

