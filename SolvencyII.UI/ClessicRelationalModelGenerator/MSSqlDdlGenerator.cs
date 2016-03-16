using T4U.CRT.Generation;
using T4U.CRT.Generation.Model;
using DpmDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4U.CRT.MSSSQL.DDL.Generation
{
    public class MSSqlDdlGenerator
    {
        private  SQLiteConnector connector;
        private  IEnumerable<int> tableIDs;
        private  string filePath;
        private MssqlTableLoadingHelper tlh;

        public MSSqlDdlGenerator(SQLiteConnector dataConnector, IEnumerable<int>TableIds, string filePath)
        {
            connector = dataConnector;
            tableIDs = TableIds;
            this.filePath = filePath;
        }


        public string GenerateDdl()
        {
            RelationalTablesProcessor rtp = new RelationalTablesProcessor(filePath, connector);

            StringBuilder sb = new StringBuilder();
            ClassicRelationalTable table;
            SQLiteTableLoadingHelper tlh;
            List<string> done = new List<string>();

            foreach (int tableID in tableIDs)
            {   
                try
                {
                    table = rtp.generateRelationalTable(tableID);
                    CRTLogger.LogProgress("Generated table " + table.Name);
                }
                catch (Exception ex)
                {                 
                    continue;
                }

                int i = 1;

                try
                {
                    sb.Append(GenerateTaxTables(table));
                    CRTLogger.LogProgress("Generated DDl for table " + table.Name);
                }
                catch (Exception ex)
                {                 
                    continue;
                }
            }

            return sb.ToString();
        }

        private string GenerateTaxTables(ClassicRelationalTable table)
        {
            tlh = new MssqlTableLoadingHelper(table, new DefaultMemberResolver(connector));

            StringBuilder sb = new StringBuilder();
            //sb.Append(tlh.getTableDrop()).Append(Environment.NewLine);

            string tabDDl = tlh.getTableDDL();
            sb.Append(tabDDl).Append(Environment.NewLine);
            
            table = null;
            tlh = null;

            return sb.ToString();
        }
    }
}
