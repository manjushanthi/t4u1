using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction
{
    /// <summary>
    /// Interface ogf 
    /// </summary>
    public interface ITableNamesExtractor
    {
        /// <summary>
        /// Gets the table names.
        /// </summary>
        /// <param name="potentialTableIds">The potential table ids.</param>
        /// <returns></returns>
        string[] getTableNames(IEnumerable<int> potentialTableIds);
        /// <summary>
        /// Gets the tables codes.
        /// </summary>
        /// <param name="tableIDs">The table i ds.</param>
        /// <returns></returns>
        System.Collections.Generic.Dictionary<int, string> getTablesCodes(int[] tableIDs);
    }
}
