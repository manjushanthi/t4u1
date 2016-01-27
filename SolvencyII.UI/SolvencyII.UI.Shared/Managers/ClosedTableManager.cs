using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.UserControls;

namespace SolvencyII.Data.Shared.Helpers
{
    /// <summary>
    /// Handler for closed templates; setup, delegates, events. 
    /// </summary>
    public class ClosedTableManager
    {

        #region Declarations

        public IUserControlBase MainCtl;
        public int LanguageID = 0;
        public long InstanceID = 0;
        public GenericDelegates.StringResponse ShowMessageBox;
        public GenericDelegates.StringResponse ShowToolBar;
        private GenericDelegates.BoolResultQuestion AskUserQuestion;

        private event EventHandler CombosOnSelectedIndexChanged;
        private event EventHandler CombosOnLostFocus;
        private event EventHandler ComboBoxOnDropDown;
        private TreeItem _selectedItem;

        public ClosedTableManager(IUserControlBase mainControl, int languageID, long instanceID)
        {
            MainCtl = mainControl;
            LanguageID = languageID;
            InstanceID = instanceID;
        }

        private void MessageBox(string msg)
        {
            if (ShowMessageBox != null) ShowMessageBox(msg);
        }
        private void ToolBar(string msg)
        {
            if (ShowToolBar != null)
                ShowToolBar(msg);
        }
        private void ToolBar(TreeItem selectedTreeItem, string msg)
        {
            if (selectedTreeItem == null)
                return;

            if (msg == null)
                return;

            ToolBar(selectedTreeItem.TemplateVariant + " → " + selectedTreeItem.DisplayText + " - " + msg);

        }

        #endregion

        #region Public methods

        public void Selected(EventHandler combosOnSelectedIndexChanged, EventHandler combosOnLostFocus, bool comboUpdate, EventHandler comboBoxOnDropDown, TreeItem selectedItem, GenericDelegates.BoolResultQuestion askUserQuestion, bool setupNPageFirstEntries, GenericDelegates.StringResponse alertMessageBox)
        {
            MainCtl.InstanceID = InstanceID;
            CombosOnSelectedIndexChanged = combosOnSelectedIndexChanged;
            CombosOnLostFocus = combosOnLostFocus;
            ComboBoxOnDropDown = comboBoxOnDropDown;
            _selectedItem = selectedItem;
            AskUserQuestion = askUserQuestion;
            ShowMessageBox = alertMessageBox;

            using (GetSQLData getData = new GetSQLData())
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                PopulateLabels(getData);
                sw.Stop();
                Console.WriteLine("Labels {0}ms", sw.ElapsedMilliseconds);
                sw.Reset();

                if (!comboUpdate)
                {
                    sw.Start();
                    // Populate the list items for the combo boxes contained on the user control
                    UI.Shared.Managers.PopulateNPageCombos.PopulateComboUserControls(getData, MainCtl.GetUserComboBoxControls(), LanguageID);
                    UI.Shared.Managers.PopulateNPageCombos.PopulateComboUserControls(getData, MainCtl.GetDataComboBoxControls(), LanguageID);

                    sw.Stop();
                    Console.WriteLine("ComboUserControls {0}ms", sw.ElapsedMilliseconds);
                    sw.Reset();
                }
                if (MainCtl as UserControlBase != null)
                {
                    sw.Start();
                    PopulateDataForDataRepeater(getData, comboUpdate, selectedItem, _selectedItem.IsTyped, setupNPageFirstEntries);
                    sw.Stop();
                    Console.WriteLine("Controls {0}ms", sw.ElapsedMilliseconds);
                }
                else
                {
                    // Special Case
                    sw.Start();
                    PopulateUserDataControls(getData, comboUpdate, selectedItem, setupNPageFirstEntries);
                    sw.Stop();
                    Console.WriteLine("Controls {0}ms", sw.ElapsedMilliseconds);
                }

                sw.Reset();

                // If the refresh is raised from a comboUpdate keep the items as they are.
                if (!comboUpdate)
                {
                    sw.Start();
                    PopulateNPageCombos(getData, setupNPageFirstEntries, MainCtl.GetPAGEnControls(), LanguageID);
                    sw.Stop();
                    Console.WriteLine("Populate Combo Controls {0}ms", sw.ElapsedMilliseconds);
                    sw.Reset();
                }

            }

            if (setupNPageFirstEntries)
            {
                // Refresh the form with the correct data;
                combosOnSelectedIndexChanged(this, new EventArgs());
            }
        }

        public void UpdateLabels(TreeItem treeItem)
        {
            using (GetSQLData getData = new GetSQLData())
            {
                UpdateLabels(treeItem, getData);
            }
        }

        public void UpdateLabels(TreeItem treeItem, GetSQLData getData)
        {
            List<mAxisOrdinate> labelText = GatherLabelsForUserControl(treeItem.GroupTableIDs.Split('|'), getData);

            if (labelText != null && labelText.Count > 0)
            {
                MainCtl.PopulateLabels(labelText);
            }
        }

        /// <summary>
        /// Build the nPage details and passed them through to the controls with the appropriate queries
        /// </summary>
        /// <param name="selectedItem"></param>
        /// <returns></returns>
        public bool Save(TreeItem selectedItem)
        {
            List<ISolvencyPageControl> nPageControls = MainCtl.GetPAGEnControls().ToList();
            ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;
            string errorText = "";
            if (userControl.IsValid())
            {
                Cursor.Current = Cursors.WaitCursor;

                if (MainCtl as UserControlBase != null)
                {

                    if (userControl.UpdateData(out errorText, nPageControls))
                    {
                        // Save the Filing Indicator
                        Filed_SetTrue(selectedItem);

                        ToolBar(selectedItem, LanguageLabels.GetLabel(31, "Saved Successfully."));

                        // Refresh:
                        if (!selectedItem.IsTyped)
                        {
                            // Now update the control with the newest data.
                            for (int i = 0; i < userControl.DataTables.Count; i++)
                            {
                                string query = MainCtl.BuildSQLQuery_Select(userControl.DataTables[i], true);
                                userControl.SetupData(i, query);
                            }
                        }
                        else
                        {
                            // Single record with a primary key - no need to refresh.
                        }
                        userControl.RefreshDR();
                        return true;
                    }
                }
                else
                {
                    // Special Cases
                    IClosedRowRepository controlRepository = ((IControlContainsRepository)MainCtl).CtrlRepository;

                    bool validData = controlRepository.IsValid();
                    if (validData)
                    {
                        errorText = "";
                        if (controlRepository.SaveAll(userControl, nPageControls, selectedItem, InstanceID, out errorText))
                        {
                            // Alert the user
                            ToolBar(selectedItem, LanguageLabels.GetLabel(31, "Saved Successfully."));
                            controlRepository.IsDirty = false;
                            return true;

                        }
                    }
                    else
                    {
                        // The data is invalid so alert the user.
                        MessageBox("Some of the values for the form are not valid. Please update those controls in red.");
                        return false;
                    }

                }
                Cursor.Current = Cursors.Default;
                if (errorText.StartsWith("A record conflict occurred")) throw new Exception(errorText);
                ToolBar(selectedItem, LanguageLabels.GetLabel(32, "Errors found."));
                MessageBox(string.Format("{0}", errorText));
            }
            else
            {
                // The data is invalid so alert the user.
                MessageBox("Some of the values for the form are not valid. Please update those controls in red.\r\nCheck the PAGE values - duplicates are not permitted.");
            }

            return false;
        }



        public bool Delete(TreeItem selectedItem, bool deleteFilingIndicator)
        {
            List<string> queries = new List<string>();

            ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;
            if (userControl != null)
            {

                for (int i = 0; i < userControl.DataTables.Count; i++)
                {
                    queries.Add(MainCtl.BuildSQLQuery_Delete(userControl.DataTables[i], selectedItem.SingleZOrdinateID != 0));
                }

                if (queries.Count > 0)
                {
                    PutSQLData putData = new PutSQLData();
                    bool result = putData.DeleteClosedTableData(queries);
                    // Update dFilingIndicator

                    if (deleteFilingIndicator)
                        putData.DeleteFilingIndicator(InstanceID, selectedItem.FilingTemplateOrTableID, null);
                    string errors = putData.Errors;
                    putData.Dispose();

                    if (result)
                    {
                        // Alert the user
                        ToolBar(selectedItem, "Deleted Successfully.");
                        return true;
                    }
                    // Alert the users
                    ToolBar(selectedItem, "Error Deleting.");
                    MessageBox(string.Format("There was a problem deleting the details for:\n{0}\n{1}", selectedItem.TableLabel, errors));
                }
            }
            return false;
        }

        public bool Filed_Toggle(TreeItem selectedItem)
        {

            ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;
            if (userControl != null)
            {
                using (PutSQLData putData = new PutSQLData())
                {
                    putData.ToggleFileIndicatorFiled(InstanceID, selectedItem.FilingTemplateOrTableID);
                }

                // Alert the user
                ToolBar(selectedItem, "Updated Successfully.");
                return true;
            }
            return false;
        }

        private void Filed_SetTrue(TreeItem selectedItem)
        {
            ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;
            if (userControl != null)
            {
                using (PutSQLData putData = new PutSQLData())
                {
                    putData.SaveFilingIndicator(InstanceID, selectedItem.FilingTemplateOrTableID);
                }
            }
        }

        public bool IsFiled(TreeItem selectedItem)
        {
            using (GetSQLData getData = new GetSQLData())
            {
                return getData.GetFilingIndicatorFiled(InstanceID, selectedItem.FilingTemplateOrTableID);
            }
        }

        #endregion

        #region Worker methods


        /// <summary>
        /// Create the list of data objects needed for the data repeater binding.
        /// </summary>
        /// <param name="getData"></param>
        /// <param name="comboUpdate"></param>
        /// <param name="selectedItem"></param>
        /// <param name="isTyped"></param>
        /// <param name="setupNPageFirstEntries"></param>
        private void PopulateDataForDataRepeater(GetSQLData getData, bool comboUpdate, TreeItem selectedItem, bool isTyped, bool setupNPageFirstEntries)
        {
            // We have all the controls gather the table names from there.
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            ISolvencyUserControl userControl = (ISolvencyUserControl) MainCtl;
            if (selectedItem.SingleZOrdinateID != 0)
            {
                List<FormDataPage> controlSetupPAGEn = getData.GetFixedDimensionPageData(userControl.GroupTableIDs.Split('|').ToList(), selectedItem.SingleZOrdinateID);
                MainCtl.PopulatePAGEnControls(controlSetupPAGEn);
            }

            sw.Stop();
            Console.WriteLine("PopulateDataForDataRepeater 01 PopulatePAGEnControls {0}ms", sw.ElapsedMilliseconds);
            sw.Reset();


            if (!isTyped && !setupNPageFirstEntries)
            {
                sw.Start();
                for (int i = 0; i < userControl.DataTables.Count; i++)
                {
                    string query = MainCtl.BuildSQLQuery_Select(userControl.DataTables[i], comboUpdate);
                    userControl.SetupData(i, query);
                }
                sw.Stop();
                Console.WriteLine("PopulateDataForDataRepeater 02 SetupData {0}ms", sw.ElapsedMilliseconds);
                sw.Reset();

                sw.Start();
                userControl.RefreshDR();
                sw.Stop();
                Console.WriteLine("PopulateDataForDataRepeater 03 Refresh Data Repeater {0}ms", sw.ElapsedMilliseconds);
                sw.Reset();


            }
            MainCtl.IsDirty = false;

        }

        
        private void PopulateNPageCombos(GetSQLData getData, bool setupNPageFirstEntries, IEnumerable<ISolvencyPageControl> combos, int languageID)
        {

            Type solvencyType = typeof(ISolvencyComboBox);
            List<ISolvencyComboBox> midStep = combos.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyComboBox>().ToList();

            Dictionary<string, string> startingEntries = midStep.Distinct().ToDictionary(c => c.ColName, c => "");
            if (setupNPageFirstEntries)
                getData.GetnPageStartingData(MainCtl.InstanceID, MainCtl.GetDataTables(), ref startingEntries);


            UI.Shared.Managers.PopulateNPageCombos.PopulateCombosNPage(getData, MainCtl.InstanceID, MainCtl.GetDataTables(), languageID, midStep, startingEntries, CombosOnSelectedIndexChanged, ComboBoxOnDropDown, CombosOnLostFocus, null);

        }

        private void PopulateLabels(GetSQLData getData)
        {
            ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;

            string[] tableVIDs = userControl.GroupTableIDs.Split('|');
            List<mAxisOrdinate> labelText = GatherLabelsForUserControl(tableVIDs, getData);
            if (labelText != null && labelText.Count > 0)
            {
                MainCtl.PopulateLabels(labelText);
            }
        }

        private List<mAxisOrdinate> GatherLabelsForUserControl(string[] tableVIDs, GetSQLData getData)
        {
            List<mAxisOrdinate> labelText = getData.GetTableLabelText(tableVIDs.ToList(), LanguageID).ToList();
            // Does the database contain labels?
            if (labelText.Count == 0) labelText = getData.GetTableLabelText(tableVIDs.ToList()).ToList();
            if (labelText.Count == 0) labelText = getData.GetTableLabelTextNoTranslations(tableVIDs.ToList()).ToList();
            return labelText;
        }

        #endregion

        #region Special Cases

        private void PopulateUserDataControls(GetSQLData getData, bool comboUpdate, TreeItem selectedItem, bool setupNPageFirstEntries = false)
        {
            // We have all the controls gather the table names from there.
            // Check all the combos have been selected
            if (MainCtl.PageCombosCheck())
            {
                ISolvencyUserControl userControl = (ISolvencyUserControl) MainCtl;
                IClosedRowRepository controlRepository = ((IControlContainsRepository) MainCtl).CtrlRepository;
                if (userControl != null)
                {
                    Stopwatch sw = new Stopwatch();
                    sw.Start();

                    if (selectedItem.SingleZOrdinateID != 0)
                    {
                        List<FormDataPage> controlSetupPAGEn = getData.GetFixedDimensionPageData(userControl.GroupTableIDs.Split('|').ToList(), selectedItem.SingleZOrdinateID);
                        MainCtl.PopulatePAGEnControls(controlSetupPAGEn);
                    }

                    sw.Stop();
                    Console.WriteLine("> Setup PAGEn Controls {0}", sw.ElapsedMilliseconds);
                    sw.Reset();
                    sw.Start();

                    if (!setupNPageFirstEntries)
                    {
                        bool firstTable = true;
                        for (int i = 0; i < userControl.DataTables.Count; i++)
                        {
                            string query = MainCtl.BuildSQLQuery_Select(userControl.DataTables[i], comboUpdate);
                            // If typed we only want a single row
                            List<object> result = null;
                            if (!selectedItem.IsTyped)
                            {
                                result = getData.GetClosedTableInfo(userControl.DataTypes[i], query);
                            }
                            controlRepository.PopulateAll(userControl.DataTables[i], result, firstTable);
                            firstTable = false;
                        }
                    }
                    controlRepository.IsDirty = false;
                    sw.Stop();
                    Console.WriteLine("> Populated Input Controls {0}", sw.ElapsedMilliseconds);

                }
            }
            else
            {
                //Debugger.Break(); 
                // Removed line below but might need to be put back in
                //MainCtl.ClearFormControls();
            }

        }

        #endregion



    }
}
