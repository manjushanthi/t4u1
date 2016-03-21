using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using System.Data.SQLite;
using System.Data;
using SolvencyII.Data.CRT.ETL.Model;
using System.Collections;
using SolvencyII.Data.CRT.ETL.DBcontrollers.Loading;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using System.Data.Common;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Loads facts to the SQL database
    /// </summary>
    public class SQLiteLoader : ILoader
    {
        string filePath=string.Empty;
        IDataConnector _dataConnector;
        private static bool _cancel = false;
        private IQueryConstructor _queryConstructor;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteLoader"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        public SQLiteLoader(IDataConnector dataConnector)
        {
            this._dataConnector = dataConnector;
            _queryConstructor = new QuickQueryConstructor(dataConnector);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteLoader"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <param name="queryConstructor">The query constructor.</param>
        public SQLiteLoader(IDataConnector dataConnector, IQueryConstructor queryConstructor)
        {
            this._dataConnector = dataConnector;
            this._queryConstructor = queryConstructor;
            _queryConstructor = new QuickQueryConstructor(dataConnector);
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="pfilePath">The pfile path.</param>
        /// <returns></returns>
        private string getConnString(string pfilePath)
        {
            return "Data Source=" + pfilePath + ";Version=3";
        }

        /// <summary>
        /// Loads the inserts.
        /// </summary>
        /// <param name="inserts">The inserts.</param>
        /// <exception cref="EtlException"></exception>
        public void loadInserts(HashSet<CrtRow> inserts)
        {
            _cancel = false;
            HashSet<CrtRowIdentification> ris = new HashSet<CrtRowIdentification>((inserts.Where(x=>x != null).Select(x => x.rowIdentification).Distinct()));
            inserts = new HashSet<CrtRow>(inserts.Where(x => x != null));

            IDbCommand command;
            int size = ris.Count;
            foreach (CrtRowIdentification ri in ris)
            {
                if (_cancel) break;

                try
                {
                    if (ri.PK_ID == 0)
                    {
                        command = constructInsertCommand(ri, inserts.Where(x => x.rowIdentification == ri));
                        ri.PK_ID = int.Parse(_dataConnector.executeQuery(command).Rows[0][0].ToString());
                    }
                    else
                    {
                        command = constructUpdateCommand(ri, inserts.Where(x => x.rowIdentification == ri));
                        _dataConnector.executeNonQuery(command);
                    }
                }
                catch (Exception ex)
                {                    
                    throw new EtlException(string.Format("Exception while saving data into table {0} with row ", ri.TABLE_NAME.Replace("T__", "")), ex);
                }
            }
            ProgressHandler.EtlProgress(size, size, "Loaded/updated relational rows ");
        }

        /// <summary>
        /// Constructs the update command.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        private IDbCommand constructUpdateCommand(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            return _queryConstructor.constructUpdateQuery(ri, inserts);
        }

        /// <summary>
        /// Constructs the insert command.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        private IDbCommand constructInsertCommand(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            return _queryConstructor.constructInsertQuery(ri, inserts);            
        }

        /// <summary>
        /// Loads the facts.
        /// </summary>
        /// <param name="facts">The facts.</param>
        /// <exception cref="EtlException"></exception>
        public void loadFacts(HashSet<dFact> facts)
        {
            IDbCommand command;
            _dataConnector.openConnection();
            int i = 0;
            int size = facts.Count();
            foreach (dFact fact in facts)
            {
                if (string.IsNullOrEmpty(fact.getStringValue()))
                    continue;

                command = getInsertQuery(fact);
                try {
                    _dataConnector.executeNonQuery(command);}
                catch (Exception ex) 
                {
                    if (ex is DbException || (ex.InnerException != null && ex.InnerException is DbException))
                        this.checkDuplication(fact, ex);
                    else
                        throw new EtlException(string.Format("Exception while saving fact {0} with value {1}", fact.dataPointSignature, fact.getStringValue()), ex);
                }
   
                if (++i % 1000 == 0)
                    ProgressHandler.EtlProgress(i, size, " loaded facts ");
            }
            ProgressHandler.EtlProgress(i, size, " loaded facts ");
            _dataConnector.closeConnection();
        }

        /// <summary>
        /// Checks the duplication.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <param name="ex">The ex.</param>
        /// <exception cref="EtlException"></exception>
        private void checkDuplication(dFact fact, Exception ex)
        {
            IDbCommand command = _queryConstructor.constructQueryForDuplication(fact);
            DataTable dt = _dataConnector.executeQuery(command);
            if (dt.Rows.Count == 0)
            {
                IDbCommand query = _queryConstructor.constructQueryForFact(fact.instanceId, fact.dataPointSignature);
                DataTable dt2 = _dataConnector.executeQuery(query);
                
                if (dt2.Rows.Count > 0)
                    serilizeTodMessages(new DataPointDuplicationException(string.Format("Duplicated fact {0} with inconsistent value of {1} and {2}"
                        , fact.dataPointSignature, fact.getStringValue(), dt2.Rows[0][getValueColumn(fact)].ToString())), fact);
                else
                    throw new EtlException(string.Format("Exception durign fact {0} with value of {1}", fact.dataPointSignature, fact.getStringValue()), ex);
            }
        }

        /// <summary>
        /// Serilizes the tod messages.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="fact">The fact.</param>
        private void serilizeTodMessages(DataPointDuplicationException ex, dFact fact)
        {
            IDbCommand comm = this._queryConstructor.getdMessageInsertCommand(ex, fact.instanceId);
            _dataConnector.executeNonQuery(comm);
        }

        /// <summary>
        /// Gets the value column.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        private string getValueColumn(dFact fact)
        {
            if (fact.boolValue != null)
                return "BooleanValue";
            else if (fact.numericValue != null)
                return "NumericValue";
            else if (fact.dateTimeValue != null)
                return "DateTimeValue";
            else
                return "TextValue";
        }

        /// <summary>
        /// Gets the insert query.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        public IDbCommand getInsertQuery(dFact fact)
        {
            return _queryConstructor.getInsertCommand(fact);            
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public void Cancel()
        {
            _cancel = true;
        }

        /// <summary>
        /// Opens the connection.
        /// </summary>
        public void openConnection()
        {
            _dataConnector.openConnection();
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void closeConnection()
        {
            _dataConnector.closeConnection();
        }

        /// <summary>
        /// Cleans the d facts.
        /// </summary>
        /// <param name="instanceID">The instance identifier.</param>
        public void CleanDFacts(int instanceID)
        {
            string query = string.Format("delete from dFact where InstanceID = {0};", instanceID);
            try
            {
                _dataConnector.executeNonQuery(query);
            }
            catch (Exception)
            {                
                throw;
            }
        }

        /// <summary>
        /// Sets the maximum col number.
        /// </summary>
        internal void SetMaxColNumber()
        {
            //string query = "sqlite3_limit(db,SQLITE_LIMIT_COLUMN,size)";
            //_dataConnector.executeNonQuery(query);
        }
    }
}
