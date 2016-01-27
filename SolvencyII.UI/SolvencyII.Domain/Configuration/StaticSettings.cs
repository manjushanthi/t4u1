using SolvencyII.Domain.ENumerators;

namespace SolvencyII.Domain.Configuration
{
    /// <summary>
    /// Domain wide static variable storage for regularly needed information.
    /// </summary>
    public static class StaticSettings
    {
        public static string ConnectionString { get; set; }

        public static eDataTier DataTier { get; set; }

        public static string InstanceDescription { get; set; }

        public static string InstanceCurrency { get; set; }

        public static int ApplicationID { get; set; }
        public static eLanguageID FormLanguage { get; set; }

        public static string SolvencyIITemplateDBConnectionString { get; set; }

        public static bool TestingMode { get; set; }

        public static bool ConsoleApp { get; set; }

        public static DbType DbType { get; set; }

        public static int LanguageID { get; set; }

    }
}
