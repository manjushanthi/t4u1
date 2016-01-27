using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation.DataTypeFieldValidation
{
    internal interface IDataTypeFieldValidation
    {
        DataTypeValidationResult Validate(string tableName, string columnCode, string dataType, string Value);
    }
}
