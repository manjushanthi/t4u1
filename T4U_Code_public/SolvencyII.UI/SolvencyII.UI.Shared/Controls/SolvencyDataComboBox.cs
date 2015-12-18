using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Config;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.Validators;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed Combobox, used for data.
    /// </summary>
    public sealed class SolvencyDataComboBox : ComboBox, ISolvencyDataComboBox
    {
        private object _result;

        public SolvencyDataComboBox()
        {
            ColumnType = SolvencyDataType.Code;
            LostFocus += delegate { IsValid(); };
            DropDownStyle = ComboBoxStyle.DropDownList;
            SelectedIndexChanged += SelectedIndexChangedAbstractCheck;
        }
        public SolvencyDataType ColumnType { get; set; }
        public long OrdinateID { get; set; }
        public long HierarchyID { get; set; }
        public void PopulateWithHierarchy2(List<OpenComboItem> items)
        {
            Items.Clear();
            this.PopulateWithHierarchy2(items);
        }

        public long StartOrder { get; set; }
        public long NextOrder { get; set; }
        public void PopulateWithComboItems(List<ComboItem> data)
        {
            Items.Clear();
            this.PopulateComboItems(data);
        }
        public string TrueValue { get { return this.SelectedText; } }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public bool IsRowKey { get; set; }
        public ISolvencyDataControl DeepCopy()
        {
            SolvencyDataComboBox result = new SolvencyDataComboBox();
            this.CopyDataControl(result);
            return result;
        }
        public bool StopIndexChangedEvent { get; set; }

        public object Result
        {
            get { return _result; }
            set
            {
                if (!DesignMode)
                {
                    _result = value;
                    if (_result != null)
                    {
                        StopIndexChangedEvent = true;
                        this.SelectItemByValue(_result.ToString());
                        StopIndexChangedEvent = false;
                        this.SetDropDownWidth();
                    }
                }
            }
        }

        public long AxisID { get; set; }
        public ComboItemType TypeOfItems { get; set; }

        public string GetValue
        {
            get
            {
                if (!DesignMode)
                {
                    if (SelectedItem != null)
                        return ((ListViewItem)SelectedItem).Name;
                }
                return "";
            }
        }

        public string SetValue
        {
            set
            {
                if (!DesignMode)
                {
                    StopIndexChangedEvent = true;
                    this.SelectItemByValue(value);
                    StopIndexChangedEvent = false;
                }
            }
        }

        private bool IsPageCombo()
        {
            return ColName.StartsWith("PAGE");
        }

        public bool IsValid()
        {
            if (!Enabled) return true;
            if (IsPageCombo() && string.IsNullOrEmpty(GetValue))
            {
                BackColor = Color.MistyRose;
                return false;
            }
            if (SolvencyControlValidator.IsValid(ColumnType, this.Text))
            {
                BackColor = Color.White;
                return true;
            }
            BackColor = Color.MistyRose;
            return false;

        }

        private void SelectedIndexChangedAbstractCheck(object sender, EventArgs e)
        {
            if (StopIndexChangedEvent) return;
            ListViewItem selectedItem = (ListViewItem)((ComboBox)sender).SelectedItem;
            bool IsAbstract = false;
            if (selectedItem.Tag != null && bool.TryParse(selectedItem.Tag.ToString(), out IsAbstract))
            {
                if (IsAbstract)
                {
                    MessageBox.Show("This is a grouping item, not a valid selection.");
                    this.SelectFirstItem();
                }
            }
            if(!IsAbstract)
                OnDataChanged();
        }

        #region Events

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

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            OnDisplayDimensions();

        }
        public bool IsPopulated()
        {
            return this.ItemsPopulated();
        }


    }


}
