using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public class WorkFlowEventsImpl : IWorkFlowEvents
    {
        event ProgressEventHandler completeOverallProgress;
        event DpmErrorHandler err;
        event ProgressEventHandler parsingProgress;
        event ProgressEventHandler parsingCompleted;
        event ProgressEventHandler savingDpmProgress;
        event ProgressEventHandler savingDpmCompleted;
        event ProgressEventHandler transformProgress;
        event ProgressEventHandler transformComplete;
        event ProgressEventHandler verifyProgress;
        event ProgressEventHandler verifyCompleted;
        event ProgressEventHandler dpmProcessorStart;
        event ProgressEventHandler dpmProcessorComplete;

        object objectLock = new Object();

        public event ProgressEventHandler CompleteOverallProgress
        {
            add
            {
                lock (objectLock)
                {
                    completeOverallProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    completeOverallProgress -= value;
                }
            }
        }

        public event DpmErrorHandler Err
        {
            add
            {
                lock (objectLock)
                {
                    err += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    err -= value;
                }
            }
        }

        public event ProgressEventHandler ParsingProgress
        {
            add
            {
                lock (objectLock)
                {
                    parsingProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    parsingProgress -= value;
                }
            }
        }

        public event ProgressEventHandler ParsingCompleted
        {
            add
            {
                lock (objectLock)
                {
                    parsingCompleted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    parsingCompleted -= value;
                }
            }
        }

        public event ProgressEventHandler SavingDpmProgress
        {
            add
            {
                lock (objectLock)
                {
                    savingDpmProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    savingDpmProgress -= value;
                }
            }
        }

        public event ProgressEventHandler SavingDpmCompleted
        {
            add
            {
                lock (objectLock)
                {
                    savingDpmCompleted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    savingDpmCompleted -= value;
                }
            }
        }

        public event ProgressEventHandler TransformProgress
        {
            add
            {
                lock (objectLock)
                {
                    transformProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    transformProgress -= value;
                }
            }
        }

        public event ProgressEventHandler TransformComplete
        {
            add
            {
                lock (objectLock)
                {
                    transformComplete += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    transformComplete -= value;
                }
            }
        }

        public event ProgressEventHandler VerifyProgress
        {
            add
            {
                lock (objectLock)
                {
                    verifyProgress += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    verifyProgress -= value;
                }
            }
        }

        public event ProgressEventHandler VerifyCompleted
        {
            add
            {
                lock (objectLock)
                {
                    verifyCompleted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    verifyCompleted -= value;
                }
            }
        }

        public event ProgressEventHandler DpmProcessorStart
        {
            add
            {
                lock (objectLock)
                {
                    dpmProcessorStart += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    dpmProcessorStart -= value;
                }
            }
        }

        public event ProgressEventHandler DpmProcessorComplete
        {
            add
            {
                lock (objectLock)
                {
                    dpmProcessorComplete += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    dpmProcessorComplete -= value;
                }
            }
        }

        public void OnCompleteOverallProgress(ProgressArgs args)
        {
            if (completeOverallProgress != null)
                completeOverallProgress(args);
        }

        public void OnError(ErrorArgs args)
        {
            if (err != null)
                err(args);
        }

        public void OnParsingProgress(ProgressArgs args)
        {
            if (parsingProgress != null)
                parsingProgress(args);
        }

        public void OnParsingCompleted(ProgressArgs args)
        {
            if (parsingCompleted != null)
                parsingCompleted(args);
        }

        public void OnSavingDpmProgress(ProgressArgs args)
        {
            if (savingDpmProgress != null)
                savingDpmProgress(args);
        }

        public void OnSavingDpmCompleted(ProgressArgs args)
        {
            if (savingDpmCompleted != null)
                savingDpmCompleted(args);
        }

        public void OnTransformProgress(ProgressArgs args)
        {
            if (transformProgress != null)
                transformProgress(args);
        }

        public void OnTransformComplete(ProgressArgs args)
        {
            if (transformComplete != null)
                transformComplete(args);
        }

        public void OnVerifyProgress(ProgressArgs args)
        {
            if (verifyProgress != null)
                verifyProgress(args);
        }

        public void OnVerifyCompleted(ProgressArgs args)
        {
            if (verifyCompleted != null)
                verifyCompleted(args);
        }

        public void OnDpmProcessorStart(ProgressArgs args)
        {
            if (dpmProcessorStart != null)
                dpmProcessorStart(args);
        }

        public void OnDpmProcessorComplete(ProgressArgs args)
        {
            if (dpmProcessorComplete != null)
                dpmProcessorComplete(args);
        }

    }
}
