using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.Events
{
    public class ProcessorEvents : IError
    {
        public DpmEventHandler Progress;
        public DpmEventHandler Completed;
    }
}