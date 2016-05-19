using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.Events
{
    public class LoadProgress : DpmProgress
    {
        public ISaveDpmEvents SaveEvents { get; set; }

        public LoadProgress(ISaveDpmEvents se)
        {
            SaveEvents = se;
        }

        public override void OnProgress()
        {
            base.OnProgress();

            if (SaveEvents != null)
                SaveEvents.OnSavingDpmProgress(DpmEventArgs);
        }

        public override void OnProgressComplete()
        {
            base.OnProgressComplete();

            if (SaveEvents != null)
                SaveEvents.OnSavingDpmCompleted(DpmEventArgs);
        }
    }
}
