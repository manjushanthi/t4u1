using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucGenerator.Classes
{
    public class MergeTemplateInfo
    {
        public bool MergeTemplate { get; set; }
        public LocationRangeCalculator.eTypeOfMergedTemplate TypeOfMerge { get; set; }
        public bool TemplateInfirstCol { get; set; }
        public bool RangesIdentical { get; set; }
    }
}
