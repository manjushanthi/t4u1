using System.Collections.Generic;
using SolvencyII.Domain.Interfaces;

namespace SolvencyII.Domain
{
    /// <summary>
    /// Generics Delegates used throughout T4U 
    /// </summary>
    public class GenericDelegates
    {
        public delegate void StringResponse(string response);

        public delegate void BoolResponse(bool response);

        public delegate void BoolResponseWithPkID(bool response, int pkID, List<ISolvencyComboBox> parentCombos);

        public delegate void Response();

        public delegate void TwoInts(int one, int two);

        public delegate bool BoolResult();

        public delegate bool BoolResultQuestion(string question);

        public delegate bool ListLongs(List<long> pKeys);

        public delegate void SolvencyControlChanged(object sender, string colName);

        public delegate void DisplayDimensions(object sender, string colName);

    }
}
