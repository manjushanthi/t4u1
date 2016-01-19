using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.ETLControllers;

namespace SolvencyII.Data.CRT.ETL
{
    public class MSSQLExtractionManager : IExtractor
    {
        MSSQLConnector _connector;

        public MSSQLExtractionManager(string connectionsString)
        {
            _connector = new MSSQLConnector(connectionsString);
        }

        public HashSet<Model.dFact> exctractFacts()
        {
            throw new NotImplementedException();
        }

        public int getFactsNumber()
        {
            throw new NotImplementedException();
        }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public HashSet<Model.CrtRow> extractInserts()
        {
            throw new NotImplementedException();
        }


        public int getTotalFactsNumber(out int maxFactId, out int minfactId)
        {
            throw new NotImplementedException();
        }


        public HashSet<Model.dFact> exctractFacts(int minfactId, int p)
        {
            throw new NotImplementedException();
        }

        HashSet<Model.dFact> IExtractor.exctractFacts()
        {
            throw new NotImplementedException();
        }

        HashSet<Model.CrtRow> IExtractor.extractInserts()
        {
            throw new NotImplementedException();
        }

        int IExtractor.getFactsNumber()
        {
            throw new NotImplementedException();
        }

        void IExtractor.Cancel()
        {
            throw new NotImplementedException();
        }

        int IExtractor.getTotalFactsNumber(out int maxFactId, out int minfactId)
        {
            throw new NotImplementedException();
        }

        HashSet<Model.dFact> IExtractor.exctractFacts(int minfactId, int p)
        {
            throw new NotImplementedException();
        }


        public void checkAndAddFactIdColumn()
        {
            throw new NotImplementedException();
        }


        public int getInstnaceId()
        {
            throw new NotImplementedException();
        }


        public int getFactsNumber(out int maxFactId, out int minfactId)
        {
            throw new NotImplementedException();
        }

        public HashSet<Model.CrtRow> extractInserts(string tableName, List<int> rowIds)
        {
            throw new NotImplementedException();
        }
    }
}
