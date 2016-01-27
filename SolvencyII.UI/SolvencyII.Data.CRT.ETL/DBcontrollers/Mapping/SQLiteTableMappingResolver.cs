using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using System.Data;
using SolvencyII.Data.CRT.ETL.Model;

namespace SolvencyII.Data.CRT.ETL.MappingControllers
{
    /// <summary>
    /// Table mapping resolver from SQLite database
    /// </summary>
    public class SQLiteTableMappingResolver : ITableMappingResolver
    {
        private IDataConnector _dataConnector;
        private HashSet<int> _tablesForDimByDimAnalyzer;
        private HashSet<int> _tablesForDataPointAnalyzer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteTableMappingResolver"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <exception cref="System.ArgumentNullException">No connector specified</exception>
        public SQLiteTableMappingResolver(IDataConnector dataConnector)
        {
            if(dataConnector == null) throw new ArgumentNullException("No connector specified");

            _dataConnector = dataConnector;
        }

        /// <summary>
        /// Resolves the specified maping.
        /// </summary>
        /// <param name="maping">The maping.</param>
        /// <returns></returns>
        public HowHandleEnum resolve(CrtMapping maping)
        {
            if (_tablesForDataPointAnalyzer == null)
                _tablesForDataPointAnalyzer = getTablesForDataPointAnalyzer();
            if (_tablesForDimByDimAnalyzer == null)
                _tablesForDimByDimAnalyzer = getTablesForDimByDimAnalyzer();

            if (_tablesForDataPointAnalyzer.Contains(maping.TABLE_VERSION_ID))
                return HowHandleEnum.DataPoint;
            if (_tablesForDimByDimAnalyzer.Contains(maping.TABLE_VERSION_ID))
                return HowHandleEnum.DimByDim;

            return HowHandleEnum.None;
        }

        /// <summary>
        /// Gets the tables for data point analyzer.
        /// </summary>
        /// <returns></returns>
        private HashSet<int> getTablesForDataPointAnalyzer()
        {
            string query = @"select distinct t.TableID
from mTable t
    inner join mTableAxis ta on t.TableID = ta.TableID
    inner join mAxis a on a.AxisID = ta.AxisID
    left join mOpenAxisValueRestriction oxa on oxa.AxisID = a.AxisID
where a.IsOpenAxis = 1 and oxa.HierarchyID is null";

            return tableIDs(query);
        }

        /// <summary>
        /// Gets the tables for dim by dim analyzer.
        /// </summary>
        /// <returns></returns>
        private HashSet<int> getTablesForDimByDimAnalyzer()
        {
            string query = @"select distinct t.TableID
from mTable t    
where not exists (select 1 
                    from  mTableAxis ta
                    inner join mAxis a on a.AxisID = ta.AxisID 
                    left join mOpenAxisValueRestriction oxa on oxa.AxisID = a.AxisID
                    where t.TableID = ta.TableID 
                        and a.IsOpenAxis = 1
                        and oxa.HierarchyID is null)";

            return tableIDs(query);
        }

        /// <summary>
        /// Tables the i ds.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        private HashSet<int> tableIDs(string query)
        {
            DataTable dt = _dataConnector.executeQuery(query);

            HashSet<int> result = new HashSet<int>();
            foreach (DataRow dr in dt.Rows)
                result.Add(int.Parse(dr[0].ToString()));

            return result;
        }
    }
}
