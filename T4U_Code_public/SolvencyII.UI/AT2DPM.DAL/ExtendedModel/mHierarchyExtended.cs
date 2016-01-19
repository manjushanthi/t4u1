using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mHierarchy
    {
        public string DomainCode { get; set; }
        public string Owner { get; set; }
        public int TempHierarchyID { get; set; }
        public string ConceptType { get; set; }
        public long OwnerID { get; set; }
        public long LanguageID { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
