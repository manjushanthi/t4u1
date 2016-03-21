using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Extarctor of filling indicators from SQLite database
    /// </summary>
    public class SQLiteFillIndicatorsExtractor : IFilinglIndicatorsExtractor
    {
        IDataConnector _dataConnector;
        ITableNamesExtractor _namesExtractor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteFillIndicatorsExtractor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        public SQLiteFillIndicatorsExtractor(IDataConnector dataConnector)
        {
            _dataConnector = dataConnector;
            _namesExtractor = new TableNamesExtractor(dataConnector);
        }

        /// <summary>
        /// Gets the tables names from filling indicators.
        /// </summary>
        /// <param name="instnanceId">The instnance identifier.</param>
        /// <returns></returns>
        /// <exception cref="EtlException">Exception during reading filling indicators for instance  + instnanceId</exception>
        public string[] getTablesNamesFromFillingIndicators(int instnanceId)
        {
            try
            {
                HashSet<int> potentialTableIds = getPotentialTableIDsFromFillingIndicators(instnanceId);
                return this.getTableNames(getTableNames(potentialTableIds));
            }
            catch (Exception ex)
            {
                throw new EtlException("Exception during reading filling indicators for instance " + instnanceId, ex);
            }
        }

        /// <summary>
        /// Gets the potential table i ds from filling indicators.
        /// </summary>
        /// <param name="instnanceId">The instnance identifier.</param>
        /// <returns></returns>
        private HashSet<int> getPotentialTableIDsFromFillingIndicators(int instnanceId)
        {
            string query = string.Format(@"select fi.BusinessTemplateID
                                            from dFilingIndicator fi 
                                            where fi.InstanceID = {0}", instnanceId);

            HashSet<int> potentialTableIds = new HashSet<int>();
            DataTable dt = this._dataConnector.executeQuery(query);
            int tabVarId = 0;

            foreach (DataRow dr in dt.Rows)
            {
                tabVarId = 0;
                tabVarId = int.Parse(dr[0].ToString());

                foreach (int tabId in getTablesIds(tabVarId))
                    potentialTableIds.Add(tabId);
            }
            return potentialTableIds;
        }

        /// <summary>
        /// Gets the table names.
        /// </summary>
        /// <param name="potentialTableIds">The potential table ids.</param>
        /// <returns></returns>
        private string[] getTableNames(HashSet<int> potentialTableIds)
        {
            return _namesExtractor.getTableNames(potentialTableIds);
        }

        /// <summary>
        /// Gets the table names.
        /// </summary>
        /// <param name="tableNames">The table names.</param>
        /// <returns></returns>
        private string[] getTableNames(string[] tableNames)
        {
            string[] formatedTableNames = new string[tableNames.Count()];
            for (int i = 0; i < tableNames.Count(); i++)
            {
                if (tableNames[i].StartsWith("T__"))
                    formatedTableNames[i] = tableNames[i];
                else
                    formatedTableNames[i] = "T__" + tableNames[i];
            }
            return formatedTableNames;
        }

        /// <summary>
        /// Gets the tables ids.
        /// </summary>
        /// <param name="parentTableOrTemplate">The parent table or template.</param>
        /// <returns></returns>
        private IEnumerable<int> getTablesIds(int parentTableOrTemplate)
        {
            HashSet<int> tabIds = new HashSet<int>();

            string query = @"select TemplateOrTableID, ParentTemplateOrTableID, TemplateOrTableType, IFNULL(TableID, 'NULL') TableID
                            from mTemplateOrTable tt 
                                left join mTaxonomyTable taxt 
                                    on (taxt.AnnotatedTableID = tt.TemplateOrTableID and taxt.TaxonomyID = tt.TaxonomyID)
                            where tt.ParentTemplateOrTableID = " + parentTableOrTemplate;

            DataTable dt = _dataConnector.executeQuery(query);

            //AnnotatedTemplate
            //AnnotatedTable

            int id = 0;
            foreach (DataRow dr in dt.Rows)
            {
                id = int.Parse(dr["TemplateOrTableID"].ToString());
                if (dr["TemplateOrTableType"].ToString().Trim().Equals("AnnotatedTable"))
                    tabIds.Add(int.Parse(dr["TableID"].ToString()));

                foreach (int tabId in getTablesIds(id))                
                    tabIds.Add(tabId);
                
            }
            return tabIds;
        }

        /// <summary>
        /// Gets the tables names from module.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        /// <returns></returns>
        /// <exception cref="EtlException">Exception during reading modules for instance  + instanceID</exception>
        public string[] getTablesNamesFromModule(int instanceID)
        {
            try
            {
                HashSet<int> potentialTableIds = getPotentialTableIDsFromModule(instanceID);
                return this.getTableNames(getTableNames(potentialTableIds));
            }
            catch (Exception ex)
            {
                throw new EtlException("Exception during reading modules for instance " + instanceID, ex);
            }
        }

        /// <summary>
        /// Gets the potential table i ds from module.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        /// <returns></returns>
        private HashSet<int> getPotentialTableIDsFromModule(int instanceID)
        {
            string query = @"select mbt.BusinessTemplateID
from dInstance i 
    inner join mModule m on m.ModuleID = i.ModuleID
    inner join mModuleBusinessTemplate mbt on mbt.ModuleID = m.ModuleID
where i.InstanceID  = {0}";
            query = string.Format(query, instanceID);

            HashSet<int> potentialTableIds = new HashSet<int>();
            DataTable dt = this._dataConnector.executeQuery(query);
            int tabVarId = 0;
            foreach (DataRow dr in dt.Rows)
            {
                tabVarId = 0;
                tabVarId = int.Parse(dr[0].ToString());

                foreach (int tabId in getTablesIds(tabVarId))
                    potentialTableIds.Add(tabId);
            }
            return potentialTableIds;
        }

        /// <summary>
        /// Gets the tables i ds from filling indicators.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        /// <returns></returns>
        public int[] getTablesIDsFromFillingIndicators(int instanceID)
        {
            HashSet<int> potentialTableIds = getPotentialTableIDsFromFillingIndicators(instanceID);
            int[] tableIds = getTableIDs(potentialTableIds);            
            return tableIds;
        }

        /// <summary>
        /// Gets the tables i ds from module.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        /// <returns></returns>
        public int[] getTablesIDsFromModule(int instanceID)
        {
            HashSet<int> potentialTableIds = getPotentialTableIDsFromModule(instanceID);
            int[] tableIds = getTableIDs(potentialTableIds);
            return tableIds;
        }

        /// <summary>
        /// Gets the table i ds.
        /// </summary>
        /// <param name="potentialTableIds">The potential table ids.</param>
        /// <returns></returns>
        private int[] getTableIDs(HashSet<int> potentialTableIds)
        {
            string query = @"select TableID, TableCode 
                            from mTable t
                            where t.TableID in ({0})";
            StringBuilder condition = new StringBuilder(); bool first = true;
            foreach (int id in potentialTableIds)
            {
                if (!first) condition.Append(", ");
                condition.Append(id);
                if (first) first = false;
            }

            query = string.Format(query, condition.ToString());
            DataTable dt = _dataConnector.executeQuery(query);
            int[] tableIds = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
                tableIds[i] = int.Parse(dt.Rows[i]["TableID"].ToString());

            return tableIds;
        }

    }
}
