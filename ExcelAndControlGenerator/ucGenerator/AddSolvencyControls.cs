using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Domain.Constants;
using SolvencyII.UI.Shared.Controls;
using ucGenerator.Classes;

namespace ucGenerator
{
    /// <summary>
    /// Centralised code for writing Solvency controls onto template.designers.cs
    /// </summary>
    public static class AddSolvencyControls
    {
        public static ConstantsForDesigner constants = new ConstantsForDesigner();



        public static string AddSolvencyDataRepeater(ControlParameter par, bool horizontal, List<string> labelHeaderLabelControlNames, List<string> labelRowLabelControlNames, List<string> panel2ControlNames, int splitterDistance, bool semiOpen, enumClosedTemplateType templateType, bool formControl = true)
        {
            string controlName = par.ControlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyDataRepeater {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyDataRepeater();", controlName));

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");

            // par.SbProperties.AppendLine(string.Format(@"this.{0}.Dock = System.Windows.Forms.DockStyle.Fill;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth + 6, par.CtrlHeight + 6));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));", controlName));
            // if (!semiOpen)
            if(templateType == enumClosedTemplateType.HorizontalSeparator || templateType == enumClosedTemplateType.FixedDimension)
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Dock = DockStyle.Fill;", controlName));


            if (!horizontal)
            {
                par.SbProperties.AppendLine(string.Format(@"this.{0}.LayoutStyle = Microsoft.VisualBasic.PowerPacks.DataRepeaterLayoutStyles.Horizontal;", controlName));
            }
            else
            {
                par.SbProperties.AppendLine(string.Format(@"this.{0}.LayoutStyle = Microsoft.VisualBasic.PowerPacks.DataRepeaterLayoutStyles.Vertical;", controlName));
            }

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}.ItemTemplate", controlName));
            par.SbProperties.AppendLine("//");
            if (!horizontal)
            {
                foreach (string header in labelHeaderLabelControlNames)
                {
                    par.SbProperties.AppendLine(string.Format("this.{0}.ItemTemplate.Controls.Add(this.{1});", controlName, header));
                }
            }
            else
            {
                // par.SbProperties.AppendLine(string.Format(@"this.{0}.ItemTemplate.Hide();", controlName));
            }
            foreach (string row in labelRowLabelControlNames)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.ItemTemplate.Controls.Add(this.{1});", controlName, row));
            }

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}.ItemTemplate", controlName));
            par.SbProperties.AppendLine("//");
            if (horizontal)
            {
                foreach (string header in labelHeaderLabelControlNames)
                {
                    par.SbProperties.AppendLine(string.Format("this.{0}.ItemTemplate.Controls.Add(this.{1});", controlName, header));
                }
            }

            foreach (string p2Ctrl in panel2ControlNames)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.ItemTemplate.Controls.Add(this.{1});", controlName, p2Ctrl));
            }


            par.SbProperties.AppendLine(string.Format("this.{0}.ItemTemplate.AutoScroll = true;", controlName));

            par.SbProperties.AppendLine(string.Format("this.{0}.ItemTemplate.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            // par.SbProperties.AppendLine(string.Format("this.{0}.ItemTemplate.Height = {1};", controlName, splitterDistance));

            if (splitterDistance == 0 || horizontal)
            {
                // par.SbProperties.AppendLine(string.Format(@"this.{0}.Panel1Collapsed = true;", controlName));
            }


            if (formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));

            return controlName;
        }





        public static string AddSolvencyLabel(StringBuilders stringBuilders, int controlCount, int locationX, int locationY, int ordinateID, string text, int labelwidth, int labelHeight, bool formControl, bool autoSize, string specifiedName = "")
        {
            return AddSolvencyControls.AddSolvencyLabel(stringBuilders.sbInstantiate, stringBuilders.sbProperties, stringBuilders.sbThisControl, stringBuilders.sbDeclare, controlCount, locationX, locationY, ordinateID, text, labelwidth, labelHeight, false, false, formControl, autoSize, specifiedName);
        }

        public static string AddSolvencyLabel(StringBuilder sbInstantiate, StringBuilder sbProperties, StringBuilder sbThisControl, StringBuilder sbDeclare, int controlCount, int locationX, int locationY, int ordinateID, string text, int labelwidth, int labelHeight, bool boarder, bool centre, bool formControl, bool autoSize, string specifiedName = "")
        {

            string controlName = string.Format("solvencyLabel{0}", controlCount);
            if (!string.IsNullOrEmpty(specifiedName)) controlName = specifiedName;
            
            sbDeclare.AppendLine(string.Format("private SolvencyLabel {0};", controlName));
            sbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyLabel();", controlName));
            sbProperties.AppendLine("//");
            sbProperties.AppendLine(string.Format("// {0}", controlName));
            sbProperties.AppendLine("//");

            sbProperties.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Transparent;", controlName));

            sbProperties.AppendLine(string.Format("this.{0}.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;", controlName));
            sbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, locationX, locationY));
            sbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            sbProperties.AppendLine(string.Format("this.{0}.OrdinateID_Label = {1};", controlName, ordinateID));
            sbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, labelwidth, labelHeight));
            sbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, controlCount));
            sbProperties.AppendLine(string.Format(@"this.{0}.Text = ""{1}"" ;", controlName, ProcessText(text)));
            // sbProperties.AppendLine(string.Format("this.{0}.TextAlign = System.Drawing.ContentAlignment.TopCenter;", controlName));
            if (boarder) sbProperties.AppendLine(string.Format("this.{0}.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;", controlName));
            if (centre)
            {
                sbProperties.AppendLine(string.Format("this.{0}.TextAlign = System.Drawing.ContentAlignment.TopCenter;", controlName));
            }
            else
            {
                sbProperties.AppendLine(string.Format("this.{0}.TextAlign = System.Drawing.ContentAlignment.TopLeft;", controlName));
            }

            if (autoSize)
                sbProperties.AppendLine(string.Format("this.{0}.AutoSize = true;", controlName));

            if(formControl) sbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));

            return controlName;
        }

        public static Control AddSolvencyLabelControl(StringBuilder sbInstantiate, StringBuilder sbProperties, StringBuilder sbThisControl, StringBuilder sbDeclare, int controlCount, int locationX, int locationY, int ordinateID, string text, int labelwidth, int labelHeight, bool boarder, bool centre, bool formControl = true)
        {

            string controlName = string.Format("solvencyLabel{0}", controlCount);
            SolvencyLabel ctrl = new SolvencyLabel();
            ctrl.Name = controlName;
            ctrl.BackColor = Color.Transparent;
            ctrl.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;
            ctrl.Location = new Point(locationX, locationY);
            if(centre) ctrl.TextAlign = ContentAlignment.TopCenter;
            
            ctrl.OrdinateID_Label =  ordinateID;
            ctrl.Size = new Size(labelwidth, labelHeight);
            ctrl.TabIndex = controlCount;
            ctrl.Text = ProcessText(text);
            
            if(boarder) ctrl.BorderStyle = BorderStyle.FixedSingle;

            return ctrl;
        }

        public static string AddSolvencyControl(ControlParameter par, string dataTypeY, string dataTypeX, bool formControl, string specifiedName = "")
        {
            if (string.IsNullOrEmpty(dataTypeY))
            {
                if (string.IsNullOrEmpty(dataTypeX)) return AddSolvencyCurrencyTextBox(par, "Monetry", formControl, specifiedName);
                return AddSolvencyControlKnownType(par, dataTypeX, formControl, specifiedName);
            }
            return AddSolvencyControlKnownType(par, dataTypeY, formControl, specifiedName);
        }

        private static string AddSolvencyControlKnownType(ControlParameter par, string dataType, bool formControl, string specifiedName = "")
        {
            switch (dataType.ToUpper())
            {
                case "BOOLEAN":
                    return AddSolvencyCheckCombo(par, false, formControl, specifiedName);
                case "TRUE":
                    return AddSolvencyCheckCombo(par, true, formControl, specifiedName);
                case "DATE":
                    return AddSolvencyDateTimePicker(par, formControl, specifiedName);
                case "ENUMERATION/CODE":
                    //if(!par.IsTyped)
                    return AddSolvencyDataComboBox(par, formControl, specifiedName); // NAJ replaced line below 01/04/2015
                        //return AddSolvencyComboBox(par, formControl, specifiedName);
                    //return AddSolvencyRowComboBox(par, formControl);
                case "URI":
                case "STRING":
                    return AddSolvencyTextBox(par, "String", formControl, specifiedName);
                case "INTEGER":
                    return AddSolvencyCurrencyTextBox(par, "Integer", formControl, specifiedName);
                case "PERCENTAGE":
                case "PERCENT":
                    return AddSolvencyCurrencyTextBox(par, "Percentage", formControl, specifiedName);
                case "DECIMAL":
                    return AddSolvencyCurrencyTextBox(par, "Decimal", formControl, specifiedName);
                case "MONETARY":
                default:
                    return AddSolvencyCurrencyTextBox(par, "Monetry", formControl, specifiedName);

            }
        }

        private static string AddSolvencyTextBox(ControlParameter par, string type, bool formControl, string specifiedName = "")
        {
            string controlName = string.Format("solvencyTextBox{0}", par.ControlCount);
            if (!string.IsNullOrEmpty(specifiedName)) controlName = specifiedName;
            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyTextBox {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyTextBox();", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("this.{0}.BorderStyle = System.Windows.Forms.BorderStyle.None;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.{1};", controlName, type));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
            switch (type.ToLower())
            {
                case "string":
                    par.SbProperties.AppendLine(string.Format("this.{0}.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;", controlName));
                    break;
                default:
                    par.SbProperties.AppendLine(string.Format("this.{0}.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;", controlName));
                    break;
            }
            // par.SbProperties.AppendLine(string.Format("this.{0}.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;", controlName));
            if (par.IsRowKey) par.SbProperties.AppendLine(string.Format("this.{0}.IsRowKey = true;", controlName));
            if (par.GreyBox)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Enabled = false;", controlName));
                par.SbProperties.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Gray;", controlName));
            }
            if(formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        public static string AddSolvencyCheckCombo(ControlParameter par, bool alwaysTrue, bool formControl, string specifiedName = "")
        {
            string controlName;
            if (!alwaysTrue)
            {
                controlName = string.Format("solvencyCheckCombo{0}", par.ControlCount);
                if (!string.IsNullOrEmpty(specifiedName)) controlName = specifiedName;
                par.ControlName = controlName;
                par.SbDeclare.AppendLine(string.Format("private SolvencyCheckCombo {0};", controlName));
                par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyCheckCombo();", controlName));
            }
            else
            {
                controlName = string.Format("solvencyTrueCombo{0}", par.ControlCount);
                if (!string.IsNullOrEmpty(specifiedName)) controlName = specifiedName;
                par.ControlName = controlName;
                par.SbDeclare.AppendLine(string.Format("private SolvencyTrueCombo {0};", controlName));
                par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyTrueCombo();", controlName));
            }
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
            if (par.IsRowKey) par.SbProperties.AppendLine(string.Format("this.{0}.IsRowKey = true;", controlName));

            if (par.GreyBox)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Enabled = false;", controlName));
                par.SbProperties.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Gray;", controlName));
            }
            if(formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        private static string ProcessText(string text)
        {
            // Remove invalid items.
            if (text == null) return "";
            text = text.Replace("\r", "").Trim();
            text = text.Replace("\n", "").Trim();
            text = text.Replace("\"", "");
            return text;
        }

        private static string AddSolvencyCurrencyTextBox(ControlParameter par, string type, bool formControl, string specifiedName = "")
        {
            string controlName = string.Format("solvencyCurrencyTextBox{0}", par.ControlCount);
            if (!string.IsNullOrEmpty(specifiedName))
                controlName = specifiedName;

            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyCurrencyTextBox {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyCurrencyTextBox();", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("this.{0}.BorderStyle = System.Windows.Forms.BorderStyle.None;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.{1};", controlName, type));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
            par.SbProperties.AppendLine(string.Format("this.{0}.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;", controlName));
            if (par.IsRowKey) par.SbProperties.AppendLine(string.Format("this.{0}.IsRowKey = true;", controlName));
            if (par.GreyBox)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Enabled = false;", controlName));
                par.SbProperties.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Gray;", controlName));
            }
            if(formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        private static string AddSolvencyDateTimePicker(ControlParameter par, bool formControl, string specifiedName = "")
        {
            const int TOP_ADJUSTMENT = -2;

            string controlName = string.Format("solvencyDateTimePicker{0}", par.ControlCount);
            if (!string.IsNullOrEmpty(specifiedName)) controlName = specifiedName;
            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyDateTimePicker {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyDateTimePicker();", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("this.{0}.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Date;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY + TOP_ADJUSTMENT));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
            if (par.IsRowKey) par.SbProperties.AppendLine(string.Format("this.{0}.IsRowKey = true;", controlName));
            if (par.GreyBox)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Enabled = false;", controlName));
                par.SbProperties.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Gray;", controlName));
            }
            if(formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        public static string AddSolvencyComboBox(ControlParameter par, bool formControl, string specifiedName = "")
        {
            string controlName = string.Format("SolvencyComboBox{0}", par.ControlCount);
            if (!string.IsNullOrEmpty(specifiedName)) controlName = specifiedName;
            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyComboBox {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyComboBox();", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            //par.SbProperties.AppendLine(string.Format("this.{0}.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.TableNames = ""{1}"";", controlName, par.TableNames));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.AxisID = {1};", controlName, par.AxisID));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.OrdinateID = {1};", controlName, par.OrdinateID));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.HierarchyID = {1};", controlName, par.HierarchyID));

            par.SbProperties.AppendLine(string.Format(@"this.{0}.StartOrder = {1};", controlName, par.StartOrder));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.NextOrder = {1};", controlName, par.NextOrder));
            if (par.IsRowKey) par.SbProperties.AppendLine(string.Format("this.{0}.IsRowKey = true;", controlName));

            if (par.GreyBox)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Enabled = false;", controlName));
                par.SbProperties.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Gray;", controlName));
            }

            if(formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        //public static string AddSolvencyRowComboBox(ControlParameter par, bool formControl = true)
        //{
        //    string controlName = string.Format("SolvencyRowComboBox{0}", par.ControlCount);
        //    par.ControlName = controlName;
        //    par.SbDeclare.AppendLine(string.Format("private SolvencyRowComboBox {0};", controlName));
        //    par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyRowComboBox();", controlName));
        //    par.SbProperties.AppendLine("//");
        //    par.SbProperties.AppendLine(string.Format("// {0}", controlName));
        //    par.SbProperties.AppendLine("//");
        //    //par.SbProperties.AppendLine(string.Format("this.{0}.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;", controlName));
        //    par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
        //    par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
        //    par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
        //    par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
        //    par.SbProperties.AppendLine(string.Format(@"this.{0}.TableNames = ""{1}"";", controlName, par.TableNames));
        //    par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
        //    par.SbProperties.AppendLine(string.Format(@"this.{0}.AxisID = {1};", controlName, par.AxisID));
        //    par.SbProperties.AppendLine(string.Format(@"this.{0}.OrdinateID = {1};", controlName, par.OrdinateID));

        //    par.SbProperties.AppendLine(string.Format(@"this.{0}.StartOrder = {1};", controlName, par.StartOrder));
        //    par.SbProperties.AppendLine(string.Format(@"this.{0}.NextOrder = {1};", controlName, par.NextOrder));
        //    if (par.IsRowKey) par.SbProperties.AppendLine(string.Format("this.{0}.IsRowKey = true;", controlName));

        //    if (par.GreyBox)
        //    {
        //        par.SbProperties.AppendLine(string.Format("this.{0}.Enabled = false;", controlName));
        //        par.SbProperties.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Gray;", controlName));
        //    }

        //    if (formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
        //    return controlName;
        //}

        public static string AddSolvencyDataComboBox(ControlParameter par, bool formControl, string specifiedName = "")
        {
            string controlName = string.Format("SolvencyDataComboBox{0}", par.ControlCount);
            if (!string.IsNullOrEmpty(specifiedName))
                controlName = specifiedName;

            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyDataComboBox {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyDataComboBox();", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("this.{0}.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.Code;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
            // par.SbProperties.AppendLine(string.Format(@"this.{0}.TableNames = ""{1}"";", controlName, par.TableNames));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.AxisID = {1};", controlName, par.AxisID));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.OrdinateID = {1};", controlName, par.OrdinateID));

            par.SbProperties.AppendLine(string.Format(@"this.{0}.StartOrder = {1};", controlName, par.StartOrder));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.NextOrder = {1};", controlName, par.NextOrder));
            if (par.GreyBox)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Enabled = false;", controlName));
                par.SbProperties.AppendLine(string.Format("this.{0}.BackColor = System.Drawing.Color.Gray;", controlName));
            }

            if (formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        public static string AddSolvencyPageTextBox(ControlParameter par, bool formControl = true)
        {
            string controlName = string.Format("SolvencyPageTextBox{0}", par.ControlCount);
            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyPageTextBox {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyPageTextBox();", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.TableNames = ""{1}"";", controlName, par.TableNames));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.FixedDimension = true;", controlName));
            if (!string.IsNullOrEmpty(par.DimensionText))
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Text = ""{1}"";", controlName, par.DimensionText));
            if(formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        public static string AddSolvencyPanel(ControlParameter par, List<string> controlsOnPanel, string dock, bool autoScroll, bool formControl = true)
        {
            string controlName = par.ControlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyPanel {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyPanel();", controlName));

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");

            foreach (string control in controlsOnPanel)
            {
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Controls.Add(this.{1});", controlName, control));
            }

            par.SbProperties.AppendLine(string.Format(@"this.{0}.Dock = System.Windows.Forms.DockStyle.{1};", controlName, dock));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            if (autoScroll)
                par.SbProperties.AppendLine(string.Format(@"this.{0}.AutoScroll = true;", controlName));


            if(formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        public static string AddSolvencySplitContainer(ControlParameter par, bool horizontal, List<string> labelHeaderLabelControlNames, List<string> labelRowLabelControlNames, List<string> panel2ControlNames, int splitterDistance, bool formControl = true)
        {
            string controlName = par.ControlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencySplitContainer {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();", controlName));

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");

            par.SbProperties.AppendLine(string.Format(@"this.{0}.Dock = System.Windows.Forms.DockStyle.Fill;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));

            if (horizontal)
            {
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Orientation = System.Windows.Forms.Orientation.Horizontal;", controlName));
            }
            else
            {
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Orientation = System.Windows.Forms.Orientation.Vertical;", controlName));
            }


            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}.Panel1", controlName));
            par.SbProperties.AppendLine("//");
            if (!horizontal)
            {
                foreach (string header in labelHeaderLabelControlNames)
                {
                    par.SbProperties.AppendLine(string.Format("this.{0}.Panel1.Controls.Add(this.{1});", controlName, header));
                }
            }
            else
            {
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Panel1.Hide();", controlName));
            }
            foreach (string row in labelRowLabelControlNames)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Panel1.Controls.Add(this.{1});", controlName, row));
            }

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}.Panel2", controlName));
            par.SbProperties.AppendLine("//");
            if (horizontal)
            {
                foreach (string header in labelHeaderLabelControlNames)
                {
                    par.SbProperties.AppendLine(string.Format("this.{0}.Panel2.Controls.Add(this.{1});", controlName, header));
                }
            }

            foreach (string p2Ctrl in panel2ControlNames)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Panel2.Controls.Add(this.{1});", controlName, p2Ctrl));
            }


            par.SbProperties.AppendLine(string.Format("this.{0}.Panel2.AutoScroll = true;", controlName));
            
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.SplitterDistance = {1};", controlName, splitterDistance));
            //par.SbProperties.AppendLine(string.Format(@"this.{0}.Panel1MinSize = {1};", controlName, splitterDistance));

            if (splitterDistance == 0 || horizontal)
            {
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Panel1Collapsed = true;", controlName));
            }


            if(formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));

            return controlName;
        }

        public static string AddSolvencySplitContainerFixed(ControlParameter par, bool horizontal, List<string> panel1ControlNames, List<string> panel2ControlNames, bool autoScrollRightPanel, int splitterDistance, bool formControl = true)
        {
            string controlName = par.ControlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencySplitContainer {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencySplitContainer();", controlName));

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Dock = System.Windows.Forms.DockStyle.Fill;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            if (horizontal)
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Orientation = System.Windows.Forms.Orientation.Horizontal;", controlName));
            else
                par.SbProperties.AppendLine(string.Format(@"this.{0}.Orientation = System.Windows.Forms.Orientation.Vertical;", controlName));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Panel1MinSize = 0;", controlName));

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}.Panel1", controlName));
            par.SbProperties.AppendLine("//");
            foreach (string row in panel1ControlNames)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Panel1.Controls.Add(this.{1});", controlName, row));
            }

            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}.Panel2", controlName));
            par.SbProperties.AppendLine("//");
            foreach (string p2Ctrl in panel2ControlNames)
            {
                par.SbProperties.AppendLine(string.Format("this.{0}.Panel2.Controls.Add(this.{1});", controlName, p2Ctrl));
            }
            if (autoScrollRightPanel)
                par.SbProperties.AppendLine(string.Format("this.{0}.Panel2.AutoScroll = true;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight + splitterDistance));
            par.SbProperties.AppendLine(string.Format("this.{0}.SplitterDistance = {1};", controlName, splitterDistance));

            if (formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));

            return controlName;
        }

        public static string AddUserControl(ControlParameter par, string className, enumClosedTemplateType templateType, bool formControl = true)
        {
            string controlName = string.Format("{0}{1}", className, par.ControlCount);
            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private {0} {1};", className, controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new {1}();", controlName, className));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            if (templateType == enumClosedTemplateType.HorizontalSeparator)
                par.SbProperties.AppendLine(string.Format("this.{0}.Dock = System.Windows.Forms.DockStyle.Fill;", controlName));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));

            if (formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));

            return controlName;
        }

        public static string AddSolvencyButton(ControlParameter par, string text, string functionName, bool formControl, string specifiedName = "")
        {

            string controlName = string.Format("solvencyButton{0}", par.ControlCount);
            if (!string.IsNullOrEmpty(specifiedName))
                controlName = specifiedName;

            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyButton {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyButton();", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.TableNames = ""{1}"";", controlName, par.TableNames));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Text = ""{1}"";", controlName, text));
            par.SbProperties.AppendLine(string.Format("this.{0}.UseVisualStyleBackColor = true;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Click += new System.EventHandler(this.{1});", controlName, functionName));

            if (formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        public static string AddSolvencyTextComboBox(ControlParameter par, bool formControl = true)
        {
            string controlName = string.Format("SolvencyTextComboBox{0}", par.ControlCount);
            par.ControlName = controlName;
            par.SbDeclare.AppendLine(string.Format("private SolvencyTextComboBox {0};", controlName));
            par.SbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyTextComboBox();", controlName));
            par.SbProperties.AppendLine("//");
            par.SbProperties.AppendLine(string.Format("// {0}", controlName));
            par.SbProperties.AppendLine("//");
            //par.SbProperties.AppendLine(string.Format("this.{0}.ColumnType = SolvencyII.Domain.ENumerators.SolvencyDataType.String;", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, par.LocationX, par.LocationY));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            par.SbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, par.CtrlWidth, par.CtrlHeight));
            par.SbProperties.AppendLine(string.Format("this.{0}.TabIndex = {1};", controlName, par.ControlCount));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.TableNames = ""{1}"";", controlName, par.TableNames));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.ColName = ""{1}"";", controlName, par.ColName.ToUpper()));
            par.SbProperties.AppendLine(string.Format(@"this.{0}.OrdinateID = {1};", controlName, par.OrdinateID));

            // Never used
            //par.SbProperties.AppendLine(string.Format(@"this.{0}.StartOrder = {1};", controlName, par.StartOrder));
            //par.SbProperties.AppendLine(string.Format(@"this.{0}.NextOrder = {1};", controlName, par.NextOrder));

            
            if (formControl) par.SbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            return controlName;
        }

        public static string AddSolvencyLine(StringBuilder sbInstantiate, StringBuilder sbProperties, StringBuilder sbThisControl, StringBuilder sbDeclare, ref int controlCount, int x1, int y1, int width, int height, bool formControl)
        {
            string controlName = string.Format("solvencyLine{0}", controlCount);
            sbDeclare.AppendLine(string.Format("private SolvencyLine {0};", controlName));
            sbInstantiate.AppendLine(string.Format("this.{0} = new SolvencyII.UI.Shared.Controls.SolvencyLine();", controlName));
            sbProperties.AppendLine("//");
            sbProperties.AppendLine(string.Format("// {0}", controlName));
            sbProperties.AppendLine("//");
            
            sbProperties.AppendLine(string.Format("this.{0}.Location = new System.Drawing.Point({1},{2});", controlName, x1, y1));
            sbProperties.AppendLine(string.Format(@"this.{0}.Name = ""{0}"";", controlName));
            sbProperties.AppendLine(string.Format("this.{0}.Size = new System.Drawing.Size({1}, {2});", controlName, width, height));
            if (formControl) sbThisControl.AppendLine(string.Format("this.Controls.Add(this.{0});", controlName));
            controlCount++;
            return controlName;
        }

        public static Control AddSolvencyLineControl(StringBuilder sbInstantiate, StringBuilder sbProperties, StringBuilder sbThisControl, StringBuilder sbDeclare, int controlCount, int x1, int y1, int width, int height, bool formControl)
        {

            string controlName = string.Format("solvencyLine{0}", controlCount);
            SolvencyLine ctrl = new SolvencyLine();
            ctrl.Name = controlName;
            ctrl.Visible = true;
            ctrl.Location = new System.Drawing.Point(x1, y1);
            ctrl.Size = new System.Drawing.Size(width, height);

            return ctrl;
        }
    }
}
