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
    
    public partial class mModule
    {
        public mModule()
        {
            this.mModuleBusinessTemplate = new HashSet<mModuleBusinessTemplate>();
            this.mTableDimensionSet = new HashSet<mTableDimensionSet>();
            this.vValidationRule = new HashSet<vValidationRule>();
        }
    
        public long ModuleID { get; set; }
        public Nullable<long> TaxonomyID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleLabel { get; set; }
        public Nullable<long> ConceptualModuleID { get; set; }
        public string DefaultFrequency { get; set; }
        public Nullable<long> ConceptID { get; set; }
        public string XBRLSchemaRef { get; set; }
    
        public virtual mConceptualModule mConceptualModule { get; set; }
        public virtual mTaxonomy mTaxonomy { get; set; }
        public virtual ICollection<mModuleBusinessTemplate> mModuleBusinessTemplate { get; set; }
        public virtual ICollection<mTableDimensionSet> mTableDimensionSet { get; set; }
        public virtual ICollection<vValidationRule> vValidationRule { get; set; }
    }
}
