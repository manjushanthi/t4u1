using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using AT2DPM.DAL;
using AT2DPM.DAL.Model;
using DpmDB;
using System.Data.SQLite;


namespace DpmDB
{
    public class DpmDbHelper
    {
        //public  string GetDpmDbConnectionString(string filePath)
        //{
        //    return "metadata=res://*/DpmModelVer7.csdl|res://*/DpmModelVer7.ssdl|res://*/DpmModelVer7.msl;provider=System.Data.SQLite;provider connection string=';data source=" + filePath + "';";
        //    // Set the properties for the data source.
        //    //SQLiteConnectionStringBuilder sqliteBuilder = new SQLiteConnectionStringBuilder();
        //    //sqliteBuilder.DataSource = filePath;

        //    //// Initialize the EntityConnectionStringBuilder.
        //    //EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
        //    //entityBuilder.Provider = "System.Data.SQLite";
        //    //entityBuilder.Metadata = @"res://*/Model.DPMdb.csdl|res://*/Model.DPMdb.ssdl|res://*/Model.DPMdb.msl";
        //    //entityBuilder.ProviderConnectionString = sqliteBuilder.ToString();


        //    //// Connect to DPMdb sqlite database.
        //    //return entityBuilder.ToString();
        //}

        public DPMdb loadDpmDbModel(string filePath)
        {
            DPMdbConnection con = new DPMdbConnection();
            return con.OpenDpmConnection(filePath);
            //return new DPMdb();
        }

        public HashSet<int> gteInstanceTabels(int insetnceId, string filePath)
        {
            HashSet<int> mTablesIds = new HashSet<int>();
            DPMdb db = loadDpmDbModel(filePath);



            db.Dispose();
            return mTablesIds;
        }
    }
}
