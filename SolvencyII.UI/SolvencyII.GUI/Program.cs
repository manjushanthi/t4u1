using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using SolvencyII.Domain.ENumerators;
using System.IO;
using System.Diagnostics;
using SolvencyII.GUI.Classes;
using SolvencyII.UI.Shared.Loggers;
using Ionic.Zip;
using SolvencyII.Domain.Configuration;
using SolvencyII.Data.Shared;
using SolvencyII.Domain;

namespace SolvencyII.GUI
{
    public class Program
    {
        private static string _applicationEnvironment;

        public static string ApplicationEnvironment
        {
            get { return _applicationEnvironment; }
            set { _applicationEnvironment = value; }
        }

        /// <summary>
        ///  The below method is used to enable the SolvencyII.GUI in the GUI or CONSOLE mode, based upon the parameter passed. 
        /// </summary>
         

        [STAThread]
        public static void Main(string[] args)
        {
           bool consoleModeEnabled=false;
            if (args.Length > 0)
            {
               if(!string.IsNullOrEmpty(args[0]))
               {
                   if(args[0].Trim().ToUpper() == "-CONSOLE")
                   {
                       //To show the unhandled exception in the console
                       AppDomain.CurrentDomain.UnhandledException
                        += delegate(object sender, UnhandledExceptionEventArgs _exceptionArgs)
                        {
                            var exception = (Exception)_exceptionArgs.ExceptionObject;
                            Console.WriteLine("Exception Occured: ");
                            Console.WriteLine("Unhandled exception: " + exception);
                            Console.ReadLine();
                            Environment.Exit(0);
                        };
                       // Command line given, display console
                       consoleModeEnabled = true;
                       AllocConsole();
                       ConsoleMain(args);
                      
                   }
               }
                
            }

            if (consoleModeEnabled == false)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.ApplicationExit += Application_ApplicationExit;

                #if (FOR_NCA)
                                InitializeNCAEnvironment();
                #elif  (FOR_UT)
                                                InitializeUTEnvironment();
                #else
                #error "Compilation variable not set for FOR_NCA nor FOR_UT";
                #endif

                Application.Run(new frmMain());
            }
            
        }
        private static void ConsoleMain(string[] args)
        {
            Console.WriteLine("Windows T4U started in Console mode");
            Sqlite3Setup();
            if (args.Length >= 5)
            {
                //foreach (string s in args)
                //{
                //    Console.WriteLine(s);
                //}
                string dbPath = string.Empty;
                if (args.Length >= 6)
                {
                    if (File.Exists(args[5]) && System.IO.Path.GetExtension(args[5]).ToUpper().EndsWith("XBRT"))
                    {
                        using (GetSQLData getData = new GetSQLData(args[5]))
                        {
                            GetDbVersion(getData);
                        }

                    }
                    else
                    {
                        Console.WriteLine("Invalid Container");
                        Console.ReadLine();
                        return;
                    }

                    if (StaticSettings.DbType == DbType.No_container_selected)
                    {
                        Console.WriteLine("Invalid Container type");
                        Console.ReadLine();
                        return;
                    }
                    else
                    {
                        dbPath = args[5];

                    }
                }


                if (StaticSettings.DbType == DbType.No_container_selected)              
                StaticSettings.DbType = DbType.SolvencyII_Preparatory;
                string applicationPath = Path.Combine(System.Windows.Forms.Application.StartupPath);

                if (string.IsNullOrEmpty(dbPath))
                {
                    CreateTempFolderForCMD(Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp"));
                    bool result = CreateDataBaseForCMD();
                    string folderpath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp");
                    folderpath = Path.Combine(folderpath, "CMD");
                    dbPath = Path.Combine(folderpath, "TempContainer.xbrt");
                    
                }
               
                SolvencyII.CMD.Operations.CMD_Util cmdLine = new SolvencyII.CMD.Operations.CMD_Util(args, dbPath, applicationPath);
                cmdLine.ExecuteCMD();
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Invalid number of arguments");
                Console.ReadLine();
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        

        private static void GetDbVersion(GetSQLData getData)
        {
            aApplication version = getData.CheckDBVersion();
            if (version != null && version.DatabaseType != null)
            {
                DbType dbType = (DbType)Enum.Parse(typeof(DbType), version.DatabaseType);
                StaticSettings.DbType = dbType;
            }
        }

        public static void CreateTempFolderForCMD(string dirPath)
        {
           
            try
            {
                if (!Directory.Exists(dirPath))
                    Directory.CreateDirectory(dirPath);

                string cmdFolder = Path.Combine(System.Windows.Forms.Application.StartupPath, dirPath);
                cmdFolder = Path.Combine(cmdFolder, "CMD");
                if (!Directory.Exists(cmdFolder))
                    Directory.CreateDirectory(cmdFolder);
                else
                {
                    var dir = new DirectoryInfo(cmdFolder);
                    dir.Delete(true);
                    Directory.CreateDirectory(cmdFolder);
                }

            }
            catch (IOException e)
            {
                Logger.WriteLog(eSeverity.Error, string.Format("There was a problem creating temp folder {0}", e.Message));
                
            }
        
        }

        /// <summary>
        /// The below method is used to create the Database, 
        /// if the SolvencyII.GUI is invoked in the console mode without specifying the database in the command line arguments.
        /// </summary>
        

        private static bool CreateDataBaseForCMD()
        {
            string folderpath = Path.Combine(System.Windows.Forms.Application.StartupPath, "Temp");
            folderpath = Path.Combine(folderpath, "CMD");
            if (!Directory.Exists(folderpath))
            {
                //throw errorContainerValidationView
                MessageBox.Show("CMD DB folder not created");
                return false;
            }
            string DBpath = Path.Combine(folderpath, "TempContainer.xbrt");
            string sourceFile = Path.Combine(Application.StartupPath, "T4Udb_Sol2_prep.zip");
            if (File.Exists(DBpath)) File.Delete(DBpath);
            using (ZipFile zip = ZipFile.Read(sourceFile))
                foreach (ZipEntry entry in zip.EntriesSorted)
                {
                    entry.FileName = Path.GetFileName(DBpath);
                    entry.Extract(System.IO.Path.GetDirectoryName(DBpath));
                }

            if (File.Exists(DBpath))
                return true;
            else
                return false;



        }
        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        //public static void Main(string[] args)
        //{
        //    //Global exception handler
        //    Application.ThreadException += ExceptionHandler.Application_ThreadException;
        //    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        //    AppDomain.CurrentDomain.UnhandledException += ExceptionHandler.AppDomain_UnhandledException;

        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    Application.ApplicationExit += Application_ApplicationExit;

        //    //The below code required for SQLite3.dll deployment based upon OS type
        //    #if (FOR_NCA)
        //                InitializeNCAEnvironment();
        //    #elif  (FOR_UT)
        //                    InitializeUTEnvironment();
        //    #else
        //    #error "Compilation variable not set for FOR_NCA nor FOR_UT";
        //    #endif

        //    Application.Run(new frmMain());
        //}

        /// <summary>
        /// The below method is used to initialize the NCA (NATIONAL COMPETENT AUTHORITIES) Version SolvencyII.GUI environment
        /// </summary>

        public static void InitializeNCAEnvironment()
        {
            Sqlite3Setup();
        }

        /// <summary>
        /// The below method is used to initialize the UT (Undertaking) Version SolvencyII.GUI environment
        /// </summary>
        public static void InitializeUTEnvironment()
        {
            Sqlite3Setup();
            DeleteFiles("T4Udb_CRDIV.zip");
            DeleteFiles("T4Udb_Sol2.zip");
            DeleteFiles("SolvencyII.Extensibility_EBA.dll");
            DeleteFiles("SolvencyII.Extensibility_SOL2.dll");
        }

        public static void DeleteFiles(string fileName)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, fileName)))
            {
                File.Delete(Path.Combine(Application.StartupPath, fileName));
            }
        }


        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            // This should stop all threads.
            Environment.Exit(Environment.ExitCode);
        }

        public static string[]  GetFirstCommandLineArgument(string environment)
         {
             ApplicationEnvironment = environment;
            if (Environment.GetCommandLineArgs().Length > 1) {
               
               return Environment.GetCommandLineArgs();

            } 
            else      
            {
               string[] args = new String[1];
               
               if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) {
                  System.Collections.Specialized.NameValueCollection NameValueTable = null;


                  if (AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null) {

                     foreach (string fileparam in AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData) {
                        NameValueTable = System.Web.HttpUtility.ParseQueryString(fileparam);

                     }
                  
                     if (NameValueTable.GetValues(0)[0].Contains("file:///"))
                     {
                        args[0]= NameValueTable.GetValues(0)[0].Replace("file:///", "");
                     }
                  }

               }
               return args;
            }                      
         }

        /// <summary>
        /// The below method is used to deploy the sqlite3.dll, it’s based upon the machine bit architecture (32 bit/64 bit) 
        /// </summary>

        public static void Sqlite3Setup()
        {
            string sqliteTargetFilePath = Path.Combine(Application.StartupPath, "sqlite3.dll");
            FileInfo CorrectSQLiteFI;
            if (SolvencyII.UI.Shared.Functions.OperatingSystemType.Is64BitOperatingSystem())
            {
                CorrectSQLiteFI = new FileInfo(Path.Combine(Path.Combine(Application.StartupPath, "x64"), "sqlite3.dll"));
            }
            else
            {
                CorrectSQLiteFI=new FileInfo(Path.Combine(Path.Combine(Application.StartupPath, "x86"), "sqlite3.dll"));
            }
            
            try
            {
                //Only copy if file size is new
                if (!File.Exists(sqliteTargetFilePath) || 
                    CorrectSQLiteFI.Length!=(new FileInfo(sqliteTargetFilePath)).Length)
                {
                    File.Copy(CorrectSQLiteFI.FullName, sqliteTargetFilePath,true); 
                }
            }   
            catch (IOException ex)
            {

                MessageBox.Show("sqlite3.dll couldn't be copied." + ex.Message);
            
            }

            

        }

           
    }
}
