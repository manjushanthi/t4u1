using System.Reflection;

namespace SolvencyII.GUI.Classes
{
    /// <summary>
    /// Central point to generate the T4U version number
    /// </summary>
    public static class GuiSpecific
    {
        public static string ApplicationVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}
