using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.Events.EventArguments
{
    public class ProgressArgs : EventArgs
    {
        public string Message { get; set; }
        public int TotalRecord { get; set; }
        public int ProcessedRecord { get; set; }
    }
}
