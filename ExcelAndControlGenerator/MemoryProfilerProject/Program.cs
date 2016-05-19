using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T4U.CRT.Generation;
using T4U.CRT.Generation.Model;
using DpmDB.BusinessData;
using DpmDB;
using AT2DPM.DAL.Model;
using T4U.CRT.Generation.ExcelTemplateProcessor;
using System.Data;
using System.IO;
using System.Reflection;


namespace MemoryProfilerProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            CRTLogger.Progress += logToConsole;
            if(args.Length == 0 || args == null)
                RunApp();
            else
                foreach (string arg in args)
                    RunApp(arg);       

            Console.ReadLine();
        }

        private static void RunApp(string filePath)
        {
            try
            {
                GnerateTaxTables(filePath);
            }
            catch (Exception ex)
            {
                logToConsole(ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "")); 
                //Console.ReadLine();
            }
        }

        private static void RunApp()
        {
            try
            {
                Console.WriteLine("DB file location: ");
                string filePath = Console.ReadLine();
                GnerateTaxTables(filePath);
                }
                catch (Exception ex)
                {
                    logToConsole(ex.Message + (ex.InnerException != null ? ex.InnerException.Message : ""));         
                }
            RunApp(); 
        }

        static void GnerateTaxTables(string filePath)
        {
            logFilePath = filePath + ".log";
            logToConsole(filePath);
            SQLiteConnector connector = new SQLiteConnector(filePath);
            DateTime start = DateTime.Now;

            connector.openConnection();
            //connector.beginTransaction();

            HashSet<int> tabIds = TestHelper.getTableIDs(connector);
            CRTGenerator crtGen = new CRTGenerator(connector, tabIds, filePath);
            crtGen.GenerateMappingTable();
            crtGen.DropCurrentCRTs();
            crtGen.GenereateCRTs(true);

            //connector.commitTransaction();
            connector.closeConnection();

            DateTime end = DateTime.Now;

            Console.WriteLine("Done in " + (end - start).TotalSeconds + " sec");
            //Console.ReadLine();
        }

        static string logFilePath;
        private static void logToConsole(string message)
        {
            Console.WriteLine(string.Format("{0} - {1}", DateTime.Now.ToLongTimeString(), message));
            
           if(string.IsNullOrWhiteSpace(logFilePath)) logFilePath = Assembly.GetExecutingAssembly().Location + ".log";
           File.AppendAllText(logFilePath, message + Environment.NewLine);
        }

        #region ExcelTemplate Generation
        //below function for excel template generation

        public static Dictionary<string, long> ExcelTemplateGeneration(string filePath, long moduleId)
        {
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            RelationalTablesProcessor rtp = new RelationalTablesProcessor(filePath, sqliteConn);
            Dictionary<string, long> tables = TestHelper.getTableIDsForExcel(sqliteConn, moduleId);
            return (tables);
        }
       

        public static ExcelTemplateColumns getColumns(string filePath, mTable tableName, DataTable pageColumnTable)
        {
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            RelationalTablesProcessor rtp = new RelationalTablesProcessor(filePath, sqliteConn);
            ExcelTemplateColumns obj = new ExcelTemplateColumns();
            obj = rtp.getColumnsForExcel(tableName,pageColumnTable);
            sqliteConn.closeConnection();
            return (obj);
        }


        public static DataTable GetPageColumnDetails(string filePath)
        {
            SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
            RelationalTablesProcessor rtp = new RelationalTablesProcessor(filePath, sqliteConn);
            StringBuilder sb = new StringBuilder();
            //sb.Append(" SELECT DISTINCT t.tablecode        AS TableCode,'PAGE' || o.OwnerPrefix || '_' || dim.DimensionCode  AS PAGECOLUMN, ao.ordinatelabel   AS PAGECOLUMN_LABEL,  CASE  ");
            //sb.Append(" WHEN dim.istypeddimension = 1 THEN dom.datatype    ELSE    CASE   WHEN oavr.hierarchystartingmemberid IS NOT NULL THEN 'E:'    ||  ");
            //sb.Append(" oavr.hierarchyid  ELSE 'E:'  || oavr.hierarchyid   END   END     AS PAGECOLUMN_DATATYPE,       'Z'  ");
            //sb.Append(" || ao.ordinatecode AS PAGECOLUMN_CODE FROM   mtableaxis ta   INNER JOIN mtable t  ON t.tableid = ta.tableid  INNER JOIN maxis a  ON a.axisid = ta.axisid  ");
            //sb.Append(" LEFT OUTER JOIN mopenaxisvaluerestriction oavr  ON oavr.axisid = ta.axisid  ");
            //sb.Append(" INNER JOIN maxisordinate ao ON ao.axisid = ta.axisid  ");
            //sb.Append(" INNER JOIN mordinatecategorisation oc ON oc.ordinateid = ao.ordinateid  ");
            //sb.Append(" INNER JOIN mdimension dim ON dim.dimensionid = oc.dimensionid inner join mConcept c on c.ConceptID = dim.ConceptID ");
            //sb.Append(" inner join mOwner o on o.OwnerID = c.OwnerID INNER JOIN mdomain dom ON dom.domainid = dim.domainid WHERE  ( a.axisorientation = 'Z'  ");
            //sb.Append(" AND a.isopenaxis = 1 )  OR ( a.axisorientation != 'Z'  AND dim.istypeddimension != 1   AND ( t.ydimval NOT LIKE '%|%'  OR t.ydimval IS NULL )  ");
            //sb.Append(" AND oavr.axisid IS NOT NULL ) ORDER  BY t.tablecode; ");


            sb.Append("  SELECT DISTINCT t.tablecode AS TableCode, ");
            sb.Append(" 'PAGE' || o.OwnerPrefix || '_' || dim.DimensionCode AS PAGECOLUMN, ");
            sb.Append("  ao.ordinatelabel AS PAGECOLUMN_LABEL, ");
            sb.Append(" CASE  ");
            sb.Append("   WHEN dim.istypeddimension = 1 THEN dom.datatype ");
            sb.Append("    ELSE CASE WHEN oavr.hierarchystartingmemberid IS NOT NULL THEN 'E:' || oavr.hierarchyid || ',' || oavr.HierarchyStartingMemberID ||  ',' || oavr.IsStartingMemberIncluded ");
            sb.Append("        ELSE 'E:' || oavr.hierarchyid END  ");
            sb.Append("            END AS PAGECOLUMN_DATATYPE,  ");
            sb.Append("             'Z' || ao.ordinatecode AS PAGECOLUMN_CODE  ");
            sb.Append("  FROM mtableaxis ta ");
            sb.Append("  INNER JOIN mtable t ON t.tableid = ta.tableid ");
            sb.Append("  INNER JOIN maxis a ON a.axisid = ta.axisid  ");
            sb.Append("  LEFT OUTER JOIN mopenaxisvaluerestriction oavr ON oavr.axisid = ta.axisid  ");
            sb.Append("   INNER JOIN maxisordinate ao ON ao.axisid = ta.axisid  ");
            sb.Append(" INNER JOIN mordinatecategorisation oc ON oc.ordinateid = ao.ordinateid  ");
            sb.Append("   INNER JOIN mdimension dim ON dim.dimensionid = oc.dimensionid  ");
            sb.Append(" INNER JOIN mConcept c on c.ConceptID = dim.ConceptID  ");
            sb.Append("  INNER JOIN mOwner o on o.OwnerID = c.OwnerID  ");
            sb.Append(" INNER JOIN mdomain dom ON dom.domainid = dim.domainid  ");
            sb.Append("  WHERE ( a.axisorientation = 'Z' AND a.isopenaxis = 1 ) OR ( a.axisorientation != 'Z' AND dim.istypeddimension != 1 AND ( t.ydimval NOT LIKE '%|%' OR t.ydimval IS NULL )  ");
            sb.Append("  AND oavr.axisid IS NOT NULL ) ORDER BY t.tablecode; ");


            DataTable dt = sqliteConn.executeQuery(sb.ToString());
            sqliteConn.closeConnection();
            return (dt);

        }
        
        public static DataTable GetOrdinateHierarchyID_HD_Table(string filePath)
        {
            DataTable dt = null;
            try
            {
                SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
                //int result = sqliteConn.executeScalar(string.Format("Select HierarchyID from vwGetOrdinateHierarchyID_HD Where OrdinateID = {0} ", ordinateID));               
                dt = sqliteConn.executeQuery(string.Format("Select HierarchyID as hierarchyID,OrdinateID,HierarchyStartingMemberID,IsStartingMemberIncluded from vwGetOrdinateHierarchyID_HD "));
                sqliteConn.closeConnection();
                return dt;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return dt;
            }

        }

        public static DataTable GetOrdinateHierarchyID_MD_Table(string filePath)
        {
            DataTable dt = null;
            try
            {
                SQLiteConnector sqliteConn = new SQLiteConnector(filePath);
                //int result = sqliteConn.executeScalar(string.Format("Select HierarchyID from vwGetOrdinateHierarchyID_HD Where OrdinateID = {0} ", ordinateID));               
                dt = sqliteConn.executeQuery(string.Format("Select HierarchyID  as hierarchyID,OrdinateID,HierarchyStartingMemberID,IsStartingMemberIncluded from vwGetOrdinateHierarchyID_MD "));
                sqliteConn.closeConnection();
                return dt;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return dt;
            }
        }
     
        #endregion

    }
}
