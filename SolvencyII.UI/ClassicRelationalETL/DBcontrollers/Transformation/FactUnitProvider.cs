using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers.Transformation
{
    class FactUnitProvider
    {
        private IDataConnector dataConnector;
        private Dictionary<int, string> instanceUnitDict = new Dictionary<int, string>();
        private string reportingCurrencyQname = "s2c_dim:OC";
        private string currencyApproachQname = "s2c_dim:AF";
        private ITableDimensionRepository repository;

        public FactUnitProvider(IDataConnector dataConnector)
        {
            this.dataConnector = dataConnector;
            this.repository = new TableDimensionRepository(dataConnector);
        }

        public FactUnitProvider(IDataConnector dataConnector, ITableDimensionRepository repository)
        {            
            this.dataConnector = dataConnector;
            this.repository = repository;
        }

        internal string getUnit(CrtRow insert, IEnumerable<CrtMapping> factMappings)
        {
            string instanceUnit = getInstanceUnit(insert);
            if(isFromTableWithreportincCurrencyDimension(insert) && isCurrencyOfDenomination(insert, factMappings))
            {
                string memberQname = getCurrencyDimensionMember(factMappings, insert);
                string memberUnit = getCurrencyUnit(memberQname);
                return memberUnit;
            }

            return instanceUnit;                            
        }

        private bool isCurrencyOfDenomination(CrtRow insert, IEnumerable<CrtMapping> factMappings)
        {
            CrtMapping mapping = findMappingForDimension(factMappings, insert, this.currencyApproachQname);
            return (mapping != null && mapping.DIM_CODE.Equals("s2c_dim:AF(s2c_CA:x1)", StringComparison.InvariantCultureIgnoreCase));
        }

        private string getCurrencyUnit(string memberQname)
        {
            return memberQname.Split(new char[] { ':' })[1];
        }

        private string getCurrencyDimensionMember(IEnumerable<CrtMapping> factMappings, CrtRow insrt)
        {
            CrtMapping mapping = findMappingForDimension(factMappings, insrt, reportingCurrencyQname);                            
            return mapping.DIM_CODE.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries)[1];
        }

        private CrtMapping findMappingForDimension(IEnumerable<CrtMapping> factMappings, CrtRow insrt, string dimensionQname)
        {
            CrtMapping mapping = insrt.contextMappings.FirstOrDefault(x => x.DIM_CODE.StartsWith(dimensionQname));
            if (mapping == null)
                mapping = factMappings.FirstOrDefault(x => x.DIM_CODE.StartsWith(dimensionQname));

            return mapping;
        }

        private bool isFromTableWithreportincCurrencyDimension(CrtRow insert)
        {
            int tableID = insert.factMapings.First().TABLE_VERSION_ID;
            return this.CurrencyDimensionTablesIDs.Contains(tableID);
        }

        HashSet<int> currencyDimensionableIDs = null;
        private HashSet<int> CurrencyDimensionTablesIDs
        {
            get
            {
                if(currencyDimensionableIDs == null)
                    currencyDimensionableIDs = new HashSet<int>( repository.getTablesWithDimension(this.reportingCurrencyQname).Select(x=>x.GetTableID()));

                return currencyDimensionableIDs;
            }
        }

        /// <summary>
        /// Gets the instance unit.
        /// </summary>
        /// <param name="insert">The insert.</param>
        /// <returns></returns>
        private string getInstanceUnit(CrtRow insert)
        {
            string unit;
            int instnaceID = insert.rowIdentification.INSTANCE;
            if (instanceUnitDict.TryGetValue(instnaceID, out unit))
                return unit;

            DataTable dt = this.dataConnector.executeQuery(string.Format(@"select i.EntityCurrency from dInstance i where i.InstanceID = {0}", instnaceID));

            unit = dt.Rows[0][0].ToString();
            instanceUnitDict.Add(instnaceID, unit);
            return unit;
        }        
    }
}
