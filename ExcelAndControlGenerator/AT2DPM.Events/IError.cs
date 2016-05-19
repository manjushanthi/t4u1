using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.Events.Delegate;
using AT2DPM.Events.EventArguments;

namespace AT2DPM.Events
{
    public interface IError
    {
        event DpmErrorHandler Err;

        void OnError(ErrorArgs args);
    }

    public class Error : IError
    {
        event DpmErrorHandler Err;

        object objectLock = new Object();

        event DpmErrorHandler IError.Err
        {
            add
            {
                lock (objectLock)
                {
                    Err += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    Err -= value;
                }
            }
        }

        void IError.OnError(ErrorArgs args)
        {
            if (Err != null)
                Err(args);
        }
    }
}
