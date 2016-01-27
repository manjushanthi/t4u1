using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation.DataTypeFieldValidation
{
    internal class DataTypeDateFieldValidation : IDataTypeFieldValidation
    {
        public DataTypeValidationResult Validate(string tableName, string columnCode, string dataType, string value)
        {
            
            if (!IsDateTime(value))
            {
                return new DataTypeValidationResult
                {
                    TableName = tableName,
                    ColumnName = columnCode,
                    ColumnType = dataType,
                    ColumnValue = value,
                    Error = "Invalid data represented as Date"
                };
            }
            else
            {
                return null;
            }
        }
        private static bool IsDateTime(string value)
        {
                   
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime date;
            if (DateTime.TryParse(value, provider, DateTimeStyles.None, out date))
                return true;
            else
            return false;
            
        }
    }
}
