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
    
    public partial class aInterfaceComponent
    {
        public aInterfaceComponent()
        {
            this.aApplication = new HashSet<aApplication>();
        }
    
        public long InterfaceComponentID { get; set; }
        public string InBulgarian { get; set; }
        public string InCroatian { get; set; }
        public string InCzech { get; set; }
        public string InDanish { get; set; }
        public string InDutch { get; set; }
        public string InEnglish { get; set; }
        public string InEstonian { get; set; }
        public string InFinnish { get; set; }
        public string InFrench { get; set; }
        public string InGerman { get; set; }
        public string InGreek { get; set; }
        public string InHungarian { get; set; }
        public string InIrish { get; set; }
        public string InItalian { get; set; }
        public string InLatvian { get; set; }
        public string InLithuanian { get; set; }
        public string InMaltese { get; set; }
        public string InPolish { get; set; }
        public string InPortuguese { get; set; }
        public string InRomanian { get; set; }
        public string InSlovak { get; set; }
        public string InSlovenian { get; set; }
        public string InSpanish { get; set; }
        public string InSwedish { get; set; }
    
        public virtual ICollection<aApplication> aApplication { get; set; }
    }
}
