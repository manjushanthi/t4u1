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

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed ComboBox
    /// </summary>
    public sealed class SolvencyComboBox : ComboBox, ISolvencyComboBox
    {
        public SolvencyComboBox()
        {
            ColumnType = SolvencyDataType.Code;
            LostFocus += delegate { IsValid(); };
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawFixed;
            DrawItem += ComboBoxConfig.ComboBoxDrawItem;
            FixedDimension = false;
            SelectedIndexChanged += SelectedIndexChangedAbstractCheck;
            SelectedIndexChanged += SelectedIndexChangedDataChanged;
            DropDown += SolvencyComboBox_DropDown;
        }

        void SolvencyComboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;
            int newWidth;

            foreach (ListViewItem listViewItem in ((ComboBox)sender).Items)
            {
                newWidth = (int)g.MeasureString(listViewItem.Text, font).Width
                    + vertScrollBarWidth;

                if (width < newWidth)
                {
                    width = newWidth;
                }
            }

            senderComboBox.DropDownWidth = width;
        }
        public SolvencyDataType ColumnType { get; set; }
        public long OrdinateID { get; set; }
        public long HierarchyID { get; set; }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public string TableNames { get; set; }
        public string ColNames { get; set; }
        public long AxisID { get; set; }
        public long StartOrder { get; set; }
        public long NextOrder { get; set; }
        public bool IsRowKey { get; set; }
        public bool StopIndexChangedEvent { get; set; }
        public object GetBackColor { get { return BackColor; } }

        public object PreviousSelectedItem { get; set; }
        public ComboItemType TypeOfItems { get; set; }
        private EventHandler myDelegate;
        private EventHandler myDelegateDrop;
        private EventHandler myDelegateLost;
        private EventHandler myDelegateGot;

        public void PopulateWithComboItems(List<ComboItem> data, string selectedValue)
        {
            StopIndexChangedEvent = true;
            Items.Clear();
            this.PopulateComboItems(data);
            if(!string.IsNullOrEmpty(selectedValue)) this.SelectItemByValue(selectedValue);
            StopIndexChangedEvent = false;
        }

        public void PopulateWithHierarchy(List<OpenComboItem> items, string selectedValue)
        {
            if (this.Items.Count == 0)
            {
                StopIndexChangedEvent = true;
                Items.Clear();
                this.PopulateWithHierachy2(items, selectedValue);
                StopIndexChangedEvent = false;
            }
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


        public bool IsValid()
        {
            if (!Enabled) return true;
            // Check the tag to see if IsAbstract is set.
            if (!this.IsAbstract())
            {
                if (SolvencyControlValidator.IsValid(ColumnType, this.Text))
                {
                    BackColor = Color.White;
                    return true;
                }
            }
            BackColor = Color.MistyRose;
            return false;
        }

        private void SelectedIndexChangedAbstractCheck(object sender, EventArgs e)
        {
            if(StopIndexChangedEvent) return;
            ListViewItem selectedItem = (ListViewItem)((ComboBox)sender).SelectedItem;
            bool IsAbstract = false;
            if (selectedItem != null && selectedItem.Tag != null && bool.TryParse(selectedItem.Tag.ToString(), out IsAbstract))
            {
                if (IsAbstract)
                {
                    MessageBox.Show("This is a grouping item, not a valid selection.");
                    this.SelectFirstItem();
                }
            }
        }

        public void SetSelectedIndexChanged(EventHandler selectedIndexChanged)
        {
            // Clear any existing event
            if (myDelegate != null) SelectedIndexChanged -= myDelegate;
            SelectedIndexChanged -= SelectedIndexChangedDataChanged;
            myDelegate = selectedIndexChanged;
            this.SelectedIndexChanged += selectedIndexChanged;
            SelectedIndexChanged += SelectedIndexChangedDataChanged;
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


        public bool FixedDimension { get; set; }
        //public string Text { get; set; }

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

        #region Events

        private void SelectedIndexChangedDataChanged(object sender, EventArgs e)
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
