using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Clas managing communitation of the ETL progress
    /// </summary>
    public static class ProgressHandler
    {
        public delegate void ProgressDelegate(int current, int total, string message);
        public static event ProgressDelegate Progress;

        public delegate void ErrorDelegate(Exception ex);
        public static event ErrorDelegate Error;

        /// <summary>
        /// Etls the progress.
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="total">The total.</param>
        /// <param name="message">The message.</param>
        public static void EtlProgress(int current, int total, string message)
        {
            if (Progress != null)                
                Progress(current, total, message);
        }

        /// <summary>
        /// Etls the error.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void EtlError(Exception ex)
        {
            if (Error != null)
                Error(ex);
        }
    }
}
