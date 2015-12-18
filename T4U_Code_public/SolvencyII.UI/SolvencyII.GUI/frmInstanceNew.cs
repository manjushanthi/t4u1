using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain;
using SolvencyII.Domain.Extensions;
using SolvencyII.UI.Shared.Config;
using SolvencyII.UI.Shared.Extensions;

namespace SolvencyII.GUI
{
    public partial class frmInstanceNew : Form
    {
        private bool _newInstance;
        public long InstanceID;

        public frmInstanceNew(bool newInstance, long instanceID = 0)
        {
            InitializeComponent();
            _newInstance = newInstance;
            InstanceID = instanceID;
            SetupForm();
        }

        private void SetupForm()
        {
            using (GetSQLData getData = new GetSQLData())
            {
                List<ListViewItem> comboData = ComboBoxConfig.PopulateComboParentBranches(getData);
                List<ListViewItem> comboCurrencyData = ComboBoxConfig.PopulateComboCurrency(getData);
                dInstance instance = new dInstance();
                if (!_newInstance)
                {
                    instance = getData.GetInstanceDetails(InstanceID);
                }
                else
                {
                    instance.Timestamp = DateTime.Now;
                    instance.PeriodEndDateOrInstant = LastDayOfLastQuarter(DateTime.Now);// DateTime.Now.AddYears(1);
                }
                cboModel.PopulateWithListViewItems(comboData);
                cboModel.SetDropDownWidth();
                cboCurrency.PopulateWithListViewItems(comboCurrencyData);
                cboCurrency.SetDropDownWidth();
                PopulateForm(instance);
            }
        }

        public DateTime LastDayOfLastQuarter(DateTime date)
        {
            int result = ((int)Math.Ceiling(date.Month / 3.0)) - 1;

            if (result == 0)
            {
                // January - March
                return new DateTime(date.Year - 1, 12, 31);
            }
            else if (result == 1)
            {
                // April - June
                return new DateTime(date.Year, 3, 31);
            }
            else if (result == 2)
            {
                // July - September
                return new DateTime(date.Year, 6, 30);
            }
            else if (result == 3)
            {
                // October - December
                return new DateTime(date.Year, 9, 30);
            }
            else // Defalut
            {
                return new DateTime(date.Year, 3, 31);
            }
        }

        private void PopulateForm(dInstance instance)
        {
            // Populate controls with instance
            cboModel.SelectItemByValue(instance.ModuleID);
            txtEntityID.Text = instance.EntityIdentifier;           
            dtPeriod.Value = instance.PeriodEndDateOrInstant ?? dtPeriod.MinDate;
            cboCurrency.SelectItemByValue(instance.EntityCurrency);
            txtReportName.Text = instance.EntityName;
            if (!_newInstance)
            txtEntityScheme.Text = instance.EntityScheme;
        }

        private dInstance GatherForm()
        {
            // Gather information from the form and create the instance.
            dInstance result = new dInstance();
            result.InstanceID = InstanceID;
            result.ModuleID = int.Parse(((ListViewItem)cboModel.SelectedItem).Name);
            result.EntityIdentifier = txtEntityID.Text;           
            result.PeriodEndDateOrInstant = dtPeriod.Value;
            result.EntityName = txtReportName.Text;
            result.EntityScheme = txtEntityScheme.Text;

            string currency = ((ListViewItem)cboCurrency.SelectedItem).Name;

            result.EntityCurrency = currency;
            result.Timestamp = DateTime.Now;
            return result;
        }

        private void CloseForm()
        {
            Close();
            Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            CloseForm();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if(FormValid() && InsertInstance())
            {
                DialogResult = DialogResult.OK;
                CloseForm();
            }
        }

        private bool InsertInstance()
        {
            PutSQLData putData = new PutSQLData();
            long newInstanceID;
            string result = putData.InsertUpdateInstance(GatherForm(), out newInstanceID);
            putData.Dispose();
            if (newInstanceID > 0)
            {
                // Set the parent form - using the DialogResult instead.
                InstanceID = newInstanceID;
                // ((frmMain) Owner).InstanceID = newInstanceID;
                return true;
            }
            MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(56, "Unable to create the new instance"), result));
            return false;
        }

        private bool FormValid()
        {
            if (string.IsNullOrEmpty(txtEntityID.Text))
            {
                MessageBox.Show(LanguageLabels.GetLabel(57, "Please add an entity identifier"), LanguageLabels.GetLabel(56, "Unable to create the new instance"));
                return false;
            }
            int intModuleID;
            if (cboModel.SelectedItem == null || !int.TryParse(((ListViewItem)cboModel.SelectedItem).Name, out intModuleID) || intModuleID == 0)
            {
                MessageBox.Show(LanguageLabels.GetLabel(59, "Please select a module"), LanguageLabels.GetLabel(56, "Unable to create the new instance:"));
                return false;
            }          
            if (string.IsNullOrEmpty(txtReportName.Text))
            {
                MessageBox.Show(LanguageLabels.GetLabel(61, "Please add a report name"), LanguageLabels.GetLabel(56, "Unable to create the new instance:"));
                return false;
            }

            ListViewItem cboCurrencySelectedItem = cboCurrency.SelectedItem as ListViewItem;
            if (cboCurrencySelectedItem == null || cboCurrencySelectedItem.Text == "Please Select")
            {
                MessageBox.Show(LanguageLabels.GetLabel(62, "Please select a currency"), LanguageLabels.GetLabel(56, "Unable to create the new instance:"));
                return false;
            }


            return true;
        }

        private void frmInstanceNew_Load(object sender, EventArgs e)
        {
            SetupLabels();
        }

        private void SetupLabels()
        {
            if (!_newInstance)
            {
                Text = LanguageLabels.GetLabel(54, "Edit an active report");
                btnInsert.Text = LanguageLabels.GetLabel(55, "Update");
                cboModel.Enabled = false;
            }
            else
            {
                cboModel.Enabled = true;
                Text = LanguageLabels.GetLabel(45, "Add a new report");
                btnInsert.Text = LanguageLabels.GetLabel(52, "Create a new report");
            }
            lblInternalRptName.Text = LanguageLabels.GetLabel(46, "Report name");
            lblTypeOfReort.Text = LanguageLabels.GetLabel(47, "Type of report");
            lblDate.Text = LanguageLabels.GetLabel(48, "Date");
            lblEntityID.Text = LanguageLabels.GetLabel(49, "Entity identifier");
            //lblNameOfUndertaking.Text = LanguageLabels.GetLabel(50, "Name of the undertaking");
            lblCurrency.Text = LanguageLabels.GetLabel(51, "Currency");
            
            btnCancel.Text = LanguageLabels.GetLabel(53, "Cancel");
        }

        private void dtPeriod_CloseUp(object sender, EventArgs e)
        {
            DateTimePicker timePicker = sender as DateTimePicker;

            if (timePicker != null)
            {
                DateTime newDate = timePicker.Value;
                System.Console.WriteLine(newDate.ToString());

                DateTime nearestQuarter = newDate.GetQuarter();
                System.Console.WriteLine(nearestQuarter.ToString());

                if (newDate != nearestQuarter)
                {
                    if (
                    MessageBox.Show("Selected date is not a quarter end date which is typically used for reporting. Are you sure it is correct?.", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        == System.Windows.Forms.DialogResult.No)
                    {
                        timePicker.Value = nearestQuarter;
                    }
                }
            }
        }
    }
}
