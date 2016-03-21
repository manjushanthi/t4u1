using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.ComponentModel;
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
    public abstract class BasicTemplateImportExportBase : ImportExportBase
    {
         protected override bool Invoke(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, TransformBase transformData, LoadBase loadData, string[] tableFilter = null, string version=null)
         {
             ExcelConnection excelConn = (ExcelConnection)excelConnection;
             ExtractData extract = new ExtractData();
             decimal totalRecord = tableFilter != null ? tableFilter.Count() : 0;
             decimal progressRecord = 0;
             int pageSize = 1000;
             int pages = 0;

             string[] filter = tableFilter;

             if (filter == null)
             {
                 filter = GetTableCodes(sqliteConnection, excelConnection);
                 totalRecord = filter.Count();
             }

             string processingTable = string.Empty;

             Worksheet ws = null;
             Names names = null;
             Range headerRange = null;
             BasicTemplateDto dto = null;

             try
             {
                 //Begin the transaction
                 BeginTransaction(sqliteConnection, excelConn.TemplateWorkbook);

                 //Read all the worksheet names
                 foreach (string s in tableFilter)
                 {

                     processingTable = s;

                     ws = (Worksheet)excelConn.WorkbookSheets[s];

                     if (ws == null)
                         continue;

                     if (_IsStoprequested == true)
                     {
                         throw new Exception("Cancel import exception");
                     }                     

                     //throw new Exception("Test exception");

                     //Look for '.TH' range
                     string tableHeader = ws.Name.Trim() + ".TH";

                     names = ws.Names;
                     headerRange = extract.FindRange(names, tableHeader);

                     //Extract header data from the range
                     string[,] headerData = extract.ExtractDataFromRange(ws, headerRange);

                     int totalRow = GetTotalTableRows(sqliteConnection, ws, headerRange, instance, s);

                     //calculate page size
                     pages = (int)Math.Ceiling(((decimal)totalRow / (decimal)pageSize));


                     //adjust total record
                     if (pages > 0)
                         totalRecord += (pages - 1);

                     int offset = 0;
                     int rows = 0;

                     dto = new BasicTemplateDto
                     {
                         HeaderRange = headerRange,
                         Instance = instance,
                         TableCode = s,
                         TotalRows = totalRow,
                         Limit = pageSize,
                         Version = version
                     };

                     for (int p = 0; p < pages; p++)
                     {

                         offset = p * pageSize;
                         dto.Offset = offset;
                         
                         //string[,] tableData = GetTableData(sqliteConnection, ws, headerRange, instance, s, totalRow, pageSize, offset, version);
                         string[,] tableData = GetTableData(sqliteConnection, ws, dto);

                         //Transform the data read from excel sheet
                         dto.TableData = tableData;
                         dto.HeaderData = headerData;
                         transformData.Transform(sqliteConnection, ws, dto);

                         //Construct query from worksheet name(table) and the data that read from 'TABLE_HEADER' column codes
                         //Loop through each data row and update to the table
                         rows += loadData.LoadData(sqliteConnection, ws, dto);

                         //Count one record is progressing and invoke the progress event
                         progressRecord++;
                         OnGranuleProgressChanged((int)((progressRecord / totalRecord) * 100), s);

                     }

                     StringBuilder mb = new StringBuilder();
                     mb.Append("Table ").Append(s).Append(" successfully ");
                     if (behaviour == ImportExportBehaviour.Exporting)
                         mb.Append("exported");
                     else
                         mb.Append("imported");

                     mb.Append(" with ").Append(rows).Append(" number of rows");

                     //int percent = pages > 0 ? (int)((progressRecord / totalRecord) * 100) : (int)((progressRecord++ / totalRecord) * 100);
                     int percent = 0;
                     if (pages <= 0)
                     {
                         progressRecord++;
                     }
                     percent = (int)((progressRecord / totalRecord) * 100);

                     OnProgressChanged(percent, mb.ToString());

                     names.Dispose();
                     headerRange.Dispose();
                     headerRange.DisposeChildInstances();
                     ws.DisposeChildInstances();
                     ws.Dispose();
                     dto.Dispose();

                     names = null;
                     headerRange = null;
                     ws = null;
                     dto = null;


                 }

                 //Commit the transaction
                 Commit(sqliteConnection, excelConn.TemplateWorkbook);

             }
             catch (SQLiteException ex)
             {
                 //sqliteConnection.Rollback();
                 Rollback(sqliteConnection, excelConn.TemplateWorkbook);

                 //Release all the COM variables
                 if (names != null)
                 {
                     names.DisposeChildInstances();
                     names.Dispose();
                 }
                 if (headerRange != null)
                 {
                     headerRange.DisposeChildInstances();
                     headerRange.Dispose();
                 }

                 if (ws != null)
                 {
                     ws.DisposeChildInstances();
                     ws.Dispose();
                 }

                 if(dto != null)
                 {
                     dto.Dispose();
                     dto = null;
                 }

                 names = null;
                 headerRange = null;
                 ws = null;

                 GC.Collect();
                 GC.WaitForPendingFinalizers();
                 GC.Collect();
                 GC.WaitForPendingFinalizers();


                 throw new T4UExcelImportExportException(GetErrorMsg(behaviour, processingTable), ex);
             }
             catch (Exception ex)
             {
                 //sqliteConnection.Rollback();
                 Rollback(sqliteConnection, excelConn.TemplateWorkbook);

                 //Release all the COM variables
                 if (names != null)
                 {
                     names.DisposeChildInstances();
                     names.Dispose();
                 }
                 if (headerRange != null)
                 {
                     headerRange.DisposeChildInstances();
                     headerRange.Dispose();
                 }

                 if (ws != null)
                 {
                     ws.DisposeChildInstances();
                     ws.Dispose();
                 }

                 names = null;
                 headerRange = null;
                 ws = null;

                 GC.Collect();
                 GC.WaitForPendingFinalizers();
                 GC.Collect();
                 GC.WaitForPendingFinalizers();


                 if(!_IsStoprequested)
                 throw new T4UExcelImportExportException(GetErrorMsg(behaviour, processingTable), ex);
             }

             //Send the completed event
             OnCompleted(null, false, "Completed");

             return true;
         }

         private string GetErrorMsg(ImportExportBehaviour behaviour, string tableCode)
         {
             StringBuilder eb = new StringBuilder();
             eb.Append("There is a problem ");

             if (behaviour == ImportExportBehaviour.Exporting)
                 eb.Append("exporting");
             else
                 eb.Append("importing");

             eb.Append(" the table ").Append(tableCode).Append(".");
             if (behaviour == ImportExportBehaviour.Exporting)
                 eb.Append(" The whole exporting has been cancelled and no data has being committed in the database");
             else
                 eb.Append(" The whole Excel file importing has been cancelled and no data has being committed in the database");

             return eb.ToString();
         }

         protected abstract string[] GetTableCodes(ISolvencyData sqliteConnection, IExcelConnection excelConnection);
         protected abstract int GetTotalTableRows(ISolvencyData sqliteConnection, Worksheet workSheet, Range headerRange, dInstance instance, string tableCode);
         protected abstract string[,] GetTableData(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto);


    }
}
