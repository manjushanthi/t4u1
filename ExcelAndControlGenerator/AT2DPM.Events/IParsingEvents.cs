using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public interface IParsingEvents 
    {
        event ProgressEventHandler ParsingProgress;
        event ProgressEventHandler ParsingCompleted;

        void OnParsingProgress(ProgressArgs args);
        void OnParsingCompleted(ProgressArgs args);
    }

    public class ParsingEvents : IParsingEvents
    {
        event ProgressEventHandler ParsingProgress;
        event ProgressEventHandler ParsingCompleted;

        object objectLock = new Object();

        event ProgressEventHandler IParsingEvents.ParsingProgress
        {
            add
            {
                lock (objectLock)
                {
                    ParsingProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    ParsingProgress -= value;
                }
            }
        }

        event ProgressEventHandler IParsingEvents.ParsingCompleted
        {
            add
            {
                lock (objectLock)
                {
                    ParsingCompleted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    ParsingCompleted -= value;
                }
            }
        }

        public void OnParsingProgress(ProgressArgs args)
        {
            if (ParsingProgress != null)
                ParsingProgress(args);
        }

        public void OnParsingCompleted(ProgressArgs args)
        {
            if (ParsingCompleted != null)
                ParsingCompleted(args);
        }
    }
}
