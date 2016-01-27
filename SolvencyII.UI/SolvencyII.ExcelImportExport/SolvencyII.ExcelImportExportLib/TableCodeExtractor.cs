using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using NetOffice.ExcelApi;

namespace SolvencyII.ExcelImportExportLib
{
    public class TableCodeExtractor
    {
        public string[] GetTableCodesFromDb(ISolvencyData sqliteConnection)
        {
            TableInfo info = new TableInfo();
            //get the whole list of table codes from database
            IList<mTable> tableList = (IList<mTable>)info.GetAllTables(sqliteConnection);
            return (from t in tableList
                    select t.TableCode).ToArray<string>();
        }

        public string[] GetTableCodesFromExcel(IExcelConnection excelConnection, string sPattern)
        {
            ExcelConnection reader = (ExcelConnection)excelConnection;

            List<string> alWorksheets = new List<string>();
            //string sPattern = @"(^[A-Z|a-z]){1}(\.\d{2}){4}";

            foreach (Worksheet ws in reader.TemplateWorkbook.Worksheets)
            {
                if (Regex.IsMatch(ws.Name, sPattern, RegexOptions.IgnoreCase))
                    alWorksheets.Add(ws.Name);

                ws.Dispose();
            }

            return alWorksheets.ToArray<string>();
        }
    }
}
