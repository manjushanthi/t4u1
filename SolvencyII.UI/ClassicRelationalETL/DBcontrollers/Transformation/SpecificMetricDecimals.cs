using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Transformation
{
    class SpecificMetricDecimals
    {

        HashSet<string> specificMetrics = new HashSet<string>()
        {
            "MET(s2md_met:mi1110)"
,"MET(s2md_met:mi1128)"
,"MET(s2md_met:mi1131)"
,"MET(s2md_met:mi1987)"
,"MET(s2md_met:mi1126)"
,"MET(s2md_met:mi1096)"
,"MET(s2md_met:mi1101)"
,"MET(s2md_met:mi1112)"
,"MET(s2md_met:mi1115)"
,"MET(s2md_met:mi1127)"
,"MET(s2md_met:mi1812)"
,"MET(s2md_met:mi1813)"
,"MET(s2md_met:mi1988)"
,"MET(s2md_met:mi1989)"
,"MET(s2md_met:mi1990)"
,"MET(s2md_met:mi1129)"
,"MET(s2md_met:mi1132)"
,"MET(s2md_met:mi2376)"
,"MET(s2md_met:mi1626)"
,"MET(s2md_met:mi2235)"
        };

        internal bool isSpecific(string metCode)
        {
            return specificMetrics.Contains(metCode);
        }

        public string Decimals { get { return "2"; } }
    }
}
