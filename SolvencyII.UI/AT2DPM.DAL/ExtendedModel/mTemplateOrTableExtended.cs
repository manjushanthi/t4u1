using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mTemplateOrTable
    {
        public string OwnerCode { get; set; }
        public string ConceptType { get; set; }
        public string LanguageName { get; set; }
        public string TaxonomyCode { get; set; }
        public string ParentTemplateOrTableCode { get; set; }
    }
}
