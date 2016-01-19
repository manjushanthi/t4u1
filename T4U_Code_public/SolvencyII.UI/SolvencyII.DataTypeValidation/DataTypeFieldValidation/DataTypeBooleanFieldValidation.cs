using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation.DataTypeFieldValidation
{
    internal class DataTypeBooleanFieldValidation : IDataTypeFieldValidation
    {
        public DataTypeValidationResult Validate(string tableName, string columnCode, string dataType, string value)
        {
            if (!isBoolean(value))
            {
                return new DataTypeValidationResult
                {
                    TableName = tableName,
                    ColumnName = columnCode,
                    ColumnType = dataType,
                    ColumnValue = value,
                    Error = "Invalid data represented as Boolean"
                };
            }
            else
            {
                return null;
            }
        }

        public static bool isBoolean(string val)
        {
            if (!string.IsNullOrEmpty(val))
            {
                if (val.Trim() == "1" || val.Trim() == "0")
                    return true;
                else
                    return false;
            }
            else
                return false;
           
        }
    }
}
