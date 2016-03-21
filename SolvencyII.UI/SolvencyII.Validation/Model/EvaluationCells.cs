using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Validation.Model
{
    public class EvaluationCells
    {
        public int EvalNr { get; set; }
        public string Cells { get; set; }
        public long ValidationRuleId { get; set; }
        public long TableId { get; set; }
    }
}
