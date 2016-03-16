using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.ExcelImportExportLib.Events
{
    public interface CancelOperationEvent
    {
        void CancelOperation(object sender, EventArgs e);
    }
}
