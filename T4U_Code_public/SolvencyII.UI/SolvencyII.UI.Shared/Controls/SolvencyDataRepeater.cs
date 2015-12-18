using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed DataRepeater from Visual basic power packs
    /// </summary>
    public class SolvencyDataRepeater : Microsoft.VisualBasic.PowerPacks.DataRepeater
    {
        public SolvencyDataRepeater()
        {
            this.ItemHeaderVisible = false;
        }
    }
}
