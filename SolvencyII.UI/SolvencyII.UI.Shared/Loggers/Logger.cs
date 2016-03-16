using System.Diagnostics;
using System.Reflection;
using SolvencyII.Domain.ENumerators;

namespace SolvencyII.UI.Shared.Loggers
{
    /// <summary>
    /// Central class to capture information throughout the program and to record it.
    /// This was originally planned to use MEF but only currently only uses FileLogger. 
    /// </summary>
    public static class Logger
    {
       public static void WriteLog(eSeverity severity, string message)
       {
           StackTrace stackTrace = new StackTrace();
           StackFrame stackFrame = stackTrace.GetFrame(1);
           MethodBase methodBase = stackFrame.GetMethod();
           string location = methodBase.Name;

           WriteLog(severity, location, message);
       }

       public static void WriteLog(eSeverity severity, string location, string message)
       {
           FileLogger fileLogger = new FileLogger();
           fileLogger.WriteLog(severity, location, message);

           EMailLogger eMailLogger = new EMailLogger();
           eMailLogger.WriteLog(severity, location, message);
       }
    }
}
