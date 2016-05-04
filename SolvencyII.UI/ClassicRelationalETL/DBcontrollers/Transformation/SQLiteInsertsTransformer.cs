using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using System.Data;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.DBcontrollers.Transformation;

namespace SolvencyII.Data.CRT.ETL.DBcontrollers
{
    /// <summary>
    /// Transforms CRt rows into dFacts
    /// </summary>
    class SQLiteCrtRowsTransformer
    {
        private IDataConnector _dataConnector;
        SpecificMetricDecimals specificMetricDecimals = new SpecificMetricDecimals();
        FactUnitProvider unitprovider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteCrtRowsTransformer"/> class.
        /// </summary>
        /// <param name="dataConnector">The _data connector.</param>
        public SQLiteCrtRowsTransformer(IDataConnector dataConnector)
        {            
            this._dataConnector = dataConnector;
            unitprovider = new FactUnitProvider(dataConnector);
        }

        /// <summary>
        /// Transforms the inserts.
        /// </summary>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        public List<dFact> transformInserts(HashSet<CrtRow> inserts)
        {
            List<dFact> allfacts = new List<dFact>();

            List<dFact> facts;
            int i = 0;
            int size = inserts.Count();
            foreach (CrtRow insert in inserts)
            {
                facts = transformInsert(insert);

                foreach (dFact fact in facts)
                {
                    allfacts.Add(fact);
                    if (++i % 1000 == 0)
                        ProgressHandler.EtlProgress(i, 0, " trasnformed facts ");
                }
            }
            ProgressHandler.EtlProgress(i, i, " trasnformed facts ");

            return allfacts;
        }

        /// <summary>
        /// Transforms the insert.
        /// </summary>
        /// <param name="insert">The insert.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// No context mappings
        /// or
        /// No facts mappings
        /// </exception>
        /// <exception cref="System.NullReferenceException">Could not find metric code</exception>
        private List<dFact> transformInsert(CrtRow insert)
        {
            if (insert.contextMappings == null)
                throw new ArgumentNullException("No context mappings");
            if (insert.factMapings == null || insert.factMapings.Count == 0)
                throw new ArgumentNullException("No facts mappings");

            List<dFact> facts = new List<dFact>();
            string metCode, dataPointCode, dataType, factUnit;            

            dFact fact = null;
            foreach (KeyValuePair<string, object> kvp in insert.rcColumnsValues)
            {
                if (kvp.Key.Contains("C999") || kvp.Value == null)
                    continue;

                IEnumerable<CrtMapping> factMappings = insert.factMapings.Where(x => x.DYN_TAB_COLUMN_NAME.Equals(kvp.Key));
                metCode = getMetricCode(insert, kvp, factMappings);
                dataPointCode = constructDpCode(metCode, insert, kvp.Key);
                dataType = getDataType(insert, kvp.Key);
                factUnit = getUnit(dataType, insert, factMappings);
                

                fact = new dFact(kvp.Key.GetHashCode(), dataPointCode, insert.rowIdentification.INSTANCE, kvp.Value, factUnit, dataType);
                if (this.specificMetricDecimals.isSpecific(metCode))
                    fact.decimals = this.specificMetricDecimals.Decimals;
                facts.Add(fact);
            }

            return facts;
        }

        private string getMetricCode(CrtRow insert, KeyValuePair<string, object> kvp, IEnumerable<CrtMapping> factMappings)
        {
            string metCode = findMetricCode(factMappings);
            if (string.IsNullOrEmpty(metCode))
                metCode = findMetricCode(insert.contextMappings.Where(x => x.DYN_TAB_COLUMN_NAME.Equals(kvp.Key)));
            if (string.IsNullOrEmpty(metCode))
                metCode = findMetricCode(insert.contextMappings.Where(x => x.ORIGIN.Equals("C")));
            if (string.IsNullOrEmpty(metCode))
                throw new NullReferenceException("Could not find metric code");
            return metCode;
        }

        private string getUnit(string dataType, CrtRow insert, IEnumerable<CrtMapping> factMappings)
        {
            string result = dataType.Equals("M") ? this.unitprovider.getUnit(insert, factMappings) : "pure";
            return result;
        }


        /// <summary>
        /// Gets the type of the data.
        /// </summary>
        /// <param name="insert">The insert.</param>
        /// <param name="rcColumnName">Name of the rc column.</param>
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException"></exception>
        private string getDataType(CrtRow insert, string rcColumnName)
        {
            CrtMapping dataType = insert.factMapings.Where(x => x.DYN_TAB_COLUMN_NAME.Equals(rcColumnName)).FirstOrDefault();
            if(dataType == null)
                dataType = insert.contextMappings.Where(x => x.DYN_TAB_COLUMN_NAME.Equals(rcColumnName)).FirstOrDefault();
            if (dataType == null)
                throw new NullReferenceException(string.Format("No data point for {0} in {1}", rcColumnName, insert.rowIdentification.TABLE_NAME));

            return dataType.DATA_TYPE;
        }

        
        
        /// <summary>
        /// Constructs the dp code.
        /// </summary>
        /// <param name="metCode">The met code.</param>
        /// <param name="insert">The insert.</param>
        /// <param name="dynColumnaName">Name of the dyn columna.</param>
        /// <returns></returns>
        private string constructDpCode(string metCode, CrtRow insert, string dynColumnaName)
        {
            HashSet<string> dimCodes = new HashSet<string>(insert.factMapings.Where(x=>x.DYN_TAB_COLUMN_NAME.Equals(dynColumnaName)).Select(x => x.DIM_CODE));
            dimCodes = new HashSet<string>(dimCodes.Where(x => !x.Equals(metCode)));

            string dimCode;
            foreach (CrtMapping map in insert.contextMappings)
            {
                dimCode = constructDimCodeForMapping(map, insert);

                if (!dimCode.Equals(metCode) && !dimCodes.Contains(dimCode))
                    dimCodes.Add(dimCode);
            }            

            StringBuilder builder = new StringBuilder(metCode);
            foreach (string dimC in dimCodes.OrderBy(x => x))            
                builder.Append("|").Append(dimC);            

            string dpCode = builder.ToString();
            return dpCode;
        }

        /// <summary>
        /// Constructs the dim code for mapping.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="insert">The insert.</param>
        /// <returns></returns>
        private string constructDimCodeForMapping(CrtMapping map, CrtRow insert)
        {
            string colValue = insert.getColStringValue(map.DYN_TAB_COLUMN_NAME);
            //StringBuilder build = new StringBuilder(map.DIM_CODE.Substring(0, map.DIM_CODE.IndexOf("(")));            
            if (map.ORIGIN.Equals("C") && map.DIM_CODE.Contains("*"))
            {
                string value;
                if (!string.IsNullOrEmpty(map.DOM_CODE) && string.IsNullOrWhiteSpace(colValue))
                    value = string.Format("<{0}/>", map.DOM_CODE);
                else if (!string.IsNullOrEmpty(map.DOM_CODE))
                {
                    value = string.Format("<{0}>{1}</{0}>", map.DOM_CODE, "{0}");
                    value = string.Format(value, colValue);
                }
                else
                    value = colValue;

                string result = map.DIM_CODE.Replace("*", value);
                return result;
            }

            return map.DIM_CODE;
        }

        /// <summary>
        /// Gets the metric code.
        /// </summary>
        /// <param name="mapings">The mapings.</param>
        /// <returns></returns>
        private string findMetricCode(IEnumerable<CrtMapping> mapings)
        {
            string metCode = mapings
                .Where(x => x.DIM_CODE.Contains(EtlGlobals.MetDimCode))
                .Select(x=>x.DIM_CODE)
                .FirstOrDefault();

            if (string.IsNullOrEmpty(metCode))
                metCode = mapings
                    .Where(x => x.DIM_CODE.Contains(EtlGlobals.AtyDimCode))
                    .Select(x => x.DIM_CODE)
                    .FirstOrDefault();           

            return metCode;
        }
    }
}
