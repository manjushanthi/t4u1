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
    
    public partial class mConceptualModule
    {
        public mConceptualModule()
        {
            this.mModules = new HashSet<mModule>();
        }
    
        public long ConceptualModuleID { get; set; }
        public string ConceptualModuleCode { get; set; }
        public string ConceptualModuleLabel { get; set; }
    
        public virtual ICollection<mModule> mModules { get; set; }
    }
}