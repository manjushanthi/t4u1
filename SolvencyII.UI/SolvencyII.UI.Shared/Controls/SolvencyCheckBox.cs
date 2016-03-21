using System;
using System.ComponentModel;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.Validators;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed CheckBox
    /// </summary>
    public sealed class SolvencyCheckBox : CheckBox, ISolvencyDataControl
    {
        /// <Note>
        /// There are three places where the settings are text based; "true", "false" & "".
        /// OpenColInfo2Ext.SQLValue Where the text value is used to build the query.
        /// VirtualObjectViewDelegates2.objectListView_OnCellEditFinishing where the control result is saved to the text of an open table row
        /// VirtualObjectViewDelegates2.objectListView_CellEditStarting where the control is created for user to edit
        /// OpenUserControlBase2.CreateColumn take the value from the db and populate the text.
        /// 
        /// </Note>
        public SolvencyCheckBox()
        {
            ColumnType = SolvencyDataType.Boolean;
            //CheckStateChanged += delegate { IsValid(); };
            ThreeState = true;
            Result = null;

        }
        public SolvencyDataType ColumnType { get; set; }
        public string TableCellSignature { get; set; }
        //public int OrdinateID_OneDim { get; set; }
        //public int OrdinateID_TwoDim { get; set; }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public string TrueValue { get { return Text; } }
        public bool IsRowKey { get; set; }
        public ISolvencyDataControl DeepCopy()
        {
            SolvencyCheckBox result = new SolvencyCheckBox();
            this.CopyDataControl(result);
            return result;
        }

        /// <summary>
        /// Text is not set since it appears on the control next to the check box.
        /// </summary>
        public override string Text
        {
            get
            {
                return "";
            }
            set
            {
                //if (!DesignMode)
                //{
                //    if (!String.IsNullOrEmpty(value))
                //    {
                //        bool chk;
                //        if (bool.TryParse(value, out chk))
                //        {
                //            Checked = chk;
                //            return;
                //        }
                //    }
                //    CheckState = CheckState.Indeterminate;
                //}
            }
        }

        [Category("Appearance"), Description("The background color of the component when disabled")]
        [Browsable(true)]
        public object Result
        {
            get
            {
                switch (CheckState)
                {
                    case CheckState.Indeterminate:
                        return null;
                    case CheckState.Checked:
                        return true;
                    case CheckState.Unchecked:
                        return false;
                    default:
                        return null;
                }
            }
            set
            {
                if (!DesignMode)
                {
                    if (!(value == null || value.ToString().Length == 0))
                    {
                        if (value is decimal)
                        {
                            if ((decimal) value == 0)
                                CheckState = CheckState.Unchecked;
                            else
                                CheckState = CheckState.Checked;
                            return;
                        }
                        bool myBool;
                        if (bool.TryParse(value.ToString(), out myBool))
                        {
                            CheckState = myBool ? CheckState.Checked : CheckState.Unchecked;
                            return;
                        }
                    }
                    CheckState = CheckState.Indeterminate;
                }

            }
        }

        public bool IsValid()
        {
            if (!Enabled) return true;
            return true; // Cannot be invalid.
            // return SolvencyControlValidator.IsValid(ColumnType, Text);
        }

        #region DataChanged Event


        private CheckState boolPreUserChange;
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            boolPreUserChange = this.CheckState;

            OnDisplayDimensions();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (boolPreUserChange != this.CheckState)
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
