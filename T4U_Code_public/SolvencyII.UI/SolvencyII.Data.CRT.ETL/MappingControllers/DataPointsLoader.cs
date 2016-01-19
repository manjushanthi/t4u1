using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.MappingControllers
{
    /// <summary>
    /// Loads data points to sqldatabase
    /// </summary>
    public class SQLiteDataPointsLoader
    {
        IDataConnector connector;
        private string tableDDL = @"
                    CREATE TABLE DATA_POINTS ( 
                        DP_ID    INTEGER PRIMARY KEY AUTOINCREMENT,
                        DP_HASH  INTEGER,
                        DP_CODE  VARCHAR UNIQUE,
                        TAB_COLS VARCHAR 
                    );

                    CREATE INDEX idx_DATA_POINTS ON DATA_POINTS ( 
                        DP_HASH ASC 
                    );

                    CREATE INDEX idx_DATA_POINTS2 ON DATA_POINTS ( 
                        DP_CODE 
                    );";

        private string dropTable = @"drop table DATA_POINTS;";

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteDataPointsLoader"/> class.
        /// </summary>
        /// <param name="connector">The connector.</param>
        public SQLiteDataPointsLoader(IDataConnector connector)
        {
            this.connector = connector;
        }

        /// <summary>
        /// Loads the data points.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public void LoadDataPoints(Dictionary<string, List<Model.ColumnMapping>> dictionary)
        {
            try
            {
                connector.executeNonQuery(dropTable);
            }
            catch (Exception)
            {
            }
            connector.executeNonQuery(tableDDL);

            int hash;
            string dpCode;
            string tabCols;
            string query;
            int i = 0;
            foreach (var kvp in dictionary)
            {
                dpCode = kvp.Key;
                hash = dpCode.GetHashCode();
                tabCols = getTabColsString(kvp.Value);
                query = constructInsert(hash, dpCode, tabCols);
                connector.executeNonQuery(query);
                if(++i%10000 == 0) Console.WriteLine("{0} :{1}", DateTime.Now, i);
            }
        }

        /// <summary>
        /// Constructs the insert.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <param name="dpCode">The dp code.</param>
        /// <param name="tabCols">The tab cols.</param>
        /// <returns></returns>
        private string constructInsert(int hash, string dpCode, string tabCols)
        {
            StringBuilder build = new StringBuilder();
            build.Append(@"INSERT INTO DATA_POINTS(DP_HASH, DP_CODE, TAB_COLS) values
                     (");
            //1, 'A', 'B');
            build.Append(hash);
            build.Append(", '");
            build.Append(dpCode);
            build.Append("', '");
            build.Append(tabCols);
            build.Append("');");

            return build.ToString();
        }

        /// <summary>
        /// Gets the tab cols string.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <returns></returns>
        private string getTabColsString(List<Model.ColumnMapping> list)
        {
            StringBuilder builder = new StringBuilder();

            foreach (ColumnMapping item in list)
            {
                builder.Append(item.DYN_TABLE_NAME);
                builder.Append("_");
                builder.Append(item.DYN_TAB_COLUMN_NAME);
                builder.Append("|");
            }

            return builder.ToString();
        }
    }
}
