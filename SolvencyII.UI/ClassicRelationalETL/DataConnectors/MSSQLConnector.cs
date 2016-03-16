using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DataConnectors
{
    /// <summary>
    /// Connector to the MSSQL database
    /// </summary>
    public class MSSQLConnector : IDataConnector
    {
        private string connectionsString;
        SqlConnection conn;
        SqlCommand comm;
        SqlTransaction trans;
        /// <summary>
        /// Initializes a new instance of the <see cref="MSSQLConnector"/> class.
        /// </summary>
        /// <param name="connectionsString">The connections string.</param>
        public MSSQLConnector(string connectionsString)
        {
            this.connectionsString = connectionsString;

            conn = new SqlConnection(connectionsString);
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
        /// <param name="command">The command.</param>
        /// <exception cref="System.ArgumentException">Command has to be SqlCommand</exception>
        public void executeNonQuery(IDbCommand command)
        {
            if (!(command is SqlCommand))
                throw new ArgumentException("Command has to be SqlCommand");

            SqlCommand sqlIteCommand = comm as SqlCommand;
            sqlIteCommand.Connection = conn;
            sqlIteCommand.Transaction = trans;

            bool close = true;
            if (conn.State == ConnectionState.Open)
                close = false;
            else
                conn.Open();

            sqlIteCommand.ExecuteNonQuery();

            if (close)
                conn.Close();
        }

        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="nonQuery">The non query.</param>
        public void executeNonQuery(string nonQuery)
        {
            comm = new SqlCommand(nonQuery);
            executeNonQuery(comm);
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">Command has to be SqlCommand</exception>
        public DataTable executeQuery(IDbCommand command)
        {
            if (!(command is SqlCommand))
                throw new ArgumentException("Command has to be SqlCommand");

            SqlCommand sqlIteCommand = comm as SqlCommand;

            sqlIteCommand.Connection = conn;
            sqlIteCommand.Transaction = trans;

            bool close = true;
            if (conn.State == ConnectionState.Open)
                close = false;
            else
                conn.Open();

            SqlDataReader rdr = sqlIteCommand.ExecuteReader();
            DataTable dt = new DataTable();

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

            if (close)
                conn.Close();

            return dt;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public DataTable executeQuery(string query)
        {
            comm = new SqlCommand(query);
            return this.executeQuery(comm);
        }

        /// <summary>
        /// Gets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        /// <exception cref="System.NotImplementedException"></exception>
        public string FilePath
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the schema table.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public DataTable getSchemaTable(string query)
        {
            comm = new SqlCommand(query);
            return this.executeQuery(comm);
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
        /// Executes the reader.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public IDataReader executeReader(string query)
        {
            comm = new SqlCommand(query, conn, trans);

            bool close = true;
            if (conn.State == ConnectionState.Open)
                close = false;
            else
                conn.Open();

            SqlDataReader rdr = comm.ExecuteReader();

            if (close)
                conn.Close();

            return rdr;
        }


        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        public IDbCommand createCommand()
        {
            return new SqlCommand();
        }

        /// <summary>
        /// Gets the type of the DBMS.
        /// </summary>
        /// <value>
        /// The type of the DBMS.
        /// </value>
        public DbmsType DbmsType
        {
            get { return DbmsType.MSSQL; }
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
