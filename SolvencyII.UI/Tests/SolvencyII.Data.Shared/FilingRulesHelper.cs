﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Tests.SolvencyII.Data.Shared
{
    internal class FilingRulesHelper
    {
        internal static string FileLocation(string fileName, string subFolder = null)
        {
            /*
            string subdir = string.IsNullOrEmpty(subFolder) ? string.Format(@"TestData\FilingRules\{0}", fileName) : string.Format(@"TestData\FilingRules\{0}\{1}", subFolder, fileName);
            string result = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), subdir);
            return result;
             */
            if (string.IsNullOrEmpty(subFolder))
                return string.Format(@"Z:\Documents\mvsl\projects\EIOPA\xbrt\13. XBRL Instance Documents\Filing rules 201\{0}", fileName);
            else
                return string.Format(@"Z:\Documents\mvsl\projects\EIOPA\xbrt\13. XBRL Instance Documents\Filing rules 201\{0}\{1}", subFolder, fileName);
        }

        public static string Sol2PrepDb { get { return @"z:\temp\DBtest1.xbrt"; /* FileLocation( "T4Udb_Sol2_prep.db"); */ } }
        public static string Sol2Db { get { return FileLocation("T4Udb_Sol2.db"); } }
        public static string TestCasesIndexFile { get { return FileLocation("index.xml"); } }
        public static string TestsErrorLog { get { return FileLocation("testsErrorLog.txt"); } }
        public static string TestsResults { get { return FileLocation("testsResults.csv"); } }
    }
}
