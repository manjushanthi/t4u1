using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL.Model
{
    /// <summary>
    /// Represents single row in dFact table
    /// </summary>
    public class dFact
    {
        public int? dFactId = null;
        public int instanceId;

        public string textValue;
        public DateTime? dateTimeValue = null;
        public bool? boolValue = null;
        public Decimal? numericValue = null;

        public string dataPointSignature;
        public string dataPointSignatureWithWildCards;
        private int _dimCodesNumber = -1;
        private string _unit = "unit";

        /// <summary>
        /// Gets or sets the unit.
        /// </summary>
        /// <value>
        /// The unit.
        /// </value>
        public string unit
        {
            get
            {
                if (this.numericValue == null)
                    return null;
                else return _unit;
            }
            set
            {
                _unit = value;
            }
        }
        private string _decimals;
        /// <summary>
        /// Gets or sets the decimals.
        /// </summary>
        /// <value>
        /// The decimals.
        /// </value>
        public string decimals
        {
            get
            {
                if (this.numericValue == null)
                    return null;
                else if (!string.IsNullOrEmpty(_decimals))
                    return _decimals;
                else if (!string.IsNullOrEmpty(this.metCode) && this.metCode.Contains("_met:p"))
                    return "4";

                return "2";
            }
            set
            {
                _decimals = value;
            }
        }
        /// <summary>
        /// Gets the number format information.
        /// </summary>
        /// <value>
        /// The number format information.
        /// </value>
        public static DateTimeFormatInfo numberFormatInfo
        {
            get
            {
                return DateTimeFormatInfo.InvariantInfo;
                //NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                //nfi.NumberDecimalSeparator = ".";
                //nfi.NumberGroupSeparator = "";
                //return nfi;
            }
        }
        /// <summary>
        /// Gets the date time format.
        /// </summary>
        /// <value>
        /// The date time format.
        /// </value>
        public static string dateTimeFormat
        {
           get
           {
               return "yyyy-MM-dd";
           }
        }

        public string metCode = "";
        public Dictionary<string, string> dimensionsMembers;
        HashSet<string> _dimCodes;

        /// <summary>
        /// Initializes a new instance of the <see cref="dFact"/> class.
        /// </summary>
        /// <param name="FactId">The fact identifier.</param>
        /// <param name="textValue">The text value.</param>
        /// <param name="dateTimeValue">The date time value.</param>
        /// <param name="boolValue">The bool value.</param>
        /// <param name="numericValue">The numeric value.</param>
        /// <param name="DataPointSignature">The data point signature.</param>
        /// <param name="DataPointSignatureWithValuesForWildcards">The data point signature with values for wildcards.</param>
        /// <param name="istanceId">The istance identifier.</param>
        public dFact(int FactId, string textValue, DateTime? dateTimeValue, bool? boolValue, decimal? numericValue, string DataPointSignature, string DataPointSignatureWithValuesForWildcards, int istanceId)
        {
            this.dFactId = FactId;
            this.textValue = textValue;
            this.dateTimeValue = dateTimeValue;
            this.boolValue = boolValue;
            this.numericValue = numericValue;
            this.instanceId = istanceId;
            this.dataPointSignature = DataPointSignature;

            //if (string.IsNullOrEmpty(textValue) && this.dateTimeValue == null && this.numericValue == null && this.boolValue == null)
            //    throw new EtlException("No value was provided for fact " + (dFactId == null ? "" : dFactId.ToString()));

            dimensionsMembers = new Dictionary<string, string>();
            this.mapDimCodes(DataPointSignature, DataPointSignatureWithValuesForWildcards);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="dFact"/> class.
        /// </summary>
        /// <param name="FactId">The fact identifier.</param>
        /// <param name="DataPointSignature">The data point signature.</param>
        /// <param name="istanceId">The istance identifier.</param>
        /// <param name="value">The value.</param>
        /// <param name="pUnit">The p unit.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <exception cref="EtlException">No value was provided for fact  + dFactId == null ?  : dFactId.ToString()</exception>
        public dFact(int FactId, string DataPointSignature, int istanceId, object value, string pUnit, string dataType)
        {
            this.dFactId = FactId;
            this.instanceId = istanceId;
            this.dataPointSignature = DataPointSignature;


            if(value is string)
                this.setValue(value as string, dataType);
            else
                this.setValue(value, dataType);

            if (string.IsNullOrEmpty(textValue) && this.dateTimeValue == null && this.numericValue == null && this.boolValue == null)
                throw new EtlException("No value was provided for fact " + dFactId == null ? "" : dFactId.ToString());

            this.setUnit(pUnit);
            dimensionsMembers = new Dictionary<string, string>();
            this.mapDimCodes(DataPointSignature, null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="dFact"/> class.
        /// </summary>
        /// <param name="FactId">The fact identifier.</param>
        /// <param name="DataPointSignature">The data point signature.</param>
        /// <param name="istanceId">The istance identifier.</param>
        /// <param name="value">The value.</param>
        /// <param name="pUnit">The p unit.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <exception cref="EtlException">No value was provided for fact  + dFactId == null ?  : dFactId.ToString()</exception>
        public dFact(int FactId, string DataPointSignature, int istanceId, string value, string pUnit, string dataType)
        {
            this.dFactId = FactId;
            this.instanceId = istanceId;
            this.dataPointSignature = DataPointSignature;

            this.setValue(value, dataType);
            if (string.IsNullOrEmpty(textValue) && this.dateTimeValue == null && this.numericValue == null && this.boolValue == null)
                throw new EtlException("No value was provided for fact " + dFactId == null ? "" : dFactId.ToString());

            this.setUnit(pUnit);
            dimensionsMembers = new Dictionary<string, string>();
            this.mapDimCodes(DataPointSignature, null);
        }

        /// <summary>
        /// Sets the unit.
        /// </summary>
        /// <param name="pUnit">The p unit.</param>
        private void setUnit(string pUnit)
        {
            this.unit = (pUnit.StartsWith("iso4217") || pUnit.Contains(":") || pUnit.Equals("pure")) ? pUnit : string.Format("iso4217:{0}", pUnit);

            if (this.numericValue == null && this._unit.StartsWith("iso4217"))
                this.unit = null;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dataType">Type of the data.</param>
        /// <exception cref="System.ArgumentException">
        /// Not monetary fact value
        /// or
        /// Not monetary fact value
        /// or
        /// Not string fact value
        /// or
        /// Not percentage fact value
        /// or
        /// Not boolean fact value
        /// or
        /// Not percentage fact value
        /// or
        /// Not string fact value
        /// </exception>
        /// <exception cref="EtlException">
        /// Not date fact value
        /// or
        /// Not date fact value
        /// </exception>
        private void setValue(object value, string dataType)
        {
            switch (dataType)
            {
                case "M":
                    if (!(value is int || value is decimal))
                        throw new ArgumentException("Not monetary fact value");
                    this.numericValue = decimal.Parse(value.ToString());
                    setMonetaryDecimal();
                    break;
                case "N":
                    if (!(value is int || value is decimal))
                        throw new ArgumentException("Not monetary fact value");
                    this.numericValue = decimal.Parse(value.ToString());
                    setNumericDecimal();
                    break;
                case "S":
                    if (!(value is string))
                        throw new ArgumentException("Not string fact value");
                    this.textValue = value as string;
                    break;
                case "P":
                    if (!(value is int || value is decimal))
                        throw new ArgumentException("Not percentage fact value");
                    this.numericValue = decimal.Parse(value.ToString());
                    this._decimals = "4";
                    break;
                case "B":
                    if (!(value is bool || value is int || value is string))
                        throw new ArgumentException("Not boolean fact value");
                    if (value is bool) this.boolValue = (bool)value;
                    else if (value is int) this.boolValue = (int)value == 1;
                    else if (value is string) this.boolValue = value.Equals("1");
                    break;
                case "D":
                    if (value is DateTime)
                        this.dateTimeValue = (DateTime)value;
                    else if (value is string)
                        try
                        {
                            this.dateTimeValue = DateTime.ParseExact(value as string, dateTimeFormat, numberFormatInfo);
                        }
                        catch (Exception ex)
                        {
                            throw new EtlException("Not date fact value", ex);
                        }
                    else
                        throw new EtlException("Not date fact value");
                    break;
                case "I":
                    if (!(value is int || value is decimal))
                        throw new ArgumentException("Not percentage fact value");
                    this.numericValue = decimal.Parse(value.ToString());
                    this._decimals = "2";
                    break;
                case "E":
                    if (!(value is string))
                        throw new ArgumentException("Not string fact value");
                    this.textValue = value as string;
                    break;
                default:
                    setValue(value.ToString());
                    break;
            }
        }

        /// <summary>
        /// Sets the numeric decimal.
        /// </summary>
        private void setNumericDecimal()
        {
            if (this.numericValue == null) return;
            int dec = 0;
            if (Math.Abs((decimal)this.numericValue) >= (decimal)1.0) dec = 0;
            else if (Math.Abs((decimal)this.numericValue) >= (decimal)0.1) dec = 1;
            else if (Math.Abs((decimal)this.numericValue) >= (decimal)0.01) dec = 2;
            else if (Math.Abs((decimal)this.numericValue) >= (decimal)0.001) dec = 3;
            else dec = 4;
            this._decimals = dec.ToString();
        }

        /// <summary>
        /// Sets the monetary decimal.
        /// </summary>
        private void setMonetaryDecimal()
        {
            if (this.numericValue == null) return;
            int dec = 0;
            if (Math.Abs((decimal)this.numericValue) >= 1000) dec = -3;
            else if (Math.Abs((decimal)this.numericValue) >= (decimal)100) dec = -2;
            else if (Math.Abs((decimal)this.numericValue) >= (decimal)10) dec = -1;
            else if (Math.Abs((decimal)this.numericValue) >= (decimal)1.0) dec = 0;
            else if (Math.Abs((decimal)this.numericValue) >= (decimal)0.1) dec = 1;
            else dec = 2;
            this._decimals = dec.ToString();
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dataType">Type of the data.</param>
        private void setValue(string value, string dataType)
        {
            switch (dataType)
            {
                case "M":
                    this.numericValue = decimal.Parse(value);
                    setMonetaryDecimal();
                    break;
                case "N":
                    this.numericValue = decimal.Parse(value);
                    setNumericDecimal();
                    break;
                case "S":
                    this.textValue = value;
                    break;
                case "P":
                    this.numericValue = decimal.Parse(value);
                    this._decimals = "4";
                    break;
                case "B":
                    this.boolValue = value.Equals("1");
                    break;
                case "D":
                    this.dateTimeValue = DateTime.Parse(value);
                    break;
                case "I":
                    this.numericValue = decimal.Parse(value);
                    this._decimals = "0";
                    break;
                case "E":
                    this.textValue = value;
                    break;
                default:
                    setValue(value);
                    break;
            }
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        private void setValue(string value)
        {
            bool bVal;
            decimal dVal;
            DateTime dtVal;

            if (bool.TryParse(value, out bVal))
                this.boolValue = bVal;
            else if (DateTime.TryParse(value, out dtVal))
                this.dateTimeValue = dtVal;
            else if (decimal.TryParse(value, out dVal))
                this.numericValue = dVal;
            else
                this.textValue = value;
        }

        public override int GetHashCode()
        {
            return (this.instanceId + this.dataPointSignature).GetHashCode();            
        }

        /// <summary>
        /// Maps the dim codes.
        /// </summary>
        /// <param name="DataPointSignature">The data point signature.</param>
        /// <param name="DataPointSignatureWithValuesForWildcards">The data point signature with values for wildcards.</param>
        private void mapDimCodes(string DataPointSignature, string DataPointSignatureWithValuesForWildcards)
        {
            if (!string.IsNullOrEmpty(DataPointSignatureWithValuesForWildcards))
                parseDimCodes(DataPointSignatureWithValuesForWildcards);
            else
                parseDimCodes(DataPointSignature);
        }

        /// <summary>
        /// Parses the dim codes.
        /// </summary>
        /// <param name="DataPointSignature">The data point signature.</param>
        private void parseDimCodes(string DataPointSignature)
        {
            dimensionsMembers.Clear();
            string[] codes = DataPointSignature.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);                        
            string[] dcCodes;
            for (int i = 0; i < codes.Count(); i++)
            {
                dcCodes = codes[i].Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                if (dcCodes[0] == "MET")
                {
                    this.metCode = "MET(" + dcCodes[1] + ")";
                    continue;
                }
                dimensionsMembers.Add(dcCodes[0], parseMemCode(dcCodes[1]));
            }
        }

        private string parseMemCode(string memberCode)
        {
            return memberCode;

            //string[] memCodes = memberCode.Split(new char[]{'<', '>'}, StringSplitOptions.RemoveEmptyEntries);

            //if (memCodes.Count() == 3)
            //    return memCodes[1];

            //return memCodes[0];
        }



        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <returns></returns>
        internal string getStringValue()
        {
            if (!string.IsNullOrEmpty(this.textValue))
                return textValue;
            if (this.dateTimeValue != null)
                return ((DateTime)dateTimeValue).ToString(dateTimeFormat, numberFormatInfo);
            if (numericValue != null)          
                return Convert.ToString(numericValue, numberFormatInfo);
            if (this.boolValue != null)
                return boolValue == true ? "1" : "0";

            return null;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <returns></returns>
        internal object getValue()
        {
            if (boolValue != null)
                return boolValue == true ? 1 : 0;
            if (numericValue != null)
                return (decimal)numericValue;
            if (dateTimeValue != null)
                return (DateTime)dateTimeValue;
            if (!string.IsNullOrEmpty(this.textValue))
                return textValue;

            return null;
        }

        /// <summary>
        /// Gets the dim codes.
        /// </summary>
        /// <value>
        /// The dim codes.
        /// </value>
        public HashSet<string> DimCodes
        {
            get
            {
                if (_dimCodes == null)                
                    _dimCodes = produceDimCodes();

                return _dimCodes;
            }
        }

        private HashSet<string> produceDimCodes()
        {
            HashSet<string> dimCodes = new HashSet<string>();
            dimCodes.Add(this.metCode);
            string dimCode;
            foreach (KeyValuePair<string, string> kvp in this.dimensionsMembers)
            {
                if (kvp.Value.Contains("<"))
                    dimCode = kvp.Key + "(*)";
                else
                    dimCode = kvp.Key + "(" + kvp.Value + ")";
                
                dimCodes.Add(dimCode);
            }

            return dimCodes;
        }

        internal string GetDataPointSignature()
        {
            StringBuilder builder = new StringBuilder();

            string metCode = DimCodes.SingleOrDefault(x => x.Contains(EtlGlobals.MetDimCode));
            if (string.IsNullOrEmpty(metCode))
                metCode = DimCodes.Single(x => x.Contains(EtlGlobals.AtyDimCode));

            builder.Append(metCode);
            foreach (string  dimCode in DimCodes.OrderBy(x=>x))
            {
                if (!dimCode.Equals(metCode))
                    builder.Append(dimCode);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Gets the dim codes number.
        /// </summary>
        /// <value>
        /// The dim codes number.
        /// </value>
        public int DimCodesNumber
        {
            get
            {
                if (_dimCodesNumber != -1)
                    return _dimCodesNumber;

                _dimCodesNumber = this._dimCodes.Count();
                if (!string.IsNullOrEmpty(this.metCode) && !_dimCodes.Contains(metCode))
                    _dimCodesNumber = _dimCodesNumber + 1;

                return _dimCodesNumber;
            }
        }


    }
}
