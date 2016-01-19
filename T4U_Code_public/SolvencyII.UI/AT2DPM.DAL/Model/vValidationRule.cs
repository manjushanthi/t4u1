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
    
    public partial class vValidationRule
    {
        public vValidationRule()
        {
            this.vExpressions = new HashSet<vExpression>();
            this.mModules = new HashSet<mModule>();
            this.mTables = new HashSet<mTable>();
        }
    
        public long ValidationRuleID { get; set; }
        public string ValidationCode { get; set; }
        public string Severity { get; set; }
        public string Scope { get; set; }
        public string ValidationType { get; set; }
        public Nullable<long> ExpressionID { get; set; }
        public Nullable<long> ConceptID { get; set; }
    
        public virtual mConcept mConcept { get; set; }
        public virtual vExpression vExpression { get; set; }
        public virtual ICollection<vExpression> vExpressions { get; set; }
        public virtual ICollection<mModule> mModules { get; set; }
        public virtual ICollection<mTable> mTables { get; set; }
    }
}
