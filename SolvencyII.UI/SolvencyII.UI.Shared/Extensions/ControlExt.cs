using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.Data.Shared.Dictionaries;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension for Controls; locations of generic type nested within control
    /// </summary>
    public static class ControlExt
    {
        public static IEnumerable<T> FindAllChildrenByType<T>(this Control control)
        {
            IEnumerable<Control> controls = control.Controls.Cast<Control>();
            return controls
                .OfType<T>()
                .Concat<T>(controls.SelectMany(ctrl => FindAllChildrenByType<T>(ctrl)));
        }

    }
}
