using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mAxi 
    {
        
        public string OwnerCode{get;set;}
        public string TableCode{get;set;}
        //public string[] TableCodes{ get; set; }
        public string ConceptType { get; set; }
        public long LanguageID { get; set; }
        //public long ConceptID { get; set; }
        public long AxisOrder { get; set; }
        public long OwnerID { get; set; }
        public long TableID { get; set; }
    }
}
