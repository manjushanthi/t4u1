using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AT2DPM.DAL.Model;
using T4UImportExportGenerator.Domain;

namespace T4UImportExportGenerator
{
    class ThreadParam
    {
        public DPMdb DpmContext { get; set; }

        public IEnumerable<GenerateInfo> GeneratorInfo { get; set; }
        public string VersionNumber { get; set; }
    }
}
