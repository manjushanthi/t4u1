using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL.MappingControllers
{
    /// <summary>
    /// Interrface of mapping analyzers
    /// </summary>
    public interface IMappingAnalyzer
    {
        /// <summary>
        /// Gets the fact mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        HashSet<Model.CrtMapping> getFactMappings(Model.dFact fact);

        /// <summary>
        /// Sets the mappings set.
        /// </summary>
        /// <param name="hashSet">The hash set.</param>
        void SetMappingsSet(HashSet<Model.CrtMapping> hashSet);

        /// <summary>
        /// Cleans the mappings.
        /// </summary>
        void CleanMappings();
    }
}
