using System.Drawing;
using System.Windows.Forms;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed label
    /// </summary>
    public sealed class SolvencyLabel : Label, ISolvencyDisplayControl
    {
        public SolvencyLabel()
        {
            ColumnType = SolvencyDataType.String;
            // TextAlign = ContentAlignment.MiddleLeft;
            // AutoSize = false;
        }

        public SolvencyDataType ColumnType { get; set; }
        public int OrdinateID_Label { get; set; }

        public event Domain.GenericDelegates.SolvencyControlChanged DataChanged;

        public event Domain.GenericDelegates.DisplayDimensions DisplayDimensions;
    }
}
