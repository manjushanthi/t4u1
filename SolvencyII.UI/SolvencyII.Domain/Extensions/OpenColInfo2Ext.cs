using System;
using System.Globalization;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Conversions;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Extension for OpenColInfo2
    /// Used with a previouus generation of data access (there have been 5 major changes so far - Mar 2015)
    /// </summary>
    public static class OpenColInfo2Ext
    {
        public static string SQLValue(this OpenColInfo2 colInfo2, int colNumber, OpenTableDataRow2 row2)
        {
            string value = row2.ColValues[colNumber];
            if (string.IsNullOrEmpty(value)) return "null";
            switch (colInfo2.ColType)
            {
                case "STRING":
                case "ENUMERATION/CODE":
                case "URI":
                    return string.Format("'{0}'", value);
                case "INTEGER":
                    if (!string.IsNullOrEmpty(value))
                    {
                        int result;
                        if (int.TryParse(value,NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                            return result.ToString("0", CultureInfo.InvariantCulture);    
                    }
                    return "";
                case "PERCENTAGE":
                //BRAG
                case "PERCENT":
                    return PercentageConvertor.PercentageStringToDBString(value);
                case "DECIMAL":
                    decimal temp4 = decimal.Parse(value, NumberStyles.Any, CultureInfo.CurrentCulture);
                    if (temp4 != 0)
                        return temp4.ToString("0.00000", CultureInfo.InvariantCulture);
                    if(string.IsNullOrEmpty(value))
                        return "";
                    return "0";
                case "MONETARY":
                    decimal temp3 = decimal.Parse(value, NumberStyles.Any, CultureInfo.CurrentCulture);
                    if (temp3 != 0)
                        return temp3.ToString("0.00", CultureInfo.InvariantCulture);
                    if(string.IsNullOrEmpty(value))
                        return "";
                    return "0";
                case "TRUE": //
                    return "1"; // Always true
                case "BOOLEAN": //
                    {
                        if (string.IsNullOrEmpty(value)) return "null";
                        return value == "1" ? "1" : "0";
                    }
                case "DATE": //
                    {
                        // Convert the string to a date - regionally correct
                        DateTime valueDate = DateTime.Parse(value, CultureInfo.CurrentCulture);
                        DateTime minDate = new DateTime(1753,01,01,0,0,0);
                        if (valueDate == minDate) return "null";
                        return string.Format("'{0}'", valueDate.ConvertToDateString());
                    }
                default:
                    return value;
            }

        }

        public static object Value(this OpenColInfo2 colInfo2, int colNumber, OpenTableDataRow2 row2)
        {
            string value = row2.ColValues[colNumber];
            if (string.IsNullOrEmpty(value)) return null;
            switch (colInfo2.ColType)
            {
                case "STRING":
                case "ENUMERATION/CODE":
                case "URI":
                    return value;
                case "INTEGER":
                    if (!string.IsNullOrEmpty(value))
                    {
                        int result;
                        if (int.TryParse(value, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                            return result.ToString("0", CultureInfo.InvariantCulture);
                    }
                    return null;
                case "PERCENTAGE":
                //BRAG
                case "PERCENT":
                    return PercentageConvertor.PercentageStringToDBString(value);
                case "DECIMAL":
                    decimal temp4 = decimal.Parse(value, NumberStyles.Any, CultureInfo.CurrentCulture);
                    if (temp4 != 0)
                        return temp4;
                    if (string.IsNullOrEmpty(value))
                        return null;
                    return "0";
                case "MONETARY":
                    decimal temp3 = decimal.Parse(value, NumberStyles.Any, CultureInfo.CurrentCulture);
                    if (temp3 != 0)
                        return temp3;
                    if (string.IsNullOrEmpty(value))
                        return null;
                    return "0";
                case "TRUE": //
                    return "1"; // Always true
                case "BOOLEAN": //
                    {
                        if (string.IsNullOrEmpty(value)) return null;
                        return value == "1";
                    }
                case "DATE": //
                    {
                        // Convert the string to a date - regionally correct
                        DateTime valueDate = DateTime.Parse(value, CultureInfo.CurrentCulture);
                        DateTime minDate = new DateTime(1753, 01, 01, 0, 0, 0);
                        if (valueDate == minDate) return null;
                        // There is an issue with sending a date parameter with SQLite
                        // The Export facility does not like dates and times.
                        // return valueDate; works properly but does not help the others
                        return valueDate.ToString("yyyy-MM-dd");
                        //return valueDate;
                    }
                default:
                    return value;
            }

        }
    }
}
