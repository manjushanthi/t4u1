using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Constants;
using SolvencyII.Domain.Entities;
using System.Linq;
using SolvencyII.Domain.Extensions;
using ucGenerator.Classes;
using ucGenerator.Extensions;


namespace ucGenerator
{
    /// <summary>
    /// Copy taken prior to new format using custom controls.
    /// </summary>
    public static class DesignerClassiOS
    {

        public static string GenerateCode(string className, string groupIDs, List<AxisOrdinateControls> controlList, List<FactInformation> shadedControls, List<string> locationRanges, bool iOS, List<FormDataPage> pageData, string title, out string customControl, enumClosedTemplateType templateType, string classNameControl)
        {
            // Setup the constance
            ConstantsForDesigner constants = new ConstantsForDesigner(iOS);

            // Main Designer.cs;
            // Create the string builders that are responsible for individual parts of the class
            StringBuilder sbInstantiate = new StringBuilder();
            StringBuilder sbProperties = new StringBuilder();
            StringBuilder sbThisControl = new StringBuilder();
            StringBuilder sbDeclare = new StringBuilder();

            // Custom Control;
            // Create the string builders that are responsible for individual parts of the class
            StringBuilder sbInstantiateCtrl = new StringBuilder();
            StringBuilder sbPropertiesCtrl = new StringBuilder();
            StringBuilder sbThisControlCtrl = new StringBuilder();
            StringBuilder sbDeclareCtrl = new StringBuilder();


            // Fill in the top sections of each part
            if (!iOS)
            {
                InstantiateTop(sbInstantiate, className);
                PropertiesTop(sbProperties);
                ThisControlTop(sbThisControl);
                DeclareTop(sbDeclare);

                InstantiateTopCtrl(sbInstantiateCtrl, className);
                PropertiesTopCtrl(sbPropertiesCtrl);
                ThisControlTopCtrl(sbThisControlCtrl);
                DeclareTopCtrl(sbDeclareCtrl);
            }
            else
            {
                InstantiateTopiOS(sbInstantiate, className);
                PropertiesTopiOS(sbProperties, title);
                ThisControlTopiOS(sbThisControl);
                DeclareTopiOS(sbDeclare);
            }

            // Main processing here.
            int startLocationX = 10;
            const int startLocationYConstant = 10;
            int startLocationY = startLocationYConstant;
            int controlCounter = 0;
            int rowCounter = 0;
            int maxLabelLevel = controlList.Where(d => d.AxisOrientation == "X").Select(d=>d.Level).Max();
            int tableCount = 0;
            int rangeFirstTable = GetRangeY(locationRanges, tableCount);
            int extraRows = 0;
            CompareAxisOrdinateControls compare = new CompareAxisOrdinateControls();
            int maxFormY = 0; // Record the largest y coord.


            
            #region Dimension and Hierarchy Dropdowns
            bool comboAdded = false;

            if (pageData != null && pageData.Any())
            {
                // We have some combos to set at the top of the form.
                // Get the distinct AxisIDs - NON Fixed dimensions -ie combo boxes
                IEnumerable<long> AxisIDs = pageData.Where(p => !p.FixedDimension).Select(p => (p.AxisID ?? 0)).Distinct();

                int comboStartLocationX = startLocationX;
                foreach (long axisID in AxisIDs.Distinct())
                {
                    string tableNames = "";
                    string colName = "";

                    if (axisID != 0)
                    {
                        if (pageData.Count > 0)
                        {
                            foreach (FormDataPage formDataPage in pageData.Where(p => !p.FixedDimension && p.AxisID == axisID))
                            {
                                if (tableNames.Length != 0)
                                {
                                    tableNames += "|";
                                }
                                tableNames += formDataPage.DYN_TABLE_NAME;
                                colName = formDataPage.DYN_TAB_COLUMN_NAME; // Consistent across tables.
                            }

                            comboAdded = true;
                            ControlParameter par3 = new ControlParameter(sbInstantiate, sbProperties, sbThisControl, sbDeclare);
                            par3.ControlCount = controlCounter;
                            par3.LocationX = comboStartLocationX;
                            par3.LocationY = startLocationY;
                            par3.CtrlWidth = constants.COMBO_Width;
                            par3.CtrlHeight = constants.COMBO_Height;
                            par3.TableNames = tableNames;
                            par3.ColName = colName;
                            par3.AxisID = axisID;
                            if (!iOS)
                                AddSolvencyControls.AddSolvencyComboBox(par3, true);

                            // ToDo: NAJ the iOS control has not been created
                            //else
                            //AddSolvencyControlsiOS.AddSolvencyComboBox(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX, locationY, colOrdinateID, rowOrdinateID, greyBox, dataPointKey, tableCellSignature, CURRENCY_Width, CURRENCY_Height, colName);

                            comboStartLocationX += constants.COMBO_COLUMN_WIDTH;
                            controlCounter++;
                        }
                    }
                }
                if (comboAdded) startLocationY += (constants.ROW_HEIGHT*2);


                // We now have some hiddedn PAGEn controls to set at the top of the form.
                // Fixed dimensions 
                var pageNCols = (from page in pageData
                                 where page.FixedDimension
                                 group page by page.DYN_TAB_COLUMN_NAME
                                 into grps
                                 select new
                                            {
                                                grps.Key
                                            }
                                );

                foreach (var pageNColGroup in pageNCols)
                {
                    string colName = pageNColGroup.Key;
                    string tableNames = "";
                    // We need to know if this field is used in more than one table
                    foreach (FormDataPage formDataPage in pageData.Where(p => p.DYN_TAB_COLUMN_NAME == colName))
                    {
                        if (tableNames.Length != 0)
                        {
                            tableNames += "|";
                        }
                        tableNames += formDataPage.DYN_TABLE_NAME;
                    }


                    ControlParameter par2 = new ControlParameter(sbInstantiate, sbProperties, sbThisControl, sbDeclare);
                    par2.ControlCount = controlCounter;
                    par2.LocationX = comboStartLocationX;
                    par2.LocationY = startLocationY;
                    par2.CtrlWidth = constants.COMBO_Width;
                    par2.CtrlHeight = constants.COMBO_Height;
                    par2.TableNames = tableNames;
                    par2.ColName = colName;


                    if (!iOS)
                        AddSolvencyControls.AddSolvencyPageTextBox(par2);

                    // ToDo: NAJ the iOS control has not been created
                    //else
                    //AddSolvencyControlsiOS.AddSolvencyComboBox(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX, locationY, colOrdinateID, rowOrdinateID, greyBox, dataPointKey, tableCellSignature, CURRENCY_Width, CURRENCY_Height, colName);

                    comboStartLocationX += constants.COMBO_COLUMN_WIDTH;
                    controlCounter++;

                }              



            }

            #endregion


            //int levelDefaultConstant = 0;
            //// Note. There has been an inconsitancy error on the db 2014 08 14.
            //// Whilst waiting for a fix the templates were required so I have added this work around.
            //// >> Work around 

            //if (controlList.Where(d => d.AxisOrientation == "X").Any(d => d.Level == 0))
            //    levelDefaultConstant = 1;
            //// Added below to the line;
            //// int locationY = CalcLabelYLocation(startLocationY, xDimensions[headerLables].Level + levelDefaultConstant);
            //// Added below to the line;
            //// startLocationY += ((maxLabelLevel - 1) * ROW_HEIGHT) + ROW_HEIGHT + (levelDefaultConstant * ROW_HEIGHT);
            //// Added below to the line;
            //// int height = startLocationYConstant + (rowCounter * ROW_HEIGHT) + startLocationYConstant + (4 * ROW_HEIGHT) + (extraRows * ROW_HEIGHT) + (levelDefaultConstant * ROW_HEIGHT); //20140814

            //// << Work around 

            foreach (string groupID in groupIDs.Split('|'))
            {
                
                rowCounter = 0;
                int columnCount;
                
                #region Get the data needed for this table

                int tableVid = int.Parse(groupID);
                // Iterate for each table in the group
                List<AxisOrdinateControls> xDimensions = controlList.Where(d => d.AxisOrientation == "X" && d.TableID == tableVid).OrderBy(d => d, compare).ToList();
                columnCount = xDimensions.Count;
                List<AxisOrdinateControls> yDimensions = controlList.Where(d => d.AxisOrientation == "Y" && d.TableID == tableVid).OrderBy(d => d.Order).ToList();

                #endregion

                #region Work out maximum width of text labels in first column

                // Work out the maximum label width for the left hand column
                Label myLabel = new Label();
                int firstColumnWidth = constants.LABEL_COLUMN_WIDTH;
                int maxLableSize = 0;
                for (int i = 0; i < yDimensions.Count; i++)
                {
                    string label = yDimensions[i].OrdinateLabel;
                    int literalWidth = TextRenderer.MeasureText(label, myLabel.Font).Width;
                    if (literalWidth > maxLableSize) maxLableSize = literalWidth;
                }
                if (maxLableSize + ((constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width) * 4) < constants.LABEL_COLUMN_WIDTH)
                {
                    firstColumnWidth = maxLableSize + (constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width) * 4;
                }

                #endregion

                #region Work out LocationX for each column control and column widths

                List<int> ColumnStartingPositions;
                List<int> ColumnWidths;
                ColumnCalculator.CalcColumnStartingPositionsAndWidths(out ColumnStartingPositions, out ColumnWidths, xDimensions, yDimensions, constants, startLocationX, firstColumnWidth);


                #endregion



                #region Headers

                // Headers:
                int abstractHeader = 0;
                for (int headerLables = 0; headerLables < columnCount; headerLables++)
                {
                    // int locationX = startLocationX + LABEL_COLUMN_WIDTH + ((headerLables- abstractHeader)*CURRENCY_COLUMN_WIDTH);
                    // int locationX = startLocationX + firstColumnWidth + CODE_COLUMN_WIDTH + ((headerLables - abstractHeader)*CURRENCY_COLUMN_WIDTH); //20140326
                    int locationX = ColumnStartingPositions[headerLables + 1]; // Checked
                    int tempHeadersLocationX = startLocationX + firstColumnWidth + constants.CODE_COLUMN_WIDTH + ((headerLables - abstractHeader) * constants.CURRENCY_COLUMN_WIDTH);
                    if (locationX != tempHeadersLocationX) 
                    {string debug = "stop";}


                    int locationY = CalcLabelYLocation(startLocationY, xDimensions[headerLables].Level, constants.ROW_HEIGHT);
                    //int locationY = CalcLabelYLocation(startLocationY, xDimensions[headerLables].Level + levelDefaultConstant);
                    maxFormY = maxFormY.KeepMax(locationY);
                    int ordinateID = xDimensions[headerLables].OrdinateID;
                    int labelWidth = constants.LABEL_Width;

                    if (labelWidth != ColumnWidths[headerLables]) 
                    {string debug = "stop";} // FAILS
                    
                    int lableHeight = constants.LABEL_Height;
                    int childLabelCount = xDimensions.Count(d => d.ParentOrdinateID == xDimensions[headerLables].OrdinateID && !d.IsAbstractHeader);
                    
                    

                    bool codeNeeded = false; //20140326
                    int codeLabelY = 0; //20140326

                    bool greyControl = false;
                    if (xDimensions[headerLables].IsAbstractHeader)
                    {
                        abstractHeader++;
                        // We need to calculate the width of this label

                        if (maxLabelLevel > 2)
                        {
                            // Children
                            childLabelCount = childLabelCount + (from d in xDimensions
                                                                 join d2 in xDimensions on d.OrdinateID equals d2.ParentOrdinateID
                                                                 where ((d.ParentOrdinateID == xDimensions[headerLables].OrdinateID) && !d2.IsAbstractHeader)
                                                                 select d2.OrdinateID).Count();
                            
                            // Grand children
                            childLabelCount = childLabelCount + (from d in xDimensions
                                                                 join d2 in xDimensions on d.OrdinateID equals d2.ParentOrdinateID
                                                                 join d3 in xDimensions on d2.OrdinateID equals d3.ParentOrdinateID
                                                                 where ((d.ParentOrdinateID == xDimensions[headerLables].OrdinateID) && !d3.IsAbstractHeader)
                                                                 select d3.OrdinateID).Count();
                        }


                        labelWidth = childLabelCount*constants.LABEL_Width;
                        greyControl = true;
                    }
                    else
                    {
                        // Not abstract header
                        codeNeeded = true;
                        if (childLabelCount == 0)
                        {
                            lableHeight = ((maxLabelLevel - xDimensions[headerLables].Level) + 1)*constants.ROW_HEIGHT + constants.LABEL_Height;
                            codeLabelY = locationY + lableHeight;
                        }
                        else
                            codeLabelY = locationY + ((maxLabelLevel - xDimensions[headerLables].Level) + 1)*constants.ROW_HEIGHT + constants.LABEL_Height; //20140326
                    }

                    int tempChildLabelCount = xDimensions.ChildWidth(xDimensions[headerLables].OrdinateID);
                    
                    int tempLabelWidth = tempChildLabelCount*constants.LABEL_Width;
                    if (labelWidth != tempLabelWidth)
                    { string debug = "stop"; }

                    int tempLabelWidth2 = ControlWidthCalculator.CalculateSpanLength(headerLables, tempChildLabelCount, ColumnWidths) - constants.CONTROL_MARGIN;
                    if (labelWidth != tempLabelWidth2)
                    { string debug = "stop"; }



                    if (!iOS)
                        AddSolvencyControls.AddSolvencyLabel(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX, locationY, ordinateID, xDimensions[headerLables].OrdinateLabel, labelWidth, lableHeight, false, false, true, false);
                    else
                        AddSolvencyControlsiOS.AddSolvencyLabel(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX, locationY, ordinateID, xDimensions[headerLables].OrdinateLabel, labelWidth, lableHeight, greyControl);
                    maxFormY = maxFormY.KeepMax(locationY);
                    if (codeNeeded)
                    {
                        // Add the code control //20140326
                        controlCounter++;
                        if (!iOS)
                            AddSolvencyControls.AddSolvencyLabel(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX, codeLabelY, 0, xDimensions[headerLables].OrdinateCode, constants.CODE_COLUMN_WIDTH, constants.LABEL_Height, false, false, true, false);
                        else
                            AddSolvencyControlsiOS.AddSolvencyLabel(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX, codeLabelY, 0, xDimensions[headerLables].OrdinateCode, constants.CODE_COLUMN_WIDTH, constants.LABEL_Height, true);
                        maxFormY = maxFormY.KeepMax(codeLabelY);
                    }
                    controlCounter++;
                }

                #endregion

                #region Adjust numbers

                // Extend the y to allow for the Label Levels
                startLocationY += ((maxLabelLevel - 1) * constants.ROW_HEIGHT) + constants.ROW_HEIGHT;
                // startLocationY += ((maxLabelLevel - 1) * ROW_HEIGHT) + ROW_HEIGHT + (levelDefaultConstant * ROW_HEIGHT);

                // Add extra row for OrdinateCode
                startLocationY += constants.ROW_HEIGHT; //20140326


                // Check the Location Ranges
                int downwardMove = (GetRangeY(locationRanges, tableCount) - rangeFirstTable);

                startLocationY += downwardMove*constants.ROW_HEIGHT;
                extraRows += downwardMove;

                #endregion

                #region Rows

                // Row with SolvencyCurrencyTextBoxes:
                rowCounter++;
                foreach (AxisOrdinateControls yDimension in yDimensions.OrderBy(d => d.Order))
                {
                    #region Row labels

                    // Labels first - text then code
                    //int locationX = startLocationX;
                    int locationX = ColumnStartingPositions[0];
                    int tempLocationx = startLocationX; // Checked
                    if (locationX != tempLocationx)
                    { string debug = "stop"; }


                    int locationY = startLocationY + (rowCounter*constants.ROW_HEIGHT);
                    int rowOrdinateID = yDimension.OrdinateID;
                    int levelAdjustment = (yDimension.Level - 1)*constants.LEVEL_TAB;
                    if (!iOS)
                    {
                        //int tempLocationLabelLocationX = locationX + levelAdjustment;
                        int tempLocationLabelLocationX = ColumnStartingPositions[0] + levelAdjustment; // Checked
                        if (tempLocationLabelLocationX != locationX + levelAdjustment)
                        { string debug = "stop"; }

                        AddSolvencyControls.AddSolvencyLabel(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, tempLocationLabelLocationX, locationY, rowOrdinateID, yDimension.OrdinateLabel, firstColumnWidth - ((constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width) * 2) - levelAdjustment, constants.LABEL_Height, false, false, true, false);
                    }
                    else
                        AddSolvencyControlsiOS.AddSolvencyLabel(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX + levelAdjustment, locationY, rowOrdinateID, yDimension.OrdinateLabel, firstColumnWidth - levelAdjustment, constants.LABEL_Height, false);
                    controlCounter++; //20140326
                    if (!iOS)
                    {
                        //int tempLocationLabelLocationX = locationX + firstColumnWidth;
                        int tempLocationLabelLocationX = ColumnStartingPositions[0] + firstColumnWidth; // Checked
                        if (tempLocationLabelLocationX != locationX + firstColumnWidth)
                        { string debug = "stop"; }

                        AddSolvencyControls.AddSolvencyLabel(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, tempLocationLabelLocationX, locationY, 0, yDimension.OrdinateCode, constants.CODE_COLUMN_WIDTH, constants.LABEL_Height, false, false, true, false); //20140326
                    }
                    else
                        AddSolvencyControlsiOS.AddSolvencyLabel(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX + firstColumnWidth, locationY, 0, yDimension.OrdinateCode, constants.CODE_COLUMN_WIDTH, constants.LABEL_Height, true); //20140326
                    maxFormY = maxFormY.KeepMax(locationY);
                    controlCounter++;

                    #endregion

                    #region Row Currently controls

                    // Now the currency controls
                    if (!yDimension.IsAbstractHeader)
                    {
                        abstractHeader = 0;
                        for (int headerLables = 0; headerLables < columnCount; headerLables++)
                        {
                            if (!xDimensions[headerLables].IsAbstractHeader)
                            {
                                //locationX = startLocationX + firstColumnWidth + CODE_COLUMN_WIDTH + ((headerLables - abstractHeader)*CURRENCY_COLUMN_WIDTH); //20140326
                                locationX = ColumnStartingPositions[headerLables + 1];
                                if (locationX != startLocationX + firstColumnWidth + constants.CODE_COLUMN_WIDTH + ((headerLables - abstractHeader) * constants.CURRENCY_COLUMN_WIDTH))
                                { string debug = "stop"; }

                                int colOrdinateID = xDimensions[headerLables].OrdinateID;
                                bool greyBox = shadedControls.Any(f => f.IsShaded && f.XordinateID == colOrdinateID && f.YordinateID == rowOrdinateID);
                                string dataPointKey = null;
                                string tableCellSignature = null;
                                string colName = string.Format("R{0}C{1}", yDimension.OrdinateCode, xDimensions[headerLables].OrdinateCode);

                                ControlParameter par = new ControlParameter(sbInstantiate, sbProperties, sbThisControl, sbDeclare);
                                par.ControlCount = controlCounter;
                                par.LocationX = locationX;
                                par.LocationY = locationY;
                                par.Dim1 = colOrdinateID;
                                par.Dim2 = rowOrdinateID;
                                par.GreyBox = greyBox;
                                //par.Ctrlwidth = CURRENCY_Width;
                                par.CtrlWidth = ColumnWidths[headerLables+1] - constants.CONTROL_MARGIN;
                                if (constants.CURRENCY_Width != ColumnWidths[headerLables+1])
                                { string debug = "stop"; }


                                par.CtrlHeight = constants.CURRENCY_Height;
                                par.ColName = colName;
                                par.OrdinateID = yDimension.OrdinateID;

                                if (!iOS)
                                    AddSolvencyControls.AddSolvencyControl(par, yDimension.DataType, xDimensions[headerLables].DataType, true);
                                    //AddSolvencyControls.AddSolvencyCurrencyTextBox(par, yDimension.DataType);
                                else
                                    AddSolvencyControlsiOS.AddSolvencyCurrencyTextBox(sbInstantiate, sbProperties, sbThisControl, sbDeclare, controlCounter, locationX, locationY, colOrdinateID, rowOrdinateID, greyBox, dataPointKey, tableCellSignature, constants.CURRENCY_Width, constants.CURRENCY_Height, colName);
                                maxFormY = maxFormY.KeepMax(locationY);
                                controlCounter++;
                            }
                            else
                                abstractHeader++;
                        }
                    }

                    #endregion

                    rowCounter++;
                }

                #endregion

                #region Prepare for next table


                //ColumnStartingPositions;
                //ColumnWidths;

                int temp = ColumnStartingPositions[columnCount + 1];

                // Next table - reset values:
                //startLocationX = startLocationX + firstColumnWidth + constants.CODE_COLUMN_WIDTH + ((columnCount - abstractHeader)*constants.CURRENCY_COLUMN_WIDTH);
                startLocationX = ColumnStartingPositions[columnCount + 1];

                int tempStartLocationX = ColumnStartingPositions[columnCount] + constants.CURRENCY_COLUMN_WIDTH;
                if (startLocationX != tempStartLocationX)
                { string debug = "stop"; }

                //startLocationX = startLocationX + LABEL_COLUMN_WIDTH + CODE_COLUMN_WIDTH + ((columnCount - abstractHeader) * CURRENCY_COLUMN_WIDTH);
                startLocationY = startLocationYConstant;
                if (comboAdded) startLocationY += constants.ROW_HEIGHT*2;

                #endregion

                tableCount++;
            }


            // Fill in the bottom sections of each part
            if (!iOS)
            {
                InstantiateBottom(sbInstantiate);
                PropertiesBottom(sbProperties);
            }
            else
            {
                InstantiateBottomiOS(sbInstantiate);
                PropertiesBottomiOS(sbProperties);
            }
            int width = startLocationX; // Updated on table iteration.
            width +=constants.CODE_COLUMN_WIDTH; // 20140326
            // int height = startLocationYConstant + (rowCounter * ROW_HEIGHT) + startLocationYConstant + ROW_HEIGHT + (extraRows * ROW_HEIGHT);

            // We have the largest Y coordinate for a contorl:
            // maxFormY
            maxFormY += constants.ROW_HEIGHT + startLocationYConstant;

            // int height = startLocationYConstant + (rowCounter * ROW_HEIGHT) + startLocationYConstant + (4 * ROW_HEIGHT) + (extraRows * ROW_HEIGHT); //20140326
            int height = startLocationYConstant + (rowCounter * constants.ROW_HEIGHT) + startLocationYConstant + (4 * constants.ROW_HEIGHT) + (extraRows * constants.ROW_HEIGHT);
            //int height = startLocationYConstant + (rowCounter * ROW_HEIGHT) + startLocationYConstant + (4 * ROW_HEIGHT) + (extraRows * ROW_HEIGHT) + (levelDefaultConstant * ROW_HEIGHT); //20140814
            if (comboAdded) height += constants.ROW_HEIGHT*2;

            if(!iOS) ThisControlBottom(sbThisControl, className, width, height);
            else ThisControlBottomiOS(sbThisControl, className, width, height);

            if (!iOS) DeclareBottom(sbDeclare);
            else DeclareBottomiOS(sbDeclare);

            // Draw all parts together.

            customControl = string.Format("{0}{1}{2}{3}", sbInstantiateCtrl, sbPropertiesCtrl, sbThisControlCtrl, sbDeclareCtrl);

            return string.Format("{0}{1}{2}{3}", sbInstantiate, sbProperties, sbThisControl, sbDeclare);
        }

        public static int GetRangeY(List<string> locationRanges, int i)
        {
            if(locationRanges != null && locationRanges.Count > 0 )
            {
                    if(locationRanges.Count >= i)
                    {
                        string yCoord = locationRanges[i].Split(':')[0];
                        string yNumber = new String(yCoord.Where(x => x >= '0' && x <= '9').ToArray());
                        int result;
                        if (int.TryParse(yNumber, out result))
                            return result;
                    }
            }
            return 0;
        }


        private static int CalcLabelYLocation(int startLocationY, int level, int rowHeight)
        {
            // Top most level is highest number
            // Lowest level is 1
            return startLocationY + (level - 1) * rowHeight;
        }

        private static void DeclareBottom(StringBuilder sbDeclare)
        {
            sbDeclare.AppendLine();
            sbDeclare.AppendLine("   }");
            sbDeclare.AppendLine("}");
            sbDeclare.AppendLine();
        }

        private static void DeclareBottomiOS(StringBuilder sbDeclare)
        {
            sbDeclare.AppendLine();
            sbDeclare.AppendLine("   }");
            sbDeclare.AppendLine("}");
            sbDeclare.AppendLine();
        }

        private static void ThisControlBottom(StringBuilder sbThisControl, string className, int width, int height)
        {
            sbThisControl.AppendLine(string.Format(@"            this.Name = ""{0}""; ", className));
            sbThisControl.AppendLine(string.Format("            this.Size = new System.Drawing.Size({0}, {1}); ", width, height));
            sbThisControl.AppendLine("            this.ResumeLayout(false); ");
            sbThisControl.AppendLine("            this.PerformLayout(); ");
            sbThisControl.AppendLine();
            sbThisControl.AppendLine("      } ");
        }

        private static void ThisControlBottomiOS(StringBuilder sbThisControl, string className, int width, int height)
        {
            // Prior to juggle
            //sbThisControl.AppendLine(string.Format("			AddControlsToView (controls, {0}f, {1}f); ", width, height));

            sbThisControl.AppendLine(string.Format("			AddControlsToView ({0}f, {1}f); ", width, height));
            sbThisControl.AppendLine("		}");
        }


        private static void PropertiesBottom(StringBuilder sbProperties)
        {
            // Nothing to do.
        }

        private static void PropertiesBottomiOS(StringBuilder sbProperties)
        {
            sbProperties.AppendLine();
            
        }

        private static void InstantiateBottom(StringBuilder sbInstantiate)
        {
            sbInstantiate.AppendLine("            this.SuspendLayout(); ");
            sbInstantiate.AppendLine();
        }

        private static void InstantiateBottomiOS(StringBuilder sbInstantiate)
        {
            // Nothing to do
        }

        private static void DeclareTop(StringBuilder sbDeclare)
        {
            // Nothing to do.
        }

        private static void DeclareTopiOS(StringBuilder sbDeclare)
        {
            // Nothing to do.
        }

        private static void ThisControlTop(StringBuilder sbThisControl)
        {
            sbThisControl.AppendLine("            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); ");
            sbThisControl.AppendLine("            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; ");
        }

        private static void ThisControlTopiOS(StringBuilder sbThisControl)
        {
            // sbThisControl.AppendLine("			List<UIView> controls = new List<UIView> (); ");
        }

        private static void PropertiesTop(StringBuilder sbProperties)
        {
            // Nothing to do.
        }

        private static void PropertiesTopiOS(StringBuilder sbProperties, string title = "")
        {
            // Nothing to do.
            sbProperties.AppendLine("		public override void ViewDidLoad() ");
            sbProperties.AppendLine("		{ ");
            sbProperties.AppendLine("			base.ViewDidLoad (); ");
            sbProperties.AppendLine(string.Format(@"			Title = ""{0}""; ", title));
            sbProperties.AppendLine();
        }


        private static void InstantiateTop(StringBuilder sbInstantiate, string className)
        {
            sbInstantiate.AppendLine("using SolvencyII.Domain.ENumerators; ");
            sbInstantiate.AppendLine("using SolvencyII.UI.Shared.Controls; ");
            sbInstantiate.AppendLine();
            sbInstantiate.AppendLine("namespace SolvencyII.UI.UserControls ");
            sbInstantiate.AppendLine("{ ");
            sbInstantiate.AppendLine(string.Format("   partial class {0} ", className));
            sbInstantiate.AppendLine("   { ");
            sbInstantiate.AppendLine("      private void InitializeComponent() ");
            sbInstantiate.AppendLine("      { ");

        }

        private static void InstantiateTopiOS(StringBuilder sbInstantiate, string className)
        {
            sbInstantiate.AppendLine("using System.Drawing; ");
            sbInstantiate.AppendLine("using System.Collections.Generic; ");
            sbInstantiate.AppendLine("using MonoTouch.UIKit; ");
            sbInstantiate.AppendLine("using SolvencyII.iOS.Lib; ");
            sbInstantiate.AppendLine(" ");
            sbInstantiate.AppendLine("namespace SolvencyII.iOS.Templates ");
            sbInstantiate.AppendLine("{ ");
            sbInstantiate.AppendLine(string.Format("	public class {0} : TemplateBase ", className));
            sbInstantiate.AppendLine("	{ ");
            sbInstantiate.AppendLine(" ");
        }

        #region Custom Control Code

        private static void InstantiateTopCtrl(StringBuilder sbInstantiate, string className)
        {
            sbInstantiate.AppendLine("using SolvencyII.Domain.ENumerators; ");
            sbInstantiate.AppendLine("using SolvencyII.UI.Shared.Controls; ");
            sbInstantiate.AppendLine();
            sbInstantiate.AppendLine("namespace SolvencyII.UI.UserControls ");
            sbInstantiate.AppendLine("{ ");
            sbInstantiate.AppendLine(string.Format("   partial class {0} ", className));
            sbInstantiate.AppendLine("   { ");
            sbInstantiate.AppendLine("      private void InitializeComponent() ");
            sbInstantiate.AppendLine("      { ");

        }

        private static void PropertiesTopCtrl(StringBuilder sbProperties)
        {
            // Nothing to do.
        }

        private static void ThisControlTopCtrl(StringBuilder sbThisControl)
        {
            sbThisControl.AppendLine("            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); ");
            sbThisControl.AppendLine("            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; ");
        }

        private static void DeclareTopCtrl(StringBuilder sbDeclare)
        {
            // Nothing to do.
        }

        #endregion


    }
}
