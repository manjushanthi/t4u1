using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

using SolvencyII.ExcelImportExportLib.Exceptions;

namespace SolvencyII.ExcelImportExportLib.Extract
{
    public class ExtractData
    {
        public string[, ] ExtractDataFromRange(Worksheet workSheet, Range range)
        {
            string[, ] items = null;

            if (range == null)
                return null;

            int col = range.Columns.Count;
            int row = range.Rows.Count;

            if (range.Count > 1)
            {

                object multiArry = range.Value;

                if (multiArry != null)
                {
                    items = new string[row, col];

                    for (int i = 0; i < row; i++)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            object obj = ((object[,])multiArry)[i + 1, j + 1];

                            items[i, j] = obj != null ? obj.ToString() : null;
                        }
                    }
                }
            }
            else
            {
                items = new string[1, 1];
                items[0, 0] = range.Value.ToString();
            }

            return items;
        }

        public Range FindRange(Names rangeNames, string rangename)
        {
            Range range = null;

            foreach (Name n in rangeNames)
            {
                int index = n.NameLocal.IndexOf('!');
                string ln = index > 0 ? n.NameLocal.Substring(index + 1) : n.NameLocal;

                if (rangename.ToUpper().Trim() == ln.ToUpper().Trim())
                {
                    range = n.RefersToRange;
                    break;
                }
            }

            return range;
        }

        public int FindDataRow(Worksheet ws, Range headerRange)
        {
            int startRow, startCol, endRow, endCol;
            int row = 0, col = 0;
            bool stop = false;

            startCol = headerRange.Column;
            endCol = headerRange.Column + headerRange.Columns.Count - 1;
            startRow = headerRange.Row + headerRange.Rows.Count;

            //Find the first empty cell along the first column
            row = startRow;
            col = startCol;

            object val = ws.UsedRange.Columns[col, Type.Missing].Value;

            do
            {
                //while (ws.Cells[row, col].Text != "")
                try
                {
                    while (((object[,])val)[row, 1] != null /*|| ((object[,])val)[row, 1].ToString() != ""*/)
                        row++;

                    //Verify if the entire row is not empty
                    for (int i = startCol; i <= endCol; i++)
                    {
                        object entireRow = ws.UsedRange.Rows[row, Type.Missing].Value;
                        if (((object[,])entireRow)[1, i] != null)
                        //if (ws.Cells[row, i].Text != "")
                        {
                            row++;
                            stop = false;
                            break;
                        }
                        else
                        {
                            stop = true;
                        }
                    }

                }
                catch (IndexOutOfRangeException) { stop = true; }

            } while (!stop);

            endRow = row - 1;


            //Check for next 5 row, if any empty row is found after endRow and throw an error
            int maxTryRow = 5;

            try
            {

                for (int i = row; i < row + maxTryRow; i++)
                {
                    for (int j = startCol; j <= endCol; j++)
                    {
                        object entireRow = ws.UsedRange.Rows[row, Type.Missing].Value;
                        if (((object[,])entireRow)[1, j] != null)
                        //if (ws.Cells[i, j].Text != "")
                        {

                            //Find the empty row ranges
                            Range emptyRows = ws.Range(ws.Cells[row, startCol], ws.Cells[i - 1, endCol]);

                            string errorText = "Empty rows are identified at the address " + emptyRows.Address + ". " +
                                "Remove all the empty rows to continue the process.";

                            throw new T4UExcelImportExportException(errorText, null, emptyRows.Address);
                        }
                    }
                }
            }
            catch (IndexOutOfRangeException) { }

            return endRow;
        }

        public int FindDataRow2(Worksheet ws, Range headerRange)
        {
            int startRow, startCol, endCol;
            int row = 0, col = 0;
            

            startCol = headerRange.Column;
            endCol = headerRange.Column + headerRange.Columns.Count - 1;
            startRow = headerRange.Row + headerRange.Rows.Count;

            //Find the first empty cell along the first column
            row = startRow;
            col = startCol;

            return 0;
        }


        public Range FindDataRange(Worksheet ws, Range headerRange)
        {
            int startRow, startCol, endRow, endCol;

            startCol = headerRange.Column;
            endCol = headerRange.Column + headerRange.Columns.Count - 1;
            startRow = headerRange.Row + headerRange.Rows.Count;
            endRow = FindDataRow(ws, headerRange);

            return ws.Range(ws.Cells[startRow, startCol], ws.Cells[endRow, endCol]);
        }
    }
}
