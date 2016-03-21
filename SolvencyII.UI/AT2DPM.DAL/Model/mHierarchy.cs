//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AT2DPM.DAL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class mHierarchy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mHierarchy()
        {
            this.mHierarchyNodes = new HashSet<mHierarchyNode>();
            this.mMetrics = new HashSet<mMetric>();
            this.mOpenAxisValueRestrictions = new HashSet<mOpenAxisValueRestriction>();
        }
    
        public long HierarchyID { get; set; }
        public string HierarchyCode { get; set; }
        public string HierarchyLabel { get; set; }
        public Nullable<long> DomainID { get; set; }
        public string HierarchyDescription { get; set; }
        public Nullable<long> ConceptID { get; set; }
    
        public virtual mConcept mConcept { get; set; }
        public virtual mDomain mDomain { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mHierarchyNode> mHierarchyNodes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mMetric> mMetrics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mOpenAxisValueRestriction> mOpenAxisValueRestrictions { get; set; }
    }
}
