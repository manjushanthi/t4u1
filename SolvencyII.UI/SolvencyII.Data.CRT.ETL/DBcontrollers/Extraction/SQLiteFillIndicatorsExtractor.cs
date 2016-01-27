using ClassicRelationalETL.DataConnectors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassicRelationalETL
{
    public class SQLiteFillIndicatorsExtractor : IFilinglIndicatorsExtractor
    {
        IDataConnector _dataConnector;
        public SQLiteFillIndicatorsExtractor(IDataConnector dataConnector)
        {
            _dataConnector = dataConnector;
        }

        public string[] getTablesNamesFromFillingIndicators(int instnanceId)
        {
            try
            {
                HashSet<int> potentialTableIds = getPotentialTableIDsFromFillingIndicators(instnanceId);
                return this.getTableNames(getTableNames(potentialTableIds));
            }
            catch (Exception ex)
            {
                throw new EtlException("Exception during reading filling indicators for instance " + instnanceId, ex);
            }
        }

        private HashSet<int> getPotentialTableIDsFromFillingIndicators(int instnanceId)
        {
            string query = string.Format(@"select fi.BusinessTemplateID
                                            from dFilingIndicator fi 
                                            where fi.InstanceID = {0}", instnanceId);

            HashSet<int> potentialTableIds = new HashSet<int>();
            DataTable dt = this._dataConnector.executeQuery(query);
            int tabVarId = 0;

            foreach (DataRow dr in dt.Rows)
            {
                tabVarId = 0;
                tabVarId = int.Parse(dr[0].ToString());

                foreach (int tabId in getTablesIds(tabVarId))
                    potentialTableIds.Add(tabId);
            }
            return potentialTableIds;
        }

        private string[] getTableNames(HashSet<int> potentialTableIds)
        {
            string query = @"select TableID, TableCode 
                            from mTable t
                            where t.TableID in ({0})";
            StringBuilder condition = new StringBuilder(); bool first = true;
            foreach (int id in potentialTableIds)
            {
                if (!first) condition.Append(", ");
                condition.Append(id);
                if (first) first = false;
            }

            query = string.Format(query, condition.ToString());
            DataTable dt = _dataConnector.executeQuery(query);
            string[] tabNames = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)            
                tabNames[i] = dt.Rows[i]["TableID"].ToString() + "_" + dt.Rows[i]["TableCode"].ToString().Replace(".", "_").Replace(" ", "_");
            
            return tabNames;
        }

        private string[] getTableNames(string[] tableNames)
        {
            string[] formatedTableNames = new string[tableNames.Count()];
            for (int i = 0; i < tableNames.Count(); i++)
            {
                if (tableNames[i].StartsWith("T__"))
                    formatedTableNames[i] = tableNames[i];
                else
                    formatedTableNames[i] = "T__" + tableNames[i];
            }
            return formatedTableNames;
        }

        private IEnumerable<int> getTablesIds(int parentTableOrTemplate)
        {
            HashSet<int> tabIds = new HashSet<int>();

            string query = @"select TemplateOrTableID, ParentTemplateOrTableID, TemplateOrTableType, IFNULL(TableID, 'NULL') TableID
                            from mTemplateOrTable tt 
                                left join mTaxonomyTable taxt 
                                    on (taxt.AnnotatedTableID = tt.TemplateOrTableID and taxt.TaxonomyID = tt.TaxonomyID)
                            where tt.ParentTemplateOrTableID = " + parentTableOrTemplate;

            DataTable dt = _dataConnector.executeQuery(query);

            //AnnotatedTemplate
            //AnnotatedTable

            int id = 0;
            foreach (DataRow dr in dt.Rows)
            {
                id = int.Parse(dr["TemplateOrTableID"].ToString());
                if (dr["TemplateOrTableType"].ToString().Trim().Equals("AnnotatedTable"))
                    tabIds.Add(int.Parse(dr["TableID"].ToString()));

                foreach (int tabId in getTablesIds(id))                
                    tabIds.Add(tabId);
                
            }
            return tabIds;
        }

        public string[] getTablesNamesFromModule(int instanceID)
        {
            try
            {
                HashSet<int> potentialTableIds = getPotentialTableIDsFromModule(instanceID);
                return this.getTableNames(getTableNames(potentialTableIds));
            }
            catch (Exception ex)
            {
                throw new EtlException("Exception during reading modules for instance " + instanceID, ex);
            }
        }

        private HashSet<int> getPotentialTableIDsFromModule(int instanceID)
        {
            string query = @"select mbt.BusinessTemplateID
from dInstance i 
    inner join mModule m on m.ModuleID = i.ModuleID
    inner join mModuleBusinessTemplate mbt on mbt.ModuleID = m.ModuleID
where i.InstanceID  = {0}";
            query = string.Format(query, instanceID);

            HashSet<int> potentialTableIds = new HashSet<int>();
            DataTable dt = this._dataConnector.executeQuery(query);
            int tabVarId = 0;
            foreach (DataRow dr in dt.Rows)
            {
                tabVarId = 0;
                tabVarId = int.Parse(dr[0].ToString());

                foreach (int tabId in getTablesIds(tabVarId))
                    potentialTableIds.Add(tabId);
            }
            return potentialTableIds;
        }

        public int[] getTablesIDsFromFillingIndicators(int instanceID)
        {
            HashSet<int> potentialTableIds = getPotentialTableIDsFromFillingIndicators(instanceID);
            int[] tableIds = getTableIDs(potentialTableIds);            
            return tableIds;
        }

        public int[] getTablesIDsFromModule(int instanceID)
        {
            HashSet<int> potentialTableIds = getPotentialTableIDsFromModule(instanceID);
            int[] tableIds = getTableIDs(potentialTableIds);
            return tableIds;
        }

        private int[] getTableIDs(HashSet<int> potentialTableIds)
        {
            string query = @"select TableID, TableCode 
                            from mTable t
                            where t.TableID in ({0})";
            StringBuilder condition = new StringBuilder(); bool first = true;
            foreach (int id in potentialTableIds)
            {
                if (!first) condition.Append(", ");
                condition.Append(id);
                if (first) first = false;
            }

            query = string.Format(query, condition.ToString());
            DataTable dt = _dataConnector.executeQuery(query);
            int[] tableIds = new int[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
                tableIds[i] = int.Parse(dt.Rows[i]["TableID"].ToString());

            return tableIds;
        }        
    }
}
