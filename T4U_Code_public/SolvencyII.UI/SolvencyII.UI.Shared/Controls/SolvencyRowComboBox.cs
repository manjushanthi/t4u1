using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Config;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.Validators;
using System.Linq;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed combo box used for data
    /// </summary>
    public sealed class SolvencyRowComboBox : ComboBox, ISolvencyDataControl
    {
        public SolvencyRowComboBox()
        {
            ColumnType = SolvencyDataType.String;
            LostFocus += delegate { IsValid(); };
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            DrawItem += ComboBoxConfig.ComboBoxDrawItem;
            FixedDimension = false;
            SelectedIndexChanged += SelectedIndexChangedAbstractCheck;
        }
        public SolvencyDataType ColumnType { get; set; }
        //public int OrdinateID_OneDim { get; set; }
        //public int OrdinateID_TwoDim { get; set; }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public string TableNames { get; set; }
        public string ColNames { get; set; }
        public long AxisID { get; set; }
        public long OrdinateID { get; set; }
        public ComboItemType TypeOfItems { get; set; }
        private EventHandler myDelegate;
        private EventHandler myDelegateDrop;
        public string TrueValue { get { return this.SelectedText; } }
        public bool IsRowKey { get; set; }
        public ISolvencyDataControl DeepCopy()
        {
            SolvencyRowComboBox result = new SolvencyRowComboBox();
            this.CopyDataControl(result);
            return result;
        }


        public void PopulateWithComboItems(List<ComboItem> data)
        {
            Items.Clear();
            this.PopulateComboItems(data);
        }

        public string GetValue{get
        {
            if (!DesignMode)
            {
                if (SelectedItem != null)
                    return ((ListViewItem)SelectedItem).Name;
            }
            return "";
        }}

        public bool IsValid()
        {
            if (!Enabled) return true;
            if(SolvencyControlValidator.IsValid(ColumnType, this.Text))
            {
                BackColor = Color.White;
                return true;
            }
            BackColor = Color.MistyRose;
            return false;

        }

        private void SelectedIndexChangedAbstractCheck(object sender, EventArgs e)
        {
            ListViewItem selectedItem = (ListViewItem)((ComboBox)sender).SelectedItem;
            bool IsAbstract = false;
            if (selectedItem.Tag != null && bool.TryParse(selectedItem.Tag.ToString(), out IsAbstract))
            {
                if (IsAbstract)
                {
                    MessageBox.Show("This is a grouping item, not a valid selection.");
                    ((ComboBox) sender).SelectFirstNonAbstractItem();
                }
            }
        }

        public void SetSelectedIndexChanged(EventHandler selectedIndexChanged)
        {
            // Clear any existing event
            if (myDelegate != null) SelectedIndexChanged -= myDelegate;
            myDelegate = selectedIndexChanged;
            this.SelectedIndexChanged += selectedIndexChanged;

        }

        public void SetOnDropDown(EventHandler onDropDown)
        {
            // Clear any existing event
            if (myDelegateDrop != null) DropDown -= myDelegateDrop;
            myDelegateDrop = onDropDown;
            DropDown += onDropDown;
        }

        public bool FixedDimension { get; set; }
        //public string Text { get; set; }
        public object Result { get; set; }

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

        private string textPreUserChange;
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            textPreUserChange = this.GetValue;
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (textPreUserChange != this.GetValue)
                OnDataChanged();
        }

        #endregion


    }


}
