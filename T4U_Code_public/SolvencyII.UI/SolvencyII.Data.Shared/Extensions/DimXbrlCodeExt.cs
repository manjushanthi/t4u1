using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Data.Shared.Extensions
{
    /// <summary>
    /// Extension used to get IDs from a given DimXbrlCode
    /// </summary>
    public static class DimXbrlCodeExt
    {
        public static long GetDimensionIDFromXbrlCode(this DimXbrlCode xbrlCode, ISolvencyData conn)
        {
            string query = @"select d.DimensionID from mDimension d inner join mConcept c on c.ConceptID = d.ConceptID inner join mOwner o on o.OwnerID == c.OwnerID where d.DimensionCode = ""{0}"" and o.OwnerPrefix = ""{1}"" ";
            return conn.ExecuteScalar<long>(string.Format(query, xbrlCode.DimCode, xbrlCode.Prefix));
        }

        public static long GetAxisIDFromXbrlCode(this DimXbrlCode xbrlCode, long tableID, ISolvencyData conn)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ao.AxisID from mDimension d ");
            sb.Append(" inner join mConcept c on c.ConceptID = d.ConceptID ");
            sb.Append(" inner join mOwner o on o.OwnerID == c.OwnerID ");
            sb.Append("inner join mOrdinateCategorisation oc on oc.DimensionID = d.DimensionID ");
            sb.Append(" inner join mAxisOrdinate ao on ao.OrdinateID = oc.OrdinateID ");
            sb.Append("inner join mAxis a on a.AxisID = ao.AxisID ");
            sb.Append("inner join mTableAxis ta on ta.AxisID = a.AxisID ");
            sb.Append(@"where d.DimensionCode = ""{0}"" and o.OwnerPrefix = ""{1}"" and ta.TableID = {2} ");

            
            return conn.ExecuteScalar<long>(string.Format(sb.ToString(), xbrlCode.DimCode, xbrlCode.Prefix, tableID));
        }


    }
}
