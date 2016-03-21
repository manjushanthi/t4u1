using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;
using SolvencyII.ExcelImportExportLib.Domain;
using SolvencyII.ExcelImportExportLib.Events;

namespace SolvencyII.ExcelImportExportLib
{
    public interface IExcelExport : IExcelExportEvents
    {
        /*
        //Events
        event CompletedEventHandler ExportToFileCompleted;
        event ProgressChangedEventHandler ExportToFileProgressChanged;
        event ProgressChangedEventHandler ExportToFileGranuleProgressChanged;
         * */

        //string[] GetTableCodes(ISolvencyData sqliteConnection, string[] moduleFilter);
        
        bool ExportToFile(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance);
        void ExportToFileAsync(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance);

        bool ExportToFile(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string[] tableFilter);
        void ExportToFileAsync(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string[] tableFilter);

    }
}
