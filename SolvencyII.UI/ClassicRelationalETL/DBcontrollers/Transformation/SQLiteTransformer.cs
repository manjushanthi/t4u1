using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.DBcontrollers;
using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using System.Data;

namespace SolvencyII.Data.CRT.ETL
{
    /// <summary>
    /// Transformer of SQL ite facts
    /// </summary>
    public class SQLiteTransformer : ITransformer
    {
        SQLiteCrtRowsTransformer _rela2dFactTransformer;
        SQLiteMappingProvider _mappingProvider;
        IDataConnector _dataConnector;
        private static bool _cancel = false;

        HashSet<int> notMappedFctIds  = new HashSet<int>();
        HashSet<int> mappedFctIds  = new HashSet<int>();
        /// <summary>
        /// Gets the not mapped FCT ids.
        /// </summary>
        /// <value>
        /// The not mapped FCT ids.
        /// </value>
        public HashSet<int> NotMappedFctIds { get { return new HashSet<int>(notMappedFctIds.Except(mappedFctIds)); } }

        //string filePath;
        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteTransformer"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        public SQLiteTransformer(IDataConnector dataConnector)
        {
            _dataConnector = dataConnector;
            _mappingProvider = new SQLiteMappingProvider(dataConnector);
            _rela2dFactTransformer = new SQLiteCrtRowsTransformer(_dataConnector);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteTransformer"/> class.
        /// </summary>
        /// <param name="dataConnector">The data connector.</param>
        /// <param name="mappingProvider">The mapping provider.</param>
        public SQLiteTransformer(IDataConnector dataConnector, SQLiteMappingProvider mappingProvider)
        {
            _dataConnector = dataConnector;
            _mappingProvider = mappingProvider;
            _rela2dFactTransformer = new SQLiteCrtRowsTransformer(_dataConnector);
        }

        /// <summary>
        /// Transforms the facts.
        /// </summary>
        /// <param name="facts">The facts.</param>
        /// <returns></returns>
        public HashSet<CrtRow> transformFacts(HashSet<dFact> facts)
        {
            _cancel = false;
            HashSet<CrtMapping> mappings;
            HashSet<CrtRow> allInserts = new HashSet<CrtRow>();
            HashSet<CrtRow> factInserts;

            int size = facts.Count();
            int i = 0; 
            foreach (Model.dFact fact in facts)
            {
                //try
                //{
                    if (_cancel) break;

                    mappings = this.getMappings(fact);
                    factInserts = this.createInserts(fact, mappings);
                    if (factInserts == null || factInserts.Count() == 0) notMappedFctIds.Add((int)fact.dFactId);
                    else mappedFctIds.Add((int)fact.dFactId);

                    foreach (CrtRow ins in factInserts)
                        allInserts.Add(ins);

                    if (++i % 100 == 0)
                        ProgressHandler.EtlProgress(i, size, " transformed facts ");
                //}
                //catch (Exception ex)
                //{
                //    throw new EtlException("Exception during transformation oif fact " + fact.dFactId, ex);
                //}
            }
            ProgressHandler.EtlProgress(i, size, " transformed facts ");
            return allInserts;
        }
        
        /// <summary>
        /// Creates the inserts.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <param name="mappings">The mappings.</param>
        /// <returns></returns>
        public HashSet<CrtRow> createInserts(dFact fact, HashSet<CrtMapping> mappings)
        {
            var tablesPages = mappings.Select(x => new { tableName = x.DYN_TABLE_NAME, pageNumber = x.PAGE_COLUMNS_NUMBER }).Distinct();

            HashSet<CrtRow> inserts = new HashSet<CrtRow>();
            CrtRowIdentification ri;
            IEnumerable<CrtMapping> tableMappings;

            foreach (var tablePage in tablesPages)
            {
                ri = null;
                tableMappings = null;
                tableMappings = mappings.Where(x=>x.DYN_TABLE_NAME == tablePage.tableName);

                if(tablePage.pageNumber != countPageColumns(tableMappings))
                    continue;
                
                ri = getRowIndetification(tableMappings, fact);                
                inserts.Add(createInsert(ri, fact, tableMappings, tablePage.tableName));
            }
            return inserts;
        }

        /// <summary>
        /// Creates the insert.
        /// </summary>
        /// <param name="rowIdentification">The row identification.</param>
        /// <param name="fact">The fact.</param>
        /// <param name="tableMappings">The table mappings.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <returns></returns>
        private CrtRow createInsert(CrtRowIdentification rowIdentification, dFact fact, IEnumerable<CrtMapping> tableMappings, string tableName)
        {
            int numOfPageDimCodes = (from m in tableMappings where m.ORIGIN.Equals("C") select m.REQUIRED_MAPPINGS).Sum() 
                - (from m in tableMappings where m.ORIGIN.Equals("C") && m.IS_DEFAULT select m.REQUIRED_MAPPINGS).Sum();

            HashSet<string> factMappings = new HashSet<string>(tableMappings
                                    .Where(x => x.ORIGIN == "F"
                                        && x.DYN_TABLE_NAME.Equals(tableName)
                                        && numOfPageDimCodes + x.REQUIRED_MAPPINGS == fact.DimCodesNumber)
                //number of dim codes has to be equal to total of required mappings
                                    .Select(x => x.DYN_TAB_COLUMN_NAME));
            if (factMappings.Count() == 0 
                && tableMappings.Where(x => x.ORIGIN == "F").Count().Equals(1) 
                && string.IsNullOrEmpty(tableMappings.First().DIM_CODE))
                factMappings = new HashSet<string>(tableMappings.Where(x => x.ORIGIN == "F").Select(x => x.DYN_TAB_COLUMN_NAME));
            if (factMappings.Count() == 0)
                return null;

            Dictionary<string, object> rcColumnsValues = new Dictionary<string, object>();
            foreach (string tabColname in factMappings)
                rcColumnsValues.Add(tabColname, fact.getValue());

            return new CrtRow(rowIdentification, rcColumnsValues);
        }

        /// <summary>
        /// Gets the row indetification.
        /// </summary>
        /// <param name="tableMappings">The table mappings.</param>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        private CrtRowIdentification getRowIndetification(IEnumerable<CrtMapping> tableMappings, dFact fact)
        {
            IEnumerable<CrtMapping> contextMappings = tableMappings
                    .Where(x => x.ORIGIN == "C" && x.IS_IN_TABLE);

            OrderedDictionary colValue = new OrderedDictionary();
            string value;
            HashSet<string> dimensions = new HashSet<string>();

            foreach (CrtMapping maping in contextMappings)
            {
                value = null;

                if (maping.DIM_CODE.Contains("*"))//typed dimension
                    value = getValueOfDimension(fact, maping.DIM_CODE);  
                else if (!dimensions.Contains(getDimension(maping.DIM_CODE)) && maping.IS_DEFAULT)
                    value = maping.MEM_CODE;
                else if (!dimensions.Contains(getDimension(maping.DIM_CODE)))//explicit dimension
                    value = (from dm in fact.dimensionsMembers
                             where (dm.Key + "(" + dm.Value + ")").Equals(maping.DIM_CODE)
                             select dm.Value)
                            .FirstOrDefault();                

                 colValue.Add(maping.DYN_TAB_COLUMN_NAME, value);
                 dimensions.Add(getDimension(maping.DIM_CODE));
                
            }

            CrtRowIdentification newRi =  new CrtRowIdentification(tableMappings.First().DYN_TABLE_NAME, fact.instanceId, colValue);
            CrtRowIdentification founRi;

            if (rowIdsDict.TryGetValue(newRi.GetHashCode(), out founRi))
                newRi = founRi;
            else
                rowIdsDict.Add(newRi.GetHashCode(), newRi);

            return newRi;
        }

        /// <summary>
        /// Gets the value of dimension.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <param name="dimCode">The dim code.</param>
        /// <returns></returns>
        private string getValueOfDimension(dFact fact, string dimCode)
        {
            foreach (var dm in fact.dimensionsMembers)
            {
                if(!(dm.Key + "(*)").Equals(dimCode)) continue;
                if(dm.Value.TrimEnd().EndsWith("/>")) return null;

                return dm.Value.Split(new char[] { '<', '>' }, StringSplitOptions.RemoveEmptyEntries)[1];
            }
            return null;
        }

        Dictionary<int, CrtRowIdentification> rowIdsDict = new Dictionary<int, CrtRowIdentification>();

        /// <summary>
        /// Gets the dimension.
        /// </summary>
        /// <param name="dimCode">The dim code.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">no dim code</exception>
        private string getDimension(string dimCode)
        {
            if (string.IsNullOrEmpty(dimCode))
                throw new  ArgumentNullException("no dim code");

            return dimCode.Substring(0, dimCode.IndexOf('(')+1);
        }

        /// <summary>
        /// Counts the page columns.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        /// <returns></returns>
        public int countPageColumns(IEnumerable<CrtMapping> mappings)
        {
            //return mappings.Where(x => x.DYN_TAB_COLUMN_NAME.StartsWith("P")).Count();
            int i = 0;
            foreach (CrtMapping map in mappings)
                if (map.isPageColumn)
                    i++;

            return i;
        }

        /// <summary>
        /// Gets the mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        private HashSet<CrtMapping> getMappings(Model.dFact fact)
        {
            HashSet<CrtMapping> mappings = this._mappingProvider.getMappings(fact);
            return mappings;
        }

        /// <summary>
        /// Transforms the inserts.
        /// </summary>
        /// <param name="inserts">The inserts.</param>
        /// <returns></returns>
        public HashSet<dFact> transformInserts(HashSet<CrtRow> inserts)
        {
            return new HashSet<dFact>(_rela2dFactTransformer.transformInserts(inserts));
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public void Cancel()
        {
            _cancel = true;
        }

        /// <summary>
        /// Loads the mappings.
        /// </summary>
        public void loadMappings()
        {
            _mappingProvider.getAllMappingHashSet();
        }

        /// <summary>
        /// Opens the connection.
        /// </summary>
        public void openConnection()
        {
            _dataConnector.openConnection();
        }

        /// <summary>
        /// Closes the connection.
        /// </summary>
        public void closeConnection()
        {
            _dataConnector.closeConnection();
        }


        /// <summary>
        /// Gets the maping provider.
        /// </summary>
        /// <returns></returns>
        public SQLiteMappingProvider getMapingProvider()
        {
            return _mappingProvider;
        }


        
    }
}
