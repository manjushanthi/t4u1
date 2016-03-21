using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.Shared;
using SolvencyII.Domain;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation
{
    public static class DataTypeValidationSqlHelper
    {
        private static SQLiteConnector SQLiteConnector { get; set; }
        private static string DatabaseFileName { get; set; }


        public static void Initializer(string databaseFileName)
        {
            SQLiteConnector = new SQLiteConnector(databaseFileName);
            DatabaseFileName = databaseFileName;
        }

        public static Dictionary<string, PocoColInfo> GetTableColumnInfo(string tableName)
        {
            List<PocoColInfo> pocoColInfos = null;

            using (GetSQLData getData = new GetSQLData(DatabaseFileName))
            {
                pocoColInfos = getData.GetTableInfo(tableName);
            }

            Dictionary<string, SolvencyII.Domain.Entities.PocoColInfo> dictPocoColInfos = new Dictionary<string, PocoColInfo>();
            if (pocoColInfos != null)
            {
                foreach (SolvencyII.Domain.Entities.PocoColInfo pocoColInfo in pocoColInfos)
                {
                    if (!dictPocoColInfos.ContainsKey(pocoColInfo.name))
                    {
                        dictPocoColInfos.Add(pocoColInfo.name, pocoColInfo);
                    }
                }
            }
            return dictPocoColInfos;
        }

        public static string GetAllColumnAsTextQuery(Dictionary<string, PocoColInfo> dictPocoColInfos)
        {

            StringBuilder sb = new StringBuilder();

            foreach (var pocoColInfo in dictPocoColInfos)
            {
                sb.Append(@" cast(" + pocoColInfo.Key + " as text) " + pocoColInfo.Key + ", ");

            }
            string result = sb.ToString();
            if (!string.IsNullOrEmpty(result))
            {
                result = result.Remove(result.Length - 2, 2);
            }
            return result;
        }

        public static int GetTotalTableRows(ISolvencyData sqliteConnection, dInstance instance, string tableCode)
        {
            int rows = 0;
            SolvencyII.Domain.mTable table = TableInfo.GetTable(sqliteConnection, tableCode).FirstOrDefault();
            SolvencyII.Domain.mTaxonomy taxonomy = TaxonomyInfo.GetTaxonomy(sqliteConnection, 1).FirstOrDefault();
            string tableName = GetTableName(table, taxonomy);

            string query = string.Format("select count(*) from {0} where instance = {1} ", tableName, instance.InstanceID);

            rows = sqliteConnection.ExecuteScalar<int>(query);

            return rows;
        }

        private static string GetTableName(mTable table, mTaxonomy taxonomy)
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

        public static DataTable GetPageColumnDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT DISTINCT t.tablecode        AS TableCode,'PAGE' || o.OwnerPrefix || '_' || dim.DimensionCode  AS PAGECOLUMN, ao.ordinatelabel   AS PAGECOLUMN_LABEL,  CASE  ");
            sb.Append(" WHEN dim.istypeddimension = 1 THEN dom.datatype    ELSE    CASE   WHEN oavr.hierarchystartingmemberid IS NOT NULL THEN 'E:'    ||  ");
            sb.Append(" oavr.hierarchyid  ELSE 'E:'  || oavr.hierarchyid   END   END     AS PAGECOLUMN_DATATYPE,       'Z'  ");
            sb.Append(" || ao.ordinatecode AS PAGECOLUMN_CODE FROM   mtableaxis ta   INNER JOIN mtable t  ON t.tableid = ta.tableid  INNER JOIN maxis a  ON a.axisid = ta.axisid  ");
            sb.Append(" LEFT OUTER JOIN mopenaxisvaluerestriction oavr  ON oavr.axisid = ta.axisid  ");
            sb.Append(" INNER JOIN maxisordinate ao ON ao.axisid = ta.axisid  ");
            sb.Append(" INNER JOIN mordinatecategorisation oc ON oc.ordinateid = ao.ordinateid  ");
            sb.Append(" INNER JOIN mdimension dim ON dim.dimensionid = oc.dimensionid inner join mConcept c on c.ConceptID = dim.ConceptID ");
            sb.Append(" inner join mOwner o on o.OwnerID = c.OwnerID INNER JOIN mdomain dom ON dom.domainid = dim.domainid WHERE  ( a.axisorientation = 'Z'  ");
            sb.Append(" AND a.isopenaxis = 1 )  OR ( a.axisorientation != 'Z'  AND dim.istypeddimension != 1   AND ( t.ydimval NOT LIKE '%|%'  OR t.ydimval IS NULL )  ");
            sb.Append(" AND oavr.axisid IS NOT NULL ) ORDER  BY t.tablecode; ");
            DataTable dataTable = executeQuery(sb.ToString());
            return dataTable;
            
        }

        private static DataTable executeQuery(string query)
        {
            return SQLiteConnector.executeQuery(query);
        }

        public static DataTable GetCRTDataTableSchema(string tableName, dInstance instance)
        {
            Dictionary<string, PocoColInfo> dictPocoColInfos = DataTypeValidationSqlHelper.GetTableColumnInfo(tableName);

            string columnAsTextQuery = DataTypeValidationSqlHelper.GetAllColumnAsTextQuery(dictPocoColInfos);

            string query = string.Format("select {0} from {1} where instance = {2} ", columnAsTextQuery, tableName, instance.InstanceID);


            DataTable dataTable = executeQuery(query);

            return dataTable;
        }

        

        public static void CloseConnection()
        {
            if (SQLiteConnector != null)
            {
                SQLiteConnector.closeConnection();
            }
        }

        public static DataTable GetOrdinateHierarchyID_HD_Table()
        {
            DataTable dt = null;
            try
            {
                string query = string.Format("Select HierarchyID as hierarchyID,OrdinateID,HierarchyStartingMemberID,IsStartingMemberIncluded from vwGetOrdinateHierarchyID_HD ");
                dt = SQLiteConnector.executeQuery(query);
   
                return dt;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return dt;
            }

        }

        public static DataTable GetOrdinateHierarchyID_MD_Table()
        {
            DataTable dt = null;
            try
            {
                string query = string.Format("Select HierarchyID  as hierarchyID,OrdinateID,HierarchyStartingMemberID,IsStartingMemberIncluded from vwGetOrdinateHierarchyID_MD ");
                dt = SQLiteConnector.executeQuery(query);
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return dt;
            }
        }

        public static Dictionary<string, string> HierarchyLookup2(int hierarchyID)
        {
            return ConvertToDictionary(SQLiteConnector.executeQuery(string.Format("SELECT Distinct [Inner] as Text, Name, IsAbstract FROM [vwGetAllHierarchies] Where HierarchyID = {0} Order by HierarchyID, HierarchyOrder ", hierarchyID)));
        }

        public static Dictionary<string, string> HierarchyLookupWithMetricsEnabled(int hierarchyId, int startingMemberId, int isStartingMemberIncluded)
        {
            StringBuilder sb = new StringBuilder();

            if (startingMemberId != 0)
            {
                sb.Append("select m.MemberLabel [Text] ,m.MemberXBRLCode [Name] ");
                sb.Append("from mHierarchyNode hn inner join mMember m on m.MemberID = hn.MemberID ");
                sb.Append(string.Format("where hn.HierarchyID ='{0}' and (hn.IsAbstract is null or hn.IsAbstract = 0) and (hn.Path like '%'||'{1}' ||'%' or (hn.MemberID = '{1}') = '{2}') ", hierarchyId, startingMemberId, isStartingMemberIncluded));
            }
            else
            {
                sb.Append("select m.MemberLabel [Text] ,m.MemberXBRLCode [Name] ");
                sb.Append("from mHierarchyNode hn inner join mMember m on m.MemberID = hn.MemberID ");
                sb.Append(string.Format("where hn.HierarchyID ='{0}' and (hn.IsAbstract is null or hn.IsAbstract = 0) ", hierarchyId));

            }

            return ConvertToDictionary(SQLiteConnector.executeQuery(sb.ToString()));
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
    }
}
