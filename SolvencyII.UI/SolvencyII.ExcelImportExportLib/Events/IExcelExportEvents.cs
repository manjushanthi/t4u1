using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SolvencyII.ExcelImportExportLib.Events
{
    public interface IExcelExportEvents : CancelOperationEvent
    {
        //Events
        event CompletedEventHandler ExportToFileCompleted;
        event ProgressChangedEventHandler ExportToFileProgressChanged;
        event ProgressChangedEventHandler ExportToFileGranuleProgressChanged;
    }
}
