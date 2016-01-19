using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

using SolvencyII.Data.SQLite;
using SolvencyII.Data.SQL;
using SolvencyII.Domain.Interfaces;
using SolvencyII.Domain.ENumerators;

namespace SolvencyII.Arelle
{
    public class ProcessArelleXmlResults
    {
        public const long UNINITIALIZED = Int64.MinValue;

        string xmlResult;
        XDocument xmlDoc;

        public ProcessArelleXmlResults(string xmlOutputLog)
        {
            xmlResult = xmlOutputLog;

        }

        private void OpenXmlDoc()
        {
            try
            {
                TextReader txtrdr = new StringReader(xmlResult);
                XDocument doc = XDocument.Load(txtrdr);
                txtrdr.Close();
            }
            catch(Exception ex)
            {
                throw new ArelleException("An error occured while processing the Arelle result.", ex);
            }

        }

        private void ProcessXmlResult()
        {
            foreach (XElement entryElement in xmlDoc.Descendants("entry"))
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

                // logging entry child elements are message (text) and ref(s) (file reference locators)
                foreach (XElement messageElt in entryElement.Elements())
                {
                    if (messageElt.Name.LocalName == "ref")
                        foreach (XAttribute dpmSigAttr in messageElt.Attributes("dpmSignature"))
                        {

                        }
                }

            }
        }

        public long StoreMessagesIntoDB(string xbrlFilePath, string connString, eDataTier dbType, long instanceID, BackgroundWorker asyncWorker)
        {
            ISolvencyData conn = null;

            switch(dbType)
            {
                case eDataTier.SqLite:
                    conn = new SQLiteConnection(connString);
                    break;

                case eDataTier.SqlServer:
                    conn = new DataConnection(connString);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("dbType", dbType, "Invalid database.");
            }


            //open the xml document
            OpenXmlDoc();



                

            return 0;
        }

    }
}
