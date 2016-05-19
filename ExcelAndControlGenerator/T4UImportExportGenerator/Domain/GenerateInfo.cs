using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4UImportExportGenerator.Domain
{
    public class GenerateInfo
    {
        public long ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleLabel { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public Dictionary<string, long> TableCodes { get; set; }
    }
}
