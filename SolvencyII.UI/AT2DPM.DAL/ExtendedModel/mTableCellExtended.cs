using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mTableCell
    {
        public string TableCode { get; set; }
        public int TempOrder { get; set; }
        public ArrayList XOrdinateCode { get; set; }
        public ArrayList YOrdinateCode { get; set; }
        public ArrayList ZOrdinateCode { get; set; }

    }
}
