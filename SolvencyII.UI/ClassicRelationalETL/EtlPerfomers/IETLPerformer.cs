using SolvencyII.Data.CRT.ETL.ETLControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.EtlPerfomers
{
    /// <summary>
    /// Interface of the ETLPerformer, which provide functionality of management of processing of the ETL
    /// </summary>
    public interface IETLPerformer
    {
        /// <summary>
        /// Performs the etl.
        /// </summary>
        /// <param name="_extractor">The _extractor.</param>
        /// <param name="_transformer">The _transformer.</param>
        /// <param name="_loader">The _loader.</param>
        /// <param name="cacheSize">Size of the cache.</param>
        void PerformEtl(IExtractor _extractor, ITransformer _transformer, ILoader _loader, int cacheSize);
    }
}
