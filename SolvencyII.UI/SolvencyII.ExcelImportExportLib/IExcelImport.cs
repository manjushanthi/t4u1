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
    public interface IExcelImport : IExcelImportEvents
    {
        /*
        //Events
        event CompletedEventHandler ImportFromFileCompleted;
        event ProgressChangedEventHandler ImportFromFileProgressChanged;
        event ProgressChangedEventHandler ImportFromFileGranuleProgressChanged;
         * */
        string[] GetTableCodes(IExcelConnection excelConnection);
        bool ImportFromFile(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string supportedExcelTemplateVersion);
        void ImportFromFileAsync(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string supportedExcelTemplateVersion);
        bool ImportFromFile(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string[] tableFilter, string supportedExcelTemplateVersion);
        void ImportFromFileAsync(ImportExportBehaviour behaviour, ISolvencyData sqliteConnection, IExcelConnection excelConnection, dInstance instance, string[] tableFilter, string supportedExcelTemplateVersion);
    }
}
