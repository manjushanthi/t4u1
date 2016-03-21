using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using SolvencyII.Data.Entities;
using SolvencyII.Data.SQL;
using SolvencyII.Data.SQLite;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Data.Shared.Extensions;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.Conversions;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;
using System.Reflection;

namespace SolvencyII.Data.Shared
{
    /// <summary>
    /// Business side of data tier where queries are built to be processed by ISolvencyData providers.
    /// This is used exclusively for gathering information.
    /// </summary>
    public class GetSQLData : IDisposable
    {
        #region Declarations, Constuctors and Dispose

        private ISolvencyData _conn;
        // DB specific constants
        private string CONCATENATION_CHARACTER = "||";
        private string SUBSTRING_FUNCTION = "SUBSTR";

        public GetSQLData()
            : this(StaticSettings.ConnectionString)
        { }

        // Instantiate the ISolvencyData provider and set DB specific constants
        public GetSQLData(string connectionString)
        {
            switch (StaticSettings.DataTier)
            {
                case eDataTier.SqLite:
                    _conn = new SQLiteConnection(connectionString);
                    break;
                case eDataTier.SqlServer:
                    _conn = new DataConnection(connectionString);
                    CONCATENATION_CHARACTER = "+";
                    SUBSTRING_FUNCTION = "SubString";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public bool ConnectionCheck()
        {
            try
            {
                _conn.ExecuteScalar<int>("Select Count(*) from dInstance");
                return true;
            }
            catch (SQLiteException ex)
            {
                if (ex.Message != "no such table: dInstance")
                    throw;
                return false;
            }

        }

        #endregion

        #region IDisposable implementation

        public void Dispose(bool disposing)
        {
            _conn.Close();
            _conn.Dispose();
        }

        public void Dispose()
        {
            _conn.Close();
            _conn.Dispose();
        }

        

        #endregion

        /// <summary>
        /// Data retieval and management for the tree view - found both in t4u and user control generator.
        /// </summary>
        #region TreeBranch Functions

        // Get the data from the db
        private List<TreeViewData> GetTreeData(long instanceID = 0)
        {
            string query;
            if (instanceID == 0)
                query = "Select distinct * from vwGetTreeData ORDER BY ModuleID, BusinessOrder, TemplateOrder, TemplateOrder2 ";
            else
                query = string.Format("SELECT distinct i.InstanceID, td.* FROM vwGetTreeData td left outer join dInstance i on (i.ModuleID = td.ModuleID) Where InstanceID = {0} ORDER BY BusinessOrder, TemplateOrder, TemplateOrder2 ", instanceID);


            List<TreeViewData> response = _conn.Query<TreeViewData>(query);
            return response;
        }

        public List<TreeItem> GetTreeBranches(long instanceID = 0)
        {
            TreeBranch result = GetTree(instanceID);
            return FlattenTree(result);
        }

        public TreeBranch GetTree(long instanceID = 0)
        {
            List<TreeViewData> result = GetTreeData(instanceID);
            TreeBranch results = TreeViewDataProcess(result);
            return results;
        }

        public TreeBranch GetNonTreeNotes()
        {
            List<TreeBranch> results = _conn.Query<TreeBranch>("SELECT 'S2' as FrameworkCode, t.TableCode, t.TableID, t.TableID as GroupTableIDs, 0 as SingleZOrdinateID, 0 as HasLocationRange, 'Not needed' as DisplayText FROM mTable t; ");
            TreeBranch result = new TreeBranch();
            result.SubBranches.AddRange(results);
            return result;
        }

        private List<TreeItem> FlattenTree(TreeBranch data)
        {
            List<TreeItem> result = new List<TreeItem>();
            if (data != null)
            {
                data.BranchID = result.Count + 1;
                data.HasBranches = data.SubBranches.Count > 0;
                result.Add((TreeItem)(data));
                int level = 0;
                foreach (TreeBranch subBranch in data.SubBranches)
                {
                    ProcessLevel(subBranch, result, ref level, result.Count);
                }
            }
            return result;
        }

        private void ProcessLevel(TreeBranch data, List<TreeItem> result, ref int level, int parentID)
        {
            level++;
            data.ItemType = level;
            int branchId = result.Count + 1;
            data.BranchID = branchId;
            data.ParentBranchID = parentID;
            data.HasBranches = data.SubBranches.Count > 0;
            result.Add((TreeItem)data);
            foreach (TreeBranch treeBranch in data.SubBranches)
            {
                ProcessLevel(treeBranch, result, ref level, branchId);
            }
            level--;
        }

        /// <summary>
        /// The branches are formed into their hierarchical structure.
        /// This includes the leaves and selectable items.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private TreeBranch TreeViewDataProcess(List<TreeViewData> data)
        {

            TreeBranch result = new TreeBranch();
            result.DisplayText = "root";
            if (data != null)
            {
                // Modules

                #region Modules

                int moduleID = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    TreeViewData viewData = data[i];

                    if (moduleID != viewData.ModuleID)
                    {
                        // We have a new framework 
                        TreeBranch moduleTreeBranch = new TreeBranch
                        {
                            ModuleID = viewData.ModuleID,
                            DisplayText = viewData.ModuleLabel,
                            FrameworkCode = viewData.FrameworkCode,
                            TaxonomyCode = viewData.TaxonomyCode,
                            Version = viewData.Version
                        };
                        result.SubBranches.Add(moduleTreeBranch);
                        moduleID = viewData.ModuleID;
                    }
                }

                #endregion

                #region Table Groups

                int templateOrTableID = 0;
                int tableGroupID2 = 0;

                // Table Groups
                foreach (TreeBranch subBranch in result.SubBranches)
                {
                    // Iterate through the modules

                    // We have a module here so find the groupings.
                    for (int i = 0; i < data.Count; i++)
                    {
                        TreeViewData viewData = data[i];
                        if (viewData.ModuleID == subBranch.ModuleID)
                        {
                            if (templateOrTableID != viewData.TemplateOrTableID)
                            {
                                // We have a grouped entry
                                TreeBranch child = new TreeBranch();
                                child.DisplayText = string.Format("{0} {1}", viewData.TemplateVariant, viewData.TemplateVariantLabel);
                                child.LocationRange = viewData.BusinessTableLocationRange;
                                child.Merged = viewData.WhatToShow == "Show AnnotateTable";
                                //if (viewData.WhatToShow == "Show AnnotateTable")
                                //{
                                //    string debug = "stop now";
                                //}

                                child.TableGroupID = viewData.TemplateOrTableID;
                                child.TableCode = viewData.TemplateVariant;

                                child.TableGroupID2 = viewData.CalcTemplateOrTableID;
                                child.TableGroupCode2 = viewData.CalcTableCode;

                                child.TemplateOrTableID = viewData.TemplateOrTableID;
                                child.FilingTemplateOrTableID = viewData.FilingTemplateOrTableID;

                                child.FrameworkCode = viewData.FrameworkCode;
                                child.TaxonomyCode = viewData.TaxonomyCode;
                                child.Version = viewData.Version;

                                subBranch.SubBranches.Add(child);
                                templateOrTableID = viewData.TemplateOrTableID;
                            }
                            //else
                            //{
                            //    if (viewData.TableGroupCode == null)
                            //    {
                            //        // We have a normal entry
                            //        if (tableGroupID2 != viewData.TableGroupID2)
                            //        {
                            //            TreeBranch child = new TreeBranch();
                            //            child.DisplayText = viewData.TableGroupLabel2;
                            //            //child.TableGroupCode = viewData.TableGroupCode2;
                            //            //child.TableGroupID = viewData.TableGroupID;
                            //            child.TableGroupID2 = viewData.TableGroupID2;
                            //            //child.LocationRange = viewData.LocationRange;
                            //            child.FrameworkCode = viewData.FrameworkCode;
                            //            subBranch.SubBranches.Add(child);
                            //            tableGroupID2 = viewData.TableGroupID2;
                            //        }
                            //    }
                            //}
                        }
                    }
                }

                #endregion

                //BRAG - order of TemplateVariant for a Module from mTemplateOrTable 
                foreach (var branch in result.SubBranches)
                {
                    branch.SubBranches = branch.SubBranches.OrderBy(x => x.TemplateOrTableID).ToList();
                }

                #region Tables with their GroupIDs & single Z dimension

                // Tables and their groupIDs:
                // - Z dimension addition...
                Dictionary<int, int> zDimTables = GetZdimTreeTables();

                foreach (TreeBranch branch in result.SubBranches)
                {
                    // Iterate through the modules
                    foreach (TreeBranch subBranch in branch.SubBranches)
                    {
                        // Interate through the groups
                        // Populate with the data;
                        List<TreeViewData> rec;
                        
                        if (!subBranch.Merged)
                            rec = data.Where(d => d.ModuleID == branch.ModuleID && ((d.TemplateOrTableID == subBranch.TableGroupID))).ToList();
                        else
                            rec = new List<TreeViewData>();


                        foreach (TreeViewData viewData in rec)
                        {
                            // bool foundZ = zDimTables.Select(t => t.Key == int.Parse(viewData.GroupTableIDs)).FirstOrDefault();
                            if (!zDimTables.ContainsKey(viewData.TableID))
                            {
                                // Standard entry
                                TreeBranch leave = new TreeBranch();
                                //BRAG Display table codes in the tree view
                                leave.DisplayText = String.Format("{0} {1}",viewData.AnnotatedTable.Split('.').Last(), viewData.CalcTableLabel);
                                leave.IsTyped = viewData.IsTyped;
                                leave.GroupTableIDs = viewData.TableID.ToString();
                                leave.HasLocationRange = !string.IsNullOrEmpty(rec[0].BusinessTableLocationRange);
                                leave.Merged = viewData.WhatToShow == "Show AnnotateTable";
                                //leave.HasLocationRange = (rec[0].BusinessTableLocationRange == null || rec[0].BusinessTableLocationRange.Length > 0);
                                leave.FrameworkCode = viewData.FrameworkCode;
                                leave.TaxonomyCode = viewData.TaxonomyCode;
                                leave.Version = viewData.Version;
                                leave.TableCode = viewData.TableCode;
                                leave.TemplateVariant = viewData.TemplateVariant;
                                leave.TemplateOrTableID = viewData.TemplateOrTableID;
                                leave.FilingTemplateOrTableID = viewData.FilingTemplateOrTableID;
                                subBranch.SubBranches.Add(leave);
                            }
                            else
                            {
                                // Single ZAxis entry...
                                TreeBranch leave = new TreeBranch();
                                leave.DisplayText = viewData.CalcTableLabel;
                                leave.FrameworkCode = viewData.FrameworkCode;
                                leave.TaxonomyCode = viewData.TaxonomyCode;
                                leave.Version = viewData.Version;
                                leave.TableCode = viewData.TableCode;
                                leave.TemplateVariant = viewData.TemplateVariant;
                                leave.TemplateOrTableID = viewData.TemplateOrTableID;
                                leave.FilingTemplateOrTableID = viewData.FilingTemplateOrTableID;
                                int tableVID = viewData.TableID;
                                var workingRes = TreeViewDataChildrenWhoAreZDimTables(tableVID, zDimTables[tableVID], viewData.ModuleID);
                                leave.SubBranches.AddRange(workingRes);
                                subBranch.SubBranches.Add(leave);
                            }
                        }
                        if (!rec.Any())
                        {

                            // We have a number of tables grouped.
                            List<TreeViewData> recOuter = data.Where(d => d.ModuleID == branch.ModuleID && (d.TemplateOrTableID == subBranch.TableGroupID)).ToList();
                            tableGroupID2 = 0;
                            foreach (TreeViewData viewDataOuter in recOuter)
                            {
                                if (tableGroupID2 != viewDataOuter.CalcTemplateOrTableID)
                                {
                                    rec = data.Where(d => d.ModuleID == branch.ModuleID && (d.CalcTemplateOrTableID == viewDataOuter.CalcTemplateOrTableID)).ToList();
                                    if (rec.Any())
                                    {
                                        TreeBranch leave = new TreeBranch();
                                        leave.DisplayText = rec[0].CalcTableLabel;
                                        leave.IsTyped = rec[0].IsTyped;
                                        leave.HasLocationRange = !string.IsNullOrEmpty(rec[0].BusinessTableLocationRange);
                                        leave.Merged = rec[0].WhatToShow == "Show AnnotateTable";
                                        leave.GroupTableIDs = rec.Select(r => r.TableID.ToString()).MyJoin("|");
                                        leave.FrameworkCode = rec[0].FrameworkCode;
                                        leave.TemplateVariant = rec[0].TemplateVariant;
                                        leave.TemplateOrTableID = rec[0].TemplateOrTableID;
                                        leave.FilingTemplateOrTableID = rec[0].FilingTemplateOrTableID;
                                        leave.TableCode = rec[0].TableCode;
                                        subBranch.SubBranches.Add(leave);
                                    }
                                    tableGroupID2 = viewDataOuter.CalcTemplateOrTableID;
                                }
                            }
                        }

                    }
                }

                #endregion

            }
            result.HasBranches = result.SubBranches.Count() > 0;
            return result;
        }

        private IEnumerable<TreeBranch> TreeViewDataChildrenWhoAreZDimTables(int tableVID, int zAxisID, int moduleID)
        {

            string query = string.Format("SELECT * FROM [vwGetTreeDataZDimChildren] where TableID = {0} and AxisID = {1} and ModuleID = {2} Order by  ModuleID, BusinessOrder, TemplateOrder, TemplateOrder2, AxisOrdinateOrder", tableVID, zAxisID, moduleID);

            // Main results
            var result = _conn.Query<TreeItem>(query);

            if (result != null)
            {
                TreeBit<TreeBranch> manager = new TreeBit<TreeBranch>();
                return from g in result
                       select manager.TreeBitFromTreeItem(g, true);
            }

            return null;

        }

        public Dictionary<int, int> GetZdimTreeTables()
        {
            var res = _conn.Query<TwoInts>("SELECT * from vwGetZDimTreeTables ");
            if (res == null) return new Dictionary<int, int>();
            return res.Distinct().ToDictionary(k => k.TableID, v => v.AxisID);
        }



        #endregion

        #region Module methods

        public IEnumerable<TreeItem> GetTreeViewModules(long instanceID)
        {
            StringBuilder sb = new StringBuilder();
            //Changed to include the version number
            sb.Append("select m.ModuleLabel || ' - ' || t.Version as 'ModuleLabel',m.ModuleID from mModule m ");
            sb.Append("inner join mTaxonomy t on t.TaxonomyID = m.TaxonomyID  ");
            //BRAG
            sb.Append("order by m.ModuleID asc");

            if (instanceID != 0)
            {
                sb.Append("Inner join dInstance i on (i.ModuleID = m.ModuleID) ");
                sb.Append(string.Format("Where i.InstanceID = {0} ", instanceID));
            }

            return from g in _conn.Query<mModule>(sb.ToString())
                   select TreeItemsFromModule(g);
        }

        /// <summary>
        /// Simple conversion
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static TreeItem TreeItemsFromModule(mModule m)
        {
            return new TreeItem
            {
                //ItemType = 1,
                DisplayText = m.ModuleLabel,
                ModuleID = m.ModuleID,
                TableGroupID = 0,
                GroupTableIDs = "0",
                TableCode = "",
                TableGroupLabel = ""
            };
        }

        public mModule GetModuleDetails(long moduleID)
        {
            return _conn.Query<mModule>(string.Format("Select * from mModule Where ModuleID = {0}", moduleID)).SingleOrDefault();
        }

        #endregion

        /// <summary>
        /// Open template data retieval
        /// </summary>
        #region Virtual List methods

        /// Used to count the records so the grid can display them correctly
        public int GetVirtualObjectItemCount2(string tableName, Dictionary<string, string> comboSelections, long instanceID, List<ISolvencyPageControl> specifiedColumnsNPage)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Select Count(*) from {0} Where INSTANCE = {1} ", tableName, instanceID));
            if (comboSelections.Any())
                foreach (KeyValuePair<string, string> comboSelection in comboSelections)
                {
                    sb.Append(string.Format("AND {0} = '{1}' ", comboSelection.Key, comboSelection.Value));
                }
            if (specifiedColumnsNPage.Any())
                foreach (ISolvencyPageControl pageControl in specifiedColumnsNPage)
                {
                    sb.Append(string.Format("AND {0} = '{1}' ", pageControl.ColName, pageControl.Text));
                }
            return _conn.ExecuteScalar<int>(sb.ToString());
        }

        /// <summary>
        /// Retrieves the records needed to fill the open template cache. Its result is limited by start and end record numbers.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableType"></param>
        /// <param name="comboSelections"></param>
        /// <param name="instanceID"></param>
        /// <param name="firstRow"></param>
        /// <param name="lastRow"></param>
        /// <param name="columns"></param>
        /// <param name="specifiedColumnsNPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        private List<OpenTableDataRow2> GetVirtualObjectItemData2(string tableName, Type tableType, Dictionary<string, string> comboSelections, long instanceID, int firstRow, int lastRow, List<OpenColInfo2> columns, List<ISolvencyPageControl> specifiedColumnsNPage, string orderBy, string sortOrder)
        {
            // Here we have the info and need to find out all the Open Table Rows records
            Stopwatch sw = new Stopwatch();
            Debug.WriteLine("");
            Debug.WriteLine(string.Format("{0} GetVirtualObjectItemData2", DateTime.Now));

            List<OpenTableDataRow2> results = new List<OpenTableDataRow2>();
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("Select * from {0} Where INSTANCE = {1} ", tableName, instanceID));
            if (comboSelections.Count > 0)
                foreach (KeyValuePair<string, string> comboSelection in comboSelections)
                {
                    sb.Append(string.Format("AND {0} = '{1}' ", comboSelection.Key, comboSelection.Value));
                }
            if (specifiedColumnsNPage.Any())
                foreach (ISolvencyPageControl pageControl in specifiedColumnsNPage)
                {
                    sb.Append(string.Format("AND {0} = '{1}' ", pageControl.ColName, pageControl.Text));
                }
            if (string.IsNullOrEmpty(orderBy))
                sb.Append("Order by PK_ID ");
            else
                sb.Append(string.Format("Order by {0} {1}, PK_ID ", orderBy, sortOrder));


            if (lastRow != -1)
            {
                switch (StaticSettings.DataTier)
                {
                    case eDataTier.SqLite:
                        sb.Append(string.Format("Limit {0} ", lastRow - firstRow));
                        sb.Append(string.Format("Offset {0} ", firstRow));
                        break;
                    case eDataTier.SqlServer:
                        sb.Append(string.Format("OFFSET {0} ROWS ", firstRow));
                        sb.Append(string.Format("FETCH NEXT {0} ROWS ONLY ", lastRow - firstRow));
                        break;
                    default:
                        throw new Exception("Not supported data type");
                }
            }

            sw.Start();
            // List<object> tableRows = _conn.Query(tableType, sb.ToString(), firstRow, lastRow);
            List<object> tableRows = _conn.Query(tableType, sb.ToString(), null);
            sw.Stop();
            Debug.WriteLine(string.Format("GetVirtualObjectItemData2 _conn.Query {0}ms - first {1}, last {2}", sw.ElapsedMilliseconds, firstRow, lastRow));
            sw.Reset();
            sw.Start();

            if (tableRows.Any())
            {
                PropertyInfo[] propInfos = tableRows[0].GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (object o in tableRows)
                {
                    OpenTableDataRow2 row = new OpenTableDataRow2();
                    PropertyInfo working = propInfos.FirstOrDefault(i => i.Name == "PK_ID");
                    var val = working.GetValue(o, null);
                    row.PK_ID = int.Parse(string.Format("{0}", val));
                    foreach (OpenColInfo2 col in columns)
                    {
                        working = propInfos.FirstOrDefault(i => i.Name == col.ColName);
                        if (working != null)
                        {
                            var value = working.GetValue(o, null);
                            if (value != null)
                                row.ColValues.Add(value.ToString());
                            else
                                row.ColValues.Add("");
                        }
                        else
                            row.ColValues.Add("Col Inconsistancy Error");
                    }
                    results.Add(row);
                }
            }

            sw.Stop();
            Debug.WriteLine(string.Format("GetVirtualObjectItemData2 objects to OpenTableDataRow2 {0}ms - results {1}, tableRows {2}", sw.ElapsedMilliseconds, results.Count, tableRows.Count));
            sw.Reset();

            return results;
        }

        /// <summary>
        /// Retrieves the records needed to fill the open template cache. Its result is limited by start and end record numbers.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="tableType"></param>
        /// <param name="comboSelections"></param>
        /// <param name="first"></param>
        /// <param name="last"></param>
        /// <param name="instanceID"></param>
        /// <param name="columns"></param>
        /// <param name="specifiedColumnsNPage"></param>
        /// <param name="orderBy"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public List<OpenTableDataRow2> GetVirtualObjectItemCache2(string tableName, Type tableType, Dictionary<string, string> comboSelections, int first, int last, long instanceID, List<OpenColInfo2> columns, List<ISolvencyPageControl> specifiedColumnsNPage, string orderBy, string sortOrder)
        {
            if (first != last && last != 0)
            {
                return GetVirtualObjectItemData2(tableName, tableType, comboSelections, instanceID, first, last, columns, specifiedColumnsNPage, orderBy, sortOrder);
            }
            return new List<OpenTableDataRow2>();
        }

        #endregion

        /// <summary>
        /// Populates column labels for open tables and labels on closed tables
        /// </summary>
        #region Labels

        public IEnumerable<mAxisOrdinate> GetTableLabelText(int tableVid, int language = 1)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select mAxisOrdinate.OrdinateID, c.Text  OrdinateLabel ");
            sb.Append("From mAxis ");
            sb.Append("Inner join mTableAxis atv on (atv.AxisID = mAxis.AxisID) ");
            sb.Append("Inner join mAxisOrdinate on (mAxis.AxisID = mAxisOrdinate.AxisID) ");
            sb.Append("inner join mConceptTranslation c on (c.ConceptID = mAxisOrdinate.ConceptID) ");
            sb.Append(string.Format("Where atv.TableID = {0} ", tableVid));
            sb.Append(string.Format("and c.LanguageID = {0} ", language));

            var tempDebug = _conn.Query<mAxisOrdinate>(sb.ToString());
            return tempDebug;
        }

        public IEnumerable<mAxisOrdinate> GetTableLabelText(List<string> tableVids, int language = 1)
        {
            List<mAxisOrdinate> result = new List<mAxisOrdinate>();
            foreach (string s in tableVids)
            {
                result.AddRange(GetTableLabelText(int.Parse(s), language));
            }
            return result.Distinct();
        }

        public IEnumerable<mAxisOrdinate> GetTableLabelTextNoTranslations(int tableVid, int languageId = 1)
        {
            //StringBuilder sb = new StringBuilder();
            //sb.Append("SELECT ao.OrdinateID, ao.OrdinateLabel ");
            //sb.Append("From mAxis ax ");
            //sb.Append("Inner join mTableAxis atv on (atv.AxisID = ax.AxisID) ");
            //sb.Append("Inner join mAxisOrdinate ao on (ax.AxisID = ao.AxisID) ");
            //sb.Append(string.Format("Where atv.TableID = {0} ", tableVid));
            //return _conn.Query<mAxisOrdinate>(sb.ToString());

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ao.OrdinateID, case when ct.Text is null then ao.OrdinateLabel else ct.Text end as OrdinateLabel ");
            sb.Append("From mAxis ax ");
            sb.Append("Inner join mTableAxis atv on (atv.AxisID = ax.AxisID) ");
            sb.Append("Inner join mAxisOrdinate ao on (ax.AxisID = ao.AxisID) ");
            sb.AppendFormat("left outer join mConceptTranslation ct on ct.ConceptID = ao.ConceptID and ct.LanguageID = {0}", languageId);
            sb.Append(string.Format("Where atv.TableID = {0} ", tableVid));
            return _conn.Query<mAxisOrdinate>(sb.ToString());
        }

        public IEnumerable<mAxisOrdinate> GetTableLabelTextNoTranslations(List<string> tableVids)
        {
            List<mAxisOrdinate> result = new List<mAxisOrdinate>();
            foreach (string s in tableVids)
            {
                result.AddRange(GetTableLabelTextNoTranslations(int.Parse(s)));
            }
            return result.Distinct();
        }

        #endregion

        /// <summary>
        /// Gathers Axis and Ordinate information from Tables, allowing x,y,z lookups, orindate codes, ordering etc.
        /// </summary>
        #region Axis and Ordinates

        public IEnumerable<mAxis> GetTableAxis(int tableVid, string axisOrientation = "", int languageId = 1)
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(axisOrientation))
            {
                //sb.AppendLine("SELECT a.* ");
                //sb.AppendLine("FROM mAxis a ");
                //sb.AppendLine("INNER JOIN mAxisOrdinate atv ON ( atv.AxisID = a.AxisID )  ");
                //sb.AppendLine("INNER JOIN mTableAxis ta ON ( ta.AxisID = atv.AxisID )  ");
                //sb.AppendLine(string.Format("WHERE TableID = {0} AND AxisOrientation == '{1}' ", tableVid, axisOrientation));

                sb.AppendLine("SELECT a.AxisID, a.AxisOrientation, a.ConceptID, a.IsOpenAxis, case when  ct.Text is null then a.AxisLabel else ct.Text end as AxisLabel ");
                sb.AppendLine("FROM mAxis a ");
                sb.AppendLine("INNER JOIN mAxisOrdinate atv ON ( atv.AxisID = a.AxisID )  ");
                sb.AppendLine("INNER JOIN mTableAxis ta ON ( ta.AxisID = atv.AxisID )  ");
                sb.AppendFormat("left outer join mConceptTranslation ct on ct.ConceptID = a.ConceptID and ct.LanguageID = {0}", languageId);
                sb.AppendLine(string.Format("WHERE TableID = {0} AND AxisOrientation == '{1}' ", tableVid, axisOrientation));
            }
            else
            {
                sb.AppendLine("SELECT a.AxisID, a.AxisOrientation, a.ConceptID, a.IsOpenAxis, case when  ct.Text is null then a.AxisLabel else ct.Text end as AxisLabel ");
                sb.AppendLine("FROM mAxis a ");
                sb.AppendLine("INNER JOIN mTableAxis ta ON ( ta.AxisID = a.AxisID )  ");
                sb.AppendFormat("left outer join mConceptTranslation ct on ct.ConceptID = a.ConceptID and ct.LanguageID = {0}", languageId);
                sb.AppendLine(string.Format("WHERE TableID = {0}", tableVid));
            }
            return _conn.Query<mAxis>(sb.ToString());
        }

        public IEnumerable<mAxis> GetTableAxis(List<string> tableVid, string axisOrientation = "")
        {
            List<mAxis> result = new List<mAxis>();
            foreach (string s in tableVid)
            {
                result.AddRange(GetTableAxis(int.Parse(s), axisOrientation));
            }
            return result.Distinct();
        }

        public IEnumerable<mAxisOrdinate> GetTableAxisOrdinate(int tableVid, int langugeId = 1)
        {
            return
                _conn.Query<mAxisOrdinate>(
                    string.Format(
@"Select ao.AxisID, ao.ConceptID, ao.""Order"", ao.IsAbstractHeader, ao.IsDisplayBeforeChildren, ao.IsRowKey, ao.Level, ao.OrdinateCode, ao.OrdinateID, ao.ParentOrdinateID
    , case when ct.Text is null then ao.OrdinateLabel else ct.Text end as OrdinateLabel
    , ao.TypeOfKey
From mAxis Inner join mTableAxis atv on (atv.AxisID = mAxis.AxisID) 
Inner join mAxisOrdinate ao on (mAxis.AxisID = ao.AxisID) 
left outer join mConceptTranslation ct on ct.ConceptID = ao.ConceptID and ct.LanguageID = {2}
Where atv.TableID = {0} And mAxis.AxisOrientation == '{1}' 
Order by atv.[Order], mAxisOrdinate.[Order] ",
                        tableVid, "Z", langugeId));

        }

        public IEnumerable<mAxisOrdinate> GetTableAxisOrdinate(List<string> tableVid)
        {
            List<mAxisOrdinate> result = new List<mAxisOrdinate>();
            foreach (string s in tableVid)
            {
                result.AddRange(GetTableAxisOrdinate(int.Parse(s)));
            }
            return result.Distinct();
        }

        public IEnumerable<mAxisOrdinate> GetTableAxisOrdinateColumns(int tableVid, int languageId = 1)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * from vwGetTableAxisOrdinateColumns ");
            sb.Append(string.Format("WHERE TableID = {0} and LanguageID = {1}", tableVid, languageId));
            sb.Append("Order by IsRowKey Desc,TableAxisOrder, OrdinateOrder ");
            return _conn.Query<mAxisOrdinate>(sb.ToString());
        }

        public IEnumerable<mAxisOrdinate> GetTableAxisOrdinateColumns(List<string> tableVid)
        {
            List<mAxisOrdinate> result = new List<mAxisOrdinate>();
            foreach (string s in tableVid)
            {
                result.AddRange(GetTableAxisOrdinateColumns(int.Parse(s)));
            }
            return result.Distinct().OrderBy(c => c.Order);
        }

        public List<mAxisOrdinate> GetzAxisOrdinate(long axisID, int languageId = 1)
        {
            // List is linked to not using the Hierachy that is flagged with the memberID
            StringBuilder sb = new StringBuilder();
            //sb.Append("Select Distinct ao.* ");
            //sb.Append("from mAxisOrdinate ao ");
            //sb.Append("Inner join mOrdinateCategorisation oc on (oc.OrdinateID = ao.OrdinateID) ");
            //sb.Append("Inner join mMember m on (m.MemberID = oc.MemberID) ");
            //sb.Append(string.Format("where AxisID = {0} ", axisID));
            //sb.Append("AND oc.MemberID != 9999 ");
            //sb.Append("Order by m.MemberID ");
            sb.AppendFormat(@"Select Distinct ao.AxisID, ao.ConceptID, ao.OrdinateID, ao.IsAbstractHeader, ao.IsDisplayBeforeChildren, ao.IsRowKey, ao.Level, ao.""Order"", ao.OrdinateCode
, ifnull(ct.Text, ao.OrdinateLabel) OrdinateLabel
, ao.ParentOrdinateID, ao.TypeOfKey
            from mAxisOrdinate ao 
            Inner join mOrdinateCategorisation oc on (oc.OrdinateID = ao.OrdinateID) 
            Inner join mMember m on (m.MemberID = oc.MemberID) 
            left outer join mConceptTranslation ct on ct.ConceptID = ao.ConceptID
            where AxisID = {0} and ct.LanguageID = {1}
            AND oc.MemberID != 9999 
            Order by m.MemberID ", axisID, languageId);
            return _conn.Query<mAxisOrdinate>(sb.ToString());
        }

        public List<ComboItem> GetzAxisOrdinateComboItems(long axisID, int languageID)
        {
            // Text, Value, Bold

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT m.MemberXBRLCode AS Value, case when ct.Text is null then ao.OrdinateLabel else ct.Text end as Text ");
            sb.Append("FROM mAxisOrdinate ao INNER JOIN mOrdinateCategorisation oc ON ( oc.OrdinateID = ao.OrdinateID ) ");
            sb.Append("left JOIN mMember m ON ( m.MemberID = oc.MemberID ) ");
            sb.Append("INNER JOIN mConceptTranslation ct ON (ct.ConceptId = ao.ConceptId) ");
            sb.Append(string.Format("where AxisID = {0} ", axisID));
            sb.Append("AND oc.MemberID != 9999 ");
            sb.Append(string.Format("and ct.LanguageID = {0} ", languageID));
            sb.Append("Order by m.MemberID ");
            return _conn.Query<ComboItem>(sb.ToString());
        }

        public IEnumerable<string> GetComboBoxHighlights(string table, string colName, string whereClause)
        {
            List<SingleString> result = _conn.Query<SingleString>(string.Format("Select {0} as Name from [{1}] {2} ", colName, table, whereClause));
            return result.Select(r => r.Name);
        }

        #endregion

        #region Members

        
        /// <summary>
        /// Used to populate combo boxes
        /// </summary>
        /// <param name="ordinateID"></param>
        /// <param name="languageID"></param>
        /// <returns></returns>
        public List<ComboItem> GetOrdinateComboItems(long ordinateID, int languageID)
        {
            // Here we are gathering all the info required to save details for this MemberCode

            //List<ComboItem> result = _conn.Query<ComboItem>(sb.ToString());
            StringBuilder sb = new StringBuilder();

            /*
            sb.Append("SELECT DISTINCT mem.MemberXBRLCode AS Value, " + SUBSTRING_FUNCTION + "( '                ', 0, ( hn.Level - 1 ) * 3 ) " + CONCATENATION_CHARACTER + " mem.MemberLabel Text, hn.IsAbstract,  MetricID, HierarchyID, hn.[Order] ");
            sb.Append(", met.IsStartingMemberIncluded, hn.Level ");
            sb.Append("FROM mOrdinateCategorisation oc ");
            sb.Append("INNER JOIN mMetric met ON met.CorrespondingMemberID = oc.MemberID ");
            sb.Append("INNER JOIN mHierarchyNode hn ON hn.HierarchyID = met.ReferencedHierarchyID ");
            sb.Append("INNER JOIN mMember mem ON mem.MemberID = hn.MemberID ");
            sb.Append(string.Format("WHERE OrdinateID = {0} ", ordinateID));
            sb.Append("AND oc.Source = 'MD' ");
            sb.Append("ORDER BY MetricID, HierarchyID, hn.Level, hn.[Order] ");

            

            if (result != null && result.Count > 0)
            {
                bool? startingMemberIncluded = result[0].IsStartingMemberIncluded;

                // If the starting member is not included mark it as Abstract. 
                // It cannot be selected if its abstract.
                if (startingMemberIncluded != null && startingMemberIncluded == false)
                {
                    // Find the one with the lowest level - there is only one branch in this scenario
                    ComboItem startingMember = result.OrderBy(c => c.Order).ThenBy(n=> n.Level).FirstOrDefault();
                    if (startingMember != null) startingMember.IsAbstract = true;
                }
            }

#if (FOR_UT)*/
            List<mMetric> metric = _conn.Query<mMetric>(string.Format("select distinct met.ReferencedHierarchyID, met.HierarchyStartingMemberID, met.IsStartingMemberIncluded from mMetric met inner join mMember mem on mem.MemberID = met.CorrespondingMemberID inner join mOrdinateCategorisation oc on oc.MemberID = mem.MemberID where met.DataType = 'Enumeration/Code'  and oc.OrdinateID = {0} order by met.ReferencedHierarchyID", ordinateID));
            mMetric met = metric.FirstOrDefault();
            if (met != null)
            {
                sb = new StringBuilder();
                sb.Append("SELECT DISTINCT m.MemberXBRLCode AS Value, " + SUBSTRING_FUNCTION + "( '                ', 0, ( hn.Level - 1 ) * 3 ) " + CONCATENATION_CHARACTER + " m.MemberLabel Text, hn.IsAbstract, ");
                sb.Append(string.Format(" {0} as MetricID, {1} as HierarchyID, hn.[Order], {2} as IsStartingMemberIncluded, hn.Level ", met.MetricID, met.ReferencedHierarchyID, Convert.ToInt32(met.IsStartingMemberIncluded)));
                sb.Append(" from mHierarchyNode hn inner join mMember m on m.MemberID = hn.MemberID ");
                sb.Append(string.Format("where hn.HierarchyID = {0} ", met.ReferencedHierarchyID));

                if (met.HierarchyStartingMemberID > 0)
                {
                    //BRAG Added fix for IsStartingMemberIncluded (can it be null?)
                    sb.Append(string.Format(" and (hn.Path like '%'||{0}||'%' or (hn.MemberID = {0} and 1 = {1}) ) ", met.HierarchyStartingMemberID, (bool)met.IsStartingMemberIncluded ? 1 : 0));
                }

                sb.Append(" order by hn.[Order] ");
            }

            //sb.Append(" and (hn.Path like '%'|| met.HierarchyStartingMemberID||'%'  or met.HierarchyStartingMemberID is null) ");


            List<ComboItem> result = _conn.Query<ComboItem>(sb.ToString());
            
            /*if (result != null && result.Count > 0)
            {
                bool? startingMemberIncluded = result[0].IsStartingMemberIncluded;
                
                // If the starting member is not included mark it as Abstract. 
                // It cannot be selected if its abstract.
                if (met.IsStartingMemberIncluded != null && met.IsStartingMemberIncluded == false)
                {
                    // Find the one with the lowest level - there is only one branch in this scenario
                    ComboItem startingMember = result.OrderBy(c => c.Level).FirstOrDefault();
                    if (startingMember != null) startingMember.IsAbstract = true;
                }
            }*/

//#endif

            return result;
        }

        /// <summary>
        /// Used to populate combo boxes
        /// </summary>
        /// <param name="axisID"></param>
        /// <param name="languageID"></param>
        /// <param name="startOrder"></param>
        /// <param name="nextOrder"></param>
        /// <returns></returns>
        public List<ComboItem> GetzAxisMemberComboItems(long axisID, int languageID, long startOrder, long nextOrder)
        {

            StringBuilder sb = new StringBuilder();
            
            sb.Append(" SELECT mem.MemberXBRLCode AS Value, {1}( '                ', 0, ( hn.Level - 1 ) * 3 ) {2}  ");
            sb.Append("CASE WHEN ct.Text IS NULL THEN mem.MemberLabel ELSE ct.Text END AS [Text], hn.IsAbstract,  ");
            switch (StaticSettings.DataTier)
            {
                case eDataTier.SqLite:
                    sb.Append("1 AS Include ");
                    break;
                case eDataTier.SqlServer:
                    sb.Append("cast(1 as bit) AS Include ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            sb.Append("FROM mOpenAxisValueRestriction oavr INNER JOIN mHierarchyNode hn ON hn.HierarchyID = oavr.HierarchyID ");
            sb.Append("INNER JOIN mMember mem ON mem.MemberID = hn.MemberID ");
            sb.Append("INNER JOIN mDomain dom ON dom.DomainID = mem.DomainID ");
            sb.Append("left JOIN mConcept dc ON dc.ConceptID = dom.ConceptID ");
            sb.Append("left JOIN mOwner do ON do.OwnerID = dc.OwnerID ");
            sb.Append("LEFT JOIN (SELECT * FROM mConceptTranslation e WHERE e.LanguageID = {3} ) ct ON mem.ConceptID = ct.ConceptID ");
            //sb.Append("where  (( hn.[Order] >= ({4}) and  (oavr.IsStartingMemberIncluded = 1 or oavr.IsStartingMemberIncluded is null)) ");
            //sb.Append("OR ( hn.[Order] > ({4}) AND oavr.IsStartingMemberIncluded = 0 ))  and hn.[Order] < ({5}) ");
            sb.Append(" where (hn.IsAbstract is null or hn.IsAbstract = 0) and (hn.Path like '%'||oavr.HierarchyStartingMemberID||'%' or (hn.MemberID = oavr.HierarchyStartingMemberID and 1 = oavr.IsStartingMemberIncluded)  or oavr.HierarchyStartingMemberID is null ) ");
            sb.Append(" AND AxisID = {0} ORDER BY hn.HierarchyID, hn.Level, hn.[Order] ");
            string query = string.Format(sb.ToString(), axisID, SUBSTRING_FUNCTION, CONCATENATION_CHARACTER, languageID, startOrder, nextOrder);

            List<ComboItem> result = _conn.Query<ComboItem>(query);
            return result;

        }


        /// <summary>
        /// Used to populate combo boxes
        /// </summary>
        /// <param name="hierarchyID"></param>
        /// <param name="startOrder"></param>
        /// <param name="nextOrder"></param>
        /// <param name="languageID"></param>
        /// <param name="ordinateID"></param>
        /// <returns></returns>
        public List<OpenComboItem> HierarchyLookup2(long hierarchyID, long startOrder, long nextOrder, int languageID, long ordinateID)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("SELECT Distinct mem.MemberXBRLCode AS Name, {0}( '                ', 0, ( hn.Level - 1 ) * 3 ) {1} CASE WHEN ct.Text IS NULL THEN mem.MemberLabel ELSE ct.Text END AS [Text], hn.IsAbstract, ", SUBSTRING_FUNCTION, CONCATENATION_CHARACTER));

            switch (StaticSettings.DataTier)
            {
                case eDataTier.SqLite:
                    sb.Append("Case when m.HierarchyStartingMemberID = mem.MemberID then m.IsStartingMemberIncluded else 1 end as Include, ");
                    break;
                case eDataTier.SqlServer:
                    sb.Append("cast(CASE WHEN m.HierarchyStartingMemberID = mem.MemberID THEN m.IsStartingMemberIncluded ELSE 1 END as bit) AS Include, ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            sb.Append("hn.HierarchyID, hn.[Order] ");
            sb.Append("FROM mMetric m ");
            sb.Append("inner join mOrdinateCategorisation oc on oc.MemberID = m.CorrespondingMemberID ");
            sb.Append("INNER JOIN mHierarchyNode hn ON hn.HierarchyID = m.ReferencedHierarchyID ");
            sb.Append("INNER JOIN mMember mem ON mem.MemberID = hn.MemberID ");
            sb.Append("INNER JOIN mDomain dom ON dom.DomainID = mem.DomainID ");
            sb.Append("LEFT JOIN mConcept dc ON dc.ConceptID = dom.ConceptID ");
            sb.Append("LEFT JOIN mOwner do ON do.OwnerID = dc.OwnerID ");
            sb.Append(string.Format("LEFT JOIN ( SELECT * FROM mConceptTranslation e WHERE e.LanguageID = {0} ) ct ON mem.ConceptID = ct.ConceptID ", languageID));
            sb.Append(string.Format("WHERE ( ( hn.[Order] >= {0} AND ( m.IsStartingMemberIncluded = 1 OR m.IsStartingMemberIncluded IS NULL )  ) ", startOrder));
            sb.Append(string.Format("OR ( hn.[Order] >= {0}  AND m.IsStartingMemberIncluded = 0 )  ) ", startOrder));
            sb.Append(string.Format("AND hn.[Order] < {0} AND m.ReferencedHierarchyID = {1} ", nextOrder, hierarchyID));
            sb.Append(string.Format("and oc.OrdinateID = {0} and oc.Source not like 'HD' ", ordinateID));
            sb.Append("ORDER BY hn.HierarchyID, hn.[Order] ");

            return _conn.Query<OpenComboItem>(sb.ToString());

        }


        public ComboHierarchy GetOrdinateHierarchyID_MD(long ordinateID)
        {
            IEnumerable<ComboHierarchy> result = _conn.Query<ComboHierarchy>(string.Format("Select Distinct * from vwGetOrdinateHierarchyID_MD Where OrdinateID = {0} ", ordinateID));
            return result.FirstOrDefault();
        }

        public ComboHierarchy GetOrdinateHierarchyID_HD(long ordinateID)
        {
            try
            {
                IEnumerable<ComboHierarchy> result = _conn.Query<ComboHierarchy>(string.Format("Select Distinct * from vwGetOrdinateHierarchyID_HD Where OrdinateID = {0} ", ordinateID));
                return result.FirstOrDefault();

            }
            catch (SQLiteException e)
            {
                Console.WriteLine(e);
                return new ComboHierarchy();
            }

        }

        public string GetOrdinateType(long ordinateId)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT met.DataType FROM mOrdinateCategorisation oc ");
            sb.Append("LEFT OUTER JOIN mMember m ON ( m.MemberID = oc.MemberID ) ");
            sb.Append("INNER JOIN mMetric met ON ( met.CorrespondingMemberID = m.MemberID ) ");
            sb.Append("WHERE (oc.Source NOT LIKE 'HD' OR oc.Source IS NULL) AND ");
            sb.Append(string.Format("OrdinateID = {0};", ordinateId));

            //string query = string.Format("SELECT MemberCode FROM vwGetOrdinateType WHERE OrdinateID = {0} limit 1", ordinateId);
            string memberCode = _conn.ExecuteScalar<string>(sb.ToString());
            if (!string.IsNullOrEmpty(memberCode) && memberCode.Length >= 1)
                return memberCode.ToUpper();
            return "STRING"; // String - default
        }


        public IEnumerable<ComboItem> GetLanguageDropDownData()
        {
            return _conn.Query<ComboItem>("Select LanguageName Text, LanguageID IntValue from mLanguage");
        }

        #endregion

        #region Instance

        public IEnumerable<ComboItem> GetInstanceDropDownData()
        {
            return _conn.Query<ComboItem>("SELECT Distinct * FROM [vwGetInstanceDropDownData] Order by InstanceID");
        }
        public dInstance GetInstanceDetails(long instanceID)
        {
            List<dInstance> result = _conn.Query<dInstance>(string.Format("Select * from dInstance where InstanceID = {0}  ", instanceID));
            if (result.Count == 1)
                return result[0];
            return null;
        }
        public dInstance GetInstanceDetails(string fileName)
        {
            dInstance result = (_conn.Query<dInstance>(string.Format("Select * from dInstance where FileName = '{0}' ", fileName))).FirstOrDefault();
            return result;
        }


        #endregion

        #region Form Generator Queries - SQLite Only NOT SQL Server

        public IEnumerable<FactInformation> GetAllTableControls(List<string> tableVids, int singleZOrdinateID = 0)
        {
            List<FactInformation> result = new List<FactInformation>();
            foreach (string s in tableVids)
            {
                result.AddRange(GetTableControls(int.Parse(s), false, singleZOrdinateID));
            }
            return result.Distinct();
        }

        private IEnumerable<FactInformation> GetTableControls(int tableVid, bool shadedOnly, int singleZOrdinateID = 0)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT Distinct ");
            sb.Append("IsShaded, aox.OrdinateID XordinateID, aoy.OrdinateID YordinateID ");
            sb.Append("FROM mTableCell tc  ");
            sb.Append("LEFT OUTER JOIN mCellPosition cpx ON tc.CellID = cpx.CellID ");
            sb.Append("INNER JOIN mAxisOrdinate aox ON ( cpx.OrdinateID = aox.OrdinateID ) ");
            sb.Append("INNER JOIN mAxis ax ON ( ax.AxisID = aox.AxisID AND ax.AxisOrientation = 'X' ) ");
            sb.Append("INNER JOIN mCellPosition cpy ON cpy.CellID = tc.CellID ");
            sb.Append("INNER JOIN mAxisOrdinate aoy ON ( cpy.OrdinateID = aoy.OrdinateID ) ");
            sb.Append("INNER JOIN mAxis ay ON ( ay.AxisID = aoy.AxisID AND ay.AxisOrientation = 'Y' ) ");

            if (singleZOrdinateID != 0)
            {
                sb.Append("INNER JOIN mCellPosition cpz ON cpz.CellID = tc.CellID ");
                sb.Append("INNER JOIN mAxisOrdinate aoz ON ( cpz.OrdinateID = aoz.OrdinateID ) ");
                sb.Append("INNER JOIN mAxis az ON ( az.AxisID = aoz.AxisID AND az.AxisOrientation = 'Z') ");
            }

            sb.Append(" ");
            sb.Append(string.Format("Where tc.TableID = {0} ", tableVid));
            if (singleZOrdinateID != 0)
                sb.Append(string.Format(" AND aoz.OrdinateID = {0} ", singleZOrdinateID));
            if (shadedOnly) sb.Append("and IsShaded = 1 ");


            var result = _conn.Query<FactInformation>(sb.ToString());
            return result;
        }

        public IEnumerable<FactInformation> GetSemiTableControls(List<string> tableVids, int singleZOrdinateID = 0)
        {
            List<FactInformation> result = new List<FactInformation>();
            foreach (string s in tableVids)
            {
                result.AddRange(GetSemiTableControls(int.Parse(s), false, singleZOrdinateID));
            }
            return result.Distinct();
        }

        private IEnumerable<FactInformation> GetSemiTableControls(int tableVid, bool shadedOnly, int singleZOrdinateID = 0)
        {

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT Distinct ");
            sb.Append("IsShaded, aox.OrdinateID XordinateID, aoy.OrdinateID YordinateID ");
            sb.Append("FROM mTableCell tc  ");
            sb.Append("LEFT OUTER JOIN mCellPosition cpx ON tc.CellID = cpx.CellID ");
            sb.Append("INNER JOIN mAxisOrdinate aox ON ( cpx.OrdinateID = aox.OrdinateID ) ");
            sb.Append("INNER JOIN mAxis ax ON ( ax.AxisID = aox.AxisID AND ax.AxisOrientation = 'X' AND ax.IsOpenAxis = 0 ) ");
            sb.Append("INNER JOIN mCellPosition cpy ON cpy.CellID = tc.CellID ");
            sb.Append("INNER JOIN mAxisOrdinate aoy ON ( cpy.OrdinateID = aoy.OrdinateID ) ");
            sb.Append("INNER JOIN mAxis ay ON ( ay.AxisID = aoy.AxisID AND ay.AxisOrientation = 'Y' AND ax.IsOpenAxis = 0) ");

            if (singleZOrdinateID != 0)
            {
                sb.Append("INNER JOIN mCellPosition cpz ON cpz.CellID = tc.CellID ");
                sb.Append("INNER JOIN mAxisOrdinate aoz ON ( cpz.OrdinateID = aoz.OrdinateID ) ");
                sb.Append("INNER JOIN mAxis az ON ( az.AxisID = aoz.AxisID AND az.AxisOrientation = 'Z') ");
            }

            sb.Append(" ");
            sb.Append(string.Format("Where tc.TableID = {0} ", tableVid));
            if (singleZOrdinateID != 0)
                sb.Append(string.Format(" AND aoz.OrdinateID = {0} ", singleZOrdinateID));
            if (shadedOnly) sb.Append("and IsShaded = 1 ");


            var result = _conn.Query<FactInformation>(sb.ToString());
            return result;
        }

        public IEnumerable<AxisOrdinateControls> GetControlInformation(List<string> tableVids)
        {
            List<AxisOrdinateControls> result = new List<AxisOrdinateControls>();
            foreach (string s in tableVids)
            {
                result.AddRange(GetControlInformation(int.Parse(s)));
            }
            return result.Distinct().OrderBy(a => a.TableID).ThenBy(b => b.Order);
        }

        private IEnumerable<AxisOrdinateControls> GetControlInformation(int tableVid)
        {
            IEnumerable<AxisOrdinateControls> results = _conn.Query<AxisOrdinateControls>(string.Format("SELECT * FROM [vwGetControlInformation] Where TableID = {0}", tableVid));
            foreach (AxisOrdinateControls ord in results)
            {
                ord.DataType = GetDataType(ord.OrdinateID);
                ord.ParentOrder = GetParentOrder(ord.ParentOrdinateID, ord.ParentOrder);
            }
            return results;
        }


        private int GetParentOrder(int parentOrdinateID, int parentOrder)
        {
            // This is here to take out the nest of the large view
            int result = _conn.ExecuteScalar<int>(string.Format("SELECT p.[Order] FROM mAxisOrdinate p WHERE p.OrdinateID = {0}", parentOrdinateID));
            if (result == 0) result = parentOrder;
            return result;
        }

        private string GetDataType(int ordinateID)
        {
            // This is here to take out the nest of the large view
            string result;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT met.DataType ");
            sb.Append("FROM mOrdinateCategorisation oc ");
            sb.Append("LEFT OUTER JOIN mMember mem ON mem.MemberID = oc.MemberID ");
            sb.Append("LEFT OUTER JOIN mMetric met ON met.CorrespondingMemberID = mem.MemberID ");
            sb.Append(string.Format("WHERE oc.OrdinateID = {0} AND met.DataType NOT NULL AND (oc.Source = 'MD' OR oc.Source is null) LIMIT 1 ", ordinateID));
            result = _conn.ExecuteScalar<string>(sb.ToString());

            if (string.IsNullOrEmpty(result))
            {
                sb.Length = 0;
                sb.Append("select dom.DataType ");
                sb.Append("from mOrdinateCategorisation oc ");
                sb.Append("inner join mDimension d on d.DimensionID = oc.DimensionID ");
                sb.Append("inner join mDomain dom on dom.DomainID = d.DomainID ");
                sb.Append(string.Format("where oc.OrdinateID = {0} ", ordinateID));
                result = _conn.ExecuteScalar<string>(sb.ToString());
            }

            return result;
        }

        public List<string> GetLocationRanges(List<string> tableGroupIDs)
        {
            return tableGroupIDs.Select(GetLocationRange).Where(range => range != null).ToList();
        }

        private string GetLocationRange(string tableGroupID)
        {
            return _conn.ExecuteScalar<string>(string.Format("SELECT AnnotatedTableLocationRange FROM [vwGetTreeData] Where TableID = {0} ORDER BY ModuleID, BusinessOrder, TemplateOrder, TemplateOrder2", tableGroupID));
        }

        public bool GetIsTypedDimension(string dOM_CODE)
        {
            // Used by the user control generator.

            return _conn.ExecuteScalar<bool>(string.Format("SELECT IsTypedDomain FROM mDomain WHERE DomainXbrlCode = '{0}'", dOM_CODE));
        }

        public List<NPageData> GetnPageData(IEnumerable<FormDataPage> formPageData)
        {
            List<NPageData> result = new List<NPageData>();
            foreach (FormDataPage dataPage in formPageData)
            {
                var item = GetAxisNPageData(dataPage.AxisID ?? 0);
                result.Add(item);
            }
            return result;
        }

        public List<NPageData> GetOpenPageData(IEnumerable<AxisOrdinateControls> ordinates)
        {
            List<NPageData> result = new List<NPageData>();
            foreach (AxisOrdinateControls ordinate in ordinates)
            {
                var item = GetAxisNPageData(ordinate.AxisID);
                result.Add(item);
            }
            return result;
        }

        public void GetControlComboInformation(ref List<AxisOrdinateControls> controlList)
        {
            foreach (AxisOrdinateControls ordinate in controlList)
            {
                NPageData item = GetAxisNPageData(ordinate.AxisID);
                if (item != null)
                {
                    ordinate.StartOrder = item.StartOrder;
                    ordinate.NextOrder = item.NextOrder;
                }
            }
        }

        private NPageData GetAxisNPageData(long axisID)
        {
            long startOrder = _conn.ExecuteScalar<long>(string.Format("SELECT hn.[Order] FROM mOpenAxisValueRestriction oavr INNER JOIN mHierarchyNode hn ON ( hn.HierarchyID = oavr.HierarchyID  AND hn.memberid = oavr.HierarchyStartingMemberID ) WHERE AxisID = {0} ", axisID));
            long startLevel = _conn.ExecuteScalar<long>(string.Format("SELECT hn.Level FROM mOpenAxisValueRestriction oavr INNER JOIN mHierarchyNode hn ON ( hn.HierarchyID = oavr.HierarchyID AND hn.memberid = oavr.HierarchyStartingMemberID ) WHERE AxisID = {0} ", axisID));
            long nextOrder = _conn.ExecuteScalar<long>(string.Format("SELECT hn.[Order] FROM mOpenAxisValueRestriction oavr INNER JOIN mHierarchyNode hn ON hn.HierarchyID = oavr.HierarchyID WHERE AxisID = {0} AND hn.[Order] > {1}  AND hn.Level <= {2} ORDER BY hn.[Order] LIMIT 1 ", axisID, startOrder, startLevel));
            if (nextOrder == 0) nextOrder = 100000;
            if (startOrder == 0) startOrder = 1;
            NPageData item = new NPageData
            {
                AxisID = axisID,
                StartOrder = startOrder,
                NextOrder = nextOrder,
            };
            return item;
        }

        public NPageData GetHierachyNPageData(ComboHierarchy comboHier)
        {
            long startOrder = _conn.ExecuteScalar<long>(string.Format("SELECT Distinct hn.[Order] as StartOrder FROM mMetric m INNER JOIN mHierarchyNode hn ON ( hn.HierarchyID = m.ReferencedHierarchyID AND hn.memberid = m.HierarchyStartingMemberID ) WHERE m.ReferencedHierarchyID = {0} AND m.HierarchyStartingMemberID = {1} ", comboHier.HierarchyID, comboHier.HierarchyStartingMemberID));
            long startLevel = _conn.ExecuteScalar<long>(string.Format("SELECT Distinct hn.Level FROM mMetric m INNER JOIN mHierarchyNode hn ON ( hn.HierarchyID = m.ReferencedHierarchyID AND hn.memberid = m.HierarchyStartingMemberID ) WHERE m.ReferencedHierarchyID = {0} AND m.HierarchyStartingMemberID = {1} ", comboHier.HierarchyID, comboHier.HierarchyStartingMemberID));
            long nextOrder = _conn.ExecuteScalar<long>(string.Format("SELECT hn.[Order] as NextOrder FROM mMetric m INNER JOIN mHierarchyNode hn ON hn.HierarchyID = m.ReferencedHierarchyID WHERE m.ReferencedHierarchyID = {0} AND hn.[Order] > {1} AND hn.Level <= {2} ORDER BY hn.[Order] LIMIT 1 ", comboHier.HierarchyID, startOrder, startLevel));
            if (nextOrder == 0) nextOrder = 100000;
            if (startOrder == 0) startOrder = 1;
            NPageData item = new NPageData
            {
                Hierarchy = comboHier.HierarchyID,
                StartOrder = startOrder,
                NextOrder = nextOrder
            };
            return item;
        }

        /// <summary>
        /// Get all PAGEn details for a list of tables (Dropdown and Fixed)
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="dynTableName"></param>
        /// <param name="singleZOrdinateID"></param>
        /// <returns></returns>
        public List<FormDataPage> GetClosedPageData(List<string> ids, out List<string> dynTableName, int singleZOrdinateID)
        {
            List<FormDataPage> pages = new List<FormDataPage>();
            dynTableName = new List<string>();
            foreach (string id in ids)
            {
                int tableId = int.Parse(id);
                List<MAPPING> mappings = SharedSQLData.GetMapping(tableId, _conn);
                if (mappings != null && mappings.Count > 0)
                {
                    pages.AddRange(SharedSQLData.GetTemplatePageData(mappings, tableId, _conn).ToList());
                    dynTableName.Add(SolvencyIITableNameConversion.FullDbName(mappings[0].DYN_TABLE_NAME)); // This is here since not every table has a PageColumnKey and we need to know the table name.
                }
            }
            if (singleZOrdinateID != 0) pages.AddRange(GetFixedDimensionPageData(ids, singleZOrdinateID));
            return pages;
        }


        #endregion

        #region Languages
        /*
            Select c.InBulgarian, 
            c.InCroatian, 
            c.InCzech,
            c.InDanish,
            c.InDutch,
            c.InEnglish,
            c.InEstonian,
            c.InFinnish,
            c.InFrench,
            c.InGerman,
            c.InGreek,
            c.InHungarian,
            c.InIrish,
            c.InItalian,
            c.InLatvian,
            c.InLithuanian,
            c.InMaltese,
            c.InPolish,
            c.InPortuguese,
            c.InRomanian,
            c.InSlovak,
            c.InSpanish,
            c.InSwedish,
            c.InSlovenian

            from aInterfaceComponent c
         
         */

        public List<LanguageLabel> GetLanguageDictionary(int applicationID, eLanguageID languageID)
        {
            List<LanguageLabel> result = _conn.Query<LanguageLabel>(string.Format("Select InterfaceComponentID as LabelID, {0} as LabelText  from vwGetLanguageInfo where ApplicationID = {1} order by ApplicationID, InterfaceComponentID", languageID, applicationID));
            return result;
        }

        #endregion

        #region DB 7 - Closed template data retrieval

        public List<object> GetClosedTableInfo(Type type, string query)
        {
            var result = _conn.Query(type, query);
            return result;
        }


        /// <summary>
        /// Get the PAGEn details where the value is fixed - not used for drop down lists.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="singleZOrdinateID"></param>
        /// <returns></returns>
        public List<FormDataPage> GetFixedDimensionPageData(IEnumerable<string> ids, int singleZOrdinateID)
        {
            List<FormDataPage> pages = new List<FormDataPage>();

            // Standard Lookup
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("SELECT d.DimensionXBRLCode {0} '('  {0} m.MemberXBRLCode {0} ')' as Name ", CONCATENATION_CHARACTER));
            sb.Append("FROM mDimension d ");
            sb.Append("INNER JOIN mHierarchy h ON h.DomainID = d.DomainID ");
            sb.Append("inner join mHierarchyNode hn on hn.HierarchyID = h.HierarchyID ");
            sb.Append("inner join mMember m on m.MemberID = hn.MemberID ");
            sb.Append("inner join mOrdinateCategorisation oc on oc.MemberID = m.MemberID ");
            sb.Append("Inner join mAxisOrdinate ao on ao.OrdinateID = oc.OrdinateID ");
            sb.Append("WHERE d.DimensionID = {0} and ao.OrdinateID = {1} ");

            // Default member lookup
            StringBuilder sbd = new StringBuilder();
            sbd.Append(string.Format("SELECT dim.DimensionXBRLCode {0} '(' {0} mem.MemberXBRLCode {0} ')' as Name ", CONCATENATION_CHARACTER));
            sbd.Append("FROM mDimension dim ");
            sbd.Append("INNER JOIN mDomain dom ON dom.DomainID = dim.DomainID ");
            sbd.Append("INNER JOIN mMember mem ON mem.DomainID = dom.DomainID ");
            sbd.Append("WHERE dim.DimensionID = {0} AND mem.IsDefaultMember = 1");


            foreach (string id in ids)
            {
                IEnumerable<FormDataPage> result = _conn.Query<FormDataPage>(string.Format("SELECT Distinct m.DYN_TAB_COLUMN_NAME, m.DYN_TABLE_NAME FROM MAPPING m WHERE m.TABLE_VERSION_ID = {0} and m.DYN_TAB_COLUMN_NAME like 'PAGE%' and m.IS_IN_TABLE and not m.IS_PAGE_COLUMN_KEY ", id));
                foreach (FormDataPage dataPage in result)
                {
                    string fullTableName = SolvencyIITableNameConversion.FullDbName(dataPage.DYN_TABLE_NAME);
                    string dimensionID;

                    DimXbrlCode xbrlCode = new DimXbrlCode(dataPage.DYN_TAB_COLUMN_NAME, true);
                    long dimID = xbrlCode.GetDimensionIDFromXbrlCode(_conn);
                    if (dimID == 0)
                        throw new Exception("Old db in use - debug message!");
                    dimensionID = dimID.ToString();

                    SingleString res = (_conn.Query<SingleString>(string.Format(sb.ToString(), dimensionID, singleZOrdinateID))).FirstOrDefault();
                    if (res != null)
                        pages.Add(new FormDataPage { DYN_TABLE_NAME = fullTableName, DYN_TAB_COLUMN_NAME = dataPage.DYN_TAB_COLUMN_NAME, Value = res.Name, FixedDimension = true, TableID = int.Parse(id) });
                    else
                    {
                        // Find the default member
                        res = (_conn.Query<SingleString>(string.Format(sbd.ToString(), dimensionID))).FirstOrDefault();
                        if (res != null && res.Name != null) pages.Add(new FormDataPage { DYN_TABLE_NAME = fullTableName, DYN_TAB_COLUMN_NAME = dataPage.DYN_TAB_COLUMN_NAME, Value = res.Name, FixedDimension = true, TableID = int.Parse(id) });
                    }
                }
            }
            return pages;
        }



        #endregion

        #region POCO generation - SQLite speciific, used in user control generator

        public List<string> GetTableNames()
        {
            // SQLite specific
            var results = _conn.Query<SingleString>("SELECT name as Name FROM sqlite_master WHERE type='table' AND name like 'T__%' ");
            if (results != null)
                return results.Select(r => r.Name).ToList();
            return null;
        }

        public List<PocoColInfo> GetTableInfo(string tableName)
        {
            var result = _conn.Query<PocoColInfo>(string.Format("PRAGMA table_info('{0}')", tableName));
            if (result != null)
                return result.ToList();
            return null;
        }


        public int GetTableID(string tableName)
        {
            if (tableName.StartsWith("T__")) tableName = tableName.Substring(3);


            var result = _conn.ExecuteScalar<int>(string.Format("Select TABLE_VERSION_ID from Mapping Where DYN_TABLE_NAME = '{0}' Limit 1", tableName));
            return result;
        }

        public List<MAPPING> GetTableMapping(string tableName)
        {
            string tableCode = tableName.Substring(tableName.IndexOf("__") + 2);
            int tableId = _conn.ExecuteScalar<int>(string.Format("Select Table_Version_ID from Mapping m Where m.DYN_TABLE_NAME = '{0}' Limit 1 ", tableCode));

            if (tableId != 0)
                return SharedSQLData.GetMapping(tableId, _conn);
            return null;
        }

        public void GetControlDimXbrlCodeForSpecialCases(ref List<AxisOrdinateControls> mulitpleRowUserControls, List<string> ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("SELECT o.OwnerPrefix {0} '_' {0} d.DimensionCode ", CONCATENATION_CHARACTER));
            sb.Append("FROM mDimension d ");
            sb.Append("INNER JOIN mOrdinateCategorisation oc ON oc.DimensionID = d.DimensionID ");
            sb.Append("INNER JOIN mConcept c ON c.ConceptID = d.ConceptID ");
            sb.Append("INNER JOIN mOwner o ON o.OwnerID == c.OwnerID ");
            sb.Append("WHERE oc.OrdinateID = {0}; ");

            foreach (AxisOrdinateControls cntrl in mulitpleRowUserControls)
            {
                string newCode = _conn.ExecuteScalar<string>(string.Format(sb.ToString(), cntrl.OrdinateID));
                if (!string.IsNullOrEmpty(newCode))
                    cntrl.DimXbrlCode = newCode.ToUpper();
                else
                    throw new ApplicationException(string.Format("No DimXbrlCode was found for OrdinateID {0}", cntrl.OrdinateID));
            }
        }

        #endregion




        #region nPage Combo stuff

        public List<ComboItem> GetNPageTextComboItems(string colName, long instanceID, List<string> tableNames)
        {
            List<ComboItem> result = new List<ComboItem>();
            foreach (string tableName in tableNames)
            {
                try
                {
                    result.AddRange(_conn.Query<ComboItem>(string.Format("SELECT Distinct {0} as Text, {0} as Value FROM {1} WHERE Instance = {2} Order by {0};", colName, tableName, instanceID)));
                }
                catch (Exception)
                {
                    Console.WriteLine("Column may not exist in table " + tableName);
                }
            }
            return result;
        }

        public void GetnPageStartingData(long instanceID, List<string> getDataTables, ref Dictionary<string, string> startingEntries)
        {
            // Find the first record
            foreach (string dataTable in getDataTables)
            {
                int primaryKey = _conn.ExecuteScalar<int>(string.Format("Select PK_ID from {0} Where Instance = {1} order by PK_ID", dataTable, instanceID));
                if (primaryKey > 0)
                {
                    for (int i = 0; i < startingEntries.Count; i++)
                    {
                        try
                        {
                            startingEntries[startingEntries.ElementAt(i).Key] = _conn.ExecuteScalar<string>(string.Format("Select {0} from {1} Where PK_ID = {2}", startingEntries.ElementAt(i).Key, dataTable, primaryKey));
                        }
                        catch (Exception ex)
                        {
                            if (!ex.Message.StartsWith("no such column"))
                                throw;
                        }
                        
                    }
                }
            }

        }

        #endregion

        #region dFilingIndicator

        public bool GetFilingIndicatorFiled(long instanceID, int templateOrTableID)
        {
            return SharedSQLData.GetFilingIndicatorFiled(instanceID, templateOrTableID, _conn);
        }

        #endregion


        #region Version and Application Data

        public void SetApplicationData(string applicationVersion, string applicationType)
        {
            string query = string.Format(@"Update aApplication Set ApplicationVersion = ""{0}"", DatabaseType = ""{1}"" Where ApplicationID = 1", applicationVersion, applicationType);
            _conn.Execute(query);
        }

        public aApplication CheckDBVersion()
        {
            try
            {
                string query = string.Format(@"Select * from aApplication Where ApplicationID = 1");
                return _conn.Query<aApplication>(query).FirstOrDefault();
            }
            catch (SQLiteException ex)
            {
                if (ex.Message == "no such table: aApplication")
                    // DB does not contain data type
                    return new aApplication();
                throw;
            }

        }

        #endregion

        #region Form Data Integrity checking

        public List<string> GetFieldsOfTable(string tableName)
        {
            switch (StaticSettings.DataTier)
            {
                case eDataTier.SqLite:
                    return GetTableInfo(tableName).Select(c => c.name.ToUpper()).ToList();
                case eDataTier.SqlServer:
                    if (CheckTableName(tableName))
                    {
                        List<SingleString> results = _conn.Query<SingleString>("select Column_Name from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME=@1", tableName).ToList();
                        return results.Select(s => s.Name).ToList();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return new List<string>();
        }

        private bool CheckTableName(string tableName)
        {
            string name = tableName.ToUpper();
            if (name.Contains("DEL ")) return false;
            if (name.Contains("DELETE")) return false;
            if (name.Contains("*")) return false;
            if (name.Contains("%")) return false;
            return true;
        }

        #endregion


        #region Excel specific queries

        public List<RcBusinessCodeMapping> getRcBusinessCodes()
        {
            string query = string.Format(@"   SELECT t.TableCode as 'value1', tc.CellID as 'value2',group_concat(ao.OrdinateCode, '|') as 'value3', tc.BusinessCode as 'value4'     
                                                FROM mTable t 
                                                    inner join mTableCell tc on tc.TableID = t.TableID  
                                                    inner join mCellPosition cp on cp.CellID = tc.CellID     
                                                    inner join mAxisOrdinate ao on cp.OrdinateID = ao.OrdinateID  
                                                group by t.TableCode, tc.CellID");

            return _conn.Query<RcBusinessCodeMapping>(query).ToList();

        }

        #endregion

        #region Migration
        public List<Instance> GetAllInstanceDetails()
        {
            return _conn.Query<Instance>("SELECT Distinct * FROM [vwGetInstanceDropDownData] Order by InstanceID");
        }
        #endregion

        public List<CellProperties> GetCellProperties(string tableCode)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select t.tableid, t.tablecode, a.axisorientation, ao.ordinatecode, oc.DimensionMemberSignature, oc.Source, d.dimensioncode, d.dimensionlabel, m.membercode, m.memberlabel ");
            sb.Append(" from mTable t, mTableAxis ta, mAxis a, mAxisOrdinate ao, mOrdinateCategorisation oc, mDimension d, mMember m ");
            sb.Append(" where t.tableid = ta.tableid and ta.axisid = a.axisid and a.axisid = ao.axisid and ao.ordinateid = oc.ordinateid and oc.dimensionid = d.dimensionid and oc.memberid = m.memberid ");
            sb.Append(string.Format(" and t.tablecode = '{0}'", tableCode));

            return _conn.Query<CellProperties>(sb.ToString()).ToList();

        }

    }



    /// <summary>
    /// Used to convert to TreeItems
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class TreeBit<T> where T : TreeItem, new()
    {
        public T TreeBitFromModule(mModule m)
        {
            return new T
            {
                DisplayText = m.ModuleLabel,
                GroupTableIDs = "0"
            };
        }

        public T TreeBitFromTreeItem(TreeItem q, bool useTableLabel)
        {
            return new T
            {
                ModuleID = q.ModuleID,
                DisplayText =
                    useTableLabel
                        ? (string.IsNullOrEmpty(q.TableLabel) ? q.TableCode : q.TableLabel)
                        : q.TableGroupLabel,
                BranchID = q.BranchID,
                ParentBranchID = q.ParentBranchID,
                TableID = q.TableID,
                TableCode = q.TableCode,
                TableLabel = q.TableLabel,
                TableGroupID = q.TableGroupID,
                TableGroupLabel = q.TableGroupLabel,
                GroupTableIDs = string.IsNullOrEmpty(q.GroupTableIDs) ? q.TableID.ToString() : q.GroupTableIDs,
                IsTyped = q.IsTyped,
                TemplateOrTableID = q.TemplateOrTableID,
                FrameworkCode = q.FrameworkCode,
                TaxonomyCode = q.TaxonomyCode,
                Version = q.Version,
                HasLocationRange = q.HasLocationRange,
                SingleZOrdinateID = q.SingleZOrdinateID,
                AxisID = q.AxisID

            };
        }
    }
}
