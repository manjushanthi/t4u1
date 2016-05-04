using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction;
using SolvencyII.Data.CRT.ETL.DBcontrollers;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Extractor of facts and CRT rows from SQLite database
    /// </summary>
    public class SQLiteExtractor : IExtractor
    {
        int _instanceId;
        IDataConnector _dataConnector;
        SQLiteCrtRowExtractor _insertsExtractor;
        SQLiteMappingProvider _mappingProvider;
        IFilinglIndicatorsExtractor _indicatorsExtractor;
        private static bool _cancel = false;
        private string[] _tableNames;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteExtractor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <param name="instanceId">The instance identifier.</param>
        public SQLiteExtractor(IDataConnector dataConnector, int instanceId)
        {
            this._instanceId = instanceId;
            _dataConnector = dataConnector;
            _insertsExtractor = new SQLiteCrtRowExtractor(_dataConnector, _instanceId);
            _mappingProvider = new SQLiteMappingProvider(_dataConnector);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteExtractor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <param name="instanceId">The instance identifier.</param>
        /// <param name="mappingProvider">The mapping provider.</param>
        public SQLiteExtractor(IDataConnector dataConnector, int instanceId, SQLiteMappingProvider mappingProvider)
        {
            this._instanceId = instanceId;
            _dataConnector = dataConnector;
            _insertsExtractor = new SQLiteCrtRowExtractor(_dataConnector, _instanceId);
            _mappingProvider = new SQLiteMappingProvider(_dataConnector);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteExtractor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <param name="instanceId">The instance identifier.</param>
        /// <param name="tableNames">The table names.</param>
        public SQLiteExtractor(IDataConnector dataConnector, int instanceId, string[] tableNames)
        {
            this._instanceId = instanceId;
            _dataConnector = dataConnector;
            SetTableNames(tableNames);
            _insertsExtractor = new SQLiteCrtRowExtractor(_dataConnector, _instanceId);
            _mappingProvider = new SQLiteMappingProvider(_dataConnector);
        }

        /// <summary>
        /// Sets the table names. Not implemneted for this extractor.
        /// </summary>
        /// <param name="tableNames">The table names.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void SetTableNames(string[] tableNames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteExtractor"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <param name="instanceId">The instance identifier.</param>
        /// <param name="tableNames">The table names.</param>
        /// <param name="mappingProvider">The mapping provider.</param>
        public SQLiteExtractor(SQLiteConnector dataConnector, int instanceId, string[] tableNames, SQLiteMappingProvider mappingProvider)
        {
            this._instanceId = instanceId;
            _dataConnector = dataConnector;
            SetTableNames(tableNames);
            _insertsExtractor = new SQLiteCrtRowExtractor(_dataConnector, _instanceId);
            _mappingProvider = mappingProvider;
        }

        /// <summary>
        /// Exctracts the facts.
        /// </summary>
        /// <returns></returns>
        public HashSet<Model.dFact> exctractFacts()
        {
            return extractFactsWithReader();
        }

        /// <summary>
        /// Extracts the facts with reader.
        /// </summary>
        /// <returns></returns>
        private HashSet<dFact> extractFactsWithReader()
        {
            return extractFactsWithReader("select cast(d.DateTimeValue as text) as dtval, * from dFact d where d.InstanceID = " + this._instanceId);
        }

        /// <summary>
        /// Extracts the facts with reader.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        /// <exception cref="EtlException">Exception while mapping dFact</exception>
        private HashSet<dFact> extractFactsWithReader(string query)
        {
            _cancel = false;
            HashSet<dFact> facts = new HashSet<dFact>();
            checkAndAddFactIdColumn();
            IDataReader rdr = _dataConnector.executeReader(query);
            int j = 0;
            while (rdr.Read())
            {
                if (_cancel) break;

                try
                {
                    facts.Add(MapFact(++j, rdr));
                }
                catch (Exception ex)
                {
                    throw new EtlException("Exception while mapping dFact", ex);
                }

                if (j % 10000 == 0)
                    ProgressHandler.EtlProgress(j, 0, " extracted facts ");
            }
            ProgressHandler.EtlProgress(j, 0, " extracted facts ");
            rdr.Dispose();
            return facts;
        }

        /// <summary>
        /// Checks the and add fact identifier column.
        /// </summary>
        public void checkAndAddFactIdColumn()
        {
            Dictionary<string, bool> dFactColumns = _insertsExtractor.getTableColumns("dFact");
            if (dFactColumns.Where(x => x.Key.ToUpper().Equals("FACTID")).Count() > 0)
                return;

            _dataConnector.executeNonQuery(@"CREATE TABLE dFact2 ( 
                                                FactID             INTEGER PRIMARY KEY AUTOINCREMENT,
                                                InstanceID         INTEGER NOT NULL,
                                                DataPointSignature TEXT,
                                                Unit               TEXT,
                                                Decimals           TEXT,
                                                NumericValue       REAL,
                                                DateTimeValue      DATE,
                                                BooleanValue       BOOLEAN,
                                                TextValue          TEXT,    
                                                UNIQUE ( InstanceID, DataPointSignature ) 
                                            );

                                            insert into dFact2 (InstanceID, DataPointSignature, Unit, Decimals, NumericValue, DateTimeValue, BooleanValue, TextValue)
                                            select InstanceID, DataPointSignature, Unit, Decimals, NumericValue, DateTimeValue, BooleanValue, TextValue from dFact;

                                            drop table dFact;
                                            alter table dFact2 rename to dFact;");
        }

        /// <summary>
        /// Maps the fact.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="rdr">The RDR.</param>
        /// <returns></returns>
        private dFact MapFact(int id, IDataReader rdr)
        {
            string txtVal = rdr["TEXTVALUE"].ToString();
            txtVal = string.IsNullOrEmpty(txtVal) ? null : txtVal;

            DateTime? DateTimeValue = null;
            if (!string.IsNullOrEmpty(rdr["dtval"].ToString()))
                DateTimeValue = Convert.ToDateTime(rdr["dtval"].ToString());
                //DateTime.Parse(rdr["dtval"].ToString(), "yyyy-MM-dd");

            bool? BooleanValue = null;
            string boolString = rdr["BooleanValue"].ToString();
            if(!string.IsNullOrEmpty(boolString)) 
                BooleanValue = (boolString.Equals("1") || boolString.Equals("True"));

            decimal? NumericValue = null;
            if (!string.IsNullOrEmpty(rdr["NumericValue"].ToString())) 
                NumericValue = Convert.ToDecimal(rdr["NumericValue"]);

            string DataPointSignature = rdr["DataPointSignature"].ToString();

            return new dFact(int.Parse(rdr["FactID"].ToString()), txtVal, DateTimeValue, BooleanValue, NumericValue, DataPointSignature, null, this._instanceId);
        }

        /// <summary>
        /// Gets the facts number.
        /// </summary>
        /// <returns></returns>
        public int getFactsNumber()
        {
            DataTable dt = this._dataConnector.executeQuery("select count(1) from dFact d where d where d.InstanceID = " + this._instanceId);
            return int.Parse(dt.Rows[0][0].ToString());
        }

        /// <summary>
        /// Extracts the inserts.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">No SQLite inserts extractor</exception>
        public IEnumerable<CrtRow> extractInserts()
        {
            populateTableNames();
            HashSet<CrtMapping> mapings = _mappingProvider.getMappings(_tableNames);
            IEnumerable<CrtRow> inserts = this._insertsExtractor.extractInserts(mapings, _tableNames);
            return inserts;
        }        
        
        private void populateTableNames()
        {
            if (_tableNames == null)
                loadTableNamesFromFillingIndicators();
            if (_tableNames == null || _tableNames.Count() == 0)
                loadTableNamesFromModule();
        }

        /// <summary>
        /// Loads the table names from module.
        /// </summary>
        private void loadTableNamesFromModule()
        {
            _indicatorsExtractor = new NewSQLiteFillIndicatorsExtractor(_dataConnector);
            this._tableNames = _indicatorsExtractor.getTablesNamesFromModule(this._instanceId);
        }

        /// <summary>
        /// Loads the table names from filling indicators.
        /// </summary>
        private void loadTableNamesFromFillingIndicators()
        {
            _indicatorsExtractor = new NewSQLiteFillIndicatorsExtractor(_dataConnector);
            this._tableNames = _indicatorsExtractor.getTablesNamesFromFillingIndicators(this._instanceId);
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public void Cancel()
        {
            _cancel = true;
        }

        /// <summary>
        /// Gets the total facts number.
        /// </summary>
        /// <param name="maxFactId">The maximum fact identifier.</param>
        /// <param name="minfactId">The minfact identifier.</param>
        /// <returns></returns>
        public int getTotalFactsNumber(out int maxFactId, out int minfactId)
        {
            string query = string.Format("select count(1) ile, max(FactID) max, min(FactID) min from dFact where InstanceID = {0}", this._instanceId);
            DataTable dt = _dataConnector.executeQuery(query);

            int result = int.Parse(dt.Rows[0]["ile"].ToString());

            if (!int.TryParse(dt.Rows[0]["max"].ToString(), out maxFactId))
                maxFactId = -1;
            if (!int.TryParse(dt.Rows[0]["min"].ToString(), out minfactId))
                minfactId = -1;
            
            return result;
        }

        /// <summary>
        /// Exctracts the facts.
        /// </summary>
        /// <param name="minfactId">The minfact identifier.</param>
        /// <param name="maxfactId">The maxfact identifier.</param>
        /// <returns></returns>
        public HashSet<dFact> exctractFacts(int minfactId, int maxfactId)
        {
            string query = string.Format("select cast(DateTimeValue as text) as dtval, * from dFact where InstanceID = {0} and FactID between {1} and {2};", this._instanceId, minfactId, maxfactId);
            HashSet<dFact> result = this.extractFactsWithReader(query);
            return result;
        }

        /// <summary>
        /// Gets the instnace identifier.
        /// </summary>
        /// <returns></returns>
        public int getInstnaceId()
        {
            return this._instanceId;
        }

        /// <summary>
        /// Gets the facts number.
        /// </summary>
        /// <param name="maxFactId">The maximum fact identifier.</param>
        /// <param name="minfactId">The minfact identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int getFactsNumber(out int maxFactId, out int minfactId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Extracts the inserts.
        /// </summary>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="rowIds">The row ids.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">No SQLite inserts extractor</exception>
        public IEnumerable<CrtRow> extractInserts(string tableName, List<int> rowIds)
        {
            if (_insertsExtractor == null)
                throw new ArgumentNullException("No SQLite inserts extractor");

            _tableNames = new string[] { tableName };

            HashSet<CrtMapping> mapings = _mappingProvider.getMappings(_tableNames);
            IEnumerable<CrtRow> inserts = this._insertsExtractor.extractInserts(mapings, tableName, rowIds);
            return inserts;
        }        
    }
}
