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
    
    public partial class mLanguage
    {
        public mLanguage()
        {
            this.mConceptTranslation = new HashSet<mConceptTranslation>();
        }
    
        public long LanguageID { get; set; }
        public string LanguageName { get; set; }
        public string EnglishName { get; set; }
        public string IsoCode { get; set; }
        public Nullable<long> ConceptID { get; set; }
    
        public virtual mConcept mConcept { get; set; }
        public virtual ICollection<mConceptTranslation> mConceptTranslation { get; set; }
    }
}
