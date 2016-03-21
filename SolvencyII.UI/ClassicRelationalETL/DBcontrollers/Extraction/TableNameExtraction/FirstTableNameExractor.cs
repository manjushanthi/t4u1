using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction
{
    /// <summary>
    /// Obsolete table names extractor
    /// </summary>
    internal class FirstTableNameExractor : ITableNamesExtractor
    {
        IDataConnector _dataConnector;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstTableNameExractor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        internal FirstTableNameExractor(IDataConnector dataConnector)
        {
            _dataConnector = dataConnector;
        }

        /// <summary>
        /// Gets the table names.
        /// </summary>
        /// <param name="potentialTableIds">The potential table ids.</param>
        /// <returns></returns>
        public string[] getTableNames(IEnumerable<int> potentialTableIds)
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
            string[] tabNames = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
                tabNames[i] = dt.Rows[i]["TableID"].ToString() + "_" + dt.Rows[i]["TableCode"].ToString().Replace(".", "_").Replace(" ", "_");

            return tabNames;
        }

        /// <summary>
        /// Gets the tables codes. Not implemented
        /// </summary>
        /// <param name="tableIDs">The table i ds.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Dictionary<int, string> getTablesCodes(int[] tableIDs)
        {
            throw new NotImplementedException();
        }
    }
}
