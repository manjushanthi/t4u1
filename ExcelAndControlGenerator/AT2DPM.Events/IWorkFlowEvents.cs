using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public interface IWorkFlowEvents : IDpmProcessorEvents
    {
        event ProgressEventHandler CompleteOverallProgress;

        void OnCompleteOverallProgress(ProgressArgs args);
    }
}
