using SolvencyII.Data.CRT.ETL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SolvencyII.Data.CRT.ETL.MappingControllers
{
    /// <summary>
    /// mapping analyzer based on dimension by dimension analysis
    /// </summary>
    public class DimByDimMapingAnalyzer : IMappingAnalyzer
    {
        private Dictionary<int, List<CrtMapping>> dimCodeHashToMappingDict;
        private HashSet<Model.CrtMapping> _allMappingsHashSet;
        private Dictionary<ColumnTableLocation, List<CrtMapping>> ____localDictTabColToMapping;

        /// <summary>
        /// Gets the fact mappings.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        public HashSet<Model.CrtMapping> getFactMappings(Model.dFact fact)
        {
            HashSet<CrtMapping> factMappings = new HashSet<CrtMapping>();
            List<CrtMapping> fMapings = findMappingsByDimCodesHashSet(fact);
            fMapings = findMappingsThatFallowRequiredMappings(fMapings, fact);

            HashSet<string> tablesThatHavefactMapping = new HashSet<string>();

            foreach (CrtMapping item in fMapings)            
                if (item.ORIGIN == "F") tablesThatHavefactMapping.Add(item.DYN_TABLE_NAME);            

            Dictionary<int, List<CrtMapping>> tabulaHashToMappings = getMapingsFortables(fMapings, tablesThatHavefactMapping);

            foreach (var kvp in tabulaHashToMappings)
                factMappings.Add(kvp.Value.FirstOrDefault());            

            return factMappings;
        }

        /// <summary>
        /// Finds the mappings by dim codes hash set.
        /// </summary>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        private List<CrtMapping> findMappingsByDimCodesHashSet(dFact fact)
        {
            List<CrtMapping> result = new List<CrtMapping>();
            List<CrtMapping> dimMapps;
            foreach (string dimCode in fact.DimCodes)
            {
                if (!dimCodeHashToMappingDict.TryGetValue(dimCode.GetHashCode(), out dimMapps))
                    return new List<CrtMapping>();

                foreach (CrtMapping item in dimMapps)
                    result.Add(item);                
            }
            return result;
        }

        /// <summary>
        /// Finds the mappings that fallow required mappings.
        /// </summary>
        /// <param name="fMapings">The f mapings.</param>
        /// <param name="fact">The fact.</param>
        /// <returns></returns>
        private List<CrtMapping> findMappingsThatFallowRequiredMappings(List<CrtMapping> fMapings, dFact fact)
        {
            ____localDictTabColToMapping = new Dictionary<ColumnTableLocation, List<CrtMapping>>();
            List<CrtMapping> locMaps;
            foreach (CrtMapping map in fMapings)
            {
                locMaps = null;
                if (____localDictTabColToMapping.TryGetValue(map.GetTabularLocation(), out locMaps))
                {
                    locMaps.Add(map);
                }
                else
                {
                    locMaps = new List<CrtMapping>();
                    locMaps.Add(map);
                    ____localDictTabColToMapping.Add(map.GetTabularLocation(), locMaps);
                }
            }

            List<CrtMapping> result = new List<CrtMapping>();

            foreach (CrtMapping map in fMapings)
            {
                locMaps = ____localDictTabColToMapping[map.GetTabularLocation()];

                if (locMaps.Count == map.REQUIRED_MAPPINGS)
                    result.Add(map);
            }

            return result;
        }

        /// <summary>
        /// Gets the mapings fortables.
        /// </summary>
        /// <param name="sourceMappings">The source mappings.</param>
        /// <param name="tablesNames">The tables names.</param>
        /// <returns></returns>
        private Dictionary<int, List<CrtMapping>> getMapingsFortables(IEnumerable<CrtMapping> sourceMappings, HashSet<string> tablesNames)
        {
            Dictionary<int, List<CrtMapping>> result = new Dictionary<int, List<CrtMapping>>();
            List<CrtMapping> mapsCache;
            foreach (CrtMapping map in sourceMappings)
            {
                if (!tablesNames.Contains(map.DYN_TABLE_NAME))
                    continue;

                if (result.TryGetValue(map.GetTabularLocationHashCode(), out mapsCache))
                {
                    mapsCache.Add(map);
                }
                else
                {
                    mapsCache = new List<CrtMapping>();
                    mapsCache.Add(map);
                    result.Add(map.GetTabularLocationHashCode(), mapsCache);
                }
            }
            return result;
        }

        /// <summary>
        /// Sets the mappings set.
        /// </summary>
        /// <param name="mappings">The mappings.</param>
        public void SetMappingsSet(HashSet<CrtMapping> mappings)
        {
            _allMappingsHashSet = mappings;
            dimCodeHashToMappingDict = new Dictionary<int, List<CrtMapping>>();
            List<CrtMapping> mapCache;
            int hashCode;
            foreach (CrtMapping map in _allMappingsHashSet)
            {
                hashCode = map.DIM_CODE.GetHashCode();
                if (dimCodeHashToMappingDict.TryGetValue(hashCode, out mapCache))
                {
                    mapCache.Add(map);
                }
                else
                {
                    mapCache = new List<CrtMapping>();
                    mapCache.Add(map);
                    dimCodeHashToMappingDict.Add(hashCode, mapCache);
                }
            }
        }

        /// <summary>
        /// Cleans the mappings.
        /// </summary>
        public void CleanMappings()
        {
            if(dimCodeHashToMappingDict != null) dimCodeHashToMappingDict.Clear();
            if(_allMappingsHashSet!= null) _allMappingsHashSet.Clear();
            if(____localDictTabColToMapping!=null) ____localDictTabColToMapping.Clear();
        }
    }
}
