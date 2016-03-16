using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension for ISolvencyPageControl
    /// </summary>
    public static class ISolvencyPageControlExt
    {
        public static string SQLValue(this ISolvencyPageControl iControl)
        {
            if (iControl is SolvencyTextComboBox) return string.Format("'{0}'", iControl.Text);
            if (iControl.FixedDimension)
                return string.Format("'{0}'", iControl.Text);
            return string.Format("'{0}'", ((SolvencyComboBox)iControl).GetValue);
        }

        public static bool ValueIsBlank(this ISolvencyPageControl iControl)
        {
            if (iControl is SolvencyTextComboBox) return string.IsNullOrEmpty(iControl.Text);
            if (!iControl.FixedDimension)
                return string.IsNullOrEmpty(((SolvencyComboBox) iControl).GetValue);
            return string.IsNullOrEmpty(iControl.Text);
        }

        public static string Value(this ISolvencyPageControl iControl)
        {
            if (iControl.FixedDimension)
                return string.Format("{0}", iControl.Text);

            if (iControl.GetType() != typeof(SolvencyTextComboBox))
                return string.Format("{0}", ((SolvencyComboBox)iControl).GetValue);
            return string.Format("{0}", iControl.Text);
        }
    }
}
