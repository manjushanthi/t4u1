using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain.Extensions;
using SolvencyII.Data.Entities;
using SolvencyII.Data.SQLite;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Exceptions;
using SolvencyII.ExcelImportExportLib.Utils;
using SolvencyII.ExcelImportExportLib.Dto;

namespace SolvencyII.ExcelImportExportLib.Load
{
    public class LoadBusinessExcelFromDpm : LoadBase
    {
        public override int LoadData(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto)
        {
            BusinessTemplateDto bDto = dto as BusinessTemplateDto;

            if (bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Business template transfer object");

            int hWidth = bDto.HeaderData.GetLength(1);
            int hHeight = bDto.HeaderData.GetLength(0);
            int tWidth = bDto.TableData.GetLength(1);
            int tHeight = bDto.TableData.GetLength(0);
            int startRow = 0, startCol = 0, endRow = 0, endCol = 0;

            //Load TableData

            if (bDto.TypeOfTable == Domain.TableType.OPEN_TABLE)
            {

                startRow = bDto.TableDataRange.Row + hHeight + bDto.Offset;
                endRow = startRow + tHeight - 1;
                startCol = bDto.TableDataRange.Column;
                endCol = bDto.TableDataRange.Column + bDto.TableDataRange.Columns.Count - 1;
            }
            else if(bDto.TypeOfTable == Domain.TableType.CLOSED_TABLE || bDto.TypeOfTable == Domain.TableType.SEMI_OPEN_TABLE)
            {
                startRow = bDto.TableDataRange.Row + 1;
                endRow = startRow + bDto.TableDataRange.Rows.Count - 2;
                startCol = bDto.TableDataRange.Column + 1;
                endCol = startCol +  bDto.TableDataRange.Columns.Count - 2;
            }

            //Check before if all the boundary has the proper value
            if (startRow == 0 || startCol == 0 || endRow == 0 || endCol == 0)
                throw new ArgumentOutOfRangeException("An error occured while calculating the boundary value.");

            //Table Data Range
            Range startRange = workSheet.Cells[startRow, startCol];
            Range endRange = workSheet.Cells[endRow, endCol];
            Range range = workSheet.Range(startRange, endRange);

            try
            {
                range.Application.DisplayAlerts = false;

                //Unprotect the sheet
                workSheet.Unprotect();

                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CurrentCulture;
                NumberFormatInfo numFormat = CultureInfo.CurrentCulture.NumberFormat;
                string numberFormat = "#" + numFormat.NumberGroupSeparator + "##0" + numFormat.NumberDecimalSeparator;
                for (int i = 0; i < numFormat.NumberDecimalDigits; i++)
                {
                    numberFormat = numberFormat + "0";
                }

                    //Write the value which convert all number to be ientified bz excel excpet percentages
                range.Value = bDto.TableData;
                //range.NumberFormat = numberFormat;

                int col = range.Columns.Count;
                int row = range.Rows.Count;
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (bDto.TableData[i, j] == null)
                            continue;

                        Range cell = range.Cells[i + 1, j + 1];
                        //DisplayFormat df = cell.DisplayFormat;
                        double devnull;
                        DateTime dt;
                        IFormatProvider provider = CultureInfo.CurrentCulture;
                        if(double.TryParse((string)cell.Value, NumberStyles.Float | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, provider, out devnull))
                        {
                            cell.Value2 = devnull;
                        }
                        else if(DateTime.TryParse((string)cell.Value, provider, DateTimeStyles.None, out dt))
                        {
                            cell.Value2 = dt;
                        }
                        
                    }
                }

                    //range.Value = range.Value;
                //range.AutoFormat();

                //Filter data Range for Z axis
                if (bDto.FilterData != null)
                {
                    startRow = bDto.FilterRange.Row;
                    endRow = startRow + bDto.FilterRange.Rows.Count - 1;
                    startCol = bDto.FilterRange.Column;
                    endCol = startCol + bDto.FilterRange.Columns.Count - 1;

                    startRange = workSheet.Cells[startRow, startCol];
                    endRange = workSheet.Cells[endRow, endCol];
                    range = workSheet.Range(startRange, endRange);

                    range.Value = bDto.FilterData;
                    range.Value = range.Value;
                }

                //Filter data Range for X axis
                if (bDto.XFilterData != null)
                {
                    startRow = bDto.XFilterRange.Row;
                    endRow = startRow + bDto.XFilterRange.Rows.Count - 1;
                    startCol = bDto.XFilterRange.Column;
                    endCol = startCol + bDto.XFilterRange.Columns.Count - 1;


                    startRange = workSheet.Cells[startRow, startCol];
                    endRange = workSheet.Cells[endRow, endCol];
                    range = workSheet.Range(startRange, endRange);

                    range.Value = bDto.XFilterData;
                    range.Value = range.Value;
                }

                //Filter data Range for Y axis
                if (bDto.YFilterData != null)
                {
                    startRow = bDto.YFilterRange.Row;
                    endRow = startRow + bDto.YFilterRange.Rows.Count - 1;
                    startCol = bDto.YFilterRange.Column;
                    endCol = startCol + bDto.YFilterRange.Columns.Count - 1;


                    startRange = workSheet.Cells[startRow, startCol];
                    endRange = workSheet.Cells[endRow, endCol];
                    range = workSheet.Range(startRange, endRange);

                    range.Value = bDto.YFilterData;
                    range.Value = range.Value;
                }

                //Once the data has been written protect again
                workSheet.Protect();

            }
            finally
            {

                range.Application.DisplayAlerts = true;
            }

            range.DisposeChildInstances();
            range.Dispose();
            startRange.DisposeChildInstances();
            startRange.Dispose();
            endRange.DisposeChildInstances();
            endRange.Dispose();
            range = null;
            startRange = null;
            endRange = null;

            return tHeight;
        }
    }
}
