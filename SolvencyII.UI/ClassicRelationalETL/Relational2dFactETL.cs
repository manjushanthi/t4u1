using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.Repositories;
using SolvencyII.Data.CRT.ETL.Model.Validation;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Extracts from CRT tables, transforms and loads to the dFact table
    /// </summary>
    public class Relational2dFactETL : IEtlEngine
    {
        IFactsNumberReader _factNumberReader;
        IExtractor _extractor;
        ILoader _loader;
        ITransformer _transformer;        
        ICrtErrorRepository crtErrorRepository;
        IModelValidator<CrtRow> validator;
        HashSet<CrtRow> crtRows;
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
        public Relational2dFactETL(IExtractor extractor, ILoader loader, ITransformer transformer, ICrtErrorRepository crtErrorRepository)
        {
            Initialize(extractor, loader, transformer, crtErrorRepository);            
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
        public Relational2dFactETL(IExtractor extractor, ILoader loader, ITransformer transformer, IFactsNumberReader fnumReader, ICrtErrorRepository crtErrorRepository)
        {
            Initialize(extractor, loader, transformer, crtErrorRepository);
            this._factNumberReader = fnumReader;
        }

        private void Initialize(IExtractor extractor, ILoader loader, ITransformer transformer, ICrtErrorRepository crtErrorRepository)
        {
            if (extractor == null)
                throw new ArgumentNullException("Null extractor");
            if (loader == null)
                throw new ArgumentNullException("Null loader");
            if (transformer == null)
                throw new ArgumentNullException("Null transformer");

            _loader = loader;
            _extractor = extractor;
            _transformer = transformer;
            this.crtErrorRepository = crtErrorRepository;
            validator = new CrtRowValidator();
        }

        /// <summary>
        /// Performs the etl.
        /// </summary>
        /// <param name="cacheSize">Size of the cache.</param>
        /// <returns></returns>
        public bool PerformEtl(int cacheSize)
        {
            List<TableFacts> fns = this._factNumberReader.GetTablesNumbers();                        
            foreach (TableFacts fn in fns)            
                movefactsForTable(cacheSize, fn);                    
            
            return true;
        }

        private void movefactsForTable(int cacheSize, TableFacts tableFacts)
        {            
            tableFacts.rowsIds = tableFacts.rowsIds.OrderBy(x => x).ToList();
            int avgNumberOfFacstInRow = tableFacts.factsNumber / tableFacts.rowsIds.Count;
            int iterations = getIterationsNumber(cacheSize, tableFacts);
            for (int iterationNumber = 1; iterationNumber <= iterations; iterationNumber++)
                processIteration(cacheSize, tableFacts, iterationNumber);
        }

        private void processIteration(int cacheSize, TableFacts tableFacts, int iterationNumber)
        {
            int iterationFirtRowId = (iterationNumber - 1) * cacheSize;
            if (iterationFirtRowId > (tableFacts.rowsIds.Count - 1))
                return;

            int numberOfRows = (tableFacts.rowsIds.Count - iterationFirtRowId - 1) >= cacheSize ? cacheSize : tableFacts.rowsIds.Count - iterationFirtRowId;
            List<int> iterationRowsIDs = tableFacts.rowsIds.GetRange(iterationFirtRowId, numberOfRows);
            extractCrtRows(tableFacts, iterationRowsIDs);
            transformRowsToFacts(tableFacts);
            loadFacts(tableFacts);
        }

        private void loadFacts(TableFacts tableFacts)
        {
            int factsNumber = facts.Count();
            _loader.loadFacts(this.facts);
            ProgressHandler.EtlProgress(facts.Count(), facts.Count(), " loaded facts for " + tableFacts.TableName);
        }

        private void transformRowsToFacts(TableFacts tableFacts)
        {
            this.facts = _transformer.transformInserts(crtRows);
            ProgressHandler.EtlProgress(facts.Count(), facts.Count(), " transformed facts for " + tableFacts.TableName);
        }

        private void extractCrtRows(TableFacts tableFacts, List<int> ids)
        {
            IEnumerable<CrtRow> rows = _extractor.extractInserts(tableFacts.TableName, ids);
            IModelValidationResult<CrtRow> validationResult = validator.validate(rows);
            crtErrorRepository.Add(validationResult.errors);
            crtRows = new HashSet<CrtRow>(validationResult.validObjects);
            ProgressHandler.EtlProgress(crtRows.Count(), crtRows.Count(), " extracted facts for " + tableFacts.TableName);       
        }

        private static int getIterationsNumber(int cacheSize, TableFacts tableFacts)
        {
            int iterations = tableFacts.rowsIds.Count > cacheSize ? (int)(Decimal.Ceiling(Decimal.Divide(tableFacts.rowsIds.Count, cacheSize))) : 1;
            return iterations;
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
