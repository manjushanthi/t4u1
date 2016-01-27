using SolvencyII.Data.CRT.ETL.DataConnectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.MappingControllers;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers
{
    /// <summary>
    /// Reader of SQL database data points
    /// </summary>
    public class SQLiteDataPointsReader
    {
        SQLiteConnector _dataConnector;
        SQLiteMappingProvider _mappingProvider;
        DataPointMappingAnalyzer _mappingAnalyzer;
        HashSet<CrtMapping> _mapings;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDataPointsReader"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        SQLiteDataPointsReader(SQLiteConnector dataConnector)
        {
            _dataConnector = dataConnector;
            _mappingProvider = new SQLiteMappingProvider(_dataConnector);
            _mappingAnalyzer = new DataPointMappingAnalyzer();
        }

        /// <summary>
        /// Saves all data points.
        /// </summary>
        /// <param name="dbFilePath">The database file path.</param>
        void SaveAllDataPoints(string dbFilePath)
        {
            _mapings = _mappingProvider.getMappings();
            _mappingAnalyzer.SetMappingsSet(_mapings);
            SQLiteDataPointsLoader dpl = new SQLiteDataPointsLoader(_dataConnector);
            dpl.LoadDataPoints(_mappingAnalyzer.DataPointToRowMapings);
        }
    }
}
