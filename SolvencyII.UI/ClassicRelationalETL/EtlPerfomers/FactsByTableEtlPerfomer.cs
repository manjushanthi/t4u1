
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.EtlPerfomers
{
    /// <summary>
    /// ETL performer that processe set of facts for table seperately. 
    /// </summary>
    public class FactsByTableEtlPerfomer : IETLPerformer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FactsByTableEtlPerfomer"/> class.
        /// </summary>
        public FactsByTableEtlPerfomer(){ }


        /// <summary>
        /// Performs the etl.
        /// </summary>
        /// <param name="_extractor">The _extractor.</param>
        /// <param name="_transformer">The _transformer.</param>
        /// <param name="_loader">The _loader.</param>
        /// <param name="cacheSize">Size of the cache.</param>
        public void PerformEtl(IExtractor _extractor, ITransformer _transformer, ILoader _loader, int cacheSize)
        {
            HashSet<dFact> facts;
            List<CrtRow> inserts = new List<CrtRow>();
            int maxFactId;
            int extractedFactsNUmber = 0;
            int minfactId;
            int totalFactsNumber = _extractor.getTotalFactsNumber(out maxFactId, out minfactId);

            IMappingProvider mappingProvider = _transformer.getMapingProvider();
            List<string> alltables = mappingProvider.getInstanceTables(_extractor.getInstnaceId());
            HashSet<CrtRow> currentInserts;

            while (minfactId <= maxFactId)
            {
                facts = _extractor.exctractFacts(minfactId, minfactId + cacheSize);
                extractedFactsNUmber = extractedFactsNUmber + facts.Count();
                ProgressHandler.EtlProgress(extractedFactsNUmber, totalFactsNumber, " extracted facts ");

                foreach (string table in alltables)
                {
                    mappingProvider.CleanMappings();
                    mappingProvider.getAllMappingHashSet(new List<string>(){table});
                    ProgressHandler.EtlProgress(alltables.IndexOf(table) + 1, alltables.Count()*totalFactsNumber, " loading facts for table " + table);

                    currentInserts = _transformer.transformFacts(facts);

                    foreach (var item in currentInserts)
                        inserts.Add(item);

                    currentInserts = null;
                }

                _loader.loadInserts(new HashSet<CrtRow>(inserts));
                
                minfactId = minfactId + cacheSize + 1;
                inserts.Clear();
                facts.Clear();
                facts = null;

                GC.Collect();
            }
        }
    }
}
