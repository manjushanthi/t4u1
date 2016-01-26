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
    
    public partial class mReference
    {
        public mReference()
        {
            this.mReferenceCategorisations = new HashSet<mReferenceCategorisation>();
            this.mTableCells = new HashSet<mTableCell>();
            this.mConcepts = new HashSet<mConcept>();
        }
    
        public long ReferenceID { get; set; }
        public string SourceCode { get; set; }
        public string Article { get; set; }
        public string Paragraph { get; set; }
        public string Point { get; set; }
        public string Romans { get; set; }
        public Nullable<long> ReferenceText { get; set; }
    
        public virtual ICollection<mReferenceCategorisation> mReferenceCategorisations { get; set; }
        public virtual ICollection<mTableCell> mTableCells { get; set; }
        public virtual ICollection<mConcept> mConcepts { get; set; }
    }
}