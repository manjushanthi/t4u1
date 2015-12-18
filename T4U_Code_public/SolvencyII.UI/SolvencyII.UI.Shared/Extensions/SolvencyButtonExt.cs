using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension for SolvencyButton
    /// </summary>
    public static class SolvencyButtonExt
    {
        public static SolvencyButton DeepCopy(this SolvencyButton btn)
        {
            SolvencyButton result = new SolvencyButton();
            result.Size = btn.Size;
            result.Location = btn.Location;
            result.Text = btn.Text;
            result.Name = btn.Name;

            return result;

        }
    }
}
