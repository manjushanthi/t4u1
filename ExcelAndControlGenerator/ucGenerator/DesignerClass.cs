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
    /// Class responsible for creating the main designer part of the data repeater template
    /// </summary>
    public static class DesignerClass
    {
        public static string GenerateCode(string className, string groupIDs, List<AxisOrdinateControls> controlList, List<FactInformation> shadedControls, List<AxisOrdinateControls> mulitpleRowUserControls, List<string> locationRanges, List<FormDataPage> pageData, List<NPageData> nPageData, string title, enumClosedTemplateType templateType, string classNameControl, int controlWidth, int controlHeight, bool addButtonParameter, bool twoDimOpen, List<CreateFileParameter> superParameter)
        {

            #region Merged Template Setup

            MergeTemplateInfo mergeTemplateInfo = new MergeTemplateInfo();
            LocationRangeCalculator rangeCalculator = null;
            MergeTemplateSetup.MergeTemplateInfoSetup(superParameter, ref mergeTemplateInfo, ref rangeCalculator);

            #endregion

            bool fixedDimension = ((templateType & enumClosedTemplateType.FixedDimension) == enumClosedTemplateType.FixedDimension);
            // Setup the constants
            ConstantsForDesigner constants = new ConstantsForDesigner();
            

            #region Instantiation

            // Main Designer.cs;
            // Create the string builders that are responsible for individual parts of the class
            StringBuilders stringBuilders = new StringBuilders();

            // Fill in the top sections of each part
            InstantiateTop(stringBuilders.sbInstantiate, className);
            PropertiesTop(stringBuilders.sbProperties);
            ThisControlTop(stringBuilders.sbThisControl);
            DeclareTop(stringBuilders.sbDeclare);

            // Main processing here.
            const int startLocationXConstant = 10;
            int startLocationX = startLocationXConstant;
            int startLocationYConstant = 10;
            int startLocationY = startLocationYConstant;
            Debug.WriteLine(string.Format("After initialisation: {0}", startLocationY));
            int controlCounter = 0;
            int rowCounter = 0;
            int maxLabelLevel = 0;
            if (!mergeTemplateInfo.MergeTemplate)
                maxLabelLevel = controlList.Where(d => d.AxisOrientation == "X").Select(d => d.Level).Max();
            else
            {
                maxLabelLevel = superParameter[0].controlList.Where(d => d.AxisOrientation == "X").Select(d => d.Level).Max();
            }
            int tableCount = 0;
            int rangeFirstTable = DesignerHelpers.GetRangeY(locationRanges, tableCount);
            int extraRows = 0;
            CompareAxisOrdinateControls compare = new CompareAxisOrdinateControls();
            int maxFormY = 0; // Record the largest y coord.
            bool horizontal = ((templateType & enumClosedTemplateType.HorizontalSeparator) == enumClosedTemplateType.HorizontalSeparator);
            int panel1Width = 0;

            #endregion

            #region Dimension and Hierarchy Dropdowns

            bool comboAdded = false;
            bool pnlTopAdded = false;
            int pnlTopHeight = 0;

            if (pageData != null && pageData.Any())
            {
                List<string> controlNames;
                var par3 = DesignerHelpers.nPageCombos(controlList, pageData, nPageData, stringBuilders, startLocationX, constants, out controlNames, ref comboAdded, ref startLocationY, ref controlCounter);

                // Setup the pnlTop
                
                par3.ControlName = "pnlTop";
                par3.CtrlWidth = 777; // Arbitary - the dock top will take care of this.
                pnlTopHeight = controlNames.Any() ? (constants.ROW_HEIGHT*3) : 0;
                par3.CtrlHeight = pnlTopHeight;
                par3.LocationX = 0;
                par3.LocationY = 0;
                AddSolvencyControls.AddSolvencyPanel(par3, controlNames, "Top", false, false); // It gets added to the control Collection later so that it is at the back.
                par3.ControlName = "";
                pnlTopAdded = true;

                Debug.WriteLine(string.Format("After AddSolvencyPanel: {0}", startLocationY));

            }

            #endregion

            // Margin for the add button when needed
            if (addButtonParameter)
            {
                // The button is added on the control only accommodated here by pushing things down.
                startLocationY += constants.ROW_HEIGHT + constants.CONTROL_MARGIN; // Add button.
                startLocationYConstant = startLocationY;
                Debug.WriteLine(string.Format("After addButtonParameter: {0}", startLocationY));
            }


            #region Loop round the tables

            // Loop around each of the tables
            List<string> labelHeaderLabelControlNames = new List<string>();
            List<string> labelRowLabelControlNames = new List<string>();
            int rowsForTextComboBoxes = 0;
            foreach (string groupID in groupIDs.Split('|'))
            {

                rowCounter = 0;
                int columnCount;

                #region Get the data needed for this table

                int tableVid = int.Parse(groupID);
                // Iterate for each table in the group
                //List<AxisOrdinateControls> xDimensions = controlList.Where(d => d.AxisOrientation == "X" && d.TableID == tableVid).OrderBy(d => d, compare).ToList();
                List<AxisOrdinateControls> xDimensions = controlList.Where(d => d.AxisOrientation == "X" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).ToList();
                xDimensions.SetupTopBranchOrder();
                xDimensions = xDimensions.OrderBy(d => d, compare).ToList();
                columnCount = xDimensions.Count;
                List<AxisOrdinateControls> yDimensions = controlList.Where(d => d.AxisOrientation == "Y" && d.TableID == tableVid && string.IsNullOrEmpty(d.SpecialCase)).OrderBy(d => d.Order).ToList();

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

                List<int> ColumnStartingPositions;
                List<int> ColumnWidths;
                ColumnCalculator.CalcColumnStartingPositionsAndWidths(out ColumnStartingPositions, out ColumnWidths, xDimensions, yDimensions, constants, startLocationX, firstColumnWidth);


                #endregion

                #region Work out LocationY for each row

                List<int> RowStartingPositions;
                List<int> RowHeights;
                RowCalculator.CalcRowHeights(out RowStartingPositions, out RowHeights, yDimensions, constants, firstColumnWidth);

                #endregion

                // Capture the location of Y - we only need to work out the header height to get the label location.
                Debug.WriteLine(string.Format("After initial additions: {0}", startLocationY));

                #region Multiple Rows user selection items
                if (mulitpleRowUserControls.Any())
                {
                    // Use the mulitpleRowUserControls to populate the controls.
                    startLocationY += constants.ROW_HEIGHT * (mulitpleRowUserControls.Count * 2); // Labels and combos
                    rowsForTextComboBoxes += (mulitpleRowUserControls.Count * 2);
                    Debug.WriteLine(string.Format("After mulitpleRowUserControls: {0}", startLocationY));
                }
                #endregion

                #region Data Entry Combos
                List<AxisOrdinateControls> textComboBoxes = controlList.Where(d => d.TableID == tableVid && d.SpecialCase == "data entry dropdown" && d.AxisOrientation != "Z").OrderBy(d => d.Order).ToList();
                if (textComboBoxes.Any())
                {
                    // The ZDim data entry combos appear before the headers
                    startLocationY += constants.ROW_HEIGHT * (textComboBoxes.Count * 2); // Labels and combos
                    rowsForTextComboBoxes += (textComboBoxes.Count * 2);
                    Debug.WriteLine(string.Format("After textComboBoxes: {0}", startLocationY));
                }
                #endregion

                if (!fixedDimension && !twoDimOpen)
                {
                    if (!horizontal)
                    {
                        #region Row Labels
                        int maxTopParentHeight = constants.ROW_HEIGHT;
                        ColumnCalculator.IterativeMaxHeight(xDimensions,xDimensions, constants.LABEL_Width, constants.ROW_HEIGHT, constants.LABEL_Height_Division, ref maxTopParentHeight);

                        Debug.WriteLine(string.Format("Before column headers: {0}", startLocationY));


                        // Calculate the maximum height using what we know.

                        // The second returns the max height in context of the max top parent.
                        int maxFormHeight = 0;
                        StringBuilders tempStringBuilder = new StringBuilders();
                        int tempControlCounter = 0;
                        DesignerHelpers.PopulateHeaders(columnCount, ColumnStartingPositions, 0, firstColumnWidth, constants, 0, xDimensions, ref maxFormHeight, ColumnWidths, maxLabelLevel, tempStringBuilder, ref tempControlCounter, false, false, false);
                        maxFormHeight += constants.CONTROL_MARGIN_CENTRAL + constants.LABEL_Height + constants.CONTROL_MARGIN;
                        maxFormHeight += constants.CONTROL_MARGIN_CENTRAL;
                        Debug.WriteLine(string.Format("After maxFormHeight: {0}", maxFormHeight));

                        startLocationY += maxFormHeight;
                        Debug.WriteLine(string.Format("After column headers: {0}", startLocationY));


                        //if (addButtonParameter) startLocationY += constants.ROW_HEIGHT;
                        //// This is for the nPage combos;
                        //if (pnlTopAdded) startLocationY += constants.CONTROL_MARGIN + constants.LABEL_Height + constants.CONTROL_MARGIN_CENTRAL + constants.ROW_HEIGHT + constants.CONTROL_MARGIN;

                        // Row with SolvencyCurrencyTextBoxes:
                        DesignerHelpers.AddRowLabels(ref rowCounter, yDimensions, ColumnStartingPositions, startLocationX, startLocationY, constants, stringBuilders, firstColumnWidth, false, ref controlCounter, ref maxFormY, RowStartingPositions, RowHeights, formControl: true);
                        //labelRowLabelControlNames.AddRange(DesignerHelpers.AddRowLabels(ref rowCounter, yDimensions, ColumnStartingPositions, startLocationX, startLocationY, constants, stringBuilders, firstColumnWidth, false, ref controlCounter, ref maxFormY, false));

                        #endregion

                        // Vertical Width
                        //panel1Width = maxFormY + constants.ROW_HEIGHT;
                        panel1Width = ColumnWidths[0];
                    }
                }
                else
                {
                    panel1Width = 0;
                }

                #region Prepare for next table


                //ColumnStartingPositions;
                //ColumnWidths;

                int temp = ColumnStartingPositions[columnCount + 1];

                // Next table - reset values:
                //startLocationX = startLocationX + firstColumnWidth + constants.CODE_COLUMN_WIDTH + ((columnCount - abstractHeader)*constants.CURRENCY_COLUMN_WIDTH);
                startLocationX = ColumnStartingPositions[columnCount + 1];

                int tempStartLocationX = ColumnStartingPositions[columnCount] + constants.CURRENCY_COLUMN_WIDTH;
                if (startLocationX != tempStartLocationX)
                {
                    string debug = "stop";
                }

                //startLocationX = startLocationX + LABEL_COLUMN_WIDTH + CODE_COLUMN_WIDTH + ((columnCount - abstractHeader) * CURRENCY_COLUMN_WIDTH);
                startLocationY = startLocationYConstant;
                if (comboAdded) startLocationY += constants.ROW_HEIGHT*2;

                #endregion

                tableCount++;
            }

            #endregion

            #region Finishing off

            if (!fixedDimension && !twoDimOpen)
            {
                if (!horizontal)
                {
                    AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, startLocationXConstant, controlHeight + constants.SCROLL_BAR_Width, 0, ".", constants.LABEL_Width, constants.LABEL_Height, true, false);
                    //labelRowLabelControlNames.Add(AddSolvencyControls.AddSolvencyLabel(stringBuilders, controlCounter, startLocationXConstant, controlHeight + constants.SCROLL_BAR_Width, 0, ".", constants.LABEL_Width, constants.LABEL_Height, false));
                }
            }

            List<string> Panel2ControlNames = new List<string>();
            /**********************************************/
            // Add user control
            /**********************************************/
            ControlParameter parCtrl = new ControlParameter(stringBuilders);
            parCtrl.CtrlWidth = controlWidth;
            parCtrl.CtrlHeight = controlHeight;
            parCtrl.ControlCount = 0; // Fixed to ensure name used in Repository is correct.

            // The autoscroll on the SolvencyPanel must not cause a horizontal scroll bar to appear.
            if (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow)
                parCtrl.CtrlWidth -= 30;

            string controlNameNested = AddSolvencyControls.AddUserControl(parCtrl, classNameControl, templateType, false);

            if (mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge != LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow)
            {
                // We need to nest the control within a SolvencyPanel or else there will not be a vertical scroll bar.
                parCtrl.ControlName = "pnlContainer";
                parCtrl.LocationX = 0;
                parCtrl.LocationY = 0;
                string panelName = AddSolvencyControls.AddSolvencyPanel(parCtrl, new List<string> { controlNameNested }, "Fill", true, false);
            }
            Panel2ControlNames.Add(parCtrl.ControlName);    


            

            // At this point we have been through each of the tables.
            // We know the max

            /**********************************************/
            // Add the add button if required
            /**********************************************/

            // Never at this level - its on the user control

            // Add the add button
            bool addButton = false;
            string addButtonName = "";


            /**********************************************/
            // Compile everything together on the split container.
            /**********************************************/
            int splitterDistance = (panel1Width == 0) ? 0 : (panel1Width + 2 * constants.LEVEL_TAB);

            ControlParameter par = new ControlParameter(stringBuilders);

            if (!twoDimOpen)
            {
                par.ControlName = "dr_Main";
                par.LocationX = splitterDistance;
                par.LocationY = pnlTopAdded ? constants.ROW_HEIGHT*3 : 0;
                par.CtrlWidth = controlWidth + 10; // ((controlWidth < 777) ? 777 : controlWidth) - splitterDistance;
                par.CtrlHeight = controlHeight + 10 + constants.SCROLL_BAR_Height; // +panel1Width;


                // string controlName = AddSolvencyControls.AddSolvencySplitContainer(par, horizontal, labelHeaderLabelControlNames, labelRowLabelControlNames, Panel2ControlNames, splitterDistance, true);
                string controlName = AddSolvencyControls.AddSolvencyDataRepeater(par, horizontal, labelHeaderLabelControlNames, labelRowLabelControlNames, Panel2ControlNames, splitterDistance, !fixedDimension, templateType, true);
            }
            else
            {
                // Special Cases
                /**********************************************/
                // Compile everything together on the split container.
                /**********************************************/
                par.ControlName = "splitForm";
                par.LocationX = 0;
                par.LocationY = pnlTopAdded ? constants.ROW_HEIGHT * 3 : 0;
                par.CtrlWidth = (controlWidth < 777) ? 777 : controlWidth;
                par.CtrlHeight = controlHeight + panel1Width;
                
                string controlName = AddSolvencyControls.AddSolvencySplitContainer(par, horizontal, labelHeaderLabelControlNames, labelRowLabelControlNames, Panel2ControlNames, splitterDistance, true);

            }
            if (pnlTopAdded)
                par.SbThisControl.AppendLine("            this.Controls.Add(this.pnlTop);");


            // Fill in the bottom sections of each part
            InstantiateBottom(stringBuilders.sbInstantiate);
            PropertiesBottom(stringBuilders.sbProperties);
            int width = startLocationX; // Updated on table iteration.
            width += constants.CODE_COLUMN_WIDTH;

            if (width < controlWidth + 10) width = controlWidth + 10;

            // We have the largest Y coordinate for a contorl:
            // maxFormY
            maxFormY += constants.ROW_HEIGHT + startLocationYConstant;

            int height = startLocationYConstant + (rowCounter*constants.ROW_HEIGHT) + startLocationYConstant + (4*constants.ROW_HEIGHT) + (extraRows*constants.ROW_HEIGHT);
            if (comboAdded) height += constants.ROW_HEIGHT*2;
            height += rowsForTextComboBoxes * constants.ROW_HEIGHT;
            height += constants.ROW_HEIGHT;

            if (height < controlHeight + 10) height = controlHeight + 10;

            ThisControlBottom(stringBuilders.sbThisControl, className, width, height, templateType, twoDimOpen);

            DeclareBottom(stringBuilders.sbDeclare);

            #endregion

            
            // Draw all parts together.
            return stringBuilders.ToString();
        }

        #region Helper Subs

        private static void DeclareBottom(StringBuilder sbDeclare)
        {
            sbDeclare.AppendLine();
            sbDeclare.AppendLine("   }");
            sbDeclare.AppendLine("}");
            sbDeclare.AppendLine();
        }

        private static void ThisControlBottom(StringBuilder sbThisControl, string className, int width, int height, enumClosedTemplateType templateType, bool twoDimOpen)
        {
            sbThisControl.AppendLine(string.Format(@"            this.Name = ""{0}""; ", className));
            sbThisControl.AppendLine(string.Format("            this.Size = new System.Drawing.Size({0}, {1}); ", width, height));
            if(!twoDimOpen)
                sbThisControl.AppendLine(string.Format("            this.Load += new System.EventHandler(this.Repeater_Load);"));

            if(templateType == enumClosedTemplateType.VerticalSeparator)
                sbThisControl.AppendLine(string.Format("            this.Resize += new System.EventHandler(this.Repeater_Resize);"));
            

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
            sbInstantiate.AppendLine("using System.Windows.Forms;");
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


    }
}
