using System.Collections.Generic;
using SolvencyII.Domain.Entities;

namespace ucGenerator
{
    public class CompareAxisOrdinateControlsDev : IComparer<AxisOrdinateControls>
    {
        // -1 before
        //  0 same
        // +1 after

        public int Compare(AxisOrdinateControls aoc1, AxisOrdinateControls aoc2)
        {
            
            // Current is child of second
            if (aoc1.ParentOrdinateID == aoc2.OrdinateID)
            {
                // if true then aoc2 is before aoc1.
                // Returning 1 means aoc1 is after aoc2
                // Since aoc1 is after aoc2
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
