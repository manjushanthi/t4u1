using AT2DPM.DAL;
using AT2DPM.DAL.Model;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.Shared;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using T4U.CRT.Generation.ExcelTemplateProcessor;

namespace SolvencyII.DataTypeValidation
{
    public class DataTypeTableValidation
    {
        private CRTColumnTableInfoManager crtColumnTableInfoManager { get; set; }
        private string databaseFileName { get; set; }
        private ISolvencyData sqliteConnection { get; set; }

        public DataTypeTableValidation(string databaseFileName, ISolvencyData sqliteConnection)
        {
            DataTypeValidationDALHelper.Initializer(databaseFileName);
            DataTypeValidationSqlHelper.Initializer(databaseFileName);
            crtColumnTableInfoManager = new CRTColumnTableInfoManager(databaseFileName);
            this.databaseFileName = databaseFileName;

        }

        public List<DataTypeValidationResult> ValidateTable(SolvencyII.Domain.dInstance instance, string tableCode)
        {
            List<DataTypeValidationResult> dataTypeValidationResults = null;

            //Get the table object, that needs to be validated
            mTable validationTable = DataTypeValidationDALHelper.GetTable(tableCode);

            if (validationTable == null)
                return dataTypeValidationResults;

            string tableName = DataTypeValidationDALHelper.GetTableName(tableCode);

            try
            {
                //Get the CRT details to the current table to be validated
                DataTable crtDataTable = DataTypeValidationSqlHelper.GetCRTDataTableSchema(tableName, instance);

                //Get the POCO-Column details details to the current table to be validated, its required to construct the query
                Dictionary<string, PocoColInfo> dictPocoColInfos = DataTypeValidationSqlHelper.GetTableColumnInfo(tableName);


                //Get teh rows,columns,columnsCodes and datatypes details to the current table to be validated
                ExcelTemplateColumns excelTemplateColumns = crtColumnTableInfoManager.GetExcelTemplateColumns(validationTable);

                DataTypeDataTableValidation dataTypeDataTableValidation = new DataTypeDataTableValidation(tableName, crtDataTable, dictPocoColInfos, excelTemplateColumns);
                dataTypeValidationResults = dataTypeDataTableValidation.ValidateDataTable();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
            return dataTypeValidationResults;
        }
     




        

       
    }


}
