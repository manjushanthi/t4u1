using System;
using System.Drawing;
//using System.Globalization;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Extensions;
using SolvencyII.UI.Shared.Validators;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed TextBox used for multiple data types
    /// </summary>
    public sealed class SolvencyTextBox : TextBox, ISolvencyDataControl
    {
        public SolvencyTextBox()
        {
            ColumnType = SolvencyDataType.String;
            BorderStyle = BorderStyle.None;
            LostFocus += delegate { IsValid(); };
            TextAlign = HorizontalAlignment.Left;
            // CultureInfo.CurrentCulture..TextInfo.IsRightToLeft
        }
        public SolvencyDataType ColumnType { get; set; }
        
        //public int OrdinateID_OneDim { get; set; }
        //public int OrdinateID_TwoDim { get; set; }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public string GetValue { get { return Text; } }
        public string TrueValue { get { return Text; } }
        public bool IsRowKey { get; set; }
        public ISolvencyDataControl DeepCopy()
        {
            SolvencyTextBox result = new SolvencyTextBox();
            this.CopyDataControl(result);
            return result;
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
            textPreUserChange = this.Text;

            OnDisplayDimensions();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (textPreUserChange != this.Text)
                OnDataChanged();
        }

        #endregion



        public object Result
        {
            get { return Text; }
            set
            {
                if(value != null)
                    Text = value.ToString();
                else
                    Text = "";
            }
        }

        public bool IsValid()
        {
            if (!Enabled) return true;
            if(SolvencyControlValidator.IsValid(ColumnType, Text))
            {
                BackColor = Color.White;
                return true;
            }
            BackColor = Color.MistyRose;
            return false;

        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = CharacterValidator.OnKeyPressHandled(e.KeyChar, ColumnType);
            base.OnKeyPress(e);
        }


        
        


    }
}
