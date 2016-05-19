using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Entities;
using ucGenerator.Classes;

namespace ucGenerator
{
    /// <summary>
    /// Central class to initiate and manage the generation of template files.
    /// </summary>
    public class CreateUserControl
    {
        private Exception _exception;


        public bool CreateFiles(CreateFileParameter parameter, List<CreateFileParameter> superParameter, bool templatesRemoved, string templatesRemovedDisplayText)
        {

#if !DevFirst
            try
#endif

            {
                // Prepare the class name
                string className = Path.GetFileNameWithoutExtension(parameter.fileName).Replace('.', '_');
                string classNameControl = className + "_ctrl";
                parameter.classNameControl = classNameControl;

                // Removed from live code.
                if (parameter.iOS) return false;

                if (!parameter.headerOnly)
                {
                    /***************************************************************/
                    // Create the main class for this control (iOS does not need this)
                    /***************************************************************/
                    using (FileStream fs = new FileStream(parameter.fileName, FileMode.CreateNew, FileAccess.Write))
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        int tableVID = int.Parse(parameter.groupIDs.Split('|').ToList()[0]);
                        // The open table also has one of these for displaying the selected row;
                        if (!parameter.isTyped)
                            writer.Write(MainClass.GenerateCode(parameter.templateType, className, parameter.groupIDs, tableVID, parameter.frameworkCode, parameter.version, parameter.typeListDelimited, parameter.tableListDelimited, parameter.pkListDelimited, classNameControl, parameter.userControlGeneratorVersion, parameter.twoDimOpen, superParameter));
                        else
                        {
                            writer.Write(MainClassOpen.GenerateCode(className, parameter.groupIDs, tableVID, parameter.frameworkCode, parameter.version, parameter.typeListDelimited, parameter.tableListDelimited, parameter.gridTop, parameter.columnData, parameter.userControlGeneratorVersion));
                        }
                        fs.Flush();
                    }

                    if (parameter.isTyped)
                    {
                        string controlMainFileName = Path.ChangeExtension(parameter.fileName, "row.cs");
                        using (FileStream fs = new FileStream(controlMainFileName, FileMode.CreateNew, FileAccess.Write))
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            int tableVID = int.Parse(parameter.groupIDs.Split('|').ToList()[0]);
                            // The open table also has one of these for displaying the selected row;
                            writer.Write(MainClass.GenerateCode(parameter.templateType, className + "_row", parameter.groupIDs, tableVID, parameter.frameworkCode, parameter.version, parameter.typeListDelimited, parameter.tableListDelimited, parameter.pkListDelimited, classNameControl, parameter.userControlGeneratorVersion, parameter.twoDimOpen, superParameter));
                            fs.Flush();
                        }
                    }


                    /***************************************************************/
                    // User Control MainClass
                    /***************************************************************/

                    string controlMainFileName2 = Path.ChangeExtension(parameter.fileName, "Ctrl.cs");
                    using (FileStream fs = new FileStream(controlMainFileName2, FileMode.CreateNew, FileAccess.Write))
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        int tableVID = int.Parse(parameter.groupIDs.Split('|').ToList()[0]);
                        writer.Write(MainClassControlClosed.GenerateCode(parameter.templateType, classNameControl, parameter.groupIDs, tableVID, parameter.frameworkCode, parameter.version, parameter.typeListDelimited, parameter.tableListDelimited, parameter.pkListDelimited, parameter.templateType, parameter.userControlGeneratorVersion, parameter.twoDimOpen, superParameter));
                        fs.Flush();
                    }


                    /***************************************************************/
                    // Built custom control
                    /***************************************************************/
                    string controlFileName = Path.ChangeExtension(parameter.fileName, "Ctrl.designer.cs");
                    int controlWidth = 0;
                    int controlHeight = 0;
                    using (FileStream fs = new FileStream(controlFileName, FileMode.CreateNew, FileAccess.Write))
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        if (!parameter.twoDimOpen)
                            writer.Write(DesignerClassControlClosed.GenerateCode(parameter, out controlWidth, out controlHeight, false, superParameter, templatesRemoved, templatesRemovedDisplayText));
                        else
                            writer.Write(DesignerClassControlSpecialCase.GenerateCode(parameter.classNameControl, parameter.groupIDs, parameter.controlList, parameter.shadedControls, parameter.mulitpleRowUserControls, parameter.locationRanges, parameter.iOS, parameter.pageData, parameter.title, parameter.templateType, parameter.classNameControl, out controlWidth, out controlHeight, false, parameter.addButton, parameter.tableNamesParameter, parameter.isTyped, parameter.columnData, parameter.rowData, parameter.cellData));
                        fs.Flush();
                    }


                    /***************************************************************/
                    // Build the main designer class here.
                    /***************************************************************/
                    string designerFileName = !parameter.iOS ? Path.ChangeExtension(parameter.fileName, "designer.cs") : parameter.fileName;
                    string customControl = "";

                    using (FileStream fs = new FileStream(designerFileName, FileMode.CreateNew, FileAccess.Write))
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        if (!parameter.isTyped)
                        {
                            if (!parameter.iOS)
                                writer.Write(DesignerClass.GenerateCode(className, parameter.groupIDs, parameter.controlList, parameter.shadedControls, parameter.mulitpleRowUserControls, parameter.locationRanges, parameter.pageData, parameter.nPageData, parameter.title, parameter.templateType, classNameControl, controlWidth, controlHeight, parameter.addButton, parameter.twoDimOpen, superParameter));
                            else
                                writer.Write(DesignerClassiOS.GenerateCode(className, parameter.groupIDs, parameter.controlList, parameter.shadedControls, parameter.locationRanges, parameter.iOS, parameter.pageData, parameter.title, out customControl, parameter.templateType, classNameControl));
                        }
                        else
                        {
                            writer.Write(DesignerClassOpen.GenerateCode(className, parameter.groupIDs, parameter.controlList, parameter.shadedControls, parameter.locationRanges, parameter.iOS, parameter.pageData, parameter.nPageData, parameter.title));
                        }
                        fs.Flush();
                    }


                    if (parameter.isTyped)
                    {
                        string designerFileNameOpenSub = Path.ChangeExtension(parameter.fileName, "row.designer.cs");
                        using (FileStream fs = new FileStream(designerFileNameOpenSub, FileMode.CreateNew, FileAccess.Write))
                        using (StreamWriter writer = new StreamWriter(fs))
                        {
                            if (!parameter.iOS)
                                writer.Write(DesignerClass.GenerateCode(className + "_row", parameter.groupIDs, parameter.controlList, parameter.shadedControls, parameter.mulitpleRowUserControls, parameter.locationRanges, parameter.pageData, parameter.nPageData, parameter.title, parameter.templateType, classNameControl, controlWidth, controlHeight, parameter.addButton, parameter.twoDimOpen, superParameter));
                            fs.Flush();
                        }
                    }
                }
                else
                {
                    // Header only.
                    int controlWidth, controlHeight;
                    parameter.tableNamesParameter = "";
                    DesignerClassControlClosed.GenerateCode(parameter, out controlWidth, out controlHeight, true, superParameter, false, "");
                }

                return true;
            }
#if !DevFirst
            catch (Exception ex)
            {
                _exception = ex;
                return false;
            }
#endif
        }

        public Exception Error()
        {
            return _exception;
        }
    }
}
