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
    
    public partial class mReportingFramework
    {
        public mReportingFramework()
        {
            this.mTaxonomies = new HashSet<mTaxonomy>();
        }
    
        public long FrameworkID { get; set; }
        public string FrameworkCode { get; set; }
        public string FrameworkLabel { get; set; }
        public Nullable<long> ConceptID { get; set; }
    
        public virtual mConcept mConcept { get; set; }
        public virtual ICollection<mTaxonomy> mTaxonomies { get; set; }
    }
}
