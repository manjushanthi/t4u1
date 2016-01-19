using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mOrdinateCategorisation
    {
        public string TableCode { get; set; }
        public string AxisOrientation { get; set; }
        public string OrdinateCode { get; set; }
        public string OrdinateLabel { get; set; }
        public string DimensionCode { get; set; }
        public string MemberLabel { get; set; }
        public int TempID { get; set; }
        public string ActualLabel { get; set; }
        public string DomCode { get; set; }
    }
}
