using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolvencyII.Data.Shared;
using System.Text;

namespace Tests.SolvencyII.Data.Shared
{
    [TestClass]
    public class ArelleCsParserTest
    {

        XDocument testCasesIndexDoc = null;

        [TestInitialize]
        public void before()
        {
            testCasesIndexDoc = XDocument.Load(FilingRulesHelper.TestCasesIndexFile);
            string testsDbFile = FilingRulesHelper.Sol2PrepDb;

            StreamWriter testsErrorLog = File.CreateText(FilingRulesHelper.TestsErrorLog); // start fresh file
            testsErrorLog.Close();

            StreamWriter testsResults = File.CreateText(FilingRulesHelper.TestsResults);
            testsResults.WriteLine("Test Name,Test Description,Test File,Result,Error Expected,Errors Observed");
            testsResults.Close();
        }

        [TestMethod]
        public void RunTestSuite()
        {
            int testsNotPassing = 0;
            string assertionMessage = "", exceptionMessage = "";
            try
            {
                string testCasesRoot = null;
                foreach (XElement testcasesElt in testCasesIndexDoc.Descendants("testcases"))
                {
                    testCasesRoot = testcasesElt.Attribute("root").Value;
                }
                foreach (XElement testcaseElt in testCasesIndexDoc.Descendants("testcase"))
                {
                    if (!RunTestCase(testCasesRoot, testcaseElt.Attribute("uri").Value.Trim()))
                        testsNotPassing += 1;
                }
                if (testsNotPassing > 0)
                    assertionMessage = string.Format("ArelleCsParser {0} tests not passed, summary in {1}{2}",
                                                     testsNotPassing,
                                                     FilingRulesHelper.TestsResults,
                                                     Environment.NewLine);
            }
            catch (Exception ex)
            {
                exceptionMessage = "Handling exception " + ex.Message;
                Debug.WriteLine(exceptionMessage);
                StreamWriter testsErrorLog = File.AppendText(FilingRulesHelper.TestsErrorLog);
                testsErrorLog.WriteLine("[Exception] Exception loading test cases index file " + ex.Message + " at " + ex.StackTrace);
                testsErrorLog.Close();
            }
            Assert.IsTrue(assertionMessage == "" && exceptionMessage == "",
                          assertionMessage + exceptionMessage);
        }

        private bool RunTestCase(String testCasesRoot, String uri)
        {
            XDocument testCaseDoc = null;
            string readMeFirstUrl = null;
            string testCaseName = null;
            string variationDescription = null;
            bool expectBoolValidity = false;
            bool expectWarning = false;
            string expectErrorCode = null;
            bool isValidityBoolean = false;
            bool result = true;
            StringBuilder errorLog = new StringBuilder();

            try
            {
                string testCaseDocUri = FilingRulesHelper.FileLocation(uri, testCasesRoot);
                string testCasesDir = Path.GetDirectoryName(testCaseDocUri);
                testCaseDoc = XDocument.Load(testCaseDocUri);

                foreach (XElement testcaseElt in testCaseDoc.Descendants())
                {
                    if (testcaseElt.Name.LocalName == "name" && testcaseElt.Parent.Name.LocalName == "testcase")
                    {
                        testCaseName = testcaseElt.Value.Trim();
                    }
                    else if (testcaseElt.Name.LocalName == "variation")
                    {
                        readMeFirstUrl = null;
                        isValidityBoolean = false; // true if @expected provided, false if error code match
                        expectBoolValidity = false;
                        expectWarning = false;
                        expectErrorCode = null;
                        variationDescription = null;
                        foreach (XElement elt in testcaseElt.Descendants())
                        {
                            switch (elt.Name.LocalName)
                            {
                                case "description":
                                    variationDescription = elt.Value.Trim();
                                    break;
                                case "instance":
                                case "schema":
                                case "linkbase":
                                    XAttribute readMeFirstAttr = elt.Attribute("readMeFirst");
                                    if (readMeFirstAttr != null && readMeFirstAttr.Value == "true")
                                        readMeFirstUrl = elt.Value.Trim();
                                    break;
                                case "result":
                                    XAttribute expectedAttr = elt.Attribute("expected");
                                    if (expectedAttr != null)
                                    {
                                        isValidityBoolean = true;
                                        expectBoolValidity = expectedAttr.Value.Trim() == "valid";
                                    }
                                    XAttribute severityAttr = elt.Attribute("severity");
                                    if (severityAttr != null)
                                    {
                                        expectWarning = severityAttr.Value.Trim() == "warning";
                                    }
                                    break;
                                case "error":
                                    isValidityBoolean = false;
                                    expectErrorCode = elt.Value.Trim();
                                    break;
                            }
                        }
                        if (readMeFirstUrl != null)
                        {
                            errorLog.AppendLine(string.Format(
                                "[info] Starting test case {0} {1} {2}", 
                                readMeFirstUrl,
                                testCaseName,
                                variationDescription));
                            string xmlLogResult = new ArelleCsParser(FilingRulesHelper.Sol2PrepDb).parseXbrl(
                                Path.Combine(new string[] { testCasesDir, readMeFirstUrl })) as string;
                            TextReader txtrdr = new StringReader(xmlLogResult);
                            XDocument doc = XDocument.Load(txtrdr);
                            txtrdr.Close();
                            StringBuilder errorCodes = new StringBuilder();
                            bool isValid = true;
                            bool variationResult = true;

                            foreach (XElement entryElement in doc.Descendants("entry"))                                    {
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

                                errorLog.AppendLine(string.Format("[{0}] {1}", code, messageText));

                                if (isValidityBoolean)
                                {
                                    if (level == "error" || (level == "warning" && expectWarning))
                                    {
                                        isValid = false;
                                        if (errorCodes.Length > 0)
                                            errorCodes.Append(", ");
                                        errorCodes.Append(code);
                                    }
                                }
                                else
                                {

                                }
                            }
                            if (expectBoolValidity != isValid)
                            {
                                variationResult = false;
                            }
                            if (!variationResult)
                                result = false;

                            StreamWriter testsResults = File.AppendText(FilingRulesHelper.TestsResults);
                            testsResults.WriteLine(string.Format(
                                "{0},\"{1}\",\"{2}\",{3},\"{4}\",\"{5}\"",
                                testCaseName,
                                variationDescription.Replace("\"", "\"\""),
                                Path.GetFileName(uri), // test case name
                                (variationResult) ? "pass" : "fail",
                                (expectErrorCode != null) ? expectErrorCode.Replace("\"", "\"\"") : ((expectBoolValidity) ? "" : "(invalid)"),
                                errorCodes.ToString().Replace("\"", "\"\"")));
                            testsResults.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                string _msg = "Handling exception " + ex.GetType().Name;
                Debug.WriteLine(_msg);
                errorLog.AppendLine(string.Format("[exception] Testcase {0}\n   exception {1}\n   readMeFirst {2}\n  stacktrace {3}", 
                    uri, ex.Message, readMeFirstUrl, ex.StackTrace));

                StreamWriter testsResults = File.AppendText(FilingRulesHelper.TestsResults);
                testsResults.WriteLine(string.Format(
                    "{0},\"{1}\",\"{2}\",{3},\"{4}\",\"{5}\"",
                    testCaseName,
                    (variationDescription != null) ? variationDescription.Replace("\"", "\"\"") : null,
                    Path.GetFileName(uri), // test case name
                    "exception",
                    "",
                    ex.Message.Replace("\"", "\"\"")));
                testsResults.Close();
            }
            try
            {
                StreamWriter testsErrorLog = File.AppendText(FilingRulesHelper.TestsErrorLog);
                testsErrorLog.Write(errorLog.ToString());
                testsErrorLog.Close();
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        [TestCleanup]
        public void cleanUp()
        {
            testCasesIndexDoc = null;
        }
    }
}
