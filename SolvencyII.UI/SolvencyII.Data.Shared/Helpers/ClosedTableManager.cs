using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Data.Shared.Helpers
{
    /// <summary>
    /// Platform independant manager for closed templates.
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
                    Helpers.PopulateNPageCombos.PopulateComboUserControls(getData, MainCtl.GetUserComboBoxControls(), LanguageID);
                    Helpers.PopulateNPageCombos.PopulateComboUserControls(getData, MainCtl.GetDataComboBoxControls(), LanguageID);

                    sw.Stop();
                    Console.WriteLine("ComboUserControls {0}ms", sw.ElapsedMilliseconds);
                    sw.Reset();
                    sw.Start();
                    SetupDeletegateEvents();
                    sw.Stop();
                    Console.WriteLine("Delegate events {0}ms", sw.ElapsedMilliseconds);
                    sw.Reset();
                }
                sw.Start();
                PopulateUserDataControls(getData, comboUpdate, selectedItem, setupNPageFirstEntries);
                sw.Stop();
                Console.WriteLine("Controls {0}ms", sw.ElapsedMilliseconds);
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

        public bool Save(TreeItem selectedItem)
        {
            List<ISolvencyPageControl> nPageControls = MainCtl.GetPAGEnControls().ToList();
            ISolvencyUserControl userControl = (ISolvencyUserControl) MainCtl;
            IClosedRowRepository controlRepository = userControl.CtrlRepository;

            bool validData = controlRepository.IsValid();
            if (validData)
            {
                string errorText = "";
                if (controlRepository.SaveAll(userControl, nPageControls, selectedItem, InstanceID, out errorText))
                {
                    // Alert the user
                    ToolBar(selectedItem, LanguageLabels.GetLabel(31, "Saved Successfully."));
                    controlRepository.IsDirty = false;
                    return true;
                
                }
                if (errorText.StartsWith("A record conflict occurred")) throw new Exception(errorText);
                ToolBar(selectedItem, LanguageLabels.GetLabel(32, "Errors found."));
                MessageBox(string.Format("{0}", errorText));
            }
            else
            {
                // The data is invalid so alert the user.
                MessageBox("Some of the values for the form are not valid. Please update those controls in red.");
            }
            return false;
        }

        public bool Delete(TreeItem selectedItem, bool deleteFilingIndicator)
        {
            List<string> queries = new List<string>();

            ISolvencyUserControl userControl = (ISolvencyUserControl) MainCtl;
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
                        ToolBar(selectedItem,  "Deleted Successfully.");
                        return true;
                    }
                    // Alert the users
                    ToolBar(selectedItem,  "Error Deleting.");
                    MessageBox(string.Format("There was a problem deleting the details for:\n{0}\n{1}", selectedItem.TableLabel, errors));
                }
            }
            return false;
        }

        public bool Filed(TreeItem selectedItem)
        {
            List<string> queries = new List<string>();

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

        public bool IsFiled(TreeItem selectedItem)
        {
            using (GetSQLData getData = new GetSQLData())
            {
                return getData.GetFilingIndicatorFiled(InstanceID, selectedItem.FilingTemplateOrTableID);
            }
        }

        #endregion

        #region Worker methods

        private void SetupDeletegateEvents()
        {
            // ToDo: NAJ data repeater
            //ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;
            //IClosedRowRepository controlRepository = userControl.CtrlRepository;
            //controlRepository.DeleteControl += DeleteSingleControlFromControlRepository;
            //controlRepository.AskUserQuestion += AskUserQuestion;
            //controlRepository.AlertUser += ShowMessageBox;
        }

        private bool DeleteSingleControlFromControlRepository(List<long> pKeys)
        {
            ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;
            List<string> dataTables = userControl.DataTables;
            List<string> queries = new List<string>();
            bool success;
            PutSQLData putData = new PutSQLData();
            for (int i = 0; i < pKeys.Count; i++)
            {
                queries.Add(string.Format("Delete from [{0}] where PK_ID = {1}", dataTables[i], pKeys[i]));
            }
            success = putData.PutClosedTableDate(queries) != null;
            putData.Dispose();


            if (success)
            {
                // Alert the user
                ToolBar(LanguageLabels.GetLabel(30, "Deletion Confirmation."));
                // Now refresh the form
                GetSQLData getData = new GetSQLData();
                PopulateUserDataControls(getData, false, _selectedItem);
                getData.Dispose();
            }
            else
            {
                ToolBar(LanguageLabels.GetLabel(32, "Errors found."));
                MessageBox(string.Format("{1}\n{0}", putData.Errors, LanguageLabels.GetLabel(33, "There was a problem deleting the details:")));
            }
            return success;
        }

        private void PopulateUserDataControls(GetSQLData getData, bool comboUpdate, TreeItem selectedItem, bool setupNPageFirstEntries = false)
        {
            // We have all the controls gather the table names from there.
            // Check all the combos have been selected
            if (MainCtl.PageCombosCheck())
            {
                ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;
                // ToDo: NAJ Data repeater
                //IClosedRowRepository controlRepository = userControl.CtrlRepository;
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
                            // ToDo: NAJ Data repeater
                            //controlRepository.PopulateAll(userControl.DataTables[i], result, firstTable);
                            firstTable = false;
                        }
                    }
                    // ToDo: NAJ Data repeater
                    //controlRepository.IsDirty = false;
                    sw.Stop();
                    Console.WriteLine("> Populated Input Controls {0}", sw.ElapsedMilliseconds);

                }
            }
            else
            {
                MainCtl.ClearFormControls();
            }

        }

        private void PopulateNPageCombos(GetSQLData getData, bool setupNPageFirstEntries, IEnumerable<ISolvencyPageControl> combos, int languageID)
        {

            Type solvencyType = typeof(ISolvencyComboBox);
            List<ISolvencyComboBox> midStep = combos.Where(c => solvencyType.IsInstanceOfType(c)).Cast<ISolvencyComboBox>().ToList();

            Dictionary<string, string> startingEntries = midStep.Distinct().ToDictionary(c => c.ColName, c => "");
            if (setupNPageFirstEntries)
                getData.GetnPageStartingData(MainCtl.InstanceID, MainCtl.GetDataTables(), ref startingEntries);


            Helpers.PopulateNPageCombos.PopulateCombosNPage(getData, MainCtl.InstanceID, MainCtl.GetDataTables(), languageID, midStep, startingEntries, CombosOnSelectedIndexChanged, ComboBoxOnDropDown, CombosOnLostFocus, null);

        }

        private void PopulateLabels(GetSQLData getData)
        {
            ISolvencyUserControl userControl = (ISolvencyUserControl)MainCtl;
            IClosedRowRepository controlRepository = userControl.CtrlRepository;

            string[] tableVIDs = userControl.GroupTableIDs.Split('|');
            List<mAxisOrdinate> labelText = GatherLabelsForUserControl(tableVIDs, getData);
            if (labelText != null && labelText.Count > 0)
            {
                MainCtl.PopulateLabels(labelText);
                // ToDo: NAJ data repeater
                // controlRepository.PopulateLabels(labelText);
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

       
    }
}
