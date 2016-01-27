using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation
{
    public class TableInfo
    {
        public static IEnumerable<mTable> GetTable(ISolvencyData dpmConn, string tableCode)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select TableID, TableCode, TableLabel, XbrlFilingIndicatorCode, XbrlTableCode ");
            sb.Append("from mTable ");
            sb.Append(string.Format("where TableCode = '{0}' ", tableCode));

            return dpmConn.Query<mTable>(sb.ToString());
        }

        public static IEnumerable<mTable> GetAllTables(ISolvencyData dpmConn)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select TableID, TableCode, TableLabel, XbrlFilingIndicatorCode, XbrlTableCode ");
            sb.Append("from mTable ");
            //sb.Append(string.Format("where TableCode = '{0}' ", tableCode));

            return dpmConn.Query<mTable>(sb.ToString());
        }

        public static string GetTableName(mTable table, mTaxonomy taxonomy)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("T__");
            sb.Append(table.TableCode.Replace('.', '_'));
            sb.Append("__");
            sb.Append(taxonomy.TaxonomyCode.Trim());
            sb.Append("__");
            sb.Append(taxonomy.Version.Replace('.', '_'));

            return sb.ToString();
        }
    }
}
