using T4U.CRT.Generation.Model;
using DpmDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation
{
    public class CRTGenerator
    {
        SQLiteTableLoadingHelper tlh;
        private IEnumerable<int> tableIDs;
        private SQLiteConnector connector;
        string filePath;

        public CRTGenerator(SQLiteConnector dataConnector, IEnumerable<int>TableIds, string filePath)
        {
            connector = dataConnector;
            tableIDs = TableIds;
            this.filePath = filePath;
        }
                
        public void GenereateCRTs(bool withSingleTransaction)
        {           
            RelationalTablesProcessor rtp = new RelationalTablesProcessor(filePath, connector);

            ClassicRelationalTable table;
            SQLiteTableLoadingHelper tlh;

            CRTLogger.LogProgress("Generated mapping table");
            List<string> done = new List<string>();
            
            foreach (int tableID in tableIDs)
            {
                //if (tableID != 144)
                //    continue;

                if(withSingleTransaction) connector.beginTransaction();

                try
                {
                    table = rtp.generateRelationalTable(tableID);
                }
                catch (Exception ex)
                {
                    if (withSingleTransaction) connector.rollBackTransaction();
                    CRTLogger.LogProgress(string.Format("Error while processing table {0} : {1}", tableID, ex.Message));
                    continue;
                }

                //int i = 1;

                try
                {
                    tlh = GenerateTaxTables(connector, ref table);
                }
                catch (Exception ex)
                {
                    if (withSingleTransaction) connector.rollBackTransaction();
                    CRTLogger.LogProgress(string.Format("Error while generating table {0} : {1}", tableID, ex.Message));
                    continue;
                }

                CRTLogger.LogProgress("Currently kb " + GC.GetTotalMemory(false) / 1000);
                if (withSingleTransaction) connector.commitTransaction();
            }
        }

        public void DropCurrentCRTs()
        {
            DataTable dt = connector.executeQuery(@"select 'drop table [' || name|| '];' from sqlite_master sm 
                                                    where 
                                                        type = 'table' 
                                                       and  sm.name like 'T_%'");
            StringBuilder sb = new StringBuilder();

            foreach (DataRow dr in dt.Rows)
                sb.AppendLine(dr[0].ToString());

            connector.executeNonQuery(sb.ToString());
        }

        public void GenerateMappingTable()
        {
            if (connector.executeQuery(SQLiteTableLoadingHelper.mappingTableExistsQuery()).Rows.Count > 0)
                connector.executeQuery(SQLiteTableLoadingHelper.getmappingtableDrop());

            connector.executeNonQuery(SQLiteTableLoadingHelper.getMappingTableDDL());
        }

        private SQLiteTableLoadingHelper GenerateTaxTables(SQLiteConnector sqliteConn, ref ClassicRelationalTable table)
        {           
            tlh = new SQLiteTableLoadingHelper(table, new DefaultMemberResolver(sqliteConn));

            if (sqliteConn.executeQuery(tlh.getQueryToCheckifTableIsInDB()).Rows.Count > 0)
                sqliteConn.executeQuery(tlh.getTableDrop());

            string tabDDl = tlh.getTableDDL();
            sqliteConn.executeNonQuery(tabDDl);
            CRTLogger.LogProgress("Generated table " + table.Name);
            List<string> inserts = tlh.getMappingInserts().ToList();

            foreach (string mapping in inserts)
                sqliteConn.executeNonQuery(mapping + ";");

            CRTLogger.LogProgress("Inserted mappings for table " + table.Name);

            table = null;
            tlh = null;
            return tlh;
        }
    }
}
