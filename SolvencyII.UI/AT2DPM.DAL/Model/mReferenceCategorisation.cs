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
    
    public partial class mReferenceCategorisation
    {
        public long ReferenceID { get; set; }
        public long DimensionID { get; set; }
        public Nullable<long> MemberID { get; set; }
    
        public virtual mDimension mDimension { get; set; }
        public virtual mMember mMember { get; set; }
        public virtual mReference mReference { get; set; }
    }
}
