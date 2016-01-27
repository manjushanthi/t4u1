using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Data;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Loading
{
    /// <summary>
    /// Obsolete implementation of the SQLite query constructor
    /// </summary>
    public class BasicSqliteQueryConstructor : IQueryConstructor
    {

        /// <summary>
        /// Constructs the update query.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        public IDbCommand constructUpdateQuery(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            bool first = true;

            StringBuilder query = new StringBuilder();
            query.Append("UPDATE [T__");
            query.Append(ri.TABLE_NAME);
            query.Append("] SET ");
            foreach (CrtRow insert in inserts)
            {
                foreach (KeyValuePair<string, object> kvp in insert.rcColumnsValues)
                {
                    if (!first)
                        query.Append(", ");
                    else
                        first = false;

                    query.Append(kvp.Key);
                    query.Append("= '");
                    query.Append(kvp.Value);
                    query.Append("' ");
                }
            }
            query.Append(" where PK_ID = ");
            query.Append(ri.PK_ID);
            query.Append(";");

            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = query.ToString();
            return comm;
        }

        /// <summary>
        /// Constructs the insert query.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        public IDbCommand constructInsertQuery(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            StringBuilder query = new StringBuilder();
            query.Append("insert into [T__");
            query.Append(ri.TABLE_NAME);
            query.Append("] (INSTANCE, ");
            query.Append(getColumnNames(ri, inserts));
            query.Append(") ");
            query.Append(" values ( ");
            query.Append(ri.INSTANCE);
            query.Append(" , ");
            query.Append(getColumnValues(ri, inserts));
            query.Append("); SELECT last_insert_rowid();");

            SQLiteCommand comm = new SQLiteCommand();
            comm.CommandText = query.ToString();
            return comm;
        }

        /// <summary>
        /// Gets the column names.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        private string getColumnNames(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            StringBuilder columns = new StringBuilder();
            bool first = true;
            foreach (DictionaryEntry item in ri.contextColumnsValues)
            {
                if (!first)
                    columns.Append(" , ");

                first = false;
                columns.Append(item.Key);
            }

            foreach (var item in (from i in inserts from cv in i.rcColumnsValues select cv))
            {
                if (!first)
                    columns.Append(" , ");

                first = false;
                columns.Append(item.Key);
            }
            return columns.ToString();
        }

        /// <summary>
        /// Gets the column values.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        private string getColumnValues(CrtRowIdentification ri, IEnumerable<CrtRow> inserts)
        {
            StringBuilder values = new StringBuilder();
            bool first = true;
            foreach (DictionaryEntry item in ri.contextColumnsValues)
            {
                if (!first)
                    values.Append(" , ");

                first = false;
                values.Append("'");
                values.Append(item.Value.ToString().Replace("'", "''"));
                values.Append("'");
            }

            foreach (var item in (from i in inserts from cv in i.rcColumnsValues select cv))
            {
                if (!first)
                    values.Append(" , ");

                first = false;
                values.Append("'");
                values.Append(item.Value.ToString().Replace("'", "''"));
                values.Append("'");
            }
            return values.ToString();
        }


        /// <summary>
        /// Gets the insert command.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public System.Data.IDbCommand getInsertCommand(dFact fact)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Constructs the query for duplication.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public System.Data.IDbCommand constructQueryForDuplication(dFact fact)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Constructs the query for fact.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDbCommand constructQueryForFact(int p1, string p2)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Getds the message insert command.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDbCommand getdMessageInsertCommand(DataPointDuplicationException ex)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Getds the message insert command.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="InstanceID">The instance identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IDbCommand getdMessageInsertCommand(DataPointDuplicationException ex, int InstanceID)
        {
            throw new NotImplementedException();
        }
    }
}
