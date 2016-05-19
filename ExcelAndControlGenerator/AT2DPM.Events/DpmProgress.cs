using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public class DpmProgress
    {
        public ProgressEventHandler Progress;
        public ProgressEventHandler Completed;
        public DpmErrorHandler Error;

        protected ProgressArgs DpmEventArgs { get; set; }
        protected ErrorArgs DpmErrorArgs { get; set; }

        private int progressIndex;

        protected void IncrementProgress()
        {
            if (DpmEventArgs != null)
                DpmEventArgs.ProcessedRecord = progressIndex++;
        }

        protected void EquateProgress()
        {
            if (DpmEventArgs != null)
                DpmEventArgs.ProcessedRecord = DpmEventArgs.TotalRecord;
        }

        public virtual void ProgressInitialize(int count)
        {
            DpmEventArgs = new ProgressArgs();
            DpmEventArgs.TotalRecord = count;
            progressIndex = 0;
        }

        public virtual void OnProgress()
        {
            IncrementProgress();

            if (this.Progress != null)
                this.Progress(DpmEventArgs);
        }

        public virtual  void OnProgressComplete()
        {
            EquateProgress();

            if (this.Completed != null)
                this.Completed(DpmEventArgs);
        }

        public virtual void OnError(object o, Exception e)
        {
            DpmErrorArgs = new ErrorArgs();
            DpmErrorArgs.Object = o;
            DpmErrorArgs.Exception = e;

            if (this.Error != null)
                this.Error(DpmErrorArgs);
        }
    }
}
