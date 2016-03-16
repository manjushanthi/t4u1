using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation.DataTypeFieldValidation
{
    internal class DataTypeMonetaryFieldValidation : IDataTypeFieldValidation
    {
        public DataTypeValidationResult Validate(string tableName, string columnCode, string dataType, string value)
        {
            if (!isDecimal(value))
            {
                return new DataTypeValidationResult
                {
                    TableName = tableName,
                    ColumnName = columnCode,
                    ColumnType = dataType,
                    ColumnValue = value,
                    Error = "Invalid data represented as Monetary"
                };
            }
            else
            {
                return null;
            }
        }

        public static bool isDecimal(string val)
        {
            decimal convertedValue;
            if (!decimal.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture.NumberFormat, out convertedValue))
            {
                return false;
            }
            else
                return true;
        }
    }
}
