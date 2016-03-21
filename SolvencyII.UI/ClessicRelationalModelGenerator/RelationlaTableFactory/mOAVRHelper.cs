using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.RelationlaTableFactory
{
    internal class mOAVRHelper
    {
        internal mDimension findDimension(mOpenAxisValueRestriction moavr)
        {
            List<mOrdinateCategorisation> ctegs = (from o in moavr.mAxi.mAxisOrdinates
                                                   from oc in o.mOrdinateCategorisations
                                                   select oc).ToList();
            if (ctegs.Count == 1)
                return ctegs[0].mDimension;

            throw new NullReferenceException("No dimension found");
        }

        internal HashSet<mMember> findMembers(mOpenAxisValueRestriction moavr)
        {
            HashSet<mMember> members = new HashSet<mMember>();

            mHierarchy hierarchy = findHierarchy(moavr);
            mHierarchyNode startingNode = findStartingMember(hierarchy, Convert.ToInt64(moavr.HierarchyStartingMemberID));
            if (startingNode != null) members = getMembersTree(startingNode);
            else members = new HashSet<mMember>(hierarchy.mHierarchyNodes.Select(x => x.mMember).Where(x => x != null));

            if (moavr.IsStartingMemberIncluded != null && moavr.IsStartingMemberIncluded == true)
                members.Add(startingNode.mMember);

            return members;
        }

        private HashSet<mMember> getMembersTree(mHierarchyNode startingNode)
        {
            HashSet<mHierarchyNode> childNodes = new HashSet<mHierarchyNode>((from hn in startingNode.mHierarchy.mHierarchyNodes
                                                                              where hn.ParentMemberID == startingNode.MemberID
                                                                              select hn));

            HashSet<mMember> members = new HashSet<mMember>((from hn in childNodes
                                                             select hn.mMember).ToList());

            foreach (mHierarchyNode chn in childNodes)
                foreach (mMember member in getMembersTree(chn))
                    members.Add(member);

            return members;

        }

        private mHierarchyNode findStartingMember(mHierarchy hierarchy, long memberId)
        {
            foreach (mHierarchyNode hn in hierarchy.mHierarchyNodes.OrderBy(x => x.Level).ThenBy(x => x.Order))
            {
                if (hn.MemberID == memberId || hn.MemberID == 0 - memberId)
                    return hn;
            }

            return null;
        }

        private mHierarchy findHierarchy(mOpenAxisValueRestriction moavr)
        {
            //mHierarchy hierarchy = moavr.mHierarchy;
            mHierarchy hierarchy = null;

            if (hierarchy == null)
                hierarchy = (from o in moavr.mAxi.mAxisOrdinates
                             from c in o.mOrdinateCategorisations
                             from h in c.mDimension.mDomain.mHierarchies
                             where h.HierarchyID == moavr.HierarchyID
                             select h).FirstOrDefault();
            
            if (hierarchy == null)
                throw new NullReferenceException("Cannot find hierarchy for axis " + moavr.AxisID);

            return hierarchy;
        }
    }
}
