using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain.Interfaces;
using T4UImportExportGenerator.Domain;


namespace T4UImportExportGenerator.DpmObjects
{
    public class HierarchyInfo
    {
        public IEnumerable<HierarchyAndMemberInfo> GetHierarchyAndMemberInfo(ISolvencyData dpmConn, long hierarchyID)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("select h.Hierarchyid, m.MemberLabel, m.MemberXBRLCode ");
            sb.Append("from mHierarchy h, mHierarchyNode hn, mMember m ");
            sb.Append(string.Format("where h.hierarchyid = hn.hierarchyid and hn.memberid = m.memberid and h.hierarchyid = '{0}' ", hierarchyID));

            return dpmConn.Query<HierarchyAndMemberInfo>(sb.ToString());
        }

        public IEnumerable<HierarchyAndMemberInfo> GetMetricsEnabledHierarchyAndMemberInfo(ISolvencyData dpmConn, long hierarchyID, int hierarchyStartingMemberID, int isStartingMemberIncluded)
        {
            StringBuilder sb = new StringBuilder();

            if (hierarchyStartingMemberID != 0)
            {
                sb.Append("select hn.Hierarchyid,m.MemberLabel,m.MemberXBRLCode ");
                sb.Append("from mHierarchyNode hn inner join mMember m on m.MemberID = hn.MemberID ");
                sb.Append(string.Format("where hn.HierarchyID ='{0}' and (hn.IsAbstract is null or hn.IsAbstract = 0) and (hn.Path like '%'||'{1}' ||'%' or (hn.MemberID = '{1}') = '{2}') ", hierarchyID, hierarchyStartingMemberID, isStartingMemberIncluded));
            }
            else
            {
                sb.Append("select hn.Hierarchyid,m.MemberLabel,m.MemberXBRLCode ");
                sb.Append("from mHierarchyNode hn inner join mMember m on m.MemberID = hn.MemberID ");
                sb.Append(string.Format("where hn.HierarchyID ='{0}' and (hn.IsAbstract is null or hn.IsAbstract = 0) ", hierarchyID));

            }

            return dpmConn.Query<HierarchyAndMemberInfo>(sb.ToString());
        }

        public IEnumerable<OrdinateHierarchy> GetOrdinateHierarchyMD(ISolvencyData dpmConn, string tableCode)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" select oc.OrdinateID as OrdinateID, ao.ordinatecode as OrdinateCode, m.ReferencedDomainID, m.ReferencedHierarchyID as HierarchyID, m.HierarchyStartingMemberID, m.IsStartingMemberIncluded");
            sb.Append(" from mTable t, mTableAxis ta, mAxis a, mAxisOrdinate ao, mOrdinateCategorisation oc, mMetric m ");
            sb.Append(" where t.tableid = ta.tableid and ta.axisid = a.axisid and a.axisid = ao.axisid and m.CorrespondingMemberID = oc.MemberID and ");
            sb.Append(string.Format("oc.ordinateid = ao.ordinateid and m.DataType = 'Enumeration/Code' and t.tablecode = '{0}'", tableCode));
            sb.Append(" union ");
            sb.Append(" select ao.OrdinateID  as OrdinateID, ao.ordinatecode OrdinateCode, null as ReferencedDomainID, oa.HierarchyID as HierarchyID, oa.HierarchyStartingMemberID, oa.IsStartingMemberIncluded ");
            sb.Append(" from mTable t, mTableAxis ta, mAxis a, mAxisOrdinate ao, mOpenAxisValueRestriction oa ");
            sb.Append(" where t.tableid = ta.tableid and ta.axisid = a.axisid and a.axisid = ao.axisid and a.axisid = oa.axisid and ");
            sb.Append(string.Format(" t.tablecode = '{0}'", tableCode));


            return dpmConn.Query<OrdinateHierarchy>(sb.ToString());
        }

        public IEnumerable<OrdinateHierarchy> GetOpenAxisHierarchy(ISolvencyData dpmConn, string tableCode)
        {
            StringBuilder sb = new StringBuilder();

            /*sb.Append("select ao.OrdinateID, ao.ordinatecode, oc.* ");
            sb.Append("from mTable t, mTableAxis ta, mAxis a, mAxisOrdinate ao, mOpenAxisValueRestriction oc ");
            sb.Append("where t.tableid = ta.tableid and ta.axisid = a.axisid and a.axisid = ao.axisid and oc.AxisId == a.axisid and ");
            sb.Append(string.Format("t.tablecode = '{0}'", tableCode));*/

            sb.Append("SELECT DISTINCT t.tablecode AS TableCode,  'PAGE' || o.OwnerPrefix || '_' || dim.DimensionCode AS PageColumn, oavr.hierarchyid, oavr.HierarchyStartingMemberID, oavr.IsStartingMemberIncluded , ao.ordinatecode ");
            sb.Append("FROM mtableaxis ta   INNER JOIN mtable t ON t.tableid = ta.tableid   INNER JOIN maxis a ON a.axisid = ta.axisid    LEFT OUTER JOIN mopenaxisvaluerestriction oavr ON oavr.axisid = ta.axisid     INNER JOIN maxisordinate ao ON ao.axisid = ta.axisid   INNER JOIN mordinatecategorisation oc ON oc.ordinateid = ao.ordinateid     INNER JOIN mdimension dim ON dim.dimensionid = oc.dimensionid   INNER JOIN mConcept c on c.ConceptID = dim.ConceptID    INNER JOIN mOwner o on o.OwnerID = c.OwnerID   INNER JOIN mdomain dom ON dom.domainid = dim.domainid ");
            sb.Append("WHERE ( a.axisorientation = 'Z' AND a.isopenaxis = 1 ) OR ( a.axisorientation != 'Z' AND dim.istypeddimension != 1 AND ( t.ydimval NOT LIKE '%|%' OR t.ydimval IS NULL )    AND oavr.axisid IS NOT NULL ) ");

            IEnumerable<OrdinateHierarchy> ordHie = dpmConn.Query<OrdinateHierarchy>(sb.ToString());

            return ordHie.Where(a => a.TableCode == tableCode).ToList<OrdinateHierarchy>();
        }

    }
}
