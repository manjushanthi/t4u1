using System.Collections.Generic;
using SolvencyII.Domain.Entities;

namespace ucGenerator
{
    /// <summary>
    /// Used to compare Ordinates for open templates so headers / columns can be build correctly
    /// </summary>
    public class CompareOpenAxisOrdinateControls : IComparer<AxisOrdinateControls>
    {

        // -1 before
        //  0 same
        // +1 after

        public int Compare(AxisOrdinateControls aoc1, AxisOrdinateControls aoc2)
        {
            //bool one = false;
            //bool two = false;

            //OpenColInfo2 col = _columnData.FirstOrDefault(c => c.OrdinateID == aoc1.OrdinateID);
            //if (col != null) one = col.IsRowKey;
            //col = _columnData.FirstOrDefault(c => c.OrdinateID == aoc2.OrdinateID);
            //if (col != null) two = col.IsRowKey;

            //if (one != two)
            //    return two.CompareTo(one);

            if (aoc1.AxisOrientation != aoc2.AxisOrientation)
                return aoc2.AxisOrientation.CompareTo(aoc1.AxisOrientation);



            if (aoc1.IsRowKey != aoc2.IsRowKey)
                return aoc2.IsRowKey.CompareTo(aoc1.IsRowKey);

            // Current is child of second
            if (aoc1.ParentOrdinateID == aoc2.OrdinateID)
            {
                // if true then aoc2 is before aoc1 so - aoc1 is after aoc2
                return aoc2.IsDisplayBeforeChildren ? 1 : -1;
            }
            
            // Current is parent of second
            if (aoc1.OrdinateID == aoc2.ParentOrdinateID)
            {
                return aoc1.IsDisplayBeforeChildren ? -1 : 1;
            }

            // Same parents
            if(aoc1.ParentOrdinateID == aoc2.ParentOrdinateID)
                // Normal Order
                return aoc1.Order.CompareTo(aoc2.Order);

            // Different parents so we need to look at their order...?

            // With multiple layers this approach does not work.
            // A parent level 1 may have order 2
            // A parent may be a level 3 on a second primary branch with an order 1...

            //return aoc1.ParentOrder.CompareTo(aoc2.ParentOrder);

            return aoc1.TopBranchOrder.CompareTo(aoc2.TopBranchOrder);

        }
    }
}
