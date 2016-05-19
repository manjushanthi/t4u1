using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SolvencyII.Data.Entities;
using SolvencyII.Data.Shared;
using SolvencyII.Data.Shared.Entities;
using SolvencyII.Data.Shared.Helpers;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.Entities;
using SolvencyII.Domain.Extensions;
using SolvencyII.UI.Shared.Configuration;
using SolvencyII.UI.Shared.Databases;
using SolvencyII.UI.Shared.Extensions;
using ucIntegration;
using ucIntegration.Classes;
using ucGenerator;

namespace ucIntegration
{
    public partial class GeneratorForm : Form
    {

        #region Properties and Declarations

        private long _instanceID;
        private TreeBranch _selectedTreeItem = null;
        private bool _cancel;

        public long InstanceID
        {
            get { return _instanceID; }
            set
            {
                _instanceID = value;
                if (RegSettings.InstanceID != _instanceID)
                    RegSettings.InstanceID = _instanceID;
                RefreshInstanceChanged();
            }
        }

        #endregion

        #region Form Events

        public GeneratorForm()
        {
            InitializeComponent();
            txtPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Templates";
            txtMulti.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Templates";
            txtPoco.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\POCO";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetupForm();
        }

        #endregion

        #region Menu Items

        private void openXBRTContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ManageDatabases.LocateAndSaveDatabasePath())
            {
                InstanceID = 0;
                Text = StaticSettings.ConnectionString;
            }
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Helper functions

        #region Main control generation function

        private void SetFileName()
        {
            txtName.Text = string.Format("{0}.cs", _selectedTreeItem.GetClassName(false));
        }

        #endregion

        #region General Helper functions

        private void RefreshInstanceChanged()
        {
            // Clean down the form if it has things on it.
            ClearForm();
            StaticSettings.InstanceDescription = "";
            StaticSettings.InstanceCurrency = "EUR";

            // Rebuild the list TreeView
            GetSQLData getData = new GetSQLData();
            try
            {
                // This is the first attempt at using the database so if there is an error report it
                treeView1.Populate(getData);
                // TreeViewConfig.Setup(treeListView1, UpdateFromTreeView, getData, 0);
            }
            catch (Exception e)
            {
                if (MessageBox.Show(string.Format("There was a problem accessing the database.\n{0}\n{1}\nDo you want to select another database?", e.Message, RegSettings.ConnectionString), "Locate SolvencyII database", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                {
                    // Locate a new database and save its path.
                    if (ManageDatabases.LocateAndSaveDatabasePath()) SetupForm();
                }
                else
                    ClearForm();
            }
            finally
            {
                getData.Dispose();
            }


        }

        private void SetupForm()
        {
            string connectionString = RegSettings.ConnectionString;
            if (connectionString != null && File.Exists(connectionString))
            {
                // This approach has been used to facilitate multiplatform development for the Data.Shared component.
                StaticSettings.ConnectionString = connectionString;

                var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

                Text = string.Format("{0} (Ver {1})", StaticSettings.ConnectionString, version);
            }
            else
            {
                // Ensure everything is cleaned down.
                MessageBox.Show("Please open a XBRT container.", "No active XBRT container");
                return;
            }
            InstanceID = RegSettings.InstanceID;
        }

        private void StatusBarUpdate(string message)
        {
            try
            {
                lblStatus.Text = string.Format("{0} - {1}", StaticSettings.InstanceDescription, message);
            }
            catch (Exception)
            {

            }
        }

        private void ClearForm()
        {
            treeView1.Nodes.Clear();
            lblSelectedTemplate.Text = "None";
            _selectedTreeItem = null;
        }

        private void EnableControls(bool enable)
        {
            btnCreate.Enabled = enable;
            btnMultiple.Enabled = enable;
            btnMultipleNoTreeView.Enabled = enable;
            btnPoco.Enabled = enable;
            menuStrip1.Enabled = enable;
            btnCancel.Visible = !enable;
            Cursor.Current = enable ? Cursors.Default : Cursors.WaitCursor;
        }

        public void CreateAllFilesSpecial(string version, bool createProjectFiles)
        {
            // Use the collated treeview references to nodes and sequentially list them.

            GetSQLData getData = new GetSQLData();
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
                    if (MainOperations.TreeItemCreateControl(version, files, sb, treeItem, folder, chkiOS.Checked, duplicates, CreatedComplete, StatusBarUpdate, null)) return;
                    count++;
                }

                if (createProjectFiles)
                {
                    // Build the project file now.
                    if (!chkiOS.Checked)
                    {
                        string projName = string.Format("SolvencyII.{0}_UserControls.csproj", version);
                        projName = Path.Combine(folder, projName);
                        CreateProject project = new CreateProject();
                        if (!project.Create(projName, files.Distinct().ToList(), version))
                            sb.AppendLine("Failed to create project file");
                    }
                    else
                    {
                        string switchFile = string.Format("TemplateIndex.cs", version);
                        switchFile = Path.Combine(folder, switchFile);
                        CreateSwitchFile switchGen = new CreateSwitchFile();
                        if (!switchGen.Create(switchFile, files.Distinct().ToList(), version))
                            sb.AppendLine("Failed to create project file");

                    }
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
        }

        private void CreatedComplete(string message)
        {
            this.Invoke((MethodInvoker)(() =>
                                            {
                                                MessageBox.Show(message);
                                                EnableControls(true);
                                            }));
        }

        private void WorkingCount(int item, int max)
        {
            StatusBarUpdate(string.Format("Processing {0} of {1}", item, max));
        }

        #endregion

        #endregion

        #region Template Selected From Tree View



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                TreeBranch item = (TreeBranch)e.Node.Tag;
                // if ((item.SubBranches.Count == 0) || (item.FilingTemplateOrTableID != 0 && item.SubBranches.Count > 1))
                if (item.SubBranches.Count == 0 || (!item.SubBranches.Any(b => b.SubBranches.Count > 0) && (item.SubBranches.Count != 1)))
                {
                    _selectedTreeItem = item;
                    lblSelectedTemplate.Text = item.DisplayText;
                    StatusBarUpdate(item.DisplayText);
                    SetFileName();
                    checkBox1.Checked = File.Exists(String.Format("{0}\\{1}", txtPath.Text, txtName.Text));
                }
                Console.WriteLine(item.DisplayText);
            }
        }



        #endregion

        #region Click events from form controls

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "CS File|*.cs";
            saveFileDialog1.Title = "Create user control file";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = Path.GetDirectoryName(saveFileDialog1.FileName);
                txtName.Text = Path.GetFileName(saveFileDialog1.FileName);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string folder = Path.GetDirectoryName(txtMulti.Text + @"\");
            if (folder != null)
            {
                if (!Directory.Exists(folder))
                {
                    if (MessageBox.Show(string.Format("Do you want to create folder {0}?", folder), "Confirm new directory", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                        Directory.CreateDirectory(folder);
                }
                if (Directory.Exists(folder))
                {
                    if (_selectedTreeItem != null)
                    {
                        if (txtPath.Text != "")
                        {
                            string path = Path.Combine(txtPath.Text, txtName.Text);
                            TreeBranch item = _selectedTreeItem;
                            string response;
                            MainOperations.CreateUserControl(item, path, numVersion.Text, out response, chkiOS.Checked, false, null);
                            MessageBox.Show(response);
                        }
                        else
                            MessageBox.Show("Please select the file to create.");
                    }
                    else
                        MessageBox.Show("Please select a template");
                }
            }
        }

        private void btnMultiple_Click(object sender, EventArgs e)
        {
            string folder = Path.GetDirectoryName(txtMulti.Text + @"\");
            if (folder != null)
            {
                if (!Directory.Exists(folder))
                {
                    if (MessageBox.Show(string.Format("Do you want to create folder {0}?", folder), "Confirm new directory", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                        Directory.CreateDirectory(folder);
                }
                if (Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                    Thread myThread = new Thread(() =>
                    {
                        string path = Path.Combine(txtPath.Text, txtName.Text);
                        var treeNodes = GetLastTreeNodes(treeView1);
                        foreach (var node in treeNodes)
                        {
                            string response;
                            if (node.SubBranches.Count == 0)
                            {
                                string filePath = folder + @"\" + string.Format("{0}.cs", node.GetClassName(false));
                                if (!File.Exists(filePath))
                                    MainOperations.CreateUserControl(node as TreeBranch, filePath, numVersion.Text, out response, chkiOS.Checked, false, null);
                            }
                            else if(checkBoxMergedTemplates.Checked)
                            {
                                string filePath = folder + @"\" + string.Format("{0}.cs", node.GetClassName(false));
                                if (!File.Exists(filePath))
                                    try
                                    {
                                        MainOperations.CreateUserControl(node as TreeBranch, filePath, numVersion.Text, out response, chkiOS.Checked, false, null);
                                    }
                                    catch (Exception)
                                    {
                                    }
                            }
                        }

                        Invoke((Action)(() => { EnableControls(true); }));
                    });
                    myThread.Start();
                    EnableControls(false);
                }
                else
                {
                    return;
                }
                //Thread myThread = new Thread(() => MainOperations.CreateAllFiles(numVersion.Text, chkCreateProject.Checked, treeView1.GetTrunck(), txtMulti.Text, chkiOS.Checked, CreatedComplete, StatusBarUpdate, WorkingCount));
                //myThread.Start();
                //EnableControls(false);
            }
            else
            {
                MessageBox.Show("Please specify a folder to use.");
            }
        }

        private void btnMultipleNoTreeView_Click(object sender, EventArgs e)
        {
            string folder = Path.GetDirectoryName(txtMulti.Text + @"\");
            if (folder != null)
            {
                if (!Directory.Exists(folder))
                {
                    if (MessageBox.Show(string.Format("Do you want to create folder {0}?", folder), "Confirm new directory", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    {
                        Directory.CreateDirectory(folder);
                    }
                    else
                    {
                        return;
                    }
                }

                Thread myThread = new Thread(() => CreateAllFilesSpecial(numVersion.Text, chkCreateProject.Checked));
                myThread.Start();
                EnableControls(false);
            }
            else
            {
                MessageBox.Show("Please specify a folder to use.");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            MainOperations.Cancel = true;
        }

        private void btnPoco_Click(object sender, EventArgs e)
        {
            string folder = Path.GetDirectoryName(txtPoco.Text + @"\");
            if (folder != null)
            {
                if (!Directory.Exists(folder))
                {
                    if (MessageBox.Show(string.Format("Do you want to create folder {0}?", folder), "Confirm new directory", MessageBoxButtons.YesNoCancel) == DialogResult.Yes)
                    {
                        Directory.CreateDirectory(folder);
                    }
                    else
                    {
                        return;
                    }
                }

                Thread myThread = new Thread(() => MainOperations.CreateAllPocos(txtPoco.Text, CreatedComplete, StatusBarUpdate));
                myThread.Start();
                EnableControls(false);
            }
            else
            {
                MessageBox.Show("Please specify a folder to use.");
            }
        }

        private void chkiOS_CheckedChanged(object sender, EventArgs e)
        {
            SetFileName();
        }

        private void btnExploreFolder_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtMulti.Text))
                Process.Start("explorer.exe", txtMulti.Text);
        }

        #endregion

        private void btnHeader_Click(object sender, EventArgs e)
        {
            if (_selectedTreeItem != null)
            {
                if (txtPath.Text != "")
                {
                    string path = Path.Combine(txtPath.Text, txtName.Text);
                    TreeBranch item = _selectedTreeItem;

                    frmHeaderTest headerTest = new frmHeaderTest();
                    headerTest.Text = _selectedTreeItem.TableCode + " " + _selectedTreeItem.DisplayText;


                    Panel resultsPanel = headerTest.GetPanel;
                    ucGenerator.Classes.DesignerHelpers.HostPanel = resultsPanel;
                    string response;
                    MainOperations.CreateUserControl(item, path, numVersion.Text, out response, chkiOS.Checked, true, null);
                    headerTest.Show();
                    MessageBox.Show(response);
                }
                else
                    MessageBox.Show("Please select the file to create.");
            }
            else
                MessageBox.Show("Please select a template");
        }

        private List<TreeBranch> GetLastTreeNodes(TreeView node)
        {
            var retList = new List<TreeBranch>();
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode item in node.Nodes)
                    retList.AddRange(GetLastTreeNodes(item));
                return retList;
            }
            return retList;
        }

        private List<TreeBranch> GetLastTreeNodes(TreeNode node)
        {
            var retList = new List<TreeBranch>();
            if (node.Nodes.Count > 0)
                foreach (TreeNode item in node.Nodes)
                {
                    retList.AddRange(GetLastTreeNodes(item));
                }
            else if (node.Nodes.Count == 0)
                retList.Add((TreeBranch)node.Tag);
            if (((TreeBranch)node.Tag).TemplateVariant == null && ((TreeBranch)node.Tag).TableCode != null)
                retList.Add((TreeBranch)node.Tag);
            return retList;
        }
    }
}
