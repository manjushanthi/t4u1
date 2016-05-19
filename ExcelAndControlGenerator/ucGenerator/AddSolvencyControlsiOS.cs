using System.Text;

namespace ucGenerator
{
    /// <summary>
    /// Not used anymore kept just as a reminder of what has been accomplished
    /// </summary>
    public static class AddSolvencyControlsiOS
    {
        public static void AddSolvencyLabel(StringBuilder sbInstantiate, StringBuilder sbProperties, StringBuilder sbThisControl, StringBuilder sbDeclare, int controlCount, int locationX, int locationY, int ordinateID, string text, int labelwidth, int labelHeight, bool greyControl)
        {

            // Pre-juggle
            //string controlName = string.Format("solvencyLabel{0}", controlCount);
            //sbProperties.AppendLine(string.Format(@"		SolvencyLabel {0} = new SolvencyLabel {{Text = ""{1}"", OrdinateID_Label = {2}, Frame = new RectangleF({3}, {4}, {5}, {6})}};", controlName, ProcessText(text), ordinateID, locationX, locationY, labelwidth, labelHeight));
            //if (greyControl) sbProperties.AppendLine(string.Format(@"		{0}.BackgroundColor = UIColor.Gray; ", controlName));
            //sbThisControl.AppendLine(string.Format("			controls.Add ({0});", controlName));


            //			labels.Add(new SolvencyControlInfo("Solvency II value", 1675, 0, 410, 10, 125, 59, false));
            sbProperties.AppendLine(string.Format(@"			labels.Add(new SolvencyControlInfo(""{0}"", {1}, {2}, {3}, {4}, {5}, {6}, {7}));", ProcessText(text), ordinateID, 0, locationX, locationY, labelwidth, labelHeight, greyControl ? "true" : "false"));

        }

        private static string ProcessText(string text)
        {
            // Remove invalid items.
            if (text == null) return "";
            text = text.Replace("\r","").Trim();
            text = text.Replace("\n", "").Trim();
            text = text.Replace("\"", "");
            return text;
        }

        public static void AddSolvencyCurrencyTextBox(StringBuilder sbInstantiate, StringBuilder sbProperties, StringBuilder sbThisControl, StringBuilder sbDeclare, int controlCount, int locationX, int locationY, int dim1, int dim2, bool greyBox, string dataPointKey, string tableCellSignature, int ctrlwidth, int ctrlHeight, string colName)
        {
            // Pre Juggle
            //string controlName = string.Format("solvencyCurrencyTextBox{0}", controlCount);
            //sbProperties.Append(string.Format(@"		SolvencyCurrencyTextBox {0} = new SolvencyCurrencyTextBox {{OrdinateID_OneDim = {1}, OrdinateID_TwoDim = {2}, Frame = new RectangleF({3}, {4}, {5}, {6}), TableCellSignature  = ""{7}"", ColName  = ""{8}""", controlName, dim1, dim2, locationX, locationY, ctrlwidth, ctrlHeight, tableCellSignature, colName));
            //if (greyBox)
            //{
            //    sbProperties.Append(@", Enabled = false, BackgroundColor = UIColor.Gray ");
            //}
            //sbProperties.AppendLine(@"};");
            //sbThisControl.AppendLine(string.Format("			controls.Add ({0});", controlName));

            
            //			textBoxes.Add (new SolvencyControlInfo ("", 1675, 1678, 410, 162, 121, 30, false, "R010C010"));
            //			textBoxes.Add (new SolvencyControlInfo ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, ""{8}""));
            sbProperties.AppendLine(string.Format(@"			textBoxes.Add (new SolvencyControlInfo (""{0}"", {1}, {2}, {3}, {4}, {5}, {6}, {7}, ""{8}""));", "", dim1, dim2, locationX, locationY, ctrlwidth, ctrlHeight, greyBox ? "true" : "false", colName));
        
        }
    
    }
}
