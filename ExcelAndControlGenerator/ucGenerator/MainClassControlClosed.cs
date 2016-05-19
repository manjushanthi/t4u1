using System.Collections.Generic;
using System.Text;
using ucGenerator.Classes;

namespace ucGenerator
{
    /// <summary>
    /// Generates the Main class for the custom control that contains the main user information.
    /// </summary>
    public static class MainClassControlClosed
    {
        public static string GenerateCode(enumClosedTemplateType type, string className, string groupTableVIDs, int tableVID, string frameworkCode, string version, string typeListDelimited, string tableListDelimited, string pkListDelimited, enumClosedTemplateType templateType, string userControlGeneratorVersion, bool twoDimOpen, List<CreateFileParameter> superParameter)
        {

            #region Merged Template Setup

            MergeTemplateInfo mergeTemplateInfo = new MergeTemplateInfo();
            LocationRangeCalculator rangeCalculator = null;
            MergeTemplateSetup.MergeTemplateInfoSetup(superParameter, ref mergeTemplateInfo, ref rangeCalculator);

            #endregion

            bool fixedDimension = (templateType & enumClosedTemplateType.FixedDimension) == enumClosedTemplateType.FixedDimension;

            // Previously includeSplitPanel = fixedDimension;
            bool includeSplitPanel = (fixedDimension && !mergeTemplateInfo.MergeTemplate) || ((mergeTemplateInfo.MergeTemplate && mergeTemplateInfo.TypeOfMerge == LocationRangeCalculator.eTypeOfMergedTemplate.SingleRow ));


            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Windows.Forms;");
            sb.AppendLine("using SolvencyII.Domain;");
            sb.AppendLine("using SolvencyII.Domain.Interfaces;");
            sb.AppendLine("using SolvencyII.UI.Shared.Controls;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.ComponentModel;");
            sb.AppendLine("using SolvencyII.UI.Shared.Extensions;");
            sb.AppendLine();
            sb.AppendLine("namespace SolvencyII.UI.UserControls ");
            sb.AppendLine("{ "); // Start Namespace

            sb.AppendLine(string.Format("    // UserControlGenerator version: {0}", userControlGeneratorVersion));
            sb.AppendLine(@"    [DefaultBindingProperty(""UserData"")]");
            sb.AppendLine(string.Format("    public partial class {0} : SolvencyClosedRowControl, IClosedRowControl ", className));
            sb.AppendLine("    { "); // Start Class
            sb.AppendLine();

            string[] types = tableListDelimited.Replace("\"", string.Empty).Split(',');
            int count = 0;
            foreach (string s in types)
            {
                sb.AppendLine(string.Format(@"        private {0} _rowData{1};", s, count));
                count++;
            }
            sb.AppendLine(@"        private bool populating = false;");
            
            sb.AppendLine();
            sb.AppendLine(string.Format(@"        [Bindable(true)]"));
            sb.AppendLine(@"        public object[] UserData");
            sb.AppendLine(@"        {");
            sb.AppendLine(@"            get {return ReturnUserData();}");
            sb.AppendLine(@"            set{if (value != null){");

            sb.AppendLine(string.Format(@"                if (value.Length == {0}){{ ", count));
            count = 0;
            foreach (string s in types)
            {
                sb.AppendLine(string.Format(@"                _rowData{1} = ({0}) value[{1}];", s, count));
                count++;
            }
            sb.AppendLine(@"                 populating = true; ");
            sb.AppendLine(@"                 PopulateCombos(); ");
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine(string.Format(@"                 PopulateSingleControl(_rowData{0});", i));    
            }
            sb.AppendLine(@"                 populating = false;");
            sb.AppendLine(@"                }} } ");



            //if(count == 2)
            //    sb.AppendLine(string.Format(@"                if (value.Length == 2){{_rowData1 = ({0}) value[1];PopulateSingleControl(_rowData1);}}", types[1]));
            //sb.AppendLine(string.Format(@"                _rowData0 = ({0}) value[0];populating = true;PopulateSingleControl(_rowData0);populating = false;}} }}", types[0]));
            sb.AppendLine(@"        }");

            sb.AppendLine();
            sb.AppendLine(@"        private void DataChanged(object sender, string colName){if(!populating) { GatherData(); SetToolText(sender); }}");
            
            sb.AppendLine();
            sb.AppendLine(@"        private void GatherData()");
            sb.AppendLine(@"        {");
            for (int j = 0; j < count; j++)
            {
                sb.AppendLine(string.Format(@"            object userDataObject{0} = _rowData{0};GatherSingleControl(ref userDataObject{0});", j));
            }
            //if (count == 2)
            //    sb.AppendLine(@"            object userDataObjectSecond = _rowData1;GatherSingleControl(ref userDataObjectSecond);");
            sb.AppendLine(@"        }");

            sb.AppendLine();

            sb.AppendLine(@"                private object[] ReturnUserData(){object[] data;");
            sb.AppendLine(string.Format(@"                    data = new object[{0}];", count));

            for (int j = 0; j < count; j++)
            {
                sb.AppendLine(string.Format(@"                    data[{0}] = _rowData{0};", j));
            }
            sb.AppendLine("                    return data;}");                

            //if (count == 1)
            //    sb.AppendLine(@"        private object[] ReturnUserData(){object[] data;data = new object[1]; data[0] = _rowData0;return data;}");
            //else
            //    sb.AppendLine(@"        private object[] ReturnUserData(){object[] data;data = new object[2]; data[0] = _rowData0; data[1] = _rowData1;return data;}");



            sb.AppendLine();
            sb.AppendLine(@"        public event GenericDelegates.Response AddControl;");
            sb.AppendLine(@"        private void btnAdd_Click(object sender, EventArgs e) { if (AddControl != null) AddControl(); }");
            sb.AppendLine(@"        public event GenericDelegates.ListLongs DelControl;");
            sb.AppendLine(@"        private void btnDelControl_Click(List<long> res) { if (DelControl != null) DelControl(res); }");
            sb.AppendLine(@"        public event GenericDelegates.Response DelControlDR;");
            sb.AppendLine(@"        private void btnDel_Click(object sender, EventArgs e) { if (DelControlDR != null) DelControlDR(); }");


            sb.AppendLine();
            sb.AppendLine(@"        public event EventHandler AddRow;");
            sb.AppendLine(@"        private void btnAddRow_Click(object sender, EventArgs e) { if (AddRow != null) AddRow(sender, e); }");
            sb.AppendLine(@"        public event EventHandler AddCol;");
            sb.AppendLine(@"        private void btnAddCol_Click(object sender, EventArgs e) { if (AddCol != null) AddCol(sender, e); }");
            sb.AppendLine(@"        public event EventHandler DelRow;");
            sb.AppendLine(@"        private void btnDelRow_Click(object sender, EventArgs e) { if (DelRow != null) DelRow(sender, e); }");
            sb.AppendLine(@"        public event EventHandler DelCol;");
            sb.AppendLine(@"        private void btnDelCol_Click(object sender, EventArgs e) { if (DelCol != null) DelCol(sender, e); }");



            sb.AppendLine();
            sb.AppendLine(@"        public List<long> PK_IDs { get; set; }");

            sb.AppendLine();
            sb.AppendLine(string.Format("        public {0}()", className));
            sb.AppendLine("        {"); // Start Sub
            sb.AppendLine("            InitializeComponent();");


            if (includeSplitPanel)
            {
                sb.AppendLine("            { ");
                sb.AppendLine("                spltMain.SplitterWidth = 1; ");
                sb.AppendLine("                splitContainerColTitles.SplitterWidth = 1; ");
                sb.AppendLine("                splitContainerRowTitles.SplitterWidth = 1; ");
                sb.AppendLine("            } ");
            }


            sb.AppendLine(string.Format(@"            PK_IDs = new List<long> {{ {0} }};", pkListDelimited));
            if (includeSplitPanel)
            {
                sb.AppendLine("            Dock = DockStyle.Fill;");
                sb.AppendLine("            if (!DesignMode){splitContainerRowTitles.Panel2.Scroll += Panel2_Scroll;spltMain.SizeChanged += PanelResized;splitContainerRowTitles.Panel2.MouseWheel += MouseWheelScroll;}");
            }
            sb.AppendLine("        }"); // End sub


            // Load sub: BoundControl_Load 
            sb.AppendLine("        private void BoundControl_Load(object sender, EventArgs e)");
            sb.AppendLine("        {");
            sb.AppendLine("            try{");
            sb.AppendLine("                 if (!DesignMode)");
            sb.AppendLine("                     {SetupEvents(ref DelControlDR, ref AddControl);}");
            //sb.AppendLine("                     {ISolvencyUserControl ctrl = (ISolvencyUserControl)(this.Parent.Parent).Parent;DelControlDR += ctrl.DelRecord;AddControl += ctrl.AddRecord;}");
            sb.AppendLine("            }catch (NullReferenceException){");
            sb.AppendLine("                // This is a DataRepeater event.");
            sb.AppendLine("            }");
            sb.AppendLine("            this.SetupDataChangedEvents(DataChanged, GetDataControls());"); // Use of an extension
            sb.AppendLine("        }");




            if (includeSplitPanel)
            {
                sb.AppendLine("        private void MouseWheelScroll(object sender, MouseEventArgs e){PositionScrollBars();}");
                sb.AppendLine();
                sb.AppendLine("        private void PanelResized(object sender, EventArgs e){");
                sb.AppendLine("            splitContainerColTitles.Panel2.HorizontalScroll.Minimum = splitContainerRowTitles.Panel2.HorizontalScroll.Minimum;");
                sb.AppendLine("            splitContainerColTitles.Panel2.HorizontalScroll.Maximum = splitContainerRowTitles.Panel2.HorizontalScroll.Maximum;");
                sb.AppendLine("            splitContainerRowTitles.Panel1.VerticalScroll.Minimum = splitContainerRowTitles.Panel2.VerticalScroll.Minimum;");
                sb.AppendLine("            splitContainerRowTitles.Panel1.VerticalScroll.Maximum = splitContainerRowTitles.Panel2.VerticalScroll.Maximum;");
                sb.AppendLine("            PositionScrollBars();}");
                sb.AppendLine("");
                sb.AppendLine("        private void PositionScrollBars(){// winForms bug work around");
                sb.AppendLine("            int posX = -splitContainerRowTitles.Panel2.AutoScrollPosition.X;if (posX == 0) posX = 1;");
                sb.AppendLine("            int posY = -splitContainerRowTitles.Panel2.AutoScrollPosition.Y;if (posY == 0) posY = 1;");
                sb.AppendLine("            splitContainerRowTitles.Panel2.Focus();");
                sb.AppendLine("            splitContainerColTitles.Panel2.HorizontalScroll.Value = posX;");
                sb.AppendLine("            splitContainerRowTitles.Panel1.VerticalScroll.Value = posY;}");
                sb.AppendLine();
                sb.AppendLine("        private void Panel2_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e){");
                sb.AppendLine("            int pos = e.NewValue; if (pos <= 0) pos = 1;");
                sb.AppendLine("            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll) splitContainerColTitles.Panel2.HorizontalScroll.Value = pos;");
                sb.AppendLine("            else splitContainerRowTitles.Panel1.VerticalScroll.Value = pos;");
                sb.AppendLine("            splitContainerRowTitles.Panel2.Focus();}");
                sb.AppendLine();

            }
            sb.AppendLine("   } "); // End class
            sb.AppendLine("} "); // End Namespace
            return sb.ToString();
        }
    }
}
