using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Domain.Interfaces;
using SolvencyII.UI.Shared.Controls;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    /// Extension for ISolvencyComboBox
    /// </summary>
    public static class ISolvencyComboBoxExt
    {
        public static void ClearGreyed(this List<ISolvencyComboBox> lst)
        {
            foreach (ISolvencyComboBox ctrl in lst)
            {
                if (ctrl is SolvencyComboBox)
                {
                    if ((Color)ctrl.GetBackColor == Color.Gray)
                    {
                        ((SolvencyComboBox)ctrl).Items.Clear();
                    }
                }
            }
        }

        public static void AddTextValue(this ISolvencyComboBox combo, string value)
        {
            SolvencyTextComboBox textCombo = combo as SolvencyTextComboBox;
            if (textCombo != null)
            {
                textCombo.Items.Add(new ListViewItem {Text = value, Name = value});
            }
            else
            {
                SolvencyComboBox stdCombo = combo as SolvencyComboBox;
                if (stdCombo != null)
                {
                    stdCombo.Items.Add(new ListViewItem {Text = value, Name = value});
                }
            }
        }

    }
}
