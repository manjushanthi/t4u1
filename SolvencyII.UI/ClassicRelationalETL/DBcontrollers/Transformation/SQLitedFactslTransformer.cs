using SolvencyII.Data.CRT.ETL.DataConnectors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers
{
    class SQLitedFactslTransformer
    {
        private IDataConnector _dataConnector;

        public SQLitedFactslTransformer(IDataConnector _dataConnector)
        {            
            this._dataConnector = _dataConnector;
        }
    }
}
