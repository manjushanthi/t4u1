using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction;
using SolvencyII.Data.CRT.ETL.MappingControllers;
using SolvencyII.Data.CRT.ETL.DataConnectors;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Mapping provder of SQLite database
    /// </summary>
    public class SQLiteMappingProvider : IMappingProvider
    {
        protected IDataConnector _dataConnector;

        protected IMappingAnalyzer _mappingAnalyzer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteMappingProvider"/> class.
        /// </summary>
        /// <param name="_dataConnector">The _data connector.</param>
        /// <exception cref="System.ArgumentNullException">No data connector</exception>
        public SQLiteMappingProvider(IDataConnector _dataConnector)
        {
            if (_dataConnector == null)
                throw new ArgumentNullException("No data connector");

            this._dataConnector = _dataConnector;

            _mappingAnalyzer = new DataPointMappingAnalyzer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteMappingProvider"/> class.
        /// </summary>
        /// <param name="_dataConnector">The _data connector.</param>
        /// <param name="mappingAnalyzer">The mapping analyzer.</param>
        /// <exception cref="System.ArgumentNullException">No data connector</exception>
        public SQLiteMappingProvider(DataConnectors.IDataConnector _dataConnector, IMappingAnalyzer mappingAnalyzer)
        {
            if (_dataConnector == null)
                throw new ArgumentNullException("No data connector");

            this._dataConnector = _dataConnector;

            _mappingAnalyzer = mappingAnalyzer;
        }

        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        internal HashSet<CrtMapping> getMappings(Model.dFact fact)
        {
            return getFactMappings(fact);
        }

        protected HashSet<CrtMapping> _allMappingsHashSet;

        /// <summary>
        /// Gets the fact mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        private HashSet<CrtMapping> getFactMappings(dFact fact)
        {
            if (_allMappingsHashSet == null)
            {
                List<string> mTableIds = getInstanceTables(fact.instanceId);
                if(mTableIds.Count == 0)
                    getAllMappingHashSet();
                else
                    getAllMappingHashSet(mTableIds);
            }

            HashSet<CrtMapping> factMappings = new HashSet<CrtMapping>();
            factMappings = _mappingAnalyzer.getFactMappings(fact);
            return factMappings;
        }

        /// <summary>
        /// Gets all mapping hash set.
        /// </summary>
        /// <param name="mTableIds">The m table ids.</param>
        public virtual void getAllMappingHashSet(List<string> mTableIds)
        {
            this._allMappingsHashSet = getMappings(mTableIds.ToArray());
            _mappingAnalyzer.SetMappingsSet(this._allMappingsHashSet);
        }

        /// <summary>
        /// Gets the instance tables.
        /// </summary>
        /// <param name="instanceId">The instance identifier.</param>
        /// <returns></returns>
        public List<string> getInstanceTables(int instanceId)
        {
            IFilinglIndicatorsExtractor _indicatorsExtractor = new NewSQLiteFillIndicatorsExtractor(_dataConnector);
            List<string> result = new List<string>(_indicatorsExtractor.getTablesNamesFromFillingIndicators(instanceId));

            if (result == null || result.Count == 0)
                result = new List<string>(_indicatorsExtractor.getTablesNamesFromModule(instanceId));

            return result;
        }

        /// <summary>
        /// Gets all mapping hash set.
        /// </summary>
        public void getAllMappingHashSet()
        {
            _allMappingsHashSet = queryMappings(this.allMapingsQuery);
            _mappingAnalyzer.SetMappingsSet(this._allMappingsHashSet);
            return;            
        }

        /// <summary>
        /// Queries the mappings.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="withPageColumnKey">if set to <c>true</c> [with page column key].</param>
        /// <returns></returns>
        public HashSet<CrtMapping> queryMappings(string query, bool withPageColumnKey = false)
        {
            DataTable dt = _dataConnector.executeQuery(query);
            HashSet<CrtMapping> mappings = new HashSet<CrtMapping>();
            bool areDomains = dt.Columns.Contains("DOM_CODE");
            ProgressHandler.EtlProgress(0,0, " reading mapping" );
            int i = 0;
            CrtMapping newMap;

            foreach (DataRow dr in dt.Rows)
            {
                if (int.Parse(dr["IS_PAGE_COLUMN_KEY"].ToString()) == 1 && !withPageColumnKey)
                    continue;

                newMap = new CrtMapping
                {
                    DYN_TABLE_NAME = dr["DYN_TABLE_NAME"].ToString(),
                    DYN_TAB_COLUMN_NAME = dr["DYN_TAB_COLUMN_NAME"].ToString(),
                    ORIGIN = dr["ORIGIN"].ToString(),
                    REQUIRED_MAPPINGS = int.Parse(dr["REQUIRED_MAPPINGS"].ToString()),
                    PAGE_COLUMNS_NUMBER = int.Parse(dr["PAGE_COLUMNS_NUMBER"].ToString()),
                    DIM_CODE = dr["DIM_CODE"].ToString().Replace(EtlGlobals.AtyDimCode, EtlGlobals.MetDimCode).Replace(EtlGlobals.AtDomPrefix, EtlGlobals.MetDomPrefix),
                    IS_IN_TABLE = dr["IS_IN_TABLE"].ToString().Contains("1"),
                    DATA_TYPE = dr["DATA_TYPE"].ToString(),
                    DOM_CODE = areDomains ? dr["DOM_CODE"].ToString() : "",
                    MEM_CODE = dr["MEM_CODE"].ToString(),
                    TABLE_VERSION_ID = int.Parse(dr["TABLE_VERSION_ID"].ToString()),
                    IS_PAGE_COLUMN_KEY = int.Parse(dr["IS_PAGE_COLUMN_KEY"].ToString())
                };
                if (dt.Columns.Contains("IS_DEFAULT"))
                    newMap.IS_DEFAULT = dr["IS_DEFAULT"].ToString().Contains("1");

                mappings.Add(newMap);
                if(++i%1000 == 0)
                    ProgressHandler.EtlProgress(i, 0, " reading mapping");
            }
            ProgressHandler.EtlProgress(i, i, " reading mapping");
            return mappings;
        }

        /// <summary>
        /// Formats the codes.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        private string FormatCodes(Model.dFact fact)
        {
            string dpQuery = "";
            dpQuery = dpQuery + (string.IsNullOrEmpty(fact.metCode) ? "" : "'" + fact.metCode + "',");

            foreach (KeyValuePair<string, string> kvp in fact.dimensionsMembers.OrderBy(x => x.Key))
            {
                if (kvp.Value.Contains("<"))
                {
                    dpQuery = dpQuery + " '" + kvp.Key + "(*)',";
                    continue;
                }
                dpQuery = dpQuery + " '" + kvp.Key + "(" + kvp.Value + ")" + "',";
            }
            dpQuery = dpQuery.Substring(0, dpQuery.Length - 1);
            return dpQuery;
        }  
               
        string allMapingsQuery = @"select * from MAPPING m;";

        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <param name="tableNames">The table names.</param>
        /// <returns></returns>
        public HashSet<CrtMapping> getMappings(string[] tableNames)
        {
            string query = "select * from MAPPING m where m.DYN_TABLE_NAME in (";

            StringBuilder builder = new StringBuilder();
            builder.Append(query);
            int max = tableNames.Count();
            for (int i = 0; i < max; i++)
            {
                builder.Append("'");
                builder.Append(tableNames[i].Replace("T__", ""));
                builder.Append("'");
                if (i < (max - 1)) builder.Append(", ");
            }
            builder.Append(")");

            return this.queryMappings(builder.ToString());
        }

        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <returns></returns>
        public HashSet<CrtMapping> getMappings()
        {
            string query = "select * from MAPPING m";
            return this.queryMappings(query);
        }

        /// <summary>
        /// Cleans the mappings.
        /// </summary>
        public void CleanMappings()
        {
            if(_allMappingsHashSet != null)
            this._allMappingsHashSet.Clear();
            
            if(_mappingAnalyzer != null)
                this._mappingAnalyzer.CleanMappings();
        }

        /// <summary>
        /// Replicates this instance.
        /// </summary>
        /// <returns></returns>
        internal SQLiteMappingProvider Replicate()
        {
            return new SQLiteMappingProvider(this._dataConnector, this._mappingAnalyzer);
        }
    }
}