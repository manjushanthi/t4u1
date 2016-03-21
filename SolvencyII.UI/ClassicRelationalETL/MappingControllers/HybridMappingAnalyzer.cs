using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SolvencyII.Data.CRT.ETL.Model;
using SolvencyII.Data.CRT.ETL.DataConnectors;

namespace SolvencyII.Data.CRT.ETL.MappingControllers
{
    /// <summary>
    /// mapping analyzer that analyzes wth both (data point and dimension by dimension approach)
    /// </summary>
    public class HybridMappingAnalyzer : IMappingAnalyzer
    {
        DataPointMappingAnalyzer _dataPointAnalyzer;
        DimByDimMapingAnalyzer _dimByDimAnalyzer;
        
        private HashSet<CrtMapping> _mappings;
        private ITableMappingResolver _tableMappingResolver;

        /// <summary>
        /// Initializes a new instance of the <see cref="HybridMappingAnalyzer"/> class.
        /// </summary>
        /// <param name="tableMappingResolver">The table mapping resolver.</param>
        public HybridMappingAnalyzer(ITableMappingResolver tableMappingResolver)
        {
            _tableMappingResolver = tableMappingResolver;

            _dataPointAnalyzer = new DataPointMappingAnalyzer();
            _dimByDimAnalyzer = new DimByDimMapingAnalyzer();
        }

        /// <summary>
        /// Gets the fact mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        public HashSet<Model.CrtMapping> getFactMappings(Model.dFact fact)
        {
            HashSet<CrtMapping> result = _dataPointAnalyzer.getFactMappings(fact);
            if (result == null || result.Count() == 0)
                result = _dimByDimAnalyzer.getFactMappings(fact);

            return result;
        }

        /// <summary>
        /// Sets the mappings set.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        /// <exception cref="System.NullReferenceException">No mappings</exception>
        public void SetMappingsSet(HashSet<Model.CrtMapping> mappings)
        {
            if (mappings == null)
                throw new NullReferenceException("No mappings");

            this._mappings = mappings;

            setMappingsForAnalyzers();
        }

        /// <summary>
        /// Sets the mappings for analyzers.
        /// </summary>
        /// <exception cref="System.ApplicationException">
        /// Mapping not recognized
        /// or
        /// Mapping not recognized
        /// </exception>
        private void setMappingsForAnalyzers()
        {
            HashSet<CrtMapping> dimByDimMappings = new HashSet<CrtMapping>();
            HashSet<CrtMapping> dataPointMappings = new HashSet<CrtMapping>();

            foreach (CrtMapping map in _mappings)
            {
                switch (_tableMappingResolver.resolve(map))
                {
                    case HowHandleEnum.DimByDim:
                        dimByDimMappings.Add(map);
                        break;
                    case HowHandleEnum.DataPoint:
                        dataPointMappings.Add(map);
                        break;
                    case HowHandleEnum.None:
                        throw new ApplicationException("Mapping not recognized");
                    default:
                        throw new ApplicationException("Mapping not recognized");
                }
            }

            _dataPointAnalyzer.SetMappingsSet(dataPointMappings);
            _dimByDimAnalyzer.SetMappingsSet(dimByDimMappings);
        }


        /// <summary>
        /// Cleans the mappings.
        /// </summary>
        public void CleanMappings()
        {
            if(_mappings!=null) _mappings.Clear();
            if(_dataPointAnalyzer !=null) this._dataPointAnalyzer.CleanMappings();
            if(_dimByDimAnalyzer != null) this._dimByDimAnalyzer.CleanMappings();
        }
    }
}
