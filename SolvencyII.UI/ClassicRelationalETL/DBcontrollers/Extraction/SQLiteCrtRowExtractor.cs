using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.Model;
using System.Collections;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction
{
    /// <summary>
    /// Extractor of the CRT data rows from SQL database
    /// </summary>
    public class SQLiteCrtRowExtractor
    {
        private DataConnectors.IDataConnector _dataConnector;
        private int _instanceId;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteCrtRowExtractor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <param name="instanceId">The instance identifier.</param>
        public SQLiteCrtRowExtractor(DataConnectors.IDataConnector dataConnector, int instanceId)
        {            
            this._dataConnector = dataConnector;
            this._instanceId = instanceId;
        }

        /// <summary>
        /// Extracts the inserts.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        /// <param name="tableNames">The table names.</param>
        /// <returns></returns>
        /// <exception cref="SolvencyII.Data.CRT.ETL.EtlException">Exception while reading data from table  + tableName</exception>
        internal IEnumerable<CrtRow> extractInserts(HashSet<CrtMapping> mappings, string[] tableNames)
        {
            HashSet<CrtRow> inserts = new HashSet<CrtRow>();
            DataTable dt;
            string query;
            HashSet<CrtMapping> tableMappings;
            List<CrtRow> crtTableRows;
            foreach (string tableName in tableNames)
            {
                query = constructQuery(tableName);
                dt = _dataConnector.executeQuery(query);
                tableMappings = getTableMappings(mappings, tableName);
                crtTableRows = getTableRows(dt, tableMappings, tableName);

                foreach (CrtRow ins in crtTableRows)
                    inserts.Add(ins);

                ProgressHandler.EtlProgress(inserts.Count(), 0, " extracted total rows");
            }

            return inserts;
        }                      

        private List<CrtRow> getTableRows(DataTable dt, HashSet<CrtMapping> tableMappings, string tableName)
        {
            List<CrtRow> tableInserts;
            try
            {
                tableInserts = getTableInserts(tableMappings, dt, tableName);
            }
            catch (Exception ex)
            {
                throw new EtlException("Exception while reading data from table " + tableName, ex);
            }
            return tableInserts;
        }

        /// <summary>
        /// Gets the table inserts.
        /// </summary>
        /// <param name="tableMappings">The table mappings.</param>
        /// <param name="dt">The dt.</param>
        /// <returns></returns>
        private List<CrtRow> getTableInserts(HashSet<CrtMapping> tableMappings, DataTable dt, string tableName)
        {
            List<CrtMapping> allContextMapping = new List<CrtMapping>(tableMappings.Where(x => x.ORIGIN.Equals("C")));            
            HashSet<CrtMapping> factMapings = new HashSet<CrtMapping>(tableMappings.Where(x => x.ORIGIN.Equals("F")));
            List<CrtRow> inserts = new List<CrtRow>();
            CrtRow insert;

            CrtRowIdentification rowId;
            Queue<CrtMapping> contextMapings;
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                contextMapings = getContextMappings(dr, allContextMapping);
                rowId = getRowIdentification(dr, contextMapings);
                insert = getInsert(dr, rowId, factMapings);
                insert.rowIdentification.TABLE_NAME = tableName;
                insert.contextMappings = contextMapings;
                insert.factMapings = factMapings;
                inserts.Add(insert);

                if (++i%1000 == 0)
                    ProgressHandler.EtlProgress(i, 0, string.Format(" extracted rows for table {0}", rowId.TABLE_NAME));
            }
            
            return inserts;
        }

        /// <summary>
        /// Gets the context mappings.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="allContextMapping">All context mapping.</param>
        /// <returns></returns>
        private Queue<CrtMapping> getContextMappings(DataRow dr, List<CrtMapping> allContextMapping)
        {
            List<CrtMapping> contextMappings = new List<CrtMapping>();
            string colVal;

            foreach (var item in allContextMapping.Where(x => !x.IS_IN_TABLE))
                contextMappings.Add(item);

            foreach (DataColumn dc in dr.Table.Columns)
            {
                colVal = dr[dc.ColumnName].ToString();
                //colVal = colVal;
                foreach (CrtMapping mapping in allContextMapping)
                {
                    if (!dc.ColumnName.ToUpper().Equals(mapping.DYN_TAB_COLUMN_NAME.ToUpper()))
                        continue;

                    if ((mapping.MEM_CODE.Equals(colVal) && !mapping.IS_DEFAULT )
                        || (mapping.IS_PAGE_COLUMN_KEY!=1 && mapping.DIM_CODE.Contains("*")))
                    {
                        contextMappings.Add(mapping);
                        break;
                    }
                }
            }

            Queue<CrtMapping> result = new Queue<CrtMapping>(contextMappings.OrderBy(x => x.DIM_CODE));
            contextMappings.Clear();
            contextMappings = null;
            return result;
        }

        /// <summary>
        /// Gets the insert.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="rowId">The row identifier.</param>
        /// <param name="factMapings">The fact mapings.</param>
        /// <returns></returns>
        private CrtRow getInsert(DataRow dr, CrtRowIdentification rowId, HashSet<CrtMapping> factMapings)
        {
            Dictionary<string, object> rcValues = new Dictionary<string, object>();
            string value;
            foreach (string dyntableName in factMapings.Select(x=>x.DYN_TAB_COLUMN_NAME).Distinct())
            {
                value = dr[dyntableName].ToString();
                if(!string.IsNullOrEmpty(value))
                    rcValues.Add(dyntableName, value);
            }                    
                        
            CrtRow insert = new CrtRow(rowId, rcValues);
            return insert;
        }

        /// <summary>
        /// Gets the row identification.
        /// </summary>
        /// <param name="dr">The dr.</param>
        /// <param name="contextMapings">The context mapings.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">No data row</exception>
        private CrtRowIdentification getRowIdentification(DataRow dr, Queue<CrtMapping> contextMapings)
        {
            if(dr == null)
                throw new ArgumentNullException("No data row");

            OrderedDictionary dict = new OrderedDictionary();
            foreach (CrtMapping cMap in contextMapings)
                if (cMap.IS_IN_TABLE)
                    dict.Add(cMap.DYN_TAB_COLUMN_NAME, dr[cMap.DYN_TAB_COLUMN_NAME].ToString());            
            
            CrtRowIdentification ri = new CrtRowIdentification(dr.Table.TableName, this._instanceId, dict);
            return ri;
        }

        /// <summary>
        /// Gets the table mappings.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        private HashSet<CrtMapping> getTableMappings(HashSet<CrtMapping> mappings, string tableName)
        {
            var maps = mappings.Where(x => x.DYN_TABLE_NAME.Contains(tableName.Replace("T__", "")));
            return new HashSet<CrtMapping>(maps);
        }

        /// <summary>
        /// Constructs the query.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        private string constructQuery(string tableName)
        {
            Dictionary<string, bool> colToCast = getTableColumns(tableName);
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(constructColumnNames(colToCast));
            builder.Append(" from [");
            builder.Append(tableName);
            builder.Append("] where INSTANCE = ");
            builder.Append(this._instanceId);
            return builder.ToString();
        }

        /// <summary>
        /// Constructs the query.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="rowIds">The row ids.</param>
        /// <returns></returns>
        private string constructQuery(string tableName, List<int> rowIds)
        {
            Dictionary<string, bool> colToCast = getTableColumns(tableName);
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            builder.Append(constructColumnNames(colToCast));
            builder.Append(" from [");
            builder.Append(tableName);
            builder.Append("] where INSTANCE = ");
            builder.Append(this._instanceId);
            builder.Append(string.Format(" AND PK_ID BETWEEN {0} AND {1} ", rowIds[0], rowIds[rowIds.Count - 1]));
            return builder.ToString();
        }

        /// <summary>
        /// Constructs the column names.
        /// </summary>
        /// <param name="colToCast">The col to cast.</param>
        /// <returns></returns>
        private string constructColumnNames(Dictionary<string, bool> colToCast)
        {
            bool areColumnsToCast = colToCast.Where(x => x.Value).Count() > 0;
            if(!areColumnsToCast)            
                return "*";

            StringBuilder result = new StringBuilder();
            bool first = true;
            foreach (var kvp in colToCast)
            {
                if (first)
                    first = false;
                else
                    result.Append(", ");

                if (!kvp.Value)
                    result.Append(kvp.Key);
                else
                {
                    result.Append("cast(");
                    result.Append(kvp.Key);
                    result.Append(" as varchar) as ");
                    result.Append(kvp.Key);
                }
            }

            return result.ToString();
        }

        /// <summary>
        /// Gets the table columns.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        internal Dictionary<string, bool> getTableColumns(string tableName)
        {
            string query = string.Format("pragma table_info({0});", tableName);
            DataTable dt = _dataConnector.executeQuery(query);
            string colName;
            bool requiresCast;
            string type;
            Dictionary<string, bool> result = new Dictionary<string, bool>();
            foreach (DataRow dr in dt.Rows)
            {
                colName = dr["name"].ToString();
                type = dr["type"].ToString();
                requiresCast = !(type.StartsWith("INT") || type.StartsWith("VAR") || type.StartsWith("NUM"));
                result.Add(colName, requiresCast);
            }
            return result;
        }

        /// <summary>
        /// Extracts the inserts.
        /// </summary>
        /// <param name="mapings">The mapings.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="rowIds">The row ids.</param>
        /// <returns></returns>
        internal List<CrtRow> extractInserts(HashSet<CrtMapping> mapings, string tableName, List<int> rowIds)
        {
            List<CrtRow> inserts = new List<CrtRow>();

            DataTable dt;
            string query;
            HashSet<CrtMapping> tableMappings;
            List<CrtRow> tableInserts;

            query = constructQuery(tableName, rowIds);
            dt = _dataConnector.executeQuery(query);
            tableMappings = getTableMappings(mapings, tableName);
            tableInserts = getTableInserts(tableMappings, dt, tableName);

            foreach (CrtRow ins in tableInserts)
                inserts.Add(ins);

            ProgressHandler.EtlProgress(inserts.Count(), 0, " extracted total rows");
            

            return inserts;
        }

        
    }
}
