using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ucGenerator.Classes;

namespace ucGenerator.Extensions
{
    public static class CreateFileParameterExt
    {
        public static CreateFileParameter DeepCopy(this CreateFileParameter parameter)
        {
            CreateFileParameter result = new CreateFileParameter();

            result.fileName = parameter.fileName;
            result.groupIDs = parameter.groupIDs;
            result.controlList = parameter.controlList;
            result.shadedControls = parameter.shadedControls;
            result.mulitpleRowUserControls = parameter.mulitpleRowUserControls;
            result.frameworkCode = parameter.frameworkCode;
            result.version = parameter.version;
            result.locationRanges = parameter.locationRanges;
            result.iOS = parameter.iOS;
            result.typeListDelimited = parameter.typeListDelimited;
            result.tableListDelimited = parameter.tableListDelimited;
            result.pkListDelimited = parameter.pkListDelimited;
            result.pageData = parameter.pageData;
            result.nPageData = parameter.nPageData;
            result.isTyped = parameter.isTyped;
            result.columnData = parameter.columnData;
            result.rowData = parameter.rowData;
            result.cellData = parameter.cellData;
            result.gridTop = parameter.gridTop;
            result.templateType = parameter.templateType;
            result.title = parameter.title;
            result.headerOnly = parameter.headerOnly;
            result.addButton = parameter.addButton;
            result.tableNamesParameter = parameter.tableNamesParameter;
            result.userControlGeneratorVersion = parameter.userControlGeneratorVersion;
            result.twoDimOpen = parameter.twoDimOpen;

            result.classNameControl = parameter.classNameControl;

            result.Head = parameter.Head;
            result.Middle = parameter.Middle;
            result.Tail = parameter.Tail;


            return result;
        }
    }
}
