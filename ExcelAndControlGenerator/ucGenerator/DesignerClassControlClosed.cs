using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Domain.Constants;
using SolvencyII.Domain.Entities;
using System.Linq;
using SolvencyII.Domain.Extensions;
using ucGenerator.Classes;


namespace ucGenerator
{
    public static class DesignerClassControlClosed
    {
        //public static string GenerateCode(string className, string groupIDs, List<AxisOrdinateControls> controlList, List<FactInformation> shadedControls, List<AxisOrdinateControls> multipleRowControls, List<string> locationRanges, bool iOS, List<FormDataPage> pageData, string title, enumClosedTemplateType templateType, out int controlWidth, out int controlHeight, bool headerOnly, bool addButton, string tableNamesParameter, bool isTyped, List<OpenColInfo2> columnData)
        public static string GenerateCode(CreateFileParameter parameter, out int controlWidth, out int controlHeight, bool headerOnly, List<CreateFileParameter> superParameter, bool templatesRemoved, string templatesRemovedDisplayText)
        {
            bool fixedDimension = (parameter.templateType & enumClosedTemplateType.FixedDimension) == enumClosedTemplateType.FixedDimension;
            MergeTemplateInfo mergeTemplateInfo = new MergeTemplateInfo();
            mergeTemplateInfo.MergeTemplate = superParameter.Count > 0;

            // Setup the constants
            ConstantsForDesigner constants = new ConstantsForDesigner();

            // Main Designer.cs;
            // Create the string builders that are responsible for individual parts of the class
            StringBuilders stringBuilders = new StringBuilders();

            // Fill in the top sections of each part
            InstantiateTop(stringBuilders.sbInstantiate, parameter.classNameControl);
            PropertiesTop(stringBuilders.sbProperties);
            ThisControlTop(stringBuilders.sbThisControl, parameter.templateType);
            DeclareTop(stringBuilders.sbDeclare);

            // Main processing here.
            int startLocationXConstant = 10;
            int startLocationX = startLocationXConstant;
            int startLocationYConstant = 10;

            int startLocationY = startLocationYConstant;
            int controlCounter = 0;
            int rowCounter = 0;
            int maxLabelLevel = parameter.controlList.Where(d => d.AxisOrientation == "X").Select(d => d.Level).Max();
            
            int tableCount = 0;
            int rangeFirstTable = DesignerHelpers.GetRangeY(parameter.locationRanges, tableCount);
            int extraRows = 0;
            int headerHeight = 100;
            
            int maxFormY = 0; // Record the largest y coord.
            bool horizontal = ((parameter.templateType & enumClosedTemplateType.HorizontalSeparator) == enumClosedTemplateType.HorizontalSeparator);
            
            List<string> labelHeaderLabelControlNames = new List<string>();
            List<string> labelRowLabelControlNames = new List<string>();
            List<string> labelRowDataControlNames = new List<string>();
            List<int> ColumnStartingPositions = new List<int>();
            List<int> ColumnWidths = new List<int>( );
            List<string> alertControlNames = new List<string>();

            int rowsForTextComboBoxes = 0;

            LocationRangeCalculator rangeCalculator = null;
            

            #region Merged Template Setup

            MergeTemplateSetup.MergeTemplateInfoSetup(superParameter, ref mergeTemplateInfo, ref rangeCalculator);

            #endregion

            #region Semi Open Add / Delete button TOP

            if (parameter.addButton && !mergeTemplateInfo.MergeTemplate)
            {
                if (!horizontal)
                {
                    DesignerHelpers.DelButton(ref rowCounter, startLocationX + constants.CONTROL_MARGIN, startLocationY, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height);
                    DesignerHelpers.AddButton(ref rowCounter, startLocationX + constants.CONTROL_MARGIN + constants.BUTTON_Width + 1, startLocationY, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height);
                    startLocationY += constants.ROW_HEIGHT + constants.CONTROL_MARGIN;
                    startLocationYConstant = startLocationY; // Ensures all controls are measured below the button
                }
                else
                {
                    DesignerHelpers.AddButton(ref rowCounter, constants.LEVEL_TAB, startLocationY, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height);
                    // startLocationY += constants.ROW_HEIGHT + constants.CONTROL_MARGIN;
                    // startLocationYConstant = startLocationY; // Ensures all controls are measured below the button
                }
            }

            #endregion

            #region Semi Open Left Margin

            if (parameter.addButton && horizontal && !mergeTemplateInfo.MergeTemplate)
            {
                startLocationX += constants.USER_COMBO_Width + constants.BUTTON_Width + (2*constants.CONTROL_MARGIN);
            }

            #endregion

            #region Label to say templates have been removed:

            if (templatesRemoved)
            {
                bool check = (fixedDimension && !mergeTemplateInfo.MergeTemplate) || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge == LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow);
                alertControlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, 5, 5, 0, string.Format("{0} not rendered (open or semi open table)", templatesRemovedDisplayText), 220, constants.LABEL_Height, !check, true));
                controlCounter++;
            }

            #endregion

            #region Create Main Control; headers, row labels, controls

            if (!mergeTemplateInfo.MergeTemplate)
            {
                foreach (string groupID in parameter.groupIDs.Split('|'))
                {
                    rowCounter = CreateControl(parameter, headerOnly, rowCounter, groupID, constants, horizontal, stringBuilders, labelHeaderLabelControlNames, fixedDimension, maxLabelLevel, rangeFirstTable, labelRowLabelControlNames, labelRowDataControlNames, startLocationYConstant, ref ColumnStartingPositions, ref ColumnWidths, ref startLocationX, ref startLocationY, ref controlCounter, ref maxFormY, ref rowsForTextComboBoxes, ref headerHeight, ref tableCount, ref extraRows, mergeTemplateInfo);
                }
            }
            else
            {
                List<int> rowHeights = new List<int>();
                int maxWidth = 0;

                foreach (CreateFileParameter fileParameter in superParameter)
                {
                    int groupCounter = 0;
                    foreach (string groupID in fileParameter.groupIDs.Split('|'))
                    {

                        // Might need to implement something different for when 
                        // mergeTemplateInfo.RangesIdentical.

                        int row = rangeCalculator.RowNumber(fileParameter.locationRanges[groupCounter]);

                        Point startingCoords = rangeCalculator.CalcStartingLocation(fileParameter.locationRanges[groupCounter]);
                        Point startingPositions = CalcStartingPosition(startingCoords, constants);
                        mergeTemplateInfo.TemplateInfirstCol = rangeCalculator.TemplateInFirstColumn(fileParameter.locationRanges[groupCounter]);

                        startLocationX = startLocationXConstant + startingPositions.X;
                        int myLocalStartLocationY = CalcStartingPositionFinal(startLocationYConstant, startingPositions.Y, rowHeights, row, constants.ROW_HEIGHT);
                        startLocationY = myLocalStartLocationY;


                        rowCounter = CreateControl(fileParameter, headerOnly, rowCounter, groupID, constants, horizontal, stringBuilders, labelHeaderLabelControlNames, fixedDimension, maxLabelLevel, rangeFirstTable, labelRowLabelControlNames, labelRowDataControlNames, startLocationYConstant, ref ColumnStartingPositions, ref ColumnWidths, ref startLocationX, ref startLocationY, ref controlCounter, ref maxFormY, ref rowsForTextComboBoxes, ref headerHeight, ref tableCount, ref extraRows, mergeTemplateInfo);

                        int heightOfRow = maxFormY - myLocalStartLocationY + constants.ROW_HEIGHT;
                        if (rowHeights.Count == row)
                        {
                            rowHeights.Add(heightOfRow);
                        }

                        groupCounter++;

                        int thisWidth = ColumnWidths.Sum(c => c);
                        if (thisWidth > maxWidth)
                            maxWidth = thisWidth;

                        // startLocationX = maxFormY + constants.ROW_HEIGHT;
                    }
                }
                startLocationX = maxWidth;

            }

            #endregion

            #region Semi Open Add / Delete button

            if (parameter.addButton)
            {
                if (horizontal)
                {
                    //DesignerHelpers.AddButton(ref rowCounter, constants.LEVEL_TAB, maxFormY + constants.LABEL_Height + constants.CONTROL_MARGIN, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height); 
                    DesignerHelpers.DelButton(ref rowCounter, constants.LEVEL_TAB, maxFormY - constants.CONTROL_MARGIN_CENTRAL, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height);
                }
                //else
                    //DesignerHelpers.AddButton(ref rowCounter, startLocationX + (2*constants.CONTROL_MARGIN), startLocationY, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height);
            }

            


            #endregion

            #region Semi Open combos button

            if (parameter.addButton && horizontal)
            {

                // I have been informed that there will never be multiple tables with 
                // Semi-open scenarios thus the use of multipleRowControls will function correctly with only 
                // one iteration of the table group loop.

                int textComboWidth = constants.USER_COMBO_Width;
                DesignerHelpers.AddMultiRowComboBoxes(parameter.mulitpleRowUserControls, constants.LEVEL_TAB + constants.BUTTON_Width + constants.CONTROL_MARGIN, maxFormY - constants.ROW_HEIGHT - constants.CONTROL_MARGIN_CENTRAL, constants, stringBuilders, textComboWidth, ref controlCounter, ref maxFormY, parameter.tableNamesParameter);
            }

            #endregion


            int width = startLocationX; // Updated on table iteration.
            width += constants.CODE_COLUMN_WIDTH; // 20140326

            // maxFormY contains the last starting position of the botton row of controls
            int height = 0;
            if (fixedDimension)
                height = maxFormY + headerHeight + constants.ROW_HEIGHT + constants.CONTROL_MARGIN_CENTRAL;
            else
                height = maxFormY + constants.ROW_HEIGHT + constants.CONTROL_MARGIN_CENTRAL;



            if ((fixedDimension && !mergeTemplateInfo.MergeTemplate) || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge == LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow))
            {
                /**********************************************/
                // Compile everything together on the split containers.
                /**********************************************/

                int fullWidth = ColumnWidths[ColumnWidths.Count - 1] + ColumnStartingPositions[ColumnStartingPositions.Count - 1] + constants.CONTROL_MARGIN;

                ControlParameter par = new ControlParameter(stringBuilders);
                par.LocationX = 0;
                par.LocationY = 0;
                
                par.CtrlHeight = headerHeight;
                int splitterDistance = ColumnWidths[0] + constants.CONTROL_MARGIN;
                par.CtrlWidth = fullWidth;

                par.ControlName = "splitContainerColTitles";
                string splitTopName = AddSolvencyControls.AddSolvencySplitContainerFixed(par, false, alertControlNames, labelHeaderLabelControlNames, false, splitterDistance, false);

                par.ControlName = "splitContainerRowTitles";
                string splitBottomName = AddSolvencyControls.AddSolvencySplitContainerFixed(par, false, labelRowLabelControlNames, labelRowDataControlNames, true, splitterDistance, false);

                par.ControlName = "spltMain";
                par.CtrlHeight = height;
                splitterDistance = headerHeight; 
                par.CtrlWidth = fullWidth;
                // par.CtrlWidth = width;
                string splitMain = AddSolvencyControls.AddSolvencySplitContainerFixed(par, true, new List<string> { splitTopName }, new List<string> { splitBottomName }, true, splitterDistance, true);


                // Added by NAJ 25/03/2015
                width = fullWidth;

            }




            // Fill in the bottom sections of each part
            InstantiateBottom(stringBuilders.sbInstantiate);
            PropertiesBottom(stringBuilders.sbProperties);

            // = startLocationYConstant + (rowCounter*constants.ROW_HEIGHT) + startLocationYConstant + (4*constants.ROW_HEIGHT) + (extraRows*constants.ROW_HEIGHT);
            // height += rowsForTextComboBoxes*constants.ROW_HEIGHT;
            //if (addButton && horizontal)

            ThisControlBottom(stringBuilders.sbThisControl, parameter.classNameControl, width, height);
            DeclareBottom(stringBuilders.sbDeclare);

            // Draw all parts together.
            controlWidth = width;
            controlHeight = height;

            var tmp1 = stringBuilders.sbDeclare.ToString();
            var tmp2 = stringBuilders.sbInstantiate.ToString();
            var tmp3 = stringBuilders.sbProperties.ToString();
            var tmp4 = stringBuilders.sbThisControl.ToString();


            return stringBuilders.ToString();
        }

        private static int CalcStartingPositionFinal(int startLocationYConstant, int startingPositionsY, List<int> rowHeights, int rowNumber, int margin)
        {
            int result = startLocationYConstant + startingPositionsY;

            // Check if previous rowHeight exist
            if (rowHeights.Count == rowNumber && rowNumber > 0)
            {
                // We have previous rows so summate their height;
                result = 0;
                for (int i = 0; i < rowNumber; i++)
                {
                    result += rowHeights[i] + margin;
                }
            }
            return result;
        }

        private static Point CalcStartingPosition(Point startingCoords, ConstantsForDesigner constants)
        {
            // This contains a number of assumtions regarding the width of the controls.
            // The starting coords are cell based not the actual column widths.
            // The actual widths can be calcuated using  ColumnCalculator.CalcColumnStartingPositionsAndWidths 
            // if the generated merged tables require more accuracy with their layout this will be needed.

            int x = startingCoords.X * constants.CURRENCY_COLUMN_WIDTH;
            int y = startingCoords.Y * constants.ROW_HEIGHT;
            return new Point(x,y);
        }

        private static int CreateControl(CreateFileParameter parameter, bool headerOnly, int rowCounter, string groupID, ConstantsForDesigner constants, bool horizontal, StringBuilders stringBuilders, List<string> labelHeaderLabelControlNames, bool fixedDimension, int maxLabelLevel, int rangeFirstTable, List<string> labelRowLabelControlNames, List<string> labelRowDataControlNames, int startLocationYConstant, ref List<int> ColumnStartingPositions, ref List<int> ColumnWidths, ref int startLocationX, ref int startLocationY, ref int controlCounter, ref int maxFormY, ref int rowsForTextComboBoxes, ref int headerHeight, ref int tableCount, ref int extraRows, MergeTemplateInfo mergeTemplateInfo)
        {
            rowCounter = 0;
            int columnCount;
            bool useItemTemplatePanel = (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow);

            #region Get the data needed for this table

            int tableVid = int.Parse(groupID);
            // Iterate for each table in the group
            List<AxisOrdinateControls> xDimensions = parameter.controlList.Where(d => d.AxisOrientation == "X" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).ToList();
            if (parameter.isTyped)
                xDimensions.AddRange(parameter.controlList.Where(d => d.AxisOrientation == "Y" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).OrderBy(d => d.Order).ToList());

            xDimensions.SetupTopBranchOrder();

            if (!parameter.isTyped)
            {
                CompareAxisOrdinateControls compare = new CompareAxisOrdinateControls();
                xDimensions = xDimensions.OrderBy(d => d, compare).ToList();
            }
            else
            {
                CompareOpenAxisOrdinateControls compare = new CompareOpenAxisOrdinateControls();
                xDimensions = xDimensions.OrderBy(d => d, compare).ToList();
            }

            if (headerOnly)
            {
                StringBuilder sb = new StringBuilder();
                foreach (AxisOrdinateControls xDimension in xDimensions)
                {
                    sb.AppendLine(string.Format("Level:{0}, Order:{1}, ParnOrder:{2}, OrdID:{3}, ParnID:{4}, Code:{5}, BeforeChildren:{6}, {7}", xDimension.Level, xDimension.Order, xDimension.ParentOrder, xDimension.OrdinateID, xDimension.ParentOrdinateID, xDimension.OrdinateCode, xDimension.IsDisplayBeforeChildren, xDimension.OrdinateLabel));
                }

                TextBox txtResponse = new TextBox();
                txtResponse.Dock = System.Windows.Forms.DockStyle.Bottom;
                txtResponse.Location = new System.Drawing.Point(0, 223);
                txtResponse.Multiline = true;
                txtResponse.Name = "txtResponse";
                txtResponse.Size = new System.Drawing.Size(711, 142);
                txtResponse.TabIndex = 0;
                txtResponse.Text = sb.ToString();

                DesignerHelpers.HostPanel.Controls.Add(txtResponse);
            }
            columnCount = xDimensions.Count;

            List<AxisOrdinateControls> yDimensions;
            if (!parameter.isTyped || headerOnly)
                yDimensions = parameter.controlList.Where(d => d.AxisOrientation == "Y" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).OrderBy(d => d.Order).ToList();
            else
            {
                yDimensions = new List<AxisOrdinateControls>();
                yDimensions.Add(new AxisOrdinateControls {OrdinateCode = "999"});
            }


            List<AxisOrdinateControls> textComboBoxes = parameter.controlList.Where(d => d.TableID == tableVid && d.SpecialCase == "data entry dropdown" && d.AxisOrientation != "Z").OrderBy(d => d.Order).ToList();
            //c.SpecialCase == "multiply rows/columns"

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
            if (maxLableSize + ((constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width)*4) < constants.LABEL_COLUMN_WIDTH)
            {
                firstColumnWidth = maxLableSize + (constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width)*4;
            }

            #endregion

            #region Work out LocationX for each column control and column widths

            ColumnCalculator.CalcColumnStartingPositionsAndWidths(out ColumnStartingPositions, out ColumnWidths, xDimensions, yDimensions, constants, startLocationX, firstColumnWidth);

            #endregion

            #region Work out LocationY for each row

            List<int> RowStartingPositions;
            List<int> RowHeights;
            RowCalculator.CalcRowHeights(out RowStartingPositions, out RowHeights, yDimensions, constants, firstColumnWidth);

            #endregion

            #region Multiple Rows user selection items

            if (parameter.addButton && !mergeTemplateInfo.MergeTemplate)
            {
                // Use the mulitpleRowUserControls to populate the controls.
                if (!horizontal)
                {
                    int textComboWidth = ControlWidthCalculator.CalculateSpanLength(0, 2, ColumnWidths) - constants.CONTROL_MARGIN;
                    DesignerHelpers.AddMultiRowComboBoxes(parameter.mulitpleRowUserControls, ColumnStartingPositions[horizontal ? 1 : 0], startLocationY, constants, stringBuilders, textComboWidth, ref controlCounter, ref maxFormY, parameter.tableNamesParameter);
                    startLocationY += constants.ROW_HEIGHT*(parameter.mulitpleRowUserControls.Count*2); // Labels and combos
                    rowsForTextComboBoxes += (parameter.mulitpleRowUserControls.Count*2);
                }
                else
                {
                    // Found below
                }
            }

            #endregion

            #region Data Entry Combos

            if (textComboBoxes.Any())
            {
                // The ZDim data entry combos appear before the headers
                int textComboWidth = ControlWidthCalculator.CalculateSpanLength(0, 2, ColumnWidths) - constants.CONTROL_MARGIN;
                DesignerHelpers.AddTextComboBoxes(textComboBoxes, ColumnStartingPositions[horizontal ? 1 : 0], startLocationX, startLocationY, constants, stringBuilders, textComboWidth, ref controlCounter, ref maxFormY);
                startLocationY += constants.ROW_HEIGHT*(textComboBoxes.Count*2); // Labels and combos
                rowsForTextComboBoxes += (textComboBoxes.Count*2);
            }

            #endregion

            #region Headers

            
            // Labels
            // Reverse the order of the labels so the z order works with the layered header
            bool shouldControlsBeWrittenDirectlyOnToForm = ((!fixedDimension && !mergeTemplateInfo.MergeTemplate )|| (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow));
            // Non Merge gap
            bool leaveGapForRowLabels = (!(!horizontal || fixedDimension)) && !mergeTemplateInfo.MergeTemplate;
            // Merge gap if required
            leaveGapForRowLabels = leaveGapForRowLabels || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow);

            int startHeaderLocationX = startLocationX - (!fixedDimension ? ColumnWidths[0] : 0);
            labelHeaderLabelControlNames.AddRange(DesignerHelpers.PopulateHeaders(columnCount, ColumnStartingPositions, startHeaderLocationX, firstColumnWidth, constants, startLocationY, xDimensions, ref maxFormY, ColumnWidths, maxLabelLevel, stringBuilders, ref controlCounter, shouldControlsBeWrittenDirectlyOnToForm, leaveGapForRowLabels, headerOnly));
            if (!mergeTemplateInfo.MergeTemplate || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TemplateInfirstCol && mergeTemplateInfo.RangesIdentical))
            {
                headerHeight = maxFormY + constants.LABEL_Height + constants.CONTROL_MARGIN;
            }

            #endregion

            #region Adjust numbers

            // Extend the y to allow for the Label Levels
            //startLocationY += ((maxLabelLevel - 1)*constants.ROW_HEIGHT) + constants.ROW_HEIGHT;
            startLocationY = maxFormY;
            // startLocationY += ((maxLabelLevel - 1) * ROW_HEIGHT) + ROW_HEIGHT + (levelDefaultConstant * ROW_HEIGHT);

            // Add extra row for OrdinateCode
            // startLocationY += constants.ROW_HEIGHT; //20140326


            // Check the Location Ranges
            int downwardMove2 = (DesignerHelpers.GetRangeY(parameter.locationRanges, tableCount) - rangeFirstTable);

            if (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow)
                downwardMove2 += 1;

            startLocationY += downwardMove2*constants.ROW_HEIGHT;
            extraRows += downwardMove2;

            #endregion

            int startOfLabelsAndControls = startLocationY;
            int rowCounterOfLabelsAndControls = rowCounter;

            if (!mergeTemplateInfo.MergeTemplate || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TemplateInfirstCol))
            {
                if (horizontal || fixedDimension)
                {
                    #region Row Labels

                    // Row with SolvencyCurrencyTextBoxes:
                    if ((fixedDimension && !mergeTemplateInfo.MergeTemplate) || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge == LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow))
                    {
                        maxFormY = 0;
                        rowCounter = -1;
                        startLocationY = 3;
                    }
                    bool formControl = (!fixedDimension && !mergeTemplateInfo.MergeTemplate) || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow);
                    labelRowLabelControlNames.AddRange(DesignerHelpers.AddRowLabels(ref rowCounter, yDimensions, ColumnStartingPositions, startLocationX, startLocationY, constants, stringBuilders, firstColumnWidth, fixedDimension, ref controlCounter, ref maxFormY, RowStartingPositions, RowHeights, formControl));

                    #endregion
                }
            }
            // This is because the labels and control need to be have parrallel rows.
            startLocationY = startOfLabelsAndControls;
            rowCounter = rowCounterOfLabelsAndControls;

            #region Row Currency etc controls
            //if (fixedDimension && !mergeTemplateInfo.MergeTemplate)
            if ((fixedDimension  && !mergeTemplateInfo.MergeTemplate) || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge == LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow))
            {
                maxFormY = 0;
                rowCounter = -1;
                startLocationY = 3;
            }
            if (parameter.addButton) startLocationY += constants.ROW_HEIGHT;
            bool formControlRows = (!fixedDimension && !mergeTemplateInfo.MergeTemplate) || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow);
            int startPositionXRows = startLocationX;
            if (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow)
            {
                startPositionXRows += firstColumnWidth;
            }

            // ToDo: NAJ continue here. 11/05/2015
            bool moveControlsToRightOfFirstColumn = ((!horizontal || fixedDimension) && !mergeTemplateInfo.MergeTemplate) || (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge == LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow);
            labelRowDataControlNames.AddRange(DesignerHelpers.AddRowControls(ref rowCounter, xDimensions, yDimensions, parameter.shadedControls, ColumnStartingPositions, ColumnWidths, startPositionXRows, startLocationY, constants, stringBuilders, firstColumnWidth, ref controlCounter, ref maxFormY, horizontal, RowStartingPositions, parameter.isTyped, parameter.columnData, moveControlsToRightOfFirstColumn, formControlRows));

            #endregion

            #region Prepare for next table

            // Next table - reset values:
            startLocationX = ColumnStartingPositions[columnCount + 1];
            if (!horizontal) startLocationX -= ColumnWidths[0];
            startLocationY = startLocationYConstant;

            #endregion

            tableCount++;
            return rowCounter;
        }

        #region Helper Functions

        public static int GetRangeY(List<string> locationRanges, int i)
        {
            if (locationRanges != null && locationRanges.Count > 0)
            {
                if (locationRanges.Count >= i)
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
            return startLocationY + (level - 1)*rowHeight;
        }

        #endregion

        #region WinForm control

        private static void DeclareBottom(StringBuilder sbDeclare)
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
            sbThisControl.AppendLine(string.Format("            this.Load += new System.EventHandler(this.BoundControl_Load);"));
            sbThisControl.AppendLine("            this.ResumeLayout(false); ");
            sbThisControl.AppendLine("            this.PerformLayout(); ");
            sbThisControl.AppendLine();
            sbThisControl.AppendLine("      } ");
        }

        private static void PropertiesBottom(StringBuilder sbProperties)
        {
            // Nothing to do.
        }

        private static void InstantiateBottom(StringBuilder sbInstantiate)
        {
            sbInstantiate.AppendLine("            this.SuspendLayout(); ");
            sbInstantiate.AppendLine();
        }

        private static void DeclareTop(StringBuilder sbDeclare)
        {
            // Nothing to do.
        }

        private static void ThisControlTop(StringBuilder sbThisControl, enumClosedTemplateType templateType)
        {
            sbThisControl.AppendLine("            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); ");
            sbThisControl.AppendLine("            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; ");
            if (templateType == enumClosedTemplateType.HorizontalSeparator)
                sbThisControl.AppendLine("            this.AutoScroll = true;");

        }

        private static void PropertiesTop(StringBuilder sbProperties)
        {
            // Nothing to do.
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

        #endregion

        #region WinForm Custom Control Code

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
