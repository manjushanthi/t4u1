using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.Validators;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed combo box for text based user input
    /// </summary>
    public class SolvencyTextComboBox : ComboBox, ISolvencyComboBox
    {
        public SolvencyTextComboBox()
        {
            ColumnType = SolvencyDataType.String;
            LostFocus += delegate { IsValid(); };
            DropDownStyle = ComboBoxStyle.DropDownList;
            TypeOfItems = ComboItemType.TextEntries;
            // DrawMode = DrawMode.OwnerDrawFixed;
        }
        public SolvencyDataType ColumnType { get; set; }
        public long OrdinateID { get; set; }
        public long HierarchyID { get; set; }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public object Result { get; set; }
        public string TableNames { get; set; }
        public bool FixedDimension { get; set; }
        public string ColNames { get; set; }
        public long AxisID { get; set; }
        public object PreviousSelectedItem { get; set; }
        public ComboItemType TypeOfItems { get; set; }
        public bool IsRowKey { get; set; }

        public long StartOrder { get; set; }
        public long NextOrder { get; set; }


        private EventHandler myDelegate;
        private EventHandler myDelegateDrop;
        private EventHandler myDelegateLost;
        private EventHandler myDelegateGot;


        #region Events

        public event GenericDelegates.DisplayDimensions DisplayDimensions;

        private void OnDisplayDimensions()
        {
            if (DisplayDimensions != null)
                DisplayDimensions(this, this.ColName);
        }

        #endregion


        public string GetValue
        {
            get
            {
                if (!DesignMode)
                {
                    return Text;
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
                    Text = value;
                }
            }
        }

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

        public void PopulateWithComboItems(List<ComboItem> data)
        {
            if(Items != null) Items.Clear();
            this.PopulateComboItems(data);
        }

        public object GetBackColor { get { return (object) this.BackColor; } }

        public void PopulateWithComboItems(List<ComboItem> data, string selectedValue)
        {
            Items.Clear();
            this.PopulateComboItems(data);
            if (!string.IsNullOrEmpty(selectedValue)) this.SelectItemByValue(selectedValue);
        }

        public void PopulateWithHierarchy(List<OpenComboItem> items, string selectedValue)
        {
            if (this.Items.Count == 0)
            {
                Items.Clear();
                this.PopulateWithHierachy2(items, selectedValue);
            }
        }

        public void PopulateWithComboItems(List<string> data, string selectedValue)
        {
            Items.Clear();
            this.PopulateComboItems(data);
            if (!string.IsNullOrEmpty(selectedValue)) this.SelectItemByValue(selectedValue);
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

        public void SetOnLostFocus(EventHandler onLostFocus)
        {
            if (myDelegateLost != null) LostFocus -= myDelegateLost;
            myDelegateLost = onLostFocus;
            LostFocus += onLostFocus;
        }
        public void SetOnGotFocus(EventHandler onGotFocus)
        {
            if (myDelegateGot != null) GotFocus -= myDelegateGot;
            myDelegateGot = onGotFocus;
            GotFocus += onGotFocus;
        }
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            PreviousSelectedItem = SelectedItem;
        }

        public void SetPrevious()
        {
            // This gets called if the user says they do not want to loose changes and this combo was changed.
            if (PreviousSelectedItem != null)
            {
                // Stop events - this stops iteration.
                if (myDelegate != null)
                    SelectedIndexChanged -= myDelegate;
                SelectedItem = PreviousSelectedItem;
                // Enable events again.
                if (myDelegate != null)
                    SelectedIndexChanged += myDelegate;
            }
        }

        public bool IsPopulated()
        {
            return this.ItemsPopulated();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            OnDisplayDimensions();
        }

        public event GenericDelegates.SolvencyControlChanged DataChanged;
    }


}
