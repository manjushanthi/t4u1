using System.Drawing;
using System.Windows.Forms;

namespace SolvencyII.UI.Shared.Controls
{
    /// <summary>
    /// Sub classed line
    /// </summary>
    public class SolvencyLine : UserControl
    {
        public SolvencyLine()
        {
            Paint += LineSeparator_Paint;
            BringToFront();
        }

        private void LineSeparator_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;
            if (!(Width < 5))
            {
                g.DrawLine(Pens.Black, new Point(0, 0), new Point(this.Width, 0));
                //g.DrawLine(Pens.DarkGray, new Point(0, 0), new Point(this.Width, 0));
                //g.DrawLine(Pens.White, new Point(0, 1), new Point(this.Width, 1));
            }
            else
            {
                g.DrawLine(Pens.Black, new Point(0, 0), new Point(0, this.Height));
                //g.DrawLine(Pens.DarkGray, new Point(0, 0), new Point(0, this.Height));
                //g.DrawLine(Pens.White, new Point(1, 0), new Point(1, this.Height));
                
            }
        }

    }
}
