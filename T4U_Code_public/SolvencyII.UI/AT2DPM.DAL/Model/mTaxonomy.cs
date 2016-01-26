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
    
    public partial class mTaxonomy
    {
        public mTaxonomy()
        {
            this.mModules = new HashSet<mModule>();
            this.mTaxonomyTables = new HashSet<mTaxonomyTable>();
            this.mTemplateOrTables = new HashSet<mTemplateOrTable>();
        }
    
        public long TaxonomyID { get; set; }
        public Nullable<long> FrameworkID { get; set; }
        public string TaxonomyCode { get; set; }
        public string TaxonomyLabel { get; set; }
        public string Version { get; set; }
        public Nullable<System.DateTime> PublicationDate { get; set; }
        public string TechnicalStandard { get; set; }
        public Nullable<long> ConceptID { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
    
        public virtual mConcept mConcept { get; set; }
        public virtual ICollection<mModule> mModules { get; set; }
        public virtual mReportingFramework mReportingFramework { get; set; }
        public virtual ICollection<mTaxonomyTable> mTaxonomyTables { get; set; }
        public virtual ICollection<mTemplateOrTable> mTemplateOrTables { get; set; }
    }
}