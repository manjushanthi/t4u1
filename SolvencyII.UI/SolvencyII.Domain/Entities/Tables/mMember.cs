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
    
    public partial class mMember
    {
        public mMember()
        {
            this.mHierarchyNode = new HashSet<mHierarchyNode>();
            this.mMetric = new HashSet<mMetric>();
            this.mOrdinateCategorisation = new HashSet<mOrdinateCategorisation>();
            this.mReferenceCategorisation = new HashSet<mReferenceCategorisation>();
        }
    
        public long MemberID { get; set; }
        public Nullable<long> DomainID { get; set; }
        public string MemberCode { get; set; }
        public string MemberLabel { get; set; }
        public string MemberXBRLCode { get; set; }
        public Nullable<bool> IsDefaultMember { get; set; }
        public Nullable<long> ConceptID { get; set; }
    
        public virtual mConcept mConcept { get; set; }
        public virtual mDomain mDomain { get; set; }
        public virtual ICollection<mHierarchyNode> mHierarchyNode { get; set; }
        public virtual ICollection<mMetric> mMetric { get; set; }
        public virtual ICollection<mOrdinateCategorisation> mOrdinateCategorisation { get; set; }
        public virtual ICollection<mReferenceCategorisation> mReferenceCategorisation { get; set; }
    }
}
