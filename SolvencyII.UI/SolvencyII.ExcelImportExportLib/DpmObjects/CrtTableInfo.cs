using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;
using SolvencyII.ExcelImportExportLib.Utils;

namespace SolvencyII.ExcelImportExportLib.DpmObjects
{
    public class PageGroup
    {
        public string String1 { get; set; }
        public string String2 { get; set; }
        public string String3 { get; set; }
        public string String4 { get; set; }
        public string String5 { get; set; }
    }
    public class CrtTableInfo
    {
        public IEnumerable<PageGroup> GetTable(ISolvencyData dpmConn, string tableCode, long instanceId, string select, string groupby)
        {
            StringBuilder sb = new StringBuilder();
            mTable table = (new TableInfo().GetTable(dpmConn, tableCode)).FirstOrDefault();
            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(dpmConn, 1)).FirstOrDefault();
            string tableName = Helper.GetTableName(taxonomy, table);

            sb.Append(string.Format(" select {0} from {1} where instance = {2} group by {3} ", select, tableName, instanceId, groupby));

            return dpmConn.Query<PageGroup>(sb.ToString());
        }

        public IEnumerable<object> GetCrtTableDataByPage(ISolvencyData dpmConn, string tableCode, string where)
        {
            StringBuilder sb = new StringBuilder();
            mTable table = (new TableInfo().GetTable(dpmConn, tableCode)).FirstOrDefault();
            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(dpmConn, 1)).FirstOrDefault();
            string tableName = Helper.GetTableName(taxonomy, table);

            sb.Append(string.Format(" select * from {0} where {1} ", tableName, where));

            Type t = Helper.ReferencedLookup(tableName);

            return dpmConn.Query(t, sb.ToString()).ToList();
        }
    }
}
