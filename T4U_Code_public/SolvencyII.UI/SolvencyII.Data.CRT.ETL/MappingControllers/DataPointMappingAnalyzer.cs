﻿using SolvencyII.Data.CRT.ETL.ETLControllers;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL.MappingControllers
{
    /// <summary>
    /// Analyzer of mapping with indexed data points
    /// </summary>
    public class DataPointMappingAnalyzer : IMappingAnalyzer
    {
        protected HashSet<CrtMapping> _allMappings;
        protected Dictionary<string, List<ColumnMapping>> dataPointToRowMapings = new Dictionary<string, List<ColumnMapping>>();
        public Dictionary<string, List<ColumnMapping>> DataPointToRowMapings { get { return dataPointToRowMapings; } }

        /// <summary>
        /// Gets the fact mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">No fact</exception>
        public HashSet<Model.CrtMapping> getFactMappings(dFact fact)
        {
            if (fact == null)
                throw new ArgumentNullException("No fact");

            List<ColumnMapping> colMaps;
            if (dataPointToRowMapings.TryGetValue(fact.GetDataPointSignature(), out colMaps))
            {
                HashSet<CrtMapping> result = new HashSet<CrtMapping>();
                foreach (ColumnMapping colMap in colMaps)
                    foreach (CrtMapping map in colMap.Mappings)
                        result.Add(map);

                return result;
            }
            else
                return new HashSet<CrtMapping>();
        }

        /// <summary>
        /// Sets the mappings set.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        public virtual void SetMappingsSet(HashSet<CrtMapping> mappings)
        {
            _allMappings = mappings;
            populateDictionary();
        }

        private void populateDictionary()
        {
            dataPointToRowMapings = new Dictionary<string, List<ColumnMapping>>();
            var tables = _allMappings.Select(x => x.DYN_TABLE_NAME).Distinct();

            HashSet<ColumnMapping> tabMapings;
            List<ColumnMapping> dictColMaps;
            string dpCode;
            int i = 0;
            int count = tables.Count();
            foreach (var table in tables)
            {
                tabMapings = getColumnMappings(table);

                foreach (ColumnMapping colMap in tabMapings)
                {
                    dpCode = colMap.DataPointCode;
                    if (dataPointToRowMapings.TryGetValue(dpCode, out dictColMaps))
                        dictColMaps.Add(colMap);
                    else                    
                        dataPointToRowMapings.Add(dpCode, new List<ColumnMapping>(){colMap});                    
                }
                ProgressHandler.EtlProgress(++i, count, string.Format(" mapped data points for table {0} ", table));
            }           
        }


        /// <summary>
        /// Gets the column mappings.
        /// </summary>
        /// <param name="DYN_TABLE_NAME">Name of the dy n_ tabl e_.</param>
        /// <returns></returns>
        private HashSet<ColumnMapping> getColumnMappings(string DYN_TABLE_NAME)
        {
            List<CrtMapping> tableMappings = new List<CrtMapping>(_allMappings.Where(x => x.DYN_TABLE_NAME.Equals(DYN_TABLE_NAME)));

            List<string> factColumnsNames = new List<string>(tableMappings.Where(x => x.ORIGIN.Equals("F")).Select(x => x.DYN_TAB_COLUMN_NAME).Distinct());
            Dictionary<string, List<ColumnMapping>> factColumnMappings = mapColumnMapings(tableMappings, factColumnsNames);

            List<string> contextColumnsNames = new List<string>(tableMappings.Where(x => x.ORIGIN.Equals("C")).Select(x => x.DYN_TAB_COLUMN_NAME).Distinct());
            Dictionary<string, List<ColumnMapping>> contextColumnMappings = mapColumnMapings(tableMappings, contextColumnsNames);

            List<ColumnMapping> result = new List<ColumnMapping>();
            List<ColumnMapping> factCache = null;

            Action<string> dpjob = (factColName) =>
            {
                factCache = factColumnMappings[factColName];

                foreach (var contColumn in contextColumnMappings)
                    factCache = combineColumnMappings(factCache, contColumn.Value);

                foreach (ColumnMapping fColMap in factCache)
                    result.Add(fColMap);
            };

            //factColumnsNames.AsParallel().WithDegreeOfParallelism(10).ForAll(x=>dpjob(x));
            factColumnsNames.ForEach(x => dpjob(x));

            tableMappings = null;
            factColumnsNames = null;
            contextColumnMappings = null;
            contextColumnsNames = null;
            GC.Collect();

            return new HashSet<ColumnMapping>(result);

        }

        /// <summary>
        /// Maps the column mapings.
        /// </summary>
        /// <param name="tableMappings">The table mappings.</param>
        /// <param name="columnsNames">The columns names.</param>
        /// <returns></returns>
        private Dictionary<string, List<ColumnMapping>> mapColumnMapings(List<CrtMapping> tableMappings, List<string> columnsNames)
        {
            IEnumerable<KeyValuePair<string, List<CrtMapping>>> columns = columnsNames.Select(x => new KeyValuePair<string, List<CrtMapping>>(x, tableMappings.Where(y => y.DYN_TAB_COLUMN_NAME.Equals(x)).ToList()));

            Dictionary<string, List<ColumnMapping>> factColumnMappings = new Dictionary<string, List<ColumnMapping>>();
            columns.Select(x => new KeyValuePair<string, List<ColumnMapping>>(x.Key, getColumnMappings(x.Value))).ToList().ForEach(x => factColumnMappings.Add(x.Key, x.Value));

            return factColumnMappings;
        }

        /// <summary>
        /// Combines the column mappings.
        /// </summary>
        /// <param name="factColMappings">The fact col mappings.</param>
        /// <param name="contextColMappings">The context col mappings.</param>
        /// <returns></returns>
        protected List<ColumnMapping> combineColumnMappings(List<ColumnMapping> factColMappings, IEnumerable<ColumnMapping> contextColMappings)
        {
            if (contextColMappings == null || contextColMappings.Count() == 0)
                return factColMappings;

            List<ColumnMapping> result = new List<ColumnMapping>();
            ColumnMapping newColMap;
            foreach (ColumnMapping contColMaps in contextColMappings)
            {
                foreach (ColumnMapping factColMaps in factColMappings)
                {
                    newColMap = new ColumnMapping(factColMaps);
                    foreach (CrtMapping map in contColMaps.Mappings)
                    {
                        newColMap.Mappings.Add(map);
                    }
                    result.Add(newColMap);
                }
            }
            return result;
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
            //else
                //throw new ArgumentException("Invalid number of mappings");

            return result;
        }

        /// <summary>
        /// Creates the column mapping.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <returns></returns>
        private ColumnMapping createColumnMapping(CrtMapping map)
        {
            return createColumnMapping(new List<CrtMapping>{ map});
        }

        /// <summary>
        /// Creates the column mapping.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        private ColumnMapping createColumnMapping(List<CrtMapping> mappings)
        {
            if (mappings == null || mappings.Count() == 0)
                throw new ArgumentNullException();

            ColumnMapping result = new ColumnMapping(mappings.First().DYN_TABLE_NAME, mappings.First().DYN_TAB_COLUMN_NAME);
            result.Mappings = (mappings);
            return result;
        }
        
        /// <summary>
        /// Cleans the mappings.
        /// </summary>
        public void CleanMappings()
        {
            if(_allMappings != null) _allMappings.Clear();
            if(dataPointToRowMapings != null) dataPointToRowMapings.Clear();
        }
    }
}