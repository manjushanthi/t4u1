using System;
using System.Deployment.Application;
using System.Windows.Forms;
using SolvencyII.Data.Shared.Dictionaries;
using System.IO;
using SolvencyII.Domain.Configuration;

namespace SolvencyII.GUI.Classes
{
    public static class CheckUpdates
    {
        public static void CheckNow()
        {
            UpdateCheckInfo info;

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                ApplicationDeployment ad = ApplicationDeployment.CurrentDeployment;

                try
                {
                    info = ad.CheckForDetailedUpdate();
                }
                catch (DeploymentDownloadException dde)
                {
                    MessageBox.Show(LanguageLabels.GetLabel(66, "The new version of the application cannot be downloaded at this time. \n\nPlease check your network connection, or try again later. Error:") + ": " + dde.Message);
                    return;
                }
                catch (InvalidDeploymentException ide)
                {
                    MessageBox.Show(LanguageLabels.GetLabel(67, "Cannot check for a new version of the application. The deployment is corrupted. Please redeploy the application and try again. Error:") + ": " + ide.Message);
                    return;
                }
                catch (InvalidOperationException ioe)
                {
                    MessageBox.Show(LanguageLabels.GetLabel(68, "This application cannot be updated. It is likely not a ClickOnce application. Error:") + ": " + ioe.Message);
                    return;
                }

                if (info.UpdateAvailable)
                {
                    Boolean doUpdate = true;

                    if (!info.IsUpdateRequired)
                    {
                        DialogResult dr = MessageBox.Show(LanguageLabels.GetLabel(69, "An update is available. Would you like to update the application now?"), LanguageLabels.GetLabel(70, "Update available"), MessageBoxButtons.OKCancel);
                        if (!(DialogResult.OK == dr))
                        {
                            doUpdate = false;
                        }
                    }
                    else
                    {
                        // Display a message that the app MUST reboot. Display the minimum required version.
                        MessageBox.Show(LanguageLabels.GetLabel(71) +
                                        "\n => " + info.MinimumRequiredVersion.ToString(),
                                        LanguageLabels.GetLabel(70), MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }

                    if (doUpdate)
                    {
                        try
                        {
                            ad.Update();
                            MessageBox.Show(LanguageLabels.GetLabel(72, "Application has been updated and will now restart."));                            
                            if (File.Exists(StaticSettings.SolvencyIITemplateDBConnectionString))                            
                                File.Delete(StaticSettings.SolvencyIITemplateDBConnectionString);
                            Application.Restart();
                        }
                        catch (DeploymentDownloadException dde)
                        {
                            MessageBox.Show(LanguageLabels.GetLabel(73, "Cannot install the latest version of the application. \n\nPlease check your network connection, or try again later. Error:") + ": " + dde);
                            
                        }
                    }
                }
                else
                {
                    MessageBox.Show(LanguageLabels.GetLabel(74, "There is no new version available. You are using the latest version."));
                }
            }
        }
    }
}
