using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NetOffice.ExcelApi;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;
using SolvencyII.Data.SQLite;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Dto;
using SolvencyII.ExcelImportExportLib.Events;
using SolvencyII.ExcelImportExportLib.Extract;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Transform;
using SolvencyII.ExcelImportExportLib.Load;
using SolvencyII.ExcelImportExportLib.Utils;
using SolvencyII.Domain.Configuration;


namespace SolvencyII.ExcelImportExportLib
{
    public class ExcelBusinessTemplateExportImpl : BusinessTemplateImportExportBase
    {
        private List<ExcelExportValidationMessage> ExcelExportValidationMessage = new List<ExcelExportValidationMessage>();

        public List<ExcelExportValidationMessage> ExcelExportValidationMessageLst
        {
            set { ExcelExportValidationMessage = value; }
            get { return ExcelExportValidationMessage; }
        }

        public ExcelBusinessTemplateExportImpl(List<ExcelExportValidationMessage> excelExportValidationMessageLst)
        {
            ExcelExportValidationMessageLst = excelExportValidationMessageLst;
        }

        protected override TransformBase GetTransformer()
        {
            return new TransformDpmBusinessData();
        }

        protected override LoadBase GetLoader()
        {
            return new LoadBusinessExcelFromDpm();
        }

        protected override string[] GetTableCodes(ISolvencyData sqliteConnection, IExcelConnection excelConnection)
        {
            TableCodeExtractor extractor = new TableCodeExtractor();

            return extractor.GetTableCodesFromDb(sqliteConnection);
        }


        protected override int GetTotalTableRows(ISolvencyData sqliteConnection, NetOffice.ExcelApi.Worksheet workSheet, NetOffice.ExcelApi.Range headerRange, dInstance instance, string tableCode)
        {
            int rows = 0;
            TableInfo info = new TableInfo();
            mTable table = (new TableInfo().GetTable(sqliteConnection, tableCode)).FirstOrDefault();
            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(sqliteConnection, 1)).FirstOrDefault();
            string tableName = Helper.GetTableName(taxonomy, table);

            Type t = Helper.ReferencedLookup(tableName);

            string query = string.Format("select count(*) from {0} where instance = {1} ", tableName, instance.InstanceID);

            rows = sqliteConnection.ExecuteScalar<int>(query);


            return rows;
        }

        protected override string[,] GetTableData(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto )
        {
            BusinessTemplateDto bDto = dto as BusinessTemplateDto;

            if (bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Business template transfer object");

            ExtractData extract = new ExtractData();
            mTable table = (new TableInfo().GetTable(sqliteConnection, bDto.TableCode)).FirstOrDefault();
            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(sqliteConnection, 1)).FirstOrDefault();
            string tableName = Helper.GetTableName(taxonomy, table);

            Type t = Helper.ReferencedLookup(tableName);

            string query = string.Format("select * from {0} where instance = {1} limit {2} offset {3}", tableName, bDto.Instance.InstanceID, bDto.Limit, bDto.Offset);

            string mappingQuery = string.Format("select * from mapping where table_version_id = {0} ", table.TableID);

            IList<object> tempData = sqliteConnection.Query(t, query);

            bDto.CRTData = tempData;

            return null;
        }

        protected override void BeginTransaction(ISolvencyData sqliteConnection, NetOffice.ExcelApi.Workbook workbook)
        {
            //do nothing
        }

        protected override void Commit(ISolvencyData sqliteConnection, NetOffice.ExcelApi.Workbook workbook)
        {
            workbook.Save();
        }

        protected override void Rollback(ISolvencyData sqliteConnection, NetOffice.ExcelApi.Workbook workbook)
        {
            workbook.Save();
        }
    }
}
