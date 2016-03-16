using SolvencyII.Data.CRT.ETL;
using SolvencyII.Data.Shared;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Arelle;
using SolvencyII.Validation;
using SolvencyII.Validation.Domain;
using SolvencyII.Validation.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using SolvencyII.ExcelImportExportLib;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Exceptions;

namespace SolvencyII.CMD.Operations
{  
        public class ImportXBRLExportExcel
        {
            private AutoResetEvent _resetEvent = new AutoResetEvent(false);
            ArelleCmdInterface ImportExportArelle { get; set; }
            public string importedFileMigration = null;
            public string importedFileMigration_InstanceName = null;
            private EtlOperations factsETL;
            private string _dbPath = null;
            private string _excelFilePath = null;
            private string _applicationPath = null;
            /// <summary>
            /// Intialize ArelleProgress
            /// </summary>
            /// <param name="s"></param>
            /// <param name="args">Progress updates</param>

            public void ArelleProgress(object s, ProgressChangedEventArgs args)
            {
                if (ImportExportArelle != null)
                {
                    string msg = args.UserState as string;                   
                    Console.WriteLine(msg);                    
               }
            }

            /// <summary>
            /// This mehod imports the XBRL into the database after successful import the method ExportToExcel will be called
            /// </summary>
            /// <param name="s"></param>
            /// <param name="args"></param>

            private void ImportXBRL2DbArelleComplete_ImportComplete(object s, RunWorkerCompletedEventArgs args)
            {

                string report = string.Empty;
                eImportExportOperationType operation_type = eImportExportOperationType.No_Operation_selected;
                if (args.Result is Object[])
                {
                    object[] resultParams = args.Result as Object[];
                    if (resultParams[0] != null)
                        report = resultParams[0].ToString();
                    if (resultParams[1] != null)
                    {
                        //To get the operation type
                        string value = resultParams[1].ToString();
                        operation_type = (eImportExportOperationType)Enum.Parse(typeof(eImportExportOperationType), value, true);
                    }


                }

               
                dInstance instance;
                using (GetSQLData getData = new GetSQLData(_dbPath))
                {
                    instance = getData.GetInstanceDetails(importedFileMigration);
                }

                //loading the insatnce
                long newInstanceID;
                string entityName = "un-named";
                if (!string.IsNullOrEmpty(importedFileMigration_InstanceName))
                {
                    var val = importedFileMigration_InstanceName.Split('-')[0];
                    if (!string.IsNullOrEmpty(val))
                    {
                        entityName = val;
                    }
                    else
                        entityName = importedFileMigration_InstanceName;

                }
                instance.EntityName = entityName;
                //instance.FileName = importedFileMigration;
                if (instance != null)
                {
                    PutSQLData putData = new PutSQLData(_dbPath);
                    //to show validation error details              

                    IValidationQuery validationQuery = ValidationFactory.GetValidationQuery(eDataTier.SqLite);
                    ISolvencyData conn = ConnectionFactory.GetConnection(eDataTier.SqLite, _dbPath);
                    //SolvencyII.Validation.GetMessage getMessage = new SolvencyII.Validation.GetMessage(StaticSettings.ConnectionString);
                    IEnumerable<ValidationMessage> messages = validationQuery.GetArelleValidationErrors(conn, instance.InstanceID);

                    //Show only if there are any errors
                    //if (messages != null)
                    if (messages != null && messages.Count() > 0)
                    {
                        //ArelleValidationStatus statusDlg = null;
                        if (operation_type.Equals(eImportExportOperationType.Native_Import))
                        {
                            Console.WriteLine("Validation error messages");
                            foreach (var item in messages)
                            {
                                if (item != null)
                                {
                                    Console.WriteLine("-------------------------------------------------");
                                    Console.WriteLine("DataPointSignature =>" + item.DataPointSignature);
                                    Console.WriteLine("MessageCode =>" + item.MessageCode);
                                    Console.WriteLine("Value =>" + item.Value);
                                    Console.WriteLine("-------------------------------------------------");
                                }
                               
                            }
                           
                            //statusDlg = new ArelleValidationStatus(messages, report, ArelleValidationDisplayType.Arelle_results_for_native_import, true, entityName);
                            //statusDlg.ShowDialog();
                        }
                        Console.WriteLine("Press any key to continue");
                        Console.ReadLine();
                    }

                    string result = putData.InsertUpdateInstance(instance, out newInstanceID);
                    factsETL = new EtlOperations();

                    factsETL.etlLoadingXBRLinstance(_dbPath, Convert.ToInt32(instance.InstanceID));
                }


                ImportExportArelle = null;
                //on successful import 

                if (instance != null)
                {

                    string templateFilePtah = GetTemplateFileExcel(_dbPath, instance.InstanceID, GetExcelTemplatePath(), _applicationPath);
                    if (!string.IsNullOrEmpty(templateFilePtah))
                        ExportToExcel(_dbPath, templateFilePtah, instance.InstanceID);
                    else
                    {
                        Console.WriteLine("Template file not exists, not able to create the excel file");
                    }
                }else
                {
                    Console.WriteLine("Not able to create the instance, after importing the XBRL file");
                }
               
                _resetEvent.Set();
                Console.WriteLine("Import XBRL & Export excel completed successfully...");
            }

           /// <summary>
            /// Export the instance into the excel - by calling the method ExportToExcelCMD
           /// </summary>
           /// <param name="_dbPath">Container path</param>
           /// <param name="templateFilePtah">Excel template path</param>
            /// <param name="instanceId">InstanceID to get exported</param>

            public void ExportToExcel(string _dbPath, string templateFilePtah, long instanceId)
            {
               
                Console.WriteLine("Exporting to excel .......");               
                ExportToExcelCMD(_dbPath, instanceId, templateFilePtah);
                Console.WriteLine("Exported Successfully.............");
               
            }

            /// <summary>
            /// Based upon the connectecd container type, it will provide the excel template path 
            /// </summary>
            /// <returns>Template path</returns>
            protected string GetExcelTemplatePath()
            {

                if (StaticSettings.DbType == DbType.SolvencyII)
                {
                    return ("ExcelTemplates\\Full\\");

                }
                if (StaticSettings.DbType == DbType.SolvencyII_Preparatory)
                {
                    return ("ExcelTemplates\\Preparatory\\");

                }
                return (string.Empty);
            }

            protected string GetExcelFilename(mModule module)
            {
                return module.ModuleCode + "_" + module.ModuleLabel.Trim().ToLower().Replace(' ', '_') + ".xlsx";
            }

            /// <summary>
            /// GetTemplateFileExcel method provides the excel template path, It required to get copy of the excel file and the data will be exported on it. 
            /// </summary>
            /// <param name="connectionString">ConnectionString for the conatiner</param>
            /// <param name="instanceID">InstanceID to get exported</param>
            /// <param name="excelTemplatePath">Excel template path</param>
            /// <param name="appStartupPath">Application installed path</param>
            /// <returns></returns>
            public string GetTemplateFileExcel(string connectionString, long instanceID, string excelTemplatePath, string appStartupPath)
            {
                string templateFilPath = string.Empty;
                mModule module;
                using (GetSQLData sqlData = new GetSQLData(connectionString))
                {
                    dInstance instance = sqlData.GetInstanceDetails(instanceID);
                    if (instance == null)
                        return templateFilPath;
                    module = sqlData.GetModuleDetails(instance.ModuleID);
                }
                if (module == null)
                    return templateFilPath;
                string fileName = GetExcelFilename(module);
                templateFilPath = Path.Combine(appStartupPath, excelTemplatePath + fileName);
                if (!File.Exists(templateFilPath))
                {
                    return templateFilPath;
                }
                return templateFilPath;
            }
           
            /// <summary>
            /// ExportToExcelCMD method exports the specified instance instance into the excel file, (Operation: Excel export)
            /// </summary>
            /// <param name="connectionString">ConnectionString for the conatiner</param>
            /// <param name="instanceID">InstanceID to get exported</param>
            /// <param name="excelTemplatePath">Excel template path</param>

            public void ExportToExcelCMD(string connectionString, long instanceID, string excelTemplatePath)
            {
                List<ExcelExportValidationMessage> validationMessages = new List<ExcelExportValidationMessage>();
                IExcelExport exportExcel = new ExcelBasicTemplateExportImpl(validationMessages);

                IExcelConnection excelConnection = null;
                ISolvencyData dpmConn = null;
                dpmConn = new SQLiteConnection(connectionString);
                File.Copy(excelTemplatePath, _excelFilePath);

                excelConnection = new ExcelConnection(_excelFilePath);
              
                excelConnection.OpenConnection();

                dInstance instance;
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
              

                ExportExcelEvents obj = new ExportExcelEvents();
                exportExcel.ExportToFileCompleted += obj.CompletedHandler;
                exportExcel.ExportToFileProgressChanged += obj.ProgressChangedHandler;
                exportExcel.ExportToFileGranuleProgressChanged += obj.GranuleProgressChangedHandler;
                exportExcel.ExportToFile(ImportExportBehaviour.Exporting, dpmConn, excelConnection, instance, tableCodes);                
                excelConnection.CloseConnection();

                if (validationMessages!=null)
                {
                    if(validationMessages.Count>0)
                    {
                        Console.WriteLine("                     ");
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("Invalid data stored in the database , run valid container to get the full list of failures");
                        Console.WriteLine("-------------------------------------------------");
                        //foreach (var item in validationMessages)
                        //{
                        //    if (item != null)
                        //    {
                        //        Console.WriteLine("-------------------------------------------------");
                        //        Console.WriteLine("Table Code =>" + item.TableCode);
                        //        Console.WriteLine("Field Type =>" + item.FieldType);
                        //        Console.WriteLine("Value =>" + item.Value);
                        //        Console.WriteLine("-------------------------------------------------");
                               
                        //    }

                        //}


                    }
                }
                
               
            }



            public void importXBRLExportExcelAsync(string sourceXBRL, string excelFilePath,string DBPath, string applicationPath)
            {
                _dbPath = DBPath;
                _excelFilePath = excelFilePath;
                _applicationPath = applicationPath;

                if (ImportExportArelle == null)
                    ImportExportArelle = new ArelleCmdInterface("Loading instance - ");
                
                //importedFileMigration = sourceXBRL;
                //importedFileMigration = Path.GetFileName(sourceXBRL);
                importedFileMigration = Path.GetFileName(sourceXBRL);
                importedFileMigration_InstanceName = Path.GetFileNameWithoutExtension(sourceXBRL);

                ImportExportArelle.ParseInstanceIntoDatabase(eImportExportOperationType.Native_Import, sourceXBRL, ArelleProgress, ImportXBRL2DbArelleComplete_ImportComplete, DBPath);
                _resetEvent.WaitOne();
            }
        }
    
}
