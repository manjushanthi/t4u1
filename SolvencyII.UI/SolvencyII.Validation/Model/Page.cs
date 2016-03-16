using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain;

namespace SolvencyII.Validation.Model
{
    public class Page
    {
        public string PageCode { get; set; }
        public string DimensionXBRLCode { get; set; }
        public string MemberText { get; set; }
        public mDimension Dimension { get; set; }
        public mMember Member { get; set; }
        
    }
}
