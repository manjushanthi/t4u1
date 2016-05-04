using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

using SolvencyII.Domain;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Dto;

namespace SolvencyII.ExcelImportExportLib.Transform
{
    public class TransformExcelBusinessData : TransformBase
    {

        public override void Transform(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto)
        {
            BusinessTemplateDto bDto = dto as BusinessTemplateDto;
            if (bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Basic template transfer object");

            mTable table = (new TableInfo().GetTable(sqliteConnection, bDto.TableCode)).FirstOrDefault();
            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(sqliteConnection, 1)).FirstOrDefault();

            Dictionary<int, List<HierarchyAndMemberInfo>> hierarchies = new Dictionary<int, List<HierarchyAndMemberInfo>>();

            List<MAPPING> mapData = (List<MAPPING>)(new MappingInfo()).GetMappingInfo(sqliteConnection, table.TableID);

            List<OrdinateHierarchy> ordHierarchy = (List<OrdinateHierarchy>)(new HierarchyInfo()).GetOrdinateHierarchyMD(sqliteConnection, bDto.TableCode);
            IFormatProvider provider = CultureInfo.CurrentCulture;

            //If closed table transform the AT style format into CRT format
            //The first row and first column are ordinate codes
            if (bDto.TypeOfTable == TableType.CLOSED_TABLE || bDto.TypeOfTable == TableType.SEMI_OPEN_TABLE)
            {
                Dictionary<string, string> tableValues = new Dictionary<string, string>();
                for (int row = 1; row < bDto.TableData.GetLength(0); row++)
                {
                    for (int col = 1; col < bDto.TableData.GetLength(1); col++)
                    {
                        string rowCode = bDto.TableData[row, 0];
                        string colCode = bDto.TableData[0, col];

                        string rowcolCode = rowCode + colCode;

                        int excelRow = bDto.TableDataRange.Row + row;
                        int excelCol = bDto.TableDataRange.Column + col;

                        //Check if the specified row col code matches with the mapping table
                        MAPPING dataType = mapData.Where(a => a.DYN_TAB_COLUMN_NAME.ToUpper() == rowcolCode).FirstOrDefault();
                        if (dataType != null)
                        {
                            if (dataType.DATA_TYPE == "B")
                            {
                                if (bDto.TableData[row, col] != null)
                                {
                                    if (bDto.TableData[row, col].ToString().ToUpper().Trim() == "TRUE")
                                        bDto.TableData[row, col] = "1";

                                    else if (bDto.TableData[row, col].ToString().ToUpper().Trim() == "FALSE")
                                        bDto.TableData[row, col] = "0";

                                    else
                                        ThrowError(SolvencyDataType.Boolean, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);

                                }
                            }
                            else if (dataType.DATA_TYPE == "E")
                            {

                                //Get th ordinate hierarchy
                                OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == rowCode || a.OrdinateCode == colCode).FirstOrDefault();

                                if (ordHie == null)
                                    throw new KeyNotFoundException("Could not able to find the hierarchy");

                                List<HierarchyAndMemberInfo> info =
                                        (List<HierarchyAndMemberInfo>)(new HierarchyInfo()).GetMetricsEnabledHierarchyAndMemberInfo(
                                        sqliteConnection,
                                        ordHie.HierarchyID,
                                        (int)ordHie.HierarchyStartingMemberID,
                                        ordHie.IsStartingMemberIncluded ? 1 : 0);


                                //Get the hierarchy for the corresponding column
                                if (info != null)
                                {
                                    if (bDto.TableData[row, col] != null && bDto.TableData[row, col].ToUpper().Trim().Contains('{'))
                                    {
                                        int startIndex = bDto.TableData[row, col].IndexOf('{');
                                        int endIndex = bDto.TableData[row, col].IndexOf('}');
                                        bDto.TableData[row, col] = bDto.TableData[row, col].Substring(startIndex + 1, (endIndex - startIndex) - 1);


                                        //Get the HierarchyAndMemberInfo for the corresponding text
                                        HierarchyAndMemberInfo matched = (from hm in info
                                                                          where hm.MemberXBRLCode.ToUpper() == bDto.TableData[row, col].ToUpper()
                                                                          select hm).FirstOrDefault();

                                        //If matched replace the hierarchy with the corresponding XBRL code
                                        if (matched == null)
                                            ThrowError(SolvencyDataType.Code, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);
                                    }
                                }
                            }
                            else if (dataType.DATA_TYPE == "P")
                            {
                                if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                                {
                                    var percentageValue = bDto.TableData[row, col];
                                    if (!string.IsNullOrEmpty(percentageValue))
                                    {
                                        decimal convertedValue;
                                        //modified for the current culture's decimal represenation
                                        if (decimal.TryParse(bDto.TableData[row, col], NumberStyles.Float | NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent, CultureInfo.CurrentCulture.NumberFormat, out convertedValue))
                                        {
                                            //convertedValue = Convert.ToDecimal(bDto.TableData[row, col], provider);
                                            bDto.TableData[row, col] = Convert.ToString(convertedValue, CultureInfo.InvariantCulture);
                                        }
                                        else
                                            ThrowError(SolvencyDataType.Percentage, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);
                                    }
                                }
                            }
                            else if (dataType.DATA_TYPE == "D")
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                                    {
                                        double decimalDate;
                                        DateTime date;

                                        if (double.TryParse(bDto.TableData[row, col], out decimalDate))
                                        {
                                            date = DateTime.FromOADate(decimalDate);
                                            bDto.TableData[row, col] = date.Date.ToString("yyyy/MM/dd");
                                        }
                                        else if (DateTime.TryParse(bDto.TableData[row, col], provider, DateTimeStyles.None, out date))
                                        {
                                            bDto.TableData[row, col] = date.Date.ToString("yyyy/MM/dd");
                                        }
                                    }
                                }
                                catch (FormatException fe)
                                {
                                    ThrowError(SolvencyDataType.Date, workSheet, fe, excelRow, excelCol, bDto.TableData[row, col]);
                                }
                            }
                            else if (dataType.DATA_TYPE == "M")
                            {
                                if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                                {
                                    decimal convertedValue;
                                    //modified for the current culture's decimal represenation
                                    if (decimal.TryParse(bDto.TableData[row, col], NumberStyles.Float, CultureInfo.CurrentCulture.NumberFormat, out convertedValue))
                                    {
                                        //convertedValue = Convert.ToDecimal(bDto.TableData[row, col], provider);
                                        bDto.TableData[row, col] = Convert.ToString(convertedValue, CultureInfo.InvariantCulture);
                                    }
                                    else
                                        ThrowError(SolvencyDataType.Monetry, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);

                                }
                            }
                            else if (dataType.DATA_TYPE == "I")
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                                    {
                                        Decimal val = Decimal.Parse(bDto.TableData[row, col], provider);
                                        bDto.TableData[row, col] = val.ToString(CultureInfo.InvariantCulture);//.ToString().Replace(',', '.');
                                    }
                                }
                                catch (FormatException fe)
                                {
                                    ThrowError(SolvencyDataType.Integer, workSheet, fe, excelRow, excelCol, bDto.TableData[row, col]);
                                }
                            }


                            tableValues.Add(rowcolCode, bDto.TableData[row, col]);
                        }
                    }
                }

                //Get th ordinate hierarchy
                ordHierarchy = (List<OrdinateHierarchy>)(new HierarchyInfo()).GetOpenAxisHierarchy(sqliteConnection, bDto.TableCode);

                if (bDto.FilterData != null)
                {
                    for (int i = 0; i < bDto.FilterData.GetLength(0); i++)
                    {
                        string ordCode = bDto.FilterData[i, 0];

                        int excelRow = bDto.FilterRange.Row + i;
                        int excelCol = bDto.FilterRange.Column + 1;

                        //Check if the specified row col code matches with the mapping table
                        OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == ordCode).FirstOrDefault();
                        MAPPING dataType = mapData.Where(a => a.DYN_TAB_COLUMN_NAME.ToUpper() == ordHie.PageColumn.ToUpper()).FirstOrDefault();

                        if (dataType != null)
                        {
                            //Get th ordinate hierarchy
                            if (ordHie != null && ordHie.HierarchyID > 0)
                            {
                                // throw new KeyNotFoundException("Could not able to find the hierarchy");

                                List<HierarchyAndMemberInfo> info =
                                        (List<HierarchyAndMemberInfo>)(new HierarchyInfo()).GetMetricsEnabledHierarchyAndMemberInfo(
                                        sqliteConnection,
                                        ordHie.HierarchyID,
                                        (int)ordHie.HierarchyStartingMemberID,
                                        ordHie.IsStartingMemberIncluded ? 1 : 0);


                                //Get the hierarchy for the corresponding column
                                if (info != null)
                                {
                                    if (bDto.FilterData[i, 1] != null && bDto.FilterData[i, 1].ToUpper().Trim().Contains('{'))
                                    {
                                        int startIndex = bDto.FilterData[i, 1].IndexOf('{');
                                        int endIndex = bDto.FilterData[i, 1].IndexOf('}');
                                        bDto.FilterData[i, 1] = bDto.FilterData[i, 1].Substring(startIndex + 1, (endIndex - startIndex) - 1);

                                        //Get the HierarchyAndMemberInfo for the corresponding text
                                        HierarchyAndMemberInfo matched = (from hm in info
                                                                          where hm.MemberXBRLCode.ToUpper() == bDto.FilterData[i, 1].ToUpper()
                                                                          select hm).FirstOrDefault();

                                        //If matched replace the hierarchy with the corresponding XBRL code
                                        if (matched == null)
                                            ThrowError(SolvencyDataType.Code, workSheet, null, excelRow, excelCol, bDto.FilterData[i, 1]);

                                    }


                                }
                            }

                            tableValues.Add(dataType.DYN_TAB_COLUMN_NAME.ToUpper(), bDto.FilterData[i, 1]);
                        }
                    }
                }

                if (bDto.XFilterData != null)
                {
                    for (int i = 0; i < bDto.XFilterData.GetLength(0); i++)
                    {
                        string ordCode = bDto.XFilterData[i, 0];

                        int excelRow = bDto.XFilterRange.Row + i;
                        int excelCol = bDto.XFilterRange.Column + 1;

                        //Check if the specified row col code matches with the mapping table
                        OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == ordCode).FirstOrDefault();
                        MAPPING dataType = mapData.Where(a => a.DYN_TAB_COLUMN_NAME.ToUpper() == ordHie.PageColumn.ToUpper()).FirstOrDefault();

                        if (dataType != null)
                        {
                            //Get th ordinate hierarchy
                            if (ordHie != null)
                            {
                                // throw new KeyNotFoundException("Could not able to find the hierarchy");

                                List<HierarchyAndMemberInfo> info =
                                        (List<HierarchyAndMemberInfo>)(new HierarchyInfo()).GetMetricsEnabledHierarchyAndMemberInfo(
                                        sqliteConnection,
                                        ordHie.HierarchyID,
                                        (int)ordHie.HierarchyStartingMemberID,
                                        ordHie.IsStartingMemberIncluded ? 1 : 0);


                                //Get the hierarchy for the corresponding column
                                if (info != null)
                                {
                                    if (bDto.XFilterData[i, 1] != null && bDto.XFilterData[i, 1].ToUpper().Trim().Contains('{'))
                                    {
                                        int startIndex = bDto.XFilterData[i, 1].IndexOf('{');
                                        int endIndex = bDto.XFilterData[i, 1].IndexOf('}');
                                        bDto.XFilterData[i, 1] = bDto.XFilterData[i, 1].Substring(startIndex + 1, (endIndex - startIndex) - 1);

                                        //Get the HierarchyAndMemberInfo for the corresponding text
                                        HierarchyAndMemberInfo matched = (from hm in info
                                                                          where hm.MemberXBRLCode.ToUpper() == bDto.XFilterData[i, 1].ToUpper()
                                                                          select hm).FirstOrDefault();

                                        //If matched replace the hierarchy with the corresponding XBRL code
                                        if (matched == null)
                                            ThrowError(SolvencyDataType.Code, workSheet, null, excelRow, excelCol, bDto.XFilterData[i, 1]);

                                    }


                                }
                            }

                            tableValues.Add(dataType.DYN_TAB_COLUMN_NAME.ToUpper(), bDto.XFilterData[i, 1]);
                        }
                    }
                }

                if (bDto.YFilterData != null)
                {
                    for (int i = 0; i < bDto.YFilterData.GetLength(0); i++)
                    {
                        string ordCode = bDto.YFilterData[i, 0];

                        int excelRow = bDto.YFilterRange.Row + i;
                        int excelCol = bDto.YFilterRange.Column + 1;

                        //Check if the specified row col code matches with the mapping table
                        OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == ordCode).FirstOrDefault();
                        MAPPING dataType = mapData.Where(a => a.DYN_TAB_COLUMN_NAME.ToUpper() == ordHie.PageColumn.ToUpper()).FirstOrDefault();

                        if (dataType != null)
                        {
                            //Get th ordinate hierarchy
                            if (ordHie != null)
                            {
                                // throw new KeyNotFoundException("Could not able to find the hierarchy");

                                List<HierarchyAndMemberInfo> info =
                                        (List<HierarchyAndMemberInfo>)(new HierarchyInfo()).GetMetricsEnabledHierarchyAndMemberInfo(
                                        sqliteConnection,
                                        ordHie.HierarchyID,
                                        (int)ordHie.HierarchyStartingMemberID,
                                        ordHie.IsStartingMemberIncluded ? 1 : 0);


                                //Get the hierarchy for the corresponding column
                                if (info != null)
                                {
                                    if (bDto.YFilterData[i, 1] != null && bDto.YFilterData[i, 1].ToUpper().Trim().Contains('{'))
                                    {
                                        int startIndex = bDto.YFilterData[i, 1].IndexOf('{');
                                        int endIndex = bDto.YFilterData[i, 1].IndexOf('}');
                                        bDto.YFilterData[i, 1] = bDto.YFilterData[i, 1].Substring(startIndex + 1, (endIndex - startIndex) - 1);

                                        //Get the HierarchyAndMemberInfo for the corresponding text
                                        HierarchyAndMemberInfo matched = (from hm in info
                                                                          where hm.MemberXBRLCode.ToUpper() == bDto.YFilterData[i, 1].ToUpper()
                                                                          select hm).FirstOrDefault();

                                        //If matched replace the hierarchy with the corresponding XBRL code
                                        if (matched == null)
                                            ThrowError(SolvencyDataType.Code, workSheet, null, excelRow, excelCol, bDto.YFilterData[i, 1]);

                                    }


                                }
                            }

                            tableValues.Add(dataType.DYN_TAB_COLUMN_NAME.ToUpper(), bDto.YFilterData[i, 1]);
                        }
                    }
                }

                //Create a new table data and deflate the vlaues
                string[,] newTableData = new string[1, tableValues.Count];
                string[,] newHeaderData = new string[1, tableValues.Count];

                int pos = 0;
                foreach (string s in tableValues.Keys)
                {
                    newHeaderData[0, pos] = s;
                    newTableData[0, pos] = tableValues[s];

                    pos++;
                }

                //Finally attach the new table and header data into main stream
                bDto.HeaderData = newHeaderData;
                bDto.TableData = newTableData;
            }

            else if (bDto.TypeOfTable == TableType.OPEN_TABLE)
            {
                int hWidth = bDto.HeaderData.GetLength(1);
                int hHeight = bDto.HeaderData.GetLength(0);
                int tWidth = bDto.TableData.GetLength(1);
                int tHeight = bDto.TableData.GetLength(0);

                for (int row = 0; row < tHeight; row++)
                {
                    for (int col = 0; col < tWidth; col++)
                    {
                        int excelRow = bDto.TableDataRange.Row + row;
                        int excelCol = bDto.TableDataRange.Column + col;

                        if (string.IsNullOrEmpty(bDto.TableData[row, col]))
                            continue;

                        MAPPING dataType = mapData.Where(a => a.DYN_TAB_COLUMN_NAME.ToUpper() == bDto.HeaderData[0, col]).FirstOrDefault();

                        if (dataType == null)
                            continue;

                        //Added for the boolean import

                        if (dataType.DATA_TYPE == "B")
                        {
                            if (bDto.TableData[row, col] != null)
                            {
                                if (bDto.TableData[row, col].ToString().ToUpper().Trim() == "TRUE")
                                    bDto.TableData[row, col] = "1";

                                else if (bDto.TableData[row, col].ToString().ToUpper().Trim() == "FALSE")
                                    bDto.TableData[row, col] = "0";

                                else
                                    ThrowError(SolvencyDataType.Boolean, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);

                            }
                        }

                        else if (dataType.DATA_TYPE == "E")
                        {

                            //Get th ordinate hierarchy
                            OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == bDto.HeaderData[0, col]).FirstOrDefault();

                            //This piece of code is to fix for the semi-open table in y-axis which is renderd as open table
                            if(ordHie == null)
                            {
                                ordHie = ordHierarchy.Where(a => a.OrdinateCode ==  bDto.HeaderData[0, col].Substring(5)).FirstOrDefault();
                            }
                            //End of fix

                            if (ordHie == null)
                                throw new KeyNotFoundException("Could not able to find the hierarchy");

                            List<HierarchyAndMemberInfo> info =
                                    (List<HierarchyAndMemberInfo>)(new HierarchyInfo()).GetMetricsEnabledHierarchyAndMemberInfo(
                                    sqliteConnection,
                                    ordHie.HierarchyID,
                                    (int)ordHie.HierarchyStartingMemberID,
                                    ordHie.IsStartingMemberIncluded ? 1 : 0);


                            //Get the hierarchy for the corresponding column
                            if (info != null)
                            {
                                if (bDto.TableData[row, col].ToUpper().Trim().Contains('{'))
                                {
                                    int startIndex = bDto.TableData[row, col].IndexOf('{');
                                    int endIndex = bDto.TableData[row, col].IndexOf('}');
                                    bDto.TableData[row, col] = bDto.TableData[row, col].Substring(startIndex + 1, (endIndex - startIndex) - 1);
                                }

                                //Get the HierarchyAndMemberInfo for the corresponding text
                                HierarchyAndMemberInfo matched = (from hm in info
                                                                  where hm.MemberXBRLCode.ToUpper() == bDto.TableData[row, col].ToUpper()
                                                                  select hm).FirstOrDefault();

                                //If matched replace the hierarchy with the corresponding XBRL code
                                if (matched == null)
                                    ThrowError(SolvencyDataType.Code, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);

                            }
                        }

                        else if (dataType.DATA_TYPE == "P")
                        {
                            if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                            {

                                var percentageValue = bDto.TableData[row, col];
                                if (!string.IsNullOrEmpty(percentageValue))
                                {
                                    decimal convertedValue;
                                    //modified for the current culture's decimal represenation
                                    if (decimal.TryParse(bDto.TableData[row, col], NumberStyles.Float|NumberStyles.AllowDecimalPoint|NumberStyles.AllowExponent, CultureInfo.CurrentCulture.NumberFormat, out convertedValue))
                                    {
                                        //convertedValue = Convert.ToDecimal(bDto.TableData[row, col], provider);
                                        bDto.TableData[row, col] = Convert.ToString(convertedValue, CultureInfo.InvariantCulture);
                                    }
                                    else
                                        ThrowError(SolvencyDataType.Percentage, workSheet, null, excelRow, excelCol, bDto.TableData[row, col]);
                                }
                            }
                        }
                        else if (dataType.DATA_TYPE == "D")
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(bDto.TableData[row, col]))
                                {
                                    double decimalDate;
                                    DateTime date;

                                    if(double.TryParse(bDto.TableData[row, col], out decimalDate))
                                    {
                                        date = DateTime.FromOADate(decimalDate);
                                        bDto.TableData[row, col] = date.Date.ToString("yyyy/MM/dd");
                                    }
                                    else if(DateTime.TryParse(bDto.TableData[row, col], provider, DateTimeStyles.None, out date))
                                    {
                                        bDto.TableData[row, col] = date.Date.ToString("yyyy/MM/dd");
                                    }
                                }
                            }
                            catch (FormatException fe)
                            {
                                ThrowError(SolvencyDataType.Date, workSheet, fe, excelRow, excelCol, bDto.TableData[row, col]);
                            }
                        }

                        else if (dataType.DATA_TYPE == "M")
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
                        else if (dataType.DATA_TYPE == "I")
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

                        if (bDto.TableData[row, col] != null && bDto.TableData[row, col].ToUpper().Trim().Contains('{'))
                        {
                            int startIndex = bDto.TableData[row, col].IndexOf('{');
                            int endIndex = bDto.TableData[row, col].IndexOf('}');
                            bDto.TableData[row, col] = bDto.TableData[row, col].Substring(startIndex + 1, (endIndex - startIndex) - 1);
                        }
                    }
                }

                //Get th ordinate hierarchy
                ordHierarchy = (List<OrdinateHierarchy>)(new HierarchyInfo()).GetOpenAxisHierarchy(sqliteConnection, bDto.TableCode);

                if (bDto.FilterData != null)
                {
                    Dictionary<string, string> tableValues = new Dictionary<string, string>();

                    for (int i = 0; i < bDto.FilterData.GetLength(0); i++)
                    {
                        string ordCode = bDto.FilterData[i, 0];

                        int excelRow = bDto.FilterRange.Row + i;
                        int excelCol = bDto.FilterRange.Column + 1;

                        //Check if the specified row col code matches with the mapping table
                        OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == ordCode).FirstOrDefault();
                        MAPPING dataType = mapData.Where(a => a.DYN_TAB_COLUMN_NAME.ToUpper() == ordHie.PageColumn.ToUpper()).FirstOrDefault();

                        if (dataType != null)
                        {
                            //Get th ordinate hierarchy
                            if (ordHie != null && ordHie.HierarchyID > 0)
                            {
                                // throw new KeyNotFoundException("Could not able to find the hierarchy");

                                List<HierarchyAndMemberInfo> info =
                                        (List<HierarchyAndMemberInfo>)(new HierarchyInfo()).GetMetricsEnabledHierarchyAndMemberInfo(
                                        sqliteConnection,
                                        ordHie.HierarchyID,
                                        (int)ordHie.HierarchyStartingMemberID,
                                        ordHie.IsStartingMemberIncluded ? 1 : 0);


                                //Get the hierarchy for the corresponding column
                                if (info != null)
                                {
                                    if (bDto.FilterData[i, 1].ToUpper().Trim().Contains('{'))
                                    {
                                        int startIndex = bDto.FilterData[i, 1].IndexOf('{');
                                        int endIndex = bDto.FilterData[i, 1].IndexOf('}');
                                        bDto.FilterData[i, 1] = bDto.FilterData[i, 1].Substring(startIndex + 1, (endIndex - startIndex) - 1);
                                    }

                                    //Get the HierarchyAndMemberInfo for the corresponding text
                                    HierarchyAndMemberInfo matched = (from hm in info
                                                                      where hm.MemberXBRLCode.ToUpper() == bDto.FilterData[i, 1].ToUpper()
                                                                      select hm).FirstOrDefault();

                                    //If matched replace the hierarchy with the corresponding XBRL code
                                    if (matched == null)
                                        ThrowError(SolvencyDataType.Code, workSheet, null, excelRow, excelCol, bDto.FilterData[i, 1]);
                                }
                            }

                            tableValues.Add(dataType.DYN_TAB_COLUMN_NAME.ToUpper(), bDto.FilterData[i, 1]);
                        }
                    }

                    //Re-arrange the table data values to include the filter data as well
                    //Create a new table data and deflate the vlaues
                    string[,] newTableData = new string[bDto.TableData.GetLength(0), tableValues.Count + bDto.TableData.GetLength(1)];
                    string[,] newHeaderData = new string[1, tableValues.Count + bDto.HeaderData.GetLength(1)];

                    int pos = 0;
                    foreach (string s in tableValues.Keys)
                    {
                        newHeaderData[0, pos] = s;

                        for (int i = 0; i < newTableData.GetLength(0); i++)
                            newTableData[i, pos] = tableValues[s];

                        pos++;
                    }


                    for (int j = 0; j < bDto.TableData.GetLength(1); j++)
                    {
                        for (int i = 0; i < bDto.TableData.GetLength(0); i++)
                        {
                            newTableData[i, pos + j] = bDto.TableData[i, j];
                        }

                        newHeaderData[0, pos + j] = bDto.HeaderData[0, j];
                    }

                    //Finally attach the new table and header data into main stream
                    bDto.HeaderData = newHeaderData;
                    bDto.TableData = newTableData;
                }


                //Verify and remove if all the columns are listed in the crt table
                List<string> invalidCol = new List<string>();
                foreach (string s in bDto.HeaderData.Cast<string>().Skip(0).Take(bDto.HeaderData.GetLength(1)))
                {
                    MAPPING found = mapData.Where(t => t.DYN_TAB_COLUMN_NAME.ToUpper() == s.ToUpper()).FirstOrDefault();
                    if (found == null)
                        invalidCol.Add(s);

                }

                //Remove those columns from the matrix
                if (invalidCol.Count > 0)
                {
                    string[,] newTableData = new string[bDto.TableData.GetLength(0), bDto.TableData.GetLength(1) - invalidCol.Count];
                    string[,] newHeaderData = new string[1, bDto.HeaderData.GetLength(1) - invalidCol.Count];

                    int pos = 0;

                    for (int j = 0; j < bDto.TableData.GetLength(1); j++)
                    {
                        if (invalidCol.Contains(bDto.HeaderData[0, j]))
                            continue;

                        for (int i = 0; i < bDto.TableData.GetLength(0); i++)
                        {
                            newTableData[i, pos] = bDto.TableData[i, j];
                        }

                        newHeaderData[0, pos] = bDto.HeaderData[0, j];

                        pos++;
                    }

                    //Finally attach the new table and header data into main stream
                    bDto.HeaderData = newHeaderData;
                    bDto.TableData = newTableData;

                }
            }
        }
    }
}
