using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Entities;

namespace SolvencyII.UI.Shared.Config
{
    public static class TreeViewConfig
    {
        #region Misc

        private static bool runOnce;

        public delegate void ItemClicked(TreeItem item);

        public static ItemClicked itemClicked;
        private static IEnumerable<TreeItem> _queryTable;
        private static IEnumerable<TreeItem> _tableGroups;
        private static IEnumerable<TreeItem> _modules;
        public static int InstanceId { get; set; }




        public static void CleanUp(TreeListView treeListView1)
        {
            treeListView1.Roots = null;

        }

        public static void Setup(TreeListView treeListView1, ItemClicked updateMainPane, GetSQLiteData getData, int instanceID)
        {
            InstanceId = instanceID;
            GetDataSecond(getData, InstanceId);

            treeListView1.Enabled = true;

            if (!runOnce)
            {
                itemClicked = updateMainPane;
                treeListView1.CellToolTipShowing += treeListView1_OnCellToolTipShowing;
                treeListView1.CellClick += treeListView1_CellClick;
                treeListView1.CanExpandGetter = OnCanExpandGetterSecond;
                treeListView1.ChildrenGetter = OnChildrenGetterSecond;

                TreeListView.TreeRenderer renderer = treeListView1.TreeColumnRenderer;
                renderer.LinePen = new Pen(Color.Firebrick, 0.5f) {DashStyle = DashStyle.Dot};

                OLVColumn rootColum = new OLVColumn
                                          {
                                              Text = "Templates",
                                              AspectName = "ParentTableGroupID",
                                              Width = 450
                                          };
                rootColum.AspectGetter = rowObject => ((TreeItem) rowObject).DisplayText;

                treeListView1.Columns.Clear();
                treeListView1.Columns.Add(rootColum);


                runOnce = true;
            }

            treeListView1.Roots = _modules;
        }

        private static void treeListView1_OnCellToolTipShowing(object sender, ToolTipShowingEventArgs toolTipShowingEventArgs)
        {
            toolTipShowingEventArgs.Text = ((TreeItem) toolTipShowingEventArgs.Model).DisplayText;
        }

        private static void treeListView1_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.Model != null)
            {
                TreeItem selectedItem = (TreeItem) e.Model;
                if (selectedItem.TableID != 0 || !string.IsNullOrEmpty(selectedItem.GroupTableIDs))
                {
                    if (itemClicked != null) itemClicked(selectedItem);
                }
            }
        }

        #endregion

        #region Working first version (updated needed for EBA)

        private static bool OnCanExpandGetter(object model)
        {
            // The expander gets shown when the model has a parent id with some children
            // also when there are a number of tables with the same tablegroupId

            TreeItem item = (TreeItem) model;

            switch (item.ItemType)
            {
                case 1:
                    // Module
                    return _tableGroups.Any(g => g.ModuleID == item.ModuleID);
                case 2:
                    // Table Group

                    // Nested Table Groups never have children
                    if (item.Level >= 2 && item.HasLocationRange) return false;

                    // Tables
                    //result = _queryTable.Where(t => t.ParentTableGroupID == item.TableGroupID).Any();
                    bool result = _queryTable.Any(t => t.TableGroupID == item.TableGroupID && t.HasLocationRange);
                    // Table Groups
                    result = result || _tableGroups.Any(g => g.ParentTableGroupID == item.TableGroupID);
                    return result;
                case 3:
                    // Table
                    return false;
            }

            return false;

        }

        private static IEnumerable OnChildrenGetter(object model)
        {
            TreeItem item = (TreeItem) model;

            switch (item.ItemType)
            {
                case 1:
                    // Module - Get all apprpriate Table Groups
                    return _tableGroups.Where(g => g.ModuleID == item.ModuleID && g.Level == 1);
                case 2:
                    // Table Group - Get all children Table groups and tables
                    List<TreeItem> results = new List<TreeItem>();
                    // Get the tables
                    IEnumerable<TreeItem> temp = _queryTable.Where(t => t.TableGroupID == item.TableGroupID);
                    results.AddRange(temp);

                    // Get the groups
                    temp = _tableGroups.Where(g => g.ParentTableGroupID == item.TableGroupID);
                    results.AddRange(temp);
                    return results.Distinct();
                case 3:
                    // Table - there are no children from tables.
                    return null;
            }


            return null;
        }

        private static void GetData(GetSQLiteData data, int instanceID = 0)
        {
            _modules = data.GetTreeViewModules(instanceID);
            _tableGroups = data.GetTreeViewTableGroups();
            _queryTable = data.GetTreeViewNodes();
        }

        #endregion


        private static bool OnCanExpandGetterSecond(object model)
        {
            // The expander gets shown when the model has a parent id with some children
            // also when there are a number of tables with the same tablegroupId

            TreeItem item = (TreeItem)model;
            bool result;
            switch (item.ItemType)
            {
                case 1:
                    // Module
                    GetSQLiteData getData = new GetSQLiteData();
                    result = getData.GetDoesModuleHaveChildren(item.ModuleID, InstanceId);
                    getData.Dispose();
                    return result;
                case 2:
                    // Table Group

                    // Nested Table Groups never have children
                    if (item.Level >= 2 && item.HasLocationRange) return false;

                    // Tables
                    //result = _queryTable.Where(t => t.ParentTableGroupID == item.TableGroupID).Any();
                    result = _queryTable.Any(t => t.TableGroupID == item.TableGroupID && t.HasLocationRange);
                    // Table Groups
                    result = result || _tableGroups.Any(g => g.ParentTableGroupID == item.TableGroupID);
                    return result;
                case 3:
                    // Table
                    return false;
            }

            return false;

        }

        private static IEnumerable OnChildrenGetterSecond(object model)
        {
            TreeItem item = (TreeItem)model;
            IEnumerable result;
            switch (item.ItemType)
            {
                case 1:
                    // Module - Get all apprpriate Table Groups
                    GetSQLiteData getData = new GetSQLiteData();
                    result = getData.GetModulesChildren(item.ModuleID);
                    getData.Dispose();
                    return result;
                    // return _tableGroups.Where(g => g.ModuleID == item.ModuleID && g.Level == 1);
                case 2:
                    // Table Group - Get all children Table groups and tables
                    List<TreeItem> results = new List<TreeItem>();
                    // Get the tables
                    IEnumerable<TreeItem> temp = _queryTable.Where(t => t.TableGroupID == item.TableGroupID);
                    results.AddRange(temp);

                    // Get the groups
                    temp = _tableGroups.Where(g => g.ParentTableGroupID == item.TableGroupID);
                    results.AddRange(temp);
                    return results.Distinct();
                case 3:
                    // Table - there are no children from tables.
                    return null;
            }


            return null;
        }

        private static void GetDataSecond(GetSQLiteData data, int instanceID = 0)
        {
            _modules = data.GetTreeViewModules(instanceID);
            _tableGroups = data.GetTreeViewTableGroups();
            _queryTable = data.GetTreeViewNodes();
        }
    }
}
