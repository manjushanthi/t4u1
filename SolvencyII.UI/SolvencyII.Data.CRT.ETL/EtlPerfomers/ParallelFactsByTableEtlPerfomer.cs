
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.EtlPerfomers
{
    /// <summary>
    /// facts by table performer with parallel approach.
    /// </summary>
    public class ParallelFactsByTableEtlPerfomer : IETLPerformer
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
            HashSet<dFact> facts;
            List<CrtRow> inserts = new List<CrtRow>();
            int maxFactId;
            int extractedFactsNUmber = 0;
            int minfactId;
            int totalFactsNumber = _extractor.getTotalFactsNumber(out maxFactId, out minfactId);

            SQLiteMappingProvider mappingProv = _transformer.getMapingProvider();
            List<string> alltables = mappingProv.getInstanceTables(_extractor.getInstnaceId());
            
            while (minfactId <= maxFactId)
            {
                facts = _extractor.exctractFacts(minfactId, minfactId + cacheSize);
                extractedFactsNUmber = extractedFactsNUmber + facts.Count();
                ProgressHandler.EtlProgress(extractedFactsNUmber, totalFactsNumber, " extracted facts ");

                Action<string> tabJob = (table) =>
                {
                    SQLiteMappingProvider mappingProvider = mappingProv.Replicate();

                    mappingProvider.CleanMappings();
                    mappingProvider.getAllMappingHashSet(new List<string>() { table });
                    ProgressHandler.EtlProgress(alltables.IndexOf(table) + 1, alltables.Count() * totalFactsNumber, " transforming facts for table " + table);

                    HashSet<CrtRow> currentInserts = _transformer.transformFacts(facts);

                    foreach (var item in currentInserts)
                        inserts.Add(item);

                    currentInserts = null;
                };

                alltables.AsParallel().WithDegreeOfParallelism(10).ForAll(x => tabJob(x));                

                _loader.loadInserts(new HashSet<CrtRow>(inserts));
                
                minfactId = minfactId + cacheSize + 1;
                inserts.Clear();
                facts.Clear();
                facts = null;

                //GC.Collect();
            }
        }
    }
}
