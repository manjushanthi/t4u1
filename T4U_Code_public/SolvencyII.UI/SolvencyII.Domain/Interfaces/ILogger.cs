using SolvencyII.Domain.ENumerators;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interfaces for logging components
    /// </summary>
    public interface ILogger
    {
        void WriteLog(eSeverity severity, string location, string message);
    }
}
