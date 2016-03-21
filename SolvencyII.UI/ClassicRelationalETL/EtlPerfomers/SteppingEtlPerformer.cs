using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.EtlPerfomers
{
    /// <summary>
    /// Reads facts and maps without any split
    /// </summary>
    public class SteppingEtlPerformer : IETLPerformer
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
            int maxFactId;
            int minfactId;
            int totalFactsNumber = _extractor.getTotalFactsNumber(out maxFactId, out minfactId);

            performEtlInParts(cacheSize, totalFactsNumber, maxFactId, minfactId, _extractor, _transformer, _loader);
        }

        /// <summary>
        /// Performs the etl in parts.
        /// </summary>
        /// <param name="cacheSize">Size of the cache.</param>
        /// <param name="totalFactsNumber">The total facts number.</param>
        /// <param name="maxFactId">The maximum fact identifier.</param>
        /// <param name="minfactId">The minfact identifier.</param>
        /// <param name="_extractor">The _extractor.</param>
        /// <param name="_transformer">The _transformer.</param>
        /// <param name="_loader">The _loader.</param>
        /// <exception cref="System.ApplicationException"></exception>
        public void performEtlInParts(int cacheSize, int totalFactsNumber,int maxFactId, int minfactId, 
            IExtractor _extractor, ITransformer _transformer, ILoader _loader)
        {
            HashSet<dFact> facts;
            HashSet<CrtRow> inserts;
            int extractedFactsNUmber = 0;

            while (minfactId <= maxFactId)
            {
                facts = _extractor.exctractFacts(minfactId, minfactId + cacheSize);
                extractedFactsNUmber = extractedFactsNUmber + facts.Count();
                ProgressHandler.EtlProgress(extractedFactsNUmber, totalFactsNumber, " extracted facts ");

                inserts = _transformer.transformFacts(facts);
                inserts = new HashSet<CrtRow>(inserts.Where(x => x != null));
                ProgressHandler.EtlProgress(extractedFactsNUmber, totalFactsNumber, " transformed facts ");

                int factsNumber = facts.Count();
                int factsToBeInsertedNumber = (from i in inserts
                                               from rcv in i.rcColumnsValues
                                               select new { key = rcv.Key, value = rcv.Value }).ToArray().Count();

                _loader.loadInserts(inserts);
                ProgressHandler.EtlProgress(extractedFactsNUmber, totalFactsNumber, " loaded/updated facts ");

                minfactId = minfactId + cacheSize + 1;
                facts.Clear();
                facts = null;
                inserts.Clear();
                inserts = null;
                GC.Collect();
            }

            if (extractedFactsNUmber != totalFactsNumber)
                throw new ApplicationException(string.Format("Number of processed facts {0} is dfferent than expected {1}", 
                    extractedFactsNUmber, totalFactsNumber));
        }
    }
}
