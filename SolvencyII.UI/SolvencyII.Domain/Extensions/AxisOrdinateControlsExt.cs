using System.Collections.Generic;
using System.Linq;
using SolvencyII.Domain.Entities;

namespace SolvencyII.Domain.Extensions
{
    /// <summary>
    /// Extension for AxisOrdinateControls
    /// </summary>
    public static class AxisOrdinateControlsExp
    {
        /// <summary>
        /// Works out the width of child ordinate controls to make sure the parents are the correct width.
        /// </summary>
        /// <param name="xDimensions"></param>
        /// <param name="parentID"></param>
        /// <returns></returns>
        public static int ChildWidth(this List<AxisOrdinateControls> xDimensions, int parentID)
        {
            int childrenCount = 0;
            foreach (AxisOrdinateControls axisOrdinateControlse in xDimensions.Where(d => d.ParentOrdinateID == parentID))
            {
                childrenCount += ChildWidth(xDimensions, axisOrdinateControlse.OrdinateID);
            }

            if (childrenCount == 0)
            {
                // No children - see if this parent is Abstract
                AxisOrdinateControls ordinate = xDimensions.FirstOrDefault(d => d.OrdinateID == parentID);
                if(ordinate != null && !ordinate.IsAbstractHeader)
                    childrenCount++;
            }
            return childrenCount;
        }

        /// <summary>
        /// Manually setting up the correct order of ordinate controls.
        /// </summary>
        /// <param name="xDimensions"></param>
        public static void SetupTopBranchOrder(this List<AxisOrdinateControls> xDimensions)
        {
            foreach (AxisOrdinateControls dim in xDimensions)
            {
                dim.TopBranchOrder = GetTopOrdinateParent(dim, xDimensions);
            }
        }
        private static string GetTopOrdinateParent(AxisOrdinateControls dim, List<AxisOrdinateControls> xDimensions)
        {
            AxisOrdinateControls parent = xDimensions.FirstOrDefault(o => o.OrdinateID == dim.ParentOrdinateID);
            if (parent == null)
                return string.Format("{0:00}{1:00}", dim.Level, dim.Order);
            return string.Format("{2}{0:00}{1:00}", dim.Level, dim.Order, GetTopOrdinateParent(parent, xDimensions));
        }
    }
}
