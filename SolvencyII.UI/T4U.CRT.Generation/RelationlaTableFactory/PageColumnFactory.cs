using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using DpmDB;
using T4U.CRT.Generation.Model;
using AT2DPM.DAL;
using AT2DPM.DAL.ExtendedModel;
using AT2DPM.DAL.Model;

namespace T4U.CRT.Generation.RelationlaTableFactory
{
    public class PageColumnFactory : IRelationalColumnFactory
    {
        private readonly mDimension _dimension;
        private readonly HashSet<mMember> _members;
        private readonly string _colIndex;

        private PageColumn column;
        private bool _forceIsInTable = false;

        public PageColumnFactory(mDimension dimension, HashSet<mMember> members, string colIndex)
        {
            if (dimension == null)
                throw new ArgumentNullException("No dimension");
            
            this._dimension = dimension;
            this._members = members;
            this._colIndex = colIndex;
        }

        public PageColumnFactory(mDimension mDimension, HashSet<mMember> members, string colIndex, bool forceIsInTable)
        {
            this._dimension = mDimension;
            this._members = members;
            this._colIndex = colIndex;
            this._forceIsInTable = forceIsInTable;
        }

        public IRelationalColumn getColumn()
        {
            column = new PageColumn();
                        
            column.dimCode = _dimension.DimensionXBRLCode;
            column.index = _colIndex;
            column.addDomCode(_dimension.mDomain.DomainXBRLCode, _dimension.DimensionXBRLCode);

            foreach (mMember mem in this._members.Where(x=>x!= null))
                if(!string.IsNullOrEmpty(mem.MemberXBRLCode)) 
                    column.memCodes.Add(mem.MemberXBRLCode);

            if (_forceIsInTable) 
                column.forceIsInTable();

            return column;
        }
    }
}
