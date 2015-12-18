using System.Windows.Forms;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed button
    /// </summary>
    public class SolvencyButton : Button, ISolvencyControl
    {
        public string ColName { get; set; }
        public string TableNames { get; set; }

        public event Domain.GenericDelegates.SolvencyControlChanged DataChanged;

        public event Domain.GenericDelegates.DisplayDimensions DisplayDimensions;
    }
}
