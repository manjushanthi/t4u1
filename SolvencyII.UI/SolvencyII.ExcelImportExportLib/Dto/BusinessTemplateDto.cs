using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

using SolvencyII.ExcelImportExportLib.Domain;

namespace SolvencyII.ExcelImportExportLib.Dto
{
    public class BusinessTemplateDto : AbstractTransferObject
    {
        public Range TableDataRange { get; set; }
        public Range FilterRange { get; set; }
        public Range XFilterRange { get; set; }
        public Range YFilterRange { get; set; }

        public string[,] HeaderData { get; set; }
        public string[,] TableData { get; set; }
        public string[,] FilterData { get; set; }
        public string[,] XFilterData { get; set; }
        public string[,] YFilterData { get; set; }

        public IList<object> CRTData { get; set; }

        public object CurrentObject { get; set; }

        public TableType TypeOfTable { get; set; }

        public int TableID { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(TableDataRange != null)
            {
                TableDataRange.DisposeChildInstances();
                TableDataRange.Dispose();
                TableDataRange = null;
            }

            if(FilterRange != null)
            {
                FilterRange.DisposeChildInstances();
                FilterRange.Dispose();
                FilterRange = null;
            }

            if (HeaderData != null)
            {
                HeaderData = null;
            }

            if (TableData != null)
            {
                TableData = null;
            }

            if(FilterData != null)
            {
                FilterData = null;
            }

            if (CRTData != null)
                CRTData = null;
        }
        
    }
}
