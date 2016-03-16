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
    
    public partial class mTableCell
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public mTableCell()
        {
            this.mAxisOrdinates = new HashSet<mAxisOrdinate>();
            this.mReferences = new HashSet<mReference>();
        }
    
        public long CellID { get; set; }
        public Nullable<long> TableID { get; set; }
        public Nullable<bool> IsRowKey { get; set; }
        public Nullable<bool> IsShaded { get; set; }
        public string BusinessCode { get; set; }
        public string DatapointSignature { get; set; }
    
        public virtual mTable mTable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mAxisOrdinate> mAxisOrdinates { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<mReference> mReferences { get; set; }
    }
}
