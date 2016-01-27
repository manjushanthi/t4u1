using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using SolvencyII.UI.Shared.Configuration;
using SolvencyII.UI.Shared.Registry;
using Ionic.Zip;
using SolvencyII.UI.Shared.Loggers;

namespace SolvencyII.UI.Shared.Databases
{
    /// <summary>
    /// Manager for SQLite data bases
    /// </summary>
    public static class ManageDatabases
    {

        public static bool CheckConnectionString(string connectionString)
        {
            GetSQLData getData = new GetSQLData(connectionString);
            try
            {
                if (getData.ConnectionCheck()) {return true;}
            }
            finally
            {
                getData.Dispose();
            }
            MessageBox.Show(LanguageLabels.GetLabel(89, "There is a problem with this multi report container. Please select another one and try again."));
            return false;
        }

        public static bool LocateAndSaveDatabasePath()
        {
            string connectionString;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "SolvencyII (*.xbrt)|*.xbrt";
            openFileDialog1.Title = LanguageLabels.GetLabel(90, "Locate Solvency II multi report container");
            // openFileDialog1.CheckFileExists = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                connectionString = openFileDialog1.FileName;
                // Check the connection string.
                if (File.Exists(connectionString))
                {
                    if (CheckConnectionString(connectionString))
                    {
                        Logger.WriteLog(eSeverity.Note, "Opening Database" + connectionString);
                        ChangeDatabase(connectionString);
                        return true;
                    }
                }
                else
                {
                    // File does not exist so copy the template database
                    // Check a valid file name was given.
                    string sourceFile = Path.Combine(Application.StartupPath, "TemplateDatabase.db");
                    if(!File.Exists(sourceFile))
                    {
                        MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(91, "Unfortunately a file is missing:"), sourceFile));
                        return false;
                    }
                    File.Copy(sourceFile, connectionString);
                    ChangeDatabase(connectionString);
                    //RegSettings.ConnectionString = connectionString;
                    //StaticSettings.ConnectionString = connectionString;
                    //RecentFilesRegistryManagement.OpenFile(connectionString);
                    return true;

                }
            }
            return false;
        }

        public static bool ChangeDatabaseAfterMigration(string connectionString)
        {
           bool result= ChangeDatabase(connectionString);
           return result;

        }

      

        //Added method to the retrieve the connection string for the Solvency Template DB
        public static string GetTemplateDBConnectionStringForSolvencyII()
        {
            //Added new method to extract the TemplateDatabase.DB
            string templateDBConString_zip = Path.Combine(Application.StartupPath, "T4Udb_master.zip");
            string templateDBConString = Path.Combine(System.IO.Path.GetDirectoryName(templateDBConString_zip),Path.GetFileName("TemplateDatabase.db"));

            //TODO: Need to find the feasible solution for this after release
            //if (File.Exists(templateDBConString))            
            //    return templateDBConString;
           
            if (!File.Exists(templateDBConString_zip))
            {
                MessageBox.Show(string.Format("{0}:\n{1}", "Unfortunately a file is missing:", templateDBConString_zip));
                return string.Empty;
            }
            else
            {
                using (ZipFile zip = ZipFile.Read(templateDBConString_zip))
                    foreach (ZipEntry entry in zip.EntriesSorted)
                    {
                        entry.FileName = Path.GetFileName("TemplateDatabase.db");
                        entry.Extract(System.IO.Path.GetDirectoryName(templateDBConString_zip), ExtractExistingFileAction.OverwriteSilently);
                    }
                
                return templateDBConString;
            }

            //string templateDBConString = Path.Combine(Application.StartupPath, "TemplateDatabase.db");
            //if (!File.Exists(templateDBConString))
            //{
            //    MessageBox.Show(string.Format("{0}:\n{1}", "Unfortunately a file is missing:", templateDBConString));
            //    return string.Empty;
            //}
            //else
            //    return templateDBConString;
        }

        public static bool CreateAndSaveDatabasePath(DbType dbType, string applicationVersion)
        {
            string connectionString;
            //string dbName = dbType == DbType.SolvencyII ? "Solvency II" : "CRV IV";
            string dbName = string.Empty;
            switch (dbType)
            {
                case DbType.SolvencyII:
                    dbName = "Solvency II";
                    break;
                case DbType.CRDIV:
                    dbName = "CRDIV";
                    break;
                case DbType.SolvencyII_Preparatory:
                    dbName = "SolvencyII_Preparatory";
                    break;
            }  

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = string.Format("{0} (*.xbrt)|*.xbrt", dbName);           
            saveFileDialog1.Title = string.Format(LanguageLabels.GetLabel(92, "Create {0} multi report container"), dbName);
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                connectionString = saveFileDialog1.FileName;
                // Check the connection string.
                // File does not exist so copy the template database
                // Check a valid file name was given.
                string sourceFile =string.Empty;
                switch (dbType)
                    {
                        case DbType.SolvencyII:
                            sourceFile = Path.Combine(Application.StartupPath, "T4Udb_Sol2.zip");
                            Logger.WriteLog(eSeverity.Note, "Creating the SolvencyII Database " + connectionString);
                            break;
                        case DbType.CRDIV:
                            sourceFile = Path.Combine(Application.StartupPath, "T4Udb_CRDIV.zip");
                            Logger.WriteLog(eSeverity.Note, "Creating the T4Udb_CRDIV Database " + connectionString);
                            break;
                        case DbType.SolvencyII_Preparatory:
                            sourceFile = Path.Combine(Application.StartupPath, "T4Udb_Sol2_prep.zip");
                            Logger.WriteLog(eSeverity.Note, "Creating the T4Udb_Sol2_prep Database " + connectionString);
                            break; 
                    }                
                
                if (!File.Exists(sourceFile))
                {
                    MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(91, "Unfortunately a file is missing:"), sourceFile));
                    return false;
                }

                try
                {
                   
                    if (File.Exists(connectionString)) File.Delete(connectionString);
                    //UnZip the DB.Zip into XBRT instance
                    using (ZipFile zip = ZipFile.Read(sourceFile))
                        foreach (ZipEntry entry in zip.EntriesSorted)
                        {
                            entry.FileName = Path.GetFileName(connectionString);
                            entry.Extract(System.IO.Path.GetDirectoryName(connectionString));
                        }
                    
                    if(ChangeDatabase(connectionString))
                        SetDatabaseDateAndType(applicationVersion, dbType);
                }
                catch (IOException ex)
                {
                    MessageBox.Show(LanguageLabels.GetLabel(93, "The file you wish to create is in use. Please close whichever program is accessing it and try again."), LanguageLabels.GetLabel(94, "Create error"));
                    Console.WriteLine(ex);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}\n{1}", LanguageLabels.GetLabel(95), ex.Message), LanguageLabels.GetLabel(94, "Create error"));
                    return false;
                }
                return true;

            }
            return false;
        }

        private static void SetDatabaseDateAndType(string applicationVersion, DbType dbType)
        {
            using (GetSQLData getData = new GetSQLData())
            {
                getData.SetApplicationData(applicationVersion, dbType.ToString());
            }
        }



        public static bool ChangeDatabase(string connectionString)
        {
            try
            {
                RegSettings.ConnectionString = connectionString;
                StaticSettings.ConnectionString = connectionString;
                RecentFilesRegistryManagement.OpenFile(connectionString);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format(
                        "{0}\n{1}\n{2}",
                        LanguageLabels.GetLabel(96, "Unfortunately there was a problem writing to the registry. Please contact your system administrator."),
                        "Computer\\HKEY_CURRENT_USER\\SOFTWARE\\EIOPA\\XBRT\\WINDOWS_T4U\\LastDatabasePath",
                        ex.Message));
                return false;
            }
        }

    }
}
