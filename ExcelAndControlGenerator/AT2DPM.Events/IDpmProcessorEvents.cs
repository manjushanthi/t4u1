using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public interface IDpmProcessorEvents : IError, IParsingEvents, ISaveDpmEvents, ITransformEvents, IVerifyEvents
    {
        event ProgressEventHandler DpmProcessorStart;
        event ProgressEventHandler DpmProcessorComplete;

        void OnDpmProcessorStart(ProgressArgs args);
        void OnDpmProcessorComplete(ProgressArgs args);
    }

    /*public class DpmProcessorEvents : IDpmProcessorEvents
    {
        event DpmEventHandler DpmProcessorStart;
        event DpmEventHandler DpmProcessorComplete;

        object objectLock = new Object();

        event DpmEventHandler IDpmProcessorEvents.DpmProcessorStart
        {
            add
            {
                lock (objectLock)
                {
                    DpmProcessorStart += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    DpmProcessorStart -= value;
                }
            }
        }

        event DpmEventHandler IDpmProcessorEvents.DpmProcessorComplete
        {
            add
            {
                lock (objectLock)
                {
                    DpmProcessorComplete += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    DpmProcessorComplete -= value;
                }
            }
        }

        void IDpmProcessorEvents.OnDpmProcessorStart(DpmEventArgs args)
        {
            if (DpmProcessorStart != null)
                DpmProcessorStart(args);
        }

        void IDpmProcessorEvents.OnDpmProcessorComplete(DpmEventArgs args)
        {
            if (DpmProcessorComplete != null)
                DpmProcessorComplete(args);
        }
    }*/
}
