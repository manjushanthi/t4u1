using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain.Configuration;

namespace SolvencyII.Data.Shared
{
    public class ArelleMessagesInterface
    {
        public const long UNINITIALIZED = Int64.MinValue;

        string _sqlConnectionPath;
        SQLiteConnection _conn;
        string xbrlFilePath = null, xbrlFileName = null;
        //long factCount = 0;

        long instanceId = UNINITIALIZED;

        public ArelleMessagesInterface()
        {
            this._sqlConnectionPath = StaticSettings.ConnectionString;
        }

        public long storeMessagesIntoDB(string xbrlFilePath, long instanceID, XDocument messagesDoc, BackgroundWorker asyncWorker,string DbPath=null)
        {
            this.xbrlFilePath = xbrlFilePath;
            this.xbrlFileName = System.IO.Path.GetFileName(this.xbrlFilePath);
            if (DbPath != null)
                this._sqlConnectionPath = DbPath;
            // check that dMessages and dMessageReferences are preseint in database
            try
            {
                asyncWorker.ReportProgress(0, "Connecting to database to store messages " + this._sqlConnectionPath);
                
                this._conn = new SQLiteConnection(this._sqlConnectionPath);
                if (DbPath!=null)
                {
                    this._sqlConnectionPath = DbPath;
                    this._conn = new SQLiteConnection(this._sqlConnectionPath);
                }
                this._conn.BeginTransaction();

                // check if messages tables are defined and exist
                IEnumerable<StrResult> result = _conn.Query<StrResult>(
                    "SELECT name FROM sqlite_master WHERE type='table' AND name in ('dMessage', 'dMessageReference');");
                if (result.Count() == 0)
                {
                    // create tables
                    _conn.Execute("CREATE TABLE dMessage (" +
                                  "    MessageID INTEGER PRIMARY KEY AUTOINCREMENT," +
                                  "    InstanceID INTEGER," +
                                  "    SequenceInReport int," +
                                  "    MessageCode TEXT," +
                                  "    MessageLevel TEXT," +
                                  "    Value TEXT);");
                    _conn.Execute("CREATE INDEX MessageIndex02 ON dMessage  (InstanceID, SequenceInReport);");
                    _conn.Execute("CREATE TABLE dMessageReference (" +
                        "    MessageID INTEGER NOT NULL," +
                        "    DataPointSignature TEXT NOT NULL);");
                    _conn.Execute("CREATE INDEX MessageReferenceIndex01 ON dMessageReference  (MessageID);");
                    _conn.Execute("CREATE UNIQUE INDEX MessageReferenceIndex02 ON dMessageReference  (MessageID, DataPointSignature);");
                }
                else if (result.Count() != 2)
                {
                    throw new ApplicationException(
                        "dMessages and dMessageReferences expected in database but not found as expected");
                }
                if (instanceID < 0)
                {
                    // try to get instanceID from instance file name
                    IEnumerable<IdResult> result2 = _conn.Query<IdResult>(
                        "SELECT InstanceID AS id FROM dInstance WHERE FileName = ?", 
                        new object[] {this.xbrlFileName});
                    if (result2.Count() != 1)
                    {
                        // not there, add to dInstance
                        _conn.Execute("INSERT INTO dInstance (ModuleID, FileName, CompressedFileBlob, Timestamp, " +
                                                             "EntityScheme, EntityIdentifier, PeriodEndDateOrInstant, EntityName, EntityCurrency) " +
                                      "VALUES (?,?,?,?,?,?,?,?,?)",
                                      new object[] {-1,
                                        this.xbrlFileName,
                                        null,
                                        DateTime.Now,
                                        null, null, null, null, null});
                        result2 = _conn.Query<IdResult>(
                            "SELECT InstanceID AS id FROM dInstance WHERE FileName = ?",
                            new object[] { this.xbrlFileName });
                    }
                    if (result2.Count() != 1)
                        throw new ApplicationException(string.Format(
                            "File references instance file which is not in dInstance table and was not able to be inserted to dInstance table: file {0}",
                            this.xbrlFileName));
                    foreach (IdResult r in result2)
                    {
                        this.instanceId = r.id;
                        break;
                    }
                }
                else
                {
                    // use passed-in instance id
                    this.instanceId = instanceID; 
                }
                // store into dMessages and dMessageReferences
                if (this.instanceId >= 0)
                {
                    // delete prior messages for this instance ID
                    asyncWorker.ReportProgress(0, "Deleting messages from any prior activity on this instance");
                    IEnumerable<IdResult> result3 = _conn.Query<IdResult>(
                        "SELECT MessageID AS id FROM dMessage WHERE InstanceID = ? AND MessageCode NOT IN ('DP Duplication')",
                        new object[] { this.instanceId });
                    List<string> messageIDs = new List<string>();
                    foreach (IdResult r in result3)
                        messageIDs.Add(r.id.ToString());
                    _conn.Execute(string.Format(
                        "DELETE from dMessageReference WHERE dMessageReference.MessageID in ({0})",
                        string.Join(", ", messageIDs.ToArray<string>())));
                    _conn.Execute(
                        "DELETE FROM dMessage WHERE dMessage.InstanceID = ? AND MessageCode NOT IN ('DP Duplication')",
                        new object[] { this.instanceId });

                    asyncWorker.ReportProgress(0, "Storing messages from current action");
                    long sequenceInReport = 0;
                    foreach (XElement entryElement in messagesDoc.Descendants("entry"))
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
                        sequenceInReport += 1;
                        _conn.Execute("INSERT INTO dMessage (InstanceID, SequenceInReport, MessageCode, MessageLevel, Value) " +
                                "VALUES (?, ?, ?, ?, ?)",
                                new object[] {this.instanceId,
                                              sequenceInReport,
                                              code,
                                              level,
                                              messageText});
                        long messageId = _conn.ExecuteScalar<long>(
                            "SELECT MessageID FROM dMessage WHERE InstanceID = ? and SequenceInReport = ?", 
                            new object[] {this.instanceId,
                                          sequenceInReport});

                        // logging entry child elements are message (text) and ref(s) (file reference locators)
                        foreach (XElement messageElt in entryElement.Elements())
                        {
                            if (messageElt.Name.LocalName == "ref")
                                foreach (XAttribute dpmSigAttr in messageElt.Attributes("dpmSignature"))
                                {
                                    _conn.Execute("INSERT INTO dMessageReference (MessageID, DataPointSignature) " +
                                            "VALUES (?, ?)",
                                            new object[] {messageId,
                                                          dpmSigAttr.Value});
                                }
                        }
                    }
                    this._conn.Commit();
                    asyncWorker.ReportProgress(0, "Completed storing messages");
                }
            }
            catch (Exception)
            {
                asyncWorker.ReportProgress(0, "Handling exception from storing messages");
                if (this._conn != null)
                {
                    try
                    {
                        this._conn.Rollback();
                        this._conn.Close();
                        this._conn.Dispose();
                    }
                    catch (Exception)
                    {
                    }
                }
                throw;
            }
            asyncWorker.ReportProgress(0, "");
            return this.instanceId;

        }

        private class IdResult
        {
            public long id { get; set; }
        }

        private class StrResult
        {
            public string str { get; set; }
        }
    }
}
