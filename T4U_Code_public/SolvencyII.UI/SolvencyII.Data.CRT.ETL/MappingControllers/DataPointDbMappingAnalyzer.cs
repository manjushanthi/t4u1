using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.MappingControllers
{
    /// <summary>
    /// Mapping analyzer that stores data points in seperate database
    /// </summary>
    public class DataPointDbMappingAnalyzer : IMappingAnalyzer
    {
        private Dictionary<string, List<ColumnMapping>> dataPointToRowMapings = new Dictionary<string, List<ColumnMapping>>();
        bool _generateDataPoints = false;
        IDataConnector _dataConnector;
        HashSet<CrtMapping> _mappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataPointDbMappingAnalyzer"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <param name="generateDataPoints">if set to <c>true</c> [generate data points].</param>
        public DataPointDbMappingAnalyzer(IDataConnector dataConnector, bool generateDataPoints)
        {
            _generateDataPoints = generateDataPoints;
            _dataConnector = dataConnector;
        }

        /// <summary>
        /// Gets the fact mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">No fact</exception>
        public HashSet<CrtMapping> getFactMappings(dFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("No fact");

            List<ColumnMapping> colMaps;
            string dpCode = fact.GetDataPointSignature();

            if(!dataPointToRowMapings.TryGetValue(dpCode, out colMaps) 
               && !getDataPointFromDatabase(dpCode, out colMaps))
                return new HashSet<CrtMapping>();

            HashSet<CrtMapping> result = new HashSet<CrtMapping>();
            foreach (ColumnMapping colMap in colMaps)
                foreach (CrtMapping map in getMappingsForColumnMapping(colMap, fact.DimCodes))
                    result.Add(map);

            return result;
        }

        /// <summary>
        /// Gets the mappings for column mapping.
        /// </summary>
        /// <param name="colMap">The col map.</param>
        /// <param name="dimCodes">The dim codes.</param>
        /// <returns></returns>
        private List<CrtMapping> getMappingsForColumnMapping(ColumnMapping colMap, HashSet<string> dimCodes)
        {
            List<CrtMapping> result = new List<CrtMapping>();

            IEnumerable<CrtMapping> tabMapps = _mappings.Where(x => x.DYN_TABLE_NAME.Equals(colMap.DYN_TABLE_NAME));
            IEnumerable<CrtMapping> columnMappings = tabMapps.Where(x => x.DYN_TAB_COLUMN_NAME.Equals(colMap.DYN_TAB_COLUMN_NAME));
            IEnumerable<CrtMapping> pageMappings = tabMapps.Where(x => x.ORIGIN.Equals("C"));

            foreach (CrtMapping columnMap in columnMappings)            
                if (columnMap.IS_PAGE_COLUMN_KEY != 1 && dimCodes.Contains(columnMap.DIM_CODE))
                    result.Add(columnMap);

            foreach (CrtMapping pageMap in pageMappings)
                if (pageMap.IS_PAGE_COLUMN_KEY != 1 && dimCodes.Contains(pageMap.DIM_CODE))
                    result.Add(pageMap);

            columnMappings = null;
            pageMappings = null;
            return result;
        }

        /// <summary>
        /// Gets the data point from database.
        /// </summary>
        /// <param name="dataPointCode">The data point code.</param>
        /// <param name="colMaps">The col maps.</param>
        /// <returns></returns>
        private bool getDataPointFromDatabase(string dataPointCode, out List<ColumnMapping> colMaps)
        {
            colMaps = null;
            if (string.IsNullOrEmpty(dataPointCode))            
                return false;

            string query = string.Format(@"select *
                                            from DATA_POINTS
                                            where DP_CODE = '{0}'", dataPointCode);

            DataTable dt = _dataConnector.executeQuery(query);
            if (dt.Rows.Count == 0 || dt.Rows.Count > 1)
                return false;

            colMaps = parseColMaps(dt.Rows[0]["TAB_COLS"].ToString());
            if (!dataPointToRowMapings.ContainsKey(dataPointCode))
                dataPointToRowMapings.Add(dataPointCode, colMaps);

            return true;
        }

        /// <summary>
        /// Parses the col maps.
        /// </summary>
        /// <param name="tabCols">The tab cols.</param>
        /// <returns></returns>
        private List<ColumnMapping> parseColMaps(string tabCols)
        {
            List<ColumnMapping> result = new List<ColumnMapping>();
            int colIdx = 0;
            ColumnMapping colMap;
            string tabName, colName;
            foreach (string tabCol in tabCols.Split(new char[]{'|'}, StringSplitOptions.RemoveEmptyEntries))
            {
                colIdx = tabCol.LastIndexOf('_');
                tabName = tabCol.Substring(0, colIdx);
                colName = tabCol.Substring(colIdx + 1);
                colMap = new ColumnMapping(tabName, colName);
                result.Add(colMap);
            }
            return result;
        }

        /// <summary>
        /// Sets the mappings set.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        public void SetMappingsSet(HashSet<CrtMapping> mappings)
        {
            _mappings = mappings;
            if(!_generateDataPoints)
                return;

            DataPointMappingAnalyzer _mappingAnalyzer = new DataPointMappingAnalyzer();
            _mappingAnalyzer.SetMappingsSet(mappings);
            this.dataPointToRowMapings = _mappingAnalyzer.DataPointToRowMapings;
        }


        /// <summary>
        /// Cleans the mappings.
        /// </summary>
        public void CleanMappings()
        {
            if(_mappings != null) _mappings.Clear();
            if (dataPointToRowMapings != null) dataPointToRowMapings.Clear();
        }
    }
}
