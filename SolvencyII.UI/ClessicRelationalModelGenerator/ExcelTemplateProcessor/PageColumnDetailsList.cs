using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.ExcelTemplateProcessor
{
    public class PageColumnDetailsList
    {
        #region Properties

        private static DataTable _PageColumnDetailsTable;

        public static DataTable PageColumnDetailsTable
        {
            get
            {
                return _PageColumnDetailsTable;
            }
            set
            {
                _PageColumnDetailsTable = value;
            }
        }

        #endregion

        #region Construct

        public  PageColumnDetailsList()
        {

        }

        #endregion
    }
}
