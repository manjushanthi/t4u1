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
    
    public partial class mDimension
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mDimension()
        {
            this.dInstanceLargeDimensionMembers = new HashSet<dInstanceLargeDimensionMember>();
            this.mOrdinateCategorisations = new HashSet<mOrdinateCategorisation>();
            this.mReferenceCategorisations = new HashSet<mReferenceCategorisation>();
            this.mModules = new HashSet<mModule>();
        }
    
        public long DimensionID { get; set; }
        public string DimensionLabel { get; set; }
        public string DimensionCode { get; set; }
        public string DimensionDescription { get; set; }
        public string DimensionXBRLCode { get; set; }
        public Nullable<long> DomainID { get; set; }
        public Nullable<bool> IsTypedDimension { get; set; }
        public Nullable<long> ConceptID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dInstanceLargeDimensionMember> dInstanceLargeDimensionMembers { get; set; }
        public virtual mConcept mConcept { get; set; }
        public virtual mDomain mDomain { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mOrdinateCategorisation> mOrdinateCategorisations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mReferenceCategorisation> mReferenceCategorisations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mModule> mModules { get; set; }
    }
}
