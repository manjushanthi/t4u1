using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.ExcelImportExportLib.Domain
{
    public class ExcelExportValidationMessage 
    {
        public string TableCode { get; set; }
        public string FieldType { get; set; }
        public string Value { get; set; }
       
    }
}
