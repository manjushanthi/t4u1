using System.Collections.Generic;
using System.Text;
using SolvencyII.Domain.Constants;
using ucGenerator.Classes;

namespace ucGenerator
{
    public static class MainClass
    {
        public static string GenerateCode(enumClosedTemplateType templateType, string className, string groupTableVIDs, int tableVID, string frameworkCode, string version, string typeListDelimited, string tableListDelimited, string pkListDelimited, string classNameControl, string userControlGeneratorVersion, bool twoDimOpen, List<CreateFileParameter> superParameter)
        {
            

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using System.ComponentModel.Composition;");
            sb.AppendLine("using System.Linq; ");
            sb.AppendLine("using System.Windows.Forms;");
            sb.AppendLine("using SolvencyII.Domain;");
            sb.AppendLine("using SolvencyII.Domain.Interfaces;");
            sb.AppendLine("using SolvencyII.UI.Shared.Respositories;");
            sb.AppendLine("using SolvencyII.UI.Shared.UserControls; "); 
            sb.AppendLine("using SolvencyII.Data.Shared.Repository;");
            sb.AppendLine("using Microsoft.VisualBasic.PowerPacks;");
            sb.AppendLine();
            sb.AppendLine("namespace SolvencyII.UI.UserControls ");
            sb.AppendLine("{ ");
            sb.AppendLine("   [Export(typeof(ISolvencyUserControl))]");
            if(!twoDimOpen)
                sb.AppendLine(string.Format("    public partial class {0} : UserControlBase, ISolvencyUserControl ", className));
            else
                sb.AppendLine(string.Format("    public partial class {0} : UserControlBase2, ISolvencyUserControl, IClosedRowFactory, IControlContainsRepository ", className));
            sb.AppendLine("    { ");

            sb.AppendLine(string.Format("       // UserControlGenerator version: {0}", userControlGeneratorVersion));
            sb.AppendLine(string.Format("       public ISolvencyUserControl Create {{get{{return new {0}();}}}}", className));
            // sb.AppendLine(string.Format("       public IClosedRowControl CreateRowControl() {{ return new {0}(); }}", classNameControl));
            if (twoDimOpen)
                sb.AppendLine("       public IClosedRowRepository CtrlRepository { get; set; } ");
            
            sb.AppendLine("       public double Version { get; private set; }");
            sb.AppendLine("       public string GroupTableIDs { get; private set; }");
            sb.AppendLine("       public int TableVID { get; private set; }");
            sb.AppendLine("       public string FrameworkCode { get; private set; }");
            sb.AppendLine("       public int VersionT4U { get { return 2; } }");
            sb.AppendLine("       public int SingleZOrdinateID { get; set; }");
            sb.AppendLine("       public List<Type> DataTypes { get; private set; }");
            sb.AppendLine("       public List<string> DataTables { get; private set; }");
            //sb.AppendLine("       public List<long> PK_IDs { get; set; } ");
            sb.AppendLine("       public bool SpecialCases { get; set; } ");


            sb.AppendLine();

            sb.AppendLine(string.Format("       public {0}()", className));
            sb.AppendLine("       {");
            sb.AppendLine("           InitializeComponent();");


            //if ((templateType & enumClosedTemplateType.FixedDimension) == enumClosedTemplateType.FixedDimension)
            //    sb.AppendLine("           {splitForm.SplitterWidth = 1;}");

            sb.AppendLine(string.Format(@"           Version = {0};", version));
            sb.AppendLine(string.Format(@"           GroupTableIDs = ""{0}"";", groupTableVIDs));
            sb.AppendLine(string.Format(@"           TableVID = {0};", tableVID));
            sb.AppendLine(string.Format(@"           FrameworkCode = ""{0}"";", frameworkCode));
            sb.AppendLine(string.Format(@"           DataTypes = new List<Type>{{ {0} }};", typeListDelimited));
            sb.AppendLine(string.Format(@"           DataTables = new List<string>{{ {0} }};", tableListDelimited));
            //sb.AppendLine(string.Format(@"           PK_IDs = new List<long> {{ {0} }};", pkListDelimited));
            sb.AppendLine(string.Format(@"           SpecialCases = {0};", twoDimOpen.ToString().ToLower()));
            if (twoDimOpen)
                sb.AppendLine(string.Format(@"           CtrlRepository = new SpecialCaseRepository<{0}>({1}, splitForm.Panel2, splitForm.Orientation == Orientation.Horizontal, DataTables);", classNameControl, string.Format("{0}{1}", classNameControl, 0)));
            //else
                //sb.AppendLine(string.Format(@"           CtrlRepository = new ClosedRowRepository<{0}>({1}, dr_Main.ItemTemplate, dr_Main.LayoutStyle == Microsoft.VisualBasic.PowerPacks.DataRepeaterLayoutStyles.Vertical, DataTables);", classNameControl, string.Format("{0}{1}", classNameControl, 0)));

            //if(addButton) sb.AppendLine(string.Format(@"           btnAdd.Click += (sender, args) => CtrlRepository.AddControl();"));

            sb.AppendLine("       }"); // End sub

            if (twoDimOpen)
                sb.AppendLine(string.Format(@"public IClosedRowControl CreateRowControl() {{ {{ return new {0}(); }} }}", classNameControl));

            sb.AppendLine();
            sb.AppendLine("       public void addControlText(object sender, EventArgs e) { AddSingleControlText(sender); }  "); // End sub
            sb.AppendLine("       public void deleteControlText(object sender, EventArgs e) { DeleteSingleControlText(sender); } "); // End sub
            if (twoDimOpen)
            {
                sb.AppendLine("       public void addRow(object sender, EventArgs e) { AddRow(sender); }  "); // End sub
                sb.AppendLine("       public void addCol(object sender, EventArgs e) { AddCol(sender); }  "); // End sub
            }

            sb.AppendLine();

            if (!twoDimOpen)
            {
                sb.AppendLine("        #region Data ");

                string[] types = tableListDelimited.Replace("\"", string.Empty).Split(',');
                int count = 0;
                foreach (string s in types)
                {
                    sb.AppendLine();
                    sb.AppendLine(string.Format(@"        private List<{0}> mainData{1} = new List<{0}>() {{new {0}()}};", s, count));
                    sb.AppendLine(string.Format(@"        private List<long?> deletedData{0} = new List<long?>();", count));
                    sb.AppendLine(string.Format(@"        public GenericRepository<{0}> Repository{1}{{get{{if (!DesignMode) return new GenericRepository<{0}>();return null;}} }}", s, count));
                    sb.AppendLine(string.Format(@"        private {0} SingleRow{1} {{get {{ return new {0}(); }} }}", s, count));
                    count++;
                }
                sb.AppendLine("        #endregion");


                sb.AppendLine();
                sb.AppendLine("       ");
                sb.AppendLine("       private void Repeater_Load(object sender, EventArgs e)");
                sb.AppendLine("       {");
                sb.AppendLine("           dr_Main.VirtualMode = true;");
                sb.AppendLine("           dr_Main.ItemValueNeeded += dr_Main_ItemValueNeeded;");
                sb.AppendLine("           dr_Main.ItemsRemoved += dr_Main_ItemsRemoved;");
                sb.AppendLine("           if (DesignMode) dr_Main.ItemCount = 1;");
                sb.AppendLine("       }");


                //Repeater_Resize
                if (templateType == enumClosedTemplateType.VerticalSeparator)
                {
                    sb.AppendLine();
                    sb.AppendLine("       private void Repeater_Resize(object sender, EventArgs e)");
                    sb.AppendLine("       {");
                    ConstantsForDesigner constants = new ConstantsForDesigner();
                    sb.AppendLine(string.Format("            dr_Main.Width = Width - dr_Main.Location.X - {0};", constants.SCROLL_BAR_Width));
                    sb.AppendLine("       }");
                }

                sb.AppendLine();
                sb.AppendLine("       private void dr_Main_ItemsRemoved(object sender, DataRepeaterAddRemoveItemsEventArgs e){");

                for (int i = 0; i < count; i++)
                {
                    sb.AppendLine(string.Format("           deletedData{0}.Add(mainData{0}[e.ItemIndex].PK_ID);mainData{0}.RemoveAt(e.ItemIndex);", i));
                }
                //sb.AppendLine("           deletedData0.Add(mainData0[e.ItemIndex].PK_ID);mainData0.RemoveAt(e.ItemIndex);");
                //if(count == 2)
                //    sb.AppendLine("           deletedData1.Add(mainData1[e.ItemIndex].PK_ID);mainData1.RemoveAt(e.ItemIndex);");

                sb.AppendLine("       }");



                sb.AppendLine();
                sb.AppendLine("       private void dr_Main_ItemValueNeeded(object sender, DataRepeaterItemValueEventArgs e)");
                sb.AppendLine("       {");
                sb.Append(@"           if (e.ItemIndex < mainData0.Count){");

                sb.Append(string.Format("object[] data = new object[{0}];", count));
                for (int i = 0; i < count; i++)
                {
                    sb.Append(string.Format("data[{0}] = mainData{0}[e.ItemIndex];", i));
                }
                sb.Append("e.Value = data;");


                //if(count == 1)
                //    sb.AppendLine("                       object[] data = new object[1];data[0] = mainData0[e.ItemIndex];e.Value = data;");
                //else
                //    sb.AppendLine("                       object[] data = new object[2];data[0] = mainData0[e.ItemIndex];data[1] = mainData1[e.ItemIndex];e.Value = data;");



                sb.AppendLine("            }");
                sb.AppendLine("       }");

                sb.AppendLine();
                sb.AppendLine("       private void RefreshDataRepeater() {dr_Main.BeginResetItemTemplate();dr_Main.EndResetItemTemplate();}");

                sb.AppendLine();
                sb.AppendLine("       public void AddRecord()");
                sb.AppendLine("       {");

                for (int i = 0; i < count; i++)
                {
                    sb.AppendLine(string.Format("           mainData{0}.Add(SingleRow{0});", i));
                }

                //sb.AppendLine("           mainData0.Add(SingleRow0);");
                //if(count == 2) 
                //    sb.AppendLine("           mainData1.Add(SingleRow1);");


                sb.AppendLine("           dr_Main.ItemCount = mainData0.Count;");
                sb.AppendLine("           dr_Main.CurrentItemIndex = mainData0.Count - 1;");
                sb.AppendLine("       }");


                sb.AppendLine();
                sb.AppendLine("       public void DelRecord(){dr_Main.RemoveAt(dr_Main.CurrentItemIndex);}");
                sb.AppendLine();
                sb.AppendLine("       public void RefreshDR(){dr_Main.ItemCount = mainData0.Count;RefreshDataRepeater();}");

                sb.AppendLine();
                sb.AppendLine("       public void SetupData(int i, string query)");
                sb.AppendLine("       {");

                for (int i = 0; i < count; i++)
                {
                    sb.AppendLine(string.Format("           if (i == {0})using (var repository{0} = Repository{0}){{if (repository{0} != null){{ mainData{0} = repository{0}.Select(query).ToList();if (mainData{0}.Count == 0) mainData{0}.Add(SingleRow{0});}} }}", i));
                }


                sb.AppendLine("       }");

                sb.AppendLine();
                sb.AppendLine("       public void BindRepeater(){dr_Main.ItemCount = mainData0.Count;}");


                sb.AppendLine();
                sb.AppendLine("        public bool UpdateData(out string errorText, List<ISolvencyPageControl> nPageControls)");
                sb.AppendLine("        {");

                sb.AppendLine("            try {");

                for (int i = 0; i < count; i++)
                {
                    sb.AppendLine(string.Format("                using (var repository{0} = Repository{0})", i));
                }


                sb.AppendLine("                {");
                sb.AppendLine("                    bool success = true;");

                sb.AppendLine(string.Format("                    repository{0}.BeginTransaction();  // Shared singleton connection => one transaction", 0));

                sb.AppendLine("                    success = UserRepository(dr_Main, repository0, mainData0, deletedData0, nPageControls);");

                for (int i = 1; i < count; i++)
                {
                    sb.AppendLine(string.Format("                    success = success & UserRepository(dr_Main, repository{0}, mainData{0}, deletedData{0}, nPageControls);", i));
                }

                sb.AppendLine("                    if (success) {");
                sb.AppendLine(string.Format("                        repository{0}.Commit();", 0));
                sb.AppendLine(@"                        errorText = """";");
                sb.AppendLine("                    } else {");
                sb.AppendLine(string.Format("                        repository{0}.RollBack();", 0));
                sb.AppendLine(@"                        errorText = repository0.ErrorMessage;");

                for (int i = 1; i < count; i++)
                {
                    sb.AppendLine(string.Format(@"                        errorText += ""\r\n"" + repository1.ErrorMessage;", i));
                }

                sb.AppendLine("                    }");
                sb.AppendLine("                    return success;");
                sb.AppendLine("                }");
                sb.AppendLine("            }");
                sb.AppendLine("            catch (Exception e)");
                sb.AppendLine("            {");
                sb.AppendLine("                MessageBox.Show(e.ToString());");
                sb.AppendLine("                errorText = e.Message;");
                sb.AppendLine("            }");
                sb.AppendLine("            return false;");
                sb.AppendLine("        }");



                sb.AppendLine();

                // This is only used with open templates that have only one table

                sb.AppendLine("       public void SetPK_ID(long PK_ID){");
                sb.AppendLine("           mainData0.Clear();");
                sb.AppendLine("           if (PK_ID == -1){mainData0.Add(SingleRow0);}");
                sb.AppendLine("           else{using (var repository0 = Repository0){mainData0.Add(repository0.Where(r => r.PK_ID == PK_ID).FirstOrDefault());}}");
                sb.AppendLine("           dr_Main.ItemCount = 1;RefreshDataRepeater();");
                sb.AppendLine("       }");
            }
            else
            {
                // Special Cases

                sb.AppendLine("       public void AddRecord(){}");
                sb.AppendLine("       public void DelRecord(){}");
                sb.AppendLine("       public void RefreshDR(){}");
                sb.AppendLine("       public void SetupData(int i, string query){}");
                sb.AppendLine("       public void BindRepeater(){}");
                sb.AppendLine("       public bool IsValid(){return true;}");
                sb.AppendLine(@"       public bool UpdateData(out string errorText, List<ISolvencyPageControl> nPageControls){errorText = """";return true;}");
                sb.AppendLine("       public void SetPK_ID(long PK_ID){}");

            }

            sb.AppendLine();
            sb.AppendLine("   } "); // End class
            sb.AppendLine("} "); // End Namespace

            return sb.ToString();
        }

    }
}
