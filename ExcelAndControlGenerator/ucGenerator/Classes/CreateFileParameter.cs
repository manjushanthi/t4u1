using System.Collections.Generic;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Entities;

namespace ucGenerator.Classes
{
    /// <summary>
    /// A single parameter that encapulates many.
    /// It could be used on more functions to tidy up but was only introduced on the last generation of changes
    /// </summary>
    public class CreateFileParameter
    {
        
        // Note:
        // Changes made here should be reflected in:
        // CreateFileParameterExt.DeepCopy


        public string fileName { get; set; }
        public string groupIDs { get; set; }
        public List<AxisOrdinateControls> controlList { get; set; }
        public List<FactInformation> shadedControls { get; set; }
        public List<AxisOrdinateControls> mulitpleRowUserControls { get; set; }
        public string frameworkCode { get; set; }
        public string version { get; set; }
        public List<string> locationRanges { get; set; }
        public bool iOS { get; set; }
        public string typeListDelimited { get; set; }
        public string tableListDelimited { get; set; }
        public string pkListDelimited { get; set; }
        public List<FormDataPage> pageData { get; set; }
        public List<NPageData> nPageData { get; set; }
        public bool isTyped { get; set; }
        public List<OpenColInfo2> columnData { get; set; }
        public List<OpenColInfo2> rowData { get; set; }
        public List<OpenColInfo2> cellData { get; set; }
        public int gridTop { get; set; }
        public enumClosedTemplateType templateType { get; set; }
        public string title { get; set; }
        public bool headerOnly { get; set; }
        public bool addButton { get; set; }
        public string tableNamesParameter { get; set; }
        public string userControlGeneratorVersion { get; set; }
        public bool twoDimOpen { get; set; }

        public string displayText { get; set; }

        public string classNameControl { get; set; }

        public bool Head { get; set; }
        public bool Middle { get; set; }
        public bool Tail { get; set; }

    }
}
