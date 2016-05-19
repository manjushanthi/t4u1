using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Constants;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using ucGenerator.Extensions;

namespace ucGenerator.Classes
{
    /// <summary>
    /// Core class that manages the insertion of substantial parts of templates;
    /// Headers
    /// nPage combos
    /// Row labels
    /// Row controls
    /// </summary>
    public static class DesignerHelpers
    {
        private static Panel _hostPanel;
        public static Panel HostPanel
        {
            get { return _hostPanel; }
            set
            {
                _hostPanel = value;
                _hostPanel.Controls.Clear();
            }
        }

        public static bool AddNPageCombos(List<FormDataPage> pageData, List<NPageData> nPageData, List<AxisOrdinateControls> controlList, IEnumerable<long> AxisIDs, ControlParameter par3, int startLocationY, ConstantsForDesigner constants, ref int controlCounter, ref int comboStartLocationX, ref List<string> controlNames)
        {
            bool comboAdded = false;
            foreach (long axisID in AxisIDs.Distinct())
            {
                string tableNames = "";
                string colName = "";
                bool isTypedDimension = false;
                int colStartLocationY = startLocationY;

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

                            isTypedDimension = isTypedDimension || formDataPage.IsTypedDimension;
                        }

                        comboAdded = true;

                        AxisOrdinateControls foundCtrl = controlList.FirstOrDefault(c => c.AxisID == axisID);
                        if (foundCtrl != null)
                        {
                            // Post label here.
                            controlNames.Add(AddSolvencyControls.AddSolvencyLabel(par3.stringBuilder, controlCounter, comboStartLocationX, colStartLocationY, foundCtrl.OrdinateID, String.Format("{0} ({1})", foundCtrl.OrdinateLabel, foundCtrl.OrdinateCode), constants.COMBO_Width, constants.LABEL_Height, false, false));
                            controlCounter++;
                        }
                        else
                        {
                            string devMessage = "This should never happen - db error.";
                        }
                        colStartLocationY += constants.ROW_HEIGHT;

                        par3.ControlCount = controlCounter;
                        par3.LocationX = comboStartLocationX;
                        par3.LocationY = colStartLocationY;
                        par3.CtrlWidth = constants.COMBO_Width;
                        par3.CtrlHeight = constants.COMBO_Height;
                        par3.TableNames = tableNames;
                        par3.ColName = colName;
                        par3.AxisID = axisID;

                        NPageData foundPage = nPageData.FirstOrDefault(p => p.AxisID == axisID);
                        if (foundPage != null)
                        {
                            par3.StartOrder = foundPage.StartOrder;
                            par3.NextOrder = foundPage.NextOrder;
                        }
                        else
                        {
                            par3.StartOrder = 0;
                            par3.NextOrder = 0;
                        }

                        if (!isTypedDimension)
                        {
                            AddSolvencyControls.AddSolvencyComboBox(par3, false);
                            comboStartLocationX += constants.COMBO_COLUMN_WIDTH;
                        }
                        else
                        {
                            AddSolvencyControls.AddSolvencyTextComboBox(par3, false);
                            controlNames.Add(par3.ControlName);
                            comboStartLocationX += constants.COMBO_COLUMN_WIDTH;
                            // Button next to it.
                            controlCounter++;
                            par3.ControlCount = controlCounter;
                            par3.LocationX = comboStartLocationX;
                            par3.CtrlWidth = 37;
                            par3.CtrlHeight = 21;
                            AddSolvencyControls.AddSolvencyButton(par3, "Add", "addControlText", false);
                            controlNames.Add(par3.ControlName);
                            comboStartLocationX += 44;
                            // Button next to it.
                            controlCounter++;
                            par3.ControlCount = controlCounter;
                            par3.LocationX = comboStartLocationX;
                            par3.CtrlWidth = 51;
                            par3.CtrlHeight = 21;
                            AddSolvencyControls.AddSolvencyButton(par3, "Delete", "deleteControlText", false);
                            comboStartLocationX += 58;

                        }
                        controlNames.Add(par3.ControlName);
                        controlCounter++;
                    }
                }
            }
            return comboAdded;
        }

        public static int GetRangeY(List<string> locationRanges, int i)
        {
            if (locationRanges != null && locationRanges.Count > 1)
            {
                // locationRanges is zero based, Count is not.
                string yCoord = locationRanges[i].Split(':')[0];
                string yNumber = new String(yCoord.Where(x => x >= '0' && x <= '9').ToArray());
                int result;
                if (Int32.TryParse(yNumber, out result))
                    return result;
            }
            return 0;
        }

        public static int CalcLabelYLocation(int startLocationY, int level, int rowHeight, int maxTopParentHeight)
        {
            // Top most level is lowest number = 1

            return startLocationY + (level > 1 ? maxTopParentHeight : 0) + (level > 2 ? ((level - 2) * rowHeight) : 0);
        }

        public static int AddNPageTextBoxes(List<FormDataPage> pageData, IEnumerable<string> pageNCols, ControlParameter par2, int controlCounter, int comboStartLocationX, int startLocationY, ConstantsForDesigner constants, ref List<string> controlNames)
        {
            foreach (string pageNColGroup in pageNCols)
            {
                string colName = pageNColGroup;
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

                par2.ControlCount = controlCounter;
                par2.LocationX = comboStartLocationX;
                par2.LocationY = startLocationY;
                par2.CtrlWidth = constants.COMBO_Width;
                par2.CtrlHeight = constants.COMBO_Height;
                par2.TableNames = tableNames;
                par2.ColName = colName;
                AddSolvencyControls.AddSolvencyPageTextBox(par2, false);
                controlNames.Add(par2.ControlName);
                comboStartLocationX += constants.COMBO_COLUMN_WIDTH;
                controlCounter++;
            }
            return controlCounter;
        }

        public static List<string> PopulateHeaders(int columnCount, List<int> ColumnStartingPositions, int startLocationX, int firstColumnWidth, ConstantsForDesigner constants, int startLocationY, List<AxisOrdinateControls> xDimensions, ref int maxFormY, List<int> ColumnWidths, int maxLabelLevel, StringBuilders stringBuilder, ref int controlCounter, bool formControl, bool leaveGapForRowLabels, bool headerOnly = false)
        {
            // Note - We work out the maximum height then position the bordered labels, using their know column starting positions
            // calculated width and CalcLabelYLocation. The height of the box is determined by the maximum height. This gives us 
            // the correct layout for abstract columns.
            // Also note the reversal of the loop below - this determines the z order on the control.


            // List for populating Panel
            List<string> controlNames = new List<string>();

            // Work out the labels' width and levels
            Font font = new Font("Microsoft Sans Serif", 8.25f);
            for (int headerLables = 0; headerLables < columnCount; headerLables++)
            {
                xDimensions[headerLables].TextWidth = TextRenderer.MeasureText(xDimensions[headerLables].OrdinateLabel, font).Width;
                int childLabelCount = xDimensions.Count(d => d.ParentOrdinateID == xDimensions[headerLables].OrdinateID && !d.IsAbstractHeader);
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
                xDimensions[headerLables].ColSpan = childLabelCount;

                int calcuWidth = ControlWidthCalculator.CalculateSpanLength(headerLables, childLabelCount == 0 ? 1 : childLabelCount, ColumnWidths);
                // xDimensions[headerLables].LabelWidth = calcuWidth + (childLabelCount == 0 ? 1 : childLabelCount);
                xDimensions[headerLables].LabelWidth = calcuWidth + 1;
            }

            // Calculate the maximum height using what we know.
            int maxTopParentHeight = constants.ROW_HEIGHT;
            // First iteration gets the top parent height correct
            ColumnCalculator.IterativeMaxHeight(xDimensions,xDimensions, constants.LABEL_Width, constants.ROW_HEIGHT, constants.LABEL_Height_Division, ref maxTopParentHeight);

            // The second returns the max heigh in context of the max top parent.
            int MaxHeight = ColumnCalculator.IterativeMaxHeight(xDimensions, xDimensions, constants.LABEL_Width, constants.ROW_HEIGHT, constants.LABEL_Height_Division, ref maxTopParentHeight);

            

            // Headers:
            int abstractHeader = 0;

            for (int headerLables = columnCount - 1; headerLables >= 0; headerLables--)
            {
                int locationX = ColumnStartingPositions[headerLables + 1];
                if (!leaveGapForRowLabels) locationX -= ColumnWidths[0];

                int locationY = CalcLabelYLocation(startLocationY, xDimensions[headerLables].Level, constants.ROW_HEIGHT, maxTopParentHeight);
                maxFormY = maxFormY.KeepMax(locationY);
                int ordinateID = xDimensions[headerLables].OrdinateID;
                int lableHeight = MaxHeight + startLocationY - locationY;

                bool codeNeeded = false;
                int codeLabelY = 0;

                if (xDimensions[headerLables].IsAbstractHeader)
                {
                    abstractHeader++;
                }
                else
                {
                    // Not abstract header
                    codeNeeded = true;
                    if (xDimensions[headerLables].ColSpan == 0)
                    {
                        //lableHeight = MaxHeight + startLocationY - locationY;
                        codeLabelY = locationY + lableHeight;
                    }
                    else
                    {
                        //lableHeight += startLocationY;
                        codeLabelY = locationY + ((maxLabelLevel - xDimensions[headerLables].Level) + 1)*constants.ROW_HEIGHT + constants.LABEL_Height; //20140326
                    }
                }


                if (!string.IsNullOrEmpty(xDimensions[headerLables].OrdinateLabel))
                {
                    if (!headerOnly)
                    {
                        controlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilder.sbInstantiate, stringBuilder.sbProperties, stringBuilder.sbThisControl, stringBuilder.sbDeclare, controlCounter, locationX, locationY, ordinateID, xDimensions[headerLables].OrdinateLabel, xDimensions[headerLables].LabelWidth, lableHeight, true, true, formControl, false));
                    }
                    else
                    {
                        var ctrl = AddSolvencyControls.AddSolvencyLabelControl(stringBuilder.sbInstantiate, stringBuilder.sbProperties, stringBuilder.sbThisControl, stringBuilder.sbDeclare, controlCounter, locationX, locationY, ordinateID, xDimensions[headerLables].OrdinateLabel, xDimensions[headerLables].LabelWidth, lableHeight, true, true, formControl);
                        HostPanel.Controls.Add(ctrl);
                    }
                }

                maxFormY = maxFormY.KeepMax(locationY);
                if (codeNeeded)
                {
                    // Add the code control
                    controlCounter++;
                    if (!headerOnly)
                        controlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilder.sbInstantiate, stringBuilder.sbProperties, stringBuilder.sbThisControl, stringBuilder.sbDeclare, controlCounter, locationX, codeLabelY, 0, xDimensions[headerLables].OrdinateCode, xDimensions[headerLables].LabelWidth, constants.LABEL_Height, false, true, formControl, false));
                    else
                        HostPanel.Controls.Add(AddSolvencyControls.AddSolvencyLabelControl(stringBuilder.sbInstantiate, stringBuilder.sbProperties, stringBuilder.sbThisControl, stringBuilder.sbDeclare, controlCounter, locationX, codeLabelY, 0, xDimensions[headerLables].OrdinateCode, xDimensions[headerLables].LabelWidth, constants.LABEL_Height, false, true, formControl));
                    maxFormY = maxFormY.KeepMax(codeLabelY);
                }
                controlCounter++;
            }
            return controlNames;
        }


        public static List<string> AddRowLabels(ref int rowCounter, IEnumerable<AxisOrdinateControls> yDimensions, List<int> ColumnStartingPositions, int startLocationX, int startLocationY, ConstantsForDesigner constants, StringBuilders stringBuilders, int firstColumnWidth, bool fixedDimension, ref int controlCounter, ref int maxFormY, List<int> rowStartingPositions, List<int> rowHeights, bool formControl = true)
        {
            rowCounter++;
            List<string> controlNames = new List<string>();
            int locationX = 0;
            int locationY = 0;
            int yDimCounter = 0;
            foreach (AxisOrdinateControls yDimension in yDimensions.OrderBy(d => d.Order))
            {
                #region Row labels

                // Labels first - text then code
                //int locationX = startLocationX;

                if(constants.DynamicRowHeight)
                    locationY = startLocationY + rowStartingPositions[yDimCounter];
                else
                    locationY = startLocationY + (rowCounter*constants.ROW_HEIGHT);
                
                int rowOrdinateID = yDimension.OrdinateID;
                int levelAdjustment = (yDimension.Level - 1)*constants.LEVEL_TAB;
                locationX = ColumnStartingPositions[0] + levelAdjustment;

                int rowHeight = constants.DynamicRowHeight ? rowHeights[yDimCounter] : constants.LABEL_Height;
                //controlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, locationX, locationY, rowOrdinateID, yDimension.OrdinateLabel, firstColumnWidth - ((constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width) * 2) - levelAdjustment, constants.LABEL_Height, formControl));
                controlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, locationX, locationY, rowOrdinateID, yDimension.OrdinateLabel, firstColumnWidth - ((constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width) * 2) - levelAdjustment, rowHeight, formControl, false));
                controlCounter++; 
                locationX = ColumnStartingPositions[0] + firstColumnWidth;

                if (yDimension.OrdinateCode != "999")
                    controlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, locationX, locationY, 0, yDimension.OrdinateCode, constants.CODE_COLUMN_WIDTH, constants.LABEL_Height, formControl, false)); 
                maxFormY = maxFormY.KeepMax(locationY);
                controlCounter++;

                #endregion

                rowCounter++;
                yDimCounter++;
            }
            if (fixedDimension)
            {
                locationY += constants.ROW_HEIGHT; // This is specifically for the scrolling
                controlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, locationX, locationY, 0, ".", constants.CODE_COLUMN_WIDTH, constants.LABEL_Height, formControl, false));
                controlCounter++;
            }
            return controlNames;
        }

        public static List<string> AddRowControls(ref int rowCounter, List<AxisOrdinateControls> xDimensions, IEnumerable<AxisOrdinateControls> yDimensions, List<FactInformation> allResults, List<int> ColumnStartingPositions, List<int> ColumnWidths, int startLocationX, int startLocationY, ConstantsForDesigner constants, StringBuilders stringBuilders, int firstColumnWidth, ref int controlCounter, ref int maxFormY, bool rowLabelsPresent, List<int> rowStartingPositions, bool isTyped, List<OpenColInfo2> columnData, bool moveControlsToRightOfFirstColumn, bool formControl = true)
        {
            rowCounter++;
            int columnCount = xDimensions.Count;
            List<string> controlNames = new List<string>();
            int yDimCounter = 0;
            foreach (AxisOrdinateControls yDimension in yDimensions)
            //foreach (AxisOrdinateControls yDimension in yDimensions.OrderBy(d => d.Order))
            {
                int locationY;
                if(constants.DynamicRowHeight)
                    locationY = startLocationY + rowStartingPositions[yDimCounter];
                else
                    locationY = startLocationY + (rowCounter * constants.ROW_HEIGHT);

                int rowOrdinateID = yDimension.OrdinateID;
                
               

                // Now the currency controls
                if (!yDimension.IsAbstractHeader)
                {
                    #region Row Currency etc controls
                    int abstractHeader = 0;
                    for (int headerLables = 0; headerLables < columnCount; headerLables++)
                    {
                        if (!xDimensions[headerLables].IsAbstractHeader)
                        {
                            //locationX = startLocationX + firstColumnWidth + CODE_COLUMN_WIDTH + ((headerLables - abstractHeader)*CURRENCY_COLUMN_WIDTH); //20140326
                            int locationX = ColumnStartingPositions[headerLables + 1];
                            if (moveControlsToRightOfFirstColumn) locationX -= ColumnWidths[0];

                            int colOrdinateID = xDimensions[headerLables].OrdinateID;
                            bool greyBox = allResults.Any(f => f.IsShaded && f.XordinateID == colOrdinateID && f.YordinateID == rowOrdinateID);

                            string colName = BuildColName(xDimensions[headerLables].OrdinateCode, yDimension.OrdinateCode);


                            ControlParameter par = new ControlParameter(stringBuilders);
                            par.ControlCount = controlCounter;
                            par.LocationX = locationX;
                            par.LocationY = locationY;
                            par.Dim1 = colOrdinateID;
                            par.Dim2 = rowOrdinateID;
                            par.GreyBox = greyBox;
                            //par.Ctrlwidth = CURRENCY_Width;
                            par.CtrlWidth = ColumnWidths[headerLables + 1] - constants.CONTROL_MARGIN;


                            par.CtrlHeight = constants.CURRENCY_Height;
                            par.ColName = colName;


                            // Below modified 3rd Feb 2015 to facilitate correct combo population.
                            par.OrdinateID = xDimensions[headerLables].OrdinateID;
                            par.AxisID = xDimensions[headerLables].AxisID;
                            // Check which of the ordinates is the enumeration:

                            string dataType = xDimensions[headerLables].DataType;
                            if (!string.IsNullOrEmpty(dataType)) dataType = dataType.ToUpper();
                            if (dataType != "ENUMERATION/CODE" && yDimension.DataType != null)
                            {
                                //par.HierarchyID = yDimension.
                                par.OrdinateID = yDimension.OrdinateID;
                                par.AxisID = yDimension.AxisID;
                            }
                            string xDimDataType = dataType;
                            if (string.IsNullOrEmpty(xDimDataType))
                            {
                                xDimDataType = GetColumnDataType(columnData, xDimensions[headerLables], ref par);
                            }
                            par.IsTyped = isTyped;
                            par.IsRowKey = xDimensions[headerLables].IsRowKey;

                            controlNames.Add(AddSolvencyControls.AddSolvencyControl(par, yDimension.DataType, xDimDataType, formControl));
                            maxFormY = maxFormY.KeepMax(locationY);
                            controlCounter++;
                        }
                        else
                            abstractHeader++;
                    }
                    #endregion
                }
                yDimCounter++;

                rowCounter++;
            }
            return controlNames;
        }

        private static string GetColumnDataType(List<OpenColInfo2> columnData, AxisOrdinateControls colNumber, ref ControlParameter par)
        {
            var colD = columnData.Where(c=>c.OrdinateID == colNumber.OrdinateID).FirstOrDefault();
            if (colD != null)
            {
                par.IsRowKey = colD.IsRowKey;
                par.HierarchyID = colD.HierarchyID;
                par.StartOrder = colD.StartOrder;
                par.NextOrder = colD.NextOrder;
                return colD.ColType;
            }
            par.HierarchyID = 0;
            par.IsRowKey = false;
            par.StartOrder = 0;
            par.NextOrder = 0;
            return "STRING";

            //switch (dataType.ToUpper())
            //{
            //    case "BOOLEAN":
            //        return AddSolvencyCheckCombo(par, false, formControl);
            //    case "TRUE":
            //        return AddSolvencyCheckCombo(par, true, formControl);
            //    case "DATE":
            //        return AddSolvencyDateTimePicker(par, formControl);
            //    case "ENUMERATION/CODE":
            //        return AddSolvencyComboBox(par, formControl);
            //    case "URI":
            //    case "STRING":
            //        return AddSolvencyTextBox(par, "String", formControl);
            //    case "INTEGER":
            //        return AddSolvencyCurrencyTextBox(par, "Integer", formControl);
            //    case "PERCENTAGE":
            //        return AddSolvencyCurrencyTextBox(par, "Percentage", formControl);
            //    case "DECIMAL":
            //        return AddSolvencyCurrencyTextBox(par, "Decimal", formControl);
            //    case "MONETARY":
            //    default:
            //        return AddSolvencyCurrencyTextBox(par, "Monetry", formControl);

            //}



        }

        private static string BuildColName(string xDimensionOrdinateCode, string yDimensionOrdinateCode)
        {
            if (xDimensionOrdinateCode.IsNumeric())
            {
                if (!(yDimensionOrdinateCode.Contains("999")))
                    return String.Format("R{0}C{1}", yDimensionOrdinateCode, xDimensionOrdinateCode);
                return String.Format("C{0}", xDimensionOrdinateCode);
            }
            if (!(yDimensionOrdinateCode.Contains("999")))
                return String.Format("{0}{1}", yDimensionOrdinateCode, xDimensionOrdinateCode);
            return String.Format("{0}", xDimensionOrdinateCode);
        }

        public static int CalcHeightOfHeaders(int columnCount, List<int> columnStartingPositions, int startLocationX, int firstColumnWidth, ConstantsForDesigner constants, int startLocationY, List<AxisOrdinateControls> xDimensions, ref int maxFormY, List<int> columnWidths, int maxLabelLevel, StringBuilders stringBuilders, ref int controlCounter, bool b, bool horizontal, int maxTopParentHeight)
        {
            // Headers:
            int abstractHeader = 0;
            for (int headerLables = 0; headerLables < columnCount; headerLables++)
            {
                int locationY = CalcLabelYLocation(startLocationY, xDimensions[headerLables].Level, constants.ROW_HEIGHT, maxTopParentHeight);
                maxFormY = maxFormY.KeepMax(locationY);
                int lableHeight = constants.LABEL_Height;
                int childLabelCount = xDimensions.Count(d => d.ParentOrdinateID == xDimensions[headerLables].OrdinateID && !d.IsAbstractHeader);

                bool codeNeeded = false;
                int codeLabelY = 0;

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

                }
                else
                {
                    // Not abstract header
                    codeNeeded = true;
                    if (childLabelCount == 0)
                    {
                        lableHeight = ((maxLabelLevel - xDimensions[headerLables].Level) + 1) * constants.ROW_HEIGHT + constants.LABEL_Height;
                        codeLabelY = locationY + lableHeight;
                    }
                    else
                        codeLabelY = locationY + ((maxLabelLevel - xDimensions[headerLables].Level) + 1) * constants.ROW_HEIGHT + constants.LABEL_Height; //20140326
                }
                maxFormY = maxFormY.KeepMax(locationY);
                if (codeNeeded)
                {
                    // Add the code control //20140326
                    controlCounter++;
                    maxFormY = maxFormY.KeepMax(codeLabelY);
                }
                controlCounter++;
            }
            return maxFormY;
        }

        public static string AddButton(ref int rowCounter, int startLocationX, int startLocationY, StringBuilders stringBuilders, ref int controlCounter, ref int maxFormY, int width, int height)
        {
            ControlParameter par = new ControlParameter(stringBuilders);
            par.CtrlHeight = height;
            par.CtrlWidth = width;
            par.LocationX = startLocationX;
            par.LocationY = startLocationY;
            par.ControlCount = controlCounter;
            controlCounter++;
            return AddSolvencyControls.AddSolvencyButton(par, "Add", "btnAdd_Click", true);
        }
        
        public static string DelButton(ref int rowCounter, int startLocationX, int startLocationY, StringBuilders stringBuilders, ref int controlCounter, ref int maxFormY, int width, int height)
        {
            ControlParameter par = new ControlParameter(stringBuilders);
            par.CtrlHeight = height;
            par.CtrlWidth = width;
            par.LocationX = startLocationX;
            par.LocationY = startLocationY;
            par.ControlCount = controlCounter;
            controlCounter++;
            return AddSolvencyControls.AddSolvencyButton(par, "Delete", "btnDel_Click", true);
        }

        public static List<string> AddTextComboBoxes(List<AxisOrdinateControls> textComboBoxes, int columnStartingPosition, int startLocationX, int startLocationY, ConstantsForDesigner constants, StringBuilders stringBuilders, int columnWidth, ref int controlCounter, ref int maxFormY)
        {
            List<string> controlNames = new List<string>();
            int startComboLocationY = startLocationY;
            foreach (AxisOrdinateControls dim in textComboBoxes.OrderBy(d => d.Order))
            {
                // Label above
                controlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, columnStartingPosition, startComboLocationY, dim.OrdinateID, dim.OrdinateLabel, columnWidth, constants.LABEL_Height, true, false));
                controlCounter++;
                startComboLocationY += constants.ROW_HEIGHT;

                //Control below
                ControlParameter par2 = new ControlParameter(stringBuilders);
                par2.CtrlHeight = constants.COMBO_Height;
                par2.CtrlWidth = columnWidth;
                par2.LocationX = columnStartingPosition;
                par2.LocationY = startComboLocationY;
                par2.OrdinateID = dim.OrdinateID;
                par2.ColName = dim.OrdinateCode;
                par2.ControlCount = controlCounter;
                controlNames.Add(AddSolvencyControls.AddSolvencyTextComboBox(par2));
                controlCounter++;
                maxFormY = maxFormY.KeepMax(startComboLocationY);
                startComboLocationY += constants.ROW_HEIGHT;
            }
            return controlNames;
        }

        public static List<string> AddMultiRowComboBoxes(List<AxisOrdinateControls> textComboBoxes, int startLocationX, int startLocationY, ConstantsForDesigner constants, StringBuilders stringBuilders, int columnWidth, ref int controlCounter, ref int maxFormY, string tableNames)
        {
            List<string> controlNames = new List<string>();
            int startComboLocationY = startLocationY;
            foreach (AxisOrdinateControls dim in textComboBoxes.OrderBy(d => d.Order))
            {
                // Label above
                controlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, startLocationX, startComboLocationY, dim.OrdinateID, dim.OrdinateLabel, columnWidth, constants.LABEL_Height, true, false));
                controlCounter++;
                startComboLocationY += constants.ROW_HEIGHT;

                //Control below
                ControlParameter par2 = new ControlParameter(stringBuilders);
                par2.CtrlHeight = constants.COMBO_Height;
                par2.CtrlWidth = columnWidth;
                par2.LocationX = startLocationX;
                par2.LocationY = startComboLocationY;
                par2.OrdinateID = dim.OrdinateID;
                par2.AxisID = dim.AxisID;
                par2.TableNames = tableNames;
                par2.ColName = String.Format("PAGE{0}", dim.DimXbrlCode);
                par2.ControlCount = controlCounter;
                par2.StartOrder = dim.StartOrder;
                par2.NextOrder = dim.NextOrder;

                //par2.NotBlank = ?

                controlNames.Add(AddSolvencyControls.AddSolvencyDataComboBox(par2, true));
                controlCounter++;
                maxFormY = maxFormY.KeepMax(startComboLocationY);
                startComboLocationY += constants.ROW_HEIGHT;
            }
            return controlNames;
        }

        public static ControlParameter nPageCombos(List<AxisOrdinateControls> controlList, List<FormDataPage> pageData, List<NPageData> nPageData, StringBuilders stringBuilders, int startLocationX, ConstantsForDesigner constants, out List<string> controlNames, ref bool comboAdded, ref int startLocationY, ref int controlCounter)
        {
            /********************************************************/
            // We have some combos to set at the top of the form.
            /********************************************************/
            ControlParameter par3 = new ControlParameter(stringBuilders);
            controlNames = new List<string>();

            List<FormDataPage> check = pageData.Where(d => !d.FixedDimension)
                .GroupBy(d => new { d.DYN_TAB_COLUMN_NAME, d.DOM_CODE, d.Value })
                .Select(d => new FormDataPage
                            {
                                DOM_CODE = d.Key.DOM_CODE,
                                DYN_TAB_COLUMN_NAME = d.Key.DYN_TAB_COLUMN_NAME,
                                Value = d.Key.Value,
                                DYN_TABLE_NAME = d.Select(e=>e.DYN_TABLE_NAME).First(),
                                AxisID = d.Select(e=>e.AxisID).Distinct().Count()
                            }).ToList();
            if (check.Exists(c => c.AxisID > 1))
            {
                string result = "";
                try
                {
                    var offender = check.FirstOrDefault(c => c.AxisID > 1);
                    result = string.Format("; offending records found in MAPPING while building a merge template -\r\nDYN_TABLE_NAME: {3}, \r\nDYN_TAB_COLUMN_NAME: {1}, \r\nDOM_CODE: {0}, \r\n DIM_CODE: {2}", offender.DOM_CODE, offender.DYN_TAB_COLUMN_NAME, offender.Value, offender.DYN_TABLE_NAME);
                }
                catch (Exception)
                {
                }
                throw new ApplicationException(string.Format("This DB has not been pruned. There are duplicates{0}", result));
            }

            // Get the distinct AxisIDs - NON Fixed dimensions -ie combo boxes
            IEnumerable<long> AxisIDs = pageData.Where(p => !p.FixedDimension).Select(p => (p.AxisID ?? 0)).Distinct();
            int comboStartLocationX = startLocationX;
            comboAdded = AddNPageCombos(pageData, nPageData, controlList, AxisIDs, par3, startLocationY, constants, ref controlCounter, ref comboStartLocationX, ref controlNames);
            if (comboAdded) startLocationY += (constants.ROW_HEIGHT*3);

            /********************************************************/
            // We now have some hiddedn PAGEn controls to set at the top of the form.
            /********************************************************/
            // Fixed dimensions 
            List<string> pageNCols = (from page in pageData
                                      where page.FixedDimension
                                      group page by page.DYN_TAB_COLUMN_NAME
                                      into grps
                                      select grps.Key
                                     ).ToList();
            ControlParameter par2 = new ControlParameter(stringBuilders);
            controlCounter = AddNPageTextBoxes(pageData, pageNCols, par2, controlCounter, comboStartLocationX, startLocationY, constants, ref controlNames);
            return par3;
        }
    }
}
