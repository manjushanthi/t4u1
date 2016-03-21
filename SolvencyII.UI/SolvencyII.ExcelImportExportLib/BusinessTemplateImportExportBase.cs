using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using NetOffice.ExcelApi;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;
using SolvencyII.Data.SQLite;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Events;
using SolvencyII.ExcelImportExportLib.Extract;
using SolvencyII.ExcelImportExportLib.Exceptions;
using SolvencyII.ExcelImportExportLib.Transform;
using SolvencyII.ExcelImportExportLib.Load;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Dto;

namespace SolvencyII.ExcelImportExportLib
{
    public abstract class BusinessTemplateImportExportBase : ImportExportBase
    {

         protected override bool Invoke(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, TransformBase transformData, LoadBase loadData, string[] tableFilter = null, string version = null)
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
             Range tableDataRange = null;
             BusinessTemplateDto dto = null;

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

                     int rows = 0;

                     //Look for '.TD' range
                     names = ws.Names;
                     tableDataRange = extract.FindRange(names, ws.Name.Trim() + ".TD");

                     //Extract header data from the range
                     string[,] headerData = extract.ExtractDataFromRange(ws, tableDataRange);

                     int totalRow = GetTotalTableRows(sqliteConnection, ws, tableDataRange, instance, s);


                     //calculate page size
                     pages = (int)Math.Ceiling(((decimal)totalRow / (decimal)pageSize));

                     dto = new BusinessTemplateDto
                     {
                         TableCode = s,
                         Instance = instance,
                         TotalRows = totalRow,
                         Limit = pageSize,
                         Version = version,
                         TableDataRange = tableDataRange,
                         Offset = 0
                     };

                     //Look for filter ranges
                     dto.FilterRange = extract.FindRange(names, ws.Name.Trim() + ".FL");
                     dto.FilterData = extract.ExtractDataFromRange(ws, dto.FilterRange);

                     //adjust total record
                     if (pages > 0)
                         totalRecord += (pages - 1);


                     //Check to see if a table is closed or open or semi open table
                     TableTypeInfo typeInfo = new TableTypeInfo();
                     if (typeInfo.IsClosedTable(sqliteConnection, s))
                     {
                         //Set the type of table
                         dto.TypeOfTable = TableType.CLOSED_TABLE;

                         if (pages > 1)
                             throw new ArgumentOutOfRangeException("pages", pages, "Pages for closed template cannot be greater than 1");

                         string[,] tableData = GetTableData(sqliteConnection, ws, dto);

                         //for each set of row, create an another sheet and render(multi dimensional)

                         //Transform the data read from excel sheet
                         dto.HeaderData = headerData;
                         //dto.TableData = tableData;

                         int dimCount = 1;
                         IList<object> tempData = dto.CRTData;
                         foreach (object o in tempData)
                         {
                             dto.CurrentObject = o;

                             transformData.Transform(sqliteConnection, ws, dto);

                             rows += loadData.LoadData(sqliteConnection, ws, dto);

                             if (dto.CRTData.Count > 1 && dimCount < dto.CRTData.Count)
                             {
                                 ws.Copy(Type.Missing, ws);

                                 string newSheetName = s.Trim() + " (" + (++dimCount).ToString() + ") ";
                                 Worksheet copySheet = (Worksheet)excelConn.TemplateWorkbook.Sheets[ws.Index + 1];

                                 //Move the sheet next to his clone
                                 //copySheet.Move(Type.Missing, ws);

                                 ws = copySheet;

                                 //Look for '.TH' range
                                 Range newTableRange = ws.Range(ws.Cells[dto.TableDataRange.Row, dto.TableDataRange.Column], ws.Cells[dto.TableDataRange.Row + dto.TableDataRange.Rows.Count - 1, dto.TableDataRange.Column + dto.TableDataRange.Columns.Count - 1]);
                                 if (newTableRange != null)
                                 {
                                     dto.TableDataRange = newTableRange;

                                     //Extract header data from the range
                                     dto.HeaderData = extract.ExtractDataFromRange(ws, dto.TableDataRange);
                                 }

                                 if (dto.FilterRange != null)
                                 {
                                     Range newFilterRange = ws.Range(ws.Cells[dto.FilterRange.Row, dto.FilterRange.Column], ws.Cells[dto.FilterRange.Row + dto.FilterRange.Rows.Count - 1, dto.FilterRange.Column + dto.FilterRange.Columns.Count - 1]);

                                     if (newFilterRange != null)
                                     {
                                         dto.FilterRange = newFilterRange;
                                         dto.FilterData = extract.ExtractDataFromRange(ws, dto.FilterRange);
                                     }
                                 }
                             }
                         }
                     }
                     else if (typeInfo.IsOpenTable(sqliteConnection, s))
                     {
                         //Set the type of table
                         dto.TypeOfTable = TableType.OPEN_TABLE;

                         for (int p = 0; p < pages; p++)
                         {
                             dto.Offset = p * pageSize;

                             string[,] tableData = GetTableData(sqliteConnection, ws, dto);

                             //Transform the data read from excel sheet
                             dto.HeaderData = headerData;
                             dto.TableData = tableData;

                             //Wrong data
                             dto.CurrentObject = dto.CRTData.FirstOrDefault();

                             transformData.Transform(sqliteConnection, ws, dto);

                             //Construct query from worksheet name(table) and the data that read from 'TABLE_HEADER' column codes
                             //Loop through each data row and update to the table
                             rows += loadData.LoadData(sqliteConnection, ws, dto);

                         }
                     }
                     else if (typeInfo.IsSemiOpenTable(sqliteConnection, s))
                     {
                         dto.XFilterRange = extract.FindRange(names, ws.Name.Trim() + ".XL");
                         dto.XFilterData = extract.ExtractDataFromRange(ws, dto.XFilterRange);

                         dto.YFilterRange = extract.FindRange(names, ws.Name.Trim() + ".YL");
                         dto.YFilterData = extract.ExtractDataFromRange(ws, dto.YFilterRange);

                         //Set the type of table
                         dto.TypeOfTable = TableType.SEMI_OPEN_TABLE;

                         if (pages > 1)
                             throw new ArgumentOutOfRangeException("pages", pages, "Pages for closed template cannot be greater than 1");


                         string[,] tableData = GetTableData(sqliteConnection, ws, dto);

                         //for each set of row, create an another sheet and render(multi dimensional)

                         //Transform the data read from excel sheet
                         dto.HeaderData = headerData;

                         int dimCount = 1;
                         IList<object> tempData = dto.CRTData;
                         foreach (object o in tempData)
                         {
                             dto.CurrentObject = o;

                             transformData.Transform(sqliteConnection, ws, dto);

                             rows += loadData.LoadData(sqliteConnection, ws, dto);

                             if (dto.CRTData.Count > 1 && dimCount < dto.CRTData.Count)
                             {
                                 ws.Copy(Type.Missing, ws);

                                 string newSheetName = s.Trim() + " (" + (++dimCount).ToString() + ") ";
                                 Worksheet copySheet = (Worksheet)excelConn.TemplateWorkbook.Sheets[ws.Index + 1];

                                 ws = copySheet;

                                 //Look for '.TD' range
                                 Range newTableRange = ws.Range(ws.Cells[dto.TableDataRange.Row, dto.TableDataRange.Column], ws.Cells[dto.TableDataRange.Row + dto.TableDataRange.Rows.Count - 1, dto.TableDataRange.Column + dto.TableDataRange.Columns.Count - 1]);
                                 if (newTableRange != null)
                                 {
                                     dto.TableDataRange = newTableRange;

                                     //Extract header data from the range
                                     dto.HeaderData = extract.ExtractDataFromRange(ws, dto.TableDataRange);
                                 }

                                 //Look for Z-Axis filter data
                                 if (dto.FilterRange != null)
                                 {
                                     Range newFilterRange = ws.Range(ws.Cells[dto.FilterRange.Row, dto.FilterRange.Column], ws.Cells[dto.FilterRange.Row + dto.FilterRange.Rows.Count - 1, dto.FilterRange.Column + dto.FilterRange.Columns.Count - 1]);

                                     if (newFilterRange != null)
                                     {
                                         dto.FilterRange = newFilterRange;
                                         dto.FilterData = extract.ExtractDataFromRange(ws, dto.FilterRange);
                                     }
                                 }

                                 //Look for X-Axis filter data
                                 if (dto.XFilterRange != null)
                                 {
                                     Range newFilterRange = ws.Range(ws.Cells[dto.XFilterRange.Row, dto.XFilterRange.Column], ws.Cells[dto.XFilterRange.Row + dto.XFilterRange.Rows.Count - 1, dto.XFilterRange.Column + dto.XFilterRange.Columns.Count - 1]);

                                     if (newFilterRange != null)
                                     {
                                         dto.XFilterRange = newFilterRange;
                                         dto.XFilterData = extract.ExtractDataFromRange(ws, dto.XFilterRange);
                                     }
                                 }

                                 //Look for Y-Axis filter data
                                 if (dto.YFilterRange != null)
                                 {
                                     Range newFilterRange = ws.Range(ws.Cells[dto.YFilterRange.Row, dto.YFilterRange.Column], ws.Cells[dto.YFilterRange.Row + dto.YFilterRange.Rows.Count - 1, dto.YFilterRange.Column + dto.YFilterRange.Columns.Count - 1]);

                                     if (newFilterRange != null)
                                     {
                                         dto.YFilterRange = newFilterRange;
                                         dto.YFilterData = extract.ExtractDataFromRange(ws, dto.YFilterRange);
                                     }
                                 }
                             }
                         }
                     }

                     else
                         throw new T4UExcelImportExportException(
                             "An error occured while identifying the type of table. Table code: " + s, null);

                     //Count one record is progressing and invoke the progress event
                     progressRecord++;
                     OnGranuleProgressChanged((int)((progressRecord / totalRecord) * 100), s);

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

                     dto.Dispose();
                     names.Dispose();
                     tableDataRange.Dispose();
                     tableDataRange.DisposeChildInstances();
                     ws.DisposeChildInstances();
                     ws.Dispose();

                     names = null;
                     tableDataRange = null;
                     ws = null;
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
                 if (tableDataRange != null)
                 {
                     tableDataRange.DisposeChildInstances();
                     tableDataRange.Dispose();
                 }
                 if(dto != null)
                 {
                     dto.Dispose();
                 }

                 if (ws != null)
                 {
                     ws.DisposeChildInstances();
                     ws.Dispose();
                 }

                 names = null;
                 tableDataRange = null;
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
                 if (tableDataRange != null)
                 {
                     tableDataRange.DisposeChildInstances();
                     tableDataRange.Dispose();
                 }

                 if (ws != null)
                 {
                     ws.DisposeChildInstances();
                     ws.Dispose();
                 }

                 names = null;
                 tableDataRange = null;
                 ws = null;

                 GC.Collect();
                 GC.WaitForPendingFinalizers();
                 GC.Collect();
                 GC.WaitForPendingFinalizers();


                 if (!_IsStoprequested)
                     throw new T4UExcelImportExportException(GetErrorMsg(behaviour, processingTable), ex);
             }

             //Send the completed event
             OnCompleted(null, false, "Completed");

             return true;
         }

         protected string GetErrorMsg(ImportExportBehaviour behaviour, string tableCode)
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
