using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SolvencyII.Domain.ENumerators;
using SolvencyII.UI.Shared.Loggers;

namespace SolvencyII.GUI.Classes
{
    /// <summary>
    /// Used as a central point for fail safe error handling.
    /// </summary>
    public static class ExceptionHandler
    {

        internal static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string location;
            string stackFrame2 = e.Exception.StackTrace;
            int length = stackFrame2.IndexOf("\r\n");
            if (length != -1)
                location = stackFrame2.Substring(0, length);
            else
                location = stackFrame2;

            ErrorAlertUser(e.Exception.ToString(), e.Exception.Message, location);
        }

        /// <summary>
        /// Catches all un handled threads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string location;
            string stackFrame2 = ((Exception)e.ExceptionObject).StackTrace;
            int length = stackFrame2.IndexOf("\r\n");
            if (length != -1)
                location = stackFrame2.Substring(0, length);
            else
                location = stackFrame2;

            ErrorAlertUser(e.ExceptionObject.ToString(), ((Exception) e.ExceptionObject).Message, location);
        }

        private static void ErrorAlertUser(string eString, string eMessage, string location)
        {
            Logger.WriteLog(eSeverity.Critical, location, string.Format("System Crash. {0}", eString));

            if (eMessage.StartsWith("no such table"))
            {
                if (MessageBox.Show(string.Format("There is a problem with this database unfortunately a table or view is not present. Please open or create a new container.\r\nError: {0}\r\n  Full details have been logged.", eMessage), "Critical Error", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    Application.Exit();
            }
            else
            {
                if (MessageBox.Show(string.Format("Sorry there has been a unhandled error.\r\n If this fails to fix the issue please contact support.\r\n\r\nLocation: {0}\r\nError: {1}\r\n  Full details have been logged.", location, eMessage), "Critical Error", MessageBoxButtons.RetryCancel) == DialogResult.Cancel)
                    Application.Exit();
            }
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "frmMain")
                {
                    openForm.Show();
                    // (openForm as frmMain).ClearForm();
                }
            }
        }

        public static void Exception(eSeverity severity, Exception ex)
        {
            // Get the details
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            string location = methodBase.Name;

            // Write log
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("{0}", ex.Message));
            if (ex.InnerException != null)
                sb.AppendLine(string.Format(">> Inner Error: {0}", ex.InnerException.Message));
            if (severity == eSeverity.Critical)
            {
                sb.AppendLine();
                sb.AppendLine("Stack Trace:");
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine();
            }
            Logger.WriteLog(severity, location, sb.ToString());

            // Alert user
            switch (severity)
            {
                case eSeverity.Debug:
                    MessageBox.Show(string.Format("Debug Message:\n{0}", ex.Message), "Debug");

                    break;
                case eSeverity.Note:
                    MessageBox.Show(string.Format("Please Note:\n{0}", ex.Message), "Note");
                    break;
                case eSeverity.Warning:
                    MessageBox.Show(string.Format("Warning\n{0}", ex.Message), "Warning");
                    break;
                case eSeverity.Error:
                    MessageBox.Show(string.Format("Sorry there has been a error.\nPlease try performing the action again.\nIf this fails please restart the program and try again.\nIf this fails to fix the issue please contact support.\nError: {0}", ex.Message), "Error");
                    break;
                case eSeverity.Critical:
                    MessageBox.Show(string.Format("Sorry there has been a critical error.\nPlease check that the versions correspond.\n  Version of T4U\n  Version of database\n  Version of template\nand try again.\nIf this fails to fix the issue please contact support.\nError: {0}", ex.Message), "Critical Error");
                    break;
                default:
                    throw new ArgumentOutOfRangeException("severity");
            }

            // Required action
            if (severity == eSeverity.Critical)
            {
                Application.Exit();
            }

        }

        public static void AsynchronousThreadException(eSeverity severity, Exception ex, string title, string message)
        {
            // Get the details
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase methodBase = stackFrame.GetMethod();
            string location = methodBase.Name;

            // Write log
            StringBuilder sb = new StringBuilder();
            Exception e = ex;
            if (e != null)
            {
                do
                {
                    sb.Append(e.Message);
                    sb.AppendLine();

                } while ((e = e.InnerException) != null);
                sb.AppendLine();
                if (e != null)
                {
                    if (e.StackTrace != null)
                    {
                        sb.AppendLine("Stack trace");
                        sb.AppendLine("==============");
                        sb.AppendLine(e.StackTrace);
                    }
                }
            }

            

            Logger.WriteLog(severity, location, sb.ToString());


            // Alert user
            switch (severity)
            {
                case eSeverity.Debug:
                    MessageBox.Show(string.Format("Debug Message:\n{0}", ex.Message), "Debug");
                    break;

                case eSeverity.Note:
                    MessageBox.Show(string.Format("Please Note:\n{0}", ex.Message), "Note");
                    break;

                case eSeverity.Warning:
                    MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;

                case eSeverity.Error:
                    MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                case eSeverity.Critical:
                    MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("severity");
            }

            // Required action
            if (severity == eSeverity.Critical)
            {
                Application.Exit();
            }

        }

    }
}
