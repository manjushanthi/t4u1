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
    /// Sub classed Combobox that only has a selected Yes element
    /// </summary>
    public class SolvencyTrueCombo : ComboBox, ISolvencyDataControl
    {
        public SolvencyTrueCombo()
        {
            Items.Add(new ComboItem {Text = "Yes"});

            DisplayMember = "Text";
            DropDownStyle = ComboBoxStyle.DropDownList;
            ColumnType = SolvencyDataType.Boolean;
            Width = 100;

        }

        #region Events

        public event GenericDelegates.DisplayDimensions DisplayDimensions;

        private void OnDisplayDimensions()
        {
            if (DisplayDimensions != null)
                DisplayDimensions(this, this.ColName);
        }

        #endregion


        public SolvencyDataType ColumnType { get; set; }
        public string ColName { get; set; }
        public string TableName { get; set; }
        public string TrueValue { get; private set; }
        public bool IsRowKey { get; set; }
        public ISolvencyDataControl DeepCopy()
        {
            SolvencyTrueCombo result = new SolvencyTrueCombo();
            this.CopyDataControl(result);
            return result;
        }

        public object Result
        {
            get
            {
                return true;
            }
            set
            {
                if (!DesignMode) SelectedItem = Items[0];
            }
        }

        public bool IsValid()
        {
            // Cannot be invalid
            return true;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            OnDisplayDimensions();
        }



        public event GenericDelegates.SolvencyControlChanged DataChanged;
    }
}

