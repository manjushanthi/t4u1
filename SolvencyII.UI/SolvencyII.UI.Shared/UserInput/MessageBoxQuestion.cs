using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SolvencyII.UI.Shared.UserInput
{
    /// <summary>
    /// Standardise message box for questions
    /// </summary>
    public static class MessageBoxQuestion
    {
        public static bool ShowMessageBoxAnsYes(string question)
        {
            DialogResult response = MessageBox.Show(question, "Confirmation", MessageBoxButtons.YesNo);
            return response == DialogResult.Yes;
        }

        public static void AlertMessageBox(string message)
        {
            MessageBox.Show(message, "Alert");
        }

    }
}
