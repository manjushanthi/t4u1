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
    
    public partial class mModuleBusinessTemplate
    {
        public long ModuleID { get; set; }
        public long Order { get; set; }
        public Nullable<long> BusinessTemplateID { get; set; }
    
        public virtual mModule mModule { get; set; }
    }
}