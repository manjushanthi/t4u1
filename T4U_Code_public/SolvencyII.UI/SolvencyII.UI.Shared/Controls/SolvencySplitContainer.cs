using System.Drawing;
using System.Windows.Forms;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed SplitContainer
    /// </summary>
    public class SolvencySplitContainer : SplitContainer
    {
        public SolvencySplitContainer()
        {
            this.FixedPanel = FixedPanel.Panel1;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //Panel panel = new SolvencyPanel();
            //this.Panel2 = panel;
            
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            this.Invalidate();
            base.OnScroll(se);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // WS_CLIPCHILDREN
                return cp;
            }
        }


        //protected override System.Drawing.Point ScrollToControl(Control activeControl)
        //{
        //    // When there's only 1 control in the panel and the user clicks
        //    //  on it, .NET tries to scroll to the control. This invariably
        //    //  forces the panel to scroll up. This little hack prevents that.
        //    return DisplayRectangle.Location;
        //}

        //protected override System.Drawing.Point ScrollToControl(Control activeControl)
        //{
        //    return DisplayRectangle.Location;
        //}
        //protected override Point ScrollToControl(Control activeControl)
        //{
        //    return this.AutoScrollPosition;
        //}
    }
}
