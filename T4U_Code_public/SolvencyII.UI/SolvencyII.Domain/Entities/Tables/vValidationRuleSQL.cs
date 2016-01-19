using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Domain
{
    public class vValidationRuleSQL
    {
        public long ValidationRuleID { get; set; }
        public string SQL { get; set; }
        public string CELLS { get; set; }
        public string Severity { get; set; }
    }
}
