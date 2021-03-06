﻿/*
 * Arelle Command Line interface for C#
 * 
 * Copyright 2010-2014 Mark V Systems Limited (as Licensor).
 * License information: http://www.apache.org/licenses/LICENSE-2.0.html
 *
 */
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;
using SolvencyII.UI.Shared.Loggers;
using SolvencyII.Data.Shared;
using SolvencyII.Domain;
using SolvencyII.Domain.Configuration;
using SolvencyII.Domain.ENumerators;
using System.Threading;
using System.IO.Pipes;
using System.Collections.Generic;
using System.Globalization;

namespace SolvencyII.UI.Shared.Arelle
{
    public class ArelleCmdInterface
    {
        // for status bar of main form
        //public delegate void Main_Win_Status_Bar_Update(string statusMsg);
        public delegate object Arelle_CS_Method(string fileName, BackgroundWorker asyncWorker);

        // for production with cx_Freeze exe pre-compiled
        static string arelleFolder = ArelleSetup.ArelleInstallationFolder();
        static string arelleExeFile = Path.Combine(arelleFolder, "arelleCmdLine.exe");
        //Changed
        static string[] arelleStaticArguments = {"--xdgConfigHome", "\"" + Path.Combine(ArelleSetup.ArelleInstallationFolder(), "config") + "\""
                                                };

        string arelleVersion = "unknown";

        const int ERROR_FILE_NOT_FOUND = 2;
        const int ERROR_ACCESS_DENIED = 5;

        string instanceFile = null;

        long instanceID = -1;

        BackgroundWorker asyncWorker;

        public string StatusPrompt { get; set; }
        public long InstanceID { get { return instanceID; } }

        public ArelleCmdInterface()
        {
            asyncWorker = new BackgroundWorker();
            asyncWorker.WorkerSupportsCancellation = true;
            asyncWorker.WorkerReportsProgress = true;
        }

        public ArelleCmdInterface(string statusPrompt)
        {
            StatusPrompt = statusPrompt;
            asyncWorker = new BackgroundWorker();
            asyncWorker.WorkerSupportsCancellation = true;
            asyncWorker.WorkerReportsProgress = true;
        }

        public void CancelArelleAsync()
        {
            if (asyncWorker != null)
                asyncWorker.CancelAsync();
        }

        public string GetArelleVersion()
        {
            string[] results = runArelleCmdLine(new string[] { "--version" }, null, useXmlLogFile: false);

            if (results[1].Length > 0)
            {
                string message = string.Format("There was a problem in getting Arelle version: {0}", results[1]);
                Logger.WriteLog(eSeverity.Error, message);
                throw new ArelleException(message);
            }

            arelleVersion = results[0];
            Logger.WriteLog(eSeverity.Note, string.Format("Arelle version: {0}", arelleVersion));

            return results[0];
        }

        private string XbrlInstanceAttribution()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(); // start comment below the <!-- line in XML file
            sb.AppendLine(string.Format("Generated by EIOPA T4U at {0}",
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK", System.Globalization.CultureInfo.InvariantCulture)));
            sb.AppendLine(string.Format("(c) {0} EIOPA European Insurance and Occupational Pensions Authority",
                DateTime.Today.Year));
            string t4uVersion;
	    /*
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                if (System.Deployment.Application.ApplicationDeployment.CurrentDeployment.ActivationUri.AbsolutePath.Contains("/PRE/"))
                    t4uVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                else
                    t4uVersion = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }
            else
	    */
                t4uVersion = Application.ProductVersion.ToString();
            sb.AppendLine("T4U Version " + t4uVersion);
            if (string.IsNullOrEmpty(arelleVersion) || arelleVersion == "unknown")
                GetArelleVersion();
            sb.Append(arelleVersion.Split(new char[] {'\n'})[0]); // includes platform-specific \n at end of version
            return sb.ToString();
        }

        public BackgroundWorker ParseInstanceIntoDatabase(eImportExportOperationType type, string instanceFile, ProgressChangedEventHandler arelleProgress, RunWorkerCompletedEventHandler arelleComplete)
        {
            this.instanceFile = instanceFile;
            this.instanceID = -1;

            //Register events
            if(asyncWorker != null)
            {
                asyncWorker.ProgressChanged += arelleProgress;
                asyncWorker.RunWorkerCompleted += arelleComplete;
            }

            switch (type)
            {
                case eImportExportOperationType.Integrated:
                    return runArelleCsAsync(instanceFile,
                                     new ArelleCsParser().parseXbrl);


                case eImportExportOperationType.ArelleWith:
                    return runArelleCmdLineAsync(new string[] {
                            "--file", "\"" + instanceFile + "\"",
                            "-v", // request validation
                            "--formula", "none", // block formula validation and running
                            "--store-to-XBRL-DB", string.Format("\",,,,{0},90,sqliteDpmDB\"", StaticSettings.ConnectionString),
                            // "--skipDTS",
                            "--plugins", "xbrlDB|logging/dpmSignature.py|streamingExtensions.py",
                            // packages
                            "--packages", string.Format("\"{0}\"",
                                                        String.Join("|", ArelleSetup.ArelleTaxonomiesFolderPackageFiles())),
                            "--packageManifestName", "*taxonomyPackage.xml",
                            "--skipLoading", "\"*/val/*|*-rend*|*-lab-*\"",  // block loading of formula, rendering, and label linkbases
                            "--internetConnectivity", "offline"
                            });

                case eImportExportOperationType.ArelleWithout:
                    return runArelleCmdLineAsync(new string[] {
                            "--file", "\"" + instanceFile + "\"",
                            "--store-to-XBRL-DB", string.Format("\",,,,{0},90,sqliteDpmDB\"", StaticSettings.ConnectionString),
                            "--skipDTS",
                            "--plugins", "xbrlDB|logging/dpmSignature.py|streamingExtensions.py",
                            "--internetConnectivity", "offline"
                            });

                default:
                    throw new ArelleException("type");
            }
        }

        public BackgroundWorker SaveFromDatabaseToInstance(eImportExportOperationType type, long instanceID, string instanceFile, ProgressChangedEventHandler arelleProgress, RunWorkerCompletedEventHandler arelleComplete)
        {
            this.instanceID = instanceID;
            this.instanceFile = instanceFile;
            string outputAttribution = XbrlInstanceAttribution();

            //Register events
            if (asyncWorker != null)
            {
                asyncWorker.ProgressChanged += arelleProgress;
                asyncWorker.RunWorkerCompleted += arelleComplete;
            }

            switch (type)
            {
                case eImportExportOperationType.Integrated:
                    return runArelleCsAsync(instanceFile,
                                     new ArelleCsSaver(instanceID, outputAttribution).saveXbrl);

                case eImportExportOperationType.ArelleWith:
                    return runArelleCmdLineAsync(new string[] {
                            "--file", "\"" + instanceFile + "\"",
                            "-v", // request validation
                            "--formula", "none", // block formula validation and running
                            "--load-from-XBRL-DB", string.Format("\",,,,{0},90,sqliteDpmDB,loadInstanceId={1}\"", 
                                                                 StaticSettings.ConnectionString, instanceID),
                            "--outputAttribution", string.Format("\"{0}\"", outputAttribution),
                            // "--skipDTS",
                            "--plugins", "xbrlDB|logging/dpmSignature.py",
                            // packages
                            "--packages", string.Format("\"{0}\"",
                                                        String.Join("|", ArelleSetup.ArelleTaxonomiesFolderPackageFiles())),
                            "--packageManifestName", "*taxonomyPackage.xml",
                            "--skipLoading", "\"*/val/*|*-rend*|*-lab-*\"",  // block loading of formula, rendering, and label linkbases
                            "--internetConnectivity", "offline"
                        });

                case eImportExportOperationType.ArelleWithout:
                    return runArelleCmdLineAsync(new string[] {
                            "--file", "\"" + instanceFile + "\"",
                            "--load-from-XBRL-DB", string.Format("\",,,,{0},90,sqliteDpmDB,loadInstanceId={1}\"", 
                                                                 StaticSettings.ConnectionString, instanceID),
                            "--outputAttribution", string.Format("\"{0}\"", outputAttribution),
                            "--skipDTS",
                            "--plugins", "xbrlDB",
                            "--internetConnectivity", "offline"
                        });

                default:
                    throw new ArelleException("type");
            }
        }

        public void ValidateXBRL(string instanceFile, string sqliteDB, ProgressChangedEventHandler arelleProgress, RunWorkerCompletedEventHandler arelleComplete)
        {
            this.instanceFile = instanceFile;
            this.instanceID = -1;

            //Register events
            if(asyncWorker != null)
            {
                asyncWorker.ProgressChanged += arelleProgress;
                asyncWorker.RunWorkerCompleted += arelleComplete;
            }

            runArelleCmdLineAsync(new string[] {
                    "--file", "\"" + instanceFile + "\"",
                    "-v",
                    "--formula", "none", // block formula validation and running
                    //"--load-from-XBRL-DB", ",,,," + sqliteDB + ",90,sqliteDpmDB\"",
                    //"--skipDTS",
                    "--plugins", "validateEBA.py|logging/dpmSignature.py",
                    //"--skipDTS"
                    // packages
                    "--packages", string.Format("\"{0}\"",
                                                String.Join("|", ArelleSetup.ArelleTaxonomiesFolderPackageFiles())),
                    "--packageManifestName", "*taxonomyPackage.xml",
                    "--skipLoading", "\"*/val/*|*-rend*|*-lab-*\"",  // block loading of formula, rendering, and label linkbases
                    "--internetConnectivity", "offline"
                    });
        }

        private BackgroundWorker runArelleCmdLineAsync(string[] cmdLineArgs)
        {

            asyncWorker.DoWork += delegate(object s, DoWorkEventArgs args)
                {   // any exception raised here is passed to the Error property of RunWorkerCompleted
                    string[] results = (string[])runArelleCmdLine(cmdLineArgs, asyncWorker);
                    string report = "results are missing";

                    if (!string.IsNullOrEmpty(results[0]))
                    {
                        string stdOutReport = this.processArelleXmlResults(results[0], asyncWorker);
                        if (!string.IsNullOrEmpty(results[1]))
                            report = string.Format("{0} \n{1}", results[1], stdOutReport);
                        else
                            report = stdOutReport;
                    }

                    args.Result = report;

                    if (!string.IsNullOrEmpty(results[1]))
                        throw new ArelleException(results[1]);
                };
           
            asyncWorker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
            {
                if (args.Error != null) {
                        Logger.WriteLog(eSeverity.Error, string.Format("There was a problem setting up Arelle.{0}", args.Error.Message));
                }

                Logger.WriteLog(eSeverity.Debug, string.Format("Arelle report: {0}", (string) args.Result));

                asyncWorker = null;  // dereference setupWorker (for GC encouragement)
                // enable any menus or UI components which could invoke Arelle
            };

            asyncWorker.RunWorkerAsync();
            return asyncWorker;
        }

        private BackgroundWorker runArelleCsAsync(string xbrlFile, Arelle_CS_Method arelleCsMethod)
        {

            asyncWorker.DoWork += delegate(object s, DoWorkEventArgs args)
            {   // any exception raised here is passed to the Error property of RunWorkerCompleted
                string xmlLogResult = arelleCsMethod(xbrlFile, asyncWorker) as string;
                args.Result = this.processArelleXmlResults(xmlLogResult, asyncWorker);
            };

            asyncWorker.RunWorkerCompleted += delegate(object s, RunWorkerCompletedEventArgs args)
            {
                if (args.Error != null)
                {
                    Logger.WriteLog(eSeverity.Error, string.Format("There was a problem setting up Arelle.{0}", args.Error.Message));
                }

                // report results back to main thread for woker completed
                string results = (string)args.Result;
                Logger.WriteLog(eSeverity.Debug, string.Format("C# parser output: {0}", results));

                asyncWorker = null;  // dereference setupWorker (for GC encouragement)
                // enable any menus or UI components which could invoke Arelle
            };

            asyncWorker.RunWorkerAsync();
            return asyncWorker;
        }

        private string[] runArelleCmdLine(string[] cmdLineArgs, BackgroundWorker asyncWorker, bool useXmlLogFile = true)
        {
            Process process = new Process();
            string stdOutString = "";
            string stdErrString = "";
            string logFile = (useXmlLogFile) ? GetTempFile("arelleLogFile", "xml") : null;

            try
            {
                string extraArgs = (useXmlLogFile) ? string.Format(" --logFile \"{0}\"", logFile) : "";
                NamedPipeServerStream statusPipe = null;
                if (asyncWorker != null)
                {
                    string statusPipeName = string.Format("ArelleStatusPipe{0}", System.Threading.Thread.CurrentThread.ManagedThreadId);
                    statusPipe = new NamedPipeServerStream(statusPipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                    statusRead(statusPipe, asyncWorker);
                    extraArgs += string.Format(" --statusPipe {0}", statusPipeName);
                }
                process.StartInfo.FileName = arelleExeFile;
                process.StartInfo.Arguments = String.Join(" ", arelleStaticArguments.Concat(cmdLineArgs).ToArray()) + extraArgs;
                // see this on standard output redirecting problems: http://msdn.microsoft.com/en-us/library/system.diagnostics.processstartinfo.redirectstandardoutput.aspx
                if (useXmlLogFile)
                {
                    process.StartInfo.RedirectStandardOutput = false;  // causes blocking problems when true
                }
                else
                {
                    process.StartInfo.RedirectStandardOutput = true;  // used for short string output such as --version
                    process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
                }
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.Start();
                while (!process.HasExited && (asyncWorker == null || !asyncWorker.CancellationPending))
                {
                    Thread.Sleep(300); // longer time?
                }
                if (statusPipe != null)
                {
                    if (statusPipe.IsConnected)
                        statusPipe.Disconnect();
                    statusPipe.Close();
                    statusPipe.Dispose();
                }
                if (useXmlLogFile)
                {
                    using (StreamReader logFileReader = new StreamReader(logFile, Encoding.UTF8))
                        stdOutString = logFileReader.ReadToEnd();
                    stdErrString = process.StandardError.ReadToEnd();
                } else {
                    stdOutString = process.StandardOutput.ReadToEnd();
                }
            }
            catch (Win32Exception e)
            {
                if (e.NativeErrorCode == ERROR_FILE_NOT_FOUND)
                {
                    Logger.WriteLog(eSeverity.Error, string.Format("There was a problem in the path to Arelle: {0}", e.Message));
                }

                else if (e.NativeErrorCode == ERROR_ACCESS_DENIED)
                {
                    Logger.WriteLog(eSeverity.Error, string.Format("There was a problem of permissions to access Arelle: {0}", e.Message));
                }
            }
            finally
            {
                var processList = new List<Process>();
                GetProcessAndChildren(Process.GetProcesses(), process, processList, 1);
                process.Close();
                try
                {
                    // FOR DEBUGGING: you may want to comment out this delete to examine the logFile before it is processed by code
                    if (useXmlLogFile)
                        System.IO.File.Delete(logFile);
                }
                catch (Exception)
                {
                }
                // be sure any remaining process tree is closed

                foreach (Process p in processList)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception)
                    {
                    }
                }

            }
            string[] result = { stdOutString, stdErrString };
            return result;
        }

        private string processArelleXmlResults(string xmlOutputLog, BackgroundWorker asyncWorker)
        {
            try
            {
                TextReader txtrdr = new StringReader(xmlOutputLog);
                XDocument doc = XDocument.Load(txtrdr);
                txtrdr.Close();

                // store into database, return instanceID under which messages were stored
                this.instanceID = 
                    new ArelleMessagesInterface().storeMessagesIntoDB(this.instanceFile, this.instanceID, doc, asyncWorker);

                // build string results for dialog box
                int lines = 0; // max lines to display
                const int MAX_LINES = 20;
                bool linesTruncated = false;
                StringBuilder messages = new StringBuilder();

                foreach (XElement entryElement in doc.Descendants("entry"))
                {
                    // an entry in the logging output
                    string level = entryElement.Attribute("level").Value;
                    string code = entryElement.Attribute("code").Value;
                    string messageText = "";

                    // logging ref child elements are XBRL object references (if fact, has dpmSignature)
                    foreach (XElement messageElt in entryElement.Elements())
                    {
                        if (messageElt.Name.LocalName == "message")
                        {
                            // message text from Arelle message composition
                            messageText = messageElt.Value;
                        }
                    }

                    lines += 1;
                    if (lines < MAX_LINES)
                    {
                        messages.AppendLine(messageText);

                        // logging entry child elements are message (text) and ref(s) (file reference locators)
                        foreach (XElement messageElt in entryElement.Elements())
                        {
                            if (messageElt.Name.LocalName == "ref")
                                foreach (XAttribute dpmSigAttr in messageElt.Attributes("dpmSignature"))
                                {
                                    messages.AppendLine(string.Format("   reference: {0}", dpmSigAttr.Value));
                                }
                        }
                    }
                    else
                        linesTruncated = true;
                    
                    Logger.WriteLog(
                        (level == "warning") ? eSeverity.Warning : (level == "info") ? eSeverity.Warning : eSeverity.Error, 
                        string.Format("[{0}] {1}", code, messageText));
                }
                if (linesTruncated)
                {
                    messages.AppendLine(string.Format("Output truncated, {0} messages skipped...", lines - MAX_LINES));
                }

                return messages.ToString();
            }
            catch (Exception e)
            {
                Logger.WriteLog(eSeverity.Error, string.Format("There was a problem processing Arelle output results: {0}", e.Message));
                //return string.Format("Exception raised in processing Arelle output results:\n {0}", e.Message);
                throw new ArelleException(string.Format("Exception raised in processing Arelle output results:\n {0}", e.Message), e); ;
            }
        }

        static void statusRead(NamedPipeServerStream statusPipe, BackgroundWorker asyncWorker)
        {
            PipeState state = new PipeState(statusPipe, asyncWorker);
            try
            {
                if (!statusPipe.IsConnected)
                    statusPipe.BeginWaitForConnection(statusConnectCallback, state);
                else
                    statusPipe.BeginRead(state.buffer, 0, state.buffer.Length, statusReadCallback, state);
            }
            catch (InvalidOperationException e) //disconnected/disposed
            {
                //return;
                throw new ArelleException("An error occurred.", e);
            }
        }

        static void statusConnectCallback(IAsyncResult ar)
        {
            PipeState state = (PipeState)ar.AsyncState;
            try
            {
                state.statusPipe.EndWaitForConnection(ar);
            }
            catch (IOException e) //closed
            {
                //return;
                throw new ArelleException("An error occurred.", e);
            }
            catch (ObjectDisposedException e)
            {
                //return;
                throw new ArelleException("An error occurred.", e);
            }
            statusRead(state.statusPipe, state.asyncWorker);
        }

        static void statusReadCallback(IAsyncResult ar)
        {
            PipeState state = (PipeState)ar.AsyncState;
            int bytesRead;
            try
            {
                bytesRead = state.statusPipe.EndRead(ar);
            }
            catch (IOException e) //closed
            {
                //return;
                throw new ArelleException("An error occurred.", e);
            }
            catch (ObjectDisposedException e)
            {
                //return;
                throw new ArelleException("An error occured.", e);
            }
            if (bytesRead > 0)
            {
                StringBuilder msg = new StringBuilder();
                byte[] data = new byte[bytesRead];
                Array.Copy(state.buffer, 0, data, 0, bytesRead);
                msg.Append(System.Text.Encoding.UTF8.GetString(data));
                state.asyncWorker.ReportProgress(0, msg.ToString());
            }
            try
            {
                //statusRead(state.statusPipe, state.asyncWorker);
                if (state.statusPipe.IsConnected)
                    state.statusPipe.BeginRead(state.buffer, 0, state.buffer.Length, statusReadCallback, state);

            }
            catch (InvalidOperationException e) //disconnected/disposed
            {
                //return;
                throw new ArelleException("An error occurred.", e);
            }
        }

        class PipeState
        {
            public byte[] buffer = new byte[4096];
            public NamedPipeServerStream statusPipe;
            public BackgroundWorker asyncWorker;

            public PipeState(NamedPipeServerStream statusPipe, BackgroundWorker asyncWorker)
            {
                this.statusPipe = statusPipe;
                this.asyncWorker = asyncWorker;
            }
        }



        private string GetTempFile(string fileDescriptor, string fileExtension)
        {
            string temp = System.IO.Path.GetTempPath();
            string res = string.Empty;
            while (true)
            {
                res = string.Format("{2}_{0}.{1}", 
                                    DateTime.Now.ToString("yyyyMMddHHmmss_fff"), //Guid.NewGuid().ToString(), 
                                    fileExtension, // e.g. "xml"
                                    fileDescriptor);  // e.g. "arelleLogFile"
                res = System.IO.Path.Combine(temp, res);
                if (!System.IO.File.Exists(res))
                {
                    try
                    {
                        System.IO.FileStream s = System.IO.File.Create(res);
                        s.Close();
                        break;
                    }
                    catch (Exception ex)
                    {
                        throw new ArelleException("An error occured.", ex);

                    }
                }
            }
            return res;
        } // GetTempFile

        public static void KillProcessTree(Process root)
        {
            if (root != null)
            {
                var list = new List<Process>();
                GetProcessAndChildren(Process.GetProcesses(), root, list, 1);

                foreach (Process p in list)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Exception)
                    {
                        //Log error?
                    }
                }
            }
        }

        private static int GetParentProcessId(Process p)
        {
            int parentId = 0;
            try
            {
                ManagementObject mo = new ManagementObject("win32_process.handle='" + p.Id + "'");
                mo.Get();
                parentId = Convert.ToInt32(mo["ParentProcessId"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                parentId = 0;
            }
            return parentId;
        }

        private static void GetProcessAndChildren(Process[] plist, Process parent, List<Process> output, int indent)
        {
            foreach (Process p in plist)
            {
                if (GetParentProcessId(p) == parent.Id)
                {
                    GetProcessAndChildren(plist, p, output, indent + 1);
                }
            }
            output.Add(parent);
        }
    }
}
