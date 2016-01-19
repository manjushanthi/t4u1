using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.EtlPerfomers;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Extracts data from dfact table, transforms and loads to CRT tables.
    /// </summary>
    public class dFact2RelationalETL : IEtlEngine
    {
        IExtractor _extractor;
        ILoader _loader;
        ITransformer _transformer;
        private IETLPerformer _etlPerfomer;

        /// <summary>
        /// Initializes a new instance of the <see cref="dFact2RelationalETL"/> class.
        /// </summary>
        /// <param name="extractor">The extractor.</param>
        /// <param name="loader">The loader.</param>
        /// <param name="transformer">The transformer.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Null extractor
        /// or
        /// Null loader
        /// </exception>
        public dFact2RelationalETL(IExtractor extractor, ILoader loader, ITransformer transformer)
        {
            if(extractor == null)
                throw new ArgumentNullException("Null extractor");
            if (loader == null)
                throw new ArgumentNullException("Null loader");

            _loader = loader;
            _extractor = extractor;
            _transformer = transformer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="dFact2RelationalETL"/> class.
        /// </summary>
        /// <param name="extractor">The extractor.</param>
        /// <param name="loader">The loader.</param>
        /// <param name="transformer">The transformer.</param>
        /// <param name="etlPerfomer">The etl perfomer.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Null extractor
        /// or
        /// Null loader
        /// </exception>
        public dFact2RelationalETL(IExtractor extractor, ILoader loader, ITransformer transformer, IETLPerformer etlPerfomer)
        {
            if (extractor == null)
                throw new ArgumentNullException("Null extractor");
            if (loader == null)
                throw new ArgumentNullException("Null loader");

            _loader = loader;
            _extractor = extractor;
            _transformer = transformer;
            _etlPerfomer = etlPerfomer;
        }

        /// <summary>
        /// Performs the etl.
        /// </summary>
        /// <param name="cacheSize">Size of the cache.</param>
        /// <returns></returns>
        public bool PerformEtl(int cacheSize)
        {
            _extractor.checkAndAddFactIdColumn();

            if (this._etlPerfomer != null)
                _etlPerfomer.PerformEtl(_extractor, _transformer, _loader, cacheSize);
            else
                new SteppingEtlPerformer().PerformEtl(_extractor, _transformer, _loader, cacheSize);

            return true;
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public void Cancel()
        {
            if (_extractor != null)
                _extractor.Cancel();

            if (_transformer != null)
                _transformer.Cancel();

            if (_loader != null)
                _loader.Cancel(); 
        }
    }
}
