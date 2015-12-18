using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SolvencyII.Metrics.Domain;

namespace SolvencyII.Metrics
{
    public class MetricsEnumerationExtractor
    {
        private DPMdb dpmDb;
        Dictionary<string, mMetric> codeToEnumerationMetric = null;
        Dictionary<string, mMetric> CodeToEnumerationMetric
        {
            get
            {
                if (codeToEnumerationMetric == null)
                {
                    codeToEnumerationMetric = new Dictionary<string, mMetric>();
                    openClose();

                    foreach (mMetric met in dpmDb.mMetrics)
                        if (met.ReferencedHierarchyID != null)
                            codeToEnumerationMetric.Add(met.mMember.MemberCode, met);
                    
                    openClose();
                }

                return codeToEnumerationMetric;
            }
        }

        AT2DPM.DAL.DPMdbConnection conn = null;
        private string filePath;

        public MetricsEnumerationExtractor(DPMdb DpmDb)
        {
            this.dpmDb = DpmDb;
        }

        public MetricsEnumerationExtractor(string filePath)
        {
            conn = new AT2DPM.DAL.DPMdbConnection();
            this.filePath = filePath;
        }

        public bool IsMetricEnumeration(string metricCode)
        {
            return CodeToEnumerationMetric.ContainsKey(metricCode);
        }

        public Dictionary<string, string> getHierarchyQnamesToText(long? hierarchyId, long? startingMemberId = null, bool? isStartingMemberIncluded = null)
        {
            openClose();
            List<MetricComboBoxItem> items = getComboBoxItemsForMetric(hierarchyId, startingMemberId, isStartingMemberIncluded);

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in items)
                GetDictionaryFromItems(item, result);

            openClose(); 
            return result;            
        }

        public List<HierarchyAndMemberInfo> getHierarchyQnamesToTextLst(long hierarchyId, long? startingMemberId = null, bool? isStartingMemberIncluded = null)
        {
            openClose();
            List<MetricComboBoxItem> items = getComboBoxItemsForMetric(hierarchyId, startingMemberId, isStartingMemberIncluded);

            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var item in items)
                GetDictionaryFromItems(item, result);

            List<HierarchyAndMemberInfo> info = new List<HierarchyAndMemberInfo>();


            foreach (var item in result)
            {
                //HierarchyAndMemberInfo obj=new HierarchyAndMemberInfo();
                info.Add(new HierarchyAndMemberInfo { HierarchyID = hierarchyId, MemberLabel = item.Value, MemberXBRLCode = item.Key });

            }

            openClose();
            return info;
        }


        private void openClose()
        {
            if (conn == null)
                return;
            if (dpmDb == null)
                dpmDb = conn.OpenDpmConnection(this.filePath);
            else
            {
                conn.CloseActiveDatabase();
                dpmDb = null;
            }
        }

        private void GetDictionaryFromItems(MetricComboBoxItem item, Dictionary<string, string> result)
        {
            if (!result.ContainsKey(item.member.MemberXBRLCode))
                result.Add(item.member.MemberXBRLCode, item.member.MemberLabel);

            foreach (MetricComboBoxItem child in item.children)
                if (!result.ContainsKey(child.member.MemberXBRLCode))
                    GetDictionaryFromItems(child, result);
        }

        public List<MetricComboBoxItem> getComboBoxItemsForMetric(long? hierarchyId, long? startingMemberId = null, bool? isStartingMemberIncluded = null)
        {
            //openClose();
            mHierarchy hierarchy = dpmDb.mHierarchies.FirstOrDefault(x=>x.HierarchyID == hierarchyId);
            if(hierarchy == null) return new List<MetricComboBoxItem>();

            HashSet<MetricComboBoxItem> result = new HashSet<MetricComboBoxItem>();

            if (startingMemberId == null)
                foreach (mHierarchyNode hn in hierarchy.mHierarchyNodes.Where(x => x.ParentMemberID == null))
                    result = new HashSet<MetricComboBoxItem>(result.Union(readComboBoxItems(hn, isStartingMemberIncluded)));
            else
                result = new HashSet<MetricComboBoxItem>(readComboBoxItems(hierarchy.mHierarchyNodes.First(x => x.MemberID.Equals(startingMemberId))
                    , isStartingMemberIncluded));
            
            //openClose();
            return result.ToList();
        }

        public List<MetricComboBoxItem> getComboBoxItemsForMetric(string metricCode)
        {
            mMetric metric;
            if (!CodeToEnumerationMetric.TryGetValue(metricCode, out metric))            
                return new List<MetricComboBoxItem>();

            //openClose();
            
            var result = getComboBoxItemsForMetric(metric.ReferencedHierarchyID, metric.HierarchyStartingMemberID, metric.IsStartingMemberIncluded);

            //openClose();
            return result;
        }

        private IEnumerable<MetricComboBoxItem> readComboBoxItems(mHierarchyNode hn, bool? isIncluded, MetricComboBoxItem parent = null)
        {
            //openClose();

            List<MetricComboBoxItem> result = new List<MetricComboBoxItem>();
            MetricComboBoxItem newItem = null;

            if(isIncluded == null || (bool)isIncluded)
            {
                newItem = new MetricComboBoxItem();
                newItem.level = hn.Level;
                newItem.order = hn.Order;
                newItem.isAbstract = (hn.IsAbstract == null || (bool)hn.IsAbstract);

                newItem.member = hn.mMember;
                if (parent != null)
                    parent.AddChild(newItem);
                else
                    result.Add(newItem);
            }

            foreach (mHierarchyNode childHN in hn.mHierarchy.mHierarchyNodes.Where(x=>x.ParentMemberID.Equals(hn.MemberID)).OrderBy(x=>x.Order))
                result = result.Union(readComboBoxItems(childHN, true, newItem)).ToList();

            //openClose();
            return new HashSet<MetricComboBoxItem>(result);
        }
    }

    public class MetricComboBoxItem
    {
        public mMember member;
        public MetricComboBoxItem parent;
        public long? order;
        public long? level;
        public bool isAbstract;
        public HashSet<MetricComboBoxItem> children = new HashSet<MetricComboBoxItem>();

        internal void AddChild(MetricComboBoxItem newItem)
        {
            newItem.parent = this;
            this.children.Add(newItem);
        }

        public override string ToString()
        {
 	         return string.Format("{0} {1}" , new String(' ', Convert.ToInt16(level-1)), member.MemberLabel);
        }        

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            if (!(obj is MetricComboBoxItem)) return false;
            return (obj as MetricComboBoxItem).member.MemberXBRLCode.Equals(this.member.MemberXBRLCode);
        }

        public override int GetHashCode()
        {
            return this.member.MemberXBRLCode.GetHashCode();
        }
    }
}
