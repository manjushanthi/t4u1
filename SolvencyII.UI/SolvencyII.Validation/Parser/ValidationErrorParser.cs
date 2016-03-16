using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using SolvencyII.Validation.Model;

namespace SolvencyII.Validation.Parser
{
    public class ValidationErrorParser
    {
        ValidationError validationError;

        private const string rowColPttrn = @"[r|R|P|p]{1,2}\d{1,4}[c|C|p|P]{1,2}\d{1,4}";
        private const string colPttrn = @"[c|C]\d{1,4}";
        public ValidationErrorParser(ValidationError ve)
        {
            validationError = ve;
        }

        public string GetTemplateName()
        {
            return null;
        }

        public string[] GetCellCodes()
        {
            return ParseCells(validationError.Cells, rowColPttrn);
        }

        public string[] GetCellCodes(string tableCode)
        {
            //string tableCellPttrn = tableCode.Trim() + @"\.[r|R]\d{1,4}[c|C]\d{1,4}";
            string tableCellPttrn = tableCode.Trim() + @"\.[r|R|P|p]{1,2}\d{1,4}[c|C|p|P]{1,2}\d{1,4}";
            Regex regEx = new Regex(tableCellPttrn);
            MatchCollection mc = regEx.Matches(validationError.Cells);

            List<string> tblCellCodes = new List<string>(mc.Count);

            foreach (Match m in mc)
            {
                string[] cellCode = ParseCells(m.Value, rowColPttrn);
             
                if (cellCode != null)
                    tblCellCodes.AddRange(cellCode);

            }

            return tblCellCodes.ToArray<string>();
        }

        public string[] GetOpenCellCodes(string tableCode)
        {
            string tableCellPttrn = tableCode.Trim() + @".[c|C]\d{1,4}";
            Regex regEx = new Regex(tableCellPttrn);           

            MatchCollection mc = regEx.Matches(validationError.Cells);

            List<string> tblCellCodes = new List<string>(mc.Count);

            foreach (Match m in mc)
            {

                string[] cellCode = ParseCells(m.Value, colPttrn);

                if (cellCode != null)
                    tblCellCodes.AddRange(cellCode);

            }

            return tblCellCodes.ToArray<string>();
        }

        public string[] GetTableCodes()
        {
            string tblCodePttrn = @"([A-Z]|[a-z]){1,3}(.\d{2}){4}";
            Regex regEx = new Regex(tblCodePttrn, RegexOptions.IgnoreCase);

            string[] tableCode = null;

            if (validationError != null)
            {
                MatchCollection mc = regEx.Matches(validationError.Cells);

                tableCode = new string[mc.Count];

                int i = 0;
                foreach (Match m in mc)
                    tableCode[i++] = m.Value;

            }

            return tableCode.Distinct().ToArray();
        }

        /// <summary>
        /// Each ‘PAGE’ start new dimensional characteristic
        ///First colon ‘:’ in dimensional characteristic splits it into two components (dimension and member)
        ///First component is dimension. To query dimension name, underscore ‘_’ has to be replaced by ‘_dim:’
        ///Second is member. If member contains colon ‘:’, it is to be queried in DB, if not, it is representing typed value
        /// </summary>
        /// <returns></returns>
        public Page[] GetPagesFromContext()
        {
            string strError = "An error occured while parsing context information: " + validationError.Context;
            Page[] pages = null;

            if (validationError.Context == null) return null;

            string[] splitPages = validationError.Context.Trim().Split(new string[] {"PAGE"}, StringSplitOptions.RemoveEmptyEntries);

            //Throw an exception 
            if (splitPages == null || splitPages.Count() == 0) throw new ValidationException(strError);

            pages = new Page[splitPages.Count()];

            int i = 0;
            foreach (string p in splitPages)
            {
                string[] splitDim = p.Split(new char[] { ':' });

                int semi = p.IndexOf(':');

                if (semi == 0 || semi < 0) throw new ValidationException(strError);

                pages[i++] = new Page
                {
                    PageCode = "PAGE" + p.Substring(0, semi).ToUpper(),
                    DimensionXBRLCode = p.Substring(0, semi).Replace("_", "_dim:"),
                    MemberText = p.Substring(semi + 1).Trim()
                };
            }

            return pages;
        }


        private string[] ParseCells(string cells, string cellCodePattern)
        {
            Regex regEx = new Regex(cellCodePattern);

            string[] cellCode = null;

            MatchCollection mc = regEx.Matches(cells);

            cellCode = new string[mc.Count];

            int i = 0;
            foreach (Match m in mc)
                cellCode[i++] = m.Value;


            return cellCode;
        }
    }
}
