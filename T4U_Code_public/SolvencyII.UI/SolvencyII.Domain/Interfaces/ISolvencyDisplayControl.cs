using SolvencyII.Domain.ENumerators;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface for a SolvencyControl used only for displaying information
    /// </summary>
    public interface ISolvencyDisplayControl : ISolvencyControl
    {
        SolvencyDataType ColumnType { get; set; }
        string Text { get; set; }
    }
}
