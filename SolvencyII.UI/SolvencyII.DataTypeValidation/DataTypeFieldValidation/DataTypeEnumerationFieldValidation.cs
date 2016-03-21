using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SolvencyII.DataTypeValidation.DataTypeFieldValidation
{
    internal class DataTypeEnumerationFieldValidation : IDataTypeFieldValidation
    {
        public DataTypeValidationResult Validate(string tableName, string columnCode, string dataType, string value)
        {
            Dictionary<string, string> enumerationCodes = GetEnumerationCode(dataType);
            if (enumerationCodes == null || enumerationCodes.Count == 0)
            {
                return new DataTypeValidationResult
                {
                    TableName = tableName,
                    ColumnName = columnCode,
                    ColumnType = dataType,
                    ColumnValue = value,
                    Error = "Not able to fetch enum values from Database"
                };
            }
            else
            {
                if (!enumerationCodes.ContainsKey(value))
                {
                    return new DataTypeValidationResult
                    {
                        TableName = tableName,
                        ColumnName = columnCode,
                        ColumnType = "Enumeration" + "(" +  dataType + ")",
                        ColumnValue = value,
                        Error = "Invalid data as given Enumeration"
                    };
                }
                else
                {
                    return null;
                }
            }
            
        }



        public Dictionary<string, string> GetEnumerationCode(string dataType)
        {
            Dictionary<string, string> enumerationCodes = null;
            int hierarchyID = 0;
            int hierarchyStartingMemberID = 0;
            int isStartingMemberIncluded = 0;
            if (isEnumerationDataType(dataType))
            {
                string[] strhierarchyID = Regex.Split(dataType.Trim(), "E:");
                string hierarchyIDValue = strhierarchyID[1];
               // hierarchyID = Convert.ToInt32(hierarchyIDValue);

                //Code changes to handle the Metrics 
               

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
            }

            if (hierarchyID != 0)
            {
                dataType = "E:" + hierarchyID.ToString();
                enumerationCodes = DataTypeValidationSqlHelper.HierarchyLookupWithMetricsEnabled(hierarchyID, hierarchyStartingMemberID, isStartingMemberIncluded);
                return enumerationCodes;
            }
            return enumerationCodes;

        }

        public bool isEnumerationDataType(string dataTypes)
        {
            return dataTypes.StartsWith("E:");
        }
    }
}
