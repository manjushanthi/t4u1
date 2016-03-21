using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Data.SQLite;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Validation.Domain;

namespace SolvencyII.Validation.Query
{
    public interface IValidationQuery
    {
        /*string TableExistsQuery { get; }
        string TableDDL { get; }*/
        IEnumerable<ValidationMessage> GetArelleValidationErrors(ISolvencyData _conn, long instanceID);
        IEnumerable<ValidationMessage> GetETLErrors(ISolvencyData _conn, long instanceID);
        IEnumerable<vValidationRuleSQL> CrossTableValidationScripts(ISolvencyData _conn, int[] tableIDs);
        IEnumerable<vIntraTableSQL> IntraTableValidationScripts(ISolvencyData _conn, int[] tableIDs);
    }
}
