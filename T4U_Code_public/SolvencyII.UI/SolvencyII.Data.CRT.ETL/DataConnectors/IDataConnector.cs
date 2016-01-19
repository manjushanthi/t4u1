using System;
using System.Data;
namespace SolvencyII.Data.CRT.ETL.DataConnectors
{
    /// <summary>
    /// Interface of the connection to the data
    /// </summary>
    public interface IDataConnector
    {
        /// <summary>
        /// Closes the connection.
        /// </summary>
        void closeConnection();
        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="command">The command.</param>
        void executeNonQuery(IDbCommand command);
        /// <summary>
        /// Executes the non query.
        /// </summary>
        /// <param name="nonQuery">The non query.</param>
        void executeNonQuery(string nonQuery);
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        System.Data.DataTable executeQuery(IDbCommand command);
        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        System.Data.DataTable executeQuery(string query);
        /// <summary>
        /// Gets the schema table.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        System.Data.DataTable getSchemaTable(string query);
        /// <summary>
        /// Executes the reader.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        IDataReader executeReader(string query);
        /// <summary>
        /// Opens the connection.
        /// </summary>
        void openConnection();
        /// <summary>
        /// Creates the command.
        /// </summary>
        /// <returns></returns>
        IDbCommand createCommand();
        /// <summary>
        /// Gets the type of the DBMS.
        /// </summary>
        /// <value>
        /// The type of the DBMS.
        /// </value>
        DbmsType DbmsType { get; }
        /// <summary>
        /// Rollbacks the transaction.
        /// </summary>
        void RollbackTransaction();

        IDbDataParameter CreateParameter(IDbCommand comm, string paramName, object paramValue);
    }

    /// <summary>
    /// Type of database
    /// </summary>
    public enum DbmsType
    {
        MSSQL,
        SQLite
    }
}
