using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SolvencyII.ExcelImportExportLib.Exceptions
{
    public class T4UExcelImportExportException : Exception
    {
        /// <summary>
        /// Excel range address where error occured
        /// </summary>
        public string ExcelAddress { get; set; }

        public T4UExcelImportExportException(
            string auxMessage, Exception inner, string excelAddress = null) :
            base(auxMessage, inner)
        {
            ExcelAddress = excelAddress;

        }
    }
}
