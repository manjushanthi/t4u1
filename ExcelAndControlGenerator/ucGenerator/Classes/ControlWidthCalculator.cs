using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucGenerator.Classes
{
    /// <summary>
    /// Calculates the span of a number of controls
    /// </summary>
    public class ControlWidthCalculator
    {
        public static int CalculateSpanLength(int position, int spanNumber, List<int> columnWidths)
        {
            int span = 0;
            int colCount = columnWidths.Count;
            int colAdded = 0;
            int item = position + 1;
            while (colAdded < spanNumber & item < colCount)
            {
                if (columnWidths[item] != 0)
                {
                    colAdded++;
                    span += columnWidths[item];
                }
                item++;
            }
            if (colCount < position + spanNumber)
            {
                if (item == colCount)
                {
                    // We have run out of columns!
                    int extra = (position + spanNumber) - item;
                    for (int i = colCount - 1; i > 0; i--)
                    {
                        if (columnWidths[i] != 0)
                        {
                            span += columnWidths[i]*extra;
                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception("The column width cannot be calculated - the span is too far.");
                }
            }
                
            return span;
        }
    }
}
