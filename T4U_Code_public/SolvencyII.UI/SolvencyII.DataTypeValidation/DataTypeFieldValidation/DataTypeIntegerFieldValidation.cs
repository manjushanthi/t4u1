using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation.DataTypeFieldValidation
{
    internal class DataTypeIntegerFieldValidation : IDataTypeFieldValidation
    {
        public DataTypeValidationResult Validate(string tableName, string columnCode, string dataType, string value)
        {
            if (!isInteger(value))
            {
                return new DataTypeValidationResult
                {
                    TableName = tableName,
                    ColumnName = columnCode,
                    ColumnType = dataType,
                    ColumnValue = value,
                    Error = "Invalid data represented as Integer"
                };
            }
            else
            {
                return null;
            }
        }

        public static bool isInteger(string val)
        {
            int intVal;
            if (int.TryParse(val, out intVal))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
