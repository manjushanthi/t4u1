using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.ExcelTemplateProcessor
{
    public class MDOrdinateHierarchyIdList
    {
        #region Properties

        private static DataTable _MDOrdinateHierarchyIdTable;

        public static DataTable MDOrdinateHierarchyIdTable
        {
            get
            {
                return _MDOrdinateHierarchyIdTable;
            }
            set
            {
                _MDOrdinateHierarchyIdTable = value;
            }
        }

        #endregion

        #region Construct

        public MDOrdinateHierarchyIdList()
        {

        }

        #endregion
    }
}
