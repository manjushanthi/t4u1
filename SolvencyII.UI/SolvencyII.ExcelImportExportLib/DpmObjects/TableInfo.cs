using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;


namespace SolvencyII.ExcelImportExportLib.DpmObjects
{
    public class TableInfo
    {
        public IEnumerable<mTable> GetTable(ISolvencyData dpmConn, string tableCode)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select TableID, TableCode, TableLabel, XbrlFilingIndicatorCode, XbrlTableCode ");
            sb.Append("from mTable ");
            sb.Append(string.Format("where TableCode = '{0}' ", tableCode));

            return dpmConn.Query<mTable>(sb.ToString());
        }

        public IEnumerable<mTable> GetAllTables(ISolvencyData dpmConn)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select TableID, TableCode, TableLabel, XbrlFilingIndicatorCode, XbrlTableCode ");
            sb.Append("from mTable ");
            //sb.Append(string.Format("where TableCode = '{0}' ", tableCode));

            return dpmConn.Query<mTable>(sb.ToString());
        }
    }
}
