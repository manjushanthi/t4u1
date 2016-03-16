using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SQLite;
//using SolvencyII.UI.Shared.Loggers;
//using SolvencyII.Domain.ENumerators;

namespace SolvencyII.Data.CRT.ETL.DataConnectors
{
    /// <summary>
    /// Connector to the SQL database
    /// </summary>
    public class SQLiteConnector : IDataConnector
    {
        readonly string connString;
        SQLiteConnection conn;
        SQLiteCommand comm;
        SQLiteTransaction trans;
        private string _filePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteConnector"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public SQLiteConnector(string filePath)
        {
            connString = "Data Source=" + filePath + ";Version=3";          
            conn = new SQLiteConnection(connString,true);
            this._filePath = filePath;
        }

        /// <summary>
        /// Opens the connection.
        /// </summary>
        public void openConnection()
        {

            if (this.conn.State == ConnectionState.Open)
                return;
                
            this.conn.Open();
            trans = this.conn.BeginTransaction();
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void closeConnection()
        {
            if (trans != null)
                trans.Commit();
            this.conn.Close();
            trans = null;
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="nonQuery">The non query.</param>
        public void executeNonQuery(string nonQuery)
        {
            comm = new SQLiteCommand(nonQuery);
            executeNonQuery(comm);
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentException">Command has to be SQLiteCommand</exception>
        public void executeNonQuery(IDbCommand command)
        {
            if (!(command is SQLiteCommand))
                throw new ArgumentException("Command has to be SQLiteCommand");

            SQLiteCommand sqlIteCommand = command as SQLiteCommand;
            sqlIteCommand.Connection = conn;
            sqlIteCommand.Prepare();

            bool close = true;
            if (conn.State == ConnectionState.Open)
                close = false;
            else
                conn.Open();

            try
            {
                sqlIteCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                EtlException etEx = getDbException(ex, sqlIteCommand);
                throw etEx;
            }

            if (close)
                conn.Close();
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public DataTable executeQuery(string query)
        {
            comm = new SQLiteCommand(query);
            return this.executeQuery(comm);
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Command has to be SQLiteCommand</exception>
        public DataTable executeQuery(IDbCommand command)
        {
            if (!(command is SQLiteCommand))
                throw new ArgumentException("Command has to be SQLiteCommand");

            DataTable dt = new DataTable();
            try
            {
                SQLiteCommand sqlIteCommand = command as SQLiteCommand;
                sqlIteCommand.Connection = conn;
                sqlIteCommand.Prepare();

                bool close = true;
                if (conn.State == ConnectionState.Open)
                    close = false;
                else
                    conn.Open();



                SQLiteDataReader rdr = null;
                try
                {
                    rdr = sqlIteCommand.ExecuteReader();
                }
                catch (Exception ex)
                {
                    EtlException etEx = getDbException(ex, sqlIteCommand);
                    throw etEx;
                }
                

                for (int i = 0; i < rdr.FieldCount; i++)
                    dt.Columns.Add(rdr.GetName(i));

                DataRow dr;
                while (rdr.Read())
                {
                    dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                        dr[dc.ColumnName] = rdr[dc.ColumnName];

                    dt.Rows.Add(dr);
                }
                rdr.Close();

                if (close)
                    conn.Close();

                return dt;
            }
            catch (Exception ex)
            {
                //Logger.WriteLog(eSeverity.Error, "Exception occurs in SQLiteConnector - Method executeQuery :" + ex.ToString());
                //Logger.WriteLog(eSeverity.Error, "with Connection :" +conn.ConnectionString.ToString());
                throw ex;
                
            }
            //return dt;
        }


        /// <summary>
        /// Gets the schema table.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Command has to be SqlCommand</exception>
        public DataTable getSchemaTable(IDbCommand command)
        {
            if (!(command is SQLiteCommand))
                throw new ArgumentException("Command has to be SqlCommand");

            SQLiteCommand sqlIteCommand = comm as SQLiteCommand;

            sqlIteCommand.Connection = conn;
            sqlIteCommand.Transaction = trans;

            bool close = true;
            if (conn.State == ConnectionState.Open)
                close = false;
            else
                conn.Open();

            SQLiteDataReader rdr = sqlIteCommand.ExecuteReader();

            DataTable dt = rdr.GetSchemaTable();

            if (close)
                conn.Close();

            return dt;
        }

        /// <summary>
        /// Gets the schema table.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public DataTable getSchemaTable(string query)
        {
            comm = new SQLiteCommand(query);
            return this.executeQuery(comm);
        }

        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IDataReader executeReader(string query)
        {
            comm = new SQLiteCommand(query, conn);

            bool close = true;
            if (conn.State == ConnectionState.Open)
                close = false;
            else
                conn.Open();

            SQLiteDataReader rdr = null;

            try
            {
                rdr = comm.ExecuteReader();
            }
            catch (Exception ex)
            {
                EtlException etEx = getDbException(ex, comm);
                throw etEx;
            }

            if (close)
                conn.Close();

            return rdr;
        }

        /// <summary>
        /// Gets the database exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="comm">The comm.</param>
        /// <returns></returns>
        private EtlException getDbException(Exception ex, SQLiteCommand comm)
        {
            StringBuilder sb = new StringBuilder("Exception in command: ");
            sb.Append(comm.CommandText);
            if (comm.Parameters.Count > 0) sb.Append(" with parameters: ");

            foreach (SQLiteParameter param in comm.Parameters)
                if(param != null) sb.AppendLine(string.Format("{0} : {1}", param.ParameterName, param.Value == null ? "" : param.Value.ToString()));

            EtlException result = new EtlException(sb.ToString(), ex);
            return result;
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath {
            get
            {
                return this._filePath;
            }
        }


        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public IDbCommand createCommand()
        {
            return new SQLiteCommand();
        }

        /// <summary>
        /// Gets the type of the DBMS.
        /// </summary>
        /// <value>
        /// The type of the DBMS.
        /// </value>
        public DbmsType DbmsType
        {
            get { return DbmsType.SQLite; }
        }


        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            if (trans != null && trans.Connection.State == ConnectionState.Open)
            {
                trans.Rollback();
                trans.Dispose();
            }

            trans = null;
        }

        /// <summary>
        /// Creates the parameter.
        /// </summary>
        /// <param name="comm">The comm.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <param name="paramValue">The parameter value.</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(IDbCommand comm, string paramName, object paramValue)
        {
            if (comm == null)
                return null;

            IDbDataParameter param = comm.CreateParameter();
            param.ParameterName = paramName;
            param.Value = paramValue;
            comm.Parameters.Add(param);
            return param;
        }   
    }
}
