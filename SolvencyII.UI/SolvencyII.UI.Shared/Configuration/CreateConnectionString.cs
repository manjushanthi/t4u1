using System.Windows.Forms;
using Microsoft.Data.ConnectionUI;

namespace SolvencyII.UI.Shared.Configuration
{
    /// <summary>
    /// SQL Server connection string creation
    /// </summary>
    public static class CreateConnectionString
    {
        public static string Create()
        {
            string newConnection = "";
            DataConnectionDialog objDataConnectionDialog = new DataConnectionDialog();
            DataSource.AddStandardDataSources(objDataConnectionDialog);
            if (DataConnectionDialog.Show(objDataConnectionDialog) == DialogResult.OK)
            {
                newConnection = objDataConnectionDialog.ConnectionString;
            }
            objDataConnectionDialog.Dispose();
            return newConnection;
        }
    }
}
