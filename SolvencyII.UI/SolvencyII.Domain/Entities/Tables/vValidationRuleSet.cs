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
    
    public partial class vValidationRuleSet
    {
        public long ModuleID { get; set; }
        public long ValidationRuleID { get; set; }
    
        public virtual vValidationRule vValidationRule { get; set; }
    }
}
