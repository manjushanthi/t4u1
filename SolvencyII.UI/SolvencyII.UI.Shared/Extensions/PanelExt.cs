using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolvencyII.UI.Shared.Extensions
{
    /// <summary>
    ///  Not used
    /// </summary>
    public static class PanelExt
    {
        private static List<Control>  ControlList = new List<Control>();

        public static void AddHorizontalScrollBars(this Panel panel, Control containerControl)
        {
            // IEnumerable<Control> ctrls = containerControl.Controls.Cast<Control>();
            GetAllControls(containerControl);

            ScrollBar scrollBar = new HScrollBar();
            scrollBar.Dock = DockStyle.Bottom;
            // scrollBar.Scroll += (sender, e) => { panel.HorizontalScroll.Value = scrollBar.Value; };
            scrollBar.Scroll += scrollBar_Scroll;
            panel.Controls.Add(scrollBar);
            scrollBar.SendToBack();
        }

        private static void GetAllControls(Control container)
        {
            foreach (Control c in container.Controls)
            {
                GetAllControls(c);
                if (c is TextBox) ControlList.Add(c);
            }
        }


        static void scrollBar_Scroll(object sender, ScrollEventArgs e)
        {

            ScrollBar bar = (ScrollBar) sender;
            Panel panel = (Panel) bar.Parent;

            panel.HorizontalScroll.Value = e.NewValue;
        }



    }
}
