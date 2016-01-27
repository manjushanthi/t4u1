/*
 * Arelle streaming loader in C# with no DTS validation
 * 
 * author: Mark V Systems Limited
 * (c) Copyright 2014 Mark V Systems Limited, All rights reserved.
 * 
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using SolvencyII.Data.SQLite;

namespace SolvencyII.UI.Shared.Arelle
{
    class QName
    {
        public string namespaceURI, localName, prefix; // no prefix = "", no namdspace = ""

        public QName(string namespaceURI, string prefix, string localName)
        {
            this.namespaceURI = namespaceURI;
            this.prefix = prefix;
            this.localName = localName;
        }
        public QName(XmlReader reader, string prefixedName)
        {
            string[] nameParts = prefixedName.Trim().Split(new Char[] { ':' });
            if (nameParts.Count() == 2)
            {
                this.namespaceURI = reader.LookupNamespace(nameParts[0]);
                this.prefix = nameParts[0];
                this.localName = nameParts[1];
            }
            else if (nameParts.Count() == 1)
            {
                this.namespaceURI = reader.LookupNamespace("");
                this.prefix = "";
                this.localName = nameParts[0];
            }
            else
            {
                this.namespaceURI = this.prefix = this.localName = "";
            }
        }

        public string clarkName
        {
            get 
            { 
                if (string.IsNullOrEmpty(this.namespaceURI))
                    return this.localName;
                else
                    return string.Format("{{{0}}}{1}", this.namespaceURI, this.localName); 
            }
        }

        public string prefixedName
        {
            get 
            { 
                if (string.IsNullOrEmpty(this.prefix))
                    return this.localName;
                else
                    return string.Format("{0}:{1}", this.prefix, this.localName); 
            }
        }
        public override bool Equals(object obj)
        {
            return this.Equals(obj as QName);
        }

        public bool Equals(QName other)
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
            return (this.namespaceURI == other.namespaceURI) && (this.localName == other.localName);
        }

        public override int GetHashCode()
        {
            if (string.IsNullOrEmpty(this.namespaceURI))
                if (string.IsNullOrEmpty(this.namespaceURI))
                    return 0;
                else
                    return this.localName.GetHashCode();
            return this.namespaceURI.GetHashCode() * 0x00010000 + this.localName.GetHashCode();
        }

        public static bool operator ==(QName lhs, QName rhs)
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

        public static bool operator !=(QName lhs, QName rhs)
        {
            return !(lhs == rhs);
        }

    }

    class ModelXmlElement
    {
        public string id, value = "";
        public QName tag;
        public Dictionary<QName, ModelXmlAttribute> attributes = new Dictionary<QName, ModelXmlAttribute>();

        public ModelXmlElement(XmlReader reader)
        {
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
    }

    class ModelXmlProcessingInstruction
    {
        public string name;
        public Dictionary<string, string> attributes = new Dictionary<string, string>();

        public ModelXmlProcessingInstruction(XmlReader reader)
        {
            name = reader.Name;
            XmlReader attrRdr = XmlReader.Create(new System.IO.MemoryStream(UTF8Encoding.UTF8.GetBytes(
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

        public ModelXbrl(XmlReader reader): base(reader)
        {
        }
    }

    class ModelContext : ModelXmlElement
    {
        public DateTime startDate, endDate, instantDate;
        public string entityScheme, entityIdentifier;
        public Boolean isForever;
        public List<ModelDimension> dimensions = new List<ModelDimension>();

        public ModelContext(XmlReader reader): base(reader)
        {
        }
    }

    class ModelDimension : ModelXmlElement
    {
        static QName qnDimension = new QName("", "", "dimension");
        public Boolean isTyped;
        public QName dimensionName, memberName;
        public string typedValue;

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

        public ModelFact(XmlReader reader): base(reader)
        {
        }
    }

    class XbrlStreamingParser
    {
        public QName qnXlinkHref = new QName("http://www.w3.org/1999/xlink", "xlink", "href");
        public QName qnXbrliXbrl = new QName("http://www.xbrl.org/2003/instance", "xbrli", "xbrl");
        public QName qnXbrliContext = new QName("http://www.xbrl.org/2003/instance", "xbrli", "context");
        public QName qnXbrliUnit = new QName("http://www.xbrl.org/2003/instance", "xbrli", "unit");
        public QName qnXbrliUnitNumerator = new QName("http://www.xbrl.org/2003/instance", "xbrli", "unitNumerator");
        public QName qnXbrliUnitDenominator = new QName("http://www.xbrl.org/2003/instance", "xbrli", "unitDenominator");
        public QName qnScheme = new QName("", "", "scheme");
        public QName qnContextRef = new QName("", "", "contextRef");
        public QName qnUnitRef = new QName("", "", "unitRef");
        public QName qnDecimals = new QName("", "", "decimals");
        public QName qnXsiNil = new QName("http://www.w3.org/2001/XMLSchema-instance", "xsi", "nil");

        public string[] xmlBoolTrueValues = { "true", "1" };

        string _sqlConnectionPath;
        SQLiteConnection _conn;


        public XbrlStreamingParser(string sqlConnectionPath)
        {
            this._sqlConnectionPath = sqlConnectionPath;
        }

        static ModelXmlElement elementFactory(XmlReader reader, List<ModelXmlElement> elementStack)
        {
            switch (reader.NamespaceURI)
            {
                case "http://www.xbrl.org/2003/instance":
                switch (reader.LocalName)
                {
                    case "xbrl":
                        return new ModelXbrl(reader);
                    case "context":
                        return new ModelContext(reader);
                    case "unit":
                        return new ModelUnit(reader);
                    default:
                        return new ModelXmlElement(reader);
                }
                case "http://www.xbrl.org/2003/linkbase":
                switch (reader.LocalName)
                {
                    default:
                        return new ModelXmlElement(reader);
                }
                case "http://xbrl.org/2006/xbrldi":
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
                    return new ModelFact(reader);
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

        public void parseXbrl(string xbrlFile)
        {

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Fragment;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(xbrlFile, settings);
            List<ModelXmlElement> elementStack = new List<ModelXmlElement>();
            bool isStreamingMode = false;
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
            while (reader.Read())
            {
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
                                        elt.tag.prefixedName, xbrlFile));
                                }
                                else
                                {
                                    startInstance(xbrlFile);
                                }
                            }
                            elementStack.Add(elt); // push current element

                            if (!reader.IsEmptyElement) // an empty StartElement is processed as if it were an EndElement
                                break;
                        }
                        elt = elementStack[elementStack.Count - 1];
                        parentElt = (elementStack.Count() >= 2) ? elementStack[elementStack.Count - 2] : null;
                        if (elt is ModelContext)
                        {
                            contexts.Add(elt as ModelContext);
                            contextById[elt.id] = elt as ModelContext;
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
                            }
                            cntx = elementStack[1] as ModelContext;
                            cntx.dimensions.Add(dim);
                        }
                        else if (parentElt is ModelDimension && (parentElt as ModelDimension).isTyped)
                        {
                            ModelDimension dim = parentElt as ModelDimension;
                            if (elt.attributes.ContainsKey(qnXsiNil) && xmlBoolTrueValues.Contains(elt.attributes[qnXsiNil].value))
                                dim.typedValue = "nil";  // used by DPM database keys
                            else // this is only good for non-complex typed dimensions
                                dim.typedValue = string.Format("<{0}>{1}</{0}>", elt.tag.prefixedName, elt.value);
                        }
                        else if (elt is ModelUnit)
                        {
                            units.Add(elt as ModelUnit);
                            unitById[elt.id] = elt as ModelUnit;
                            if (isStreamingMode && units.Count() > contextBuffer)
                            {
                                unitById.Remove(units[0].id);
                                units.RemoveAt(0);
                            }
                        }
                        else if (elt.tag.namespaceURI == "http://www.xbrl.org/2003/instance")
                            switch (elt.tag.localName)
                            {
                                case "identifier":
                                    cntx = elementStack[1] as ModelContext;
                                    cntx.entityScheme = elt.attributes[qnScheme].value;
                                    cntx.entityIdentifier = elt.value;
                                    break;
                                case "instant":
                                case "endDate":
                                    cntx = elementStack[1] as ModelContext;
                                    cntx.endDate = xbrlDateUnionValue(elt.value, true);
                                    break;
                                case "startDate":
                                    cntx = elementStack[1] as ModelContext;
                                    cntx.startDate = xbrlDateUnionValue(elt.value);
                                    break;
                                case "forever":
                                    cntx = elementStack[1] as ModelContext;
                                    cntx.isForever = true;
                                    break;
                                case "measure":
                                    QName qnMeasure = new QName(reader, elt.value);
                                    if (parentElt.tag == qnXbrliUnitNumerator)
                                    {
                                        unit = elementStack[elementStack.Count - 3] as ModelUnit;
                                        unit.multMeasures.Add(qnMeasure);
                                    }
                                    else if (parentElt.tag == qnXbrliUnitDenominator)
                                    {
                                        unit = elementStack[elementStack.Count - 3] as ModelUnit;
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
                                            processFact(f);
                                        }
                                    }
                                    facts.Clear();  // dereference all facts
                                    contexts.Clear();
                                    units.Clear();
                                    contextById.Clear();
                                    unitById.Clear();
                                    finishInstance(); // final processing of instance
                                    break;
                            }
                        else if (elt.tag.namespaceURI == "http://www.xbrl.org/2003/linkbase" && elt.tag.localName == "schemaRef")
                        {
                            schemaRef = elt.attributes[qnXlinkHref].value;
                            processSchemaRef(schemaRef);
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
                                if (isStreamingMode)
                                {
                                    processFact(fact);
                                }
                                else
                                {
                                    if ((string.IsNullOrEmpty(fact.contextRef) || contextById.ContainsKey(fact.contextRef)) &&
                                        (string.IsNullOrEmpty(fact.unitRef) || unitById.ContainsKey(fact.unitRef)))
                                    {   // fact context and unit are resolved, process fact
                                        processFact(fact);
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
                            catch (FormatException)
                            {
                            }
                        }
                        break;
                    case XmlNodeType.Comment:
                        break;
                    case XmlNodeType.XmlDeclaration:
                        break;
                    case XmlNodeType.Document:
                        break;
                    case XmlNodeType.DocumentType:
                        break;
                    case XmlNodeType.EntityReference:
                        break;
                }
            }
        }

        void startInstance(string xbrlFile)
        {
            this._conn = new SQLiteConnection(this._sqlConnectionPath);
        }

        private class IdResult
        {
            public int id { get; set; }
        }

        private class StrResult
        {
            public string str { get; set; }
        }

        void processSchemaRef(string schemaFile)
        {

            IEnumerable<IdResult> result = _conn.Query<IdResult>(string.Format("SELECT ModuleID FROM mModule WHERE XBRLSchemaRef = '{0}'", schemaFile));
            foreach (IdResult id in result)
            {

            }
        }

        void processFact(ModelFact fact)
        {
            debugFact(fact);
        }

        void debugFact(ModelFact fact, string indent="")
        {
            // process a fact that has all of its data
            System.Diagnostics.Debug.WriteLine(string.Format("{0}Fact {1} cntx={2} unit={3} dec={4} value={5}", 
                indent, fact.tag.prefixedName, fact.contextRef, fact.unitRef, fact.decimals, fact.value));
            // show context
            if (fact.context != null)
            {
                if (fact.context.startDate != DateTime.MinValue)
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}    startDate={1}", indent, fact.context.startDate.ToString("o")));
                if (fact.context.endDate != DateTime.MinValue)
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}    endInstantDate={1}", indent, fact.context.endDate.ToString("o")));
                System.Diagnostics.Debug.WriteLine(string.Format("{0}    scheme={1} ident={2}", indent, fact.context.entityScheme, fact.context.entityIdentifier));
                foreach (ModelDimension dim in fact.context.dimensions)
                {
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}    dim={1} val={2}", indent, dim.dimensionName.prefixedName, 
                        (dim.isTyped) ? dim.typedValue : dim.memberName.prefixedName));
                }
            }
            if (fact.unit != null)
            {
                foreach (QName qnMeas in fact.unit.multMeasures)
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}    unit mult meas={1}", indent, qnMeas.prefixedName));
                foreach (QName qnMeas in fact.unit.divMeasures)
                    System.Diagnostics.Debug.WriteLine(string.Format("{0}    unit div meas={1}", indent, qnMeas.prefixedName));
            }
            foreach (ModelFact childFact in fact.tupleFacts)
                debugFact(childFact, indent = indent + "    ");
        }

        void finishInstance()
        {
            System.Diagnostics.Debug.WriteLine(string.Format("Finish instance"));
        }

    }
}
