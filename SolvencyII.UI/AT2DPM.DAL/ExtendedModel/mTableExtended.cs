using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mTable
    {
        public string OwnerCode { get; set; }
        public string ConceptType { get; set; }
        public long LanguageID { get; set; }
        public long OwnerID { get; set; }

    }
}
