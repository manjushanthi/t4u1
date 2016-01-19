using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mOpenAxisValueRestriction
    {
        public string AxisLabel { get; set; }
        public string TableCode { get; set; }
        public string HierarchyCode { get; set; }
        public string StartingMemberText { get; set; }
    }
}
