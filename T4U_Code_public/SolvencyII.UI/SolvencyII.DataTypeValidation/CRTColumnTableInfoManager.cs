using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using T4U.CRT.Generation;
using T4U.CRT.Generation.ExcelTemplateProcessor;

namespace SolvencyII.DataTypeValidation
{
    public class CRTColumnTableInfoManager : IDisposable
    {
        private DataTable pageColumnDataTable { get; set; }

        private RelationalTablesProcessor relationalTablesProcessor { get; set; }

        private DpmDB.SQLiteConnector sqliteConnector { get; set; }

        public CRTColumnTableInfoManager(string databaseFileName)
        {
            HDOrdinateHierarchyIdList.HDOrdinateHierarchyIdListTable = DataTypeValidationSqlHelper.GetOrdinateHierarchyID_HD_Table();
            MDOrdinateHierarchyIdList.MDOrdinateHierarchyIdTable = DataTypeValidationSqlHelper.GetOrdinateHierarchyID_MD_Table();
            pageColumnDataTable = DataTypeValidationSqlHelper.GetPageColumnDetails();
            DpmDB.SQLiteConnector sqliteConnector = new DpmDB.SQLiteConnector(databaseFileName);
            relationalTablesProcessor = new RelationalTablesProcessor(databaseFileName, sqliteConnector);

        }
        public ExcelTemplateColumns GetExcelTemplateColumns(mTable table)
        {
            ExcelTemplateColumns obj = relationalTablesProcessor.getColumnsForExcel(table, pageColumnDataTable);
            return (obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (sqliteConnector != null)
                {
                    sqliteConnector.closeConnection();
                }
            }
        }
    }
}
