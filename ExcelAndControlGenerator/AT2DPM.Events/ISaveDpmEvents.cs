using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public interface ISaveDpmEvents
    {
        event ProgressEventHandler SavingDpmProgress;
        event ProgressEventHandler SavingDpmCompleted;

        void OnSavingDpmProgress(ProgressArgs args);
        void OnSavingDpmCompleted(ProgressArgs args);
    }

    public class SaveDpmEvents : ISaveDpmEvents
    {
        event ProgressEventHandler SavingDpmProgress;
        event ProgressEventHandler SavingDpmCompleted;

        object objectLock = new Object();

        event ProgressEventHandler ISaveDpmEvents.SavingDpmProgress
        {
            add
            {
                lock (objectLock)
                {
                    SavingDpmProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    SavingDpmProgress -= value;
                }
            }
        }

        event ProgressEventHandler ISaveDpmEvents.SavingDpmCompleted
        {
            add
            {
                lock (objectLock)
                {
                    SavingDpmCompleted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    SavingDpmCompleted -= value;
                }
            }
        }

        public void OnSavingDpmProgress(ProgressArgs args)
        {
            if (SavingDpmProgress != null)
                SavingDpmProgress(args);
        }

        public void OnSavingDpmCompleted(ProgressArgs args)
        {
            if (SavingDpmCompleted != null)
                SavingDpmCompleted(args);
        }
    }
}
