using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using SolvencyII.Data.CRT.ETL.ETLControllers;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction
{
    public class SQLiteInsertsExtractor
    {
        private DataConnectors.IDataConnector _dataConnector;
        private int _instanceId;

        public SQLiteInsertsExtractor(DataConnectors.IDataConnector dataConnector, int instanceId)
        {            
            this._dataConnector = dataConnector;
            this._instanceId = instanceId;
        }

        internal HashSet<CrtRow> extractInserts(HashSet<CrtMapping> mappings, string[] tableNames)
        {
            HashSet<CrtRow> inserts = new HashSet<CrtRow>();

            DataTable dt;
            string query;
            HashSet<CrtMapping> tableMappings;
            HashSet<CrtRow> tableInserts;
            foreach (string tableName in tableNames)
            {
                query = constructQuery(tableName);
                dt = _dataConnector.executeQuery(query);
                tableMappings = getTableMappings(mappings, tableName);
                try
                {
                    tableInserts = getTableInserts(tableMappings, dt);
                }
                catch (Exception ex)
                {
                    throw new EtlException("Exception while reading data from table " + tableName, ex);
                }

                foreach (CrtRow ins in tableInserts)                
                    inserts.Add(ins);

                ProgressHandler.EtlProgress(inserts.Count(), 0," extracted total rows");
            }
            
            return inserts;
        }

        private HashSet<CrtRow> getTableInserts(HashSet<CrtMapping> tableMappings, DataTable dt)
        {
            List<CrtMapping> allContextMapping = new List<CrtMapping>(tableMappings.Where(x => x.ORIGIN.Equals("C")));            
            HashSet<CrtMapping> factMapings = new HashSet<CrtMapping>(tableMappings.Where(x => x.ORIGIN.Equals("F")));
            HashSet<CrtRow> inserts = new HashSet<CrtRow>();
            CrtRow insert;

            CrtRowIdentification rowId;
            Queue<CrtMapping> contextMapings;
            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                contextMapings = getContextMappings(dr, allContextMapping);
                rowId = getRowIdentification(dr, contextMapings);
                insert = getInsert(dr, rowId, factMapings);
                insert.contextMappings = contextMapings;
                insert.factMapings = factMapings;
                inserts.Add(insert);

                if (++i%1000 == 0)
                    ProgressHandler.EtlProgress(i, 0, string.Format(" extracted rows for table {0}", rowId.TABLE_NAME));
            }
            
            return inserts;
        }

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

        private HashSet<CrtMapping> getTableMappings(HashSet<CrtMapping> mappings, string tableName)
        {
            var maps = mappings.Where(x => x.DYN_TABLE_NAME.Contains(tableName.Replace("T__", "")));
            return new HashSet<CrtMapping>(maps);
        }

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

        internal HashSet<CrtRow> extractInserts(HashSet<CrtMapping> mapings, string tableName, List<int> rowIds)
        {
            HashSet<CrtRow> inserts = new HashSet<CrtRow>();

            DataTable dt;
            string query;
            HashSet<CrtMapping> tableMappings;
            HashSet<CrtRow> tableInserts;

            query = constructQuery(tableName, rowIds);
            dt = _dataConnector.executeQuery(query);
            tableMappings = getTableMappings(mapings, tableName);
            tableInserts = getTableInserts(tableMappings, dt);

            foreach (CrtRow ins in tableInserts)
                inserts.Add(ins);

            ProgressHandler.EtlProgress(inserts.Count(), 0, " extracted total rows");
            

            return inserts;
        }

        
    }
}
