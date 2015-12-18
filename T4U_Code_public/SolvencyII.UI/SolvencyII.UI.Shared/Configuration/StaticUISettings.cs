using System.Windows.Forms;

namespace SolvencyII.UI.Shared.Configuration
{
    /// <summary>
    /// Static settings that are visible to windows forms / templates etc.
    /// </summary>
    public static class StaticUISettings
    {
        ///
        /// Will the data bound control the simplest way to get hold of the tool tip object is to create this static pointer.
        /// Its container is frmMain.
        ///
        public static ToolTip MainToolTips { get; set; }
    }
}
