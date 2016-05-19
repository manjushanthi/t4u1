using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.Events
{
    public interface IUserEvents
    {
        void Abort();
        void Continue();
    }
}
