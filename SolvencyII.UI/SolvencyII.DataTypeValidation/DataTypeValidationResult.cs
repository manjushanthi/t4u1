using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.DataTypeValidation
{
    public class DataTypeValidationResult
    {
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
        public string ColumnValue { get; set; }
        public string Value { get; set; }
        public string Error { get; set; }
    }

}
