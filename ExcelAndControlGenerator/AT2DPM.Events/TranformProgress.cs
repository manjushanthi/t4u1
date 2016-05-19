using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.Events
{
    public class TranformProgress : DpmProgress
    {
        public ITransformEvents TransformEvents { get; set; }

        public TranformProgress(ITransformEvents te)
        {
            TransformEvents = te;
        }

        public override void OnProgress()
        {
            base.OnProgress();

            if (TransformEvents != null)
                TransformEvents.OnTransformProgress(DpmEventArgs);
        }

        public override void OnProgressComplete()
        {
            base.OnProgressComplete();

            if (TransformEvents != null)
                TransformEvents.OnTransformComplete(DpmEventArgs);
        }
    }
}
