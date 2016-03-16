using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.ExcelImportExportLib.Domain
{
    public class OrdinateHierarchy
    {
        public string TableCode { get; set; }
        public int OrdinateID { get; set; }
        public string OrdinateCode { get; set; }
        public long HierarchyID { get; set; }
        public long HierarchyStartingMemberID { get; set; }
        public bool IsStartingMemberIncluded { get; set; }
        public string PageColumn { get; set; }
    }
}
