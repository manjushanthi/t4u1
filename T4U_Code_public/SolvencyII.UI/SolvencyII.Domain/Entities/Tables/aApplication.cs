namespace SolvencyII.Domain
{
    /// <summary>
    /// Application data object
    /// </summary>
    public class aApplication
    {
        public long ApplicationID { get; set; }
        public string ApplicationName { get; set; }
        public string ApplicationVersion { get; set; }
        public string DatabaseType { get; set; }
    }
}
