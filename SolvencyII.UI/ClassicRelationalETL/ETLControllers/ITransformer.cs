using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SolvencyII.Data.CRT.ETL.Model;

namespace SolvencyII.Data.CRT.ETL.ETLControllers
{
    /// <summary>
    /// Transofmree of dFacts and CRT rows
    /// </summary>
    public interface ITransformer
    {
        /// <summary>
        /// Transforms the facts.
        /// </summary>
        /// <param name="facts">The facts.</param>
        /// <returns></returns>
        HashSet<CrtRow> transformFacts(HashSet<dFact> facts);
        /// <summary>
        /// Transforms the inserts.
        /// </summary>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        HashSet<dFact> transformInserts(HashSet<CrtRow> inserts);
                
        /// <summary>
        /// Cancels this instance.
        /// </summary>
        void Cancel();

        /// <summary>
        /// Loads the mappings.
        /// </summary>
        void loadMappings();

        /// <summary>
        /// Gets the maping provider.
        /// </summary>
        /// <returns></returns>
        SQLiteMappingProvider getMapingProvider();
    }
}
