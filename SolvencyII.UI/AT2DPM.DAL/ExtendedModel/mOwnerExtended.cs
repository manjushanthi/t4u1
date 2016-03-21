using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.ExtendedModel
{
    public class mOwnerExtended : AT2DPM.DAL.Model.mOwner
    {
        public string ConceptType { get; set; }
        public long LanguageID { get; set; }
    }
}
