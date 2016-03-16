using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.DBcontrollers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Interface of internal ETL
    /// </summary>
    public interface IEtlEngine
    {
        /*bool Extract();
        bool Transform();
        bool Load();*/
        bool PerformEtl(int cacheSize);
        void Cancel();
    }

}
