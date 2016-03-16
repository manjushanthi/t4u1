using SolvencyII.Data.CRT.ETL.ETLControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.EtlPerfomers
{
    /// <summary>
    /// ETl performer that performs regular stepping ETL but for each table seperately.
    /// </summary>
    public class TableByFactEtlPerformer : IETLPerformer
    {

        /// <summary>
        /// Performs the etl.
        /// </summary>
        /// <param name="_extractor">The _extractor.</param>
        /// <param name="_transformer">The _transformer.</param>
        /// <param name="_loader">The _loader.</param>
        /// <param name="cacheSize">Size of the cache.</param>
        public void PerformEtl(IExtractor _extractor, ITransformer _transformer, ILoader _loader, int cacheSize)
        {           
            SQLiteMappingProvider mappingProvider = _transformer.getMapingProvider();
            List<string> tableList;
            SteppingEtlPerformer stepEtlPerformer = new SteppingEtlPerformer();
            List<string> alltables = mappingProvider.getInstanceTables(_extractor.getInstnaceId());

            foreach (string table in alltables)
            {
                tableList = new List<string>();
                tableList.Add(table);

                mappingProvider.getAllMappingHashSet(tableList);

                ProgressHandler.EtlProgress(alltables.IndexOf(table) + 1, alltables.Count(), " loading facts for table " + table);

                stepEtlPerformer.PerformEtl(_extractor, _transformer, _loader, cacheSize);
            }
        }
    }
}
