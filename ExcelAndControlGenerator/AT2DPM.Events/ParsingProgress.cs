using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.Events
{
    public class ParsingProgress : DpmProgress
    {
        public IParsingEvents ParsingEvents { get; set; }

        public ParsingProgress(IParsingEvents parsingEvents)
        {
            ParsingEvents = parsingEvents;
        }

        public override void OnProgress()
        {
            base.OnProgress();

            if (ParsingEvents != null)
                ParsingEvents.OnParsingProgress(DpmEventArgs);

        }

        public override void OnProgressComplete()
        {
            base.OnProgressComplete();

            if (ParsingEvents != null)
                ParsingEvents.OnParsingCompleted(DpmEventArgs);
        }
    }
}
