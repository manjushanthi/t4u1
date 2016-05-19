using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public interface ITransformEvents 
    {
        event ProgressEventHandler TransformProgress;
        event ProgressEventHandler TransformComplete;

        void OnTransformProgress(ProgressArgs args);
        void OnTransformComplete(ProgressArgs args);
    }

    public class TransformEvents : ITransformEvents
    {
        event ProgressEventHandler TransformProgress;
        event ProgressEventHandler TransformComplete;

        object objectLock = new Object();

        event ProgressEventHandler ITransformEvents.TransformProgress
        {
            add
            {
                lock (objectLock)
                {
                    TransformProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    TransformProgress -= value;
                }
            }
        }

        event ProgressEventHandler ITransformEvents.TransformComplete
        {
            add
            {
                lock (objectLock)
                {
                    TransformComplete += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    TransformComplete -= value;
                }
            }
        }

        public void OnTransformProgress(ProgressArgs args)
        {
            if (TransformProgress != null)
                TransformProgress(args);
        }

        public void OnTransformComplete(ProgressArgs args)
        {
            if (TransformComplete != null)
                TransformComplete(args);
        }
    }
}
