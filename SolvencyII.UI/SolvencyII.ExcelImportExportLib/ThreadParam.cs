using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Transform;
using SolvencyII.ExcelImportExportLib.Load;

namespace SolvencyII.ExcelImportExportLib
{
    class ThreadParam
    {
        public ImportExportBehaviour Behaviour { get; set; }
        public ISolvencyData SqliteConnection { get; set; }
        public IExcelConnection ExcelConnection { get; set; }
        public dInstance Instance { get; set; }
        public TransformBase Transform { get; set; }
        public LoadBase Load { get; set; }
        public string[] TableFilter { get; set; }
        public string CurrentExcelTemplateVersion { get; set; }
    }
}
