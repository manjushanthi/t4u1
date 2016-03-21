using System.Text.RegularExpressions;

namespace SolvencyII.Domain.Entities
{
    public class CellKey
    {
        private const string COLROWPRIMARYKEY_FORMAT = "x{0}_{1}_{2}";
        public int ColumnNumber { get; set; }
        public int RowNumber { get; set; }
        public long PrimaryKey { get; set; }

        public static string CellKeyToText(int columnNumber, int rowNumber, long primaryKey)
        {
            return BuildText(columnNumber, rowNumber, primaryKey);
        }
        public CellKey(int columnNumber, int rowNumber, long primaryKey)
        {
            ColumnNumber = columnNumber;
            RowNumber = rowNumber;
            PrimaryKey = primaryKey;
        }
        public CellKey(string cellName)
        {
            string working = cellName.Substring(1);
            string[] workArray = working.Split('_');
            if (workArray.Length == 3)
            {
                ColumnNumber = int.Parse(workArray[0]);
                RowNumber = int.Parse(workArray[1]);
                PrimaryKey = long.Parse(workArray[2]);
            }
            else
            {
                ColumnNumber = 0;
                RowNumber = 0;
                PrimaryKey = 0;
            }
        }
        public override string ToString()
        {
            return BuildText(ColumnNumber, RowNumber, PrimaryKey);
        }
        private static string BuildText(int columnNumber, int rowNumber, long primaryKey)
        {
            return string.Format(COLROWPRIMARYKEY_FORMAT, columnNumber, rowNumber, primaryKey);
        }

        public static string RegexWildCardForCol(int columnNumber)
        {
            string pattern = string.Format(COLROWPRIMARYKEY_FORMAT, columnNumber, "*", "*");
            return "^" + Regex.Escape(pattern).
                               Replace("\\*", ".*").
                               Replace("\\?", ".") + "$";
        }

        public static string RegexWildCardForRow(int rowNumber)
        {
            string pattern =  string.Format(COLROWPRIMARYKEY_FORMAT, "*", rowNumber, "*");
            return "^" + Regex.Escape(pattern).
                               Replace("\\*", ".*").
                               Replace("\\?", ".") + "$";
        }
    }
}
