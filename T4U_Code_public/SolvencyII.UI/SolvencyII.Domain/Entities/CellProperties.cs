using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Domain.Entities
{
    public class CellProperties
    {
        public long TableID { get; set; }
        public string TableCode { get; set; }
        public long AxisID { get; set; }
        public string AxisOrientation { get; set; }
        public int OrdinateID { get; set; }
        public string OrdinateLabel { get; set; }
        public string OrdinateCode { get; set; }
        public string DimensionLabel { get; set; }
        public string DimensionCode { get; set; }
        public string DimensionMemberSignature { get; set; }
        public string Source { get; set; }
        public string MemberCode { get; set; }
        public string MemberLabel { get; set; }
    }
}
