using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using AT2DPM.DAL.Model;
using AT2DPM.Events.Delegate;

using T4UImportExportGenerator.Domain;

namespace T4UImportExportGenerator
{
    interface IImportExportGenerate
    {
        event CompletedEventHandler Completed;
        event ProgressChangedEventHandler ProgressChanged;
        void Generate(DPMdb dpmContext, IEnumerable<GenerateInfo> generatorInfo, string versionNumber);
        void GenerateAsync(DPMdb dpmContext, IEnumerable<GenerateInfo> generatorInfo, string versionNumber);
    }
}
