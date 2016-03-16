using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetOffice.ExcelApi;



using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Data.Shared;
using SolvencyII.Data.SQLite;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Exceptions;
using SolvencyII.ExcelImportExportLib.UI.Dialog;
using SolvencyII.ExcelImportExportLib.Utils;
using SolvencyII.ExcelImportExportLib.Extract;
using System.Reflection;
using NetOffice.ExcelApi.Enums;
using SolvencyII.Domain.Entities;
using System.Drawing;
using System.Security.Cryptography;

namespace SolvencyII.ExcelImportExportLib
{
    public static class InvokeExcel
    {
        public static string excelTemplateVersion = string.Empty;
        public static string currentExcelSheetVersion = string.Empty;

        public static void ImportFromExcel(string connectionString, long instanceID, string supportedExcelTemplateVersion, ExcelTemplateType _type)
        {
            //Version management
            //Get the excel template Version number

            int index = supportedExcelTemplateVersion.IndexOf(":") + 1;
            excelTemplateVersion = supportedExcelTemplateVersion.Substring(index);


            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            string destFile = dialog.FileName;

            //Connect to an Excel file
            IExcelConnection excelConnection = new ExcelConnection(destFile);
            ISolvencyData dpmConn = null;

            try
            {
                //Open the database connection
                dpmConn = new SQLiteConnection(connectionString);
                //int count = dpmConn.ExecuteScalar<int>("Select count(*) from mTable");

                excelConnection.OpenConnection();

                //Get the import implementor from the factory
                IExcelImport importExcel = TemplateFactory.GetExcelImport(_type);

                string[] tableCodes = importExcel.GetTableCodes(excelConnection);

                //Get the instance from dpm
                GetSQLData sqlData = new GetSQLData(connectionString);
                dInstance instance = sqlData.GetInstanceDetails(instanceID);

                mModule module = sqlData.GetModuleDetails(instance.ModuleID);

                if (module == null)
                    throw new T4UExcelImportExportException(LanguageLabels.GetLabel(146, "An error occured. The process could not found the active module"), null);

                string[] instanceTableCodes = GetInstanceTableCodes(dpmConn, instance.InstanceID, module.ModuleID);

                //Select only those table codes which are in the current instance
                string[] filteredTableCodes = (from x in tableCodes
                                               from y in instanceTableCodes
                                               where x.ToUpper() == y.ToUpper()
                                               select y).ToArray<string>();

                //Select only those table codes which are in the current instance
                string[] otherTableCodes = (from x in tableCodes
                                            where !filteredTableCodes.Contains(x)
                                            select x).ToArray<string>();


                if (filteredTableCodes.Count() == 0)
                {
                    MessageBox.Show(LanguageLabels.GetLabel(144, "There are no tables found in the excel file that match with the current report.\n Please verify the file and import again."), "Import error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                /*TablesListDlg dlg = new TablesListDlg(filteredTableCodes);
                dlg.ShowDialog();*/

                TablesListDlg3 dlg = new TablesListDlg3(filteredTableCodes, otherTableCodes);
                dlg.ShowDialog();

                if (dlg.DialogResult == DialogResult.Cancel)
                    return;

                if (dlg.SelectedTableCodes != null)
                {
                    if (dlg.SelectedTableCodes.Count() > 0)
                    {
                        StatusDlg statusDlg = new StatusDlg(ImportExportBehaviour.Importing);
                        statusDlg.Text = "Importing...";

                        //Subscribe to events
                        importExcel.ImportFromFileCompleted += statusDlg.CompletedHandler;
                        importExcel.ImportFromFileProgressChanged += statusDlg.ProgressChangedHandler;
                        importExcel.ImportFromFileGranuleProgressChanged += statusDlg.GranuleProgressChangedHandler;
                        statusDlg.Canceled += importExcel.CancelOperation;
                        ExcelConnection excelConn = (ExcelConnection)excelConnection;

                        //To check the excel template Version compatibility
                        if (_type == ExcelTemplateType.BasicTemplate)
                        {
                            if (!CheckExcelTemplateVersion(excelConn, dlg.SelectedTableCodes))
                                return;
                        }
                        else if (_type == ExcelTemplateType.BusinessTemplate)
                        {
                            Worksheet ws = (Worksheet)excelConn.WorkbookSheets["Info"];

                            if (ws == null)
                            {
                                MessageBox.Show("The selected excel file is not a Business Template",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            Range headerRange = null;
                            ExtractData extract = new ExtractData();

                            //Look for 'Version' range
                            headerRange = extract.FindRange(ws.Names, "Version");

                            //Extract header data from the range
                            string[,] headerData = extract.ExtractDataFromRange(ws, headerRange);
                            bool result = headerData == null ? IsSupportedExcelTemplateVersion(null) : IsSupportedExcelTemplateVersion(headerData[2, 1]);
                            if (result == false)
                                return;
                        }

                        //Call the import function
                        importExcel.ImportFromFileAsync(ImportExportBehaviour.Importing, dpmConn, excelConnection, instance, dlg.SelectedTableCodes, currentExcelSheetVersion);
                        //importExcel.ImportFromFile(ImportExportBehaviour.Importing, dpmConn, excelConnection, instance, dlg.SelectedTableCodes);

                        //Show the status dialog
                        statusDlg.ShowDialog();
                    }
                }

            }
            catch (T4UExcelImportExportException te)
            {
                MessageBox.Show(te.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close the excel and database connection
                if (excelConnection != null)
                    excelConnection.CloseConnection();

                if (dpmConn != null)
                    dpmConn.Close();
            }
        }

        private static string GetExcelFilename(mModule module)
        {
            return module.ModuleCode + "_" + module.ModuleLabel.Trim().ToLower().Replace(' ', '_') + ".xlsx";
        }


        private static string[] GetInstanceTableCodes(ISolvencyData dpmConn, long instanceID, long moduleID)
        {
            string query = string.Format("SELECT distinct i.InstanceID, td.* FROM vwGetTreeData td left outer join dInstance i on (i.ModuleID = td.ModuleID) Where InstanceID = {0} ORDER BY BusinessOrder, TemplateOrder, TemplateOrder2 ", instanceID);

            List<TreeViewData> response = dpmConn.Query<TreeViewData>(query);

            return (from t in response
                    where t.ModuleID == moduleID
                    select t.TableCode).ToArray<string>();
        }

        public static void ExportToExcel(string connectionString, long instanceID, string excelTemplatePath, ExcelTemplateType _type, string businessTemplatePath="")
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel files (*.xlsx)|*.xlsx";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            string destFile = dialog.FileName;

            IExcelConnection excelConnection = null;
            ISolvencyData dpmConn = null;

            try
            {
                //Open the database connection
                dpmConn = new SQLiteConnection(connectionString);
                //int count = dpmConn.ExecuteScalar<int>("Select count(*) from mTable");


                dInstance instance;

                //Get the instance from dpm
                GetSQLData sqlData = new GetSQLData(connectionString);
                instance = sqlData.GetInstanceDetails(instanceID);

                mModule module = sqlData.GetModuleDetails(instance.ModuleID);

                if (module == null)
                    throw new T4UExcelImportExportException("An error occured. The process could not found the active module", null);


                TableInfo tableInfo = new TableInfo();
                //get the whole list of table codes from database

                string query = string.Format("SELECT distinct i.InstanceID, td.* FROM vwGetTreeData td left outer join dInstance i on (i.ModuleID = td.ModuleID) Where InstanceID = {0} ORDER BY BusinessOrder, TemplateOrder, TemplateOrder2 ", instanceID);

                List<TreeViewData> response = dpmConn.Query<TreeViewData>(query);

                string[] tableCodes = (from t in response
                                       where t.ModuleID == instance.ModuleID
                                       select t.TableCode).ToArray<string>();

                IList<string> emptyTables = new List<string>();
                IList<string> nonEmptyTable = new List<string>();

                foreach (string s in tableCodes)
                {
                    //Get the table information from database
                    mTable table = (tableInfo.GetTable(dpmConn, s)).FirstOrDefault();

                    mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(dpmConn, 1)).FirstOrDefault();
                    string tableName = Helper.GetTableName(taxonomy, table);

                    string queryCount = string.Format(" select count(*) from {0} where instance = {1}", tableName, instance.InstanceID);

                    int count = dpmConn.ExecuteScalar<int>(queryCount);

                    if (count > 0)
                        nonEmptyTable.Add(s);
                    else
                        emptyTables.Add(s);

                }

                //Show the empty and non empty tables to user to select
                TablesListDlg2 dlg = new TablesListDlg2(nonEmptyTable.ToArray<string>(), emptyTables.ToArray<string>());
                dlg.ShowDialog();

                if (dlg.DialogResult == DialogResult.Cancel)
                    return;


                //Copy the template file dlg.SelectedAll  !dlg.SelectedAll
                if (dlg.SelectedTableCodes != null)
                {
                    if (dlg.SelectedTableCodes.Length > 0)
                    {
                        //Connect to an Excel file

                        string fileName = GetExcelFilename(module);
                        string sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, excelTemplatePath + fileName);
                        //Check if the template file exists
                        if (!File.Exists(sourceFile))
                        {
                            MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(91, "Unfortunately a file is missing:"), sourceFile));
                            return;
                        }
                        //Delete the file if the file exists in destination folder
                        if (File.Exists(destFile))
                        {
                            File.Delete(destFile);
                        }

                        File.Copy(sourceFile, destFile);
                        excelConnection = new ExcelConnection(destFile);
                        excelConnection.OpenConnection();

                        ExcelConnection conn = (ExcelConnection)excelConnection;

                        //set the flag so that it will delete the worksheet
                        conn.ExcelApp.DisplayAlerts = false;

                        if (!dlg.SelectedAll)
                        {
                            //Remove all sheets in template which is not in user selection

                            foreach (string s in tableCodes /*(tableInfo.GetAllTables(dpmConn)).Select(t => t.TableCode)*/)
                            {
                                if (!dlg.SelectedTableCodes.Contains<string>(s))
                                {
                                    try
                                    {
                                        Worksheet ws = (Worksheet)conn.WorkbookSheets[s];
                                        if (ws != null)
                                        {
                                            ws.Delete();
                                        }

                                        ws.DisposeChildInstances();
                                        ws.Dispose();
                                        ws = null;
                                    }
                                    catch (Exception) { continue; }
                                }
                            }

                            //Remove the table of contents list for those tables that are not selected
                            try
                            {
                                Worksheet ws = (Worksheet)conn.WorkbookSheets["Table of contents"];
                                
                                if (ws != null)
                                {
                                    ws.Unprotect();

                                    int count = 3;
                                    do
                                    {
                                        string s = ws.Cells[count, 3].Value.ToString();

                                        if (!dlg.SelectedTableCodes.Contains<string>(s))
                                        {
                                            ws.Rows[count].Delete();
                                        }
                                        else
                                            count++;

                                    } while (ws.Cells[count, 3].Value != null);

                                    for (int i = 1; i <= count-3; i++)
                                        ws.Cells[i + 2, 2].Value = i.ToString();

                                }
                            }
                            catch(Exception ex)
                            {

                            }
                        }

                        conn.ExcelApp.DisplayAlerts = false;
                        //Save the workbook
                        conn.TemplateWorkbook.Save();



                        //Close the connection and open again
                        excelConnection.CloseConnection();

                        excelConnection = new ExcelConnection(destFile);
                        excelConnection.OpenConnection();


                        //Initialize to export the data from dpm
                        List<ExcelExportValidationMessage> validationMessages = new List<ExcelExportValidationMessage>();

                        //Get the export implementor from the factory
                        IExcelExport exportExcel = TemplateFactory.GetExcelExport(_type, validationMessages);  //new ExcelBasicTemplateExportImpl(validationMessages);

                        StatusDlg statusDlg = new StatusDlg(ImportExportBehaviour.Exporting);
                        statusDlg.Text = "Exporting...";
                        exportExcel.ExportToFileCompleted += statusDlg.CompletedHandler;
                        exportExcel.ExportToFileProgressChanged += statusDlg.ProgressChangedHandler;
                        exportExcel.ExportToFileGranuleProgressChanged += statusDlg.GranuleProgressChangedHandler;
                        statusDlg.Canceled += exportExcel.CancelOperation;

                        //Call the asynchronous call to export the data
                        exportExcel.ExportToFileAsync(ImportExportBehaviour.Exporting, dpmConn, excelConnection, instance, dlg.SelectedTableCodes);


                        //Show the status dialog
                        statusDlg.ShowDialog();

                        excelConnection.CloseConnection();
                        dpmConn.Close();
                        excelConnection = null;
                        dpmConn = null;

                        if (validationMessages != null)
                        {
                            if (validationMessages.Count > 0)
                            {
                                MessageBox.Show("The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ValidationDialog validationDialog = new ValidationDialog(validationMessages);
                                validationDialog.ShowDialog();
                            }
                        }

                        //Open the excel file in its own process
                        if (statusDlg.OpenExcelFile && _type != ExcelTemplateType.BusinessTemplate_Macro)
                        {
                            OpenMicrosoftExcel(destFile);
                        }

                        if(_type == ExcelTemplateType.BusinessTemplate_Macro)
                        {
                            //Code implementation to copy the Business excel 
                            if (!string.IsNullOrEmpty(businessTemplatePath))
                            {
                                const string keyToEncrypt = "HR$2pIjHR$2pIj12";
                                string sourceEncryptedFilepath = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Concat("Exl-Business_Encrypted\\", "Exl-Business_Encrypted.xlsx"));
                                System.Console.WriteLine(sourceEncryptedFilepath);
                                string sourceDecryptedFilepath = Path.Combine(System.Windows.Forms.Application.StartupPath, string.Concat("Exl-Business_Encrypted\\", "Excel-Business-Templates-(1.5.2.c).xlsm"));
                                System.Console.WriteLine(sourceDecryptedFilepath);
                                DecryptFile(sourceEncryptedFilepath, sourceDecryptedFilepath, keyToEncrypt);

                                string destPath = Path.Combine(Path.GetDirectoryName(destFile), "Excel-Business-Templates-(1.5.2.c).xlsm");

                                //string destPath = Path.Combine(Path.GetDirectoryName(destFile), Path.GetFileName(BusinessTemplatePath));

                                if (File.Exists(sourceDecryptedFilepath))
                                {
                                    if (File.Exists(destPath))
                                        File.Delete(destPath);
                                    File.Copy(sourceDecryptedFilepath, destPath);

                                    if (statusDlg.OpenExcelFile)
                                    {
                                        OpenMicrosoftExcel(destPath);
                                    }
                                }
                            }

                        }

                    }
                }


            }
            catch (T4UExcelImportExportException te)
            {
                MessageBox.Show(te.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close the excel and database connection
                if (excelConnection != null)
                    excelConnection.CloseConnection();

                if (dpmConn != null)
                    dpmConn.Close();
            }

        }


        /// <summary>
        /// Open specified word document.
        /// </summary>
        private static void OpenMicrosoftExcel(string file)
        {

            Process.Start(file);
        }

        public static void DownloadTemplate(string connectionString, long instanceID, string excelTemplatePath)
        {
            try
            {
                //Open the database connection
                ISolvencyData dpmConn = new SQLiteConnection(connectionString);
                //int count = dpmConn.ExecuteScalar<int>("Select count(*) from mTable");


                //Get the modules from dpm
                //IList<mModule> modules = dpmConn.Query<mModule>("select * from mModule ");

                //Get the instance from dpm
                GetSQLData sqlData = new GetSQLData(connectionString);
                dInstance instance = sqlData.GetInstanceDetails(instanceID);

                mModule module = sqlData.GetModuleDetails(instance.ModuleID);

                //Show the list of dialog box to the user
                //Show the empty and non empty tables to user to select
                //TablesListDlg dlg = new TablesListDlg( modules.Select(t=> t.ModuleLabel).ToArray<string>());

                //if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                // return;


                if (module == null)
                    throw new T4UExcelImportExportException("An error occured. The process could not found the active module", null);

                string fileName;
                string sourceFile;
                string destFile;

                //Get the destination folder where a file has to be downloaded
                //FolderBrowserDialog dialog = new FolderBrowserDialog();


                //if (dialog.ShowDialog() == DialogResult.Cancel)
                //  return;



                /*foreach (mModule module in (from m in modules
                                                from s in dlg.SelectedTableCodes
                                                where m.ModuleLabel.ToUpper() == s.ToUpper()
                                                select m))
                {*/

                fileName = GetExcelFilename(module);

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                dialog.FileName = fileName;

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, excelTemplatePath + fileName);
                destFile = dialog.FileName;


                //Check if the template file exists
                if (!File.Exists(sourceFile))
                {
                    MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(91, "Unfortunately a file is missing:"), sourceFile));
                    return;
                }


                //Delete the file if the file exists in destination folder
                if (File.Exists(destFile))
                {
                    /*if (MessageBox.Show(string.Format("The file {0} already existis in the folder {1}. Do you want to over write the file?", fileName, dialog.FileName), "File exists",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }*/

                    File.Delete(destFile);
                }

                File.Copy(sourceFile, destFile);
                //}

                MessageBox.Show("Download complete.", "Completed.", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (T4UExcelImportExportException te)
            {
                MessageBox.Show(te.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close the excel and database connection
                //excelConnection.CloseConnection();
                //dpmConn.Close();
            }
        }

        public static bool IsTemplateFileExits(string connectionString, long instanceID, string excelTemplatePath)
        {
            mModule module;
            using (GetSQLData sqlData = new GetSQLData(connectionString))
            {
                dInstance instance = sqlData.GetInstanceDetails(instanceID);
                if (instance == null)
                    return false;
                module = sqlData.GetModuleDetails(instance.ModuleID);
            }
            if (module == null)
                return false;
            string fileName = GetExcelFilename(module);
            string sourceFile = Path.Combine(System.Windows.Forms.Application.StartupPath, excelTemplatePath + fileName);
            if (!File.Exists(sourceFile))
            {
                return false;
            }
            return true;
        }

        public static bool IsSupportedExcelTemplateVersion(string excelSheetVersion)
        {
            Version ver = new Version(excelTemplateVersion);

            if (string.IsNullOrEmpty(excelSheetVersion))
            {
                MessageBox.Show(string.Format("This excel template was created with previous version. Please import the values with the updated excel template (migration is not currently supported). Supported Template version: {0} current Template Version: no template version found", ver));
                return false;
            }
            int index = excelSheetVersion.IndexOf(":") + 1;
            excelSheetVersion = excelSheetVersion.Substring(index);
            //for the Preparatory issue fix - text to percentage
            currentExcelSheetVersion = excelSheetVersion;

            if ((ver.CompareTo(new Version(excelSheetVersion)) > 0))
            {
                MessageBox.Show(string.Format("This excel template was created with previous version. Please import the values with the updated excel template (migration is not currently supported). Supported Template version: {0} current Template Version: {1}", ver, excelSheetVersion));
                return false;
            }
            return true;

        }

        public static bool CheckExcelTemplateVersion(ExcelConnection excelConn, string[] selectedExcelSheets)
        {

            Worksheet ws = null;
            Names names = null;
            Range headerRange = null;
            ExtractData extract = new ExtractData();

            foreach (string s in selectedExcelSheets)
            {
                string processingTable = string.Empty;
                processingTable = s;

                ws = (Worksheet)excelConn.WorkbookSheets[s];

                if (ws == null)
                    continue;
                //Look for '.V' range
                string tableHeader = ws.Name.Trim() + ".V";

                names = ws.Names;
                headerRange = extract.FindRange(names, tableHeader);

                //Extract header data from the range
                string[,] headerData = extract.ExtractDataFromRange(ws, headerRange);
                bool result = headerData == null ? IsSupportedExcelTemplateVersion(null) : IsSupportedExcelTemplateVersion(headerData[0, 0]);
                if (result == false)
                    return (false);
            }

            return true;
        }

        public static void getRcBusinessCodes(string connectionString)
        {
            List<RcBusinessCodeMapping> lst = null;
            using (GetSQLData sqlData = new GetSQLData(connectionString))
            {
                lst = sqlData.getRcBusinessCodes();

            }

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Excel files (*.xlsx)|*.xlsx";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;

            if (File.Exists(dialog.FileName))
                File.Delete(dialog.FileName);

            exportBusinessCodes(lst, dialog.FileName);


        }

        public static void exportBusinessCodes(List<RcBusinessCodeMapping> lst, string filePath)
        {
            NetOffice.ExcelApi.Application application = new NetOffice.ExcelApi.Application();
            try
            {
                if (!string.IsNullOrEmpty(filePath))
                {

                    application.Workbooks.Add();
                    application.DisplayAlerts = false;


                    Workbook newWb = application.ActiveWorkbook; ;

                    if (newWb != null)
                    {

                        //Adding mapping sheet
                        Worksheet mappingSheet = (Worksheet)application.ActiveSheet;
                        mappingSheet.Name = "Mapping";
                        application.Sheets.Add(mappingSheet);

                        //Formatting
                        mappingSheet.Columns.VerticalAlignment = NetOffice.ExcelApi.Enums.XlVAlign.xlVAlignCenter;
                        mappingSheet.Columns.HorizontalAlignment = NetOffice.ExcelApi.Enums.XlHAlign.xlHAlignCenter;
                        mappingSheet.Columns.AutoFit();
                        mappingSheet.Columns.ColumnWidth = 25;

                        //getting Ranges
                        Range headerRange = mappingSheet.Range(mappingSheet.Cells[1, 1], mappingSheet.Cells[1, 4]);
                        Range valuesRange = mappingSheet.Range(mappingSheet.Cells[2, 1], mappingSheet.Cells[lst.Count, 4]);
                        Range numberRange = mappingSheet.Range(mappingSheet.Cells[2, 2], mappingSheet.Cells[lst.Count, 2]);


                        ////To freeze the Panes 
                        mappingSheet.Activate();
                        mappingSheet.Application.ActiveWindow.SplitRow = 1;
                        mappingSheet.Application.ActiveWindow.FreezePanes = true;

                        //storing values
                        setRangeValues(headerRange, valuesRange, lst, numberRange);




                        //Deleting the last sheet                
                        if (newWb.Worksheets.Count > 1)
                        {
                            Worksheet ws = (Worksheet)newWb.Worksheets.ElementAt(0);
                            if (ws != null)
                                ws.Delete();
                        }
                        application.DisplayAlerts = true;

                        //save the excel
                        newWb.SaveAs(filePath, Missing.Value, Missing.Value, Missing.Value, false, false, XlSaveAsAccessMode.xlExclusive);
                        newWb.Close();
                    }
                    application.Quit();
                    application.Dispose();
                    MessageBox.Show("Download complete.", "Completed.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                application.Quit();
                application.Dispose();
                throw ex;
            }

        }

        public static void setRangeValues(Range headerRange, Range valuesRange, List<RcBusinessCodeMapping> lst, Range numberRange)
        {
            valuesRange.Interior.Color = XlRgbColor.rgbAliceBlue;
            valuesRange.Borders.LineStyle = XlLineStyle.xlDouble;
            valuesRange.Borders.Weight = 2;

            headerRange.Borders.LineStyle = XlLineStyle.xlDouble;
            headerRange.Borders.Weight = 2;
            headerRange.Interior.Color = XlRgbColor.rgbLightGrey;
            headerRange.Font.Bold = true;

            string[,] array = new string[lst.Count, 4];
            int row = 0;
            int col = 0;

            if (lst != null)
            {
                foreach (RcBusinessCodeMapping s in lst)
                {
                    array[row, col] = s.value1;
                    array[row, col + 1] = s.value2;
                    array[row, col + 2] = s.value3;
                    array[row, col + 3] = s.value4;
                    row = row + 1;
                }
                valuesRange.Value = array;
                //valuesRange.Value = valuesRange.Value;             
                numberRange.NumberFormat = "0.0";
                numberRange.Value = numberRange.Value;
            }

            //header values
            List<string> headerlst = new List<string>();
            headerlst.Add("t.TableCode");
            headerlst.Add("tc.CellID");
            headerlst.Add("group_concat(ao.OrdinateCode, '|')");
            headerlst.Add("tc.BusinessCode");
            headerRange.Value = headerlst.ToArray();

        }

        /// <summary>   
        /// Decrypts the input file
        /// </summary>   
        /// <param name="inputFile">input file name</param>   
        /// <param name="outputFile">output file name</param>  
        /// <param name="skey">Key to decrypt</param> 
        /// 
        private static void DecryptFile(string inputFile, string outputFile, string skey)
        {
            try
            {
                using (RijndaelManaged aes = new RijndaelManaged())
                {
                    byte[] key = ASCIIEncoding.UTF8.GetBytes(skey);

                    /* This is for demostrating purposes only. 
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    byte[] IV = ASCIIEncoding.UTF8.GetBytes(skey);

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(key, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static bool isExcelInstalled()
        {
            Type officeType = Type.GetTypeFromProgID("Excel.Application");
            if (officeType == null)
            {
                //no Excel installed
                MessageBox.Show("Excel application is not installed in the machine");
                return false;
            }
            else
            {
                //Excel installed
                return true;
            }
        }

    }
}
