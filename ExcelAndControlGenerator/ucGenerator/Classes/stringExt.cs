using System.Collections.Generic;
using System.Linq;
using SolvencyII.Domain;

namespace ucGenerator
{
    internal static class stringExt
    {
        public static string TypeConversion(this string sQLiteType, bool notNull, MAPPING tableMapping)
        {

            switch (sQLiteType)
            {
                case "INTEGER":
                    return "long" + (!notNull ? "?" : "");
                case "NUMERIC":
                case "REAL":
                    // Confirm number type - Integers are Numeric;
                    if(tableMapping != null && tableMapping.DATA_TYPE.ToUpper() == "I")
                        return "long" + (!notNull ? "?" : "");
                    return "decimal" + (!notNull ? "?" : "");
                case "DATE":
                case "DATETIME":
                    return "DateTime" + (!notNull ? "?" : "");
                case "BOOLEAN":
                    return "bool" + (!notNull ? "?" : "");
                case "VARCHAR":
                case "TEXT":
                    return "string";
                case "BLOB":
                    return "byte[]";
                default:
                    return "string";
            }

        }
    }
}
