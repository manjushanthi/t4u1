using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SolvencyII.Domain;
using SolvencyII.Domain.Entities;

namespace ucGenerator
{

    /// <summary>
    /// Central class for managing the generation of POCO classes.
    /// </summary>
    public class CreatePoco
    {
        private Exception _exception;


        public bool Create(string fileName, string tableName, List<PocoColInfo> tableInfo, List<MAPPING> tableMapping)
        {
            try
            {
                CreatePocoFile(fileName, tableName, tableInfo, tableMapping);
                return true;
            }
            catch (Exception ex)
            {
                _exception = ex;
                return false;
            }
        }

        private static void CreatePocoFile(string fileName, string tableName, List<PocoColInfo> tableInfo, List<MAPPING> tableMapping)
        {
            // Project File
            if (File.Exists(fileName)) File.Delete(fileName);
            // Make the project file itself
            using (FileStream fs = new FileStream(fileName, FileMode.CreateNew, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(Poco.GenerateCode(fileName, tableName, tableInfo, tableMapping));
                fs.Flush();
            }
        }

        public Exception Error()
        {
            return _exception;
        }
    }
}
