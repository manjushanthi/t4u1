using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucGenerator.Classes
{
    public static class MergeTemplateSetup
    {
        public static void MergeTemplateInfoSetup(List<CreateFileParameter> superParameter, ref MergeTemplateInfo mergeTemplateInfo, ref LocationRangeCalculator rangeCalculator)
        {
            mergeTemplateInfo.MergeTemplate = superParameter.Count > 0;

            if (mergeTemplateInfo.MergeTemplate)
            {
                // Work out the starting positions;
                List<string> ranges = new List<string>();
                foreach (CreateFileParameter fileParameter in superParameter)
                {
                    ranges.AddRange(fileParameter.locationRanges);
                }
                rangeCalculator = new LocationRangeCalculator(ranges);
                mergeTemplateInfo.RangesIdentical = rangeCalculator.AllRegionsTheSame();
                mergeTemplateInfo.TypeOfMerge = rangeCalculator.TypeOfMergedTemplate();
            }
        }
    }
}
