using System;
using System.Collections.Generic;
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
using SolvencyII.ExcelImportExportLib.Utils;
using NetOffice.ExcelApi.Enums;
using System.Drawing;
using SolvencyII.ExcelImportExportLib.Dto;

namespace SolvencyII.ExcelImportExportLib.Load
{
    public class LoadExcel : LoadBase
    {
        public override int LoadData(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto)
        {
            BasicTemplateDto bDto = dto as BasicTemplateDto;

            if (bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Basic template transfer object");

            int hWidth = bDto.HeaderData.GetLength(1);
            int hHeight = bDto.HeaderData.GetLength(0);
            int tWidth = bDto.TableData.GetLength(1);
            int tHeight = bDto.TableData.GetLength(0);
            int startRow, startCol, endRow, endCol;

            startRow = bDto.HeaderRange.Row + hHeight + bDto.Offset;
            endRow = startRow + tHeight - 1;
            startCol = bDto.HeaderRange.Column;
            endCol = bDto.HeaderRange.Column + bDto.HeaderRange.Columns.Count - 1;


            Range startRange = workSheet.Cells[startRow, startCol];
            Range endRange = workSheet.Cells[endRow, endCol];
            Range range = workSheet.Range(startRange, endRange);
           
             
            //Write the value which convert all number to be ientified bz excel excpet percentages
            range.Value = bDto.TableData;
            //range.Value = range.Value;

            try 
            {
                range.Application.DisplayAlerts =false;

                //convert percentages types the columns
                int typeRow = 2;
                for (int i = 0; i < hWidth; i++)
                {

                    string type = bDto.HeaderData[typeRow, i];
                    if (type.ToUpper().Trim() == "PERCENTAGE" || type.ToUpper().Trim() == "DECIMAL" || type.ToUpper().Trim() == "MONETARY" || type.ToUpper().Trim() == "INTEGER")
                    {
                        Range rangePercentages = workSheet.Range(workSheet.Cells[startRow, i + startCol], workSheet.Cells[endRow, i + startCol]);
                        if (rangePercentages != null)
                            if (rangePercentages.Value != null)
                                try { 
                                        rangePercentages.TextToColumns();
                                    }
                                    catch(Exception ex)
                                    {
                                        //requires to catch if the all the values in the range value is NULL, 
                                        //Reason is the Netoffice fails to format the values in the excel to TextToColumns if all the values to the range is null
                                    }
                                
                        
                    }

                }
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

            

            //calculate and set the sequence number for all the data rows

            if (startCol > 1)   //Check if we have room to insert sequence numbers
            {

                string[,] rowSequence = new string[tHeight, 1];
                int sequence = bDto.Offset + 1;

                for (int i = 0; i < tHeight; i++)
                    rowSequence[i, 0] = (sequence++).ToString();

                startRange = workSheet.Cells[startRow, startCol - 1];
                endRange = workSheet.Cells[endRow, startCol - 1];
                range = workSheet.Range(startRange, endRange);

                range.Value = rowSequence;
                range.Value = range.Value;


                range.DisposeChildInstances();
                range.Dispose();
                startRange.DisposeChildInstances();
                startRange.Dispose();
                endRange.DisposeChildInstances();
                endRange.Dispose();
                range = null;
                startRange = null;
                endRange = null;
            }

            return tHeight;
        }

       
    }
}
