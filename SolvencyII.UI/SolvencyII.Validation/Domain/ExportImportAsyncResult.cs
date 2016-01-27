using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Validation.Domain
{
    public class ExportImportAsyncResult
    {
        public string result { get; set; }
        public ArelleValidationDisplayType operation { get; set; }
        
        public ExportImportAsyncResult(string _result, ArelleValidationDisplayType _operation)
        {
            result=_result;
            operation = _operation;
        }
    }
}
