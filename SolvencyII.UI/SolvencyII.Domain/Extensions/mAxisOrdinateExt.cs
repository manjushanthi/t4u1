using System.Collections.Generic;
using System.Linq;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Currently not used. Previous generation. (Prior to Mar 2015)
    /// </summary>
    public static class mAxisOrdinateExp
    {
        public static int ChildWidth(this List<mAxisOrdinate> xDimensions, int parentID)
        {
            int childrenCount = 0;
            foreach (mAxisOrdinate axisOrdinateControlse in xDimensions.Where(d => d.ParentOrdinateID == parentID))
            {
                childrenCount += ChildWidth(xDimensions, axisOrdinateControlse.OrdinateID);
            }
            if (childrenCount == 0)
            {
                // No children - see if this parent is Abstract
                mAxisOrdinate ordinate = xDimensions.FirstOrDefault(d => d.OrdinateID == parentID);
                if(ordinate != null && !ordinate.IsAbstractHeader)
                    childrenCount++;
            }
            return childrenCount;
        }

        public static void SetupTopBranchOrder(this List<mAxisOrdinate> xDimensions)
        {
            foreach (mAxisOrdinate dim in xDimensions)
            {
                dim.TopBranchOrder = GetTopOrdinateParent(dim, xDimensions);
            }
        }

        private static string GetTopOrdinateParent(mAxisOrdinate dim, List<mAxisOrdinate> xDimensions)
        {
            mAxisOrdinate parent = xDimensions.FirstOrDefault(o => o.OrdinateID == dim.ParentOrdinateID);
            if (parent == null)
                return string.Format("{0:00}{1:00}", dim.Level, dim.Order);
            return string.Format("{2}{0:00}{1:00}", dim.Level, dim.Order, GetTopOrdinateParent(parent, xDimensions));
        }
    }
}
