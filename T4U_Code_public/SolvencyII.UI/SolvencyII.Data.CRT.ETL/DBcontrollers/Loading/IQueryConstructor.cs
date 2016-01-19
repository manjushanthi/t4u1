using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Loading
{
    /// <summary>
    /// Constructor of the queries for internsal ETL
    /// </summary>
    public interface IQueryConstructor
    {
        /// <summary>
        /// Constructs the update query.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        IDbCommand constructUpdateQuery(CrtRowIdentification ri, IEnumerable<CrtRow> inserts);
        /// <summary>
        /// Constructs the insert query.
        /// </summary>
        /// <param name="ri">The ri.</param>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        IDbCommand constructInsertQuery(CrtRowIdentification ri, IEnumerable<CrtRow> inserts);
        /// <summary>
        /// Gets the insert command.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        IDbCommand getInsertCommand(dFact fact);
        /// <summary>
        /// Constructs the query for duplication.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        IDbCommand constructQueryForDuplication(dFact fact);

        /// <summary>
        /// Constructs the query for fact.
        /// </summary>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        IDbCommand constructQueryForFact(int p1, string p2);

        /// <summary>
        /// Getds the message insert command.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <param name="InstanceID">The instance identifier.</param>
        /// <returns></returns>
        IDbCommand getdMessageInsertCommand(DataPointDuplicationException ex, int InstanceID);
    }
}
