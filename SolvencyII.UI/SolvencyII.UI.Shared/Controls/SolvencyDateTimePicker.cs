using System;
using System.Drawing;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.Validators;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed Date Time picker
    /// </summary>
    public sealed class SolvencyDateTimePicker : DateTimePicker, ISolvencyDataControl
    {
        public SolvencyDateTimePicker()
        {
            ColumnType = SolvencyDataType.Date;
            LostFocus += delegate { IsValid(); };
            Format = DateTimePickerFormat.Short;
            Value = DateTime.MinValue;
        }
        public SolvencyDataType ColumnType { get; set; }
        public string TableCellSignature { get; set; }
        //public int OrdinateID_OneDim { get; set; }
        //public int OrdinateID_TwoDim { get; set; }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public DateTime GetValue { get { return Value; } }
        public string TrueValue { get { return Text; } }
        public bool IsRowKey { get; set; }
        public ISolvencyDataControl DeepCopy()
        {
            SolvencyDateTimePicker result = new SolvencyDateTimePicker();
            this.CopyDataControl(result);
            return result;
        }


        /// <summary>
        /// Interaction with the results of this control are through Result - not value
        /// This allows us to pass null / empty values
        /// </summary>
        public object Result
        {
            get
            {
                if (bIsNull) return null;
                return Value;
            }
            set
            {
                if (!DesignMode)
                {
                    if (value == null) Value = DateTime.MinValue;
                    if (value is DateTime?) Value = (DateTime) value;
                }
            }
        }

        public bool IsValid()
        {
            if (!Enabled) return true;
            if (bIsNull) return true;
            return SolvencyControlValidator.IsValid(ColumnType, Text);

            // This control ensures the date is valid itself    

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

        private string textPreUserChange;
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (this.Result != null)
                textPreUserChange = this.Result.ToString();
            else
                textPreUserChange = "";

            OnDisplayDimensions();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (this.Result != null)
            {
                if (textPreUserChange != this.Result.ToString())
                    OnDataChanged();
            }
            else
            {
                if (textPreUserChange != "")
                    OnDataChanged();
            }
        }

        #endregion


        #region Code added to Manage Null and blank values

        private DateTimePickerFormat oldFormat = DateTimePickerFormat.Long;
        private string oldCustomFormat;
        private bool bIsNull;

        public new DateTime Value
        {
            get
            {
                if (bIsNull)
                    return DateTime.MinValue;
                return base.Value;
            }
            set
            {
                if (value == DateTime.MinValue)
                {
                    if (bIsNull == false)
                    {
                        oldFormat = this.Format;
                        oldCustomFormat = this.CustomFormat;
                        bIsNull = true;
                    }

                    this.Format = DateTimePickerFormat.Custom;
                    this.CustomFormat = " ";
                }
                else
                {
                    if (bIsNull)
                    {
                        this.Format = oldFormat;
                        this.CustomFormat = oldCustomFormat;
                        bIsNull = false;
                    }
                    base.Value = value;
                }
            }
        }

        protected override void OnCloseUp(EventArgs eventargs)
        {
            if (MouseButtons == MouseButtons.None)
            {
                if (bIsNull)
                {
                    this.Format = oldFormat;
                    this.CustomFormat = oldCustomFormat;
                    bIsNull = false;
                }
            }
            base.OnCloseUp(eventargs);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.KeyCode == Keys.Delete)
                this.Value = DateTime.MinValue;
            if (e.KeyCode == Keys.Back)
                this.Value = DateTime.MinValue;
        }

        #endregion




    }
}
