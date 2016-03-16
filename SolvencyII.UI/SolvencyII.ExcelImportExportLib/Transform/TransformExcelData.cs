using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NetOffice.ExcelApi;

using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Dto;

namespace SolvencyII.ExcelImportExportLib.Transform
{
    public class TransformExcelData : TransformBase
    {
        public override void Transform(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto)
        {
            BasicTemplateDto bDto = dto as BasicTemplateDto;

            if (bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Basic template transfer object");

            int hWidth = bDto.HeaderData.GetLength(1);
            int hHeight = bDto.HeaderData.GetLength(0);
            int tWidth = bDto.TableData.GetLength(1);
            int tHeight = bDto.TableData.GetLength(0);
            int typeRow = 2;
            HierarchyInfo hierarchyInfo = new HierarchyInfo();

            Dictionary<int, List<HierarchyAndMemberInfo>> hierarchies = new Dictionary<int, List<HierarchyAndMemberInfo>>();

            for (int i = 0; i < hWidth; i++)
            {
                //If the type of a column is an enumerator
                string type = bDto.HeaderData[typeRow, i];

                if (type.Trim().StartsWith("E:"))
                {
                    //Extract the hierarchy id from the type information

                    //Code changes to handle the Metrics
                    //type = "E:108, ,";
                    string[] strhierarchyID = Regex.Split(type.Trim(), "E:");
                    long hierarchyID = 0;
                    int hierarchyStartingMemberID = 0;
                    int isStartingMemberIncluded = 0;

                    var listSplit = strhierarchyID[1].Split(',');
                    if (listSplit.Length >= 1)
                    {
                        if (!string.IsNullOrEmpty(listSplit[0]))
                        {
                            hierarchyID = int.Parse(listSplit[0].Trim());
                        }
                    }
                    if (listSplit.Length >= 2)
                    {
                        if (!string.IsNullOrEmpty(listSplit[1]))
                        {
                            hierarchyStartingMemberID = int.Parse(listSplit[1].Trim());
                        }
                    }
                    if (listSplit.Length >= 3)
                    {
                        if (!string.IsNullOrEmpty(listSplit[2]))
                        {
                            if (int.Parse(listSplit[2].Trim()) == 0)
                                isStartingMemberIncluded = 0;
                            else if (int.Parse(listSplit[2].Trim()) == 1)
                                isStartingMemberIncluded = 1;
                        }
                    }

                    int startIndex = type.IndexOf(':') + 1;
                    int endIndex = type.IndexOf(';');
                    //int hierarchyID = endIndex == -1 ? Int32.Parse(type.Substring(startIndex)) : Int32.Parse(type.Substring(startIndex, endIndex - startIndex));

                    if (hierarchyID > 0)
                    {
                        //Code changes to handle the Metrics
                        //Get hierarchy information from DPM database
                        //List<HierarchyAndMemberInfo> info = (List<HierarchyAndMemberInfo>)hierarchyInfo.GetHierarchyAndMemberInfo(sqliteConnection, hierarchyID);
                        //List<HierarchyAndMemberInfo> info = new MetricsEnumerationExtractor(StaticSettings.ConnectionString).getHierarchyQnamesToTextLst(hierarchyID, hierarchyStartingMemberID, isStartingMemberIncluded);
                        List<HierarchyAndMemberInfo> info = (List<HierarchyAndMemberInfo>)hierarchyInfo.GetMetricsEnabledHierarchyAndMemberInfo(sqliteConnection, hierarchyID, hierarchyStartingMemberID, isStartingMemberIncluded);


                        if (info != null)
                        {
                            hierarchies.Add(i, info);
                        }

                    }
                }
            }

            IFormatProvider provider = CultureInfo.CurrentCulture;
            for (int row = 0; row < tHeight; row++)
            {
                for (int col = 0; col < tWidth; col++)
                {
                    int excelRow = bDto.HeaderRange.Row + hHeight + row;
                    int excelCol = bDto.HeaderRange.Column + col;

                    if (string.IsNullOrEmpty(bDto.TableData[row, col]))
                        continue;

                    string type = bDto.HeaderData[typeRow, col];

                    //Added for the boolean import

                    if (type.ToUpper().Trim() == "BOOLEAN")
                    {
                        if (bDto.TableData[row, col] != null)
                        {
                            if (bDto.TableData[row, col].ToString().ToUpper().Trim() == "TRUE")
                            {
                                bDto.TableData[row, col] = "1";
                            }
                            else if (bDto.TableData[row, col].ToString().ToUpper().Trim() == "FALSE")
                            {
                                bDto.TableData[row, col] = "0";
                            }
                            else
                                ThrowError(SolvencyDataType.Boolean, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);
                        }
                    }
                    else if (type.Trim().StartsWith("E:"))
                    {
                        if (hierarchies.ContainsKey(col))
                        {
                            //Get the hierarchy for the corresponding column
                            List<HierarchyAndMemberInfo> info = hierarchies[col];
                            if (info != null)
                            {
                                if (bDto.TableData[row, col].ToUpper().Trim().Contains('{'))
                                    bDto.TableData[row, col] = bDto.TableData[row, col].Substring(0, bDto.TableData[row, col].LastIndexOf('{'));

                                HierarchyAndMemberInfo matched = (from hm in info
                                                                  where hm.MemberLabel.ToUpper().Trim() == bDto.TableData[row, col].ToUpper().Trim()
                                                                  select hm).FirstOrDefault();

                                //If matched replace the hierarchy with the corresponding XBRL code
                                if (matched != null)
                                {
                                    bDto.TableData[row, col] = matched.MemberXBRLCode;
                                }
                                else
                                    ThrowError(SolvencyDataType.Code, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);
                            }
                        }
                    }
                    else if (type.ToUpper().Trim() == "PERCENTAGE")
                    {
                        if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                        {
                            var percentageValue = bDto.TableData[row, col];
                            if (!string.IsNullOrEmpty(percentageValue))
                            {
                                decimal convertedValue;
                                //modified for the current culture's decimal represenation
                                if (decimal.TryParse(bDto.TableData[row, col], NumberStyles.Float, CultureInfo.CurrentCulture.NumberFormat, out convertedValue))
                                {
                                    convertedValue = Convert.ToDecimal(bDto.TableData[row, col], provider);
                                    bDto.TableData[row, col] = Convert.ToString(convertedValue, CultureInfo.InvariantCulture);
                                }
                                else
                                    ThrowError(SolvencyDataType.Percentage, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);
                            }
                        }
                    }
                    else if (type.ToUpper().Trim() == "DATE")
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                            {
                                DateTime date = DateTime.Parse(bDto.TableData[row, col], provider);
                                bDto.TableData[row, col] = date.Date.ToString("yyyy/MM/dd");
                            }
                        }
                        catch (FormatException fe)
                        {
                            ThrowError(SolvencyDataType.Date, workSheet, fe, excelRow, excelCol, bDto.TableData[row, col]);
                        }
                    }
                    else if (type.ToUpper().Trim() == "DECIMAL" || type.ToUpper().Trim() == "MONETARY")
                    {
                        if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                        {
                            decimal convertedValue;
                            //modified for the current culture's decimal represenation
                            if (decimal.TryParse(bDto.TableData[row, col], NumberStyles.Float, CultureInfo.CurrentCulture.NumberFormat, out convertedValue))
                            {
                                convertedValue = Convert.ToDecimal(bDto.TableData[row, col], provider);
                                bDto.TableData[row, col] = Convert.ToString(convertedValue, CultureInfo.InvariantCulture);
                            }
                            else
                                ThrowError(SolvencyDataType.Monetry, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);
                        }
                    }
                    else if (type.ToUpper().Trim() == "INTEGER")
                    {
                        try
                        {
                            Decimal val = Decimal.Parse(bDto.TableData[row, col], provider);
                            bDto.TableData[row, col] = val.ToString(CultureInfo.InvariantCulture);//.ToString().Replace(',', '.');
                        }
                        catch (FormatException fe)
                        {
                            ThrowError(SolvencyDataType.Integer, workSheet, fe, excelRow, excelCol, bDto.TableData[row, col]);
                        }
                    }
                }
            }
        }
    }
}
