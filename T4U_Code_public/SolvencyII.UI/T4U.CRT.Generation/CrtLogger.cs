using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation
{
    public static class CRTLogger
    {
        public delegate void LogDelegate(string message);
        public static event LogDelegate Progress;

        public static void LogProgress(string message)
        {
            if (Progress != null)
                Progress(message);
        }
    }
}
