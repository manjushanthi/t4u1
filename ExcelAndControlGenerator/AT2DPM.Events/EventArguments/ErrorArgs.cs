using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.Events.EventArguments
{
    public class ErrorArgs : EventArgs
    {
        public Exception Exception { get; set; }
        public object Object { get; set; }
        public IUserEvents UserEvents { get; set; }
    }
}
