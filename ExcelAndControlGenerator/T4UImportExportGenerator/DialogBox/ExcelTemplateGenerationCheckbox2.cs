using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NetOffice.ExcelApi;
using System.Reflection;
using NetOffice.ExcelApi.Enums;
using ExcelIns = NetOffice.ExcelApi;
using AT2DPM.DAL.Model;
using AT2DPM.DAL;
using System.Collections;
using System.Globalization;
using System.Text.RegularExpressions;
//using AT2DPM.Excel.UI;


namespace T4UImportExportGenerator.DialogBox
{
    public partial class ExcelTemplateGenerationCheckbox2 : Form
    {

        IList<mModule> moduleList;

        IList<mModule> selectedList;

        public IList<mModule> SelectedList
        {
            get{return selectedList;}
        }


        public ExcelTemplateGenerationCheckbox2(IList<mModule> moduleList)
        {
            InitializeComponent();

            this.moduleList = moduleList;            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Dictionary<string, long> tables = new Dictionary<string, long>();

            if (chkModules.CheckedItems.Count <= 0)
            {
                this.Close();
                return;
            }

            selectedList = new List<mModule>();

            foreach (var item in chkModules.CheckedItems)
            {
                long moduleId = Convert.ToInt64(((KeyValuePair<long, string>)item).Key);

                mModule newModule = (from mod in moduleList
                                     where mod.ModuleID == moduleId
                                     select mod).FirstOrDefault();

                if (newModule != null)
                    SelectedList.Add(newModule);
            }           
            
            this.Close();
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked)
            {
                for (int i = 0; i < chkModules.Items.Count; i++)
                {
                    chkModules.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < chkModules.Items.Count; i++)
                {
                    chkModules.SetItemChecked(i, false);
                }
            }           
            
        }

        private void ExcelTemplateGenerationCheckbox2_Load(object sender, EventArgs e)
        {
            foreach (var module in moduleList)
            {
                chkModules.Items.Add(new KeyValuePair<long, string>(module.ModuleID, module.ModuleLabel));
            }
        }
    }
}
