using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Data.SQLite;
using SolvencyII.Data.SQL;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;


namespace SolvencyII.Data.Shared
{
    public static class ConnectionFactory
    {
        public static ISolvencyData GetConnection(eDataTier tier, string connString)
        {
            switch(tier)
            {
                case eDataTier.SqLite:
                    return new SQLiteConnection(connString);
                case eDataTier.SqlServer:
                    return new DataConnection(connString);
                default:
                    return null;

            }
        }
    }
}
