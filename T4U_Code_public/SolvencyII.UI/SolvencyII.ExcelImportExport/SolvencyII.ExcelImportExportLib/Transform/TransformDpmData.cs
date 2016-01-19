using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Dto;
using System.Globalization;

namespace SolvencyII.ExcelImportExportLib.Transform
{
    public class TransformDpmData : TransformBase
    {
       
        public override void Transform(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto)
        {
            BasicTemplateDto bDto = dto as BasicTemplateDto;

            if (bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Basic template transfer object");

            //nothing to do
            return;

            //int hWidth = headerData.GetLength(1);
            //int hHeight = headerData.GetLength(0);
            //int tWidth = tableData.GetLength(1);
            //int tHeight = tableData.GetLength(0);
            //int typeRow = 2;
            //HierarchyInfo hierarchyInfo = new HierarchyInfo();

            //Dictionary<int, List<HierarchyAndMemberInfo>> hierarchies = new Dictionary<int, List<HierarchyAndMemberInfo>>();

            ////Parsing the headers to load the hirearchies
            //for (int i = 0; i < hWidth; i++)
            //{
            //    //If the type of a column is an enumerator
            //    string type = headerData[typeRow, i];

            //    if (type.Trim().StartsWith("E:"))
            //    {
            //        //Extract the hierarchy id from the type information

            //        int startIndex = type.IndexOf(':') + 1;
            //        int endIndex = type.IndexOf(';');
            //        int hierarchyID = endIndex == -1 ? Int32.Parse(type.Substring(startIndex)) : Int32.Parse(type.Substring(startIndex, endIndex - startIndex));

            //        if (hierarchyID > 0)
            //        {
            //            //Get hierarchy information from DPM database
            //            List<HierarchyAndMemberInfo> info = (List<HierarchyAndMemberInfo>)hierarchyInfo.GetHierarchyAndMemberInfo(sqliteConnection, hierarchyID);

            //            if (info != null)
            //            {
            //                hierarchies.Add(i, info);
            //            }

            //        }
            //    }               

            //}

            ////testing
            ////throw new SolvencyII.ExcelImportExportLib.Exceptions.T4UExcelImportExportException("This is a test exception from tranform", null);

            
            //for (int row = 0; row < tHeight; row++)
            //{
            //    for (int col = 0; col < tWidth; col++)
            //    {

            //        if (string.IsNullOrEmpty(tableData[row, col]))
            //            continue;

            //        string type = headerData[typeRow, col];

            //        if (type.ToUpper().Trim() == "DATE")
            //        {
                      
            //            tableData[row, col] = DateTime.Parse(tableData[row, col]).ToShortDateString();
                     
            //        }

            //        if (type.ToUpper().Trim() == "BOOLEAN")
            //        {
            //            if (tableData[row, col] != null)
            //            {
            //                if (tableData[row, col].ToString().Trim() == "1")
            //                {
            //                    tableData[row, col] = "TRUE";
            //                }
            //                else if (tableData[row, col].ToString().Trim() == "0")
            //                {
            //                    tableData[row, col] = "FALSE";
            //                }
            //                else {

            //                    //Todo:vijay
            //                    //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate. Boolean Data has exported as "false" value
            //                    //catch errors.
            //                }

            //            }

            //        }

            //        ////Missing handling of decimals
            //        //if (type.ToUpper().Trim() == "PERCENTAGE")
            //        //{
            //        //    tableData[row, col] =decimal.Parse( tableData[row, col]).ToString("P");

            //        //}
                    
            //        //if (type.ToUpper().Trim() == "PERCENTAGE" || type.ToUpper().Trim() == "DECIMAL" )
            //        //{
            //        //    if (!string.IsNullOrEmpty(tableData[row, col]))
            //        //    {
            //        //        decimal convertedValue;

            //        //        if (decimal.TryParse(tableData[row, col],NumberStyles.Any, CultureInfo.InvariantCulture,out convertedValue))
            //        //        {
            //        //            tableData[row, col] = convertedValue.ToString();
            //        //        }
            //        //        else 
            //        //        {
            //        //            //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate. Boolean Data has exported as "false" value
            //        //            //catch errors.
            //        //        }

                           
            //        //    }
                       

            //        //}


            //        if (type.Trim().StartsWith("E:"))
            //        {
            //            bool success = false;
            //            if (hierarchies.ContainsKey(col))
            //            {
            //                //Get the hierarchy for the corresponding column
            //                List<HierarchyAndMemberInfo> info = hierarchies[col];
            //                if (info != null)
            //                {
            //                    //if (tableData[row, col] == null)
            //                    //    continue;


            //                    //Get the HierarchyAndMemberInfo for the corresponding text
            //                    HierarchyAndMemberInfo matched = (from hm in info
            //                                                      where hm.MemberXBRLCode.ToUpper() == tableData[row, col].ToUpper()
            //                                                      select hm).FirstOrDefault();

            //                    //If matched replace the hierarchy with the corresponding XBRL code
            //                    if (matched != null)
            //                    {
            //                        tableData[row, col] = matched.MemberLabel + "   {" + matched.MemberXBRLCode + "}";
            //                        success = true;
            //                    }
            //                }
            //            }

            //            if(!success)
            //            { 
            //                //Todo:vijay
            //                //What happend in the data of the hirerchies is wrong.
            //                //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate.
            //                //Export the value as in the database
            //            }
            //        }

            //    }
            //}
        }
    }
}
