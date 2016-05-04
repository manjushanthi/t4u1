using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Threading;
using NetOffice.ExcelApi;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;
using SolvencyII.Data.SQLite;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Dto;
using SolvencyII.ExcelImportExportLib.Events;
using SolvencyII.ExcelImportExportLib.Extract;
using SolvencyII.ExcelImportExportLib.Exceptions;
using SolvencyII.ExcelImportExportLib.Transform;
using SolvencyII.ExcelImportExportLib.Load;

namespace SolvencyII.ExcelImportExportLib
{
    public class ExcelBasicTemplateImportImpl : BasicTemplateImportExportBase,  IExcelImport
    {

        public string[] GetTableCodes(IExcelConnection excelConnection)
        {
            return GetTableCodes(null, excelConnection);
        }

        protected override TransformBase GetTransformer()
        {
            return new TransformExcelData();
        }

        protected override LoadBase GetLoader()
        {
            return new LoadDpm();
        }

        protected override string[] GetTableCodes(ISolvencyData sqliteConnection, IExcelConnection excelConnection)
        {
            ExcelConnection reader = (ExcelConnection)excelConnection;

            List<string> alWorksheets = new List<string>();
            string sPattern = @"([A-Z|a-z]){1,2}(\.\d{2}){4}";

            TableCodeExtractor extractor = new TableCodeExtractor();

            return extractor.GetTableCodesFromExcel(excelConnection, sPattern);
        }

        protected override string[,] GetTableData(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto)
        {
            BasicTemplateDto bDto = dto as BasicTemplateDto;

            if (bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Basic template transfer object");

            int startRow, startCol, endRow, endCol;
            ExtractData extract = new ExtractData();


            //calculate the page to take the data
            startRow = bDto.HeaderRange.Row + bDto.HeaderRange.Rows.Count() + bDto.Offset;
            endRow = startRow + bDto.Limit - 1;
            startCol = bDto.HeaderRange.Column;
            endCol = bDto.HeaderRange.Column + bDto.HeaderRange.Columns.Count - 1;

            if (endRow > (bDto.TotalRows + bDto.HeaderRange.Row + bDto.HeaderRange.Rows.Count() - 1))
                endRow = bDto.TotalRows + bDto.HeaderRange.Row + bDto.HeaderRange.Rows.Count() - 1;

            Range dataRange = workSheet.Range(workSheet.Cells[startRow, startCol], workSheet.Cells[endRow, endCol]); //extract.FindDataRange(workSheet, headerRange);

            if (!string.IsNullOrEmpty(bDto.Version))
            {
                if (bDto.Version.Trim() == "2015.03.10")
                {
                    try
                    {
                        dataRange.Application.DisplayAlerts = false;
                        string[,] headerData = extract.ExtractDataFromRange(workSheet, bDto.HeaderRange);
                        int hWidth = headerData.GetLength(1);
                        int hHeight = headerData.GetLength(0);
                        int typeRow = 2;
                        for (int i = 0; i < hWidth; i++)
                        {
                            //If the type of a column is an enumerator
                            string type = headerData[typeRow, i];
                            if (type.ToUpper().Trim() == "PERCENTAGE" /*BRAG*/ || type.ToUpper().Trim() == "PERCENT")
                            {
                                if (dataRange.Range(workSheet.Cells[startRow - 4, i + 1], workSheet.Cells[endRow - 4, i + 1]) != null)
                                {
                                    if (dataRange.Range(workSheet.Cells[startRow - 4, i + 1], workSheet.Cells[endRow - 4, i + 1]).Value != null)
                                        try
                                        {
                                            dataRange.Range(workSheet.Cells[startRow - 4, i + 1], workSheet.Cells[endRow - 4, i + 1]).TextToColumns();
                                        }
                                        catch (Exception ex)
                                        {
                                            //requires to catch if the all the values in the range value is NULL, 
                                            //Reason is the Netoffice fails to format the values in the excel to TextToColumns if all the values to the range is null
                                        }
                                }

                            }

                        }
                    }
                    finally
                    {

                       // dataRange.Application.DisplayAlerts = true;
                    }

                }
            }


            //Extract data from the range
            //Read table data.
            string[,] tableData = extract.ExtractDataFromRange(workSheet, dataRange);

            //dataRange.DisposeChildInstances();
            dataRange.Dispose();           
            dataRange = null;

            return tableData;
        }

        protected override int GetTotalTableRows(ISolvencyData sqliteConnection, Worksheet workSheet, Range headerRange, dInstance instance, string tableCode)
        {
            int rows = 0;

            ExtractData extract = new ExtractData();

            int excelRow = extract.FindDataRow(workSheet, headerRange);

            rows = excelRow - (headerRange.Row + headerRange.Rows.Count);

            return rows + 1;
        }

        protected override void BeginTransaction(ISolvencyData sqliteConnection, Workbook workbook)
        {
            if (sqliteConnection != null)
                sqliteConnection.BeginTransaction();
        }

        protected override void Commit(ISolvencyData sqliteConnection, Workbook workbook)
        {
            if (sqliteConnection != null)
                sqliteConnection.Commit();
        }

        protected override void Rollback(ISolvencyData sqliteConnection, Workbook workbook)
        {
            if (sqliteConnection != null)
                sqliteConnection.Rollback();
        }
    }
}
