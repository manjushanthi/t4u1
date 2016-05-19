using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public interface IVerifyEvents
    {
        event ProgressEventHandler VerifyProgress;
        event ProgressEventHandler VerifyCompleted;

        void OnVerifyProgress(ProgressArgs args);
        void OnVerifyCompleted(ProgressArgs args);
    }

    public class VerifyEvents : IVerifyEvents
    {
        event ProgressEventHandler VerifyProgress;
        event ProgressEventHandler VerifyCompleted;

        object objectLock = new Object();

        event ProgressEventHandler IVerifyEvents.VerifyProgress
        {
            add
            {
                lock (objectLock)
                {
                    VerifyProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    VerifyProgress -= value;
                }
            }
        }

        event ProgressEventHandler IVerifyEvents.VerifyCompleted
        {
            add
            {
                lock (objectLock)
                {
                    VerifyCompleted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    VerifyCompleted -= value;
                }
            }
        }


        public void OnVerifyProgress(ProgressArgs args)
        {
            if (VerifyProgress != null)
                VerifyProgress(args);
        }

        public void OnVerifyCompleted(ProgressArgs args)
        {
            if (VerifyCompleted != null)
                VerifyCompleted(args);
        }
    }
}
