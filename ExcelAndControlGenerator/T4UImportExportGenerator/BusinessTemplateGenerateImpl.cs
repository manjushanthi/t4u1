using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;
using System.Drawing;

using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;

using AT2DPM.DAL.Model;
using T4UImportExportGenerator.Domain;
using T4UImportExportGenerator.DpmObjects;
using T4U.CRT.Generation.ExcelTemplateProcessor;
using MemoryProfilerProject;
using SolvencyII.Domain.Entities;
using SolvencyII.Data.SQLite;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Interfaces;

namespace T4UImportExportGenerator
{
    public class BusinessTemplateGenerateImpl : GenerateBase, IImportExportGenerate
    {
        int sRow = 1, sCol = 2;

        Dictionary<long, string> hierarchyRange;
        ISolvencyData _conn;
        int validationListCount = 1;
        Worksheet filterSheet;
        string filterSheetName = "CRT_Filters";

        private void FreezePane(Worksheet ws, int row, int col)
        {
            ws.Application.ActiveWindow.SplitRow = row;
            ws.Application.ActiveWindow.SplitColumn = col;
            ws.Application.ActiveWindow.FreezePanes = true;
        }

        private void DrawBorder(Worksheet ws, Range borderRange)
        {
            borderRange.Borders.Color = Color.Black;
            borderRange.Borders[XlBordersIndex.xlEdgeRight].Weight = 4;
            borderRange.Borders[XlBordersIndex.xlEdgeLeft].Weight = 4;
            borderRange.Borders[XlBordersIndex.xlEdgeBottom].Weight = 4;
            borderRange.Borders[XlBordersIndex.xlEdgeTop].Weight = 4;
        }

        private void RowColCodeColor(Range r)
        {
            //Change backgroud color of row/col code
            r.Interior.Color = ColorTranslator.ToOle(Color.LightBlue);
        }

        private void CellShadedColor(Range r)
        {
            //Change backgroud color of row/col code
            r.Interior.Color = ColorTranslator.ToOle(Color.DimGray);
        }

        private void CellDataColor(Range r)
        {
            //Change backgroud color of row/col code
            r.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
        }

        private int CalculateColOffset(List<AxisOrdinateControls> xOrdinatesList, AxisOrdinateControls ordinate)
        {
            int totalChild = 0;

            if (ordinate == null)
                return 0;

            List<AxisOrdinateControls> child =
                    (
                    from sub in xOrdinatesList
                    where sub.ParentOrdinateID == ordinate.OrdinateID
                    orderby sub.Order
                    select sub).ToList();


            foreach (AxisOrdinateControls a in child)
            {
                totalChild += CalculateColOffset(xOrdinatesList, a);
            }

            if (totalChild == 0)
                totalChild += child.Count();

            return totalChild;

        }



        private System.Drawing.Rectangle RenderClosedTable(Worksheet ws, int row, int col, List<AxisOrdinateControls> xOrdinatesList, List<AxisOrdinateControls> yOrdinatesList)
        {
            System.Drawing.Rectangle table = new System.Drawing.Rectangle();
            int yAxisWidth = 2;

            int maxLevel = xOrdinatesList.Max(m => m.Level);
            table.Y = row - 1;
            table.X = col + yAxisWidth;
            table.Height = maxLevel;

            int xRow = table.Y;
            int xCol = table.X;

            Stack<AxisOrdinateControls> ordinate = new Stack<AxisOrdinateControls>();

            for (int l = maxLevel; l >= 1; l--)
            {
                List<AxisOrdinateControls> subOrdinates = new List<AxisOrdinateControls>();

                var ord1 =
                    from sub in xOrdinatesList
                    where sub.Level == l
                    orderby sub.Order
                    select sub;

                if (l == maxLevel)
                {
                    var ord2 =
                        from sub in xOrdinatesList
                        //where sub.Level == 1 && sub.IsAbstractHeader == false
                        where sub.Level < maxLevel && sub.IsAbstractHeader == false
                        orderby sub.Order
                        select sub;

                    subOrdinates.AddRange(ord1);

                    if (maxLevel > 1)
                        subOrdinates.AddRange(ord2);

                    table.Width = subOrdinates.Count();

                    foreach (AxisOrdinateControls a in subOrdinates.OrderBy(t => t.OrdinateID))
                    {
                        ws.Cells[xRow + maxLevel, xCol].Value = a.OrdinateLabel;

                        Range cell = ws.Cells[xRow + maxLevel, xCol];
                        cell.EntireColumn.ColumnWidth = 20;
                        cell.WrapText = true;

                        Range r = ws.Cells[xRow + maxLevel + 1, xCol];
                        ws.Cells[xRow + maxLevel + 1, xCol].Value = a.OrdinateCode;

                        //Change backgroud color of row/col code
                        RowColCodeColor(r);

                        xCol++;
                    }
                }
                else
                {
                    subOrdinates.AddRange(ord1);

                    foreach (AxisOrdinateControls a in subOrdinates.OrderBy(t => t.Order))
                    {
                        ws.Cells[xRow + l, xCol].Value = a.OrdinateLabel;

                        int childOrd = CalculateColOffset(xOrdinatesList, a);


                        //If ordinate is at level 1 and it is not an abstract header then merge the row
                        //if (a.Level == 1 && a.IsAbstractHeader != true)
                        if (a.Level < maxLevel && a.IsAbstractHeader != true)
                        {
                            //Merge column
                            Range mergeColumn = ws.Range(ws.Cells[xRow + a.Level, xCol], ws.Cells[xRow + maxLevel, xCol]);
                            mergeColumn.Merge(Missing.Value);
                            mergeColumn.WrapText = true;
                            mergeColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        }

                        //Merge column and Offset next column based on number of child ordinates
                        if (childOrd > 0)
                        {
                            //Merge column
                            Range mergeColumn = ws.Range(ws.Cells[xRow + l, xCol], ws.Cells[xRow + l, xCol + childOrd - 1]);
                            mergeColumn.Merge();
                            mergeColumn.WrapText = true;
                            mergeColumn.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                            //Next column
                            xCol += childOrd;
                        }
                        else
                        {
                            xCol++;
                        }
                    }
                }

                //Reset postion
                //xRow++;
                xCol = table.X;
            }


            //Draw the table border
            DrawBorder(ws, row, sCol, yOrdinatesList.Count() + maxLevel + 1, table.Width + yAxisWidth);
            table = new System.Drawing.Rectangle(row, sCol, table.Width + yAxisWidth, yOrdinatesList.Count() + maxLevel + 1);

            //freeze the Panes  
            FreezePane(ws, row + maxLevel, 3);

            row += maxLevel + 1;

            //Set the Yordinate column width
            Range er = ws.Range("B:B", System.Type.Missing);

            er.EntireColumn.ColumnWidth = 50;
            er.WrapText = true;

            foreach (AxisOrdinateControls a in yOrdinatesList)
            {
                ws.Cells[row, col].Value = a.OrdinateLabel;
                ws.Cells[row, col].IndentLevel = a.Level;
                ws.Cells[row, col + 1].Value = a.OrdinateCode;
                Range r = ws.Cells[row, col + 1];

                //Change backgroud color of row/col code
                RowColCodeColor(r);

                row++;
            }

            return table;
        }

        private System.Drawing.Rectangle RenderOpenTable(Worksheet ws, string tableCode, int row, int col, List<AxisOrdinateControls> xOrdinatesList, List<AxisOrdinateControls> yOrdinatesList, IList<MAPPING> mapData, List<OrdinateHierarchy> ordHierarchy)
        {
            System.Drawing.Rectangle table = new System.Drawing.Rectangle();

            int yRow = row;
            int yCol = col;
            int maxLevel = xOrdinatesList.Max(m => m.Level);

            //Render y-ordinates in columns

            //Check for special case
            AxisOrdinateControls specialCase = yOrdinatesList.Where(t => t.SpecialCase.ToUpper() == "MULTIPLY ROWS/COLUMNS" && t.IsAbstractHeader == false).FirstOrDefault();
            AxisOrdinateControls redundantOrdinate = yOrdinatesList.Where(t => t.SpecialCase.ToUpper() != "MULTIPLY ROWS/COLUMNS" && t.IsAbstractHeader == false).FirstOrDefault();

            if (specialCase != null)
            {
               

                //if (redundantOrdinate == null)
                    //throw new ApplicationException("No redundant ordinate has been found for the special case");

                ws.Cells[yRow, yCol].Value = specialCase.OrdinateLabel;
                Range cell = ws.Cells[yRow, yCol];
                cell.EntireColumn.ColumnWidth = 20;
                cell.WrapText = true;

                ws.Cells[yRow + 1, yCol].Value = "PAGE" + specialCase.DimXbrlCode.ToUpper();//specialCase.OrdinateCode;
                Range r = ws.Cells[yRow + 1, yCol];

                //Change backgroud color of row/col code
                RowColCodeColor(r);

                string colCode = specialCase.OrdinateCode;

                MAPPING type = mapData.Where(t => t.DYN_TAB_COLUMN_NAME == "PAGE" + specialCase.DimXbrlCode.ToUpper()).FirstOrDefault();
                OrdinateHierarchy ordHie = ordHierarchy.Where(o => o.OrdinateCode == colCode).FirstOrDefault();
                if (ordHie != null)
                {
                    //Range dataRange = ws.Range(ws.Cells[row + 1, col], );
                    Range sRange = ws.Cells[row + 2, yCol];
                    Range eRange = ws.Cells[row + 2, yCol].get_End(XlDirection.xlDown);

                    Range rcToEnd = ws.Range(sRange, eRange);

                    FormatCell(rcToEnd, type, ordHie);
                }

                yCol++;
            }
            else
            {
                foreach (AxisOrdinateControls a in yOrdinatesList)
                {
                    //Do not include the abstract header in the column
                    if (a.IsAbstractHeader)
                        continue;

                    //Do not include this column in the excel this is a special case
                    //if (a.SpecialCase.ToUpper() == "MULTIPLY ROWS/COLUMNS")
                    //  continue;

                    ws.Cells[yRow, yCol].Value = a.OrdinateLabel;
                    Range cell = ws.Cells[yRow, yCol];
                    cell.EntireColumn.ColumnWidth = 20;
                    cell.WrapText = true;

                    ws.Cells[yRow + 1, yCol].Value = a.OrdinateCode;

                    Range r = ws.Cells[yRow + 1, yCol];

                    //Change backgroud color of row/col code
                    RowColCodeColor(r);

                    string colCode = a.OrdinateCode;

                    //Range dataRange = ws.Range(ws.Cells[row + 1, col], );
                    Range sRange = ws.Cells[row + 2, yCol];
                    Range eRange = ws.Cells[row + 2, yCol].get_End(XlDirection.xlDown);

                    Range rcToEnd = ws.Range(sRange, eRange);

                    MAPPING type = mapData.Where(t => t.DYN_TAB_COLUMN_NAME == colCode).FirstOrDefault();
                    OrdinateHierarchy ordHie = ordHierarchy.Where(o => o.OrdinateCode == colCode).FirstOrDefault();
                    if (ordHie != null)
                    {
                        FormatCell(rcToEnd, type, ordHie);
                    }
                    else
                    {
                        FormatCell(rcToEnd, type, ordHie);
                        rcToEnd.Locked = false;
                    }

                    yCol++;
                }
            }

            //freeze the Panes  
            FreezePane(ws, yRow + 1, yCol - 1);

            var filteredXOrdinates = (from x in xOrdinatesList
                                      where x.Level == maxLevel && x.IsAbstractHeader == false
                                      orderby x.Order
                                      select x)
                                      .Union(from x in xOrdinatesList
                                                 where x.Level == 1 && x.IsAbstractHeader == false
                                                 orderby x.Order
                                                 select x
                                                 );

            //Render x-Ordinates in columns
            foreach (AxisOrdinateControls a in filteredXOrdinates.OrderBy(t=>t.OrdinateCode))
            {
                //Do not include the abstract header in the column
                if (a.IsAbstractHeader)
                    continue;

                ws.Cells[yRow, yCol].Value = a.OrdinateLabel;
                Range cell = ws.Cells[yRow, yCol];
                cell.EntireColumn.ColumnWidth = 20;
                cell.WrapText = true;



                string colCode = a.OrdinateCode;

                if (specialCase != null && redundantOrdinate != null)
                {
                    ws.Cells[yRow + 1, yCol].Value = redundantOrdinate.OrdinateCode + a.OrdinateCode;
                    colCode = redundantOrdinate.OrdinateCode + a.OrdinateCode;
                }
                else
                {
                    ws.Cells[yRow + 1, yCol].Value = a.OrdinateCode;

                }

                Range r = ws.Cells[yRow + 1, yCol];

                //Change backgroud color of row/col code
                RowColCodeColor(r);


                MAPPING type = mapData.Where(t => t.DYN_TAB_COLUMN_NAME == colCode).FirstOrDefault();
                OrdinateHierarchy ordHie = ordHierarchy.Where(o => o.OrdinateCode == colCode).FirstOrDefault();
                
                //Try another way if ordHie is null
                if(ordHie == null)
                    ordHie = ordHierarchy.Where(o => o.OrdinateCode == a.OrdinateCode).FirstOrDefault();
                
                //Range dataRange = ws.Range(ws.Cells[row + 1, col], );
                Range sRange = ws.Cells[row + 2, yCol];
                Range eRange = ws.Cells[row + 2, yCol].get_End(XlDirection.xlDown);

                Range rcToEnd = ws.Range(sRange, eRange);

                if (ordHie != null)
                {
                    FormatCell(rcToEnd, type, ordHie);
                    rcToEnd.Locked = false;
                }
                else if(type == null)
                {
                    CellShadedColor(rcToEnd);
                    rcToEnd.Locked = true;
                }
                else
                {
                    FormatCell(rcToEnd, type, ordHie);
                    rcToEnd.Locked = false;
                }

                yCol++;


            }

            //Create a Range for Table data

            Range tableData = ws.Range(ws.Cells[yRow + 1, col], ws.Cells[yRow + 1, yCol - 1]);

            tableData.Name = ws.Name + "!" + tableCode.Replace(" ", string.Empty) + ".TD";

            DrawBorder(ws, row, col, 2, yCol - col);

            //Range dataRange = ws.Range(ws.Cells[row + 1, col], );
            Range startRange = ws.Cells[row + 2, col];
            Range endRange = ws.Cells[row + 2, yCol - col + 1].get_End(XlDirection.xlDown);

            Range rangeColumnToEnd = ws.Range(startRange, endRange);
            //rangeColumnToEnd.Locked = false;
            DrawBorder(ws, rangeColumnToEnd);

            //CellDataColor(rangeColumnToEnd);

            return table;
        }

        private System.Drawing.Rectangle RenderSemiOpenTable(Worksheet ws, int row, int col)
        {
            System.Drawing.Rectangle table = new System.Drawing.Rectangle();


            return table;
        }

        private void FormatCell(Range cell, MAPPING mapData, OrdinateHierarchy ordHie)
        {
            if ((mapData != null && mapData.DATA_TYPE == "E") || ordHie != null)
            {
                //Get th ordinate hierarchy
                //OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == rowCode || a.OrdinateCode == colCode).FirstOrDefault();

                if (ordHie == null)
                    throw new KeyNotFoundException("Could not able to find the hierarchy");

                if (hierarchyRange.ContainsKey(ordHie.HierarchyID))
                {
                    string validationListName = hierarchyRange[ordHie.HierarchyID];

                    cell.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop,
                                 XlFormatConditionOperator.xlEqual, validationListName, Type.Missing);
                    cell.Validation.ErrorMessage = "Please select values from the dropdown";
                    cell.Validation.ErrorTitle = "Invalid data";


                }
                else
                {
                    HierarchyInfo hierarchyInfo = new HierarchyInfo();
                    /*List<HierarchyAndMemberInfo> info =
                        (List<HierarchyAndMemberInfo>)hierarchyInfo.GetMetricsEnabledHierarchyAndMemberInfo(
                        _conn,
                        ordHie.HierarchyID,
                        (int)ordHie.HierarchyStartingMemberID,
                        ordHie.IsStartingMemberIncluded ? 1 : 0);*/

                    Dictionary<string, string> hierarchyValuesMatricsEnabled = MemoryProfilerProject.TestHelper.HierarchyLookupWithMetricsEnabled(TemplateGeneration.DbPath, (int)ordHie.HierarchyID, (int)ordHie.HierarchyStartingMemberID, Convert.ToInt32(ordHie.IsStartingMemberIncluded ? 1 : 0));

                    if (hierarchyValuesMatricsEnabled == null || hierarchyValuesMatricsEnabled.Count == 0)
                        return;

                    List<string> lstFiedls = hierarchyValuesMatricsEnabled.Select(itm => itm.Key + "    {" + itm.Value + "}").ToList();

                    Range addRange = filterSheet.Range(filterSheet.Cells[1, validationListCount], filterSheet.Cells[lstFiedls.Count, validationListCount]);
                    int rowCount = 0;
                    foreach (Range indivual_row in addRange.Rows)
                    {
                        indivual_row.Value = lstFiedls[rowCount++];
                    }

                    //string specialCharPattern = "[^a-zA-Z0-9_]+";
                    //string columnToValidate = z.OrdinateCode != null ? Regex.Replace(z.OrdinateCode, specialCharPattern, "_") : string.Empty;

                    Range enumerationRange = filterSheet.Range(filterSheet.Cells[1, validationListCount], filterSheet.Cells[lstFiedls.Count, validationListCount]);
                    //string validationListName = tv.TableCode.Replace('.', '_') + "_" + columnToValidate + "_" + z.OrdinateID;
                    string validationListName = "Hierarchy_" + ordHie.HierarchyID.ToString();
                    enumerationRange.Name = validationListName;
                    validationListName = "=" + validationListName;

                    cell.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop,
                                 XlFormatConditionOperator.xlEqual, validationListName, Type.Missing);
                    cell.Validation.ErrorMessage = "Please select values from the dropdown";
                    cell.Validation.ErrorTitle = "Invalid data";

                    validationListCount = validationListCount + 1;

                    //Add the hierarchy to the dictionary to avoid duplication
                    hierarchyRange.Add(ordHie.HierarchyID, validationListName);
                }

                cell.Interior.Color = ColorTranslator.ToOle(Color.Lavender);

            }
            else if (mapData != null && mapData.DATA_TYPE == "B")
            {
                List<string> boolValues = new List<string>();
                boolValues.Add("TRUE");
                boolValues.Add("FALSE");
                string boolValuesJoined = string.Join(",", boolValues.ToArray());
                cell.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateList, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                        NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlEqual, boolValuesJoined, Type.Missing);
                cell.Validation.ErrorMessage = "Please select the bool value";
                cell.Validation.ErrorTitle = "Invalid data";
                cell.Validation.IgnoreBlank = false;

                cell.Interior.Color = ColorTranslator.ToOle(Color.MistyRose);
            }

            else if (mapData != null && mapData.DATA_TYPE == "I")
            {
                cell.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateWholeNumber, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                        NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, int.MinValue, int.MaxValue);
                cell.Validation.ErrorMessage = "Please enter only the integer value";
                cell.Validation.ErrorTitle = "Invalid data";
                cell.Validation.IgnoreBlank = false;

                cell.Interior.Color = ColorTranslator.ToOle(Color.Azure);
            }
            else if (mapData != null && mapData.DATA_TYPE == "P")
            {
                cell.ClearFormats();
                cell.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                         NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                cell.Validation.ErrorMessage = "Please enter only the decimal value";
                cell.Validation.ErrorTitle = "Invalid data";
                cell.Validation.IgnoreBlank = false;
                cell.NumberFormat = "0.00%";

                cell.Interior.Color = ColorTranslator.ToOle(Color.LightPink);
            }
            /*else if(mapData.DATA_TYPE == "P")
            {
                                case "DECIMAL":
                                    validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                           NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                                    validationTableRange.Validation.ErrorMessage = "Please enter only the decimal value";
                                    validationTableRange.Validation.ErrorTitle = "Invalid data";
                                    validationTableRange.Validation.IgnoreBlank = false;
                                    validationTableRange.NumberFormat = "0.0000";
            }*/
            else if (mapData != null && mapData.DATA_TYPE == "M")
            {


                cell.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                        NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                cell.Validation.ErrorMessage = "Please enter only the decimal value";
                cell.Validation.ErrorTitle = "Invalid data";
                cell.Validation.IgnoreBlank = false;
                cell.NumberFormat = "0.00";
                cell.Interior.Color = ColorTranslator.ToOle(Color.LightYellow);
            }
            else if (mapData != null && mapData.DATA_TYPE == "D")
            {

                cell.ClearFormats();
                cell.NumberFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                cell.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDate, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                        NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlGreaterEqual, DateTime.Parse("1/1/1900"), Type.Missing);
                cell.Validation.ErrorMessage = "Please enter only the date value";
                cell.Validation.ErrorTitle = "Invalid data";
                cell.Validation.IgnoreBlank = false;

                cell.Interior.Color = ColorTranslator.ToOle(Color.PaleGoldenrod);
            }
            else if (mapData != null && mapData.DATA_TYPE == "S")
            {
                cell.Interior.Color = ColorTranslator.ToOle(Color.Wheat);
            }

            cell.Locked = false;
            cell.WrapText = true;

        }

        private void RenderClosedTableCells(Worksheet ws, string tableCode, int row, int col, List<FactInformation> shadedControls, IList<MAPPING> mapData, List<OrdinateHierarchy> ordHierarchy)
        {
            // Get ordinateIDs across all controls and check if they should be shaded.

            var orderedControls =
                from o in shadedControls
                orderby o.YordinateID, o.XordinateID
                select o;

            int cellRow = row;
            int cellCol = col;

            FactInformation prevData = null;

            foreach (var a in orderedControls)
            {
                if (prevData != null)
                {
                    if (a.YordinateID > prevData.YordinateID)
                    {
                        cellRow++;

                        cellCol = col;
                    }
                }

                Range r = ws.Cells[cellRow, cellCol];

                if (a.IsShaded)
                {

                    CellShadedColor(r);
                }
                else
                {
                    r.Locked = false;

                    Range rowRange = ws.Cells[cellRow, col - 1];
                    Range colRange = ws.Cells[row - 1, cellCol];

                    if ((rowRange != null && rowRange.Value != null) && (colRange != null && colRange.Value != null))
                    {

                        string rowCode = rowRange.Value.ToString();
                        string colCode = colRange.Value.ToString();
                        string rowcolCode = rowCode + colCode;
                        MAPPING type = mapData.Where(t => t.DYN_TAB_COLUMN_NAME == rowcolCode).FirstOrDefault();
                        OrdinateHierarchy ordHie = ordHierarchy.Where(o => o.OrdinateCode == rowCode || o.OrdinateCode == colCode).FirstOrDefault();
                        if (type != null)
                            FormatCell(r, type, ordHie);

                    }

                    //CellDataColor(r);
                }

                //Move to next column
                cellCol++;

                //Retain the previous state
                prevData = a;

            }

            //Create a Range for Table data

            Range tableData = ws.Range(ws.Cells[row - 1, col - 1], ws.Cells[cellRow, cellCol - 1]);

            tableData.Name = ws.Name + "!" + tableCode.Replace(" ", string.Empty) + ".TD";

        }

        public override void Generate(DPMdb dpmContext, IEnumerable<GenerateInfo> generatorInfo, string versionNumber)
        {
            decimal totalRecord = 0;
            decimal progressRecord = 0;

            string tocSheetName = "Table of contents";


            //Calculate total record to process
            foreach (GenerateInfo g in generatorInfo)
                totalRecord += g.TableCodes != null ? g.TableCodes.Count() : 0;

            _conn = new SQLiteConnection(TemplateGeneration.DbPath); ;
            IEnumerable<mAxi> zAxis = _conn.Query<mAxi>("select * from mAxis where AxisOrientation = 'Z'");
            IEnumerable<mAxi> yAxis = _conn.Query<mAxi>("select * from mAxis where AxisOrientation = 'Y'");
            IEnumerable<mAxi> xAxis = _conn.Query<mAxi>("select * from mAxis where AxisOrientation = 'X'");
            IEnumerable<mOpenAxisValueRestriction> openAxis = _conn.Query<mOpenAxisValueRestriction>("select * from mOpenAxisValueRestriction");


            foreach (GenerateInfo g in generatorInfo)
            {
                //Create an excel file
                NetOffice.ExcelApi.Application application = new NetOffice.ExcelApi.Application();
                application.Workbooks.Add();
                application.DisplayAlerts = false;

                Workbook newWb = null;
                newWb = application.ActiveWorkbook;

                //Filter sheet contains all the enumerations value
                filterSheet = (Worksheet)application.ActiveSheet;
                filterSheet.Name = filterSheetName;
                application.Sheets.Add(filterSheet);
                hierarchyRange = new Dictionary<long, string>();

                //Hide the filter sheet from the user
                filterSheet.Visible = XlSheetVisibility.xlSheetHidden;

                OnProgressChanged((int)((progressRecord / totalRecord) * 100), "Table of contents & Index sheet");

                //Table of contents sheet
                //int contentRow = 2;
                Worksheet tableContents = (Worksheet)application.ActiveSheet;
                //tableContents.Name = tocSheetName;
                application.Sheets.Add(tableContents);
                GenerateToc(dpmContext, tableContents, g.TableCodes.Keys.ToArray());
                /*tableContents.Cells[contentRow, 2].Value = "S.No";
                tableContents.Cells[contentRow, 3].Value = "Table Code";
                tableContents.Cells[contentRow, 4].Value = "Table Label";*/

                //Add index sheet
                int infoSheetRow = 2;
                Worksheet infoSheet = (Worksheet)application.ActiveSheet;
                infoSheet.Name = "Info";
                application.Sheets.Add(infoSheet);
                infoSheet.Cells[infoSheetRow, 2].Value = "Module Name";
                infoSheet.Cells[infoSheetRow, 3].Value = g.ModuleLabel;
                infoSheetRow++;
                infoSheet.Cells[infoSheetRow, 2].Value = "File Name";
                infoSheet.Cells[infoSheetRow, 3].Value = g.FileName;
                infoSheetRow++;
                infoSheet.Cells[infoSheetRow, 2].Value = "Version";
                infoSheet.Cells[infoSheetRow, 3].Value = "Ver:" + versionNumber;

                Range infoRange = infoSheet.Range(infoSheet.Cells[2, 2], infoSheet.Cells[infoSheetRow, 3]);

                infoRange.Name = infoSheet.Name + "!" + "Version";

                /*int sno = 1;
                foreach (string s in g.TableCodes.Keys)
                {
                    //Get table information from DPM database
                    mTable tv = dpmContext.mTables.FirstOrDefault(x => x.TableCode == s);

                    //Add to table of contents
                    contentRow++;
                    tableContents.Cells[contentRow, 2].Value = sno++;
                    tableContents.Cells[contentRow, 3].Value = tv.TableCode;
                    tableContents.Cells[contentRow, 4].Value = tv.TableLabel;

                    Range link = tableContents.Cells[contentRow, 3];
                    tableContents.Hyperlinks.Add(link, "", tv.TableCode + "!A1", "Click to navigate " + tv.TableCode, tv.TableCode);
                }*/

                //New Code to get the required tables               
                //HDOrdinateHierarchyIdList.HDOrdinateHierarchyIdListTable = Program.GetOrdinateHierarchyID_HD_Table(TemplateGeneration.DbPath);
                //MDOrdinateHierarchyIdList.MDOrdinateHierarchyIdTable = Program.GetOrdinateHierarchyID_MD_Table(TemplateGeneration.DbPath);
                PageColumnDetailsList.PageColumnDetailsTable = Program.GetPageColumnDetails(TemplateGeneration.DbPath);

                foreach (string s in g.TableCodes.Keys.ToArray<string>().Reverse())
                {
                    //Count one record is progressing and invoke the progress event
                    progressRecord++;
                    OnProgressChanged((int)((progressRecord / totalRecord) * 100), s);

#if DEBUG
                    string[] testTemplate = 
                    { 
                        
                        /*"S.01.02.07.03",
                        "S.01.03.01.01",
                        "S.01.03.01.02",
                        "S.12.02.01.02",
                        "S.17.02.01.02",
                        "S.22.06.01.03",
                        "S.27.01.01.23",
                        "S.27.01.01.23",
                        "S.30.03.01.01",
                        "SR.27.01.01.23",
                        "SR.27.01.01.24",
                        "E.03.01.16.02",
                        "S.02.02.01.02"*/
                        "C 00.01"
                    };

                    //if (!testTemplate.Contains(s))
                        //continue;
#endif

                    int row = sRow, col = sCol;

                    //Get table information from DPM database
                    mTable tv = dpmContext.mTables.FirstOrDefault(x => x.TableCode == s);

                    //throw exception if the process couldn't find the table
                    if (tv == null)
                        throw new Exception("An error occured, could not find table for " + s);

                    // Get data object
                    GetSQLData getData = TemplateGeneration.DbPath != null ? new GetSQLData(TemplateGeneration.DbPath) : new GetSQLData();

                    List<string> ids = new List<string>() { tv.TableID.ToString() };
                    // Get full list of controls required for the ui control
                    List<AxisOrdinateControls> controlList = getData.GetControlInformation(new List<string>() { tv.TableID.ToString() }).ToList();

                    // Get combo information
                    getData.GetControlComboInformation(ref controlList);

                    // Special Cases
                    List<AxisOrdinateControls> mulitpleRowUserControls = controlList.Where(c => c.SpecialCase == "multiply rows/columns").ToList();
                    if (mulitpleRowUserControls.Any())
                    {
                        // We need to populate the DimXbrlCode
                        getData.GetControlDimXbrlCodeForSpecialCases(ref mulitpleRowUserControls, new List<string>() { tv.TableID.ToString() });
                    }

                    //Get ordinates from the table tv
                    string mappingQuery = string.Format("select * from mapping where table_version_id = {0} ", tv.TableID);
                    IList<MAPPING> mapData = _conn.Query<MAPPING>(mappingQuery);

                    HierarchyInfo hierarchyInfo = new HierarchyInfo();
                    List<OrdinateHierarchy> ordHierarchy = (List<OrdinateHierarchy>)hierarchyInfo.GetOrdinateHierarchyMD(_conn, tv.TableCode);

                    // Work out if this template is open in two dimensions.
                    int specialCols = controlList.Count(c => c.SpecialCase == "multiply rows/columns");
                    bool twoDimOpen = (specialCols == 2);

                    //Add new excel worksheet
                    Worksheet tempSheet = (Worksheet)application.ActiveSheet;
                    tempSheet.Name = tv.TableCode.Replace(" ", string.Empty) ;
                    application.Sheets.Add(tempSheet);

                    //Hide the grid lines
                    tempSheet.Application.ActiveWindow.DisplayGridlines = false;

                    //Add back to TOC
                    Range link = tempSheet.Cells[row, col];
                    tempSheet.Hyperlinks.Add(tempSheet.Cells[row, col], "", "'" + tocSheetName + "'!A1", "Click to navigate " + tocSheetName, "Back to TOC");

                    row++;

                    //Add table label
                    tempSheet.Cells[row, col].Value = tv.TableLabel;

                    //Merge title cells
                    tempSheet.Range("B2", "I2").Merge();
                    tempSheet.Range("B2", "I2").Font.Size = 16;

                    //Set the First column width
                    Range er = tempSheet.Range("A:A", System.Type.Missing);
                    er.EntireColumn.ColumnWidth = 3;

                    row += 2;

                    //Z-Ordinates
                    //Check if we have more than one Z-Axis                    

                    IEnumerable<AxisOrdinateControls> zOrdinatesList =
                        from axi in zAxis
                        from c in controlList
                        where axi.AxisID == c.AxisID &&
                        c.AxisOrientation == "Z" && axi.IsOpenAxis == true
                        select c;

                    if (zOrdinatesList != null && zOrdinatesList.Count() > 0)
                    {
                        DrawBorder(tempSheet, row, col, zOrdinatesList.Count(), 3);

                        //Change the name of the tempSheet 
                        //tempSheet.Name = tempSheet.Name + ".001";

                        //Create a filter range
                        Range tableData = tempSheet.Range(tempSheet.Cells[row, col + 1], tempSheet.Cells[row + zOrdinatesList.Count() - 1, col + 2]);

                        tableData.Name = tempSheet.Name + "!" + tv.TableCode.Replace(" ", string.Empty) + ".FL";
                    }

                    foreach (AxisOrdinateControls z in zOrdinatesList)
                    {
                        tempSheet.Cells[row, col].Value = z.OrdinateLabel;
                        tempSheet.Cells[row, col + 1].Value = z.OrdinateCode;
                        Range r = tempSheet.Cells[row, col + 1];
                        Range dataCell = tempSheet.Cells[row, col + 2];
                        CellDataColor(dataCell);
                        dataCell.Locked = false;

                        RowColCodeColor(r);

                        // Work out the parts for user drop downs AND get the linked table names
                        // Each of these are combos;
                        List<string> tableNames;
                        List<FormDataPage> pageData = getData.GetClosedPageData(ids, out tableNames, 0);

                        // Check to see if they are typed and need textComboBoxes
                        foreach (FormDataPage dataPage in pageData)
                        {
                            if (z.AxisID != dataPage.AxisID)
                                continue;

                            if (!dataPage.IsTypedDimension)
                            {
                                long hierarchyID = openAxis.Where(t => t.AxisID == dataPage.AxisID).Select(t => t.HierarchyID).FirstOrDefault();
                                string hierarchyCode = openAxis.Where(t => t.AxisID == dataPage.AxisID).Select(t => t.HierarchyCode).FirstOrDefault();
                                Dictionary<string, string> hierarchyValues = MemoryProfilerProject.TestHelper.HierarchyLookupWithName(TemplateGeneration.DbPath, (int)hierarchyID);
                                List<string> lstFiedls = hierarchyValues.Select(itm => itm.Value).ToList();

                                if (hierarchyRange.ContainsKey(hierarchyID))
                                {
                                    string validationListName = hierarchyRange[hierarchyID];

                                    Range rangeColumn = tempSheet.Cells[row, col + 2];
                                    rangeColumn.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop,
                                                 XlFormatConditionOperator.xlEqual, validationListName, Type.Missing);
                                    rangeColumn.Validation.ErrorMessage = "Please select values from the dropdown";
                                    rangeColumn.Validation.ErrorTitle = "Invalid data";


                                }
                                else if (lstFiedls.Count > 0)
                                {
                                    Range addRange = filterSheet.Range(filterSheet.Cells[1, validationListCount], filterSheet.Cells[lstFiedls.Count, validationListCount]);
                                    int rowCount = 0;
                                    foreach (Range indivual_row in addRange.Rows)
                                    {
                                        indivual_row.Value = lstFiedls[rowCount];
                                        rowCount = rowCount + 1;
                                    }

                                    string specialCharPattern = "[^a-zA-Z0-9_]+";
                                    string columnToValidate = z.OrdinateCode != null ? Regex.Replace(z.OrdinateCode, specialCharPattern, "_") : string.Empty;

                                    Range enumerationRange = filterSheet.Range(filterSheet.Cells[1, validationListCount], filterSheet.Cells[lstFiedls.Count, validationListCount]);
                                    //string validationListName = tv.TableCode.Replace('.', '_') + "_" + columnToValidate + "_" + z.OrdinateID;
                                    string validationListName = "Hierarchy_" + hierarchyID.ToString();
                                    enumerationRange.Name = validationListName;
                                    validationListName = "=" + validationListName;

                                    Range rangeColumn = tempSheet.Cells[row, col + 2];
                                    rangeColumn.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop,
                                                 XlFormatConditionOperator.xlEqual, validationListName, Type.Missing);
                                    rangeColumn.Validation.ErrorMessage = "Please select values from the dropdown";
                                    rangeColumn.Validation.ErrorTitle = "Invalid data";

                                    validationListCount = validationListCount + 1;

                                    //Add the hierarchy to the dictionary to avoid duplication
                                    hierarchyRange.Add(hierarchyID, validationListName);
                                }
                            }
                        }

                        row++;
                    }

                    //Reset column and increment row
                    row += 1;
                    col = sCol;

                    //X-Ordinates
                    List<AxisOrdinateControls> xOrdinatesList = (
                        from c in controlList
                        where c.AxisOrientation == "X"
                        orderby c.Order
                        select c).ToList<AxisOrdinateControls>();

                    List<AxisOrdinateControls> yOrdinatesList = (
                       from c in controlList
                       where c.AxisOrientation == "Y"
                       orderby c.Order
                       select c).ToList<AxisOrdinateControls>();

                    int xAxisCount =
                        (
                        from xaxi in xAxis
                        from x in xOrdinatesList
                        where xaxi.AxisID == x.AxisID &&
                        xaxi.IsOpenAxis == true
                        select x.AxisOrientation
                        ).Distinct().Count();

                    int yAxisCount =
                        (
                        from yaxi in yAxis
                        from y in yOrdinatesList
                        where yaxi.AxisID == y.AxisID &&
                        yaxi.IsOpenAxis == true
                        select y.AxisOrientation
                        ).Distinct().Count();

                    //ClosedTable
                    if (xAxisCount == 0 && yAxisCount == 0)
                    {
                        System.Drawing.Rectangle border = RenderClosedTable(tempSheet, row, col, xOrdinatesList, yOrdinatesList);

                        List<FactInformation> shadedControls = getData.GetAllTableControls(ids, 0).ToList();


                        int cellRow = row + xOrdinatesList.Max(m => m.Level) + 1;
                        int cellCol = col + 2;

                        RenderClosedTableCells(tempSheet, tv.TableCode, cellRow, cellCol, shadedControls, mapData, ordHierarchy);

                        DrawBorder(tempSheet, border.X, border.Y, border.Height, border.Width);
                    }
                    else if (xAxisCount == 0 && yAxisCount > 0)
                    {
                        RenderOpenTable(tempSheet, tv.TableCode, row, col, xOrdinatesList, yOrdinatesList, mapData, ordHierarchy);
                    }
                    else if (xAxisCount > 0 || yAxisCount > 0)
                    {
                        //RenderSemiOpenTable(tempSheet, row, col);

                        List<AxisOrdinateControls> xWithOpenAxis =
                            (
                            from axi in xAxis
                            from c in controlList
                            where axi.AxisID == c.AxisID &&
                            c.AxisOrientation == "X" && axi.IsOpenAxis == true
                            select c
                            ).ToList();

                        List<AxisOrdinateControls> yWithOpenAxis =
                            (
                            from axi in yAxis
                            from c in controlList
                            where axi.AxisID == c.AxisID &&
                            c.AxisOrientation == "Y" && axi.IsOpenAxis == true
                            select c
                            ).ToList();


                        List<AxisOrdinateControls> xWithoutOpenAxis =
                            (
                            from axi in xAxis
                            from c in controlList
                            where axi.AxisID == c.AxisID &&
                            c.AxisOrientation == "X" && axi.IsOpenAxis == false
                            select c
                            ).ToList();

                        //Change the name of the tempSheet 
                        //if(!tempSheet.Name.EndsWith(".001"))
                        //tempSheet.Name = tempSheet.Name + ".001";

                        if (xWithOpenAxis != null && xWithOpenAxis.Count > 0)
                        {

                            DrawBorder(tempSheet, row, col, xWithOpenAxis.Count(), 3);

                            //Create a range name with .XL as suffix
                            Range filterData = tempSheet.Range(tempSheet.Cells[row, col + 1], tempSheet.Cells[row + xWithOpenAxis.Count() - 1, col + 2]);

                            filterData.Name = tempSheet.Name + "!" + tv.TableCode + ".XL";

                            foreach (AxisOrdinateControls x in xWithOpenAxis)
                            {
                                tempSheet.Cells[row, col].Value = x.OrdinateLabel;
                                tempSheet.Cells[row, col + 1].Value = x.OrdinateCode;
                                Range r = tempSheet.Cells[row, col + 1];

                                Range dataCell = tempSheet.Cells[row, col + 2];
                                
                                string pageCode = "PAGE" + x.DimXbrlCode.ToUpper();
                                MAPPING type = mapData.Where(t => t.DYN_TAB_COLUMN_NAME.ToUpper() == pageCode.ToUpper()).FirstOrDefault();
                                OrdinateHierarchy ordHie = ordHierarchy.Where(o => o.OrdinateCode == x.OrdinateCode).FirstOrDefault();

                                FormatCell(dataCell, type, ordHie);
                                row++;
                            }

                            row++;
                        }




                        if (yWithOpenAxis != null && yWithOpenAxis.Count > 0)
                        {

                            DrawBorder(tempSheet, row, col, yWithOpenAxis.Count(), 3);

                            //Create a range name with .XL as suffix
                            Range filterData = tempSheet.Range(tempSheet.Cells[row, col + 1], tempSheet.Cells[row + xWithOpenAxis.Count() - 1, col + 2]);

                            filterData.Name = tempSheet.Name + "!" + tv.TableCode + ".YL";

                            foreach (AxisOrdinateControls y in yWithOpenAxis)
                            {
                                tempSheet.Cells[row, col].Value = y.OrdinateLabel;
                                tempSheet.Cells[row, col + 1].Value = y.OrdinateCode;
                                Range r = tempSheet.Cells[row, col + 1];

                                Range dataCell = tempSheet.Cells[row, col + 2];

                                string pageCode = "PAGE" + y.DimXbrlCode.ToUpper();
                                MAPPING type = mapData.Where(t => t.DYN_TAB_COLUMN_NAME.ToUpper() == pageCode.ToUpper()).FirstOrDefault();
                                OrdinateHierarchy ordHie = ordHierarchy.Where(o => o.OrdinateCode == y.OrdinateCode).FirstOrDefault();

                                FormatCell(dataCell, type, ordHie);

                                row++;
                            }

                            row += 1;
                        }

                        List<AxisOrdinateControls> yWithoutOpenAxis =
                            (
                            from axi in yAxis
                            from c in controlList
                            where axi.AxisID == c.AxisID &&
                            c.AxisOrientation == "Y" && axi.IsOpenAxis == false
                            select c
                            ).ToList();

                        System.Drawing.Rectangle border = RenderClosedTable(tempSheet, row, col, xWithoutOpenAxis, yWithoutOpenAxis);

                        List<FactInformation> shadedControls = getData.GetSemiTableControls(ids, 0).ToList();

                        int cellRow = row + xOrdinatesList.Max(m => m.Level) + 1;
                        int cellCol = col + 2;

                        RenderClosedTableCells(tempSheet, tv.TableCode, cellRow, cellCol, shadedControls, mapData, ordHierarchy);

                        DrawBorder(tempSheet, border.X, border.Y, border.Height, border.Width);
                    }
                    else
                        throw new ApplicationException("Invalid table type");


                    //Protect the sheet
                    tempSheet.Protect();
                }

                /*

                //Format table of contents sheet
                contentRow--;

                //Set the First column width
                Range cell = tableContents.Range("A:A", System.Type.Missing);
                cell.EntireColumn.ColumnWidth = 3;

                //Table code column
                cell = tableContents.Range(tableContents.Cells[2, 3], tableContents.Cells[contentRow, 3]);
                cell.EntireColumn.ColumnWidth = 15;
                //TableLable column
                cell = tableContents.Range(tableContents.Cells[2, 4], tableContents.Cells[contentRow, 4]);
                cell.EntireColumn.ColumnWidth = 70;
                cell.WrapText = true;

                //Header Range
                cell = tableContents.Range(tableContents.Cells[2, 2], tableContents.Cells[2, 4]);
                cell.Font.Bold = true;
                //Change backgroud color of row/col code
                cell.Interior.Color = ColorTranslator.ToOle(Color.Gold);
                
                DrawBorder(tableContents, 2, 2, contentRow, 3);
                
                //Hide the grid lines
                tableContents.Activate();
                tableContents.Application.ActiveWindow.DisplayGridlines = false;

                //Move table of contents to first
                tableContents.Move(application.Sheets[1]);
                tableContents.Protect();*/

                FormatToc(tableContents, application.Sheets);


                //Format info sheet
                infoSheetRow--;

                //Set the First column width
                Range cell = infoSheet.Range("A:A", System.Type.Missing);
                cell.EntireColumn.ColumnWidth = 3;
                //First column
                cell = infoSheet.Range(infoSheet.Cells[2, 2], infoSheet.Cells[infoSheetRow + 1, 2]);
                cell.EntireColumn.ColumnWidth = 20;
                cell.WrapText = true;
                cell.Interior.Color = ColorTranslator.ToOle(Color.LightGoldenrodYellow);

                //Second column
                cell = infoSheet.Range(infoSheet.Cells[2, 3], infoSheet.Cells[infoSheetRow, 3]);
                cell.EntireColumn.ColumnWidth = 50;
                cell.WrapText = true;

                DrawBorder(infoSheet, 2, 2, infoSheetRow, 2);

                //Hide the grid lines
                infoSheet.Activate();
                infoSheet.Application.ActiveWindow.DisplayGridlines = false;

                //Move table of contents to first
                infoSheet.Move(application.Sheets[1]);
                infoSheet.Protect();

                //Remove any extra sheets
                List<Worksheet> wsDelete = new List<Worksheet>();

                foreach (Worksheet w in newWb.Worksheets)
                {
                    if (w.Name.StartsWith("Sheet"))
                        wsDelete.Add(w);
                }

                application.DisplayAlerts = false;

                foreach (Worksheet w in wsDelete)
                {
                    if (w != null)
                        w.Delete();
                }

                //if (newWb.Worksheets.Count > 2)
                //{
                //    Worksheet ws = (Worksheet)newWb.Worksheets.ElementAt(0);
                //    if (ws != null)
                //        ws.Delete();
                //}

                application.DisplayAlerts = true;


                //Save file
                newWb.SaveAs(g.FilePath, Missing.Value, Missing.Value, Missing.Value, false,
                                          false, XlSaveAsAccessMode.xlExclusive);
                newWb.Close();

                application.Quit();
                application.Dispose();

            }

            //Send the completed event
            OnCompleted(null, false, "Completed");
        }

        public void GenerateAsync(DPMdb dpmContext, IEnumerable<GenerateInfo> generatorInfo, string versionNumber)
        {
            ThreadParam param = new ThreadParam
            {
                DpmContext = dpmContext,
                GeneratorInfo = generatorInfo,
                VersionNumber = versionNumber
            };

            Thread thread = new Thread(Run);
            thread.Start(param);
        }
    }
}
