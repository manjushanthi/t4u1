using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4UImportExportGenerator.Domain
{
    public class MAPPING
    {
        public int TABLE_ID { get; set; }
        public string DYN_TABLE_NAME { get; set; }
        public string DYN_TAB_COLUMN_NAME { get; set; }
        public string DIM_CODE { get; set; }
        public string DOM_CODE { get; set; }
        public string ORIGIN { get; set; }
        public int REQUIRED_MAPPINGS { get; set; }
        public string DATA_TYPE { get; set; }
        public bool IS_PAGE_COLUMN_KEY { get; set; }
        public bool IS_IN_TABLE { get; set; }


    }
}
