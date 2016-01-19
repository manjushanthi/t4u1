using System;
using SolvencyII.Data.CRT.ETL.DataConnectors;
using SolvencyII.Data.CRT.ETL.DBcontrollers.Extraction;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SolvencyII.Data.CRT.ETL
{
    public class QuickFillingIndicatorsExtractor : IFilinglIndicatorsExtractor
    {
        IDataConnector _dataConnector;
        ITableNamesExtractor _namesExtractor;

        public QuickFillingIndicatorsExtractor(IDataConnector dataConnector)
        {
            _dataConnector = dataConnector;
            _namesExtractor = new TableNamesExtractor(dataConnector);
        }

        string[] IFilinglIndicatorsExtractor.getTablesNamesFromFillingIndicators(int instnanceId)
        {
            try
            {
                HashSet<int> potentialTableIds = getPotentialTableIDsFromFillingIndicators(instnanceId);
                potentialTableIds = filterTableIdsByModule(potentialTableIds, instnanceId);
                return this.getTableNames(getTableNames(potentialTableIds));
            }
            catch (Exception ex)
            {
                throw new EtlException("Exception during reading filling indicators for instance " + instnanceId, ex);
            }
        }
        
        private HashSet<int> filterTableIdsByModule(HashSet<int> potentialTableIds, int instnanceId)
        {
            string query = @"select t.TableID, t.TableCode
from mTable t
inner join (select totv.TemplateOrTableCode from dInstance i
inner join mModuleBusinessTemplate mbt on mbt.ModuleID = i.ModuleID
inner join mTemplateOrTable totv on totv.TemplateOrTableID = mbt.BusinessTemplateID
where i.InstanceID = @instnanceId) e on t.TableCode like e.TemplateOrTableCode || '%'";

            IDbCommand comm = _dataConnector.createCommand();
            comm.CommandText = query;
            IDbDataParameter param = comm.CreateParameter();
            param.ParameterName = "@instnanceId";
            param.Value = instnanceId;
            comm.Parameters.Add(param);

            DataTable dt = _dataConnector.executeQuery(comm);
            HashSet<int>  modTabIds = new HashSet<int>();
            foreach (DataRow dr in dt.Rows)
                modTabIds.Add(int.Parse(dr["t.TableID"].ToString())); 
            

            return new HashSet<int>(potentialTableIds.Where(x => modTabIds.Contains(x)));
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
        

        string[] IFilinglIndicatorsExtractor.getTablesNamesFromModule(int instanceID)
        {
            try
            {
                HashSet<int> potentialTableIds = getPotentialTableIDsFromModule(instanceID);
                potentialTableIds = filterTableIdsByModule(potentialTableIds, instanceID);
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

        int[] IFilinglIndicatorsExtractor.getTablesIDsFromFillingIndicators(int instanceID)
        {
            HashSet<int> potentialTableIds = getPotentialTableIDsFromFillingIndicators(instanceID);
            int[] tableIds = getTableIDs(potentialTableIds);
            return tableIds;
        }

        int[] IFilinglIndicatorsExtractor.getTablesIDsFromModule(int instanceID)
        {
            HashSet<int> potentialTableIds = getPotentialTableIDsFromModule(instanceID);
            int[] tableIds = getTableIDs(potentialTableIds);
            return tableIds;
        }

        private string[] getTableNames(HashSet<int> potentialTableIds)
        {
            return _namesExtractor.getTableNames(potentialTableIds);
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

        List<mTaxTable> _mTaxTables;
        List<mTaxTable> mTaxTables
        {
            get
            {
                if (_mTaxTables != null)
                    return _mTaxTables;

                string query = @"select TemplateOrTableID, ParentTemplateOrTableID, TemplateOrTableType, IFNULL(TableID, 'NULL') TableID
                            from mTemplateOrTable tt 
                                left join mTaxonomyTable taxt 
                                    on (taxt.AnnotatedTableID = tt.TemplateOrTableID and taxt.TaxonomyID = tt.TaxonomyID)";

                _mTaxTables = new List<mTaxTable>();
                DataTable dt = _dataConnector.executeQuery(query);
                mTaxTable tt;
                foreach (DataRow dr in dt.Rows)
                {
                    tt = new mTaxTable()
                    {
                        TemplateOrTableID = getInt(dr["TemplateOrTableID"].ToString()),
                        ParentTemplateOrTableID = getInt(dr["ParentTemplateOrTableID"].ToString()),
                        TableID = getInt(dr["TableID"].ToString())
                    };
                    tt.setTemplateOrTableType(dr["TemplateOrTableType"].ToString());
                    _mTaxTables.Add(tt);
                }

                _mTaxTables.ForEach(x => x.findChildren(_mTaxTables));

                return _mTaxTables;
            }
        }

        private int? getInt(string strValue)
        {
            if (string.IsNullOrEmpty(strValue) || strValue.Trim().ToUpper().Equals("NULL"))
                return null;

            int result;
            if (int.TryParse(strValue, out result))
                return result;

            return null;
        }

        private int[] getTableIDs(HashSet<int> potentialTableIds)
        {
            return mTaxTables
                .Select(x => x.TableID)
                .Distinct()
                .Where(x => x!=null && potentialTableIds.Contains((int)x))
                .Select(x=>(int)x).ToArray();
        }

        private IEnumerable<int> getTablesIds(int tabVarId)
        {
            foreach (mTaxTable tt in mTaxTables)
                if (tt.TemplateOrTableID != null
                    && ((int)tt.TemplateOrTableID).Equals(tabVarId))
                    return tt.getTablesFromSubtree();

            return new HashSet<int>();
        }

        private class mTaxTable
        {
            public mTaxTable parent;
            public HashSet<mTaxTable> children = new HashSet<mTaxTable>();

            public int? TemplateOrTableID;
            public int? ParentTemplateOrTableID;
            public mTempTableType templateOrTableType;
            public int? TableID;            

            public void setTemplateOrTableType(string value)
            {
                switch (value)
                {
                    case "TemplatesGroup":
                        this.templateOrTableType = mTempTableType.TemplatesGroup;
                        break;
                    case "Template":
                        this.templateOrTableType = mTempTableType.Template;
                        break;
                     case "TemplateVariant":
                        this.templateOrTableType = mTempTableType.TemplateVariant;
                        break;
                     case "BusinessTable":
                        this.templateOrTableType = mTempTableType.BusinessTable;
                        break;
                     case "AnnotatedTable":
                        this.templateOrTableType = mTempTableType.AnnotatedTable;
                        break;
                    default:
                        break;
                }
            }

            internal void findChildren(List<mTaxTable> _mTaxTables)
            {
                this.children.Clear();

                if (this.TemplateOrTableID == null)
                    throw new ArgumentNullException("No tempplate or table id specified");

                foreach (mTaxTable tt in _mTaxTables)
                {
                    if (tt.ParentTemplateOrTableID != null
                        && tt.ParentTemplateOrTableID.Equals(this.TemplateOrTableID))
                    {
                        this.children.Add(tt);
                        tt.parent = this;
                    }
                }
            }

            internal IEnumerable<int> getTablesFromSubtree()
            {
                List<int> tabIds = new List<int>();
                if (this.templateOrTableType.Equals(mTempTableType.AnnotatedTable))
                    tabIds.Add((int)this.TableID);

                if (this.children != null && this.children.Count > 0)
                    foreach (mTaxTable child in this.children)
                        tabIds = tabIds.Union(child.getTablesFromSubtree()).ToList();

                return new HashSet<int>(tabIds);
            }
        }

        private enum mTempTableType
        {
            TemplatesGroup,
            Template,
            TemplateVariant,
            BusinessTable,
            AnnotatedTable
        }

    }    
}
