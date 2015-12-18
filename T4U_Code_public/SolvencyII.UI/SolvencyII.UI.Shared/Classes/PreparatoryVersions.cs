using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.UI.Shared.Classes
{
    public static class Extensions
    {
        public static string EnumExtendedName(this PreParatoryVersions e)
        {
            switch (e)
            {
                case PreParatoryVersions.SecondVersion:
                    return "1.5.2(b)";
                case PreParatoryVersions.ThirdVersion:
                    return "1.5.2(c)";               
            }
            return "NotApplicable";
        }
    }

    public enum PreParatoryVersions
    {        
        SecondVersion,
        ThirdVersion,
        NotApplicable

    }

    
}
