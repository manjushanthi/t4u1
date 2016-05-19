using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using AT2DPM.DAL;
using AT2DPM.DAL.Model;
using T4UImportExportGenerator;
using T4UImportExportGenerator.Domain;

namespace ExcelGenCmd
{
    class Program
    {
        static bool done = false;
        static void ErrorHandler()
        {

            Console.WriteLine("Error: unrecognized or incomplete command line.");
            Console.WriteLine();
            Console.WriteLine("Usage: ");
            Console.WriteLine("xlsgencmd -d <<directory>> -v <<version info>> -i <<input database>> [-a or -m <<module1, module2, etc>>] -t [\"BASIC\"|\"BUSINESS\"|\"ENUMERATION\"]");
            Console.WriteLine();
            Console.WriteLine("Where: ");
            Console.WriteLine("-d: Path of the dictory where file to be generated.");
            Console.WriteLine("-v: Version number of the file.");
            Console.WriteLine("-i: Input database file.");
            Console.WriteLine("-a: Optional parameter to generate all modules.");
            Console.WriteLine("-m: Optional parameter to select particular modules. The modules are seperated by comma.");
            Console.WriteLine("-t: Type of template BASIC or BUSINESS or ENUMERATION ");

        }

        static void Main(string[] args)
        {
            string file;
            string directory;
            string version;
            string[] modules = null;
            bool allModules = false;
            ExcelTemplateType type;

            // Command line parsing
            Arguments cmdLine = new Arguments(args);

            if (cmdLine["d"] != null)
                directory = cmdLine["d"];
            else
            {
                ErrorHandler();
                return;
            }

            if (cmdLine["v"] != null)
                version = cmdLine["v"];
            else
            {
                ErrorHandler();
                return;
            }

            if (cmdLine["i"] != null)
                file = cmdLine["i"];
            else
            {
                ErrorHandler();
                return;
            }

            if (cmdLine["a"] != null || cmdLine["m"] != null)
            {
                if(cmdLine["a"] != null)
                {
                    allModules = true;
                }
                else
                {
                    modules = cmdLine["m"].Split(new char[] { ',' });
                    if(modules.Count() <=0)
                    {
                        ErrorHandler();
                        return;
                    }
                }
            }
            else
            {
                ErrorHandler();
                return;
            }

            if (cmdLine["t"] != null)
            {
                switch (cmdLine["t"]) 
                {
                    case "BASIC":
                        type = ExcelTemplateType.BASIC_TEMPLATE;
                        break;

                    case "BUSINESS":
                        type = ExcelTemplateType.BUSINESS_TEMPLATE;
                        break;

                    case "ENUMERATION":
                        type = ExcelTemplateType.ENUMERATION_TEMPLATE;
                        break;

                    default:
                        ErrorHandler();
                        return;
                }
            }
            else
            {
                ErrorHandler();
                return;
            }

            DPMdb dbContext;

            //Connect to DPM database
            DPMdbConnection dpmConnection = new DPMdbConnection();
            try
            {
                dbContext = dpmConnection.OpenDpmConnection(file);


                var tables = from tab in dbContext.mTables select tab;

                if (tables.Count() > 0)
                    Console.WriteLine("Connection successfull.");

                TemplateGeneration.DbPath = file;

                if(allModules)
                    modules = (from mod in dbContext.mModules
                                          select mod.ModuleCode).ToArray();

                InvokeImportExport.InvokeConsole(dbContext, type, directory, version, modules, ProgressChangedHandler, CompletedHandler);

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured while connecting to database.");
                Console.WriteLine(e.Message);
                done = true;
            }

            while(!done)
            {
                System.Threading.Thread.Sleep(1000);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        private static void ProgressChangedHandler(object sender, ProgressChangedEventArgs args)
        {
            Console.WriteLine(args.ProgressPercentage + "% completed");
            Console.WriteLine((string)args.UserState);
        }

        private static void CompletedHandler(object sender, AsyncCompletedEventArgs args)
        {
            Console.WriteLine((string)args.UserState);
            done = true;
        }
    }
}
