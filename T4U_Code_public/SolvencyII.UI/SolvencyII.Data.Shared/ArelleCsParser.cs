/*
 * Arelle streaming loader in C# with no DTS validation
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
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using SolvencyII.Data.SQLite;
using SolvencyII.Domain.Configuration;

namespace SolvencyII.Data.Shared
{
    class ModelXmlElement
    {
        public string id, value = "";
        public QName tag;
        public Dictionary<QName, ModelXmlAttribute> attributes = new Dictionary<QName, ModelXmlAttribute>();
        public ModelXmlElement parentElement;

        static QName qnXmlLang = new QName("http://www.w3.org/XML/1998/namespace", "xml", "lang");
        static QName qnXmlBase = new QName("http://www.w3.org/XML/1998/namespace", "xml", "base");

        public ModelXmlElement(XmlReader reader, ModelXmlElement parentElement = null)
        {
            this.parentElement = parentElement;
            tag = new QName(reader.NamespaceURI, reader.Prefix, reader.LocalName);
            if (reader.HasAttributes)
            {
                while (reader.MoveToNextAttribute())
                {
                    ModelXmlAttribute attr = new ModelXmlAttribute(reader);
                    this.attributes[attr.tag] = attr;
                    if (attr.tag.prefixedName == "id")
                        this.id = attr.value;
                }
                reader.MoveToElement();
            }
            if (reader.HasValue)
            {
                this.value = reader.Value;
            }
        }

        public string xmlLang
        {
            get
            {
                if (this.attributes.ContainsKey(qnXmlLang))
                    return this.attributes[qnXmlLang].value;
                string ancestorLang = null;
                if (this.parentElement != null)
                    ancestorLang = parentElement.xmlLang;
                return ancestorLang;
            }
        }

        public string xmlBase
        {
            get
            {
                string _thisBase = "";
                if (this.attributes.ContainsKey(qnXmlBase))
                {
                    _thisBase = this.attributes[qnXmlBase].value;
                    if (_thisBase.StartsWith("http:") || _thisBase.StartsWith("/"))
                        return _thisBase;
                }
                string _ancestorBase = "";
                if (this.parentElement != null)
                    _ancestorBase = parentElement.xmlBase;
                return _ancestorBase + _thisBase;
            }
        }

        public string basedValue(string _value)
        {
            string _base = this.xmlBase;
            return _base + _value;
        }

    }

    class ModelAttributedXmlObject
    {
        public string name;
        public Dictionary<string, string> attributes = new Dictionary<string, string>();

        public ModelAttributedXmlObject(XmlReader reader)
        {
            name = reader.Name;
            XmlReader attrRdr = XmlReader.Create(new MemoryStream(UTF8Encoding.UTF8.GetBytes(
                string.Format("<{0} {1}/>", reader.Name, reader.Value))));
            attrRdr.Read();
            if (attrRdr.HasAttributes)
            {
                while (attrRdr.MoveToNextAttribute())
                {
                    this.attributes[attrRdr.Name] = attrRdr.Value;
                }
                attrRdr.MoveToElement();
            }
            attrRdr.Close();
        }
    }

    class ModelXmlDeclaration : ModelAttributedXmlObject
    {
        public ModelXmlDeclaration(XmlReader reader): base(reader)
        {
        }
    }

    class ModelXmlProcessingInstruction : ModelAttributedXmlObject
    {
        public ModelXmlProcessingInstruction(XmlReader reader): base(reader)
        {
        }
    }

    class ModelXmlAttribute
    {
        public string value;
        public QName tag;

        public ModelXmlAttribute(XmlReader reader)
        {
            tag = new QName(reader.NamespaceURI, reader.Prefix, reader.LocalName);
            value = reader.Value;
        }
    }

    class ModelXbrl : ModelXmlElement
    {
        public Dictionary<string,ModelContext> contexts = new Dictionary<string,ModelContext>();
        public List<ModelFact> facts = new List<ModelFact>();

        public ModelXbrl(XmlReader reader, ModelXmlElement parentElement = null): base(reader, parentElement)
        {
        }
    }

    class ModelContext : ModelXmlElement
    {
        public DateTime startDate, endDate, instantDate;
        public string entityScheme, entityIdentifier;
        public Boolean isForever, isInstant, isStartEnd;
        public List<ModelDimension> dimensions = new List<ModelDimension>();
        public Md5Sum md5sum = null;

        public ModelContext(XmlReader reader): base(reader)
        {
        }
    }

    class ModelDimension : ModelXmlElement
    {
        static QName qnDimension = new QName("", "", "dimension");
        public Boolean isTyped;
        public QName dimensionName, memberName; // memberName is also the qname for typed content
        public string typedValue, typedContent; // typedContent is used for hash computation (assuyming only 1 typed member per typed dim
        public Md5Sum md5sum = null;

        public ModelDimension(XmlReader reader): base(reader)
        {
            this.isTyped = reader.LocalName == "typedMember";
            this.dimensionName = new QName(reader, this.attributes[qnDimension].value);
        }
    }

    class ModelUnit : ModelXmlElement
    {
        public List<QName> multMeasures = new List<QName>();
        public List<QName> divMeasures = new List<QName>();
        public Md5Sum md5sum = null;

        public ModelUnit(XmlReader reader): base(reader)
        {
        }
    }

    class ModelFact : ModelXmlElement
    {
        public string contextRef, unitRef, decimals;
        public bool isNil = false;
        public List<ModelFact> tupleFacts = new List<ModelFact>();
        public ModelContext context = null;
        public ModelUnit unit = null;

        public ModelFact(XmlReader reader, ModelXmlElement parentElement = null): base(reader, parentElement)
        {
        }
    }

    public class ArelleCsParser : ArelleCsShared
    {

        public const long UNINITIALIZED = Int64.MinValue;

        static Regex decimalPattern = new Regex(@"\s*^[+-]?([0-9]+(\.[0-9]*)?|\.[0-9]+)\s*$");
        static Regex integerPattern = new Regex(@"^\s*[+-]?([0-9]+)\s*$");
        static Regex floatPattern = new Regex(@"^\s*(\+|-)?([0-9]+(\.[0-9]*)?|\.[0-9]+)([Ee](\+|-)?[0-9]+)?\s*$|^\s*(\+|-)?INF\s*$|^\s*NaN\s*$");
        static Regex datePattern = new Regex(@"^\s*([0-9]{4})-([0-9]{2})-([0-9]{2})\s*$");
        static Regex dateTimePattern = new Regex(@"^\s*([0-9]{4})-([0-9]{2})-([0-9]{2})[T ]([0-9]{2}):([0-9]{2}):([0-9]{2})\s*$|^\s*([0-9]{4})-([0-9]{2})-([0-9]{2})\s*$");
        static Regex boolPattern = new Regex(@"\s*(true|1|false|0)\s*");
        static Regex decimalsPattern = new Regex(@"^\s*([+-]?[0-9]+|INF)$\s*");
        static Regex sigDimPattern = new Regex(@"([^(]+)[(]([^\[)]*)(\[([0-9;]+)\])?[)]");
        static Regex isoCurrencyPattern = new Regex(@"^[A-Z]{3}$");
        static Regex logMessagePattern = new Regex(@"^\[([^\]]+)\]\s(.*)$");
        static Regex schemaRefDatePattern = new Regex(@".*/([0-9]{4}-[01][0-9]-[0-3][0-9])/mod.*");



        public string[] xmlBoolTrueValues = { "true", "1" };
        public string[] leiEntitySchemes = { "http://standard.iso.org/iso/17442", "LEI", };

        bool isStreamingMode = false;
        string _sqlConnectionPath;
        SQLiteConnection _conn;
        string xbrlFilePath = null, xbrlFileName = null;
        long factCount = 0, itemCount = 0;

        long moduleId = UNINITIALIZED,
            instanceId = UNINITIALIZED;

        public bool isEIOPAfullVersion = false; // 2.0 filer manual "full" version

        string entityScheme, entityIdentifier = null;
        DateTime periodInstantDate = DateTime.MinValue;
        string entityCurrency = null;

        Dictionary<string, bool> dFilingIndicators = new Dictionary<string, bool>();
        HashSet<long> tableIDs = new HashSet<long>();
        Dictionary<string,HashSet<long>> metricAndDimensionsTableId = new Dictionary<string,HashSet<long>>();
        HashSet<string>templateOrTableCodes = new HashSet<string>();

        Dictionary<string, string> dpmNsPrefixes = new Dictionary<string, string>();
        HashSet<string> unusedXmlnsPrefixes = new HashSet<string>();
        HashSet<string> unusedContextIDs = new HashSet<string>();
        HashSet<string> invalidLEIs = new HashSet<string>();
        Dictionary<Md5Sum, string> cntxHashIDs = new Dictionary<Md5Sum, string>();
        HashSet<string> unusedUnitIDs = new HashSet<string>();
        Dictionary<Md5Sum, string> unitHashIDs = new Dictionary<Md5Sum, string>();

        string factsCheckVersion = null;
        Md5Sum factsCheckMd5s = null;

        bool hasFootnotes = false;

        // for status bar of main form
        BackgroundWorker asyncWorker;
        Dictionary<string, string> scheamRefSubstitutions;

        /*
        private class AvailableTableRowsKey 
        {
            public long tableID;
            public string zDimKey;
            public AvailableTableRowsKey(long tableID, string zDimKey)
            {
                this.tableID = tableID;
                this.zDimKey = zDimKey;
            }
            public override bool Equals(object obj)
            {
                return this.Equals(obj as AvailableTableRowsKey);
            }

            public bool Equals(AvailableTableRowsKey other)
            {
                if (Object.ReferenceEquals(other, null))
                {
                    return false;
                }
                if (Object.ReferenceEquals(this, other))
                {
                    return true;
                }
                if (this.GetType() != other.GetType())
                    return false;
                return (this.tableID == other.tableID) && (this.zDimKey == other.zDimKey);
            }

            public override int GetHashCode()
            {
                if (string.IsNullOrEmpty(this.zDimKey))
                    return unchecked((int)this.tableID);
                return unchecked((int)this.tableID) * 0x00010000 + this.zDimKey.GetHashCode();
            }

            public static bool operator ==(AvailableTableRowsKey lhs, AvailableTableRowsKey rhs)
            {
                if (Object.ReferenceEquals(lhs, null))
                {
                    if (Object.ReferenceEquals(rhs, null))
                    {
                        return true;
                    }
                    return false;
                }
                return lhs.Equals(rhs);
            }

            public static bool operator !=(AvailableTableRowsKey lhs, AvailableTableRowsKey rhs)
            {
                return !(lhs == rhs);
            }
        }
        Dictionary<AvailableTableRowsKey,HashSet<string>> availableTableRows = new Dictionary<AvailableTableRowsKey,HashSet<string>>();
        Dictionary<long,Dictionary<string,string>> yDimVal = new Dictionary<long,Dictionary<string,string>>();
        Dictionary<long,Dictionary<string,string>> zDimVal = new Dictionary<long,Dictionary<string,string>>();
        */

        public ArelleCsParser(string dbPath = null, Dictionary<string, string> scheamRefSubstitutions=null)
        {
            if (dbPath == null)
                this._sqlConnectionPath = StaticSettings.ConnectionString;
            else
                this._sqlConnectionPath = dbPath;
            this.scheamRefSubstitutions = scheamRefSubstitutions;
        }
        public ArelleCsParser(long specificInstanceID) : this()
        {
            instanceId = specificInstanceID;
        }

        static ModelXmlElement elementFactory(XmlReader reader, List<ModelXmlElement> elementStack)
        {
            ModelXmlElement parentElement = (elementStack.Count > 0) ? elementStack[elementStack.Count - 1] : null;
            switch (reader.NamespaceURI)
            {
                case nsXbrli:
                switch (reader.LocalName)
                {
                    case "xbrl":
                        return new ModelXbrl(reader, parentElement);
                    case "context":
                        return new ModelContext(reader);
                    case "unit":
                        return new ModelUnit(reader);
                    default:
                        return new ModelXmlElement(reader);
                }
                case nsLink:
                switch (reader.LocalName)
                {
                    default:
                        return new ModelXmlElement(reader);
                }
                case nsXbrldi:
                switch (reader.LocalName)
                {
                    case "explicitMember":
                    case "typedMember":
                        return new ModelDimension(reader);
                    default:
                        return new ModelXmlElement(reader);
                }
                default:
                for (int i = elementStack.Count() - 1; i >= 1; i--)
                {
                    ModelXmlElement ancestorElt = elementStack[i];
                    if (!(ancestorElt is ModelFact))
                        return new ModelXmlElement(reader); // prevent context/footnote descendant from being a ModelFact
                }
                    return new ModelFact(reader, parentElement);
            }
        }

        DateTime xbrlDateUnionValue(string source, bool addOneDayIfNoTime = false)
        {
            DateTime result;
            if (source.EndsWith("T24:00:00") || (addOneDayIfNoTime && source.Length == 10))
            {
                result = XmlConvert.ToDateTime(source.Substring(0, 10), XmlDateTimeSerializationMode.RoundtripKind).AddDays(1);
            }
            else
            {
                result = XmlConvert.ToDateTime(source.Substring(0, 10), XmlDateTimeSerializationMode.RoundtripKind);
            }
            return result;
        }

        string dateUnionToString(DateTime dateUnionValue, bool isEndInstant = false)
        {
            if (dateUnionValue.Hour != 0 || dateUnionValue.Minute != 0 || dateUnionValue.Second != 0 || dateUnionValue.Millisecond != 0)
                return dateUnionValue.ToString("yyyy-MM-ddTHH:mm:ss");
            // otherwise want a date result with no time portion
            if (isEndInstant && dateUnionValue != DateTime.MinValue)
                dateUnionValue -= new TimeSpan(1, 0, 0, 0);
            return dateUnionValue.ToString("yyyy-MM-dd");
        }

        public string stringJoin(IEnumerable<string> args, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in args)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(s);
            }
            return sb.ToString();
        }

        public string stringJoin(IEnumerable<long> args, string separator)
        {
            StringBuilder sb = new StringBuilder();
            foreach (long i in args)
            {
                if (sb.Length > 0) sb.Append(separator);
                sb.Append(i);
            }
            return sb.ToString();
        }

        public string stringJoinQuoted(IEnumerable<string> args, string separator, bool quoteForSQL=false)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in args)
            {
                if (sb.Length > 0) sb.Append(separator);
                if (quoteForSQL) // use Apostrophe instead of double quote
                    sb.Append("'").Append(s.Replace("'", "''")).Append("'");
                else
                    sb.Append("\"").Append(s).Append("\"");
            }
            return sb.ToString();
        }

        public object parseXbrl(string xbrlFilePath, BackgroundWorker asyncWorker = null)
        {
            this.asyncWorker = asyncWorker;
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            List<ModelXmlElement> elementStack = new List<ModelXmlElement>();
            int contextBuffer = Int32.MaxValue, unitBuffer = Int32.MaxValue;
            ModelXmlElement elt = null, parentElt = null;
            List<ModelContext> contexts = new List<ModelContext>();
            Dictionary<string, ModelContext> contextById = new Dictionary<string, ModelContext>();
            List<ModelUnit> units = new List<ModelUnit>();
            Dictionary<string, ModelUnit> unitById = new Dictionary<string, ModelUnit>();
            List<ModelFact> facts = new List<ModelFact>();
            string schemaRef = null;
            ModelContext cntx;
            ModelUnit unit;
            ModelFact fact;
            DateTime dateValue;

            this.isEBA = false;
            this.isEIOPA = true;

            this.initializeLog();

            this.xbrlFilePath = xbrlFilePath; // for error reporting in class methods
            xbrlFileName = Path.GetFileName(xbrlFilePath);
            if (!xbrlFileName.EndsWith(".xbrl"))
                logError("EBA.1.1|EIOPA.S.1.1.a", 
                         string.Format("XBRL instance documents SHOULD use the extension \".xbrl\" but it is {0}",
                                       xbrlFileName));
             XmlReader reader = null;
            try
            {
                if (asyncWorker != null)
                    asyncWorker.ReportProgress(0, "Connecting to database to store instance " + this._sqlConnectionPath);
                this._conn = new SQLiteConnection(this._sqlConnectionPath);
                this._conn.BeginTransaction();
                // move to processSchemaRef after moduleId known: loadDimensionsAndEnumerations();
                setupBeforeInstance();

                reader = XmlReader.Create(xbrlFilePath, settings);
                while (reader.Read())
                {
                    if (asyncWorker != null && asyncWorker.CancellationPending) break; // Added by NAJ 29/07/2014
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                        case XmlNodeType.EndElement:
                            bool isStartOrEmptyElement = reader.NodeType == XmlNodeType.Element;
                            if (isStartOrEmptyElement)
                            {
                                elt = elementFactory(reader, elementStack);
                                if (reader.Depth == 0)
                                {
                                    if (elt.tag != qnXbrliXbrl)
                                    {
                                        throw new ApplicationException(string.Format(
                                            "File does not appear to be an instance, document element {0} unexpected in file {1}",
                                            elt.tag.prefixedName, xbrlFileName));
                                    }
                                    foreach (ModelXmlAttribute _attr in elt.attributes.Values)
                                        if (_attr.tag.prefix == "xmlns")
                                        {
                                            unusedXmlnsPrefixes.Add(_attr.tag.localName);
                                            if (dpmNsPrefixes.ContainsKey(_attr.value) &&
                                                dpmNsPrefixes[_attr.value] != _attr.tag.localName &&
                                                !(_attr.value == nsXbrli && _attr.tag.localName == "xbrli")) // xbrli is alternate for xmlns default prefix
                                                logWarning("EBA.3.5|EIOPA.3.5",
                                                           string.Format("Prefix for namespace {0} is {1} but {2} was declared by xmlns in instance.",
                                                                         _attr.value, dpmNsPrefixes[_attr.value], _attr.tag.localName));
                                        }
                                }
                                else // not xbrli:xbrl
                                {
                                    if (unusedXmlnsPrefixes.Contains(elt.tag.prefix))
                                        unusedXmlnsPrefixes.Remove(elt.tag.prefix);
                                    foreach (ModelXmlAttribute _attr in elt.attributes.Values)
                                    {
                                        if (unusedXmlnsPrefixes.Contains(_attr.tag.prefix))
                                            unusedXmlnsPrefixes.Remove(_attr.tag.prefix);
                                    }
                                }
                                elementStack.Add(elt); // push current element

                                if (!reader.IsEmptyElement) // an empty StartElement is processed as if it were an EndElement
                                    break;
                            }
                            elt = elementStack[elementStack.Count - 1];
                            parentElt = elementStack.Count() >= 2 ? elementStack[elementStack.Count - 2] : null;
                            if (elt.attributes.ContainsKey(qnXmlBase))
                                logError("EBA.2.1|EIOPA.2.1",
                                         string.Format("element {0} has disallowed base attribute: {1}",
                                                       elt.tag, elt.attributes[qnXmlBase].value));
                            if (elt is ModelContext)
                            {
                                cntx = elt as ModelContext;
                                contexts.Add(cntx);
                                contextById[elt.id] = cntx;
                                processContext(cntx);
                                if (isStreamingMode && contexts.Count() > contextBuffer)
                                {
                                    contextById.Remove(contexts[0].id);
                                    contexts.RemoveAt(0);
                                }
                            }
                            else if (elt is ModelDimension)
                            {
                                ModelDimension dim = elt as ModelDimension;
                                if (!dim.isTyped)
                                {
                                    dim.memberName = new QName(reader, elt.value);
                                    dim.md5sum = md5hash(dim.dimensionName, dim.memberName);
                                }
                                else
                                {
                                    dim.md5sum = md5hash(dim.dimensionName, dim.typedContent);
                                }
                                cntx = elementStack[1] as ModelContext;
                                cntx.dimensions.Add(dim);
                            }
                            else if (parentElt is ModelDimension && (parentElt as ModelDimension).isTyped)
                            {
                                ModelDimension dim = parentElt as ModelDimension;
                                if (elt.attributes.ContainsKey(qnXsiNil) && xmlBoolTrueValues.Contains(elt.attributes[qnXsiNil].value))
                                {
                                    dim.typedValue = string.Format("<{0}/>", canonicalQname(elt.tag));  // was: "<{0} xsi:nil=''true''/>"
                                    dim.typedContent = "";
                                }
                                else // this is only good for non-complex typed dimensions
                                {
                                    dim.typedValue = string.Format("<{0}>{1}</{0}>", canonicalQname(elt.tag), elt.value);
                                    dim.typedContent = elt.value; // for hash computation
                                }
                                dim.memberName = elt.tag;
                            }
                            else if (elt is ModelUnit)
                            {
                                unit = elt as ModelUnit;
                                units.Add(unit);
                                unitById[elt.id] = unit;
                                processUnit(unit);
                                if (isStreamingMode && units.Count() > unitBuffer)
                                {
                                    unitById.Remove(units[0].id);
                                    units.RemoveAt(0);
                                }
                            }
                            else if (elt.tag.namespaceURI == nsXbrli)
                                switch (elt.tag.localName)
                                {
                                    case "identifier":
                                        cntx = elementStack[1] as ModelContext;
                                        cntx.entityScheme = elt.attributes[qnScheme].value;
                                        cntx.entityIdentifier = elt.value;
                                        break;
                                    case "instant":
                                        cntx = elementStack[1] as ModelContext;
                                        cntx.isInstant = true;
                                        if (!dateTimePattern.IsMatch(elt.value) || !DateTime.TryParse(elt.value, out dateValue))
                                            logError("xmlSchema:valueError",
                                                     string.Format("Context instant date lexical error: context {0} instant date {1}",
                                                                   cntx.id, elt.value));
                                        else
                                        {
                                            cntx.instantDate = xbrlDateUnionValue(elt.value, true);
                                            if (!datePattern.IsMatch(elt.value))
                                                logError("EBA.2.10|EIOPA.2.10",
                                                         string.Format("Period dates must be whole dates without time or timezone: {0}",
                                                                        elt.value));
                                        }
                                        break;
                                    case "endDate":
                                        cntx = elementStack[1] as ModelContext;
                                        cntx.isStartEnd = true;
                                        if (!dateTimePattern.IsMatch(elt.value) || !DateTime.TryParse(elt.value, out dateValue))
                                            logError("xmlSchema:valueError",
                                                     string.Format("Context end date lexical error: context {0} end date {1}",
                                                                   cntx.id, elt.value));
                                        else
                                        { 
                                            cntx.endDate = xbrlDateUnionValue(elt.value, true);
                                            if (!datePattern.IsMatch(elt.value))
                                                logError("EBA.2.10|EIOPA.2.10",
                                                         string.Format("Period dates must be whole dates without time or timezone: {0}",
                                                                        elt.value));
                                        }
                                        break;
                                    case "startDate":
                                        cntx = elementStack[1] as ModelContext;
                                        if (!dateTimePattern.IsMatch(elt.value) || !DateTime.TryParse(elt.value, out dateValue))
                                            logError("xmlSchema:valueError",
                                                     string.Format("Context start date lexical error: context {0} start date {1}",
                                                                   cntx.id, elt.value));
                                        else
                                        { 
                                            cntx.startDate = xbrlDateUnionValue(elt.value);
                                            if (!datePattern.IsMatch(elt.value))
                                                logError("EBA.2.10|EIOPA.2.10",
                                                         string.Format("Period dates must be whole dates without time or timezone: {0}",
                                                                        elt.value));
                                        }
                                        break;
                                    case "forever":
                                        cntx = elementStack[1] as ModelContext;
                                        cntx.isForever = true;
                                        break;
                                    case "measure":
                                        QName qnMeasure = new QName(reader, elt.value);
                                        if (parentElt.tag == qnXbrliUnitNumerator)
                                        {
                                            unit = elementStack[elementStack.Count - 4] as ModelUnit;
                                            if (unit != null)
                                                unit.multMeasures.Add(qnMeasure);
                                        }
                                        else if (parentElt.tag == qnXbrliUnitDenominator)
                                        {
                                            unit = elementStack[elementStack.Count - 4] as ModelUnit;
                                            if (unit != null)
                                                unit.divMeasures.Add(qnMeasure);
                                        }
                                        else
                                        {
                                            unit = parentElt as ModelUnit;
                                            unit.multMeasures.Add(qnMeasure);
                                        }
                                        break;
                                    case "xbrl": // end of xbrl root element
                                        if (!isStreamingMode)
                                        {
                                            // resolve unit and context references, process remaining unprocessed facts
                                            foreach (ModelFact f in facts)
                                            {
                                                if (!string.IsNullOrEmpty(f.contextRef) && f.context == null && contextById.ContainsKey(f.contextRef))
                                                    f.context = contextById[f.contextRef];
                                                if (!string.IsNullOrEmpty(f.unitRef) && f.unit == null && unitById.ContainsKey(f.unitRef))
                                                    f.unit = unitById[f.unitRef];
                                                processFact(reader, f);
                                            }
                                        }
                                        facts.Clear();  // dereference all facts
                                        contexts.Clear();
                                        units.Clear();
                                        contextById.Clear();
                                        unitById.Clear();
                                        finishInstance(); // final processing of instance
                                        break;
                                    case "segment":
                                        cntx = elementStack[1] as ModelContext;
                                        logError("EBA.2.14|EIOPA.N.2.14",
                                                 string.Format("The segment element not allowed in context Id: {0}",
                                                                cntx.id));
                                        break;
                                                                
                                }
                            else if (elt.tag.namespaceURI == nsLink && elt.tag.localName == "schemaRef")
                            {
                                schemaRef = elt.basedValue(elt.attributes[qnXlinkHref].value);
                                processSchemaRef(schemaRef);
                            }
                            else if (elt.tag.namespaceURI == nsLink && elt.tag.localName == "linkbaseRef")
                            {
                                string linkbaseRef = elt.basedValue(elt.attributes[qnXlinkHref].value);
                                processLinkbaseRef(linkbaseRef);
                            }
                            else if (elt.tag.namespaceURI == nsLink && elt.tag.localName == "loc")
                            {
                                // only tests if there is a footnote loc (for a fact) but doesn't accumulate fact id's or loc id's due to possible memory consumption
                                hasFootnotes = true;
                            }
                            else if (elt is ModelFact)
                            {   // note that fact may be nil (no EndElement event)
                                fact = elt as ModelFact;
                                if (fact.tupleFacts.Count() == 0) // not a tuple
                                {
                                    if (elt.attributes.ContainsKey(qnXsiNil) && xmlBoolTrueValues.Contains(elt.attributes[qnXsiNil].value))
                                        fact.isNil = true;
                                    if (fact.attributes.ContainsKey(qnContextRef))
                                        fact.contextRef = fact.attributes[qnContextRef].value;
                                    if (fact.attributes.ContainsKey(qnUnitRef))
                                        fact.unitRef = fact.attributes[qnUnitRef].value;
                                    if (fact.attributes.ContainsKey(qnDecimals))
                                        fact.decimals = fact.attributes[qnDecimals].value;
                                    if (!string.IsNullOrEmpty(fact.contextRef) && contextById.ContainsKey(fact.contextRef))
                                        fact.context = contextById[fact.contextRef];
                                    if (!string.IsNullOrEmpty(fact.unitRef) && unitById.ContainsKey(fact.unitRef))
                                        fact.unit = unitById[fact.unitRef];
                                }
                                if (reader.Depth == 1)
                                {
                                    if (isStreamingMode && instanceId != UNINITIALIZED)
                                    {
                                        processFact(reader, fact);
                                    }
                                    else
                                    {
                                        if (instanceId != UNINITIALIZED && 
                                            (string.IsNullOrEmpty(fact.contextRef) || contextById.ContainsKey(fact.contextRef)) &&
                                            (string.IsNullOrEmpty(fact.unitRef) || unitById.ContainsKey(fact.unitRef)))
                                        {   // fact context and unit are resolved, process fact
                                            processFact(reader, fact);
                                        }
                                        else // defer fact for later processing
                                        {
                                            facts.Add(elt as ModelFact);
                                        }
                                    }
                                }
                                else if (elementStack[1] is ModelFact)
                                {
                                    (elementStack[1] as ModelFact).tupleFacts.Add(elt as ModelFact);
                                }
                            }
                            else if (elt is ModelXmlElement)
                            {
                                ModelXmlElement parent = elementStack[elementStack.Count - 1];
                                if (parent.tag.namespaceURI == "" && parent.tag.localName == "segment"
                                    && elt.tag.localName != "explicitMember" && elt.tag.localName != "typedMember")
                                {
                                    cntx = elementStack[1] as ModelContext;
                                    logError(this.isEIOPAfullVersion ? "EBA.2.15|EIOPA.S.2.15" : "EBA.2.15|EIOPA.N.2.15",
                                             string.Format("Scenario of context Id {0} has disallowed content: {1}",
                                                           cntx.id, elt.tag.prefixedName));
                                }
                            }
                            elementStack.RemoveAt(elementStack.Count - 1); // pop last element
                            elt = parentElt = null;
                            break;
                        case XmlNodeType.Text:
                        case XmlNodeType.CDATA:
                            elementStack[elementStack.Count - 1].value += reader.Value;  // text may be between comments and as tail of nested elements
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            if (reader.Name == "xbrl-streamable-instance" && !isStreamingMode)
                            {
                                ModelXmlProcessingInstruction pi = new ModelXmlProcessingInstruction(reader);
                                try
                                {
                                    if (pi.attributes.Keys.Contains("contextBuffer"))
                                        contextBuffer = Convert.ToInt32(pi.attributes["contextBuffer"]);
                                    if (pi.attributes.Keys.Contains("unitBuffer"))
                                        unitBuffer = Convert.ToInt32(pi.attributes["unitBuffer"]);
                                    isStreamingMode = true;
                                }
                                catch (FormatException ex)
                                {
                                    logError("xmlSchema:valueError",
                                             string.Format("Error in processing instruction xbrl-streamable-instance {1}", 
                                             ex.Message));
                                }
                            }
                            else if (reader.Name == "xbrl-facts-check")
                            {
                                ModelXmlProcessingInstruction pi = new ModelXmlProcessingInstruction(reader);
                                if (pi.attributes.Keys.Contains("version"))
                                {
                                    factsCheckVersion = pi.attributes["version"];
                                    factsCheckMd5s = new Md5Sum();
                                }
                                else if (pi.attributes.Keys.Contains("sum-of-fact-md5s"))
                                {
                                    try
                                    {
                                        Md5Sum expectedMd5 = new Md5Sum(pi.attributes["sum-of-fact-md5s"]);
                                        if (factsCheckMd5s != expectedMd5)
                                            logWarning("t4u:xbrlFactsCheckWarning",
                                                    string.Format("Xbrl facts sum of md5s expected {0} not matched to actual sum {1}",
                                                                  expectedMd5.ToHex(), factsCheckMd5s.ToHex()));
                                        else
                                            logInfo("info", "Successful XBRL facts sum of md5s.");
                                    }
                                    catch (FormatException)
                                    {
                                        logError("t4u:xbrlFactsCheckError",
                                                 string.Format("Invalid sum-of-md5s {0}",
                                                 pi.attributes["sum-of-fact-md5s"]));
                                    }
                                }
                            }
                            break;
                        case XmlNodeType.Comment:
                            break;
                        case XmlNodeType.XmlDeclaration:
                            ModelXmlDeclaration decl = new ModelXmlDeclaration(reader);
                            string encoding = "(none)";
                            if (decl.attributes.ContainsKey("encoding"))
                                encoding = decl.attributes["encoding"];
                            if (encoding.ToLower() != "utf-8")
                                logError("EBA.1.4|EIOPA.1.4",
                                         string.Format("XBRL instance documents MUST use \"UTF-8\" encoding but is \"{0}\"",
                                                       encoding));
                            break;
                        case XmlNodeType.Document:
                            break;
                        case XmlNodeType.DocumentType:
                            break;
                        case XmlNodeType.EntityReference:
                            break;
                    }
                }
                string _msg = "XBRL instance loaded " + xbrlFileName;
                logInfo("info", _msg);
                if (asyncWorker != null)
                    asyncWorker.ReportProgress(0, _msg);
                Debug.WriteLine(_msg);
            }
            catch (Exception ex)
            {
                string _msg = "Handling exception " + ex.GetType().Name;
                if (asyncWorker != null)
                    asyncWorker.ReportProgress(0, _msg);
                Debug.WriteLine(_msg);
                _msg = ex.Message;
                if (ex is XmlException)
                    logError("xmlSchema:malformedXML",
                         string.Format("XML Exception Parsing XBRL instance: {0}", _msg));
                else 
                {
                    // application messages can be of format "[code] messagetext"
                    Match _msgMatch = logMessagePattern.Match(_msg);
                    if (_msgMatch.Success)
                        logError(_msgMatch.Groups[1].Value, _msgMatch.Groups[2].Value);
                    else
                        logError("t4u:csParserException",
                             string.Format("Exception loading XBRL instance: {0}", _msg));
                }

                // attempt to close and roll back any SQLite table operations
                // (this has to be done before any higher level handlers attempt to process an exception)
                if (this._conn != null)
                {
                    try
                    {
                        this._conn.Rollback();
                        this._conn.Close();
                        this._conn.Dispose();
                    }
                    catch (Exception ex2)
                    {
                        logError("t4u:csParserException",
                                 string.Format("Exception rolling back or closing database after handling exception: {0}",
                                               ex2.Message));
                    }
                }
                if (reader != null)
                {
                    try
                    {
                        reader.Close();
                    }
                    catch (Exception ex3)
                    {
                        logError("t4u:csParserException",
                                 string.Format("Exception closing xml reader after handling exception: {0}",
                                 ex3.Message));
                    }
                }
                // for exceptions that do not relate to XML parsing, re-throw exception
                if (asyncWorker != null)
                    asyncWorker.ReportProgress(0, "");
                if (!(ex is XmlException || ex is ApplicationException))
                    throw;
            }
            if (asyncWorker != null)
                asyncWorker.ReportProgress(0, "");
            this.closeLog();
            return this.logToString();
        }

        private class IdResult
        {
            public long id { get; set; }
        }

        private class StrResult
        {
            public string str { get; set; }
        }

        private class QnValResult
        {
            public string qn { get; set; }
            public string val { get; set; }
        }

        private class QnIntResult
        {
            public string qn { get; set; }
            public int val { get; set; }
        }

        private class QnValIntResult
        {
            public string qn { get; set; }
            public string val { get; set; }
            public int val2 { get; set; }
        }

        void processSchemaRef(string schemaFile)
        {
            if (this.isEIOPA && this.moduleId == UNINITIALIZED)
            {
                // determine if 2.0 EIOPA (date > 2015-02-28)
                Match _schemaFileDateMatch = schemaRefDatePattern.Match(schemaFile);
                if (_schemaFileDateMatch.Success)
                {
                    String _date = _schemaFileDateMatch.Groups[1].Value;
                    this.isEIOPAfullVersion = _date.CompareTo("2015-02-28") > 0;
                }
            }
            if (!schemaFile.StartsWith("http://"))
                logError(isEIOPAfullVersion ? "EBA.2.2|EIOPA.S.1.5.a" : "EBA.2.2|EIOPA.S.1.5.b",
                         string.Format("The link:schemaRef element in submitted instances MUST resolve to the full published entry point URL: {0}.",
                                        schemaFile));
            if (this.moduleId != UNINITIALIZED)
            {
                logError("EBA.1.5|EIOPA.S.1.5.a", 
                         string.Format("XBRL instance documents MUST reference only one entry point schema but multiple schema files referenced: {0}", 
                                       schemaFile));
                logError("EBA.2.3",
                         string.Format("Any reported XBRL instance document MUST contain only one xbrli:xbrl/link:schemaRef node: {0}",
                                       schemaFile));
            }
            else
            {
                // long result1 = _conn.ExecuteScalar<long>(string.Format("SELECT ModuleID FROM mModule WHERE XBRLSchemaRef = '{0}'", schemaFile));
                string _schemaFile = schemaFile;

                // replace schemaRef dates as needed
                if (scheamRefSubstitutions != null)
                    foreach (KeyValuePair<string,string> schemaRefSub in scheamRefSubstitutions)
                    {
                        _schemaFile = _schemaFile.Replace(schemaRefSub.Key, schemaRefSub.Value);
                    }

                IEnumerable<IdResult> result = _conn.Query<IdResult>("SELECT ModuleID AS id FROM mModule WHERE XBRLSchemaRef = ?", _schemaFile);
                if (result.Count() != 1)
                    throw new ApplicationException(string.Format(
                        "[EBA.1.5|EIOPA.S.1.5.a] File references schema file which is not in mModule table: file {0}, schemaRef {1}",
                        this.xbrlFileName, schemaFile));
                foreach (IdResult idResult in result)
                {
                    this.moduleId = idResult.id;
                }

                loadDimensionsAndEnumerations();  // requires moduleId to be known
            }
        }

        void processLinkbaseRef(string linkbaseFile)
        {
            logError("EBA.2.3|EIOPA.S.1.5.a",
                     string.Format("The link:linkbaseRef element is not allowed: {0}",
                                   linkbaseFile));
        }

        string canonicalQname(QName qn)
        {
            if (dpmNsPrefixes.ContainsKey(qn.namespaceURI) && !string.IsNullOrEmpty(dpmNsPrefixes[qn.namespaceURI]))
                return dpmNsPrefixes[qn.namespaceURI] + ":" + qn.localName;
            return qn.prefixedName;
        }

        string met(ModelFact f)
        {
            return string.Format("MET({0})", canonicalQname(f.tag));
        }

        string dimNameKey(ModelContext cntx)
        {
            List<string> dimPrefixedNames = new List<string>();
            foreach (ModelDimension d in cntx.dimensions)
                dimPrefixedNames.Add(canonicalQname(d.dimensionName));
            dimPrefixedNames.Sort();
            return stringJoin(dimPrefixedNames, "|");
        }
            
        string metDimNameKey(ModelFact f, ModelContext cntx)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(met(f));
            if (cntx.dimensions.Count > 0)
            {
                sb.Append("|");
                sb.Append(dimNameKey(cntx));
            }
            return sb.ToString();
        }

        string dimValKey(ModelContext cntx, Dictionary<string,string> restrictToDims = null)
        {
            List<string> dimVals = new List<string>();
            foreach (ModelDimension dim in cntx.dimensions)
            {
                if (dim.isTyped)
                    dimVals.Add(string.Format("{0}({1})", canonicalQname(dim.dimensionName), dim.typedValue));
                else
                    dimVals.Add(string.Format("{0}({1})", canonicalQname(dim.dimensionName), canonicalQname(dim.memberName)));
            }
            dimVals.Sort();
            return stringJoin(dimVals, "|");
        }

        string metDimTypedKey(ModelFact f)
        {
            return met(f) + (f.context != null ? ("|" + dimValKey(f.context)) : "");
        }

        string metDimValKey(ModelContext cntx, Dictionary<string,string> restrictToDims)
        {
            string key;
            if (restrictToDims.ContainsKey("MET"))
                key = string.Format("MET({0})|", restrictToDims["MET"]);
            else
                key = "";
            return key + dimValKey(cntx, restrictToDims);
        }

        void processFact(XmlReader reader, ModelFact f)
        {
            factCount += 1;
            if (asyncWorker != null && factCount % 100 == 0)
                asyncWorker.ReportProgress(0, string.Format("Loading XBRL instance {0}, fact {1}", xbrlFileName, factCount));

            ModelContext cntx = f.context;
            ModelUnit unit = f.unit;
            List<object> _toHash = new List<object>();
            _toHash.Add(f.tag);
            // if there were an xmlLang it's qn and value would be added to _toHash here
            object xValue;
            string dataPointSignature = metDimTypedKey(f);
            bool isNumeric = false, isDateTime = false, isBool = false, isText = false, isInstant = false, isValid = true;
            // debugFact(f);
            if (f.isNil)
            {
                xValue = null;
                _toHash.Add(qnXsiNil);
                _toHash.Add("true");
                logError("EBA.2.19|EIOPA.S.2.19",
                         string.Format("Nil facts MUST NOT be present in the instance: {0}",
                                       f.tag.prefixedName),
                         dataPointSignature);
            }
            else
            {
                xValue = f.value;
                char c = f.tag.localName[0];
                isInstant = f.tag.localName.Length > 1 ? f.tag.localName[1] == 'i' : false;
                if (c == 'm' || c == 'p' || c == 'r' || c == 'i')
                {
                    int decimals = int.MinValue;
                    if (string.IsNullOrEmpty(f.decimals) || !decimalsPattern.IsMatch(f.decimals))
                    {
                        logError("xbrl.4.6.3:missingDecimals",
                                    string.Format("Fact decimals value error: {0} decimals {1} context {2} value {3}",
                                                f.tag.prefixedName, f.decimals, f.contextRef, f.value),
                                    dataPointSignature);
                    }
                    else
                    {
                        if (f.decimals.Trim() == "INF")
                        {
                            if (!this.isEIOPAfullVersion)
                                logError("EIOPA.S.2.18.f",
                                         string.Format("Facts MUST NOT be reported with decimals INF: {0} context {1} value {2}",
                                                       f.tag.prefixedName, f.contextRef, f.value),
                                         dataPointSignature);
                            decimals = int.MaxValue;
                        }
                        else
                        {
                            int.TryParse(f.decimals, out decimals);
                        }
                    }
                    isNumeric = true;
                    if (!(c == 'i' ? integerPattern : decimalPattern).IsMatch(f.value))
                    {
                        logError("xmlSchema:valueError",
                                 string.Format("Fact value lexical error: {0} context {1} value {2}",
                                               f.tag.prefixedName, f.contextRef, f.value),
                                 dataPointSignature);
                        xValue = null;
                        isValid = false;
                    }
                    else
                    {
                        try
                        {
                            if (c == 'i') // integer
                            {
                                xValue = long.Parse(f.value, CultureInfo.InvariantCulture);
                                if (decimals != 0 && decimals != int.MinValue && decimals != int.MaxValue)
                                    logError("EBA.2.18|EIOPA.S.2.18.d",
                                             string.Format("Integer fact {0} of context {1} has a decimal attribute \u2260 0: '{2}'",
                                                           f.tag.prefixedName, f.context.id, decimals),
                                             dataPointSignature);
                            }
                            else
                            {
                                Decimal d = Decimal.Parse(f.value, CultureInfo.InvariantCulture);
                                xValue = d;
                                // check minimum number of decimals
                                int _decimals = (Math.Abs(d) % 1m).ToString().Length - 2;
                                int dMin;
                                if (c == 'm')
                                {
                                    /*
                                    if (_decimals < 2)
                                        logWarning("EIOPA:factDecimalDigitsWarning",
                                                   string.Format("Monetary fact must have 2 decimal digits: {0} context {1} value {2}",
                                                                 f.tag.prefixedName, f.contextRef, f.value),
                                                   dataPointSignature);
                                    */

                                    if (decimals < -3 && decimals != int.MinValue)
                                        logError("EBA.2.18|EIOPA.S.2.18.c",
                                                 string.Format("Monetary fact {0} of context {1} has a decimals attribute < -3: '{2}'",
                                                               f.tag.prefixedName, f.context.id, decimals),
                                                 dataPointSignature);
                                    else // apply dynamic decimals check
                                    {
                                        if  ((-.1m < d) && (d < .1m)) dMin = 2;
                                        else if ((-1m < d) && (d < 1m)) dMin = 1;
                                        else if ((-10m < d) && (d < 10m)) dMin = 0;
                                        else if ((-100m < d) && (d < 100m)) dMin = -1;
                                        else if ((-1000 < d) && (d < 1000)) dMin = -2;
                                        else dMin = -3;
                                        if (dMin > decimals)
                                            logWarning("EIOPA:factDecimalDigitsWarning",
                                                       string.Format("Monetary fact {0} of context {1} value {2} has an imprecise decimals attribute {4}, minimum is {3}",
                                                                     f.tag.prefixedName, f.contextRef, f.value, dMin, decimals),
                                                       dataPointSignature);

                                    }
                                }
                                else if (c == 'p')
                                {
                                    /*
                                    if (_decimals < 4)
                                        logWarning("EIOPA:factDecimalDigitsWarning",
                                                   string.Format("Decimals/percent fact must have 4 decimal digits: {0} context {1} value {2}",
                                                                 f.tag.prefixedName, f.contextRef, f.value),
                                                   dataPointSignature);
                                     */
                                    if (decimals < 4 && decimals != int.MinValue)
                                        logError("EBA.2.18|EIOPA.S.2.18.e",
                                                 string.Format("Percent fact {0} of context {1} has a decimal attribute < 4 : '{2}'",
                                                               f.tag.prefixedName, f.context.id, decimals),
                                                 dataPointSignature);
                                }
                                else // apply dynamic decimals check
                                {
                                    if  ((-.001m < d) && (d < .001m)) dMin = 4;
                                    else if ((-.01m < d) && (d < .01m)) dMin = 5;
                                    else if ((-.1m < d) && (d < .1m)) dMin = 2;
                                    else if ((-1m < d) && (d < 1m)) dMin = 1;
                                    else dMin = 0;
                                    if (dMin > decimals)
                                        logWarning("EIOPA:factDecimalsWarning",
                                                    string.Format("Numeric fact {0} of context {1} value {2} has an imprecise decimals attribute {4}, minimum is {3}",
                                                                    f.tag.prefixedName, f.contextRef, f.value, dMin, decimals),
                                                    dataPointSignature);
                                }
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            xValue = null;
                            isValid = false;
                            logError("xmlSchema:valueError",
                                     string.Format("Fact value missing: {0} context {1}",
                                                                    f.tag.prefixedName, f.contextRef),
                                     dataPointSignature);
                        }
                        catch (FormatException)
                        {
                            xValue = float.NaN;
                            isValid = false;
                            logError("xmlSchema:valueError",
                                     string.Format("Fact value error: {0} context {1} value {2}",
                                                   f.tag.prefixedName, f.contextRef, f.value),
                                     dataPointSignature);
                        }
                        catch (OverflowException)
                        {
                            xValue = float.NaN;
                            isValid = false;
                            logError("xmlSchema:valueError",
                                      string.Format("Fact value overflow error: {0} context {1} value {3}",
                                                    f.tag.prefixedName, f.contextRef, f.value),
                                      dataPointSignature);
                        }
                    }
                    if (f.unit == null)
                    {
                        logError("xbrl.4.6.2:numericUnit",
                                 string.Format("Fact missing unit: {0} context {1} value {2}",
                                               f.tag.prefixedName, f.contextRef, f.value),
                                 dataPointSignature);
                    }
                    else
                    {
                        if (unusedUnitIDs.Contains(unit.id))
                            unusedUnitIDs.Remove(unit.id);
                        if (c == 'm')
                        {
                            if (f.unit.multMeasures.Count != 1 || f.unit.divMeasures.Count != 0 ||
                                f.unit.multMeasures[0].namespaceURI != nsIso4217 || !isoCurrencyPattern.IsMatch(f.unit.multMeasures[0].localName))
                                logError("xbrl.4.8.2:monetaryFactUnit-notMonetaryMeasure",
                                         string.Format("Monetary (numeric) facts MUST use the ISO4217 unit: {0} context {1} value {2}",
                                                   f.tag.prefixedName, f.contextRef, f.value),
                                         dataPointSignature);
                        }
                        else
                        {
                            if (f.unit.multMeasures.Count != 1 || f.unit.divMeasures.Count != 0 ||
                                f.unit.multMeasures[0] != qnXbrliPure)
                                logError("EBA.3.2|EIOPA.3.2.a",
                                         string.Format("Non monetary (numeric) facts MUST use the pure unit: {0} context {1} value {2}",
                                                   f.tag.prefixedName, f.contextRef, f.value),
                                         dataPointSignature);
                        }
                    }
                    if (f.attributes.ContainsKey(qnPrecision))
                    {
                        logError("EBA.2.17|EIOPA.2.18.a",
                                 string.Format("Fact contains a precision attribute: {0} precision {1} context {2} value {3}",
                                               f.tag.prefixedName, f.attributes[qnPrecision].value, f.contextRef, f.value),
                                 dataPointSignature);
                    }
                }
                else
                {
                    if (c == 'd')
                    {
                        isDateTime = true;
                        // fact value is already a string
                        DateTime dateValue;
                        if (!datePattern.IsMatch(f.value) || !DateTime.TryParse(f.value, out dateValue))
                        {
                            logError("xmlSchema:valueError",
                                     string.Format("Fact value lexical error: {0} context {1} value {2}",
                                                   f.tag.prefixedName, f.contextRef, f.value),
                                     dataPointSignature);
                            isValid = false;
                        }
                    }
                    else if (c == 'b')
                    {
                        isBool = true;
                        // convert to int 1 for true or int 0 for false
                        xValue = xmlBoolTrueValues.Contains(f.value.Trim()) ? 1 : 0;
                        if (!boolPattern.IsMatch(f.value))
                        {
                            logError("xmlSchema:valueError",
                                     string.Format("Fact value lexical error: {0} context {1} value {2}",
                                                   f.tag.prefixedName, f.contextRef, f.value),
                                     dataPointSignature);
                            isValid = false;
                        }
                    }
                    else if (c == 'e')
                    {
                        QName enumQnValue = new QName(reader, f.value);
                        string enumStrValue = canonicalQname(enumQnValue);
                        xValue = enumStrValue;
                        // check enumeration value
                        if (enumElementValues.ContainsKey(f.tag.prefixedName) &&
                            !enumElementValues[f.tag.prefixedName].Contains(enumStrValue))
                        {
                            logError("xmlSchema:valueError",
                                     string.Format("Fact value enumeration error: {0} context {1} value {2}",
                                                   f.tag.prefixedName, f.contextRef, f.value),
                                     dataPointSignature);
                            isValid = false;
                        }
                        if (unusedXmlnsPrefixes.Contains(enumQnValue.prefix))
                            unusedXmlnsPrefixes.Remove(enumQnValue.prefix);
                        if (enumQnValue.hasUndeclaredPrefix)
                            logError("xmlSchema:qnameError",
                                     string.Format("Fact value enumeration has undeclared prefix: {0} context {1} value {2}",
                                                   f.tag.prefixedName, f.contextRef, f.value),
                                     dataPointSignature);
                    }
                    else if (c == 's')
                    {
                        /* temporarily deactivate until lang provided per e-mail 2014-11-30
                        if (string.IsNullOrEmpty(f.xmlLang))
                            logError("EBA.2.20",
                                     string.Format("String facts need to report xml:lang: {0}",
                                                   f.tag.prefixedName),
                                     dataPointSignature);
                         */
                    }
                    if (f.unit != null)
                    {
                        logError("xbrl.4.6.2:nonnumericUnit",
                                 string.Format("Fact is non-numeric but has a unit: {0} context {1} value {2}",
                                               f.tag.prefixedName, f.contextRef, f.value),
                                 dataPointSignature);
                    }
                    if (f.attributes.ContainsKey(qnPrecision))
                    {
                        logError("EBA.2.17|EIOPA.2.18.a",
                                 string.Format("Fact is non-numeric and contains a precision attribute: {0} precision {1} context {2} value {3}",
                                               f.tag.prefixedName, f.attributes[qnPrecision].value, f.contextRef, f.value),
                                 dataPointSignature);
                    }
                    else if (!string.IsNullOrEmpty(f.decimals))
                    {
                        logError("xbrl.4.6.3:extraneousDecimals",
                                 string.Format("Fact is non-numeric and contains a decimals value: {0} decimals {1} context {2} value {3}",
                                               f.tag.prefixedName, f.decimals, f.contextRef, f.value),
                                               dataPointSignature);
                    }
                }
            }
                
            isText = ! (isNumeric || isBool || isDateTime); // 's' or 'u' type
            if (f.tag == qnFindFilingIndicators)
            {
                if (cntx != null)
                    logError("xbrl.4.6.1:tupleContextRef",
                             string.Format("Filing indicators tuple may not have a context: {0}",
                                           f.contextRef),
                             dataPointSignature);
                string filingIndicators = "";
                foreach (ModelFact filingIndicator in f.tupleFacts)
                {
                    if (string.IsNullOrEmpty(filingIndicator.contextRef))
                        logError("xbrl.4.6.1:itemContextRef",
                                 string.Format("Filing indicator missing a context: {0}",
                                               filingIndicator.value),
                                 dataPointSignature);
                    if (unusedContextIDs.Contains(filingIndicator.contextRef))
                        unusedContextIDs.Remove(filingIndicator.contextRef);
                    string v = filingIndicator.value.Trim();
                    if (dFilingIndicators.ContainsKey(v))
                        logError("EBA.1.6.1|EIOPA.1.6.1", 
                                 string.Format("Multiple filing indicators facts for indicator {0}",
                                               v),
                                 dataPointSignature);

                    dFilingIndicators[v] = bool.Parse(filingIndicator.attributes.ContainsKey(qnFindFiled) ? filingIndicator.attributes[qnFindFiled].value : "true");

                    if (filingIndicator.context == null)
                        logError("xbrl.4.6.1:itemContextRef",
                                 string.Format("Filing indicator missing context error: {0} context {1} value {2}",
                                               filingIndicator.tag.prefixedName, filingIndicator.contextRef, filingIndicator.value),
                                 dataPointSignature);
                    else if (filingIndicator.context.dimensions.Count > 0)
                        logError(this.isEIOPAfullVersion ? "EIOPA.N.1.6.d" : "EIOPA.S.1.6.d",
                                 string.Format("Filing indicators context may not have segment or scenario: {0} context {1} value {2}",
                                               filingIndicator.tag.prefixedName, filingIndicator.contextRef, filingIndicator.value),
                                 dataPointSignature);
                    if (factsCheckVersion != null && filingIndicator.context != null)
                        factsCheckMd5s += md5hash(filingIndicator.tag, filingIndicator.context.md5sum, v);

                    if (filingIndicators.Length > 0)
                        filingIndicators += ", ";
                    filingIndicators += v;
                }
                if (itemCount > 0)
                    logWarning("EIOPA.1.6.2",
                               string.Format("Filing indicators should preceed business facts: {0}",
                                             filingIndicators),
                               dataPointSignature);
                if (factsCheckVersion != null)
                    factsCheckMd5s += md5hash(f.tag);
                // loadAllowedMetricsAndDims();  // reload allowed metrics and dims
            }
            else if (f.tag == qnFindFilingIndicator && this.isEIOPAfullVersion)
            {
                bool _isPos = bool.Parse(f.attributes.ContainsKey(qnFindFiled) ? f.attributes[qnFindFiled].value : "true");
                logError(_isPos ? "EIOPA.1.6.a" : "EIOPA.1.6.b",
                         string.Format("The filing indicator is not in a tuple: {0}",
                                       f.value.Trim()));
            }
            else if (cntx == null)
            {
                logError("xbrl.4.6.1:itemContextRef",
                         string.Format("Fact missing context error: {0} context {1} value {2}",
                                       f.tag.prefixedName, f.contextRef, f.value),
                         dataPointSignature);
            }
            else // context is not null
            {
                itemCount += 1;
                if (unusedContextIDs.Contains(cntx.id))
                    unusedContextIDs.Remove(cntx.id);
                // find which explicit dimensions act as typed
                string factMetDimNameKey = metDimNameKey(f, cntx);
                /*
                if (metricAndDimensionsTableId.ContainsKey(factMetDimNameKey))
                {
                    foreach (long tableID in metricAndDimensionsTableId[factMetDimNameKey])
                    {
                        if (yDimVal.ContainsKey(tableID) && zDimVal.ContainsKey(tableID))
                        {
                            Dictionary<string, string> yDimVals = yDimVal[tableID];
                            Dictionary<string, string> zDimVals = zDimVal[tableID];
                            string yDimKey = metDimValKey(cntx, yDimVals);
                            string zDimKey = metDimValKey(cntx, zDimVals);
                            if (zDimKey == "")
                                zDimKey = null;
                            AvailableTableRowsKey availableTableRowsKey = new AvailableTableRowsKey(tableID, zDimKey);
                            if (!availableTableRows.ContainsKey(availableTableRowsKey))
                                availableTableRows[availableTableRowsKey] = new HashSet<string>();
                            availableTableRows[availableTableRowsKey].Add(yDimKey);
                        }
                    }
                }
                 */
                // verify that fact isn't a duplicate
                IEnumerable<IdResult> result = _conn.Query<IdResult>(
                    "SELECT FactID AS id FROM dFact WHERE InstanceID = ? AND DataPointSignature = ?",
                    instanceId, dataPointSignature);
                if (result.Count() > 0)
                {
                    logError("EBA.2.16|EIOPA.S.2.16.a",
                              string.Format("Fact is a duplicate: {0} context {1} value {2}",
                                            f.tag.prefixedName, f.contextRef, f.value),
                              dataPointSignature);
                } else if (isValid) {
                    // add fact (if it is lexically valid)
                    _conn.Execute("INSERT INTO dFact ('InstanceID', 'DataPointSignature', " +
                                                      "'Unit', 'Decimals', 'NumericValue', 'DateTimeValue', 'BooleanValue', 'TextValue') " +
                                             "VALUES (?, ?, ?, ?, ?, ?, ?, ?)",
                                   instanceId,
                                   dataPointSignature,
                                   isNumeric && f.unit != null && f.unit.multMeasures.Count == 1 && f.unit.divMeasures.Count == 0 ? f.unit.multMeasures[0].prefixedName : null,
                                   f.decimals,
                                   isNumeric ? xValue : null,
                                   isDateTime ? xValue : null,
                                   isBool ? xValue : null,
                                   isText ? xValue : null
                                );
                }
                if (isInstant && !cntx.isInstant)
                    logError("xbrl.4.7.2:contextPeriodType",
                             string.Format("Instant fact has duration context: {0} context {1} value {2}",
                                           f.tag.prefixedName, f.contextRef, f.value),
                             dataPointSignature);
                    if (!string.IsNullOrEmpty(f.value))
                        _toHash.Add(f.value);
                _toHash.Add(cntx.md5sum);
                if (f.unit != null)
                    _toHash.Add(f.unit.md5sum);
                if (factsCheckVersion != null)
                    factsCheckMd5s += md5hash(_toHash);
            }
        }

        void debugFact(ModelFact fact, string indent="")
        {
            // process a fact that has all of its data
            Debug.WriteLine(string.Format("{0}Fact {1} cntx={2} unit={3} dec={4} value={5}", 
                indent, fact.tag.prefixedName, fact.contextRef, fact.unitRef, fact.decimals, fact.value));
            // show context
            if (fact.context != null)
            {
                if (fact.context.startDate != DateTime.MinValue)
                    Debug.WriteLine(string.Format("{0}    startDate={1}", indent, fact.context.startDate.ToString("o")));
                if (fact.context.endDate != DateTime.MinValue)
                    Debug.WriteLine(string.Format("{0}    endInstantDate={1}", indent, fact.context.endDate.ToString("o")));
                Debug.WriteLine(string.Format("{0}    scheme={1} ident={2}", indent, fact.context.entityScheme, fact.context.entityIdentifier));
                foreach (ModelDimension dim in fact.context.dimensions)
                {
                    Debug.WriteLine(string.Format("{0}    dim={1} val={2}", indent, dim.dimensionName.prefixedName, 
                        (dim.isTyped) ? dim.typedValue : dim.memberName.prefixedName));
                }
            }
            if (fact.unit != null)
            {
                foreach (QName qnMeas in fact.unit.multMeasures)
                    Debug.WriteLine(string.Format("{0}    unit mult meas={1}", indent, qnMeas.prefixedName));
                foreach (QName qnMeas in fact.unit.divMeasures)
                    Debug.WriteLine(string.Format("{0}    unit div meas={1}", indent, qnMeas.prefixedName));
            }
            foreach (ModelFact childFact in fact.tupleFacts)
                debugFact(childFact, indent = indent + "    ");
        }

        void processContext(ModelContext cntx)
        {
            unusedContextIDs.Add(cntx.id);
            List<object> _toHash = new List<object>();
            if (entityIdentifier == null)
            {
                entityIdentifier = cntx.entityIdentifier;
                entityScheme = cntx.entityScheme;
            }
            else if (cntx.entityIdentifier != entityIdentifier)
                logError("EBA.2.9|EIOPA.2.9",
                         string.Format("All entity identifiers and schemes MUST be the same, contexts have conflicting entity identifiers: {0} and {1}",
                                       entityIdentifier, cntx.entityIdentifier));
            else if (cntx.entityScheme != entityScheme)
                logError("EBA.2.9|EIOPA.2.9",
                         string.Format("All entity identifiers and schemes MUST be the same, contexts have conflicting entity schemes: {0} and {1}",
                                       entityScheme, cntx.entityScheme));
            if (leiEntitySchemes.Contains(cntx.entityScheme) || (!this.isEIOPAfullVersion && cntx.entityScheme == "PRE-LEI"))
            {
                if (!invalidLEIs.Contains(cntx.entityIdentifier))
                {
                    int leiValidity = checkLei(cntx.entityIdentifier);
                    if (leiValidity == LEI_INVALID_LEXICAL)
                        logError("EIOPA.S.2.8.c",
                                 string.Format("Context has lexically invalid LEI: {0}.", cntx.entityIdentifier));
                    else if (leiValidity == LEI_INVALID_CHECKSUM)
                        logError("EIOPA.S.2.8.c",
                                 string.Format("Context has LEI checksum error in LEI: {0}.", cntx.entityIdentifier));
                    invalidLEIs.Add(cntx.entityIdentifier);
                }
            }
            else if (cntx.entityScheme != "SC" && !invalidLEIs.Contains(cntx.entityScheme))
            {
                logError("EIOPA.S.2.8.c",
                         string.Format("Context has unrecognized entity scheme: {0}.", cntx.entityScheme));

                invalidLEIs.Add(cntx.entityScheme);
            }
            _toHash.Add(cntx.entityScheme);
            _toHash.Add(cntx.entityIdentifier);
            if (cntx.isInstant)
            {
                if (periodInstantDate == DateTime.MinValue)  // capture first instantDate for the whole instance
                {
                    periodInstantDate = cntx.instantDate;
                }
                else if (cntx.instantDate != periodInstantDate)
                    logError("EBA.2.13|EIOPA.2.13",
                             string.Format("Contexts have conflicting instant periods: {0} and {1}",
                                           periodInstantDate.ToString("yyyy-MM-dd"), cntx.instantDate.ToString("yyyy-MM-dd")));
                _toHash.Add(cntx.instantDate // instant is full date-time, not date-only
                    .ToString("yyyy-MM-ddTHH:mm:ssK", System.Globalization.CultureInfo.InvariantCulture));
            }
            else if (cntx.isStartEnd)
                logError("EBA.2.13|EIOPA.N.2.11", "Start-End (flow) context period is not allowed.");
            else if (cntx.isForever)
                logError("EBA.2.11|EIOPA.N.2.11", "Forever context period is not allowed.");

            foreach (ModelDimension _dim in cntx.dimensions)
            {
                _toHash.Add(_dim.md5sum);
                if (unusedXmlnsPrefixes.Contains(_dim.dimensionName.prefix))
                    unusedXmlnsPrefixes.Remove(_dim.dimensionName.prefix);
                if (unusedXmlnsPrefixes.Contains(_dim.memberName.prefix)) // memberName is used for both explicit and typed dimensions
                    unusedXmlnsPrefixes.Remove(_dim.memberName.prefix);
                if (_dim.dimensionName.hasUndeclaredPrefix)
                    logError("xmlSchema:qnameError",
                             string.Format("Context dimension has undeclared prefix: context {0} dimension {1}",
                                           cntx.id, _dim.dimensionName.localName));
                if (_dim.memberName.hasUndeclaredPrefix)
                    logError("xmlSchema:qnameError",
                             string.Format("Context dimension member has undeclared prefix: context {0} member {1}",
                                           cntx.id, _dim.memberName.localName));
            }
            cntx.md5sum = md5hash(_toHash);

            if (cntxHashIDs.ContainsKey(cntx.md5sum))
            {
                if (!isStreamingMode)
                    log(this.isEIOPAfullVersion ? "warning" : "error",
                        "EIOPA.S.2.7.b",
                        string.Format("Duplicate contexts MUST NOT be reported, contexts {0} and {1} are equivalent.",
                                                cntx.id, cntxHashIDs[cntx.md5sum]));
            }
            else
                cntxHashIDs[cntx.md5sum] = cntx.id;

            if (instanceId == UNINITIALIZED && entityIdentifier != null)  // start the instance without waiting for unit of EntityCurrency
                startInstance();
        }

        void processUnit(ModelUnit unit)
        {
            unusedUnitIDs.Add(unit.id);
            if (unit.divMeasures.Count == 0 && unit.multMeasures.Count == 1 && unit.multMeasures[0].namespaceURI == nsIso4217)
            {
                if (entityCurrency == null) // capture first monetary unit for instance
                {
                    entityCurrency = unit.multMeasures[0].localName;
                    if (instanceId == UNINITIALIZED)
                    {
                        if (entityIdentifier != null && entityCurrency != null)
                            startInstance();
                    }
                    else  // now got an entityCurrency which had been null, update the instance's entity currency
                    {
                        _conn.Execute("UPDATE dInstance SET EntityCurrency=? WHERE InstanceID=?",
                                      new object[] {entityCurrency,
                                                instanceId});
                    }
                }
                else if (entityCurrency != unit.multMeasures[0].localName)
                    logError("EBA.3.1|EIOPA.3.1",
                             string.Format("There MUST be only one currency but currencies found:  {0} and {1}",
                                           entityCurrency, unit.multMeasures[0].localName));
            }
            List<object> _multMeasureHashes = new List<object>(), _divMeasureHashes = new List<object>();
            foreach (QName multMeasure in unit.multMeasures)
            {
                _multMeasureHashes.Add(md5hash(multMeasure));
                if (unusedXmlnsPrefixes.Contains(multMeasure.prefix))
                    unusedXmlnsPrefixes.Remove(multMeasure.prefix);
                if (multMeasure.hasUndeclaredPrefix)
                    logError("xmlSchema:qnameError",
                             string.Format("Unit measure has undeclared prefix: unit {0} measure {1}",
                                           unit.id, multMeasure.localName));
            }
            foreach (QName divMeasure in unit.divMeasures)
            {
                _divMeasureHashes.Add(md5hash(divMeasure));
                if (unusedXmlnsPrefixes.Contains(divMeasure.prefix))
                    unusedXmlnsPrefixes.Remove(divMeasure.prefix);
                if (divMeasure.hasUndeclaredPrefix)
                    logError("xmlSchema:qnameError",
                             string.Format("Unit measure has undeclared prefix: unit {0} measure {1}",
                                           unit.id, divMeasure.localName));
            }
            if (unit.divMeasures.Count == 0)
                unit.md5sum = md5hash(_multMeasureHashes);
            else // for mult/div want hash of hash-hex strings of mult and div
                unit.md5sum = md5hash(md5hash(_multMeasureHashes).ToHex(), md5hash(_divMeasureHashes).ToHex());
            if (unitHashIDs.ContainsKey(unit.md5sum))
            {
                logWarning("EBA.2.21",
                           string.Format("Duplicate units SHOULD NOT be reported, units {0} and {1} have same measures.",
                                         unit.id, unitHashIDs[unit.md5sum]));
                if (!isStreamingMode)
                    logError("EIOPA.2.21",
                               string.Format("Duplicate units MUST NOT be reported, units {0} and {1} have same measures.",
                                             unit.id, unitHashIDs[unit.md5sum]));
            }
            else
                unitHashIDs[unit.md5sum] = unit.id;
        }

        Dictionary<string, Regex>typedDimensionDomain = null;
        Dictionary<String, HashSet<string>> explicitDimensionDomain = null;
        Dictionary<string, int> largeDimensionIds = null;
        Dictionary<string, Dictionary<string, int>> largeDimensionMemberIds = null;

        class ArrayEqCmpForInt2 : IEqualityComparer<int[]>
        {
            public bool Equals(int[] a, int[] b) {
                return a[0] == b[0] && a[1] == b[1];
            }
            public int GetHashCode(int[] data) {
                return data[0].GetHashCode() ^ (data[1].GetHashCode() * 65536);
            }
        }
        HashSet<int[]> largeDimMemIds = null;
        Dictionary<String, HashSet<string>> domainHiearchyMembers = null;
        Dictionary<String, HashSet<string>> enumElementValues = null; 

        private void loadDimensionsAndEnumerations()
        {
            // get typed dimension domain element regex value expressions
            IEnumerable<QnValResult> result1 = _conn.Query<QnValResult>(
                "SELECT dim.DimensionXBRLCode as qn, ( '[<]' || dom.DomainXBRLCode || '[>]' || " +
                " CASE WHEN dom.DataType = 'Integer' THEN '\\d+' ELSE '.+' END || " + // HF: change string from \\S+ to .+
                " '[<][/]' || dom.DomainXBRLCode || '[>]' " +
                " || '|[<]' || dom.DomainXBRLCode || '/>' ) as val " +
                "FROM mDimension dim, mDomain dom " +
                "WHERE dim.IsTypedDimension AND dim.DomainID = dom.DomainID");
            typedDimensionDomain = new Dictionary<string, Regex>();
            foreach (QnValResult r1 in result1)
                typedDimensionDomain[r1.qn] = new Regex(r1.val);

            // get large dimension ids & qnames
            IEnumerable<QnIntResult>result1a = _conn.Query<QnIntResult>(
                "SELECT dim.DimensionXBRLCode as qn, dim.DimensionId as val " +
                "FROM mModuleLargeDimension mld, mDimension dim " +
                " WHERE mld.ModuleId = ? AND dim.DimensionId = mld.DimensionId",
                this.moduleId);
            largeDimensionIds = new Dictionary<string, int>();
            foreach (QnIntResult r1a in result1a)
                largeDimensionIds[r1a.qn] = r1a.val;
            largeDimensionMemberIds = new Dictionary<string, Dictionary<string, int>>();

            // get explicit dimension domain element qnames
            IEnumerable<QnValIntResult> result2 = _conn.Query<QnValIntResult>(
                "SELECT dim.DimensionXBRLCode as qn, mem.MemberXBRLCode as val, mem.MemberId as val2 " +
                "FROM mDomain dom " +
                "left outer join mHierarchy h on h.DomainID = dom.DomainID " +
                "left outer join mHierarchyNode hn on hn.HierarchyID = h.HierarchyID " +
                "left outer join mMember mem on mem.MemberID = hn.MemberID " +
                "inner join mDimension dim on dim.DomainID = dom.DomainID and not dim.isTypedDimension");
            explicitDimensionDomain = new Dictionary<String, HashSet<string>>();
            domainHiearchyMembers = new Dictionary<String, HashSet<string>>(); // filled in as needed below

            foreach (QnValIntResult r2 in result2)
            {
                if (!string.IsNullOrEmpty(r2.val))
                {
                    if (!explicitDimensionDomain.ContainsKey(r2.qn))
                        explicitDimensionDomain[r2.qn] = new HashSet<string>();
                    explicitDimensionDomain[r2.qn].Add(r2.val);
                    if (largeDimensionIds.ContainsKey(r2.qn))
                    {
                        if (!largeDimensionMemberIds.ContainsKey(r2.qn))
                            largeDimensionMemberIds[r2.qn] = new Dictionary<string, int>();
                        largeDimensionMemberIds[r2.qn][r2.val] = r2.val2;
                    }
                }
            }
            
            // get enumeration element values
            IEnumerable<QnValResult> result3 = _conn.Query<QnValResult>(
                "select mem.MemberXBRLCode as qn, enum.MemberXBRLCode as val from mMetric met " +
                "inner join mMember mem on mem.MemberID = met.CorrespondingMemberID " +
                "inner join mHierarchyNode hn on hn.HierarchyID = met.ReferencedHierarchyID " +
                "inner join mMember enum on enum.MemberID = hn.MemberID " +
                "where (hn.IsAbstract is null or hn.IsAbstract = 0) " +
                "      and case when met.HierarchyStartingMemberID is not null then " +
                "        (hn.Path like '%'||ifnull(met.HierarchyStartingMemberID,'')||'%' " +
                "            or (hn.MemberID = ifnull(met.HierarchyStartingMemberID,'') and 1 = ifnull(met.IsStartingMemberIncluded,0))) " +
                "        else 1 end");
            enumElementValues = new Dictionary<String, HashSet<string>>();
            foreach (QnValResult r3 in result3)
            {
                if (!enumElementValues.ContainsKey(r3.qn))
                    enumElementValues[r3.qn] = new HashSet<string>();
                enumElementValues[r3.qn].Add(r3.val);
            }
        }

        HashSet<string> metricsForFilingIndicators = new HashSet<string>();
        Dictionary<string, List<Dictionary<string,string[]>>> signaturesForFilingIndicators = new Dictionary<string, List<Dictionary<string,string[]>>>();
        //TODO: review with Herm
        private void loadAllowedMetricsAndDims()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, bool> _filingIndicator in dFilingIndicators)
                if (_filingIndicator.Value) 
                {
                    if (sb.Length > 0) sb.Append(",");
                    sb.Append("'").Append(_filingIndicator.Key.Replace("'", "''")).Append("'");
                }
            string _filedFilingIndicators = sb.ToString();
            IEnumerable<StrResult> result = _conn.Query<StrResult>(
                string.Format(
                    "select distinct mem.MemberXBRLCode as str " +
                    "from mTemplateOrTable tott " + 
                    "inner join mTemplateOrTable tottv on tottv.ParentTemplateOrTableID = tott.TemplateOrTableID " +
                    "   and tott.TemplateOrTableCode in ({0}) " +
                    "inner join mTemplateOrTable totbt on totbt.ParentTemplateOrTableID = tottv.TemplateOrTableID " +
                    "inner join mTemplateOrTable totat on totat.ParentTemplateOrTableID = totbt.TemplateOrTableID " +
                    "inner join mTaxonomyTable tt on tt.AnnotatedTableID = totat.TemplateOrTableID " +
                    "inner join mTableAxis ta on ta.TableID = tt.TableID " +
                    "inner join mAxisOrdinate ao on ao.AxisID = ta.AxisID " +
                    "inner join mOrdinateCategorisation oc on oc.OrdinateID = ao.OrdinateID AND (oc.Source = 'MD' or oc.Source is null) " +
                    "inner join mMember mem on mem.MemberID = oc.MemberID " +
                    "inner join mMetric met on met.CorrespondingMemberID = mem.MemberID " +
                    "inner join mModuleBusinessTemplate mbt on mbt.BusinessTemplateID = tottv.TemplateOrTableID " +
                    "   and mbt.ModuleID = {1} ",
                    _filedFilingIndicators, this.moduleId));
            this.metricsForFilingIndicators = new HashSet<String>();
            foreach (StrResult r in result)
            {
                this.metricsForFilingIndicators.Add(r.str);
            }

            result = _conn.Query<StrResult>(
                string.Format(
                    "select distinct tc.DatapointSignature as str " +
                    "from mTemplateOrTable tott " +
                    "inner join mTemplateOrTable tottv on tottv.ParentTemplateOrTableID = tott.TemplateOrTableID " +
                    "  and tott.TemplateOrTableCode in ( {0} ) " +
                    "inner join mTemplateOrTable totbt on totbt.ParentTemplateOrTableID = tottv.TemplateOrTableID " +
                    "inner join mTemplateOrTable totat on totat.ParentTemplateOrTableID = totbt.TemplateOrTableID " +
                    "inner join mTaxonomyTable tt on tt.AnnotatedTableID = totat.TemplateOrTableID " +
                    "inner join mTableCell tc on tc.TableID = tt.TableID and (tc.IsShaded = 0 or tc.IsShaded is null) " +
                    "inner join mModuleBusinessTemplate mbt on mbt.BusinessTemplateID = tottv.TemplateOrTableID " +
                    "  and mbt.ModuleID = {1} ",
                    _filedFilingIndicators, this.moduleId));
            this.signaturesForFilingIndicators = new Dictionary<string, List<Dictionary<string,string[]>>>();
            foreach (StrResult r in result)
            {
                string[] metDims = r.str.Split(new char[] { '|' });
                string met = metDims[0].Substring(4, metDims[0].Length - 5);
                Dictionary<string,string[]> _dimVals = new  Dictionary<string,string[]>();
                for (int i = 1; i < metDims.Length; i++)
                {
                    string _dimVal = metDims[i];
                    Match _dimSigMatch = sigDimPattern.Match(_dimVal);
                    if (_dimSigMatch.Success)
                    {
                        String _dim = _dimSigMatch.Groups[1].Value;
                        String _sig = _dimSigMatch.Groups[2].Value;
                        String _hier = (_dimSigMatch.Groups.Count > 4 && _dimSigMatch.Groups[4].Value != "") ? _dimSigMatch.Groups[4].Value : null;
                        _dimVals[_dim] = new string[] {_sig, _hier};
                    }
                }
                if (!this.signaturesForFilingIndicators.ContainsKey(met))
                    this.signaturesForFilingIndicators[met] = new List<Dictionary<string,string[]>>();
                this.signaturesForFilingIndicators[met].Add(_dimVals);
            }
        }

        private HashSet<string> dHierarchyMembers(string hierarchyKey)
        {
            if (domainHiearchyMembers.ContainsKey(hierarchyKey))
                return domainHiearchyMembers[hierarchyKey];

            // hierarchyKey not yet loaded into domainHiearchyMembers, query it from database
            string[] _hierarchies = hierarchyKey.Split(new char[] { ';' });

            IEnumerable<StrResult> result;
            if (_hierarchies.Length == 1)
            {
                result = _conn.Query<StrResult>(
                    "select mem.MemberXBRLCode as str from mHierarchyNode hn " +
                    "inner join mMember mem on mem.MemberID = hn.MemberID " +
                    "where hn.HierarchyID = ? and hn.IsAbstract != 1",
                     _hierarchies[0]);
            }
            else if (_hierarchies.Length == 3)
            {
                result = _conn.Query<StrResult>(
                    "select distinct mem.MemberXBRLCode as str from mHierarchyNode hn " +
                    "left outer join mHierarchyNode hn2 on hn2.ParentMemberID = hn.MemberID and hn2.HierarchyID = hn.HierarchyID " +
                    "left outer join mHierarchyNode hn3 on hn3.ParentMemberID = hn2.MemberID and hn3.HierarchyID = hn.HierarchyID " +
                    "left outer join mHierarchyNode hn4 on hn4.ParentMemberID = hn3.MemberID and hn4.HierarchyID = hn.HierarchyID " +
                    "inner join mMember mem on mem.MemberID = hn.MemberID or mem.MemberID = hn2.MemberID or mem.MemberID = hn3.MemberID or mem.MemberID = hn4.MemberID " +
                    "where hn.HierarchyID = ? and hn.MemberID = ? ",
                    _hierarchies[0], _hierarchies[1]);
            }
            else
            {
                result = null; // empty list
            }
            HashSet<string> _mems = new HashSet<string>();
            if (result != null)
                foreach (StrResult r in result)
                {
                    _mems.Add(r.str);
                }
            domainHiearchyMembers[hierarchyKey] = _mems;
            return _mems;
        }


        private void validateFactSignature(string dpmSignature)
        {

            string[] _metDims = dpmSignature.Split(new char[] { '|' });
            HashSet<int[]> _largeDimMemIds = new HashSet<int[]>(new ArrayEqCmpForInt2()); 

            if (_metDims.Length > 1)
            {
                string met = _metDims[0];
                if (!string.IsNullOrEmpty(met) && met.Length >= 4)
                    met = met.Substring(4, met.Length - 5);
                if (!metricsForFilingIndicators.Contains(met))
                {
                    logError("EBA.1.7.1|EIOPA.1.7.1",
                             string.Format("Fact Qname {0} is not allowed for filing indicators",
                                           met),
                             dpmSignature);
                }
                else if (signaturesForFilingIndicators.ContainsKey(met))
                {
                    Dictionary<string, string> _dimVals = new Dictionary<string, string>();
                    for (int iDim=1; iDim < _metDims.Length; iDim++)
                    {
                        string[] _dimVal = _metDims[iDim].Split(new char[] { '(', ')' });
                        if (_dimVal.Length >= 2)
                            _dimVals[_dimVal[0]] = _dimVal[1];
                    }
                    List<Dictionary<string, string[]>> _dimSigs = signaturesForFilingIndicators[met];
                    bool _sigMatched = false;
                    HashSet<string> _differentDims = new HashSet<string>();
                    HashSet<string> _missingDims = new HashSet<string>();
                    IEnumerable<string> _extraDims = new HashSet<string>();
                    HashSet<string> _closestMatchDiffDims = new HashSet<string>();
                    int _closestMatch = 9999;
                    Dictionary<string, string[]> _closestMatchSig = null;
                    if (_dimSigs.Count > 0)
                    {
                        foreach (Dictionary<string, string[]> _dimSig in _dimSigs)
                        {
                            int _lenDimVals = _dimVals.Count;
                            int _lenDimSig = 0;
                            foreach (KeyValuePair<string, string[]> _ds in _dimSig)
                                if (_ds.Value[0] != "*?" || _dimVals.ContainsKey(_ds.Key))
                                    _lenDimSig += 1;
                            int _differentDimCount = Math.Abs(_lenDimSig - _lenDimVals);
                            _differentDims = new HashSet<string>();
                            foreach (KeyValuePair<string,string> _dimVal in _dimVals)
                                if (_dimSig.ContainsKey(_dimVal.Key) &&
                                    (_dimSig[_dimVal.Key][0] != "*" && _dimSig[_dimVal.Key][0] != "*?" && _dimSig[_dimVal.Key][0] != _dimVal.Value))
                                    _differentDims.Add(_dimVal.Key);
                            int _difference = _differentDimCount + _differentDims.Count;
                            if (_difference == 0)
                            {
                                _largeDimMemIds.Clear();
                                // check * dimensions
                                foreach (KeyValuePair<string,string> _dimVal in _dimVals)
                                    if (_dimSig.ContainsKey(_dimVal.Key))
                                    {
                                        string[] _sigValHier = _dimSig[_dimVal.Key];
                                        string _sigVal = _sigValHier[0];
                                        string _sigHier = _sigValHier[1];
                                        if (_sigVal == "*" || _sigVal == "*?")
                                        {
                                            if (explicitDimensionDomain.ContainsKey(_dimVal.Key))
                                            {
                                                HashSet<string> _dimMems;
                                                if (!String.IsNullOrEmpty(_sigHier))
                                                    _dimMems = dHierarchyMembers(_sigHier);
                                                else
                                                    _dimMems = explicitDimensionDomain[_dimVal.Key];
                                                if (!_dimMems.Contains(_dimVal.Value))
                                                {
                                                    _difference += 1;
                                                    _differentDims.Add(_dimVal.Key);
                                                }
                                            }
                                            else if (typedDimensionDomain.ContainsKey(_dimVal.Key))
                                            {
                                                if (!typedDimensionDomain[_dimVal.Key].IsMatch(_dimVal.Value))
                                                {
                                                    _difference += 1;
                                                    _differentDims.Add(_dimVal.Key);
                                                }
                                            }
                                        }
                                        if (largeDimensionIds.ContainsKey(_dimVal.Key) && 
                                            largeDimensionMemberIds.ContainsKey(_dimVal.Key) &&
                                            largeDimensionMemberIds[_dimVal.Key].ContainsKey(_dimVal.Value))
                                        {
                                            _largeDimMemIds.Add(new int[] { largeDimensionIds[_dimVal.Key], largeDimensionMemberIds[_dimVal.Key][_dimVal.Value] });
                                        }
                                    }
                                if (_difference == 0)
                                {
                                    _sigMatched = true;
                                    break;
                                }
                            }
                            if (_difference < _closestMatch)
                            {
                                _extraDims = (IEnumerable<string>)_dimVals.Keys.Except<string>(_dimSig.Keys);
                                _missingDims = new HashSet<string>();
                                foreach (KeyValuePair<string, string[]> _ds in _dimSig)
                                    if (!_dimVals.ContainsKey(_ds.Key) && _ds.Value[0] != "*?")
                                        _missingDims.Add(_ds.Key);
                                _closestMatchDiffDims = _differentDims;
                                _closestMatchSig = _dimSig;
                                _closestMatch = _difference;
                            }
                        }
                        if (_sigMatched)
                        {
                            largeDimMemIds.UnionWith(_largeDimMemIds);
                        }
                        else
                        {
                            string _missings = "", _extras = "", _diffs = "";
                            foreach (string _dimM in _missingDims)
                            {
                                if (_missings.Length > 0)
                                    _missings += ", ";
                                string[] _m = _closestMatchSig[_dimM];
                                _missings += string.Format("{0}({1})", _dimM, _m[0] + (String.IsNullOrEmpty(_m[1]) ? (";" + _m[1]) : ""));
                            }
                            if (_missings.Length == 0)
                                _missings = "none";
                            foreach (string _dimE in _extraDims)
                            {
                                if (_extras.Length > 0)
                                    _extras += ", ";
                                _extras += string.Format("{0}({1})", _dimE, _dimVals[_dimE]);
                            }
                            if (_extras.Length == 0)
                                _extras = "none";
                            foreach (string _dimD in _closestMatchDiffDims)
                            {
                                if (_diffs.Length > 0)
                                    _diffs += ", ";
                                _diffs += string.Format("dim: {0} fact: {1} DPMsig: {2}", _dimD, _dimVals[_dimD], _closestMatchSig[_dimD]);
                            }
                            if (_diffs.Length == 0)
                                _diffs = "none";
                            logError("sqlDB:factDimensionsError",
                                     string.Format("Fact Qname {0} dimensions not allowed for filing indicators; " +
                                                   "Extra dimensions of fact: {1}, missing dimensions of fact: {2}, different dimensions of fact: {3}",
                                                    met, _extras, _missings, _diffs),
                                     dpmSignature);
                        }
                    }
                }
            }

        }

        private class MetricAndDimensionsResult
        {
            public string MetricAndDimensions { get; set; }
            public long TableID { get; set; }
        }

        /*
        private class TableYZDimValResult
        {
            public long TableID { get; set; }
            public string YDimVal { get; set; }
            public string ZDimVal { get; set; }
        }
         */

        void setupBeforeInstance()
        {
            // get expected namespace definitions
            // find prefixes and namespaces in DB
            IEnumerable<NamespaceDefinition> dpmPrefixedNamespaces =
                _conn.Query<NamespaceDefinition>("SELECT * FROM [vwGetNamespacesPrefixes]");

            if (dpmPrefixedNamespaces.Count<NamespaceDefinition>() == 0)
                throw new Exception("Error, namespace definitions, corresponding to the instance, were not found for " + xbrlFileName);
            foreach (NamespaceDefinition ns in dpmPrefixedNamespaces)
                dpmNsPrefixes[ns.Namespace] = ns.Prefix;
        }

        void startInstance()
        {
            if (asyncWorker != null)
                asyncWorker.ReportProgress(0, "Loading XBRL instance " + xbrlFileName);
            factCount = 0;
            itemCount = 0;
            Debug.WriteLine(string.Format("Loading XBRL instance {0} at {1}", xbrlFileName, DateTime.Now));
            if (moduleId == UNINITIALIZED)
                throw new ApplicationException("Instance does not reference a schema file for lookup in mModule table");
            // check if instance is already in database
            instanceId = _conn.ExecuteScalar<long>("SELECT InstanceID FROM dInstance WHERE FileName = ?", xbrlFileName);
            if (instanceId == 0)
            {
                // not there, add to dInstance
                _conn.Execute("INSERT INTO dInstance (ModuleID, FileName, CompressedFileBlob, Timestamp, " +
                                                     "EntityScheme, EntityIdentifier, PeriodEndDateOrInstant, EntityName, EntityCurrency) " +
                              "VALUES (?,?,?,?,?,?,?,?,?)",
                              new object[] {moduleId,
                                        xbrlFileName,
                                        null,
                                        DateTime.Now,
                                        entityScheme, entityIdentifier,
                                        dateUnionToString(periodInstantDate, true), 
                                        null, 
                                        entityCurrency});  // entityCurrency may be NULL at first and UPDATEd later
                instanceId = _conn.ExecuteScalar<long>("SELECT InstanceID FROM dInstance WHERE FileName = ?", xbrlFileName);
            }
            if (instanceId == 0)
                throw new ApplicationException(string.Format(
                    "Error inserting mModule {0} into dInstance table for schemaRef {1}",
                    moduleId, xbrlFileName));
            // clear any prior facts
            foreach (string tableName in new string[] { "dFact", "dFilingIndicator", "dInstanceLargeDimensionMember" })
            {
                _conn.Execute(string.Format("DELETE FROM {0} WHERE {0}.InstanceID = {1}",
                                            tableName,
                                            instanceId));
            }
            /*
            // get MetricAndDimensions
            IEnumerable<MetricAndDimensionsResult> result = _conn.Query<MetricAndDimensionsResult>(
                "SELECT MetricAndDimensions, TableID FROM mTableDimensionSet WHERE ModuleID = ?", moduleId);
            foreach (MetricAndDimensionsResult r in result)
            {
                tableIDs.Add(r.TableID);
                if (!metricAndDimensionsTableId.ContainsKey(r.MetricAndDimensions))
                    metricAndDimensionsTableId[r.MetricAndDimensions] = new HashSet<long>();
                metricAndDimensionsTableId[r.MetricAndDimensions].Add(r.TableID);
            }
            // get Tabls Y and Z DimVals
            IEnumerable<TableYZDimValResult> result2 = _conn.Query<TableYZDimValResult>(
                string.Format("SELECT TableID, YDimVal, ZDimVal FROM mTable WHERE TableID in ({0})", stringJoin(tableIDs, ", ")));
            foreach (TableYZDimValResult r in result2)
            {
                if (!string.IsNullOrEmpty(r.YDimVal))
                {
                    foreach (string dimVal in r.YDimVal.Split(new char[] { '|' }))
                    {
                        string[] dimMem = dimVal.Split(new char[] { '(', ')' });  // dim is [0] mem is [1]
                        if (!yDimVal.ContainsKey(r.TableID))
                            yDimVal[r.TableID] = new Dictionary<string, string>();
                        yDimVal[r.TableID][dimMem[0]] = dimMem[1];
                    }
                }
                if (!string.IsNullOrEmpty(r.ZDimVal))
                {
                    foreach (string dimVal in r.ZDimVal.Split(new char[] {'|'}))
                    {
                        string[] dimMem = dimVal.Split(new char[] {'(', ')'});  // dim is [0] mem is [1]
                        if (!zDimVal.ContainsKey(r.TableID)) 
                            zDimVal[r.TableID] = new Dictionary<string,string>();
                        zDimVal[r.TableID][dimMem[0]] = dimMem[1];
                    }
                }
            }
             */

        }

        class TemplateOrTableResult
        {
            public string TemplateOrTableCode { get; set; }
            public long TemplateOrTableID { get; set; }
        }

        void finishInstance()
        {
            if (asyncWorker != null)
                asyncWorker.ReportProgress(0, "Finishing XBRL instance " + xbrlFileName);

            if (hasFootnotes)
                logWarning("EIOPA.S.19",
                            "Footnotes SHOULD NOT be used");

            if (dFilingIndicators.Count == 0)
            {
                logError("EBA.1.6|EIOPA.1.6.a",
                         "No filing indicators were present in the instance");
            }
            else if (dFilingIndicators.Values.All<bool>(filed => filed == false))
            {
                logError("EBA.1.6|EIOPA.1.6.a",
                         "All filing indicators in the instance have filed=\"false\"");
            }
            else
            {
                // check filing indicators for validity
                IEnumerable<StrResult> resultFi = _conn.Query<StrResult>(
                    "select tot.TemplateOrTableCode as str from mTemplateOrTable tot where tot.TemplateOrTableType='Template'");
                HashSet<string> _filingIndicatorsInDB = new HashSet<string>();
                foreach (StrResult r in resultFi)
                    _filingIndicatorsInDB.Add(r.str);
                List<string> _invalidIndicators = new List<string>();
                bool anyFiledIndicatorIsInvalid = false;
                foreach (KeyValuePair<string, bool> _filingIndicator in dFilingIndicators)
                    if (!_filingIndicatorsInDB.Contains(_filingIndicator.Key))
                    {
                        _invalidIndicators.Add(_filingIndicator.Key);
                        if (_filingIndicator.Value)
                            anyFiledIndicatorIsInvalid = true;
                    }
                _filingIndicatorsInDB.Clear(); //dereference
                if (_invalidIndicators.Count > 0)
                {
                    logError("EIOPA.1.6.c",
                                string.Format("These filing indicator IDs were not found in the templates: {0}",
                                            stringJoinQuoted(_invalidIndicators, ",")));
                }
                if (!anyFiledIndicatorIsInvalid) // don't do filing indicator checks if some of them are invalid
                {

                    largeDimMemIds = new HashSet<int[]>(new ArrayEqCmpForInt2());

                    // perform metric and filing indicator checks after all facts loaded (because filing indicators could be last)
                    loadAllowedMetricsAndDims();  // reload allowed metrics and dims
                    IEnumerable<StrResult> result = _conn.Query<StrResult>(
                        "SELECT DataPointSignature AS str FROM dFact WHERE InstanceID = ?",
                        instanceId);
                    foreach (StrResult r in result)
                        validateFactSignature(r.str.Replace("''", "'"));  // xsi:nil='true' comes back as ''true''

                    foreach (int[] _dimIdMemId in largeDimMemIds)
                    {
                        _conn.Execute("INSERT INTO dInstanceLargeDimensionMember (InstanceID, DimensionID, MemberID) " +
                                        "VALUES (?,?,?)",
                                        new object[] { instanceId, _dimIdMemId[0], _dimIdMemId[1] });
                    }

                    // get all fact signatures in this instance

                    // get filing indicator template IDs
                    IEnumerable<TemplateOrTableResult> result2 = _conn.Query<TemplateOrTableResult>(
                        string.Format("SELECT mToT2.TemplateOrTableCode, mToT2.TemplateOrTableID " +
                                       "  FROM mModuleBusinessTemplate mBT, mTemplateOrTable mToT, mTemplateOrTable mToT2 " +
                                       "  WHERE mBT.ModuleID = {0} AND" +
                                       "        mToT.TemplateOrTableID = mBT.BusinessTemplateID AND" +
                                       "        mToT.ParentTemplateOrTableID = mToT2.TemplateOrTableID AND" +
                                       "        mToT2.TemplateOrTableCode in ({1})",
                                       moduleId,
                                       stringJoinQuoted(dFilingIndicators.Keys, ",", true))); // want all filing indicators, not just those with @filed=true
                    HashSet<string> templateOrTableCodes = new HashSet<string>();
                    HashSet<long> templateOrTableIDs = new HashSet<long>();
                    Dictionary<long,string> templateCodeForId = new Dictionary<long,string>();
                    foreach (TemplateOrTableResult r in result2)
                    {
                        templateOrTableCodes.Add(r.TemplateOrTableCode);
                        templateOrTableIDs.Add(r.TemplateOrTableID);
                        templateCodeForId[r.TemplateOrTableID] = r.TemplateOrTableCode;
                    }
                    // check for missing filing indicators
                    if (dFilingIndicators.Count != templateOrTableCodes.Count)
                    {
                        IEnumerable<string> missingIndicators = templateOrTableCodes.Except<string>(dFilingIndicators.Keys);
                        if (missingIndicators.Any())
                            logError("EBA.1.6|EIOPA.1.6.a",
                                     string.Format("The filing indicator IDs were not found for codes : {0}",
                                                   stringJoinQuoted(missingIndicators, ",")));
                        IEnumerable<string> extraneousIndicators = dFilingIndicators.Keys.Except<string>(templateOrTableCodes);
                        if (extraneousIndicators.Any())
                            logError(this.isEIOPAfullVersion ? "EIOPA.N.1.7.a" : "EIOPA.S.1.7.a",
                                     string.Format("The filing indicator IDs were not in scope for module : {0}",
                                                   stringJoinQuoted(extraneousIndicators, ",")));
                    }
                    // filing indicators
                    foreach (long templateOrTableID in templateOrTableIDs)
                    {
                        object _filed = null;
                        if (dFilingIndicators.ContainsKey(templateCodeForId[templateOrTableID]))
                            _filed = dFilingIndicators[templateCodeForId[templateOrTableID]];
                        _conn.Execute("INSERT INTO dFilingIndicator (InstanceID, BusinessTemplateID, Filed) " +
                                        "VALUES (?,?,?)", instanceId, templateOrTableID, _filed);
                    }
                }
            }
            /*
            // available table
            foreach (KeyValuePair<AvailableTableRowsKey,HashSet<string>> kv in availableTableRows)
            {
                AvailableTableRowsKey availableTableRowsKey = kv.Key;
                HashSet<string> setOfYDimVals = kv.Value;
                _conn.Execute("INSERT INTO dAvailableTable (InstanceID, TableID, ZDimVal, NumberOfRows) " +
                                "VALUES (?, ?, ?, ?)",
                                new object[] {instanceId,
                                              availableTableRowsKey.tableID,
                                              availableTableRowsKey.zDimKey,
                                              setOfYDimVals.Count});
            }
             */
            this._conn.Commit();
            this._conn.Close();
            this._conn.Dispose();

            if (unusedContextIDs.Count > 0)
            {
                string _unusedCntxIDs = string.Join(", ", unusedContextIDs.ToArray<string>());
                logWarning("EBA.2.7",
                           string.Format("Unused xbrli:context nodes SHOULD NOT be present in the instance: {0}", _unusedCntxIDs));
                logError("EIOPA.2.7",
                           string.Format("Unused xbrli:context nodes MUST NOT be present in the instance: {0}", _unusedCntxIDs));
            }
            if (unusedUnitIDs.Count > 0)
            {
                string _unusedUnitUDs = string.Join(", ", unusedUnitIDs.ToArray<string>());
                logWarning("EBA.2.22",
                           string.Format("Unused xbrli:unit nodes SHOULD NOT be present in the instance: {0}", _unusedUnitUDs));
                logError("EIOPA.2.22",
                           string.Format("Unused xbrli:unit nodes MUST NOT be present in the instance: {0}", _unusedUnitUDs));
            }
            if (unusedXmlnsPrefixes.Count > 0)
            {
                logWarning("EBA.3.4|EIOPA.3.4",
                           string.Format("There SHOULD be no unused prefixes but these were declared: {0}",
                                         string.Join(", ", unusedXmlnsPrefixes.ToArray<string>())));
            }
        }

    }
}
