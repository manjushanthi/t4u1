using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AT2DPM.DAL.Model;
using AT2DPM.Events.Delegate;
using T4UImportExportGenerator.DialogBox;
using T4UImportExportGenerator.Domain;

namespace T4UImportExportGenerator
{
    public static class InvokeImportExport
    {
        private static IList<GenerateInfo> GetGenerateInfo(IList<mModule> selectedModules, string directory)
        {
            IList<GenerateInfo> generateInfo = new List<GenerateInfo>();

            foreach (mModule m in selectedModules)
            {
                GenerateInfo info = new GenerateInfo
                {
                    ModuleID = m.ModuleID,
                    ModuleCode = m.ModuleCode,
                    ModuleLabel = m.ModuleLabel,
                    FileName = m.ModuleCode + "_" + m.ModuleLabel.Trim().ToLower().Replace(' ', '_') + ".xlsx"
                };

                //Construct the excel file names from the module names
                info.FilePath = Path.Combine(directory, info.FileName);

                //Get the corresponding table codes from the list of module ID selected by the user
                info.TableCodes = MemoryProfilerProject.Program.ExcelTemplateGeneration(TemplateGeneration.DbPath, m.ModuleID);

                generateInfo.Add(info);
            }

            return generateInfo;
        }

        public static void Invoke(DPMdb dpmContext, ExcelTemplateType type)
        {

            try
            {
                //Get the list of modules from the dpmContext

                IList<mModule> modules = (from mod in dpmContext.mModules
                                          select mod).ToList<mModule>();

                ExcelTemplateGenerationCheckbox2 selectionDlg = new ExcelTemplateGenerationCheckbox2(modules);

                if (selectionDlg.ShowDialog() == DialogResult.Cancel)
                    return;

                IList<mModule> selectedModules =  selectionDlg.SelectedList;

                //With Folder browser dialog box get the directory where the user wants to store the file

                FolderBrowserDialog dialog = new FolderBrowserDialog();


                if (dialog.ShowDialog() == DialogResult.Cancel)
                    return;

                IList<GenerateInfo> generateInfo = GetGenerateInfo(selectedModules, dialog.SelectedPath);

                string[] filesInFolder = Directory.GetFiles(dialog.SelectedPath);


                //Check and delete if there are files with the same module names in the user selected folder
                int count = 0;
                foreach (GenerateInfo g in generateInfo)
                {
                    count = (from fi in filesInFolder
                                 where g.FilePath.ToUpper() == fi.ToUpper()
                                 select fi).Count();

                    if (count > 0)
                    {
                        if (MessageBox.Show("There are files already existis in the folder. Do you want to over write the files?", "File exists",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                        {
                            MessageBox.Show("Process aborted.");
                            return;
                        }


                        try
                        {
                            if (File.Exists(g.FilePath))
                            {
                                File.Delete(g.FilePath);
                            }
                        }
                        catch (IOException ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                    }
                }               

                IImportExportGenerate generate = null;

                switch(type)
                {
                    case ExcelTemplateType.BASIC_TEMPLATE:
                        generate = new BasicTemplateGenerateImpl();
                        break;
                    case ExcelTemplateType.BUSINESS_TEMPLATE:
                        generate = new BusinessTemplateGenerateImpl();
                        break;
                }
                
                if(generate == null)
                {
                    MessageBox.Show("Unknown error occured. Cannot continue the process.");
                    return;
                }

                //Create the progress dialog box
                ImportExportProgressDlg statusDlg = new ImportExportProgressDlg();

                //Subscribe to the events
                generate.Completed += statusDlg.CompletedHandler;
                generate.ProgressChanged += statusDlg.ProgressChangedHandler;

                //Get the version number from user
                string versionNumber = string.Empty;

                if ((InputBox.Show("Version Input Box",
                    "&Enter a version number:", ref versionNumber) != DialogResult.OK) || string.IsNullOrEmpty(versionNumber))
                {
                    MessageBox.Show("Version Number required");
                    return;
                }

                //Call the asynchronous 'Generate' method
                generate.GenerateAsync(dpmContext, generateInfo, versionNumber);

                //Show the progress dialog box
                statusDlg.ShowDialog();                

            }
            catch(Exception e)
            {
                MessageBox.Show("An error occured: " + e.Message);
            }
        }

        public static void InvokeConsole(DPMdb dpmContext, ExcelTemplateType type, string directory, string version, string[] modulesSelected, ProgressChangedEventHandler progress, CompletedEventHandler completed)
        {
            try
            {
                //Get the list of modules from the dpmContext

                IList<mModule> modules = (from mod in dpmContext.mModules
                                          select mod).ToList<mModule>();

                IList<mModule> selectedModules;

                if (modules != null)
                {

                    selectedModules = (from m in modules
                                       from s in modulesSelected
                                       where m.ModuleCode.ToUpper() == s.ToUpper()
                                       select m).ToList();
                }
                else
                    selectedModules = modules;

                IList<GenerateInfo> generateInfo = GetGenerateInfo(selectedModules, directory);

                string[] filesInFolder = Directory.GetFiles(directory);

                //Check and delete if there are files with the same module names in the user selected folder
                int count = 0;
                foreach (GenerateInfo g in generateInfo)
                {
                    count = (from fi in filesInFolder
                             where g.FilePath.ToUpper() == fi.ToUpper()
                             select fi).Count();

                    if (count > 0)
                    {
                        try
                        {
                            if (File.Exists(g.FilePath))
                            {
                                File.Delete(g.FilePath);
                            }
                        }
                        catch (IOException ex)
                        {
                            completed(null, new AsyncCompletedEventArgs(ex, true, "An error occured: " + ex.Message));
                            return;
                        }
                        catch (Exception e)
                        {
                            completed(null, new AsyncCompletedEventArgs(e, true, "An error occured: " + e.Message));
                            return;
                        }

                    }
                }

                IImportExportGenerate generate = null;

                switch (type)
                {
                    case ExcelTemplateType.BASIC_TEMPLATE:
                        generate = new BasicTemplateGenerateImpl();
                        break;
                    case ExcelTemplateType.BUSINESS_TEMPLATE:
                        generate = new BusinessTemplateGenerateImpl();
                        break;
                }

                if (generate == null)
                {
                    completed(null, new AsyncCompletedEventArgs(null, true, "An error occured, cannot identity the template type"));
                    return;
                }

                //Subscribe to the events
                generate.Completed += completed;
                generate.ProgressChanged += progress;

                //Call the asynchronous 'Generate' method
                generate.GenerateAsync(dpmContext, generateInfo, version);

            }
            catch (Exception e)
            {
                //MessageBox.Show("An error occured: " + e.Message);
                completed(null, new AsyncCompletedEventArgs(e, true, "An error occured: " + e.Message));
            }
        }

    }
}
