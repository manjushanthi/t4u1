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
    
    public partial class vwGetTreeDataZDimChildren
    {
        public long ModuleID { get; set; }
        public long FrameworkID { get; set; }
        public long TemplateOrTableID { get; set; }
        public long TableID { get; set; }
        public string FrameworkCode { get; set; }
        public string TaxonomyCode { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleLabel { get; set; }
        public string TemplateVariant { get; set; }
        public string TemplateVariantLabel { get; set; }
        public string BusinessTable { get; set; }
        public string BusinessTableLocationRange { get; set; }
        public string BusinessTableLabel { get; set; }
        public string AnnotatedTable { get; set; }
        public string AnnotatedTableLocationRange { get; set; }
        public string AnnotatedTableLabel { get; set; }
        public string TableCode { get; set; }
        public long AxisID { get; set; }
        public long SingleZOrdinateID { get; set; }
        public string TableLabel { get; set; }
        public long BusinessOrder { get; set; }
        public Nullable<long> TemplateOrder { get; set; }
        public Nullable<long> TemplateOrder2 { get; set; }
        public Nullable<long> AxisOrdinateOrder { get; set; }
    }
}
