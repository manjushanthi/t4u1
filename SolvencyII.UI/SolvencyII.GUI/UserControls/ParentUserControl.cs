using System;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.UserControls;

namespace SolvencyII.GUI.UserControls
{
    /// <summary>
    /// This is the control that gets docked above the current use control to show the Save, Cancel and Delete buttons.
    /// </summary>
    public partial class ParentUserControl : UserControl, IParentUserControl
    {
        private Action<IUserControlBase> _saveUserControlData;
        private Action<IUserControlBase> _deleteUserControlData;
        private Action<bool> _toggleFilingIndicator;
        private void ToggleFilingIndicator(bool newValue)
        {
            if (_toggleFilingIndicator != null) _toggleFilingIndicator.Invoke(newValue);
        }
        private Action _cancelControlData;
        private bool _filed = true;

        public bool Filed
        {
            get { return _filed; }
            set
            {
                _filed = value;
                if (_filed)
                {
                    btnFiled.Text = "Mark as not reported";
                }
                else
                {
                    btnFiled.Text = "Mark as reported";
                }

                UserControlBase ctrl = Controls.OfType<UserControlBase>().FirstOrDefault();
                // Enable / Disable the control
                if (ctrl != null) ctrl.Enabled = _filed;
                else
                {
                    UserControlBase2 ctrl2 = Controls.OfType<UserControlBase2>().FirstOrDefault();
                    if (ctrl2 != null) ctrl2.Enabled = _filed;
                }

            }
        }

        #region 

        public ParentUserControl(Action<IUserControlBase> saveUserControlData, Action<IUserControlBase> deleteUserControlData, Action<bool> toggleFilingIndicator, Action cancelUserControlData, bool showMainButtons, bool typed)
        {
            InitializeComponent();
            // Event should be found on frmMain in POC.
            ClearAllRefs(); // Prevent doubling up with refreshes.
            _saveUserControlData += saveUserControlData;
            _deleteUserControlData += deleteUserControlData;
            _toggleFilingIndicator += toggleFilingIndicator;
            _cancelControlData += cancelUserControlData;
            Dock = DockStyle.Fill;
            AutoScroll = true;
            HideSaveCancel(showMainButtons);

            // Filing Indicator button is not visible for open row templates.
            btnFiled.Visible = !typed;

        }

        #endregion


        public void EnableSaveCancel(bool enable)
        {
            btnSave.Enabled = enable;
            btnCancel.Enabled = enable;
            btnDelete.Enabled = enable;
            // btnFiled.Enabled = enable;
        }

        public bool ChangeCancelToClose
        {
            set { btnCancel.Text = value ? "Back" : "Cancel"; }
        }

        private void HideSaveCancel(bool hide)
        {
            btnCancel.Visible = !hide;
            btnSave.Visible = !hide;
            btnDelete.Visible = !hide;
            btnFiled.Visible = !hide;
        }

        private void ParentUserControl_Load(object sender, EventArgs e)
        {
            SetupLabels();
        }

        private void SetupLabels()
        {
            btnSave.Text = LanguageLabels.GetLabel(77, "Save");
            btnCancel.Text = LanguageLabels.GetLabel(78, "Cancel");

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _cancelControlData.Invoke();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Find the control
            UserControlBase ctrl = Controls.OfType<UserControlBase>().FirstOrDefault();
            // Raise the event
            if(ctrl != null)
            {
                // Save the data
                _saveUserControlData.Invoke(ctrl);
            }
            else
            {
                UserControlBase2 ctrl2 = Controls.OfType<UserControlBase2>().FirstOrDefault();
                if (ctrl2 != null)
                {
                    // Save the data
                    _saveUserControlData.Invoke(ctrl2);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this template?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                UserControlBase ctrl = Controls.OfType<UserControlBase>().FirstOrDefault();
                // Raise the event
                if (ctrl != null)
                {
                    // Save the data
                    _deleteUserControlData.Invoke(ctrl);
                }
                else
                {
                    UserControlBase2 ctrl2 = Controls.OfType<UserControlBase2>().FirstOrDefault();
                    // Raise the event
                    if (ctrl2 != null)
                    {
                        // Save the data
                        _deleteUserControlData.Invoke(ctrl2);
                    }
                }
                Visible = false;
            }
        }

        private void btnFiled_Click(object sender, EventArgs e)
        {
            if (Filed)
            {
                if (MessageBox.Show("NOTE: Marking this table as not reported with apply to the entire template variant and delete data in all tables of this template variant.\r\nDo you want to continue?", "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    UserControlBase ctrl = Controls.OfType<UserControlBase>().FirstOrDefault();
                    // Raise the event
                    if (ctrl != null)
                    {
                        ToggleFilingIndicator(!Filed);
                        Filed = !_filed;
                    }
                    else
                    {
                        UserControlBase2 ctrl2 = Controls.OfType<UserControlBase2>().FirstOrDefault();
                        if (ctrl2 != null)
                        {
                            ToggleFilingIndicator(!Filed);
                            Filed = !_filed;
                        }
                    }
                    Visible = false;
                }
            }
            else
            {
                ToggleFilingIndicator(!Filed);
                Filed = !_filed;
            }
        }

        public void ClearAllRefs()
        {
            if (_saveUserControlData != null)
                foreach (Delegate d in _saveUserControlData.GetInvocationList())
                {
                    _saveUserControlData -= (Action<IUserControlBase>)d;
                }
            if (_deleteUserControlData != null)
                foreach (Delegate d in _deleteUserControlData.GetInvocationList())
                {
                    _deleteUserControlData -= (Action<IUserControlBase>)d;
                }
            if (_cancelControlData != null)
                foreach (Delegate d in _cancelControlData.GetInvocationList())
                {
                    _cancelControlData -= (Action)d;
                }
        }


    }
}
