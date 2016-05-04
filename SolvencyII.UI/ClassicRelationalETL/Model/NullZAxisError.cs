using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSolvencyII.Data.CRT.ETL.Model
{
    class NullZAxisError : CrtError
    {
        public NullZAxisError(string crtTableName, string columnName, int InstanceID) : base()
        {
            base.InstanceID = InstanceID;
            base.Message = string.Format("Null value of Z axis for dimension {0} in table {1}", extractDimension(columnName), extractTable(crtTableName));
        }

        private string extractDimension(string columnName)
        {
            string result = columnName;
            result = columnName.Replace("PAGE", "");
            return result;
        }

        private string extractTable(string tableName)
        {
            string result = tableName;
            result = result.Replace("__", " ").Replace("_", ".");
            return result;
        }
    }
}
