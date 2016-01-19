using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.ExcelImportExportLib
{
    public interface IExcelConnection
    {
        bool OpenConnection();
        void CloseConnection();
        string GetConnectionString();

    }
}
