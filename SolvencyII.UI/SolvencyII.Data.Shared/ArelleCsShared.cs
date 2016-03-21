using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;

/***>> note that we are using .net 3.5 which does not include System.Numerics.  The source is provided below.
using System.Numerics;
 ****/
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace SolvencyII.Data.Shared
{
    public class QName
    {
        public string namespaceURI, localName, prefix; // no prefix = "", no namdspace = ""

        public QName(string namespaceURI, string prefix, string localName)
        {
            this.namespaceURI = namespaceURI;
            this.prefix = prefix;
            this.localName = localName;
        }
        public QName(string namespaceURI, string localName)
        {
            this.namespaceURI = namespaceURI;
            this.prefix = null;
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

        public bool hasUndeclaredPrefix
        {
            get
            {
                return !string.IsNullOrEmpty(this.prefix) && this.namespaceURI == null;
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
                if (string.IsNullOrEmpty(this.localName))
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

    public class ArelleCsShared
    {
        public QName qnXlinkHref = new QName("http://www.w3.org/1999/xlink", "xlink", "href");
        public QName qnXbrliXbrl = new QName("http://www.xbrl.org/2003/instance", "xbrli", "xbrl");
        public QName qnXbrliContext = new QName("http://www.xbrl.org/2003/instance", "xbrli", "context");
        public QName qnXbrliUnit = new QName("http://www.xbrl.org/2003/instance", "xbrli", "unit");
        public QName qnXbrliPure = new QName("http://www.xbrl.org/2003/instance", "xbrli", "pure");
        public QName qnXbrliUnitNumerator = new QName("http://www.xbrl.org/2003/instance", "xbrli", "unitNumerator");
        public QName qnXbrliUnitDenominator = new QName("http://www.xbrl.org/2003/instance", "xbrli", "unitDenominator");
        public QName qnScheme = new QName("", "", "scheme");
        public QName qnContextRef = new QName("", "", "contextRef");
        public QName qnUnitRef = new QName("", "", "unitRef");
        public QName qnDecimals = new QName("", "", "decimals");
        public QName qnPrecision = new QName("", "", "precision");
        public QName qnXmlBase = new QName("http://www.w3.org/XML/1998/namespace", "xml", "base");
        public QName qnXmlLang = new QName("http://www.w3.org/XML/1998/namespace", "xml", "lang");
        public QName qnXsiNil = new QName("http://www.w3.org/2001/XMLSchema-instance", "xsi", "nil");
        public QName qnFindFilingIndicators = new QName("http://www.eurofiling.info/xbrl/ext/filing-indicators", "find", "fIndicators");
        public QName qnFindFilingIndicator = new QName("http://www.eurofiling.info/xbrl/ext/filing-indicators", "find", "filingIndicator");
        public QName qnFindFiled = new QName("http://www.eurofiling.info/xbrl/ext/filing-indicators", "find", "filed");

        public const string nsIso4217 = "http://www.xbrl.org/2003/iso4217";
        public const string nsXbrli = "http://www.xbrl.org/2003/instance";
        public const string nsXbrldi = "http://xbrl.org/2006/xbrldi";
        public const string nsLink = "http://www.xbrl.org/2003/linkbase";
        public const string nsXlink = "http://www.w3.org/1999/xlink";
        public const string nsXsi = "http://www.w3.org/2001/XMLSchema-instance";
        public const string nsFind = "http://www.eurofiling.info/xbrl/ext/filing-indicators";
        public const string nsXsd = "http://www.w3.org/2001/XMLSchema";

        public static Regex schemaRefDatePattern = new Regex(@".*/([0-9]{4}-[01][0-9]-[0-3][0-9])/.*");

        public bool isEBA = false;
        public bool isEIOPA = false;
        public bool isEIOPAfullVersion = false; // 2.0 filer manual "full" version
        public bool isEIOPA_2_0_1 = false; // 2.0.1 filer manual

        protected class NamespaceDefinition
        {
            public string Owner { get; set; }
            public string Prefix { get; set; }
            public string Namespace { get; set; }
        }

        // message log utilities

        StringBuilder errorMsgs = null;

        private string escapeXml(string xml)
        {
            string _xml;
            if (xml.Length > 32767)
                _xml = xml.Substring(0, 32767) + "...";
            else
                _xml = xml;
            return _xml.Replace("&", "&amp;").Replace("<", "&lt;").Replace("\"", "&quot;");
        }

        public void initializeLog()
        {
            this.errorMsgs = new StringBuilder();
            // start errors log
            this.errorMsgs.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            this.errorMsgs.AppendLine("<log>");
        }

        public void closeLog()
        {
            this.errorMsgs.AppendLine("</log>");
        }

        public string logToString()
        {
            return this.errorMsgs.ToString();
        }

        public void log(string level, string code, string message, string dpmSignature = null)
        {
            // codes can be EBA.n.n.n|EIOPA.n.n.n, if either EIOPA or EBA then code chosen corresponds to isEBA or isEIOPA
            if (code.Contains("|"))
            {
                string[] codes = code.Split(new char[] { '|' });
                code = null;
                foreach (string _code in codes)
                {
                    if ((isEBA && _code.StartsWith("EBA.")) || (isEIOPA && _code.StartsWith("EIOPA.")) ||
                        (!_code.StartsWith("EBA.") && !_code.StartsWith("EIOPA.")))
                    {
                        code = _code;
                        break;
                    }
                }
                if (code == null)
                    return;  // nothing to report in this case

            }
            // else single code
            else if ((isEBA && code.StartsWith("EIOPA.")) || (isEIOPA && code.StartsWith("EBA.")))
                return; // nothing to report in this case
            this.errorMsgs.AppendLine(string.Format("<entry code=\"{0}\" level=\"{1}\"><message>{2}</message>{3}</entry>",
                                                    code,
                                                    level,
                                                    escapeXml(message),
                                                    string.IsNullOrEmpty(dpmSignature)
                                                       ? ""
                                                       : string.Format("<ref dpmSignature=\"{0}\"/>", escapeXml(dpmSignature))));
        }

        public void logInfo(string code, string message, string dpmSignature = null)
        {
            this.log("info", code, message, dpmSignature);
        }

        public void logWarning(string code, string message, string dpmSignature = null)
        {
            this.log("warning", code, message, dpmSignature);
        }

        public void logError(string code, string message, string dpmSignature = null)
        {
            log("error", code, message, dpmSignature);
        }

        // md5 hash utilities

        // note: these utilities use a private implementation of BigInteger (below) because BigInteger (msft) is not in .NET 3.5

        static Md5Sum MD5SUM0 = new Md5Sum();

        public Md5Sum md5hash(params object[] args)
        {
            StringBuilder md5sb = new StringBuilder();
            Md5Sum nestedSum = new Md5Sum();


            foreach (object _arg in ((args.Length == 1 && (args[0] is List<object>)) ? (List<object>)args[0] : (IEnumerable<object>)args))
            {
                if (_arg is Md5Sum)
                {
                    nestedSum += (Md5Sum)_arg;
                }
                else
                {
                    if (md5sb.Length > 0)
                        md5sb.Append("\x1E");
                    if (_arg is QName)
                    {
                        QName _argQn = (QName)_arg;
                        if (!string.IsNullOrEmpty(_argQn.namespaceURI))
                        {
                            md5sb.Append(_argQn.namespaceURI);
                            md5sb.Append("\x1F");
                        }
                        md5sb.Append(_argQn.localName);
                    }
                    else if (_arg is string)
                    {
                        md5sb.Append((string)_arg);
                    }
                    else if (_arg is DateTime)
                    {
                        md5sb.Append(((DateTime)_arg).ToString("yyyy-MM-ddTHH:mm:ss"));
                    }
                    else if (_arg is ModelDimension)
                    {
                        md5sb.Append(((ModelDimension)_arg).typedContent);
                    }
                }
            }
            Md5Sum md5sum;
            if (md5sb.Length > 0)
            {
                MD5 md5 = MD5.Create();
                md5.ComputeHash(Encoding.UTF8.GetBytes(md5sb.ToString()));
                md5sum = new Md5Sum(md5.Hash);
            }
            else
                md5sum = MD5SUM0;
            if (nestedSum == MD5SUM0)
                return md5sum;
            return md5sum + nestedSum;
        }

        // LEI utilities
        public static int LEI_VALID = 0;
        public static int LEI_INVALID_LEXICAL = 1;
        public static int LEI_INVALID_CHECKSUM = 2;

        public static string[] LEI_RESULTS = { "valid", "invalid lexical", "invalid checksum" };

        static Regex leiLexicalPattern = new Regex(@"^\s*[0-9A-Z]{18}[0-9]{2}\s*$");
        static Dictionary<char, string> leiCharValue = new Dictionary<char, string>()
        {
            {'0',"0"}, {'1',"1"}, {'2',"2"}, {'3',"3"}, {'4',"4"}, {'5',"5"}, {'6',"6"}, {'7',"7"}, {'8',"8"}, {'9',"9"},
            {'A',"10"}, {'B',"11"}, {'C',"12"}, {'D',"13"}, {'E',"14"}, {'F',"15"}, {'G',"16"}, {'H',"17"}, {'I',"18"},
            {'J',"19"}, {'K',"20"}, {'L',"21"}, {'M',"22"}, {'N',"23"}, {'O',"24"}, {'P',"25"}, {'Q',"26"}, {'R',"27"},
            {'S',"28"}, {'T',"29"}, {'U',"30"}, {'V',"31"}, {'W',"32"}, {'X',"33"}, {'Y',"34"}, {'Z',"35"}
        };

        public int checkLei(string lei)
        {
            if (!leiLexicalPattern.IsMatch(lei))
                return LEI_INVALID_LEXICAL;
            StringBuilder sb = new StringBuilder();
            foreach (char c in lei)
            {
                sb.Append(leiCharValue[c]);
            }
            if (BigInteger.Parse(sb.ToString()) % 97 != 1)
                return LEI_INVALID_CHECKSUM;
            return LEI_VALID;
        }

        /* test case for checkLei
        public string testLei()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string[] test in new List<string[]> {
                        new string[] {"001GPB6A9XPE8XJICC14", "Fidelity Advisor Series I"},  //valid U.S. LEI
                        new string[] {"21380016W7GAG26FIJ74", "SOCIETE FRANCAISE ET SUISSE"},  //valid France LEI
                        new string[] {"213800A9GT65GAES2V60", "BARCLAYS SECURITIES JAPAN LIMITED"}, //valid Japan LEI
                        new string[] {"214800A9GT65GAES2V60", "Error 1"},  // checksum error
                        new string[] {"213800A9GT65GAE%2V60", "Error 2"},  // lexical error
                        new string[] {"213800A9GT65GAES2V62", "Error 3"},  // checksum error
                        new string[] {"1234", "Error 4"}})  // lexical error
            {
                sb.Append(string.Format("test LEI {0} result {1} name {2}{3}", test[0], LEI_RESULTS[checkLei(test[0])], test[1], Environment.NewLine));
            }
            return sb.ToString();
        }
        */
    }

    public class Md5Sum
    {
        // value array has hex number order (BigEndian)
        private byte[] value = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public Md5Sum(string hexNumber)
        {
            byte[] num = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            int byteNum = 15;
            for (int iHexPosition = hexNumber.Length - 2; iHexPosition >= 0 && byteNum < 16; iHexPosition -= 2)
            {
                num[byteNum] = byte.Parse(hexNumber.Substring(iHexPosition, 2), NumberStyles.AllowHexSpecifier);
                byteNum--;
            }
            this.setValue(num);
        }
        public Md5Sum(byte[] binaryValue)
        {
            this.setValue(binaryValue);
        }
        public Md5Sum()
        {
        }
        private void setValue(byte[] binaryValue)
        {
            // works only for Little Endian architecture (MD5 is probably always little endian)
            int byteNum = 0;
            foreach (byte b in binaryValue)
            {
                if (byteNum <= 15)
                    value[byteNum] = b;
                byteNum += 1;
            }
            for (; byteNum < 15; byteNum++)
                value[byteNum] = 0;
        }
        public string ToHex()
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in value)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
        public static Md5Sum operator +(Md5Sum left, Md5Sum right)
        {
            Md5Sum result = new Md5Sum();
            UInt16 carry = 0;
            for (int byteNum = 15; byteNum >= 0; byteNum--)
            {
                UInt16 bytesSum = (UInt16)(carry + left.value[byteNum] + right.value[byteNum]);
                result.value[byteNum] = (byte)(bytesSum & 0xFF);
                carry = (UInt16)(bytesSum >> 8);
            }
            return result;
        }
        public static bool operator ==(Md5Sum left, Md5Sum right)
        {
            for (int byteNum = 0; byteNum < 16; byteNum++)
            {
                if (left.value[byteNum] != right.value[byteNum])
                    return false;
            }
            return true;
        }
        public static bool operator !=(Md5Sum left, Md5Sum right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            uint hashSum = 2654435761;
            for (int byteNum = 15; byteNum >= 0; byteNum--)
            {
                hashSum += this.value[byteNum];
                hashSum *= 2654435761;
            }
            return (int)hashSum;
        }

        public override bool Equals(object obj)
        {
            if (obj is Md5Sum)
                return this == (Md5Sum)obj;
            return base.Equals(obj);
        }
       
     
       
    }
}
