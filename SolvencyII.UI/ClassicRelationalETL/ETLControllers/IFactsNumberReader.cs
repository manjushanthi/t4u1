using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.ETLControllers
{
    /// <summary>
    /// Checks number of facts in instance file.
    /// </summary>
    public interface IFactsNumberReader
    {
        /// <summary>
        /// Gets the tables numbers.
        /// </summary>
        /// <returns></returns>
        List<TableFacts> GetTablesNumbers();
    }

    /// <summary>
    /// Number of facts that appear in żarticular instance in single table
    /// </summary>
    public class TableFacts
    {
        internal string TableName;
        internal int factsNumber;
        internal List<int> rowsIds;
    }
}
