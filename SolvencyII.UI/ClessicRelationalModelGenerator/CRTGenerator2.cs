using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

using T4U.CRT.Generation.Model;
using DpmDB;

namespace T4U.CRT.Generation
{
    public class CRTGenerator2
    {
        SQLiteTableLoadingHelper tlh;
        private IEnumerable<int> tableIDs;
        //private SQLiteConnector connector;
        string filePath;

        BackgroundWorker asyncWorker;
        int percentage;

        public CRTGenerator2(IEnumerable<int> TableIds, string filePath)
        {
            tableIDs = TableIds;
            this.filePath = filePath;
        }

        public void CancelAsync()
        {
            if (asyncWorker != null)
                asyncWorker.CancelAsync();
        }

        public bool IsBusy()
        {
            return asyncWorker != null ? asyncWorker.IsBusy : false;
        }

        void InitializeBackgroundWorker(ProgressChangedEventHandler crtProgress, RunWorkerCompletedEventHandler crtComplete)
        {
            if (asyncWorker != null)
            {
                if (asyncWorker.IsBusy)
                    throw new CRTGenerationException("A CRT generation process is already running. Cannot run more than one CRT generation process at the same time.");

                asyncWorker.Dispose();
            }

            asyncWorker = new BackgroundWorker();
            asyncWorker.WorkerSupportsCancellation = true;
            asyncWorker.WorkerReportsProgress = true;
            asyncWorker.ProgressChanged += crtProgress;
            asyncWorker.RunWorkerCompleted += crtComplete;
        }

        public void GenereateCRTAsync(ProgressChangedEventHandler pruneProgress, RunWorkerCompletedEventHandler pruneComplete)
        {
            InitializeBackgroundWorker(pruneProgress, pruneComplete);

            asyncWorker.DoWork += delegate(object sender, DoWorkEventArgs e)
            {
                GenereateCRTs();
            };

            asyncWorker.RunWorkerAsync();

        }

        public void GenereateCRT(ProgressChangedEventHandler pruneProgress, RunWorkerCompletedEventHandler pruneComplete)
        {
            InitializeBackgroundWorker(pruneProgress, pruneComplete);

            GenereateCRTs();

        }

        public void GenereateCRTs()
        {
            SQLiteConnector connector = new SQLiteConnector(filePath);
            RelationalTablesProcessor rtp = new RelationalTablesProcessor(filePath, connector);

            ClassicRelationalTable table;
            SQLiteTableLoadingHelper tlh;

            connector.openConnection();

            GenerateMappingTable(connector);
            DropCurrentCRTs(connector);

            int prg = 0;

            asyncWorker.ReportProgress(percentage, "Generated mapping table");
            List<string> done = new List<string>();

            try
            {
                foreach (int tableID in tableIDs)
                {
                    connector.beginTransaction();

                    prg++; //To show how much is progressing
                    percentage = (int)(((decimal)prg / (decimal)tableIDs.Count()) * 100);

                    try
                    {

                        table = rtp.generateRelationalTable(tableID);
                    }
                    catch (Exception ex)
                    {
                        connector.rollBackTransaction();
                        asyncWorker.ReportProgress(percentage, string.Format("Error while processing table {0} : {1}", tableID, ex.Message));
                        continue;
                    }

                    //int i = 1;

                    try
                    {
                        tlh = GenerateTaxTables(connector, ref table);
                    }
                    catch (Exception ex)
                    {
                        connector.rollBackTransaction();
                        asyncWorker.ReportProgress(percentage, string.Format("Error while generating table {0} : {1}", tableID, ex.Message));
                        continue;
                    }

                    asyncWorker.ReportProgress(percentage, "Currently kb " + GC.GetTotalMemory(false) / 1000);
                    connector.commitTransaction();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                connector.closeConnection();
            }
        }

        private void DropCurrentCRTs(SQLiteConnector connector)
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

        private void GenerateMappingTable(SQLiteConnector connector)
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
            asyncWorker.ReportProgress(percentage, "Generated table " + table.Name);
            List<string> inserts = tlh.getMappingInserts().ToList();

            foreach (string mapping in inserts)
                sqliteConn.executeNonQuery(mapping + ";");

            asyncWorker.ReportProgress(percentage, "Inserted mappings for table " + table.Name);

            table = null;
            tlh = null;
            return tlh;
        }
    }
}
