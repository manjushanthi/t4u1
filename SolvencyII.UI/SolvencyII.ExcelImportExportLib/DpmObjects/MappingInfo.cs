using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.ExcelImportExportLib.DpmObjects
{
    class ColType
    {
        public string datatypen { get; set; }
    }

    public class MappingInfo
    {
        public IEnumerable<MAPPING> GetMappingInfo(ISolvencyData dpmConn, long tableID)
        {
            string mappingQuery = string.Format("select * from mapping where table_version_id = {0} ", tableID);

            return dpmConn.Query<MAPPING>(mappingQuery).ToList<MAPPING>();
        }

        public string GetPageColumnDatatype(ISolvencyData dpmConn, long tableID, string pageCode)
        {
            StringBuilder str = new StringBuilder();

            str.Append(" select distinct case when ovar.AxisID is not null then'E' else dom.datatype end datatypen ");
            str.Append(" from MAPPING m ");
            str.Append(" inner join mTableAxis ta on ta.TableID = m.TABLE_VERSION_ID ");
            str.Append(" inner join mAxis a on a.AxisID = ta.AxisID ");
            str.Append(" inner join mAxisOrdinate ao on ao.AxisID = a.AxisID ");
            str.Append(" inner join mOrdinateCategorisation oc on oc.OrdinateID = ao.OrdinateID ");
            str.Append(" inner join mDimension d on d.DimensionID = oc.DimensionID and d.DimensionCode = substr(m.DYN_TAB_COLUMN_NAME, instr(m.DYN_TAB_COLUMN_NAME, '_')+1) ");
            str.Append(" inner join mDomain dom on dom.DomainID = d.DomainID ");
            str.Append(" left join mOpenAxisValueRestriction ovar on ovar.AxisID = a.AxisID ");
            str.Append(string.Format(" where m.TABLE_VERSION_ID = {0} and upper(m.DYN_TAB_COLUMN_NAME) like upper('{1}')",tableID, pageCode));


            return dpmConn.Query<ColType>(str.ToString()).FirstOrDefault().datatypen;
        }

        public string[] GetZAxisPageName(ISolvencyData dpmConn, string tableCode)
        {
            StringBuilder str = new StringBuilder();

            str.Append("  select  'PAGE' || o.OwnerPrefix || '_' || d.DimensionCode as datatypen  ");
            str.Append(" from mTable t, mTableAxis ta, mAxis a, mAxisOrdinate ao, mOrdinateCategorisation oc, mDimension d, mConcept c, mOwner o ");
            str.Append(" where t.tableid = ta.tableid and ta.AxisID = a.AxisID and a.AxisID = ao.AxisID and ao.OrdinateID = oc.OrdinateID and oc.DimensionID = d.DimensionID and a.AxisOrientation = 'Z' and a.IsOpenAxis = 1 and d.ConceptID = c.ConceptID and c.OwnerID = o.OwnerID and ");
            str.Append(string.Format(" t.TableCode = '{0}' ", tableCode));


            return dpmConn.Query<ColType>(str.ToString()).Select(t => t.datatypen).ToArray<string>();
        }
    }
}
