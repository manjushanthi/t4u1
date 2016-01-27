using System.Collections.Generic;
using System.Windows.Forms;
using SolvencyII.Domain;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension of UserControl
    /// </summary>
    public static class UserControlExt
    {
        public static void SetupDataChangedEvents(this UserControl userControl, GenericDelegates.SolvencyControlChanged DataChanged, List<ISolvencyDataControl> dataControls)
        {
            // All DataControls have the data changed event.
            foreach (ISolvencyDataControl control in dataControls)
            {
                control.DataChanged += DataChanged;
            }
        }
    }
}
