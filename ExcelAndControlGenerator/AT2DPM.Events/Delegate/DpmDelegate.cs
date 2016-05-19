using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events.Delegate
{
    public delegate void ProgressEventHandler(ProgressArgs args);
    public delegate void DpmErrorHandler(ErrorArgs args);
    public delegate void CompletedEventHandler(object sender, AsyncCompletedEventArgs e);
}
