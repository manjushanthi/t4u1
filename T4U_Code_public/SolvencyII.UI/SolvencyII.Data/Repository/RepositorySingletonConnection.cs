using System;
using SolvencyII.Data.SQLite;

namespace SolvencyII.Data.Repository
{
    /// <summary>
    /// This Singleton is designed to allow multiple base repositories to use the same connection.
    /// This allows us to use a single transaction safeguarding updates.
    /// Originally there were transactions on each repository but the nested nature was not compatible
    /// with SQLite; each repository had its own connection.
    /// </summary>
    internal class RepositorySingletonConnection 
    {
        private static SQLiteConnection _conn;

        internal static SQLiteConnection Instance(string connectionString)
        {
            if (_conn == null)
            {
                _conn = new SQLiteConnection(connectionString);
            }
            return _conn;
        }

        internal static void Dispose()
        {
            if (_conn != null)
            {
                _conn.Close();
                _conn.Dispose();
                _conn = null;
            }
        }
    }
}
