using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AT2DPM.DAL.Model
{
    public partial class mLanguage
    {
        public override string ToString()
        {
            return this.LanguageName;
        }

        public static mLanguage GetDefaultLanguage()
        {
            mLanguage lan = new mLanguage 
            {
                LanguageName = "English", 
                EnglishName="English", 
                IsoCode= "en" 
            };

            return lan;
        }
    }
}
