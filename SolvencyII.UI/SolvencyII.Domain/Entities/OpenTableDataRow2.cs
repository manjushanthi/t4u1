using System.Collections.Generic;

namespace SolvencyII.Data.Shared.Entities
{
    /// <summary>
    /// Object used to create open table columns.
    /// Implemented SolvencyII.UI.Shared.UserControls.OpenUserControlBase2.CreateColumn
    /// </summary>
    public class OpenTableDataRow2
    {
        public OpenTableDataRow2()
        {
            ColValues = new List<string>();
        }

        // We have un indeterminate number of columns thus the lists.
        public List<string> ColValues; // Where the actual data is stored
        public int PK_ID { get; set; }
        public bool Modified { get; set; }

    }
}
