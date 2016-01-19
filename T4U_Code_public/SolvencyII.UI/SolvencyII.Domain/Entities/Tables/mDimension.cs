//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SolvencyII.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class mDimension
    {
        public mDimension()
        {
            this.mOrdinateCategorisation = new HashSet<mOrdinateCategorisation>();
            this.mReferenceCategorisation = new HashSet<mReferenceCategorisation>();
        }
    
        public long DimensionID { get; set; }
        public string DimensionLabel { get; set; }
        public string DimensionCode { get; set; }
        public string DimensionDescription { get; set; }
        public string DimensionXBRLCode { get; set; }
        public Nullable<long> DomainID { get; set; }
        public Nullable<bool> IsTypedDimension { get; set; }
        public Nullable<long> ConceptID { get; set; }
    
        public virtual mConcept mConcept { get; set; }
        public virtual mDomain mDomain { get; set; }
        public virtual ICollection<mOrdinateCategorisation> mOrdinateCategorisation { get; set; }
        public virtual ICollection<mReferenceCategorisation> mReferenceCategorisation { get; set; }
    }
}
