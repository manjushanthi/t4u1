using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.ETLControllers;

namespace SolvencyII.Data.CRT.ETL
{
    public class MSSQLoadingManager : ILoader
    {
        MSSQLConnector _connector;

        public MSSQLoadingManager(string connectionsString)
        {
            _connector = new MSSQLConnector(connectionsString);
        }

        public void loadInserts(HashSet<Model.CrtRow> inserts)
        {
            throw new NotImplementedException();
        }


        public void loadFacts(HashSet<Model.dFact> inserts)
        {
            throw new NotImplementedException();
        }


        public void Cancel()
        {
            throw new NotImplementedException();
        }


        public void openConnection()
        {
            throw new NotImplementedException();
        }

        public void closeConnection()
        {
            throw new NotImplementedException();
        }


        public void CleanDFacts(int instanceID)
        {
            throw new NotImplementedException();
        }
    }
}
