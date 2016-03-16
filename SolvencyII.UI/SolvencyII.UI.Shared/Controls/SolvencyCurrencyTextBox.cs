using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.Conversions;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.Validators;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed TextBox that is used for currency
    /// </summary>
    public sealed class SolvencyCurrencyTextBox : TextBox, ISolvencyDataControl
    {
        public SolvencyCurrencyTextBox()
        {
            ColumnType = SolvencyDataType.Monetry;
            BorderStyle = BorderStyle.None;
            TextAlign = HorizontalAlignment.Right;
            LostFocus += delegate { IsValid(); };
        }

        public string TrueValue { get; set; }

        public SolvencyDataType ColumnType { get; set; }
        public string TableCellSignature { get; set; }

        public string ColName { get; set; }
        public string TableName { get; set; }
        public long AxisID { get; set; } // Used to populate data at run time.
        public bool IsRowKey { get; set; }
        public ISolvencyDataControl DeepCopy()
        {
            SolvencyCurrencyTextBox result = new SolvencyCurrencyTextBox();
            this.CopyDataControl(result);
            return result;
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value.FormatCurrency();
            }
        }

        public double? GetValue { get
        {
            if (!DesignMode)
                if (!string.IsNullOrEmpty(Text) && Text.IsNumeric()) return double.Parse(Text);
            return null;
        } }


        // Test for integeters - EBA C 17.0 a
        // Test for percentages -  S_06_02_01_02__s2md__1_5_2
        public object Result
        {
            get
            {
                if (!DesignMode)
                    if (!string.IsNullOrEmpty(TrueValue))
                    {
                        if (ColumnType != SolvencyDataType.Percentage)
                        {
                            if (TrueValue.IsNumeric())
                            {
                                return decimal.Parse(TrueValue, NumberStyles.Any, CultureInfo.CurrentCulture);
                            }
                        }
                        else
                        {
                            return TrueValue.GetDecimalValue();
                        }
                    }
                    else
                        return null; // Added 30th March 2015 NAJ. Previously was returning 0 for nulls.
                return (decimal) 0;
            }
            set
            {
                if (!DesignMode)
                {
                    if (value != null)
                    {
                        if (value.ToString().IsNumeric())
                        {
                            // Set true value
                            if (value is decimal) TrueValue = ((decimal)value).ToString("0.#############################");
                            else TrueValue = value.ToString();

                            switch (ColumnType)
                            {
                                case SolvencyDataType.Integer:
                                    if (value is decimal)
                                    {
                                        Text = ((decimal) value).ToString("N0");
                                        break;
                                    }
                                    Text = ((long) value).ToString("N0");
                                    break;
                                case SolvencyDataType.Percentage:
                                    Text = value.PercentageToString(CultureInfo.InvariantCulture);
                                    break;
                                case SolvencyDataType.Monetry:
                                    Text = value.DecimalToString(CultureInfo.InvariantCulture, 2);
                                    break;
                                case SolvencyDataType.Decimal:
                                    Text = value.DecimalToString(CultureInfo.InvariantCulture, 4);
                                    break;
                                default:
                                    Text = value.ToString();
                                    break;
                            }
                        }
                        else
                        {
                            TrueValue = value.ToString();
                            Text = value.ToString();
                        }
                    }
                    else
                    {
                        TrueValue = "";
                        Text = "";
                    }
                }
            }
        }

        public bool IsValid()
        {
            if (!Enabled || Text.Length == 0) return true;
            if(SolvencyControlValidator.IsValid(ColumnType, string.IsNullOrEmpty(TrueValue) ? Text : TrueValue))
            {
                BackColor = Color.White;
                return true;
            }
            BackColor = Color.MistyRose;
            return false;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SolvencyCurrencyTextBox
            // 
            this.ResumeLayout(false);

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = CharacterValidator.OnKeyPressHandled(e.KeyChar, ColumnType);
            base.OnKeyPress(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            textPreUserChange = TrueValue;

            // Set the text to be the true value
            if (ColumnType != SolvencyDataType.Percentage)
                Text = TrueValue;
            else
            {
                decimal? trueValue = TrueValue.GetDecimalValue();
                if (trueValue != null)
                    Text = ((decimal)(trueValue * 100)).ToString("0.#############################");
                else
                {
                    Text = TrueValue;
                }
            }

            OnDisplayDimensions();

        }

        protected override void OnLostFocus(EventArgs e)
        {
            // Set the true value - via the Result
            decimal trueDec;
            if (ColumnType != SolvencyDataType.Percentage)
            {
                if (decimal.TryParse(Text, out trueDec))
                    Result = trueDec;
                else
                    TrueValue = Text;
            }
            else
            {
                decimal? trueValue = PercentageConvertor.PercentageStringToDecimal(Text);
                Result = trueValue;
            }
            base.OnLostFocus(e);

            if (textPreUserChange != TrueValue)
                OnDataChanged();
        }


        /// <summary>
        /// Stub exclusively used for testing.
        /// </summary>
        /// <param name="eventArgs"></param>
        public void OnLostFocusStub(EventArgs eventArgs)
        {
            OnLostFocus(eventArgs);
        }

        /// <summary>
        /// Stub exclusively used for testing.
        /// </summary>
        /// <param name="eventArgs"></param>
        public void OnGotFocusStub(EventArgs eventArgs)
        {
            OnGotFocus(eventArgs);
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
        
        #endregion

    }
}
