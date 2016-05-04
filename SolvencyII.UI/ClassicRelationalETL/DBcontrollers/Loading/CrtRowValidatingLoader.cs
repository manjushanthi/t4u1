using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.Model.Validation;
using SolvencyII.Data.CRT.ETL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Loading
{
    public class CrtRowValidatingLoader : ILoader
    {
        private ILoader mainLoader;
        CrtRowValidator validator = new CrtRowValidator();
        CrtErrorsRepository errorsRepository; 

        public CrtRowValidatingLoader(ILoader mainLoader,  CrtErrorsRepository errorsRepository)
        {
            this.mainLoader = mainLoader;
            this.errorsRepository = errorsRepository;
        }

        public void loadInserts(HashSet<Model.CrtRow> rows)
        {
            var valResult = validator.validate(rows);
            errorsRepository.Add(valResult.errors);
            this.mainLoader.loadInserts(new HashSet<CrtRow>(valResult.validObjects));
        }

        public void loadFacts(HashSet<Model.dFact> facts)
        {
            this.mainLoader.loadFacts(facts);
        }

        public void CleanDFacts(int instanceID)
        {
            this.mainLoader.CleanDFacts(instanceID);
        }

        public void Cancel()
        {
            this.mainLoader.Cancel();
        }

        public void openConnection()
        {
            this.mainLoader.openConnection();
        }

        public void closeConnection()
        {
            this.mainLoader.closeConnection();
        }
    }
}