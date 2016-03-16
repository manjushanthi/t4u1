using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SolvencyII.ExcelImportExportLib.Domain;

namespace SolvencyII.ExcelImportExportLib
{
    public static class TemplateFactory
    {
        public static IExcelImport GetExcelImport(ExcelTemplateType _type)
        {
            switch(_type)
            {
                case ExcelTemplateType.BasicTemplate:
                    return new ExcelBasicTemplateImportImpl();

                case ExcelTemplateType.BusinessTemplate:
                    return new ExcelBusinessTemplateImportImpl();

            }

            return null;

        }

        public static IExcelExport GetExcelExport(ExcelTemplateType _type, List<ExcelExportValidationMessage> excelExportValidationMessageLst)
        {
            switch(_type)
            {
                case ExcelTemplateType.BasicTemplate:
                    return new ExcelBasicTemplateExportImpl(excelExportValidationMessageLst);

                case ExcelTemplateType.BusinessTemplate_Macro:
                    return new ExcelBasicTemplateExportImpl(excelExportValidationMessageLst);

                case ExcelTemplateType.BusinessTemplate:
                    return new ExcelBusinessTemplateExportImpl(excelExportValidationMessageLst);

            }

            return null;
        }
    }
}
