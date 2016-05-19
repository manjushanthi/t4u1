using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using ucGenerator.Extensions;

namespace ucGenerator.Classes
{
    /// <summary>
    /// Used to work out relative positions of Location Ranges
    /// </summary>
    public class LocationRangeCalculator
    {
        #region Declare private vars

        private readonly List<MyPoint> _workingRange = new List<MyPoint>();
        private readonly List<int> _colWidths = new List<int>();
        private readonly List<int> _rowHeights = new List<int>();
        private readonly List<string> _regionLocations;
        private readonly eTypeOfMergedTemplate _typeOfMergedTemplate = eTypeOfMergedTemplate.NotSet;

        #endregion

        public enum eTypeOfMergedTemplate
        {
            NotSet,
            SingleRow,
            SingleColumn,
            MultipleRowsAndColumns
        }

        public LocationRangeCalculator(List<string> regionLocations)
        {
            _regionLocations = regionLocations;

            if (regionLocations.Count == 0)
            {
                _typeOfMergedTemplate = eTypeOfMergedTemplate.NotSet;
                throw new Exception("There is a problem with the region locations. A merged template requiring them had not found any.");
                return;
            }

            // Bring the raw information together to work out the starting / ending places.
            GatherCoodsFromLocationRegions(regionLocations, _workingRange);

            // Remove the outer padding so the first point can be 0,0.
            int minX = _workingRange.Min(p => p.StartX);
            int minY = _workingRange.Min(p => p.StartY);
            foreach (MyPoint point in _workingRange)
            {
                point.StartX -= minX;
                point.StartY -= minY;
                point.EndX -= minX;
                point.EndY -= minY;
            }
            
            // Calculate the x widths;
            
            int start = -1;
            foreach (MyPoint range in _workingRange)
            {
                if (start < range.StartX)
                {
                    start = range.StartX;
                    int width = _workingRange.Where(r => r.StartX == start).Max(r => r.EndX);
                    width = width - start + 1;
                    _colWidths.Add(width);
                    RecordColumnNumber(_workingRange, start, width, _colWidths.Count);
                }
            }

            // Calculate the y heights;
            
            start = -1;
            foreach (MyPoint range in _workingRange)
            {
                if (start < range.StartY)
                {
                    start = range.StartY;
                    int height = _workingRange.Where(r => r.StartY == start).Max(r => r.EndY);
                    height = height - start + 1;
                    _rowHeights.Add(height);
                    RecordRowNumber(_workingRange, start, height, _rowHeights.Count);
                }
            }

            if (_rowHeights.Count > 1 && _colWidths.Count > 1)
                _typeOfMergedTemplate = eTypeOfMergedTemplate.MultipleRowsAndColumns;
            else
            {
                if(_rowHeights.Count > 1)
                    _typeOfMergedTemplate = eTypeOfMergedTemplate.SingleColumn;
                else
                    _typeOfMergedTemplate = eTypeOfMergedTemplate.SingleRow;
            }

        }

        public Point CalcStartingLocation(string range)
        {
            return CalcStartingLocation(_regionLocations.FindIndex(s => s == range));
        }

        public Point CalcStartingLocation(int rangeId)
        {
            const int MARGIN = 1;
            int col = _workingRange[rangeId].Col;
            int row = _workingRange[rangeId].Row;
            int x = 0;
            int y = 0;
            for (int i = 0; i < col; i++)
            {
                x += _colWidths[i] + MARGIN;
            }
            //if (col > 0) x += MARGIN;
            for (int j = 0; j < row; j++)
            {
                 y += _rowHeights[j] + MARGIN;
            }
            //if (row > 0) y += MARGIN;
            return new Point(x,y);

        }


        public bool AllRegionsTheSame()
        {
            if (_workingRange.Count == 0) return true;
            int xDim, yDim;
            xDim = _workingRange[0].EndX - _workingRange[0].StartX;
            yDim = _workingRange[0].EndY - _workingRange[0].StartY;
            return (!_workingRange.Exists(r=>(r.EndX-r.StartX) != xDim) && (!_workingRange.Exists(r=>(r.EndY-r.StartY) != yDim)));
        }

        /// <summary>
        /// Is template in the first column?
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        public bool TemplateInFirstColumn(string range)
        {
            int rangeID = _regionLocations.FindIndex(s => s == range);
            return (_workingRange[rangeID].Col == 0);
        }

        public eTypeOfMergedTemplate TypeOfMergedTemplate()
        {
            return _typeOfMergedTemplate;
        }

        #region Worker functions

        private static void RecordColumnNumber(IEnumerable<MyPoint> workingRange, int start, int width, int columnNumber)
        {
            foreach (MyPoint myPoint in workingRange)
            {
                // If this range exists between the start and the start plus width then its in this column:
                if (myPoint.StartX >= start && myPoint.StartX < (width + start) && myPoint.EndX <= (start + width))
                    myPoint.Col = columnNumber - 1;
            }
        }

        private static void RecordRowNumber(IEnumerable<MyPoint> workingRange, int start, int width, int rowNumber)
        {
            foreach (MyPoint myPoint in workingRange)
            {
                // If this range exists between the start and the start plus width then its in this column:
                if (myPoint.StartY >= start && myPoint.StartY < (width + start) && myPoint.EndY <= (start + width))
                    myPoint.Row = rowNumber - 1;
            }
        }

        public static void GatherCoodsFromLocationRegions(IEnumerable<string> regionLocations, List<MyPoint> workingRanges)
        {
            foreach (string regionLocation in regionLocations)
            {
                string[] location = regionLocation.Split(':');
                List<string> splitString = location[0].ToUpper().SeparateCharFromNumber();

                MyPoint calc = new MyPoint();
                calc.Range = regionLocation;
                calc.StartX = splitString[0].NumberFromChar();
                calc.StartY = int.Parse(splitString[1]);

                if (location.Count() > 1)
                {
                    splitString = location[1].ToUpper().SeparateCharFromNumber();
                    calc.EndX = splitString[0].NumberFromChar();
                    calc.EndY = int.Parse(splitString[1]);
                }
                else
                {
                    calc.EndX = calc.StartX;
                    calc.EndY = calc.StartY;
                }
                workingRanges.Add(calc);
            }
        }

        #endregion
    


        public int RowNumber(string regionLocation)
        {
            MyPoint range = _workingRange.FirstOrDefault(w => w.Range == regionLocation);
            if (range != null)
                return range.Row;
            return 0;
        }

        public void UpdateColWidth(int col, int width)
        {
            _colWidths[col] = width;
        }

        public void UpdateRowHeight(int row, int height)
        {
            _rowHeights[row] = height;
        }


    }
}
