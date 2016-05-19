using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Domain.Constants;
using SolvencyII.Domain.Entities;
using System.Linq;
using ucGenerator.Classes;

namespace ucGenerator
{
    public static class DesignerClassOpen
    {
        private static int ROW_HEIGHT = 20; // 17 too small with DateTimePicker being 20
        private static  int CODE_COLUMN_WIDTH = 30;
        public static int LABEL_Width = 106;
        public static int LABEL_Height = 13;
        public static int CURRENCY_Width = 100;
        public static int CURRENCY_Height = 13; 
        public static int COMBO_Width = 250;
        public static int COMBO_Height = 13;
        public static int COMBO_COLUMN_WIDTH = COMBO_Width + 7;

        public static string GenerateCode(string className, string groupIDs, List<AxisOrdinateControls> controlList, List<FactInformation> shadedControls, List<string> locationRanges, bool iOS, List<FormDataPage> pageData, List<NPageData> nPageData, string title)
        {
            

            // Create the string builders that are responsible for individual parts of the class

            StringBuilders stringBuilders = new StringBuilders();

            // Fill in the top sections of each part
            InstantiateTop(stringBuilders.sbInstantiate, className);
            PropertiesTop(stringBuilders.sbProperties);
            ThisControlTop(stringBuilders.sbThisControl);
            DeclareTop(stringBuilders.sbDeclare);

            // Main processing here.
            int startLocationX = 10;

            // With a new button on the control for adding new the start Y constant need to be larger:
            //const int startLocationYConstant = 10;
            const int startLocationYConstant = 30;

            int startLocationY = startLocationYConstant;
            int controlCounter = 0;
            int rowCounter = 0;
            int extraRows = 0;
            int maxFormY = 0; // Record the largest y coord.
            ConstantsForDesigner constants = new ConstantsForDesigner();

            
            #region Dimension and Hierarchy Dropdowns
            bool comboAdded = false;

            if (pageData != null && pageData.Any())
            {
                comboAdded = true;
                List<string> controlNames;
                var par3 = DesignerHelpers.nPageCombos(controlList, pageData, nPageData, stringBuilders, startLocationX, constants, out controlNames, ref comboAdded, ref startLocationY, ref controlCounter);

                // Setup the pnlTop
                par3.ControlName = "pnlTop";
                par3.CtrlWidth = 777; // Arbitary - the dock top will take care of this.
                par3.CtrlHeight = controlNames.Any() ? (constants.ROW_HEIGHT * 3) + 20 : 0;
                par3.LocationX = 0;
                par3.LocationY = 0;
                AddSolvencyControls.AddSolvencyPanel(par3, controlNames, "Top", false); 
                par3.ControlName = "";
            }

            #endregion




            // Fill in the bottom sections of each part
            if (!iOS)
            {
                InstantiateBottom(stringBuilders.sbInstantiate);
                PropertiesBottom(stringBuilders.sbProperties);
            }
            else
            {
                InstantiateBottomiOS(stringBuilders.sbInstantiate);
                PropertiesBottomiOS(stringBuilders.sbProperties);
            }
            int width = startLocationX; // Updated on table iteration.
            width += CODE_COLUMN_WIDTH; // 20140326
            // int height = startLocationYConstant + (rowCounter * ROW_HEIGHT) + startLocationYConstant + ROW_HEIGHT + (extraRows * ROW_HEIGHT);

            // We have the largest Y coordinate for a contorl:
            // maxFormY
            maxFormY += ROW_HEIGHT + startLocationYConstant;

            int height = startLocationYConstant + (rowCounter * ROW_HEIGHT) + startLocationYConstant + (4 * ROW_HEIGHT) + (extraRows * ROW_HEIGHT); //20140326
            if (comboAdded) height += ROW_HEIGHT*2;

            if (!iOS) ThisControlBottom(stringBuilders.sbThisControl, className, width, height);
            else ThisControlBottomiOS(stringBuilders.sbThisControl, className, width, height);

            if (!iOS) DeclareBottom(stringBuilders.sbDeclare);
            else DeclareBottomiOS(stringBuilders.sbDeclare);

            // Draw all parts together.
            return stringBuilders.ToString();
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

        private static int CalcLabelYLocation(int startLocationY, int level)
        {
            // Top most level is highest number
            // Lowest level is 1
            return startLocationY + (level-1)*ROW_HEIGHT;
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
    }
}
