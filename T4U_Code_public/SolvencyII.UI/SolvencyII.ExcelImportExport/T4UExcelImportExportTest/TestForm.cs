using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using T4UExcelImportExportLib;
using T4UExcelImportExportLib.DpmObjects;
using T4UExcelImportExportLib.Exceptions;
using T4UExcelImportExportLib.UI.Dialog;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain;
using SolvencyII.Data.Shared;

namespace T4UExcelImport
{
    public partial class Form1 : Form
    {
        int progressPercentage;
        string message;

        Timer timerClock = new Timer();
        List<string> tablecodes = new List<string>();

        public Form1()
        {
            InitializeComponent();

            timerClock.Tick += timerClock_Tick;
            timerClock.Interval = 100;
            timerClock.Enabled = true;
        }

        void timerClock_Tick(object sender, EventArgs e)
        {
            label3.Text = message;
            label4.Text = progressPercentage.ToString() + "% completed";
            progressBar1.Value = progressPercentage;

            listBox1.Items.Clear();
            foreach(string s in tablecodes)
            {
                listBox1.Items.Add(s);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IExcelImport importExcel = new ExcelImportImpl();

            importExcel.ImportFromFileProgressChanged += importExcel_ImportFromFileProgressChanged;
            importExcel.ImportFromFileCompleted += importExcel_ImportFromFileCompleted;

            if(textBox1.Text == "")
            {
                MessageBox.Show("Database input file cannot be null.");
                return;
            }

            //Create coonection 
            ISolvencyData dpmConn = new SQLiteConnection(textBox1.Text);
            int count = dpmConn.ExecuteScalar<int>("Select count(*) from mTable");


            if (textBox2.Text == "")
            {
                MessageBox.Show("Excel input file cannot be null.");
                return;
            }

            IExcelConnection excelConnection = new ExcelConnection(textBox2.Text);

            try
            {
                
                excelConnection.OpenConnection();

                string[] tableCodes = importExcel.GetTableCodes(excelConnection);

                TablesListDlg dlg = new TablesListDlg(tableCodes);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                dInstance instance;

                //Get the instance from dpm
                GetSQLData sqlData = new GetSQLData(textBox1.Text);
                instance = sqlData.GetInstanceDetails(1);

                //Create new if instance is null
                if(instance == null)
                {
                    long instanceID;

                    instance = new dInstance {
                        ModuleID = 1,
                        FileName = "abcd.xbrl",
                        Timestamp = DateTime.Now,
                        EntityIdentifier = "abcd",
                        EntityName = "abcd",
                        PeriodEndDateOrInstant = DateTime.Parse("2015-09-30"),
                        EntityCurrency = "EUR"
                    };

                    PutSQLData putSql = new PutSQLData(textBox1.Text);

                    putSql.InsertUpdateInstance(instance, out instanceID);
                    instance.InstanceID = instanceID;

                }


                if (dlg.SelectedTableCodes == null || dlg.SelectedTableCodes.Count() == 0)
                {
                    importExcel.ImportFromFile(dpmConn, excelConnection, instance);
                }
                else
                {
                    //Import the file, passing the filter(user selection)
                    //importExcel.ImportFromFile(dpmConn, excelConnection, instance, dlg.SelectedTableCodes);
                    importExcel.ImportFromFileAsync(dpmConn, excelConnection, instance, dlg.SelectedTableCodes);
                }

                
            }
            catch(T4UExcelImportExportException te)
            {
                MessageBox.Show(te.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close the excel and database connection
                excelConnection.CloseConnection();
                dpmConn.Close();
            }


            //Connect to abc.db and display in grid view



        }

        void importExcel_ImportFromFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            message = (string)e.UserState;
        }

        void importExcel_ImportFromFileProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressPercentage = e.ProgressPercentage;
            message = "Processing " + (string) e.UserState;

            tablecodes.Add((string)e.UserState);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "SQLite files (*.db)|*.db|All files (*.*)|*.*";

            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox2.Text = dialog.FileName;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            IExcelExport exportExcel = new ExcelExportImpl();

            exportExcel.ExportToFileProgressChanged += importExcel_ImportFromFileProgressChanged;
            exportExcel.ExportToFileCompleted += importExcel_ImportFromFileCompleted;

            if (textBox1.Text == "")
            {
                MessageBox.Show("Database input file cannot be null.");
                return;
            }

            //Create coonection 
            ISolvencyData dpmConn = new SQLiteConnection(textBox1.Text);
            int count = dpmConn.ExecuteScalar<int>("Select count(*) from mTable");


            if (textBox2.Text == "")
            {
                MessageBox.Show("Excel input file cannot be null.");
                return;
            }

            IExcelConnection excelConnection = new ExcelConnection(textBox2.Text);

            try
            {

                excelConnection.OpenConnection();

                TableInfo info = new TableInfo();
                //get the whole list of table codes from database
                //IList<mTable> tableList = (IList<mTable>)info.GetAllTables(dpmConn);

                string[] tableCodes = new string[] 
                { 
                    "S.01.01.02.01", "S.02.02.01.01", "S.01.02.01.01",
                    "S.01.02.02.01", "S.06.03.02.01", "S.15.02.02.01",
                    "S.16.01.01.01"
                };

                    /*= (from t in tableList
                                       select t.TableCode).ToArray<string>();*/

                TablesListDlg dlg = new TablesListDlg(tableCodes);
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                    return;

                dInstance instance;

                //Get the instance from dpm
                GetSQLData sqlData = new GetSQLData(textBox1.Text);
                instance = sqlData.GetInstanceDetails(1);

                //Create new if instance is null
                if (instance == null)
                {
                    long instanceID;

                    instance = new dInstance
                    {
                        ModuleID = 1,
                        FileName = "abcd.xbrl",
                        Timestamp = DateTime.Now,
                        EntityIdentifier = "abcd",
                        EntityName = "abcd",
                        PeriodEndDateOrInstant = DateTime.Parse("2015-09-30"),
                        EntityCurrency = "EUR"
                    };

                    PutSQLData putSql = new PutSQLData(textBox1.Text);

                    putSql.InsertUpdateInstance(instance, out instanceID);
                    instance.InstanceID = instanceID;

                }


                if (dlg.SelectedTableCodes == null || dlg.SelectedTableCodes.Count() == 0)
                {
                    exportExcel.ExportToFileAsync(dpmConn, excelConnection, instance);
                }
                else
                {
                    //Import the file, passing the filter(user selection)
                    //importExcel.ImportFromFile(dpmConn, excelConnection, instance, dlg.SelectedTableCodes);
                    exportExcel.ExportToFileAsync(dpmConn, excelConnection, instance, dlg.SelectedTableCodes);
                }


            }
            catch (T4UExcelImportExportException te)
            {
                MessageBox.Show(te.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //Close the excel and database connection
                excelConnection.CloseConnection();
                dpmConn.Close();
            }
        }
    }
}
