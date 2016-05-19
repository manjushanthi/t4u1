using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using SolvencyII.Data.Shared;
using SolvencyII.Domain.Entities;
using SolvencyII.UI.Shared.Extensions;
using ucIntegration.Classes;
using ucGenerator;

namespace ucIntegration
{
    public partial class GenerateForm2 : Form
    {
        string connSrting;

        private void CreatedComplete(string message)
        {
            this.Invoke((MethodInvoker)(() =>
            {
                MessageBox.Show(message);
                EnableControls(true);
            }));
        }

        private void StatusBarUpdate(string message)
        {
            //lblStatus.Text = string.Format("{0} - {1}", StaticSettings.InstanceDescription, message);
            lblStatus.Text = message;
        }

        private void WorkingCount(int item, int max)
        {
            StatusBarUpdate(string.Format("Processing {0} of {1}", item, max));

            //progressBar1.Value = (int)(((decimal)item / (decimal)max) * 100);
        }

        private void CreateAllFilesSpecial(string version, bool createProjectFiles)
        {
            // Use the collated treeview references to nodes and sequentially list them.

            GetSQLData getData = new GetSQLData(connSrting);
            TreeBranch trunck = getData.GetNonTreeNotes();
            getData.Dispose();
            if (trunck != null && trunck.SubBranches.Any())
            {
                List<string> files = new List<string>();
                List<string> duplicates = new List<string>();
                StringBuilder sb = new StringBuilder();

                int templateCount = TreeComputations.CountTemplatesInTree(trunck);
                TreeComputations treeComp = new TreeComputations(trunck);
                int count = 1;
                string folder = txtMulti.Text;
                MainOperations.Cancel = false;
                foreach (TreeBranch treeItem in treeComp.TemplatesInATree())
                {
                    WorkingCount(count, templateCount);
                    if (MainOperations.TreeItemCreateControl(version, files, sb, treeItem, folder, false, duplicates, CreatedComplete, StatusBarUpdate, connSrting)) return;
                    count++;
                }

                if (createProjectFiles)
                {
                    // Build the project file now.

                    string switchFile = string.Format("TemplateIndex.cs", version);
                    switchFile = Path.Combine(folder, switchFile);
                    CreateSwitchFile switchGen = new CreateSwitchFile();
                    if (!switchGen.Create(switchFile, files.Distinct().ToList(), version))
                        sb.AppendLine("Failed to create project file");
                }
                if (sb.Length == 0)
                {
                    CreatedComplete(string.Format("{0} user controls successfully created.\n{1} duplicates or pre-existing user controls", files.Count(), duplicates.Count()));
                    StatusBarUpdate("Complete");
                }
                //MessageBox.Show(string.Format("Created {0} user controls successfully.", files.Count()));
                else
                    CreatedComplete(string.Format("There were some errors:\n{0}", sb));
                //MessageBox.Show(string.Format("There were some errors:\n{0}", sb));
            }
            else
            {
                StatusBarUpdate("No templates found in the database.");
            }


            //Create POCO classes
            MainOperations.CreateAllPocos(txtPoco.Text, CreatedComplete, StatusBarUpdate, connSrting);
        }

        private void EnableControls(bool enable)
        {
            Cursor.Current = enable ? Cursors.Default : Cursors.WaitCursor;
        }

        public GenerateForm2(string connectionString, string initialDirectory)
        {
            InitializeComponent();

            connSrting = connectionString;
            txtDirectory.Text = initialDirectory;
            txtMulti.Text = Path.Combine(txtDirectory.Text, "Templates");
            txtPoco.Text = Path.Combine(txtDirectory.Text, "Poco");

            // Rebuild the list TreeView
            GetSQLData getData = new GetSQLData(connectionString);
            try
            {
                // This is the first attempt at using the database so if there is an error report it
                treeView1.Populate(getData);
                // TreeViewConfig.Setup(treeListView1, UpdateFromTreeView, getData, 0);
            }
            catch (Exception e)
            {
                MessageBox.Show("There was a problem accessing the database.", "Error", MessageBoxButtons.OK);
               
            }
            finally
            {
                getData.Dispose();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            if (dialog.ShowDialog() == DialogResult.Cancel)
                return;

            txtDirectory.Text = dialog.SelectedPath;
            txtMulti.Text = Path.Combine(txtDirectory.Text, "Templates");
            txtPoco.Text = Path.Combine(txtDirectory.Text, "Poco");
        }

        private void btnExploreFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtDirectory.Text))
                Process.Start("explorer.exe", txtDirectory.Text);
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string tempFolder = Path.GetDirectoryName(txtMulti.Text + @"\");

            string pocoFolder = Path.GetDirectoryName(txtPoco.Text + @"\");

            try
            {
                if (!Directory.Exists(tempFolder))
                    Directory.CreateDirectory(tempFolder);


                if (!Directory.Exists(pocoFolder))
                    Directory.CreateDirectory(pocoFolder);
            }
            catch(Exception ex)
            {
                MessageBox.Show("An error occured: " + ex.Message);
                return;
            }


            Thread myThread = new Thread(() => CreateAllFilesSpecial(numVersion.Text, true));
            myThread.Start();            
            
            EnableControls(false);


        }
    }
}
