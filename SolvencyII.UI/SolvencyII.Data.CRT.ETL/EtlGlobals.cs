using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Global string variables of internal ETL
    /// </summary>
    internal static class EtlGlobals
    {
        internal static readonly string AtyDimCode = "eba_dim:ATY";
        internal static readonly string MetDimCode = "MET";
        public static string AtDomPrefix = "eba_AT";
        public static string MetDomPrefix = "eba_met";
    }
}
