using System.Windows.Forms;
using SolvencyII.Domain.Constants;
using SolvencyII.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucGenerator.Classes
{
    /// <summary>
    /// Calculator for row information
    /// </summary>
    public static class RowCalculator
    {

        /// <summary>
        /// Row titles / row heights are calculated here.
        /// Row starting positions are relative to 0.
        /// </summary>
        /// <param name="RowStartingPositions"></param>
        /// <param name="RowHeights"></param>
        /// <param name="yDimensions"></param>
        /// <param name="constants"></param>
        /// <param name="firstColumnWidth"></param>
        public static void CalcRowHeights(out List<int> RowStartingPositions, out List<int> RowHeights, List<AxisOrdinateControls> yDimensions, ConstantsForDesigner constants, int firstColumnWidth)
        {
            RowStartingPositions = new List<int>();
            RowHeights = new List<int>();
            int startLocationY = 0;

            Label myLabel = new Label();
            for (int i = 0; i < yDimensions.Count; i++)
            {
                int levelAdjustment = (yDimensions[i].Level - 1)*constants.LEVEL_TAB;
                int labelWidth = firstColumnWidth - ((constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width)*2) - levelAdjustment;
                yDimensions[i].LabelWidth = labelWidth;
                int literalWidth = TextRenderer.MeasureText(yDimensions[i].OrdinateLabel, myLabel.Font).Width;
                yDimensions[i].TextWidth = literalWidth;

                int requiredHeight = (int) Math.Ceiling(literalWidth/(decimal) (labelWidth))*constants.LABEL_Height_Division;

                RowStartingPositions.Add(startLocationY);
                RowHeights.Add(requiredHeight);

                if (requiredHeight < constants.ROW_HEIGHT) startLocationY += constants.ROW_HEIGHT;
                else
                {
                    startLocationY += requiredHeight + constants.CONTROL_MARGIN_CENTRAL;
                }
            }
        }
    }


}
