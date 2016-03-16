using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetOffice.ExcelApi;

using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.ExcelImportExportLib.Dto;


namespace SolvencyII.ExcelImportExportLib.Load
{
    public abstract class LoadBase
    {
        public abstract int LoadData(ISolvencyData sqliteConnection, Worksheet workSheet, AbstractTransferObject dto);
    }
}
