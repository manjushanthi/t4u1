using AT2DPM.DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T4U.CRT.Generation.RelationlaTableFactory
{
    class DimCodePageNamefactory : IPageColumnNameFactory
    {
        string IPageColumnNameFactory.PageColumnIdx(AT2DPM.DAL.Model.mOrdinateCategorisation oc)
        {
            return constructPageCode(oc.mDimension);
        }        

        string IPageColumnNameFactory.PageColumnIdx(AT2DPM.DAL.Model.mOpenAxisValueRestriction moavr)
        {
            return constructPageCode(moavr.mAxi.mAxisOrdinates.Single().mOrdinateCategorisations.Single().mDimension);
        }

        private string constructPageCode(mDimension mDimension)
        {
            string prefix = mDimension.mConcept.mOwner.OwnerPrefix;
            string dimCode = mDimension.DimensionCode;

            return string.Format("{0}_{1}", prefix, dimCode);
        }
    }
}
