using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SolvencyII.ExcelImportExportLib.Events
{
    public class ExportImportEvents : IExcelImportEvents, IExcelExportEvents
    {
        protected event CompletedEventHandler completedEventHandler;

        protected event ProgressChangedEventHandler progressChangedEventHandler;
        protected event ProgressChangedEventHandler granuleProgressChangedEventHandler;

        protected object objectLock = new Object();
        protected bool _IsStoprequested = false;

        public void CancelOperation(object sender, EventArgs e)
        {
            _IsStoprequested = true;
        }

        protected void OnCompleted(Exception error, bool cancelled, object userState)
        {
            if (completedEventHandler != null)
            {
                AsyncCompletedEventArgs args = new AsyncCompletedEventArgs(error, cancelled, userState);

                completedEventHandler(this, args);
            }

        }

        protected void OnProgressChanged(int progressPercentage, object userState)
        {
            if (progressChangedEventHandler != null)
            {
                ProgressChangedEventArgs args = new ProgressChangedEventArgs(progressPercentage, userState);

                progressChangedEventHandler(this, args);
            }
        }

        protected void OnGranuleProgressChanged(int progressPercentage, object userState)
        {
            if (granuleProgressChangedEventHandler != null)
            {
                ProgressChangedEventArgs args = new ProgressChangedEventArgs(progressPercentage, userState);

                granuleProgressChangedEventHandler(this, args);
            }
        }

        public event CompletedEventHandler ImportFromFileCompleted
        {
            add
            {
                lock (objectLock)
                {
                    completedEventHandler += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    completedEventHandler -= value;
                }
            }
        }

        public event ProgressChangedEventHandler ImportFromFileProgressChanged
        {
            add
            {
                lock (objectLock)
                {
                    progressChangedEventHandler += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    progressChangedEventHandler -= value;
                }
            }
        }

        public event ProgressChangedEventHandler ImportFromFileGranuleProgressChanged
        {
            add
            {
                lock (objectLock)
                {
                    granuleProgressChangedEventHandler += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    granuleProgressChangedEventHandler -= value;
                }
            }
        }

        public event CompletedEventHandler ExportToFileCompleted
        {
            add
            {
                lock (objectLock)
                {
                    completedEventHandler += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    completedEventHandler -= value;
                }
            }
        }

        public event ProgressChangedEventHandler ExportToFileProgressChanged
        {
            add
            {
                lock (objectLock)
                {
                    progressChangedEventHandler += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    progressChangedEventHandler -= value;
                }
            }
        }

        public event ProgressChangedEventHandler ExportToFileGranuleProgressChanged
        {
            add
            {
                lock (objectLock)
                {
                    granuleProgressChangedEventHandler += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    granuleProgressChangedEventHandler -= value;
                }
            }
        }
    }
}
