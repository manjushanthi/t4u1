using System;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed Text Box, for nPage usage.
    /// </summary>
    public sealed class SolvencyPageTextBox : TextBox, ISolvencyPageControl
    {
        public SolvencyPageTextBox()
        {
            Visible = false;
        }
 
        public string ColName { get; set; }
        public string TableNames { get; set; }
        public bool FixedDimension { get; set; }
        public SolvencyDataType ColumnType { get{ return SolvencyDataType.String;} }

        #region Events

        public event GenericDelegates.DisplayDimensions DisplayDimensions;

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

        public event GenericDelegates.SolvencyControlChanged DataChanged;
    }
}
