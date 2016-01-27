using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows.Forms;
using SolvencyII.Domain.ENumerators;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.UI.Shared.Loggers
{
    /// <summary>
    /// File logger will create and write a log file in the folder of the exe called log.txt
    /// </summary>
    [Export(typeof(ILogger))]
    public class FileLogger : ILogger
    {
        public void WriteLog(eSeverity severity, string location, string message)
        {
            try
            {
                string logPath = Path.GetDirectoryName(Application.ExecutablePath);
                logPath = Path.Combine(logPath, "Log.txt");
                using (Stream stream = new FileStream(logPath, FileMode.Append, FileAccess.Write))
                using (TextWriter writer = new StreamWriter(stream))
                {
                    string line;
                    switch (severity)
                    {
                        case eSeverity.Debug:
                        case eSeverity.Note:
                            line = string.Format("{0}|  {1}", location, message);
                            break;
                        case eSeverity.Warning:
                            line = string.Format("Warning: {1}| from {0}", location, message);
                            ;
                            break;
                        case eSeverity.Error:
                            line = string.Format("*Error*  {1}| from {0}", location, message);
                            ;
                            break;
                        case eSeverity.Critical:
                            line = string.Format("*** Critical ***  {1}| from {0}", location, message);
                            
                            break;
                        default:
                            throw new ArgumentOutOfRangeException("severity");
                    }
                    line = string.Format("{0:yyyyMMdd HH:mm:ss.ff} {1}", DateTime.Now, line);
                    writer.WriteLine(line);
                    writer.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        
        }
    }
}
