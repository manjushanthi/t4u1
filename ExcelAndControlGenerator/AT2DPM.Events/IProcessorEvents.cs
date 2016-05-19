using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;

namespace AT2DPM.Events
{
    public class IProcessorEvents
    {
        public ProgressEventHandler Progress;
        public ProgressEventHandler Completed;
    }
}