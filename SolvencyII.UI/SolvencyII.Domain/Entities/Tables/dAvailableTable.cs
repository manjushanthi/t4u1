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
    
    public partial class dAvailableTable
    {
        public long InstanceID { get; set; }
        public long TableID { get; set; }
        public string ZDimVal { get; set; }
        public Nullable<long> NumberOfRows { get; set; }
    
        public virtual mTable mTable { get; set; }
    }
}
