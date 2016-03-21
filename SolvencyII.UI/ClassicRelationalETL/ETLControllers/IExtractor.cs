using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.Model;

namespace SolvencyII.Data.CRT.ETL.ETLControllers
{
    /// <summary>
    /// Extracts dfacts and CRT rows fomr database
    /// </summary>
    public interface IExtractor
    {
        /// <summary>
        /// Exctracts the facts.
        /// </summary>
        /// <returns></returns>
        HashSet<dFact> exctractFacts();
        /// <summary>
        /// Extracts the inserts.
        /// </summary>
        /// <returns></returns>
        HashSet<CrtRow> extractInserts();

        /// <summary>
        /// Gets the facts number.
        /// </summary>
        /// <returns></returns>
        int getFactsNumber();
        /// <summary>
        /// Cancels this instance.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Gets the total facts number.
        /// </summary>
        /// <param name="maxFactId">The maximum fact identifier.</param>
        /// <param name="minfactId">The minfact identifier.</param>
        /// <returns></returns>
        int getTotalFactsNumber(out int maxFactId, out int minfactId);
        /// <summary>
        /// Gets the facts number.
        /// </summary>
        /// <param name="maxFactId">The maximum fact identifier.</param>
        /// <param name="minfactId">The minfact identifier.</param>
        /// <returns></returns>
        int getFactsNumber(out int maxFactId, out int minfactId);

        /// <summary>
        /// Exctracts the facts.
        /// </summary>
        /// <param name="minfactId">The minfact identifier.</param>
        /// <param name="p">The p.</param>
        /// <returns></returns>
        HashSet<dFact> exctractFacts(int minfactId, int p);

        /// <summary>
        /// Checks the and add fact identifier column.
        /// </summary>
        void checkAndAddFactIdColumn();

        /// <summary>
        /// Gets the instnace identifier.
        /// </summary>
        /// <returns></returns>
        int getInstnaceId();

        /// <summary>
        /// Extracts the inserts.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="rowIds">The row ids.</param>
        /// <returns></returns>
        HashSet<CrtRow> extractInserts(string tableName, List<int> rowIds);
    }
}
