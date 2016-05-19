using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DpmDB;
using AT2DPM.DAL.Model;

namespace MemoryProfilerProject
{
    public static class TestHelper
    {

        public static HashSet<int> getTableIDs(SQLiteConnector sqliteConn)
        {
            string query = @"select TableID from mTable";
            DataTable dt = sqliteConn.executeQuery(query);

            HashSet<int> ret = new HashSet<int>();

            foreach (DataRow dr in dt.Rows)            
                ret.Add(int.Parse(dr[0].ToString()));            

            return ret;
        }

        public static HashSet<int> getTableIDs(SQLiteConnector sqliteConn, int taxonomyId)
        {
            string query = @"select TableID from mTaxonomyTable where TaxonomyID = " + taxonomyId;
            DataTable dt = sqliteConn.executeQuery(query);
            sqliteConn.closeConnection();

            HashSet<int> ret = new HashSet<int>();

            foreach (DataRow dr in dt.Rows)
            {
                ret.Add(int.Parse(dr[0].ToString()));
            }

            return ret;
        }

        //Adding the below function excel Template Generation

        #region ForExcelTemplate Generation

        public static Dictionary<string, long> getTableIDsForExcel(SQLiteConnector sqliteConn, long moduleId)
        {
            Dictionary<string, long> tables = new Dictionary<string, long>();
            string query = @"Select TableID,Tablecode from vwGetTreeData Where ModuleID =" + moduleId;

            sqliteConn.openConnection();
           


            DataTable dt = sqliteConn.executeQuery(query);
            sqliteConn.closeConnection();

            foreach (DataRow dr in dt.Rows)
            {
                if (!tables.ContainsKey(dr[1].ToString()))
                {
                    tables.Add(dr[1].ToString(), Convert.ToInt64(dr[0]));
                }
            }

            return tables;
        }
                   
        

        public static Dictionary<string, string> HierarchyLookup2(string filePath, int hierarchyID)
        {
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            // Here we are gathering all the info required to save details for this MemberCode
            Dictionary<string, string> values = new Dictionary<string, string>();
            return ConvertToDictionary(sqliteConn.executeQuery(string.Format("SELECT Distinct [Inner] as Text, Name, IsAbstract FROM [vwGetAllHierarchies] Where HierarchyID = {0} Order by HierarchyID, HierarchyOrder ", hierarchyID)));
        }

        public static Dictionary<string, string> HierarchyLookupWithMetricsEnabled(string filePath, int hierarchyId, int startingMemberId , int isStartingMemberIncluded)
        {
            StringBuilder sb = new StringBuilder();
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            // Here we are gathering all the info required to save details for this MemberCode
            Dictionary<string, string> values = new Dictionary<string, string>();
            sb.Append(" select SUBSTR( '              ', 0, ( hn.Level - 1 ) * 3 ) || m.MemberLabel [Name], m.MemberXBRLCode [Text] ");
            sb.Append(" from mHierarchyNode hn  inner join mMember m on m.MemberID = hn.MemberID ");
            if (startingMemberId!=0)
                sb.Append(string.Format(" where hn.HierarchyID = {0}   and (hn.Path like '%'|| {1} ||'%' or (hn.MemberID = {1} and 1 = {2} )) ", hierarchyId, startingMemberId, isStartingMemberIncluded));
            else
                sb.Append(string.Format(" where hn.HierarchyID = {0} ", hierarchyId));

            sb.Append(" order by hn.[Order] ");
         
            return ConvertToDictionary(sqliteConn.executeQuery(sb.ToString()));
        }

        public static Dictionary<string, string> HierarchyLookupWithName(string filePath, int hierarchyID)
        {
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            // Here we are gathering all the info required to save details for this MemberCode
            Dictionary<string, string> values = new Dictionary<string, string>();
            return ConvertToDictionary(sqliteConn.executeQuery(string.Format("SELECT Distinct [Inner] || '    {{'  || Name || '}}' as Text ,Name, IsAbstract FROM [vwGetAllHierarchies] Where HierarchyID = {0} Order by HierarchyID, HierarchyOrder ", hierarchyID)));
        }

        public static Dictionary<string, string> HierarchyLookupWithMetrics(string filePath, int hierarchyID,int HierarchyStartingMemberID,int IsStartingMemberIncluded)
        {
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            // Here we are gathering all the info required to save details for this MemberCode
            Dictionary<string, string> values = new Dictionary<string, string>();
            StringBuilder query = new StringBuilder();
            if (HierarchyStartingMemberID > 0)
                query.Append(string.Format("select m.MemberLabel as Name,m.MemberXBRLCode as Text from mHierarchyNode hn    inner join mMember m on m.MemberID = hn.MemberID where hn.HierarchyID = {0}   and (hn.IsAbstract is null or hn.IsAbstract = 0)    and (hn.Path like '%'||{1}||'%' or (hn.MemberID = {1}) and 1 = {2})", hierarchyID, HierarchyStartingMemberID, IsStartingMemberIncluded));
            else
                query.Append(string.Format("select m.MemberLabel as Name,m.MemberXBRLCode as Text from mHierarchyNode hn    inner join mMember m on m.MemberID = hn.MemberID where hn.HierarchyID = {0}   and (hn.IsAbstract is null or hn.IsAbstract = 0) ", hierarchyID));

            query.Append(" order by hn.[Order] ");
            
            return ConvertToDictionary(sqliteConn.executeQuery(query.ToString()));
        }

        public static List<int> GetAllHierarchy(string filePath)
        {
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            // Here we are gathering all the info required to save details for this MemberCode
            List<int> values = new List<int>();
            return ConvertToList(sqliteConn.executeQuery(string.Format("SELECT Distinct HierarchyID FROM [vwGetAllHierarchies] Order by HierarchyID, HierarchyOrder  ")));
        }

         public static List<string> GetAllHierarchyListWithMetrics(string filePath)
        {
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            // Here we are gathering all the info required to save details for this MemberCode
            return ConvertToListString(sqliteConn.executeQuery(string.Format("select distinct case when met.HierarchyStartingMemberID is not null then 'E:' || met.ReferencedHierarchyID ||',' || met.HierarchyStartingMemberID || ',' || met.IsStartingMemberIncluded else 'E:' || met.ReferencedHierarchyID end from mMetric met inner join mMember mem on mem.MemberID = met.CorrespondingMemberID inner join mOrdinateCategorisation oc on oc.MemberID = mem.MemberID where met.DataType = 'Enumeration/Code' order by met.ReferencedHierarchyID")));
            
         }

         public static List<string> GetAllPageColumnsWithMetrics(string filePath)
         {
             SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
             // Here we are gathering all the info required to save details for this MemberCode
             return ConvertToListString(sqliteConn.executeQuery(string.Format(
                 "select case when oavr.HierarchyStartingMemberID is not null then 'E:' || oavr.HierarchyID ||',' || oavr.HierarchyStartingMemberID || ',' || oavr.IsStartingMemberIncluded else 'E:' || oavr.HierarchyID end from mOpenAxisValueRestriction oavr inner join mAxisOrdinate ao on ao.AxisID = oavr.AxisID inner join mOrdinateCategorisation oc on oc.OrdinateID = ao.OrdinateID inner join mDimension dim on dim.DimensionID = oc.DimensionID inner join mConcept c on c.ConceptID = dim.ConceptID inner join mOwner o on o.OwnerID = c.OwnerID")));

         }

         private static List<string> ConvertToListString(System.Data.DataTable dataTable)
         {
             List<string> hierarchyLst = new List<string>();
             foreach (DataRow row in dataTable.Rows)
                 hierarchyLst.Add(row[0].ToString());
             return (hierarchyLst);


         }     


        private static List<int> ConvertToList(System.Data.DataTable dataTable)
        {
            List<int> hierarchyLst = new List<int>();
            foreach (DataRow row in dataTable.Rows)
            {
               int convertedVal;
               bool res= int.TryParse(row[0].ToString(), out convertedVal);
                if(res)
                {
                    if (convertedVal != 0)
                        hierarchyLst.Add(convertedVal);
                }
            }

            return (hierarchyLst);


        }
 
        
        private static Dictionary<string, string> ConvertToDictionaryEnumeration(System.Data.DataTable dataTable)
        {
            Dictionary<string, string> val = new Dictionary<string, string>();
            foreach (DataRow row in dataTable.Rows)
            {
                val.Add(row["m.MemberLabel"].ToString(), row["m.MemberXBRLCode"].ToString());

            }
            return (val);

        }

        private static Dictionary<string, string> ConvertToDictionary(System.Data.DataTable dataTable)
        {
            Dictionary<string, string> val = new Dictionary<string, string>();
            foreach (DataRow row in dataTable.Rows)
            {
                val.Add(row["Name"].ToString(), row["Text"].ToString());

            }
            return (val);

        }

        #endregion

    }
}
