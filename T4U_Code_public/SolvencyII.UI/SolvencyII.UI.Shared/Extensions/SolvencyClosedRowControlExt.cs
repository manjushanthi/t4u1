using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension for SolvencyClosedRowControl
    /// </summary>
    public static class SolvencyClosedRowControlExt
    {
        public static List<Control> GetControls(this SolvencyClosedRowControl userControl, Type type = null)
        {
            List<Control> result = new List<Control>();
            GetAllControls(userControl, ref result);
            if (type == null)
                return result;
            return result.Where(type.IsInstanceOfType).ToList();
        }

        private static void GetAllControls(Control container, ref List<Control> result)
        {
            if (container != null && container.Controls.Count > 0)
            {
                foreach (Control c in container.Controls)
                {
                    GetAllControls(c, ref result);
                    result.Add(c);
                }
            }
        }

    }
}
