using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4UImportExportGenerator
{
    public static class TemplateGeneration
    {
        private static DPMdb dpmDb;
               
        public static DPMdb DpmContext
        {
            get { return dpmDb; }
            set { dpmDb = value; }
        }

        private static string dbPath;

        public static string DbPath
        {
            get { return dbPath; }
            set { dbPath = value; }
        }
    }
}
