using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Constants;
using SolvencyII.Domain.Entities;
using System.Linq;
using SolvencyII.Domain.Extensions;
using ucGenerator.Classes;


namespace ucGenerator
{
    /// <summary>
    /// This class is used to generate the special cases template - its open in two dimensions.
    /// </summary>
    public static class DesignerClassControlSpecialCase
    {
        public static string GenerateCode(string className, string groupIDs, List<AxisOrdinateControls> controlList, List<FactInformation> shadedControls, List<AxisOrdinateControls> multipleRowControls, List<string> locationRanges, bool iOS, List<FormDataPage> pageData, string title, enumClosedTemplateType templateType, string classNameControl, out int controlWidth, out int controlHeight, bool headerOnly, bool addButton, string tableNamesParameter, bool isTyped, List<OpenColInfo2> columnData, List<OpenColInfo2> rowData, List<OpenColInfo2> cellData)
        {
            
            // Setup the constants
            ConstantsForDesigner constants = new ConstantsForDesigner();

            // Main Designer.cs;
            // Create the string builders that are responsible for individual parts of the class
            StringBuilders stringBuilders = new StringBuilders();

            // Fill in the top sections of each part
            InstantiateTop(stringBuilders.sbInstantiate, className);
            PropertiesTop(stringBuilders.sbProperties);
            ThisControlTop(stringBuilders.sbThisControl);
            DeclareTop(stringBuilders.sbDeclare);

            // Main processing here.
            int startLocationX = 10;
            int startLocationYConstant = 10;
            int controlCounter = 0;
            int columnWidthComplete = constants.CURRENCY_Width + constants.CONTROL_MARGIN + constants.CONTROL_MARGIN_CENTRAL;

            // Work out the first row.
            int colTop = startLocationYConstant + (2 * constants.ROW_HEIGHT);

            // Label - row
            AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, startLocationX + columnWidthComplete, colTop, rowData[0].OrdinateID, rowData[0].Label, constants.LABEL_Width, (2 * constants.LABEL_Height), true, false, "lblRow");
            controlCounter++;
            
            // Combo - row
            ControlParameter par2 = new ControlParameter(stringBuilders);
            par2.CtrlHeight = constants.COMBO_Height;
            par2.CtrlWidth = constants.CURRENCY_Width;
            par2.LocationX = startLocationX + columnWidthComplete;
            par2.LocationY = colTop + (2*constants.ROW_HEIGHT);
            par2.OrdinateID = rowData[0].OrdinateID;
            par2.AxisID = rowData[0].AxisID;
            par2.TableNames = tableNamesParameter;
            par2.ColName = rowData[0].ColName;
            par2.ControlCount = controlCounter;
            par2.StartOrder = rowData[0].StartOrder;
            par2.NextOrder = rowData[0].NextOrder;
            AddSolvencyControls.AddSolvencyDataComboBox(par2, true, "cboRow");
            controlCounter++;

            // Button - row
            par2.LocationY = par2.LocationY + constants.COMBO_Height + constants.CONTROL_MARGIN + constants.CONTROL_MARGIN_CENTRAL;
            par2.CtrlHeight = constants.BUTTON_Height;
            AddSolvencyControls.AddSolvencyButton(par2, "Add Row", "btnAddRow_Click", true, "btnAddRow");
            controlCounter++;

            // Delete Button - row
            par2.LocationX = startLocationX;
            par2.LocationY  = colTop + (2*constants.ROW_HEIGHT);
            AddSolvencyControls.AddSolvencyButton(par2, "Del Row", "btnDelRow_Click", true, "btnDelRow");
            par2.LocationY += constants.ROW_HEIGHT;
            controlCounter++;

            //int height = par2.LocationY + par2.CtrlHeight + constants.CONTROL_MARGIN + constants.CONTROL_MARGIN_CENTRAL;
            int height = colTop + (2 * constants.ROW_HEIGHT) + par2.CtrlHeight + constants.COMBO_Height + constants.CONTROL_MARGIN + constants.CONTROL_MARGIN_CENTRAL;

             // Work out the second row.
            colTop = startLocationYConstant;
            // Label - col
            int colX = startLocationX + (2*columnWidthComplete);
            AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, colX, colTop, columnData[0].OrdinateID, columnData[0].Label, constants.LABEL_Width, (2 * constants.LABEL_Height), true, false, "lblCol");
            controlCounter++;

            // Cobo - col
            par2.CtrlHeight = constants.COMBO_Height;
            par2.CtrlWidth = constants.CURRENCY_Width;
            par2.LocationX = colX;
            par2.LocationY = colTop + (3*constants.ROW_HEIGHT);
            par2.OrdinateID = columnData[0].OrdinateID;
            par2.AxisID = columnData[0].AxisID;
            par2.TableNames = tableNamesParameter;
            par2.ColName = columnData[0].ColName;
            par2.ControlCount = controlCounter;
            par2.StartOrder = columnData[0].StartOrder;
            par2.NextOrder = columnData[0].NextOrder;
            AddSolvencyControls.AddSolvencyDataComboBox(par2, true, "cboCol");
            controlCounter++;

            // Delete Button  - col
            par2.LocationY -= constants.ROW_HEIGHT;
            par2.CtrlWidth = 100;
            par2.CtrlHeight = 20;
            AddSolvencyControls.AddSolvencyButton(par2, "Del Col", "btnDelCol_Click", true, "btnDelCol");
            par2.LocationY += constants.ROW_HEIGHT;
            controlCounter++;



            // Main control
            par2.CtrlHeight = constants.CURRENCY_Height;
            par2.CtrlWidth = constants.CURRENCY_Width;
            par2.LocationX = colX;
            par2.LocationY = colTop + (3*constants.ROW_HEIGHT) + constants.COMBO_Height + constants.CONTROL_MARGIN + constants.CONTROL_MARGIN_CENTRAL;
            par2.OrdinateID = cellData[0].OrdinateID;
            par2.AxisID = cellData[0].AxisID;
            par2.TableNames = tableNamesParameter;
            par2.ColName = cellData[0].ColName;
            par2.ControlCount = controlCounter;
            par2.StartOrder = cellData[0].StartOrder;
            par2.NextOrder = cellData[0].NextOrder;
            AddSolvencyControls.AddSolvencyControl(par2, cellData[0].ColType, cellData[0].ColType, true, "txtCell");
            controlCounter++;

            // Work out the third row - add column only.
            colX += constants.CURRENCY_Width + constants.CONTROL_MARGIN;

            // Button
            par2.LocationX = colX;
            par2.LocationY = startLocationYConstant + (3 * constants.ROW_HEIGHT);
            par2.CtrlHeight = constants.BUTTON_Height;
            AddSolvencyControls.AddSolvencyButton(par2, "Add Col", "btnAddCol_Click", true, "btnAddCol");
            controlCounter++;



            int width = par2.LocationX + par2.CtrlWidth + constants.CONTROL_MARGIN;
            


            #region Old stuff

//List<string> labelHeaderLabelControlNames = new List<string>();
            //List<string> labelRowLabelControlNames = new List<string>();
            //List<string> labelRowDataControlNames = new List<string>();
            //List<int> ColumnStartingPositions = new List<int>();
            //List<int> ColumnWidths = new List<int>( );


            //int rowsForTextComboBoxes = 0;

            //#region Semi Open Add / Delete button TOP

            //if (addButton)
            //{
            //    if (!horizontal)
            //    {
            //        DesignerHelpers.DelButton(ref rowCounter, startLocationX + constants.CONTROL_MARGIN, startLocationY, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height);
            //        startLocationY += constants.ROW_HEIGHT + constants.CONTROL_MARGIN;
            //        startLocationYConstant = startLocationY; // Ensures all controls are measured below the button
            //    }
            //    else
            //    {
            //        DesignerHelpers.AddButton(ref rowCounter, constants.LEVEL_TAB, startLocationY, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height);
            //        // startLocationY += constants.ROW_HEIGHT + constants.CONTROL_MARGIN;
            //        // startLocationYConstant = startLocationY; // Ensures all controls are measured below the button
            //    }
            //}

            //#endregion

            //#region Semi Open Left Margin

            //if (addButton && horizontal)
            //{
            //    startLocationX += constants.USER_COMBO_Width + constants.BUTTON_Width + (2*constants.CONTROL_MARGIN);
            //}

            //#endregion



            //foreach (string groupID in groupIDs.Split('|'))
            //{

            //    rowCounter = 0;
            //    int columnCount;


            //    #region Get the data needed for this table

            //    int tableVid = int.Parse(groupID);
            //    // Iterate for each table in the group
            //    List<AxisOrdinateControls> xDimensions = controlList.Where(d => d.AxisOrientation == "X" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).ToList();
            //    if(isTyped)
            //        xDimensions.AddRange(controlList.Where(d => d.AxisOrientation == "Y" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).OrderBy(d => d.Order).ToList());

            //    xDimensions.SetupTopBranchOrder();

            //    if (!isTyped)
            //    {
            //        CompareAxisOrdinateControls compare = new CompareAxisOrdinateControls();
            //        xDimensions = xDimensions.OrderBy(d => d, compare).ToList();
            //    }
            //    else
            //    {
            //        CompareOpenAxisOrdinateControls compare = new CompareOpenAxisOrdinateControls();
            //        xDimensions = xDimensions.OrderBy(d => d, compare).ToList();
            //    }

            //    // xDimensions = xDimensions.OrderBy(d=>d.).OrderBy(d => d, compare).ToList();


            //    // The order below does not work with parent ordinates.
            //    //if(isTyped)
            //    //{
            //    //    CompareColumnDimsControls compare = new CompareColumnDimsControls(columnData);
            //    //    xDimensions = xDimensions.OrderBy(d => d, compare).ToList();
            //    //}


            //    if (headerOnly)
            //    {
            //        StringBuilder sb = new StringBuilder();
            //        foreach (AxisOrdinateControls xDimension in xDimensions)
            //        {
            //            sb.AppendLine(string.Format("Level:{0}, Order:{1}, ParnOrder:{2}, OrdID:{3}, ParnID:{4}, Code:{5}, BeforeChildren:{6}, {7}", xDimension.Level, xDimension.Order, xDimension.ParentOrder, xDimension.OrdinateID, xDimension.ParentOrdinateID, xDimension.OrdinateCode, xDimension.IsDisplayBeforeChildren, xDimension.OrdinateLabel));
            //        }

            //        TextBox txtResponse = new TextBox();
            //        txtResponse.Dock = System.Windows.Forms.DockStyle.Bottom;
            //        txtResponse.Location = new System.Drawing.Point(0, 223);
            //        txtResponse.Multiline = true;
            //        txtResponse.Name = "txtResponse";
            //        txtResponse.Size = new System.Drawing.Size(711, 142);
            //        txtResponse.TabIndex = 0;
            //        txtResponse.Text = sb.ToString();

            //        DesignerHelpers.HostPanel.Controls.Add(txtResponse);
            //    }
            //    columnCount = xDimensions.Count;

            //    List<AxisOrdinateControls> yDimensions;
            //    if (!isTyped || headerOnly)
            //        yDimensions = controlList.Where(d => d.AxisOrientation == "Y" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).OrderBy(d => d.Order).ToList();
            //    else
            //    {
            //        yDimensions = new List<AxisOrdinateControls>();
            //        yDimensions.Add(new AxisOrdinateControls{OrdinateCode =  "999"});
            //    }



            //    List<AxisOrdinateControls> textComboBoxes = controlList.Where(d => d.TableID == tableVid && d.SpecialCase == "data entry dropdown" && d.AxisOrientation != "Z").OrderBy(d => d.Order).ToList();
            //    //c.SpecialCase == "multiply rows/columns"
            //    #endregion

            //    #region Work out maximum width of text labels in first column

            //    // Work out the maximum label width for the left hand column
            //    Label myLabel = new Label();
            //    int firstColumnWidth = constants.LABEL_COLUMN_WIDTH;
            //    int maxLableSize = 0;
            //    for (int i = 0; i < yDimensions.Count; i++)
            //    {
            //        string label = yDimensions[i].OrdinateLabel;
            //        int literalWidth = TextRenderer.MeasureText(label, myLabel.Font).Width;
            //        if (literalWidth > maxLableSize) maxLableSize = literalWidth;
            //    }
            //    if (maxLableSize + ((constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width)*4) < constants.LABEL_COLUMN_WIDTH)
            //    {
            //        firstColumnWidth = maxLableSize + (constants.CURRENCY_COLUMN_WIDTH - constants.CURRENCY_Width)*4;
            //    }

            //    #endregion

            //    #region Work out LocationX for each column control and column widths

            //    ColumnCalculator.CalcColumnStartingPositionsAndWidths(out ColumnStartingPositions, out ColumnWidths, xDimensions, yDimensions, constants, startLocationX, firstColumnWidth);

            //    #endregion

            //    #region Work out LocationY for each row

            //    List<int> RowStartingPositions;
            //    List<int> RowHeights;
            //    RowCalculator.CalcRowHeights(out RowStartingPositions, out RowHeights, yDimensions, constants, firstColumnWidth);

            //    #endregion


            //    #region Multiple Rows user selection items

            //    if (addButton)
            //    {
            //        // Use the mulitpleRowUserControls to populate the controls.
            //        if (!horizontal)
            //        {
            //            int textComboWidth = ControlWidthCalculator.CalculateSpanLength(0, 2, ColumnWidths) - constants.CONTROL_MARGIN;
            //            DesignerHelpers.AddMultiRowComboBoxes(multipleRowControls, ColumnStartingPositions[horizontal ? 1 : 0], startLocationY, constants, stringBuilders, textComboWidth, ref controlCounter, ref maxFormY, tableNamesParameter);
            //            startLocationY += constants.ROW_HEIGHT*(multipleRowControls.Count*2); // Labels and combos
            //            rowsForTextComboBoxes += (multipleRowControls.Count*2);
            //        }
            //        else
            //        {
            //            // Found below
            //        }
            //    }

            //    #endregion



            //    #region Data Entry Combos
            //    if (textComboBoxes.Any())
            //    {
            //        // The ZDim data entry combos appear before the headers
            //        int textComboWidth = ControlWidthCalculator.CalculateSpanLength(0, 2, ColumnWidths) - constants.CONTROL_MARGIN;
            //        DesignerHelpers.AddTextComboBoxes(textComboBoxes, ColumnStartingPositions[horizontal ? 1 : 0], startLocationX, startLocationY, constants, stringBuilders, textComboWidth, ref controlCounter, ref maxFormY);
            //        startLocationY += constants.ROW_HEIGHT * (textComboBoxes.Count * 2); // Labels and combos
            //        rowsForTextComboBoxes += (textComboBoxes.Count*2);
            //    }
            //    #endregion 

            //    #region Headers

            //    // Labels
            //    // Reverse the order of the labels so the z order works with the layered header
            //    labelHeaderLabelControlNames.AddRange(DesignerHelpers.PopulateHeaders(columnCount, ColumnStartingPositions, startLocationX - (!fixedDimension ? ColumnWidths[0]:0), firstColumnWidth, constants, startLocationY, xDimensions, ref maxFormY, ColumnWidths, maxLabelLevel, stringBuilders, ref controlCounter, !fixedDimension, horizontal, fixedDimension, headerOnly));
            //    headerHeight = maxFormY + constants.LABEL_Height  + constants.CONTROL_MARGIN;

            //    #endregion

            //    #region Adjust numbers

            //    // Extend the y to allow for the Label Levels
            //    //startLocationY += ((maxLabelLevel - 1)*constants.ROW_HEIGHT) + constants.ROW_HEIGHT;
            //    startLocationY = maxFormY;
            //    // startLocationY += ((maxLabelLevel - 1) * ROW_HEIGHT) + ROW_HEIGHT + (levelDefaultConstant * ROW_HEIGHT);

            //    // Add extra row for OrdinateCode
            //    // startLocationY += constants.ROW_HEIGHT; //20140326


            //    // Check the Location Ranges
            //    int downwardMove2 = (DesignerHelpers.GetRangeY(locationRanges, tableCount) - rangeFirstTable);

            //    startLocationY += downwardMove2*constants.ROW_HEIGHT;
            //    extraRows += downwardMove2;

            //    #endregion


            //    int startOfLabelsAndControls = startLocationY;
            //    int rowCounterOfLabelsAndControls = rowCounter;

            //    if (horizontal || fixedDimension)
            //    {
            //        #region Row Labels

            //        // Row with SolvencyCurrencyTextBoxes:
            //        if (fixedDimension)
            //        {
            //            maxFormY = 0;
            //            rowCounter = -1;
            //            startLocationY = 3;
            //        }
            //        labelRowLabelControlNames.AddRange(DesignerHelpers.AddRowLabels(ref rowCounter, yDimensions, ColumnStartingPositions, startLocationX, startLocationY, constants, stringBuilders, firstColumnWidth, fixedDimension, ref controlCounter, ref maxFormY, RowStartingPositions, RowHeights, !fixedDimension));

            //        #endregion
            //    }

            //    // This is because the labels and control need to be have parrallel rows.
            //    startLocationY = startOfLabelsAndControls;
            //    rowCounter = rowCounterOfLabelsAndControls;

            //    #region Row Currency etc controls

            //    if (fixedDimension)
            //    {
            //        maxFormY = 0;
            //        rowCounter = -1;
            //        startLocationY = 3;
            //    }
            //    if (addButton) startLocationY += constants.ROW_HEIGHT;
            //    labelRowDataControlNames.AddRange(DesignerHelpers.AddRowControls(ref rowCounter, xDimensions, yDimensions, allResults, ColumnStartingPositions, ColumnWidths, startLocationX, startLocationY, constants, stringBuilders, firstColumnWidth,fixedDimension, ref controlCounter, ref maxFormY, horizontal, RowStartingPositions, isTyped, columnData, !fixedDimension));

            //    #endregion

            //    #region Prepare for next table

            //    // Next table - reset values:
            //    startLocationX = ColumnStartingPositions[columnCount + 1];
            //    if (!horizontal) startLocationX -= ColumnWidths[0];
            //    startLocationY = startLocationYConstant;

            //    #endregion

            //    tableCount++;

            //}

            //#region Semi Open Add / Delete button

            //if (addButton)
            //{
            //    if (!horizontal)
            //        DesignerHelpers.AddButton(ref rowCounter, startLocationX + (2 * constants.CONTROL_MARGIN), startLocationY, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height);
            //    else
            //    {
            //        //DesignerHelpers.AddButton(ref rowCounter, constants.LEVEL_TAB, maxFormY + constants.LABEL_Height + constants.CONTROL_MARGIN, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height); 
            //        DesignerHelpers.DelButton(ref rowCounter, constants.LEVEL_TAB, maxFormY - constants.CONTROL_MARGIN_CENTRAL, stringBuilders, ref controlCounter, ref maxFormY, constants.BUTTON_Width, constants.BUTTON_Height); 
            //    }
            //}




            //#endregion

            //#region Semi Open combos button

            //if (addButton && horizontal)
            //{

            //    // I have been informed that there will never be multiple tables with 
            //    // Semi-open scenarios thus the use of multipleRowControls will function correctly with only 
            //    // one iteration of the table group loop.

            //    int textComboWidth = constants.USER_COMBO_Width;
            //    DesignerHelpers.AddMultiRowComboBoxes(multipleRowControls, constants.LEVEL_TAB + constants.BUTTON_Width + constants.CONTROL_MARGIN, maxFormY - constants.ROW_HEIGHT - constants.CONTROL_MARGIN_CENTRAL, constants, stringBuilders, textComboWidth, ref controlCounter, ref maxFormY, tableNamesParameter);
            //}

            //#endregion


            //int width = startLocationX; // Updated on table iteration.
            //width += constants.CODE_COLUMN_WIDTH; // 20140326
            //if (addButton && !horizontal)
            //    width += constants.BUTTON_Width; // Button width

            //// maxFormY contains the last starting position of the botton row of controls

            //int height = 0;
            //if (fixedDimension)
            //    height = maxFormY + headerHeight + constants.ROW_HEIGHT + constants.CONTROL_MARGIN_CENTRAL;
            //else
            //    height = maxFormY + constants.ROW_HEIGHT + constants.CONTROL_MARGIN_CENTRAL;



            //if (fixedDimension)
            //{
            //    /**********************************************/
            //    // Compile everything together on the split containers.
            //    /**********************************************/

            //    int fullWidth = ColumnWidths[ColumnWidths.Count - 1] + ColumnStartingPositions[ColumnStartingPositions.Count - 1] + constants.CONTROL_MARGIN;

            //    ControlParameter par = new ControlParameter(stringBuilders);
            //    par.LocationX = 0;
            //    par.LocationY = 0;

            //    par.CtrlHeight = headerHeight;
            //    int splitterDistance = ColumnWidths[0] + constants.CONTROL_MARGIN;
            //    par.CtrlWidth = fullWidth;

            //    //if(ColumnStartingPositions.Count >= 2)
            //    //    splitterDistance = ColumnStartingPositions[2] - 1;

            //    par.ControlName = "splitContainerColTitles";
            //    string splitTopName = AddSolvencyControls.AddSolvencySplitContainerFixed(par, false, new List<string>(), labelHeaderLabelControlNames, false, splitterDistance, false);

            //    par.ControlName = "splitContainerRowTitles";
            //    string splitBottomName = AddSolvencyControls.AddSolvencySplitContainerFixed(par, false, labelRowLabelControlNames, labelRowDataControlNames, true, splitterDistance, false);


            //    par.ControlName = "spltMain";
            //    par.CtrlHeight = height;
            //    splitterDistance = headerHeight; 
            //    par.CtrlWidth = fullWidth;
            //    par.CtrlWidth = width;
            //    string splitMain = AddSolvencyControls.AddSolvencySplitContainerFixed(par, true, new List<string> { splitTopName }, new List<string> { splitBottomName }, true, splitterDistance, true);

            //}

            #endregion





            // Fill in the bottom sections of each part
            InstantiateBottom(stringBuilders.sbInstantiate);
            PropertiesBottom(stringBuilders.sbProperties);

            // = startLocationYConstant + (rowCounter*constants.ROW_HEIGHT) + startLocationYConstant + (4*constants.ROW_HEIGHT) + (extraRows*constants.ROW_HEIGHT);
            // height += rowsForTextComboBoxes*constants.ROW_HEIGHT;
            //if (addButton && horizontal)

            ThisControlBottom(stringBuilders.sbThisControl, className, width, height);
            DeclareBottom(stringBuilders.sbDeclare);

            // Draw all parts together.
            controlWidth = width;
            controlHeight = height;

            return stringBuilders.ToString();
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

        private static void ThisControlTop(StringBuilder sbThisControl)
        {
            sbThisControl.AppendLine("            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); ");
            sbThisControl.AppendLine("            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font; ");
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

        #region iOS Control

        private static void DeclareTopiOS(StringBuilder sbDeclare)
        {
            // Nothing to do.
        }

        private static void ThisControlTopiOS(StringBuilder sbThisControl)
        {
            // sbThisControl.AppendLine("			List<UIView> controls = new List<UIView> (); ");
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

        private static void PropertiesTopiOS(StringBuilder sbProperties, string title = "")
        {
            // Nothing to do.
            sbProperties.AppendLine("		public override void ViewDidLoad() ");
            sbProperties.AppendLine("		{ ");
            sbProperties.AppendLine("			base.ViewDidLoad (); ");
            sbProperties.AppendLine(string.Format(@"			Title = ""{0}""; ", title));
            sbProperties.AppendLine();
        }

        private static void InstantiateBottomiOS(StringBuilder sbInstantiate)
        {
            // Nothing to do
        }

        private static void PropertiesBottomiOS(StringBuilder sbProperties)
        {
            sbProperties.AppendLine();

        }

        private static void ThisControlBottomiOS(StringBuilder sbThisControl, string className, int width, int height)
        {
            // Prior to juggle
            //sbThisControl.AppendLine(string.Format("			AddControlsToView (controls, {0}f, {1}f); ", width, height));

            sbThisControl.AppendLine(string.Format("			AddControlsToView ({0}f, {1}f); ", width, height));
            sbThisControl.AppendLine("		}");
        }

        private static void DeclareBottomiOS(StringBuilder sbDeclare)
        {
            sbDeclare.AppendLine();
            sbDeclare.AppendLine("   }");
            sbDeclare.AppendLine("}");
            sbDeclare.AppendLine();
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
