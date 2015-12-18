using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.ExcelTemplateProcessor
{
    public class GreyedOutOrdinatesPairsList
    {       

        #region Properties

        private static HashSet<Tuple<int, int>> _GreyedOutOrdinatesPairsTable;

        public static HashSet<Tuple<int, int>> GreyedOutOrdinatesPairsTable
        {
            get
            {
                return _GreyedOutOrdinatesPairsTable;
            }
            set
            {
                _GreyedOutOrdinatesPairsTable = value;
            }
        }

        #endregion

        #region Construct

        public  GreyedOutOrdinatesPairsList()
        {

        }

        #endregion
    }
}
