﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain.Extensions;
using SolvencyII.Data.Entities;
using SolvencyII.Data.SQLite;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Exceptions;
using SolvencyII.ExcelImportExportLib.Utils;
using SolvencyII.ExcelImportExportLib.Dto;

namespace SolvencyII.ExcelImportExportLib.Load
{
    public class LoadDpm : LoadBase
    {
        public override int LoadData(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto)
        {
            BasicTemplateDto bDto = dto as BasicTemplateDto;

            if (bDto == null)
                throw new InvalidCastException("An error occured whil casint Transfer object to Basic template transfer object");
            int hWidth = bDto.HeaderData.GetLength(1);
            int hHeight = bDto.HeaderData.GetLength(0);
            int tWidth = bDto.TableData.GetLength(1);
            int tHeight = bDto.TableData.GetLength(0);

            TableInfo info = new TableInfo();
            IEnumerable<mTable> tableList = info.GetTable(sqliteConnection, bDto.TableCode);

            

            if (tableList == null)
            {
                //throw an exception here
            }

            mTable table = (from t in tableList
                            select t).FirstOrDefault<mTable>();

            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(sqliteConnection, 1)).FirstOrDefault();
            string tableName = Helper.GetTableName(taxonomy, table);

            //Add column information
            List<OpenColInfo2> openColInfo = new List<OpenColInfo2>();

            /*openColInfo.Add(new OpenColInfo2
            {
                ColName = PK_ID
            });*/


            PreparatoryBreakingChangesFix(hHeight, hWidth, bDto.TableCode, bDto.HeaderData);
            for (int i = 0; i < hWidth; i++)
            {              
                openColInfo.Add(new OpenColInfo2
                {
                    ColName = bDto.HeaderData[hHeight-1, i]
                });
            }



            for (int row = 0; row < tHeight; row++)
            {
                OpenTableDataRow2 dataRow = new OpenTableDataRow2();

                for(int col=0; col<tWidth; col++)
                {
                    dataRow.ColValues.Add(bDto.TableData[row, col]);
                }

                try
                {

                    SaveOpenTableDataRow2(sqliteConnection, dataRow, tableName, openColInfo, bDto.Instance.InstanceID, new List<FormDataPage>(), new List<ISolvencyPageControl>());
                }
                catch (SQLiteException se)
                {

                    int excelRow = bDto.HeaderRange.Row + hHeight + row;

                    Range errorRange = workSheet.Range(workSheet.Cells[excelRow, bDto.HeaderRange.Column], workSheet.Cells[excelRow, bDto.HeaderRange.Column + hWidth - 1]);


                    StringBuilder sb = new StringBuilder();
                    sb.Append("Please check that you do not have duplicate elements in Excel or existing data in the database. The value at the row ");
                    sb.Append(excelRow);
                    sb.Append(" [");

                    string cellAddress = errorRange.Address;

                    do
                    {
                        int index = cellAddress.IndexOf('$');

                        if (index >= 0)
                            cellAddress = cellAddress.Remove(index, 1);
                    } while (cellAddress.IndexOf('$') >= 0);


                    sb.Append(cellAddress);
                    sb.Append("] ");
                    sb.AppendLine();

                    sb.Append("(");
                    for (int col = 0; col < tWidth; col++)
                        sb.Append(bDto.TableData[row, col]).Append(",");

                    sb.Remove(sb.Length - 1, 1);

                    sb.Append(")");
                    sb.AppendLine();
                    sb.Append("Internal exception: ").Append(se.Message);

                    string address = errorRange.Address;
                    errorRange.Dispose();
                    errorRange = null;

                    throw new T4UExcelImportExportException(sb.ToString(), se);
                }
            }


            //mTaxonomyTable taxonomyTable = (sqliteConnection.Query<mTaxonomyTable>(string.Format(" select * from mTaxonomyTable where tableid = {0} ", table.TableID))).FirstOrDefault();

            //Update and insert dFillingIndicator
            try
            {
                //BRAG
                //Why there is Substring(0,7) here? It`s not working, when table code has more than one character at the begining.
                //mTemplateOrTable templateOrTable = (sqliteConnection.Query<mTemplateOrTable>(string.Format(" select * from mTemplateOrTable where TemplateOrTableCode = '{0}' ", table.TableCode.Substring(0, 7)))).FirstOrDefault();
                mTemplateOrTable templateOrTable = (sqliteConnection.Query<mTemplateOrTable>(string.Format("select tem.TemplateOrTableID from mTemplateOrTable tab inner join mTemplateOrTable var on var.TemplateOrTableID = tab.ParentTemplateOrTableID inner join mTemplateOrTable tem on tem.TemplateOrTableID = var.ParentTemplateOrTableID where tab.TemplateOrTableCode = '{0}' and tab.TemplateOrTableType = 'BusinessTable'", table.TableCode))).FirstOrDefault();

                IList<dFilingIndicator> filling = sqliteConnection.Query<dFilingIndicator>(string.Format("select * from dFilingIndicator where instanceid = {0} and businesstemplateid = {1}", bDto.Instance.InstanceID, templateOrTable.TemplateOrTableID));

                //update only if template id is not already updated in the table
                if (filling.Count() == 0)
                {
                    StringBuilder query = new StringBuilder();
                    query.Append(" insert into dFilingIndicator (InstanceID, BusinessTemplateID) ");
                    query.Append(string.Format(" values({0},{1}) ", bDto.Instance.InstanceID, templateOrTable.TemplateOrTableID));

                    sqliteConnection.Execute(query.ToString());
                }
            }
            catch (SQLiteException se)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("There was a constraint error occured while updating the dFilingIndicator.");
                /*sb.AppendLine();
                sb.Append("Operation cannot continue.");*/


                throw new T4UExcelImportExportException(sb.ToString(), se);
            }

            return tHeight; //total rows
            
        }

        private void PreparatoryBreakingChangesFix(int hHeight, int hWidth, string tableCode, string[,] headerData)
        {
            for (int i = 0; i < hWidth; i++)
            {
                if (!string.IsNullOrEmpty(tableCode))
                {
                    string tableCodesWithbreakChanges = "S.26.04.02.04,S.26.04.04.04,S.26.04.06.04,S.26.04.01.04,S.26.04.03.04,S.26.04.05.04";
                    if (tableCodesWithbreakChanges.Split(',').Contains(tableCode))
                    {
                        if (headerData[hHeight - 1, i].ToString() == ("R01100C0170"))
                        {
                            headerData[hHeight - 1, i] = "R1100C0170";
                        }
                    }
                }
                
            }
        }

        private int SaveOpenTableDataRow2(ISolvencyData dpmConn, OpenTableDataRow2 row, string dataTable, List<OpenColInfo2> colManager, long instanceID, List<FormDataPage> formDataPages, List<ISolvencyPageControl> ctrls)
        {
            //dpmConn.BeginTransaction();
            /*try
            {*/
            int result = 0;
            Dictionary<string, object> parameters;
            //string query = BuildOpenTableUpdateQuery2(row, dataTable, colManager, instanceID, formDataPages, out parameters, ctrls);

            parameters = new Dictionary<string, object>();

            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            // Insert query required
            sb.Append(string.Format("Insert into [{0}] ( INSTANCE", dataTable));
            sb2.Append(string.Format(") Values ( {0}", instanceID));

            int colCount = 0;
            foreach (OpenColInfo2 col in colManager)
            {
                sb.Append(string.Format(", {0}", col.ColName));
                sb2.Append(string.Format(", @{0}", col.ColName));

                parameters.Add(string.Format("@{0}", col.ColName), col.Value(colCount, row));
                colCount++;
            }

            // Combo box information processing.
            foreach (var pageControl in formDataPages)
            {
                sb.Append(string.Format(", {0}", pageControl.DYN_TAB_COLUMN_NAME));
                sb2.Append(string.Format(", @{0}", pageControl.DYN_TAB_COLUMN_NAME));

                parameters.Add(string.Format("@{0}", pageControl.DYN_TAB_COLUMN_NAME), pageControl.Value);
            }

            // Hidden nPage boxes information processing.
            foreach (ISolvencyPageControl pageControl in ctrls)
            {

                sb.Append(string.Format(", {0}", pageControl.ColName));
                sb2.Append(string.Format(", @{0}", pageControl.ColName));

                parameters.Add(string.Format("@{0}", pageControl.ColName), pageControl.Text);
            }

            string query = string.Format("{0} {1}", sb, sb2 + "); ");

            if (parameters.Any())
                dpmConn.Execute(query, parameters);
            else
                dpmConn.Execute(query);
            // Was this an insert?
            if (row.PK_ID == 0)
            {

                result = dpmConn.ExecuteScalar<int>("Select last_insert_rowid();");

                row.PK_ID = result;
            }

            return result;

        }
    }
}
