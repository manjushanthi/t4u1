/*
 * Arelle setup for C#
 * 
 * Copyright 2010-2014 Mark V Systems Limited (as Licensor).
 * License information: http://www.apache.org/licenses/LICENSE-2.0.html
 * 
 */
using Ionic.Zip;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.UI.Shared.Loggers;
using SolvencyII.UI.Shared.Registry;
using SolvencyII.Domain.ENumerators;
using System.Runtime.InteropServices;
using SolvencyII.Domain.Configuration;

namespace SolvencyII.UI.Shared.Arelle
{
    public static class ArelleSetup
    {

        // Note that unzipping is performed using the code found here:
        // https://dotnetzip.codeplex.com/documentation

        #region Declarations

        public delegate void Arelle_Setup_Completed();

        private const string KEY_FILE = "ArelleFile";
        private const string KEY_VERSION = "ArelleVersion";
        private const string ARELLE_FOLDER = "EIOPA\\T4U Arelle";
        private const string ARELLE_FOLDER_32BIT = "Arelle-x86";
        private const string ARELLE_FOLDER_64BIT = "Arelle-x64";
        private const string ARELLE_FOLDER_TAXONOMIES = "Arelle\\Taxonomies";

        #endregion


        #region Public methods

        public static void Configure(Arelle_Setup_Completed arelle_setup_completed)
        {
            BackgroundWorker setupWorker = new BackgroundWorker();
            setupWorker.DoWork += delegate(object s, DoWorkEventArgs args)
                {
                    try
                    {
                        CheckArelleClientSetup();// New implementation : check for arelle executable or newer resource zip distribution (in background)
                        //CheckSetup(); // check for arelle executable or newer resource zip distribution (in background)
                    }
                    catch (Exception e)
                    {
                        Logger.WriteLog(eSeverity.Error, string.Format("There was a problem setting up Arelle.{0}", e.Message));
                    }
                };

            // may need this if logger cannot be accessed in background worker thread
            //setupWorker.ProgressChanged += new ProgressChangedEventHandler( messageHandler );

            setupWorker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
            {
                setupWorker = null;  // dereference setupWorker (for GC encouragement)
                arelle_setup_completed(); // enable any menus or UI components which could invoke Arelle
            };

            setupWorker.RunWorkerAsync();
        }

       


        public static string ArelleSourceZipFileLocation()
        {
          bool flag = SolvencyII.UI.Shared.Functions.MachineType.Is64BitMachine;        

          if (flag)
              return Path.Combine((Path.Combine(Application.StartupPath, ARELLE_FOLDER_64BIT)), "arelle-cmd64-t4u.zip");
          else
              return Path.Combine((Path.Combine(Application.StartupPath, ARELLE_FOLDER_32BIT)), "arelle-cmd32-t4u.zip");                
               
        }

        public static string[] ArelleTaxonomiesFolderPackageFiles()
        {
            if (StaticSettings.DbType == DbType.SolvencyII_Preparatory)
                return Directory.GetFiles(Path.Combine(Application.StartupPath, ARELLE_FOLDER_TAXONOMIES),
                                         "EIOPA_SolvencyII_Preparatory_XBRL_Taxonomy_152c.zip");

            else if (StaticSettings.DbType == DbType.SolvencyII)
                return Directory.GetFiles(Path.Combine(Application.StartupPath, ARELLE_FOLDER_TAXONOMIES),
                                         "EIOPA_SolvencyII_XBRL_Taxonomy_2.0.0.zip");


            else throw new Exception("No Taxonomy file found");
            //return Directory.GetFiles(Path.Combine(Application.StartupPath, ARELLE_FOLDER_TAXONOMIES),
            //                          "*.zip");
        }

        public static string ArelleFolder()
        {
             //Old path
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ARELLE_FOLDER);
            
        }

        public static string ArelleInstallationFolder()
        { 
                return (Path.Combine(Application.StartupPath, "Arelle"));
        }

        #endregion


        #region Worker methods


        private static void CheckArelleClientSetup()
        {
           
            string arelleInstallationFolder = ArelleInstallationFolder();
            string locationZip = ArelleSourceZipFileLocation();
            string deployedVersionInfo = Path.Combine(arelleInstallationFolder, "deployed_version.txt");

            bool isInstallationRequired = false;
            string currentDeploymentVersion = string.Empty;
            
            bool arelleFilesExist =
                  Directory.Exists(arelleInstallationFolder) &&
                  File.Exists(Path.Combine(arelleInstallationFolder, "arelleCmdLine.exe")) &&
                  File.Exists(Path.Combine(arelleInstallationFolder, "version.txt")) &&
                  File.Exists(Path.Combine(arelleInstallationFolder, "library.zip")) &&
                  File.Exists(Path.Combine(arelleInstallationFolder, "deployed_version.txt"));
            
            FileInfo fileInfo = new FileInfo(locationZip);           
            DateTime fileDate = fileInfo.LastWriteTimeUtc;
            Stream fStream = fileInfo.OpenRead();
            long fileDateTicks = fileDate.Ticks;
            fStream.Dispose(); // Here to force stream to loose file
            currentDeploymentVersion = string.Format("{0}", fileDateTicks);

            if (arelleFilesExist)
            {
                long viewedTicks = ReadDeploymentVersionTicks(); // Read version from the deployed_version.txt
                if (viewedTicks == 0 || fileDateTicks != viewedTicks)
                    isInstallationRequired = true; 
            }

            if (isInstallationRequired || !arelleFilesExist)//Need to install/extract the arelle-cmd**-t4u
            {
                ArelleSetupInClient(arelleInstallationFolder, locationZip, deployedVersionInfo, currentDeploymentVersion);
               
            }

        }

        private static void CheckSetup()
        {

            // Check to make sure arelle installed
            string exePath = Path.GetDirectoryName(Application.ExecutablePath);
            string datafilesPath = Path.Combine(exePath, "DataFiles");
            bool is64bit = Marshal.SizeOf(typeof(IntPtr)) == 8;  // test if running on 64 bit architecture (else 32 bit)
            var arelleZips = Directory.GetFiles(Directory.Exists(datafilesPath) ? datafilesPath : exePath,
                                                (is64bit) ? "arelle-cmd64-*.zip" : "arelle-cmd32-*.zip" );
           
            if (arelleZips.Length > 0)
            {
                Array.Sort(arelleZips, StringComparer.InvariantCultureIgnoreCase);
                string locationZip = arelleZips[arelleZips.Length - 1];

                // location where arelle is installed
                string arelleFolder = ArelleFolder();
                // be sure folder, exe, compiled python (library.zip) and version info file are present
                bool arelleFilesExist = Directory.Exists(arelleFolder) &&
                    File.Exists(Path.Combine(arelleFolder, "arelleCmdLine.exe")) &&
                    File.Exists(Path.Combine(arelleFolder, "version.txt")) &&
                    File.Exists(Path.Combine(arelleFolder, "library.zip"));

                // Check the file date / time
                FileInfo fileInfo = new FileInfo(locationZip);
                DateTime fileDate = fileInfo.LastWriteTimeUtc;
                Stream fStream = fileInfo.OpenRead();
                long fileDateTicks = fileDate.Ticks;
                fStream.Dispose(); // Here to force stream to loose file

                long viewedTicks = ReadRegistryFileTicks(); // Read from registry
                if (viewedTicks == 0 || fileDateTicks != viewedTicks || !arelleFilesExist)
                {

                    // Install the zip
                    CheckVersionBeforeInstall(locationZip, arelleFolder, arelleFilesExist);

                    string updateReg = string.Format("{0}", fileDateTicks);
                    WriteRegistry(KEY_FILE, updateReg); //  Write to registry - Date of zip
                }

            }
            else
            {
                Logger.WriteLog(eSeverity.Error, "Arelle installation resource is not in this build (arelle-cmd32-*.zip)");
            }
        }

        private static void ArelleSetupInClient(string arelleInstallationFolder, string locationZip, string deployedVersionInfo, string currentDeploymentVersion)
        {

            if (Directory.Exists(arelleInstallationFolder))
            {
                try
                {
                    //Directory.Delete(arelleInstallationFolder, true); // remove prior version

                    string[] filePaths = Directory.GetFiles(arelleInstallationFolder);
                    foreach (string filePath in filePaths)
                    {
                        var name = new FileInfo(filePath).Name;
                        name = name.ToLower();
                        if (name != "EIOPA_SolvencyII_Preparatory_XBRL_Taxonomy_152c.zip" || name != "EIOPA_SolvencyII_XBRL_Taxonomy_2.0.0.zip")
                        {
                            File.Delete(filePath);
                        }
                    }

                }
                catch (System.IO.IOException) // directory can't be deleted maybe, try to delete individual files
                {
                }
            }

            //overwrite the existing version
            using (ZipFile zip = ZipFile.Read(locationZip))
                foreach (ZipEntry e in zip)
                {
                    e.Extract(arelleInstallationFolder, ExtractExistingFileAction.OverwriteSilently);
                }
            //Write the version details
            if (File.Exists(deployedVersionInfo))
                File.Delete(deployedVersionInfo);

            if (!File.Exists(deployedVersionInfo))
            {
                FileStream fs = new FileStream(deployedVersionInfo, FileMode.OpenOrCreate);
                StreamWriter str = new StreamWriter(fs);
                str.BaseStream.Seek(0, SeekOrigin.End);
                str.Write(currentDeploymentVersion);
                str.Flush();
                str.Close();
                fs.Close();

            }

        }


        private static void CheckVersionBeforeInstall(string locationZip, string arelleFolder, bool arelleFilesExist)
        {
            // Gather the version of the zip file.
            string zipVer = GetVersionOfZip(locationZip);

            // check if tdLocalData matches this version & that dir exists and at least one file exists
            if (zipVer != ReadRegistryString(KEY_VERSION) || !arelleFilesExist)
            {
                if (Directory.Exists(arelleFolder))
                {
                    try
                    {
                        Directory.Delete(arelleFolder, true); // remove prior version
                    }
                    catch (System.IO.IOException) // directory can't be deleted maybe, try to delete individual files
                    {
                        foreach (string file in new string[] { "arelleCmdLine.exe", "version.txt", "library.zip" })
                            File.Delete(Path.Combine(arelleFolder, file));
                        // if undeletable exception here, propogate up and give up
                    }
                }

                using (ZipFile zip = ZipFile.Read(locationZip))
                    foreach (ZipEntry e in zip)
                        e.Extract(arelleFolder, ExtractExistingFileAction.OverwriteSilently);

                WriteRegistry(KEY_VERSION, zipVer);

            }

        }

        private static string GetVersionOfZip(string locationZip)
        {
            string version;
            try
            {
                MemoryStream stream = new MemoryStream();
                using (ZipFile zip = ZipFile.Read(locationZip))
                {
                    // Find the file
                    ZipEntry e = zip["version.txt"];
                    // Read the file
                    e.Extract(stream);
                }
                TextReader reader = new StreamReader(stream);
                stream.Position = 0;
                version = reader.ReadLine();
                reader.Close();
                reader.Dispose();
                stream.Dispose();
                if (string.IsNullOrEmpty(version)) version = "0";
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                version = "0";
            }
            return version;
        }

        private static void WriteRegistry(string key, string ticks)
        {
            ModifyRegistry reg = new ModifyRegistry();
            reg.Write(key, ticks);
        }

        private static long ReadRegistryFileTicks()
        {
            ModifyRegistry reg = new ModifyRegistry();
            string result = reg.Read(KEY_FILE);
            long ticks;
            if (long.TryParse(result, out ticks))
                return ticks;
            return 0;
        }

        private static long ReadDeploymentVersionTicks()
        {            
            string deploymentVersion = Path.Combine(ArelleInstallationFolder(), "deployed_version.txt");
            if (!File.Exists(deploymentVersion))
                return 0;
            else
            {
                if (File.Exists(deploymentVersion))
                {                   
                    using (TextReader tr = new StreamReader(deploymentVersion))
                    {
                        long ticks=0;
                        if (tr != null)
                        {                           
                                long.TryParse((string)tr.ReadLine(), out ticks);
                                return ticks;                           
                        }
                        else
                            return 0;
                    }
                }
            }
            return 0;
            
        }

        private static string ReadRegistryString(string key)
        {
            ModifyRegistry reg = new ModifyRegistry();
            string result = reg.Read(key);
            return result;
        }

        #endregion

    }
}
