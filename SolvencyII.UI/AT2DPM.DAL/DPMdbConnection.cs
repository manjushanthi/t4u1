using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.SQLite;
using System.IO;
using System.Data.Entity;
using System.Data.EntityClient;

using AT2DPM.DAL.Model;

namespace AT2DPM.DAL
{
    public class DPMdbConnection
    {
        private DPMdb dpmDb = null;

        public DPMdb OpenDpmConnection(string dpmPath)
        {
            string providerName = "System.Data.SQLite.EF6";
            string metaData = @"res://*/Model.DPMdb.csdl|res://*/Model.DPMdb.ssdl|res://*/Model.DPMdb.msl";
            //string metaData = "res://*/Model.DPMdb.csdl|res://*/Model.DPMdb.ssdl|res://*/Model.DPMdb.msl;

            try
            {
                // Check for database file existence.
                if (!File.Exists(dpmPath))
                    throw new ApplicationException("Database not found at:" + dpmPath);

                // Close if there is any open database connection.
                CloseActiveDatabase();

                if (dpmDb == null)
                {
                    // Set the properties for the data source.
                    SQLiteConnectionStringBuilder sqliteBuilder = new SQLiteConnectionStringBuilder();
                    sqliteBuilder.DataSource = dpmPath;
                    sqliteBuilder.ForeignKeys = true;


                    // Initialize the EntityConnectionStringBuilder.
                    EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
                    entityBuilder.Provider = providerName;
                    entityBuilder.Metadata = metaData;
                    entityBuilder.ProviderConnectionString = sqliteBuilder.ConnectionString;


                    // Connect to DPMdb sqlite database.
                    dpmDb = new DPMdb(entityBuilder.ConnectionString);
                    //_DPMdb = new DPMdb();

                    //TODO: nicholas memory database:http://stackoverflow.com/questions/6080322/in-memory-database-system-data-sqlite 

                }

                return dpmDb;
            }
            catch (Exception ex)
            {
                throw new DALException("An error occured while connecting to DPM database.", ex);
                //ErrorMessages.GetErrorMessage("DPM_CONNECTIVITY_ERROR"), ex);
            }
        }

        public Boolean CloseActiveDatabase()
        {
            if (dpmDb != null)
            {
                dpmDb.Dispose();
                dpmDb = null;
            }

            return true;
        }
    }
}
