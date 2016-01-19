using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation
{
    public static class TaxonomyInfo
    {
        public static IEnumerable<mTaxonomy> GetTaxonomy(ISolvencyData dpmConn, long taxonomyID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select * ");
            sb.Append("from mTaxonomy ");
            sb.Append(string.Format(" where TaxonomyID = {0} ", taxonomyID));

            return dpmConn.Query<mTaxonomy>(sb.ToString());
        }
    }
}