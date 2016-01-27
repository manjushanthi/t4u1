using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.ETLControllers;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Extracts from CRT tables, transforms and loads to the dFact table
    /// </summary>
    public class Relational2dFactETL : IEtlEngine
    {
        IFactsNumberReader _factNumberReader;
        IExtractor _extractor;
        /// <summary>
        /// Gets the i extractor.
        /// </summary>
        /// <value>
        /// The i extractor.
        /// </value>
        /// <exception cref="System.NullReferenceException">Null extractor</exception>
        public IExtractor IExtractor
        {
            get
            {
                if (_extractor == null)
                    throw new NullReferenceException("Null extractor");

                return _extractor;
            }
        }

        ILoader _loader;
        /// <summary>
        /// Gets the i loader.
        /// </summary>
        /// <value>
        /// The i loader.
        /// </value>
        /// <exception cref="System.NullReferenceException">Null loader</exception>
        public ILoader ILoader
        {
            get
            {
                if (_loader == null)
                    throw new NullReferenceException("Null loader");

                return _loader;
            }
        }

        ITransformer _transformer;

        HashSet<CrtRow> inserts;
        HashSet<dFact> facts;

        /// <summary>
        /// Initializes a new instance of the <see cref="Relational2dFactETL"/> class.
        /// </summary>
        /// <param name="extractor">The extractor.</param>
        /// <param name="loader">The loader.</param>
        /// <param name="transformer">The transformer.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Null extractor
        /// or
        /// Null loader
        /// or
        /// Null transformer
        /// </exception>
        public Relational2dFactETL(IExtractor extractor, ILoader loader, ITransformer transformer)
        {
            if(extractor == null)
                throw new ArgumentNullException("Null extractor");
            if (loader == null)
                throw new ArgumentNullException("Null loader");
            if (transformer == null)
                throw new ArgumentNullException("Null transformer");

            _loader = loader;
            _extractor = extractor;
            _transformer = transformer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Relational2dFactETL"/> class.
        /// </summary>
        /// <param name="extractor">The extractor.</param>
        /// <param name="loader">The loader.</param>
        /// <param name="transformer">The transformer.</param>
        /// <param name="fnumReader">The fnum reader.</param>
        /// <exception cref="System.ArgumentNullException">
        /// Null extractor
        /// or
        /// Null loader
        /// or
        /// Null transformer
        /// </exception>
        public Relational2dFactETL(IExtractor extractor, ILoader loader, ITransformer transformer, IFactsNumberReader fnumReader)
        {
            // TODO: Complete member initialization
            if (extractor == null)
                throw new ArgumentNullException("Null extractor");
            if (loader == null)
                throw new ArgumentNullException("Null loader");
            if (transformer == null)
                throw new ArgumentNullException("Null transformer");

            _loader = loader;
            _extractor = extractor;
            _transformer = transformer;

            this._factNumberReader = fnumReader;
        }

        /*public bool Extract()
        {
            if (_extractor == null)
                return false;

            this.inserts = this._extractor.extractInserts();
            return true;
        }

        public bool Transform()
        {
            if (_transformer == null || this.inserts == null || this.inserts.Count == 0)
                return false;

            this.facts = _transformer.transformInserts(inserts);
            return true;
        }

        public bool Load()
        {
            if (_loader == null || this.facts == null || this.facts.Count == 0)
                return false;

            _loader.loadFacts(this.facts);
            return true;
        }*/

        /// <summary>
        /// Performs the etl.
        /// </summary>
        /// <param name="cacheSize">Size of the cache.</param>
        /// <returns></returns>
        public bool PerformEtl(int cacheSize)
        {
            List<FactsNumber> fns = this._factNumberReader.GetTablesNumbers();            
            List<int> ids;
            foreach (FactsNumber fn in fns)
            {
                fn.rowsIds = fn.rowsIds.OrderBy(x => x).ToList();
                int factInRow = fn.factsNumber/fn.rowsIds.Count;
                int iterations = fn.rowsIds.Count > cacheSize ? (int)(Decimal.Ceiling(Decimal.Divide(fn.rowsIds.Count , cacheSize))) : 1;
                for (int i = 1; i <= iterations; i++)
                {
                    
                    int start = (i - 1) * cacheSize;
                    if (start > (fn.rowsIds.Count - 1))
                        break;

                    int length = (fn.rowsIds.Count - start - 1) >= cacheSize ? cacheSize : fn.rowsIds.Count - start;
                    ids = fn.rowsIds.GetRange(start, length);
                    this.inserts = _extractor.extractInserts(fn.TableName, ids);
                    ProgressHandler.EtlProgress(inserts.Count(), inserts.Count(), " extracted facts for " + fn.TableName);
                    this.facts = _transformer.transformInserts(inserts);
                    ProgressHandler.EtlProgress(facts.Count(), facts.Count(), " transformed facts for " + fn.TableName);

                    int factsNumber = facts.Count();
                    int factsToBeInsertedNumber = (from j in inserts
                                                   from rcv in j.rcColumnsValues
                                                   select new { key = rcv.Key, value = rcv.Value }).ToArray().Count();

                    _loader.loadFacts(this.facts);
                    ProgressHandler.EtlProgress(facts.Count(), facts.Count(), " loaded facts for " + fn.TableName);
                    
                    inserts.Clear();
                    inserts = null;
                    facts.Clear();
                    facts = null;
                }                 
            }
            
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
