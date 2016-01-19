/*
 * Arelle streaming saver in C# with no DTS validation
 * 
 * author: Mark V Systems Limited
 * (c) Copyright 2014 Mark V Systems Limited, All rights reserved.
 * 
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain.Configuration;
using System.IO;

namespace SolvencyII.Data.Shared
{
    public class ArelleCsSaver : ArelleCsShared
    {
        const string nsXbrli = "http://www.xbrl.org/2003/instance";
        const string nsLink = "http://www.xbrl.org/2003/linkbase";
        const string nsXlink = "http://www.w3.org/1999/xlink";
        const string nsXbrldi = "http://xbrl.org/2006/xbrldi";
        const string nsXsi = "http://www.w3.org/2001/XMLSchema-instance";
        const string nsFind = "http://www.eurofiling.info/xbrl/ext/filing-indicators";

        static Regex dpSigPattern = new Regex(@"MET[(](\w+)[:](\w+)[)][|](.*)$", RegexOptions.Compiled);
        static Regex typedDimPattern = new Regex(@"[<](\w+)[:](\w+)[>]([^<]+)[<]", RegexOptions.Compiled);

        BackgroundWorker asyncWorker = null;
        string _sqlConnectionPath = null;
        SQLiteConnection _conn;
        string xbrlFilePath = null, xbrlFileName = null;
        //long factCount;
        long instanceID = -1;
        string instanceAttribution = null;

        public ArelleCsSaver(long instanceID, string instanceAttribution)
        {
            this._sqlConnectionPath = StaticSettings.ConnectionString;
            this.instanceID = instanceID;
            this.instanceAttribution = instanceAttribution;
        }

        private class Instance
        {
            public long InstanceID { get; set; }
            public long ModuleID { get; set; }
            public string EntityScheme { get; set; }
            public string EntityIdentifier { get; set; }
            public string PeriodEndDateOrInstant { get; set; }
        }

        private class FilingIndicator
        {
            public string TemplateOrTableCode { get; set; }
        }

        private class TypedDimensionMemberDefinition
        {
            public string DimensionQname { get; set; }
            public string TypedMemberQname { get; set; }
        }

        private class Fact
        {
            public string DataPointSignature { get; set; }
            public string Unit { get; set; }
            public string Decimals { get; set; }
            public Decimal? NumericValue { get; set; }
            public string DateTimeValue { get; set; }
            public bool? BooleanValue { get; set; }
            public string TextValue { get; set; }
        }

        Instance instance = null;
        IEnumerable<NamespaceDefinition> dpmPrefixedNamespaces = null;
        Dictionary<string, string> xmlnsPrefixNS = new Dictionary<string, string>();
        Dictionary<string, string> typedDimElts = new Dictionary<string, string>();

        void writeContext(XmlWriter writer, string cntxId, List<object> _toHash, string dims = "")
        {
            writer.WriteStartElement("context", nsXbrli);
            writer.WriteAttributeString("id", cntxId);
            writer.WriteStartElement("entity", nsXbrli);
            writer.WriteStartElement("identifier", nsXbrli);
            writer.WriteAttributeString("scheme", instance.EntityScheme);
            writer.WriteString(instance.EntityIdentifier);
            _toHash.Add(instance.EntityScheme);
            _toHash.Add(instance.EntityIdentifier);
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.WriteStartElement("period", nsXbrli);
            writer.WriteStartElement("instant", nsXbrli);
            writer.WriteString(instance.PeriodEndDateOrInstant);
            _toHash.Add(DateTime.Parse(instance.PeriodEndDateOrInstant).AddDays(1)
                .ToString("yyyy-MM-ddTHH:mm:ssK", System.Globalization.CultureInfo.InvariantCulture));
            writer.WriteEndElement();
            writer.WriteEndElement();
            if (!string.IsNullOrEmpty(dims))
            {
                writer.WriteStartElement("scenario", nsXbrli);
                foreach (string dimVal in dims.Split(new char[] { '|' }))
                {
                    string[] dimMem = dimVal.Split(new char[] { '(', ')' }); // [0] is dim, [1] is mem
                    string dimQn = dimMem.Length >= 1 ? dimMem[0] : "";
                    string[] dimQnParts = dimQn.Split(new char[] { ':' });
                    if (dimQnParts.Length < 2 || !xmlnsPrefixNS.ContainsKey(dimQnParts[0]))
                        throw new ArgumentException("invalid dimension Qname or prefix " + dimQn);
                    string mem = dimMem.Length >= 2 ? dimMem[1] : "";
                    if (mem != "nil" && !mem.StartsWith("<"))
                    { // is explicit
                        string[] memQnParts = mem.Split(new char[] { ':' });
                        if (memQnParts.Length < 2 || !xmlnsPrefixNS.ContainsKey(memQnParts[0]))
                            throw new ArgumentException("invalid member Qname or prefix " + mem);
                        writer.WriteStartElement("explicitMember", nsXbrldi);
                        writer.WriteAttributeString("dimension", dimQn);
                        writer.WriteString(mem);
                        writer.WriteEndElement();
                        _toHash.Add(md5hash(new QName(xmlnsPrefixNS[dimQnParts[0]], dimQnParts[1]),
                                            new QName(xmlnsPrefixNS[memQnParts[0]], memQnParts[1])));
                    }
                    else // is typed
                    {
                        writer.WriteStartElement("typedMember", nsXbrldi);
                        writer.WriteAttributeString("dimension", dimQn);
                        if (mem == "nil")
                        {
                            string typedDimEltQn = typedDimElts[dimQn];
                            string[] typedDimQnParts = typedDimEltQn.Split(new char[] { ':' });
                            if (typedDimQnParts.Length >= 2)
                            {
                                writer.WriteStartElement(typedDimQnParts[1], xmlnsPrefixNS[typedDimQnParts[0]]);
                                writer.WriteAttributeString("nil", nsXsi, "true");
                                writer.WriteEndElement();
                            }
                            _toHash.Add(md5hash(new QName(xmlnsPrefixNS[dimQnParts[0]], dimQnParts[1]), ""));
                        }
                        else
                        {
                            // parse the typed dimension into its member QName and text value portions
                            Match typedDimParts = typedDimPattern.Match(mem); // @"[<](\w+)[:](\w+)[>]([^<]+)[<]"
                            if (typedDimParts.Groups.Count > 3)
                            {
                                writer.WriteStartElement(typedDimParts.Groups[2].Value, xmlnsPrefixNS[typedDimParts.Groups[1].Value]);
                                writer.WriteString(typedDimParts.Groups[3].Value);
                                writer.WriteEndElement();
                                _toHash.Add(md5hash(new QName(xmlnsPrefixNS[dimQnParts[0]], dimQnParts[1]),
                                                              typedDimParts.Groups[3].Value));
                            }
                        }
                        writer.WriteEndElement();
                    }
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        void writeUnit(XmlWriter writer, string unitId, string unit)
        {
            writer.WriteStartElement("unit", nsXbrli);
            writer.WriteAttributeString("id", unitId);
            writer.WriteStartElement("measure", nsXbrli);
            writer.WriteString(unit);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }

        public object saveXbrl(string xbrlFilePath, BackgroundWorker asyncWorker)
        {
            this.asyncWorker = asyncWorker;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.ConformanceLevel = ConformanceLevel.Document;
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            settings.NewLineOnAttributes = true;
            this.xbrlFilePath = xbrlFilePath;
            this.xbrlFileName = Path.GetFileName(xbrlFilePath);

            this.initializeLog();

            try
            {
                XmlWriter writer = XmlWriter.Create(xbrlFilePath, settings);

                asyncWorker.ReportProgress(0, "Connecting to database " + this._sqlConnectionPath);
                this._conn = new SQLiteConnection(this._sqlConnectionPath);
                asyncWorker.ReportProgress(0, "Saving XBRL instance " + xbrlFileName);
                //factCount = 0;
                Debug.WriteLine(string.Format("Saving XBRL instance {0} at {1}", xbrlFileName, DateTime.Now));

                // find instance in DB
                IEnumerable<Instance> result = _conn.Query<Instance>(
                    "SELECT InstanceID, ModuleID, EntityScheme, EntityIdentifier, PeriodEndDateOrInstant" +
                    " FROM dInstance WHERE InstanceID = ?",
                    instanceID);
                foreach (Instance r in result)
                {
                    instance = r;
                    break;
                }
                if (instance == null)
                    return string.Format("Error, record is not found in table dInstance for instance: {0}", instanceID);

                // find module in DB   
                string xbrlSchemaRef = _conn.ExecuteScalar<string>("SELECT XbrlSchemaRef FROM mModule WHERE ModuleID = ?", instance.ModuleID);
                if (string.IsNullOrEmpty(xbrlSchemaRef))
                    return "Error, the module in mModule, corresponding to the instance, was not found for " + xbrlFileName;
            
                // find prefixes and namespaces in DB
                dpmPrefixedNamespaces = _conn.Query<NamespaceDefinition>("SELECT * FROM [vwGetNamespacesPrefixes]");

                if (dpmPrefixedNamespaces.Count<NamespaceDefinition>() == 0)
                    return "Error, namespace definitions, corresponding to the instance, were not found for " + xbrlFileName;

                writer.WriteStartDocument();
                if (!string.IsNullOrEmpty(this.instanceAttribution))
                    writer.WriteComment(this.instanceAttribution);

                writer.WriteStartElement("xbrl", nsXbrli);
                foreach (NamespaceDefinition ns in dpmPrefixedNamespaces)
                {
                    if (!xmlnsPrefixNS.ContainsKey(ns.Prefix))
                    {
                        writer.WriteAttributeString("xmlns", ns.Prefix, null, ns.Namespace);
                        xmlnsPrefixNS[ns.Prefix] = ns.Namespace;
                    }
                }
                writer.WriteAttributeString("xmlns", "xsi", null, nsXsi);
                writer.WriteProcessingInstruction("xbrl-streamable-instance", "version=\"1.0\" contextBuffer=\"1\"");
                Md5Sum xbrlFactsCheckSum = new Md5Sum();
                writer.WriteProcessingInstruction("xbrl-facts-check", "version=\"0.8\"");

                writer.WriteStartElement("schemaRef", nsLink);
                writer.WriteAttributeString("href", nsXlink, xbrlSchemaRef);
                writer.WriteAttributeString("type", nsXlink, "simple");
                writer.WriteEndElement();

                // filing indicator code IDs
                IEnumerable<FilingIndicator> filingIndicators = _conn.Query<FilingIndicator>(
                    "SELECT mToT.TemplateOrTableCode " +
                    "  FROM dFilingIndicator dFI, mTemplateOrTable mToT " +
                    "  WHERE dFI.InstanceID = ? AND mTot.TemplateOrTableID = dFI.BusinessTemplateID",
                    instance.InstanceID);
                if (filingIndicators.Count() > 0)
                {
                    List<object> _toHash = new List<object>();
                    writeContext(writer, "c", _toHash);
                    Md5Sum fiCntxMd5sum = md5hash(_toHash);
                    writer.WriteStartElement("fIndicators", nsFind);
                    foreach (FilingIndicator r in filingIndicators)
                    {
                        writer.WriteStartElement("filingIndicator", nsFind);
                        writer.WriteAttributeString("contextRef", "c");
                        writer.WriteString(r.TemplateOrTableCode);
                        _toHash.Clear();
                        _toHash.Add(qnFindFilingIndicator);
                        // QNxmlLang if present
                        // xsiNil
                        _toHash.Add(r.TemplateOrTableCode);
                        _toHash.Add(fiCntxMd5sum);
                        xbrlFactsCheckSum += md5hash(_toHash);
                        Debug.Print(string.Format("fil ind {0} {1}", r.TemplateOrTableCode, md5hash(_toHash).ToHex()));
                        writer.WriteEndElement();
                    }
                    _toHash.Clear();
                    _toHash.Add(qnFindFilingIndicators);
                    // QNxmlLang if present
                    // xsiNil
                    xbrlFactsCheckSum += md5hash(_toHash);
                    Debug.Print(string.Format("fil ind {0} {1}", "finds", md5hash(_toHash).ToHex()));
                    writer.WriteEndElement();
                }

                // get typed dimension elements
                IEnumerable<TypedDimensionMemberDefinition> typedDimDefs = _conn.Query<TypedDimensionMemberDefinition>(
                    "SELECT dim.DimensionXBRLCode AS DimensionQname, " +
                    "       owndom.OwnerPrefix || '_typ:' || dom.DomainCode AS TypedMemberQname" +
                    "  FROM mDimension dim" +
                    "       INNER JOIN mDomain dom" +
                    "               ON dom.DomainID = dim.DomainID" +
                    "       INNER JOIN mConcept condom" +
                    "               ON condom.ConceptID = dom.ConceptID" +
                    "       INNER JOIN mOwner owndom" +
                    "               ON owndom.OwnerID = condom.OwnerID" +
                    " WHERE dim.IsTypedDimension = 1");

                foreach (TypedDimensionMemberDefinition typeDimDef in typedDimDefs)
                    typedDimElts[typeDimDef.DimensionQname] = typeDimDef.TypedMemberQname;

                // facts in this instance
                IEnumerable<Fact> factsInInstance = _conn.Query<Fact>(
                    "SELECT DataPointSignature, " +
                    " Unit, Decimals, NumericValue, DateTimeValue, BooleanValue, TextValue " +
                    "FROM dFact WHERE InstanceID = ? " +
                    "ORDER BY substr(DataPointSignature, instr(DataPointSignature,'|') + 1)",
                    instance.InstanceID);

                Dictionary<string,string> cntxTbl = new Dictionary<string,string>(); // cntx key to cntx id
                Dictionary<string,string> unitTbl = new Dictionary<string,string>(); // unit measure to unit id
                Dictionary<string, Md5Sum> cntxMd5tbl = new Dictionary<string, Md5Sum>();
                Dictionary<string, Md5Sum> unitMd5tbl = new Dictionary<string, Md5Sum>();

                foreach (Fact f in factsInInstance)
                {
                    bool isNumeric = false, isBool = false, isDateTime = false, isQName = false, isText = false;
                    string dpSig = f.DataPointSignature;
                    Match dpSigParts = dpSigPattern.Match(dpSig);  // @"MET[(](\w+)[:](\w+)[)][|](.*)$"
                    List<object> _toHash = new List<object>();
                    if (dpSigParts.Groups.Count != 4 || dpSigParts.Groups[2].Value.Length == 0)
                    {
                        logError("sqlDB:InvalidFactConcept",
                                 string.Format("Fact DPM signature key malformed, fact ignored: {0}",
                                               dpSig),
                                 dpSig);
                        continue;
                    }
                    string conceptPrefix = dpSigParts.Groups[1].Value;
                    string conceptLocalName = dpSigParts.Groups[2].Value;
                    if (!xmlnsPrefixNS.ContainsKey(conceptPrefix))
                    {
                        logError("sqlDB:InvalidFactConceptPrefix",
                                 string.Format("Fact DPM metric prefix namespace undevined, fact ignored: {0}",
                                               dpSig),
                                 dpSig);
                        continue;
                    }
                    _toHash.Clear();
                    _toHash.Add(new QName(xmlnsPrefixNS[conceptPrefix], conceptPrefix, conceptLocalName));
                    string dims = dpSigParts.Groups[3].Value;
                    char c = conceptLocalName[0]; // first letter of concept local name
                    if (c == 'm' || c == 'p' || c == 'i')
                        isNumeric = true;
                    else if (c == 'd')
                        isDateTime = true;
                    else if (c == 'b')
                        isBool = true;
                    else if (c == 'e')
                        isQName = true;
                    string v = null;
                    isText = ! (isNumeric || isBool || isDateTime || isQName); // 's' or 'u' type
                    string cntxKey = string.Format("{0}#{1}#{2}", dims, instance.EntityIdentifier, instance.PeriodEndDateOrInstant);
                    string cntxId = null, unitId = null;
                    Md5Sum cntxMd5sum = null, unitMd5sum = null;
                    if (cntxTbl.ContainsKey(cntxKey))
                    {
                        cntxId = cntxTbl[cntxKey];
                        cntxMd5sum = cntxMd5tbl[cntxKey];
                    }
                    else
                    {
                        cntxId = string.Format("c-{0:00}", cntxTbl.Count + 1);
                        cntxTbl[cntxKey] = cntxId;
                        try
                        {
                            List<object> _cHash = new List<object>();
                            writeContext(writer, cntxId, _cHash, dims);
                            cntxMd5sum = md5hash(_cHash);
                            cntxMd5tbl[cntxKey] = cntxMd5sum;
                        }
                        catch (ArgumentNullException)
                        {
                            logError("sqlDB:InvalidFactContextDate",
                                     string.Format("Fact context date null, fact ignored: {0}",
                                                   dpSig),
                                     dpSig);
                            continue;
                        }
                        catch (FormatException)
                        {
                            logError("sqlDB:InvalidFactContextDate",
                                     string.Format("Fact context date invalid, fact ignored: {0}",
                                                   dpSig),
                                     dpSig);
                            continue;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            logError("sqlDB:InvalidFactContextDate",
                                     string.Format("Fact context date null, fact ignored: {0}",
                                                   dpSig),
                                     dpSig);
                            continue;
                        }
                    }
                    _toHash.Add(cntxMd5sum);
                    if (isNumeric && !string.IsNullOrEmpty(f.Unit))
                    {
                        if (unitTbl.ContainsKey(f.Unit))
                        {
                            unitId = unitTbl[f.Unit];
                            unitMd5sum = unitMd5tbl[f.Unit];
                        }
                        else
                        {
                            string[] unitParts = f.Unit.Split(new char[] { ':' });
                            string prefix = "", localName = "";
                            if (unitParts.Length == 2)
                            {
                                prefix = unitParts[0];
                                localName = unitParts[1];
                            }
                            else if (unitParts.Length == 1)
                            {
                                prefix = "";
                                localName = unitParts[0];
                            }
                            if (!xmlnsPrefixNS.ContainsKey(prefix))
                            {
                                logError("sqlDB:InvalidUnitPrefix",
                                         string.Format("Fact's unit {0} has undefined prefix: {1}",
                                                        f.Unit, prefix),
                                         dpSig);
                                continue;
                            }
                            else
                            {
                                unitId = string.Format("u{0}", unitParts[unitParts.Length - 1]);
                                unitTbl[f.Unit] = unitId;
                                writeUnit(writer, unitId, f.Unit);
                                List<object> unitMeasures = new List<object>();
                                // note there's only one multiplicand hash here, otherwise we'd have to handle divisors too
                                unitMeasures.Add(md5hash(new QName(xmlnsPrefixNS[prefix], localName)));
                                unitMd5sum = md5hash(unitMeasures);
                                unitMd5tbl[f.Unit] = unitMd5sum;
                            }
                        }
                        _toHash.Add(unitMd5sum);
                    }
                    if (!xmlnsPrefixNS.ContainsKey(conceptPrefix))
                    {
                        logError("sqlDB:InvalidFactConcept",
                                 string.Format("Fact concept prefix {0} not in taxonomy, fact ignored: {1}",
                                                conceptPrefix, dpSig),
                                 dpSig);
                        continue;
                    }
                    // write fact
                    writer.WriteStartElement(conceptPrefix, conceptLocalName, xmlnsPrefixNS[conceptPrefix]);
                    if (!string.IsNullOrEmpty(cntxId))
                        writer.WriteAttributeString("contextRef", cntxId);
                    if (isNumeric)
                    {
                        if (f.NumericValue.HasValue && !string.IsNullOrEmpty(f.Decimals))
                            writer.WriteAttributeString("decimals", f.Decimals.EndsWith(".0") ? f.Decimals.Substring(0, f.Decimals.Length-2) : f.Decimals);
                        if (!string.IsNullOrEmpty(unitId))
                            writer.WriteAttributeString("unitRef", unitId);
                    }
                    if (isBool)
                    {
                        if (f.BooleanValue.HasValue)
                        {
                            v = f.BooleanValue.Value ? "true" : "false";
                            writer.WriteString(v);
                        }
                        else
                            writer.WriteAttributeString("nil", nsXsi, "true");
                    }
                    else if (isDateTime)
                    {
                        if (f.DateTimeValue != null)
                        {
                            v = f.DateTimeValue;
                            writer.WriteString(v);
                        }
                        else
                            writer.WriteAttributeString("nil", nsXsi, "true");
                    }
                    else if (isNumeric)
                    {
                        if (f.NumericValue.HasValue)
                        {
                            if (c == 'm')
                            {   // pad to .00 2 decimal places
                                v = (f.NumericValue.Value + 1.00m - 1m).ToString(CultureInfo.InvariantCulture); 
                            }
                            else if (c == 'p')
                            {   // pad to .0000 4 decimal places
                                v = (f.NumericValue.Value + 1.0000m - 1m).ToString(CultureInfo.InvariantCulture);
                            }
                            else if (c == 'i')
                            {
                                v = Decimal.Floor(f.NumericValue.Value).ToString(CultureInfo.InvariantCulture);
                            }
                            else
                            {
                                v = f.NumericValue.Value.ToString(CultureInfo.InvariantCulture);
                            }
                            writer.WriteString(v);
                        }
                        else
                            writer.WriteAttributeString("nil", nsXsi, "true");
                    } 
                    else // text
                    {
                        if (f.TextValue != null)
                        {
                            v = f.TextValue;
                            writer.WriteString(v);
                        }
                        else
                            writer.WriteAttributeString("nil", nsXsi, "true");
                    }
                    // _toHash.Add(null); // xmlLang
                    if (v == null)
                    {
                        _toHash.Add(qnXsiNil);
                        _toHash.Add("true");
                    }
                    else
                        _toHash.Add(v);
                    xbrlFactsCheckSum += md5hash(_toHash);
                    //Debug.WriteLine(string.Format("f {0} v {1} c {2} u {3} f {4} s {5}",
                    //    conceptLocalName, v, cntxMd5sum.ToHex(), unitMd5sum.ToHex(),  md5hash(_toHash).ToHex(), xbrlFactsCheckSum.ToHex()));

                    writer.WriteEndElement();
                }

                writer.WriteProcessingInstruction("xbrl-facts-check", 
                    string.Format("sum-of-fact-md5s=\"{0}\"", xbrlFactsCheckSum.ToHex()));
                writer.WriteEndDocument();
                writer.Close();

                string _msg = string.Format("Finished saving XBRL instance {0} at {1}", xbrlFileName, DateTime.Now);
                logInfo("info", _msg);
                Debug.WriteLine(_msg);
            }
            catch (Exception ex)
            {
                if (this._conn != null)
                {
                    try
                    {
                        this._conn.Close();
                        this._conn.Dispose();
                    }
                    catch (Exception)
                    {
                    }
                }
                logError("t4u:csSaverException",
                         string.Format("Exception saving XBRL instance: {0}", ex.Message));
            }
            this.closeLog();
            return this.logToString();
        }

    }
}
