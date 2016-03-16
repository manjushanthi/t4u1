using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SolvencyII.ExcelImportExportLib.Events
{
    public interface IExcelImportEvents : CancelOperationEvent
    {
        //Events
        event CompletedEventHandler ImportFromFileCompleted;
        event ProgressChangedEventHandler ImportFromFileProgressChanged;
        event ProgressChangedEventHandler ImportFromFileGranuleProgressChanged;

    }
}
