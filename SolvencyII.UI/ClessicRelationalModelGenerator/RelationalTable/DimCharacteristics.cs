using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.Model
{
    internal class DimCharacteristic
    {        
        string dimCode;
        public string DimCode
        {
            get { return dimCode; }
            set { dimCode = value; }
        }

        string domCode;
        public string DomCode
        {
            get { return domCode; }
            set { domCode = value; }
        }

        readonly HashSet<string> memCodes = new HashSet<string>();
        public HashSet<string> MemCodes
        {
            get { return memCodes; }
        }

        string memPrefix;
        public string MemPrefix
        {
            get { return memPrefix; }
            set { memPrefix = value; }
        }

        internal string getDimCode()
        {
            string dimCode = this.dimCode;

            if (this.memCodes.Count == 0)
            {
                dimCode = dimCode + "(*)";
            }
            else if (this.memCodes.Count == 1)
            {
                dimCode = dimCode + "("+(string.IsNullOrWhiteSpace(memCodes.ToList()[0]) ? "*":memCodes.ToList()[0])+")";
            }
            else
            {
                dimCode = dimCode + "(";
                foreach (string memCode in memCodes)
                {
                    dimCode = dimCode + "|" + memCode;
                }

                dimCode = dimCode + ")";
            }

            return dimCode;
        }
    }
}
