using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Validation.Domain
{
    public class ValidationMessage
    {
        public long MessageID { get; set; }
        public long SequenceInReport { get; set; }
        public string DataPointSignature { get; set; }
        public string MessageCode { get; set; }
        public string MessageLevel { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}
