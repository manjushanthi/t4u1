using System.Linq;
using SolvencyII.Data.Shared.Entities;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    ///  Not used
    /// </summary>
    public static class OpenTableDataRow2Ext
    {
        public static bool IsEmpty(this OpenTableDataRow2 row)
        {
            bool foundValue = false;


            foundValue = foundValue || (row.ColValues.Any(v => !string.IsNullOrEmpty(v)));

            return !foundValue;
        }
    }
}
