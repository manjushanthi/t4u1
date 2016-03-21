using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SolvencyII.ExcelImportExportLib;

namespace SolvencyII.CMD.Operations
{
    public class CMD_Util
    {
        private string[] _args;
        private string _dbpath = null;
        private string _applicationPath = null;
        private bool _inputXBRL = false;
        private bool _outputEXCEL = false;

        /// <summary>
        /// CMD_Util constructor to initialize the parameters required for the command line enabled Solvency II operations 
        /// </summary>
        /// <param name="args">command line arguments</param>
        /// <param name="DBpath">databae</param>
        /// <param name="applicationPath">current application location</param>

        public CMD_Util(string[] args, string DBpath, string applicationPath)
        {
            _args = args;
            _dbpath = DBpath;
            _applicationPath = applicationPath;
        }

        /// <summary>
        /// Method to check the command line parameters, if the parameters are valid then the Import XBRL method excutions will starts
        /// </summary>

        public void ExecuteCMD()
        {
            Console.WriteLine("Execution Starts.....................");

                            if (_args[1].Trim().ToUpper() == "-IXBRL")
                            {
                                if (File.Exists(_args[2]) && System.IO.Path.GetExtension(_args[2]).ToUpper().EndsWith("XBRL") )                                                       
                                     _inputXBRL = true;
                                else
                                    Console.WriteLine("The input file is not valid XBRL file or File not exists test", _args[2]);
                            }
                            if (_args[3].Trim().ToUpper() == "-OEXCEL")                            
                            {
                                if (System.IO.Path.GetExtension(_args[4]).ToUpper().EndsWith("XLSX") && Directory.Exists(Path.GetDirectoryName(_args[4])))
                                {
                                    if (!File.Exists(_args[4]))
                                        _outputEXCEL = true;
                                    else
                                    {
                                        Console.WriteLine("Already the destination excel file exists");
                                        return;
                                    }
                                }
                                else
                                    Console.WriteLine("The input path/file is not valid");                               
                            
                            }
                            if (_inputXBRL == true && _outputEXCEL == true)                           
                                ImportXBRL(_args[2], _args[4]);
                            else
                                Console.WriteLine("Wrong Parameters");

                       
        }

        /// <summary>
        /// ImportXBRL method intialize the ImportXBRLExportExcel class and starts import XBRL operation by calling the method importXBRLExportExcelAsync.
        /// </summary>
        /// <param name="xbrlFilePath">Source XBRL file for import</param>
        /// <param name="destExcelFilePath">Destiantion excel file for export</param>

        public void ImportXBRL(string xbrlFilePath,string destExcelFilePath)
        {
            Console.WriteLine("Import starts.......");
            //return;
            ImportXBRLExportExcel importXBRLExportExcel = new ImportXBRLExportExcel();
            string dbPath = _dbpath;
            importXBRLExportExcel.importXBRLExportExcelAsync(_args[2], _args[4], _dbpath, _applicationPath);
            Console.ReadLine();
            
        }
           
    }
}
