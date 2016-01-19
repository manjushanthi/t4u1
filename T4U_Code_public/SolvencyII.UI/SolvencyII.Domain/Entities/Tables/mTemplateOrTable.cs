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
    
    public partial class mTemplateOrTable
    {
        public mTemplateOrTable()
        {
            this.mTaxonomyTable = new HashSet<mTaxonomyTable>();
            this.mTemplateOrTable1 = new HashSet<mTemplateOrTable>();
        }
    
        public long TemplateOrTableID { get; set; }
        public Nullable<long> TaxonomyID { get; set; }
        public string TemplateOrTableCode { get; set; }
        public string TemplateOrTableLabel { get; set; }
        public string TemplateOrTableType { get; set; }
        public Nullable<long> Order { get; set; }
        public Nullable<long> Level { get; set; }
        public Nullable<long> ParentTemplateOrTableID { get; set; }
        public Nullable<long> ConceptID { get; set; }
        public string TC { get; set; }
        public string TT { get; set; }
        public string TL { get; set; }
        public string TD { get; set; }
        public string YC { get; set; }
        public string XC { get; set; }
    
        public virtual mConcept mConcept { get; set; }
        public virtual mTaxonomy mTaxonomy { get; set; }
        public virtual ICollection<mTaxonomyTable> mTaxonomyTable { get; set; }
        public virtual ICollection<mTemplateOrTable> mTemplateOrTable1 { get; set; }
        public virtual mTemplateOrTable mTemplateOrTable2 { get; set; }
    }
}
