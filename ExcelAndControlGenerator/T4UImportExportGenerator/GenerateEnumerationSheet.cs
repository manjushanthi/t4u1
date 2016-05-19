using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using NetOffice.ExcelApi;
using NetOffice.ExcelApi.Enums;

namespace T4UImportExportGenerator
{
    public class GenerateEnumerationSheet
    {

        public void GenerateEnumerationSheetFULL(string path)
        {

            NetOffice.ExcelApi.Application application = new NetOffice.ExcelApi.Application();
            application.Workbooks.Add();
            application.DisplayAlerts = false;

            Workbook newWb = null;
            newWb = application.ActiveWorkbook;

            //Enumeration sheet contains all the enumerations value
            Worksheet enumerationSheet = (Worksheet)application.ActiveSheet;
            enumerationSheet.Name = "Enumerations";
            application.Sheets.Add(enumerationSheet);

            //get the columns
            List<string> lstFiedlsStr = MemoryProfilerProject.TestHelper.GetAllHierarchyListWithMetrics(TemplateGeneration.DbPath);
            List<string> lstPageFiedlsStr = MemoryProfilerProject.TestHelper.GetAllPageColumnsWithMetrics(TemplateGeneration.DbPath);
            if (lstPageFiedlsStr != null)
                lstFiedlsStr.AddRange(lstPageFiedlsStr);

            if (lstFiedlsStr.Count > 0)
            {
                var row = enumerationSheet.Range(enumerationSheet.Cells[1, 1], enumerationSheet.Cells[1, lstFiedlsStr.Count]);


                row.Value = lstFiedlsStr.ToArray();
                int noOfItems = 1;
                int maxcount = 1;

                enumerationSheet.Columns.VerticalAlignment = NetOffice.ExcelApi.Enums.XlVAlign.xlVAlignCenter;
                enumerationSheet.Columns.HorizontalAlignment = NetOffice.ExcelApi.Enums.XlHAlign.xlHAlignCenter;
                enumerationSheet.Columns.AutoFit();
                enumerationSheet.Columns.ColumnWidth = 30;

                ////To freeze the Panes 
                enumerationSheet.Activate();
                enumerationSheet.Application.ActiveWindow.SplitRow = 1;
                enumerationSheet.Application.ActiveWindow.FreezePanes = true;

                foreach (string value in lstFiedlsStr)
                {
                    if (value.Trim().StartsWith("E:"))
                    {
                        //Extract the hierarchy id from the type information

                        string[] strhierarchyID = Regex.Split(value.Trim(), "E:");
                        int hierarchyID = 0;
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

                        Dictionary<string, string> hierarchyValues1 = MemoryProfilerProject.TestHelper.HierarchyLookupWithMetrics(TemplateGeneration.DbPath, hierarchyID, hierarchyStartingMemberID, isStartingMemberIncluded);
                        List<string> hierarchyValueslst = hierarchyValues1.Select(itm => itm.Key + "    {" + itm.Value + "}").ToList();

                        if (hierarchyValueslst.Count > 0)
                        {
                            Range addRange = enumerationSheet.Range(enumerationSheet.Cells[2, noOfItems], enumerationSheet.Cells[hierarchyValueslst.Count + 1, noOfItems]);
                            int rowCount = 0;
                            foreach (Range indivual_row in addRange.Rows)
                            {
                                indivual_row.Value = hierarchyValueslst[rowCount];
                                rowCount = rowCount + 1;
                            }
                            if (maxcount < hierarchyValueslst.Count)
                                maxcount = hierarchyValueslst.Count;
                        }
                        noOfItems = noOfItems + 1;
                    }

                    Range headerRange = enumerationSheet.Range(enumerationSheet.Cells[1, 1], enumerationSheet.Cells[1, lstFiedlsStr.Count()]);
                    Range valuesRange = enumerationSheet.Range(enumerationSheet.Cells[2, 1], enumerationSheet.Cells[maxcount + 1, lstFiedlsStr.Count()]);

                    valuesRange.Interior.Color = XlRgbColor.rgbAliceBlue;
                    valuesRange.Borders.LineStyle = XlLineStyle.xlDouble;
                    valuesRange.Borders.Weight = 2;
                    valuesRange.WrapText = true;

                    headerRange.Borders.LineStyle = XlLineStyle.xlDouble;
                    headerRange.Borders.Weight = 2;
                    headerRange.Interior.Color = XlRgbColor.rgbLightGrey;
                    headerRange.Font.Bold = true;

                }
            }

            //Remove the last sheet
            application.DisplayAlerts = false;

            if (File.Exists(Path.Combine(path, "EnumerationSheet.xlsx")))
                File.Delete(Path.Combine(path, "EnumerationSheet.xlsx"));

            if (newWb.Worksheets.Count > 1)
            {
                Worksheet ws = (Worksheet)newWb.Worksheets.ElementAt(0);
                if (ws != null)
                    ws.Delete();
            }
            application.DisplayAlerts = true;

            newWb.SaveAs(Path.Combine(path, "EnumerationSheet.xlsx"), Missing.Value, Missing.Value, Missing.Value, false,
                                         false, XlSaveAsAccessMode.xlExclusive);
            newWb.Close();

            application.Quit();
            application.Dispose();

        }


        public void GenerateES()
        {

            NetOffice.ExcelApi.Application application = new NetOffice.ExcelApi.Application();
            application.Workbooks.Add();
            application.DisplayAlerts = false;

            Workbook newWb = null;
            newWb = application.ActiveWorkbook;

            //Enumeration sheet contains all the enumerations value
            Worksheet filterSheet = (Worksheet)application.ActiveSheet;
            filterSheet.Name = "Enumerations";
            application.Sheets.Add(filterSheet);

            //get the columns
            List<int> lstFiedls = MemoryProfilerProject.TestHelper.GetAllHierarchy(TemplateGeneration.DbPath);
            List<string> lstFiedlsStr = new List<string>();


            if (lstFiedls.Count > 0)
            {
                var row = filterSheet.Range(filterSheet.Cells[1, 1], filterSheet.Cells[1, lstFiedls.Count]);

                foreach (int enu in lstFiedls)
                    lstFiedlsStr.Add(string.Concat("E:", enu));
                row.Value = lstFiedlsStr.ToArray();
                int noOfItems = 1;
                int maxcount = 1;

                filterSheet.Columns.VerticalAlignment = NetOffice.ExcelApi.Enums.XlVAlign.xlVAlignCenter;
                filterSheet.Columns.HorizontalAlignment = NetOffice.ExcelApi.Enums.XlHAlign.xlHAlignCenter;
                filterSheet.Columns.AutoFit();
                filterSheet.Columns.ColumnWidth = 30;


                ////To freeze the Panes 
                filterSheet.Activate();
                filterSheet.Application.ActiveWindow.SplitRow = 1;
                filterSheet.Application.ActiveWindow.FreezePanes = true;

                foreach (int value in lstFiedls)
                {
                    Dictionary<string, string> hierarchyValues1 = MemoryProfilerProject.TestHelper.HierarchyLookupWithName(TemplateGeneration.DbPath, value);
                    List<string> lstFiedlstest = hierarchyValues1.Select(itm => itm.Value).ToList();

                    if (lstFiedlstest.Count > 0)
                    {
                        Range addRange = filterSheet.Range(filterSheet.Cells[2, noOfItems], filterSheet.Cells[lstFiedlstest.Count + 1, noOfItems]);
                        int rowCount = 0;
                        foreach (Range indivual_row in addRange.Rows)
                        {
                            indivual_row.Value = lstFiedlstest[rowCount];
                            rowCount = rowCount + 1;
                        }
                        if (maxcount < lstFiedlstest.Count)
                            maxcount = lstFiedlstest.Count;
                    }
                    noOfItems = noOfItems + 1;
                }

                Range headerRange = filterSheet.Range(filterSheet.Cells[1, 1], filterSheet.Cells[1, lstFiedls.Count()]);
                Range valuesRange = filterSheet.Range(filterSheet.Cells[2, 1], filterSheet.Cells[maxcount + 1, lstFiedls.Count()]);

                valuesRange.Interior.Color = XlRgbColor.rgbAliceBlue;
                valuesRange.Borders.LineStyle = XlLineStyle.xlDouble;
                valuesRange.Borders.Weight = 2;
                valuesRange.WrapText = true;

                headerRange.Borders.LineStyle = XlLineStyle.xlDouble;
                headerRange.Borders.Weight = 2;
                headerRange.Interior.Color = XlRgbColor.rgbLightGrey;
                headerRange.Font.Bold = true;

            }

            newWb.SaveAs("D:\\App\\test.xlsx", Missing.Value, Missing.Value, Missing.Value, false,
                                         false, XlSaveAsAccessMode.xlExclusive);
            newWb.Close();


            application.Quit();
            application.Dispose();

        }
    }
}
