using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.ExcelImportExportLib.Domain
{
    public class HierarchyAndMemberInfo
    {
        public long HierarchyID { get; set; }
        public string MemberLabel { get; set; }
        public string MemberXBRLCode { get; set; }
    }
}
