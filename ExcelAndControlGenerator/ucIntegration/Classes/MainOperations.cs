using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Data.Shared.Helpers;
using SolvencyII.Domain;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.UI.Shared.Managers;
using ucGenerator;
using ucGenerator.Classes;
using ucGenerator.Extensions;
using System.Diagnostics;

namespace ucIntegration.Classes
{
    public static class MainOperations
    {
        private static string _version;
        private static string GetVersion()
        {
            if (string.IsNullOrEmpty(_version))
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                _version = string.Format("{0}", version);
            }
            return _version;
        }

        public static bool Cancel { get; set; }

        public static bool CreateUserControl(TreeBranch paramItem, string path, string version, out string response, bool iOS, bool headerOnly, string connString = null)
        {
            // Cleanuup any files that exist.
            if (File.Exists(path)) File.Delete(path);
            if (File.Exists(Path.ChangeExtension(path, "designer.cs"))) File.Delete(Path.ChangeExtension(path, "designer.cs"));
            if (File.Exists(Path.ChangeExtension(path, "Ctrl.cs"))) File.Delete(Path.ChangeExtension(path, "Ctrl.cs"));
            if (File.Exists(Path.ChangeExtension(path, "Ctrl.designer.cs"))) File.Delete(Path.ChangeExtension(path, "Ctrl.designer.cs"));
            if (File.Exists(Path.ChangeExtension(path, "row.cs"))) File.Delete(Path.ChangeExtension(path, "row.cs"));
            if (File.Exists(Path.ChangeExtension(path, "row.designer.cs"))) File.Delete(Path.ChangeExtension(path, "row.designer.cs"));

            return CreateUserControlStage2(paramItem, path, version, out response, iOS, headerOnly, connString, (paramItem.SubBranches.Count > 1));

        }

        public static bool CreateUserControlStage2(TreeBranch item, string path, string version, out string response, bool iOS, bool headerOnly, string connString, bool mergedTemplate)
        {
            bool templatesRemoved = false;
            string templatesRemovedDisplayText = "";
            List<string> removedDisplayText = new List<string>();

            CreateFileParameter parameter = new CreateFileParameter();
            parameter.fileName = path;
            parameter.groupIDs = item.GroupTableIDs;
            parameter.frameworkCode = item.FrameworkCode;
            parameter.version = version;
            parameter.iOS = iOS;
            parameter.isTyped = item.IsTyped;
            parameter.title = item.DisplayText;
            parameter.userControlGeneratorVersion = GetVersion();
            parameter.headerOnly = headerOnly;
            
            // Used to ensure correct template parts are build.
            parameter.Head = true;
            parameter.Middle = true;
            parameter.Tail = true;
            List<CreateFileParameter> superParameter = new List<CreateFileParameter>();
            if (mergedTemplate)
            {
                // Add a new parameter for each sub branch
                superParameter.AddRange(item.SubBranches.Select(subBranch => parameter.DeepCopy()));
                for (int i = 0; i < item.SubBranches.Count; i++)
                {
                    superParameter[i].groupIDs = item.SubBranches[i].GroupTableIDs;
                    superParameter[i].isTyped = item.SubBranches[i].IsTyped;
                    superParameter[i].displayText = item.SubBranches[i].DisplayText;
                }

                // At this point we will now remove all typed templates from the SuperParameters
                templatesRemoved = superParameter.Any(p => p.isTyped);
                
                if (templatesRemoved)
                {
                    removedDisplayText.AddRange(superParameter.Where(p => p.isTyped).Select(p => p.displayText));
                    superParameter.RemoveAll(p => p.isTyped);
                }

                // If there are no non-typed templates then do not continue;
                if (superParameter.Count == 0)
                {
                    Debug.WriteLine(string.Format("Merge Template {0} does not have any non-type templates", parameter.title));
                    response = "";
                    return true;
                }

            }


            if (Path.GetFileNameWithoutExtension(path).IsAlphaNum())
            {
                CreateFileParameter workingParameter;

                // Get data object
                using (GetSQLData getData = connString != null ? new GetSQLData(connString) : new GetSQLData())
                {
                    
                    // We have a merged template here so do all the calculations now ready to send.
                    if (!mergedTemplate)
                    {
                        // The standard template
                        GatherTheData(item, getData, parameter);
                        workingParameter = parameter;
                    }
                    else
                    {
                        int iteration = 0;
                        foreach (CreateFileParameter createFileParameter in superParameter)
                        {
                            TreeBranch matchedItem = item.SubBranches.FirstOrDefault(i => i.GroupTableIDs == createFileParameter.groupIDs);
                            if (matchedItem != null)
                            {
                                matchedItem.Merged = true;
                                GatherTheData(matchedItem, getData, createFileParameter);
                                GetTemplateType(matchedItem, true, createFileParameter, createFileParameter);
                            }
                            else
                            {
                                throw new ApplicationException("A miss-match has taken place. A CreateFileParameter did not find a matching TreeItem based upon GroupID.");
                            }
                            //item.SubBranches[iteration].Merged = true;
                            //GatherTheData(item.SubBranches[iteration], getData, createFileParameter);
                            //GetTemplateType(item.SubBranches[iteration], true, createFileParameter, createFileParameter);

                            iteration++;
                        }

                        // Remove all semi open & fully open templates.
                        bool workingFlag = superParameter.Any(p => p.mulitpleRowUserControls.Count > 0 && p.mulitpleRowUserControls[0].AxisOrientation == "X");
                        if (workingFlag)
                        {
                            // Semi-open
                            templatesRemoved = true;
                            removedDisplayText.AddRange(superParameter.Where(p => p.mulitpleRowUserControls.Count > 0 && p.mulitpleRowUserControls[0].AxisOrientation == "X").Select(p => p.displayText));
                            superParameter.RemoveAll(p => p.mulitpleRowUserControls.Count > 0 && p.mulitpleRowUserControls[0].AxisOrientation == "X");
                        }
                        workingFlag = superParameter.Any(p => p.mulitpleRowUserControls.Count > 0 && p.mulitpleRowUserControls[0].AxisOrientation == "Y");
                        if (workingFlag)
                        {
                            // Semi-open
                            templatesRemoved = true;
                            removedDisplayText.AddRange(superParameter.Where(p => p.mulitpleRowUserControls.Count > 0 && p.mulitpleRowUserControls[0].AxisOrientation == "Y").Select(p => p.displayText));
                            superParameter.RemoveAll(p => p.mulitpleRowUserControls.Count > 0 && p.mulitpleRowUserControls[0].AxisOrientation == "Y");
                        }
                        workingFlag = superParameter.Any(p => p.twoDimOpen);
                        if (workingFlag)
                        {
                            // Special cases
                            templatesRemoved = true;
                            removedDisplayText.AddRange(superParameter.Where(p => p.twoDimOpen).Select(p => p.displayText));
                            superParameter.RemoveAll(p => p.twoDimOpen);
                        }

                        CompareCreateFileParameter compare = new CompareCreateFileParameter();
                        superParameter.Sort(compare);

                        workingParameter = superParameter[0];
                    }
                }
                //getData.Dispose();

                

                // Work out the template type and if an add button is needed
                // The add button is on semi - open templates (basically replicateable closed templates).
                parameter.addButton = false;
                GetTemplateType(item, mergedTemplate, workingParameter, parameter);


                // Ensure all GroupTableIDs are included
                if (mergedTemplate)
                {

                    parameter.templateType = enumClosedTemplateType.FixedDimension;
                    // Table Group IDs
                    List<string> groupTableIds = new List<string>();
                    if(!string.IsNullOrEmpty(item.GroupTableIDs)) groupTableIds.AddRange(item.GroupTableIDs.Split('|'));
                    foreach (TreeBranch branch in item.SubBranches)
                    {
                        if(!string.IsNullOrEmpty(branch.GroupTableIDs)) groupTableIds.AddRange(branch.GroupTableIDs.Split('|'));
                    }
                    string groupTables = string.Join("|", groupTableIds.Distinct());
                    parameter.groupIDs = groupTables;



                    // ******************************************************
                    // *** Summate the SuperParameters into the Parameter ***
                    // ******************************************************

                    List<string> tableListDelimited = new List<string>();
                    List<string> typeListDelimited = new List<string>();
                    List<FormDataPage> pageDataCollection = new List<FormDataPage>();
                    List<NPageData> nPageDataCollection = new List<NPageData>();
                    List<AxisOrdinateControls> controlListCollection = new List<AxisOrdinateControls>();
                    List<AxisOrdinateControls> mulitpleRowUserControlsCollection = new List<AxisOrdinateControls>();
                    List<FactInformation> shadedControlsCollection = new List<FactInformation>();
                    List<OpenColInfo2> columnDataCollection = new List<OpenColInfo2>();

                    if (!string.IsNullOrEmpty(parameter.tableListDelimited)) tableListDelimited.AddRange(parameter.tableListDelimited.Split('|'));
                    if (!string.IsNullOrEmpty(parameter.typeListDelimited)) typeListDelimited.AddRange(parameter.typeListDelimited.Split('|'));

                    if (parameter.nPageData != null && parameter.nPageData.Count > 0) nPageDataCollection = parameter.nPageData;
                    if (parameter.controlList != null && parameter.controlList.Count > 0) controlListCollection = parameter.controlList;
                    if (parameter.mulitpleRowUserControls != null && parameter.mulitpleRowUserControls.Count > 0) mulitpleRowUserControlsCollection = parameter.mulitpleRowUserControls;
                    if (parameter.shadedControls != null && parameter.shadedControls.Count > 0) shadedControlsCollection = parameter.shadedControls;
                    if (parameter.columnData != null && parameter.columnData.Count > 0) columnDataCollection = parameter.columnData;
                    
                    foreach (CreateFileParameter par in superParameter)
                    {
                        par.templateType = enumClosedTemplateType.FixedDimension;
                        if (!string.IsNullOrEmpty(par.tableListDelimited)) tableListDelimited.AddRange(par.tableListDelimited.Split('|'));
                        if (!string.IsNullOrEmpty(par.typeListDelimited)) typeListDelimited.AddRange(par.typeListDelimited.Split('|'));

                        if (par.nPageData != null && par.nPageData.Count > 0) nPageDataCollection.AddRange(par.nPageData);
                        if (par.pageData != null && par.pageData.Count > 0) pageDataCollection.AddRange(par.pageData);
                        if (par.controlList != null && par.controlList.Count > 0) controlListCollection.AddRange(par.controlList);
                        if (par.mulitpleRowUserControls != null && par.mulitpleRowUserControls.Count > 0) mulitpleRowUserControlsCollection.AddRange(par.mulitpleRowUserControls);
                        if (par.shadedControls != null && par.shadedControls.Count > 0) shadedControlsCollection.AddRange(par.shadedControls);
                        if (par.columnData != null && par.columnData.Count > 0) columnDataCollection.AddRange(par.columnData);
                    }
                    string tableNames = string.Join(",", tableListDelimited.Distinct());
                    parameter.tableListDelimited = tableNames;

                    string typeNames = string.Join(",", typeListDelimited.Distinct());
                    parameter.typeListDelimited = typeNames;

                    parameter.nPageData = nPageDataCollection.Distinct().ToList();
                    parameter.pageData = pageDataCollection.Distinct().ToList();
                    parameter.controlList = controlListCollection;
                    parameter.mulitpleRowUserControls = mulitpleRowUserControlsCollection;
                    parameter.shadedControls = shadedControlsCollection;
                    parameter.columnData = columnDataCollection;
                }


                templatesRemovedDisplayText = removedDisplayText.MyJoin(", ");

                // Action the main class creation routine.
                CreateUserControl userControl = new CreateUserControl();
                if (userControl.CreateFiles(parameter, superParameter, templatesRemoved, templatesRemovedDisplayText))
                {
                    response = "Created Successfully";
                    return true;
                }
                response = String.Format("Unfortunately there was an error creating the file.\n{1}\n{0}", userControl.Error().Message, item.TableCode);
                return false;
            }
            response = "The selected file name is used for the class generation and it is invalid. Use only alphanumerics and no spaces.";
            return false;
        }

       

        private static void GetTemplateType(TreeBranch item, bool mergedTemplate, CreateFileParameter workingParameter, CreateFileParameter parameter)
        {
            parameter.templateType = enumClosedTemplateType.FixedDimension;
            if (workingParameter.mulitpleRowUserControls.Any())
            {
                parameter.addButton = true;
                if (workingParameter.mulitpleRowUserControls[0].AxisOrientation == "X") parameter.templateType = enumClosedTemplateType.VerticalSeparator;
                if (workingParameter.mulitpleRowUserControls[0].AxisOrientation == "Y") parameter.templateType = enumClosedTemplateType.HorizontalSeparator;
                if (workingParameter.twoDimOpen) parameter.templateType = enumClosedTemplateType.None;
            }

            // With multiple tables we have to use FixedDimension format
            //if (item.GroupTableIDs.Split('|').Count() > 1)
            if (mergedTemplate || item.GroupTableIDs.Split('|').Count() > 1)
                parameter.templateType = enumClosedTemplateType.FixedDimension;
        }

        private static void GatherTheData(TreeItem item, GetSQLData getData, CreateFileParameter parameter)
        {
            // Get the list of grouped tables
            List<string> ids = item.GroupTableIDs.Split('|').ToList();

            // Get full list of controls required for the ui control
            List<AxisOrdinateControls> controlList = getData.GetControlInformation(ids).ToList();

            // Get combo information
            getData.GetControlComboInformation(ref controlList);

            // Special Cases
            List<AxisOrdinateControls> mulitpleRowUserControls = controlList.Where(c => c.SpecialCase == "multiply rows/columns").ToList();
            if (mulitpleRowUserControls.Any())
            {
                // We need to populate the DimXbrlCode
                getData.GetControlDimXbrlCodeForSpecialCases(ref mulitpleRowUserControls, ids);
            }
            parameter.mulitpleRowUserControls = mulitpleRowUserControls;

            // Work out if this template is open in two dimensions.
            int specialCols = controlList.Count(c => c.SpecialCase == "multiply rows/columns");
            bool twoDimOpen = (specialCols == 2);


            // Get ordinateIDs across all controls and check if they should be shaded.
            List<FactInformation> shadedControls = getData.GetAllTableControls(ids, item.SingleZOrdinateID).ToList();

            // If a table is grouped get their location - based upon Excel positioning.
            List<string> locationRanges = null;
            if (item.Merged)
            {
                locationRanges = getData.GetLocationRanges(ids);
            }

            // Work out the parts for user drop downs AND get the linked table names
            // Each of these are combos;
            List<string> tableNames;
            List<FormDataPage> pageData = getData.GetClosedPageData(ids, out tableNames, item.SingleZOrdinateID);
            // Prepare a table delimited list
            string tableNamesParameter = tableNames.MyJoin("|");


            // Make sure no PageControls are here that are also in multipleRowUserControls
            pageData.RemoveAll(p => parameter.mulitpleRowUserControls.Exists(c => c.AxisID == p.AxisID));

            // Here we get the information that is used to populate nPage details. (Can be combos or fixed text - Text was the case with previous version.)
            List<NPageData> nPageData = getData.GetnPageData(pageData.Where(p => !p.FixedDimension));
            // Check to see if they are typed and need textComboBoxes
            foreach (FormDataPage dataPage in pageData)
            {
                dataPage.IsTypedDimension = getData.GetIsTypedDimension(dataPage.DOM_CODE);
            }

            // Prepare parameters specifying Types and TableNames for the grouped and non grouped tables
            string typeListDelimited = "";
            string tableListDelimited = "";
            string pkListDelimited = "";
            foreach (string tableName in tableNames)
            {
                if (typeListDelimited.Length != 0)
                {
                    typeListDelimited += ", ";
                    tableListDelimited += ", ";
                    pkListDelimited += ", ";
                }
                typeListDelimited += String.Format("typeof({0})", tableName);
                tableListDelimited += String.Format(@"""{0}""", tableName);
                pkListDelimited += "0";
            }


            // Position of the OpenTableTemplate Grid
            int gridTop = 10;
            // Column Information for the OpenTableTemplate Grid
            List<OpenColInfo2> columnData = new List<OpenColInfo2>();
            List<OpenColInfo2> rowData = new List<OpenColInfo2>();
            List<OpenColInfo2> cellData = new List<OpenColInfo2>();

            if (item.IsTyped || twoDimOpen)
            {
                // Open table so work out relevant information
                // Top position
                if (pageData.Any(p => !p.FixedDimension)) gridTop = 60;

                // Columns
                OpenTableColumnManager2 columnManager2 = new OpenTableColumnManager2(item.TableID.ToString(), getData, controlList, twoDimOpen);
                columnData = columnManager2.ColumnInfo;
                if (twoDimOpen)
                {
                    rowData = columnManager2.RowInfo;
                    cellData = columnManager2.CellInfo;
                }
            }

            // Releaase the data object

            parameter.controlList = controlList;
            parameter.shadedControls = shadedControls;
            parameter.locationRanges = locationRanges;
            parameter.typeListDelimited = typeListDelimited;
            parameter.tableListDelimited = tableListDelimited;
            parameter.pageData = pageData;
            parameter.nPageData = nPageData;
            parameter.columnData = columnData;
            parameter.rowData = rowData;
            parameter.cellData = cellData;
            parameter.gridTop = gridTop;
            parameter.tableNamesParameter = tableNamesParameter;
            parameter.twoDimOpen = twoDimOpen;


        }

        public static void CreateAllFiles(string version, bool createProjectFiles, TreeBranch trunk, string folder, bool iOSChecked, GenericDelegates.StringResponse messageBox, GenericDelegates.StringResponse toolBar, GenericDelegates.TwoInts progress)
        {
            Cancel = false;
            // Use the collated treeview references to nodes and sequentially list them.
            if (trunk != null)
            {
                List<string> files = new List<string>();
                List<string> duplicates = new List<string>();
                StringBuilder sb = new StringBuilder();

                int templateCount = TreeComputations.CountTemplatesInTree(trunk);
                TreeComputations treeComp = new TreeComputations(trunk);
                int count = 1;

                foreach (TreeBranch treeItem in treeComp.TemplatesInATree())
                {
                    // Nested sub branches are not allowed here.
                    if (treeItem.HasBranches && (treeItem.SubBranches.Any(branch => branch.SubBranches.Count > 0))) continue;

                    //// Merged templates should not contain typed templates.
                    //if (treeItem.HasBranches && (treeItem.SubBranches.Any(b => b.IsTyped))) continue;

                    if (!Cancel)
                    {
                        progress(count, templateCount);
#if !ENABLE_MERGE_TEMPLATES
                        if (treeItem.SubBranches.Count == 0)
#else
                        if (treeItem.SubBranches.Count == 0 || (!treeItem.SubBranches.Any(b => b.SubBranches.Count > 0) && (treeItem.SubBranches.Count != 1)))
#endif
                            if (TreeItemCreateControl(version, files, sb, treeItem, folder, iOSChecked, duplicates, messageBox, toolBar)) return;

                        count++;
                    }
                }


                if (createProjectFiles)
                {
                    // Build the project file now.
                    if (!iOSChecked)
                    {
                        string projName = String.Format("SolvencyII.{0}_UserControls.csproj", version);
                        projName = Path.Combine(folder, projName);
                        CreateProject project = new CreateProject();
                        if (!project.Create(projName, files.Distinct().ToList(), version))
                            sb.AppendLine("Failed to create project file");
                    }
                    else
                    {
                        string switchFile = String.Format("TemplateIndex.cs", version);
                        switchFile = Path.Combine(folder, switchFile);
                        CreateSwitchFile switchGen = new CreateSwitchFile();
                        if (!switchGen.Create(switchFile, files.Distinct().ToList(), version))
                            sb.AppendLine("Failed to create project file");

                    }
                }
                Console.Beep(5000, 500);
                if (sb.Length == 0)
                {
                    messageBox(String.Format("{0} user controls successfully created.\r\n{1} duplicates or pre-existing user controls", files.Count(), duplicates.Count()));
                    toolBar("Complete");
                }
                else
                    messageBox(String.Format("There were some errors:\r\n{0}", sb));
            }
            else
            {
                toolBar("No templates found in the database.");
            }
            
        }

        public static bool TreeItemCreateControl(string version, List<string> files, StringBuilder sb, TreeBranch treeItem, string folder, bool iOS, List<string> duplicates, GenericDelegates.StringResponse messageBox, GenericDelegates.StringResponse toolBar, string connString = null)
        {
            if (!Cancel)
            {
                if (String.IsNullOrEmpty(treeItem.TableCode))
                {
                    bool debugStop = true;
                }


                // T__S_01_01_01_01__s2md__1.0.0

                string fileName = treeItem.GetClassName(false) + ".cs";
                string path = Path.Combine(folder, fileName);
                if (!File.Exists(path))
                {
                    string response;
                    if (CreateUserControl(treeItem, path, version, out response, iOS, false, connString))
                        files.Add(fileName);
                    else
                        sb.AppendLine(response);
                }
                else
                {
                    duplicates.Add(fileName);
                }
            }
            else
            {
                messageBox(String.Format("Cancelled."));
                toolBar("Cancelled");
                return true;
            }
            return false;
        }

        public static void CreateAllPocos(string folder, GenericDelegates.StringResponse messageBox, GenericDelegates.StringResponse toolBar, string connString = null)
        {
            // Use the collated treeview references to nodes and sequentially list them.
            Cancel = false;
            List<string> tqbles = new List<string>();
            StringBuilder sb = new StringBuilder();

            // Get the table names
            GetSQLData getData = connString != null ? new GetSQLData(connString) : new GetSQLData();
            List<string> tables = getData.GetTableNames();

            StringBuilder sbIntegity = new StringBuilder();
            // Loop through the tables
            CreatePoco poco = new CreatePoco();
            foreach (string table in tables)
            {

                Debug.WriteLine(table);

                // Get table info
                List<PocoColInfo> tableInfo = getData.GetTableInfo(table);
                List<MAPPING> tableMapping = getData.GetTableMapping(table);
                string check = CheckPocoMapping(tableInfo, tableMapping);
                if(!string.IsNullOrEmpty(check))
                    sbIntegity.AppendLine(string.Format("{0} ({1})", check, table));

                // Create the files
                string className = String.Format("{0}.cs", table);
                className = Path.Combine(folder, className);
                if (!poco.Create(className, table, tableInfo, tableMapping))
                    sb.AppendLine(string.Format("Failed to create poco file {0}", table));

            }

            if (sbIntegity.Length != 0)
            {
                var exception = sbIntegity.ToString();
                //BRAG_Jeremi
                //throw new SystemException(sbIntegity.ToString());
            }

            // Update UI
            if (sb.Length == 0)
            {
                messageBox(String.Format("Created {0} poco controls successfully.", tables.Count()));
                toolBar("Complete");
                var sbString = sb.ToString();
            }
            else
                messageBox(String.Format("There were some errors:\n{0}", sb));

        }

        private static string CheckPocoMapping(List<PocoColInfo> tableInfo, List<MAPPING> tableMapping)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbInner = new StringBuilder();
            foreach (PocoColInfo colInfo in tableInfo)
            {
                string nameUpper = colInfo.name.ToUpper();
                if (nameUpper != "PK_ID" && nameUpper != "INSTANCE")
                {
                    MAPPING locate = tableMapping.FirstOrDefault(mapping => mapping.DYN_TAB_COLUMN_NAME.ToUpper() == nameUpper);
                    bool found = locate != null;
                    if (found)
                    {
                        if (DataTypeCheck.Check(colInfo.type, locate))
                            continue;
                    }
                    sbInner.Append(string.Format("{0} ", nameUpper));
                }
            }
            if (sbInner.Length > 0)
            {
                string tableName = "Not Knowm";
                if (tableMapping.Count > 0) tableName = tableMapping[0].DYN_TABLE_NAME;
                sb.AppendLine(string.Format("Absent fields or mismatched data types from mapping for table {0}: {1}", tableName, sbInner.ToString()));
            }

            sbInner.Length = 0;

            foreach (MAPPING colMapping in tableMapping)
            {
                string nameUpper = colMapping.DYN_TAB_COLUMN_NAME.ToUpper();
                PocoColInfo locate = tableInfo.FirstOrDefault(mapping => mapping.name.ToUpper() == nameUpper);
                bool found = locate != null;
                if (found)
                {
                    if (DataTypeCheck.Check(locate.type, colMapping))
                        continue;
                }
                sbInner.Append(string.Format("{0} ", nameUpper));
            }
            if (sbInner.Length > 0)
            {
                string tableName = "Not Knowm";
                if (tableMapping.Count > 0) tableName = tableMapping[0].DYN_TABLE_NAME;
                sb.AppendLine(string.Format("Absent fields or mismatched data types from concrete class for table {0}: {1}", tableName, sbInner.ToString()));
            }

            return sb.ToString();

        }

    }
}
