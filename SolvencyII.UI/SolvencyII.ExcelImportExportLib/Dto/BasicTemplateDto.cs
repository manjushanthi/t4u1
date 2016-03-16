using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

namespace SolvencyII.ExcelImportExportLib.Dto
{
    public class BasicTemplateDto : AbstractTransferObject
    {
        public Range HeaderRange { get; set; }

        public string[,] HeaderData{get;set;}
        public string[,] TableData { get; set; }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (HeaderRange != null)
            {
                HeaderRange.DisposeChildInstances();
                HeaderRange.Dispose();
                HeaderRange = null;
            }

            if(HeaderData != null)
            {
                HeaderData = null;
            }

            if(TableData != null)
            {
                TableData = null;
            }
        }
    }
}
