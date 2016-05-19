using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetOffice.ExcelApi;
using System.Reflection;
using NetOffice.ExcelApi.Enums;
using ExcelIns = NetOffice.ExcelApi;
using AT2DPM.DAL.Model;
using AT2DPM.DAL;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
//using AT2DPM.Excel.UI;


namespace T4UImportExportGenerator.DialogBox
{
    public partial class ExcelTemplateGenerationCheckbox : Form
    {

        #region Properties



        #endregion

        #region Events

        public ExcelTemplateGenerationCheckbox()
        {
            InitializeComponent();
            chkModules.ColumnWidth = 300;

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.SuspendLayout();

            var modules = from pro in TemplateGeneration.DpmContext.mModules
                          select new { pro.ModuleID, pro.ModuleLabel };
            Dictionary<long, string> dic = new Dictionary<long, string>();
            foreach (var module in modules)
            {

                dic.Add(module.ModuleID, module.ModuleLabel);
            }
            foreach (KeyValuePair<long, string> entry in dic)
            {
                chkModules.Items.Add(entry);
            }

            this.ResumeLayout();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
          /*  Dictionary<string, long> tables = new Dictionary<string, long>();
            int validationListCount = 1;
            foreach (var item in chkModules.CheckedItems)
            {
                long moduleId = Convert.ToInt64(((KeyValuePair<long, string>)item).Key);
                if (!string.IsNullOrEmpty(TemplateGeneration.DbPath))
                    tables = MemoryProfilerProject.Program.ExcelTemplateGeneration(TemplateGeneration.DbPath, moduleId);



                NetOffice.ExcelApi.Application application = new NetOffice.ExcelApi.Application();
                application.Workbooks.Add();
                application.DisplayAlerts = false;

                Workbook newWb = null;
                newWb = application.ActiveWorkbook;

                Worksheet filterSheet = (ExcelIns.Worksheet)application.ActiveSheet;
                filterSheet.Name = "CRT_Filters";
                application.Sheets.Add(filterSheet);

                foreach (KeyValuePair<string, long> entry in tables)
                {

                    //if (entry.Key != "S.16.01.01.01") //TODO its just for test need to remove S.02.01.03.01,S.01.02.01.01,S.02.02.01.01 ,  OPEN S.06.03.02.01 S.16.01.01.01  S.16.01.01.01
                    //continue;

                    mTable tv = TemplateGeneration.DpmContext.mTables.FirstOrDefault(x => x.TableID == entry.Value);
                    if (tv != null)
                    {
                        List<string> yOrdinatesList = new List<string>();
                        List<string> xOrdinatesList = new List<string>();
                        List<string> zOrdinatesList = new List<string>();
                        List<string> PageOrdinatesList = new List<string>();
                        List<string> lstColumns = new List<string>();

                        xOrdinatesList.Add("Column Header");
                        yOrdinatesList.Add("Row Header");

                        ClassicRelationalModelGenerator.ExcelTemplateProcessor.ExcelTemplateColumns obj = MemoryProfilerProject.Program.getColumns(TemplateGeneration.DbPath, tv);
                        HashSet<mAxisOrdinate> xOrdinates = obj.xOrdinates;
                        HashSet<mAxisOrdinate> yOrdinates = obj.yOrdinates;
                        HashSet<mAxisOrdinate> zOrdinates = obj.zOrdinates;
                        HashSet<mAxi> openZaxes = obj.openZaxes;
                        lstColumns = obj.columns;
                        List<Tuple<string, string, long, long>> rowAndColumns = obj.RowsAndColumns;
                        List<string> dataTypes = obj.DataTypes;

                        foreach (var s in rowAndColumns)
                        {
                            xOrdinatesList.Add(s.Item1);
                            yOrdinatesList.Add(s.Item2);
                        }

                        lstColumns.Insert(0, "Code");

                        foreach (object zOrdinate in (IEnumerable)zOrdinates)
                        {
                            string OrdinateLabel = ((mAxisOrdinate)zOrdinate).OrdinateLabel;
                            zOrdinatesList.Add(OrdinateLabel);
                        }
                        foreach (object Zaxes in (IEnumerable)openZaxes)
                        {
                            string OrdinateLabel = ((mAxi)Zaxes).AxisLabel;
                            PageOrdinatesList.Add(OrdinateLabel);
                        }

                        Worksheet tempSheet = (ExcelIns.Worksheet)application.ActiveSheet;
                        tempSheet.Name = entry.Key;

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

                        string sheetRangeName = entry.Key;
                        sheetRangeName = sheetRangeName.Replace(".", "_");

                        Range SetnamedHeaderRange = tempSheet.Range(tempSheet.Cells[1, 1], tempSheet.Cells[4, lstColumns.Count]);
                        SetnamedHeaderRange.Name = sheetRangeName;
                        SetnamedHeaderRange.Interior.Color = XlRgbColor.rgbLightGrey;
                        SetnamedHeaderRange.Borders.LineStyle = XlLineStyle.xlDouble;
                        SetnamedHeaderRange.Borders.Weight = 2;
                        SetnamedHeaderRange.Borders.Color = Color.Black;
                        SetnamedHeaderRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateCustom, Type.Missing, Type.Missing, "\"\"");

                        for (int i = 0; i <= dataTypes.Count - 1; i++)
                        {
                            if (i >= rowAndColumns.Count) // TODO : Need Page details here to fix 
                                break;

                            var s = rowAndColumns[i];
                            long val1 = s.Item3;
                            long val2 = s.Item4;
                            string xVal = s.Item1;
                            string yVal = s.Item2;

                            if (dataTypes[i] == "Enumeration")
                            {
                                int HierarchyID = MemoryProfilerProject.TestHelper.GetOrdinateHierarchyID_MD(TemplateGeneration.DbPath, val1);
                                if (HierarchyID == 0) HierarchyID = MemoryProfilerProject.TestHelper.GetOrdinateHierarchyID_HD(TemplateGeneration.DbPath, val1);
                                if (HierarchyID == 0) HierarchyID = MemoryProfilerProject.TestHelper.GetOrdinateHierarchyID_MD(TemplateGeneration.DbPath, val2);
                                if (HierarchyID == 0) HierarchyID = MemoryProfilerProject.TestHelper.GetOrdinateHierarchyID_HD(TemplateGeneration.DbPath, val2);

                                if (HierarchyID != 0)
                                {
                                    string Hierarchycode = MemoryProfilerProject.TestHelper.GetHierarchycode(TemplateGeneration.DbPath, HierarchyID);
                                    dataTypes[i] = "E:" + Hierarchycode;
                                    Dictionary<string, string> test = MemoryProfilerProject.TestHelper.HierarchyLookup2(TemplateGeneration.DbPath, HierarchyID);
                                    List<string> lstFiedls;
                                    lstFiedls = test.Select(itm => itm.Value).ToList();


                                    Range AddRange = filterSheet.Range(filterSheet.Cells[1, validationListCount], filterSheet.Cells[lstFiedls.Count, validationListCount]);
                                    int rowCount = 0;
                                    foreach (Range indivual_row in AddRange.Rows)
                                    {
                                        indivual_row.Value = lstFiedls[rowCount];
                                        rowCount = rowCount + 1;

                                    }

                                    string columnToValidate = Regex.Replace(xVal, "[^a-zA-Z0-9_]+", "_");
                                    columnToValidate = columnToValidate + Regex.Replace(yVal, "[^a-zA-Z0-9_]+", "_");

                                    Range SetnamedRange = filterSheet.Range(filterSheet.Cells[1, validationListCount], filterSheet.Cells[lstFiedls.Count, validationListCount]);
                                    string validationListName = sheetRangeName + "_" + columnToValidate;
                                    SetnamedRange.Name = validationListName;
                                    validationListName = "=" + validationListName;

                                    var index = i + 1;

                                    if (index != -1)
                                    {
                                        Range rangeColumn = tempSheet.Cells[5, index + 1];
                                        Range rangeColumnToEnd = rangeColumn.get_End(XlDirection.xlDown).get_End(XlDirection.xlDown);
                                        Range validationTableRange = tempSheet.Range(rangeColumn, rangeColumnToEnd);
                                        validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateList, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                                     NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlEqual, validationListName, Type.Missing);
                                        validationTableRange.Validation.ErrorMessage = "Please select values from the dropdown";
                                        validationTableRange.Validation.ErrorTitle = "Invalid data";
                                    }
                                    validationListCount = validationListCount + 1;
                                }

                            }
                            //
                            else
                            {

                                int pos = i + 1;

                                Range rangeColumn = tempSheet.Cells[5, pos];
                                Range rangeColumnToEnd = rangeColumn.get_End(XlDirection.xlDown).get_End(XlDirection.xlDown);
                                Range validationTableRange = tempSheet.Range(rangeColumn, rangeColumnToEnd);

                                switch (dataTypes[i])
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
                                        validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                                NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                                        validationTableRange.Validation.ErrorMessage = "Please enter only the decimal value";
                                        validationTableRange.Validation.ErrorTitle = "Invalid data";
                                        validationTableRange.Validation.IgnoreBlank = false;
                                        break;
                                    case "DECIMAL":
                                        validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                               NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                                        validationTableRange.Validation.ErrorMessage = "Please enter only the decimal value";
                                        validationTableRange.Validation.ErrorTitle = "Invalid data";
                                        validationTableRange.Validation.IgnoreBlank = false;
                                        break;
                                    case "MONETARY":
                                        validationTableRange.Validation.Add(NetOffice.ExcelApi.Enums.XlDVType.xlValidateDecimal, NetOffice.ExcelApi.Enums.XlDVAlertStyle.xlValidAlertStop,
                                                NetOffice.ExcelApi.Enums.XlFormatConditionOperator.xlBetween, decimal.MinValue, decimal.MaxValue);
                                        validationTableRange.Validation.ErrorMessage = "Please enter only the decimal value";
                                        validationTableRange.Validation.ErrorTitle = "Invalid data";
                                        validationTableRange.Validation.IgnoreBlank = false;
                                        break;
                                    case "DATE":
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

                        dataTypes.Insert(0, "Type");
                        type.Value = dataTypes.ToArray();
                    }
                }

                filterSheet.Columns.WrapText = true;
                filterSheet.Columns.ColumnWidth = 15;


                application.ActiveWorkbook.SaveAs("D:\\ExcelTemplate", Missing.Value, Missing.Value, Missing.Value, false,
                                            false, XlSaveAsAccessMode.xlExclusive);
                application.ActiveWorkbook.Close();
                application.Quit();
                application.Dispose();
            }



            this.Close();
            */
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < chkModules.Items.Count; i++)
                {
                    chkModules.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < chkModules.Items.Count; i++)
                {
                    chkModules.SetItemChecked(i, false);
                }
            }

        }

        #endregion

        #region Methods
        /*

        public string GetHierarchycode(int hierarchyId)
        {
       
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Hierarchycode FROM mHierarchy ");
            sb.Append("WHERE ");
            sb.Append(string.Format("HierarchyID = {0};", hierarchyId));

            string memberCode = Convert.ToString(ExcelApplication.SqliteConnStr.getColumnType(sb.ToString()));
            //if (!string.IsNullOrEmpty(memberCode) && memberCode.Length >= 1)
            //    return memberCode.ToUpper();
            return memberCode;
        }

        public string GetOrdinateType(long ordinateId)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT met.DataType FROM mOrdinateCategorisation oc ");
            sb.Append("LEFT OUTER JOIN mMember m ON ( m.MemberID = oc.MemberID ) ");
            sb.Append("INNER JOIN mMetric met ON ( met.CorrespondingMemberID = m.MemberID ) ");
            sb.Append("WHERE (oc.Source NOT LIKE 'HD' OR oc.Source IS NULL) AND ");
            sb.Append(string.Format("OrdinateID = {0};", ordinateId));

            //string query = string.Format("SELECT MemberCode FROM vwGetOrdinateType WHERE OrdinateID = {0} limit 1", ordinateId);
            string memberCode = Convert.ToString(ExcelApplication.SqliteConnStr.getColumnType(sb.ToString()));
            if (!string.IsNullOrEmpty(memberCode) && memberCode.Length >= 1)
                return memberCode.ToUpper();
            return "STRING"; // String - default
        }
     
        public IEnumerable<mAxisOrdinate> GetTableAxisOrdinateColumns_Int(int tableVid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * from vwGetTableAxisOrdinateColumns ");
            sb.Append(string.Format("WHERE TableID = {0} ", tableVid));
            sb.Append("Order by IsRowKey Desc,TableAxisOrder, OrdinateOrder ");
            return ConvertTomAxisOrdinates(ExcelApplication.SqliteConnStr.executeQuery(sb.ToString()));
        }

        private IEnumerable<mAxisOrdinate> ConvertTomAxisOrdinates(System.Data.DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new mAxisOrdinate
                {
                    OrdinateID = Convert.ToInt64(row["OrdinateID"]),
                    OrdinateLabel = row["OrdinateLabel"].ToString(),
                    OrdinateCode = row["OrdinateCode"].ToString(),

                };
            }

        }

        public int GetOrdinateHierarchyID_MD(long ordinateID)
        {
            int result = ExcelApplication.SqliteConnStr.executeScalar(string.Format("Select ReferencedHierarchyID from vwGetOrdinateHierarchyID_MD Where OrdinateID = {0} ", ordinateID));
            return result;
        }

        public int GetOrdinateHierarchyID_HD(long ordinateID)
        {
            try
            {
                int result = ExcelApplication.SqliteConnStr.executeScalar(string.Format("Select HierarchyID from vwGetOrdinateHierarchyID_HD Where OrdinateID = {0} ", ordinateID));
                return result;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 0;
            }

        }

        public Dictionary<string, string> HierarchyLookup2(int hierarchyID)
        {
            // Here we are gathering all the info required to save details for this MemberCode
            Dictionary<string, string> values = new Dictionary<string, string>();
            return ConvertToDictionary(ExcelApplication.SqliteConnStr.executeQuery(string.Format("SELECT Distinct [Inner] as Text, Name, IsAbstract FROM [vwHierarchyLookupMetrics] Where HierarchyID = {0} Order by MetricID, HierarchyID, HierarchyOrder ", hierarchyID)));
        }

        private Dictionary<string, string> ConvertToDictionary(System.Data.DataTable dataTable)
        {
            Dictionary<string, string> val = new Dictionary<string, string>();
            foreach (DataRow row in dataTable.Rows)
            {
                val.Add(row["Name"].ToString(), row["Text"].ToString());

            }
            return (val);

        }*/
        #endregion


    }
}
