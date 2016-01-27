using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mMetric
    {
        public string Owner { get; set; }
        public string DomainCode { get; set; }
        public string MetricDomainCode { get; set; }
        public string Label { get; set; }
        public string Name { get; set; }
        public string HierarchyCode { get; set; }
        public string MemberXBRLCode { get; set; }
        public string StartingMemberText { get; set; }
        public string ConceptType { get; set; }
        public long MemberID { get; set; }
        public long DomainID { get; set; }
        public long MetricDomainID { get; set; }
        public long OwnerID { get; set; }
        public long ConceptID { get; set; }

        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
