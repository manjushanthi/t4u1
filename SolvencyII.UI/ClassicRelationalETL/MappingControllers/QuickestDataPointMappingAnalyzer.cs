using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolvencyII.Data.CRT.ETL.MappingControllers
{
    /// <summary>
    /// Data point mapping analyzer with highest performance - in development
    /// </summary>
    public class QuickestDataPointMappingAnalyzer : IMappingAnalyzer    {

        protected HashSet<CrtMapping> _allMappings;
        Dictionary<string, Dictionary<string, List<CrtMapping>>> dataPointToRowMapings = new Dictionary<string, Dictionary<string, List<CrtMapping>>>();

        /// <summary>
        /// Gets the fact mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public HashSet<CrtMapping> getFactMappings(dFact fact)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the mappings set.
        /// </summary>
        /// <param name="hashSet">The hash set.</param>
        public void SetMappingsSet(HashSet<CrtMapping> hashSet)
        {
            _allMappings = hashSet;
            populateDictionary();
        }

        /// <summary>
        /// Populates the dictionary.
        /// </summary>
        private void populateDictionary()
        {
            dataPointToRowMapings = new Dictionary<string, Dictionary<string, List<CrtMapping>>>();
            var tables = _allMappings.Select(x => x.DYN_TABLE_NAME).Distinct();

            Dictionary<string, Dictionary<string, List<CrtMapping>>> tabMapings;
            Dictionary<string, List<CrtMapping>> dictColMaps;
            int i = 0;
            int count = tables.Count();
            foreach (var table in tables)
            {
                tabMapings = metricToDimsToMapping(table);

                foreach (var metMap in tabMapings)
                {
                    if (dataPointToRowMapings.TryGetValue(metMap.Key, out dictColMaps))
                    {
                        foreach (var item in dictColMaps)
                        {
                            
                        }
                    }
                    else                    
                        dataPointToRowMapings.Add(metMap.Key, metMap.Value);                    
                }
                ProgressHandler.EtlProgress(++i, count, string.Format(" mapped data points for table {0} ", table));
            } 
        }

        private Dictionary<string, Dictionary<string, List<CrtMapping>>> metricToDimsToMapping(string DYN_TABLE_NAME)
        {
            List<CrtMapping> tableMappings = new List<CrtMapping>(_allMappings.Where(x => x.DYN_TABLE_NAME.Equals(DYN_TABLE_NAME)));

            List<string> factColumnsNames = new List<string>(tableMappings.Where(x => x.ORIGIN.Equals("F")).Select(x => x.DYN_TAB_COLUMN_NAME).Distinct());
            Dictionary<string, List<ColumnMapping>> factColumnMappings = mapColumnMapings(tableMappings, factColumnsNames);
            Dictionary<string, List<CrtMapping>> factColumnToCrtMappingds = mapCrtMappings(factColumnMappings);

            List<string> contextColumnsNames = new List<string>(tableMappings.Where(x => x.ORIGIN.Equals("C")).Select(x => x.DYN_TAB_COLUMN_NAME).Distinct());
            Dictionary<string, List<ColumnMapping>> contextColumnMappings = mapColumnMapings(tableMappings, contextColumnsNames);
            Dictionary<string, List<CrtMapping>> contextToCrtMappingds = mapCrtMappings(contextColumnMappings);

            Dictionary<string,Dictionary<string, List<CrtMapping>>> result = new Dictionary<string,Dictionary<string, List<CrtMapping>>>();
            List<CrtMapping> factCache = null;

            Action<string> dpjob = (factColName) =>
            {
                factCache = factColumnToCrtMappingds[factColName];

                throw new NotImplementedException();

                //foreach (var contColumn in contextColumnMappings)
                //    factCache = combineColumnMappings(factCache, contColumn.Value);

                //foreach (ColumnMapping fColMap in factCache)
                //    result.Add(fColMap);
            };

            factColumnsNames.ForEach(x => dpjob(x));

            tableMappings = null;
            factColumnsNames = null;
            contextColumnMappings = null;
            contextColumnsNames = null;
            GC.Collect();

            return result;
        }

        private Dictionary<string, List<CrtMapping>> mapCrtMappings(Dictionary<string, List<ColumnMapping>> factColumnMappings)
        {
            throw new NotImplementedException();
        }

        private Dictionary<string, List<ColumnMapping>> mapColumnMapings(List<CrtMapping> tableMappings, List<string> columnsNames)
        {
            IEnumerable<KeyValuePair<string, List<CrtMapping>>> columns = columnsNames.Select(x => new KeyValuePair<string, List<CrtMapping>>(x, tableMappings.Where(y => y.DYN_TAB_COLUMN_NAME.Equals(x)).ToList()));

            Dictionary<string, List<ColumnMapping>> factColumnMappings = new Dictionary<string, List<ColumnMapping>>();
            columns.Select(x => new KeyValuePair<string, List<ColumnMapping>>(x.Key, getColumnMappings(x.Value))).ToList().ForEach(x => factColumnMappings.Add(x.Key, x.Value));

            return factColumnMappings;
        }

        /// <summary>
        /// Gets the column mappings.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// All mappings should be for one column
        /// or
        /// All mappings should be for one column
        /// </exception>
        protected List<ColumnMapping> getColumnMappings(List<CrtMapping> mappings)
        {
            if (mappings == null || mappings.Count() == 0)
                return new List<ColumnMapping>();
            if (mappings.Select(x => x.DYN_TABLE_NAME).Distinct().Count() > 1)
                throw new ArgumentException("All mappings should be for one column");
            if (mappings.Select(x => x.DYN_TAB_COLUMN_NAME).Distinct().Count() > 1)
                throw new ArgumentException("All mappings should be for one column");

            List<ColumnMapping> result = new List<ColumnMapping>();
            if (mappings.Count() == mappings.First().REQUIRED_MAPPINGS)
                result.Add(createColumnMapping(mappings));
            else if (mappings.First().REQUIRED_MAPPINGS == 1)
                foreach (CrtMapping map in mappings)
                    result.Add(createColumnMapping(map));

            return result;
        }

        private ColumnMapping createColumnMapping(CrtMapping map)
        {
            return createColumnMapping(new List<CrtMapping>() { map });
        }

        private ColumnMapping createColumnMapping(List<CrtMapping> mappings)
        {
            if (mappings == null || mappings.Count() == 0)
                throw new ArgumentNullException();

            ColumnMapping result = new ColumnMapping(mappings.First().DYN_TABLE_NAME, mappings.First().DYN_TAB_COLUMN_NAME);
            result.Mappings = mappings;
            return result;
        }

        /// <summary>
        /// Cleans the mappings.
        /// </summary>
        public void CleanMappings()
        {
            if (_allMappings != null) _allMappings.Clear();
            if (dataPointToRowMapings != null) dataPointToRowMapings.Clear();            
        }
    }
}
