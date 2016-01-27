
using SolvencyII.DataTypeValidation.DataTypeFieldValidation;
using SolvencyII.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using T4U.CRT.Generation.ExcelTemplateProcessor;

namespace SolvencyII.DataTypeValidation
{
    public class DataTypeDataTableValidation
    {
        private DataTable crtDataTable { get; set; }

        private string tableName { get; set; }


        private Dictionary<string, PocoColInfo> DictPocoColInfos { get; set; }

        public ExcelTemplateColumns excelTemplateColumns { get; set; }
        public DataTypeDataTableValidation(string tableName, DataTable crtDataTable, Dictionary<string, PocoColInfo> dictPocoColInfos, ExcelTemplateColumns excelTemplateColumns)
        {
            this.tableName = tableName;
            this.crtDataTable = crtDataTable;
            DictPocoColInfos = new Dictionary<string, PocoColInfo>();
            if (dictPocoColInfos != null)
            {
                foreach (var item in dictPocoColInfos)
                {
                    DictPocoColInfos.Add(item.Key, item.Value);
                }
            }
            this.excelTemplateColumns = excelTemplateColumns;
        }

        public List<DataTypeValidationResult> ValidateDataTable()
        {
            List<DataTypeValidationResult> dataTypeValidationResults = new List<DataTypeValidationResult>();

            foreach (DataRow dataRow in crtDataTable.Rows)
            {
                for (int i = 0; i < excelTemplateColumns.ColumnsCodes.Count; i++)
                {
                    string columnCode = excelTemplateColumns.ColumnsCodes[i].ToString();
                    string dataType = excelTemplateColumns.DataTypes[i].ToString();
                    string column = excelTemplateColumns.columns[i];

                    string actualValue = dataRow[columnCode].ToString();

                    if (!string.IsNullOrEmpty(actualValue))
                    {
                        IDataTypeFieldValidation dataTypeFieldValidation = DataTypeDataTableValidation.CreateDataTypeValidation(dataType);
                        if (dataTypeFieldValidation != null)
                        {
                            DataTypeValidationResult dataTypeValidationResult = dataTypeFieldValidation.Validate(tableName, columnCode, dataType, actualValue);
                            if (dataTypeValidationResult != null)
                            {
                                dataTypeValidationResults.Add(dataTypeValidationResult);
                            }

                        }
                    }
                }
            }
            return dataTypeValidationResults;
        }

        private static IDataTypeFieldValidation CreateDataTypeValidation(string dataType)
        {
            if (dataType == "Monetary")
                return new DataTypeMonetaryFieldValidation();
            else if (dataType == "String")
                return new DataTypeStringFieldValidation();
            else if (dataType == "Date")
                return new DataTypeDateFieldValidation();
            else if (dataType == "Integer")
                return new DataTypeIntegerFieldValidation();
            else if (dataType == "Decimal")
                return new DataTypeDecimalFieldValidation();
            else if (dataType == "Percentage")
                return new DataTypePercentageFieldValidation();
            else if (dataType == "Boolean")
                return new DataTypeBooleanFieldValidation();
            else if (dataType == "URI")
                return new DataTypeURIFieldValidation();
            else if (isEnumerationDataType(dataType))
                return new DataTypeEnumerationFieldValidation();
            return null;

        }
        public static bool isEnumerationDataType(string dataType)
        {
            return dataType.StartsWith("E:");
        }
    }
}
