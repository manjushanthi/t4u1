using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction;
using System.Data;
using System.Data.Common;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers
{
    /// <summary>
    /// Analyzer of the facts number in dInstance table
    /// </summary>
    public class SQLiteFactsNumberReader : IFactsNumberReader
    {
        IFilinglIndicatorsExtractor fiExtractor;
        IDataConnector connector;
        int instanceId;

        public SQLiteFactsNumberReader(IDataConnector dataConnector, int instanceId)
        {
            fiExtractor = new NewSQLiteFillIndicatorsExtractor(dataConnector);
            connector = dataConnector;
            this.instanceId = instanceId;
        }

        public List<FactsNumber> GetTablesNumbers()
        {
            string[] tableNames = fiExtractor.getTablesNamesFromFillingIndicators(instanceId);
            if (tableNames == null || tableNames.Count() == 0)
                tableNames = fiExtractor.getTablesNamesFromModule(instanceId);
            
            List<FactsNumber> result = new List<FactsNumber>();
            FactsNumber fn;
            foreach (string tabName in tableNames)
            {
                fn = getFactNumber(tabName);
                if (fn != null)
                    result.Add(fn);
            }

            return result;
        }

        private FactsNumber getFactNumber(string tabName)
        {
            FactsNumber fn = new FactsNumber();
            fn.TableName = tabName;
            fn.rowsIds = new List<int>();

            string query = string.Format(pkQuery, tabName, this.instanceId);
            DataTable dt;
            try
            {
                dt = connector.executeQuery(query);
            }
            catch (Exception ex)
            {
                if(ex is DbException || (ex.InnerException != null && ex.InnerException is DbException))
                    return null;

                throw ex;
            }
            foreach (DataRow dr in dt.Rows)
                fn.rowsIds.Add(int.Parse(dr[0].ToString()));

            if (fn.rowsIds.Count() > 0)
            {
                dt = connector.executeQuery(string.Format(rowQuery, tabName));
                fn.factsNumber = (dt.Rows.Count - 1) * fn.rowsIds.Count;
                return fn;
            }

            return null;
        }

        string pkQuery = "select PK_ID from {0} where instance = {1}";
        //string rowQuery = "select * from {0} where PK_ID = {1}";
        string rowQuery = "PRAGMA table_info( {0} );";
    }
}
