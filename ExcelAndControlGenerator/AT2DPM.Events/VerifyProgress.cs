using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.Events
{
    public class VerifyProgress : DpmProgress
    {
        public IVerifyEvents VerifyEvents { get; set; }

        public VerifyProgress(IVerifyEvents ve)
        {
            VerifyEvents = ve;
        }

        public override void OnProgress()
        {
            base.OnProgress();

            if (VerifyEvents != null)
                VerifyEvents.OnVerifyProgress(DpmEventArgs);
        }

        public override void OnProgressComplete()
        {
            base.OnProgressComplete();

            if (VerifyEvents != null)
                VerifyEvents.OnVerifyCompleted(DpmEventArgs);
        }
    }
}
