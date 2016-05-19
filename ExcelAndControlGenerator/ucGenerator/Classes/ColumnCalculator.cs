using System;
using System.Collections.Generic;
using System.Linq;
using SolvencyII.Domain.Constants;
using SolvencyII.Domain.Entities;
using ucGenerator.Extensions;

namespace ucGenerator.Classes
{
    /// <summary>
    /// Used to work out column specific information
    /// Given the axis ordinates workout the starting positions and on the bases of their nesting the maximum height.
    /// </summary>
    public static class ColumnCalculator
    {
        public static void CalcColumnStartingPositionsAndWidths(out List<int> ColumnStartingPositions, out List<int> ColumnWidths, List<AxisOrdinateControls> xDimensions, List<AxisOrdinateControls> yDimensions, ConstantsForDesigner constants, int startLocationX, int firstColumnWidth)
        {
            int columnCount = xDimensions.Count;

            ColumnStartingPositions = new List<int>();
            ColumnWidths = new List<int>();
            // Iterate across the columns to see their types
            ColumnStartingPositions.Add(startLocationX);
            ColumnStartingPositions.Add(startLocationX + firstColumnWidth + constants.CODE_COLUMN_WIDTH);
            ColumnWidths.Add(firstColumnWidth + constants.CODE_COLUMN_WIDTH);

            // Developer note
            /*
             * If the DataType of the first column X dim is null it means the data types can vary for each row.
             * This means that the widest data type across each of the rows is the width needed for each column. 
             */
            bool useRowTypes = (columnCount == 0 || xDimensions[0].DataType == null);
            if (!useRowTypes)
            {
                // Each Row type should be specified.
                for (int colItem = 1; colItem <= columnCount; colItem++)
                {
                    if (!xDimensions[colItem - 1].IsAbstractHeader)
                    {
                        int widthOfType;
                        string colDataType = "";
                        if (xDimensions[colItem - 1].DataType != null)
                        {
                            colDataType = xDimensions[colItem - 1].DataType.ToUpper();
                        }
                        switch (colDataType)
                        {
                            case "ENUMERATION/CODE":
                                widthOfType = constants.USER_COMBO_COLUMN_WIDTH;
                                // widthOfType = CURRENCY_COLUMN_WIDTH;
                                break;
                            case "BOOLEAN":
                            case "DATE":
                            case "URI":
                            case "STRING":
                            case "INTEGER":
                            case "PERCENTAGE/DECIMAL":
                            case "DECIMAL":
                            case "MONETARY":
                            default:
                                widthOfType = constants.CURRENCY_COLUMN_WIDTH;
                                break;

                        }
                        ColumnWidths.Add(widthOfType);
                        ColumnStartingPositions.Add(ColumnStartingPositions[colItem] + widthOfType);
                    }
                    else
                    {
                        ColumnWidths.Add(0);
                        ColumnStartingPositions.Add(ColumnStartingPositions[colItem]);
                    }
                }
            }
            else
            {
                // We need to work out the largest column width across each of the rows and set that one.

                int maxRowTypeWidth = 0;
                for (int rowItem = 1; rowItem <= yDimensions.Count; rowItem++)
                {
                    if (!yDimensions[rowItem - 1].IsAbstractHeader)
                    {
                        int widthOfType;
                        string rowDataType = "";
                        if (yDimensions[rowItem - 1].DataType != null)
                        {
                            rowDataType = yDimensions[rowItem - 1].DataType.ToUpper();
                        }
                        switch (rowDataType)
                        {
                            case "ENUMERATION/CODE":
                                widthOfType = constants.USER_COMBO_COLUMN_WIDTH;
                                // widthOfType = CURRENCY_COLUMN_WIDTH;
                                break;
                            case "BOOLEAN":
                            case "DATE":
                            case "URI":
                            case "STRING":
                            case "INTEGER":
                            case "PERCENTAGE/DECIMAL":
                            case "DECIMAL":
                            case "MONETARY":
                            default:
                                widthOfType = constants.CURRENCY_COLUMN_WIDTH;
                                break;

                        }
                        if (widthOfType > maxRowTypeWidth) maxRowTypeWidth = widthOfType;
                    }

                }

                // We have the width of the columns now so modify the column arrays correctly.
                if (columnCount > 0)
                {
                    for (int colItem = 1; colItem <= columnCount; colItem++)
                    {
                        if (!xDimensions[colItem - 1].IsAbstractHeader)
                        {
                            ColumnWidths.Add(maxRowTypeWidth);
                            ColumnStartingPositions.Add(ColumnStartingPositions[colItem] + maxRowTypeWidth);
                        }
                        else
                        {
                            ColumnWidths.Add(0);
                            ColumnStartingPositions.Add(ColumnStartingPositions[colItem]);
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Column Header Height
        /// </summary>
        /// <param name="xDimensions"></param>
        /// <param name="labelWidth"></param>
        /// <param name="rowHeight"></param>
        /// <param name="rowOfText"></param>
        /// <param name="maxTopParentHeight"></param>
        /// <returns></returns>
        public static int IterativeMaxHeight(List<AxisOrdinateControls> allXDimensions, List<AxisOrdinateControls> xDimensions, int labelWidth, int rowHeight, int rowOfText, ref int maxTopParentHeight)
        {
            int max = 0;
            foreach (AxisOrdinateControls ao in xDimensions)
            {
                int calcHeight = 0;
                if (ao.ColSpan != 0)
                {
                    calcHeight = (int)Math.Ceiling(ao.TextWidth / (decimal)(ao.ColSpan * labelWidth)) * rowOfText;
                    if (calcHeight < rowHeight) calcHeight = rowHeight;
                }
                else
                {
                    if (!ao.IsAbstractHeader)
                    {
                        calcHeight = (int)Math.Ceiling(ao.TextWidth / (decimal)(labelWidth)) * rowOfText;
                        if (calcHeight < rowHeight) calcHeight = rowHeight;
                    }
                }

                if (ao.ParentOrdinateID == 0)
                {
                    // This is true only if this ordinate id has children
                    if (xDimensions.Any(d => d.ParentOrdinateID == ao.OrdinateID))
                    {
                        maxTopParentHeight = maxTopParentHeight.KeepMax(calcHeight);
                        calcHeight = maxTopParentHeight;
                    }
                }
                calcHeight += IterativeMaxHeight(allXDimensions, allXDimensions.Where(x => x.ParentOrdinateID == ao.OrdinateID).ToList(), labelWidth, rowHeight, rowOfText, ref maxTopParentHeight);
                if (calcHeight > max) max = calcHeight;
            }
            return max;
        }

    }
}
