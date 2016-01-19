namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Base interface for solvency controls
    /// </summary>
    public interface ISolvencyControl
    {
        event GenericDelegates.SolvencyControlChanged DataChanged;
        event GenericDelegates.DisplayDimensions DisplayDimensions;
    }
}
