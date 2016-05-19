using System.Collections.Generic;
using ucGenerator.Classes;

namespace ucIntegration.Classes
{
    public class CompareCreateFileParameter : IComparer<CreateFileParameter>
    {
        public int Compare(CreateFileParameter x, CreateFileParameter y)
        {
            // -1 before
            //  0 same
            // +1 after

            // Use the starting location to order the parameters.
            List<MyPoint> rangeX = new List<MyPoint>();
            List<MyPoint> rangeY = new List<MyPoint>();
            LocationRangeCalculator.GatherCoodsFromLocationRegions(x.locationRanges, rangeX);
            LocationRangeCalculator.GatherCoodsFromLocationRegions(y.locationRanges, rangeY);

            if (rangeX[0].StartX == rangeY[0].StartX)
                return rangeX[0].StartY.CompareTo(rangeY[0].StartY);
            return rangeX[0].StartX.CompareTo(rangeY[0].StartX);

        }
    }
}
