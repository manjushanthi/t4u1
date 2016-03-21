using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using NetOffice.ExcelApi;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.ExcelImportExportLib.DpmObjects;
using SolvencyII.ExcelImportExportLib.Domain;
using System.Globalization;

using SolvencyII.ExcelImportExportLib.Dto;
using SolvencyII.ExcelImportExportLib.Utils;

namespace SolvencyII.ExcelImportExportLib.Transform
{
    public class TransformDpmBusinessData : TransformBase
    {
        List<OrdinateHierarchy> ordHierarchy;

        Type tableType;

        /*private OrdinateHierarchy GetOrdinateHierarchy()
        {

        }*/

        public override void Transform(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto)
        {
            BusinessTemplateDto bDto = dto as BusinessTemplateDto;

            mTable table = (new TableInfo().GetTable(sqliteConnection, bDto.TableCode)).FirstOrDefault();
            mTaxonomy taxonomy = (new TaxonomyInfo().GetTaxonomy(sqliteConnection, 1)).FirstOrDefault();
            string tableName = Helper.GetTableName(taxonomy, table);
            tableType = Helper.ReferencedLookup(tableName);

            List<MAPPING> mapData = (List<MAPPING>) (new MappingInfo()).GetMappingInfo(sqliteConnection, table.TableID);

            HierarchyInfo hierarchyInfo = new HierarchyInfo();

            ordHierarchy = (List<OrdinateHierarchy>)hierarchyInfo.GetOrdinateHierarchyMD(sqliteConnection, bDto.TableCode);


            if(bDto.TypeOfTable == TableType.OPEN_TABLE)
            {
                string[,] tableData = new string[bDto.CRTData.Count(), bDto.HeaderData.GetLength(1)];

                int i = 0;

                //assign the value of data in the same order as the header column
                for (int j = 0; j < bDto.HeaderData.GetLength(1); j++)
                {
                    string ordinateCode = bDto.HeaderData[bDto.HeaderData.GetLength(0) - 1, j].ToUpper();

                    PropertyInfo prop = (from p in tableType.GetProperties()
                                         where p.Name.ToUpper() == ordinateCode
                                         select p).FirstOrDefault();
                    i = 0;

                    //Select only those colummns which are in the template
                    foreach (object o in bDto.CRTData)
                    {

                        if (prop != null)
                        {
                            if (prop.GetValue(o, null) == null)
                            {
                                tableData[i, j] = string.Empty;
                            }
                            else
                            {
                                tableData[i, j] = prop.GetValue(o, null).ToString();
                            }
                        }
                        else
                            tableData[i, j] = string.Empty;

                        //If it is and enumeration
                        MAPPING dataType = mapData.Where(a => a.DYN_TAB_COLUMN_NAME.ToUpper() == ordinateCode).FirstOrDefault();

                        //if it is a page column ordinate
                        if(ordinateCode.Contains("PAGE"))
                        {
                           string type = (new MappingInfo()).GetPageColumnDatatype(sqliteConnection, table.TableID, ordinateCode);

                           if (type != null)
                               dataType.DATA_TYPE = type;
                        }

                        if (dataType != null && dataType.DATA_TYPE == "E")
                        {
                            //Get th ordinate hierarchy
                            OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == ordinateCode).FirstOrDefault();

                            //Try get hierarchy from open axis
                            if (ordHie == null)
                            {
                                List<OrdinateHierarchy> openAxisHierarchy = (List<OrdinateHierarchy>)hierarchyInfo.GetOpenAxisHierarchy(sqliteConnection, bDto.TableCode);
                                ordHie = openAxisHierarchy.Where(a => a.PageColumn.ToUpper() == ordinateCode).FirstOrDefault();
                            }

                            //This piece of code is to fix for the semi-open table in y-axis which is renderd as open table
                            if (ordHie == null)
                            {
                                ordHie = ordHierarchy.Where(a => a.OrdinateCode == ordinateCode.Substring(5)).FirstOrDefault();
                            }
                            //End of fix

                            if (ordHie == null)
                                throw new KeyNotFoundException("Could not able to find the hierarchy");

                            List<HierarchyAndMemberInfo> info =
                                    (List<HierarchyAndMemberInfo>)hierarchyInfo.GetMetricsEnabledHierarchyAndMemberInfo(
                                    sqliteConnection,
                                    ordHie.HierarchyID,
                                    (int)ordHie.HierarchyStartingMemberID,
                                    ordHie.IsStartingMemberIncluded ? 1 : 0);


                            //Get the hierarchy for the corresponding column
                            if (info != null)
                            {

                                //Get the HierarchyAndMemberInfo for the corresponding text
                                HierarchyAndMemberInfo matched = (from hm in info
                                                                  where hm.MemberXBRLCode.ToUpper() == tableData[i, j].ToUpper()
                                                                  select hm).FirstOrDefault();

                                //If matched replace the hierarchy with the corresponding XBRL code
                                if (matched != null)
                                    tableData[i, j] = matched.MemberLabel + "   {" + matched.MemberXBRLCode + "}";
                            }

                        }
                        else
                        {
                            if (dataType != null)
                            {
                                string transformedValue;
                                ExcelExportValidationMessage message = Validate(tableData[i, j].ToString(), dataType.DATA_TYPE, out transformedValue);

                                if (message != null || transformedValue != string.Empty)
                                    tableData[i, j] = transformedValue;
                                /*else
                                    ExcelExportValidationMessageLst.Add(message);*/
                            }
                        }

                        i++;
                    }
                }

                //Assign the transformed data to the transfer object
                bDto.TableData = tableData;
            }
            else if (bDto.TypeOfTable == TableType.CLOSED_TABLE || bDto.TypeOfTable == TableType.SEMI_OPEN_TABLE)
            {
                int col = bDto.TableDataRange.Columns.Count - 1;
                int row = bDto.TableDataRange.Rows.Count - 1;

                string[,] tableData = new string[row, col];

                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        string rowCode = bDto.HeaderData[i + 1, 0];
                        string colCode = bDto.HeaderData[0, j + 1];

                        string rowcolCode = "";

                        if (rowCode == null || colCode == null || rowCode == "" || colCode == "")
                            continue;

                        rowcolCode = rowCode.Trim().ToUpper() + colCode.Trim().ToUpper();

                        PropertyInfo prop = (from p in tableType.GetProperties()
                                             where p.Name.ToUpper() == rowcolCode
                                             select p).FirstOrDefault();

                        if (prop == null)
                            continue;

                        object value = prop.GetValue(bDto.CurrentObject , null);

                        //If it is and enumeration
                        MAPPING dataType = mapData.Where(a => a.DYN_TAB_COLUMN_NAME.ToUpper() == rowcolCode).FirstOrDefault();

                        //Select only those colummns which are in the template
                        if (value == null)
                            tableData[i, j] = string.Empty;
                        
                        else
                        {
                            if (dataType != null && dataType.DATA_TYPE == "E")
                            {
                                //Get th ordinate hierarchy
                                OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == rowCode || a.OrdinateCode == colCode).FirstOrDefault();

                                if (ordHie == null)
                                    throw new KeyNotFoundException("Could not able to find the hierarchy");

                                List<HierarchyAndMemberInfo> info =
                                    (List<HierarchyAndMemberInfo>)hierarchyInfo.GetMetricsEnabledHierarchyAndMemberInfo(
                                    sqliteConnection,
                                    ordHie.HierarchyID,
                                    (int)ordHie.HierarchyStartingMemberID,
                                    ordHie.IsStartingMemberIncluded ? 1 : 0);

                                if (info != null)
                                {
                                    //Get the HierarchyAndMemberInfo for the corresponding text
                                    HierarchyAndMemberInfo matched = (from hm in info
                                                                      where hm.MemberXBRLCode.ToUpper() == value.ToString().ToUpper()
                                                                      select hm).FirstOrDefault();

                                    //If matched replace the hierarchy with the corresponding XBRL code
                                    if (matched != null)
                                        value = matched.MemberLabel + "   {" + matched.MemberXBRLCode + "}";

                                }
                            }
                            else
                            {
                                if (dataType != null)
                                {
                                    string transformedValue = string.Empty;
                                    ExcelExportValidationMessage message = Validate(value.ToString(), dataType.DATA_TYPE, out transformedValue);

                                    if (message != null || transformedValue != string.Empty)
                                        value = transformedValue;
                                    /*else
                                        ExcelExportValidationMessageLst.Add(message);*/
                                }
                            }

                            tableData[i, j] = value.ToString();
                        }
                    }
                }

                //Assign the transformed data to the transfer object
                bDto.TableData = tableData;

            }

            //Render Z - Axis filter data
            if (bDto.FilterData != null)
            {

                string[, ] tranformedData = TransformFilterData(sqliteConnection, bDto.FilterData, bDto);
                if (tranformedData != null)
                    bDto.FilterData = tranformedData;
            }

            //Render X - Axis filter data
            if (bDto.XFilterData != null)
            {

                string[,] tranformedData = TransformFilterData(sqliteConnection, bDto.XFilterData, bDto);
                if (tranformedData != null)
                    bDto.XFilterData = tranformedData;
            }

            //Render Y - Axis filter data
            if (bDto.YFilterData != null)
            {

                string[,] tranformedData = TransformFilterData(sqliteConnection, bDto.YFilterData, bDto);
                if (tranformedData != null)
                    bDto.YFilterData = tranformedData;
            }
            
            return;
        }

        private string[,] TransformFilterData(ISolvencyData sqliteConnection, string[,] filterData, BusinessTemplateDto bDto)
        {
            string[,] tranformedData = new string[filterData.GetLength(0), filterData.GetLength(1)];

            //copy the data to transformedData
            for (int i = 0; i < tranformedData.GetLength(0); i++)
                for (int j = 0; j < tranformedData.GetLength(1); j++)
                    tranformedData[i, j] = filterData[i, j];

            HierarchyInfo hierarchyInfo = new HierarchyInfo();

            for (int i = 0; i < tranformedData.GetLength(0); i++)
            {
                //Get th ordinate hierarchy
                ordHierarchy = (List<OrdinateHierarchy>)hierarchyInfo.GetOpenAxisHierarchy(sqliteConnection, bDto.TableCode);
                OrdinateHierarchy ordHie = ordHierarchy.Where(a => a.OrdinateCode == tranformedData[i, 0]).FirstOrDefault();

                if (ordHie != null)
                {
                    List<HierarchyAndMemberInfo> info =
                                (List<HierarchyAndMemberInfo>)hierarchyInfo.GetMetricsEnabledHierarchyAndMemberInfo(
                                sqliteConnection,
                                ordHie.HierarchyID,
                                (int)ordHie.HierarchyStartingMemberID,
                                ordHie.IsStartingMemberIncluded ? 1 : 0);


                    //Get the hierarchy for the corresponding column
                    if (info != null)
                    {
                        PropertyInfo prop = (from p in tableType.GetProperties()
                                             where p.Name.ToUpper() == ordHie.PageColumn.ToUpper()
                                             select p).FirstOrDefault();

                        if (prop == null)
                            continue;

                        object value = prop.GetValue(bDto.CurrentObject, null);

                        //Get the HierarchyAndMemberInfo for the corresponding text
                        HierarchyAndMemberInfo matched = (from hm in info
                                                          where hm.MemberXBRLCode.ToUpper() == value.ToString().ToUpper()
                                                          select hm).FirstOrDefault();

                        //If matched replace the hierarchy with the corresponding XBRL code
                        if (matched != null)
                            tranformedData[i, 1] = matched.MemberLabel + "   {" + matched.MemberXBRLCode + "}";
                        else
                            tranformedData[i, 1] = value.ToString();

                    }
                }
            }

            return tranformedData;
        }
    }
}
