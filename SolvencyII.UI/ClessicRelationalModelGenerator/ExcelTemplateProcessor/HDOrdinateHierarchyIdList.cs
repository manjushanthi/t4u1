using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.ExcelTemplateProcessor
{
    public class HDOrdinateHierarchyIdList
    {
        #region Properties

        private static DataTable _HDOrdinateHierarchyIdListTable;

        public static DataTable HDOrdinateHierarchyIdListTable
        {
            get
            {
                return _HDOrdinateHierarchyIdListTable;
            }
            set
            {
                _HDOrdinateHierarchyIdListTable = value;
            }
        }

        #endregion

        #region Construct

        public  HDOrdinateHierarchyIdList()
        {

        }

        #endregion
    }
}
