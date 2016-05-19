using System.Collections.Generic;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Entities;

namespace ucGenerator
{
    public class CompareColumnDimsControlsDeleteMe : IComparer<AxisOrdinateControls>
    {
        private List<OpenColInfo2> _columns;
        public CompareColumnDimsControlsDeleteMe(List<OpenColInfo2> columns)
        {
            _columns = columns;
        }

        // -1 before
        //  0 same
        // +1 after

        public int Compare(AxisOrdinateControls aoc1, AxisOrdinateControls aoc2)
        {
            int pos1 = 0;
            int pos2 = 0;
            int row = 0;
            foreach (OpenColInfo2 openColInfo2 in _columns)
            {
                if(aoc1.OrdinateID == openColInfo2.OrdinateID) pos1 = row;
                if (aoc2.OrdinateID == openColInfo2.OrdinateID) pos2 = row;
                row++;
            }

            return pos1.CompareTo(pos2);
            
        }
    }
}
