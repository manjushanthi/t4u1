using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain;

namespace SolvencyII.ExcelImportExportLib.Dto
{
    public abstract class AbstractTransferObject : IDisposable
    {
        public dInstance Instance { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
        public string Version { get; set; }
        public int TotalRows { get; set; }
        public string TableCode { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if(disposing == true)
            {
                if(Instance != null)
                {
                    Instance = null;
                }
            }
            else
            {

            }
        }
    }
}
