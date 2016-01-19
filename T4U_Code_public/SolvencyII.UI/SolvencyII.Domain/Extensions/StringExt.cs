using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Domain wide string Extensions
    /// </summary>
    public static class StringEx
    {
        public static bool IsNumeric(this string str)
        {
            // Changed for Localisation issues.
            decimal check;
            // Uses local culture
            return decimal.TryParse(str, NumberStyles.Any, CultureInfo.CurrentCulture, out check);
        }

        public static Double DoubleValue(this string str)
        {
            double result = 0;
            if (!(string.IsNullOrEmpty(str)))
            {
                Double.TryParse(str, out result); // Uses local culture
            }
            return result;
        }

        public static DateTime? DateValue(this string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            DateTime myDate;
            if (DateTime.TryParse(str, out myDate))
                return myDate;
            return null;
        }

        public static bool IsInteger(this string str)
        {
            int value;
            return int.TryParse(str, NumberStyles.Any, CultureInfo.CurrentCulture, out value);
        }

        public static bool IsDate(this string str)
        {
            if (string.IsNullOrEmpty(str)) return true;
            DateTime myDate;
            return (DateTime.TryParse(str, out myDate));
        }

        public static bool IsBoolean(this string str)
        {
            if (string.IsNullOrEmpty(str)) return true;
            bool myBool;
            return (bool.TryParse(str, out myBool));
        }

        public static string FormatCurrency(this string str)
        {
            string newText;
            try
            {
                newText = string.Format("{0:N2}", str);
                // newText = string.Format("{0:#.00}", str);
            }
            catch (Exception)
            {
                newText = "-1";
            }
            return newText;
        }

        public static DateTime FormatSolvencyDateTime(this string dt)
        {
            try
            {
                return Convert.ToDateTime(dt);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static string MyJoin(this IEnumerable<string> str, string delimiter)
        {
            // .net 3.5
            string delimitedResult = str.Aggregate("", (current, s) => current + (s + delimiter));

            // .Net 4
            //string delimitedResult = string.Join("|", signatures.OrderBy(t => t));

            if (delimitedResult.Length > delimiter.Length)
            {
                // We will have a delimiter at the end of the text so remove it.
                delimitedResult = delimitedResult.Substring(0, delimitedResult.Length - delimiter.Length);
            }


            return delimitedResult;
        }

        public static Type GetColumnType(this string str)
        {

            switch (str)
            {
                case "p":
                    // Percent
                    return typeof(decimal);
                case "m":
                    // Monetary
                    return typeof(decimal);
                case "d":
                    // Date
                    return typeof(DateTime);
                case "b":
                    // Boolean
                    return typeof(bool);
                case "i":
                    // Integer
                    return typeof(int);
                case "e":
                // String (URI)
                case "s":
                // String
                default:
                    return typeof(string);
            }

        }

        public static string FormatValueForUser(this string str, string colType)
        {
            switch (colType)
            {
                case "p":
                    // Percent
                    double resultDoub2;
                    if (double.TryParse(str, out resultDoub2))
                        return string.Format("{0:N}", resultDoub2);
                    break;
                case "m":
                    // Monetary
                    double resultDoub;
                    if (double.TryParse(str, out resultDoub))
                        return string.Format("{0:N2}", resultDoub);
                    break;
                case "d":
                    // Date
                    DateTime result;
                    if (DateTime.TryParse(str, out result))
                        return result.ToShortDateString();
                    break;
                case "b":
                    // Boolean
                    bool resultBool;
                    if (bool.TryParse(str, out resultBool))
                        return resultBool.ToString();
                    break;
                case "i":
                    // Integer
                    int resultInt;
                    if (int.TryParse(str, out resultInt))
                        return string.Format("{0:N}", resultInt);
                    break;
                case "e":
                // String (URI)
                case "s":
                // String
                default:
                    return str;
            }
            return str;
        }

        public static string FormatValueForDB(this string str, string colType)
        {
            return str;
        }

        public static int? GetIntValue(this string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            int result;
            if (int.TryParse(str, out result))
            {
                return result;
            }
            return null;
        }

        public static decimal? GetDecimalValue(this string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            decimal result;
            if (decimal.TryParse(str, out result))
            {
                return result;
            }
            return null;
        }
    }
}
