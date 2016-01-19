using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using NetOffice.ExcelApi;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;
using SolvencyII.Data.SQLite;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Dto;
using SolvencyII.ExcelImportExportLib.Events;
using SolvencyII.ExcelImportExportLib.Extract;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Transform;
using SolvencyII.ExcelImportExportLib.Load;
using SolvencyII.ExcelImportExportLib.Utils;
using SolvencyII.Domain.Configuration;
using System.Text.RegularExpressions;


namespace SolvencyII.ExcelImportExportLib
{
    public class ExcelBasicTemplateExportImpl : BasicTemplateImportExportBase,  IExcelExport
    {
        private List<ExcelExportValidationMessage> ExcelExportValidationMessage = new List<ExcelExportValidationMessage>();

        public List<ExcelExportValidationMessage> ExcelExportValidationMessageLst
        {
            set { ExcelExportValidationMessage = value; }
            get { return ExcelExportValidationMessage; }
        }

        public ExcelBasicTemplateExportImpl(List<ExcelExportValidationMessage> excelExportValidationMessageLst)
        {
            ExcelExportValidationMessageLst = excelExportValidationMessageLst;
        }

        protected override TransformBase GetTransformer()
        {
            return new TransformDpmData();
        }

        protected override LoadBase GetLoader()
        {
            return new LoadExcel();
        }

        protected override string[] GetTableCodes(ISolvencyData sqliteConnection, IExcelConnection excelConnection)
        {
            /*TableInfo info = new TableInfo();
            //get the whole list of table codes from database
            IList<mTable> tableList = (IList<mTable>)info.GetAllTables(sqliteConnection);
            return (from t in tableList
                      select t.TableCode).ToArray<string>();*/

            TableCodeExtractor extractor = new TableCodeExtractor();

            return extractor.GetTableCodesFromDb(sqliteConnection);
        }

        protected override string[, ] GetTableData(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto )
        {
            BasicTemplateDto bDto = dto as BasicTemplateDto;

            if(bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Basic template transfer object");

            ExtractData extract = new ExtractData();
            mTable table = (new TableInfo().GetTable(sqliteConnection, bDto.TableCode)).FirstOrDefault();
            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(sqliteConnection, 1)).FirstOrDefault();
            string tableName = Helper.GetTableName(taxonomy, table);

            Type t = Helper.ReferencedLookup(tableName);

            string query = string.Format("select * from {0} where instance = {1} limit {2} offset {3}", tableName, bDto.Instance.InstanceID, bDto.Limit, bDto.Offset);

            IList<object> tempData = sqliteConnection.Query(t, query);

            string[,] headerData = extract.ExtractDataFromRange(workSheet, bDto.HeaderRange);
            string[,] tableData = new string[tempData.Count(), headerData.GetLength(1)];

            int row = 0 ;

            int hWidth = headerData.GetLength(1);
            int hHeight = headerData.GetLength(0);
            int tWidth = tableData.GetLength(1);
            int tHeight = tableData.GetLength(0);
            int typeRow = 2;
            HierarchyInfo hierarchyInfo = new HierarchyInfo();

            Dictionary<int, List<HierarchyAndMemberInfo>> hierarchies = new Dictionary<int, List<HierarchyAndMemberInfo>>();

            //Parsing the headers to load the hirearchies
            for (int i = 0; i < hWidth; i++)
            {
                //If the type of a column is an enumerator
                string type = headerData[typeRow, i];

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

                    //int startIndex = type.IndexOf(':') + 1;
                    //int endIndex = type.IndexOf(';');
                    //int hierarchyID = endIndex == -1 ? Int32.Parse(type.Substring(startIndex)) : Int32.Parse(type.Substring(startIndex, endIndex - startIndex));

                    if (hierarchyID > 0)
                    {
                        //Get hierarchy information from DPM database
                        //List<HierarchyAndMemberInfo> info = (List<HierarchyAndMemberInfo>)hierarchyInfo.GetHierarchyAndMemberInfo(sqliteConnection, hierarchyID);
                        List<HierarchyAndMemberInfo> info = (List<HierarchyAndMemberInfo>)hierarchyInfo.GetMetricsEnabledHierarchyAndMemberInfo(sqliteConnection, hierarchyID,hierarchyStartingMemberID, isStartingMemberIncluded);
                        //List<HierarchyAndMemberInfo> info = new MetricsEnumerationExtractor(StaticSettings.ConnectionString).getHierarchyQnamesToTextLst(hierarchyID, hierarchyStartingMemberID, isStartingMemberIncluded);

                        if (info != null)
                        {
                            hierarchies.Add(i, info);
                        }

                    }
                }

            }


            //assign the value of data in the same order as the header column
            for (int col = 0; col < headerData.GetLength(1); col++)
            {
                string type = headerData[typeRow, col];

                PropertyInfo prop = (from p in t.GetProperties()
                                        where p.Name.ToUpper() == headerData[headerData.GetLength(0) - 1, col].ToUpper()
                                        select p).FirstOrDefault();
                row = 0;

                //Select only those colummns which are in the template
                foreach (object o in tempData)
                {

                    if (prop.GetValue(o, null)==null)
                    {
                        tableData[row, col] = string.Empty;
                    }
                    else
                    {
                        tableData[row, col] = prop.GetValue(o, null).ToString();
                    }

                     if (tableData[row, col] == null)
                     {
                         tableData[row, col]=string.Empty;
                     }
                     else
                     {
                        if (type.ToUpper().Trim() == "DATE")
                        {
                            try
                            {
                                if (tableData[row, col]!=string.Empty)
                                tableData[row, col] = DateTime.Parse(tableData[row, col]).ToShortDateString();
                            }
                            catch(FormatException fe)
                            {
                                //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate. 
                                ExcelExportValidationMessage message = new ExcelExportValidationMessage();
                                message.TableCode = table.TableCode;
                                message.Value = tableData[row, col];
                                message.FieldType = type;
                                ExcelExportValidationMessageLst.Add(message);  

                            }
                        }
                        else if (type.ToUpper().Trim() == "BOOLEAN")
                        {
                            if (tableData[row, col] != string.Empty)
                            {
                                if (!(tableData[row, col].Trim().ToUpper() == "TRUE" || tableData[row, col].Trim().ToUpper() == "FALSE"))
                                {
                                    //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate. Boolean Data has exported as "false" value                                
                                    ExcelExportValidationMessage message = new ExcelExportValidationMessage();
                                    message.TableCode = table.TableCode;
                                    message.Value = tableData[row, col];
                                    message.FieldType = type;
                                    ExcelExportValidationMessageLst.Add(message);
                                    tableData[row, col] = "FALSE";
                                }                               
                            }

                        }
                        else if (type.Trim().StartsWith("E:"))
                        {

                            bool success = false;
                            if (hierarchies.ContainsKey(col))
                            {
                                //Get the hierarchy for the corresponding column
                                List<HierarchyAndMemberInfo> info = hierarchies[col];
                                if (info != null)
                                {

                                    //Get the HierarchyAndMemberInfo for the corresponding text
                                    HierarchyAndMemberInfo matched = (from hm in info
                                                                      where hm.MemberXBRLCode.ToUpper() == tableData[row, col].ToUpper()
                                                                      select hm).FirstOrDefault();

                                    //If matched replace the hierarchy with the corresponding XBRL code
                                    if (matched != null)
                                    {
                                        tableData[row, col] = matched.MemberLabel + "   {" + matched.MemberXBRLCode + "}";
                                        success = true;
                                    }
                                }
                            }

                            if (!success && tableData[row, col]!=string.Empty)
                            {


                                 //Export the value as in the database
                                //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate.
                                    ExcelExportValidationMessage message = new ExcelExportValidationMessage();                              
                                    message.TableCode = table.TableCode;
                                    message.Value = tableData[row, col];
                                    message.FieldType = type;
                                    ExcelExportValidationMessageLst.Add(message);
                               
                            }
                        }
                        else if (type.ToUpper().Trim() == "DECIMAL" || type.ToUpper().Trim() == "MONETARY")
                        {   
                            decimal devnull;
                            if (tableData[row, col] != string.Empty)
                            {
                                if (!decimal.TryParse(tableData[row, col], out devnull))
                                {
                                   
                                    //we are missing a decimal or monetary
                                    //Export the value as in the database
                                    //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate.
                                    ExcelExportValidationMessage message = new ExcelExportValidationMessage();
                                    message.TableCode = table.TableCode;
                                    message.Value = tableData[row, col];
                                    message.FieldType = type;
                                    ExcelExportValidationMessageLst.Add(message);

                                }
                               
                            }
                        
                        }
                        else if (type.ToUpper().Trim() == "INTEGER" )
                        {
                            int devnull;
                            if (tableData[row, col] != string.Empty)
                            {
                                if (!int.TryParse(tableData[row, col], out devnull))
                                {
                                    //Export the value as in the database
                                    //The data for the row/column/table with value is not having the expeted format. Please run the Validate container function in the menu validate.
                                    ExcelExportValidationMessage message = new ExcelExportValidationMessage();
                                    message.TableCode = table.TableCode;
                                    message.Value = tableData[row, col];
                                    message.FieldType = type;
                                    ExcelExportValidationMessageLst.Add(message);
                                }
                            }
                        }

                    }
                    
                    row++;
                }
              
            }
            
            return tableData;
        }

        protected override int GetTotalTableRows(ISolvencyData sqliteConnection, Worksheet workSheet, Range headerRange, dInstance instance, string tableCode)
        {
            int rows = 0;
            TableInfo info = new TableInfo();
            mTable table = (new TableInfo().GetTable(sqliteConnection, tableCode)).FirstOrDefault();
            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(sqliteConnection, 1)).FirstOrDefault();
            string tableName = Helper.GetTableName(taxonomy, table);

            Type t = Helper.ReferencedLookup(tableName);

            string query = string.Format("select count(*) from {0} where instance = {1} ", tableName, instance.InstanceID);

            rows = sqliteConnection.ExecuteScalar<int>(query);


            return rows;
        }

        protected override void BeginTransaction(ISolvencyData sqliteConnection, Workbook workbook)
        {
            //Do nothing
        }

        protected override void Commit(ISolvencyData sqliteConnection, Workbook workbook)
        {
            workbook.Save();
        }

        protected override void Rollback(ISolvencyData sqliteConnection, Workbook workbook)
        {
            //required to avoid the errors 
            workbook.Save();
        }
    }
}
