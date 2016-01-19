using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.Model;

namespace SolvencyII.Data.CRT.ETL.ETLControllers
{
    /// <summary>
    /// Loads dfacts and CRT rows into database
    /// </summary>
    public interface ILoader
    {
        /// <summary>
        /// Loads the inserts.
        /// </summary>
        /// <param name="inserts">The inserts.</param>
        void loadInserts(HashSet<CrtRow> inserts);

        /// <summary>
        /// Loads the facts.
        /// </summary>
        /// <param name="inserts">The inserts.</param>
        void loadFacts(HashSet<dFact> inserts);
        /// <summary>
        /// Cleans the d facts.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        void CleanDFacts(int instanceID);
        /// <summary>
        /// Cancels this instance.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Opens the connection.
        /// </summary>
        void openConnection();

        /// <summary>
        /// Closes the connection.
        /// </summary>
        void closeConnection();
    }
}
