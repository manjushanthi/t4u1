//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AT2DPM.DAL.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class mOpenAxisValueRestriction
    {
        public long AxisID { get; set; }
        public long HierarchyID { get; set; }
        public Nullable<long> HierarchyStartingMemberID { get; set; }
        public Nullable<bool> IsStartingMemberIncluded { get; set; }
    
        public virtual mAxi mAxi { get; set; }
        public virtual mHierarchy mHierarchy { get; set; }
        public virtual mMember mMember { get; set; }
    }
}