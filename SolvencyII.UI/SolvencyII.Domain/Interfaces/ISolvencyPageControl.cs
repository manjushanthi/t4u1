using SolvencyII.Domain.ENumerators;

namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface used on controls for the nPage fields from the concrete tables
    /// </summary>
    public interface ISolvencyPageControl : ISolvencyControl
    {
        string ColName { get; set; }
        string TableNames { get; set; }
        bool FixedDimension { get; set; }
        string Text { get; set; }
        bool Enabled { get; set; }
        SolvencyDataType ColumnType { get; }
    }
}
