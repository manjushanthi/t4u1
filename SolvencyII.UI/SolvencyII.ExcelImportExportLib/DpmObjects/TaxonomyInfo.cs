using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;

namespace SolvencyII.ExcelImportExportLib.DpmObjects
{
    public class TaxonomyInfo
    {
        public IEnumerable<mTaxonomy> GetTaxonomy(ISolvencyData dpmConn, long taxonomyID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select * ");
            sb.Append("from mTaxonomy ");
            sb.Append(string.Format(" where TaxonomyID = {0} ", taxonomyID));

            return dpmConn.Query<mTaxonomy>(sb.ToString());
        }
    }
}
