using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;

using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;

using AT2DPM.DAL.Model;
using AT2DPM.Events.Delegate;

using T4U.CRT.Generation.ExcelTemplateProcessor;
using MemoryProfilerProject;

using T4UImportExportGenerator.Domain;
using System.Drawing;

namespace T4UImportExportGenerator
{
    public class BasicTemplateGenerateImpl : GenerateBase, IImportExportGenerate
    {

        public override void Generate(DPMdb dpmContext, IEnumerable<GenerateInfo> generatorInfo, string versionNumber)
        {
            decimal totalRecord = 0;
            decimal progressRecord = 0;
            int validationListCount = 1;
            string filterSheetName = "CRT_Filters";
            //string dirPath = string.Empty;
           

            //Calculate total record to process
            foreach (GenerateInfo g in generatorInfo)
                totalRecord += g.TableCodes != null ? g.TableCodes.Count() : 0;

            foreach (GenerateInfo g in generatorInfo)
            {
                //Create an excel file
                NetOffice.ExcelApi.Application application = new NetOffice.ExcelApi.Application();
                application.Workbooks.Add();
                application.DisplayAlerts = false;

                Workbook newWb = null;
                newWb = application.ActiveWorkbook;

                //Filter sheet contains all the enumerations value
                Worksheet filterSheet = (Worksheet)application.ActiveSheet;
                filterSheet.Name = filterSheetName;
                application.Sheets.Add(filterSheet);

                //Hide the filter sheet from the user
                filterSheet.Visible = XlSheetVisibility.xlSheetHidden;

                //Table of contents sheet
                Worksheet tableContents = (Worksheet)application.ActiveSheet;
                application.Sheets.Add(tableContents);
                GenerateToc(dpmContext, tableContents, g.TableCodes.Keys.ToArray());

                //New Code to get the required tables               
                HDOrdinateHierarchyIdList.HDOrdinateHierarchyIdListTable = Program.GetOrdinateHierarchyID_HD_Table(TemplateGeneration.DbPath);
                MDOrdinateHierarchyIdList.MDOrdinateHierarchyIdTable = Program.GetOrdinateHierarchyID_MD_Table(TemplateGeneration.DbPath);
                PageColumnDetailsList.PageColumnDetailsTable = Program.GetPageColumnDetails(TemplateGeneration.DbPath);

                foreach (string s in g.TableCodes.Keys.ToArray<string>().Reverse())
                {
                    //Count one record is progressing and invoke the progress event
                    progressRecord++;
                    OnProgressChanged((int)((progressRecord / totalRecord) * 100), s);
                    
                     //Generate CRT structures

                    //Get table information from DPM database
                    mTable tv = dpmContext.mTables.FirstOrDefault(x => x.TableCode == s);

//#if DEBUG
//                    if (tv.TableCode != "S.08.02.01.02")//S.11.01.01.01
//                       continue;
//#endif

                    //throw exception if the process couldn't find the table
                    if (tv == null)
                        throw new Exception("An error occured, could not find table for " + s);

                    //Get ordinates from the table tv
                    ExcelTemplateColumns objExcelTemplateColumns = Program.getColumns(TemplateGeneration.DbPath, tv, PageColumnDetailsList.PageColumnDetailsTable);  
                    
                    List<string> yOrdinatesList = new List<string>();
                    List<string> xOrdinatesList = new List<string>();
                    List<string> lstColumns = new List<string>();
                    List<string> dataTypes = objExcelTemplateColumns.DataTypes;

                    lstColumns = objExcelTemplateColumns.ColumnsCodes;
                    dataTypes = objExcelTemplateColumns.DataTypes;
                    yOrdinatesList = objExcelTemplateColumns.Rows;
                    xOrdinatesList = objExcelTemplateColumns.columns;
                    lstColumns.Insert(0, string.Empty);
                    xOrdinatesList.Insert(0, "Column Header");
                    yOrdinatesList.Insert(0, "Row Header");

                     //Add new excel worksheet
                    Worksheet tempSheet = (Worksheet)application.ActiveSheet;
                    tempSheet.Name = tv.TableCode;

                    //Create range for table header
                    var row = tempSheet.Range(tempSheet.Cells[1, 1], tempSheet.Cells[1, lstColumns.Count]);
                    var column = tempSheet.Range(tempSheet.Cells[2, 1], tempSheet.Cells[2, lstColumns.Count]);
                    var type = tempSheet.Range(tempSheet.Cells[3, 1], tempSheet.Cells[3, lstColumns.Count]);
                    var range = tempSheet.Range(tempSheet.Cells[4, 1], tempSheet.Cells[4, lstColumns.Count]);

                    row.Value = xOrdinatesList.ToArray();
                    column.Value = yOrdinatesList.ToArray();
                    range.Value = lstColumns.ToArray();
                    application.Sheets.Add(tempSheet);

                    tempSheet.Columns.VerticalAlignment = NetOffice.ExcelApi.Enums.XlVAlign.xlVAlignCenter;
                    tempSheet.Columns.ColumnWidth = 15;
                    tempSheet.Columns.WrapText = true;
                  
                    //Create header range
                    Range setnamedHeaderRange = tempSheet.Range(tempSheet.Cells[1, 2], tempSheet.Cells[4, lstColumns.Count]); 
                    setnamedHeaderRange.Interior.Color = XlRgbColor.rgbLightGrey;
                    setnamedHeaderRange.Borders.LineStyle = XlLineStyle.xlDouble;
                    setnamedHeaderRange.Borders.Weight = 2;
                    setnamedHeaderRange.Borders.Color = Color.Black;
                    setnamedHeaderRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateCustom, Type.Missing, Type.Missing, "\"\"");
                    //setnamedHeaderRange = tempSheet.Range(tempSheet.Cells[1, 2], tempSheet.Cells[4, lstColumns.Count]);
                    setnamedHeaderRange.Name = tv.TableCode.Trim() + "!" + tv.TableCode + ".TH";
                    
                                       
                    ////To freeze the Panes                   
                    tempSheet.Application.ActiveWindow.SplitRow = 4;
                    tempSheet.Application.ActiveWindow.FreezePanes = true;                   


                    for (int i = 0; i <= dataTypes.Count - 1; i++)
                    {
                        if (dataTypes[i].StartsWith("E:"))
                        {
                            int hierarchyID = 0;
                            int hierarchyStartingMemberID = 0;
                            int isStartingMemberIncluded = 0;

                            if (dataTypes[i].StartsWith("E:"))
                            {
                                string[] strhierarchyID = Regex.Split(dataTypes[i].Trim(), "E:");
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
                                        if (int.Parse(listSplit[2].Trim())==0)
                                            isStartingMemberIncluded =0;
                                        else if (int.Parse(listSplit[2].Trim())==1)
                                            isStartingMemberIncluded =1;
                                    }
                                }

                                //string hierarchyIDValue = strhierarchyID[1];
                                //hierarchyID = Convert.ToInt32(hierarchyIDValue);
                               
                            }

                            if (hierarchyID != 0)
                            {
                                //Code changes to handle the Metrics  
                                //dataTypes[i] = "E:" + hierarchyID.ToString();     
                                //int testhierarchyID = Convert.ToInt32(hierarchyID);HierarchyLookupWithMetricsEnabled
                                //Dictionary<string, string> hierarchyValues1 = MemoryProfilerProject.TestHelper.HierarchyLookupWithName(TemplateGeneration.DbPath, testhierarchyID);
                                Dictionary<string, string> hierarchyValuesMatricsEnabled = MemoryProfilerProject.TestHelper.HierarchyLookupWithMetricsEnabled(TemplateGeneration.DbPath, hierarchyID, hierarchyStartingMemberID, isStartingMemberIncluded);
                                //Dictionary<string, string> hierarchyValues = new MetricsEnumerationExtractor(dpmContext).getHierarchyQnamesToText(hierarchyID, hierarchyStartingMemberID, isStartingMemberIncluded);

                                //List<string> lstFiedlstest = hierarchyValues.Select(itm => itm.Value).ToList();

                                List<string> lstFiedls = hierarchyValuesMatricsEnabled.Select(itm => itm.Key + "    {" + itm.Value + "}").ToList();

                                if (lstFiedls.Count > 0)
                                {
                                    Range addRange = filterSheet.Range(filterSheet.Cells[1, validationListCount], filterSheet.Cells[lstFiedls.Count, validationListCount]);
                                    int rowCount = 0;
                                    foreach (Range indivual_row in addRange.Rows)
                                    {
                                        indivual_row.Value = lstFiedls[rowCount];
                                        rowCount = rowCount + 1;
                                    }

                                    string specialCharPattern = "[^a-zA-Z0-9_]+";
                                    string columnToValidate = string.Empty;
                                    columnToValidate = xOrdinatesList[i+1] != null ? Regex.Replace(xOrdinatesList[i+1], specialCharPattern, "_") : string.Empty;

                                    if (yOrdinatesList[i+1] != null)
                                        columnToValidate = columnToValidate + Regex.Replace(yOrdinatesList[i+1], specialCharPattern, "_");

                                    Range enumerationRange = filterSheet.Range(filterSheet.Cells[1, validationListCount], filterSheet.Cells[lstFiedls.Count, validationListCount]);
                                    string validationListName = tv.TableCode.Replace('.', '_') + "_" + columnToValidate;
                                    enumerationRange.Name = validationListName;
                                    validationListName = "=" + validationListName;
                                   
                                    var index = i + 1;
                                    if (index != -1)
                                    {
                                        Range rangeColumn = tempSheet.Cells[5, index + 1];
                                        Range rangeColumnToEnd = rangeColumn.get_End(XlDirection.xlDown).get_End(XlDirection.xlDown);
                                        Range validationTableRange = tempSheet.Range(rangeColumn, rangeColumnToEnd);
                                        //Add validation, Error message and error titel
                                        validationTableRange.Validation.Add(XlDVType.xlValidateList, XlDVAlertStyle.xlValidAlertStop,
                                                     XlFormatConditionOperator.xlEqual, validationListName, Type.Missing);
                                        validationTableRange.Validation.ErrorMessage = "Please select values from the dropdown";
                                        validationTableRange.Validation.ErrorTitle = "Invalid data";
                                    }
                                    validationListCount = validationListCount + 1;
                                }
                            }
                        }                      
                        else
                        {
                            int pos = i + 1;

                            Range rangeColumn = tempSheet.Cells[5, pos+1];
                            Range rangeColumnToEnd = rangeColumn.get_End(XlDirection.xlDown).get_End(XlDirection.xlDown);
                            Range validationTableRange = tempSheet.Range(rangeColumn, rangeColumnToEnd);

                            switch (dataTypes[i].Trim().ToUpper())
                            {

                                case "BOOLEAN":
                                    List<string> boolValues = new List<string>();
                                    boolValues.Add("TRUE");
                                    boolValues.Add("FALSE");
                                    string boolValuesJoined = string.Join(",", boolValues.ToArray());
                                    validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateList, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                            NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlEqual, boolValuesJoined, Type.Missing);
                                    validationTableRange.Validation.ErrorMessage = "Please select the bool value";
                                    validationTableRange.Validation.ErrorTitle = "Invalid data";
                                    validationTableRange.Validation.IgnoreBlank = false;
                                    break;

                                case "INTEGER":
                                    validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateWholeNumber, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                            NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, int.MinValue, int.MaxValue);
                                    validationTableRange.Validation.ErrorMessage = "Please enter only the integer value";
                                    validationTableRange.Validation.ErrorTitle = "Invalid data";
                                    validationTableRange.Validation.IgnoreBlank = false;
                                    break;

                                case "PERCENTAGE":
                                   validationTableRange.ClearFormats();                                                                     
                                    validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                            NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                                    validationTableRange.Validation.ErrorMessage = "Please enter only the decimal value";
                                    validationTableRange.Validation.ErrorTitle = "Invalid data";
                                    validationTableRange.Validation.IgnoreBlank = false;
                                    validationTableRange.NumberFormat = "0.00%";
                                    break;

                                case "DECIMAL":
                                    validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                           NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                                    validationTableRange.Validation.ErrorMessage = "Please enter only the decimal value";
                                    validationTableRange.Validation.ErrorTitle = "Invalid data";
                                    validationTableRange.Validation.IgnoreBlank = false;
                                    validationTableRange.NumberFormat = "0.0000";
                                    break;

                                case "MONETARY":
                                    validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                            NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                                    validationTableRange.Validation.ErrorMessage = "Please enter only the decimal value";
                                    validationTableRange.Validation.ErrorTitle = "Invalid data";                                    
                                    validationTableRange.Validation.IgnoreBlank = false;
                                    validationTableRange.NumberFormat = "0.00";
                                    break;

                                case "DATE":
                                    validationTableRange.ClearFormats();
                                    validationTableRange.NumberFormat = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;                                   
                                    validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDate, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                            NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlGreaterEqual, DateTime.Parse("1/1/1900"), Type.Missing);                                    
                                    validationTableRange.Validation.ErrorMessage = "Please enter only the date value";
                                    validationTableRange.Validation.ErrorTitle = "Invalid data";
                                    validationTableRange.Validation.IgnoreBlank = false;
                                    break;

                                default:
                                    break;
                            }

                        }
                    }

                    //TEST

                    Range firstColumnRange = tempSheet.Cells[5, 2];
                    Range firstColumnRangeColumnToEnd = firstColumnRange.get_End(XlDirection.xlDown).get_End(XlDirection.xlDown);
                    Range firstColumnRangeTillEnd = tempSheet.Range(firstColumnRange, firstColumnRangeColumnToEnd);
                    firstColumnRangeTillEnd.Borders[XlBordersIndex.xlEdgeLeft].Weight = 4;


                    Range lastColumnRange = tempSheet.Cells[5, lstColumns.Count];
                    Range lastColumnRangeColumnToEnd = lastColumnRange.get_End(XlDirection.xlDown).get_End(XlDirection.xlDown);
                    Range lastColumnRangeTillEnd = tempSheet.Range(lastColumnRange, lastColumnRangeColumnToEnd);
                    lastColumnRangeTillEnd.Borders[XlBordersIndex.xlEdgeRight].Weight = 4;


                   

                    //lastColumnRangeTill.Borders.LineStyle = XlLineStyle.xlDouble;
                    //firstColumnRangeTill.Borders.LineStyle = XlLineStyle.xlContinuous;

                    dataTypes.Insert(0, string.Empty);
                    type.Value = dataTypes.ToArray();
                    Range setnamedVersionRange = tempSheet.Cells[3, 1];
                    setnamedVersionRange.Name = tv.TableCode.Trim() + "!" + tv.TableCode + ".V";
                    setnamedVersionRange.Value = "VER:" + versionNumber.Trim();
                    setnamedVersionRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateCustom, Type.Missing, Type.Missing, "\"\"");

                }

                FormatToc(tableContents, application.Sheets);

                //Remove the last sheet
                application.DisplayAlerts = false;

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
