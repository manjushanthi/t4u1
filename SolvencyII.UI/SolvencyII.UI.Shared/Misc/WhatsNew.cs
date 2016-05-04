using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SolvencyII.Data.Shared.Dictionaries;
using SolvencyII.UI.Shared.Registry;
using SolvencyII.UI.Shared.Configuration;

namespace SolvencyII.UI.Shared.Misc
{
    /// <summary>
    /// Management of the WhatsNew text - to show the user.
    /// </summary>
    public static class WhatsNew
    {
        private static bool updateFlag;
        
        public static void Show(Action checkUpdate,bool updateEnabled)
        {
            try
            {
                
                string location =Path.GetDirectoryName(Application.ExecutablePath);
                location = Path.Combine(location, "WhatsNew.txt");
                updateFlag = updateEnabled;
                string strWhatsNew = string.Empty;
                if (File.Exists(location))
                {
                    FileInfo fileInfo = new FileInfo(location);
                    Stream fStream = fileInfo.OpenRead();
                    TextReader reader = new StreamReader(fStream);
                        strWhatsNew = reader.ReadToEnd();
                    reader.Close();                    
                    fStream.Close();
                }

                ShowPopupRichText(strWhatsNew, "What's New", checkUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        public static void ShowPopupRichText(string message, string title, Action checkUpdate)
        {
            RichTextBox rtbWhatsNew = new RichTextBox {Text = message};
            Form ctrRtb = new Form();
            ctrRtb.Text = title;
            ctrRtb.Width = 755;
            ctrRtb.Height = 400;
            ctrRtb.Enabled = true;
            ctrRtb.StartPosition = FormStartPosition.CenterScreen;
           
            rtbWhatsNew.WordWrap = true;
            rtbWhatsNew.Font = new System.Drawing.Font("Courier New", 10);
            rtbWhatsNew.GotFocus += new System.EventHandler(RTBGotFocus);
            rtbWhatsNew.ShortcutsEnabled = true;
            //rtbWhatsNew.cop
           
            ctrRtb.Controls.Add(rtbWhatsNew);



            const int margin = 10;
            Button btnUpdate = new Button();
            btnUpdate.Size = new System.Drawing.Size(132, 23);
            btnUpdate.Location = new Point(ctrRtb.Width - btnUpdate.Width - margin - 10, ctrRtb.Height - (4 * margin) - btnUpdate.Height - 8);
            btnUpdate.Text = LanguageLabels.GetLabel(64, "Check for updates");
            btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            btnUpdate.Click += (sender, args) => checkUpdate();
            ctrRtb.Controls.Add(btnUpdate);

            rtbWhatsNew.Size = new Size(ctrRtb.Width - (3 * margin), ctrRtb.Height - (5 * margin) - btnUpdate.Height - 9);
            rtbWhatsNew.Location = new Point(margin - 3, margin - 3);
            rtbWhatsNew.Anchor = ((System.Windows.Forms.AnchorStyles) ((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                                                                         | System.Windows.Forms.AnchorStyles.Left)
                                                                        | System.Windows.Forms.AnchorStyles.Right)));


            if (!updateFlag)
                btnUpdate.Enabled = false;

            rtbWhatsNew.ReadOnly = true;
            rtbWhatsNew.Enabled=true;

            //BRAG
            rtbWhatsNew.DetectUrls = true;
            rtbWhatsNew.LinkClicked += RtbWhatsNew_LinkClicked;

            ctrRtb.ShowDialog();

        }

        //BRAG
        private static void RtbWhatsNew_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        public static void RTBGotFocus(object sender, System.EventArgs e)
        {
            System.Windows.Forms.SendKeys.Send("{tab}");
        }
    }
}
