using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ucGenerator.Classes
{
    /// <summary>
    /// Used to manage Region Locations information
    /// </summary>
    public class MyPoint
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }

        public string Range { get; set; }
    }
}
