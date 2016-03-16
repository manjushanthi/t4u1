namespace SolvencyII.Domain.Interfaces
{
    /// <summary>
    /// Interface for the parent control which sits above the main template.
    /// </summary>
    public interface IParentUserControl
    {
        void EnableSaveCancel(bool enable);
        bool ChangeCancelToClose { set; }
        bool Filed { get; set; }
    }
}
